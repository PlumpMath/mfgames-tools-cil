using System;
using System.Collections.Generic;
using MfGames.Tools.Cli.Reader;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Implements a command-line argument parser that takes an argument set and
	/// separates the various components into individual parameters and optional
	/// arguments. This handles both long and short options while consolidating the
	/// results down to a single field. It also manages repeated arguments and
	/// validation of results.
	/// 
	/// This differs from <see cref="ArgumentReader">ArgumentReader</see> in that
	/// the reader processes the arguments one at a time while this one manages all
	/// of the arguments and produces a combined, non-iterative view.
	/// 
	/// The parser will produce the same results for "param1 --option1 param2" as
	/// "param1 param2 --option1" and "--option1 param1 param2".
	/// </summary>
	public class ArgumentParser
	{
		private string[] arguments;
		private ArgumentSettings settings;

		/// <summary>
		/// Initializes a new instance of the <see cref="ArgumentParser"/> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="System.ArgumentNullException">
		/// settings
		/// or
		/// arguments
		/// </exception>
		public ArgumentParser(
			ArgumentSettings settings,
			string[] arguments,
			bool automaticallyParse = true)
		{
			// Make sure our input arguments are sane.
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}

			// Save the values for processing in the Read() method.
			this.settings = settings;
			this.arguments = arguments;

			// Populate collections.
			parameters = new List<string>();
			optionals = new Dictionary<string, ArgumentReference>();
			unknown = new List<ArgumentReference>();

			// If we should be automatically parsing, do so.
			if (automaticallyParse)
			{
				Parse();
			}
		}

		public void Parse()
		{
			// Use an ArgumentReader to parse through the properties, figuring out what
			// to do with each one as we get it.
			var reader = new ArgumentReader(settings, arguments);

			while (reader.Read())
			{
				// The argument type is used to figure out how we'll be parsing this.
				switch (reader.ReaderArgumentType)
				{
					case ReaderArgumentType.Parameter:
						// For parameters, we just add it to the list.
						parameters.Add(reader.Key);
						break;

					case ReaderArgumentType.LongOption:
					case ReaderArgumentType.ShortOption:
						// For optional arguments, we need to populate the argument
						// reference list and update any values.
						ParseOptionalArgument(reader);
						break;
				}
			}
		}

		private void ParseOptionalArgument(ArgumentReader reader)
		{
			// Start by finding the argument reference that this optional. This will
			// either be a known argument or the key itself if one cannot be found.
			Argument argument = GetOptionalArgumentKey(reader.Key);

			if (argument == null)
			{
				// This was an unknown argument and we don't allow that. So break out
				// if the function since there is nothing else we can do.
				return;
			}

			// See if we have an argument reference for this key already.
			ArgumentReference reference;
			bool found = optionals.TryGetValue(
				argument.Key,
				out reference);
			
			if (!found)
			{
				// We couldn't find it, so create a new one.
				reference = new ArgumentReference(argument);
				optionals[argument.Key] = reference;
			}

			// Increment the reference counter.
			reference.ReferenceCount++;

			// Check to see if we have a parameter associated with this argument.
		}


		private Argument GetOptionalArgumentKey(string key)
		{
			// Look up the argument definitions inside the settings. If we can't find it,
			// then just return the key since it is an unknown value.
			Argument argument;

			bool found = settings.Arguments.TryGet(
				key,
				out argument);

			if (found)
			{
				return argument;
			}

			// Create a placeholder argument for this one.
			argument = CreatePlaceholderArgument(key);

			// Check to see if the settings allow for unknown arguments.
			if (settings.IncludeUnknownArguments)
			{
				// If we can't find it, use the placeholder argument to represent the
				// optional argument.
				return argument;
			}

			// We have an unknown argument and they aren't allowed. Create an
			// argument reference so we can report it and then add it to the unkown
			// argument list. Then, we return null to indicate that this argument
			// should be skipped.
			var unknownReference = new ArgumentReference(argument);

			unknown.Add(unknownReference);

			return null;
		}

		protected virtual Argument CreatePlaceholderArgument(string key)
		{
			var argument = new Argument
			{
				Key = key
			};
			return argument;
		}

		public int ParameterCount
		{
			get { return parameters.Count; }
		}

		public int OptionalCount
		{
			get { return optionals.Count; }
		}

		public List<string> Parameters
		{
			get { return parameters; }
		}

		public Dictionary<string, ArgumentReference> Optionals
		{
			get { return optionals; }
		}

		private List<string> parameters;
		private Dictionary<string, ArgumentReference> optionals;
		private List<ArgumentReference> unknown;
	}
}

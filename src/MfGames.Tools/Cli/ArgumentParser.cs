using System;
using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Encapsulates the logic for parsing command-line arguments into an ordered list
	/// of non-optional arguments (typically those without a slash or dash in front of
	/// the argument) and the optional arguments (those with a slash or dash).
	/// </summary>
	public class ArgumentParser
	{
		private Dictionary<string, List<string>> options;
		private List<string> parameters;

		/// <summary>
		/// Gets or sets the stop processing options argument. If this is encountered
		/// as the sole string in the argument, then nothing else will be treated as
		/// an option even if it starts with one of the option prefixes.
		/// </summary>
		public string StopProcessingOptionsArgument { get; set; }

		/// <summary>
		/// Gets or sets the long argument prefix. This is the prefix string that
		/// identifies a long (or verbose) option from a normal one. If this is
		/// null or blank, then no long arguments will be parsed.
		/// </summary>
		public string LongOptionPrefix { get; set; }

		/// <summary>
		/// Gets or sets the short argument prefix. This is the prefix string that
		/// identifies a short option from a command-line argument. If this is null
		/// or blank, then no short arguments will be parsed.
		/// </summary>
		public string ShortOptionPrefix { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether short arguments can be bundled.
		/// If this is true, then "-abc" is the same as "-a -b -c".
		/// </summary>
		public bool AllowShortOptionBundling { get; set; }

		/// <summary>
		/// Gets the number of parameter found after parsing.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Cannot retrieve the argument count before Parse() is called.</exception>
		public int ParameterCount
		{
			get
			{
				// If we don't have any parameters, we tell the user that we're
				// in an unknown state.
				if (parameters == null)
				{
					throw new InvalidOperationException(
						"Cannot retrieve the argument count before Parse() is called.");
				}

				// Return the resulting count.
				return parameters.Count;
			}
		}

		/// <summary>
		/// Gets the number of options after parsing the arguments.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Cannot retrieve the option count before Parse() is called.</exception>
		public int OptionCount
		{
			get
			{
				// If we don't have any options, we tell the user that we're
				// in an unknown state.
				if (options == null)
				{
					throw new InvalidOperationException(
						"Cannot retrieve the option count before Parse() is called.");
				}

				// Return the resulting count.
				return options.Count;
			}
		}

		/// <summary>
		/// Parses the specified arguments and creates the parameters and options
		/// based on the settings in this class.
		/// </summary>
		/// <param name="arguments">The arguments to parse.</param>
		public void Parse(string[] arguments)
		{
			// Set up the collections we'll be populating.
			parameters = new List<string>();
			options = new Dictionary<string, List<string>>();

			// Loop through the arguments one-by-one.
			bool stopOptionProcessing = false;

			foreach (string argument in arguments)
			{
				// If we are done processing options, we don't do anything else.
				if (!stopOptionProcessing)
				{
					// Check for the stop processing argument.
					if (!string.IsNullOrEmpty(StopProcessingOptionsArgument)
						&& argument == StopProcessingOptionsArgument)
					{
						stopOptionProcessing = true;
						continue;
					}

					// Check to see if the argument matches a long argument pattern.
					string longPrefix = LongOptionPrefix;

					if (!string.IsNullOrEmpty(longPrefix)
						&& argument.StartsWith(longPrefix))
					{
						string option = argument.Substring(longPrefix.Length);
						ProcessLongOption(option);
						continue;
					}

					// Check to see if we have a short option prefix on the argument.
					string shortPrefix = ShortOptionPrefix;

					if (!string.IsNullOrEmpty(shortPrefix)
						&& argument.StartsWith(shortPrefix))
					{
						string option = argument.Substring(shortPrefix.Length);
						ProcessShortOption(option);
						continue;
					}
				}

				// If we get this far, then everything is considered a parameter so
				// add it to the list.
				parameters.Add(argument);
			}
		}

		/// <summary>
		/// Gets the options from the parsed value.
		/// </summary>
		public IDictionary<string, List<string>> Options
		{
			get
			{
				// If we don't have any options, we tell the user that we're
				// in an unknown state.
				if (options == null)
				{
					throw new InvalidOperationException(
						"Cannot retrieve the options before Parse() is called.");
				}

				// Return a "read-only" collection of the options.
				var copiedOptions = new Dictionary<string, List<string>>(options);
				return copiedOptions;
			}
		}

		public IList<string> Parameters
		{
			get
			{
				// If we don't have any parameters, we tell the user that we're
				// in an unknown state.
				if (parameters == null)
				{
					throw new InvalidOperationException(
						"Cannot retrieve the parameters before Parse() is called.");
				}

				// Create a read-only list of the parameters.
				var copiedParameters = new List<string>(parameters);
				return copiedParameters;
			}
		}

		private void ProcessShortOption(string option)
		{
			// Check to see if this option is already in the dictionary. If it isn't,
			// then create an empty list in the options list.
			if (!options.ContainsKey(option))
			{
				options[option] = new List<string>();
			}
		}

		private void ProcessLongOption(string option)
		{
			// Check to see if this option is already in the dictionary. If it isn't,
			// then create an empty list in the options list.
			if (!options.ContainsKey(option))
			{
				options[option] = new List<string>();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using MfGames.Tools.Cli.Reader;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A command line interface (CLI) argument parser implemented as a state-based
	/// reader. This is normally used by CliArgumentParser, but can be used directly
	/// when the order of parameters is imporant, such as how ImageMagick convert
	/// operates.
	/// </summary>
	public class ArgumentReader
	{
		private readonly ArgumentSettings settings;

		/// <summary>
		/// Contains the index into arguments array for the current position.
		/// </summary>
		private int nextArgumentIndex;

		private string[] arguments;

		public ArgumentReader(
			ArgumentSettings settings,
			string[] arguments)
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
		}

		/// <summary>
		/// Gets the key for the current argument. This will be the name of the option
		/// or the value of the parameter.
		/// </summary>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the values parsed with the current argument. If there are no values,
		/// this will be null.
		/// </summary>
		public List<string> Values { get; private set; }

		/// <summary>
		/// Gets the type of the current argument.
		/// </summary>
		public ReaderArgumentType ReaderArgumentType { get; private set; }

		/// <summary>
		/// Resets the various fields to a netural state for populating.
		/// </summary>
		private void Reset()
		{
			Key = null;
			Values = null;
			ReaderArgumentType = ReaderArgumentType.None;
		}

		/// <summary>
		/// Reads the next argument component and sets the internal state to describe
		/// it. If there are no such arguments, this returns false to indicate the
		/// end of processing.
		/// </summary>
		/// <returns></returns>
		public bool Read()
		{
			// We always reset our internal state before processing.
			Reset();

			// If we have short options, then process those instead.
			if (!string.IsNullOrEmpty(currentShortOptions))
			{
				bool results = ParseShortOption();
				return results;
			}

			// Figure out which argument we'll be processing. If this returns null, then
			// we have nothing else to retrieve.
			string argument = GetNextArgument();

			if (argument == null)
			{
				return false;
			}

			// If we are still processing options, check them first.
			if (!stopProcessing)
			{
				// First check to see if we got the stop processing argument.
				if (settings.IsStopProcessing(argument))
				{
					// Indicate we are no longer processing and move to the next full
					// argument in the array.
					stopProcessing = true;
					nextArgumentIndex++;

					// Call this method again because we skip over the stop.
					bool innerReadResults = Read();
					return innerReadResults;
				}

				// Check to see if we have a long option.
				string longOption;
				string longValue;

				settings.GetLongOption(argument, out longOption, out longValue);

				if (longOption != null)
				{
					// Advance the option to the next argument.
					nextArgumentIndex++;

					// Populate the internal state of the long option. This can
					// potentially advance the nextArgumentIndex further.
					bool results = ParseLongOption(longOption, longValue);
					return results;
				}

				// Check to see if we have a short option.
				string shortOptions = settings.GetShortOptions(argument);

				if (shortOptions != null)
				{
					// Set the short options in the parser.
					currentShortOptions = shortOptions;

					// Populate the internal state of the long option. This can
					// potentially advance the nextArgumentIndex further.
					bool results = ParseShortOption();
					return results;
				}
			}

			// If we got this far, it's a parameter.
			ReaderArgumentType = ReaderArgumentType.Parameter;
			Key = argument;
			nextArgumentIndex++;
			
			return true;
		}

		/// <summary>
		/// Parses the argument as a long option.
		/// </summary>
		/// <param name="option">The argument.</param>
		/// <param name="valuealue"></param>
		/// <returns>True if successfull parsed, otherwise false.</returns>
		private bool ParseLongOption(
			string option,
			string value)
		{
			// Set the argument type to long option.
			ReaderArgumentType = ReaderArgumentType.LongOption;

			// Set the key value with what's left in the argument.
			Key = option;

			// Using the key, pull out the definition of the argument. If there is none,
			// them create a default argument.
			Argument definition = settings.Arguments[Key];

			// If the definition requires values, we need to populate them.
			if (definition.ValueCount > 0)
			{
				// Populate the list of values.
				int missingValues = definition.ValueCount;

				Values = new List<string>();

				// If we have a value, we use that.
				if (!string.IsNullOrEmpty(value))
				{
					Values.Add(value);
					missingValues--;
				}

				// If we still have a required parameter, we need to pull elements down.
				while (missingValues > 0)
				{
					string nextValue = GetNextArgument();
					Values.Add(nextValue);
					nextArgumentIndex++;
					missingValues--;
				}
			}

			// Indicate that we successfully parsed it.
			return true;
		}

		/// <summary>
		/// Parses the argument as a short option bundle.
		/// </summary>
		/// <returns>
		/// True if successfull parsed, otherwise false.
		/// </returns>
		private bool ParseShortOption()
		{
			// Set the argument type to long option.
			ReaderArgumentType = ReaderArgumentType.ShortOption;

			// The first character is the parameter.
			Key = currentShortOptions[0].ToString();

			// Trim off the character we just put into the key.
			currentShortOptions = currentShortOptions.Substring(1);

			// Using the key, pull out the definition of the argument. If there is none,
			// them create a default argument.
			Argument definition = settings.Arguments[Key];

			// If the definition requires values, we need to populate them.
			if (definition.ValueCount > 0)
			{
				// Populate the list of values.
				Values = new List<string>();

				// Figure out how many values we need.
				int missingValues = definition.ValueCount;

				// If we have values and there is still a currentShortOptions, pull it off.
				if (missingValues > 0 && !string.IsNullOrEmpty(currentShortOptions))
				{
					Values.Add(currentShortOptions);
					missingValues--;
					currentShortOptions = "";
				}

				// If we have a required parameter, we need to pull elements down.
				while (missingValues > 0)
				{
					nextArgumentIndex++;
					string value = GetNextArgument();
					Values.Add(value);
					missingValues--;
				}
			}

			// If we got this far and we don't have any more short options, we move
			// to the next line.
			if (string.IsNullOrEmpty(currentShortOptions))
			{
				nextArgumentIndex++;
			}

			// Indicate that we successfully parsed it.
			return true;
		}

		/// <summary>
		/// Gets the next argument.
		/// </summary>
		/// <returns></returns>
		private string GetNextArgument()
		{
			// Find out if the next processed argument is out of bounds. We don't advance
			// the pointers because it is dependent on the state (bundled short options
			// advance in a different manner than the rest).
			if (nextArgumentIndex >= arguments.Length)
			{
				// No more arguments to process.
				return null;
			}

			// Grab the value we'll be processing.
			string argument = arguments[nextArgumentIndex];
			return argument;
		}

		/// <summary>
		/// If this is set, then option processing will be suspended.
		/// </summary>
		private bool stopProcessing;

		/// <summary>
		/// The short options currently being processed.
		/// </summary>
		private string currentShortOptions;
	}
}

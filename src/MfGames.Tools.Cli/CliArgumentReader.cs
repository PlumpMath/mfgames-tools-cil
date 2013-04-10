using System;
using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A command line interface (CLI) argument parser implemented as a state-based
	/// reader.
	/// </summary>
	public class CliArgumentReader
	{
		private readonly CliArgumentReaderSettings settings;

		/// <summary>
		/// Contains the index into arguments array for the current position.
		/// </summary>
		private int nextArgumentIndex;

		private string[] arguments;

		public CliArgumentReader(
			CliArgumentReaderSettings settings,
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
		public CliArgumentType CliArgumentType { get; private set; }

		/// <summary>
		/// Resets the various fields to a netural state for populating.
		/// </summary>
		private void Reset()
		{
			Key = null;
			Values = null;
			CliArgumentType = CliArgumentType.None;
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
				string longOption = settings.GetLongOption(argument);

				if (longOption != null)
				{
					// Advance the option to the next argument.
					nextArgumentIndex++;

					// Populate the internal state of the long option. This can
					// potentially advance the nextArgumentIndex further.
					bool results = ParseLongOption(longOption);
					return results;
				}
			}

			// If we got this far, it's a parameter.
			CliArgumentType = CliArgumentType.Parameter;
			Key = argument;
			nextArgumentIndex++;
			
			return true;
		}

		/// <summary>
		/// Parses the argument as a long option.
		/// </summary>
		/// <param name="argument">The argument.</param>
		/// <returns>True if successfull parsed, otherwise false.</returns>
		private bool ParseLongOption(string argument)
		{
			// Set the argument type to long option.
			CliArgumentType = CliArgumentType.LongOption;

			// Set the key value with what's left in the argument.
			Key = argument;

			// Using the key, pull out the definition of the argument. If there is none,
			// them create a default argument.
			ICliArgumentReaderArgument definition = settings.Arguments[Key];

			// If the definition requires values, we need to populate them.
			if (definition.ValueCount > 0)
			{
				// Populate the list of values.
				Values = new List<string>();

				// If we have a required parameter, we need to pull elements down.
				int missingValues = definition.ValueCount;

				while (missingValues > 0)
				{
					string value = GetNextArgument();
					Values.Add(value);
					nextArgumentIndex++;
					missingValues--;
				}
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
	}
}

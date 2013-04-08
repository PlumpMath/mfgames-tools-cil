using System;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A command line interface (CLI) argument parser implemented as a state-based
	/// reader.
	/// </summary>
	public class CliArgumentReader
	{
		private readonly string[] arguments;

		/// <summary>
		/// Initializes a new instance of the <see cref="CliArgumentReader"/> class.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		public CliArgumentReader(string[] arguments)
		{
			// Make sure our input arguments are sane.
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}

			// Save the arguments and set up the internal states for reading.
			this.arguments = arguments;
		}

		/// <summary>
		/// Reads the next argument component and sets the internal state to describe
		/// it. If there are no such arguments, this returns false to indicate the
		/// end of processing.
		/// </summary>
		/// <returns></returns>
		public bool Read()
		{
			throw new NotImplementedException();
		}
	}
}
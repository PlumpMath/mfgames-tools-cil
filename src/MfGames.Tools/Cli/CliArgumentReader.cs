using System;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A command line interface (CLI) argument parser implemented as a state-based
	/// reader.
	/// </summary>
	public class CliArgumentReader
	{
		private string[] arguments;

		public CliArgumentReader(string[] arguments)
		{
			// Make sure our input arguments are sane.
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}

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
			throw new InvalidOperationException();
		}
	}
}
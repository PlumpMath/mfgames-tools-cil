using System;

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
	}
}

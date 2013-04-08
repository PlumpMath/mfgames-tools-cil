using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A specialized collection for handling associations with options and their
	/// hints.
	/// </summary>
	public class OptionHintsCollection: Dictionary<string, OptionHints>
	{
		public new OptionHints this[string key]
		{
			get
			{
				if (ContainsKey(key))
				{
					return base[key];
				}

				return OptionHints.None;
			}
			set { base[key] = value; }
		}
	}
}

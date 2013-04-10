using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A collection of ICliArgumentReaderArgument along with associated retrieval
	/// methods.
	/// </summary>
	public class CliArgumentReaderArgumentCollection:
		List<ICliArgumentReaderArgument>
	{
		public ICliArgumentReaderArgument this[string name]
		{
			get
			{
				foreach (ICliArgumentReaderArgument argument in this)
				{
					if (argument.LongOptionNames.Contains(name) ||
						argument.ShortOptionNames.Contains(name))
					{
						return argument;
					}
				}

				return new CliArgument();
			}
		}
	}
}

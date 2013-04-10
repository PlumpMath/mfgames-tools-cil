using System.Collections.Generic;

namespace MfGames.Tools.Cli.Reader
{
	/// <summary>
	/// A collection of IReaderArgument along with associated retrieval
	/// methods.
	/// </summary>
	public class ReaderArgumentCollection:
		List<IReaderArgument>
	{
		public IReaderArgument this[string name]
		{
			get
			{
				foreach (IReaderArgument argument in this)
				{
					if (argument.LongOptionNames.Contains(name) ||
						argument.ShortOptionNames.Contains(name))
					{
						return argument;
					}
				}

				return new Argument();
			}
		}
	}
}

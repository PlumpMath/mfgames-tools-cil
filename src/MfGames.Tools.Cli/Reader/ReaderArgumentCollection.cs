using System.Collections.Generic;

namespace MfGames.Tools.Cli.Reader
{
	/// <summary>
	/// A collection of IReaderArgument along with associated retrieval
	/// methods.
	/// </summary>
	public class ReaderArgumentCollection:
		List<Argument>
	{
		public bool TryGet(
			string name,
			out Argument argument)
		{
			// Go through the collection of arguments and look for an argument that
			// has this key in the long or short names.
			foreach (Argument readerArgument in this)
			{
				bool isLongOption = readerArgument.LongOptionNames.Contains(name);
				bool isShortOption = readerArgument.ShortOptionNames.Contains(name);

				if (isLongOption || isShortOption)
				{
					argument = readerArgument;
					return true;
				}
			}

			// If we got out of the loop, we couldn't find it.
			argument = null;
			return false;
		}

		public Argument this[string name]
		{
			get
			{
				foreach (Argument argument in this)
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

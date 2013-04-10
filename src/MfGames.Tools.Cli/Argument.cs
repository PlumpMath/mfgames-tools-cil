using System.Collections.Generic;
using MfGames.Tools.Cli.Reader;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Describes a single argument that can be processed or read.
	/// </summary>
	public class Argument: IReaderArgument
	{
		public string Key { get; set; }
		public int ValueCount { get; set; }
		public bool HasOptionalValues { get; set; }
		public ICollection<string> LongOptionNames { get; set; }
		public ICollection<string> ShortOptionNames { get; set; }
	}
}

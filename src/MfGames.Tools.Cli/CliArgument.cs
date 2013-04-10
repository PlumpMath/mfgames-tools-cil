using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Describes a single argument that can be processed or read.
	/// </summary>
	public class CliArgument: ICliArgumentReaderArgument
	{
		public int ValueCount { get; set; }
		public bool HasOptionalValues { get; set; }
		public ICollection<string> LongOptionNames { get; set; }
		public ICollection<string> ShortOptionNames { get; set; }
	}
}

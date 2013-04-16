// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Describes a single argument that can be processed or read.
	/// </summary>
	public class Argument
	{
		#region Fields and Properties

		public bool HasOptionalValues { get; set; }
		public string Key { get; set; }
		public ICollection<string> LongOptionNames { get; set; }
		public ICollection<string> ShortOptionNames { get; set; }
		public int ValueCount { get; set; }

		#endregion
	}
}

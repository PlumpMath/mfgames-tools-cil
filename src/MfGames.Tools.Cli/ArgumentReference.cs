// Copyright 2013 Moonfire Games
// 
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Contains information about an argument that was referenced from the arguments
	/// given. This contains information about how many times it was seen, which values
	/// were given.
	/// </summary>
	public class ArgumentReference
	{
		public ArgumentReference(Argument argument)
		{
			Argument = argument;
		}

		public Argument Argument { get; private set; }
		public int ReferenceCount { get; set; }

		public string Value
		{
			get { return Values[0][0]; }
		}

		public bool HasValues
		{
			get { return Values != null; }
		}

		public List<List<string>> Values { get; private set; }

		public void AddValues(List<string> values)
		{
			// Make sure we have the collection of values.
			if (Values == null)
			{
				Values = new List<List<string>>();
			}

			// Append the values to the list.
			Values.Add(values);
		}
	}
}

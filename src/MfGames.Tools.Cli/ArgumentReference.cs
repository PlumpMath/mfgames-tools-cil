// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
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
		#region Constructors

		public ArgumentReference(Argument argument)
		{
			Argument = argument;
		}

		#endregion

		#region Methods

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

		#endregion

		#region Fields and Properties

		public Argument Argument { get; private set; }

		public bool HasValues
		{
			get { return Values != null; }
		}

		public int ReferenceCount { get; set; }

		public string Value
		{
			get { return Values[0][0]; }
		}

		public List<List<string>> Values { get; private set; }

		#endregion
	}
}

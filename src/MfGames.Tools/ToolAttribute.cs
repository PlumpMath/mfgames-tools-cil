// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System;

namespace MfGames.Tools
{
	/// <summary>
	/// Defines a class that represents a tool in the system. A tool is a generic process
	/// that can run from the command-line (CLI), a GUI, MSBuild, or another framework.
	/// Non-abstract classes that are marked with this attribute are built into invidual
	/// tasks (or whatever the framework calls those tools).
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class ToolAttribute: Attribute
	{
	}
}

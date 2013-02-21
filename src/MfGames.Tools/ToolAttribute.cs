using System;

namespace MfGames.Tools
{
	/// <summary>
	/// Defines a class that represents a tool in the system. A tool is a generic process
	/// that can run from the command-line (CLI), a GUI, MSBuild, or another framework.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
    public class ToolAttribute
    {
    }
}

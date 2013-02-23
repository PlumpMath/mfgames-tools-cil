using System.ComponentModel;
using MfGames.Tools;

namespace UnitTests
{
	[Tool]
	[ToolAlias("test-item", AliasScopes.Cli)]
	public class Class1
	{
		[ToolConstructor]
		public Class1(
			[ToolParameterAlias("required-int")]
			int requiredIntegerParameter)
		{
		}

		[ToolParameter]
		[ToolParameterAlias("optional", AliasScopes.Cli)]
		[Description("")]
		public int OptionalParameter { get; set; }
	}
}

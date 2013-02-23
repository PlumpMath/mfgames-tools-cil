using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Implements the NUnit testing for the <see cref="GetOptLongArgumentParser"/>
	/// class and its various options.
	/// </summary>
	public class GetOptLongArgumentParserTests
	{
		[Test]
		public void TestSingleParameter()
		{
			// Arrange
			string[] arguments =
			{
				"parameter",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				0,
				parser.OptionCount,
				"An unexpected number of options were found.");
		}

		[Test]
		public void TestSingleLongArgument()
		{
			// Arrange
			string[] arguments =
			{
				"--verbose",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				1,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("verbose"),
				"Could not find the 'verbose' option.");
		}

		[Test]
		public void TestSingleValueLongArgument()
		{
			// Arrange
			string[] arguments =
			{
				"--file=input-file",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				1,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("file"),
				"Could not find the 'file' option.");
		}

		[Test]
		public void TestSingleSingleArgument()
		{
			// Arrange
			string[] arguments =
			{
				"-v",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				1,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("v"),
				"Could not find the 'v' option.");
		}
	}
}
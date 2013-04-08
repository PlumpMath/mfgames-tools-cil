using System;
using System.Collections.Generic;
using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Implements the NUnit testing for the <see cref="GetOptLongArgumentParser"/>
	/// class and its various options.
	/// </summary>
	[TestFixture]
	public class GetOptLongArgumentParserTests
	{
		[Test]
		public void TestInvalidStateParameterCount()
		{
			// Arrange
			var parser = new GetOptLongArgumentParser();

			// Act
			Assert.Throws<InvalidOperationException>(
				() =>
				{ int results = parser.ParameterCount; });
		}

		[Test]
		public void TestInvalidStateParameters()
		{
			// Arrange
			var parser = new GetOptLongArgumentParser();

			// Act
			Assert.Throws<InvalidOperationException>(
				() =>
				{ IList<string> results = parser.Parameters; });
		}

		[Test]
		public void TestInvalidStateOptionCount()
		{
			// Arrange
			var parser = new GetOptLongArgumentParser();

			// Act
			Assert.Throws<InvalidOperationException>(
				() =>
				{ int results = parser.OptionCount; });
		}

		[Test]
		public void TestInvalidStateOptions()
		{
			// Arrange
			var parser = new GetOptLongArgumentParser();

			// Act
			Assert.Catch<InvalidOperationException>(
				() =>
				{ IDictionary<string, List<string>> results = parser.Options; });
		}

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
			Assert.AreEqual(
				1,
				parser.Parameters.Count,
				"An unexpected number of parameters were found.");
			Assert.AreEqual(
				"parameter",
				parser.Parameters[0],
				"An unexpected parameter 0 was found.");
		}

		[Test]
		public void TestMultipleParameters()
		{
			// Arrange
			string[] arguments =
			{
				"parameter", "parameter 2", "parameter 3",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				3,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				0,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.AreEqual(
				"parameter",
				parser.Parameters[0],
				"An unexpected parameter 0 was found.");
			Assert.AreEqual(
				"parameter 2",
				parser.Parameters[1],
				"An unexpected parameter 1 was found.");
			Assert.AreEqual(
				"parameter 3",
				parser.Parameters[2],
				"An unexpected parameter 2 was found.");
		}

		[Test]
		public void TestMultiple()
		{
			// Arrange
			string[] arguments =
			{
				"--verbose", "--file=input-file", "parameter",
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
				2,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.AreEqual(
				"parameter",
				parser.Parameters[0],
				"An unexpected parameter 0 was found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("verbose"),
				"Could not find the 'verbose' option.");
			Assert.AreEqual(
				1,
				parser.Options["verbose"].Count,
				"Unexpected number of values in the --verbose parameter.");
			Assert.IsNull(
				parser.Options["verbose"][0],
				"The --verbose parameter was not expected.");
			Assert.IsTrue(
				parser.Options.ContainsKey("file"),
				"Could not find the 'file' option.");
			Assert.AreEqual(
				1,
				parser.Options["file"].Count,
				"Unexpected number of values in the --file parameter.");
			Assert.AreEqual(
				"input-file",
				parser.Options["file"][0],
				"The --file parameter was not expected.");
		}

		[Test]
		public void TestMultipleWithStop()
		{
			// Arrange
			string[] arguments =
			{
				"--verbose", "--", "--file=input-file", "parameter",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				2,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				1,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.AreEqual(
				"--file=input-file",
				parser.Parameters[0],
				"An unexpected parameter 0 was found.");
			Assert.AreEqual(
				"parameter",
				parser.Parameters[1],
				"An unexpected parameter 1 was found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("verbose"),
				"Could not find the 'verbose' option.");
			Assert.AreEqual(
				1,
				parser.Options["verbose"].Count,
				"Unexpected number of values in the --verbose parameter.");
			Assert.IsNull(
				parser.Options["verbose"][0],
				"The --verbose parameter was not expected.");
		}

		[Test]
		public void TestMultipleMixed()
		{
			// Arrange
			string[] arguments =
			{
				"--verbose", "parameter", "--file=input-file", "parameter 2",
			};

			var parser = new GetOptLongArgumentParser();

			// Act
			parser.Parse(arguments);

			// Assert
			Assert.AreEqual(
				2,
				parser.ParameterCount,
				"An unexpected number of parameters was found.");
			Assert.AreEqual(
				2,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.AreEqual(
				"parameter",
				parser.Parameters[0],
				"An unexpected parameter 0 was found.");
			Assert.AreEqual(
				"parameter 2",
				parser.Parameters[1],
				"An unexpected parameter 1 was found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("verbose"),
				"Could not find the 'verbose' option.");
			Assert.AreEqual(
				1,
				parser.Options["verbose"].Count,
				"Unexpected number of values in the --verbose parameter.");
			Assert.IsNull(
				parser.Options["verbose"][0],
				"The --verbose parameter was not expected.");
			Assert.IsTrue(
				parser.Options.ContainsKey("file"),
				"Could not find the 'file' option.");
			Assert.AreEqual(
				1,
				parser.Options["file"].Count,
				"Unexpected number of values in the --file parameter.");
			Assert.AreEqual(
				"input-file",
				parser.Options["file"][0],
				"The --file parameter was not expected.");
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
			Assert.AreEqual(
				1,
				parser.Options["verbose"].Count,
				"Unexpected number of values in the --verbose parameter.");
			Assert.IsNull(
				parser.Options["verbose"][0],
				"The --verbose parameter was not expected.");
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
			Assert.AreEqual(
				1,
				parser.Options["file"].Count,
				"Unexpected number of values in the --file parameter.");
			Assert.AreEqual(
				"input-file",
				parser.Options["file"][0],
				"The --file parameter was not expected.");
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
			Assert.AreEqual(
				1,
				parser.Options["v"].Count,
				"Incorrect number of values found for -v");
			Assert.IsNull(
				parser.Options["v"][0],
				"Incorrect value found for -v");
		}

		[Test]
		public void TestMultipleShortArguments()
		{
			// Arrange
			string[] arguments =
			{
				"-v",
				"-a",
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
				2,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("v"),
				"Could not find the 'v' option.");
			Assert.AreEqual(
				1,
				parser.Options["v"].Count,
				"Incorrect number of values found for -v");
			Assert.IsNull(
				parser.Options["v"][0],
				"Incorrect value found for -v");
			Assert.IsTrue(
				parser.Options.ContainsKey("a"),
				"Could not find the 'a' option.");
			Assert.AreEqual(
				1,
				parser.Options["a"].Count,
				"Incorrect number of values found for -a");
			Assert.IsNull(
				parser.Options["a"][0],
				"Incorrect value found for -a");
		}

		[Test]
		public void TestMultipleShortArgumentsBundled()
		{
			// Arrange
			string[] arguments =
			{
				"-va",
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
				2,
				parser.OptionCount,
				"An unexpected number of options were found.");
			Assert.IsTrue(
				parser.Options.ContainsKey("v"),
				"Could not find the 'v' option.");
			Assert.AreEqual(
				1,
				parser.Options["v"].Count,
				"Incorrect number of values found for -v");
			Assert.IsNull(
				parser.Options["v"][0],
				"Incorrect value found for -v");
			Assert.IsTrue(
				parser.Options.ContainsKey("a"),
				"Could not find the 'a' option.");
			Assert.AreEqual(
				1,
				parser.Options["a"].Count,
				"Incorrect number of values found for -a");
			Assert.IsNull(
				parser.Options["a"][0],
				"Incorrect value found for -a");
		}

		[Test]
		public void TestMultipleShortArgumentsBundledParameter()
		{
			// Arrange
			string[] arguments =
			{
				"-va",
			};

			var parser = new GetOptLongArgumentParser();
			parser.OptionHints["v"] = OptionHints.HasRequiredParameter;

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
			Assert.AreEqual(
				1,
				parser.Options["v"].Count,
				"Incorrect number of values found for -v");
			Assert.AreEqual(
				"a",
				parser.Options["v"][0],
				"Incorrect value found for -v");
		}

		[Test]
		public void TestMultipleShortArgumentsParameter()
		{
			// Arrange
			string[] arguments =
			{
				"-v",
				"a",
			};

			var parser = new GetOptLongArgumentParser();
			parser.OptionHints["v"] = OptionHints.HasRequiredParameter;

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
			Assert.AreEqual(
				1,
				parser.Options["v"].Count,
				"Incorrect number of values found for -v");
			Assert.AreEqual(
				"a",
				parser.Options["v"][0],
				"Incorrect value found for -v");
		}
	}
}
using System.Collections.Generic;
using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class ArgumentParserTests: ArgumentTestBase
	{
		/// <summary>
		/// Creates the CLI argument reader with standard arguments set up.
		/// 
		/// <list type="bullet">
		/// <item>-t, --array: Takes a single value</item>
		/// <item>-d, --dual: Takes two values</item>
		/// </list>
		/// </summary>
		/// <param name="arguments">The string arguments.</param>
		/// <returns>An ArgumentReader with the arguments set up.</returns>
		private ArgumentParser CreateParser(string[] arguments)
		{
			ArgumentSettings settings = CreateCommonSettings();
			var reader = new ArgumentParser(
				settings,
				arguments);
			return reader;
		}

		[Test]
		public void CountTwoShorts()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "-a"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");

			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				2,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseBundledShortWithParam()
		{
			// Arrange
			var arguments = new[]
			{
				"-ab", "param1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
	"param1",
	parser.Parameters[0],
	"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find 'Option 2' argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseBundledShortWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-atfilename"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
			Assert.IsNull(
				option1.Values,
				"Found values for 'Option 1'.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"filename",
				array.Value,
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseCountedShortBundledWithNonCounted()
		{
			// Arrange
			var arguments = new[]
			{
				"-aab"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				2,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseCountedShortBundledWithNonCountedTrailing()
		{
			// Arrange
			var arguments = new[]
			{
				"-abb"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				2,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseCountedShortBundling()
		{
			// Arrange
			var arguments = new[]
			{
				"-aa"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");

			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				2,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseDoubleArgumentShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-d", "1", "2"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Dual"),
				"Could not find 'Dual' argument.");
			ArgumentReference dual = parser.Optionals["Dual"];
			Assert.AreEqual(
				1,
				dual.ReferenceCount,
				"'Dual' was seen an unexpected number of times.");
			Assert.IsNotNull(
				dual.Values,
				"Could not find values for 'Dual'.");
			Assert.AreEqual(
				1,
				dual.Values.Count,
				"Unexpected value for 'Dual'.");
			Assert.AreEqual(
				2,
				dual.Values[0].Count,
				"Unexpected inner value count for 'Dual'.");
			Assert.AreEqual(
				"1",
				dual.Values[0][0],
				"Unexpected value for first value of 'Dual'.");
			Assert.AreEqual(
				"2",
				dual.Values[0][1],
				"Unexpected valee for second value of 'Dual'.");
		}

		[Test]
		public void ParseDoubleValueLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--dual", "1", "2"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Dual"),
				"Could not find 'Dual' argument.");
			ArgumentReference dual = parser.Optionals["Dual"];
			Assert.AreEqual(
				1,
				dual.ReferenceCount,
				"'Dual' was seen an unexpected number of times.");
			Assert.IsNotNull(
				dual.Values,
				"Could not find values for 'Dual'.");
			Assert.AreEqual(
				1,
				dual.Values.Count,
				"Unexpected value for 'Dual'.");
			Assert.AreEqual(
				2,
				dual.Values[0].Count,
				"Unexpected inner value count for 'Dual'.");
			Assert.AreEqual(
				"1",
				dual.Values[0][0],
				"Unexpected value for first value of 'Dual'.");
			Assert.AreEqual(
				"2",
				dual.Values[0][1],
				"Unexpected valee for second value of 'Dual'.");
		}

		[Test]
		public void ParseEmptyArguments()
		{
			// Arrange
			var arguments = new string[]
			{
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				0,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");
		}

		[Test]
		public void ParseLongArrayValues()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "a", "--array=b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				2,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				2,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"a",
				array.Values[0][0],
				"Unexpected value for filename.");
			Assert.AreEqual(
				1,
				array.Values[1].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"b",
				array.Values[1][0],
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseLongDualLongParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "--dual", "1", "2", "param1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
			parser.Optionals.ContainsKey("Dual"),
			"Could not find 'Dual' argument.");
			ArgumentReference dual = parser.Optionals["Dual"];
			Assert.AreEqual(
				1,
				dual.ReferenceCount,
				"'Dual' was seen an unexpected number of times.");
			Assert.IsNotNull(
				dual.Values,
				"Could not find values for 'Dual'.");
			Assert.AreEqual(
				1,
				dual.Values.Count,
				"Unexpected value for 'Dual'.");
			Assert.AreEqual(
				2,
				dual.Values[0].Count,
				"Unexpected inner value count for 'Dual'.");
			Assert.AreEqual(
				"1",
				dual.Values[0][0],
				"Unexpected value for first value of 'Dual'.");
			Assert.AreEqual(
				"2",
				dual.Values[0][1],
				"Unexpected valee for second value of 'Dual'.");
		}

		[Test]
		public void ParseLongParameterDualLongParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "param1", "--dual", "1", "2", "param2"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				2,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");
			Assert.AreEqual(
				"param2",
				parser.Parameters[1],
				"Second parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
			parser.Optionals.ContainsKey("Dual"),
			"Could not find 'Dual' argument.");
			ArgumentReference dual = parser.Optionals["Dual"];
			Assert.AreEqual(
				1,
				dual.ReferenceCount,
				"'Dual' was seen an unexpected number of times.");
			Assert.IsNotNull(
				dual.Values,
				"Could not find values for 'Dual'.");
			Assert.AreEqual(
				1,
				dual.Values.Count,
				"Unexpected value for 'Dual'.");
			Assert.AreEqual(
				2,
				dual.Values[0].Count,
				"Unexpected inner value count for 'Dual'.");
			Assert.AreEqual(
				"1",
				dual.Values[0][0],
				"Unexpected value for first value of 'Dual'.");
			Assert.AreEqual(
				"2",
				dual.Values[0][1],
				"Unexpected valee for second value of 'Dual'.");
		}

		[Test]
		public void ParseLongValueWithStop()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "a", "--", "--array=b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"--array=b",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"a",
				array.Value,
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseLongWithSeparatedValue()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "value"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"value",
				array.Value,
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseLongWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"--array=value"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"value",
				array.Value,
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseParamWithBundledShort()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-ab"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseParamWithTwoShorts()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-a", "-b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseParameterWithShort()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-a"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseShortAndParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "param1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseShortArrayValues()
		{
			// Arrange
			var arguments = new[]
			{
				"-t", "a", "-t", "b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				2,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				2,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"a",
				array.Values[0][0],
				"Unexpected value for filename.");
			Assert.AreEqual(
				1,
				array.Values[1].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"b",
				array.Values[1][0],
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseShortBundling()
		{
			// Arrange
			var arguments = new[]
			{
				"-ab"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseShortParamAndShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "param1", "-b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseShortWithSeparatedValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-t", "filename"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"filename",
				array.Values[0][0],
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseShortWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-tfilename"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Array"),
				"Could not find 'Array' argument.");
			ArgumentReference array = parser.Optionals["Array"];
			Assert.AreEqual(
				1,
				array.ReferenceCount,
				"'Array' was seen an unexpected number of times.");
			Assert.IsNotNull(
				array.Values,
				"Could not find values for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values.Count,
				"Unexpected value for 'Array'.");
			Assert.AreEqual(
				1,
				array.Values[0].Count,
				"Unexpected inner value count for 'Array'.");
			Assert.AreEqual(
				"filename",
				array.Values[0][0],
				"Unexpected value for filename.");
		}

		[Test]
		public void ParseSingleBlankArgument()
		{
			// Arrange
			var arguments = new[]
			{
				""
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				0,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"",
				parser.Parameters[0],
				"First parameter was not expected.");
		}

		[Test]
		public void ParseSingleLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseSingleParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"param1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				0,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");
		}

		[Test]
		public void ParseSingleShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-a"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				1,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseTwoLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "--opt2"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseTwoParameters()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "param2"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				2,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				0,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");
			Assert.AreEqual(
				"param2",
				parser.Parameters[1],
				"Second parameter was not expected.");
		}

		[Test]
		public void ParseTwoShorts()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "-b"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				0,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}

		[Test]
		public void ParseTwoShortsWithParam()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "-b", "param1"
			};

			// Act
			ArgumentParser parser = CreateParser(arguments);

			// Assert
			Assert.AreEqual(
				1,
				parser.ParameterCount,
				"There was an unexpected number of parameter arguments.");
			Assert.AreEqual(
				2,
				parser.OptionalCount,
				"There were an unexpected number of optional arguments.");

			Assert.AreEqual(
				"param1",
				parser.Parameters[0],
				"First parameter was not expected.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 1"),
				"Could not find Option 1 argument.");
			ArgumentReference option1 = parser.Optionals["Option 1"];
			Assert.AreEqual(
				1,
				option1.ReferenceCount,
				"'Option 1' was seen an unexpected number of times.");

			Assert.IsTrue(
				parser.Optionals.ContainsKey("Option 2"),
				"Could not find Option 2 argument.");
			ArgumentReference option2 = parser.Optionals["Option 2"];
			Assert.AreEqual(
				1,
				option2.ReferenceCount,
				"'Option 2' was seen an unexpected number of times.");
		}
	}
}

using System.Collections.Generic;
using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class ArgumentParserTests : ArgumentTestBase
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

#if NOT_UPDATED
		[Test]
		public void ParseBundledShortWithParam()
		{
			// Arrange
			var arguments = new[]
			{
				"-ab", "param1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Second argument's had values.");

			// Act and Assert - Argument 3 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseBundledShortWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-atfilename"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-ffilename)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"t",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"Second argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"Second's arguments value count was not expected.");
			Assert.AreEqual(
				"filename",
				reader.Values[0],
				"Second argument's first value was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseCountedShortBundledWithNonCounted()
		{
			// Arrange
			var arguments = new[]
			{
				"-aab"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Second argument's had values.");

			// Act and Assert - Argument 3 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseCountedShortBundling()
		{
			// Arrange
			var arguments = new[]
			{
				"-aa"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Second argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseDoubleArgumentShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-d", "1", "2"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"d",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				2,
				reader.Values.Count,
				"Second argument's value count was not expected.");
			Assert.AreEqual(
				"1",
				reader.Values[0],
				"Second argument's first value was not expected.");
			Assert.AreEqual(
				"2",
				reader.Values[1],
				"Second argument's second value was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseDoubleValueLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--dual", "1", "2"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--dual 1 2)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"dual",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				2,
				reader.Values.Count,
				"First argument's value count was not expected.");
			Assert.AreEqual(
				"1",
				reader.Values[0],
				"First argument's first value was not expected.");
			Assert.AreEqual(
				"2",
				reader.Values[1],
				"First argument's second value was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseEmptyArguments()
		{
			// Arrange
			var arguments = new string[]
			{
			};
			ArgumentReader reader = CreateParser(arguments);

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongArrayValues()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "a", "--array=b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--array a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"array",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"a",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Act and Assert - Argument 2 (--array=b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"array",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"Second argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"Second argument's value count is not expected.");
			Assert.AreEqual(
				"b",
				reader.Values[0],
				"Second argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongDualLongParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "--dual", "1", "2", "param1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"opt1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (--dual 1 2)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"dual",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"Second argument's did not had values.");
			Assert.AreEqual(
				2,
				reader.Values.Count,
				"Second argument's value count was not expected.");
			Assert.AreEqual(
				"1",
				reader.Values[0],
				"Second argument's first value was not expected.");
			Assert.AreEqual(
				"2",
				reader.Values[1],
				"Second argument's second value was not expected.");

			// Act and Assert - Argument 3 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongParameterDualLongParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "param1", "--dual", "1", "2", "param2"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"opt1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Act and Assert - Argument 3 (--dual 1 2)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"dual",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"Third argument's did not had values.");
			Assert.AreEqual(
				2,
				reader.Values.Count,
				"Third argument's value count was not expected.");
			Assert.AreEqual(
				"1",
				reader.Values[0],
				"Third argument's first value was not expected.");
			Assert.AreEqual(
				"2",
				reader.Values[1],
				"Third argument's second value was not expected.");

			// Act and Assert - Argument 4 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve fourth argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Fourth argument's type was not expected.");
			Assert.AreEqual(
				"param2",
				reader.Key,
				"Fourth argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Fourth argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongValueWithStop()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "a", "--", "--array=b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--array a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not a long option.");
			Assert.AreEqual(
				"array",
				reader.Key,
				"First argument's key was not array.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not have values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's did not have exactly one value.");
			Assert.AreEqual(
				"a",
				reader.Values[0],
				"First argument's value was not 'a'.");

			// Act and Assert - Argument 2 (--array=b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Second argument's type was not a parameter.");
			Assert.AreEqual(
				"--array=b",
				reader.Key,
				"Second argument's key was not array.");
			Assert.IsNull(
				reader.Values,
				"Second argument's had values.");

			// Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongWithSeparatedValue()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "value"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--array value)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"array",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"value",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseLongWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"--array=value"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--array=value)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"array",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"value",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseParamWithBundledShort()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-ab"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Act and Assert - Argument 3 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseParamWithTwoShorts()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-a", "-b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Act and Assert - Argument 3 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseParameterWithShort()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "-a"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortAndParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "param1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortArrayValues()
		{
			// Arrange
			var arguments = new[]
			{
				"-t", "a", "-t", "b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-t a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"t",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"a",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Act and Assert - Argument 2 (-t b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"t",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"Second argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"Second argument's value count is not expected.");
			Assert.AreEqual(
				"b",
				reader.Values[0],
				"Second argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortBundling()
		{
			// Arrange
			var arguments = new[]
			{
				"-ab"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortParamAndShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "param1", "-b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Act and Assert - Argument 3 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortWithSeparatedValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-t", "filename"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--array a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"t",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"filename",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseShortWithValue()
		{
			// Arrange
			var arguments = new[]
			{
				"-tfilename"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-vfilename)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"t",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNotNull(
				reader.Values,
				"First argument's did not had values.");
			Assert.AreEqual(
				1,
				reader.Values.Count,
				"First argument's value count is not expected.");
			Assert.AreEqual(
				"filename",
				reader.Values[0],
				"First argument's first value is not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseSingleBlankArgument()
		{
			// Arrange
			var arguments = new[]
			{
				""
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseSingleLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"opt1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseSingleParameter()
		{
			// Arrange
			var arguments = new[]
			{
				"param1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseSingleShort()
		{
			// Arrange
			var arguments = new[]
			{
				"-a"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseTwoLong()
		{
			// Arrange
			var arguments = new[]
			{
				"--opt1", "--opt2"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"opt1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.LongOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"opt2",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseTwoParameters()
		{
			// Arrange
			var arguments = new[]
			{
				"param1", "param2"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (--opt1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"param2",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseTwoShorts()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "-b"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseTwoShortsWithParam()
		{
			// Arrange
			var arguments = new[]
			{
				"-a", "-b", "param1"
			};
			ArgumentReader reader = CreateParser(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (-b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				ReaderArgumentType.ShortOption,
				reader.ReaderArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"b",
				reader.Key,
				"Second argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"second argument values was not expected.");

			// Act and Assert - Argument 3 (param1)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve third argument.");
			Assert.AreEqual(
				ReaderArgumentType.Parameter,
				reader.ReaderArgumentType,
				"Third argument's type was not expected.");
			Assert.AreEqual(
				"param1",
				reader.Key,
				"Third argument's key was not expected.");
			Assert.IsNull(
				reader.Values,
				"Third argument values was not expected.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}
#endif
	}
}

using System.Collections.Generic;
using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class CliArgumentReaderTests
	{
		/// <summary>
		/// Creates the CLI argument reader with standard arguments set up.
		/// 
		/// <list type="bullet">
		/// <item>-a, --array: Takes a single value</item>
		/// <item>-d, --dual: Takes two values</item>
		/// </list>
		/// </summary>
		/// <param name="arguments">The string arguments.</param>
		/// <returns>A CliArgumentReader with the arguments set up.</returns>
		private CliArgumentReader CreateCliArgumentReader(string[] arguments)
		{
			// Create the initial settings using the Unix-style parameters.
			var settings = new GetOptLongReaderSettings();

			settings.Arguments.Add(
				new CliArgument()
				{
					LongOptionNames = new List<string>()
					{
						"array"
					},
					ShortOptionNames = new List<string>()
					{
						"a"
					},
					ValueCount = 1
				});
			settings.Arguments.Add(
				new CliArgument()
				{
					LongOptionNames = new List<string>()
					{
						"dual"
					},
					ShortOptionNames = new List<string>()
					{
						"d"
					},
					ValueCount = 2
				});

			// Create the reader and return it.
			var reader = new CliArgumentReader(settings, arguments);
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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
				"First argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"First argument's key was not array.");
			Assert.IsNull(
				reader.Values,
				"First argument's had values.");

			// Act and Assert - Argument 2 (--array=b)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve second argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"a",
				reader.Key,
				"Second argument's key was 'a'.");
			Assert.IsNull(
				reader.Values,
				"Second argument's had values.");

			// Final Act and Assert
			Assert.IsFalse(
				reader.Read(),
				"Retrieved argument when there should be no more.");
		}

		[Test]
		public void ParseBundledShortWithParam()
		{
			// Arrange
			var arguments = new[]
			{
				"-ab", "param1"
			};
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.Parameter,
				reader.CliArgumentType,
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
				"-affilename"
			};
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
				"Second argument's type was not expected.");
			Assert.AreEqual(
				"f",
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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.ShortOption,
				reader.CliArgumentType,
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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (-a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.LongOption,
				reader.CliArgumentType,
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
		public void ParseEmptyArguments()
		{
			// Arrange
			var arguments = new string[]
			{
			};
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

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
			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseLongDualLongParameter()
		{
			var arguments = new[]
			{
				"--opt1", "--dual", "1", "2", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseLongParameterDualLongParameter()
		{
			var arguments = new[]
			{
				"--opt1", "param1", "--dual", "1", "2", "param2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseLongValueWithStop()
		{
			// Arrange
			var arguments = new[]
			{
				"--array", "a", "--", "--array=b"
			};
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Act and Assert - Argument 1 (--array a)
			Assert.IsTrue(
				reader.Read(),
				"Could not retrieve first argument.");
			Assert.AreEqual(
				CliArgumentType.LongOption,
				reader.CliArgumentType,
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
				CliArgumentType.Parameter,
				reader.CliArgumentType,
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
			var arguments = new[]
			{
				"--arg1", "value"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseLongWithValue()
		{
			var arguments = new[]
			{
				"--arg1=value"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseParamWithBundledShort()
		{
			var arguments = new[]
			{
				"param1", "-ab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseParamWithTwoShorts()
		{
			var arguments = new[]
			{
				"param1", "-a", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseParameterWithShort()
		{
			var arguments = new[]
			{
				"param1", "-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortAndParameter()
		{
			var arguments = new[]
			{
				"-a", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortArrayValues()
		{
			var arguments = new[]
			{
				"-t", "a", "-t", "b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortBundling()
		{
			var arguments = new[]
			{
				"-ab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortParamAndShort()
		{
			var arguments = new[]
			{
				"-a", "param1", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortWithSeparatedValue()
		{
			var arguments = new[]
			{
				"-v", "filename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseShortWithValue()
		{
			var arguments = new[]
			{
				"-vfilename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseSingleBlankArgument()
		{
			var arguments = new[]
			{
				""
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseSingleLong()
		{
			var arguments = new[]
			{
				"--opt1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseSingleParameter()
		{
			var arguments = new[]
			{
				"param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseSingleShort()
		{
			var arguments = new[]
			{
				"-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseTwoLong()
		{
			var arguments = new[]
			{
				"--opt1", "--opt2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseTwoParameters()
		{
			var arguments = new[]
			{
				"param1", "param2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseTwoShorts()
		{
			var arguments = new[]
			{
				"-a", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseTwoShortsWithParam()
		{
			var arguments = new[]
			{
				"-a", "-b", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}
	}
}

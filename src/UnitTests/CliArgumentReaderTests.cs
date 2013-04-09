using MfGames.Tools.Cli;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class CliArgumentReaderTests
	{
		private CliArgumentReader CreateCliArgumentReader(string[] arguments)
		{
			var reader = new CliArgumentReader(arguments);
			return reader;
		}

		[Test]
		public void CountTwoShorts()
		{
			var arguments = new[]
			{
				"-a", "-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseBundledShortWithParam()
		{
			var arguments = new[]
			{
				"-ab", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseBundledShortWithValue()
		{
			var arguments = new[]
			{
				"-avfilename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseCountedShortBundledWithNonCounted()
		{
			var arguments = new[]
			{
				"-aab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseCountedShortBundling()
		{
			var arguments = new[]
			{
				"-aa"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseDoubleArgumentShort()
		{
			var arguments = new[]
			{
				"-d", "1", "2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseDoubleValueLong()
		{
			var arguments = new[]
			{
				"--dual", "1", "2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		[Test]
		public void ParseEmptyArguments()
		{
			// Arrange
			var arguments = new string[]
			{
			};

			// Act
			CliArgumentReader reader = CreateCliArgumentReader(arguments);

			// Assert
			reader.Read();
		}

		[Test]
		public void ParseLongArrayValues()
		{
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

			// Act and Assert
			//bool successful = reader.Read();
			//Assert.IsTrue(successful);
			//reader.ArgumentType == Optional

			//successful = reader.Read();
			//Assert.IsTrue(successful);
			//reader.ArgumentType == Parameter
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

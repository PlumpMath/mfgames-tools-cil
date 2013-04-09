using MfGames.Tools.Cli;

namespace UnitTests
{
	public class CliArgumentReaderTests
	{
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

		public void ParseSingleBlankArgument()
		{
			var arguments = new[]
			{
				""
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseSingleParameter()
		{
			var arguments = new[]
			{
				"param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseTwoParameters()
		{
			var arguments = new[]
			{
				"param1", "param2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseSingleLong()
		{
			var arguments = new[]
			{
				"--opt1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseTwoLong()
		{
			var arguments = new[]
			{
				"--opt1", "--opt2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseSingleShort()
		{
			var arguments = new[]
			{
				"-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseTwoShorts()
		{
			var arguments = new[]
			{
				"-a", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void CountTwoShorts()
		{
			var arguments = new[]
			{
				"-a", "-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortBundling()
		{
			var arguments = new[]
			{
				"-ab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseCountedShortBundling()
		{
			var arguments = new[]
			{
				"-aa"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseCountedShortBundledWithNonCounted()
		{
			var arguments = new[]
			{
				"-aab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseLongWithValue()
		{
			var arguments = new[]
			{
				"--arg1=value"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseLongWithSeparatedValue()
		{
			var arguments = new[]
			{
				"--arg1", "value"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortWithSeparatedValue()
		{
			var arguments = new[]
			{
				"-v", "filename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortWithValue()
		{
			var arguments = new[]
			{
				"-vfilename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseBundledShortWithValue()
		{
			var arguments = new[]
			{
				"-avfilename"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseDoubleArgumentShort()
		{
			var arguments = new[]
			{
				"-d", "1", "2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseDoubleValueLong()
		{
			var arguments = new[]
			{
				"--dual", "1", "2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortArrayValues()
		{
			var arguments = new[]
			{
				"-t", "a", "-t", "b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseLongArrayValues()
		{
			var arguments = new[]
			{
				"--array", "a", "--array=b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortAndParameter()
		{
			var arguments = new[]
			{
				"-a", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseParameterWithShort()
		{
			var arguments = new[]
			{
				"param1", "-a"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseShortParamAndShort()
		{
			var arguments = new[]
			{
				"-a", "param1", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseTwoShortsWithParam()
		{
			var arguments = new[]
			{
				"-a", "-b", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseParamWithTwoShorts()
		{
			var arguments = new[]
			{
				"param1", "-a", "-b"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseParamWithBundledShort()
		{
			var arguments = new[]
			{
				"param1", "-ab"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseBundledShortWithParam()
		{
			var arguments = new[]
			{
				"-ab", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseLongDualLongParameter()
		{
			var arguments = new[]
			{
				"--opt1", "--dual", "1", "2", "param1"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

		public void ParseLongParameterDualLongParameter()
		{
			var arguments = new[]
			{
				"--opt1", "param1", "--dual", "1", "2", "param2"
			};

			CliArgumentReader reader = CreateCliArgumentReader(arguments);
			reader.Read();
		}

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

		private CliArgumentReader CreateCliArgumentReader(string[] arguments)
		{
			var reader = new CliArgumentReader(arguments);
			return reader;
		}
	}
}

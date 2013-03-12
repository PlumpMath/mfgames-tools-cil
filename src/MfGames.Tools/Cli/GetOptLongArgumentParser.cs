namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Implements a GNU getopt_long influenced argument parser. The basic pattern
	/// is to use "-" for single-character options and "--" for long arguments.
	/// 
	/// Multiple single character options can be bundled together so "-abc" is the same
	/// as "-a -b -c". For arguments that take parameters (such as "-o foo"), the
	/// whitespace can be optional ("-ofoo") only if hints are provided.
	/// 
	/// Long arguments have the pattern of "--boolean-argument" or
	/// "--string=value".
	/// 
	/// See http://www.linuxtopia.org/online_books/programming_books/gnu_c_programming_tutorial/Processing-command-line-options.html
	/// </summary>
	public class GetOptLongArgumentParser: ArgumentParser
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetOptLongArgumentParser"/> class.
		/// </summary>
		public GetOptLongArgumentParser()
		{
			StopProcessingOptionsArgument = "--";
			LongOptionPrefix = "--";
			LongAssignment = "=";
			ShortOptionPrefix = "-";
			AllowShortOptionBundling = true;
		}
	}
}

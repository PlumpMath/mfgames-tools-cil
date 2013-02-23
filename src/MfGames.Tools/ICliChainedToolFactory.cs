using System;

namespace MfGames.Tools
{
	/// <summary>
	/// An interface that indicates that the tool can potentially create a new
	/// tool based on its state after the CliToolFactory has populated its properties
	/// and constructors. This is used to create "sub commands", such as many of the
	/// Git and Subversion commands (i.e., `git commit` verses `git log`).
	/// 
	/// The basic functionality is to populate as many properties as possible in the
	/// tool and then to call CreateChainedTool(). If this returns null, the loop
	/// is broken and the validation is performed. Otherwise, the Type object returned
	/// is then treated as the original type passed into the CliToolFactory and
	/// then processed as normal.
	/// 
	/// The chained tool can also implement this interface and allow for continually
	/// looped tools (e.g., `netsh http add urlacl`).
	/// 
	/// The class that implements this interface typically doesn't have the
	/// [Tool] attribute because the intial "tool" is just used to find the actual
	/// process, whereas those tools marked with the [Tool] attribute are the
	/// actual operations and therefore what would generate MSBuild or NAnt tasks.
	/// 
	/// The object returned by this interface doesn't have to extend the chained
	/// tool factory, but see the various methods for CliToolFactory because some do
	/// make that assumption.
	/// </summary>
	public interface ICliChainedToolFactory
	{
		/// <summary>
		/// Returns the type that a chained tool should be reparsed with the arguments
		/// and processed as if that was the tool passed into the CliToolFactory.
		/// </summary>
		/// <returns>A Type that should be instantianted as a chained tool or null to
		/// indicate there is no chained tool.</returns>
		Type CreateChainedToolType();
	}
}

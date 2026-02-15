using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x02000212 RID: 530
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0003B5E1 File Offset: 0x000397E1
		public CodeLinePragma LinePragma { get; }
	}
}

using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x02000214 RID: 532
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x06000C92 RID: 3218 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent. </param>
		// Token: 0x06000C93 RID: 3219 RVA: 0x0003B601 File Offset: 0x00039801
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x1700029A RID: 666
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0003B610 File Offset: 0x00039810
		public string Value
		{
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040008F2 RID: 2290
		private string _value;
	}
}

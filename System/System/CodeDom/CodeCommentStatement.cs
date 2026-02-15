using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement consisting of a single comment.</summary>
	// Token: 0x020001EA RID: 490
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class.</summary>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeCommentStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003A92A File Offset: 0x00038B2A
		public CodeCommentStatement(string text, bool docComment)
		{
			this.Comment = new CodeComment(text, docComment);
		}

		/// <summary>Gets or sets the contents of the comment.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003A93F File Offset: 0x00038B3F
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0003A947 File Offset: 0x00038B47
		public CodeComment Comment { get; set; }
	}
}

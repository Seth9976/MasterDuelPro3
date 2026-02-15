using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x020001E9 RID: 489
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003A8FA File Offset: 0x00038AFA
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003A902 File Offset: 0x00038B02
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.DocComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>true if the comment is a documentation comment; otherwise, false.</returns>
		// Token: 0x17000245 RID: 581
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0003A918 File Offset: 0x00038B18
		public bool DocComment
		{
			[CompilerGenerated]
			set
			{
				this.<DocComment>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x17000246 RID: 582
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0003A921 File Offset: 0x00038B21
		public string Text
		{
			set
			{
				this._text = value;
			}
		}

		// Token: 0x0400089C RID: 2204
		private string _text;
	}
}

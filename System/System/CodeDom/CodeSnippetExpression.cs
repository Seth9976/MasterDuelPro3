using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal expression.</summary>
	// Token: 0x02000213 RID: 531
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class.</summary>
		// Token: 0x06000C8F RID: 3215 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeSnippetExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class using the specified literal expression.</summary>
		/// <param name="value">The literal expression to represent. </param>
		// Token: 0x06000C90 RID: 3216 RVA: 0x0003B5E9 File Offset: 0x000397E9
		public CodeSnippetExpression(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal string of code.</summary>
		/// <returns>The literal string.</returns>
		// Token: 0x17000299 RID: 665
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public string Value
		{
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040008F1 RID: 2289
		private string _value;
	}
}

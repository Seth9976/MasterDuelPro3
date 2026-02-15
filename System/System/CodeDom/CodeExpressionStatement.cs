using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x020001F9 RID: 505
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x06000C05 RID: 3077 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement. </param>
		// Token: 0x06000C06 RID: 3078 RVA: 0x0003AC03 File Offset: 0x00038E03
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0003AC12 File Offset: 0x00038E12
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0003AC1A File Offset: 0x00038E1A
		public CodeExpression Expression { get; set; }
	}
}

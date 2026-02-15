using System;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an if statement.</summary>
	// Token: 0x020001ED RID: 493
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003AA09 File Offset: 0x00038C09
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003AA27 File Offset: 0x00038C27
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Gets or sets the expression to evaluate true or false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate true or false.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0003AA58 File Offset: 0x00038C58
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0003AA60 File Offset: 0x00038C60
		public CodeExpression Condition { get; set; }

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to true.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to true.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003AA69 File Offset: 0x00038C69
		public CodeStatementCollection TrueStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to false.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0003AA71 File Offset: 0x00038C71
		public CodeStatementCollection FalseStatements { get; } = new CodeStatementCollection();
	}
}

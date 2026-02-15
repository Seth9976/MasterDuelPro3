using System;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x02000205 RID: 517
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06000C43 RID: 3139 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value. </param>
		// Token: 0x06000C44 RID: 3140 RVA: 0x0003B0A1 File Offset: 0x000392A1
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or null if the statement is part of a subroutine.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0003B0B0 File Offset: 0x000392B0
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0003B0B8 File Offset: 0x000392B8
		public CodeExpression Expression { get; set; }
	}
}

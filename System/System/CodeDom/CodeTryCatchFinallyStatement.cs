using System;

namespace System.CodeDom
{
	/// <summary>Represents a try block with any number of catch clauses and, optionally, a finally block.</summary>
	// Token: 0x0200021A RID: 538
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003B6DC File Offset: 0x000398DC
		public CodeStatementCollection TryStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0003B6E4 File Offset: 0x000398E4
		public CodeCatchClauseCollection CatchClauses { get; } = new CodeCatchClauseCollection();

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0003B6EC File Offset: 0x000398EC
		public CodeStatementCollection FinallyStatements { get; } = new CodeStatementCollection();
	}
}

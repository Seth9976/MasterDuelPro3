using System;

namespace System.CodeDom
{
	/// <summary>Represents a for statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x020001FD RID: 509
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0003ACAC File Offset: 0x00038EAC
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0003ACB4 File Offset: 0x00038EB4
		public CodeStatement InitStatement { get; set; }

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003ACBD File Offset: 0x00038EBD
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0003ACC5 File Offset: 0x00038EC5
		public CodeExpression TestExpression { get; set; }

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0003ACCE File Offset: 0x00038ECE
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0003ACD6 File Offset: 0x00038ED6
		public CodeStatement IncrementStatement { get; set; }

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003ACDF File Offset: 0x00038EDF
		public CodeStatementCollection Statements { get; } = new CodeStatementCollection();
	}
}

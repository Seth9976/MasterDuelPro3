using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x020001EE RID: 494
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003AA79 File Offset: 0x00038C79
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0003AAA2 File Offset: 0x00038CA2
		public CodeExpressionCollection BaseConstructorArgs { get; } = new CodeExpressionCollection();

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0003AAAA File Offset: 0x00038CAA
		public CodeExpressionCollection ChainedConstructorArgs { get; } = new CodeExpressionCollection();
	}
}

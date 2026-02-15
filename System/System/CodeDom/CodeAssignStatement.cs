using System;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x020001DC RID: 476
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06000B9F RID: 2975 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to. </param>
		/// <param name="right">The value to assign. </param>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003A611 File Offset: 0x00038811
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0003A627 File Offset: 0x00038827
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0003A62F File Offset: 0x0003882F
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003A638 File Offset: 0x00038838
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0003A640 File Offset: 0x00038840
		public CodeExpression Right { get; set; }
	}
}

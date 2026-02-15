using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x020001E3 RID: 483
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06000BBE RID: 3006 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator. </param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator. </param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator. </param>
		// Token: 0x06000BBF RID: 3007 RVA: 0x0003A7CF File Offset: 0x000389CF
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0003A7EC File Offset: 0x000389EC
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0003A7F4 File Offset: 0x000389F4
		public CodeExpression Right { get; set; }

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0003A7FD File Offset: 0x000389FD
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0003A805 File Offset: 0x00038A05
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x1700023E RID: 574
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0003A80E File Offset: 0x00038A0E
		public CodeBinaryOperatorType Operator
		{
			[CompilerGenerated]
			set
			{
				this.<Operator>k__BackingField = value;
			}
		}
	}
}

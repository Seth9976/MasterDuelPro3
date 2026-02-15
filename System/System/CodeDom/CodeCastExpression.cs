using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x020001E5 RID: 485
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003A817 File Offset: 0x00038A17
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0003A834 File Offset: 0x00038A34
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x0003A85E File Offset: 0x00038A5E
		public CodeTypeReference TargetType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._targetType) == null)
				{
					codeTypeReference = (this._targetType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._targetType = value;
			}
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0003A867 File Offset: 0x00038A67
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0003A86F File Offset: 0x00038A6F
		public CodeExpression Expression { get; set; }

		// Token: 0x04000896 RID: 2198
		private CodeTypeReference _targetType;
	}
}

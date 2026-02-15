using System;

namespace System.CodeDom
{
	/// <summary>Represents a typeof expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the typeof expression. </param>
		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003BA09 File Offset: 0x00039C09
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the typeof expression. </param>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003BA18 File Offset: 0x00039C18
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the typeof expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the typeof expression. This property will never return null, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0003BA2C File Offset: 0x00039C2C
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0003BA56 File Offset: 0x00039C56
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x0400090C RID: 2316
		private CodeTypeReference _type;
	}
}

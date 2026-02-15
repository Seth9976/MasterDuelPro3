using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x06000C6E RID: 3182 RVA: 0x0003B3FC File Offset: 0x000395FC
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x06000C6F RID: 3183 RVA: 0x0003B40F File Offset: 0x0003960F
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x06000C70 RID: 3184 RVA: 0x0003B435 File Offset: 0x00039635
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0003B460 File Offset: 0x00039660
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0003B48A File Offset: 0x0003968A
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._createType) == null)
				{
					codeTypeReference = (this._createType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0003B493 File Offset: 0x00039693
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x040008E5 RID: 2277
		private CodeTypeReference _createType;
	}
}

using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a data type.</summary>
	// Token: 0x02000224 RID: 548
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class.</summary>
		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeTypeReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified type.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference. </param>
		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003BAD0 File Offset: 0x00039CD0
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type name.</summary>
		/// <param name="type">The name of the data type to reference. </param>
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003BADF File Offset: 0x00039CDF
		public CodeTypeReferenceExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type.</summary>
		/// <param name="type">An instance of the data type to reference. </param>
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003BAF3 File Offset: 0x00039CF3
		public CodeTypeReferenceExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003BB08 File Offset: 0x00039D08
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0003BB32 File Offset: 0x00039D32
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

		// Token: 0x04000910 RID: 2320
		private CodeTypeReference _type;
	}
}

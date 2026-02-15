using System;

namespace System.CodeDom
{
	/// <summary>Represents a variable declaration.</summary>
	// Token: 0x02000225 RID: 549
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class.</summary>
		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003A609 File Offset: 0x00038809
		public CodeVariableDeclarationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003BB3B File Offset: 0x00039D3B
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Gets or sets the initialization expression for the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003BB5D File Offset: 0x00039D5D
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0003BB65 File Offset: 0x00039D65
		public CodeExpression InitExpression { get; set; }

		/// <summary>Gets or sets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003BB6E File Offset: 0x00039D6E
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003BB7F File Offset: 0x00039D7F
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the data type of the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003BB88 File Offset: 0x00039D88
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x0003BBB2 File Offset: 0x00039DB2
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

		// Token: 0x04000911 RID: 2321
		private CodeTypeReference _type;

		// Token: 0x04000912 RID: 2322
		private string _name;
	}
}

using System;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x0200020B RID: 523
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x06000C74 RID: 3188 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06000C75 RID: 3189 RVA: 0x0003B49B File Offset: 0x0003969B
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06000C76 RID: 3190 RVA: 0x0003B4B1 File Offset: 0x000396B1
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0003B4CC File Offset: 0x000396CC
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._customAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0003B4F4 File Offset: 0x000396F4
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0003B51E File Offset: 0x0003971E
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

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0003B527 File Offset: 0x00039727
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0003B538 File Offset: 0x00039738
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

		// Token: 0x040008E7 RID: 2279
		private CodeTypeReference _type;

		// Token: 0x040008E8 RID: 2280
		private string _name;

		// Token: 0x040008E9 RID: 2281
		private CodeAttributeDeclarationCollection _customAttributes;
	}
}

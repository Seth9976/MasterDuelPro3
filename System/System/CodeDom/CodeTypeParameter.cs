using System;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0003BA5F File Offset: 0x00039C5F
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
		}

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0003BA70 File Offset: 0x00039C70
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._constraints) == null)
				{
					codeTypeReferenceCollection = (this._constraints = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is null.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0003BA98 File Offset: 0x00039C98
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

		// Token: 0x0400090D RID: 2317
		private string _name;

		// Token: 0x0400090E RID: 2318
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x0400090F RID: 2319
		private CodeTypeReferenceCollection _constraints;
	}
}

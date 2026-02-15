using System;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x0200021F RID: 543
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0003B92A File Offset: 0x00039B2A
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0003B93B File Offset: 0x00039B3B
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

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />. </returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0003B944 File Offset: 0x00039B44
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0003B94C File Offset: 0x00039B4C
		public MemberAttributes Attributes { get; set; } = (MemberAttributes)20482;

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0003B958 File Offset: 0x00039B58
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

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0003B97D File Offset: 0x00039B7D
		public CodeLinePragma LinePragma { get; }

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0003B985 File Offset: 0x00039B85
		public CodeCommentStatementCollection Comments { get; } = new CodeCommentStatementCollection();

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003B990 File Offset: 0x00039B90
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04000905 RID: 2309
		private string _name;

		// Token: 0x04000906 RID: 2310
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000907 RID: 2311
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000908 RID: 2312
		private CodeDirectiveCollection _endDirectives;
	}
}

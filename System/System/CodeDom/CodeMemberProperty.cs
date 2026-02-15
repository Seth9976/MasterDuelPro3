using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x02000203 RID: 515
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0003AF5B File Offset: 0x0003915B
		public CodeTypeReference PrivateImplementationType { get; }

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0003AF64 File Offset: 0x00039164
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._implementationTypes) == null)
				{
					codeTypeReferenceCollection = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0003AF8C File Offset: 0x0003918C
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0003AFB6 File Offset: 0x000391B6
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

		/// <summary>Gets or sets a value indicating whether the property has a get method accessor.</summary>
		/// <returns>true if the Count property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to true; otherwise, false.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003AFBF File Offset: 0x000391BF
		public bool HasGet
		{
			get
			{
				return this._hasGet || this.GetStatements.Count > 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a set method accessor.</summary>
		/// <returns>true if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, false.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0003AFD9 File Offset: 0x000391D9
		public bool HasSet
		{
			get
			{
				return this._hasSet || this.SetStatements.Count > 0;
			}
		}

		/// <summary>Gets the collection of get statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the get statements for the member property.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0003AFF3 File Offset: 0x000391F3
		public CodeStatementCollection GetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of set statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the set statements for the member property.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0003AFFB File Offset: 0x000391FB
		public CodeStatementCollection SetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0003B003 File Offset: 0x00039203
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x040008CE RID: 2254
		private CodeTypeReference _type;

		// Token: 0x040008CF RID: 2255
		private bool _hasGet;

		// Token: 0x040008D0 RID: 2256
		private bool _hasSet;

		// Token: 0x040008D1 RID: 2257
		private CodeTypeReferenceCollection _implementationTypes;
	}
}

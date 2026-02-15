using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000200 RID: 512
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0003AD08 File Offset: 0x00038F08
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0003AD32 File Offset: 0x00038F32
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

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0003AD3B File Offset: 0x00038F3B
		public CodeTypeReference PrivateImplementationType { get; }

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003AD44 File Offset: 0x00038F44
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

		// Token: 0x040008BE RID: 2238
		private CodeTypeReference _type;

		// Token: 0x040008BF RID: 2239
		private CodeTypeReferenceCollection _implementationTypes;
	}
}

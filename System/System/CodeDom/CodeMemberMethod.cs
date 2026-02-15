using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a method of a type.</summary>
	// Token: 0x02000202 RID: 514
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the return value of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the value returned by the method.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0003ADE4 File Offset: 0x00038FE4
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x0003AE18 File Offset: 0x00039018
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(typeof(void).FullName));
				}
				return codeTypeReference;
			}
			set
			{
				this._returnType = value;
			}
		}

		/// <summary>Gets the statements within the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements within the method.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0003AE21 File Offset: 0x00039021
		public CodeStatementCollection Statements
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateStatements = this.PopulateStatements;
					if (populateStatements != null)
					{
						populateStatements(this, EventArgs.Empty);
					}
				}
				return this._statements;
			}
		}

		/// <summary>Gets the parameter declarations for the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the method parameters.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0003AE58 File Offset: 0x00039058
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateParameters = this.PopulateParameters;
					if (populateParameters != null)
					{
						populateParameters(this, EventArgs.Empty);
					}
				}
				return this._parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface this method, if private, implements a method of, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface with the method that the private method whose declaration is represented by this <see cref="T:System.CodeDom.CodeMemberMethod" /> implements.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0003AE8F File Offset: 0x0003908F
		public CodeTypeReference PrivateImplementationType { get; }

		/// <summary>Gets the data types of the interfaces implemented by this method, unless it is a private method implementation, which is indicated by the <see cref="P:System.CodeDom.CodeMemberMethod.PrivateImplementationType" /> property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the interfaces implemented by this method.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0003AE98 File Offset: 0x00039098
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this._implementationTypes == null)
				{
					this._implementationTypes = new CodeTypeReferenceCollection();
				}
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateImplementationTypes = this.PopulateImplementationTypes;
					if (populateImplementationTypes != null)
					{
						populateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this._implementationTypes;
			}
		}

		/// <summary>Gets the custom attributes of the return type of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0003AEF0 File Offset: 0x000390F0
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._returnAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._returnAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets the type parameters for the current generic method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the generic method.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0003AF18 File Offset: 0x00039118
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection codeTypeParameterCollection;
				if ((codeTypeParameterCollection = this._typeParameters) == null)
				{
					codeTypeParameterCollection = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return codeTypeParameterCollection;
			}
		}

		// Token: 0x040008C3 RID: 2243
		private readonly CodeParameterDeclarationExpressionCollection _parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x040008C4 RID: 2244
		private readonly CodeStatementCollection _statements = new CodeStatementCollection();

		// Token: 0x040008C5 RID: 2245
		private CodeTypeReference _returnType;

		// Token: 0x040008C6 RID: 2246
		private CodeTypeReferenceCollection _implementationTypes;

		// Token: 0x040008C7 RID: 2247
		private CodeAttributeDeclarationCollection _returnAttributes;

		// Token: 0x040008C8 RID: 2248
		private CodeTypeParameterCollection _typeParameters;

		// Token: 0x040008C9 RID: 2249
		private int _populated;

		// Token: 0x040008CA RID: 2250
		[CompilerGenerated]
		private EventHandler PopulateParameters;

		// Token: 0x040008CB RID: 2251
		[CompilerGenerated]
		private EventHandler PopulateStatements;

		// Token: 0x040008CC RID: 2252
		[CompilerGenerated]
		private EventHandler PopulateImplementationTypes;
	}
}

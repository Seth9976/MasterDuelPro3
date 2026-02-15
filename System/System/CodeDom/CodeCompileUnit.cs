using System;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x020001EC RID: 492
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0003A963 File Offset: 0x00038B63
		public CodeNamespaceCollection Namespaces { get; } = new CodeNamespaceCollection();

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0003A96C File Offset: 0x00038B6C
		public StringCollection ReferencedAssemblies
		{
			get
			{
				StringCollection stringCollection;
				if ((stringCollection = this._assemblies) == null)
				{
					stringCollection = (this._assemblies = new StringCollection());
				}
				return stringCollection;
			}
		}

		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0003A994 File Offset: 0x00038B94
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._attributes) == null)
				{
					codeAttributeDeclarationCollection = (this._attributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0003A9BC File Offset: 0x00038BBC
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

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0003A9E4 File Offset: 0x00038BE4
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

		// Token: 0x0400089F RID: 2207
		private StringCollection _assemblies;

		// Token: 0x040008A0 RID: 2208
		private CodeAttributeDeclarationCollection _attributes;

		// Token: 0x040008A1 RID: 2209
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x040008A2 RID: 2210
		private CodeDirectiveCollection _endDirectives;
	}
}

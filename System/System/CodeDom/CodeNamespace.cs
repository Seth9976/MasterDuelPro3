using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x02000206 RID: 518
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x06000C47 RID: 3143 RVA: 0x0003B0C1 File Offset: 0x000392C1
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared. </param>
		// Token: 0x06000C48 RID: 3144 RVA: 0x0003B0EA File Offset: 0x000392EA
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0003B11A File Offset: 0x0003931A
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateTypes = this.PopulateTypes;
					if (populateTypes != null)
					{
						populateTypes(this, EventArgs.Empty);
					}
				}
				return this._classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0003B151 File Offset: 0x00039351
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateImports = this.PopulateImports;
					if (populateImports != null)
					{
						populateImports(this, EventArgs.Empty);
					}
				}
				return this._imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0003B188 File Offset: 0x00039388
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0003B199 File Offset: 0x00039399
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

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0003B1A2 File Offset: 0x000393A2
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateComments = this.PopulateComments;
					if (populateComments != null)
					{
						populateComments(this, EventArgs.Empty);
					}
				}
				return this._comments;
			}
		}

		// Token: 0x040008D9 RID: 2265
		private string _name;

		// Token: 0x040008DA RID: 2266
		private readonly CodeNamespaceImportCollection _imports = new CodeNamespaceImportCollection();

		// Token: 0x040008DB RID: 2267
		private readonly CodeCommentStatementCollection _comments = new CodeCommentStatementCollection();

		// Token: 0x040008DC RID: 2268
		private readonly CodeTypeDeclarationCollection _classes = new CodeTypeDeclarationCollection();

		// Token: 0x040008DD RID: 2269
		private int _populated;

		// Token: 0x040008DE RID: 2270
		[CompilerGenerated]
		private EventHandler PopulateComments;

		// Token: 0x040008DF RID: 2271
		[CompilerGenerated]
		private EventHandler PopulateImports;

		// Token: 0x040008E0 RID: 2272
		[CompilerGenerated]
		private EventHandler PopulateTypes;
	}
}

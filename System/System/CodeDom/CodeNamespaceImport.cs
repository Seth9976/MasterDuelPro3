using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x02000208 RID: 520
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x06000C51 RID: 3153 RVA: 0x0003A8FA File Offset: 0x00038AFA
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import. </param>
		// Token: 0x06000C52 RID: 3154 RVA: 0x0003B1E8 File Offset: 0x000393E8
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0003B1F7 File Offset: 0x000393F7
		public CodeLinePragma LinePragma { get; }

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0003B1FF File Offset: 0x000393FF
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0003B210 File Offset: 0x00039410
		public string Namespace
		{
			get
			{
				return this._nameSpace ?? string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		// Token: 0x040008E1 RID: 2273
		private string _nameSpace;
	}
}

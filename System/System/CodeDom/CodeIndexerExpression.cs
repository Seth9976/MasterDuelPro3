using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an indexer property of an object.</summary>
	// Token: 0x020001FC RID: 508
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		/// <summary>Gets or sets the target object that can be indexed.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the indexer object.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0003AC6C File Offset: 0x00038E6C
		public CodeExpression TargetObject { get; }

		/// <summary>Gets the collection of indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0003AC74 File Offset: 0x00038E74
		public CodeExpressionCollection Indices
		{
			get
			{
				CodeExpressionCollection codeExpressionCollection;
				if ((codeExpressionCollection = this._indices) == null)
				{
					codeExpressionCollection = (this._indices = new CodeExpressionCollection());
				}
				return codeExpressionCollection;
			}
		}

		// Token: 0x040008B6 RID: 2230
		private CodeExpressionCollection _indices;
	}
}

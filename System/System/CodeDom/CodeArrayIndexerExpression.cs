using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x020001DB RID: 475
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0003A5DA File Offset: 0x000387DA
		public CodeExpression TargetObject { get; }

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0003A5E4 File Offset: 0x000387E4
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

		// Token: 0x04000876 RID: 2166
		private CodeExpressionCollection _indices;
	}
}

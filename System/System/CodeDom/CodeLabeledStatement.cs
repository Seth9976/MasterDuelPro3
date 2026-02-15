using System;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x020001FE RID: 510
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0003ACE7 File Offset: 0x00038EE7
		public string Label
		{
			get
			{
				return this._label ?? string.Empty;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		public CodeStatement Statement { get; }

		// Token: 0x040008BC RID: 2236
		private string _label;
	}
}

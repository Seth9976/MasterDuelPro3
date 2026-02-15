using System;

namespace System.CodeDom
{
	/// <summary>Represents a goto statement.</summary>
	// Token: 0x020001FB RID: 507
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		/// <summary>Gets or sets the name of the label at which to continue program execution.</summary>
		/// <returns>A string that indicates the name of the label at which to continue program execution.</returns>
		/// <exception cref="T:System.ArgumentNullException">The label cannot be set because<paramref name=" value" /> is null or an empty string.</exception>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0003AC64 File Offset: 0x00038E64
		public string Label
		{
			get
			{
				return this._label;
			}
		}

		// Token: 0x040008B5 RID: 2229
		private string _label;
	}
}

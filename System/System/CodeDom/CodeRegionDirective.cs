using System;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x02000210 RID: 528
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0003B59A File Offset: 0x0003979A
		public string RegionText
		{
			get
			{
				return this._regionText ?? string.Empty;
			}
		}

		// Token: 0x040008ED RID: 2285
		private string _regionText;
	}
}

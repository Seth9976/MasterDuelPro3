using System;

namespace SFB
{
	// Token: 0x02000070 RID: 112
	public struct ExtensionFilter
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00006ABE File Offset: 0x00004CBE
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x040002BC RID: 700
		public string Name;

		// Token: 0x040002BD RID: 701
		public string[] Extensions;
	}
}

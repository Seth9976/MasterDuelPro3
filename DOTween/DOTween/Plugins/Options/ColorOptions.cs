using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200008D RID: 141
	public struct ColorOptions : IPlugOptions
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000EC39 File Offset: 0x0000CE39
		public void Reset()
		{
			this.alphaOnly = false;
		}

		// Token: 0x04000188 RID: 392
		public bool alphaOnly;
	}
}

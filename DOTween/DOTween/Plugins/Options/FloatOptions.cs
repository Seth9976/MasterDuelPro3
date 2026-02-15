using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200008E RID: 142
	public struct FloatOptions : IPlugOptions
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000EC42 File Offset: 0x0000CE42
		public void Reset()
		{
			this.snapping = false;
		}

		// Token: 0x04000189 RID: 393
		public bool snapping;
	}
}

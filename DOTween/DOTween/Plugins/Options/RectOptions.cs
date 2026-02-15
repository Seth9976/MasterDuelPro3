using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200008F RID: 143
	public struct RectOptions : IPlugOptions
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000EC4B File Offset: 0x0000CE4B
		public void Reset()
		{
			this.snapping = false;
		}

		// Token: 0x0400018A RID: 394
		public bool snapping;
	}
}

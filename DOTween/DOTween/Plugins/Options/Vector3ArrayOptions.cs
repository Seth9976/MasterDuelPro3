using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200008B RID: 139
	public struct Vector3ArrayOptions : IPlugOptions
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000EC22 File Offset: 0x0000CE22
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
			this.durations = null;
		}

		// Token: 0x04000185 RID: 389
		public AxisConstraint axisConstraint;

		// Token: 0x04000186 RID: 390
		public bool snapping;

		// Token: 0x04000187 RID: 391
		internal float[] durations;
	}
}

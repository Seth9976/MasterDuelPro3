using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000091 RID: 145
	public struct VectorOptions : IPlugOptions
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000EC86 File Offset: 0x0000CE86
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
		}

		// Token: 0x04000190 RID: 400
		public AxisConstraint axisConstraint;

		// Token: 0x04000191 RID: 401
		public bool snapping;
	}
}

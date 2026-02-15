using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000007 RID: 7
	internal struct DoublePoint
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002208 File Offset: 0x00000408
		public DoublePoint(double x = 0.0, double y = 0.0)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002218 File Offset: 0x00000418
		public DoublePoint(DoublePoint dp)
		{
			this.X = dp.X;
			this.Y = dp.Y;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002232 File Offset: 0x00000432
		public DoublePoint(IntPoint ip)
		{
			this.X = (double)ip.X;
			this.Y = (double)ip.Y;
		}

		// Token: 0x04000007 RID: 7
		public double X;

		// Token: 0x04000008 RID: 8
		public double Y;
	}
}

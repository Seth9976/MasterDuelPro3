using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000B RID: 11
	internal struct IntPoint
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002711 File Offset: 0x00000911
		public IntPoint(long X, long Y)
		{
			this.X = X;
			this.Y = Y;
			this.NX = 0.0;
			this.NY = 0.0;
			this.N = -1L;
			this.D = 0L;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000274F File Offset: 0x0000094F
		public IntPoint(double x, double y)
		{
			this.X = (long)x;
			this.Y = (long)y;
			this.NX = 0.0;
			this.NY = 0.0;
			this.N = -1L;
			this.D = 0L;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002790 File Offset: 0x00000990
		public IntPoint(IntPoint pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
			this.NX = pt.NX;
			this.NY = pt.NY;
			this.N = pt.N;
			this.D = pt.D;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027E5 File Offset: 0x000009E5
		public static bool operator ==(IntPoint a, IntPoint b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002805 File Offset: 0x00000A05
		public static bool operator !=(IntPoint a, IntPoint b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002828 File Offset: 0x00000A28
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IntPoint)
			{
				IntPoint a = (IntPoint)obj;
				return this.X == a.X && this.Y == a.Y;
			}
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002869 File Offset: 0x00000A69
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000013 RID: 19
		public long N;

		// Token: 0x04000014 RID: 20
		public long X;

		// Token: 0x04000015 RID: 21
		public long Y;

		// Token: 0x04000016 RID: 22
		public long D;

		// Token: 0x04000017 RID: 23
		public double NX;

		// Token: 0x04000018 RID: 24
		public double NY;
	}
}

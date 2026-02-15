using System;

namespace Spine
{
	// Token: 0x02000067 RID: 103
	public class Pow : IInterpolation
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000E927 File Offset: 0x0000CB27
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000E92F File Offset: 0x0000CB2F
		public float Power { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x0000E938 File Offset: 0x0000CB38
		public Pow(float power)
		{
			this.Power = power;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000E948 File Offset: 0x0000CB48
		protected override float Apply(float a)
		{
			if (a <= 0.5f)
			{
				return (float)Math.Pow((double)(a * 2f), (double)this.Power) / 2f;
			}
			return (float)Math.Pow((double)((a - 1f) * 2f), (double)this.Power) / (float)((this.Power % 2f == 0f) ? (-2) : 2) + 1f;
		}
	}
}

using System;

namespace Spine
{
	// Token: 0x02000068 RID: 104
	public class PowOut : Pow
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public PowOut(float power)
			: base(power)
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E9BD File Offset: 0x0000CBBD
		protected override float Apply(float a)
		{
			return (float)Math.Pow((double)(a - 1f), (double)base.Power) * (float)((base.Power % 2f == 0f) ? (-1) : 1) + 1f;
		}
	}
}

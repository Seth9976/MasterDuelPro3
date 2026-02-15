using System;

namespace Spine
{
	// Token: 0x02000066 RID: 102
	public abstract class IInterpolation
	{
		// Token: 0x0600035A RID: 858
		protected abstract float Apply(float a);

		// Token: 0x0600035B RID: 859 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public float Apply(float start, float end, float a)
		{
			return start + (end - start) * this.Apply(a);
		}

		// Token: 0x040001DF RID: 479
		public static IInterpolation Pow2 = new Pow(2f);

		// Token: 0x040001E0 RID: 480
		public static IInterpolation Pow2Out = new PowOut(2f);
	}
}

using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000195 RID: 405
	internal static class SpaceFillingCurves
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x000293CA File Offset: 0x000275CA
		private static uint Part1By1(uint x)
		{
			x &= 65535U;
			x = (x ^ (x << 8)) & 16711935U;
			x = (x ^ (x << 4)) & 252645135U;
			x = (x ^ (x << 2)) & 858993459U;
			x = (x ^ (x << 1)) & 1431655765U;
			return x;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002940A File Offset: 0x0002760A
		private static uint Compact1By1(uint x)
		{
			x &= 1431655765U;
			x = (x ^ (x >> 1)) & 858993459U;
			x = (x ^ (x >> 2)) & 252645135U;
			x = (x ^ (x >> 4)) & 16711935U;
			x = (x ^ (x >> 8)) & 65535U;
			return x;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002944A File Offset: 0x0002764A
		public static uint EncodeMorton2D(uint2 coord)
		{
			return (SpaceFillingCurves.Part1By1(coord.y) << 1) + SpaceFillingCurves.Part1By1(coord.x);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00029465 File Offset: 0x00027665
		public static uint2 DecodeMorton2D(uint code)
		{
			return math.uint2(SpaceFillingCurves.Compact1By1(code), SpaceFillingCurves.Compact1By1(code >> 1));
		}
	}
}

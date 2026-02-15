using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000075 RID: 117
	public static class MathUtilities
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00013010 File Offset: 0x00011210
		public static float InverseLerp(float a, float b, float value)
		{
			return (value - a) / (b - a);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00013019 File Offset: 0x00011219
		public static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 value)
		{
			return new Vector2((value.x - a.x) / (b.x - a.x), (value.y - a.y) / (b.y - a.y));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00013058 File Offset: 0x00011258
		public static Vector3 InverseLerp(Vector3 a, Vector3 b, Vector3 value)
		{
			return new Vector3((value.x - a.x) / (b.x - a.x), (value.y - a.y) / (b.y - a.y), (value.z - a.z) / (b.z - a.z));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000130BC File Offset: 0x000112BC
		public static Vector4 InverseLerp(Vector4 a, Vector4 b, Vector4 value)
		{
			return new Vector4((value.x - a.x) / (b.x - a.x), (value.y - a.y) / (b.y - a.y), (value.z - a.z) / (b.z - a.z), (value.w - a.w) / (b.w - a.w));
		}
	}
}

using System;

namespace UnityEngine.Sprites
{
	// Token: 0x02000400 RID: 1024
	public sealed class DataUtility
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x0003CB80 File Offset: 0x0003AD80
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			return sprite.GetInnerUVs();
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0003CB98 File Offset: 0x0003AD98
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			return sprite.GetOuterUVs();
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		public static Vector4 GetPadding(Sprite sprite)
		{
			return sprite.GetPadding();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 v;
			v.x = sprite.border.x + sprite.border.z;
			v.y = sprite.border.y + sprite.border.w;
			return v;
		}
	}
}

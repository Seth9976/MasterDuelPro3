using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[UsedByNativeCode]
	public struct CharacterInfo
	{
		// Token: 0x0400003B RID: 59
		public int index;

		// Token: 0x0400003C RID: 60
		[Obsolete("CharacterInfo.uv is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead.")]
		public Rect uv;

		// Token: 0x0400003D RID: 61
		[Obsolete("CharacterInfo.vert is deprecated. Use minX, maxX, minY, maxY instead.")]
		public Rect vert;

		// Token: 0x0400003E RID: 62
		[Obsolete("CharacterInfo.width is deprecated. Use advance instead.")]
		[NativeName("advance")]
		public float width;

		// Token: 0x0400003F RID: 63
		public int size;

		// Token: 0x04000040 RID: 64
		public FontStyle style;

		// Token: 0x04000041 RID: 65
		[Obsolete("CharacterInfo.flipped is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead, which will be correct regardless of orientation.")]
		public bool flipped;
	}
}

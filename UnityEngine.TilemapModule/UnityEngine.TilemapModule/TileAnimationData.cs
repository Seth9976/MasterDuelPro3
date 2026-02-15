using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000F RID: 15
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileAnimationData
	{
		// Token: 0x04000039 RID: 57
		private Sprite[] m_AnimatedSprites;

		// Token: 0x0400003A RID: 58
		private float m_AnimationSpeed;

		// Token: 0x0400003B RID: 59
		private float m_AnimationStartTime;

		// Token: 0x0400003C RID: 60
		private TileAnimationFlags m_Flags;
	}
}

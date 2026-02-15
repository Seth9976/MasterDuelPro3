using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000D RID: 13
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	internal struct TileDataNative
	{
		// Token: 0x0400002F RID: 47
		private int m_Sprite;

		// Token: 0x04000030 RID: 48
		private Color m_Color;

		// Token: 0x04000031 RID: 49
		private Matrix4x4 m_Transform;

		// Token: 0x04000032 RID: 50
		private int m_GameObject;

		// Token: 0x04000033 RID: 51
		private TileFlags m_Flags;

		// Token: 0x04000034 RID: 52
		private Tile.ColliderType m_ColliderType;
	}
}

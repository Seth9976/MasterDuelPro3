using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000E RID: 14
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	[Serializable]
	public struct TileChangeData
	{
		// Token: 0x04000035 RID: 53
		[SerializeField]
		private Vector3Int m_Position;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		private Object m_TileAsset;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private Color m_Color;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private Matrix4x4 m_Transform;
	}
}

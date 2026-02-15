using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileData
	{
		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000027C8 File Offset: 0x000009C8
		public Sprite sprite
		{
			set
			{
				this.m_Sprite = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027E3 File Offset: 0x000009E3
		public Color color
		{
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027ED File Offset: 0x000009ED
		public Matrix4x4 transform
		{
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027F7 File Offset: 0x000009F7
		public GameObject gameObject
		{
			set
			{
				this.m_GameObject = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x1700000C RID: 12
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002813 File Offset: 0x00000A13
		public TileFlags flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000281D File Offset: 0x00000A1D
		public Tile.ColliderType colliderType
		{
			set
			{
				this.m_ColliderType = value;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002828 File Offset: 0x00000A28
		private static TileData CreateDefault()
		{
			return new TileData
			{
				color = Color.white,
				transform = Matrix4x4.identity,
				flags = TileFlags.None,
				colliderType = Tile.ColliderType.None
			};
		}

		// Token: 0x04000028 RID: 40
		private int m_Sprite;

		// Token: 0x04000029 RID: 41
		private Color m_Color;

		// Token: 0x0400002A RID: 42
		private Matrix4x4 m_Transform;

		// Token: 0x0400002B RID: 43
		private int m_GameObject;

		// Token: 0x0400002C RID: 44
		private TileFlags m_Flags;

		// Token: 0x0400002D RID: 45
		private Tile.ColliderType m_ColliderType;

		// Token: 0x0400002E RID: 46
		internal static readonly TileData Default = TileData.CreateDefault();
	}
}

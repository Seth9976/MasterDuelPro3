using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000003 RID: 3
	[HelpURL("https://docs.unity3d.com/Manual/Tilemap-TileAsset.html")]
	[RequiredByNativeCode]
	[Serializable]
	public class Tile : TileBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002330 File Offset: 0x00000530
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				this.m_Sprite = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000233C File Offset: 0x0000053C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002354 File Offset: 0x00000554
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002360 File Offset: 0x00000560
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002378 File Offset: 0x00000578
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002384 File Offset: 0x00000584
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000239C File Offset: 0x0000059C
		public GameObject gameObject
		{
			get
			{
				return this.m_InstancedGameObject;
			}
			set
			{
				this.m_InstancedGameObject = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023A8 File Offset: 0x000005A8
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000023C0 File Offset: 0x000005C0
		public TileFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000023E4 File Offset: 0x000005E4
		public Tile.ColliderType colliderType
		{
			get
			{
				return this.m_ColliderType;
			}
			set
			{
				this.m_ColliderType = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F0 File Offset: 0x000005F0
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.sprite = this.m_Sprite;
			tileData.color = this.m_Color;
			tileData.transform = this.m_Transform;
			tileData.gameObject = this.m_InstancedGameObject;
			tileData.flags = this.m_Flags;
			tileData.colliderType = this.m_ColliderType;
		}

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private Matrix4x4 m_Transform = Matrix4x4.identity;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private GameObject m_InstancedGameObject;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private TileFlags m_Flags = TileFlags.LockColor;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private Tile.ColliderType m_ColliderType = Tile.ColliderType.Sprite;

		// Token: 0x02000004 RID: 4
		public enum ColliderType
		{
			// Token: 0x0400000D RID: 13
			None,
			// Token: 0x0400000E RID: 14
			Sprite,
			// Token: 0x0400000F RID: 15
			Grid
		}
	}
}

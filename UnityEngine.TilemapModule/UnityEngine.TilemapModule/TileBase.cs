using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000005 RID: 5
	[RequiredByNativeCode]
	public abstract class TileBase : ScriptableObject
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002479 File Offset: 0x00000679
		[RequiredByNativeCode]
		public virtual void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			tilemap.RefreshTile(position);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002484 File Offset: 0x00000684
		[RequiredByNativeCode]
		public virtual void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002488 File Offset: 0x00000688
		private TileData GetTileDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileData tileData = default(TileData);
			this.GetTileData(position, tilemap, ref tileData);
			return tileData;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024B0 File Offset: 0x000006B0
		[RequiredByNativeCode]
		public virtual bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024C4 File Offset: 0x000006C4
		private TileAnimationData GetTileAnimationDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileAnimationData tileAnimationData = default(TileAnimationData);
			this.GetTileAnimationData(position, tilemap, ref tileAnimationData);
			return tileAnimationData;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024EA File Offset: 0x000006EA
		[RequiredByNativeCode]
		private void GetTileAnimationDataRef(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData, ref bool hasAnimation)
		{
			hasAnimation = this.GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024FC File Offset: 0x000006FC
		[RequiredByNativeCode]
		public virtual bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000250F File Offset: 0x0000070F
		[RequiredByNativeCode]
		private void StartUpRef(Vector3Int position, ITilemap tilemap, GameObject go, ref bool startUpInvokedByUser)
		{
			startUpInvokedByUser = this.StartUp(position, tilemap, go);
		}
	}
}

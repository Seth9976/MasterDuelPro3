using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	public class ITilemap
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal ITilemap()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public void RefreshTile(Vector3Int position)
		{
			bool addToList = this.m_AddToList;
			if (addToList)
			{
				bool flag = this.m_RefreshCount >= this.m_RefreshPos.Length;
				if (flag)
				{
					NativeArray<Vector3Int> refreshPos = new NativeArray<Vector3Int>(Math.Max(1, this.m_RefreshCount * 2), Allocator.Temp, NativeArrayOptions.ClearMemory);
					NativeArray<Vector3Int>.Copy(this.m_RefreshPos, refreshPos, this.m_RefreshPos.Length);
					this.m_RefreshPos.Dispose();
					this.m_RefreshPos = refreshPos;
				}
				int refreshCount = this.m_RefreshCount;
				this.m_RefreshCount = refreshCount + 1;
				this.m_RefreshPos[refreshCount] = position;
			}
			else
			{
				this.m_Tilemap.RefreshTile(position);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		[RequiredByNativeCode]
		private static ITilemap CreateInstance()
		{
			ITilemap.s_Instance = new ITilemap();
			return ITilemap.s_Instance;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002128 File Offset: 0x00000328
		[RequiredByNativeCode]
		private unsafe static void FindAllRefreshPositions(ITilemap tilemap, int count, IntPtr oldTilesIntPtr, IntPtr newTilesIntPtr, IntPtr positionsIntPtr)
		{
			tilemap.m_AddToList = true;
			NativeArray<Vector3Int> refreshPos = tilemap.m_RefreshPos;
			bool flag = !tilemap.m_RefreshPos.IsCreated || tilemap.m_RefreshPos.Length < count;
			if (flag)
			{
				tilemap.m_RefreshPos = new NativeArray<Vector3Int>(Math.Max(16, count), Allocator.Temp, NativeArrayOptions.ClearMemory);
			}
			tilemap.m_RefreshCount = 0;
			void* oldTilesPtr = oldTilesIntPtr.ToPointer();
			void* newTilesPtr = newTilesIntPtr.ToPointer();
			void* positionsPtr = positionsIntPtr.ToPointer();
			NativeArray<int> oldTilesIds = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(oldTilesPtr, count, Allocator.Invalid);
			NativeArray<int> newTilesIds = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(newTilesPtr, count, Allocator.Invalid);
			NativeArray<Vector3Int> positions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(positionsPtr, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				int oldTileId = oldTilesIds[i];
				int newTileId = newTilesIds[i];
				Vector3Int position = positions[i];
				bool flag2 = oldTileId != 0;
				if (flag2)
				{
					TileBase tile = (TileBase)Object.ForceLoadFromInstanceID(oldTileId);
					tile.RefreshTile(position, tilemap);
				}
				bool flag3 = newTileId != 0;
				if (flag3)
				{
					TileBase tile2 = (TileBase)Object.ForceLoadFromInstanceID(newTileId);
					tile2.RefreshTile(position, tilemap);
				}
			}
			tilemap.m_Tilemap.RefreshTilesNative(tilemap.m_RefreshPos.m_Buffer, tilemap.m_RefreshCount);
			tilemap.m_RefreshPos.Dispose();
			tilemap.m_AddToList = false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002270 File Offset: 0x00000470
		[RequiredByNativeCode]
		private unsafe static void GetAllTileData(ITilemap tilemap, int count, IntPtr tilesIntPtr, IntPtr positionsIntPtr, IntPtr outTileDataIntPtr)
		{
			void* tilesPtr = tilesIntPtr.ToPointer();
			void* positionsPtr = positionsIntPtr.ToPointer();
			void* outTileDataPtr = outTileDataIntPtr.ToPointer();
			NativeArray<int> tiles = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(tilesPtr, count, Allocator.Invalid);
			NativeArray<Vector3Int> positions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(positionsPtr, count, Allocator.Invalid);
			NativeArray<TileData> tileDataArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<TileData>(outTileDataPtr, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				TileData tileData = TileData.Default;
				int tileId = tiles[i];
				bool flag = tileId != 0;
				if (flag)
				{
					TileBase tile = (TileBase)Object.ForceLoadFromInstanceID(tileId);
					tile.GetTileData(positions[i], tilemap, ref tileData);
				}
				tileDataArray[i] = tileData;
			}
		}

		// Token: 0x04000001 RID: 1
		internal static ITilemap s_Instance;

		// Token: 0x04000002 RID: 2
		internal Tilemap m_Tilemap;

		// Token: 0x04000003 RID: 3
		internal bool m_AddToList;

		// Token: 0x04000004 RID: 4
		internal int m_RefreshCount;

		// Token: 0x04000005 RID: 5
		internal NativeArray<Vector3Int> m_RefreshPos;
	}
}

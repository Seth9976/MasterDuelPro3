using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeHeader("Modules/Grid/Public/Grid.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapTile.h")]
	[NativeType(Header = "Modules/Tilemap/Public/Tilemap.h")]
	public sealed class Tilemap : GridLayout
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		internal bool bufferSyncTile
		{
			get
			{
				return this.m_BufferSyncTile;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002540 File Offset: 0x00000740
		internal static bool HasLoopEndedForTileAnimationCallback()
		{
			return Tilemap.loopEndedForTileAnimation != null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000255C File Offset: 0x0000075C
		private unsafe void HandleLoopEndedForTileAnimationCallback(int count, IntPtr positionsIntPtr)
		{
			bool flag = !Tilemap.HasLoopEndedForTileAnimationCallback();
			if (!flag)
			{
				void* positionsPtr = positionsIntPtr.ToPointer();
				NativeArray<Vector3Int> positions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(positionsPtr, count, Allocator.Invalid);
				this.SendLoopEndedForTileAnimationCallback(positions);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002594 File Offset: 0x00000794
		private void SendLoopEndedForTileAnimationCallback(NativeArray<Vector3Int> positions)
		{
			try
			{
				Tilemap.loopEndedForTileAnimation(this, positions);
			}
			catch (Exception e)
			{
				Debug.LogException(e, this);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025D0 File Offset: 0x000007D0
		internal static bool HasSyncTileCallback()
		{
			return Tilemap.tilemapTileChanged != null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025EC File Offset: 0x000007EC
		internal static bool HasPositionsChangedCallback()
		{
			return Tilemap.tilemapPositionsChanged != null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002608 File Offset: 0x00000808
		private void HandleSyncTileCallback(Tilemap.SyncTile[] syncTiles)
		{
			bool flag = Tilemap.tilemapTileChanged == null;
			if (!flag)
			{
				this.SendTilemapTileChangedCallback(syncTiles);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000262C File Offset: 0x0000082C
		private unsafe void HandlePositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			bool flag = !Tilemap.HasPositionsChangedCallback();
			if (!flag)
			{
				void* positionsPtr = positionsIntPtr.ToPointer();
				NativeArray<Vector3Int> positions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(positionsPtr, count, Allocator.Invalid);
				this.SendTilemapPositionsChangedCallback(positions);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002664 File Offset: 0x00000864
		private void SendTilemapTileChangedCallback(Tilemap.SyncTile[] syncTiles)
		{
			try
			{
				Tilemap.tilemapTileChanged(this, syncTiles);
			}
			catch (Exception e)
			{
				Debug.LogException(e, this);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026A0 File Offset: 0x000008A0
		private void SendTilemapPositionsChangedCallback(NativeArray<Vector3Int> positions)
		{
			try
			{
				Tilemap.tilemapPositionsChanged(this, positions);
			}
			catch (Exception e)
			{
				Debug.LogException(e, this);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026DC File Offset: 0x000008DC
		[NativeMethod(Name = "RefreshTileAsset")]
		public void RefreshTile(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RefreshTile_Injected(intPtr, ref position);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002700 File Offset: 0x00000900
		[FreeFunction(Name = "TilemapBindings::RefreshTileAssetsNative", HasExplicitThis = true)]
		internal unsafe void RefreshTilesNative(void* positions, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RefreshTilesNative_Injected(intPtr, positions, count);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002724 File Offset: 0x00000924
		[RequiredByNativeCode]
		internal void GetLoopEndedForTileAnimationCallbackSettings(ref bool hasEndLoopForTileAnimationCallback)
		{
			hasEndLoopForTileAnimationCallback = Tilemap.HasLoopEndedForTileAnimationCallback();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000272E File Offset: 0x0000092E
		[RequiredByNativeCode]
		private void DoLoopEndedForTileAnimationCallback(int count, IntPtr positionsIntPtr)
		{
			this.HandleLoopEndedForTileAnimationCallback(count, positionsIntPtr);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000273A File Offset: 0x0000093A
		[RequiredByNativeCode]
		internal void GetSyncTileCallbackSettings(ref Tilemap.SyncTileCallbackSettings settings)
		{
			settings.hasSyncTileCallback = Tilemap.HasSyncTileCallback();
			settings.hasPositionsChangedCallback = Tilemap.HasPositionsChangedCallback();
			settings.isBufferSyncTile = this.bufferSyncTile;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000275F File Offset: 0x0000095F
		[RequiredByNativeCode]
		private void DoSyncTileCallback(Tilemap.SyncTile[] syncTiles)
		{
			this.HandleSyncTileCallback(syncTiles);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000276A File Offset: 0x0000096A
		[RequiredByNativeCode]
		private void DoPositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			this.HandlePositionsChangedCallback(count, positionsIntPtr);
		}

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RefreshTile_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void RefreshTilesNative_Injected(IntPtr _unity_self, void* positions, int count);

		// Token: 0x04000010 RID: 16
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<Tilemap, Tilemap.SyncTile[]> tilemapTileChanged;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Tilemap, NativeArray<Vector3Int>> tilemapPositionsChanged;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Tilemap, NativeArray<Vector3Int>> loopEndedForTileAnimation;

		// Token: 0x04000013 RID: 19
		private bool m_BufferSyncTile;

		// Token: 0x02000007 RID: 7
		[RequiredByNativeCode]
		public struct SyncTile
		{
			// Token: 0x04000014 RID: 20
			internal Vector3Int m_Position;

			// Token: 0x04000015 RID: 21
			internal TileBase m_Tile;

			// Token: 0x04000016 RID: 22
			internal TileData m_TileData;
		}

		// Token: 0x02000008 RID: 8
		internal struct SyncTileCallbackSettings
		{
			// Token: 0x04000017 RID: 23
			internal bool hasSyncTileCallback;

			// Token: 0x04000018 RID: 24
			internal bool hasPositionsChangedCallback;

			// Token: 0x04000019 RID: 25
			internal bool isBufferSyncTile;
		}
	}
}

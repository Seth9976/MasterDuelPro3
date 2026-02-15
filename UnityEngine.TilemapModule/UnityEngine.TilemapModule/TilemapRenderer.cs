using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.U2D;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Tilemap/TilemapRendererJobs.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[RequireComponent(typeof(Tilemap))]
	public sealed class TilemapRenderer : Renderer
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002776 File Offset: 0x00000976
		[RequiredByNativeCode]
		internal void RegisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered += this.OnSpriteAtlasRegistered;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000278B File Offset: 0x0000098B
		[RequiredByNativeCode]
		internal void UnregisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered -= this.OnSpriteAtlasRegistered;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027A0 File Offset: 0x000009A0
		internal void OnSpriteAtlasRegistered(SpriteAtlas atlas)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TilemapRenderer.OnSpriteAtlasRegistered_Injected(intPtr, Object.MarshalledUnityObject.Marshal<SpriteAtlas>(atlas));
		}

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnSpriteAtlasRegistered_Injected(IntPtr _unity_self, IntPtr atlas);
	}
}

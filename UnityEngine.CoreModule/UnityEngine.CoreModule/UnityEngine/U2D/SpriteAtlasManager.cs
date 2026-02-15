using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.U2D
{
	// Token: 0x02000413 RID: 1043
	[StaticAccessor("GetSpriteAtlasManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlasManager.h")]
	public class SpriteAtlasManager
	{
		// Token: 0x06001B98 RID: 7064 RVA: 0x0003D280 File Offset: 0x0003B480
		[RequiredByNativeCode]
		private static bool RequestAtlas(string tag)
		{
			bool flag = SpriteAtlasManager.atlasRequested != null;
			bool flag2;
			if (flag)
			{
				SpriteAtlasManager.atlasRequested(tag, new Action<SpriteAtlas>(SpriteAtlasManager.Register));
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001B99 RID: 7065 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		// (remove) Token: 0x06001B9A RID: 7066 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<SpriteAtlas> atlasRegistered;

		// Token: 0x06001B9B RID: 7067 RVA: 0x0003D323 File Offset: 0x0003B523
		[RequiredByNativeCode]
		private static void PostRegisteredAtlas(SpriteAtlas spriteAtlas)
		{
			Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
			if (action != null)
			{
				action(spriteAtlas);
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0003D338 File Offset: 0x0003B538
		internal static void Register(SpriteAtlas spriteAtlas)
		{
			SpriteAtlasManager.Register_Injected(Object.MarshalledUnityObject.Marshal<SpriteAtlas>(spriteAtlas));
		}

		// Token: 0x06001B9D RID: 7069
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Register_Injected(IntPtr spriteAtlas);

		// Token: 0x04000E8F RID: 3727
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, Action<SpriteAtlas>> atlasRequested;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000D0 RID: 208
	[NativeHeader("Runtime/Graphics/CustomRenderTextureManager.h")]
	public static class CustomRenderTextureManager
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0000BD74 File Offset: 0x00009F74
		[RequiredByNativeCode]
		private static void InvokeOnTextureLoaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000BD88 File Offset: 0x00009F88
		[RequiredByNativeCode]
		private static void InvokeOnTextureUnloaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x04000279 RID: 633
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<CustomRenderTexture> textureLoaded;

		// Token: 0x0400027A RID: 634
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<CustomRenderTexture> textureUnloaded;
	}
}

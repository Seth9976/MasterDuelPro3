using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[StaticAccessor("SpriteUtilityBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/SpriteMask/Public/ScriptBindings/SpriteMask.bindings.h")]
	internal static class SpriteMaskUtility
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static bool HasSpriteMaskInLayerRange(SortingLayerRange range)
		{
			return SpriteMaskUtility.HasSpriteMaskInLayerRange_Injected(ref range);
		}

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasSpriteMaskInLayerRange_Injected([In] ref SortingLayerRange range);
	}
}

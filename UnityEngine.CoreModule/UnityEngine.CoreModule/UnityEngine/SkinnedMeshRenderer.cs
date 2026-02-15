using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000123 RID: 291
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x170001A7 RID: 423
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public bool updateWhenOffscreen
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_updateWhenOffscreen_Injected(intPtr, value);
			}
		}

		// Token: 0x06000A14 RID: 2580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateWhenOffscreen_Injected(IntPtr _unity_self, bool value);
	}
}

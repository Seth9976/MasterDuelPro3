using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x0200040F RID: 1039
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	public static class PixelPerfectRendering
	{
		// Token: 0x17000437 RID: 1079
		// (set) Token: 0x06001B8C RID: 7052
		public static extern float pixelSnapSpacing
		{
			[FreeFunction("SetPixelSnapSpacing")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}

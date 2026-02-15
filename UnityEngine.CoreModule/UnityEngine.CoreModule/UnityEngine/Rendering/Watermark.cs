using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036E RID: 878
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class Watermark
	{
		// Token: 0x060018B5 RID: 6325
		[FreeFunction("IsAnyWatermarkVisible")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsVisible();
	}
}

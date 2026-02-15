using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DD RID: 221
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	public static class ScalableBufferManager
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005CE RID: 1486
		public static extern float widthScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005CF RID: 1487
		public static extern float heightScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060005D0 RID: 1488
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}

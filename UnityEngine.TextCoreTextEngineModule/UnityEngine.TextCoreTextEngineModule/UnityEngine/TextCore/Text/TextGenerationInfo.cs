using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000056 RID: 86
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class TextGenerationInfo
	{
		// Token: 0x06000231 RID: 561
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create();

		// Token: 0x06000232 RID: 562
		[FreeFunction("TextGenerationInfo::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(IntPtr ptr);
	}
}

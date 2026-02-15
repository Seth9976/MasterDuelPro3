using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000039 RID: 57
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[RequiredByNativeCode]
	public struct AnimationStream
	{
		// Token: 0x040000DC RID: 220
		private uint m_AnimatorBindingsVersion;

		// Token: 0x040000DD RID: 221
		private IntPtr constant;

		// Token: 0x040000DE RID: 222
		private IntPtr input;

		// Token: 0x040000DF RID: 223
		private IntPtr output;

		// Token: 0x040000E0 RID: 224
		private IntPtr workspace;

		// Token: 0x040000E1 RID: 225
		private IntPtr inputStreamAccessor;

		// Token: 0x040000E2 RID: 226
		private IntPtr animationHandleBinder;
	}
}

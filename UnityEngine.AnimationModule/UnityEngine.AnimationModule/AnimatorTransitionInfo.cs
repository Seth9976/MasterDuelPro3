using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[RequiredByNativeCode]
	public struct AnimatorTransitionInfo
	{
		// Token: 0x04000055 RID: 85
		[NativeName("fullPathHash")]
		private int m_FullPath;

		// Token: 0x04000056 RID: 86
		[NativeName("userNameHash")]
		private int m_UserName;

		// Token: 0x04000057 RID: 87
		[NativeName("nameHash")]
		private int m_Name;

		// Token: 0x04000058 RID: 88
		[NativeName("hasFixedDuration")]
		private bool m_HasFixedDuration;

		// Token: 0x04000059 RID: 89
		[NativeName("duration")]
		private float m_Duration;

		// Token: 0x0400005A RID: 90
		[NativeName("normalizedTime")]
		private float m_NormalizedTime;

		// Token: 0x0400005B RID: 91
		[NativeName("anyState")]
		private bool m_AnyState;

		// Token: 0x0400005C RID: 92
		[NativeName("transitionType")]
		private int m_TransitionType;
	}
}

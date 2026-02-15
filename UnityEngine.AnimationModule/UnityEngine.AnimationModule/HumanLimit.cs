using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeType(CodegenOptions.Custom, "MonoHumanLimit")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public struct HumanLimit
	{
		// Token: 0x040000A5 RID: 165
		private Vector3 m_Min;

		// Token: 0x040000A6 RID: 166
		private Vector3 m_Max;

		// Token: 0x040000A7 RID: 167
		private Vector3 m_Center;

		// Token: 0x040000A8 RID: 168
		private float m_AxisLength;

		// Token: 0x040000A9 RID: 169
		private int m_UseDefaultValues;
	}
}

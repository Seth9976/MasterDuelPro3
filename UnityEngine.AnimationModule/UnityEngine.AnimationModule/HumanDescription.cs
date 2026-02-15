using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanDescription
	{
		// Token: 0x040000AD RID: 173
		[NativeName("m_Human")]
		public HumanBone[] human;

		// Token: 0x040000AE RID: 174
		[NativeName("m_Skeleton")]
		public SkeletonBone[] skeleton;

		// Token: 0x040000AF RID: 175
		internal float m_ArmTwist;

		// Token: 0x040000B0 RID: 176
		internal float m_ForeArmTwist;

		// Token: 0x040000B1 RID: 177
		internal float m_UpperLegTwist;

		// Token: 0x040000B2 RID: 178
		internal float m_LegTwist;

		// Token: 0x040000B3 RID: 179
		internal float m_ArmStretch;

		// Token: 0x040000B4 RID: 180
		internal float m_LegStretch;

		// Token: 0x040000B5 RID: 181
		internal float m_FeetSpacing;

		// Token: 0x040000B6 RID: 182
		internal float m_GlobalScale;

		// Token: 0x040000B7 RID: 183
		internal string m_RootMotionBoneName;

		// Token: 0x040000B8 RID: 184
		internal bool m_HasTranslationDoF;

		// Token: 0x040000B9 RID: 185
		internal bool m_HasExtraRoot;

		// Token: 0x040000BA RID: 186
		internal bool m_SkeletonHasParents;
	}
}

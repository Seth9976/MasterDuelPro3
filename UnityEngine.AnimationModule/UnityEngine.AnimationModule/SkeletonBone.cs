using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeType(CodegenOptions.Custom, "MonoSkeletonBone")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct SkeletonBone
	{
		// Token: 0x040000A0 RID: 160
		[NativeName("m_Name")]
		public string name;

		// Token: 0x040000A1 RID: 161
		[NativeName("m_ParentName")]
		internal string parentName;

		// Token: 0x040000A2 RID: 162
		[NativeName("m_Position")]
		public Vector3 position;

		// Token: 0x040000A3 RID: 163
		[NativeName("m_Rotation")]
		public Quaternion rotation;

		// Token: 0x040000A4 RID: 164
		[NativeName("m_Scale")]
		public Vector3 scale;
	}
}

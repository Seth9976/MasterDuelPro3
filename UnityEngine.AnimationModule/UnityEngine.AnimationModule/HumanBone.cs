using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanBone
	{
		// Token: 0x040000AA RID: 170
		private string m_BoneName;

		// Token: 0x040000AB RID: 171
		private string m_HumanName;

		// Token: 0x040000AC RID: 172
		[NativeName("m_Limit")]
		public HumanLimit limit;
	}
}

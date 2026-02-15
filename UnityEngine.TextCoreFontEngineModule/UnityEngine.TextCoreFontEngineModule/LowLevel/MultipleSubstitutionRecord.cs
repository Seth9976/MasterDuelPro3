using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000017 RID: 23
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct MultipleSubstitutionRecord
	{
		// Token: 0x04000089 RID: 137
		[SerializeField]
		[NativeName("targetGlyphID")]
		private uint m_TargetGlyphID;

		// Token: 0x0400008A RID: 138
		[NativeName("substituteGlyphIDs")]
		[SerializeField]
		private uint[] m_SubstituteGlyphIDs;
	}
}

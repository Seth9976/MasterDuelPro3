using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F1 RID: 497
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class EnumParameter<T> : VolumeParameter<T>
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x00033FED File Offset: 0x000321ED
		public EnumParameter(T value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}

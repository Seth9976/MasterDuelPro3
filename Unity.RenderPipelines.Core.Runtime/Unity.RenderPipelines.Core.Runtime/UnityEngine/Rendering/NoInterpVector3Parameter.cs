using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020D RID: 525
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x000344EB File Offset: 0x000326EB
		public NoInterpVector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}

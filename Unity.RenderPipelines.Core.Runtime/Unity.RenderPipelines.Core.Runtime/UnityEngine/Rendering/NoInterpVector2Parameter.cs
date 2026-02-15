using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020B RID: 523
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x00034490 File Offset: 0x00032690
		public NoInterpVector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}

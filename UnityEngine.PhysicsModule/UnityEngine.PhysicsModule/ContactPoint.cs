using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public struct ContactPoint
	{
		// Token: 0x04000001 RID: 1
		internal Vector3 m_Point;

		// Token: 0x04000002 RID: 2
		internal Vector3 m_Normal;

		// Token: 0x04000003 RID: 3
		internal Vector3 m_Impulse;

		// Token: 0x04000004 RID: 4
		internal int m_ThisColliderInstanceID;

		// Token: 0x04000005 RID: 5
		internal int m_OtherColliderInstanceID;

		// Token: 0x04000006 RID: 6
		internal float m_Separation;
	}
}

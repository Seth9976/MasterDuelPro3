using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ControllerColliderHit
	{
		// Token: 0x0400000B RID: 11
		internal CharacterController m_Controller;

		// Token: 0x0400000C RID: 12
		internal Collider m_Collider;

		// Token: 0x0400000D RID: 13
		internal Vector3 m_Point;

		// Token: 0x0400000E RID: 14
		internal Vector3 m_Normal;

		// Token: 0x0400000F RID: 15
		internal Vector3 m_MoveDirection;

		// Token: 0x04000010 RID: 16
		internal float m_MoveLength;

		// Token: 0x04000011 RID: 17
		internal int m_Push;
	}
}

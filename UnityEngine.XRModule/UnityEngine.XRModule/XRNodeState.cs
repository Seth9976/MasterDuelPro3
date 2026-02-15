using System;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000006 RID: 6
	[UsedByNativeCode]
	public struct XRNodeState
	{
		// Token: 0x17000001 RID: 1
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		public ulong uniqueID
		{
			set
			{
				this.m_UniqueID = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FA File Offset: 0x000002FA
		public XRNode nodeType
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public bool tracked
		{
			set
			{
				this.m_Tracked = (value ? 1 : 0);
			}
		}

		// Token: 0x0400001C RID: 28
		private XRNode m_Type;

		// Token: 0x0400001D RID: 29
		private AvailableTrackingData m_AvailableFields;

		// Token: 0x0400001E RID: 30
		private Vector3 m_Position;

		// Token: 0x0400001F RID: 31
		private Quaternion m_Rotation;

		// Token: 0x04000020 RID: 32
		private Vector3 m_Velocity;

		// Token: 0x04000021 RID: 33
		private Vector3 m_AngularVelocity;

		// Token: 0x04000022 RID: 34
		private Vector3 m_Acceleration;

		// Token: 0x04000023 RID: 35
		private Vector3 m_AngularAcceleration;

		// Token: 0x04000024 RID: 36
		private int m_Tracked;

		// Token: 0x04000025 RID: 37
		private ulong m_UniqueID;
	}
}

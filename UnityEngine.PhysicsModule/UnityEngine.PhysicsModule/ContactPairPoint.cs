using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	public readonly struct ContactPairPoint
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030F9 File Offset: 0x000012F9
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003101 File Offset: 0x00001301
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003109 File Offset: 0x00001309
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003111 File Offset: 0x00001311
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x04000039 RID: 57
		internal readonly Vector3 m_Position;

		// Token: 0x0400003A RID: 58
		internal readonly float m_Separation;

		// Token: 0x0400003B RID: 59
		internal readonly Vector3 m_Normal;

		// Token: 0x0400003C RID: 60
		internal readonly uint m_InternalFaceIndex0;

		// Token: 0x0400003D RID: 61
		internal readonly Vector3 m_Impulse;

		// Token: 0x0400003E RID: 62
		internal readonly uint m_InternalFaceIndex1;
	}
}

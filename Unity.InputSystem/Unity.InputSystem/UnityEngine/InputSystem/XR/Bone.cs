using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000F0 RID: 240
	public struct Bone
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0003EF73 File Offset: 0x0003D173
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0003EF7B File Offset: 0x0003D17B
		public uint parentBoneIndex
		{
			get
			{
				return this.m_ParentBoneIndex;
			}
			set
			{
				this.m_ParentBoneIndex = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0003EF84 File Offset: 0x0003D184
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0003EF8C File Offset: 0x0003D18C
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0003EF95 File Offset: 0x0003D195
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0003EF9D File Offset: 0x0003D19D
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x0400058A RID: 1418
		public uint m_ParentBoneIndex;

		// Token: 0x0400058B RID: 1419
		public Vector3 m_Position;

		// Token: 0x0400058C RID: 1420
		public Quaternion m_Rotation;
	}
}

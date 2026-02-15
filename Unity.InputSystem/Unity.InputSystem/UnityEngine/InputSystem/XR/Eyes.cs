using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000F1 RID: 241
	public struct Eyes
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0003EFA6 File Offset: 0x0003D1A6
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0003EFAE File Offset: 0x0003D1AE
		public Vector3 leftEyePosition
		{
			get
			{
				return this.m_LeftEyePosition;
			}
			set
			{
				this.m_LeftEyePosition = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0003EFB7 File Offset: 0x0003D1B7
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0003EFBF File Offset: 0x0003D1BF
		public Quaternion leftEyeRotation
		{
			get
			{
				return this.m_LeftEyeRotation;
			}
			set
			{
				this.m_LeftEyeRotation = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0003EFC8 File Offset: 0x0003D1C8
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		public Vector3 rightEyePosition
		{
			get
			{
				return this.m_RightEyePosition;
			}
			set
			{
				this.m_RightEyePosition = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0003EFD9 File Offset: 0x0003D1D9
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0003EFE1 File Offset: 0x0003D1E1
		public Quaternion rightEyeRotation
		{
			get
			{
				return this.m_RightEyeRotation;
			}
			set
			{
				this.m_RightEyeRotation = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0003EFEA File Offset: 0x0003D1EA
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0003EFF2 File Offset: 0x0003D1F2
		public Vector3 fixationPoint
		{
			get
			{
				return this.m_FixationPoint;
			}
			set
			{
				this.m_FixationPoint = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0003EFFB File Offset: 0x0003D1FB
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0003F003 File Offset: 0x0003D203
		public float leftEyeOpenAmount
		{
			get
			{
				return this.m_LeftEyeOpenAmount;
			}
			set
			{
				this.m_LeftEyeOpenAmount = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003F00C File Offset: 0x0003D20C
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0003F014 File Offset: 0x0003D214
		public float rightEyeOpenAmount
		{
			get
			{
				return this.m_RightEyeOpenAmount;
			}
			set
			{
				this.m_RightEyeOpenAmount = value;
			}
		}

		// Token: 0x0400058D RID: 1421
		public Vector3 m_LeftEyePosition;

		// Token: 0x0400058E RID: 1422
		public Quaternion m_LeftEyeRotation;

		// Token: 0x0400058F RID: 1423
		public Vector3 m_RightEyePosition;

		// Token: 0x04000590 RID: 1424
		public Quaternion m_RightEyeRotation;

		// Token: 0x04000591 RID: 1425
		public Vector3 m_FixationPoint;

		// Token: 0x04000592 RID: 1426
		public float m_LeftEyeOpenAmount;

		// Token: 0x04000593 RID: 1427
		public float m_RightEyeOpenAmount;
	}
}

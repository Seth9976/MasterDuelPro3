using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[RequiredByNativeCode]
	public struct AnimatorStateInfo
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public float speed
		{
			get
			{
				return this.m_Speed;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E08 File Offset: 0x00001008
		public float speedMultiplier
		{
			get
			{
				return this.m_SpeedMultiplier;
			}
		}

		// Token: 0x0400004C RID: 76
		private int m_Name;

		// Token: 0x0400004D RID: 77
		private int m_Path;

		// Token: 0x0400004E RID: 78
		private int m_FullPath;

		// Token: 0x0400004F RID: 79
		private float m_NormalizedTime;

		// Token: 0x04000050 RID: 80
		private float m_Length;

		// Token: 0x04000051 RID: 81
		private float m_Speed;

		// Token: 0x04000052 RID: 82
		private float m_SpeedMultiplier;

		// Token: 0x04000053 RID: 83
		private int m_Tag;

		// Token: 0x04000054 RID: 84
		private int m_Loop;
	}
}

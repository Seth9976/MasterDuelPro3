using System;

namespace UnityEngine
{
	// Token: 0x020001CA RID: 458
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00026166 File Offset: 0x00024366
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0002616E File Offset: 0x0002436E
		public float waitTime { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00026178 File Offset: 0x00024378
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_WaitUntilTime < 0f;
				if (flag)
				{
					this.m_WaitUntilTime = Time.realtimeSinceStartup + this.waitTime;
				}
				bool wait = Time.realtimeSinceStartup < this.m_WaitUntilTime;
				bool flag2 = !wait;
				if (flag2)
				{
					this.Reset();
				}
				return wait;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000261CF File Offset: 0x000243CF
		public WaitForSecondsRealtime(float time)
		{
			this.waitTime = time;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000261EC File Offset: 0x000243EC
		public override void Reset()
		{
			this.m_WaitUntilTime = -1f;
		}

		// Token: 0x040006AB RID: 1707
		private float m_WaitUntilTime = -1f;
	}
}

using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000097 RID: 151
	public abstract class AbstractEventData
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00018B44 File Offset: 0x00016D44
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00018B4D File Offset: 0x00016D4D
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00018B56 File Offset: 0x00016D56
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x040002AC RID: 684
		protected bool m_Used;
	}
}

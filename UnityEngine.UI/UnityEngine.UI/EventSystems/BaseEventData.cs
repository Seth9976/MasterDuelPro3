using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000098 RID: 152
	public class BaseEventData : AbstractEventData
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00018B5E File Offset: 0x00016D5E
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00018B6D File Offset: 0x00016D6D
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_EventSystem.currentInputModule;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00018B7A File Offset: 0x00016D7A
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00018B87 File Offset: 0x00016D87
		public GameObject selectedObject
		{
			get
			{
				return this.m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x040002AD RID: 685
		private readonly EventSystem m_EventSystem;
	}
}

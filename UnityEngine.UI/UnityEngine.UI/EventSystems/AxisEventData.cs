using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000096 RID: 150
	public class AxisEventData : BaseEventData
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00018B07 File Offset: 0x00016D07
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00018B0F File Offset: 0x00016D0F
		public Vector2 moveVector { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00018B18 File Offset: 0x00016D18
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x00018B20 File Offset: 0x00016D20
		public MoveDirection moveDir { get; set; }

		// Token: 0x060005ED RID: 1517 RVA: 0x00018B29 File Offset: 0x00016D29
		public AxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}
	}
}

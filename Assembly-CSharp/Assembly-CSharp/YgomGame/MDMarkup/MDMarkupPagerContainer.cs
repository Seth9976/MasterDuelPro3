using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BBE RID: 3006
	[Serializable]
	public class MDMarkupPagerContainer : MDMarkupContainerBase
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060055C6 RID: 21958 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.ContainerType containerType
		{
			get
			{
				return (MDMarkupDef.ContainerType)0;
			}
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Clear()
		{
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0000216A File Offset: 0x0000036A
		public override object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x040092D5 RID: 37589
		public bool isLoop;

		// Token: 0x040092D6 RID: 37590
		[NonSerialized]
		public int startPage;
	}
}

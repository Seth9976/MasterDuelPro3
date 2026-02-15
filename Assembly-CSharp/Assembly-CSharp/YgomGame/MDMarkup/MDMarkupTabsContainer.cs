using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BCE RID: 3022
	[Serializable]
	public class MDMarkupTabsContainer : MDMarkupContainerBase
	{
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06005639 RID: 22073 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.ContainerType containerType
		{
			get
			{
				return (MDMarkupDef.ContainerType)0;
			}
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x0000216A File Offset: 0x0000036A
		public override object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009320 RID: 37664
		[NonSerialized]
		public int startTab;
	}
}

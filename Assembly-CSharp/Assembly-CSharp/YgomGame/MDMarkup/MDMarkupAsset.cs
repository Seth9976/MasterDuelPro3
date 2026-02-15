using System;
using System.Runtime.CompilerServices;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B81 RID: 2945
	public class MDMarkupAsset
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600549B RID: 21659 RVA: 0x0000216D File Offset: 0x0000036D
		public int invalidPos
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600549C RID: 21660 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isValidData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x0000216A File Offset: 0x0000036A
		public IMDMarkupContainer container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0000216A File Offset: 0x0000036A
		public static MDMarkupAsset CreateByJson(string json)
		{
			return null;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0000216A File Offset: 0x0000036A
		public static MDMarkupAsset CraeteInstanceByContainer(IMDMarkupContainer container)
		{
			return null;
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0000216A File Offset: 0x0000036A
		public static MDMarkupAsset CreateInstance(MDMarkupDef.ContainerType containerType)
		{
			return null;
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJson(string json)
		{
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0000216D File Offset: 0x0000036D
		public void Preload(Action onLoadCompleteCallback)
		{
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCheckLoadComplete()
		{
		}

		// Token: 0x040091DC RID: 37340
		private IMDMarkupContainer m_Container;

		// Token: 0x040091DD RID: 37341
		private int m_LoadingCnt;

		// Token: 0x040091DE RID: 37342
		private Action m_OnLoadCompleteCallback;
	}
}

using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x02001161 RID: 4449
	public class LogShowTagForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600848C RID: 33932 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600848D RID: 33933 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTagTypeForAnalysis type)
		{
		}

		// Token: 0x0600848E RID: 33934 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTargetTag()
		{
		}

		// Token: 0x0600848F RID: 33935 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCostTag()
		{
		}

		// Token: 0x0400BFD7 RID: 49111
		[SerializeField]
		protected string m_Label_TagName;

		// Token: 0x0400BFD8 RID: 49112
		private ElementObjectManager m_EOManager_Origin;
	}
}

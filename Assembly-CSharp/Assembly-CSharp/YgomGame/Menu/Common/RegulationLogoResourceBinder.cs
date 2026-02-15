using System;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B56 RID: 2902
	public class RegulationLogoResourceBinder : ResourceBinderBase
	{
		// Token: 0x0600540C RID: 21516 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(RegulationLogoResourceBinder.RegulationLogoPathData pathData)
		{
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetLogoPath(int id)
		{
			return null;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindEventLogo(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x04009174 RID: 37236
		private RegulationLogoResourceBinder.RegulationLogoPathData m_PathData;

		// Token: 0x02000B57 RID: 2903
		[Serializable]
		public class RegulationLogoPathData
		{
			// Token: 0x04009175 RID: 37237
			public string m_RegulationLogoPath;
		}
	}
}

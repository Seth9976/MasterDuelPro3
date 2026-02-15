using System;
using UnityEngine.UI;
using YgomGame.Card;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B55 RID: 2901
	public class RegulationIconBinder : ResourceBinderBase
	{
		// Token: 0x06005409 RID: 21513 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRegulationIconLabel(CardCollectionInfo.Regulation reg)
		{
			return null;
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingSpriteContainer BindRegulationIcon(Image target, CardCollectionInfo.Regulation reg, bool async = true)
		{
			return null;
		}

		// Token: 0x04009173 RID: 37235
		private const string rarityLabelCommonFormat = "Limit{0}";
	}
}

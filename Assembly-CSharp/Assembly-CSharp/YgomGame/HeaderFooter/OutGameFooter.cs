using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.HeaderFooter
{
	// Token: 0x02000BFA RID: 3066
	public class OutGameFooter : MonoBehaviour
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<OutGameFooter> onComplete = null)
		{
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShortcut(SelectorManager.KeyType keyType, string label, Action onComplete = null, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveShortcut(SelectorManager.KeyType keyType)
		{
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateTemplate(SelectorManager.KeyType keyType, string label, Action onComplete = null, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return null;
		}

		// Token: 0x040093BF RID: 37823
		private readonly string ROOT_LABEL;

		// Token: 0x040093C0 RID: 37824
		private readonly string TMP_LABEL;

		// Token: 0x040093C1 RID: 37825
		private readonly string TXT_LABEL;

		// Token: 0x040093C2 RID: 37826
		private readonly string IMG_LABEL;

		// Token: 0x040093C3 RID: 37827
		private ElementObjectManager elementObjectManager;

		// Token: 0x040093C4 RID: 37828
		private Dictionary<SelectorManager.KeyType, GameObject> shortcutDic;
	}
}

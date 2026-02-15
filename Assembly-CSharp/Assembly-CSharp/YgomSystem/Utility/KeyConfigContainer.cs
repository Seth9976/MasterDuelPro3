using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000528 RID: 1320
	public class KeyConfigContainer : ScriptableObject
	{
		// Token: 0x06002A6D RID: 10861 RVA: 0x000F1C30 File Offset: 0x000EFE30
		public ValueTuple<SelectorManager.KeyType, SelectorManager.KeyType> GetKeyType(string label)
		{
			return default(ValueTuple<SelectorManager.KeyType, SelectorManager.KeyType>);
		}

		// Token: 0x04002998 RID: 10648
		public List<KeyConfigContainer.KeyConfig> keyConfigList;

		// Token: 0x02000529 RID: 1321
		[Serializable]
		public class KeyConfig
		{
			// Token: 0x04002999 RID: 10649
			public string label;

			// Token: 0x0400299A RID: 10650
			public SelectorManager.KeyType keyTypeMain;

			// Token: 0x0400299B RID: 10651
			public SelectorManager.KeyType keyTypeSub;
		}
	}
}

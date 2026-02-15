using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000525 RID: 1317
	public class KeyCommandSetting : ScriptableObject
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x0000216A File Offset: 0x0000036A
		public KeyCommandSetting.KeyCommandInfo Get(string label)
		{
			return null;
		}

		// Token: 0x04002993 RID: 10643
		public List<KeyCommandSetting.KeyCommandInfo> infoList;

		// Token: 0x02000526 RID: 1318
		[Serializable]
		public class KeyCommandInfo
		{
			// Token: 0x04002994 RID: 10644
			public string label;

			// Token: 0x04002995 RID: 10645
			public List<SelectorManager.KeyType> keyList;
		}
	}
}

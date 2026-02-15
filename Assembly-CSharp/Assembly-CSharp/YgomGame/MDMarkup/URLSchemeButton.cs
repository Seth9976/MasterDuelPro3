using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Utility;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD9 RID: 3033
	[Serializable]
	public class URLSchemeButton
	{
		// Token: 0x06005665 RID: 22117 RVA: 0x00002739 File Offset: 0x00000939
		public URLSchemeButton()
		{
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00002739 File Offset: 0x00000939
		public URLSchemeButton(string url, string rawLabel, SelectorManager.KeyType shortcut)
		{
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0000216A File Offset: 0x0000036A
		public static object ExportJsonObj(List<URLSchemeButton> buttons)
		{
			return null;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<URLSchemeButton> ImportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0400934C RID: 37708
		[SerializeField]
		public string url;

		// Token: 0x0400934D RID: 37709
		[SerializeField]
		public GlobalTextData label;

		// Token: 0x0400934E RID: 37710
		[SerializeField]
		public SelectorManager.KeyType shortcut;

		// Token: 0x0400934F RID: 37711
		[SerializeField]
		public bool interactable;
	}
}

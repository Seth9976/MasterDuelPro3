using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B85 RID: 2949
	[Serializable]
	public class MDMarkupBannerContext
	{
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isValid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0000216D File Offset: 0x0000036D
		public void CopyTo(MDMarkupBannerContext other, bool deep = false)
		{
		}

		// Token: 0x040091FD RID: 37373
		private const string k_JKeyPrefPath = "prefPath";

		// Token: 0x040091FE RID: 37374
		private const string k_JKeyPrefArgsJson = "prefArgsJson";

		// Token: 0x040091FF RID: 37375
		public string prefPath;

		// Token: 0x04009200 RID: 37376
		[TextArea]
		public string prefArgsJson;

		// Token: 0x04009201 RID: 37377
		public Dictionary<string, object> prefArgs;
	}
}

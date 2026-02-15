using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Htjson
{
	// Token: 0x02000758 RID: 1880
	public interface HtjsonContext
	{
		// Token: 0x06003AC0 RID: 15040
		void SetTextColor(Color col);

		// Token: 0x06003AC1 RID: 15041
		Color GetTextColor();

		// Token: 0x06003AC2 RID: 15042
		void InsertItem(Transform item);

		// Token: 0x06003AC3 RID: 15043
		void SetStyle(string id, object dic);

		// Token: 0x06003AC4 RID: 15044
		Dictionary<string, object> GetStyle(string id);

		// Token: 0x06003AC5 RID: 15045
		HtjsonReceiver GetReceiver();

		// Token: 0x06003AC6 RID: 15046
		string ProcPath(string path);

		// Token: 0x06003AC7 RID: 15047
		void AddReplaceParam(Dictionary<string, object> param);

		// Token: 0x06003AC8 RID: 15048
		Dictionary<string, object> GetReplaceParam();
	}
}

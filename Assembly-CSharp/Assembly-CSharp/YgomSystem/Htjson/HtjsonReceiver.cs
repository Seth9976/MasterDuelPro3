using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Htjson
{
	// Token: 0x0200075C RID: 1884
	public interface HtjsonReceiver
	{
		// Token: 0x06003AE5 RID: 15077
		void HtjsonLink(object path);

		// Token: 0x06003AE6 RID: 15078
		void SetOption(string key, object value);

		// Token: 0x06003AE7 RID: 15079
		object GetOption(string key);

		// Token: 0x06003AE8 RID: 15080
		Dictionary<string, object> GetOptions();

		// Token: 0x06003AE9 RID: 15081
		void RegisterId(string id, GameObject entry);

		// Token: 0x06003AEA RID: 15082
		List<GameObject> GetIdObjects(string id);

		// Token: 0x06003AEB RID: 15083
		string GetIdObjectName(GameObject go);
	}
}

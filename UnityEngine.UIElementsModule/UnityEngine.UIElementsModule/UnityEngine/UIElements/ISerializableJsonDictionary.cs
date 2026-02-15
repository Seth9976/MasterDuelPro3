using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026A RID: 618
	internal interface ISerializableJsonDictionary
	{
		// Token: 0x060010C7 RID: 4295
		void Set<T>(string key, T value) where T : class;

		// Token: 0x060010C8 RID: 4296
		T Get<T>(string key) where T : class;

		// Token: 0x060010C9 RID: 4297
		void Overwrite(object obj, string key);

		// Token: 0x060010CA RID: 4298
		bool ContainsKey(string key);
	}
}

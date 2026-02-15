using System;
using System.Collections;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006E0 RID: 1760
	public interface IResourceLoader
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060036E8 RID: 14056
		bool DisablePathForErrorHandler { get; }

		// Token: 0x060036E9 RID: 14057
		void Initialize();

		// Token: 0x060036EA RID: 14058
		void Load(Resource res, uint crc);

		// Token: 0x060036EB RID: 14059
		IEnumerator LoadAsync(Resource res, uint key);

		// Token: 0x060036EC RID: 14060
		void LateUpdate();

		// Token: 0x060036ED RID: 14061
		void ClearCache();
	}
}

using System;
using System.Collections;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006E4 RID: 1764
	public class NetworkLoader : BaseLoader
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool DisablePathForErrorHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(Resource res, uint crc)
		{
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator LoadAsync(Resource res, uint key)
		{
			return null;
		}

		// Token: 0x04003145 RID: 12613
		public ResourceManager.ProgressHandler progressHandler;

		// Token: 0x04003146 RID: 12614
		public ResourceManager.RetryHandler retryHandler;

		// Token: 0x04003147 RID: 12615
		public float HttpTimeOut;
	}
}

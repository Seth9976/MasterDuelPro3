using System;
using System.Collections;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006DC RID: 1756
	public class BuiltInLoader : BaseLoader
	{
		// Token: 0x060036CA RID: 14026 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(Resource res, uint crc)
		{
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator LoadAsync(Resource res, uint key)
		{
			return null;
		}
	}
}

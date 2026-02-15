using System;
using System.Collections;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006DA RID: 1754
	public class BinaryLoader : BaseLoader
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(Resource res, uint crc)
		{
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator LoadAsync(Resource res, uint key)
		{
			return null;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual byte[] LoadFromFile(string path)
		{
			return null;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string GetNativePath(string path)
		{
			return null;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual IEnumerator LoadFromStreamFile(string path, Action<byte[]> callback)
		{
			return null;
		}
	}
}

using System;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000756 RID: 1878
	public class WindowsStorageIO : StandardStorageIO
	{
		// Token: 0x06003AAA RID: 15018 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetHashRootNativePath(Storage storage)
		{
			return null;
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetSteamUserDirectoryName()
		{
			return null;
		}
	}
}

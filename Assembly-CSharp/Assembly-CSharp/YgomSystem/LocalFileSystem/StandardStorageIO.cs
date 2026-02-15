using System;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x0200074E RID: 1870
	public class StandardStorageIO : StorageIO
	{
		// Token: 0x06003A6B RID: 14955 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setupStorageDirectory(Storage storage, string mountPath)
		{
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0000216D File Offset: 0x0000036D
		private static void createStorageDirectory(string hashRoot, string plainRoot)
		{
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool ExistsFile(string nativePath)
		{
			return false;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0000216D File Offset: 0x0000036D
		public override void DeleteFile(string nativePath)
		{
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000F1669 File Offset: 0x000EF869
		public override long GetFileSize(string nativePath)
		{
			return 0L;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x0000216D File Offset: 0x0000036D
		public override void MoveFile(string srcNativePath, string dstNativePath)
		{
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool ExistsDirectory(string nativePath)
		{
			return false;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CreateDirectory(string nativePath)
		{
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x0000216A File Offset: 0x0000036A
		public override string[] GetDirectories(string nativePath)
		{
			return null;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x0000216A File Offset: 0x0000036A
		public override string[] GetFiles(string nativePath, string searchPattern)
		{
			return null;
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x0000216D File Offset: 0x0000036D
		public override void DeleteDirectory(string nativePath)
		{
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0000216D File Offset: 0x0000036D
		public override void MoveDirectory(string srcNativePath, string dstNativePath)
		{
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x0000216A File Offset: 0x0000036A
		public override string GetStreamingAssetNativePath(string name)
		{
			return null;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool ExistsStreamingAsset(string name)
		{
			return false;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0000216A File Offset: 0x0000036A
		public override byte[] ReadStreamingAsset(string name)
		{
			return null;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ReadStreamingAssetCallback(string name, Action<byte[]> readCallback)
		{
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ClearLocalDataStorage(Action finishCallback)
		{
		}

		// Token: 0x04003446 RID: 13382
		private const int HASHDIRNUM = 256;

		// Token: 0x04003447 RID: 13383
		private const string PLAINROOTDIR = "root";
	}
}

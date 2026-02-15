using System;
using System.Runtime.CompilerServices;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000751 RID: 1873
	public abstract class StorageIO
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x0000216D File Offset: 0x0000036D
		private protected string environment
		{
			[CompilerGenerated]
			protected get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0000216A File Offset: 0x0000036A
		protected StorageIO.StorageInfo getStorageInfo(Storage type)
		{
			return null;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setupStorageInfo(Storage storage, string mountPath, string envRoot, string hashRootPath, bool useHashDir, string plainRootPath)
		{
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(string envName)
		{
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x0000216A File Offset: 0x0000036A
		public StorageIO.StorageInfo GetStorageInfo(Storage type)
		{
			return null;
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetMountNativePath(Storage storage)
		{
			return null;
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetEnvRootNativePath(Storage storage)
		{
			return null;
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetHashedFileNativePath(Storage storage, string hash)
		{
			return null;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlainNativePath(Storage storage, string path)
		{
			return null;
		}

		// Token: 0x06003A8A RID: 14986
		protected abstract void OnInitialize();

		// Token: 0x06003A8B RID: 14987 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnTerminate()
		{
		}

		// Token: 0x06003A8C RID: 14988
		public abstract bool ExistsFile(string nativePath);

		// Token: 0x06003A8D RID: 14989
		public abstract void DeleteFile(string nativePath);

		// Token: 0x06003A8E RID: 14990
		public abstract long GetFileSize(string nativePath);

		// Token: 0x06003A8F RID: 14991
		public abstract void MoveFile(string srcNativePath, string dstNativePath);

		// Token: 0x06003A90 RID: 14992
		public abstract bool ExistsDirectory(string nativePath);

		// Token: 0x06003A91 RID: 14993
		public abstract void CreateDirectory(string nativePath);

		// Token: 0x06003A92 RID: 14994
		public abstract string[] GetDirectories(string nativePath);

		// Token: 0x06003A93 RID: 14995
		public abstract string[] GetFiles(string nativePath, string searchPattern);

		// Token: 0x06003A94 RID: 14996
		public abstract void DeleteDirectory(string nativePath);

		// Token: 0x06003A95 RID: 14997
		public abstract void MoveDirectory(string srcNativePath, string dstNativePath);

		// Token: 0x06003A96 RID: 14998
		public abstract string GetStreamingAssetNativePath(string name);

		// Token: 0x06003A97 RID: 14999
		public abstract bool ExistsStreamingAsset(string name);

		// Token: 0x06003A98 RID: 15000
		public abstract byte[] ReadStreamingAsset(string name);

		// Token: 0x06003A99 RID: 15001
		public abstract void ReadStreamingAssetCallback(string name, Action<byte[]> readCallback);

		// Token: 0x06003A9A RID: 15002
		public abstract void ClearLocalDataStorage(Action finishCallback);

		// Token: 0x04003450 RID: 13392
		private StorageIO.StorageInfo[] m_storageInfos;

		// Token: 0x02000752 RID: 1874
		public class StorageInfo
		{
			// Token: 0x06003A9C RID: 15004 RVA: 0x00002739 File Offset: 0x00000939
			public StorageInfo(Storage st)
			{
			}

			// Token: 0x04003451 RID: 13393
			public Storage storage;

			// Token: 0x04003452 RID: 13394
			public string mountPath;

			// Token: 0x04003453 RID: 13395
			public bool isWritable;

			// Token: 0x04003454 RID: 13396
			public string envRoot;

			// Token: 0x04003455 RID: 13397
			public string writeHashRoot;

			// Token: 0x04003456 RID: 13398
			public bool useHashDir;

			// Token: 0x04003457 RID: 13399
			public string writePlainRoot;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace YgomSystem.LocalFileSystem.Internal
{
	// Token: 0x02000757 RID: 1879
	public static class Helper
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x0000216D File Offset: 0x0000036D
		public static LocalFileManager manager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x0000216D File Offset: 0x0000036D
		public static StorageIO storageIO
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(string env, LocalFileManager managerInstance, bool enableLog)
		{
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Cleanup()
		{
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F3800 File Offset: 0x000F1A00
		public static T GetStorageIO<T>() where T : StorageIO
		{
			return default(T);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x0000216A File Offset: 0x0000036A
		private static StorageIO createStorageIO()
		{
			return null;
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x0000216A File Offset: 0x0000036A
		public static Stream CreateIOStream(string nativePath, StreamOpenMode openMode, FileLocation location)
		{
			return null;
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0000216A File Offset: 0x0000036A
		private static FileStream createSystemIOFileStream(string nativePath, StreamOpenMode openMode)
		{
			return null;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetNativePath(Storage storage, string name, FileNameType nameType)
		{
			return null;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0000216A File Offset: 0x0000036A
		public static string LocationToNativePath(FileLocation location)
		{
			return null;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x0000216A File Offset: 0x0000036A
		public static StorageData GetStorageData(Storage storage)
		{
			return null;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStorageName(Storage storage)
		{
			return null;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Storage NameToStorage(string name, bool ignoreCase)
		{
			return Storage.None;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Storage NameToStorage(string name)
		{
			return Storage.None;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0000216A File Offset: 0x0000036A
		public static string HashFileName(string name)
		{
			return null;
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] SplitHash(string hash)
		{
			return null;
		}

		// Token: 0x0400345E RID: 13406
		private static readonly StorageData[] s_storageDescriptions;

		// Token: 0x0400345F RID: 13407
		private static Dictionary<int, StorageData> s_storages;
	}
}

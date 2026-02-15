using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000745 RID: 1861
	public static class LocalFile
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x0000216A File Offset: 0x0000036A
		public static LocalFileManager manager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(string env)
		{
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(RuntimeEnvironment.ServerType serverType)
		{
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInitialized()
		{
			return false;
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000F3744 File Offset: 0x000F1944
		public static T GetStorageIO<T>() where T : StorageIO
		{
			return default(T);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x0000216D File Offset: 0x0000036D
		private static void dispatchLocationNameType(FileNameType nameType, Action nomralCallback, Action plainCallback, Action rawCallback)
		{
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsFile(Storage storage, string name, FileNameType nameType)
		{
			return false;
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsFile(Storage storage, string name)
		{
			return false;
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsFile(FileLocation location)
		{
			return false;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsFile(string locationString)
		{
			return false;
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteFile(Storage storage, string name, FileNameType nameType, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteFile(Storage storage, string name, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteFile(FileLocation location, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteFile(string locationString, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteTextFile(Storage storage, string name, FileNameType nameType, string text, bool asNewFile)
		{
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteTextFile(Storage storage, string name, string text, bool asNewFile)
		{
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteTextFile(FileLocation location, string text, bool asNewFile)
		{
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteTextFile(string locationString, string text, bool asNewFile)
		{
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadFile(Storage storage, string name, FileNameType nameType)
		{
			return null;
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadFile(Storage storage, string name)
		{
			return null;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadFile(FileLocation location)
		{
			return null;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadFile(string locationString)
		{
			return null;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadTextFile(Storage storage, string name, FileNameType nameType)
		{
			return null;
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadTextFile(Storage storage, string name)
		{
			return null;
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadTextFile(FileLocation location)
		{
			return null;
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadTextFile(string locationString)
		{
			return null;
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteFile(Storage storage, string name, FileNameType nameType)
		{
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteFile(Storage storage, string name)
		{
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteFile(FileLocation location)
		{
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteFile(string locationString)
		{
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetFileSize(Storage storage, string name, FileNameType nameType)
		{
			return 0L;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetFileSize(Storage storage, string name)
		{
			return 0L;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetFileSize(FileLocation location)
		{
			return 0L;
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetFileSize(string locationString)
		{
			return 0L;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLocationNativePath(FileLocation location)
		{
			return null;
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLocationNativePath(string locationString)
		{
			return null;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetHashFileNativePath(Storage storage, string name)
		{
			return null;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsHashFile(Storage storage, string name)
		{
			return false;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteHashFile(Storage storage, string name, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteHashTextFile(Storage storage, string name, string text, bool asNewFile)
		{
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadHashFile(Storage storage, string name)
		{
			return null;
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadHashTextFile(Storage storage, string name)
		{
			return null;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadFileHeader(Storage storage, string name, int length)
		{
			return null;
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteHashFile(Storage storage, string name)
		{
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetHashFileSize(Storage storage, string name)
		{
			return 0L;
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsRawHashFile(Storage storage, string hash)
		{
			return false;
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteRawHashFile(Storage storage, string hash, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadRawHashFile(Storage storage, string hash)
		{
			return null;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadRawHashFileHeader(Storage storage, string hash, int length)
		{
			return null;
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteRawHashFile(Storage storage, string hash)
		{
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetRawHashFileSize(Storage storage, string hash)
		{
			return 0L;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPlainNativePath(Storage storage, string path)
		{
			return null;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsPlainFile(Storage storage, string path)
		{
			return false;
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WritePlainFile(Storage storage, string path, byte[] writeData, bool asNewFile)
		{
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WritePlainTextFile(Storage storage, string path, string text, bool asNewFile)
		{
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadPlainFile(Storage storage, string path)
		{
			return null;
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ReadPlainTextFile(Storage storage, string path)
		{
			return null;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeletePlainFile(Storage storage, string path)
		{
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetPlainFileSize(Storage storage, string path)
		{
			return 0L;
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MovePlainFile(Storage storage, string srcPath, string dstPath)
		{
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x0000216A File Offset: 0x0000036A
		public static EntryItem[] GetPlainFiles(Storage storage, string path = "", string searchPattern = "")
		{
			return null;
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsDirectory(Storage storage, string path)
		{
			return false;
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateDirectory(Storage storage, string path)
		{
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x0000216A File Offset: 0x0000036A
		public static EntryItem[] GetDirectories(Storage storage, string path = "")
		{
			return null;
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeleteDirectory(Storage storage, string path)
		{
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MoveDirectory(Storage storage, string srcPath, string dstPath)
		{
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStreamingAssetNativePath(string name)
		{
			return null;
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsStreamingAsset(string name)
		{
			return false;
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadStreamingAsset(string name)
		{
			return null;
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] ReadStreamingAssetHeader(string name, int length)
		{
			return null;
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReadStreamingAssetCallback(string name, Action<byte[]> readCallback)
		{
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0000216A File Offset: 0x0000036A
		private static string sanitizeAssetBundlePath(string path)
		{
			return null;
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistAssetBundle(string path)
		{
			return false;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x0000216A File Offset: 0x0000036A
		private static string getAssetBundleNativePathDownload(string sanitizedPath)
		{
			return null;
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistAssetBundleDownload(string path)
		{
			return false;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x0000216A File Offset: 0x0000036A
		public static AssetBundle LoadAssetBundleFromDownload(string path)
		{
			return null;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x0000216A File Offset: 0x0000036A
		public static LocalFileAssetBundleLoadRequest RequestAssetBundleFromDownload(string path)
		{
			return null;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x0000216A File Offset: 0x0000036A
		private static string getAssetBundleNativePathStreamingAssets(string sanitizedPath, bool relative)
		{
			return null;
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStreamingAssetsAssetBundleNativePath(string path)
		{
			return null;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStreamingAssetsAssetBundleRelativePath(string path)
		{
			return null;
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistStreamingAssetsAssetBundle(string path)
		{
			return false;
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x0000216A File Offset: 0x0000036A
		public static AssetBundle LoadAssetBundleFromStreamingAssets(string path)
		{
			return null;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0000216A File Offset: 0x0000036A
		public static LocalFileAssetBundleLoadRequest RequestAssetBundleFromStreamingAssets(string path)
		{
			return null;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAssetBundleLoadTaskLimit(int num)
		{
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetAssetBundleLoadTaskLimit()
		{
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearLocalDataStorage(Action finishCallback)
		{
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x0000216A File Offset: 0x0000036A
		public static string HashFileName(string path)
		{
			return null;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetFileCRC32(Storage storage, string name, FileNameType nameType)
		{
			return 0U;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] GetFileSHA1(Storage storage, string name, FileNameType nameType)
		{
			return null;
		}

		// Token: 0x04003420 RID: 13344
		private static LocalFileManager s_manager;
	}
}

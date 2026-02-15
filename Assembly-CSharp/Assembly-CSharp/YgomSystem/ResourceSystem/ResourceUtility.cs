using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006F2 RID: 1778
	public static class ResourceUtility
	{
		// Token: 0x0600375C RID: 14172 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isAssetBundleFile(string path, bool isStreamingAssets = false)
		{
			return false;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isAssetBundleFile(byte[] data)
		{
			return false;
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPNGFile(byte[] data)
		{
			return false;
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x0000216D File Offset: 0x0000036D
		private static void descramble(byte[] src, int ofs, byte[] dst)
		{
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkCompressed(byte[] data)
		{
			return false;
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] decompressedData(byte[] data)
		{
			return null;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] decompressedData(TextAsset textasset)
		{
			return null;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool compareHeader(byte[] a, byte[] b, int length)
		{
			return false;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsDownloadFile(string path)
		{
			return false;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadResource(Resource res)
		{
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UnloadAsset(global::UnityEngine.Object obj)
		{
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetCrc(string path)
		{
			return 0U;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainConvertPath(string path)
		{
			return false;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainLangPath(string path)
		{
			return false;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainPlatformPath(string path)
		{
			return false;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainResourceTypePath(string path)
		{
			return false;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainCardIllustTypePath(string path)
		{
			return false;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x0000216A File Offset: 0x0000036A
		public static string RemoveAutoConvertPath(string path)
		{
			return null;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetAllConvertAutoPathList(string path)
		{
			return null;
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ConvertAutoPath(string path)
		{
			return null;
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDataDivideByLang()
		{
			return false;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetPlatformDirectory()
		{
			return null;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetResourceTypeDirectory()
		{
			return null;
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetPlatformTypeDirectory()
		{
			return null;
		}

		// Token: 0x0400316D RID: 12653
		public const string kUniversalLangPath = "Universal";

		// Token: 0x0400316E RID: 12654
		private const string kConvertLangPath = "#";

		// Token: 0x0400316F RID: 12655
		private const string kConvertPlatformPath = "<_PLATFORM_>";

		// Token: 0x04003170 RID: 12656
		public const string kCommonResourceTypePath = "SD";

		// Token: 0x04003171 RID: 12657
		private const string kConvertResourceTypePath = "<_RESOURCE_TYPE_>";

		// Token: 0x04003172 RID: 12658
		private const string kConvertCardIllustType = "<_CARD_ILLUST_>";

		// Token: 0x04003173 RID: 12659
		private static readonly byte[] CompressedFileHeader;

		// Token: 0x04003174 RID: 12660
		public static readonly byte[] AssetBundleHeader;

		// Token: 0x04003175 RID: 12661
		private static readonly byte[] PNGFileHeader;
	}
}

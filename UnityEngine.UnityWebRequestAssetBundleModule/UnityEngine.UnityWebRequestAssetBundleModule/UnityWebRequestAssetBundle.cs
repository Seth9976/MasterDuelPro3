using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	public static class UnityWebRequestAssetBundle
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return UnityWebRequestAssetBundle.GetAssetBundle(uri, 0U);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		public static UnityWebRequest GetAssetBundle(Uri uri)
		{
			return UnityWebRequestAssetBundle.GetAssetBundle(uri, 0U);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002088 File Offset: 0x00000288
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, crc), null);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		public static UnityWebRequest GetAssetBundle(Uri uri, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, crc), null);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public static UnityWebRequest GetAssetBundle(Uri uri, CachedAssetBundle cachedAssetBundle, uint crc = 0U)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri.AbsoluteUri, cachedAssetBundle, crc), null);
		}
	}
}

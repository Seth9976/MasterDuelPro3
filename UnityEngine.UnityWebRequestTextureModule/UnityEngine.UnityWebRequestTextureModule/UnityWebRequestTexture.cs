using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000006 RID: 6
	public static class UnityWebRequestTexture
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021A8 File Offset: 0x000003A8
		public static UnityWebRequest GetTexture(string uri)
		{
			return UnityWebRequestTexture.GetTexture(uri, false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C4 File Offset: 0x000003C4
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(!nonReadable), null);
		}
	}
}

using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	public static class UnityWebRequestMultimedia
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}
	}
}

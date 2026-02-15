using System;

namespace YgomGame
{
	// Token: 0x020007C5 RID: 1989
	public static class DeepLinkInfo
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetUrl(string url)
		{
			return false;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetUrl()
		{
			return null;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDestination()
		{
			return null;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Clear()
		{
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Abort()
		{
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace YgomSystem.Advertisement
{
	// Token: 0x02000799 RID: 1945
	public static class AdSDKManager
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOptout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnableDefine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool IsInitialize
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x0000216A File Offset: 0x0000036A
		public static string AdId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Idfa
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Idfv
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x0000216A File Offset: 0x0000036A
		public static string gpsAdId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(Action callback = null)
		{
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateID()
		{
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateAdjustData()
		{
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OptoutNotificator(object o)
		{
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAdRecommendEnable()
		{
			return false;
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAdRecommend()
		{
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SendTrackCreateUserId()
		{
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SendTrackLaunch()
		{
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SendTrackTutorialComp()
		{
		}
	}
}

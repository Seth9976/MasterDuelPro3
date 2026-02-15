using System;

namespace YgomSystem.Utility
{
	// Token: 0x02000516 RID: 1302
	public abstract class DeviceInfo
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isXboxView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isTargetPlatformPlayerView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.Platform viewPlatform
		{
			get
			{
				return DeviceInfo.Platform.Unknown;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.PlatformType viewPlatformType
		{
			get
			{
				return DeviceInfo.PlatformType.Unknown;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.ResourceType resourceType
		{
			get
			{
				return DeviceInfo.ResourceType.Unknown;
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x0000216A File Offset: 0x0000036A
		private static DeviceInfo GetInstance()
		{
			return null;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0000216A File Offset: 0x0000036A
		private static DeviceInfo CreateInstance()
		{
			return null;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void initialize()
		{
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAppVersion()
		{
			return null;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string getAppVersion()
		{
			return null;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLanguage()
		{
			return null;
		}

		// Token: 0x060029D1 RID: 10705
		public abstract string getLanguage();

		// Token: 0x060029D2 RID: 10706 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRegion()
		{
			return null;
		}

		// Token: 0x060029D3 RID: 10707
		public abstract string getRegion();

		// Token: 0x060029D4 RID: 10708 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOSVersion()
		{
			return null;
		}

		// Token: 0x060029D5 RID: 10709
		public abstract string getOSVersion();

		// Token: 0x060029D6 RID: 10710 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetModelName()
		{
			return null;
		}

		// Token: 0x060029D7 RID: 10711
		public abstract string getModelName();

		// Token: 0x060029D8 RID: 10712 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTimeZone()
		{
			return null;
		}

		// Token: 0x060029D9 RID: 10713
		public abstract string getTimeZone();

		// Token: 0x060029DA RID: 10714 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPlatform()
		{
			return null;
		}

		// Token: 0x060029DB RID: 10715
		public abstract string getPlatform();

		// Token: 0x060029DC RID: 10716 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.Platform GetViewPlatform()
		{
			return DeviceInfo.Platform.Unknown;
		}

		// Token: 0x060029DD RID: 10717
		public abstract DeviceInfo.Platform getViewPlatform();

		// Token: 0x060029DE RID: 10718 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.PlatformType GetViewPlatformType()
		{
			return DeviceInfo.PlatformType.Unknown;
		}

		// Token: 0x060029DF RID: 10719
		public abstract DeviceInfo.PlatformType getViewPlatformType();

		// Token: 0x060029E0 RID: 10720 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.ResourceType GetResourceType()
		{
			return DeviceInfo.ResourceType.Unknown;
		}

		// Token: 0x060029E1 RID: 10721
		public abstract DeviceInfo.ResourceType getResourceType();

		// Token: 0x060029E2 RID: 10722 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStartupUrl()
		{
			return null;
		}

		// Token: 0x060029E3 RID: 10723
		public abstract string getStartupUrl();

		// Token: 0x060029E4 RID: 10724 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearStartupUrl()
		{
		}

		// Token: 0x060029E5 RID: 10725
		public abstract void clearStartupUrl();

		// Token: 0x060029E6 RID: 10726 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetIDFA()
		{
			return null;
		}

		// Token: 0x060029E7 RID: 10727
		public abstract string getIDFA();

		// Token: 0x060029E8 RID: 10728 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDeviceHash()
		{
			return null;
		}

		// Token: 0x060029E9 RID: 10729
		public abstract string getDeviceHash();

		// Token: 0x060029EA RID: 10730 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDateFormat()
		{
			return null;
		}

		// Token: 0x060029EB RID: 10731
		public abstract string getDateFormat();

		// Token: 0x060029EC RID: 10732 RVA: 0x000029CC File Offset: 0x00000BCC
		[Obsolete]
		public static int GetSafeAreaTopMargin()
		{
			return 0;
		}

		// Token: 0x060029ED RID: 10733
		public abstract int getSafeAreaTopMargin();

		// Token: 0x060029EE RID: 10734 RVA: 0x000029CC File Offset: 0x00000BCC
		[Obsolete]
		public static int GetSafeAreaBottomMargin()
		{
			return 0;
		}

		// Token: 0x060029EF RID: 10735
		public abstract int getSafeAreaBottomMargin();

		// Token: 0x060029F0 RID: 10736 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOverAspect()
		{
			return false;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPlatformUserName()
		{
			return null;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string getPlatformUserName()
		{
			return null;
		}

		// Token: 0x04002957 RID: 10583
		private static string version;

		// Token: 0x04002958 RID: 10584
		private static string model;

		// Token: 0x04002959 RID: 10585
		private static string platform;

		// Token: 0x0400295A RID: 10586
		private static string dateFormat;

		// Token: 0x0400295B RID: 10587
		private static DeviceInfo s_Instance;

		// Token: 0x0400295C RID: 10588
		private static int topMargin;

		// Token: 0x02000517 RID: 1303
		public enum Platform
		{
			// Token: 0x0400295E RID: 10590
			Unknown,
			// Token: 0x0400295F RID: 10591
			PS4,
			// Token: 0x04002960 RID: 10592
			PS5,
			// Token: 0x04002961 RID: 10593
			XboxOne,
			// Token: 0x04002962 RID: 10594
			XboxSeriesX,
			// Token: 0x04002963 RID: 10595
			Switch,
			// Token: 0x04002964 RID: 10596
			Android,
			// Token: 0x04002965 RID: 10597
			iOS,
			// Token: 0x04002966 RID: 10598
			PC,
			// Token: 0x04002967 RID: 10599
			Stadia
		}

		// Token: 0x02000518 RID: 1304
		public enum PlatformType
		{
			// Token: 0x04002969 RID: 10601
			Unknown,
			// Token: 0x0400296A RID: 10602
			Console,
			// Token: 0x0400296B RID: 10603
			Mobile,
			// Token: 0x0400296C RID: 10604
			PC
		}

		// Token: 0x02000519 RID: 1305
		public enum ResourceType
		{
			// Token: 0x0400296E RID: 10606
			Unknown,
			// Token: 0x0400296F RID: 10607
			HighEnd_HD,
			// Token: 0x04002970 RID: 10608
			HighEnd,
			// Token: 0x04002971 RID: 10609
			LowEnd
		}
	}
}

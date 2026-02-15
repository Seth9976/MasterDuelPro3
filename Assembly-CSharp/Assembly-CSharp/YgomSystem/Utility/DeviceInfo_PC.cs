using System;

namespace YgomSystem.Utility
{
	// Token: 0x0200051A RID: 1306
	public class DeviceInfo_PC : DeviceInfo
	{
		// Token: 0x060029F4 RID: 10740 RVA: 0x0000216D File Offset: 0x0000036D
		public override void initialize()
		{
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getLanguage()
		{
			return null;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getRegion()
		{
			return null;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getOSVersion()
		{
			return null;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getModelName()
		{
			return null;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getTimeZone()
		{
			return null;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getPlatform()
		{
			return null;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000029CC File Offset: 0x00000BCC
		public override DeviceInfo.Platform getViewPlatform()
		{
			return DeviceInfo.Platform.Unknown;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000029CC File Offset: 0x00000BCC
		public override DeviceInfo.PlatformType getViewPlatformType()
		{
			return DeviceInfo.PlatformType.Unknown;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000029CC File Offset: 0x00000BCC
		public override DeviceInfo.ResourceType getResourceType()
		{
			return DeviceInfo.ResourceType.Unknown;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getStartupUrl()
		{
			return null;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0000216D File Offset: 0x0000036D
		public override void clearStartupUrl()
		{
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getIDFA()
		{
			return null;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getDeviceHash()
		{
			return null;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getDateFormat()
		{
			return null;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int getSafeAreaTopMargin()
		{
			return 0;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int getSafeAreaBottomMargin()
		{
			return 0;
		}
	}
}

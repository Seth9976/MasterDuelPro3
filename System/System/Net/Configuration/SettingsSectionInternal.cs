using System;
using System.Configuration;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	// Token: 0x02000483 RID: 1155
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0007C7F0 File Offset: 0x0007A9F0
		internal static SettingsSectionInternal Section
		{
			get
			{
				return SettingsSectionInternal.instance;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		internal bool Ipv6Enabled
		{
			get
			{
				try
				{
					SettingsSection settingsSection = (SettingsSection)ConfigurationManager.GetSection("system.net/settings");
					if (settingsSection != null)
					{
						return settingsSection.Ipv6.Enabled;
					}
				}
				catch
				{
				}
				return true;
			}
		}

		// Token: 0x0400138C RID: 5004
		private static readonly SettingsSectionInternal instance = new SettingsSectionInternal();

		// Token: 0x0400138D RID: 5005
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance;

		// Token: 0x0400138E RID: 5006
		internal readonly bool HttpListenerUnescapeRequestUrl = true;

		// Token: 0x0400138F RID: 5007
		internal readonly IPProtectionLevel IPProtectionLevel = IPProtectionLevel.Unspecified;
	}
}

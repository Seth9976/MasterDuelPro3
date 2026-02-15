using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000142 RID: 322
	internal static class AppSettings
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004DEBD File Offset: 0x0004C0BD
		internal static bool? UseLegacySerializerGeneration
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings.useLegacySerializerGeneration;
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004DECC File Offset: 0x0004C0CC
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings.settingsInitalized)
			{
				object obj = AppSettings.appSettingsLock;
				lock (obj)
				{
					if (!AppSettings.settingsInitalized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							bool flag2;
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["System:Xml:Serialization:UseLegacySerializerGeneration"], out flag2))
							{
								AppSettings.useLegacySerializerGeneration = null;
							}
							else
							{
								AppSettings.useLegacySerializerGeneration = new bool?(flag2);
							}
							AppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x040007B2 RID: 1970
		private const string UseLegacySerializerGenerationAppSettingsString = "System:Xml:Serialization:UseLegacySerializerGeneration";

		// Token: 0x040007B3 RID: 1971
		private static bool? useLegacySerializerGeneration;

		// Token: 0x040007B4 RID: 1972
		private static volatile bool settingsInitalized = false;

		// Token: 0x040007B5 RID: 1973
		private static object appSettingsLock = new object();
	}
}

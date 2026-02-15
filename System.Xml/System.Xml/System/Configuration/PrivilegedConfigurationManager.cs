using System;

namespace System.Configuration
{
	// Token: 0x02000006 RID: 6
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}

using System;

namespace System.Configuration
{
	// Token: 0x0200011C RID: 284
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x0001C4DD File Offset: 0x0001A6DD
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}

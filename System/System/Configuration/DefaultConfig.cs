using System;

namespace System.Configuration
{
	// Token: 0x0200011F RID: 287
	internal class DefaultConfig : IConfigurationSystem
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x000026E5 File Offset: 0x000008E5
		private DefaultConfig()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001C6BD File Offset: 0x0001A8BD
		public static DefaultConfig GetInstance()
		{
			return DefaultConfig.instance;
		}

		// Token: 0x040004B7 RID: 1207
		private static readonly DefaultConfig instance = new DefaultConfig();
	}
}

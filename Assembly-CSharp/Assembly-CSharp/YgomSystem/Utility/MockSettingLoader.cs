using System;
using System.Collections.Generic;

namespace YgomSystem.Utility
{
	// Token: 0x02000531 RID: 1329
	public class MockSettingLoader
	{
		// Token: 0x06002A98 RID: 10904 RVA: 0x0000216A File Offset: 0x0000036A
		public static MockSettingLoader load(SelectEnvSetting envSetting = null)
		{
			return null;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00002739 File Offset: 0x00000939
		private MockSettingLoader(MockSetting setting)
		{
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000F1C48 File Offset: 0x000EFE48
		public ValueTuple<bool, string> getBtnAttr(RuntimeEnvironment.ServerType serverType)
		{
			return default(ValueTuple<bool, string>);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000F1C60 File Offset: 0x000EFE60
		public ValueTuple<string, string> getUrl(RuntimeEnvironment.ServerType serverType)
		{
			return default(ValueTuple<string, string>);
		}

		// Token: 0x040029BE RID: 10686
		private MockSetting setting;

		// Token: 0x040029BF RID: 10687
		private Dictionary<string, Mock> mocks;
	}
}

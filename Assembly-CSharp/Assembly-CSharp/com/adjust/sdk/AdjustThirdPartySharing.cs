using System;
using System.Collections.Generic;

namespace com.adjust.sdk
{
	// Token: 0x0200046B RID: 1131
	public class AdjustThirdPartySharing
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustThirdPartySharing(bool? isEnabled)
		{
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0000216D File Offset: 0x0000036D
		public void addGranularOption(string partnerName, string key, string value)
		{
		}

		// Token: 0x0400270B RID: 9995
		internal bool? isEnabled;

		// Token: 0x0400270C RID: 9996
		internal Dictionary<string, List<string>> granularOptions;
	}
}

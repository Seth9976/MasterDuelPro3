using System;
using Newtonsoft.Json;

namespace MDPro3.Net
{
	// Token: 0x0200132F RID: 4911
	[Serializable]
	public class MyCardWatchInfo
	{
		// Token: 0x0400CD22 RID: 52514
		[JsonProperty("event")]
		public string eventType;

		// Token: 0x0400CD23 RID: 52515
		public object data;
	}
}

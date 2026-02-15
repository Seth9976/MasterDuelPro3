using System;
using Newtonsoft.Json;

namespace MDPro3.Net
{
	// Token: 0x0200132B RID: 4907
	[Serializable]
	public class MyCardNews
	{
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06008F27 RID: 36647 RVA: 0x0013455C File Offset: 0x0013275C
		// (set) Token: 0x06008F28 RID: 36648 RVA: 0x00134564 File Offset: 0x00132764
		[JsonProperty("en-US")]
		public News[] EnglishUS { get; set; }

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06008F29 RID: 36649 RVA: 0x0013456D File Offset: 0x0013276D
		// (set) Token: 0x06008F2A RID: 36650 RVA: 0x00134575 File Offset: 0x00132775
		[JsonProperty("zh-CN")]
		public News[] ChineseCN { get; set; }
	}
}

using System;
using Newtonsoft.Json;

namespace MDPro3.Net
{
	// Token: 0x0200132A RID: 4906
	[Serializable]
	public class MyCardWindbots
	{
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06008F22 RID: 36642 RVA: 0x0013453A File Offset: 0x0013273A
		// (set) Token: 0x06008F23 RID: 36643 RVA: 0x00134542 File Offset: 0x00132742
		[JsonProperty("en-US")]
		public string[] EnglishUS { get; set; }

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06008F24 RID: 36644 RVA: 0x0013454B File Offset: 0x0013274B
		// (set) Token: 0x06008F25 RID: 36645 RVA: 0x00134553 File Offset: 0x00132753
		[JsonProperty("zh-CN")]
		public string[] ChineseCN { get; set; }
	}
}

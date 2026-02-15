using System;
using Newtonsoft.Json;

namespace MDPro3
{
	// Token: 0x02001291 RID: 4753
	[Serializable]
	public class NPC_Data
	{
		// Token: 0x0400C757 RID: 51031
		public string id;

		// Token: 0x0400C758 RID: 51032
		public string note;

		// Token: 0x0400C759 RID: 51033
		[JsonProperty("ja-JP")]
		public string japanese;

		// Token: 0x0400C75A RID: 51034
		[JsonProperty("en-US")]
		public string english;

		// Token: 0x0400C75B RID: 51035
		[JsonProperty("fr-FR")]
		public string french;

		// Token: 0x0400C75C RID: 51036
		[JsonProperty("it-IT")]
		public string italian;

		// Token: 0x0400C75D RID: 51037
		[JsonProperty("de-DE")]
		public string german;

		// Token: 0x0400C75E RID: 51038
		[JsonProperty("es-ES")]
		public string spanish;

		// Token: 0x0400C75F RID: 51039
		[JsonProperty("pt-BR")]
		public string portuguese;

		// Token: 0x0400C760 RID: 51040
		[JsonProperty("ru-RU")]
		public string russian;

		// Token: 0x0400C761 RID: 51041
		[JsonProperty("ko-KR")]
		public string korean;

		// Token: 0x0400C762 RID: 51042
		[JsonProperty("zh-TW")]
		public string tChinese;

		// Token: 0x0400C763 RID: 51043
		[JsonProperty("zh-CN")]
		public string sChinese;

		// Token: 0x0400C764 RID: 51044
		public string date;
	}
}

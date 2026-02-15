using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D4 RID: 212
	internal static class JsonTokenUtils
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00021C5E File Offset: 0x0001FE5E
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00021C6A File Offset: 0x0001FE6A
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00021C75 File Offset: 0x0001FE75
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}

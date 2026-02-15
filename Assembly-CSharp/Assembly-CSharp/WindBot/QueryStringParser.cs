using System;
using System.Collections.Specialized;

namespace WindBot
{
	// Token: 0x020001EB RID: 491
	public static class QueryStringParser
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x00028660 File Offset: 0x00026860
		public static NameValueCollection ParseQueryString(string query)
		{
			NameValueCollection result = new NameValueCollection();
			if (!string.IsNullOrEmpty(query))
			{
				foreach (string pair in query.Split('&', StringSplitOptions.None))
				{
					if (pair.Contains("="))
					{
						string[] parts = pair.Split('=', StringSplitOptions.None);
						string key = Uri.UnescapeDataString(parts[0]);
						string value = ((parts.Length > 1) ? Uri.UnescapeDataString(parts[1]) : string.Empty);
						result[key] = value;
					}
				}
			}
			return result;
		}
	}
}

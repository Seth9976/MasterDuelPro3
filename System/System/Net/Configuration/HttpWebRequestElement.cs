using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum length for response headers. This class cannot be inherited.</summary>
	// Token: 0x0200048E RID: 1166
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x06001C9C RID: 7324 RVA: 0x0007CCB0 File Offset: 0x0007AEB0
		static HttpWebRequestElement()
		{
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumErrorResponseLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumResponseHeadersLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumUnauthorizedUploadLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.useUnsafeHeaderParsingProp);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0007CD81 File Offset: 0x0007AF81
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpWebRequestElement.properties;
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0007CD88 File Offset: 0x0007AF88
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x040013A2 RID: 5026
		private static ConfigurationProperty maximumErrorResponseLengthProp = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64);

		// Token: 0x040013A3 RID: 5027
		private static ConfigurationProperty maximumResponseHeadersLengthProp = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64);

		// Token: 0x040013A4 RID: 5028
		private static ConfigurationProperty maximumUnauthorizedUploadLengthProp = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1);

		// Token: 0x040013A5 RID: 5029
		private static ConfigurationProperty useUnsafeHeaderParsingProp = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false);

		// Token: 0x040013A6 RID: 5030
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

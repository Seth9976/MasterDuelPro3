using System;
using System.Globalization;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x02000200 RID: 512
	internal static class XmlConfigurationString
	{
		// Token: 0x04000AFD RID: 2813
		internal static string XmlReaderSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xmlReader");

		// Token: 0x04000AFE RID: 2814
		internal static string XsltSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xslt");
	}
}

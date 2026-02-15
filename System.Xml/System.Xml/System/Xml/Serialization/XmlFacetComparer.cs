using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000183 RID: 387
	internal class XmlFacetComparer : IComparer
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x0005737C File Offset: 0x0005557C
		public int Compare(object o1, object o2)
		{
			XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)o1;
			XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)o2;
			return string.Compare(xmlSchemaFacet.GetType().Name + ":" + xmlSchemaFacet.Value, xmlSchemaFacet2.GetType().Name + ":" + xmlSchemaFacet2.Value, StringComparison.Ordinal);
		}
	}
}

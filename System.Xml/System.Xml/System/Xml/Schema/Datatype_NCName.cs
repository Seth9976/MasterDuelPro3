using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025C RID: 604
	internal class Datatype_NCName : Datatype_Name
	{
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0009ED1C File Offset: 0x0009CF1C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0009ED20 File Offset: 0x0009CF20
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					nameTable.Add(s);
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}

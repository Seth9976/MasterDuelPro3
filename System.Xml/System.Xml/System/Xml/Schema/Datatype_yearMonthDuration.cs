using System;

namespace System.Xml.Schema
{
	// Token: 0x02000242 RID: 578
	internal class Datatype_yearMonthDuration : Datatype_duration
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x0009E77C File Offset: 0x0009C97C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDuration xsdDuration;
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.YearMonthDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.YearMonthDuration, out timeSpan);
					if (ex == null)
					{
						ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
						if (ex == null)
						{
							typedValue = timeSpan;
							return null;
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0009E7F0 File Offset: 0x0009C9F0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}
	}
}

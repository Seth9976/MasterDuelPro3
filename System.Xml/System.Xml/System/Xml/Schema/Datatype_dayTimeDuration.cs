using System;

namespace System.Xml.Schema
{
	// Token: 0x02000243 RID: 579
	internal class Datatype_dayTimeDuration : Datatype_duration
	{
		// Token: 0x06001BB7 RID: 7095 RVA: 0x0009E7FC File Offset: 0x0009C9FC
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
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.DayTimeDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.DayTimeDuration, out timeSpan);
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0009E870 File Offset: 0x0009CA70
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DayTimeDuration;
			}
		}
	}
}

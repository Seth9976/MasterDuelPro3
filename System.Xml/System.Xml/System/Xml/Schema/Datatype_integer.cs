using System;

namespace System.Xml.Schema
{
	// Token: 0x02000261 RID: 609
	internal class Datatype_integer : Datatype_decimal
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0009E42B File Offset: 0x0009C62B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0009EEC0 File Offset: 0x0009D0C0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = this.FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToInteger(s, out num);
				if (ex == null)
				{
					ex = this.FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}
	}
}

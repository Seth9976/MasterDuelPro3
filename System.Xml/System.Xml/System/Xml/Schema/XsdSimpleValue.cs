using System;

namespace System.Xml.Schema
{
	// Token: 0x02000231 RID: 561
	internal class XsdSimpleValue
	{
		// Token: 0x06001B11 RID: 6929 RVA: 0x0009C68C File Offset: 0x0009A88C
		public XsdSimpleValue(XmlSchemaSimpleType st, object value)
		{
			this.xmlType = st;
			this.typedValue = value;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0009C6A2 File Offset: 0x0009A8A2
		public XmlSchemaSimpleType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0009C6AA File Offset: 0x0009A8AA
		public object TypedValue
		{
			get
			{
				return this.typedValue;
			}
		}

		// Token: 0x04000B9A RID: 2970
		private XmlSchemaSimpleType xmlType;

		// Token: 0x04000B9B RID: 2971
		private object typedValue;
	}
}

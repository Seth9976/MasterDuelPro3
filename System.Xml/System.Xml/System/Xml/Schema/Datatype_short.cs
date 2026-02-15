using System;

namespace System.Xml.Schema
{
	// Token: 0x02000266 RID: 614
	internal class Datatype_short : Datatype_int
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0009F10C File Offset: 0x0009D30C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_short.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0009F113 File Offset: 0x0009D313
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0009F118 File Offset: 0x0009D318
		internal override int Compare(object value1, object value2)
		{
			return ((short)value1).CompareTo(value2);
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0009F134 File Offset: 0x0009D334
		public override Type ValueType
		{
			get
			{
				return Datatype_short.atomicValueType;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0009F13B File Offset: 0x0009D33B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_short.listValueType;
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0009F144 File Offset: 0x0009D344
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_short.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				short num;
				ex = XmlConvert.TryToInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_short.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C40 RID: 3136
		private static readonly Type atomicValueType = typeof(short);

		// Token: 0x04000C41 RID: 3137
		private static readonly Type listValueType = typeof(short[]);

		// Token: 0x04000C42 RID: 3138
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-32768m, 32767m);
	}
}

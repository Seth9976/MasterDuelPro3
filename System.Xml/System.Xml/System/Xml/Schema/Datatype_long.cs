using System;

namespace System.Xml.Schema
{
	// Token: 0x02000264 RID: 612
	internal class Datatype_long : Datatype_integer
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0009EF70 File Offset: 0x0009D170
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_long.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0009EF77 File Offset: 0x0009D177
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0009EF7C File Offset: 0x0009D17C
		internal override int Compare(object value1, object value2)
		{
			return ((long)value1).CompareTo(value2);
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0009EF98 File Offset: 0x0009D198
		public override Type ValueType
		{
			get
			{
				return Datatype_long.atomicValueType;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0009EF9F File Offset: 0x0009D19F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_long.listValueType;
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0009EFA8 File Offset: 0x0009D1A8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_long.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				long num;
				ex = XmlConvert.TryToInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_long.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C3A RID: 3130
		private static readonly Type atomicValueType = typeof(long);

		// Token: 0x04000C3B RID: 3131
		private static readonly Type listValueType = typeof(long[]);

		// Token: 0x04000C3C RID: 3132
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-9223372036854775808m, 9223372036854775807m);
	}
}

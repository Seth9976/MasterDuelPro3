using System;

namespace System.Xml.Schema
{
	// Token: 0x02000269 RID: 617
	internal class Datatype_unsignedLong : Datatype_nonNegativeInteger
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0009F2BC File Offset: 0x0009D4BC
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedLong.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0009F2C3 File Offset: 0x0009D4C3
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0009F2C8 File Offset: 0x0009D4C8
		internal override int Compare(object value1, object value2)
		{
			return ((ulong)value1).CompareTo(value2);
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0009F2E4 File Offset: 0x0009D4E4
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedLong.atomicValueType;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0009F2EB File Offset: 0x0009D4EB
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedLong.listValueType;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0009F2F4 File Offset: 0x0009D4F4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ulong num;
				ex = XmlConvert.TryToUInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C47 RID: 3143
		private static readonly Type atomicValueType = typeof(ulong);

		// Token: 0x04000C48 RID: 3144
		private static readonly Type listValueType = typeof(ulong[]);

		// Token: 0x04000C49 RID: 3145
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 18446744073709551615m);
	}
}

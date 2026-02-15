using System;

namespace System.Xml.Schema
{
	// Token: 0x02000265 RID: 613
	internal class Datatype_int : Datatype_long
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0009F045 File Offset: 0x0009D245
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_int.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0009F04C File Offset: 0x0009D24C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0009F050 File Offset: 0x0009D250
		internal override int Compare(object value1, object value2)
		{
			return ((int)value1).CompareTo(value2);
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0009F06C File Offset: 0x0009D26C
		public override Type ValueType
		{
			get
			{
				return Datatype_int.atomicValueType;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0009F073 File Offset: 0x0009D273
		internal override Type ListValueType
		{
			get
			{
				return Datatype_int.listValueType;
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0009F07C File Offset: 0x0009D27C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_int.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				int num;
				ex = XmlConvert.TryToInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_int.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C3D RID: 3133
		private static readonly Type atomicValueType = typeof(int);

		// Token: 0x04000C3E RID: 3134
		private static readonly Type listValueType = typeof(int[]);

		// Token: 0x04000C3F RID: 3135
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-2147483648m, 2147483647m);
	}
}

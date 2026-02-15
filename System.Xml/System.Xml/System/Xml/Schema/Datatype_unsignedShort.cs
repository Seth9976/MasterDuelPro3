using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026B RID: 619
	internal class Datatype_unsignedShort : Datatype_unsignedInt
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0009F440 File Offset: 0x0009D640
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedShort.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0009F447 File Offset: 0x0009D647
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0009F44C File Offset: 0x0009D64C
		internal override int Compare(object value1, object value2)
		{
			return ((ushort)value1).CompareTo(value2);
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0009F468 File Offset: 0x0009D668
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedShort.atomicValueType;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0009F46F File Offset: 0x0009D66F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedShort.listValueType;
			}
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0009F478 File Offset: 0x0009D678
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ushort num;
				ex = XmlConvert.TryToUInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckValueFacets((int)num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C4D RID: 3149
		private static readonly Type atomicValueType = typeof(ushort);

		// Token: 0x04000C4E RID: 3150
		private static readonly Type listValueType = typeof(ushort[]);

		// Token: 0x04000C4F RID: 3151
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 65535m);
	}
}

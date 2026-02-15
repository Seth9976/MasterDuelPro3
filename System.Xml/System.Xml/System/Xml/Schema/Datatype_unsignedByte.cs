using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026C RID: 620
	internal class Datatype_unsignedByte : Datatype_unsignedShort
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0009F503 File Offset: 0x0009D703
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedByte.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0009F50A File Offset: 0x0009D70A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0009F510 File Offset: 0x0009D710
		internal override int Compare(object value1, object value2)
		{
			return ((byte)value1).CompareTo(value2);
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0009F52C File Offset: 0x0009D72C
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedByte.atomicValueType;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0009F533 File Offset: 0x0009D733
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedByte.listValueType;
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0009F53C File Offset: 0x0009D73C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte b;
				ex = XmlConvert.TryToByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C50 RID: 3152
		private static readonly Type atomicValueType = typeof(byte);

		// Token: 0x04000C51 RID: 3153
		private static readonly Type listValueType = typeof(byte[]);

		// Token: 0x04000C52 RID: 3154
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 255m);
	}
}

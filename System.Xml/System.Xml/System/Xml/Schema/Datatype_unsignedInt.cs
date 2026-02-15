using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026A RID: 618
	internal class Datatype_unsignedInt : Datatype_unsignedLong
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0009F381 File Offset: 0x0009D581
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedInt.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0009F388 File Offset: 0x0009D588
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0009F38C File Offset: 0x0009D58C
		internal override int Compare(object value1, object value2)
		{
			return ((uint)value1).CompareTo(value2);
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0009F3A8 File Offset: 0x0009D5A8
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedInt.atomicValueType;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0009F3AF File Offset: 0x0009D5AF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedInt.listValueType;
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0009F3B8 File Offset: 0x0009D5B8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				uint num;
				ex = XmlConvert.TryToUInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckValueFacets((long)((ulong)num), this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C4A RID: 3146
		private static readonly Type atomicValueType = typeof(uint);

		// Token: 0x04000C4B RID: 3147
		private static readonly Type listValueType = typeof(uint[]);

		// Token: 0x04000C4C RID: 3148
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 4294967295m);
	}
}

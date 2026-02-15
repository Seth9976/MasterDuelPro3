using System;

namespace System.Xml.Schema
{
	// Token: 0x02000267 RID: 615
	internal class Datatype_byte : Datatype_short
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0009F1D4 File Offset: 0x0009D3D4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_byte.numeric10FacetsChecker;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0009F1DB File Offset: 0x0009D3DB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0009F1E0 File Offset: 0x0009D3E0
		internal override int Compare(object value1, object value2)
		{
			return ((sbyte)value1).CompareTo(value2);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0009F1FC File Offset: 0x0009D3FC
		public override Type ValueType
		{
			get
			{
				return Datatype_byte.atomicValueType;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0009F203 File Offset: 0x0009D403
		internal override Type ListValueType
		{
			get
			{
				return Datatype_byte.listValueType;
			}
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0009F20C File Offset: 0x0009D40C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_byte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				sbyte b;
				ex = XmlConvert.TryToSByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_byte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C43 RID: 3139
		private static readonly Type atomicValueType = typeof(sbyte);

		// Token: 0x04000C44 RID: 3140
		private static readonly Type listValueType = typeof(sbyte[]);

		// Token: 0x04000C45 RID: 3141
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-128m, 127m);
	}
}

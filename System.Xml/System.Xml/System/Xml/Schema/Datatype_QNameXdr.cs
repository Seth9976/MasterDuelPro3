using System;

namespace System.Xml.Schema
{
	// Token: 0x02000270 RID: 624
	internal class Datatype_QNameXdr : Datatype_anySimpleType
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0009F6C8 File Offset: 0x0009D8C8
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (s == null || s.Length == 0)
			{
				throw new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			if (nsmgr == null)
			{
				throw new ArgumentNullException("nsmgr");
			}
			object obj;
			try
			{
				string text;
				obj = XmlQualifiedName.Parse(s.Trim(), nsmgr, out text);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex2);
			}
			return obj;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0009F74C File Offset: 0x0009D94C
		public override Type ValueType
		{
			get
			{
				return Datatype_QNameXdr.atomicValueType;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0009F753 File Offset: 0x0009D953
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QNameXdr.listValueType;
			}
		}

		// Token: 0x04000C54 RID: 3156
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000C55 RID: 3157
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}

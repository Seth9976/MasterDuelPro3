using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026F RID: 623
	internal class Datatype_floatXdr : Datatype_float
	{
		// Token: 0x06001C88 RID: 7304 RVA: 0x0009F65C File Offset: 0x0009D85C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			float num;
			try
			{
				num = XmlConvert.ToSingle(s);
			}
			catch (Exception ex)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex);
			}
			if (float.IsInfinity(num) || float.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}
	}
}

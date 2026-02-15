using System;

namespace System.Xml.Schema
{
	// Token: 0x0200026E RID: 622
	internal class Datatype_doubleXdr : Datatype_double
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x0009F5F0 File Offset: 0x0009D7F0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			double num;
			try
			{
				num = XmlConvert.ToDouble(s);
			}
			catch (Exception ex)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex);
			}
			if (double.IsInfinity(num) || double.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}
	}
}

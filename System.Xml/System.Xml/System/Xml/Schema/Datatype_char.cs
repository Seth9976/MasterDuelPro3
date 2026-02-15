using System;

namespace System.Xml.Schema
{
	// Token: 0x02000272 RID: 626
	internal class Datatype_char : Datatype_anySimpleType
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0009F782 File Offset: 0x0009D982
		public override Type ValueType
		{
			get
			{
				return Datatype_char.atomicValueType;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0009F789 File Offset: 0x0009D989
		internal override Type ListValueType
		{
			get
			{
				return Datatype_char.listValueType;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0009F790 File Offset: 0x0009D990
		internal override int Compare(object value1, object value2)
		{
			return ((char)value1).CompareTo(value2);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0009F7AC File Offset: 0x0009D9AC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			try
			{
				obj = XmlConvert.ToChar(s);
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

		// Token: 0x06001C97 RID: 7319 RVA: 0x0009F804 File Offset: 0x0009DA04
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			char c;
			Exception ex = XmlConvert.TryToChar(s, out c);
			if (ex == null)
			{
				typedValue = c;
				return null;
			}
			return ex;
		}

		// Token: 0x04000C56 RID: 3158
		private static readonly Type atomicValueType = typeof(char);

		// Token: 0x04000C57 RID: 3159
		private static readonly Type listValueType = typeof(char[]);
	}
}

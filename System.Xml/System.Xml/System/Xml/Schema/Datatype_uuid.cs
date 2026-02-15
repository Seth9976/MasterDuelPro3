using System;

namespace System.Xml.Schema
{
	// Token: 0x02000274 RID: 628
	internal class Datatype_uuid : Datatype_anySimpleType
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0009F90B File Offset: 0x0009DB0B
		public override Type ValueType
		{
			get
			{
				return Datatype_uuid.atomicValueType;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0009F912 File Offset: 0x0009DB12
		internal override Type ListValueType
		{
			get
			{
				return Datatype_uuid.listValueType;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0009F91C File Offset: 0x0009DB1C
		internal override int Compare(object value1, object value2)
		{
			if (!((Guid)value1).Equals(value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0009F944 File Offset: 0x0009DB44
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			try
			{
				obj = XmlConvert.ToGuid(s);
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

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0009F99C File Offset: 0x0009DB9C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Guid guid;
			Exception ex = XmlConvert.TryToGuid(s, out guid);
			if (ex == null)
			{
				typedValue = guid;
				return null;
			}
			return ex;
		}

		// Token: 0x04000C58 RID: 3160
		private static readonly Type atomicValueType = typeof(Guid);

		// Token: 0x04000C59 RID: 3161
		private static readonly Type listValueType = typeof(Guid[]);
	}
}

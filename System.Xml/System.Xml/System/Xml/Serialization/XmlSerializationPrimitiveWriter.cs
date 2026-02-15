using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200017F RID: 383
	internal class XmlSerializationPrimitiveWriter : XmlSerializationWriter
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x00056021 File Offset: 0x00054221
		internal void Write_string(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("string", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteral("string", "", (string)o);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00056059 File Offset: 0x00054259
		internal void Write_int(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("int", "");
				return;
			}
			base.WriteElementStringRaw("int", "", XmlConvert.ToString((int)o));
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00056090 File Offset: 0x00054290
		internal void Write_boolean(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("boolean", "");
				return;
			}
			base.WriteElementStringRaw("boolean", "", XmlConvert.ToString((bool)o));
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000560C7 File Offset: 0x000542C7
		internal void Write_short(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("short", "");
				return;
			}
			base.WriteElementStringRaw("short", "", XmlConvert.ToString((short)o));
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000560FE File Offset: 0x000542FE
		internal void Write_long(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("long", "");
				return;
			}
			base.WriteElementStringRaw("long", "", XmlConvert.ToString((long)o));
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00056135 File Offset: 0x00054335
		internal void Write_float(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("float", "");
				return;
			}
			base.WriteElementStringRaw("float", "", XmlConvert.ToString((float)o));
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0005616C File Offset: 0x0005436C
		internal void Write_double(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("double", "");
				return;
			}
			base.WriteElementStringRaw("double", "", XmlConvert.ToString((double)o));
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000561A3 File Offset: 0x000543A3
		internal void Write_decimal(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("decimal", "");
				return;
			}
			base.WriteElementStringRaw("decimal", "", XmlConvert.ToString((decimal)o));
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000561DA File Offset: 0x000543DA
		internal void Write_dateTime(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("dateTime", "");
				return;
			}
			base.WriteElementStringRaw("dateTime", "", XmlSerializationWriter.FromDateTime((DateTime)o));
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00056211 File Offset: 0x00054411
		internal void Write_unsignedByte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedByte", "");
				return;
			}
			base.WriteElementStringRaw("unsignedByte", "", XmlConvert.ToString((byte)o));
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00056248 File Offset: 0x00054448
		internal void Write_byte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("byte", "");
				return;
			}
			base.WriteElementStringRaw("byte", "", XmlConvert.ToString((sbyte)o));
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0005627F File Offset: 0x0005447F
		internal void Write_unsignedShort(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedShort", "");
				return;
			}
			base.WriteElementStringRaw("unsignedShort", "", XmlConvert.ToString((ushort)o));
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000562B6 File Offset: 0x000544B6
		internal void Write_unsignedInt(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedInt", "");
				return;
			}
			base.WriteElementStringRaw("unsignedInt", "", XmlConvert.ToString((uint)o));
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000562ED File Offset: 0x000544ED
		internal void Write_unsignedLong(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedLong", "");
				return;
			}
			base.WriteElementStringRaw("unsignedLong", "", XmlConvert.ToString((ulong)o));
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00056324 File Offset: 0x00054524
		internal void Write_base64Binary(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("base64Binary", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteralRaw("base64Binary", "", XmlSerializationWriter.FromByteArrayBase64((byte[])o));
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00056361 File Offset: 0x00054561
		internal void Write_guid(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("guid", "");
				return;
			}
			base.WriteElementStringRaw("guid", "", XmlConvert.ToString((Guid)o));
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00056398 File Offset: 0x00054598
		internal void Write_TimeSpan(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("TimeSpan", "");
				return;
			}
			TimeSpan timeSpan = (TimeSpan)o;
			base.WriteElementStringRaw("TimeSpan", "", XmlConvert.ToString(timeSpan));
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000563DC File Offset: 0x000545DC
		internal void Write_char(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("char", "");
				return;
			}
			base.WriteElementString("char", "", XmlSerializationWriter.FromChar((char)o));
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00056413 File Offset: 0x00054613
		internal void Write_QName(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("QName", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableQualifiedNameLiteral("QName", "", (XmlQualifiedName)o);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0000A558 File Offset: 0x00008758
		protected override void InitCallbacks()
		{
		}
	}
}

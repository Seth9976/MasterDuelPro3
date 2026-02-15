using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000180 RID: 384
	internal class XmlSerializationPrimitiveReader : XmlSerializationReader
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00056454 File Offset: 0x00054654
		internal object Read_string()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_string || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.Reader.ReadElementString();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000564CC File Offset: 0x000546CC
		internal object Read_int()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id3_int || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00056544 File Offset: 0x00054744
		internal object Read_boolean()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id4_boolean || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToBoolean(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000565BC File Offset: 0x000547BC
		internal object Read_short()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id5_short || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00056634 File Offset: 0x00054834
		internal object Read_long()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id6_long || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000566AC File Offset: 0x000548AC
		internal object Read_float()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id7_float || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToSingle(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00056724 File Offset: 0x00054924
		internal object Read_double()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id8_double || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToDouble(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0005679C File Offset: 0x0005499C
		internal object Read_decimal()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id9_decimal || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToDecimal(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00056814 File Offset: 0x00054A14
		internal object Read_dateTime()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id10_dateTime || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0005688C File Offset: 0x00054A8C
		internal object Read_unsignedByte()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id11_unsignedByte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00056904 File Offset: 0x00054B04
		internal object Read_byte()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id12_byte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToSByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0005697C File Offset: 0x00054B7C
		internal object Read_unsignedShort()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id13_unsignedShort || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000569F4 File Offset: 0x00054BF4
		internal object Read_unsignedInt()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id14_unsignedInt || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00056A6C File Offset: 0x00054C6C
		internal object Read_unsignedLong()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id15_unsignedLong || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToUInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00056AE4 File Offset: 0x00054CE4
		internal object Read_base64Binary()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id16_base64Binary || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.ToByteArrayBase64(false);
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00056B58 File Offset: 0x00054D58
		internal object Read_guid()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id17_guid || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlConvert.ToGuid(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00056BD0 File Offset: 0x00054DD0
		internal object Read_TimeSpan()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id19_TimeSpan || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.Reader.IsEmptyElement)
				{
					base.Reader.Skip();
					obj = default(TimeSpan);
				}
				else
				{
					obj = XmlConvert.ToTimeSpan(base.Reader.ReadElementString());
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00056C70 File Offset: 0x00054E70
		internal object Read_char()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id18_char || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = XmlSerializationReader.ToChar(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00056CE8 File Offset: 0x00054EE8
		internal object Read_QName()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_QName || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					obj = null;
				}
				else
				{
					obj = base.ReadElementQualifiedName();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return obj;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0000A558 File Offset: 0x00008758
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00056D5C File Offset: 0x00054F5C
		protected override void InitIDs()
		{
			this.id4_boolean = base.Reader.NameTable.Add("boolean");
			this.id14_unsignedInt = base.Reader.NameTable.Add("unsignedInt");
			this.id15_unsignedLong = base.Reader.NameTable.Add("unsignedLong");
			this.id7_float = base.Reader.NameTable.Add("float");
			this.id10_dateTime = base.Reader.NameTable.Add("dateTime");
			this.id6_long = base.Reader.NameTable.Add("long");
			this.id9_decimal = base.Reader.NameTable.Add("decimal");
			this.id8_double = base.Reader.NameTable.Add("double");
			this.id17_guid = base.Reader.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.id19_TimeSpan = base.Reader.NameTable.Add("TimeSpan");
			}
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id13_unsignedShort = base.Reader.NameTable.Add("unsignedShort");
			this.id18_char = base.Reader.NameTable.Add("char");
			this.id3_int = base.Reader.NameTable.Add("int");
			this.id12_byte = base.Reader.NameTable.Add("byte");
			this.id16_base64Binary = base.Reader.NameTable.Add("base64Binary");
			this.id11_unsignedByte = base.Reader.NameTable.Add("unsignedByte");
			this.id5_short = base.Reader.NameTable.Add("short");
			this.id1_string = base.Reader.NameTable.Add("string");
			this.id1_QName = base.Reader.NameTable.Add("QName");
		}

		// Token: 0x0400089B RID: 2203
		private string id4_boolean;

		// Token: 0x0400089C RID: 2204
		private string id14_unsignedInt;

		// Token: 0x0400089D RID: 2205
		private string id15_unsignedLong;

		// Token: 0x0400089E RID: 2206
		private string id7_float;

		// Token: 0x0400089F RID: 2207
		private string id10_dateTime;

		// Token: 0x040008A0 RID: 2208
		private string id6_long;

		// Token: 0x040008A1 RID: 2209
		private string id9_decimal;

		// Token: 0x040008A2 RID: 2210
		private string id8_double;

		// Token: 0x040008A3 RID: 2211
		private string id17_guid;

		// Token: 0x040008A4 RID: 2212
		private string id19_TimeSpan;

		// Token: 0x040008A5 RID: 2213
		private string id2_Item;

		// Token: 0x040008A6 RID: 2214
		private string id13_unsignedShort;

		// Token: 0x040008A7 RID: 2215
		private string id18_char;

		// Token: 0x040008A8 RID: 2216
		private string id3_int;

		// Token: 0x040008A9 RID: 2217
		private string id12_byte;

		// Token: 0x040008AA RID: 2218
		private string id16_base64Binary;

		// Token: 0x040008AB RID: 2219
		private string id11_unsignedByte;

		// Token: 0x040008AC RID: 2220
		private string id5_short;

		// Token: 0x040008AD RID: 2221
		private string id1_string;

		// Token: 0x040008AE RID: 2222
		private string id1_QName;
	}
}

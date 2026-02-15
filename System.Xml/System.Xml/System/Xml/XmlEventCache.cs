using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x02000063 RID: 99
	internal sealed class XmlEventCache : XmlRawWriter
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0001482E File Offset: 0x00012A2E
		public XmlEventCache(string baseUri, bool hasRootNode)
		{
			this.baseUri = baseUri;
			this.hasRootNode = hasRootNode;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014844 File Offset: 0x00012A44
		public void EndEvents()
		{
			if (this.singleText.Count == 0)
			{
				this.AddEvent(XmlEventCache.XmlEventType.Unknown);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001485C File Offset: 0x00012A5C
		public void EventsToWriter(XmlWriter writer)
		{
			if (this.singleText.Count != 0)
			{
				writer.WriteString(this.singleText.GetResult());
				return;
			}
			XmlRawWriter xmlRawWriter = writer as XmlRawWriter;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return;
					case XmlEventCache.XmlEventType.DocType:
						writer.WriteDocType(array[j].String1, array[j].String2, array[j].String3, (string)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.StartElem:
						writer.WriteStartElement(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.StartAttr:
						writer.WriteStartAttribute(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						writer.WriteEndAttribute();
						break;
					case XmlEventCache.XmlEventType.CData:
						writer.WriteCData(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Comment:
						writer.WriteComment(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.PI:
						writer.WriteProcessingInstruction(array[j].String1, array[j].String2);
						break;
					case XmlEventCache.XmlEventType.Whitespace:
						writer.WriteWhitespace(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.String:
						writer.WriteString(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Raw:
						writer.WriteRaw(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.EntRef:
						writer.WriteEntityRef(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.CharEnt:
						writer.WriteCharEntity((char)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.SurrCharEnt:
					{
						char[] array2 = (char[])array[j].Object;
						writer.WriteSurrogateCharEntity(array2[0], array2[1]);
						break;
					}
					case XmlEventCache.XmlEventType.Base64:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBase64(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.BinHex:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBinHex(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.XmlDecl1:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration((XmlStandalone)array[j].Object);
						}
						break;
					case XmlEventCache.XmlEventType.XmlDecl2:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration(array[j].String1);
						}
						break;
					case XmlEventCache.XmlEventType.StartContent:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.StartElementContent();
						}
						break;
					case XmlEventCache.XmlEventType.EndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.FullEndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteFullEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteFullEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.Nmsp:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteNamespaceDeclaration(array[j].String1, array[j].String2);
						}
						else
						{
							writer.WriteAttributeString("xmlns", array[j].String1, "http://www.w3.org/2000/xmlns/", array[j].String2);
						}
						break;
					case XmlEventCache.XmlEventType.EndBase64:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndBase64();
						}
						break;
					case XmlEventCache.XmlEventType.Close:
						writer.Close();
						break;
					case XmlEventCache.XmlEventType.Flush:
						writer.Flush();
						break;
					case XmlEventCache.XmlEventType.Dispose:
						((IDisposable)writer).Dispose();
						break;
					}
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014C6F File Offset: 0x00012E6F
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddEvent(XmlEventCache.XmlEventType.DocType, name, pubid, sysid, subset);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014C7D File Offset: 0x00012E7D
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartElem, prefix, localName, ns);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014C89 File Offset: 0x00012E89
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartAttr, prefix, localName, ns);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014C95 File Offset: 0x00012E95
		public override void WriteEndAttribute()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndAttr);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00014C9E File Offset: 0x00012E9E
		public override void WriteCData(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CData, text);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014CA8 File Offset: 0x00012EA8
		public override void WriteComment(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Comment, text);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00014CB2 File Offset: 0x00012EB2
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.PI, name, text);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014CBD File Offset: 0x00012EBD
		public override void WriteWhitespace(string ws)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Whitespace, ws);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00014CC7 File Offset: 0x00012EC7
		public override void WriteString(string text)
		{
			if (this.pages == null)
			{
				this.singleText.ConcatNoDelimiter(text);
				return;
			}
			this.AddEvent(XmlEventCache.XmlEventType.String, text);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E397 File Offset: 0x0000C597
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014CE7 File Offset: 0x00012EE7
		public override void WriteRaw(string data)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Raw, data);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00014CF2 File Offset: 0x00012EF2
		public override void WriteEntityRef(string name)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EntRef, name);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014CFD File Offset: 0x00012EFD
		public override void WriteCharEntity(char ch)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CharEnt, ch);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00014D10 File Offset: 0x00012F10
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] array = new char[] { lowChar, highChar };
			this.AddEvent(XmlEventCache.XmlEventType.SurrCharEnt, array);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00014D35 File Offset: 0x00012F35
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Base64, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00014D47 File Offset: 0x00012F47
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.BinHex, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00014D59 File Offset: 0x00012F59
		public override void Close()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Close);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014D63 File Offset: 0x00012F63
		public override void Flush()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Flush);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00014D6D File Offset: 0x00012F6D
		public override void WriteValue(object value)
		{
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014D86 File Offset: 0x00012F86
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014D90 File Offset: 0x00012F90
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.AddEvent(XmlEventCache.XmlEventType.Dispose);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014DC4 File Offset: 0x00012FC4
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl1, standalone);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014DD4 File Offset: 0x00012FD4
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl2, xmldecl);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00014DDF File Offset: 0x00012FDF
		internal override void StartElementContent()
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartContent);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014DE9 File Offset: 0x00012FE9
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndElem, prefix, localName, ns);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014DF6 File Offset: 0x00012FF6
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.FullEndElem, prefix, localName, ns);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014E03 File Offset: 0x00013003
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Nmsp, prefix, ns);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014E0F File Offset: 0x0001300F
		internal override void WriteEndBase64()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndBase64);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00014E1C File Offset: 0x0001301C
		private void AddEvent(XmlEventCache.XmlEventType eventType)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014E44 File Offset: 0x00013044
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014E6C File Offset: 0x0001306C
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014E94 File Offset: 0x00013094
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014EC0 File Offset: 0x000130C0
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3, o);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014EEC File Offset: 0x000130EC
		private void AddEvent(XmlEventCache.XmlEventType eventType, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, o);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014F14 File Offset: 0x00013114
		private int NewEvent()
		{
			if (this.pages == null)
			{
				this.pages = new List<XmlEventCache.XmlEvent[]>();
				this.pageCurr = new XmlEventCache.XmlEvent[32];
				this.pages.Add(this.pageCurr);
				if (this.singleText.Count != 0)
				{
					this.pageCurr[0].InitEvent(XmlEventCache.XmlEventType.String, this.singleText.GetResult());
					this.pageSize++;
					this.singleText.Clear();
				}
			}
			else if (this.pageSize >= this.pageCurr.Length)
			{
				this.pageCurr = new XmlEventCache.XmlEvent[this.pageSize * 2];
				this.pages.Add(this.pageCurr);
				this.pageSize = 0;
			}
			int num = this.pageSize;
			this.pageSize = num + 1;
			return num;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014FE4 File Offset: 0x000131E4
		private static byte[] ToBytes(byte[] buffer, int index, int count)
		{
			if (index != 0 || count != buffer.Length)
			{
				if (buffer.Length - index > count)
				{
					count = buffer.Length - index;
				}
				byte[] array = new byte[count];
				Array.Copy(buffer, index, array, 0, count);
				return array;
			}
			return buffer;
		}

		// Token: 0x0400022C RID: 556
		private List<XmlEventCache.XmlEvent[]> pages;

		// Token: 0x0400022D RID: 557
		private XmlEventCache.XmlEvent[] pageCurr;

		// Token: 0x0400022E RID: 558
		private int pageSize;

		// Token: 0x0400022F RID: 559
		private bool hasRootNode;

		// Token: 0x04000230 RID: 560
		private StringConcat singleText;

		// Token: 0x04000231 RID: 561
		private string baseUri;

		// Token: 0x02000064 RID: 100
		private enum XmlEventType
		{
			// Token: 0x04000233 RID: 563
			Unknown,
			// Token: 0x04000234 RID: 564
			DocType,
			// Token: 0x04000235 RID: 565
			StartElem,
			// Token: 0x04000236 RID: 566
			StartAttr,
			// Token: 0x04000237 RID: 567
			EndAttr,
			// Token: 0x04000238 RID: 568
			CData,
			// Token: 0x04000239 RID: 569
			Comment,
			// Token: 0x0400023A RID: 570
			PI,
			// Token: 0x0400023B RID: 571
			Whitespace,
			// Token: 0x0400023C RID: 572
			String,
			// Token: 0x0400023D RID: 573
			Raw,
			// Token: 0x0400023E RID: 574
			EntRef,
			// Token: 0x0400023F RID: 575
			CharEnt,
			// Token: 0x04000240 RID: 576
			SurrCharEnt,
			// Token: 0x04000241 RID: 577
			Base64,
			// Token: 0x04000242 RID: 578
			BinHex,
			// Token: 0x04000243 RID: 579
			XmlDecl1,
			// Token: 0x04000244 RID: 580
			XmlDecl2,
			// Token: 0x04000245 RID: 581
			StartContent,
			// Token: 0x04000246 RID: 582
			EndElem,
			// Token: 0x04000247 RID: 583
			FullEndElem,
			// Token: 0x04000248 RID: 584
			Nmsp,
			// Token: 0x04000249 RID: 585
			EndBase64,
			// Token: 0x0400024A RID: 586
			Close,
			// Token: 0x0400024B RID: 587
			Flush,
			// Token: 0x0400024C RID: 588
			Dispose
		}

		// Token: 0x02000065 RID: 101
		private struct XmlEvent
		{
			// Token: 0x060003FC RID: 1020 RVA: 0x0001501D File Offset: 0x0001321D
			public void InitEvent(XmlEventCache.XmlEventType eventType)
			{
				this.eventType = eventType;
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00015026 File Offset: 0x00013226
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1)
			{
				this.eventType = eventType;
				this.s1 = s1;
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x00015036 File Offset: 0x00013236
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x0001504D File Offset: 0x0001324D
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0001506C File Offset: 0x0001326C
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
				this.o = o;
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x00015093 File Offset: 0x00013293
			public void InitEvent(XmlEventCache.XmlEventType eventType, object o)
			{
				this.eventType = eventType;
				this.o = o;
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x000150A3 File Offset: 0x000132A3
			public XmlEventCache.XmlEventType EventType
			{
				get
				{
					return this.eventType;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x000150AB File Offset: 0x000132AB
			public string String1
			{
				get
				{
					return this.s1;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000404 RID: 1028 RVA: 0x000150B3 File Offset: 0x000132B3
			public string String2
			{
				get
				{
					return this.s2;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x000150BB File Offset: 0x000132BB
			public string String3
			{
				get
				{
					return this.s3;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x000150C3 File Offset: 0x000132C3
			public object Object
			{
				get
				{
					return this.o;
				}
			}

			// Token: 0x0400024D RID: 589
			private XmlEventCache.XmlEventType eventType;

			// Token: 0x0400024E RID: 590
			private string s1;

			// Token: 0x0400024F RID: 591
			private string s2;

			// Token: 0x04000250 RID: 592
			private string s3;

			// Token: 0x04000251 RID: 593
			private object o;
		}
	}
}

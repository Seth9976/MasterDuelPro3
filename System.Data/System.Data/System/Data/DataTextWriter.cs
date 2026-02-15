using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000BA RID: 186
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0003737F File Offset: 0x0003557F
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00037387 File Offset: 0x00035587
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00037398 File Offset: 0x00035598
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000373BC File Offset: 0x000355BC
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000373C9 File Offset: 0x000355C9
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000373D7 File Offset: 0x000355D7
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000373E4 File Offset: 0x000355E4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000373F6 File Offset: 0x000355F6
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00037406 File Offset: 0x00035606
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00037413 File Offset: 0x00035613
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00037420 File Offset: 0x00035620
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00037430 File Offset: 0x00035630
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0003743D File Offset: 0x0003563D
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0003744B File Offset: 0x0003564B
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00037459 File Offset: 0x00035659
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00037468 File Offset: 0x00035668
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00037476 File Offset: 0x00035676
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00037484 File Offset: 0x00035684
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00037492 File Offset: 0x00035692
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000374A0 File Offset: 0x000356A0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000374AF File Offset: 0x000356AF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000374BF File Offset: 0x000356BF
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000374CF File Offset: 0x000356CF
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000374DD File Offset: 0x000356DD
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000374ED File Offset: 0x000356ED
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000374FD File Offset: 0x000356FD
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003750A File Offset: 0x0003570A
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00037517 File Offset: 0x00035717
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00037524 File Offset: 0x00035724
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00037532 File Offset: 0x00035732
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00037541 File Offset: 0x00035741
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0003754F File Offset: 0x0003574F
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0003755C File Offset: 0x0003575C
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00037569 File Offset: 0x00035769
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x040003B9 RID: 953
		private XmlWriter _xmltextWriter;
	}
}

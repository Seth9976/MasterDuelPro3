using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000E4 RID: 228
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003D07B File Offset: 0x0003B27B
		public XmlDOMTextWriter(string filename, Encoding encoding)
			: base(filename, encoding)
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003D085 File Offset: 0x0003B285
		public XmlDOMTextWriter(TextWriter w)
			: base(w)
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003D08E File Offset: 0x0003B28E
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}
	}
}

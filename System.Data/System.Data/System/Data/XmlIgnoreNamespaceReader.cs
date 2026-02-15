using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000AD RID: 173
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0002EED6 File Offset: 0x0002D0D6
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore)
			: base(xdoc)
		{
			this._namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this._namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002EF44 File Offset: 0x0002D144
		public override bool MoveToNextAttribute()
		{
			bool flag;
			bool flag2;
			do
			{
				flag = false;
				flag2 = false;
				if (base.MoveToNextAttribute())
				{
					flag = true;
					if (this._namespacesToIgnore.Contains(this.NamespaceURI) || (this.NamespaceURI == "http://www.w3.org/XML/1998/namespace" && this.LocalName != "lang"))
					{
						flag2 = true;
					}
				}
			}
			while (flag2);
			return flag;
		}

		// Token: 0x0400035E RID: 862
		private List<string> _namespacesToIgnore;
	}
}

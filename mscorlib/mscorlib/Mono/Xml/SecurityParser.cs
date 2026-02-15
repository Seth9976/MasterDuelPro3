using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x02000049 RID: 73
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00002C4D File Offset: 0x00000E4D
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002C60 File Offset: 0x00000E60
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002C81 File Offset: 0x00000E81
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002C89 File Offset: 0x00000E89
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002C89 File Offset: 0x00000E89
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002C89 File Offset: 0x00000E89
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				((SecurityElement)this.stack.Peek()).AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002D12 File Offset: 0x00000F12
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002D2A File Offset: 0x00000F2A
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002C89 File Offset: 0x00000E89
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x0400013E RID: 318
		private SecurityElement root;

		// Token: 0x0400013F RID: 319
		private SecurityElement current;

		// Token: 0x04000140 RID: 320
		private Stack stack;
	}
}

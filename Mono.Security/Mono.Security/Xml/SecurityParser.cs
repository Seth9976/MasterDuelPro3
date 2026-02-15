using System;
using System.Collections;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x0200000A RID: 10
	[CLSCompliant(false)]
	public class SecurityParser : MiniParser, MiniParser.IHandler, MiniParser.IReader
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002A71 File Offset: 0x00000C71
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A84 File Offset: 0x00000C84
		public void LoadXml(string xml)
		{
			this.root = null;
			this.xmldoc = xml;
			this.pos = 0;
			this.stack.Clear();
			base.Parse(this, this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AAE File Offset: 0x00000CAE
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public int Read()
		{
			if (this.pos >= this.xmldoc.Length)
			{
				return -1;
			}
			string text = this.xmldoc;
			int num = this.pos;
			this.pos = num + 1;
			return (int)text[num];
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002945 File Offset: 0x00000B45
		public void OnStartParsing(MiniParser parser)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public void OnStartElement(string name, MiniParser.IAttrList attrs)
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002B7E File Offset: 0x00000D7E
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B96 File Offset: 0x00000D96
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002945 File Offset: 0x00000B45
		public void OnEndParsing(MiniParser parser)
		{
		}

		// Token: 0x0400000D RID: 13
		private SecurityElement root;

		// Token: 0x0400000E RID: 14
		private string xmldoc;

		// Token: 0x0400000F RID: 15
		private int pos;

		// Token: 0x04000010 RID: 16
		private SecurityElement current;

		// Token: 0x04000011 RID: 17
		private Stack stack;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System.Xml.Linq
{
	/// <summary>Represents a node that can contain other nodes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000009 RID: 9
	public abstract class XContainer : XNode
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002488 File Offset: 0x00000688
		internal XContainer()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002490 File Offset: 0x00000690
		internal XContainer(XContainer other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.content is string)
			{
				this.content = other.content;
				return;
			}
			XNode xnode = (XNode)other.content;
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					this.AppendNodeSkipNotify(xnode.CloneNode());
				}
				while (xnode != other.content);
			}
		}

		/// <summary>Get the last child node of this node.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> containing the last child node of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024F8 File Offset: 0x000006F8
		public XNode LastNode
		{
			get
			{
				if (this.content == null)
				{
					return null;
				}
				XNode xnode = this.content as XNode;
				if (xnode != null)
				{
					return xnode;
				}
				string text = this.content as string;
				if (text != null)
				{
					if (text.Length == 0)
					{
						return null;
					}
					XText xtext = new XText(text);
					xtext.parent = this;
					xtext.next = xtext;
					Interlocked.CompareExchange<object>(ref this.content, xtext, text);
				}
				return (XNode)this.content;
			}
		}

		/// <summary>Adds the specified content as children of this <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects to be added.</param>
		// Token: 0x0600001F RID: 31 RVA: 0x00002568 File Offset: 0x00000768
		public void Add(object content)
		{
			if (base.SkipNotify())
			{
				this.AddContentSkipNotify(content);
				return;
			}
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddString(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.AddAttribute(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNode(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object obj in array)
				{
					this.Add(obj);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object obj2 in enumerable)
				{
					this.Add(obj2);
				}
				return;
			}
			this.AddString(XContainer.GetStringValue(content));
		}

		/// <summary>Returns a collection of the child nodes of this element or document, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> containing the contents of this <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		// Token: 0x06000020 RID: 32 RVA: 0x00002670 File Offset: 0x00000870
		public IEnumerable<XNode> Nodes()
		{
			XNode i = this.LastNode;
			if (i != null)
			{
				do
				{
					i = i.next;
					yield return i;
				}
				while (i.parent == this && i != this.content);
			}
			yield break;
		}

		/// <summary>Removes the child nodes from this document or element.</summary>
		// Token: 0x06000021 RID: 33 RVA: 0x00002680 File Offset: 0x00000880
		public void RemoveNodes()
		{
			if (base.SkipNotify())
			{
				this.RemoveNodesSkipNotify();
				return;
			}
			while (this.content != null)
			{
				string text = this.content as string;
				if (text != null)
				{
					if (text.Length > 0)
					{
						this.ConvertTextToNode();
					}
					else if (this is XElement)
					{
						base.NotifyChanging(this, XObjectChangeEventArgs.Value);
						if (text != this.content)
						{
							throw new InvalidOperationException("This operation was corrupted by external code.");
						}
						this.content = null;
						base.NotifyChanged(this, XObjectChangeEventArgs.Value);
					}
					else
					{
						this.content = null;
					}
				}
				XNode xnode = this.content as XNode;
				if (xnode != null)
				{
					XNode next = xnode.next;
					base.NotifyChanging(next, XObjectChangeEventArgs.Remove);
					if (xnode != this.content || next != xnode.next)
					{
						throw new InvalidOperationException("This operation was corrupted by external code.");
					}
					if (next != xnode)
					{
						xnode.next = next.next;
					}
					else
					{
						this.content = null;
					}
					next.parent = null;
					next.next = null;
					base.NotifyChanged(next, XObjectChangeEventArgs.Remove);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual void AddAttribute(XAttribute a)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual void AddAttributeSkipNotify(XAttribute a)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002788 File Offset: 0x00000988
		internal void AddContentSkipNotify(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNodeSkipNotify(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddStringSkipNotify(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.AddAttributeSkipNotify(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNodeSkipNotify(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object obj in array)
				{
					this.AddContentSkipNotify(obj);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object obj2 in enumerable)
				{
					this.AddContentSkipNotify(obj2);
				}
				return;
			}
			this.AddStringSkipNotify(XContainer.GetStringValue(content));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002880 File Offset: 0x00000A80
		internal void AddNode(XNode n)
		{
			this.ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xnode = this;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
				}
				if (n == xnode)
				{
					n = n.CloneNode();
				}
			}
			this.ConvertTextToNode();
			this.AppendNode(n);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal void AddNodeSkipNotify(XNode n)
		{
			this.ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xnode = this;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
				}
				if (n == xnode)
				{
					n = n.CloneNode();
				}
			}
			this.ConvertTextToNode();
			this.AppendNodeSkipNotify(n);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002928 File Offset: 0x00000B28
		internal void AddString(string s)
		{
			this.ValidateString(s);
			if (this.content != null)
			{
				if (s.Length > 0)
				{
					this.ConvertTextToNode();
					XText xtext = this.content as XText;
					if (xtext != null && !(xtext is XCData))
					{
						XText xtext2 = xtext;
						xtext2.Value += s;
						return;
					}
					this.AppendNode(new XText(s));
				}
				return;
			}
			if (s.Length > 0)
			{
				this.AppendNode(new XText(s));
				return;
			}
			if (!(this is XElement))
			{
				this.content = s;
				return;
			}
			base.NotifyChanging(this, XObjectChangeEventArgs.Value);
			if (this.content != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			this.content = s;
			base.NotifyChanged(this, XObjectChangeEventArgs.Value);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal void AddStringSkipNotify(string s)
		{
			this.ValidateString(s);
			if (this.content == null)
			{
				this.content = s;
				return;
			}
			if (s.Length > 0)
			{
				string text = this.content as string;
				if (text != null)
				{
					this.content = text + s;
					return;
				}
				XText xtext = this.content as XText;
				if (xtext != null && !(xtext is XCData))
				{
					XText xtext2 = xtext;
					xtext2.text += s;
					return;
				}
				this.AppendNodeSkipNotify(new XText(s));
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A64 File Offset: 0x00000C64
		internal void AppendNode(XNode n)
		{
			bool flag = base.NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			this.AppendNodeSkipNotify(n);
			if (flag)
			{
				base.NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A9C File Offset: 0x00000C9C
		internal void AppendNodeSkipNotify(XNode n)
		{
			n.parent = this;
			if (this.content == null || this.content is string)
			{
				n.next = n;
			}
			else
			{
				XNode xnode = (XNode)this.content;
				n.next = xnode.next;
				xnode.next = n;
			}
			this.content = n;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AF4 File Offset: 0x00000CF4
		internal override void AppendText(StringBuilder sb)
		{
			string text = this.content as string;
			if (text != null)
			{
				sb.Append(text);
				return;
			}
			XNode xnode = (XNode)this.content;
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					xnode.AppendText(sb);
				}
				while (xnode != this.content);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B40 File Offset: 0x00000D40
		internal void ConvertTextToNode()
		{
			string text = this.content as string;
			if (!string.IsNullOrEmpty(text))
			{
				XText xtext = new XText(text);
				xtext.parent = this;
				xtext.next = xtext;
				this.content = xtext;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B80 File Offset: 0x00000D80
		internal static string GetStringValue(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is double)
			{
				text = XmlConvert.ToString((double)value);
			}
			else if (value is float)
			{
				text = XmlConvert.ToString((float)value);
			}
			else if (value is decimal)
			{
				text = XmlConvert.ToString((decimal)value);
			}
			else if (value is bool)
			{
				text = XmlConvert.ToString((bool)value);
			}
			else if (value is DateTime)
			{
				text = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			else if (value is DateTimeOffset)
			{
				text = XmlConvert.ToString((DateTimeOffset)value);
			}
			else if (value is TimeSpan)
			{
				text = XmlConvert.ToString((TimeSpan)value);
			}
			else
			{
				if (value is XObject)
				{
					throw new ArgumentException("An XObject cannot be used as a value.");
				}
				text = value.ToString();
			}
			if (text == null)
			{
				throw new ArgumentException("The argument cannot be converted to a string.");
			}
			return text;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C64 File Offset: 0x00000E64
		internal void ReadContentFrom(XmlReader r)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			XContainer.ContentReader contentReader = new XContainer.ContentReader(this);
			while (contentReader.ReadContentFrom(this, r) && r.Read())
			{
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CA0 File Offset: 0x00000EA0
		internal void ReadContentFrom(XmlReader r, LoadOptions o)
		{
			if ((o & (LoadOptions.SetBaseUri | LoadOptions.SetLineInfo)) == LoadOptions.None)
			{
				this.ReadContentFrom(r);
				return;
			}
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			XContainer.ContentReader contentReader = new XContainer.ContentReader(this, r, o);
			while (contentReader.ReadContentFrom(this, r, o) && r.Read())
			{
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal void RemoveNode(XNode n)
		{
			bool flag = base.NotifyChanging(n, XObjectChangeEventArgs.Remove);
			if (n.parent != this)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			XNode xnode = (XNode)this.content;
			while (xnode.next != n)
			{
				xnode = xnode.next;
			}
			if (xnode == n)
			{
				this.content = null;
			}
			else
			{
				if (this.content == n)
				{
					this.content = xnode;
				}
				xnode.next = n.next;
			}
			n.parent = null;
			n.next = null;
			if (flag)
			{
				base.NotifyChanged(n, XObjectChangeEventArgs.Remove);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D80 File Offset: 0x00000F80
		private void RemoveNodesSkipNotify()
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				do
				{
					XNode next = xnode.next;
					xnode.parent = null;
					xnode.next = null;
					xnode = next;
				}
				while (xnode != this.content);
			}
			this.content = null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual void ValidateNode(XNode node, XNode previous)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual void ValidateString(string s)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DC4 File Offset: 0x00000FC4
		internal void WriteContentTo(XmlWriter writer)
		{
			if (this.content != null)
			{
				string text = this.content as string;
				if (text != null)
				{
					if (this is XDocument)
					{
						writer.WriteWhitespace(text);
						return;
					}
					writer.WriteString(text);
					return;
				}
				else
				{
					XNode xnode = (XNode)this.content;
					do
					{
						xnode = xnode.next;
						xnode.WriteTo(writer);
					}
					while (xnode != this.content);
				}
			}
		}

		// Token: 0x04000008 RID: 8
		internal object content;

		// Token: 0x0200000A RID: 10
		private sealed class ContentReader
		{
			// Token: 0x06000035 RID: 53 RVA: 0x00002E23 File Offset: 0x00001023
			public ContentReader(XContainer rootContainer)
			{
				this._currentContainer = rootContainer;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002E32 File Offset: 0x00001032
			public ContentReader(XContainer rootContainer, XmlReader r, LoadOptions o)
			{
				this._currentContainer = rootContainer;
				this._baseUri = (((o & LoadOptions.SetBaseUri) != LoadOptions.None) ? r.BaseURI : null);
				this._lineInfo = (((o & LoadOptions.SetLineInfo) != LoadOptions.None) ? (r as IXmlLineInfo) : null);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002E6C File Offset: 0x0000106C
			public bool ReadContentFrom(XContainer rootContainer, XmlReader r)
			{
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					NamespaceCache namespaceCache = this._eCache;
					XElement xelement = new XElement(namespaceCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (r.MoveToFirstAttribute())
					{
						do
						{
							XElement xelement2 = xelement;
							namespaceCache = this._aCache;
							xelement2.AppendAttributeSkipNotify(new XAttribute(namespaceCache.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value));
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					this._currentContainer.AddNodeSkipNotify(xelement);
					if (!r.IsEmptyElement)
					{
						this._currentContainer = xelement;
						return true;
					}
					return true;
				}
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this._currentContainer.AddStringSkipNotify(r.Value);
					return true;
				case XmlNodeType.CDATA:
					this._currentContainer.AddNodeSkipNotify(new XCData(r.Value));
					return true;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						throw new InvalidOperationException("The XmlReader cannot resolve entity references.");
					}
					r.ResolveEntity();
					return true;
				case XmlNodeType.ProcessingInstruction:
					this._currentContainer.AddNodeSkipNotify(new XProcessingInstruction(r.Name, r.Value));
					return true;
				case XmlNodeType.Comment:
					this._currentContainer.AddNodeSkipNotify(new XComment(r.Value));
					return true;
				case XmlNodeType.DocumentType:
					this._currentContainer.AddNodeSkipNotify(new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value));
					return true;
				case XmlNodeType.EndElement:
					if (this._currentContainer.content == null)
					{
						this._currentContainer.content = string.Empty;
					}
					if (this._currentContainer == rootContainer)
					{
						return false;
					}
					this._currentContainer = this._currentContainer.parent;
					return true;
				case XmlNodeType.EndEntity:
					return true;
				}
				throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", r.NodeType));
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00003084 File Offset: 0x00001284
			public bool ReadContentFrom(XContainer rootContainer, XmlReader r, LoadOptions o)
			{
				XNode xnode = null;
				string baseURI = r.BaseURI;
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					NamespaceCache namespaceCache = this._eCache;
					XElement xelement = new XElement(namespaceCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (this._baseUri != null && this._baseUri != baseURI)
					{
						xelement.SetBaseUri(baseURI);
					}
					if (this._lineInfo != null && this._lineInfo.HasLineInfo())
					{
						xelement.SetLineInfo(this._lineInfo.LineNumber, this._lineInfo.LinePosition);
					}
					if (r.MoveToFirstAttribute())
					{
						do
						{
							namespaceCache = this._aCache;
							XAttribute xattribute = new XAttribute(namespaceCache.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
							if (this._lineInfo != null && this._lineInfo.HasLineInfo())
							{
								xattribute.SetLineInfo(this._lineInfo.LineNumber, this._lineInfo.LinePosition);
							}
							xelement.AppendAttributeSkipNotify(xattribute);
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					this._currentContainer.AddNodeSkipNotify(xelement);
					if (r.IsEmptyElement)
					{
						goto IL_032F;
					}
					this._currentContainer = xelement;
					if (this._baseUri != null)
					{
						this._baseUri = baseURI;
						goto IL_032F;
					}
					goto IL_032F;
				}
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if ((this._baseUri != null && this._baseUri != baseURI) || (this._lineInfo != null && this._lineInfo.HasLineInfo()))
					{
						xnode = new XText(r.Value);
						goto IL_032F;
					}
					this._currentContainer.AddStringSkipNotify(r.Value);
					goto IL_032F;
				case XmlNodeType.CDATA:
					xnode = new XCData(r.Value);
					goto IL_032F;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						throw new InvalidOperationException("The XmlReader cannot resolve entity references.");
					}
					r.ResolveEntity();
					goto IL_032F;
				case XmlNodeType.ProcessingInstruction:
					xnode = new XProcessingInstruction(r.Name, r.Value);
					goto IL_032F;
				case XmlNodeType.Comment:
					xnode = new XComment(r.Value);
					goto IL_032F;
				case XmlNodeType.DocumentType:
					xnode = new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value);
					goto IL_032F;
				case XmlNodeType.EndElement:
				{
					if (this._currentContainer.content == null)
					{
						this._currentContainer.content = string.Empty;
					}
					XElement xelement2 = this._currentContainer as XElement;
					if (xelement2 != null && this._lineInfo != null && this._lineInfo.HasLineInfo())
					{
						xelement2.SetEndElementLineInfo(this._lineInfo.LineNumber, this._lineInfo.LinePosition);
					}
					if (this._currentContainer == rootContainer)
					{
						return false;
					}
					if (this._baseUri != null && this._currentContainer.HasBaseUri)
					{
						this._baseUri = this._currentContainer.parent.BaseUri;
					}
					this._currentContainer = this._currentContainer.parent;
					goto IL_032F;
				}
				case XmlNodeType.EndEntity:
					goto IL_032F;
				}
				throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", r.NodeType));
				IL_032F:
				if (xnode != null)
				{
					if (this._baseUri != null && this._baseUri != baseURI)
					{
						xnode.SetBaseUri(baseURI);
					}
					if (this._lineInfo != null && this._lineInfo.HasLineInfo())
					{
						xnode.SetLineInfo(this._lineInfo.LineNumber, this._lineInfo.LinePosition);
					}
					this._currentContainer.AddNodeSkipNotify(xnode);
				}
				return true;
			}

			// Token: 0x04000009 RID: 9
			private readonly NamespaceCache _eCache;

			// Token: 0x0400000A RID: 10
			private readonly NamespaceCache _aCache;

			// Token: 0x0400000B RID: 11
			private readonly IXmlLineInfo _lineInfo;

			// Token: 0x0400000C RID: 12
			private XContainer _currentContainer;

			// Token: 0x0400000D RID: 13
			private string _baseUri;
		}
	}
}

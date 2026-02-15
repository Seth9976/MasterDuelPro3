using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only way of generating streams or files containing XML data that conforms to the W3C Extensible Markup Language (XML) 1.0 and the Namespaces in XML recommendations. </summary>
	// Token: 0x02000080 RID: 128
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlTextWriter : XmlWriter
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x000250E4 File Offset: 0x000232E4
		internal XmlTextWriter()
		{
			this.namespaces = true;
			this.formatting = Formatting.None;
			this.indentation = 2;
			this.indentChar = ' ';
			this.nsStack = new XmlTextWriter.Namespace[8];
			this.nsTop = -1;
			this.stack = new XmlTextWriter.TagInfo[10];
			this.top = 0;
			this.stack[this.top].Init(-1);
			this.quoteChar = '"';
			this.stateTable = XmlTextWriter.stateTableDefault;
			this.currentState = XmlTextWriter.State.Start;
			this.lastToken = XmlTextWriter.Token.Empty;
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified stream and encoding.</summary>
		/// <param name="w">The stream to which you want to write. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes out the stream as UTF-8 and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported or the stream cannot be written to. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="w" /> is null. </exception>
		// Token: 0x06000707 RID: 1799 RVA: 0x00025180 File Offset: 0x00023380
		public XmlTextWriter(Stream w, Encoding encoding)
			: this()
		{
			this.encoding = encoding;
			if (encoding != null)
			{
				this.textWriter = new StreamWriter(w, encoding);
			}
			else
			{
				this.textWriter = new StreamWriter(w);
			}
			this.xmlEncoder = new XmlTextEncoder(this.textWriter);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Xml.XmlTextWriter" /> class using the specified file.</summary>
		/// <param name="filename">The filename to write to. If the file exists, it truncates it and overwrites it with the new content. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes the file out as UTF-8, and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported; the filename is empty, contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentNullException">The filename is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to write to is not found. </exception>
		/// <exception cref="T:System.IO.IOException">The filename includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000708 RID: 1800 RVA: 0x000251DA File Offset: 0x000233DA
		public XmlTextWriter(string filename, Encoding encoding)
			: this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), encoding)
		{
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="w">The TextWriter to write to. It is assumed that the TextWriter is already set to the correct encoding. </param>
		// Token: 0x06000709 RID: 1801 RVA: 0x000251EC File Offset: 0x000233EC
		public XmlTextWriter(TextWriter w)
			: this()
		{
			this.textWriter = w;
			this.encoding = w.Encoding;
			this.xmlEncoder = new XmlTextEncoder(w);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Gets the underlying stream object.</summary>
		/// <returns>The stream to which the XmlTextWriter is writing or null if the XmlTextWriter was constructed using a <see cref="T:System.IO.TextWriter" /> that does not inherit from the <see cref="T:System.IO.StreamWriter" /> class.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00025224 File Offset: 0x00023424
		public Stream BaseStream
		{
			get
			{
				StreamWriter streamWriter = this.textWriter as StreamWriter;
				if (streamWriter != null)
				{
					return streamWriter.BaseStream;
				}
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to support namespaces; otherwise, false.The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">You can only change this property when in the WriteState.Start state. </exception>
		// Token: 0x17000164 RID: 356
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00025248 File Offset: 0x00023448
		public bool Namespaces
		{
			set
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("NotInWriteState."));
				}
				this.namespaces = value;
			}
		}

		/// <summary>Indicates how the output is formatted.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Formatting" /> values. The default is Formatting.None (no special formatting).</returns>
		// Token: 0x17000165 RID: 357
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x00025269 File Offset: 0x00023469
		public Formatting Formatting
		{
			set
			{
				this.formatting = value;
				this.indented = value == Formatting.Indented;
			}
		}

		/// <summary>Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to Formatting.Indented.</summary>
		/// <returns>Number of IndentChars for each level. The default is 2.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to a negative value. </exception>
		// Token: 0x17000166 RID: 358
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0002527C File Offset: 0x0002347C
		public int Indentation
		{
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Indentation value must be greater than 0."));
				}
				this.indentation = value;
			}
		}

		/// <summary>Gets or sets which character to use to quote attribute values.</summary>
		/// <returns>The character to use to quote attribute values. This must be a single quote (&amp;#39;) or a double quote (&amp;#34;). The default is a double quote.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to something other than either a single or double quote. </exception>
		// Token: 0x17000167 RID: 359
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00025299 File Offset: 0x00023499
		public char QuoteChar
		{
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException(Res.GetString("Invalid XML attribute quote character. Valid attribute quote characters are ' and \"."));
				}
				this.quoteChar = value;
				this.xmlEncoder.QuoteChar = value;
			}
		}

		/// <summary>Writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x0600070F RID: 1807 RVA: 0x000252C8 File Offset: 0x000234C8
		public override void WriteStartDocument()
		{
			this.StartDocument(-1);
		}

		/// <summary>Writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000710 RID: 1808 RVA: 0x000252D1 File Offset: 0x000234D1
		public override void WriteStartDocument(bool standalone)
		{
			this.StartDocument(standalone ? 1 : 0);
		}

		/// <summary>Closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid. </exception>
		// Token: 0x06000711 RID: 1809 RVA: 0x000252E0 File Offset: 0x000234E0
		public override void WriteEndDocument()
		{
			try
			{
				this.AutoCompleteAll();
				if (this.currentState != XmlTextWriter.State.Epilog)
				{
					if (this.currentState == XmlTextWriter.State.Closed)
					{
						throw new ArgumentException(Res.GetString("The Writer is closed or in error state."));
					}
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				else
				{
					this.stateTable = XmlTextWriter.stateTableDefault;
					this.currentState = XmlTextWriter.State.Start;
					this.lastToken = XmlTextWriter.Token.Empty;
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty. </param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments. </param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument. </param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument. </param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or String.Empty-or- the value for <paramref name="name" /> would result in invalid XML. </exception>
		// Token: 0x06000712 RID: 1810 RVA: 0x0002535C File Offset: 0x0002355C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Doctype);
				this.textWriter.Write("<!DOCTYPE ");
				this.textWriter.Write(name);
				if (pubid != null)
				{
					this.textWriter.Write(" PUBLIC " + this.quoteChar.ToString());
					this.textWriter.Write(pubid);
					this.textWriter.Write(this.quoteChar.ToString() + " " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				else if (sysid != null)
				{
					this.textWriter.Write(" SYSTEM " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				if (subset != null)
				{
					this.textWriter.Write("[");
					this.textWriter.Write(subset);
					this.textWriter.Write("]");
				}
				this.textWriter.Write('>');
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element. </param>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix then the writer automatically writes that prefix also. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000713 RID: 1811 RVA: 0x000254B4 File Offset: 0x000236B4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartElement);
				this.PushStack();
				this.textWriter.Write('<');
				if (this.namespaces)
				{
					this.stack[this.top].defaultNs = this.stack[this.top - 1].defaultNs;
					if (this.stack[this.top - 1].defaultNsState != XmlTextWriter.NamespaceState.Uninitialized)
					{
						this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.NotDeclaredButInScope;
					}
					this.stack[this.top].mixed = this.stack[this.top - 1].mixed;
					if (ns == null)
					{
						if (prefix != null && prefix.Length != 0 && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (prefix == null)
					{
						string text = this.FindPrefix(ns);
						if (text != null)
						{
							prefix = text;
						}
						else
						{
							this.PushNamespace(null, ns, false);
						}
					}
					else if (prefix.Length == 0)
					{
						this.PushNamespace(null, ns, false);
					}
					else
					{
						if (ns.Length == 0)
						{
							prefix = null;
						}
						this.VerifyPrefixXml(prefix, ns);
						this.PushNamespace(prefix, ns, false);
					}
					this.stack[this.top].prefix = null;
					if (prefix != null && prefix.Length != 0)
					{
						this.stack[this.top].prefix = prefix;
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.stack[this.top].name = localName;
				this.textWriter.Write(localName);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000714 RID: 1812 RVA: 0x000256B0 File Offset: 0x000238B0
		public override void WriteEndElement()
		{
			this.InternalWriteEndElement(false);
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000715 RID: 1813 RVA: 0x000256B9 File Offset: 0x000238B9
		public override void WriteFullEndElement()
		{
			this.InternalWriteEndElement(true);
		}

		/// <summary>Writes the start of an attribute.</summary>
		/// <param name="prefix">Namespace prefix of the attribute. </param>
		/// <param name="localName">LocalName of the attribute. </param>
		/// <param name="ns">NamespaceURI of the attribute </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty. </exception>
		// Token: 0x06000716 RID: 1814 RVA: 0x000256C4 File Offset: 0x000238C4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartAttribute);
				this.specialAttr = XmlTextWriter.SpecialAttr.None;
				if (this.namespaces)
				{
					if (prefix != null && prefix.Length == 0)
					{
						prefix = null;
					}
					if (ns == "http://www.w3.org/2000/xmlns/" && prefix == null && localName != "xmlns")
					{
						prefix = "xmlns";
					}
					if (prefix == "xml")
					{
						if (localName == "lang")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
						}
						else if (localName == "space")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
						}
					}
					else if (prefix == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						if (localName == null || localName.Length == 0)
						{
							localName = prefix;
							prefix = null;
							this.prefixForXmlNs = null;
						}
						else
						{
							this.prefixForXmlNs = localName;
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
					}
					else if (prefix == null && localName == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
						this.prefixForXmlNs = null;
					}
					else if (ns == null)
					{
						if (prefix != null && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (ns.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						this.VerifyPrefixXml(prefix, ns);
						if (prefix != null && this.LookupNamespaceInCurrentScope(prefix) != -1)
						{
							prefix = null;
						}
						string text = this.FindPrefix(ns);
						if (text != null && (prefix == null || prefix == text))
						{
							prefix = text;
						}
						else
						{
							if (prefix == null)
							{
								prefix = this.GeneratePrefix();
							}
							this.PushNamespace(prefix, ns, false);
						}
					}
					if (prefix != null && prefix.Length != 0)
					{
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else
				{
					if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
					{
						throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
					}
					if (localName == "xml:lang")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
					}
					else if (localName == "xml:space")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
					}
				}
				this.xmlEncoder.StartAttribute(this.specialAttr > XmlTextWriter.SpecialAttr.None);
				this.textWriter.Write(localName);
				this.textWriter.Write('=');
				if (this.curQuoteChar != this.quoteChar)
				{
					this.curQuoteChar = this.quoteChar;
					this.xmlEncoder.QuoteChar = this.quoteChar;
				}
				this.textWriter.Write(this.curQuoteChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes the previous <see cref="M:System.Xml.XmlTextWriter.WriteStartAttribute(System.String,System.String,System.String)" /> call.</summary>
		// Token: 0x06000717 RID: 1815 RVA: 0x00025978 File Offset: 0x00023B78
		public override void WriteEndAttribute()
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.EndAttribute);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">Text to place inside the CDATA block. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000718 RID: 1816 RVA: 0x000259A8 File Offset: 0x00023BA8
		public override void WriteCData(string text)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.CData);
				if (text != null && text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have ']]>' inside an XML CDATA block."));
				}
				this.textWriter.Write("<![CDATA[");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("]]>");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000719 RID: 1817 RVA: 0x00025A2C File Offset: 0x00023C2C
		public override void WriteComment(string text)
		{
			try
			{
				if (text != null && (text.IndexOf("--", StringComparison.Ordinal) >= 0 || (text.Length != 0 && text[text.Length - 1] == '-')))
				{
					throw new ArgumentException(Res.GetString("An XML comment cannot contain '--', and '-' cannot be the last character."));
				}
				this.AutoComplete(XmlTextWriter.Token.Comment);
				this.textWriter.Write("<!--");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("-->");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">Name of the processing instruction. </param>
		/// <param name="text">Text to include in the processing instruction. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlTextWriter.WriteStartDocument" /> has already been called. </exception>
		// Token: 0x0600071A RID: 1818 RVA: 0x00025AC8 File Offset: 0x00023CC8
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (text != null && text.IndexOf("?>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have '?>' inside an XML processing instruction."));
				}
				if (string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0 && this.stateTable == XmlTextWriter.stateTableDocument)
				{
					throw new ArgumentException(Res.GetString("Cannot write XML declaration. WriteStartDocument method has already written it."));
				}
				this.AutoComplete(XmlTextWriter.Token.PI);
				this.InternalWriteProcessingInstruction(name, text);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">Name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x0600071B RID: 1819 RVA: 0x00025B50 File Offset: 0x00023D50
		public override void WriteEntityRef(string name)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteEntityRef(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">Unicode character for which to generate a character entity. </param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff; or the text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x0600071C RID: 1820 RVA: 0x00025B98 File Offset: 0x00023D98
		public override void WriteCharEntity(char ch)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteCharEntity(ch);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters. </param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters. </exception>
		// Token: 0x0600071D RID: 1821 RVA: 0x00025BD8 File Offset: 0x00023DD8
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!this.xmlCharType.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AutoComplete(XmlTextWriter.Token.Whitespace);
				this.xmlEncoder.Write(ws);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="text">Text to write. </param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair. </exception>
		// Token: 0x0600071E RID: 1822 RVA: 0x00025C40 File Offset: 0x00023E40
		public override void WriteString(string text)
		{
			try
			{
				if (text != null && text.Length != 0)
				{
					this.AutoComplete(XmlTextWriter.Token.Content);
					this.xmlEncoder.Write(text);
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF. </param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF. </param>
		/// <exception cref="T:System.Exception">An invalid surrogate character pair was passed. </exception>
		// Token: 0x0600071F RID: 1823 RVA: 0x00025C88 File Offset: 0x00023E88
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteSurrogateCharEntity(lowChar, highChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position in the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000720 RID: 1824 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.Write(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position within the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06000721 RID: 1825 RVA: 0x00025D08 File Offset: 0x00023F08
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRaw(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write. </param>
		// Token: 0x06000722 RID: 1826 RVA: 0x00025D48 File Offset: 0x00023F48
		public override void WriteRaw(string data)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRawWithSurrogateChecking(data);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position within the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000723 RID: 1827 RVA: 0x00025D88 File Offset: 0x00023F88
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (!this.flush)
				{
					this.AutoComplete(XmlTextWriter.Token.Base64);
				}
				this.flush = true;
				if (this.base64Encoder == null)
				{
					this.base64Encoder = new XmlTextWriterBase64Encoder(this.xmlEncoder);
				}
				this.base64Encoder.Encode(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as binhex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000724 RID: 1828 RVA: 0x00025DF0 File Offset: 0x00023FF0
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				BinHexEncoder.Encode(buffer, index, count, this);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00025E2C File Offset: 0x0002402C
		public override WriteState WriteState
		{
			get
			{
				switch (this.currentState)
				{
				case XmlTextWriter.State.Start:
					return WriteState.Start;
				case XmlTextWriter.State.Prolog:
				case XmlTextWriter.State.PostDTD:
					return WriteState.Prolog;
				case XmlTextWriter.State.Element:
					return WriteState.Element;
				case XmlTextWriter.State.Attribute:
				case XmlTextWriter.State.AttrOnly:
					return WriteState.Attribute;
				case XmlTextWriter.State.Content:
				case XmlTextWriter.State.Epilog:
					return WriteState.Content;
				case XmlTextWriter.State.Error:
					return WriteState.Error;
				case XmlTextWriter.State.Closed:
					return WriteState.Closed;
				default:
					return WriteState.Error;
				}
			}
		}

		/// <summary>Closes this stream and the underlying stream.</summary>
		// Token: 0x06000726 RID: 1830 RVA: 0x00025E80 File Offset: 0x00024080
		public override void Close()
		{
			try
			{
				this.AutoCompleteAll();
			}
			catch
			{
			}
			finally
			{
				this.currentState = XmlTextWriter.State.Closed;
				this.textWriter.Close();
			}
		}

		/// <summary>Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		// Token: 0x06000727 RID: 1831 RVA: 0x00025EC8 File Offset: 0x000240C8
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		/// <summary>Writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000728 RID: 1832 RVA: 0x00025ED8 File Offset: 0x000240D8
		public override void WriteName(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.InternalWriteName(name, false);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write. </param>
		/// <param name="ns">The namespace URI to associate with the name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name according to the W3C Namespaces spec. </exception>
		// Token: 0x06000729 RID: 1833 RVA: 0x00025F14 File Offset: 0x00024114
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (this.namespaces)
				{
					if (ns != null && ns.Length != 0 && ns != this.stack[this.top].defaultNs)
					{
						string text = this.FindPrefix(ns);
						if (text == null)
						{
							if (this.currentState != XmlTextWriter.State.Attribute)
							{
								throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
							}
							text = this.GeneratePrefix();
							this.PushNamespace(text, ns, false);
						}
						if (text.Length != 0)
						{
							this.InternalWriteName(text, true);
							this.textWriter.Write(':');
						}
					}
				}
				else if (ns != null && ns.Length != 0)
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.InternalWriteName(localName, true);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix. Or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">Namespace URI whose prefix you want to find. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty. </exception>
		// Token: 0x0600072A RID: 1834 RVA: 0x00026000 File Offset: 0x00024200
		public override string LookupPrefix(string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			string text = this.FindPrefix(ns);
			if (text == null && ns == this.stack[this.top].defaultNs)
			{
				text = string.Empty;
			}
			return text;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning None This is the default if no xml:space scope exists. Default The current scope is xml:space="default". Preserve The current scope is xml:space="preserve". </returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00026058 File Offset: 0x00024258
		public override XmlSpace XmlSpace
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					XmlSpace xmlSpace = this.stack[i].xmlSpace;
					if (xmlSpace != XmlSpace.None)
					{
						return xmlSpace;
					}
				}
				return XmlSpace.None;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang or null if there is no xml:lang in the current scope.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00026090 File Offset: 0x00024290
		public override string XmlLang
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					string xmlLang = this.stack[i].xmlLang;
					if (xmlLang != null)
					{
						return xmlLang;
					}
				}
				return null;
			}
		}

		/// <summary>Writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x0600072D RID: 1837 RVA: 0x000260C8 File Offset: 0x000242C8
		public override void WriteNmToken(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				if (!ValidateNames.IsNmtokenNoNamespaces(name))
				{
					throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }));
				}
				this.textWriter.Write(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00026144 File Offset: 0x00024344
		private void StartDocument(int standalone)
		{
			try
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument needs to be the first call."));
				}
				this.stateTable = XmlTextWriter.stateTableDocument;
				this.currentState = XmlTextWriter.State.Prolog;
				StringBuilder stringBuilder = new StringBuilder(128);
				stringBuilder.Append("version=" + this.quoteChar.ToString() + "1.0" + this.quoteChar.ToString());
				if (this.encoding != null)
				{
					stringBuilder.Append(" encoding=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append(this.encoding.WebName);
					stringBuilder.Append(this.quoteChar);
				}
				if (standalone >= 0)
				{
					stringBuilder.Append(" standalone=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append((standalone == 0) ? "no" : "yes");
					stringBuilder.Append(this.quoteChar);
				}
				this.InternalWriteProcessingInstruction("xml", stringBuilder.ToString());
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00026260 File Offset: 0x00024460
		private void AutoComplete(XmlTextWriter.Token token)
		{
			if (this.currentState == XmlTextWriter.State.Closed)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed."));
			}
			if (this.currentState == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[8]
				}));
			}
			XmlTextWriter.State state = this.stateTable[(int)(token * XmlTextWriter.Token.EndAttribute + (int)this.currentState)];
			if (state == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[(int)this.currentState]
				}));
			}
			switch (token)
			{
			case XmlTextWriter.Token.PI:
			case XmlTextWriter.Token.Comment:
			case XmlTextWriter.Token.CData:
			case XmlTextWriter.Token.StartElement:
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.WriteEndStartTag(false);
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.WriteEndStartTag(false);
				}
				if (token == XmlTextWriter.Token.CData)
				{
					this.stack[this.top].mixed = true;
				}
				else if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.Doctype:
				if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.EndElement:
			case XmlTextWriter.Token.LongEndElement:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
				}
				if (this.currentState == XmlTextWriter.State.Content)
				{
					token = XmlTextWriter.Token.LongEndElement;
				}
				else
				{
					this.WriteEndStartTag(token == XmlTextWriter.Token.EndElement);
				}
				if (XmlTextWriter.stateTableDocument == this.stateTable && this.top == 1)
				{
					state = XmlTextWriter.State.Epilog;
				}
				break;
			case XmlTextWriter.Token.StartAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.textWriter.Write(' ');
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.textWriter.Write(' ');
				}
				break;
			case XmlTextWriter.Token.EndAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				this.WriteEndAttributeQuote();
				break;
			case XmlTextWriter.Token.Content:
			case XmlTextWriter.Token.Base64:
			case XmlTextWriter.Token.RawData:
			case XmlTextWriter.Token.Whitespace:
				if (token != XmlTextWriter.Token.Base64 && this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Element && this.lastToken != XmlTextWriter.Token.Content)
				{
					this.WriteEndStartTag(false);
				}
				if (state == XmlTextWriter.State.Content)
				{
					this.stack[this.top].mixed = true;
				}
				break;
			default:
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			this.currentState = state;
			this.lastToken = token;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000264E7 File Offset: 0x000246E7
		private void AutoCompleteAll()
		{
			if (this.flush)
			{
				this.FlushEncoders();
			}
			while (this.top > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00026508 File Offset: 0x00024708
		private void InternalWriteEndElement(bool longFormat)
		{
			try
			{
				if (this.top <= 0)
				{
					throw new InvalidOperationException(Res.GetString("There was no XML start tag open."));
				}
				this.AutoComplete(longFormat ? XmlTextWriter.Token.LongEndElement : XmlTextWriter.Token.EndElement);
				if (this.lastToken == XmlTextWriter.Token.LongEndElement)
				{
					if (this.indented)
					{
						this.Indent(true);
					}
					this.textWriter.Write('<');
					this.textWriter.Write('/');
					if (this.namespaces && this.stack[this.top].prefix != null)
					{
						this.textWriter.Write(this.stack[this.top].prefix);
						this.textWriter.Write(':');
					}
					this.textWriter.Write(this.stack[this.top].name);
					this.textWriter.Write('>');
				}
				int prevNsTop = this.stack[this.top].prevNsTop;
				if (this.useNsHashtable && prevNsTop < this.nsTop)
				{
					this.PopNamespaces(prevNsTop + 1, this.nsTop);
				}
				this.nsTop = prevNsTop;
				this.top--;
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00026660 File Offset: 0x00024860
		private void WriteEndStartTag(bool empty)
		{
			this.xmlEncoder.StartAttribute(false);
			for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
			{
				if (!this.nsStack[i].declared)
				{
					this.textWriter.Write(" xmlns");
					this.textWriter.Write(':');
					this.textWriter.Write(this.nsStack[i].prefix);
					this.textWriter.Write('=');
					this.textWriter.Write(this.quoteChar);
					this.xmlEncoder.Write(this.nsStack[i].ns);
					this.textWriter.Write(this.quoteChar);
				}
			}
			if (this.stack[this.top].defaultNs != this.stack[this.top - 1].defaultNs && this.stack[this.top].defaultNsState == XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
			{
				this.textWriter.Write(" xmlns");
				this.textWriter.Write('=');
				this.textWriter.Write(this.quoteChar);
				this.xmlEncoder.Write(this.stack[this.top].defaultNs);
				this.textWriter.Write(this.quoteChar);
				this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.DeclaredAndWrittenOut;
			}
			this.xmlEncoder.EndAttribute();
			if (empty)
			{
				this.textWriter.Write(" /");
			}
			this.textWriter.Write('>');
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00026832 File Offset: 0x00024A32
		private void WriteEndAttributeQuote()
		{
			if (this.specialAttr != XmlTextWriter.SpecialAttr.None)
			{
				this.HandleSpecialAttribute();
			}
			this.xmlEncoder.EndAttribute();
			this.textWriter.Write(this.curQuoteChar);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00026860 File Offset: 0x00024A60
		private void Indent(bool beforeEndElement)
		{
			if (this.top == 0)
			{
				this.textWriter.WriteLine();
				return;
			}
			if (!this.stack[this.top].mixed)
			{
				this.textWriter.WriteLine();
				int i = (beforeEndElement ? (this.top - 1) : this.top);
				for (i *= this.indentation; i > 0; i--)
				{
					this.textWriter.Write(this.indentChar);
				}
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000268DC File Offset: 0x00024ADC
		private void PushNamespace(string prefix, string ns, bool declared)
		{
			if ("http://www.w3.org/2000/xmlns/" == ns)
			{
				throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
			}
			if (prefix == null)
			{
				XmlTextWriter.NamespaceState defaultNsState = this.stack[this.top].defaultNsState;
				if (defaultNsState > XmlTextWriter.NamespaceState.NotDeclaredButInScope)
				{
					if (defaultNsState != XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
					{
						return;
					}
				}
				else
				{
					this.stack[this.top].defaultNs = ns;
				}
				this.stack[this.top].defaultNsState = (declared ? XmlTextWriter.NamespaceState.DeclaredAndWrittenOut : XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut);
				return;
			}
			if (prefix.Length != 0 && ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
			}
			int num = this.LookupNamespace(prefix);
			if (num != -1 && this.nsStack[num].ns == ns)
			{
				if (declared)
				{
					this.nsStack[num].declared = true;
					return;
				}
			}
			else
			{
				if (declared && num != -1 && num > this.stack[this.top].prevNsTop)
				{
					this.nsStack[num].declared = true;
				}
				this.AddNamespace(prefix, ns, declared);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000269F4 File Offset: 0x00024BF4
		private void AddNamespace(string prefix, string ns, bool declared)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlTextWriter.Namespace[] array = new XmlTextWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, array, num2);
				this.nsStack = array;
			}
			this.nsStack[num2].Set(prefix, ns, declared);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(num2);
				return;
			}
			if (num2 == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(new SecureStringHasher());
				for (int i = 0; i <= num2; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00026A90 File Offset: 0x00024C90
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int num;
			if (this.nsHashtable.TryGetValue(prefix, out num))
			{
				this.nsStack[namespaceIndex].prevNsIndex = num;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00026AE0 File Offset: 0x00024CE0
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00026B5C File Offset: 0x00024D5C
		private string GeneratePrefix()
		{
			XmlTextWriter.TagInfo[] array = this.stack;
			int num = this.top;
			int prefixCount = array[num].prefixCount;
			array[num].prefixCount = prefixCount + 1;
			int num2 = prefixCount + 1;
			return "d" + this.top.ToString("d", CultureInfo.InvariantCulture) + "p" + num2.ToString("d", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00026BC0 File Offset: 0x00024DC0
		private void InternalWriteProcessingInstruction(string name, string text)
		{
			this.textWriter.Write("<?");
			this.ValidateName(name, false);
			this.textWriter.Write(name);
			this.textWriter.Write(' ');
			if (text != null)
			{
				this.xmlEncoder.WriteRawWithSurrogateChecking(text);
			}
			this.textWriter.Write("?>");
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00026C20 File Offset: 0x00024E20
		private int LookupNamespace(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num))
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00026C74 File Offset: 0x00024E74
		private int LookupNamespaceInCurrentScope(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num) && num > this.stack[this.top].prevNsTop)
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00026CF8 File Offset: 0x00024EF8
		private string FindPrefix(string ns)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].ns == ns && this.LookupNamespace(this.nsStack[i].prefix) == i)
				{
					return this.nsStack[i].prefix;
				}
			}
			return null;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00026D5C File Offset: 0x00024F5C
		private void InternalWriteName(string name, bool isNCName)
		{
			this.ValidateName(name, isNCName);
			this.textWriter.Write(name);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00026D74 File Offset: 0x00024F74
		private void ValidateName(string name, bool isNCName)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			int length = name.Length;
			if (this.namespaces)
			{
				int num = -1;
				for (int num2 = ValidateNames.ParseNCName(name); num2 != length; num2 += ValidateNames.ParseNmtoken(name, num2))
				{
					if (name[num2] != ':' || isNCName || num != -1 || num2 <= 0 || num2 + 1 >= length)
					{
						goto IL_006F;
					}
					num = num2;
					num2++;
				}
				return;
			}
			if (ValidateNames.IsNameNoNamespaces(name))
			{
				return;
			}
			IL_006F:
			throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00026E0C File Offset: 0x0002500C
		private void HandleSpecialAttribute()
		{
			string text = this.xmlEncoder.AttributeValue;
			switch (this.specialAttr)
			{
			case XmlTextWriter.SpecialAttr.XmlSpace:
				text = XmlConvert.TrimString(text);
				if (text == "default")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Default;
					return;
				}
				if (text == "preserve")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Preserve;
					return;
				}
				throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { text }));
			case XmlTextWriter.SpecialAttr.XmlLang:
				this.stack[this.top].xmlLang = text;
				return;
			case XmlTextWriter.SpecialAttr.XmlNs:
				this.VerifyPrefixXml(this.prefixForXmlNs, text);
				this.PushNamespace(this.prefixForXmlNs, text, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00026EE0 File Offset: 0x000250E0
		private void VerifyPrefixXml(string prefix, string ns)
		{
			if (prefix != null && prefix.Length == 3 && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L') && "http://www.w3.org/XML/1998/namespace" != ns)
			{
				throw new ArgumentException(Res.GetString("Prefixes beginning with \"xml\" (regardless of whether the characters are uppercase, lowercase, or some combination thereof) are reserved for use by XML."));
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00026F58 File Offset: 0x00025158
		private void PushStack()
		{
			if (this.top == this.stack.Length - 1)
			{
				XmlTextWriter.TagInfo[] array = new XmlTextWriter.TagInfo[this.stack.Length + 10];
				if (this.top > 0)
				{
					Array.Copy(this.stack, array, this.top + 1);
				}
				this.stack = array;
			}
			this.top++;
			this.stack[this.top].Init(this.nsTop);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00026FD6 File Offset: 0x000251D6
		private void FlushEncoders()
		{
			if (this.base64Encoder != null)
			{
				this.base64Encoder.Flush();
			}
			this.flush = false;
		}

		// Token: 0x0400036D RID: 877
		private TextWriter textWriter;

		// Token: 0x0400036E RID: 878
		private XmlTextEncoder xmlEncoder;

		// Token: 0x0400036F RID: 879
		private Encoding encoding;

		// Token: 0x04000370 RID: 880
		private Formatting formatting;

		// Token: 0x04000371 RID: 881
		private bool indented;

		// Token: 0x04000372 RID: 882
		private int indentation;

		// Token: 0x04000373 RID: 883
		private char indentChar;

		// Token: 0x04000374 RID: 884
		private XmlTextWriter.TagInfo[] stack;

		// Token: 0x04000375 RID: 885
		private int top;

		// Token: 0x04000376 RID: 886
		private XmlTextWriter.State[] stateTable;

		// Token: 0x04000377 RID: 887
		private XmlTextWriter.State currentState;

		// Token: 0x04000378 RID: 888
		private XmlTextWriter.Token lastToken;

		// Token: 0x04000379 RID: 889
		private XmlTextWriterBase64Encoder base64Encoder;

		// Token: 0x0400037A RID: 890
		private char quoteChar;

		// Token: 0x0400037B RID: 891
		private char curQuoteChar;

		// Token: 0x0400037C RID: 892
		private bool namespaces;

		// Token: 0x0400037D RID: 893
		private XmlTextWriter.SpecialAttr specialAttr;

		// Token: 0x0400037E RID: 894
		private string prefixForXmlNs;

		// Token: 0x0400037F RID: 895
		private bool flush;

		// Token: 0x04000380 RID: 896
		private XmlTextWriter.Namespace[] nsStack;

		// Token: 0x04000381 RID: 897
		private int nsTop;

		// Token: 0x04000382 RID: 898
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000383 RID: 899
		private bool useNsHashtable;

		// Token: 0x04000384 RID: 900
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000385 RID: 901
		private static string[] stateName = new string[] { "Start", "Prolog", "PostDTD", "Element", "Attribute", "Content", "AttrOnly", "Epilog", "Error", "Closed" };

		// Token: 0x04000386 RID: 902
		private static string[] tokenName = new string[]
		{
			"PI", "Doctype", "Comment", "CData", "StartElement", "EndElement", "LongEndElement", "StartAttribute", "EndAttribute", "Content",
			"Base64", "RawData", "Whitespace", "Empty"
		};

		// Token: 0x04000387 RID: 903
		private static readonly XmlTextWriter.State[] stateTableDefault = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.AttrOnly,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x04000388 RID: 904
		private static readonly XmlTextWriter.State[] stateTableDocument = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x02000081 RID: 129
		private enum NamespaceState
		{
			// Token: 0x0400038A RID: 906
			Uninitialized,
			// Token: 0x0400038B RID: 907
			NotDeclaredButInScope,
			// Token: 0x0400038C RID: 908
			DeclaredButNotWrittenOut,
			// Token: 0x0400038D RID: 909
			DeclaredAndWrittenOut
		}

		// Token: 0x02000082 RID: 130
		private struct TagInfo
		{
			// Token: 0x06000745 RID: 1861 RVA: 0x0002710D File Offset: 0x0002530D
			internal void Init(int nsTop)
			{
				this.name = null;
				this.defaultNs = string.Empty;
				this.defaultNsState = XmlTextWriter.NamespaceState.Uninitialized;
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = null;
				this.prevNsTop = nsTop;
				this.prefixCount = 0;
				this.mixed = false;
			}

			// Token: 0x0400038E RID: 910
			internal string name;

			// Token: 0x0400038F RID: 911
			internal string prefix;

			// Token: 0x04000390 RID: 912
			internal string defaultNs;

			// Token: 0x04000391 RID: 913
			internal XmlTextWriter.NamespaceState defaultNsState;

			// Token: 0x04000392 RID: 914
			internal XmlSpace xmlSpace;

			// Token: 0x04000393 RID: 915
			internal string xmlLang;

			// Token: 0x04000394 RID: 916
			internal int prevNsTop;

			// Token: 0x04000395 RID: 917
			internal int prefixCount;

			// Token: 0x04000396 RID: 918
			internal bool mixed;
		}

		// Token: 0x02000083 RID: 131
		private struct Namespace
		{
			// Token: 0x06000746 RID: 1862 RVA: 0x0002714B File Offset: 0x0002534B
			internal void Set(string prefix, string ns, bool declared)
			{
				this.prefix = prefix;
				this.ns = ns;
				this.declared = declared;
				this.prevNsIndex = -1;
			}

			// Token: 0x04000397 RID: 919
			internal string prefix;

			// Token: 0x04000398 RID: 920
			internal string ns;

			// Token: 0x04000399 RID: 921
			internal bool declared;

			// Token: 0x0400039A RID: 922
			internal int prevNsIndex;
		}

		// Token: 0x02000084 RID: 132
		private enum SpecialAttr
		{
			// Token: 0x0400039C RID: 924
			None,
			// Token: 0x0400039D RID: 925
			XmlSpace,
			// Token: 0x0400039E RID: 926
			XmlLang,
			// Token: 0x0400039F RID: 927
			XmlNs
		}

		// Token: 0x02000085 RID: 133
		private enum State
		{
			// Token: 0x040003A1 RID: 929
			Start,
			// Token: 0x040003A2 RID: 930
			Prolog,
			// Token: 0x040003A3 RID: 931
			PostDTD,
			// Token: 0x040003A4 RID: 932
			Element,
			// Token: 0x040003A5 RID: 933
			Attribute,
			// Token: 0x040003A6 RID: 934
			Content,
			// Token: 0x040003A7 RID: 935
			AttrOnly,
			// Token: 0x040003A8 RID: 936
			Epilog,
			// Token: 0x040003A9 RID: 937
			Error,
			// Token: 0x040003AA RID: 938
			Closed
		}

		// Token: 0x02000086 RID: 134
		private enum Token
		{
			// Token: 0x040003AC RID: 940
			PI,
			// Token: 0x040003AD RID: 941
			Doctype,
			// Token: 0x040003AE RID: 942
			Comment,
			// Token: 0x040003AF RID: 943
			CData,
			// Token: 0x040003B0 RID: 944
			StartElement,
			// Token: 0x040003B1 RID: 945
			EndElement,
			// Token: 0x040003B2 RID: 946
			LongEndElement,
			// Token: 0x040003B3 RID: 947
			StartAttribute,
			// Token: 0x040003B4 RID: 948
			EndAttribute,
			// Token: 0x040003B5 RID: 949
			Content,
			// Token: 0x040003B6 RID: 950
			Base64,
			// Token: 0x040003B7 RID: 951
			RawData,
			// Token: 0x040003B8 RID: 952
			Whitespace,
			// Token: 0x040003B9 RID: 953
			Empty
		}
	}
}

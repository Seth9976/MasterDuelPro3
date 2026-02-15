using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200009F RID: 159
	internal class XmlWellFormedWriter : XmlWriter
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x0002D424 File Offset: 0x0002B624
		internal XmlWellFormedWriter(XmlWriter writer, XmlWriterSettings settings)
		{
			this.writer = writer;
			this.rawWriter = writer as XmlRawWriter;
			this.predefinedNamespaces = writer as IXmlNamespaceResolver;
			if (this.rawWriter != null)
			{
				this.rawWriter.NamespaceResolver = new XmlWellFormedWriter.NamespaceResolverProxy(this);
			}
			this.checkCharacters = settings.CheckCharacters;
			this.omitDuplNamespaces = (settings.NamespaceHandling & NamespaceHandling.OmitDuplicates) > NamespaceHandling.Default;
			this.writeEndDocumentOnClose = settings.WriteEndDocumentOnClose;
			this.conformanceLevel = settings.ConformanceLevel;
			this.stateTable = ((this.conformanceLevel == ConformanceLevel.Document) ? XmlWellFormedWriter.StateTableDocument : XmlWellFormedWriter.StateTableAuto);
			this.currentState = XmlWellFormedWriter.State.Start;
			this.nsStack = new XmlWellFormedWriter.Namespace[8];
			this.nsStack[0].Set("xmlns", "http://www.w3.org/2000/xmlns/", XmlWellFormedWriter.NamespaceKind.Special);
			this.nsStack[1].Set("xml", "http://www.w3.org/XML/1998/namespace", XmlWellFormedWriter.NamespaceKind.Special);
			if (this.predefinedNamespaces == null)
			{
				this.nsStack[2].Set(string.Empty, string.Empty, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			else
			{
				string text = this.predefinedNamespaces.LookupNamespace(string.Empty);
				this.nsStack[2].Set(string.Empty, (text == null) ? string.Empty : text, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			this.nsTop = 2;
			this.elemScopeStack = new XmlWellFormedWriter.ElementScope[8];
			this.elemScopeStack[0].Set(string.Empty, string.Empty, string.Empty, this.nsTop);
			this.elemScopeStack[0].xmlSpace = XmlSpace.None;
			this.elemScopeStack[0].xmlLang = null;
			this.elemTop = 0;
			this.attrStack = new XmlWellFormedWriter.AttrName[8];
			this.hasher = new SecureStringHasher();
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0002D5E9 File Offset: 0x0002B7E9
		public override WriteState WriteState
		{
			get
			{
				if (this.currentState <= XmlWellFormedWriter.State.Error)
				{
					return XmlWellFormedWriter.state2WriteState[(int)this.currentState];
				}
				return WriteState.Error;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0002D604 File Offset: 0x0002B804
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.writer.Settings;
				settings.ReadOnly = false;
				settings.ConformanceLevel = this.conformanceLevel;
				if (this.omitDuplNamespaces)
				{
					settings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				settings.WriteEndDocumentOnClose = this.writeEndDocumentOnClose;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002D65A File Offset: 0x0002B85A
		public override void WriteStartDocument()
		{
			this.WriteStartDocumentImpl(XmlStandalone.Omit);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002D663 File Offset: 0x0002B863
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocumentImpl(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002D674 File Offset: 0x0002B874
		public override void WriteEndDocument()
		{
			try
			{
				while (this.elemTop > 0)
				{
					this.WriteEndElement();
				}
				int num = (int)this.currentState;
				this.AdvanceState(XmlWellFormedWriter.Token.EndDocument);
				if (num != 7)
				{
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				if (this.rawWriter == null)
				{
					this.writer.WriteEndDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002D6E4 File Offset: 0x0002B8E4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
				if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("DTD is not allowed in XML fragments."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Dtd);
				if (this.dtdWritten)
				{
					this.currentState = XmlWellFormedWriter.State.Error;
					throw new InvalidOperationException(Res.GetString("The DTD has already been written out."));
				}
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				if (this.checkCharacters)
				{
					int num;
					if (pubid != null && (num = this.xmlCharType.IsPublicId(pubid)) >= 0)
					{
						string text = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] array = XmlException.BuildCharExceptionArgs(pubid, num);
						throw new ArgumentException(Res.GetString(text, array), "pubid");
					}
					if (sysid != null && (num = this.xmlCharType.IsOnlyCharData(sysid)) >= 0)
					{
						string text2 = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] array = XmlException.BuildCharExceptionArgs(sysid, num);
						throw new ArgumentException(Res.GetString(text2, array), "sysid");
					}
					if (subset != null && (num = this.xmlCharType.IsOnlyCharData(subset)) >= 0)
					{
						string text3 = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] array = XmlException.BuildCharExceptionArgs(subset, num);
						throw new ArgumentException(Res.GetString(text3, array), "subset");
					}
				}
				this.writer.WriteDocType(name, pubid, sysid, subset);
				this.dtdWritten = true;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002D854 File Offset: 0x0002BA54
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartElement);
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				this.writer.WriteStartElement(prefix, localName, ns);
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] array = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, array, num2);
					this.elemScopeStack = array;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		public override void WriteEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		public override void WriteFullEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteFullEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteFullEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002DB78 File Offset: 0x0002BD78
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartAttribute);
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_0224;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_0224;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_0224;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_0224;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_0224:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					this.writer.WriteStartAttribute(prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002DDF4 File Offset: 0x0002BFF4
		public override void WriteEndAttribute()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndAttribute);
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					switch (this.specAttr)
					{
					case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
					{
						string text = this.attrValueCache.StringValue;
						if (this.PushNamespaceExplicit(string.Empty, text))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(string.Empty);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(string.Empty, text);
								}
							}
							else
							{
								this.writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
					{
						string text = this.attrValueCache.StringValue;
						if (text.Length == 0)
						{
							throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
						}
						if (text == "http://www.w3.org/2000/xmlns/" || (text == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
						{
							throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
						}
						if (this.PushNamespaceExplicit(this.curDeclPrefix, text))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(this.curDeclPrefix);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(this.curDeclPrefix, text);
								}
							}
							else
							{
								this.writer.WriteStartAttribute("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
					{
						this.attrValueCache.Trim();
						string text = this.attrValueCache.StringValue;
						if (text == "default")
						{
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
						}
						else
						{
							if (!(text == "preserve"))
							{
								throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { text }));
							}
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
						}
						this.writer.WriteStartAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlLang:
					{
						string text = this.attrValueCache.StringValue;
						this.elemScopeStack[this.elemTop].xmlLang = text;
						this.writer.WriteStartAttribute("xml", "lang", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					}
					this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
					this.attrValueCache.Clear();
				}
				else
				{
					this.writer.WriteEndAttribute();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002E150 File Offset: 0x0002C350
		public override void WriteCData(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.CData);
				this.writer.WriteCData(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002E198 File Offset: 0x0002C398
		public override void WriteComment(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Comment);
				this.writer.WriteComment(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				if (text == null)
				{
					text = string.Empty;
				}
				if (name.Length == 3 && string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.currentState != XmlWellFormedWriter.State.Start)
					{
						throw new ArgumentException(Res.GetString((this.conformanceLevel == ConformanceLevel.Document) ? "Cannot write XML declaration. WriteStartDocument method has already written it." : "Cannot write XML declaration. XML declaration can be only at the beginning of the document."));
					}
					this.xmlDeclFollows = true;
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					if (this.rawWriter != null)
					{
						this.rawWriter.WriteXmlDeclaration(text);
					}
					else
					{
						this.writer.WriteProcessingInstruction(name, text);
					}
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					this.writer.WriteProcessingInstruction(name, text);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002E2BC File Offset: 0x0002C4BC
		public override void WriteEntityRef(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					this.writer.WriteEntityRef(name);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002E334 File Offset: 0x0002C534
		public override void WriteCharEntity(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					this.writer.WriteCharEntity(ch);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					this.writer.WriteSurrogateCharEntity(lowChar, highChar);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002E408 File Offset: 0x0002C608
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Whitespace);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					this.writer.WriteWhitespace(ws);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002E488 File Offset: 0x0002C688
		public override void WriteString(string text)
		{
			try
			{
				if (text != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteString(text);
					}
					else
					{
						this.writer.WriteString(text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					this.writer.WriteChars(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002E57C File Offset: 0x0002C77C
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.RawData);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					this.writer.WriteRaw(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002E614 File Offset: 0x0002C814
		public override void WriteRaw(string data)
		{
			try
			{
				if (data != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.RawData);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						this.writer.WriteRaw(data);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002E670 File Offset: 0x0002C870
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Base64);
				this.writer.WriteBase64(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		public override void Close()
		{
			if (this.currentState != XmlWellFormedWriter.State.Closed)
			{
				try
				{
					if (this.writeEndDocumentOnClose)
					{
						while (this.currentState != XmlWellFormedWriter.State.Error)
						{
							if (this.elemTop <= 0)
							{
								break;
							}
							this.WriteEndElement();
						}
					}
					else if (this.currentState != XmlWellFormedWriter.State.Error && this.elemTop > 0)
					{
						try
						{
							this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
						}
						catch
						{
							this.currentState = XmlWellFormedWriter.State.Error;
							throw;
						}
					}
					if (this.InBase64 && this.rawWriter != null)
					{
						this.rawWriter.WriteEndBase64();
					}
					this.writer.Flush();
				}
				finally
				{
					try
					{
						if (this.rawWriter != null)
						{
							this.rawWriter.Close(this.WriteState);
						}
						else
						{
							this.writer.Close();
						}
					}
					finally
					{
						this.currentState = XmlWellFormedWriter.State.Closed;
					}
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		public override void Flush()
		{
			try
			{
				this.writer.Flush();
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002E814 File Offset: 0x0002CA14
		public override string LookupPrefix(string ns)
		{
			string text;
			try
			{
				if (ns == null)
				{
					throw new ArgumentNullException("ns");
				}
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].namespaceUri == ns)
					{
						string prefix = this.nsStack[i].prefix;
						for (i++; i <= this.nsTop; i++)
						{
							if (this.nsStack[i].prefix == prefix)
							{
								return null;
							}
						}
						return prefix;
					}
				}
				text = ((this.predefinedNamespaces != null) ? this.predefinedNamespaces.LookupPrefix(ns) : null);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return text;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		public override XmlSpace XmlSpace
		{
			get
			{
				int num = this.elemTop;
				while (num >= 0 && this.elemScopeStack[num].xmlSpace == (XmlSpace)(-1))
				{
					num--;
				}
				return this.elemScopeStack[num].xmlSpace;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0002E91C File Offset: 0x0002CB1C
		public override string XmlLang
		{
			get
			{
				int num = this.elemTop;
				while (num > 0 && this.elemScopeStack[num].xmlLang == null)
				{
					num--;
				}
				return this.elemScopeStack[num].xmlLang;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002E960 File Offset: 0x0002CB60
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				string text = string.Empty;
				if (ns != null && ns.Length != 0)
				{
					text = this.LookupPrefix(ns);
					if (text == null)
					{
						if (this.currentState != XmlWellFormedWriter.State.Attribute)
						{
							throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
						}
						text = this.GeneratePrefix();
						this.PushNamespaceImplicit(text, ns);
					}
				}
				if (this.SaveAttrValue || this.rawWriter == null)
				{
					if (text.Length != 0)
					{
						this.WriteString(text);
						this.WriteString(":");
					}
					this.WriteString(localName);
				}
				else
				{
					this.rawWriter.WriteQualifiedName(text, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002EA44 File Offset: 0x0002CC44
		public override void WriteValue(bool value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002EA84 File Offset: 0x0002CC84
		public override void WriteValue(DateTime value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		public override void WriteValue(double value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002EB04 File Offset: 0x0002CD04
		public override void WriteValue(float value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002EB44 File Offset: 0x0002CD44
		public override void WriteValue(decimal value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002EB84 File Offset: 0x0002CD84
		public override void WriteValue(int value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public override void WriteValue(long value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002EC04 File Offset: 0x0002CE04
		public override void WriteValue(string value)
		{
			try
			{
				if (value != null)
				{
					if (this.SaveAttrValue)
					{
						this.AdvanceState(XmlWellFormedWriter.Token.Text);
						this.attrValueCache.WriteValue(value);
					}
					else
					{
						this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
						this.writer.WriteValue(value);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002EC68 File Offset: 0x0002CE68
		public override void WriteValue(object value)
		{
			try
			{
				if (this.SaveAttrValue && value is string)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					this.attrValueCache.WriteValue((string)value);
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
					this.writer.WriteValue(value);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			if (this.IsClosedOrErrorState)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				base.WriteBinHex(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002ED28 File Offset: 0x0002CF28
		internal XmlRawWriter RawWriter
		{
			get
			{
				return this.rawWriter;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0002ED30 File Offset: 0x0002CF30
		private bool SaveAttrValue
		{
			get
			{
				return this.specAttr > XmlWellFormedWriter.SpecialAttribute.No;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0002ED3B File Offset: 0x0002CF3B
		private bool InBase64
		{
			get
			{
				return this.currentState == XmlWellFormedWriter.State.B64Content || this.currentState == XmlWellFormedWriter.State.B64Attribute || this.currentState == XmlWellFormedWriter.State.RootLevelB64Attr;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		private void SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute special)
		{
			this.specAttr = special;
			if (XmlWellFormedWriter.State.Attribute == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.SpecialAttr;
			}
			else if (XmlWellFormedWriter.State.RootLevelAttr == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.RootLevelSpecAttr;
			}
			if (this.attrValueCache == null)
			{
				this.attrValueCache = new XmlWellFormedWriter.AttributeValueCache();
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		private void WriteStartDocumentImpl(XmlStandalone standalone)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.StartDocument);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						this.rawWriter.WriteXmlDeclaration(standalone);
					}
				}
				else
				{
					this.writer.WriteStartDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002EE38 File Offset: 0x0002D038
		private void StartFragment()
		{
			this.conformanceLevel = ConformanceLevel.Fragment;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002EE44 File Offset: 0x0002D044
		private void PushNamespaceImplicit(string prefix, string ns)
		{
			int num = this.LookupNamespaceIndex(prefix);
			XmlWellFormedWriter.NamespaceKind namespaceKind;
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					return;
				}
				else if (this.nsStack[num].kind == XmlWellFormedWriter.NamespaceKind.Special)
				{
					if (!(prefix == "xml"))
					{
						throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
					}
					if (ns != this.nsStack[num].namespaceUri)
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
					namespaceKind = XmlWellFormedWriter.NamespaceKind.Implied;
				}
				else
				{
					namespaceKind = ((this.nsStack[num].namespaceUri == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
			}
			else
			{
				if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
				{
					throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[] { prefix }));
				}
				if (this.predefinedNamespaces != null)
				{
					namespaceKind = ((this.predefinedNamespaces.LookupNamespace(prefix) == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
				else
				{
					namespaceKind = XmlWellFormedWriter.NamespaceKind.NeedToWrite;
				}
			}
			this.AddNamespace(prefix, ns, namespaceKind);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002EFC0 File Offset: 0x0002D1C0
		private bool PushNamespaceExplicit(string prefix, string ns)
		{
			bool flag = true;
			int num = this.LookupNamespaceIndex(prefix);
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					XmlWellFormedWriter.NamespaceKind kind = this.nsStack[num].kind;
					if (kind == XmlWellFormedWriter.NamespaceKind.Written)
					{
						throw XmlWellFormedWriter.DupAttrException((prefix.Length == 0) ? string.Empty : "xmlns", (prefix.Length == 0) ? "xmlns" : prefix);
					}
					if (this.omitDuplNamespaces && kind != XmlWellFormedWriter.NamespaceKind.NeedToWrite)
					{
						flag = false;
					}
					this.nsStack[num].kind = XmlWellFormedWriter.NamespaceKind.Written;
					return flag;
				}
				else if (this.nsStack[num].namespaceUri == ns && this.omitDuplNamespaces)
				{
					flag = false;
				}
			}
			else if (this.predefinedNamespaces != null && this.predefinedNamespaces.LookupNamespace(prefix) == ns && this.omitDuplNamespaces)
			{
				flag = false;
			}
			if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
			{
				throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[] { prefix }));
			}
			if (prefix.Length > 0 && prefix[0] == 'x')
			{
				if (prefix == "xml")
				{
					if (ns != "http://www.w3.org/XML/1998/namespace")
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
				}
				else if (prefix == "xmlns")
				{
					throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
				}
			}
			this.AddNamespace(prefix, ns, XmlWellFormedWriter.NamespaceKind.Written);
			return flag;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002F1A0 File Offset: 0x0002D3A0
		private void AddNamespace(string prefix, string ns, XmlWellFormedWriter.NamespaceKind kind)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlWellFormedWriter.Namespace[] array = new XmlWellFormedWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, array, num2);
				this.nsStack = array;
			}
			this.nsStack[num2].Set(prefix, ns, kind);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(this.nsTop);
				return;
			}
			if (this.nsTop == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(this.hasher);
				for (int i = 0; i <= this.nsTop; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002F24C File Offset: 0x0002D44C
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

		// Token: 0x06000880 RID: 2176 RVA: 0x0002F29C File Offset: 0x0002D49C
		private int LookupNamespaceIndex(string prefix)
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

		// Token: 0x06000881 RID: 2177 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
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

		// Token: 0x06000882 RID: 2178 RVA: 0x0002F36C File Offset: 0x0002D56C
		private static XmlException DupAttrException(string prefix, string localName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix.Length > 0)
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append(':');
			}
			stringBuilder.Append(localName);
			return new XmlException("'{0}' is a duplicate attribute name.", stringBuilder.ToString());
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002F3B4 File Offset: 0x0002D5B4
		private void AdvanceState(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_00E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_00F0;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_00FD;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_010A;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_0117;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_0124;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_0137;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_014A;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_015D;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_01C8;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_01D2;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_01DC;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_01E6;
						}
						break;
					}
					goto IL_00D1;
				}
				goto IL_01EF;
				IL_00D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_01EF;
				IL_00E3:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_00F0:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_00FD:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_01EF;
				IL_010A:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Document;
				goto IL_01EF;
				IL_0117:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_0124:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_0137:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_014A:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_015D:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.Attribute;
				goto IL_01EF;
				IL_01C8:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_01D2:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_01DC:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_01EF;
				IL_01E6:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.RootLevelAttr;
				IL_01EF:
				this.currentState = state;
				return;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
		private void StartElementContent()
		{
			int prevNSTop = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > prevNSTop; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					this.nsStack[i].WriteDecl(this.writer, this.rawWriter);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002F631 File Offset: 0x0002D831
		private static string GetStateName(XmlWellFormedWriter.State state)
		{
			if (state >= XmlWellFormedWriter.State.Error)
			{
				return "Error";
			}
			return XmlWellFormedWriter.stateName[(int)state];
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002F648 File Offset: 0x0002D848
		internal string LookupNamespace(string prefix)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			if (this.predefinedNamespaces == null)
			{
				return null;
			}
			return this.predefinedNamespaces.LookupNamespace(prefix);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002F6A8 File Offset: 0x0002D8A8
		private string LookupLocalNamespace(string prefix)
		{
			for (int i = this.nsTop; i > this.elemScopeStack[this.elemTop].prevNSTop; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			return null;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002F708 File Offset: 0x0002D908
		private string GeneratePrefix()
		{
			string text = "p" + (this.nsTop - 2).ToString("d", CultureInfo.InvariantCulture);
			if (this.LookupNamespace(text) == null)
			{
				return text;
			}
			int num = 0;
			string text2;
			do
			{
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			while (this.LookupNamespace(text2) != null);
			return text2;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002F768 File Offset: 0x0002D968
		private void CheckNCName(string ncname)
		{
			int length = ncname.Length;
			if ((this.xmlCharType.charProperties[(int)ncname[0]] & 4) != 0)
			{
				for (int i = 1; i < length; i++)
				{
					if ((this.xmlCharType.charProperties[(int)ncname[i]] & 8) == 0)
					{
						throw XmlWellFormedWriter.InvalidCharsException(ncname, i);
					}
				}
				return;
			}
			throw XmlWellFormedWriter.InvalidCharsException(ncname, 0);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002F7C8 File Offset: 0x0002D9C8
		private static Exception InvalidCharsException(string name, int badCharIndex)
		{
			string[] array = XmlException.BuildCharExceptionArgs(name, badCharIndex);
			string[] array2 = new string[]
			{
				name,
				array[0],
				array[1]
			};
			string text = "Invalid name character in '{0}'. The '{1}' character, hexadecimal value {2}, cannot be included in a name.";
			object[] array3 = array2;
			return new ArgumentException(Res.GetString(text, array3));
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002F808 File Offset: 0x0002DA08
		private void ThrowInvalidStateTransition(XmlWellFormedWriter.Token token, XmlWellFormedWriter.State currentState)
		{
			string @string = Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(currentState)
			});
			if ((currentState == XmlWellFormedWriter.State.Start || currentState == XmlWellFormedWriter.State.AfterRootEle) && this.conformanceLevel == ConformanceLevel.Document)
			{
				throw new InvalidOperationException(@string + " " + Res.GetString("Make sure that the ConformanceLevel setting is set to ConformanceLevel.Fragment or ConformanceLevel.Auto if you want to write an XML fragment."));
			}
			throw new InvalidOperationException(@string);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002F86A File Offset: 0x0002DA6A
		private bool IsClosedOrErrorState
		{
			get
			{
				return this.currentState >= XmlWellFormedWriter.State.Closed;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002F87C File Offset: 0x0002DA7C
		private void AddAttribute(string prefix, string localName, string namespaceName)
		{
			int num = this.attrCount;
			this.attrCount = num + 1;
			int num2 = num;
			if (num2 == this.attrStack.Length)
			{
				XmlWellFormedWriter.AttrName[] array = new XmlWellFormedWriter.AttrName[num2 * 2];
				Array.Copy(this.attrStack, array, num2);
				this.attrStack = array;
			}
			this.attrStack[num2].Set(prefix, localName, namespaceName);
			if (this.attrCount < 14)
			{
				for (int i = 0; i < num2; i++)
				{
					if (this.attrStack[i].IsDuplicate(prefix, localName, namespaceName))
					{
						throw XmlWellFormedWriter.DupAttrException(prefix, localName);
					}
				}
				return;
			}
			if (this.attrCount == 14)
			{
				if (this.attrHashTable == null)
				{
					this.attrHashTable = new Dictionary<string, int>(this.hasher);
				}
				for (int j = 0; j < num2; j++)
				{
					this.AddToAttrHashTable(j);
				}
			}
			this.AddToAttrHashTable(num2);
			for (int k = this.attrStack[num2].prev; k > 0; k = this.attrStack[k].prev)
			{
				k--;
				if (this.attrStack[k].IsDuplicate(prefix, localName, namespaceName))
				{
					throw XmlWellFormedWriter.DupAttrException(prefix, localName);
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
		private void AddToAttrHashTable(int attributeIndex)
		{
			string localName = this.attrStack[attributeIndex].localName;
			int count = this.attrHashTable.Count;
			this.attrHashTable[localName] = 0;
			if (count != this.attrHashTable.Count)
			{
				return;
			}
			int num = attributeIndex - 1;
			while (num >= 0 && !(this.attrStack[num].localName == localName))
			{
				num--;
			}
			this.attrStack[attributeIndex].prev = num + 1;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002FA24 File Offset: 0x0002DC24
		public override Task WriteStartDocumentAsync()
		{
			return this.WriteStartDocumentImplAsync(XmlStandalone.Omit);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002FA2D File Offset: 0x0002DC2D
		private Task TryReturnTask(Task task)
		{
			if (task.IsSuccess())
			{
				return AsyncHelper.DoneTask;
			}
			return this._TryReturnTask(task);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002FA44 File Offset: 0x0002DC44
		private async Task _TryReturnTask(Task task)
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002FA8F File Offset: 0x0002DC8F
		private Task SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			if (task.IsSuccess())
			{
				return this.TryReturnTask(nextTaskFun());
			}
			return this._SequenceRun(task, nextTaskFun);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
		private async Task _SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			try
			{
				await task.ConfigureAwait(false);
				await nextTaskFun().ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002FB04 File Offset: 0x0002DD04
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string namespaceName)
		{
			Task task2;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartAttribute);
				if (task.IsSuccess())
				{
					task2 = this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName);
				}
				else
				{
					task2 = this.WriteStartAttributeAsync_NoAdvanceState(task, prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002FB98 File Offset: 0x0002DD98
		private Task WriteStartAttributeAsync_NoAdvanceState(string prefix, string localName, string namespaceName)
		{
			Task task;
			try
			{
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_01DE;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_01DE;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_01DE;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_01DE;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_01DE:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					task = this.TryReturnTask(this.writer.WriteStartAttributeAsync(prefix, localName, namespaceName));
				}
				else
				{
					task = AsyncHelper.DoneTask;
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		private async Task WriteStartAttributeAsync_NoAdvanceState(Task task, string prefix, string localName, string namespaceName)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002FE40 File Offset: 0x0002E040
		protected internal override Task WriteEndAttributeAsync()
		{
			Task task2;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndAttribute);
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteEndAttributeAsync_NoAdvance));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002FE88 File Offset: 0x0002E088
		private Task WriteEndAttributeAsync_NoAdvance()
		{
			Task task;
			try
			{
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					task = this.WriteEndAttributeAsync_SepcialAtt();
				}
				else
				{
					task = this.TryReturnTask(this.writer.WriteEndAttributeAsync());
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002FED8 File Offset: 0x0002E0D8
		private async Task WriteEndAttributeAsync_SepcialAtt()
		{
			try
			{
				switch (this.specAttr)
				{
				case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
				{
					string text = this.attrValueCache.StringValue;
					if (this.PushNamespaceExplicit(string.Empty, text))
					{
						if (this.rawWriter != null)
						{
							if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
							{
								await this.rawWriter.WriteStartNamespaceDeclarationAsync(string.Empty).ConfigureAwait(false);
								await this.attrValueCache.ReplayAsync(this.rawWriter).ConfigureAwait(false);
								await this.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
							}
							else
							{
								await this.rawWriter.WriteNamespaceDeclarationAsync(string.Empty, text).ConfigureAwait(false);
							}
						}
						else
						{
							await this.writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
							await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
							await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
						}
					}
					this.curDeclPrefix = null;
					break;
				}
				case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
				{
					string text = this.attrValueCache.StringValue;
					if (text.Length == 0)
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
					if (text == "http://www.w3.org/2000/xmlns/" || (text == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
					{
						throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
					}
					if (this.PushNamespaceExplicit(this.curDeclPrefix, text))
					{
						if (this.rawWriter != null)
						{
							if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
							{
								await this.rawWriter.WriteStartNamespaceDeclarationAsync(this.curDeclPrefix).ConfigureAwait(false);
								await this.attrValueCache.ReplayAsync(this.rawWriter).ConfigureAwait(false);
								await this.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
							}
							else
							{
								await this.rawWriter.WriteNamespaceDeclarationAsync(this.curDeclPrefix, text).ConfigureAwait(false);
							}
						}
						else
						{
							await this.writer.WriteStartAttributeAsync("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
							await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
							await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
						}
					}
					this.curDeclPrefix = null;
					break;
				}
				case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
				{
					this.attrValueCache.Trim();
					string text = this.attrValueCache.StringValue;
					if (text == "default")
					{
						this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
					}
					else
					{
						if (!(text == "preserve"))
						{
							throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { text }));
						}
						this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
					}
					await this.writer.WriteStartAttributeAsync("xml", "space", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false);
					await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
					await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
					break;
				}
				case XmlWellFormedWriter.SpecialAttribute.XmlLang:
				{
					string text = this.attrValueCache.StringValue;
					this.elemScopeStack[this.elemTop].xmlLang = text;
					await this.writer.WriteStartAttributeAsync("xml", "lang", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false);
					await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
					await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
					break;
				}
				}
				this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
				this.attrValueCache.Clear();
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002FF1C File Offset: 0x0002E11C
		public override async Task WriteEntityRefAsync(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					await this.writer.WriteEntityRefAsync(name).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002FF68 File Offset: 0x0002E168
		public override async Task WriteCharEntityAsync(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					await this.writer.WriteCharEntityAsync(ch).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
		public override async Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					await this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00030008 File Offset: 0x0002E208
		public override async Task WriteWhitespaceAsync(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Whitespace).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					await this.writer.WriteWhitespaceAsync(ws).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00030054 File Offset: 0x0002E254
		public override Task WriteStringAsync(string text)
		{
			Task task;
			try
			{
				if (text == null)
				{
					task = AsyncHelper.DoneTask;
				}
				else
				{
					Task task2 = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text);
					if (task2.IsSuccess())
					{
						task = this.WriteStringAsync_NoAdvanceState(text);
					}
					else
					{
						task = this.WriteStringAsync_NoAdvanceState(task2, text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000300B0 File Offset: 0x0002E2B0
		private Task WriteStringAsync_NoAdvanceState(string text)
		{
			Task task;
			try
			{
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteString(text);
					task = AsyncHelper.DoneTask;
				}
				else
				{
					task = this.TryReturnTask(this.writer.WriteStringAsync(text));
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003010C File Offset: 0x0002E30C
		private async Task WriteStringAsync_NoAdvanceState(Task task, string text)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.WriteStringAsync_NoAdvanceState(text).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00030160 File Offset: 0x0002E360
		public override async Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					await this.writer.WriteCharsAsync(buffer, index, count).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000301BC File Offset: 0x0002E3BC
		public override async Task WriteRawAsync(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					await this.writer.WriteRawAsync(buffer, index, count).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00030218 File Offset: 0x0002E418
		public override async Task WriteRawAsync(string data)
		{
			try
			{
				if (data != null)
				{
					await this.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						await this.writer.WriteRawAsync(data).ConfigureAwait(false);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00030264 File Offset: 0x0002E464
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			Task task2;
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Base64);
				if (task.IsSuccess())
				{
					task2 = this.TryReturnTask(this.writer.WriteBase64Async(buffer, index, count));
				}
				else
				{
					task2 = this.WriteBase64Async_NoAdvanceState(task, buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00030304 File Offset: 0x0002E504
		private async Task WriteBase64Async_NoAdvanceState(Task task, byte[] buffer, int index, int count)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.writer.WriteBase64Async(buffer, index, count).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00030368 File Offset: 0x0002E568
		private async Task WriteStartDocumentImplAsync(XmlStandalone standalone)
		{
			try
			{
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartDocument).ConfigureAwait(false);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						await this.rawWriter.WriteXmlDeclarationAsync(standalone).ConfigureAwait(false);
					}
				}
				else
				{
					await this.writer.WriteStartDocumentAsync().ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000303B3 File Offset: 0x0002E5B3
		private Task AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return AsyncHelper.DoneTask;
			}
			return this._AdvanceStateAsync_ReturnWhenFinish(task, newState);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000303D4 File Offset: 0x0002E5D4
		private async Task _AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			await task.ConfigureAwait(false);
			this.currentState = newState;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00030427 File Offset: 0x0002E627
		private Task AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return this.AdvanceStateAsync(token);
			}
			return this._AdvanceStateAsync_ContinueWhenFinish(task, newState, token);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003044C File Offset: 0x0002E64C
		private async Task _AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			await task.ConfigureAwait(false);
			this.currentState = newState;
			await this.AdvanceStateAsync(token).ConfigureAwait(false);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000304A8 File Offset: 0x0002E6A8
		private Task AdvanceStateAsync(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_00E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_00F1;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_00FF;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_010D;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_011B;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_0129;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_014B;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_016D;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_018F;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								goto Block_6;
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								goto Block_7;
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								goto Block_8;
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_0217;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_0221;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_022B;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_0235;
						}
						break;
					}
					goto IL_00D1;
				}
				goto IL_0244;
				IL_00D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_0244;
				IL_00E3:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Content);
				IL_00F1:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Element);
				IL_00FF:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.B64Content);
				IL_010D:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Document);
				IL_011B:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Element);
				IL_0129:
				Task task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Element);
				IL_014B:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_016D:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_018F:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.Attribute);
				Block_6:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Content, token);
				Block_7:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Attribute, token);
				Block_8:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.RootLevelAttr, token);
				IL_0217:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_0244;
				IL_0221:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_0244;
				IL_022B:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_0244;
				IL_0235:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.RootLevelAttr);
				IL_0244:
				this.currentState = state;
				return AsyncHelper.DoneTask;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00030708 File Offset: 0x0002E908
		private async Task StartElementContentAsync_WithNS()
		{
			int start = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > start; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					await this.nsStack[i].WriteDeclAsync(this.writer, this.rawWriter).ConfigureAwait(false);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003074B File Offset: 0x0002E94B
		private Task StartElementContentAsync()
		{
			if (this.nsTop > this.elemScopeStack[this.elemTop].prevNSTop)
			{
				return this.StartElementContentAsync_WithNS();
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0400046F RID: 1135
		private XmlWriter writer;

		// Token: 0x04000470 RID: 1136
		private XmlRawWriter rawWriter;

		// Token: 0x04000471 RID: 1137
		private IXmlNamespaceResolver predefinedNamespaces;

		// Token: 0x04000472 RID: 1138
		private XmlWellFormedWriter.Namespace[] nsStack;

		// Token: 0x04000473 RID: 1139
		private int nsTop;

		// Token: 0x04000474 RID: 1140
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000475 RID: 1141
		private bool useNsHashtable;

		// Token: 0x04000476 RID: 1142
		private XmlWellFormedWriter.ElementScope[] elemScopeStack;

		// Token: 0x04000477 RID: 1143
		private int elemTop;

		// Token: 0x04000478 RID: 1144
		private XmlWellFormedWriter.AttrName[] attrStack;

		// Token: 0x04000479 RID: 1145
		private int attrCount;

		// Token: 0x0400047A RID: 1146
		private Dictionary<string, int> attrHashTable;

		// Token: 0x0400047B RID: 1147
		private XmlWellFormedWriter.SpecialAttribute specAttr;

		// Token: 0x0400047C RID: 1148
		private XmlWellFormedWriter.AttributeValueCache attrValueCache;

		// Token: 0x0400047D RID: 1149
		private string curDeclPrefix;

		// Token: 0x0400047E RID: 1150
		private XmlWellFormedWriter.State[] stateTable;

		// Token: 0x0400047F RID: 1151
		private XmlWellFormedWriter.State currentState;

		// Token: 0x04000480 RID: 1152
		private bool checkCharacters;

		// Token: 0x04000481 RID: 1153
		private bool omitDuplNamespaces;

		// Token: 0x04000482 RID: 1154
		private bool writeEndDocumentOnClose;

		// Token: 0x04000483 RID: 1155
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000484 RID: 1156
		private bool dtdWritten;

		// Token: 0x04000485 RID: 1157
		private bool xmlDeclFollows;

		// Token: 0x04000486 RID: 1158
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000487 RID: 1159
		private SecureStringHasher hasher;

		// Token: 0x04000488 RID: 1160
		internal static readonly string[] stateName = new string[]
		{
			"Start", "TopLevel", "Document", "Element Start Tag", "Element Content", "Element Content", "Attribute", "EndRootElement", "Attribute", "Special Attribute",
			"End Document", "Root Level Attribute Value", "Root Level Special Attribute Value", "Root Level Base64 Attribute Value", "After Root Level Attribute", "Closed", "Error"
		};

		// Token: 0x04000489 RID: 1161
		internal static readonly string[] tokenName = new string[]
		{
			"StartDocument", "EndDocument", "PI", "Comment", "DTD", "StartElement", "EndElement", "StartAttribute", "EndAttribute", "Text",
			"CDATA", "Atomic value", "Base64", "RawData", "Whitespace"
		};

		// Token: 0x0400048A RID: 1162
		private static WriteState[] state2WriteState = new WriteState[]
		{
			WriteState.Start,
			WriteState.Prolog,
			WriteState.Prolog,
			WriteState.Element,
			WriteState.Content,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Closed,
			WriteState.Error
		};

		// Token: 0x0400048B RID: 1163
		private static readonly XmlWellFormedWriter.State[] StateTableDocument = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDocEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x0400048C RID: 1164
		private static readonly XmlWellFormedWriter.State[] StateTableAuto = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x020000A0 RID: 160
		private enum State
		{
			// Token: 0x0400048E RID: 1166
			Start,
			// Token: 0x0400048F RID: 1167
			TopLevel,
			// Token: 0x04000490 RID: 1168
			Document,
			// Token: 0x04000491 RID: 1169
			Element,
			// Token: 0x04000492 RID: 1170
			Content,
			// Token: 0x04000493 RID: 1171
			B64Content,
			// Token: 0x04000494 RID: 1172
			B64Attribute,
			// Token: 0x04000495 RID: 1173
			AfterRootEle,
			// Token: 0x04000496 RID: 1174
			Attribute,
			// Token: 0x04000497 RID: 1175
			SpecialAttr,
			// Token: 0x04000498 RID: 1176
			EndDocument,
			// Token: 0x04000499 RID: 1177
			RootLevelAttr,
			// Token: 0x0400049A RID: 1178
			RootLevelSpecAttr,
			// Token: 0x0400049B RID: 1179
			RootLevelB64Attr,
			// Token: 0x0400049C RID: 1180
			AfterRootLevelAttr,
			// Token: 0x0400049D RID: 1181
			Closed,
			// Token: 0x0400049E RID: 1182
			Error,
			// Token: 0x0400049F RID: 1183
			StartContent = 101,
			// Token: 0x040004A0 RID: 1184
			StartContentEle,
			// Token: 0x040004A1 RID: 1185
			StartContentB64,
			// Token: 0x040004A2 RID: 1186
			StartDoc,
			// Token: 0x040004A3 RID: 1187
			StartDocEle = 106,
			// Token: 0x040004A4 RID: 1188
			EndAttrSEle,
			// Token: 0x040004A5 RID: 1189
			EndAttrEEle,
			// Token: 0x040004A6 RID: 1190
			EndAttrSCont,
			// Token: 0x040004A7 RID: 1191
			EndAttrSAttr = 111,
			// Token: 0x040004A8 RID: 1192
			PostB64Cont,
			// Token: 0x040004A9 RID: 1193
			PostB64Attr,
			// Token: 0x040004AA RID: 1194
			PostB64RootAttr,
			// Token: 0x040004AB RID: 1195
			StartFragEle,
			// Token: 0x040004AC RID: 1196
			StartFragCont,
			// Token: 0x040004AD RID: 1197
			StartFragB64,
			// Token: 0x040004AE RID: 1198
			StartRootLevelAttr
		}

		// Token: 0x020000A1 RID: 161
		private enum Token
		{
			// Token: 0x040004B0 RID: 1200
			StartDocument,
			// Token: 0x040004B1 RID: 1201
			EndDocument,
			// Token: 0x040004B2 RID: 1202
			PI,
			// Token: 0x040004B3 RID: 1203
			Comment,
			// Token: 0x040004B4 RID: 1204
			Dtd,
			// Token: 0x040004B5 RID: 1205
			StartElement,
			// Token: 0x040004B6 RID: 1206
			EndElement,
			// Token: 0x040004B7 RID: 1207
			StartAttribute,
			// Token: 0x040004B8 RID: 1208
			EndAttribute,
			// Token: 0x040004B9 RID: 1209
			Text,
			// Token: 0x040004BA RID: 1210
			CData,
			// Token: 0x040004BB RID: 1211
			AtomicValue,
			// Token: 0x040004BC RID: 1212
			Base64,
			// Token: 0x040004BD RID: 1213
			RawData,
			// Token: 0x040004BE RID: 1214
			Whitespace
		}

		// Token: 0x020000A2 RID: 162
		private class NamespaceResolverProxy : IXmlNamespaceResolver
		{
			// Token: 0x060008AF RID: 2223 RVA: 0x0003090A File Offset: 0x0002EB0A
			internal NamespaceResolverProxy(XmlWellFormedWriter wfWriter)
			{
				this.wfWriter = wfWriter;
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x00002CE0 File Offset: 0x00000EE0
			IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x00030919 File Offset: 0x0002EB19
			string IXmlNamespaceResolver.LookupNamespace(string prefix)
			{
				return this.wfWriter.LookupNamespace(prefix);
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00030927 File Offset: 0x0002EB27
			string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
			{
				return this.wfWriter.LookupPrefix(namespaceName);
			}

			// Token: 0x040004BF RID: 1215
			private XmlWellFormedWriter wfWriter;
		}

		// Token: 0x020000A3 RID: 163
		private struct ElementScope
		{
			// Token: 0x060008B3 RID: 2227 RVA: 0x00030935 File Offset: 0x0002EB35
			internal void Set(string prefix, string localName, string namespaceUri, int prevNSTop)
			{
				this.prevNSTop = prevNSTop;
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.xmlSpace = (XmlSpace)(-1);
				this.xmlLang = null;
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x00030962 File Offset: 0x0002EB62
			internal void WriteEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x0003097C File Offset: 0x0002EB7C
			internal void WriteFullEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteFullEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x040004C0 RID: 1216
			internal int prevNSTop;

			// Token: 0x040004C1 RID: 1217
			internal string prefix;

			// Token: 0x040004C2 RID: 1218
			internal string localName;

			// Token: 0x040004C3 RID: 1219
			internal string namespaceUri;

			// Token: 0x040004C4 RID: 1220
			internal XmlSpace xmlSpace;

			// Token: 0x040004C5 RID: 1221
			internal string xmlLang;
		}

		// Token: 0x020000A4 RID: 164
		private enum NamespaceKind
		{
			// Token: 0x040004C7 RID: 1223
			Written,
			// Token: 0x040004C8 RID: 1224
			NeedToWrite,
			// Token: 0x040004C9 RID: 1225
			Implied,
			// Token: 0x040004CA RID: 1226
			Special
		}

		// Token: 0x020000A5 RID: 165
		private struct Namespace
		{
			// Token: 0x060008B6 RID: 2230 RVA: 0x00030996 File Offset: 0x0002EB96
			internal void Set(string prefix, string namespaceUri, XmlWellFormedWriter.NamespaceKind kind)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.kind = kind;
				this.prevNsIndex = -1;
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x000309B4 File Offset: 0x0002EBB4
			internal void WriteDecl(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					rawWriter.WriteNamespaceDeclaration(this.prefix, this.namespaceUri);
					return;
				}
				if (this.prefix.Length == 0)
				{
					writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
				}
				else
				{
					writer.WriteStartAttribute("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/");
				}
				writer.WriteString(this.namespaceUri);
				writer.WriteEndAttribute();
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x00030A24 File Offset: 0x0002EC24
			internal async Task WriteDeclAsync(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					await rawWriter.WriteNamespaceDeclarationAsync(this.prefix, this.namespaceUri).ConfigureAwait(false);
				}
				else
				{
					if (this.prefix.Length == 0)
					{
						await writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
					}
					else
					{
						await writer.WriteStartAttributeAsync("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
					}
					await writer.WriteStringAsync(this.namespaceUri).ConfigureAwait(false);
					await writer.WriteEndAttributeAsync().ConfigureAwait(false);
				}
			}

			// Token: 0x040004CB RID: 1227
			internal string prefix;

			// Token: 0x040004CC RID: 1228
			internal string namespaceUri;

			// Token: 0x040004CD RID: 1229
			internal XmlWellFormedWriter.NamespaceKind kind;

			// Token: 0x040004CE RID: 1230
			internal int prevNsIndex;
		}

		// Token: 0x020000A7 RID: 167
		private struct AttrName
		{
			// Token: 0x060008BB RID: 2235 RVA: 0x00030D76 File Offset: 0x0002EF76
			internal void Set(string prefix, string localName, string namespaceUri)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.prev = 0;
			}

			// Token: 0x060008BC RID: 2236 RVA: 0x00030D94 File Offset: 0x0002EF94
			internal bool IsDuplicate(string prefix, string localName, string namespaceUri)
			{
				return this.localName == localName && (this.prefix == prefix || this.namespaceUri == namespaceUri);
			}

			// Token: 0x040004D5 RID: 1237
			internal string prefix;

			// Token: 0x040004D6 RID: 1238
			internal string namespaceUri;

			// Token: 0x040004D7 RID: 1239
			internal string localName;

			// Token: 0x040004D8 RID: 1240
			internal int prev;
		}

		// Token: 0x020000A8 RID: 168
		private enum SpecialAttribute
		{
			// Token: 0x040004DA RID: 1242
			No,
			// Token: 0x040004DB RID: 1243
			DefaultXmlns,
			// Token: 0x040004DC RID: 1244
			PrefixedXmlns,
			// Token: 0x040004DD RID: 1245
			XmlSpace,
			// Token: 0x040004DE RID: 1246
			XmlLang
		}

		// Token: 0x020000A9 RID: 169
		private class AttributeValueCache
		{
			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060008BD RID: 2237 RVA: 0x00030DC2 File Offset: 0x0002EFC2
			internal string StringValue
			{
				get
				{
					if (this.singleStringValue != null)
					{
						return this.singleStringValue;
					}
					return this.stringValue.ToString();
				}
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x00030DE0 File Offset: 0x0002EFE0
			internal void WriteEntityRef(string name)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				if (!(name == "lt"))
				{
					if (!(name == "gt"))
					{
						if (!(name == "quot"))
						{
							if (!(name == "apos"))
							{
								if (!(name == "amp"))
								{
									this.stringValue.Append('&');
									this.stringValue.Append(name);
									this.stringValue.Append(';');
								}
								else
								{
									this.stringValue.Append('&');
								}
							}
							else
							{
								this.stringValue.Append('\'');
							}
						}
						else
						{
							this.stringValue.Append('"');
						}
					}
					else
					{
						this.stringValue.Append('>');
					}
				}
				else
				{
					this.stringValue.Append('<');
				}
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef, name);
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x00030EBF File Offset: 0x0002F0BF
			internal void WriteCharEntity(char ch)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ch);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity, ch);
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x00030EE9 File Offset: 0x0002F0E9
			internal void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(highChar);
				this.stringValue.Append(lowChar);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity, new char[] { lowChar, highChar });
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x00030F28 File Offset: 0x0002F128
			internal void WriteWhitespace(string ws)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ws);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace, ws);
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x00030F4D File Offset: 0x0002F14D
			internal void WriteString(string text)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				else if (this.lastItem == -1)
				{
					this.singleStringValue = text;
					return;
				}
				this.stringValue.Append(text);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, text);
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x00030F85 File Offset: 0x0002F185
			internal void WriteChars(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x00030FB3 File Offset: 0x0002F1B3
			internal void WriteRaw(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x00030FE1 File Offset: 0x0002F1E1
			internal void WriteRaw(string data)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(data);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Raw, data);
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x00031006 File Offset: 0x0002F206
			internal void WriteValue(string value)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(value);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString, value);
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x0003102C File Offset: 0x0002F22C
			internal void Replay(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					writer.WriteString(this.singleStringValue);
					return;
				}
				for (int i = this.firstItem; i <= this.lastItem; i++)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
						writer.WriteEntityRef((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
						writer.WriteCharEntity((char)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
					{
						char[] array = (char[])item.data;
						writer.WriteSurrogateCharEntity(array[0], array[1]);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						writer.WriteWhitespace((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
						writer.WriteString((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
						writer.WriteRaw((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						writer.WriteValue((string)item.data);
						break;
					}
				}
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x00031190 File Offset: 0x0002F390
			internal void Trim()
			{
				if (this.singleStringValue != null)
				{
					this.singleStringValue = XmlConvert.TrimString(this.singleStringValue);
					return;
				}
				string text = this.stringValue.ToString();
				string text2 = XmlConvert.TrimString(text);
				if (text != text2)
				{
					this.stringValue = new StringBuilder(text2);
				}
				XmlCharType instance = XmlCharType.Instance;
				int num = this.firstItem;
				while (num == this.firstItem && num <= this.lastItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[num];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.firstItem++;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item.data = XmlConvert.TrimStringStart((string)item.data);
						if (((string)item.data).Length == 0)
						{
							this.firstItem++;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						int num2 = bufferChunk.index + bufferChunk.count;
						while (bufferChunk.index < num2 && instance.IsWhiteSpace(bufferChunk.buffer[bufferChunk.index]))
						{
							bufferChunk.index++;
							bufferChunk.count--;
						}
						if (bufferChunk.index == num2)
						{
							this.firstItem++;
						}
						break;
					}
					}
					num++;
				}
				num = this.lastItem;
				while (num == this.lastItem && num >= this.firstItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item2 = this.items[num];
					switch (item2.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.lastItem--;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item2.data = XmlConvert.TrimStringEnd((string)item2.data);
						if (((string)item2.data).Length == 0)
						{
							this.lastItem--;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk2 = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item2.data;
						while (bufferChunk2.count > 0 && instance.IsWhiteSpace(bufferChunk2.buffer[bufferChunk2.index + bufferChunk2.count - 1]))
						{
							bufferChunk2.count--;
						}
						if (bufferChunk2.count == 0)
						{
							this.lastItem--;
						}
						break;
					}
					}
					num--;
				}
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x00031415 File Offset: 0x0002F615
			internal void Clear()
			{
				this.singleStringValue = null;
				this.lastItem = -1;
				this.firstItem = 0;
				this.stringValue.Length = 0;
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x00031438 File Offset: 0x0002F638
			private void StartComplexValue()
			{
				this.stringValue.Append(this.singleStringValue);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, this.singleStringValue);
				this.singleStringValue = null;
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00031460 File Offset: 0x0002F660
			private void AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
			{
				int num = this.lastItem + 1;
				if (this.items == null)
				{
					this.items = new XmlWellFormedWriter.AttributeValueCache.Item[4];
				}
				else if (this.items.Length == num)
				{
					XmlWellFormedWriter.AttributeValueCache.Item[] array = new XmlWellFormedWriter.AttributeValueCache.Item[num * 2];
					Array.Copy(this.items, array, num);
					this.items = array;
				}
				if (this.items[num] == null)
				{
					this.items[num] = new XmlWellFormedWriter.AttributeValueCache.Item();
				}
				this.items[num].Set(type, data);
				this.lastItem = num;
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x000314E4 File Offset: 0x0002F6E4
			internal async Task ReplayAsync(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					await writer.WriteStringAsync(this.singleStringValue).ConfigureAwait(false);
				}
				else
				{
					for (int i = this.firstItem; i <= this.lastItem; i++)
					{
						XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
						switch (item.type)
						{
						case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
							await writer.WriteEntityRefAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
							await writer.WriteCharEntityAsync((char)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
						{
							char[] array = (char[])item.data;
							await writer.WriteSurrogateCharEntityAsync(array[0], array[1]).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
							await writer.WriteWhitespaceAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
							await writer.WriteStringAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
						{
							XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
							await writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
							await writer.WriteRawAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
						{
							XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
							await writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
							await writer.WriteStringAsync((string)item.data).ConfigureAwait(false);
							break;
						}
					}
				}
			}

			// Token: 0x040004DF RID: 1247
			private StringBuilder stringValue = new StringBuilder();

			// Token: 0x040004E0 RID: 1248
			private string singleStringValue;

			// Token: 0x040004E1 RID: 1249
			private XmlWellFormedWriter.AttributeValueCache.Item[] items;

			// Token: 0x040004E2 RID: 1250
			private int firstItem;

			// Token: 0x040004E3 RID: 1251
			private int lastItem = -1;

			// Token: 0x020000AA RID: 170
			private enum ItemType
			{
				// Token: 0x040004E5 RID: 1253
				EntityRef,
				// Token: 0x040004E6 RID: 1254
				CharEntity,
				// Token: 0x040004E7 RID: 1255
				SurrogateCharEntity,
				// Token: 0x040004E8 RID: 1256
				Whitespace,
				// Token: 0x040004E9 RID: 1257
				String,
				// Token: 0x040004EA RID: 1258
				StringChars,
				// Token: 0x040004EB RID: 1259
				Raw,
				// Token: 0x040004EC RID: 1260
				RawChars,
				// Token: 0x040004ED RID: 1261
				ValueString
			}

			// Token: 0x020000AB RID: 171
			private class Item
			{
				// Token: 0x060008CE RID: 2254 RVA: 0x00002127 File Offset: 0x00000327
				internal Item()
				{
				}

				// Token: 0x060008CF RID: 2255 RVA: 0x00031549 File Offset: 0x0002F749
				internal void Set(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
				{
					this.type = type;
					this.data = data;
				}

				// Token: 0x040004EE RID: 1262
				internal XmlWellFormedWriter.AttributeValueCache.ItemType type;

				// Token: 0x040004EF RID: 1263
				internal object data;
			}

			// Token: 0x020000AC RID: 172
			private class BufferChunk
			{
				// Token: 0x060008D0 RID: 2256 RVA: 0x00031559 File Offset: 0x0002F759
				internal BufferChunk(char[] buffer, int index, int count)
				{
					this.buffer = buffer;
					this.index = index;
					this.count = count;
				}

				// Token: 0x040004F0 RID: 1264
				internal char[] buffer;

				// Token: 0x040004F1 RID: 1265
				internal int index;

				// Token: 0x040004F2 RID: 1266
				internal int count;
			}
		}
	}
}

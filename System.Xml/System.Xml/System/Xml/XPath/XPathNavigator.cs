using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a cursor model for navigating and editing XML data.</summary>
	// Token: 0x0200013D RID: 317
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XPathNavigator : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
	{
		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>A string that contains the text value of the current node.</returns>
		// Token: 0x06000F7E RID: 3966 RVA: 0x0004CFE3 File Offset: 0x0004B1E3
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> information for the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object; default is null.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0004CFEC File Offset: 0x0004B1EC
		public override XmlSchemaType XmlType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo == null || schemaInfo.Validity != XmlSchemaValidity.Valid)
				{
					return null;
				}
				XmlSchemaType memberType = schemaInfo.MemberType;
				if (memberType != null)
				{
					return memberType;
				}
				return schemaInfo.SchemaType;
			}
		}

		/// <summary>Gets the current node as a boxed object of the most appropriate .NET Framework type.</summary>
		/// <returns>The current node as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004D020 File Offset: 0x0004B220
		public override object TypedValue
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(this.Value, xmlSchemaDatatype.ValueType, this);
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(xmlSchemaDatatype.ParseValue(this.Value, this.NameTable, this), xmlSchemaDatatype.ValueType, this);
							}
						}
					}
				}
				return this.Value;
			}
		}

		/// <summary>Gets the .NET Framework <see cref="T:System.Type" /> of the current node.</summary>
		/// <returns>The .NET Framework <see cref="T:System.Type" /> of the current node. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0004D0B8 File Offset: 0x0004B2B8
		public override Type ValueType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaDatatype.ValueType;
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaType.Datatype;
							if (xmlSchemaDatatype != null)
							{
								return xmlSchemaDatatype.ValueType;
							}
						}
					}
				}
				return typeof(string);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Boolean" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0004D124 File Offset: 0x0004B324
		public override bool ValueAsBoolean
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToBoolean(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToBoolean(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToBoolean(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0004D1B0 File Offset: 0x0004B3B0
		public override DateTime ValueAsDateTime
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDateTime(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDateTime(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDateTime(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Double" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0004D23C File Offset: 0x0004B43C
		public override double ValueAsDouble
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDouble(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDouble(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDouble(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int32" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		public override int ValueAsInt
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt32(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt32(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt32(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int64" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0004D354 File Offset: 0x0004B554
		public override long ValueAsLong
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt64(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt64(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt64(this.Value);
			}
		}

		/// <summary>Gets the current node's value as the <see cref="T:System.Type" /> specified, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the current node as the <see cref="T:System.Type" /> requested.</returns>
		/// <param name="returnType">The <see cref="T:System.Type" /> to return the current node's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The current node's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		// Token: 0x06000F87 RID: 3975 RVA: 0x0004D3E0 File Offset: 0x0004B5E0
		public override object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				nsResolver = this;
			}
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				if (schemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
					if (xmlSchemaType == null)
					{
						xmlSchemaType = schemaInfo.SchemaType;
					}
					if (xmlSchemaType != null)
					{
						return xmlSchemaType.ValueConverter.ChangeType(this.Value, returnType, nsResolver);
					}
				}
				else
				{
					XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
					if (xmlSchemaType != null)
					{
						XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
						if (datatype != null)
						{
							return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, nsResolver), returnType, nsResolver);
						}
					}
				}
			}
			return XmlUntypedConverter.Untyped.ChangeType(this.Value, returnType, nsResolver);
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</summary>
		/// <returns>A new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06000F88 RID: 3976 RVA: 0x0004D475 File Offset: 0x0004B675
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Returns a copy of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> copy of this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06000F89 RID: 3977 RVA: 0x0004D475 File Offset: 0x0004B675
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNameTable" /> object enabling you to get the atomized version of a <see cref="T:System.String" /> within the XML document.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000F8A RID: 3978
		public abstract XmlNameTable NameTable { get; }

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI assigned to the namespace prefix specified; null if no namespace URI is assigned to the prefix specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06000F8B RID: 3979 RVA: 0x0004D480 File Offset: 0x0004B680
		public virtual string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				return null;
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupNamespace(prefix);
				}
			}
			else if (this.MoveToNamespace(prefix))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return null;
		}

		/// <summary>Gets the prefix declared for the specified namespace URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix assigned to the namespace URI specified; otherwise, <see cref="F:System.String.Empty" /> if no prefix is assigned to the namespace URI specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="namespaceURI">The namespace URI to resolve for the prefix.</param>
		// Token: 0x06000F8C RID: 3980 RVA: 0x0004D500 File Offset: 0x0004B700
		public virtual string LookupPrefix(string namespaceURI)
		{
			if (namespaceURI == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (this.NodeType != XPathNodeType.Element)
			{
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupPrefix(namespaceURI);
				}
			}
			else if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(namespaceURI == xpathNavigator.Value))
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						goto IL_004C;
					}
				}
				return xpathNavigator.LocalName;
			}
			IL_004C:
			if (namespaceURI == this.LookupNamespace(string.Empty))
			{
				return string.Empty;
			}
			if (namespaceURI == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		/// <summary>Returns the in-scope namespaces of the current node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IDictionary`2" /> collection of namespace names keyed by prefix.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value specifying the namespaces to return.</param>
		// Token: 0x06000F8D RID: 3981 RVA: 0x0004D59C File Offset: 0x0004B79C
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			XPathNodeType nodeType = this.NodeType;
			if ((nodeType != XPathNodeType.Element && scope != XmlNamespaceScope.Local) || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.GetNamespacesInScope(scope);
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (scope == XmlNamespaceScope.All)
			{
				dictionary["xml"] = "http://www.w3.org/XML/1998/namespace";
			}
			if (this.MoveToFirstNamespace((XPathNamespaceScope)scope))
			{
				do
				{
					string localName = this.LocalName;
					string value = this.Value;
					if (localName.Length != 0 || value.Length != 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[localName] = value;
					}
				}
				while (this.MoveToNextNamespace((XPathNamespaceScope)scope));
				this.MoveToParent();
			}
			return dictionary;
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06000F8E RID: 3982
		public abstract XPathNavigator Clone();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNodeType" /> of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XPath.XPathNodeType" /> values representing the current node.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000F8F RID: 3983
		public abstract XPathNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node without any namespace prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local name of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F90 RID: 3984
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the qualified <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F91 RID: 3985
		public abstract string Name { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI of the current node, or <see cref="F:System.String.Empty" /> if the current node has no namespace URI.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F92 RID: 3986
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix associated with the current node.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F93 RID: 3987
		public abstract string Prefix { get; }

		/// <summary>When overridden in a derived class, gets the base URI for the current node.</summary>
		/// <returns>The location from which the node was loaded, or <see cref="F:System.String.Empty" /> if there is no value.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F94 RID: 3988
		public abstract string BaseURI { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element without an end element tag.</summary>
		/// <returns>true if the current node is an empty element; otherwise, false.</returns>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F95 RID: 3989
		public abstract bool IsEmptyElement { get; }

		/// <summary>Gets the xml:lang scope for the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the xml:lang scope, or <see cref="F:System.String.Empty" /> if the current node has no xml:lang scope value to return.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0004D638 File Offset: 0x0004B838
		public virtual string XmlLang
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				while (!xpathNavigator.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
				{
					if (!xpathNavigator.MoveToParent())
					{
						return string.Empty;
					}
				}
				return xpathNavigator.Value;
			}
		}

		/// <summary>Used by <see cref="T:System.Xml.XPath.XPathNavigator" /> implementations which provide a "virtualized" XML view over a store, to provide access to underlying objects.</summary>
		/// <returns>The default is null.</returns>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the attribute with the matching local name and namespace URI.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the attribute; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute; null for an empty namespace.</param>
		// Token: 0x06000F98 RID: 3992 RVA: 0x0004D672 File Offset: 0x0004B872
		public virtual bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.MoveToFirstAttribute())
			{
				while (!(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNextAttribute())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first attribute of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first attribute of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000F99 RID: 3993
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next attribute.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next attribute; false if there are no more attributes. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000F9A RID: 3994
		public abstract bool MoveToNextAttribute();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the namespace node with the specified namespace prefix.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the specified namespace; false if a matching namespace node was not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="name">The namespace prefix of the namespace node.</param>
		// Token: 0x06000F9B RID: 3995 RVA: 0x0004D6AA File Offset: 0x0004B8AA
		public virtual bool MoveToNamespace(string name)
		{
			if (this.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(name == this.LocalName))
				{
					if (!this.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first namespace node that matches the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x06000F9C RID: 3996
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node matching the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x06000F9D RID: 3997
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to first namespace node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000F9E RID: 3998 RVA: 0x0004D6D6 File Offset: 0x0004B8D6
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000F9F RID: 3999 RVA: 0x0004D6DF File Offset: 0x0004B8DF
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000FA0 RID: 4000
		public abstract bool MoveToNext();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first child node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first child node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000FA1 RID: 4001
		public abstract bool MoveToFirstChild();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the parent node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the parent node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000FA2 RID: 4002
		public abstract bool MoveToParent();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the root node that the current node belongs to.</summary>
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004D6E8 File Offset: 0x0004B8E8
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the node that you want to move to. </param>
		// Token: 0x06000FA4 RID: 4004
		public abstract bool MoveTo(XPathNavigator other);

		/// <summary>When overridden in a derived class, moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving; otherwise, false. If false, the position of the navigator is unchanged.</returns>
		/// <param name="id">A <see cref="T:System.String" /> representing the ID value of the node to which you want to move.</param>
		// Token: 0x06000FA5 RID: 4005
		public abstract bool MoveToId(string id);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the child node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the child node to move to.</param>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		public virtual bool MoveToChild(string localName, string namespaceURI)
		{
			if (this.MoveToFirstChild())
			{
				while (this.NodeType != XPathNodeType.Element || !(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child node to move to.</param>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004D740 File Offset: 0x0004B940
		public virtual bool MoveToChild(XPathNodeType type)
		{
			if (this.MoveToFirstChild())
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				while (((1 << (int)this.NodeType) & contentKindMask) == 0)
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004D780 File Offset: 0x0004B980
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			XPathNavigator xpathNavigator = this.Clone();
			XPathNodeType xpathNodeType;
			if (end != null)
			{
				xpathNodeType = end.NodeType;
				if (xpathNodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			xpathNodeType = this.NodeType;
			if (xpathNodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (this.NodeType == XPathNodeType.Element && !(localName != this.LocalName) && !(namespaceURI != this.NamespaceURI))
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(xpathNavigator);
			return false;
			Block_8:
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004D828 File Offset: 0x0004BA28
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathNavigator xpathNavigator = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			XPathNodeType xpathNodeType;
			if (end != null)
			{
				xpathNodeType = end.NodeType;
				if (xpathNodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			xpathNodeType = this.NodeType;
			if (xpathNodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (((1 << (int)this.NodeType) & contentKindMask) != 0)
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(xpathNavigator);
			return false;
			Block_8:
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; false if there are no more siblings, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the next sibling node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the next sibling node to move to.</param>
		// Token: 0x06000FAA RID: 4010 RVA: 0x0004D8C4 File Offset: 0x0004BAC4
		public virtual bool MoveToNext(string localName, string namespaceURI)
		{
			XPathNavigator xpathNavigator = this.Clone();
			while (this.MoveToNext())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node that matches the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the sibling node to move to.</param>
		// Token: 0x06000FAB RID: 4011 RVA: 0x0004D914 File Offset: 0x0004BB14
		public virtual bool MoveToNext(XPathNodeType type)
		{
			XPathNavigator xpathNavigator = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToNext())
			{
				if (((1 << (int)this.NodeType) & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(xpathNavigator);
			return false;
		}

		/// <summary>When overridden in a derived class, determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator" /> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>true if the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects have the same position; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x06000FAC RID: 4012
		public abstract bool IsSamePosition(XPathNavigator other);

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x06000FAD RID: 4013 RVA: 0x0004D953 File Offset: 0x0004BB53
		public virtual bool IsDescendant(XPathNavigator nav)
		{
			if (nav != null)
			{
				nav = nav.Clone();
				while (nav.MoveToParent())
				{
					if (nav.IsSamePosition(this))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Compares the position of the current <see cref="T:System.Xml.XPath.XPathNavigator" /> with the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeOrder" /> value representing the comparative position of the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare against.</param>
		// Token: 0x06000FAE RID: 4014 RVA: 0x0004D978 File Offset: 0x0004BB78
		public virtual XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.IsSamePosition(nav))
			{
				return XmlNodeOrder.Same;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNavigator xpathNavigator2 = nav.Clone();
			int i = XPathNavigator.GetDepth(xpathNavigator.Clone());
			int j = XPathNavigator.GetDepth(xpathNavigator2.Clone());
			if (i > j)
			{
				while (i > j)
				{
					xpathNavigator.MoveToParent();
					i--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.After;
				}
			}
			if (j > i)
			{
				while (j > i)
				{
					xpathNavigator2.MoveToParent();
					j--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.Before;
				}
			}
			XPathNavigator xpathNavigator3 = xpathNavigator.Clone();
			XPathNavigator xpathNavigator4 = xpathNavigator2.Clone();
			while (xpathNavigator3.MoveToParent() && xpathNavigator4.MoveToParent())
			{
				if (xpathNavigator3.IsSamePosition(xpathNavigator4))
				{
					xpathNavigator.GetType().ToString() != "Microsoft.VisualStudio.Modeling.StoreNavigator";
					return this.CompareSiblings(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator.MoveToParent();
				xpathNavigator2.MoveToParent();
			}
			return XmlNodeOrder.Unknown;
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object that contains the schema information for the current node.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00015458 File Offset: 0x00013658
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		/// <summary>Selects a node set, using the specified XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> pointing to the selected node set.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0004DA58 File Offset: 0x0004BC58
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(XPathExpression.Compile(xpath));
		}

		/// <summary>Selects a node set using the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004DA66 File Offset: 0x0004BC66
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			XPathNodeIterator xpathNodeIterator = this.Evaluate(expr) as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			return xpathNodeIterator;
		}

		/// <summary>Evaluates the <see cref="T:System.Xml.XPath.XPathExpression" /> and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004DA82 File Offset: 0x0004BC82
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		/// <summary>Uses the supplied context to evaluate the <see cref="T:System.Xml.XPath.XPathExpression" />, and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <param name="context">An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set that the evaluation is to be performed on.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004DA8C File Offset: 0x0004BC8C
		public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			query.Reset();
			if (context == null)
			{
				context = new XPathSingletonIterator(this.Clone(), true);
			}
			object obj = query.Evaluate(context);
			if (obj is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, query);
			}
			return obj;
		}

		/// <summary>Selects all the child nodes of the current node that have the matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child nodes.</param>
		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004DAED File Offset: 0x0004BCED
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathChildIterator(this.Clone(), type);
		}

		/// <summary>Selects all the child nodes of the current node that have the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the child nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the child nodes. </param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004DAFB File Offset: 0x0004BCFB
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			return new XPathChildIterator(this.Clone(), name, namespaceURI);
		}

		/// <summary>Selects all the descendant nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the descendant nodes.</param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004DB0A File Offset: 0x0004BD0A
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), type, matchSelf);
		}

		/// <summary>Selects all the descendant nodes of the current node with the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the descendant nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the descendant nodes. </param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004DB19 File Offset: 0x0004BD19
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0004DB2C File Offset: 0x0004BD2C
		internal bool MoveToNonDescendant()
		{
			if (this.NodeType == XPathNodeType.Root)
			{
				return false;
			}
			if (this.MoveToNext())
			{
				return true;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (!this.MoveToParent())
			{
				return false;
			}
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && this.MoveToFirstChild())
			{
				return true;
			}
			while (!this.MoveToNext())
			{
				if (!this.MoveToParent())
				{
					this.MoveTo(xpathNavigator);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0004DB90 File Offset: 0x0004BD90
		private static int GetDepth(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToParent())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		private XmlNodeOrder CompareSiblings(XPathNavigator n1, XPathNavigator n2)
		{
			int num = 0;
			XPathNodeType xpathNodeType = n1.NodeType;
			if (xpathNodeType != XPathNodeType.Attribute)
			{
				if (xpathNodeType != XPathNodeType.Namespace)
				{
					num += 2;
				}
			}
			else
			{
				num++;
			}
			xpathNodeType = n2.NodeType;
			if (xpathNodeType != XPathNodeType.Attribute)
			{
				if (xpathNodeType == XPathNodeType.Namespace)
				{
					if (num == 0)
					{
						while (n1.MoveToNextNamespace())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
				else
				{
					num -= 2;
					if (num == 0)
					{
						while (n1.MoveToNext())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
			}
			else
			{
				num--;
				if (num == 0)
				{
					while (n1.MoveToNextAttribute())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
			}
			if (num >= 0)
			{
				return XmlNodeOrder.After;
			}
			return XmlNodeOrder.Before;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0004DC44 File Offset: 0x0004BE44
		internal static int GetContentKindMask(XPathNodeType type)
		{
			return XPathNavigator.ContentKindMasks[(int)type];
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0004DC4D File Offset: 0x0004BE4D
		internal static int GetKindMask(XPathNodeType type)
		{
			if (type == XPathNodeType.All)
			{
				return int.MaxValue;
			}
			if (type == XPathNodeType.Text)
			{
				return 112;
			}
			return 1 << (int)type;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0004DC67 File Offset: 0x0004BE67
		internal static bool IsText(XPathNodeType type)
		{
			return type - XPathNodeType.Text <= 2;
		}

		// Token: 0x0400079F RID: 1951
		internal static readonly XPathNavigatorKeyComparer comparer = new XPathNavigatorKeyComparer();

		// Token: 0x040007A0 RID: 1952
		internal static readonly char[] NodeTypeLetter = new char[] { 'R', 'E', 'A', 'N', 'T', 'S', 'W', 'P', 'C', 'X' };

		// Token: 0x040007A1 RID: 1953
		internal static readonly char[] UniqueIdTbl = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4',
			'5', '6'
		};

		// Token: 0x040007A2 RID: 1954
		internal static readonly int[] ContentKindMasks = new int[] { 1, 2, 0, 0, 112, 32, 64, 128, 256, 2147483635 };
	}
}

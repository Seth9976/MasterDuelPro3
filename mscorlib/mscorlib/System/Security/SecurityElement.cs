using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	/// <summary>Represents the XML object model for encoding security objects. This class cannot be inherited.</summary>
	// Token: 0x02000326 RID: 806
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag.</summary>
		/// <param name="tag">The tag name of an XML element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter is invalid in XML. </exception>
		// Token: 0x06001CB8 RID: 7352 RVA: 0x00070384 File Offset: 0x0006E584
		public SecurityElement(string tag)
			: this(tag, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag and text.</summary>
		/// <param name="tag">The tag name of the XML element. </param>
		/// <param name="text">The text content within the element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter or <paramref name="text" /> parameter is invalid in XML. </exception>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00070390 File Offset: 0x0006E590
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		/// <summary>Gets or sets the attributes of an XML element as name/value pairs.</summary>
		/// <returns>The <see cref="T:System.Collections.Hashtable" /> object for the attribute values of the XML element.</returns>
		/// <exception cref="T:System.InvalidCastException">The name or value of the <see cref="T:System.Collections.Hashtable" /> object is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The name is not a valid XML attribute name.</exception>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000703E4 File Offset: 0x0006E5E4
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
		}

		/// <summary>Gets or sets the array of child elements of the XML element.</summary>
		/// <returns>The ordered child elements of the XML element as security elements.</returns>
		/// <exception cref="T:System.ArgumentException">A child of the XML parent node is null. </exception>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00070464 File Offset: 0x0006E664
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
		}

		/// <summary>Gets or sets the tag name of an XML element.</summary>
		/// <returns>The tag name of an XML element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The tag is null. </exception>
		/// <exception cref="T:System.ArgumentException">The tag is not valid in XML. </exception>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0007046C File Offset: 0x0006E66C
		public string Tag
		{
			get
			{
				return this.tag;
			}
		}

		/// <summary>Gets or sets the text within an XML element.</summary>
		/// <returns>The value of the text within an XML element.</returns>
		/// <exception cref="T:System.ArgumentException">The text is not valid in XML. </exception>
		// Token: 0x17000319 RID: 793
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00070474 File Offset: 0x0006E674
		public string Text
		{
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		/// <summary>Adds a name/value attribute to an XML element.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is invalid in XML.-or- An attribute with the name specified by the <paramref name="name" /> parameter already exists. </exception>
		// Token: 0x06001CBE RID: 7358 RVA: 0x000704A8 File Offset: 0x0006E6A8
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		/// <summary>Adds a child element to the XML element.</summary>
		/// <param name="child">The child element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter is null. </exception>
		// Token: 0x06001CBF RID: 7359 RVA: 0x00070516 File Offset: 0x0006E716
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		/// <summary>Finds an attribute by name in an XML element.</summary>
		/// <returns>The value associated with the named attribute, or null if no attribute with <paramref name="name" /> exists.</returns>
		/// <param name="name">The name of the attribute for which to search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x00070548 File Offset: 0x0006E748
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			if (attribute != null)
			{
				return attribute.Value;
			}
			return null;
		}

		/// <summary>Replaces invalid XML characters in a string with their valid XML equivalent.</summary>
		/// <returns>The input string with invalid characters replaced.</returns>
		/// <param name="str">The string within which to escape invalid characters. </param>
		// Token: 0x06001CC1 RID: 7361 RVA: 0x00070578 File Offset: 0x0006E778
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c != '&')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&gt;");
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&apos;");
				}
				IL_009E:
				i++;
				continue;
				IL_0096:
				stringBuilder.Append(c);
				goto IL_009E;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00070634 File Offset: 0x0006E834
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		/// <summary>Creates a security element from an XML-encoded string.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> created from the XML.</returns>
		/// <param name="xml">The XML-encoded string from which to create the security element.</param>
		/// <exception cref="T:System.Security.XmlSyntaxException">
		///   <paramref name="xml" /> contains one or more single quotation mark characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is null.</exception>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x000706A8 File Offset: 0x0006E8A8
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement securityElement;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				securityElement = securityParser.ToXml();
			}
			catch (Exception ex)
			{
				throw new XmlSyntaxException(Locale.GetText("Invalid XML."), ex);
			}
			return securityElement;
		}

		/// <summary>Determines whether a string is a valid attribute name.</summary>
		/// <returns>true if the <paramref name="name" /> parameter is a valid XML attribute name; otherwise, false.</returns>
		/// <param name="name">The attribute name to test for validity. </param>
		// Token: 0x06001CC4 RID: 7364 RVA: 0x00070714 File Offset: 0x0006E914
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid attribute value.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid XML attribute value; otherwise, false.</returns>
		/// <param name="value">The attribute value to test for validity. </param>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00070729 File Offset: 0x0006E929
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid tag.</summary>
		/// <returns>true if the <paramref name="tag" /> parameter is a valid XML tag; otherwise, false.</returns>
		/// <param name="tag">The tag to test for validity. </param>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x0007073E File Offset: 0x0006E93E
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		/// <summary>Determines whether a string is valid as text within an XML element.</summary>
		/// <returns>true if the <paramref name="text" /> parameter is a valid XML text element; otherwise, false.</returns>
		/// <param name="text">The text to test for validity. </param>
		// Token: 0x06001CC7 RID: 7367 RVA: 0x00070753 File Offset: 0x0006E953
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		/// <summary>Finds a child by its tag name.</summary>
		/// <returns>The first child XML element with the specified tag value, or null if no child element with <paramref name="tag" /> exists.</returns>
		/// <param name="tag">The tag for which to search in child elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00070768 File Offset: 0x0006E968
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		/// <summary>Produces a string representation of an XML element and its constituent attributes, child elements, and text.</summary>
		/// <returns>The XML element and its contents.</returns>
		// Token: 0x06001CC9 RID: 7369 RVA: 0x000707C8 File Offset: 0x0006E9C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000707EC File Offset: 0x0006E9EC
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value))
						.Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
				return;
			}
			s.Append(">").Append(SecurityElement.Escape(this.text));
			if (this.children != null)
			{
				s.Append(Environment.NewLine);
				foreach (object obj in this.children)
				{
					((SecurityElement)obj).ToXml(ref s, level + 1);
				}
			}
			s.Append("</").Append(this.tag).Append(">")
				.Append(Environment.NewLine);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00070998 File Offset: 0x0006EB98
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x1700031A RID: 794
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x00070A08 File Offset: 0x0006EC08
		internal string m_strText
		{
			set
			{
				this.text = value;
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00070A14 File Offset: 0x0006EC14
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.tag == null)
			{
				return null;
			}
			if (this.tag.Equals(strLocalName) || this.tag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.text);
			}
			if (this.children == null)
			{
				return null;
			}
			foreach (object obj in this.children)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04000D2F RID: 3375
		private string text;

		// Token: 0x04000D30 RID: 3376
		private string tag;

		// Token: 0x04000D31 RID: 3377
		private ArrayList attributes;

		// Token: 0x04000D32 RID: 3378
		private ArrayList children;

		// Token: 0x04000D33 RID: 3379
		private static readonly char[] invalid_tag_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04000D34 RID: 3380
		private static readonly char[] invalid_text_chars = new char[] { '<', '>' };

		// Token: 0x04000D35 RID: 3381
		private static readonly char[] invalid_attr_name_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04000D36 RID: 3382
		private static readonly char[] invalid_attr_value_chars = new char[] { '"', '<', '>' };

		// Token: 0x04000D37 RID: 3383
		private static readonly char[] invalid_chars = new char[] { '<', '>', '"', '\'', '&' };

		// Token: 0x02000327 RID: 807
		internal class SecurityAttribute
		{
			// Token: 0x06001CCF RID: 7375 RVA: 0x00070B20 File Offset: 0x0006ED20
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00070B8C File Offset: 0x0006ED8C
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00070B94 File Offset: 0x0006ED94
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04000D38 RID: 3384
			private string _name;

			// Token: 0x04000D39 RID: 3385
			private string _value;
		}
	}
}

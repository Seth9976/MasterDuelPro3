using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object.</summary>
	// Token: 0x020001A9 RID: 425
	public class XmlAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class.</summary>
		// Token: 0x060013FA RID: 5114 RVA: 0x00060FB7 File Offset: 0x0005F1B7
		public XmlAttributes()
		{
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00060FE0 File Offset: 0x0005F1E0
		internal XmlAttributeFlags XmlFlags
		{
			get
			{
				XmlAttributeFlags xmlAttributeFlags = (XmlAttributeFlags)0;
				if (this.xmlElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Elements;
				}
				if (this.xmlArrayItems.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ArrayItems;
				}
				if (this.xmlAnyElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyElements;
				}
				if (this.xmlArray != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Array;
				}
				if (this.xmlAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Attribute;
				}
				if (this.xmlText != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Text;
				}
				if (this.xmlEnum != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Enum;
				}
				if (this.xmlRoot != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Root;
				}
				if (this.xmlType != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Type;
				}
				if (this.xmlAnyAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyAttribute;
				}
				if (this.xmlChoiceIdentifier != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ChoiceIdentifier;
				}
				if (this.xmlns)
				{
					xmlAttributeFlags |= XmlAttributeFlags.XmlnsDeclarations;
				}
				return xmlAttributeFlags;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000610AC File Offset: 0x0005F2AC
		private static Type IgnoreAttribute
		{
			get
			{
				if (XmlAttributes.ignoreAttributeType == null)
				{
					XmlAttributes.ignoreAttributeType = typeof(object).Assembly.GetType("System.XmlIgnoreMemberAttribute");
					if (XmlAttributes.ignoreAttributeType == null)
					{
						XmlAttributes.ignoreAttributeType = typeof(XmlIgnoreAttribute);
					}
				}
				return XmlAttributes.ignoreAttributeType;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class and customizes how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object. </summary>
		/// <param name="provider">A class that can provide alternative implementations of attributes that control XML serialization.</param>
		// Token: 0x060013FD RID: 5117 RVA: 0x00061110 File Offset: 0x0005F310
		public XmlAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			XmlAnyElementAttribute xmlAnyElementAttribute = null;
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is XmlIgnoreAttribute || customAttributes[i] is ObsoleteAttribute || customAttributes[i].GetType() == XmlAttributes.IgnoreAttribute)
				{
					this.xmlIgnore = true;
					break;
				}
				if (customAttributes[i] is XmlElementAttribute)
				{
					this.xmlElements.Add((XmlElementAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlArrayItemAttribute)
				{
					this.xmlArrayItems.Add((XmlArrayItemAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlAnyElementAttribute)
				{
					XmlAnyElementAttribute xmlAnyElementAttribute2 = (XmlAnyElementAttribute)customAttributes[i];
					if ((xmlAnyElementAttribute2.Name == null || xmlAnyElementAttribute2.Name.Length == 0) && xmlAnyElementAttribute2.NamespaceSpecified && xmlAnyElementAttribute2.Namespace == null)
					{
						xmlAnyElementAttribute = xmlAnyElementAttribute2;
					}
					else
					{
						this.xmlAnyElements.Add((XmlAnyElementAttribute)customAttributes[i]);
					}
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.xmlDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
				else if (customAttributes[i] is XmlAttributeAttribute)
				{
					this.xmlAttribute = (XmlAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlArrayAttribute)
				{
					this.xmlArray = (XmlArrayAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTextAttribute)
				{
					this.xmlText = (XmlTextAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlEnumAttribute)
				{
					this.xmlEnum = (XmlEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlRootAttribute)
				{
					this.xmlRoot = (XmlRootAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTypeAttribute)
				{
					this.xmlType = (XmlTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlAnyAttributeAttribute)
				{
					this.xmlAnyAttribute = (XmlAnyAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlChoiceIdentifierAttribute)
				{
					this.xmlChoiceIdentifier = (XmlChoiceIdentifierAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlNamespaceDeclarationsAttribute)
				{
					this.xmlns = true;
				}
			}
			if (this.xmlIgnore)
			{
				this.xmlElements.Clear();
				this.xmlArrayItems.Clear();
				this.xmlAnyElements.Clear();
				this.xmlDefaultValue = null;
				this.xmlAttribute = null;
				this.xmlArray = null;
				this.xmlText = null;
				this.xmlEnum = null;
				this.xmlType = null;
				this.xmlAnyAttribute = null;
				this.xmlChoiceIdentifier = null;
				this.xmlns = false;
				return;
			}
			if (xmlAnyElementAttribute != null)
			{
				this.xmlAnyElements.Add(xmlAnyElementAttribute);
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000613BC File Offset: 0x0005F5BC
		internal static object GetAttr(ICustomAttributeProvider provider, Type attrType)
		{
			object[] customAttributes = provider.GetCustomAttributes(attrType, false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return customAttributes[0];
		}

		/// <summary>Gets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property as an XML element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> that contains a collection of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x000613DB File Offset: 0x0005F5DB
		public XmlElementAttributes XmlElements
		{
			get
			{
				return this.xmlElements;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property as an XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeAttribute" /> that controls the serialization of a public field or read/write property as an XML attribute.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000613E3 File Offset: 0x0005F5E3
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x000613EB File Offset: 0x0005F5EB
		public XmlAttributeAttribute XmlAttribute
		{
			get
			{
				return this.xmlAttribute;
			}
			set
			{
				this.xmlAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x000613F4 File Offset: 0x0005F5F4
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x000613FC File Offset: 0x0005F5FC
		public XmlEnumAttribute XmlEnum
		{
			get
			{
				return this.xmlEnum;
			}
			set
			{
				this.xmlEnum = value;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize a public field or public read/write property as XML text.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTextAttribute" /> that overrides the default serialization of a public property or field.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00061405 File Offset: 0x0005F605
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0006140D File Offset: 0x0005F60D
		public XmlTextAttribute XmlText
		{
			get
			{
				return this.xmlText;
			}
			set
			{
				this.xmlText = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00061416 File Offset: 0x0005F616
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0006141E File Offset: 0x0005F61E
		public XmlArrayAttribute XmlArray
		{
			get
			{
				return this.xmlArray;
			}
			set
			{
				this.xmlArray = value;
			}
		}

		/// <summary>Gets or sets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes items inserted into an array returned by a public field or read/write property.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayItemAttributes" /> object that contains a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00061427 File Offset: 0x0005F627
		public XmlArrayItemAttributes XmlArrayItems
		{
			get
			{
				return this.xmlArrayItems;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of an XML element or attribute.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0006142F File Offset: 0x0005F62F
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x00061437 File Offset: 0x0005F637
		public object XmlDefaultValue
		{
			get
			{
				return this.xmlDefaultValue;
			}
			set
			{
				this.xmlDefaultValue = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether or not the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00061440 File Offset: 0x0005F640
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x00061448 File Offset: 0x0005F648
		public bool XmlIgnore
		{
			get
			{
				return this.xmlIgnore;
			}
			set
			{
				this.xmlIgnore = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class to which the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> has been applied.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> that overrides an <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> applied to a class declaration.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00061451 File Offset: 0x0005F651
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00061459 File Offset: 0x0005F659
		public XmlTypeAttribute XmlType
		{
			get
			{
				return this.xmlType;
			}
			set
			{
				this.xmlType = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class as an XML root element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that overrides a class attributed as an XML root element.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00061462 File Offset: 0x0005F662
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0006146A File Offset: 0x0005F66A
		public XmlRootAttribute XmlRoot
		{
			get
			{
				return this.xmlRoot;
			}
			set
			{
				this.xmlRoot = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects to override.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAnyElementAttributes" /> object that represents the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00061473 File Offset: 0x0005F673
		public XmlAnyElementAttributes XmlAnyElements
		{
			get
			{
				return this.xmlAnyElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0006147B File Offset: 0x0005F67B
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x00061483 File Offset: 0x0005F683
		public XmlAnyAttributeAttribute XmlAnyAttribute
		{
			get
			{
				return this.xmlAnyAttribute;
			}
			set
			{
				this.xmlAnyAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that allows you to distinguish between a set of choices.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> that can be applied to a class member that is serialized as an xsi:choice element.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0006148C File Offset: 0x0005F68C
		public XmlChoiceIdentifierAttribute XmlChoiceIdentifier
		{
			get
			{
				return this.xmlChoiceIdentifier;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to keep all namespace declarations when an object containing a member that returns an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object is overridden.</summary>
		/// <returns>true if the namespace declarations should be kept; otherwise, false.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00061494 File Offset: 0x0005F694
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x0006149C File Offset: 0x0005F69C
		public bool Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x04000956 RID: 2390
		private XmlElementAttributes xmlElements = new XmlElementAttributes();

		// Token: 0x04000957 RID: 2391
		private XmlArrayItemAttributes xmlArrayItems = new XmlArrayItemAttributes();

		// Token: 0x04000958 RID: 2392
		private XmlAnyElementAttributes xmlAnyElements = new XmlAnyElementAttributes();

		// Token: 0x04000959 RID: 2393
		private XmlArrayAttribute xmlArray;

		// Token: 0x0400095A RID: 2394
		private XmlAttributeAttribute xmlAttribute;

		// Token: 0x0400095B RID: 2395
		private XmlTextAttribute xmlText;

		// Token: 0x0400095C RID: 2396
		private XmlEnumAttribute xmlEnum;

		// Token: 0x0400095D RID: 2397
		private bool xmlIgnore;

		// Token: 0x0400095E RID: 2398
		private bool xmlns;

		// Token: 0x0400095F RID: 2399
		private object xmlDefaultValue;

		// Token: 0x04000960 RID: 2400
		private XmlRootAttribute xmlRoot;

		// Token: 0x04000961 RID: 2401
		private XmlTypeAttribute xmlType;

		// Token: 0x04000962 RID: 2402
		private XmlAnyAttributeAttribute xmlAnyAttribute;

		// Token: 0x04000963 RID: 2403
		private XmlChoiceIdentifierAttribute xmlChoiceIdentifier;

		// Token: 0x04000964 RID: 2404
		private static volatile Type ignoreAttributeType;
	}
}

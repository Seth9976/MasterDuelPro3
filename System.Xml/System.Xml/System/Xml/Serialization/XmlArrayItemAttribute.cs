using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents an attribute that specifies the derived types that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can place in a serialized array.</summary>
	// Token: 0x020001A4 RID: 420
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlArrayItemAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class.</summary>
		// Token: 0x060013CB RID: 5067 RVA: 0x0005995F File Offset: 0x00057B5F
		public XmlArrayItemAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		// Token: 0x060013CC RID: 5068 RVA: 0x00060D59 File Offset: 0x0005EF59
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the <see cref="T:System.Type" /> that can be inserted into the serialized array.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x060013CD RID: 5069 RVA: 0x00060D68 File Offset: 0x0005EF68
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document and the <see cref="T:System.Type" /> that can be inserted into the generated XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x060013CE RID: 5070 RVA: 0x00060D77 File Offset: 0x0005EF77
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		/// <summary>Gets or sets the type allowed in an array.</summary>
		/// <returns>A <see cref="T:System.Type" /> that is allowed in the array.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00060D8D File Offset: 0x0005EF8D
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x00060D95 File Offset: 0x0005EF95
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00060D9E File Offset: 0x0005EF9E
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00060DB4 File Offset: 0x0005EFB4
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the namespace of the generated XML element.</summary>
		/// <returns>The namespace of the generated XML element.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00060DBD File Offset: 0x0005EFBD
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00060DC5 File Offset: 0x0005EFC5
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the level in a hierarchy of XML elements that the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> affects.</summary>
		/// <returns>The zero-based index of a set of indexes in an array of arrays.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00060DCE File Offset: 0x0005EFCE
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x00060DD6 File Offset: 0x0005EFD6
		public int NestingLevel
		{
			get
			{
				return this.nestingLevel;
			}
			set
			{
				this.nestingLevel = value;
			}
		}

		/// <summary>Gets or sets the XML data type of the generated XML element.</summary>
		/// <returns>An XML schema definition (XSD) data type, as defined by the World Wide Web Consortium (www.w3.org) document "XML Schema Part 2: DataTypes".</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00060DDF File Offset: 0x0005EFDF
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00060DF5 File Offset: 0x0005EFF5
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member as an empty XML tag with the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false, and no instance is generated. The default is true.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00060DFE File Offset: 0x0005EFFE
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00060E06 File Offset: 0x0005F006
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00060E16 File Offset: 0x0005F016
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the name of the generated XML element is qualified.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is XmlSchemaForm.None.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Form" /> property is set to XmlSchemaForm.Unqualified and a <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Namespace" /> value is specified. </exception>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00060E1E File Offset: 0x0005F01E
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x00060E26 File Offset: 0x0005F026
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x0400093B RID: 2363
		private string elementName;

		// Token: 0x0400093C RID: 2364
		private Type type;

		// Token: 0x0400093D RID: 2365
		private string ns;

		// Token: 0x0400093E RID: 2366
		private string dataType;

		// Token: 0x0400093F RID: 2367
		private bool nullable;

		// Token: 0x04000940 RID: 2368
		private bool nullableSpecified;

		// Token: 0x04000941 RID: 2369
		private XmlSchemaForm form;

		// Token: 0x04000942 RID: 2370
		private int nestingLevel;
	}
}

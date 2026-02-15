using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization.</summary>
	// Token: 0x020001FA RID: 506
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the object that represents the section that contains configuration elements for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection" /> that represents the schemaImporterExtenstion element in the configuration file.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00097618 File Offset: 0x00095818
		[ConfigurationProperty("schemaImporterExtensions")]
		public SchemaImporterExtensionsSection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionsSection)base.Sections["schemaImporterExtensions"];
			}
		}

		/// <summary>Gets the object that represents the <see cref="T:System.DateTime" /> serialization configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> object that represents the configuration element.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0009762F File Offset: 0x0009582F
		[ConfigurationProperty("dateTimeSerialization")]
		public DateTimeSerializationSection DateTimeSerialization
		{
			get
			{
				return (DateTimeSerializationSection)base.Sections["dateTimeSerialization"];
			}
		}

		/// <summary>Gets the object that represents the configuration group for the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> that represents the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00097646 File Offset: 0x00095846
		public XmlSerializerSection XmlSerializer
		{
			get
			{
				return (XmlSerializerSection)base.Sections["xmlSerializer"];
			}
		}
	}
}

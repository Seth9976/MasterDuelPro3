using System;
using System.Configuration;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the configuration for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class. This class cannot be inherited.</summary>
	// Token: 0x020001F9 RID: 505
	public sealed class SchemaImporterExtensionsSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection" /> class.</summary>
		// Token: 0x060019A7 RID: 6567 RVA: 0x00097262 File Offset: 0x00095462
		public SchemaImporterExtensionsSection()
		{
			this.properties.Add(this.schemaImporterExtensions);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0009729E File Offset: 0x0009549E
		private static string GetSqlTypeSchemaImporter(string typeName)
		{
			return "System.Data.SqlTypes." + typeName + ", System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000972B0 File Offset: 0x000954B0
		protected override void InitializeDefault()
		{
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterVarChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNVarChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNVarCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterText", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeTextSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNText", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNTextSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterVarBinary", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarBinarySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBinary", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBinarySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterImage", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarImageSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterDecimal", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeDecimalSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNumeric", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNumericSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBigInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBigIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterTinyInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeTinyIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBit", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBitSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterFloat", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeFloatSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterReal", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeRealSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterDateTime", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeDateTimeSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallDateTime", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallDateTimeSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterMoney", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeMoneySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallMoney", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallMoneySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterUniqueIdentifier", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeUniqueIdentifierSchemaImporterExtension")));
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00097586 File Offset: 0x00095786
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the object that represents the collection of extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> that contains the objects that represent configuration elements.</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0009758E File Offset: 0x0009578E
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public SchemaImporterExtensionElementCollection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionElementCollection)base[this.schemaImporterExtensions];
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x000975A4 File Offset: 0x000957A4
		internal SchemaImporterExtensionCollection SchemaImporterExtensionsInternal
		{
			get
			{
				SchemaImporterExtensionCollection schemaImporterExtensionCollection = new SchemaImporterExtensionCollection();
				foreach (object obj in this.SchemaImporterExtensions)
				{
					SchemaImporterExtensionElement schemaImporterExtensionElement = (SchemaImporterExtensionElement)obj;
					schemaImporterExtensionCollection.Add(schemaImporterExtensionElement.Name, schemaImporterExtensionElement.Type);
				}
				return schemaImporterExtensionCollection;
			}
		}

		// Token: 0x04000AED RID: 2797
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000AEE RID: 2798
		private readonly ConfigurationProperty schemaImporterExtensions = new ConfigurationProperty(null, typeof(SchemaImporterExtensionElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

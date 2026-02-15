using System;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001F2 RID: 498
	internal static class ConfigurationStrings
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x00096EF9 File Offset: 0x000950F9
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml.serialization", sectionName);
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00096F10 File Offset: 0x00095110
		internal static string SchemaImporterExtensionsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("schemaImporterExtensions");
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x00096F1C File Offset: 0x0009511C
		internal static string DateTimeSerializationSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("dateTimeSerialization");
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00096F28 File Offset: 0x00095128
		internal static string XmlSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("xmlSerializer");
			}
		}

		// Token: 0x04000AC1 RID: 2753
		internal const string Name = "name";

		// Token: 0x04000AC2 RID: 2754
		internal const string SchemaImporterExtensionsSectionName = "schemaImporterExtensions";

		// Token: 0x04000AC3 RID: 2755
		internal const string DateTimeSerializationSectionName = "dateTimeSerialization";

		// Token: 0x04000AC4 RID: 2756
		internal const string XmlSerializerSectionName = "xmlSerializer";

		// Token: 0x04000AC5 RID: 2757
		internal const string SectionGroupName = "system.xml.serialization";

		// Token: 0x04000AC6 RID: 2758
		internal const string SqlTypesSchemaImporterChar = "SqlTypesSchemaImporterChar";

		// Token: 0x04000AC7 RID: 2759
		internal const string SqlTypesSchemaImporterNChar = "SqlTypesSchemaImporterNChar";

		// Token: 0x04000AC8 RID: 2760
		internal const string SqlTypesSchemaImporterVarChar = "SqlTypesSchemaImporterVarChar";

		// Token: 0x04000AC9 RID: 2761
		internal const string SqlTypesSchemaImporterNVarChar = "SqlTypesSchemaImporterNVarChar";

		// Token: 0x04000ACA RID: 2762
		internal const string SqlTypesSchemaImporterText = "SqlTypesSchemaImporterText";

		// Token: 0x04000ACB RID: 2763
		internal const string SqlTypesSchemaImporterNText = "SqlTypesSchemaImporterNText";

		// Token: 0x04000ACC RID: 2764
		internal const string SqlTypesSchemaImporterVarBinary = "SqlTypesSchemaImporterVarBinary";

		// Token: 0x04000ACD RID: 2765
		internal const string SqlTypesSchemaImporterBinary = "SqlTypesSchemaImporterBinary";

		// Token: 0x04000ACE RID: 2766
		internal const string SqlTypesSchemaImporterImage = "SqlTypesSchemaImporterImage";

		// Token: 0x04000ACF RID: 2767
		internal const string SqlTypesSchemaImporterDecimal = "SqlTypesSchemaImporterDecimal";

		// Token: 0x04000AD0 RID: 2768
		internal const string SqlTypesSchemaImporterNumeric = "SqlTypesSchemaImporterNumeric";

		// Token: 0x04000AD1 RID: 2769
		internal const string SqlTypesSchemaImporterBigInt = "SqlTypesSchemaImporterBigInt";

		// Token: 0x04000AD2 RID: 2770
		internal const string SqlTypesSchemaImporterInt = "SqlTypesSchemaImporterInt";

		// Token: 0x04000AD3 RID: 2771
		internal const string SqlTypesSchemaImporterSmallInt = "SqlTypesSchemaImporterSmallInt";

		// Token: 0x04000AD4 RID: 2772
		internal const string SqlTypesSchemaImporterTinyInt = "SqlTypesSchemaImporterTinyInt";

		// Token: 0x04000AD5 RID: 2773
		internal const string SqlTypesSchemaImporterBit = "SqlTypesSchemaImporterBit";

		// Token: 0x04000AD6 RID: 2774
		internal const string SqlTypesSchemaImporterFloat = "SqlTypesSchemaImporterFloat";

		// Token: 0x04000AD7 RID: 2775
		internal const string SqlTypesSchemaImporterReal = "SqlTypesSchemaImporterReal";

		// Token: 0x04000AD8 RID: 2776
		internal const string SqlTypesSchemaImporterDateTime = "SqlTypesSchemaImporterDateTime";

		// Token: 0x04000AD9 RID: 2777
		internal const string SqlTypesSchemaImporterSmallDateTime = "SqlTypesSchemaImporterSmallDateTime";

		// Token: 0x04000ADA RID: 2778
		internal const string SqlTypesSchemaImporterMoney = "SqlTypesSchemaImporterMoney";

		// Token: 0x04000ADB RID: 2779
		internal const string SqlTypesSchemaImporterSmallMoney = "SqlTypesSchemaImporterSmallMoney";

		// Token: 0x04000ADC RID: 2780
		internal const string SqlTypesSchemaImporterUniqueIdentifier = "SqlTypesSchemaImporterUniqueIdentifier";

		// Token: 0x04000ADD RID: 2781
		internal const string Type = "type";

		// Token: 0x04000ADE RID: 2782
		internal const string Mode = "mode";

		// Token: 0x04000ADF RID: 2783
		internal const string CheckDeserializeAdvances = "checkDeserializeAdvances";

		// Token: 0x04000AE0 RID: 2784
		internal const string TempFilesLocation = "tempFilesLocation";

		// Token: 0x04000AE1 RID: 2785
		internal const string UseLegacySerializerGeneration = "useLegacySerializerGeneration";
	}
}

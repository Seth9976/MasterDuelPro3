using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization. </summary>
	// Token: 0x020001FB RID: 507
	public sealed class XmlSerializerSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> class. </summary>
		// Token: 0x060019B1 RID: 6577 RVA: 0x00097660 File Offset: 0x00095860
		public XmlSerializerSection()
		{
			this.properties.Add(this.checkDeserializeAdvances);
			this.properties.Add(this.tempFilesLocation);
			this.properties.Add(this.useLegacySerializerGeneration);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00097715 File Offset: 0x00095915
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines whether an additional check of progress of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is done.</summary>
		/// <returns>true if the check is made; otherwise, false. The default is true.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0009771D File Offset: 0x0009591D
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00097730 File Offset: 0x00095930
		[ConfigurationProperty("checkDeserializeAdvances", DefaultValue = false)]
		public bool CheckDeserializeAdvances
		{
			get
			{
				return (bool)base[this.checkDeserializeAdvances];
			}
			set
			{
				base[this.checkDeserializeAdvances] = value;
			}
		}

		/// <summary>Returns the location that was specified for the creation of the temporary file.</summary>
		/// <returns>The location that was specified for the creation of the temporary file.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00097744 File Offset: 0x00095944
		// (set) Token: 0x060019B6 RID: 6582 RVA: 0x00097757 File Offset: 0x00095957
		[ConfigurationProperty("tempFilesLocation", DefaultValue = null)]
		public string TempFilesLocation
		{
			get
			{
				return (string)base[this.tempFilesLocation];
			}
			set
			{
				base[this.tempFilesLocation] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the specified object uses legacy serializer generation.</summary>
		/// <returns>true if the object uses legacy serializer generation; otherwise, false.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00097766 File Offset: 0x00095966
		// (set) Token: 0x060019B8 RID: 6584 RVA: 0x00097779 File Offset: 0x00095979
		[ConfigurationProperty("useLegacySerializerGeneration", DefaultValue = false)]
		public bool UseLegacySerializerGeneration
		{
			get
			{
				return (bool)base[this.useLegacySerializerGeneration];
			}
			set
			{
				base[this.useLegacySerializerGeneration] = value;
			}
		}

		// Token: 0x04000AEF RID: 2799
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000AF0 RID: 2800
		private readonly ConfigurationProperty checkDeserializeAdvances = new ConfigurationProperty("checkDeserializeAdvances", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000AF1 RID: 2801
		private readonly ConfigurationProperty tempFilesLocation = new ConfigurationProperty("tempFilesLocation", typeof(string), null, null, new RootedPathValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x04000AF2 RID: 2802
		private readonly ConfigurationProperty useLegacySerializerGeneration = new ConfigurationProperty("useLegacySerializerGeneration", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}

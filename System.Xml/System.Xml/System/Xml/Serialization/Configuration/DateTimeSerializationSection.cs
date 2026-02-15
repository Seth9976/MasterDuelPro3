using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles configuration settings for XML serialization of <see cref="T:System.DateTime" /> instances.</summary>
	// Token: 0x020001F3 RID: 499
	public sealed class DateTimeSerializationSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> class.</summary>
		// Token: 0x06001985 RID: 6533 RVA: 0x00096F34 File Offset: 0x00095134
		public DateTimeSerializationSection()
		{
			this.properties.Add(this.mode);
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x00096F94 File Offset: 0x00095194
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines the serialization format.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection.DateTimeSerializationMode" /> values.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00096F9C File Offset: 0x0009519C
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x00096FAF File Offset: 0x000951AF
		[ConfigurationProperty("mode", DefaultValue = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip)]
		public DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				return (DateTimeSerializationSection.DateTimeSerializationMode)base[this.mode];
			}
			set
			{
				base[this.mode] = value;
			}
		}

		// Token: 0x04000AE2 RID: 2786
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000AE3 RID: 2787
		private readonly ConfigurationProperty mode = new ConfigurationProperty("mode", typeof(DateTimeSerializationSection.DateTimeSerializationMode), DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip, new EnumConverter(typeof(DateTimeSerializationSection.DateTimeSerializationMode)), null, ConfigurationPropertyOptions.None);

		/// <summary>Determines XML serialization format of <see cref="T:System.DateTime" /> objects.</summary>
		// Token: 0x020001F4 RID: 500
		public enum DateTimeSerializationMode
		{
			/// <summary>Same as Roundtrip.</summary>
			// Token: 0x04000AE5 RID: 2789
			Default,
			/// <summary>The serializer examines individual <see cref="T:System.DateTime" />  instances to determine the serialization format: UTC, local, or unspecified.</summary>
			// Token: 0x04000AE6 RID: 2790
			Roundtrip,
			/// <summary>The serializer formats all <see cref="T:System.DateTime" /> objects as local time. This is for version 1.0 and 1.1 compatibility.</summary>
			// Token: 0x04000AE7 RID: 2791
			Local
		}
	}
}

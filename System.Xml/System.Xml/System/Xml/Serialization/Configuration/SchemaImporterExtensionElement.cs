using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the configuration for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class. This class cannot be inherited.</summary>
	// Token: 0x020001F5 RID: 501
	public sealed class SchemaImporterExtensionElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class.</summary>
		// Token: 0x06001989 RID: 6537 RVA: 0x00096FC4 File Offset: 0x000951C4
		public SchemaImporterExtensionElement()
		{
			this.properties.Add(this.name);
			this.properties.Add(this.type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class and specifies the name and type of the extension.</summary>
		/// <param name="name">The name of the new extension. The name must be unique.</param>
		/// <param name="type">The type of the new extension, specified as a string.</param>
		// Token: 0x0600198A RID: 6538 RVA: 0x00097042 File Offset: 0x00095242
		public SchemaImporterExtensionElement(string name, string type)
			: this()
		{
			this.Name = name;
			base[this.type] = new SchemaImporterExtensionElement.TypeAndName(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class using the specified name and type.</summary>
		/// <param name="name">The name of the new extension. The name must be unique.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the new extension.</param>
		// Token: 0x0600198B RID: 6539 RVA: 0x00097063 File Offset: 0x00095263
		public SchemaImporterExtensionElement(string name, Type type)
			: this()
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Gets or sets the name of the extension.</summary>
		/// <returns>The name of the extension.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00097079 File Offset: 0x00095279
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0009708C File Offset: 0x0009528C
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[this.name];
			}
			set
			{
				base[this.name] = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0009709B File Offset: 0x0009529B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the type of the extension.</summary>
		/// <returns>A type of the extension.</returns>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x000970A3 File Offset: 0x000952A3
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x000970BB File Offset: 0x000952BB
		[TypeConverter(typeof(SchemaImporterExtensionElement.TypeTypeConverter))]
		[ConfigurationProperty("type", IsRequired = true, IsKey = false)]
		public Type Type
		{
			get
			{
				return ((SchemaImporterExtensionElement.TypeAndName)base[this.type]).type;
			}
			set
			{
				base[this.type] = new SchemaImporterExtensionElement.TypeAndName(value);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x000970CF File Offset: 0x000952CF
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x04000AE8 RID: 2792
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000AE9 RID: 2793
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000AEA RID: 2794
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new SchemaImporterExtensionElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x020001F6 RID: 502
		private class TypeAndName
		{
			// Token: 0x06001992 RID: 6546 RVA: 0x000970D7 File Offset: 0x000952D7
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x06001993 RID: 6547 RVA: 0x000970F4 File Offset: 0x000952F4
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x06001994 RID: 6548 RVA: 0x00097103 File Offset: 0x00095303
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x06001995 RID: 6549 RVA: 0x00097110 File Offset: 0x00095310
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((SchemaImporterExtensionElement.TypeAndName)comparand).type);
			}

			// Token: 0x04000AEB RID: 2795
			public readonly Type type;

			// Token: 0x04000AEC RID: 2796
			public readonly string name;
		}

		// Token: 0x020001F7 RID: 503
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x06001996 RID: 6550 RVA: 0x00097128 File Offset: 0x00095328
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06001997 RID: 6551 RVA: 0x00097146 File Offset: 0x00095346
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new SchemaImporterExtensionElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x06001998 RID: 6552 RVA: 0x00097168 File Offset: 0x00095368
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				SchemaImporterExtensionElement.TypeAndName typeAndName = (SchemaImporterExtensionElement.TypeAndName)value;
				if (typeAndName.name != null)
				{
					return typeAndName.name;
				}
				return typeAndName.type.AssemblyQualifiedName;
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Represents an attribute or a child of a configuration element. This class cannot be inherited.</summary>
	// Token: 0x0200001A RID: 26
	public sealed class ConfigurationProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		// Token: 0x060000CF RID: 207 RVA: 0x0000507B File Offset: 0x0000327B
		public ConfigurationProperty(string name, Type type)
			: this(name, type, ConfigurationProperty.NoDefaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		// Token: 0x060000D0 RID: 208 RVA: 0x00005097 File Offset: 0x00003297
		public ConfigurationProperty(string name, Type type, object defaultValue)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		// Token: 0x060000D1 RID: 209 RVA: 0x000050AF File Offset: 0x000032AF
		public ConfigurationProperty(string name, Type type, object defaultValue, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		// Token: 0x060000D2 RID: 210 RVA: 0x000050C8 File Offset: 0x000032C8
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, typeConverter, validator, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		/// <param name="description">The description of the configuration entity. </param>
		// Token: 0x060000D3 RID: 211 RVA: 0x000050DC File Offset: 0x000032DC
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options, string description)
		{
			this.name = name;
			this.converter = ((typeConverter != null) ? typeConverter : TypeDescriptor.GetConverter(type));
			if (defaultValue != null)
			{
				if (defaultValue == ConfigurationProperty.NoDefaultValue)
				{
					TypeCode typeCode = Type.GetTypeCode(type);
					if (typeCode != TypeCode.Object)
					{
						if (typeCode != TypeCode.String)
						{
							defaultValue = Activator.CreateInstance(type);
						}
						else
						{
							defaultValue = string.Empty;
						}
					}
					else
					{
						defaultValue = null;
					}
				}
				else if (!type.IsAssignableFrom(defaultValue.GetType()))
				{
					if (!this.converter.CanConvertFrom(defaultValue.GetType()))
					{
						throw new ConfigurationErrorsException(string.Format("The default value for property '{0}' has a different type than the one of the property itself: expected {1} but was {2}", name, type, defaultValue.GetType()));
					}
					defaultValue = this.converter.ConvertFrom(defaultValue);
				}
			}
			this.default_value = defaultValue;
			this.flags = options;
			this.type = type;
			this.validation = ((validator != null) ? validator : new DefaultValidator());
			this.description = description;
		}

		/// <summary>Gets the default value for this <see cref="T:System.Configuration.ConfigurationProperty" /> property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to the type specified by the <see cref="P:System.Configuration.ConfigurationProperty.Type" /> property.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000051B7 File Offset: 0x000033B7
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is the key for the containing <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationProperty" /> object is the key for the containing element; otherwise, false. The default is false.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000051BF File Offset: 0x000033BF
		public bool IsKey
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsKey) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationProperty" /> is required; otherwise, false. The default is false.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000051CC File Offset: 0x000033CC
		public bool IsRequired
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsRequired) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value that indicates whether the property is the default collection of an element. </summary>
		/// <returns>true if the property is the default collection of an element; otherwise, false.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000051D9 File Offset: 0x000033D9
		public bool IsDefaultCollection
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsDefaultCollection) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000051E6 File Offset: 0x000033E6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000051EE File Offset: 0x000033EE
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000051F6 File Offset: 0x000033F6
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000051FE File Offset: 0x000033FE
		internal object ConvertFromString(string value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertFromInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000521A File Offset: 0x0000341A
		internal string ConvertToString(object value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertToInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005236 File Offset: 0x00003436
		internal bool IsElement
		{
			get
			{
				return typeof(ConfigurationElement).IsAssignableFrom(this.type);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000524D File Offset: 0x0000344D
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005255 File Offset: 0x00003455
		internal ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return this.collectionAttribute;
			}
			set
			{
				this.collectionAttribute = value;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000525E File Offset: 0x0000345E
		internal void Validate(object value)
		{
			if (this.validation != null)
			{
				this.validation.Validate(value);
			}
		}

		// Token: 0x04000065 RID: 101
		internal static readonly object NoDefaultValue = new object();

		// Token: 0x04000066 RID: 102
		private string name;

		// Token: 0x04000067 RID: 103
		private Type type;

		// Token: 0x04000068 RID: 104
		private object default_value;

		// Token: 0x04000069 RID: 105
		private TypeConverter converter;

		// Token: 0x0400006A RID: 106
		private ConfigurationValidatorBase validation;

		// Token: 0x0400006B RID: 107
		private ConfigurationPropertyOptions flags;

		// Token: 0x0400006C RID: 108
		private string description;

		// Token: 0x0400006D RID: 109
		private ConfigurationCollectionAttribute collectionAttribute;
	}
}

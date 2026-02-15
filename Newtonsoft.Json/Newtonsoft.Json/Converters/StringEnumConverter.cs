using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001BC RID: 444
	[NullableContext(1)]
	[Nullable(0)]
	public class StringEnumConverter : JsonConverter
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00041FE5 File Offset: 0x000401E5
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00041FF7 File Offset: 0x000401F7
		[Obsolete("StringEnumConverter.CamelCaseText is obsolete. Set StringEnumConverter.NamingStrategy with CamelCaseNamingStrategy instead.")]
		public bool CamelCaseText
		{
			get
			{
				return this.NamingStrategy is CamelCaseNamingStrategy;
			}
			set
			{
				if (value)
				{
					if (this.NamingStrategy is CamelCaseNamingStrategy)
					{
						return;
					}
					this.NamingStrategy = new CamelCaseNamingStrategy();
					return;
				}
				else
				{
					if (!(this.NamingStrategy is CamelCaseNamingStrategy))
					{
						return;
					}
					this.NamingStrategy = null;
					return;
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0004202B File Offset: 0x0004022B
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00042033 File Offset: 0x00040233
		[Nullable(2)]
		public NamingStrategy NamingStrategy
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0004203C File Offset: 0x0004023C
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00042044 File Offset: 0x00040244
		public bool AllowIntegerValues { get; set; } = true;

		// Token: 0x06000EEF RID: 3823 RVA: 0x0004204D File Offset: 0x0004024D
		public StringEnumConverter()
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0004205C File Offset: 0x0004025C
		[Obsolete("StringEnumConverter(bool) is obsolete. Create a converter with StringEnumConverter(NamingStrategy, bool) instead.")]
		public StringEnumConverter(bool camelCaseText)
		{
			if (camelCaseText)
			{
				this.NamingStrategy = new CamelCaseNamingStrategy();
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00042079 File Offset: 0x00040279
		public StringEnumConverter(NamingStrategy namingStrategy, bool allowIntegerValues = true)
		{
			this.NamingStrategy = namingStrategy;
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00042096 File Offset: 0x00040296
		public StringEnumConverter(Type namingStrategyType)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, null);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000420BD File Offset: 0x000402BD
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000420E4 File Offset: 0x000402E4
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters, bool allowIntegerValues)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00042114 File Offset: 0x00040314
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text;
			if (EnumUtils.TryToString(@enum.GetType(), value, this.NamingStrategy, out text))
			{
				writer.WriteValue(text);
				return;
			}
			if (!this.AllowIntegerValues)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, @enum.ToString("D")), null);
			}
			writer.WriteValue(value);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00042188 File Offset: 0x00040388
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				bool flag = ReflectionUtils.IsNullableType(objectType);
				Type type = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
				try
				{
					if (reader.TokenType == JsonToken.String)
					{
						object value = reader.Value;
						string text = ((value != null) ? value.ToString() : null);
						if (StringUtils.IsNullOrEmpty(text) && flag)
						{
							return null;
						}
						return EnumUtils.ParseEnum(type, this.NamingStrategy, text, !this.AllowIntegerValues);
					}
					else if (reader.TokenType == JsonToken.Integer)
					{
						if (!this.AllowIntegerValues)
						{
							throw JsonSerializationException.Create(reader, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, reader.Value));
						}
						return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
					}
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(reader.Value), objectType), ex);
				}
				throw JsonSerializationException.Create(reader, "Unexpected token {0} when parsing enum.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return null;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000422BC File Offset: 0x000404BC
		public override bool CanConvert(Type objectType)
		{
			return (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum();
		}
	}
}

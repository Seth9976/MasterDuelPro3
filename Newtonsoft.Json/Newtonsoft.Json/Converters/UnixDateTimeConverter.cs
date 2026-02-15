using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001BD RID: 445
	[NullableContext(1)]
	[Nullable(0)]
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000422D4 File Offset: 0x000404D4
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x000422DC File Offset: 0x000404DC
		public bool AllowPreEpoch { get; set; }

		// Token: 0x06000EFA RID: 3834 RVA: 0x000422E5 File Offset: 0x000404E5
		public UnixDateTimeConverter()
			: this(false)
		{
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000422EE File Offset: 0x000404EE
		public UnixDateTimeConverter(bool allowPreEpoch)
		{
			this.AllowPreEpoch = allowPreEpoch;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00042300 File Offset: 0x00040500
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			long num;
			if (value is DateTime)
			{
				num = (long)(((DateTime)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Expected date object value.");
				}
				num = (long)(((DateTimeOffset)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			if (!this.AllowPreEpoch && num < 0L)
			{
				throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
			}
			writer.WriteValue(num);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00042394 File Offset: 0x00040594
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullable(objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				long num;
				if (reader.TokenType == JsonToken.Integer)
				{
					num = (long)reader.Value;
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected Integer or String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					if (!long.TryParse((string)reader.Value, out num))
					{
						throw JsonSerializationException.Create(reader, "Cannot convert invalid value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
					}
				}
				if (!this.AllowPreEpoch && num < 0L)
				{
					throw JsonSerializationException.Create(reader, "Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				DateTime dateTime = UnixDateTimeConverter.UnixEpoch.AddSeconds((double)num);
				if ((flag ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(dateTime, TimeSpan.Zero);
				}
				return dateTime;
			}
		}

		// Token: 0x04000810 RID: 2064
		internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}

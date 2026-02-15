using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001B8 RID: 440
	[NullableContext(1)]
	[Nullable(0)]
	public class IsoDateTimeConverter : DateTimeConverterBase
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00041654 File Offset: 0x0003F854
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0004165C File Offset: 0x0003F85C
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this._dateTimeStyles;
			}
			set
			{
				this._dateTimeStyles = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00041665 File Offset: 0x0003F865
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00041676 File Offset: 0x0003F876
		[Nullable(2)]
		public string DateTimeFormat
		{
			[NullableContext(2)]
			get
			{
				return this._dateTimeFormat ?? string.Empty;
			}
			[NullableContext(2)]
			set
			{
				this._dateTimeFormat = (StringUtils.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0004168A File Offset: 0x0003F88A
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0004169B File Offset: 0x0003F89B
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000416A4 File Offset: 0x0003F8A4
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			string text;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTime = dateTime.ToUniversalTime();
				}
				text = dateTime.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.".FormatWith(CultureInfo.InvariantCulture, ReflectionUtils.GetObjectType(value)));
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}
				text = dateTimeOffset.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			writer.WriteValue(text);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00041774 File Offset: 0x0003F974
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
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
				Type type = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
				if (reader.TokenType == JsonToken.Date)
				{
					if (type == typeof(DateTimeOffset))
					{
						if (!(reader.Value is DateTimeOffset))
						{
							return new DateTimeOffset((DateTime)reader.Value);
						}
						return reader.Value;
					}
					else
					{
						object value = reader.Value;
						if (value is DateTimeOffset)
						{
							return ((DateTimeOffset)value).DateTime;
						}
						return reader.Value;
					}
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					object value2 = reader.Value;
					string text = ((value2 != null) ? value2.ToString() : null);
					if (StringUtils.IsNullOrEmpty(text) && flag)
					{
						return null;
					}
					if (type == typeof(DateTimeOffset))
					{
						if (!StringUtils.IsNullOrEmpty(this._dateTimeFormat))
						{
							return DateTimeOffset.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
						}
						return DateTimeOffset.Parse(text, this.Culture, this._dateTimeStyles);
					}
					else
					{
						if (!StringUtils.IsNullOrEmpty(this._dateTimeFormat))
						{
							return DateTime.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
						}
						return DateTime.Parse(text, this.Culture, this._dateTimeStyles);
					}
				}
			}
		}

		// Token: 0x04000805 RID: 2053
		private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x04000806 RID: 2054
		private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;

		// Token: 0x04000807 RID: 2055
		[Nullable(2)]
		private string _dateTimeFormat;

		// Token: 0x04000808 RID: 2056
		[Nullable(2)]
		private CultureInfo _culture;
	}
}

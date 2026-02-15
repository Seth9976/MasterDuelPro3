using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001B7 RID: 439
	[NullableContext(1)]
	[Nullable(0)]
	public class ExpandoObjectConverter : JsonConverter
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x000153AD File Offset: 0x000135AD
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00041501 File Offset: 0x0003F701
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			return this.ReadValue(reader);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0004150C File Offset: 0x0003F70C
		[return: Nullable(2)]
		private object ReadValue(JsonReader reader)
		{
			if (!reader.MoveToContent())
			{
				throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
			}
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadObject(reader);
			}
			if (tokenType == JsonToken.StartArray)
			{
				return this.ReadList(reader);
			}
			if (JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
			{
				return reader.Value;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when converting ExpandoObject: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00041584 File Offset: 0x0003F784
		private object ReadList(JsonReader reader)
		{
			IList<object> list = new List<object>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.EndArray)
					{
						return list;
					}
					object obj = this.ReadValue(reader);
					list.Add(obj);
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000415D0 File Offset: 0x0003F7D0
		private object ReadObject(JsonReader reader)
		{
			IDictionary<string, object> dictionary = new ExpandoObject();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							return dictionary;
						}
					}
				}
				else
				{
					string text = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
					}
					object obj = this.ReadValue(reader);
					dictionary[text] = obj;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00041642 File Offset: 0x0003F842
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ExpandoObject);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}

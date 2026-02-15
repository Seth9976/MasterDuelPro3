using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001AD RID: 429
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x000406E1 File Offset: 0x0003E8E1
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000406F0 File Offset: 0x0003E8F0
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x06000EA2 RID: 3746
		public abstract T Create(Type objectType);

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00002F27 File Offset: 0x00001127
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}

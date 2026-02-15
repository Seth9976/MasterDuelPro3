using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200013C RID: 316
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonWriter : JsonWriter
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x0002EEBC File Offset: 0x0002D0BC
		public TraceJsonWriter(JsonWriter innerWriter)
		{
			this._innerWriter = innerWriter;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Serialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
			this._textWriter.Culture = innerWriter.Culture;
			this._textWriter.DateFormatHandling = innerWriter.DateFormatHandling;
			this._textWriter.DateFormatString = innerWriter.DateFormatString;
			this._textWriter.DateTimeZoneHandling = innerWriter.DateTimeZoneHandling;
			this._textWriter.FloatFormatHandling = innerWriter.FloatFormatHandling;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002EF72 File Offset: 0x0002D172
		public string GetSerializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002EF7F File Offset: 0x0002D17F
		public override void WriteValue(decimal value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
		public override void WriteValue(decimal? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002EFD7 File Offset: 0x0002D1D7
		public override void WriteValue(bool value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002EFF8 File Offset: 0x0002D1F8
		public override void WriteValue(bool? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002F02F File Offset: 0x0002D22F
		public override void WriteValue(byte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002F050 File Offset: 0x0002D250
		public override void WriteValue(byte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002F087 File Offset: 0x0002D287
		public override void WriteValue(char value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
		public override void WriteValue(char? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002F0DF File Offset: 0x0002D2DF
		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002F10A File Offset: 0x0002D30A
		public override void WriteValue(DateTime value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002F12B File Offset: 0x0002D32B
		public override void WriteValue(DateTime? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002F162 File Offset: 0x0002D362
		public override void WriteValue(DateTimeOffset value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002F183 File Offset: 0x0002D383
		public override void WriteValue(DateTimeOffset? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002F1BA File Offset: 0x0002D3BA
		public override void WriteValue(double value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002F1DB File Offset: 0x0002D3DB
		public override void WriteValue(double? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002F212 File Offset: 0x0002D412
		public override void WriteUndefined()
		{
			this._textWriter.WriteUndefined();
			this._innerWriter.WriteUndefined();
			base.WriteUndefined();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002F230 File Offset: 0x0002D430
		public override void WriteNull()
		{
			this._textWriter.WriteNull();
			this._innerWriter.WriteNull();
			base.WriteUndefined();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002F24E File Offset: 0x0002D44E
		public override void WriteValue(float value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002F26F File Offset: 0x0002D46F
		public override void WriteValue(float? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002F2A6 File Offset: 0x0002D4A6
		public override void WriteValue(Guid value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002F2C7 File Offset: 0x0002D4C7
		public override void WriteValue(Guid? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002F2FE File Offset: 0x0002D4FE
		public override void WriteValue(int value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002F31F File Offset: 0x0002D51F
		public override void WriteValue(int? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002F356 File Offset: 0x0002D556
		public override void WriteValue(long value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002F377 File Offset: 0x0002D577
		public override void WriteValue(long? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				this._textWriter.WriteValue(value);
				this._innerWriter.WriteValue(value);
				base.InternalWriteValue(JsonToken.Integer);
				return;
			}
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002F40F File Offset: 0x0002D60F
		public override void WriteValue(sbyte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002F430 File Offset: 0x0002D630
		public override void WriteValue(sbyte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002F467 File Offset: 0x0002D667
		public override void WriteValue(short value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002F488 File Offset: 0x0002D688
		public override void WriteValue(short? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002F4BF File Offset: 0x0002D6BF
		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		public override void WriteValue(TimeSpan value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002F501 File Offset: 0x0002D701
		public override void WriteValue(TimeSpan? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002F538 File Offset: 0x0002D738
		public override void WriteValue(uint value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002F559 File Offset: 0x0002D759
		public override void WriteValue(uint? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002F590 File Offset: 0x0002D790
		public override void WriteValue(ulong value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002F5B1 File Offset: 0x0002D7B1
		public override void WriteValue(ulong? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002F619 File Offset: 0x0002D819
		public override void WriteValue(ushort value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002F63A File Offset: 0x0002D83A
		public override void WriteValue(ushort? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002F671 File Offset: 0x0002D871
		public override void WriteWhitespace(string ws)
		{
			this._textWriter.WriteWhitespace(ws);
			this._innerWriter.WriteWhitespace(ws);
			base.WriteWhitespace(ws);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002F692 File Offset: 0x0002D892
		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			this._textWriter.WriteComment(text);
			this._innerWriter.WriteComment(text);
			base.WriteComment(text);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002F6B3 File Offset: 0x0002D8B3
		public override void WriteStartArray()
		{
			this._textWriter.WriteStartArray();
			this._innerWriter.WriteStartArray();
			base.WriteStartArray();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002F6D1 File Offset: 0x0002D8D1
		public override void WriteEndArray()
		{
			this._textWriter.WriteEndArray();
			this._innerWriter.WriteEndArray();
			base.WriteEndArray();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002F6EF File Offset: 0x0002D8EF
		public override void WriteStartConstructor(string name)
		{
			this._textWriter.WriteStartConstructor(name);
			this._innerWriter.WriteStartConstructor(name);
			base.WriteStartConstructor(name);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002F710 File Offset: 0x0002D910
		public override void WriteEndConstructor()
		{
			this._textWriter.WriteEndConstructor();
			this._innerWriter.WriteEndConstructor();
			base.WriteEndConstructor();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002F72E File Offset: 0x0002D92E
		public override void WritePropertyName(string name)
		{
			this._textWriter.WritePropertyName(name);
			this._innerWriter.WritePropertyName(name);
			base.WritePropertyName(name);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002F74F File Offset: 0x0002D94F
		public override void WritePropertyName(string name, bool escape)
		{
			this._textWriter.WritePropertyName(name, escape);
			this._innerWriter.WritePropertyName(name, escape);
			base.WritePropertyName(name);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002F772 File Offset: 0x0002D972
		public override void WriteStartObject()
		{
			this._textWriter.WriteStartObject();
			this._innerWriter.WriteStartObject();
			base.WriteStartObject();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002F790 File Offset: 0x0002D990
		public override void WriteEndObject()
		{
			this._textWriter.WriteEndObject();
			this._innerWriter.WriteEndObject();
			base.WriteEndObject();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		[NullableContext(2)]
		public override void WriteRawValue(string json)
		{
			this._textWriter.WriteRawValue(json);
			this._innerWriter.WriteRawValue(json);
			base.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			this._textWriter.WriteRaw(json);
			this._innerWriter.WriteRaw(json);
			base.WriteRaw(json);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002F7F1 File Offset: 0x0002D9F1
		public override void Close()
		{
			this._textWriter.Close();
			this._innerWriter.Close();
			base.Close();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002F80F File Offset: 0x0002DA0F
		public override void Flush()
		{
			this._textWriter.Flush();
			this._innerWriter.Flush();
		}

		// Token: 0x040005C2 RID: 1474
		private readonly JsonWriter _innerWriter;

		// Token: 0x040005C3 RID: 1475
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040005C4 RID: 1476
		private readonly StringWriter _sw;
	}
}

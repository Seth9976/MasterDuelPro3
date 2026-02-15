using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200013B RID: 315
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002ED19 File Offset: 0x0002CF19
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002ED26 File Offset: 0x0002CF26
		public override bool Read()
		{
			bool flag = this._innerReader.Read();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002ED39 File Offset: 0x0002CF39
		public override int? ReadAsInt32()
		{
			int? num = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002ED4C File Offset: 0x0002CF4C
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string text = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return text;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002ED5F File Offset: 0x0002CF5F
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return array;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002ED72 File Offset: 0x0002CF72
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002ED85 File Offset: 0x0002CF85
		public override double? ReadAsDouble()
		{
			double? num = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002ED98 File Offset: 0x0002CF98
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002EDAB File Offset: 0x0002CFAB
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return dateTime;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002EDBE File Offset: 0x0002CFBE
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002EDD1 File Offset: 0x0002CFD1
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002EDE7 File Offset: 0x0002CFE7
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002EE01 File Offset: 0x0002D001
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0002EE0E File Offset: 0x0002D00E
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002EE1C File Offset: 0x0002D01C
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002EE29 File Offset: 0x0002D029
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002EE36 File Offset: 0x0002D036
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002EE43 File Offset: 0x0002D043
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002EE50 File Offset: 0x0002D050
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002EE74 File Offset: 0x0002D074
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002EE98 File Offset: 0x0002D098
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040005BF RID: 1471
		private readonly JsonReader _innerReader;

		// Token: 0x040005C0 RID: 1472
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040005C1 RID: 1473
		private readonly StringWriter _sw;
	}
}

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200012D RID: 301
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000913 RID: 2323 RVA: 0x0002DE39 File Offset: 0x0002C039
		// (remove) Token: 0x06000914 RID: 2324 RVA: 0x0002DE47 File Offset: 0x0002C047
		[Nullable(new byte[] { 2, 1 })]
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002DE55 File Offset: 0x0002C055
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0002DE62 File Offset: 0x0002C062
		[Nullable(2)]
		public override IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.ReferenceResolver;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002DE70 File Offset: 0x0002C070
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0002DE7D File Offset: 0x0002C07D
		[Nullable(2)]
		public override ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.TraceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002DE8B File Offset: 0x0002C08B
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002DE98 File Offset: 0x0002C098
		[Nullable(2)]
		public override IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.EqualityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.EqualityComparer = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002DEA6 File Offset: 0x0002C0A6
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002DEB3 File Offset: 0x0002C0B3
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0002DECE File Offset: 0x0002C0CE
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0002DEDB File Offset: 0x0002C0DB
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002DEE9 File Offset: 0x0002C0E9
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0002DEF6 File Offset: 0x0002C0F6
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002DF04 File Offset: 0x0002C104
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002DF11 File Offset: 0x0002C111
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0002DF1F File Offset: 0x0002C11F
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0002DF2C File Offset: 0x0002C12C
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0002DF3A File Offset: 0x0002C13A
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0002DF47 File Offset: 0x0002C147
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002DF55 File Offset: 0x0002C155
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0002DF62 File Offset: 0x0002C162
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002DF70 File Offset: 0x0002C170
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0002DF7D File Offset: 0x0002C17D
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002DF8B File Offset: 0x0002C18B
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0002DF98 File Offset: 0x0002C198
		public override MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._serializer.MetadataPropertyHandling;
			}
			set
			{
				this._serializer.MetadataPropertyHandling = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002DFA6 File Offset: 0x0002C1A6
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0002DFB3 File Offset: 0x0002C1B3
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002DFC1 File Offset: 0x0002C1C1
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0002DFCE File Offset: 0x0002C1CE
		public override TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002DFDC File Offset: 0x0002C1DC
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0002DFE9 File Offset: 0x0002C1E9
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002DFF7 File Offset: 0x0002C1F7
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0002E004 File Offset: 0x0002C204
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002E012 File Offset: 0x0002C212
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0002E01F File Offset: 0x0002C21F
		public override ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializer.SerializationBinder;
			}
			set
			{
				this._serializer.SerializationBinder = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0002E02D File Offset: 0x0002C22D
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0002E03A File Offset: 0x0002C23A
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0002E048 File Offset: 0x0002C248
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0002E055 File Offset: 0x0002C255
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002E063 File Offset: 0x0002C263
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0002E070 File Offset: 0x0002C270
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002E07E File Offset: 0x0002C27E
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0002E08B File Offset: 0x0002C28B
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0002E099 File Offset: 0x0002C299
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0002E0A6 File Offset: 0x0002C2A6
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0002E0C1 File Offset: 0x0002C2C1
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0002E0CF File Offset: 0x0002C2CF
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0002E0DC File Offset: 0x0002C2DC
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0002E0EA File Offset: 0x0002C2EA
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0002E0F7 File Offset: 0x0002C2F7
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0002E105 File Offset: 0x0002C305
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0002E112 File Offset: 0x0002C312
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0002E120 File Offset: 0x0002C320
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0002E12D File Offset: 0x0002C32D
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002E13B File Offset: 0x0002C33B
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002E148 File Offset: 0x0002C348
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002E156 File Offset: 0x0002C356
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0002E163 File Offset: 0x0002C363
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002E171 File Offset: 0x0002C371
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002E188 File Offset: 0x0002C388
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002E1AE File Offset: 0x0002C3AE
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
		[NullableContext(2)]
		internal override object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002E1FA File Offset: 0x0002C3FA
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002E21F File Offset: 0x0002C41F
		[NullableContext(2)]
		internal override void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x040005A2 RID: 1442
		[Nullable(2)]
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x040005A3 RID: 1443
		[Nullable(2)]
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x040005A4 RID: 1444
		internal readonly JsonSerializer _serializer;
	}
}

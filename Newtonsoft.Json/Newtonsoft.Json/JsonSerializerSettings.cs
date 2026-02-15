using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000033 RID: 51
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonSerializerSettings
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006269 File Offset: 0x00004469
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006276 File Offset: 0x00004476
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006284 File Offset: 0x00004484
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006291 File Offset: 0x00004491
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000629F File Offset: 0x0000449F
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000062AC File Offset: 0x000044AC
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000062BA File Offset: 0x000044BA
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000062C7 File Offset: 0x000044C7
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000062D5 File Offset: 0x000044D5
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000062E2 File Offset: 0x000044E2
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000062F0 File Offset: 0x000044F0
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000062F8 File Offset: 0x000044F8
		[Nullable(1)]
		public IList<JsonConverter> Converters
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006301 File Offset: 0x00004501
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000630E File Offset: 0x0000450E
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000631C File Offset: 0x0000451C
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006329 File Offset: 0x00004529
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006337 File Offset: 0x00004537
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006344 File Offset: 0x00004544
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006352 File Offset: 0x00004552
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000635A File Offset: 0x0000455A
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006363 File Offset: 0x00004563
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006370 File Offset: 0x00004570
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000637E File Offset: 0x0000457E
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000638B File Offset: 0x0000458B
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006399 File Offset: 0x00004599
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000063A1 File Offset: 0x000045A1
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000063AA File Offset: 0x000045AA
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000063B2 File Offset: 0x000045B2
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000063BB File Offset: 0x000045BB
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000063D0 File Offset: 0x000045D0
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006407 File Offset: 0x00004607
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000640F File Offset: 0x0000460F
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006418 File Offset: 0x00004618
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006420 File Offset: 0x00004620
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000642C File Offset: 0x0000462C
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006463 File Offset: 0x00004663
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006477 File Offset: 0x00004677
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000647F File Offset: 0x0000467F
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006488 File Offset: 0x00004688
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006490 File Offset: 0x00004690
		[Nullable(new byte[] { 2, 1 })]
		public EventHandler<ErrorEventArgs> Error
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000649C File Offset: 0x0000469C
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000064C6 File Offset: 0x000046C6
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000064D4 File Offset: 0x000046D4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000064E5 File Offset: 0x000046E5
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000064F5 File Offset: 0x000046F5
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006510 File Offset: 0x00004710
		public int? MaxDepth
		{
			get
			{
				if (!this._maxDepthSet)
				{
					return new int?(64);
				}
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if ((num.GetValueOrDefault() <= num2) & (num != null))
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006556 File Offset: 0x00004756
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006563 File Offset: 0x00004763
		public Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006571 File Offset: 0x00004771
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000657E File Offset: 0x0000477E
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000658C File Offset: 0x0000478C
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000065B2 File Offset: 0x000047B2
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000065C0 File Offset: 0x000047C0
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000065E6 File Offset: 0x000047E6
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000065F4 File Offset: 0x000047F4
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00006601 File Offset: 0x00004801
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000660F File Offset: 0x0000480F
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000661C File Offset: 0x0000481C
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000662A File Offset: 0x0000482A
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00006637 File Offset: 0x00004837
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006645 File Offset: 0x00004845
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006656 File Offset: 0x00004856
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000665F File Offset: 0x0000485F
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000666C File Offset: 0x0000486C
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006691 File Offset: 0x00004891
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000066A4 File Offset: 0x000048A4
		[NullableContext(1)]
		public JsonSerializerSettings(JsonSerializerSettings original)
		{
			this._floatParseHandling = original._floatParseHandling;
			this._floatFormatHandling = original._floatFormatHandling;
			this._dateParseHandling = original._dateParseHandling;
			this._dateTimeZoneHandling = original._dateTimeZoneHandling;
			this._dateFormatHandling = original._dateFormatHandling;
			this._formatting = original._formatting;
			this._maxDepth = original._maxDepth;
			this._maxDepthSet = original._maxDepthSet;
			this._dateFormatString = original._dateFormatString;
			this._dateFormatStringSet = original._dateFormatStringSet;
			this._context = original._context;
			this.Error = original.Error;
			this.SerializationBinder = original.SerializationBinder;
			this.TraceWriter = original.TraceWriter;
			this._culture = original._culture;
			this.ReferenceResolverProvider = original.ReferenceResolverProvider;
			this.EqualityComparer = original.EqualityComparer;
			this.ContractResolver = original.ContractResolver;
			this._constructorHandling = original._constructorHandling;
			this._typeNameAssemblyFormatHandling = original._typeNameAssemblyFormatHandling;
			this._metadataPropertyHandling = original._metadataPropertyHandling;
			this._typeNameHandling = original._typeNameHandling;
			this._preserveReferencesHandling = original._preserveReferencesHandling;
			this.Converters = original.Converters.ToList<JsonConverter>();
			this._defaultValueHandling = original._defaultValueHandling;
			this._nullValueHandling = original._nullValueHandling;
			this._objectCreationHandling = original._objectCreationHandling;
			this._missingMemberHandling = original._missingMemberHandling;
			this._referenceLoopHandling = original._referenceLoopHandling;
			this._checkAdditionalContent = original._checkAdditionalContent;
			this._stringEscapeHandling = original._stringEscapeHandling;
		}

		// Token: 0x040000BE RID: 190
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x040000BF RID: 191
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x040000C0 RID: 192
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x040000C1 RID: 193
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x040000C2 RID: 194
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x040000C3 RID: 195
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x040000C4 RID: 196
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x040000C5 RID: 197
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x040000C6 RID: 198
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x040000C7 RID: 199
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x040000C8 RID: 200
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x040000C9 RID: 201
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x040000CA RID: 202
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x040000CB RID: 203
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x040000CC RID: 204
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x040000CD RID: 205
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x040000CE RID: 206
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x040000CF RID: 207
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x040000D0 RID: 208
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040000D1 RID: 209
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040000D2 RID: 210
		[Nullable(1)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040000D3 RID: 211
		internal const int DefaultMaxDepth = 64;

		// Token: 0x040000D4 RID: 212
		internal Formatting? _formatting;

		// Token: 0x040000D5 RID: 213
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000D6 RID: 214
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000D7 RID: 215
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x040000D8 RID: 216
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040000D9 RID: 217
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x040000DA RID: 218
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040000DB RID: 219
		internal CultureInfo _culture;

		// Token: 0x040000DC RID: 220
		internal bool? _checkAdditionalContent;

		// Token: 0x040000DD RID: 221
		internal int? _maxDepth;

		// Token: 0x040000DE RID: 222
		internal bool _maxDepthSet;

		// Token: 0x040000DF RID: 223
		internal string _dateFormatString;

		// Token: 0x040000E0 RID: 224
		internal bool _dateFormatStringSet;

		// Token: 0x040000E1 RID: 225
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x040000E2 RID: 226
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000E3 RID: 227
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x040000E4 RID: 228
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000E5 RID: 229
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000E6 RID: 230
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040000E7 RID: 231
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000E8 RID: 232
		internal StreamingContext? _context;

		// Token: 0x040000E9 RID: 233
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x040000EA RID: 234
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000EB RID: 235
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}

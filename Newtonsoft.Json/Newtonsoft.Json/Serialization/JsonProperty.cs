using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000123 RID: 291
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000281B4 File Offset: 0x000263B4
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x000281BC File Offset: 0x000263BC
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000281C5 File Offset: 0x000263C5
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x000281CD File Offset: 0x000263CD
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x000281EF File Offset: 0x000263EF
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x000281F7 File Offset: 0x000263F7
		public Type DeclaringType { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00028200 File Offset: 0x00026400
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00028208 File Offset: 0x00026408
		public int? Order { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00028211 File Offset: 0x00026411
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00028219 File Offset: 0x00026419
		public string UnderlyingName { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00028222 File Offset: 0x00026422
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0002822A File Offset: 0x0002642A
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00028233 File Offset: 0x00026433
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0002823B File Offset: 0x0002643B
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00028244 File Offset: 0x00026444
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0002824C File Offset: 0x0002644C
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002826A File Offset: 0x0002646A
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00028272 File Offset: 0x00026472
		public JsonConverter Converter { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0002827B File Offset: 0x0002647B
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00028283 File Offset: 0x00026483
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002828C File Offset: 0x0002648C
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x00028294 File Offset: 0x00026494
		public bool Ignored { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0002829D File Offset: 0x0002649D
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x000282A5 File Offset: 0x000264A5
		public bool Readable { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000282AE File Offset: 0x000264AE
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x000282B6 File Offset: 0x000264B6
		public bool Writable { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000282BF File Offset: 0x000264BF
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x000282C7 File Offset: 0x000264C7
		public bool HasMemberAttribute { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000282D0 File Offset: 0x000264D0
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x000282E2 File Offset: 0x000264E2
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000282F2 File Offset: 0x000264F2
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00028332 File Offset: 0x00026532
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0002833F File Offset: 0x0002653F
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002834D File Offset: 0x0002654D
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002835A File Offset: 0x0002655A
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00028362 File Offset: 0x00026562
		public bool? IsReference { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0002836B File Offset: 0x0002656B
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x00028373 File Offset: 0x00026573
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002837C File Offset: 0x0002657C
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00028384 File Offset: 0x00026584
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002838D File Offset: 0x0002658D
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00028395 File Offset: 0x00026595
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002839E File Offset: 0x0002659E
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x000283A6 File Offset: 0x000265A6
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x000283AF File Offset: 0x000265AF
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x000283B7 File Offset: 0x000265B7
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000283C0 File Offset: 0x000265C0
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x000283C8 File Offset: 0x000265C8
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldSerialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000283D1 File Offset: 0x000265D1
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000283D9 File Offset: 0x000265D9
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldDeserialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000283E2 File Offset: 0x000265E2
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x000283EA File Offset: 0x000265EA
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> GetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000283F3 File Offset: 0x000265F3
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x000283FB File Offset: 0x000265FB
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> SetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00028404 File Offset: 0x00026604
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00028415 File Offset: 0x00026615
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0002841D File Offset: 0x0002661D
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00028426 File Offset: 0x00026626
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0002842E File Offset: 0x0002662E
		public bool? ItemIsReference { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00028437 File Offset: 0x00026637
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0002843F File Offset: 0x0002663F
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00028448 File Offset: 0x00026648
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00028450 File Offset: 0x00026650
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002845C File Offset: 0x0002665C
		[NullableContext(1)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x04000568 RID: 1384
		internal Required? _required;

		// Token: 0x04000569 RID: 1385
		internal bool _hasExplicitDefaultValue;

		// Token: 0x0400056A RID: 1386
		private object _defaultValue;

		// Token: 0x0400056B RID: 1387
		private bool _hasGeneratedDefaultValue;

		// Token: 0x0400056C RID: 1388
		private string _propertyName;

		// Token: 0x0400056D RID: 1389
		internal bool _skipPropertyNameEscape;

		// Token: 0x0400056E RID: 1390
		private Type _propertyType;
	}
}

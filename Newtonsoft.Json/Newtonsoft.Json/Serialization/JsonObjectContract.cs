using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000121 RID: 289
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00027E16 File Offset: 0x00026016
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00027E1E File Offset: 0x0002601E
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00027E27 File Offset: 0x00026027
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00027E2F File Offset: 0x0002602F
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00027E38 File Offset: 0x00026038
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00027E40 File Offset: 0x00026040
		public Required? ItemRequired { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00027E49 File Offset: 0x00026049
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00027E51 File Offset: 0x00026051
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00027E5A File Offset: 0x0002605A
		[Nullable(1)]
		public JsonPropertyCollection Properties
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00027E62 File Offset: 0x00026062
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00027E83 File Offset: 0x00026083
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00027E8B File Offset: 0x0002608B
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00027E94 File Offset: 0x00026094
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00027E9C File Offset: 0x0002609C
		[Nullable(new byte[] { 2, 1 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00027EA5 File Offset: 0x000260A5
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00027EAD File Offset: 0x000260AD
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00027EB6 File Offset: 0x000260B6
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00027EBE File Offset: 0x000260BE
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00027EC7 File Offset: 0x000260C7
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00027ECF File Offset: 0x000260CF
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = value != null && typeof(JToken).IsAssignableFrom(value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00027EFA File Offset: 0x000260FA
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00027F02 File Offset: 0x00026102
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> ExtensionDataNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00027F0C File Offset: 0x0002610C
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								DefaultValueHandling defaultValueHandling2 = DefaultValueHandling.Populate;
								if (!((defaultValueHandling.GetValueOrDefault() == defaultValueHandling2) & (defaultValueHandling != null)))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00027FF8 File Offset: 0x000261F8
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00028019 File Offset: 0x00026219
		[NullableContext(1)]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x04000560 RID: 1376
		internal bool ExtensionDataIsJToken;

		// Token: 0x04000561 RID: 1377
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x04000562 RID: 1378
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x04000563 RID: 1379
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x04000564 RID: 1380
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x04000565 RID: 1381
		private Type _extensionDataValueType;
	}
}

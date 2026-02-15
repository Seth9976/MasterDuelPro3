using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000113 RID: 275
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000271C6 File Offset: 0x000253C6
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x000271CE File Offset: 0x000253CE
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00027208 File Offset: 0x00025408
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00027210 File Offset: 0x00025410
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00027218 File Offset: 0x00025418
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00027221 File Offset: 0x00025421
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00027229 File Offset: 0x00025429
		public bool? ItemIsReference { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00027232 File Offset: 0x00025432
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0002723A File Offset: 0x0002543A
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00027243 File Offset: 0x00025443
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0002724B File Offset: 0x0002544B
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x060007E7 RID: 2023 RVA: 0x00027254 File Offset: 0x00025454
		[NullableContext(1)]
		internal JsonContainerContract(Type underlyingType)
			: base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x0400051C RID: 1308
		private JsonContract _itemContract;

		// Token: 0x0400051D RID: 1309
		private JsonContract _finalItemContract;
	}
}

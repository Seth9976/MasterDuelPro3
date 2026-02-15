using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000027 RID: 39
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000331E File Offset: 0x0000151E
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003326 File Offset: 0x00001526
		public Type ItemConverterType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000332F File Offset: 0x0000152F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003337 File Offset: 0x00001537
		[Nullable(new byte[] { 2, 1 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003340 File Offset: 0x00001540
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003348 File Offset: 0x00001548
		public Type NamingStrategyType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003351 File Offset: 0x00001551
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003359 File Offset: 0x00001559
		[Nullable(new byte[] { 2, 1 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003362 File Offset: 0x00001562
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000336F File Offset: 0x0000156F
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000337D File Offset: 0x0000157D
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000338A File Offset: 0x0000158A
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003398 File Offset: 0x00001598
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000033A5 File Offset: 0x000015A5
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000033B3 File Offset: 0x000015B3
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000033C0 File Offset: 0x000015C0
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000033CE File Offset: 0x000015CE
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000033DB File Offset: 0x000015DB
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000033E9 File Offset: 0x000015E9
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000033F6 File Offset: 0x000015F6
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003404 File Offset: 0x00001604
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003411 File Offset: 0x00001611
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000341F File Offset: 0x0000161F
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000342C File Offset: 0x0000162C
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000343A File Offset: 0x0000163A
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003442 File Offset: 0x00001642
		public string PropertyName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000344B File Offset: 0x0000164B
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003458 File Offset: 0x00001658
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003466 File Offset: 0x00001666
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003473 File Offset: 0x00001673
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003481 File Offset: 0x00001681
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000348E File Offset: 0x0000168E
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002050 File Offset: 0x00000250
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000349C File Offset: 0x0000169C
		[NullableContext(1)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x04000051 RID: 81
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000052 RID: 82
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000053 RID: 83
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000054 RID: 84
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000055 RID: 85
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000056 RID: 86
		internal bool? _isReference;

		// Token: 0x04000057 RID: 87
		internal int? _order;

		// Token: 0x04000058 RID: 88
		internal Required? _required;

		// Token: 0x04000059 RID: 89
		internal bool? _itemIsReference;

		// Token: 0x0400005A RID: 90
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x0400005B RID: 91
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}

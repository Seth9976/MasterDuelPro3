using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023A0 File Offset: 0x000005A0
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023A8 File Offset: 0x000005A8
		public string Id { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023B1 File Offset: 0x000005B1
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023B9 File Offset: 0x000005B9
		public string Title { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023C2 File Offset: 0x000005C2
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023CA File Offset: 0x000005CA
		public string Description { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023D3 File Offset: 0x000005D3
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023DB File Offset: 0x000005DB
		public Type ItemConverterType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000023EC File Offset: 0x000005EC
		[Nullable(new byte[] { 2, 1 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000023FD File Offset: 0x000005FD
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000240D File Offset: 0x0000060D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002415 File Offset: 0x00000615
		[Nullable(new byte[] { 2, 1 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000242D File Offset: 0x0000062D
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002436 File Offset: 0x00000636
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002443 File Offset: 0x00000643
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002451 File Offset: 0x00000651
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000245E File Offset: 0x0000065E
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000246C File Offset: 0x0000066C
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002479 File Offset: 0x00000679
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002494 File Offset: 0x00000694
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002050 File Offset: 0x00000250
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024A2 File Offset: 0x000006A2
		[NullableContext(1)]
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x04000031 RID: 49
		internal bool? _isReference;

		// Token: 0x04000032 RID: 50
		internal bool? _itemIsReference;

		// Token: 0x04000033 RID: 51
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000034 RID: 52
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x04000035 RID: 53
		private Type _namingStrategyType;

		// Token: 0x04000036 RID: 54
		[Nullable(new byte[] { 2, 1 })]
		private object[] _namingStrategyParameters;
	}
}

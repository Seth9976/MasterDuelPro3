using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002FFB File Offset: 0x000011FB
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003003 File Offset: 0x00001203
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000300C File Offset: 0x0000120C
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003019 File Offset: 0x00001219
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003027 File Offset: 0x00001227
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003034 File Offset: 0x00001234
		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				return this._itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003042 File Offset: 0x00001242
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000304F File Offset: 0x0000124F
		public Required ItemRequired
		{
			get
			{
				return this._itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002380 File Offset: 0x00000580
		public JsonObjectAttribute()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000305D File Offset: 0x0000125D
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002397 File Offset: 0x00000597
		[NullableContext(1)]
		public JsonObjectAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x04000043 RID: 67
		private MemberSerialization _memberSerialization;

		// Token: 0x04000044 RID: 68
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000045 RID: 69
		internal Required? _itemRequired;

		// Token: 0x04000046 RID: 70
		internal NullValueHandling? _itemNullValueHandling;
	}
}

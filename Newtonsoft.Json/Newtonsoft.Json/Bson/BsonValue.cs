using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DE RID: 478
	internal class BsonValue : BsonToken
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x00046273 File Offset: 0x00044473
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00046289 File Offset: 0x00044489
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00046291 File Offset: 0x00044491
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400085A RID: 2138
		private readonly object _value;

		// Token: 0x0400085B RID: 2139
		private readonly BsonType _type;
	}
}

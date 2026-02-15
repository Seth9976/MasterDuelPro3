using System;

namespace System.Resources
{
	// Token: 0x020005D3 RID: 1491
	internal struct ResourceLocator
	{
		// Token: 0x06002C1F RID: 11295 RVA: 0x000AE852 File Offset: 0x000ACA52
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000AE862 File Offset: 0x000ACA62
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000AE86A File Offset: 0x000ACA6A
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x000AE872 File Offset: 0x000ACA72
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000AE87B File Offset: 0x000ACA7B
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04001669 RID: 5737
		internal object _value;

		// Token: 0x0400166A RID: 5738
		internal int _dataPos;
	}
}

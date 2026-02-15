using System;

namespace System.Data
{
	// Token: 0x0200000B RID: 11
	internal abstract class AutoIncrementValue
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004183 File Offset: 0x00002383
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000418B File Offset: 0x0000238B
		internal bool Auto { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		internal abstract object Current { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		internal abstract long Seed { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		internal abstract long Step { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000087 RID: 135
		internal abstract Type DataType { get; }

		// Token: 0x06000088 RID: 136
		internal abstract void SetCurrent(object value, IFormatProvider formatProvider);

		// Token: 0x06000089 RID: 137
		internal abstract void SetCurrentAndIncrement(object value);

		// Token: 0x0600008A RID: 138
		internal abstract void MoveAfter();

		// Token: 0x0600008B RID: 139 RVA: 0x00004194 File Offset: 0x00002394
		internal AutoIncrementValue Clone()
		{
			AutoIncrementInt64 autoIncrementInt = ((this is AutoIncrementInt64) ? new AutoIncrementInt64() : new AutoIncrementBigInteger());
			autoIncrementInt.Auto = this.Auto;
			autoIncrementInt.Seed = this.Seed;
			autoIncrementInt.Step = this.Step;
			autoIncrementInt.Current = this.Current;
			return autoIncrementInt;
		}
	}
}

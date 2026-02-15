using System;

namespace System
{
	// Token: 0x020000F1 RID: 241
	internal struct DateTimeRawInfo
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00025162 File Offset: 0x00023362
		internal unsafe void Init(int* numberBuffer)
		{
			this.month = -1;
			this.year = -1;
			this.dayOfWeek = -1;
			this.era = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
			this.fraction = -1.0;
			this.num = numberBuffer;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000251A0 File Offset: 0x000233A0
		internal unsafe void AddNumber(int value)
		{
			ref int ptr = ref *this.num;
			int num = this.numCount;
			this.numCount = num + 1;
			*((ref ptr) + (IntPtr)num * 4) = value;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000251CA File Offset: 0x000233CA
		internal unsafe int GetNumber(int index)
		{
			return this.num[index];
		}

		// Token: 0x04000398 RID: 920
		private unsafe int* num;

		// Token: 0x04000399 RID: 921
		internal int numCount;

		// Token: 0x0400039A RID: 922
		internal int month;

		// Token: 0x0400039B RID: 923
		internal int year;

		// Token: 0x0400039C RID: 924
		internal int dayOfWeek;

		// Token: 0x0400039D RID: 925
		internal int era;

		// Token: 0x0400039E RID: 926
		internal DateTimeParse.TM timeMark;

		// Token: 0x0400039F RID: 927
		internal double fraction;

		// Token: 0x040003A0 RID: 928
		internal bool hasSameDateAndTimeSeparators;
	}
}

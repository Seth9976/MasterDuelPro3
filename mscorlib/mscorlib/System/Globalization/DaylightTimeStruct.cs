using System;

namespace System.Globalization
{
	// Token: 0x0200069B RID: 1691
	internal readonly struct DaylightTimeStruct
	{
		// Token: 0x06003531 RID: 13617 RVA: 0x000CC0E9 File Offset: 0x000CA2E9
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x04001C34 RID: 7220
		public readonly DateTime Start;

		// Token: 0x04001C35 RID: 7221
		public readonly DateTime End;

		// Token: 0x04001C36 RID: 7222
		public readonly TimeSpan Delta;
	}
}

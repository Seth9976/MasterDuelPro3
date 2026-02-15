using System;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B6A RID: 2922
	public class Parameter
	{
		// Token: 0x040091D7 RID: 37335
		public int defaultYear;

		// Token: 0x040091D8 RID: 37336
		public int defaultMonth;

		// Token: 0x040091D9 RID: 37337
		public int defaultDay;

		// Token: 0x040091DA RID: 37338
		public Action<int, int, int> resultCallback;
	}
}

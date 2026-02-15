using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003DB RID: 987
	public interface IStyleValue<T>
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001D7E RID: 7550
		// (set) Token: 0x06001D7F RID: 7551
		T value { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001D80 RID: 7552
		// (set) Token: 0x06001D81 RID: 7553
		StyleKeyword keyword { get; set; }
	}
}

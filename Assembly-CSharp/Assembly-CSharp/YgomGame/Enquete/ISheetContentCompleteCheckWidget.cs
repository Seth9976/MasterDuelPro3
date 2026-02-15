using System;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1C RID: 3100
	public interface ISheetContentCompleteCheckWidget
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600587D RID: 22653
		bool isInputComplete { get; }

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x0600587E RID: 22654
		// (remove) Token: 0x0600587F RID: 22655
		event Action onChangeComplete;
	}
}

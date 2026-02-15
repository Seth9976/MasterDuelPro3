using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AC RID: 684
	internal interface IPanelRenderer
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001270 RID: 4720
		// (set) Token: 0x06001271 RID: 4721
		bool forceGammaRendering { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001272 RID: 4722
		// (set) Token: 0x06001273 RID: 4723
		uint vertexBudget { get; set; }

		// Token: 0x06001274 RID: 4724
		void Reset();

		// Token: 0x06001275 RID: 4725
		void Render();
	}
}

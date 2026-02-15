using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200008A RID: 138
	internal interface ITweenValue
	{
		// Token: 0x0600055B RID: 1371
		void TweenValue(float floatPercentage);

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600055C RID: 1372
		bool ignoreTimeScale { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600055D RID: 1373
		float duration { get; }

		// Token: 0x0600055E RID: 1374
		bool ValidTarget();
	}
}

using System;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000072 RID: 114
	public struct CircleOptions : IPlugOptions
	{
		// Token: 0x060002AF RID: 687 RVA: 0x00009D4C File Offset: 0x00007F4C
		public void Reset()
		{
			this.initialized = false;
			this.startValueDegrees = (this.endValueDegrees = 0f);
			this.relativeCenter = false;
			this.snapping = false;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009D84 File Offset: 0x00007F84
		public void Initialize(Vector2 startValue, Vector2 endValue)
		{
			this.initialized = true;
			this.center = endValue;
			if (this.relativeCenter)
			{
				this.center = startValue + this.center;
			}
			this.radius = Vector2.Distance(this.center, startValue);
			Vector2 vector = startValue - this.center;
			this.startValueDegrees = Mathf.Atan2(vector.x, vector.y) * 57.29578f;
		}

		// Token: 0x04000157 RID: 343
		public float endValueDegrees;

		// Token: 0x04000158 RID: 344
		public bool relativeCenter;

		// Token: 0x04000159 RID: 345
		public bool snapping;

		// Token: 0x0400015A RID: 346
		internal Vector2 center;

		// Token: 0x0400015B RID: 347
		internal float radius;

		// Token: 0x0400015C RID: 348
		internal float startValueDegrees;

		// Token: 0x0400015D RID: 349
		internal bool initialized;
	}
}

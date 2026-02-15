using System;

namespace UnityEngine.UI
{
	// Token: 0x0200000F RID: 15
	public interface IClippable
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83
		GameObject gameObject { get; }

		// Token: 0x06000054 RID: 84
		void RecalculateClipping();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85
		RectTransform rectTransform { get; }

		// Token: 0x06000056 RID: 86
		void Cull(Rect clipRect, bool validRect);

		// Token: 0x06000057 RID: 87
		void SetClipRect(Rect value, bool validRect);

		// Token: 0x06000058 RID: 88
		void SetClipSoftness(Vector2 clipSoftness);
	}
}

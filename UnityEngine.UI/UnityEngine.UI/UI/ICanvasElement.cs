using System;

namespace UnityEngine.UI
{
	// Token: 0x02000009 RID: 9
	public interface ICanvasElement
	{
		// Token: 0x0600001C RID: 28
		void Rebuild(CanvasUpdate executing);

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29
		Transform transform { get; }

		// Token: 0x0600001E RID: 30
		void LayoutComplete();

		// Token: 0x0600001F RID: 31
		void GraphicUpdateComplete();

		// Token: 0x06000020 RID: 32
		bool IsDestroyed();
	}
}

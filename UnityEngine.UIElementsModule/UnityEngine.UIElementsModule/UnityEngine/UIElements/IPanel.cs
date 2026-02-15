using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000286 RID: 646
	public interface IPanel : IDisposable
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001116 RID: 4374
		VisualElement visualTree { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001117 RID: 4375
		EventDispatcher dispatcher { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001118 RID: 4376
		ContextType contextType { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001119 RID: 4377
		FocusController focusController { get; }

		// Token: 0x0600111A RID: 4378
		VisualElement Pick(Vector2 point);
	}
}

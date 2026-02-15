using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E2 RID: 482
	public interface IKeyboardEvent
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D83 RID: 3459
		EventModifiers modifiers { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D84 RID: 3460
		char character { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D85 RID: 3461
		KeyCode keyCode { get; }
	}
}

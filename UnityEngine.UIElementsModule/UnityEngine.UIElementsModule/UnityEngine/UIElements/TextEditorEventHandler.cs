using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E5 RID: 229
	internal class TextEditorEventHandler
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0002196A File Offset: 0x0001FB6A
		protected TextEditorEventHandler(TextElement textElement, TextEditingUtilities editingUtilities)
		{
			this.textElement = textElement;
			this.editingUtilities = editingUtilities;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void RegisterCallbacksOnTarget(VisualElement target)
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void UnregisterCallbacksFromTarget(VisualElement target)
		{
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void HandleEventBubbleUp(EventBase evt)
		{
		}

		// Token: 0x04000456 RID: 1110
		protected TextElement textElement;

		// Token: 0x04000457 RID: 1111
		protected TextEditingUtilities editingUtilities;
	}
}

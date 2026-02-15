using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018F RID: 399
	internal interface IDragAndDrop
	{
		// Token: 0x06000BC9 RID: 3017
		void StartDrag(StartDragArgs args, Vector3 pointerPosition);

		// Token: 0x06000BCA RID: 3018
		void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000BCB RID: 3019
		void AcceptDrag();

		// Token: 0x06000BCC RID: 3020
		void DragCleanup();

		// Token: 0x06000BCD RID: 3021
		void SetVisualMode(DragVisualMode visualMode);

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BCE RID: 3022
		DragAndDropData data { get; }
	}
}

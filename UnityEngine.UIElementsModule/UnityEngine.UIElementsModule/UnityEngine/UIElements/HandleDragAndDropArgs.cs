using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000198 RID: 408
	public readonly struct HandleDragAndDropArgs
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x00038BD1 File Offset: 0x00036DD1
		internal HandleDragAndDropArgs(Vector2 position, DragAndDropArgs dragAndDropArgs)
		{
			this.<position>k__BackingField = position;
			this.m_DragAndDropArgs = dragAndDropArgs;
		}

		// Token: 0x04000790 RID: 1936
		private readonly DragAndDropArgs m_DragAndDropArgs;
	}
}

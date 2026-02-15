using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018A RID: 394
	internal static class DragAndDropUtility
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0003826C File Offset: 0x0003646C
		internal static IDragAndDrop GetDragAndDrop(IPanel panel)
		{
			bool flag = panel.contextType == ContextType.Player;
			IDragAndDrop dragAndDrop2;
			if (flag)
			{
				IDragAndDrop dragAndDrop;
				if ((dragAndDrop = DragAndDropUtility.s_DragAndDropPlayMode) == null)
				{
					dragAndDrop = (DragAndDropUtility.s_DragAndDropPlayMode = new DefaultDragAndDropClient());
				}
				dragAndDrop2 = dragAndDrop;
			}
			else
			{
				IDragAndDrop dragAndDrop3;
				if ((dragAndDrop3 = DragAndDropUtility.s_DragAndDropEditor) == null)
				{
					IDragAndDrop dragAndDrop5;
					if (DragAndDropUtility.s_MakeDragAndDropClientFunc == null)
					{
						IDragAndDrop dragAndDrop4 = new DefaultDragAndDropClient();
						dragAndDrop5 = dragAndDrop4;
					}
					else
					{
						dragAndDrop5 = DragAndDropUtility.s_MakeDragAndDropClientFunc();
					}
					dragAndDrop3 = (DragAndDropUtility.s_DragAndDropEditor = dragAndDrop5);
				}
				dragAndDrop2 = dragAndDrop3;
			}
			return dragAndDrop2;
		}

		// Token: 0x0400076B RID: 1899
		private static Func<IDragAndDrop> s_MakeDragAndDropClientFunc;

		// Token: 0x0400076C RID: 1900
		private static IDragAndDrop s_DragAndDropEditor;

		// Token: 0x0400076D RID: 1901
		private static IDragAndDrop s_DragAndDropPlayMode;
	}
}

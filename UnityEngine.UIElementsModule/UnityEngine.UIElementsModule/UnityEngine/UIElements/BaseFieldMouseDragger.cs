using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000246 RID: 582
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BaseFieldMouseDragger
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x00043449 File Offset: 0x00041649
		public void SetDragZone(VisualElement dragElement)
		{
			this.SetDragZone(dragElement, new Rect(0f, 0f, -1f, -1f));
		}

		// Token: 0x06000F9C RID: 3996
		public abstract void SetDragZone(VisualElement dragElement, Rect hotZone);
	}
}

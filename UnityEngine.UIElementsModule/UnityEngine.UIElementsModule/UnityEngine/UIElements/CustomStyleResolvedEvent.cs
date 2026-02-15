using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000235 RID: 565
	[EventCategory(EventCategory.Style)]
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x000430B4 File Offset: 0x000412B4
		static CustomStyleResolvedEvent()
		{
			EventBase<CustomStyleResolvedEvent>.SetCreateFunction(() => new CustomStyleResolvedEvent());
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x000430D0 File Offset: 0x000412D0
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement elementTarget = base.elementTarget;
				return (elementTarget != null) ? elementTarget.customStyle : null;
			}
		}
	}
}

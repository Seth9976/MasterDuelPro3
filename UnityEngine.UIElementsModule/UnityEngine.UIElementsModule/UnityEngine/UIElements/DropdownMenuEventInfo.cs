using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A2 RID: 418
	public class DropdownMenuEventInfo
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0003B328 File Offset: 0x00039528
		public DropdownMenuEventInfo(EventBase e)
		{
			IMouseEvent mouseEvent = e as IMouseEvent;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.<mousePosition>k__BackingField = mouseEvent.mousePosition;
				this.<localMousePosition>k__BackingField = mouseEvent.localMousePosition;
				this.<modifiers>k__BackingField = mouseEvent.modifiers;
				this.<character>k__BackingField = '\0';
				this.<keyCode>k__BackingField = KeyCode.None;
			}
			else
			{
				IPointerEvent pointerEvent = e as IPointerEvent;
				bool flag2 = pointerEvent != null;
				if (flag2)
				{
					this.<mousePosition>k__BackingField = pointerEvent.position;
					this.<localMousePosition>k__BackingField = pointerEvent.localPosition;
					this.<modifiers>k__BackingField = pointerEvent.modifiers;
					this.<character>k__BackingField = '\0';
					this.<keyCode>k__BackingField = KeyCode.None;
				}
				else
				{
					IKeyboardEvent keyboardEvent = e as IKeyboardEvent;
					bool flag3 = keyboardEvent != null;
					if (flag3)
					{
						this.<character>k__BackingField = keyboardEvent.character;
						this.<keyCode>k__BackingField = keyboardEvent.keyCode;
						this.<modifiers>k__BackingField = keyboardEvent.modifiers;
						this.<mousePosition>k__BackingField = Vector2.zero;
						this.<localMousePosition>k__BackingField = Vector2.zero;
					}
				}
			}
		}
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BE RID: 446
	public class ValidateCommandEvent : CommandEventBase<ValidateCommandEvent>
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x0003C1A6 File Offset: 0x0003A3A6
		static ValidateCommandEvent()
		{
			EventBase<ValidateCommandEvent>.SetCreateFunction(() => new ValidateCommandEvent());
		}
	}
}

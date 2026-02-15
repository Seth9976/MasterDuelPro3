using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C0 RID: 448
	public class ExecuteCommandEvent : CommandEventBase<ExecuteCommandEvent>
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x0003C1DB File Offset: 0x0003A3DB
		static ExecuteCommandEvent()
		{
			EventBase<ExecuteCommandEvent>.SetCreateFunction(() => new ExecuteCommandEvent());
		}
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E7 RID: 487
	public class KeyUpEvent : KeyboardEventBase<KeyUpEvent>
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003F320 File Offset: 0x0003D520
		static KeyUpEvent()
		{
			EventBase<KeyUpEvent>.SetCreateFunction(() => new KeyUpEvent());
		}
	}
}

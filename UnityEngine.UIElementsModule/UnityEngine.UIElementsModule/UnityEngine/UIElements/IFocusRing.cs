using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024A RID: 586
	public interface IFocusRing
	{
		// Token: 0x06000FC8 RID: 4040
		FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e);

		// Token: 0x06000FC9 RID: 4041
		Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction);
	}
}

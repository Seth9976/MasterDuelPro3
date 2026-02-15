using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002B RID: 43
	[UxmlObject]
	public abstract class CustomBinding : Binding
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000073F8 File Offset: 0x000055F8
		protected internal virtual BindingResult Update(in BindingContext context)
		{
			return new BindingResult(BindingStatus.Success, null);
		}
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000043 RID: 67
	public interface INotifyBindablePropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001FD RID: 509
		// (remove) Token: 0x060001FE RID: 510
		event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
	}
}

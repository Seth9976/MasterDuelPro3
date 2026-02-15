using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DD RID: 221
	public interface INotifyValueChanged<T>
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006BB RID: 1723
		// (set) Token: 0x060006BC RID: 1724
		T value { get; set; }

		// Token: 0x060006BD RID: 1725
		void SetValueWithoutNotify(T newValue);
	}
}

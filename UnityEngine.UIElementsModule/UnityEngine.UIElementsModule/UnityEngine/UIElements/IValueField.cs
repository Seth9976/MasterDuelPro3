using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000155 RID: 341
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public interface IValueField<T>
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A38 RID: 2616
		// (set) Token: 0x06000A39 RID: 2617
		T value { get; set; }

		// Token: 0x06000A3A RID: 2618
		void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, T startValue);

		// Token: 0x06000A3B RID: 2619
		void StartDragging();

		// Token: 0x06000A3C RID: 2620
		void StopDragging();
	}
}

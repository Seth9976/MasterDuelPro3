using System;

namespace UnityEngine
{
	// Token: 0x020001A5 RID: 421
	[AttributeUsage(AttributeTargets.Enum)]
	public sealed class InspectorOrderAttribute : PropertyAttribute
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000238DB File Offset: 0x00021ADB
		internal InspectorSort m_inspectorSort { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x000238E3 File Offset: 0x00021AE3
		internal InspectorSortDirection m_sortDirection { get; }
	}
}

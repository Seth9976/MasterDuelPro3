using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020005A0 RID: 1440
	[NativeHeader("Modules/UIElements/Core/Layout/Native/LayoutNative.h")]
	internal static class LayoutNative
	{
		// Token: 0x06002702 RID: 9986
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CalculateLayout(IntPtr node, float parentWidth, float parentHeight, int parentDirection, IntPtr state, IntPtr exceptionGCHandle);

		// Token: 0x06002703 RID: 9987 RVA: 0x0009B654 File Offset: 0x00099854
		[RequiredByNativeCode]
		private unsafe static void LayoutLog_Internal(IntPtr nodePtr, LayoutNative.LayoutLogEventType type, string message)
		{
			LayoutNative.LayoutLogData data = new LayoutNative.LayoutLogData();
			data.node = *(LayoutNode*)(void*)nodePtr;
			data.message = message;
			data.eventType = type;
			LayoutNative.onLayoutLog(data);
		}

		// Token: 0x0400142B RID: 5163
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<LayoutNative.LayoutLogData> onLayoutLog;

		// Token: 0x020005A1 RID: 1441
		internal enum LayoutLogEventType
		{
			// Token: 0x0400142D RID: 5165
			None,
			// Token: 0x0400142E RID: 5166
			Error,
			// Token: 0x0400142F RID: 5167
			Measure,
			// Token: 0x04001430 RID: 5168
			Layout,
			// Token: 0x04001431 RID: 5169
			CacheUsage,
			// Token: 0x04001432 RID: 5170
			BeginLayout,
			// Token: 0x04001433 RID: 5171
			EndLayout
		}

		// Token: 0x020005A2 RID: 1442
		internal class LayoutLogData
		{
			// Token: 0x04001434 RID: 5172
			public LayoutNode node;

			// Token: 0x04001435 RID: 5173
			public LayoutNative.LayoutLogEventType eventType;

			// Token: 0x04001436 RID: 5174
			public string message;
		}
	}
}

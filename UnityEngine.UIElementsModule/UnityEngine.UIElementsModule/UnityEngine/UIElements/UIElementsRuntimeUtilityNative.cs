using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027C RID: 636
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	[NativeHeader("Modules/UIElements/Core/Native/UIElementsRuntimeUtilityNative.h")]
	internal static class UIElementsRuntimeUtilityNative
	{
		// Token: 0x06001100 RID: 4352 RVA: 0x00048F07 File Offset: 0x00047107
		[RequiredByNativeCode]
		public static void UpdatePanels()
		{
			Action updatePanelsCallback = UIElementsRuntimeUtilityNative.UpdatePanelsCallback;
			if (updatePanelsCallback != null)
			{
				updatePanelsCallback();
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00048F1B File Offset: 0x0004711B
		[RequiredByNativeCode]
		public static void RepaintPanels(bool onlyOffscreen)
		{
			Action<bool> repaintPanelsCallback = UIElementsRuntimeUtilityNative.RepaintPanelsCallback;
			if (repaintPanelsCallback != null)
			{
				repaintPanelsCallback(onlyOffscreen);
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00048F30 File Offset: 0x00047130
		[RequiredByNativeCode]
		public static void RenderOffscreenPanels()
		{
			Action renderOffscreenPanelsCallback = UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback;
			if (renderOffscreenPanelsCallback != null)
			{
				renderOffscreenPanelsCallback();
			}
		}

		// Token: 0x06001103 RID: 4355
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterPlayerloopCallback();

		// Token: 0x06001104 RID: 4356
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterPlayerloopCallback();

		// Token: 0x06001105 RID: 4357
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VisualElementCreation();

		// Token: 0x040009C9 RID: 2505
		internal static Action UpdatePanelsCallback;

		// Token: 0x040009CA RID: 2506
		internal static Action<bool> RepaintPanelsCallback;

		// Token: 0x040009CB RID: 2507
		internal static Action RenderOffscreenPanelsCallback;
	}
}

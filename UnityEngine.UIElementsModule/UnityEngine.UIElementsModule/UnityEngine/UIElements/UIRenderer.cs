using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000277 RID: 631
	[NativeType(Header = "Modules/UIElements/Core/Native/Renderer/UIRenderer.h")]
	public sealed class UIRenderer : Renderer
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x00048D58 File Offset: 0x00046F58
		internal void SetNativeData(int safeFrameIndex, int cmdListIndex, Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<UIRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UIRenderer.SetNativeData_Injected(intPtr, safeFrameIndex, cmdListIndex, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00048D84 File Offset: 0x00046F84
		[RequiredByNativeCode]
		private static void OnRenderNodeExecute(UIRenderer renderer, int safeFrameIndex, int cmdListIndex)
		{
			bool flag = renderer.skipRendering;
			if (!flag)
			{
				List<CommandList>[] commandLists = renderer.commandLists;
				List<CommandList> cmdList = ((commandLists != null) ? commandLists[safeFrameIndex] : null);
				bool flag2 = cmdList != null && cmdListIndex < cmdList.Count;
				if (flag2)
				{
					CommandList commandList = cmdList[cmdListIndex];
					if (commandList != null)
					{
						commandList.Execute();
					}
				}
			}
		}

		// Token: 0x060010F5 RID: 4341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNativeData_Injected(IntPtr _unity_self, int safeFrameIndex, int cmdListIndex, IntPtr mat);

		// Token: 0x04000994 RID: 2452
		internal volatile List<CommandList>[] commandLists;

		// Token: 0x04000995 RID: 2453
		internal volatile bool skipRendering;
	}
}

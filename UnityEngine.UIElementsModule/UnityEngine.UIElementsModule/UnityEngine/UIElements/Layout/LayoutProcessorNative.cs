using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020005A5 RID: 1445
	internal class LayoutProcessorNative : ILayoutProcessor
	{
		// Token: 0x06002707 RID: 9991 RVA: 0x0009B7AC File Offset: 0x000999AC
		unsafe void ILayoutProcessor.CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection)
		{
			IntPtr pNode = (IntPtr)((void*)(&node));
			IntPtr exceptionPlaceHolder = IntPtr.Zero;
			fixed (LayoutState* ptr = &this.m_State)
			{
				void* ptrState = (void*)ptr;
				IntPtr pState = (IntPtr)ptrState;
				LayoutNative.CalculateLayout(pNode, parentWidth, parentHeight, (int)parentDirection, pState, (IntPtr)((void*)(&exceptionPlaceHolder)));
				bool flag = exceptionPlaceHolder != IntPtr.Zero;
				if (flag)
				{
					GCHandle handle = GCHandle.FromIntPtr(exceptionPlaceHolder);
					Exception e = handle.Target as Exception;
					handle.Free();
					this.m_State.error = false;
					ExceptionDispatchInfo edi = ExceptionDispatchInfo.Capture(e);
					edi.Throw();
				}
			}
		}

		// Token: 0x04001453 RID: 5203
		private LayoutState m_State = LayoutState.Default;
	}
}

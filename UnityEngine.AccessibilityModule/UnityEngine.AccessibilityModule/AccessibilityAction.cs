using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityAction.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class AccessibilityAction : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				AccessibilityAction.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000209F File Offset: 0x0000029F
		public Func<bool> activated { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020A8 File Offset: 0x000002A8
		[RequiredByNativeCode]
		private bool Internal_InvokeActivated()
		{
			return this.activated != null && this.activated();
		}

		// Token: 0x04000001 RID: 1
		private IntPtr m_Ptr;
	}
}

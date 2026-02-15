using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A0 RID: 160
	[NativeHeader("Runtime/Export/Bootstrap/BootConfig.bindings.h")]
	internal class BootConfigData
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000064D4 File Offset: 0x000046D4
		[RequiredByNativeCode]
		private static BootConfigData WrapBootConfigData(IntPtr nativeHandle)
		{
			return new BootConfigData(nativeHandle);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000064EC File Offset: 0x000046EC
		private BootConfigData(IntPtr nativeHandle)
		{
			bool flag = nativeHandle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("native handle can not be null");
			}
			this.m_Ptr = nativeHandle;
		}

		// Token: 0x040001EE RID: 494
		private IntPtr m_Ptr;
	}
}

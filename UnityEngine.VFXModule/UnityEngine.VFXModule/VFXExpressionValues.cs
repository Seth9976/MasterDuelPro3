using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000005 RID: 5
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class VFXExpressionValues
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021A5 File Offset: 0x000003A5
		private VFXExpressionValues()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			return new VFXExpressionValues
			{
				m_Ptr = ptr
			};
		}

		// Token: 0x04000009 RID: 9
		internal IntPtr m_Ptr;
	}
}

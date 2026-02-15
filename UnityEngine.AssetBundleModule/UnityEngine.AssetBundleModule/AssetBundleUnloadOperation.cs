using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUnloadOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleUnloadOperation : AsyncOperation
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000026D8 File Offset: 0x000008D8
		[NativeMethod("WaitForCompletion")]
		public void WaitForCompletion()
		{
			IntPtr intPtr = AssetBundleUnloadOperation.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AssetBundleUnloadOperation.WaitForCompletion_Injected(intPtr);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002643 File Offset: 0x00000843
		private AssetBundleUnloadOperation(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitForCompletion_Injected(IntPtr _unity_self);

		// Token: 0x02000009 RID: 9
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000032 RID: 50 RVA: 0x000026FA File Offset: 0x000008FA
			public static AssetBundleUnloadOperation ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleUnloadOperation(ptr);
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00002656 File Offset: 0x00000856
			public static IntPtr ConvertToNative(AssetBundleUnloadOperation assetBundleUnloadOperation)
			{
				return assetBundleUnloadOperation.m_Ptr;
			}
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000261C File Offset: 0x0000081C
		public AssetBundle assetBundle
		{
			[NativeMethod("GetAssetBundleBlocking")]
			get
			{
				IntPtr intPtr = AssetBundleCreateRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AssetBundle>(AssetBundleCreateRequest.get_assetBundle_Injected(intPtr));
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002643 File Offset: 0x00000843
		private AssetBundleCreateRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_assetBundle_Injected(IntPtr _unity_self);

		// Token: 0x02000004 RID: 4
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000025 RID: 37 RVA: 0x0000264E File Offset: 0x0000084E
			public static AssetBundleCreateRequest ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleCreateRequest(ptr);
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002656 File Offset: 0x00000856
			public static IntPtr ConvertToNative(AssetBundleCreateRequest assetBundleCreateRequest)
			{
				return assetBundleCreateRequest.m_Ptr;
			}
		}
	}
}

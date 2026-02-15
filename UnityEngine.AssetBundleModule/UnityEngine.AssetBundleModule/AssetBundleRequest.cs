using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRequest : ResourceRequest
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002660 File Offset: 0x00000860
		[NativeMethod("GetLoadedAsset")]
		protected override Object GetResult()
		{
			IntPtr intPtr = AssetBundleRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(AssetBundleRequest.GetResult_Injected(intPtr));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002688 File Offset: 0x00000888
		public new Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026A0 File Offset: 0x000008A0
		public Object[] allAssets
		{
			[NativeMethod("GetAllLoadedAssets")]
			get
			{
				IntPtr intPtr = AssetBundleRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AssetBundleRequest.get_allAssets_Injected(intPtr);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026C2 File Offset: 0x000008C2
		private AssetBundleRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetResult_Injected(IntPtr _unity_self);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] get_allAssets_Injected(IntPtr _unity_self);

		// Token: 0x02000007 RID: 7
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600002D RID: 45 RVA: 0x000026CD File Offset: 0x000008CD
			public static AssetBundleRequest ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleRequest(ptr);
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002656 File Offset: 0x00000856
			public static IntPtr ConvertToNative(AssetBundleRequest request)
			{
				return request.m_Ptr;
			}
		}
	}
}

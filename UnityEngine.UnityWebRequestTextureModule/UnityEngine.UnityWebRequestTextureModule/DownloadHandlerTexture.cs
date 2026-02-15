using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityWebRequestTexture/Public/DownloadHandlerTexture.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerTexture : DownloadHandler
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		private static IntPtr Create([Unmarshalled] DownloadHandlerTexture obj, DownloadedTextureParams parameters)
		{
			return DownloadHandlerTexture.Create_Injected(obj, ref parameters);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CD File Offset: 0x000002CD
		private void InternalCreateTexture(DownloadedTextureParams parameters)
		{
			this.m_Ptr = DownloadHandlerTexture.Create(this, parameters);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		public DownloadHandlerTexture(bool readable)
		{
			DownloadedTextureParams parameters = DownloadedTextureParams.Default;
			parameters.readable = readable;
			this.InternalCreateTexture(parameters);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212A File Offset: 0x0000032A
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public Texture2D texture
		{
			get
			{
				return this.InternalGetTextureNative();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002158 File Offset: 0x00000358
		[NativeThrows]
		private Texture2D InternalGetTextureNative()
		{
			IntPtr intPtr = DownloadHandlerTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(DownloadHandlerTexture.InternalGetTextureNative_Injected(intPtr));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		public static Texture2D GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerTexture>(www).texture;
		}

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerTexture obj, [In] ref DownloadedTextureParams parameters);

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTextureNative_Injected(IntPtr _unity_self);

		// Token: 0x04000008 RID: 8
		private NativeArray<byte> m_NativeData;

		// Token: 0x02000005 RID: 5
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600000E RID: 14 RVA: 0x0000219D File Offset: 0x0000039D
			public static IntPtr ConvertToNative(DownloadHandlerTexture handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}

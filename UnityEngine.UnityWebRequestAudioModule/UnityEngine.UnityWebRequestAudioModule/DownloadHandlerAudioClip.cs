using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/UnityWebRequestAudio/Public/DownloadHandlerAudioClip.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAudioClip : DownloadHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private unsafe static IntPtr Create([Unmarshalled] DownloadHandlerAudioClip obj, string url, AudioType audioType)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = DownloadHandlerAudioClip.Create_Injected(obj, ref managedSpanWrapper, audioType);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		private void InternalCreateAudioClip(string url, AudioType audioType)
		{
			this.m_Ptr = DownloadHandlerAudioClip.Create(this, url, audioType);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020B9 File Offset: 0x000002B9
		public DownloadHandlerAudioClip(string url, AudioType audioType)
		{
			this.InternalCreateAudioClip(url, audioType);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EA File Offset: 0x000002EA
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for audio clips");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		[NativeThrows]
		public AudioClip audioClip
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioClip>(DownloadHandlerAudioClip.get_audioClip_Injected(intPtr));
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public static AudioClip GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAudioClip>(www).audioClip;
		}

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerAudioClip obj, ref ManagedSpanWrapper url, AudioType audioType);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_audioClip_Injected(IntPtr _unity_self);

		// Token: 0x04000001 RID: 1
		private NativeArray<byte> m_NativeData;

		// Token: 0x02000003 RID: 3
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002155 File Offset: 0x00000355
			public static IntPtr ConvertToNative(DownloadHandlerAudioClip handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}

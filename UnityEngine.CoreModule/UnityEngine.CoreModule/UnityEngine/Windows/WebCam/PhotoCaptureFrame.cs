using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000214 RID: 532
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00029CBE File Offset: 0x00027EBE
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00029CC6 File Offset: 0x00027EC6
		public int dataLength { get; private set; }

		// Token: 0x17000321 RID: 801
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00029CCF File Offset: 0x00027ECF
		private bool hasLocationData
		{
			[CompilerGenerated]
			set
			{
				this.<hasLocationData>k__BackingField = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00029CD8 File Offset: 0x00027ED8
		private CapturePixelFormat pixelFormat
		{
			[CompilerGenerated]
			set
			{
				this.<pixelFormat>k__BackingField = value;
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00029CE4 File Offset: 0x00027EE4
		[ThreadAndSerializationSafe]
		private int GetDataLength()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetDataLength_Injected(intPtr);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00029D08 File Offset: 0x00027F08
		[ThreadAndSerializationSafe]
		private bool GetHasLocationData()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetHasLocationData_Injected(intPtr);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00029D2C File Offset: 0x00027F2C
		[ThreadAndSerializationSafe]
		private CapturePixelFormat GetCapturePixelFormat()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetCapturePixelFormat_Injected(intPtr);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00029D50 File Offset: 0x00027F50
		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00029DA0 File Offset: 0x00027FA0
		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00029DE4 File Offset: 0x00027FE4
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Dispose")]
		private void Dispose_Internal()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCaptureFrame.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00029E06 File Offset: 0x00028006
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00029E18 File Offset: 0x00028018
		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		// Token: 0x060013FA RID: 5114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataLength_Injected(IntPtr _unity_self);

		// Token: 0x060013FB RID: 5115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHasLocationData_Injected(IntPtr _unity_self);

		// Token: 0x060013FC RID: 5116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CapturePixelFormat GetCapturePixelFormat_Injected(IntPtr _unity_self);

		// Token: 0x060013FD RID: 5117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0400075B RID: 1883
		private IntPtr m_NativePtr;

		// Token: 0x02000215 RID: 533
		internal static class BindingsMarshaller
		{
			// Token: 0x060013FE RID: 5118 RVA: 0x00029E48 File Offset: 0x00028048
			public static IntPtr ConvertToNative(PhotoCaptureFrame photoCaptureFrame)
			{
				return photoCaptureFrame.m_NativePtr;
			}
		}
	}
}

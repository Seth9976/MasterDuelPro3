using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000216 RID: 534
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StaticAccessor("VideoCaptureBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("PlatformDependent/Win/Webcam/VideoCaptureBindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoCapture : IDisposable
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x00029E50 File Offset: 0x00028050
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(long hResult)
		{
			VideoCapture.VideoCaptureResult result = default(VideoCapture.VideoCaptureResult);
			bool flag = hResult == VideoCapture.HR_SUCCESS;
			VideoCapture.CaptureResultType resultType;
			if (flag)
			{
				resultType = VideoCapture.CaptureResultType.Success;
			}
			else
			{
				resultType = VideoCapture.CaptureResultType.UnknownError;
			}
			result.resultType = resultType;
			result.hResult = hResult;
			return result;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00029E94 File Offset: 0x00028094
		[RequiredByNativeCode]
		private static void InvokeOnCreatedVideoCaptureResourceDelegate(VideoCapture.OnVideoCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new VideoCapture(nativePtr));
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00029ECC File Offset: 0x000280CC
		private VideoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00029EDD File Offset: 0x000280DD
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStartedDelegate(VideoCapture.OnVideoModeStartedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00029EED File Offset: 0x000280ED
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStoppedDelegate(VideoCapture.OnVideoModeStoppedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00029EFD File Offset: 0x000280FD
		[RequiredByNativeCode]
		private static void InvokeOnStartedRecordingVideoToDiskDelegate(VideoCapture.OnStartedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00029F0D File Offset: 0x0002810D
		[RequiredByNativeCode]
		private static void InvokeOnStoppedRecordingVideoToDiskDelegate(VideoCapture.OnStoppedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00029F20 File Offset: 0x00028120
		public void Dispose()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00029F60 File Offset: 0x00028160
		[NativeMethod("VideoCaptureBindings::Dispose", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void Dispose_Internal()
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00029F84 File Offset: 0x00028184
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_NativePtr != IntPtr.Zero;
				if (flag)
				{
					this.DisposeThreaded_Internal();
					this.m_NativePtr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00029FD8 File Offset: 0x000281D8
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::DisposeThreaded", HasExplicitThis = true)]
		private void DisposeThreaded_Internal()
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.DisposeThreaded_Internal_Injected(intPtr);
		}

		// Token: 0x0600140A RID: 5130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0600140B RID: 5131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeThreaded_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0400075F RID: 1887
		internal IntPtr m_NativePtr;

		// Token: 0x04000760 RID: 1888
		private static readonly long HR_SUCCESS;

		// Token: 0x02000217 RID: 535
		public enum CaptureResultType
		{
			// Token: 0x04000762 RID: 1890
			Success,
			// Token: 0x04000763 RID: 1891
			UnknownError
		}

		// Token: 0x02000218 RID: 536
		public struct VideoCaptureResult
		{
			// Token: 0x04000764 RID: 1892
			public VideoCapture.CaptureResultType resultType;

			// Token: 0x04000765 RID: 1893
			public long hResult;
		}

		// Token: 0x02000219 RID: 537
		// (Invoke) Token: 0x0600140D RID: 5133
		public delegate void OnVideoCaptureResourceCreatedCallback(VideoCapture captureObject);

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x0600140F RID: 5135
		public delegate void OnVideoModeStartedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x0200021B RID: 539
		// (Invoke) Token: 0x06001411 RID: 5137
		public delegate void OnVideoModeStoppedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x0200021C RID: 540
		// (Invoke) Token: 0x06001413 RID: 5139
		public delegate void OnStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x0200021D RID: 541
		// (Invoke) Token: 0x06001415 RID: 5141
		public delegate void OnStoppedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x0200021E RID: 542
		internal static class BindingsMarshaller
		{
			// Token: 0x06001416 RID: 5142 RVA: 0x00029FFA File Offset: 0x000281FA
			public static IntPtr ConvertToNative(VideoCapture videoCapture)
			{
				return videoCapture.m_NativePtr;
			}
		}
	}
}

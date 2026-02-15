using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x0200020B RID: 523
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StaticAccessor("PhotoCapture", StaticAccessorType.DoubleColon)]
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCapture.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class PhotoCapture : IDisposable
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x00029AE4 File Offset: 0x00027CE4
		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(long hResult)
		{
			PhotoCapture.PhotoCaptureResult result = default(PhotoCapture.PhotoCaptureResult);
			bool flag = hResult == PhotoCapture.HR_SUCCESS;
			PhotoCapture.CaptureResultType resultType;
			if (flag)
			{
				resultType = PhotoCapture.CaptureResultType.Success;
			}
			else
			{
				resultType = PhotoCapture.CaptureResultType.UnknownError;
			}
			result.resultType = resultType;
			result.hResult = hResult;
			return result;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00029B28 File Offset: 0x00027D28
		[RequiredByNativeCode]
		private static void InvokeOnCreatedResourceDelegate(PhotoCapture.OnCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new PhotoCapture(nativePtr));
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00029B60 File Offset: 0x00027D60
		private PhotoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00029B71 File Offset: 0x00027D71
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStartedDelegate(PhotoCapture.OnPhotoModeStartedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00029B81 File Offset: 0x00027D81
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStoppedDelegate(PhotoCapture.OnPhotoModeStoppedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00029B91 File Offset: 0x00027D91
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToDiskDelegate(PhotoCapture.OnCapturedToDiskCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00029BA4 File Offset: 0x00027DA4
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToMemoryDelegate(PhotoCapture.OnCapturedToMemoryCallback callback, long hResult, IntPtr photoCaptureFramePtr)
		{
			PhotoCaptureFrame photoCaptureFrame = null;
			bool flag = photoCaptureFramePtr != IntPtr.Zero;
			if (flag)
			{
				photoCaptureFrame = new PhotoCaptureFrame(photoCaptureFramePtr);
			}
			callback(PhotoCapture.MakeCaptureResult(hResult), photoCaptureFrame);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00029BDC File Offset: 0x00027DDC
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

		// Token: 0x060013DE RID: 5086 RVA: 0x00029C1C File Offset: 0x00027E1C
		[NativeName("Dispose")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void Dispose_Internal()
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00029C40 File Offset: 0x00027E40
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

		// Token: 0x060013E0 RID: 5088 RVA: 0x00029C94 File Offset: 0x00027E94
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("DisposeThreaded")]
		[ThreadAndSerializationSafe]
		private void DisposeThreaded_Internal()
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.DisposeThreaded_Internal_Injected(intPtr);
		}

		// Token: 0x060013E1 RID: 5089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x060013E2 RID: 5090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeThreaded_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000754 RID: 1876
		internal IntPtr m_NativePtr;

		// Token: 0x04000755 RID: 1877
		private static readonly long HR_SUCCESS;

		// Token: 0x0200020C RID: 524
		public enum CaptureResultType
		{
			// Token: 0x04000757 RID: 1879
			Success,
			// Token: 0x04000758 RID: 1880
			UnknownError
		}

		// Token: 0x0200020D RID: 525
		public struct PhotoCaptureResult
		{
			// Token: 0x04000759 RID: 1881
			public PhotoCapture.CaptureResultType resultType;

			// Token: 0x0400075A RID: 1882
			public long hResult;
		}

		// Token: 0x0200020E RID: 526
		// (Invoke) Token: 0x060013E4 RID: 5092
		public delegate void OnCaptureResourceCreatedCallback(PhotoCapture captureObject);

		// Token: 0x0200020F RID: 527
		// (Invoke) Token: 0x060013E6 RID: 5094
		public delegate void OnPhotoModeStartedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000210 RID: 528
		// (Invoke) Token: 0x060013E8 RID: 5096
		public delegate void OnPhotoModeStoppedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000211 RID: 529
		// (Invoke) Token: 0x060013EA RID: 5098
		public delegate void OnCapturedToDiskCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x060013EC RID: 5100
		public delegate void OnCapturedToMemoryCallback(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame);

		// Token: 0x02000213 RID: 531
		internal static class BindingsMarshaller
		{
			// Token: 0x060013ED RID: 5101 RVA: 0x00029CB6 File Offset: 0x00027EB6
			public static IntPtr ConvertToNative(PhotoCapture photoCapture)
			{
				return photoCapture.m_NativePtr;
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000003 RID: 3
	[NativeConditional("ENABLE_VR")]
	public static class XRDevice
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002098 File Offset: 0x00000298
		[NativeName("DisableAutoVRCameraTracking")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static void DisableAutoXRCameraTracking([NotNull] Camera camera, bool disabled)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			XRDevice.DisableAutoXRCameraTracking_Injected(intPtr, disabled);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		[RequiredByNativeCode]
		private static void InvokeDeviceLoaded(string loadedDeviceName)
		{
			bool flag = XRDevice.deviceLoaded != null;
			if (flag)
			{
				XRDevice.deviceLoaded(loadedDeviceName);
			}
		}

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableAutoXRCameraTracking_Injected(IntPtr camera, bool disabled);

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string> deviceLoaded;
	}
}

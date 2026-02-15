using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/VFX/Public/VFXManager.h")]
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VFXManagerBindings.h")]
	[StaticAccessor("GetVFXManager()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	public static class VFXManager
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021D0 File Offset: 0x000003D0
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, CullingResults results)
		{
			VFXManager.Internal_ProcessCameraCommand(cam, cmd, camXRSettings, results.ptr);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E4 File Offset: 0x000003E4
		private static void Internal_ProcessCameraCommand([NotNull] Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, IntPtr cullResults)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			VFXManager.Internal_ProcessCameraCommand_Injected(intPtr, (cmd == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(cmd), ref camXRSettings, cullResults);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222C File Offset: 0x0000042C
		public static VFXCameraBufferTypes IsCameraBufferNeeded([NotNull] Camera cam)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			return VFXManager.IsCameraBufferNeeded_Injected(intPtr);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002264 File Offset: 0x00000464
		public static void SetCameraBuffer([NotNull] Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			VFXManager.SetCameraBuffer_Injected(intPtr, type, Object.MarshalledUnityObject.Marshal<Texture>(buffer), x, y, width, height);
		}

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ProcessCameraCommand_Injected(IntPtr cam, IntPtr cmd, [In] ref VFXCameraXRSettings camXRSettings, IntPtr cullResults);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VFXCameraBufferTypes IsCameraBufferNeeded_Injected(IntPtr cam);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCameraBuffer_Injected(IntPtr cam, VFXCameraBufferTypes type, IntPtr buffer, int x, int y, int width, int height);

		// Token: 0x04000018 RID: 24
		private static readonly VFXCameraXRSettings kDefaultCameraXRSettings = new VFXCameraXRSettings
		{
			viewTotal = 1U,
			viewCount = 1U,
			viewOffset = 0U
		};
	}
}

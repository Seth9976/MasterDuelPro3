using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Runtime/Camera/Camera.h")]
	internal class CameraRaycastHelper
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021C4 File Offset: 0x000003C4
		[FreeFunction("CameraScripting::RaycastTry")]
		internal static GameObject RaycastTry(Camera cam, Ray ray, float distance, int layerMask)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(CameraRaycastHelper.RaycastTry_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cam), ref ray, distance, layerMask));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E8 File Offset: 0x000003E8
		[FreeFunction("CameraScripting::RaycastTry2D")]
		internal static GameObject RaycastTry2D(Camera cam, Ray ray, float distance, int layerMask)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(CameraRaycastHelper.RaycastTry2D_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cam), ref ray, distance, layerMask));
		}

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RaycastTry_Injected(IntPtr cam, [In] ref Ray ray, float distance, int layerMask);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RaycastTry2D_Injected(IntPtr cam, [In] ref Ray ray, float distance, int layerMask);
	}
}

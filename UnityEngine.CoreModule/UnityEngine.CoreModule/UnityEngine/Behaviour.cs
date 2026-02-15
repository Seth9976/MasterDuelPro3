using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000191 RID: 401
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[UsedByNativeCode]
	public class Behaviour : Component
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0002192C File Offset: 0x0001FB2C
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00021950 File Offset: 0x0001FB50
		[RequiredByNativeCode]
		[NativeProperty]
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Behaviour.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Behaviour.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00021974 File Offset: 0x0001FB74
		[NativeProperty]
		public bool isActiveAndEnabled
		{
			[NativeMethod("IsAddedToManager")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Behaviour.get_isActiveAndEnabled_Injected(intPtr);
			}
		}

		// Token: 0x06000FEB RID: 4075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06000FEC RID: 4076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000FED RID: 4077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isActiveAndEnabled_Injected(IntPtr _unity_self);
	}
}

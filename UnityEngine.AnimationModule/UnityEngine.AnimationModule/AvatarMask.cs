using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	public sealed class AvatarMask : Object
	{
		// Token: 0x06000232 RID: 562 RVA: 0x000058C4 File Offset: 0x00003AC4
		[NativeMethod("GetBodyPart")]
		public bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AvatarMask.GetHumanoidBodyPartActive_Injected(intPtr, index);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000058E8 File Offset: 0x00003AE8
		public int transformCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AvatarMask.get_transformCount_Injected(intPtr);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000590C File Offset: 0x00003B0C
		public string GetTransformPath(int index)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				AvatarMask.GetTransformPath_Injected(intPtr, index, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000594C File Offset: 0x00003B4C
		private float GetTransformWeight(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AvatarMask.GetTransformWeight_Injected(intPtr, index);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00005970 File Offset: 0x00003B70
		public bool GetTransformActive(int index)
		{
			return this.GetTransformWeight(index) > 0.5f;
		}

		// Token: 0x06000237 RID: 567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHumanoidBodyPartActive_Injected(IntPtr _unity_self, AvatarMaskBodyPart index);

		// Token: 0x06000238 RID: 568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_transformCount_Injected(IntPtr _unity_self);

		// Token: 0x06000239 RID: 569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTransformPath_Injected(IntPtr _unity_self, int index, out ManagedSpanWrapper ret);

		// Token: 0x0600023A RID: 570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetTransformWeight_Injected(IntPtr _unity_self, int index);
	}
}

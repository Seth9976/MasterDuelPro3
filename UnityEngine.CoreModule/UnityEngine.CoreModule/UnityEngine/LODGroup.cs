using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000126 RID: 294
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	public class LODGroup : Component
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00012DE0 File Offset: 0x00010FE0
		public Vector3 localReferencePoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LODGroup.get_localReferencePoint_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00012E08 File Offset: 0x00011008
		public float size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_size_Injected(intPtr);
			}
		}

		// Token: 0x06000A23 RID: 2595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localReferencePoint_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000A24 RID: 2596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_size_Injected(IntPtr _unity_self);
	}
}

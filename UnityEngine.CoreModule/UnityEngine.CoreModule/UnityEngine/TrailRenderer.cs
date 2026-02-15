using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E9 RID: 233
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000CD30 File Offset: 0x0000AF30
		public float time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000CD54 File Offset: 0x0000AF54
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public float startWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_startWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_startWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public float endWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_endWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_endWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x0600061D RID: 1565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x0600061E RID: 1566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000620 RID: 1568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000621 RID: 1569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_endWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000622 RID: 1570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endWidth_Injected(IntPtr _unity_self, float value);
	}
}

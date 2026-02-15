using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B3 RID: 179
	[NativeHeader("Runtime/Export/Camera/CullingGroup.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CullingGroup : IDisposable
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00008D9C File Offset: 0x00006F9C
		public CullingGroup()
		{
			this.m_Ptr = CullingGroup.Init(this);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00008DBC File Offset: 0x00006FBC
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					this.FinalizerFailure();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00008E04 File Offset: 0x00007004
		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		private void DisposeInternal()
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.DisposeInternal_Injected(intPtr);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00008E26 File Offset: 0x00007026
		public void Dispose()
		{
			this.DisposeInternal();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x170000C0 RID: 192
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00008E3C File Offset: 0x0000703C
		public Camera targetCamera
		{
			set
			{
				IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CullingGroup.set_targetCamera_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(value));
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00008E64 File Offset: 0x00007064
		public void SetBoundingSpheres([Unmarshalled] BoundingSphere[] array)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetBoundingSpheres_Injected(intPtr, array);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00008E88 File Offset: 0x00007088
		public void SetBoundingSphereCount(int count)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetBoundingSphereCount_Injected(intPtr, count);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00008EAC File Offset: 0x000070AC
		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00008ECC File Offset: 0x000070CC
		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true)]
		[NativeThrows]
		private unsafe int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = new Span<int>(result);
			int num;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = CullingGroup.QueryIndices_Injected(intPtr, visible, distanceIndex, options, ref managedSpanWrapper, firstIndex);
			}
			return num;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00008F1C File Offset: 0x0000711C
		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		public unsafe void SetBoundingDistances(float[] distances)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(distances);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CullingGroup.SetBoundingDistances_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00008F64 File Offset: 0x00007164
		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetDistanceReferencePoint_InternalVector3_Injected(intPtr, ref point);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00008F88 File Offset: 0x00007188
		public void SetDistanceReferencePoint(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3(point);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00008F94 File Offset: 0x00007194
		[RequiredByNativeCode]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* events = (CullingGroupEvent*)eventsPtr.ToPointer();
			bool flag = cullingGroup.m_OnStateChanged == null;
			if (!flag)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(events[i]);
				}
			}
		}

		// Token: 0x06000461 RID: 1121
		[FreeFunction("CullingGroup_Bindings::Init")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init(object scripting);

		// Token: 0x06000462 RID: 1122 RVA: 0x00008FE8 File Offset: 0x000071E8
		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		private void FinalizerFailure()
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.FinalizerFailure_Injected(intPtr);
		}

		// Token: 0x06000463 RID: 1123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeInternal_Injected(IntPtr _unity_self);

		// Token: 0x06000464 RID: 1124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000465 RID: 1125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingSpheres_Injected(IntPtr _unity_self, BoundingSphere[] array);

		// Token: 0x06000466 RID: 1126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingSphereCount_Injected(IntPtr _unity_self, int count);

		// Token: 0x06000467 RID: 1127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryIndices_Injected(IntPtr _unity_self, bool visible, int distanceIndex, CullingQueryOptions options, ref ManagedSpanWrapper result, int firstIndex);

		// Token: 0x06000468 RID: 1128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper distances);

		// Token: 0x06000469 RID: 1129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDistanceReferencePoint_InternalVector3_Injected(IntPtr _unity_self, [In] ref Vector3 point);

		// Token: 0x0600046A RID: 1130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FinalizerFailure_Injected(IntPtr _unity_self);

		// Token: 0x04000237 RID: 567
		internal IntPtr m_Ptr;

		// Token: 0x04000238 RID: 568
		private CullingGroup.StateChanged m_OnStateChanged = null;

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x0600046C RID: 1132
		public delegate void StateChanged(CullingGroupEvent sphere);

		// Token: 0x020000B5 RID: 181
		internal static class BindingsMarshaller
		{
			// Token: 0x0600046D RID: 1133 RVA: 0x0000900A File Offset: 0x0000720A
			public static IntPtr ConvertToNative(CullingGroup cullingGroup)
			{
				return cullingGroup.m_Ptr;
			}
		}
	}
}

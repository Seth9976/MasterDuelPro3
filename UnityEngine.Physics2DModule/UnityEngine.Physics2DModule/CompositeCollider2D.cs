using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[RequireComponent(typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h")]
	public sealed class CompositeCollider2D : Collider2D
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002E68 File Offset: 0x00001068
		public int pathCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_pathCount_Injected(intPtr);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002E8C File Offset: 0x0000108C
		public int pointCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_pointCount_Injected(intPtr);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002EB0 File Offset: 0x000010B0
		public int GetPath(int index, Vector2[] points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathArray_Internal(index, points);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F1C File Offset: 0x0000111C
		[NativeMethod("GetPathArray_Binding")]
		private unsafe int GetPathArray_Internal(int index, [NotNull] Vector2[] points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(points);
			int pathArray_Internal_Injected;
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				pathArray_Internal_Injected = CompositeCollider2D.GetPathArray_Internal_Injected(intPtr, index, ref managedSpanWrapper);
			}
			return pathArray_Internal_Injected;
		}

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pathCount_Injected(IntPtr _unity_self);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pointCount_Injected(IntPtr _unity_self);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPathArray_Internal_Injected(IntPtr _unity_self, int index, ref ManagedSpanWrapper points);
	}
}

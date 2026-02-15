using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002CEC File Offset: 0x00000EEC
		[NativeMethod("GetPointCount")]
		public int GetTotalPointCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PolygonCollider2D.GetTotalPointCount_Injected(intPtr);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002D10 File Offset: 0x00000F10
		public int pathCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PolygonCollider2D.get_pathCount_Injected(intPtr);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D34 File Offset: 0x00000F34
		public Vector2[] GetPath(int index)
		{
			bool flag = index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist.", index));
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist; negative path index is invalid.", index));
			}
			return this.GetPath_Internal(index);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D94 File Offset: 0x00000F94
		[NativeMethod("GetPath_Binding")]
		private Vector2[] GetPath_Internal(int index)
		{
			Vector2[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				PolygonCollider2D.GetPath_Internal_Injected(intPtr, index, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector2[] array;
				blittableArrayWrapper.Unmarshal<Vector2>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public void SetPath(int index, Vector2[] points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPath_Internal(index, points);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E10 File Offset: 0x00001010
		[NativeMethod("SetPath_Binding")]
		private unsafe void SetPath_Internal(int index, [NotNull] Vector2[] points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(points);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				PolygonCollider2D.SetPath_Internal_Injected(intPtr, index, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTotalPointCount_Injected(IntPtr _unity_self);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pathCount_Injected(IntPtr _unity_self);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPath_Internal_Injected(IntPtr _unity_self, int index, out BlittableArrayWrapper ret);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPath_Internal_Injected(IntPtr _unity_self, int index, ref ManagedSpanWrapper points);
	}
}

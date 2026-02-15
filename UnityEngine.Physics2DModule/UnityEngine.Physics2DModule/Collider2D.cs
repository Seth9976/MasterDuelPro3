using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[RequiredByNativeCode(Optional = true)]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[RequireComponent(typeof(Transform))]
	public class Collider2D : Behaviour
	{
		// Token: 0x1700001A RID: 26
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public bool isTrigger
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_isTrigger_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002B08 File Offset: 0x00000D08
		public bool usedByEffector
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_usedByEffector_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002B2C File Offset: 0x00000D2C
		public Collider2D.CompositeOperation compositeOperation
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_compositeOperation_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B50 File Offset: 0x00000D50
		public Vector2 offset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Collider2D.get_offset_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B78 File Offset: 0x00000D78
		public Rigidbody2D attachedRigidbody
		{
			[NativeMethod("GetAttachedRigidbody_Binding")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody2D>(Collider2D.get_attachedRigidbody_Injected(intPtr));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public int shapeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_shapeCount_Injected(intPtr);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BC4 File Offset: 0x00000DC4
		[NativeMethod("GetShapeHash_Binding")]
		public uint GetShapeHash()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.GetShapeHash_Injected(intPtr);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, this.shapeCount);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C10 File Offset: 0x00000E10
		[NativeMethod("GetShapes_Binding")]
		private int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.GetShapes_Internal_Injected(intPtr, ref physicsShapeGroupState, shapeIndex, shapeCount);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C38 File Offset: 0x00000E38
		[NativeMethod("GetShapeBounds_Binding")]
		public unsafe Bounds GetShapeBounds(List<Bounds> bounds, bool useRadii, bool useWorldSpace)
		{
			Bounds bounds3;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (bounds != null)
				{
					fixed (Bounds[] array = NoAllocHelpers.ExtractArrayFromList<Bounds>(bounds))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, bounds.Count);
					}
				}
				Bounds bounds2;
				Collider2D.GetShapeBounds_Injected(intPtr, ref blittableListWrapper, useRadii, useWorldSpace, out bounds2);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Bounds>(bounds);
				Bounds bounds2;
				bounds3 = bounds2;
			}
			return bounds3;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public bool OverlapPoint(Vector2 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.OverlapPoint_Injected(intPtr, ref point);
		}

		// Token: 0x17000020 RID: 32
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002CD8 File Offset: 0x00000ED8
		[Obsolete("usedByComposite has been deprecated. Use Collider2D.compositeOperation instead", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool usedByComposite
		{
			set
			{
				this.compositeOperation = (value ? Collider2D.CompositeOperation.Merge : Collider2D.CompositeOperation.None);
			}
		}

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isTrigger_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_usedByEffector_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositeOperation_Injected(IntPtr _unity_self, Collider2D.CompositeOperation value);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_offset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedRigidbody_Injected(IntPtr _unity_self);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shapeCount_Injected(IntPtr _unity_self);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShapeHash_Injected(IntPtr _unity_self);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetShapes_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetShapeBounds_Injected(IntPtr _unity_self, ref BlittableListWrapper bounds, bool useRadii, bool useWorldSpace, out Bounds ret);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool OverlapPoint_Injected(IntPtr _unity_self, [In] ref Vector2 point);

		// Token: 0x0200000F RID: 15
		public enum CompositeOperation
		{
			// Token: 0x04000040 RID: 64
			None,
			// Token: 0x04000041 RID: 65
			Merge,
			// Token: 0x04000042 RID: 66
			Intersect,
			// Token: 0x04000043 RID: 67
			Difference,
			// Token: 0x04000044 RID: 68
			Flip
		}
	}
}

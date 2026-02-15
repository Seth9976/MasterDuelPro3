using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Physics2D/Public/PhysicsSceneHandle2D.h")]
	public struct PhysicsScene2D : IEquatable<PhysicsScene2D>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override string ToString()
		{
			return UnityString.Format("({0})", new object[] { this.m_Handle });
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000298
		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene2D);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PhysicsScene2D rhs = (PhysicsScene2D)other;
				flag2 = this.m_Handle == rhs.m_Handle;
			}
			return flag2;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public bool Equals(PhysicsScene2D other)
		{
			return this.m_Handle == other.m_Handle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.RaycastList_Internal(this, origin, direction, distance, contactFilter, results);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002194 File Offset: 0x00000394
		[NativeMethod("Raycast_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D Raycast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.Raycast_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B4 File Offset: 0x000003B4
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("RaycastArray_Binding")]
		private unsafe static int RaycastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.RaycastArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002208 File Offset: 0x00000408
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("RaycastList_Binding")]
		private unsafe static int RaycastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.RaycastList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002284 File Offset: 0x00000484
		public RaycastHit2D GetRayIntersection(Ray ray, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersection_Internal(this, ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B4 File Offset: 0x000004B4
		public int GetRayIntersection(Ray ray, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersectionArray_Internal(this, ray.origin, ray.direction, distance, layerMask, results);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E4 File Offset: 0x000004E4
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetRayIntersection_Binding")]
		private static RaycastHit2D GetRayIntersection_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.GetRayIntersection_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002304 File Offset: 0x00000504
		[NativeMethod("GetRayIntersectionArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int GetRayIntersectionArray_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int rayIntersectionArray_Internal_Injected;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				rayIntersectionArray_Internal_Injected = PhysicsScene2D.GetRayIntersectionArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, ref managedSpanWrapper);
			}
			return rayIntersectionArray_Internal_Injected;
		}

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRayIntersection_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, out RaycastHit2D ret);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRayIntersectionArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, ref ManagedSpanWrapper results);

		// Token: 0x04000001 RID: 1
		private int m_Handle;
	}
}

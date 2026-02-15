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
	// Token: 0x02000003 RID: 3
	[NativeHeader("Physics2DScriptingClasses.h")]
	[NativeHeader("Modules/Physics2D/PhysicsManager2D.h")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeHeader("Physics2DScriptingClasses.h")]
	public class Physics2D
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002358 File Offset: 0x00000558
		public static PhysicsScene2D defaultPhysicsScene
		{
			get
			{
				return default(PhysicsScene2D);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002374 File Offset: 0x00000574
		[StaticAccessor("GetPhysics2DSettings()")]
		public static Vector2 gravity
		{
			get
			{
				Vector2 vector;
				Physics2D.get_gravity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool queriesHitTriggers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000238C File Offset: 0x0000058C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023B4 File Offset: 0x000005B4
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, -5);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023D8 File Offset: 0x000005D8
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000240C File Offset: 0x0000060C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002440 File Offset: 0x00000640
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002470 File Offset: 0x00000670
		[ExcludeFromDocs]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002498 File Offset: 0x00000698
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024C0 File Offset: 0x000006C0
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024E8 File Offset: 0x000006E8
		public static RaycastHit2D GetRayIntersection(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, layerMask);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000250C File Offset: 0x0000070C
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, float.PositiveInfinity, -5);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002540 File Offset: 0x00000740
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, -5);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002570 File Offset: 0x00000770
		[RequiredByNativeCode]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000259C File Offset: 0x0000079C
		[NativeMethod("GetRayIntersectionAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] GetRayIntersectionAll_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.GetRayIntersectionAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025D8 File Offset: 0x000007D8
		[RequiredByNativeCode]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, layerMask);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025FC File Offset: 0x000007FC
		[Obsolete("Physics2D.GetRayIntersectionNonAlloc is deprecated. Use Physics2D.GetRayIntersection instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, results, -5);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002624 File Offset: 0x00000824
		[Obsolete("Physics2D.GetRayIntersectionNonAlloc is deprecated. Use Physics2D.GetRayIntersection instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, -5);
		}

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_gravity_Injected(out Vector2 ret);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRayIntersectionAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, out BlittableArrayWrapper ret);

		// Token: 0x04000002 RID: 2
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/Physics/Public/PhysicsSceneHandle.h")]
	[NativeHeader("Modules/Physics/PhysicsQuery.h")]
	public struct PhysicsScene : IEquatable<PhysicsScene>
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000311C File Offset: 0x0000131C
		public override string ToString()
		{
			return UnityString.Format("({0})", new object[] { this.m_Handle });
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000314C File Offset: 0x0000134C
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003164 File Offset: 0x00001364
		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PhysicsScene rhs = (PhysicsScene)other;
				flag2 = this.m_Handle == rhs.m_Handle;
			}
			return flag2;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031A0 File Offset: 0x000013A0
		public bool Equals(PhysicsScene other)
		{
			return this.m_Handle == other.m_Handle;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031C0 File Offset: 0x000013C0
		public bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float dirLength = direction.magnitude;
			bool flag = dirLength > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 normalizedDirection = direction / dirLength;
				Ray ray = new Ray(origin, normalizedDirection);
				flag2 = PhysicsScene.Internal_RaycastTest(this, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003210 File Offset: 0x00001410
		[FreeFunction("Physics::RaycastTest")]
		private static bool Internal_RaycastTest(PhysicsScene physicsScene, Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastTest_Injected(ref physicsScene, ref ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000322C File Offset: 0x0000142C
		public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			float dirLength = direction.magnitude;
			bool flag = dirLength > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 normalizedDirection = direction / dirLength;
				Ray ray = new Ray(origin, normalizedDirection);
				flag2 = PhysicsScene.Internal_Raycast(this, ray, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003288 File Offset: 0x00001488
		[FreeFunction("Physics::Raycast")]
		private static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_Raycast_Injected(ref physicsScene, ref ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000032A4 File Offset: 0x000014A4
		public int Raycast(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float dirLength = direction.magnitude;
			bool flag = dirLength > float.Epsilon;
			int num;
			if (flag)
			{
				Ray ray = new Ray(origin, direction.normalized);
				num = PhysicsScene.Internal_RaycastNonAlloc(this, ray, raycastHits, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000032F4 File Offset: 0x000014F4
		[FreeFunction("Physics::RaycastNonAlloc")]
		private unsafe static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_RaycastNonAlloc_Injected(ref physicsScene, ref ray, ref managedSpanWrapper, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003338 File Offset: 0x00001538
		[FreeFunction("Physics::SphereCastNonAlloc")]
		private unsafe static int Internal_SphereCastNonAlloc(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_SphereCastNonAlloc_Injected(ref physicsScene, ref origin, radius, ref direction, ref managedSpanWrapper, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003380 File Offset: 0x00001580
		public int SphereCast(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float dirLength = direction.magnitude;
			bool flag = dirLength > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_SphereCastNonAlloc(this, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033C4 File Offset: 0x000015C4
		[FreeFunction("Physics::OverlapSphereNonAlloc")]
		private static int OverlapSphereNonAlloc_Internal(PhysicsScene physicsScene, Vector3 position, float radius, [Unmarshalled] Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal_Injected(ref physicsScene, ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000033E0 File Offset: 0x000015E0
		public int OverlapSphere(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal(this, position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RaycastTest_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Raycast_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_RaycastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, ref ManagedSpanWrapper raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_SphereCastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 origin, float radius, [In] ref Vector3 direction, ref ManagedSpanWrapper raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapSphereNonAlloc_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0400005C RID: 92
		private int m_Handle;
	}
}

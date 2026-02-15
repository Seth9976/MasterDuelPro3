using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/Physics/PhysicsQuery.h")]
	[NativeHeader("Modules/Physics/PhysicsManager.h")]
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	public class Physics
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000231E File Offset: 0x0000051E
		[RequiredByNativeCode]
		private static void OnSceneContactModify(PhysicsScene scene, IntPtr buffer, int count, bool isCCD)
		{
			Action<PhysicsScene, IntPtr, int, bool> genericContactModifyEvent = Physics.GenericContactModifyEvent;
			if (genericContactModifyEvent != null)
			{
				genericContactModifyEvent(scene, buffer, count, isCCD);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002338 File Offset: 0x00000538
		private static void PhysXOnSceneContactModify(PhysicsScene scene, IntPtr buffer, int count, bool isCCD)
		{
			NativeArray<ModifiableContactPair> array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ModifiableContactPair>(buffer.ToPointer(), count, Allocator.None);
			bool flag = !isCCD;
			if (flag)
			{
				Action<PhysicsScene, NativeArray<ModifiableContactPair>> contactModifyEvent = Physics.ContactModifyEvent;
				if (contactModifyEvent != null)
				{
					contactModifyEvent(scene, array);
				}
			}
			else
			{
				Action<PhysicsScene, NativeArray<ModifiableContactPair>> contactModifyEventCCD = Physics.ContactModifyEventCCD;
				if (contactModifyEventCCD != null)
				{
					contactModifyEventCCD(scene, array);
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29
		public static extern bool invokeCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002388 File Offset: 0x00000588
		[NativeProperty("DefaultPhysicsSceneHandle", true, TargetType.Function, true)]
		public static PhysicsScene defaultPhysicsScene
		{
			get
			{
				PhysicsScene physicsScene;
				Physics.get_defaultPhysicsScene_Injected(out physicsScene);
				return physicsScene;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A0 File Offset: 0x000005A0
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023C8 File Offset: 0x000005C8
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023EC File Offset: 0x000005EC
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002414 File Offset: 0x00000614
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002440 File Offset: 0x00000640
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002468 File Offset: 0x00000668
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002490 File Offset: 0x00000690
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B8 File Offset: 0x000006B8
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024E4 File Offset: 0x000006E4
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002514 File Offset: 0x00000714
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002544 File Offset: 0x00000744
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002578 File Offset: 0x00000778
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025B0 File Offset: 0x000007B0
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E4 File Offset: 0x000007E4
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002610 File Offset: 0x00000810
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002644 File Offset: 0x00000844
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000267C File Offset: 0x0000087C
		[FreeFunction("Physics::RaycastAll")]
		private static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics.Internal_RaycastAll_Injected(ref physicsScene, ref ray, maxDistance, mask, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026B8 File Offset: 0x000008B8
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float dirLength = direction.magnitude;
			bool flag = dirLength > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 normalizedDirection = direction / dirLength;
				Ray ray = new Ray(origin, normalizedDirection);
				array = Physics.Internal_RaycastAll(Physics.defaultPhysicsScene, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000270C File Offset: 0x0000090C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002728 File Offset: 0x00000928
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002748 File Offset: 0x00000948
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			return Physics.RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000276C File Offset: 0x0000096C
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002794 File Offset: 0x00000994
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027BC File Offset: 0x000009BC
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027E8 File Offset: 0x000009E8
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002818 File Offset: 0x00000A18
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000284C File Offset: 0x00000A4C
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002880 File Offset: 0x00000A80
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028B4 File Offset: 0x00000AB4
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028EC File Offset: 0x00000AEC
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002914 File Offset: 0x00000B14
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000293C File Offset: 0x00000B3C
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002964 File Offset: 0x00000B64
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64
		public static extern bool reuseCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002990 File Offset: 0x00000B90
		[FreeFunction("Physics::ComputePenetration")]
		private static bool Query_ComputePenetration([NotNull] Collider colliderA, Vector3 positionA, Quaternion rotationA, [NotNull] Collider colliderB, Vector3 positionB, Quaternion rotationB, ref Vector3 direction, ref float distance)
		{
			if (colliderA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			if (colliderB == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(colliderA);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider>(colliderB);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			return Physics.Query_ComputePenetration_Injected(intPtr, ref positionA, ref rotationA, intPtr2, ref positionB, ref rotationB, ref direction, ref distance);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029F8 File Offset: 0x00000BF8
		public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
		{
			direction = Vector3.zero;
			distance = 0f;
			return Physics.Query_ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, ref direction, ref distance);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A30 File Offset: 0x00000C30
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A58 File Offset: 0x00000C58
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A84 File Offset: 0x00000C84
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		internal static Collider GetColliderByInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Collider>(Physics.GetColliderByInstanceID_Injected(instanceID));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A9C File Offset: 0x00000C9C
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		internal static Component GetBodyByInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Component>(Physics.GetBodyByInstanceID_Injected(instanceID));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AB4 File Offset: 0x00000CB4
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionEnter(Component component, Collision collision)
		{
			Physics.SendOnCollisionEnter_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AD0 File Offset: 0x00000CD0
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionStay(Component component, Collision collision)
		{
			Physics.SendOnCollisionStay_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AEC File Offset: 0x00000CEC
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionExit(Component component, Collision collision)
		{
			Physics.SendOnCollisionExit_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B08 File Offset: 0x00000D08
		[RequiredByNativeCode]
		private static void OnSceneContact(PhysicsScene scene, IntPtr buffer, int count)
		{
			bool flag = count == 0;
			if (!flag)
			{
				NativeArray<ContactPairHeader> array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ContactPairHeader>(buffer.ToPointer(), count, Allocator.None);
				try
				{
					Physics.ContactEventDelegate contactEvent = Physics.ContactEvent;
					if (contactEvent != null)
					{
						contactEvent(scene, array.AsReadOnly());
					}
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}
				finally
				{
					Physics.ReportContacts(array.AsReadOnly());
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B88 File Offset: 0x00000D88
		private static void ReportContacts(NativeArray<ContactPairHeader>.ReadOnly array)
		{
			bool flag = !Physics.invokeCollisionCallbacks;
			if (!flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ContactPairHeader header = array[i];
					bool hasRemovedBody = header.hasRemovedBody;
					if (!hasRemovedBody)
					{
						int j = 0;
						while ((long)j < (long)((ulong)header.m_NbPairs))
						{
							readonly ref ContactPair pair = ref header.GetContactPair(j);
							bool hasRemovedCollider = pair.hasRemovedCollider;
							if (!hasRemovedCollider)
							{
								Component actor = header.body;
								Component otherActor = header.otherBody;
								Component component = ((actor != null) ? actor : pair.collider);
								Component otherComponent = ((otherActor != null) ? otherActor : pair.otherCollider);
								bool flag2 = !component || !otherComponent;
								if (!flag2)
								{
									bool isCollisionEnter = pair.isCollisionEnter;
									if (isCollisionEnter)
									{
										Physics.SendOnCollisionEnter(component, Physics.GetCollisionToReport(in header, in pair, false));
										Physics.SendOnCollisionEnter(otherComponent, Physics.GetCollisionToReport(in header, in pair, true));
									}
									bool isCollisionStay = pair.isCollisionStay;
									if (isCollisionStay)
									{
										Physics.SendOnCollisionStay(component, Physics.GetCollisionToReport(in header, in pair, false));
										Physics.SendOnCollisionStay(otherComponent, Physics.GetCollisionToReport(in header, in pair, true));
									}
									bool isCollisionExit = pair.isCollisionExit;
									if (isCollisionExit)
									{
										Physics.SendOnCollisionExit(component, Physics.GetCollisionToReport(in header, in pair, false));
										Physics.SendOnCollisionExit(otherComponent, Physics.GetCollisionToReport(in header, in pair, true));
									}
								}
							}
							j++;
						}
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D1C File Offset: 0x00000F1C
		private static Collision GetCollisionToReport(in ContactPairHeader header, in ContactPair pair, bool flipped)
		{
			bool reuseCollisionCallbacks = Physics.reuseCollisionCallbacks;
			Collision collision;
			if (reuseCollisionCallbacks)
			{
				Physics.s_ReusableCollision.Reuse(in header, in pair);
				Physics.s_ReusableCollision.Flipped = flipped;
				collision = Physics.s_ReusableCollision;
			}
			else
			{
				collision = new Collision(in header, in pair, flipped);
			}
			return collision;
		}

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultPhysicsScene_Injected(out PhysicsScene ret);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RaycastAll_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_ComputePenetration_Injected(IntPtr colliderA, [In] ref Vector3 positionA, [In] ref Quaternion rotationA, IntPtr colliderB, [In] ref Vector3 positionB, [In] ref Quaternion rotationB, ref Vector3 direction, ref float distance);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetColliderByInstanceID_Injected(int instanceID);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBodyByInstanceID_Injected(int instanceID);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionEnter_Injected(IntPtr component, Collision collision);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionStay_Injected(IntPtr component, Collision collision);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionExit_Injected(IntPtr component, Collision collision);

		// Token: 0x04000012 RID: 18
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEvent;

		// Token: 0x04000013 RID: 19
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEventCCD;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<PhysicsScene, IntPtr, int, bool> GenericContactModifyEvent = new Action<PhysicsScene, IntPtr, int, bool>(Physics.PhysXOnSceneContactModify);

		// Token: 0x04000015 RID: 21
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Physics.ContactEventDelegate ContactEvent;

		// Token: 0x04000016 RID: 22
		private static readonly Collision s_ReusableCollision = new Collision();

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000057 RID: 87
		public delegate void ContactEventDelegate(PhysicsScene scene, NativeArray<ContactPairHeader>.ReadOnly headerArray);
	}
}

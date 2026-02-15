using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200009C RID: 156
	[DocumentationSorting(DocumentationSortingAttribute.Level.Undoc)]
	public static class RuntimeUtility
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00015A0D File Offset: 0x00013C0D
		public static void DestroyObject(global::UnityEngine.Object obj)
		{
			if (obj != null)
			{
				global::UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000C34E File Offset: 0x0000A54E
		public static bool IsPrefab(GameObject gameObject)
		{
			return false;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00015A20 File Offset: 0x00013C20
		public static bool RaycastIgnoreTag(Ray ray, out RaycastHit hitInfo, float rayLength, int layerMask, in string ignoreTag)
		{
			if (ignoreTag.Length == 0)
			{
				if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Ignore))
				{
					return true;
				}
			}
			else
			{
				int closestHit = -1;
				int numHits = Physics.RaycastNonAlloc(ray, RuntimeUtility.s_HitBuffer, rayLength, layerMask, QueryTriggerInteraction.Ignore);
				for (int i = 0; i < numHits; i++)
				{
					if (!RuntimeUtility.s_HitBuffer[i].collider.CompareTag(ignoreTag) && (closestHit < 0 || RuntimeUtility.s_HitBuffer[i].distance < RuntimeUtility.s_HitBuffer[closestHit].distance))
					{
						closestHit = i;
					}
				}
				if (closestHit >= 0)
				{
					hitInfo = RuntimeUtility.s_HitBuffer[closestHit];
					if (numHits == RuntimeUtility.s_HitBuffer.Length)
					{
						RuntimeUtility.s_HitBuffer = new RaycastHit[RuntimeUtility.s_HitBuffer.Length * 2];
					}
					return true;
				}
			}
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00015AE4 File Offset: 0x00013CE4
		public static bool SphereCastIgnoreTag(Vector3 rayStart, float radius, Vector3 dir, out RaycastHit hitInfo, float rayLength, int layerMask, in string ignoreTag)
		{
			int closestHit = -1;
			int numPenetrations = 0;
			float penetrationDistanceSum = 0f;
			int numHits = Physics.SphereCastNonAlloc(rayStart, radius, dir, RuntimeUtility.s_HitBuffer, rayLength, layerMask, QueryTriggerInteraction.Ignore);
			for (int i = 0; i < numHits; i++)
			{
				RaycastHit h = RuntimeUtility.s_HitBuffer[i];
				if (ignoreTag.Length <= 0 || !h.collider.CompareTag(ignoreTag))
				{
					if (h.distance == 0f && h.normal == -dir)
					{
						SphereCollider scratchCollider = RuntimeUtility.GetScratchCollider();
						scratchCollider.radius = radius;
						Collider c = h.collider;
						Vector3 offsetDir;
						float offsetDistance;
						if (!Physics.ComputePenetration(scratchCollider, rayStart, Quaternion.identity, c, c.transform.position, c.transform.rotation, out offsetDir, out offsetDistance))
						{
							goto IL_0148;
						}
						h.point = rayStart + offsetDir * (offsetDistance - radius);
						h.distance = offsetDistance - radius;
						h.normal = offsetDir;
						RuntimeUtility.s_HitBuffer[i] = h;
						if (h.distance < -0.0001f)
						{
							penetrationDistanceSum += h.distance;
							if (RuntimeUtility.s_PenetrationIndexBuffer.Length > numPenetrations + 1)
							{
								RuntimeUtility.s_PenetrationIndexBuffer[numPenetrations++] = i;
							}
						}
					}
					if (closestHit < 0 || h.distance < RuntimeUtility.s_HitBuffer[closestHit].distance)
					{
						closestHit = i;
					}
				}
				IL_0148:;
			}
			if (numPenetrations > 1)
			{
				hitInfo = default(RaycastHit);
				for (int j = 0; j < numPenetrations; j++)
				{
					RaycastHit h2 = RuntimeUtility.s_HitBuffer[RuntimeUtility.s_PenetrationIndexBuffer[j]];
					float t = h2.distance / penetrationDistanceSum;
					hitInfo.point += h2.point * t;
					hitInfo.distance += h2.distance * t;
					hitInfo.normal += h2.normal * t;
				}
				hitInfo.normal = hitInfo.normal.normalized;
				return true;
			}
			if (closestHit >= 0)
			{
				hitInfo = RuntimeUtility.s_HitBuffer[closestHit];
				if (numHits == RuntimeUtility.s_HitBuffer.Length)
				{
					RuntimeUtility.s_HitBuffer = new RaycastHit[RuntimeUtility.s_HitBuffer.Length * 2];
				}
				return true;
			}
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00015D2C File Offset: 0x00013F2C
		internal static SphereCollider GetScratchCollider()
		{
			if (RuntimeUtility.s_ScratchColliderGameObject == null)
			{
				RuntimeUtility.s_ScratchColliderGameObject = new GameObject("Cinemachine Scratch Collider");
				RuntimeUtility.s_ScratchColliderGameObject.hideFlags = HideFlags.HideAndDontSave;
				RuntimeUtility.s_ScratchColliderGameObject.transform.position = Vector3.zero;
				RuntimeUtility.s_ScratchColliderGameObject.SetActive(true);
				RuntimeUtility.s_ScratchCollider = RuntimeUtility.s_ScratchColliderGameObject.AddComponent<SphereCollider>();
				RuntimeUtility.s_ScratchCollider.isTrigger = true;
				Rigidbody rigidbody = RuntimeUtility.s_ScratchColliderGameObject.AddComponent<Rigidbody>();
				rigidbody.detectCollisions = false;
				rigidbody.isKinematic = true;
			}
			return RuntimeUtility.s_ScratchCollider;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015DB8 File Offset: 0x00013FB8
		internal static void DestroyScratchCollider()
		{
			if (RuntimeUtility.s_ScratchColliderGameObject != null)
			{
				RuntimeUtility.s_ScratchColliderGameObject.SetActive(false);
				RuntimeUtility.DestroyObject(RuntimeUtility.s_ScratchColliderGameObject.GetComponent<Rigidbody>());
			}
			RuntimeUtility.DestroyObject(RuntimeUtility.s_ScratchCollider);
			RuntimeUtility.DestroyObject(RuntimeUtility.s_ScratchColliderGameObject);
			RuntimeUtility.s_ScratchColliderGameObject = null;
			RuntimeUtility.s_ScratchCollider = null;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00015E0C File Offset: 0x0001400C
		public static AnimationCurve NormalizeCurve(AnimationCurve curve, bool normalizeX, bool normalizeY)
		{
			if (!normalizeX && !normalizeY)
			{
				return curve;
			}
			Keyframe[] keys = curve.keys;
			if (keys.Length != 0)
			{
				float minTime = keys[0].time;
				float maxTime = minTime;
				float minVal = keys[0].value;
				float maxVal = minVal;
				for (int i = 0; i < keys.Length; i++)
				{
					minTime = Mathf.Min(minTime, keys[i].time);
					maxTime = Mathf.Max(maxTime, keys[i].time);
					minVal = Mathf.Min(minVal, keys[i].value);
					maxVal = Mathf.Max(maxVal, keys[i].value);
				}
				float range = maxTime - minTime;
				float timeScale = ((range < 0.0001f) ? 1f : (1f / range));
				range = maxVal - minVal;
				float valScale = ((range < 1f) ? 1f : (1f / range));
				float valOffset = 0f;
				if (range < 1f)
				{
					if (minVal > 0f && minVal + range <= 1f)
					{
						valOffset = minVal;
					}
					else
					{
						valOffset = 1f - range;
					}
				}
				for (int j = 0; j < keys.Length; j++)
				{
					if (normalizeX)
					{
						keys[j].time = (keys[j].time - minTime) * timeScale;
					}
					if (normalizeY)
					{
						keys[j].value = (keys[j].value - minVal) * valScale + valOffset;
					}
				}
				curve.keys = keys;
			}
			return curve;
		}

		// Token: 0x0400034C RID: 844
		private static RaycastHit[] s_HitBuffer = new RaycastHit[16];

		// Token: 0x0400034D RID: 845
		private static int[] s_PenetrationIndexBuffer = new int[16];

		// Token: 0x0400034E RID: 846
		private static SphereCollider s_ScratchCollider;

		// Token: 0x0400034F RID: 847
		private static GameObject s_ScratchColliderGameObject;
	}
}

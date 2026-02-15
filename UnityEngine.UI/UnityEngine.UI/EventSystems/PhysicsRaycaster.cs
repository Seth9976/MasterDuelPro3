using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C7 RID: 199
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001C751 File Offset: 0x0001A951
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001C765 File Offset: 0x0001A965
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				if (this.m_EventCamera == null)
				{
					return Camera.main;
				}
				return this.m_EventCamera;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001C79B File Offset: 0x0001A99B
		public virtual int depth
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return 16777215;
				}
				return (int)this.eventCamera.depth;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001C7BD File Offset: 0x0001A9BD
		public int finalEventMask
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return -1;
				}
				return this.eventCamera.cullingMask & this.m_EventMask;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001C7E6 File Offset: 0x0001A9E6
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001C7EE File Offset: 0x0001A9EE
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001C7F7 File Offset: 0x0001A9F7
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001C7FF File Offset: 0x0001A9FF
		public int maxRayIntersections
		{
			get
			{
				return this.m_MaxRayIntersections;
			}
			set
			{
				this.m_MaxRayIntersections = value;
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001C808 File Offset: 0x0001AA08
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (this.eventCamera == null)
			{
				return false;
			}
			Vector3 eventPosition = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (eventPosition != Vector3.zero)
			{
				eventDisplayIndex = (int)eventPosition.z;
				if (eventDisplayIndex != this.eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				eventPosition = eventData.position;
			}
			if (!this.eventCamera.pixelRect.Contains(eventPosition))
			{
				return false;
			}
			ray = this.eventCamera.ScreenPointToRay(eventPosition);
			float projectionDirection = ray.direction.z;
			distanceToClipPlane = (Mathf.Approximately(0f, projectionDirection) ? float.PositiveInfinity : Mathf.Abs((this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane) / projectionDirection));
			return true;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = default(Ray);
			int displayIndex = 0;
			float distanceToClipPlane = 0f;
			if (!this.ComputeRayAndDistance(eventData, ref ray, ref displayIndex, ref distanceToClipPlane))
			{
				return;
			}
			int hitCount;
			if (this.m_MaxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.raycast3DAll(ray, distanceToClipPlane, this.finalEventMask);
				hitCount = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit[this.m_MaxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				hitCount = ReflectionMethodsCache.Singleton.getRaycastNonAlloc(ray, this.m_Hits, distanceToClipPlane, this.finalEventMask);
			}
			if (hitCount != 0)
			{
				if (hitCount > 1)
				{
					Array.Sort<RaycastHit>(this.m_Hits, 0, hitCount, PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int b = 0;
				int bmax = hitCount;
				while (b < bmax)
				{
					RaycastResult result = new RaycastResult
					{
						gameObject = this.m_Hits[b].collider.gameObject,
						module = this,
						distance = this.m_Hits[b].distance,
						worldPosition = this.m_Hits[b].point,
						worldNormal = this.m_Hits[b].normal,
						screenPosition = eventData.position,
						displayIndex = displayIndex,
						index = (float)resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(result);
					b++;
				}
			}
		}

		// Token: 0x04000354 RID: 852
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000355 RID: 853
		protected Camera m_EventCamera;

		// Token: 0x04000356 RID: 854
		[SerializeField]
		protected LayerMask m_EventMask = -1;

		// Token: 0x04000357 RID: 855
		[SerializeField]
		protected int m_MaxRayIntersections;

		// Token: 0x04000358 RID: 856
		protected int m_LastMaxRayIntersections;

		// Token: 0x04000359 RID: 857
		private RaycastHit[] m_Hits;

		// Token: 0x020000C8 RID: 200
		private class RaycastHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x0600075E RID: 1886 RVA: 0x0001CA88 File Offset: 0x0001AC88
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}

			// Token: 0x0400035A RID: 858
			public static PhysicsRaycaster.RaycastHitComparer instance = new PhysicsRaycaster.RaycastHitComparer();
		}
	}
}

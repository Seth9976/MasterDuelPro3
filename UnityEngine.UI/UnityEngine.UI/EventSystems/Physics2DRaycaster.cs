using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C6 RID: 198
	[AddComponentMenu("Event/Physics 2D Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class Physics2DRaycaster : PhysicsRaycaster
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x0001C48A File Offset: 0x0001A68A
		protected Physics2DRaycaster()
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001C494 File Offset: 0x0001A694
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = default(Ray);
			float distanceToClipPlane = 0f;
			int displayIndex = 0;
			if (!base.ComputeRayAndDistance(eventData, ref ray, ref displayIndex, ref distanceToClipPlane))
			{
				return;
			}
			int hitCount;
			if (base.maxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, distanceToClipPlane, base.finalEventMask);
				hitCount = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit2D[base.maxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				hitCount = ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc(ray, this.m_Hits, distanceToClipPlane, base.finalEventMask);
			}
			if (hitCount != 0)
			{
				int b = 0;
				int bmax = hitCount;
				while (b < bmax)
				{
					Renderer r2d = null;
					Renderer rendererResult = this.m_Hits[b].collider.gameObject.GetComponent<Renderer>();
					if (rendererResult != null)
					{
						if (rendererResult is SpriteRenderer)
						{
							r2d = rendererResult;
						}
						if (rendererResult is TilemapRenderer)
						{
							r2d = rendererResult;
						}
						if (rendererResult is SpriteShapeRenderer)
						{
							r2d = rendererResult;
						}
					}
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
						sortingGroupID = ((r2d != null) ? r2d.sortingGroupID : SortingGroup.invalidSortingGroupID),
						sortingGroupOrder = ((r2d != null) ? r2d.sortingGroupOrder : 0),
						sortingLayer = ((r2d != null) ? r2d.sortingLayerID : 0),
						sortingOrder = ((r2d != null) ? r2d.sortingOrder : 0)
					};
					if (result.sortingGroupID != SortingGroup.invalidSortingGroupID)
					{
						SortingGroup sortingGroup = SortingGroup.GetSortingGroupByIndex(r2d.sortingGroupID);
						if (sortingGroup != null)
						{
							result.distance = Vector3.Dot(ray.direction, sortingGroup.transform.position - ray.origin);
							result.sortingLayer = sortingGroup.sortingLayerID;
							result.sortingOrder = sortingGroup.sortingOrder;
						}
					}
					resultAppendList.Add(result);
					b++;
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private RaycastHit2D[] m_Hits;
	}
}

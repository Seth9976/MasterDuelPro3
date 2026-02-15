using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200001F RID: 31
	[AddComponentMenu("Event/Graphic Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006258 File Offset: 0x00004458
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006279 File Offset: 0x00004479
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.rootCanvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000629F File Offset: 0x0000449F
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000062A7 File Offset: 0x000044A7
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000062B0 File Offset: 0x000044B0
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000062B8 File Offset: 0x000044B8
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000062C1 File Offset: 0x000044C1
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000062C9 File Offset: 0x000044C9
		public LayerMask blockingMask
		{
			get
			{
				return this.m_BlockingMask;
			}
			set
			{
				this.m_BlockingMask = value;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000062D2 File Offset: 0x000044D2
		protected GraphicRaycaster()
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000062F8 File Offset: 0x000044F8
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006324 File Offset: 0x00004524
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			IList<Graphic> canvasGraphics = GraphicRegistry.GetRaycastableGraphicsForCanvas(this.canvas);
			if (canvasGraphics == null || canvasGraphics.Count == 0)
			{
				return;
			}
			Camera currentEventCamera = this.eventCamera;
			int displayIndex;
			if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || currentEventCamera == null)
			{
				displayIndex = this.canvas.targetDisplay;
			}
			else
			{
				displayIndex = currentEventCamera.targetDisplay;
			}
			Vector3 eventPosition = MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			if ((int)eventPosition.z != displayIndex)
			{
				return;
			}
			Vector2 pos;
			if (currentEventCamera == null)
			{
				float w = (float)Screen.width;
				float h = (float)Screen.height;
				if (displayIndex > 0 && displayIndex < Display.displays.Length)
				{
					w = (float)Display.displays[displayIndex].systemWidth;
					h = (float)Display.displays[displayIndex].systemHeight;
				}
				pos = new Vector2(eventPosition.x / w, eventPosition.y / h);
			}
			else
			{
				pos = currentEventCamera.ScreenToViewportPoint(eventPosition);
			}
			if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
			{
				return;
			}
			float hitDistance = float.MaxValue;
			Ray ray = default(Ray);
			if (currentEventCamera != null)
			{
				ray = currentEventCamera.ScreenPointToRay(eventPosition);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float distanceToClipPlane = 100f;
				if (currentEventCamera != null)
				{
					float projectionDirection = ray.direction.z;
					distanceToClipPlane = (Mathf.Approximately(0f, projectionDirection) ? float.PositiveInfinity : Mathf.Abs((currentEventCamera.farClipPlane - currentEventCamera.nearClipPlane) / projectionDirection));
				}
				RaycastHit hit;
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast3D != null && ReflectionMethodsCache.Singleton.raycast3D(ray, out hit, distanceToClipPlane, this.m_BlockingMask))
				{
					hitDistance = hit.distance;
				}
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast2D != null)
				{
					RaycastHit2D[] hits = ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, distanceToClipPlane, this.m_BlockingMask);
					if (hits.Length != 0)
					{
						hitDistance = hits[0].distance;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, currentEventCamera, eventPosition, canvasGraphics, this.m_RaycastResults);
			int totalCount = this.m_RaycastResults.Count;
			for (int index = 0; index < totalCount; index++)
			{
				GameObject go = this.m_RaycastResults[index].gameObject;
				bool appendGraphic = true;
				if (this.ignoreReversedGraphics)
				{
					if (currentEventCamera == null)
					{
						Vector3 dir = go.transform.rotation * Vector3.forward;
						appendGraphic = Vector3.Dot(Vector3.forward, dir) > 0f;
					}
					else
					{
						Vector3 cameraForward = currentEventCamera.transform.rotation * Vector3.forward * currentEventCamera.nearClipPlane;
						appendGraphic = Vector3.Dot(go.transform.position - currentEventCamera.transform.position - cameraForward, go.transform.forward) >= 0f;
					}
				}
				if (appendGraphic)
				{
					Transform trans = go.transform;
					Vector3 transForward = trans.forward;
					float distance;
					if (currentEventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						distance = 0f;
					}
					else
					{
						distance = Vector3.Dot(transForward, trans.position - ray.origin) / Vector3.Dot(transForward, ray.direction);
						if (distance < 0f)
						{
							goto IL_0464;
						}
					}
					if (distance < hitDistance)
					{
						RaycastResult castResult = new RaycastResult
						{
							gameObject = go,
							module = this,
							distance = distance,
							screenPosition = eventPosition,
							displayIndex = displayIndex,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[index].depth,
							sortingLayer = this.canvas.sortingLayerID,
							sortingOrder = this.canvas.sortingOrder,
							worldPosition = ray.origin + ray.direction * distance,
							worldNormal = -transForward
						};
						resultAppendList.Add(castResult);
					}
				}
				IL_0464:;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000067A4 File Offset: 0x000049A4
		public override Camera eventCamera
		{
			get
			{
				Canvas canvas = this.canvas;
				RenderMode renderMode = canvas.renderMode;
				if (renderMode == RenderMode.ScreenSpaceOverlay || (renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null))
				{
					return null;
				}
				return canvas.worldCamera ?? Camera.main;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000067E8 File Offset: 0x000049E8
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, IList<Graphic> foundGraphics, List<Graphic> results)
		{
			int totalCount = foundGraphics.Count;
			for (int i = 0; i < totalCount; i++)
			{
				Graphic graphic = foundGraphics[i];
				if (graphic.raycastTarget && !graphic.canvasRenderer.cull && graphic.depth != -1 && RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera, graphic.raycastPadding) && (!(eventCamera != null) || eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z <= eventCamera.farClipPlane) && graphic.Raycast(pointerPosition, eventCamera))
				{
					GraphicRaycaster.s_SortedGraphics.Add(graphic);
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			totalCount = GraphicRaycaster.s_SortedGraphics.Count;
			for (int j = 0; j < totalCount; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
			GraphicRaycaster.s_SortedGraphics.Clear();
		}

		// Token: 0x04000084 RID: 132
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000085 RID: 133
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x04000086 RID: 134
		[FormerlySerializedAs("blockingObjects")]
		[SerializeField]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x04000088 RID: 136
		private Canvas m_Canvas;

		// Token: 0x04000089 RID: 137
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x0400008A RID: 138
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x02000020 RID: 32
		public enum BlockingObjects
		{
			// Token: 0x0400008C RID: 140
			None,
			// Token: 0x0400008D RID: 141
			TwoD,
			// Token: 0x0400008E RID: 142
			ThreeD,
			// Token: 0x0400008F RID: 143
			All
		}
	}
}

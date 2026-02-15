using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200011F RID: 287
	[AddComponentMenu("Event/Tracked Device Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class TrackedDeviceRaycaster : BaseRaycaster
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000452C8 File Offset: 0x000434C8
		public override Camera eventCamera
		{
			get
			{
				Canvas myCanvas = this.canvas;
				if (!(myCanvas != null))
				{
					return null;
				}
				return myCanvas.worldCamera;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000452ED File Offset: 0x000434ED
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x000452F5 File Offset: 0x000434F5
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

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000452FE File Offset: 0x000434FE
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x00045306 File Offset: 0x00043506
		public bool checkFor3DOcclusion
		{
			get
			{
				return this.m_CheckFor3DOcclusion;
			}
			set
			{
				this.m_CheckFor3DOcclusion = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0004530F File Offset: 0x0004350F
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x00045317 File Offset: 0x00043517
		public bool checkFor2DOcclusion
		{
			get
			{
				return this.m_CheckFor2DOcclusion;
			}
			set
			{
				this.m_CheckFor2DOcclusion = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00045320 File Offset: 0x00043520
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x00045328 File Offset: 0x00043528
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

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00045331 File Offset: 0x00043531
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00045339 File Offset: 0x00043539
		public float maxDistance
		{
			get
			{
				return this.m_MaxDistance;
			}
			set
			{
				this.m_MaxDistance = value;
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00045342 File Offset: 0x00043542
		protected override void OnEnable()
		{
			base.OnEnable();
			TrackedDeviceRaycaster.s_Instances.AppendWithCapacity(this, 10);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00045358 File Offset: 0x00043558
		protected override void OnDisable()
		{
			int index = TrackedDeviceRaycaster.s_Instances.IndexOfReference(this);
			if (index != -1)
			{
				TrackedDeviceRaycaster.s_Instances.RemoveAtByMovingTailWithCapacity(index);
			}
			base.OnDisable();
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00045388 File Offset: 0x00043588
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			ExtendedPointerEventData trackedEventData = eventData as ExtendedPointerEventData;
			if (trackedEventData != null && trackedEventData.pointerType == UIPointerType.Tracked)
			{
				this.PerformRaycast(trackedEventData, resultAppendList);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000453B0 File Offset: 0x000435B0
		internal void PerformRaycast(ExtendedPointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = new Ray(eventData.trackedDevicePosition, eventData.trackedDeviceOrientation * Vector3.forward);
			float hitDistance = this.m_MaxDistance;
			RaycastHit hit;
			if (this.m_CheckFor3DOcclusion && Physics.Raycast(ray, out hit, hitDistance, this.m_BlockingMask))
			{
				hitDistance = hit.distance;
			}
			if (this.m_CheckFor2DOcclusion)
			{
				float raycastDistance = hitDistance;
				RaycastHit2D hits = Physics2D.GetRayIntersection(ray, raycastDistance, this.m_BlockingMask);
				if (hits.collider != null)
				{
					hitDistance = hits.distance;
				}
			}
			this.m_RaycastResultsCache.Clear();
			this.SortedRaycastGraphics(this.canvas, ray, this.m_RaycastResultsCache);
			for (int i = 0; i < this.m_RaycastResultsCache.Count; i++)
			{
				bool validHit = true;
				TrackedDeviceRaycaster.RaycastHitData hitData = this.m_RaycastResultsCache[i];
				GameObject go = hitData.graphic.gameObject;
				if (this.m_IgnoreReversedGraphics)
				{
					Vector3 direction = ray.direction;
					Vector3 goDirection = go.transform.rotation * Vector3.forward;
					validHit = Vector3.Dot(direction, goDirection) > 0f;
				}
				validHit &= hitData.distance < hitDistance;
				if (validHit)
				{
					RaycastResult castResult = new RaycastResult
					{
						gameObject = go,
						module = this,
						distance = hitData.distance,
						index = (float)resultAppendList.Count,
						depth = hitData.graphic.depth,
						worldPosition = hitData.worldHitPosition,
						screenPosition = hitData.screenPosition
					};
					resultAppendList.Add(castResult);
				}
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00045574 File Offset: 0x00043774
		private void SortedRaycastGraphics(Canvas canvas, Ray ray, List<TrackedDeviceRaycaster.RaycastHitData> results)
		{
			IList<Graphic> graphics = GraphicRegistry.GetGraphicsForCanvas(canvas);
			TrackedDeviceRaycaster.s_SortedGraphics.Clear();
			for (int i = 0; i < graphics.Count; i++)
			{
				Graphic graphic = graphics[i];
				Vector3 worldPos;
				float distance;
				if (graphic.depth != -1 && TrackedDeviceRaycaster.RayIntersectsRectTransform(graphic.rectTransform, ray, out worldPos, out distance))
				{
					Vector2 screenPos = this.eventCamera.WorldToScreenPoint(worldPos);
					if (graphic.Raycast(screenPos, this.eventCamera))
					{
						TrackedDeviceRaycaster.s_SortedGraphics.Add(new TrackedDeviceRaycaster.RaycastHitData(graphic, worldPos, screenPos, distance));
					}
				}
			}
			TrackedDeviceRaycaster.s_SortedGraphics.Sort((TrackedDeviceRaycaster.RaycastHitData g1, TrackedDeviceRaycaster.RaycastHitData g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
			results.AddRange(TrackedDeviceRaycaster.s_SortedGraphics);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00045634 File Offset: 0x00043834
		private static bool RayIntersectsRectTransform(RectTransform transform, Ray ray, out Vector3 worldPosition, out float distance)
		{
			Vector3[] corners = new Vector3[4];
			transform.GetWorldCorners(corners);
			Plane plane = new Plane(corners[0], corners[1], corners[2]);
			float enter;
			if (plane.Raycast(ray, out enter))
			{
				Vector3 intersection = ray.GetPoint(enter);
				Vector3 bottomEdge = corners[3] - corners[0];
				Vector3 leftEdge = corners[1] - corners[0];
				float bottomDot = Vector3.Dot(intersection - corners[0], bottomEdge);
				if (Vector3.Dot(intersection - corners[0], leftEdge) >= 0f && bottomDot >= 0f)
				{
					Vector3 topEdge = corners[1] - corners[2];
					Vector3 rightEdge = corners[3] - corners[2];
					float num = Vector3.Dot(intersection - corners[2], topEdge);
					float rightDot = Vector3.Dot(intersection - corners[2], rightEdge);
					if (num >= 0f && rightDot >= 0f)
					{
						worldPosition = intersection;
						distance = enter;
						return true;
					}
				}
			}
			worldPosition = Vector3.zero;
			distance = 0f;
			return false;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0004576A File Offset: 0x0004396A
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

		// Token: 0x040006A4 RID: 1700
		[NonSerialized]
		private List<TrackedDeviceRaycaster.RaycastHitData> m_RaycastResultsCache = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x040006A5 RID: 1701
		internal static InlinedArray<TrackedDeviceRaycaster> s_Instances;

		// Token: 0x040006A6 RID: 1702
		private static readonly List<TrackedDeviceRaycaster.RaycastHitData> s_SortedGraphics = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x040006A7 RID: 1703
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics;

		// Token: 0x040006A8 RID: 1704
		[FormerlySerializedAs("checkFor2DOcclusion")]
		[SerializeField]
		private bool m_CheckFor2DOcclusion;

		// Token: 0x040006A9 RID: 1705
		[FormerlySerializedAs("checkFor3DOcclusion")]
		[SerializeField]
		private bool m_CheckFor3DOcclusion;

		// Token: 0x040006AA RID: 1706
		[Tooltip("Maximum distance (in 3D world space) that rays are traced to find a hit.")]
		[SerializeField]
		private float m_MaxDistance = 1000f;

		// Token: 0x040006AB RID: 1707
		[SerializeField]
		private LayerMask m_BlockingMask;

		// Token: 0x040006AC RID: 1708
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x02000120 RID: 288
		private struct RaycastHitData
		{
			// Token: 0x06000DD1 RID: 3537 RVA: 0x000457BD File Offset: 0x000439BD
			public RaycastHitData(Graphic graphic, Vector3 worldHitPosition, Vector2 screenPosition, float distance)
			{
				this.graphic = graphic;
				this.worldHitPosition = worldHitPosition;
				this.screenPosition = screenPosition;
				this.distance = distance;
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x000457DC File Offset: 0x000439DC
			public readonly Graphic graphic { get; }

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000457E4 File Offset: 0x000439E4
			public readonly Vector3 worldHitPosition { get; }

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000457EC File Offset: 0x000439EC
			public readonly Vector2 screenPosition { get; }

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000457F4 File Offset: 0x000439F4
			public readonly float distance { get; }
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x0200005F RID: 95
	[AddComponentMenu("UI/Rect Mask 2D", 14)]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class RectMask2D : UIBehaviour, IClipper, ICanvasRaycastFilter
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00010D3B File Offset: 0x0000EF3B
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00010D43 File Offset: 0x0000EF43
		public Vector4 padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.m_Padding = value;
				MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00010D52 File Offset: 0x0000EF52
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00010D5A File Offset: 0x0000EF5A
		public Vector2Int softness
		{
			get
			{
				return this.m_Softness;
			}
			set
			{
				this.m_Softness.x = Mathf.Max(0, value.x);
				this.m_Softness.y = Mathf.Max(0, value.y);
				MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00010D94 File Offset: 0x0000EF94
		internal Canvas Canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					List<Canvas> list = CollectionPool<List<Canvas>, Canvas>.Get();
					base.gameObject.GetComponentsInParent<Canvas>(false, list);
					if (list.Count > 0)
					{
						this.m_Canvas = list[list.Count - 1];
					}
					else
					{
						this.m_Canvas = null;
					}
					CollectionPool<List<Canvas>, Canvas>.Release(list);
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		public Rect canvasRect
		{
			get
			{
				return this.m_VertexClipper.GetCanvasRect(this.rectTransform, this.Canvas);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00010E10 File Offset: 0x0000F010
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_RectTransform) == null)
				{
					rectTransform = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010E36 File Offset: 0x0000F036
		protected RectMask2D()
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010E76 File Offset: 0x0000F076
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateClipRects = true;
			ClipperRegistry.Register(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00010E91 File Offset: 0x0000F091
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ClipTargets.Clear();
			this.m_MaskableTargets.Clear();
			this.m_Clippers.Clear();
			ClipperRegistry.Disable(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00010EC6 File Offset: 0x0000F0C6
		protected override void OnDestroy()
		{
			ClipperRegistry.Unregister(this);
			base.OnDestroy();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera, this.m_Padding);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		private Rect rootCanvasRect
		{
			get
			{
				this.rectTransform.GetWorldCorners(this.m_Corners);
				if (this.Canvas != null)
				{
					Canvas rootCanvas = this.Canvas.rootCanvas;
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = rootCanvas.transform.InverseTransformPoint(this.m_Corners[i]);
					}
				}
				return new Rect(this.m_Corners[0].x, this.m_Corners[0].y, this.m_Corners[2].x - this.m_Corners[0].x, this.m_Corners[2].y - this.m_Corners[0].y);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public virtual void PerformClipping()
		{
			if (this.Canvas == null)
			{
				return;
			}
			if (this.m_ShouldRecalculateClipRects)
			{
				MaskUtilities.GetRectMasksForClip(this, this.m_Clippers);
				this.m_ShouldRecalculateClipRects = false;
			}
			bool validRect = true;
			Rect clipRect = Clipping.FindCullAndClipWorldRect(this.m_Clippers, out validRect);
			RenderMode renderMode = this.Canvas.rootCanvas.renderMode;
			if ((renderMode == RenderMode.ScreenSpaceCamera || renderMode == RenderMode.ScreenSpaceOverlay) && !clipRect.Overlaps(this.rootCanvasRect, true))
			{
				clipRect = Rect.zero;
				validRect = false;
			}
			if (clipRect != this.m_LastClipRectCanvasSpace)
			{
				foreach (IClippable clippable in this.m_ClipTargets)
				{
					clippable.SetClipRect(clipRect, validRect);
				}
				using (HashSet<MaskableGraphic>.Enumerator enumerator2 = this.m_MaskableTargets.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MaskableGraphic maskableGraphic = enumerator2.Current;
						maskableGraphic.SetClipRect(clipRect, validRect);
						maskableGraphic.Cull(clipRect, validRect);
					}
					goto IL_01B5;
				}
			}
			if (this.m_ForceClip)
			{
				foreach (IClippable clippable2 in this.m_ClipTargets)
				{
					clippable2.SetClipRect(clipRect, validRect);
				}
				using (HashSet<MaskableGraphic>.Enumerator enumerator2 = this.m_MaskableTargets.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MaskableGraphic maskableTarget = enumerator2.Current;
						maskableTarget.SetClipRect(clipRect, validRect);
						if (maskableTarget.canvasRenderer.hasMoved)
						{
							maskableTarget.Cull(clipRect, validRect);
						}
					}
					goto IL_01B5;
				}
			}
			foreach (MaskableGraphic maskableGraphic2 in this.m_MaskableTargets)
			{
				maskableGraphic2.Cull(clipRect, validRect);
			}
			IL_01B5:
			this.m_LastClipRectCanvasSpace = clipRect;
			this.m_ForceClip = false;
			this.UpdateClipSoftness();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000111DC File Offset: 0x0000F3DC
		public virtual void UpdateClipSoftness()
		{
			if (this.Canvas == null)
			{
				return;
			}
			foreach (IClippable clippable in this.m_ClipTargets)
			{
				clippable.SetClipSoftness(this.m_Softness);
			}
			foreach (MaskableGraphic maskableGraphic in this.m_MaskableTargets)
			{
				maskableGraphic.SetClipSoftness(this.m_Softness);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001128C File Offset: 0x0000F48C
		public void AddClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			this.m_ShouldRecalculateClipRects = true;
			MaskableGraphic maskable = clippable as MaskableGraphic;
			if (maskable == null)
			{
				this.m_ClipTargets.Add(clippable);
			}
			else
			{
				this.m_MaskableTargets.Add(maskable);
			}
			this.m_ForceClip = true;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public void RemoveClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			this.m_ShouldRecalculateClipRects = true;
			clippable.SetClipRect(default(Rect), false);
			MaskableGraphic maskable = clippable as MaskableGraphic;
			if (maskable == null)
			{
				this.m_ClipTargets.Remove(clippable);
			}
			else
			{
				this.m_MaskableTargets.Remove(maskable);
			}
			this.m_ForceClip = true;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011333 File Offset: 0x0000F533
		protected override void OnTransformParentChanged()
		{
			this.m_Canvas = null;
			base.OnTransformParentChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011349 File Offset: 0x0000F549
		protected override void OnCanvasHierarchyChanged()
		{
			this.m_Canvas = null;
			base.OnCanvasHierarchyChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x040001B3 RID: 435
		[NonSerialized]
		private readonly RectangularVertexClipper m_VertexClipper = new RectangularVertexClipper();

		// Token: 0x040001B4 RID: 436
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040001B5 RID: 437
		[NonSerialized]
		private HashSet<MaskableGraphic> m_MaskableTargets = new HashSet<MaskableGraphic>();

		// Token: 0x040001B6 RID: 438
		[NonSerialized]
		private HashSet<IClippable> m_ClipTargets = new HashSet<IClippable>();

		// Token: 0x040001B7 RID: 439
		[NonSerialized]
		private bool m_ShouldRecalculateClipRects;

		// Token: 0x040001B8 RID: 440
		[NonSerialized]
		private List<RectMask2D> m_Clippers = new List<RectMask2D>();

		// Token: 0x040001B9 RID: 441
		[NonSerialized]
		private Rect m_LastClipRectCanvasSpace;

		// Token: 0x040001BA RID: 442
		[NonSerialized]
		private bool m_ForceClip;

		// Token: 0x040001BB RID: 443
		[SerializeField]
		private Vector4 m_Padding;

		// Token: 0x040001BC RID: 444
		[SerializeField]
		private Vector2Int m_Softness;

		// Token: 0x040001BD RID: 445
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x040001BE RID: 446
		private Vector3[] m_Corners = new Vector3[4];
	}
}

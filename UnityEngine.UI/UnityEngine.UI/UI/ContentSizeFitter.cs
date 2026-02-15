using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000040 RID: 64
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000DC24 File Offset: 0x0000BE24
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000DC42 File Offset: 0x0000BE42
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000DC4A File Offset: 0x0000BE4A
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000DC60 File Offset: 0x0000BE60
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DC82 File Offset: 0x0000BE82
		protected ContentSizeFitter()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DC98 File Offset: 0x0000BE98
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DCB6 File Offset: 0x0000BEB6
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitting = ((axis == 0) ? this.horizontalFit : this.verticalFit);
			if (fitting == ContentSizeFitter.FitMode.Unconstrained)
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.None);
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
			if (fitting == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
				return;
			}
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DD69 File Offset: 0x0000BF69
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x0400014F RID: 335
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x04000150 RID: 336
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x04000151 RID: 337
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000152 RID: 338
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000041 RID: 65
		public enum FitMode
		{
			// Token: 0x04000154 RID: 340
			Unconstrained,
			// Token: 0x04000155 RID: 341
			MinSize,
			// Token: 0x04000156 RID: 342
			PreferredSize
		}
	}
}

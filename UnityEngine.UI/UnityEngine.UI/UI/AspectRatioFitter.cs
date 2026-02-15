using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003A RID: 58
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class AspectRatioFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D3B5 File Offset: 0x0000B5B5
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000D3D3 File Offset: 0x0000B5D3
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000D3DB File Offset: 0x0000B5DB
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
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

		// Token: 0x06000248 RID: 584 RVA: 0x0000D413 File Offset: 0x0000B613
		protected AspectRatioFitter()
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D426 File Offset: 0x0000B626
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_DoesParentExist = this.rectTransform.parent;
			this.SetDirty();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D450 File Offset: 0x0000B650
		protected override void Start()
		{
			base.Start();
			if (!this.IsComponentValidOnObject() || !this.IsAspectModeValid())
			{
				base.enabled = false;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D46F File Offset: 0x0000B66F
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D48D File Offset: 0x0000B68D
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_DoesParentExist = this.rectTransform.parent;
			this.SetDirty();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		protected virtual void Update()
		{
			if (this.m_DelayedSetDirty)
			{
				this.m_DelayedSetDirty = false;
				this.SetDirty();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		private void UpdateRect()
		{
			if (!this.IsActive() || !this.IsComponentValidOnObject())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
				if (this.DoesParentExists())
				{
					this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					this.rectTransform.anchorMin = Vector2.zero;
					this.rectTransform.anchorMax = Vector2.one;
					this.rectTransform.anchoredPosition = Vector2.zero;
					Vector2 sizeDelta = Vector2.zero;
					Vector2 parentSize = this.GetParentSize();
					if ((parentSize.y * this.aspectRatio < parentSize.x) ^ (this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent))
					{
						sizeDelta.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
					}
					else
					{
						sizeDelta.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
					}
					this.rectTransform.sizeDelta = sizeDelta;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D664 File Offset: 0x0000B864
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		private Vector2 GetParentSize()
		{
			RectTransform parent = this.rectTransform.parent as RectTransform;
			if (parent)
			{
				return parent.rect.size;
			}
			return Vector2.zero;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		protected void SetDirty()
		{
			this.UpdateRect();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		public bool IsComponentValidOnObject()
		{
			Canvas canvas = base.gameObject.GetComponent<Canvas>();
			return !canvas || !canvas.isRootCanvas || canvas.renderMode == RenderMode.WorldSpace;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D71D File Offset: 0x0000B91D
		public bool IsAspectModeValid()
		{
			return this.DoesParentExists() || (this.aspectMode != AspectRatioFitter.AspectMode.EnvelopeParent && this.aspectMode != AspectRatioFitter.AspectMode.FitInParent);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000D73C File Offset: 0x0000B93C
		private bool DoesParentExists()
		{
			return this.m_DoesParentExist;
		}

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x04000128 RID: 296
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000129 RID: 297
		private bool m_DelayedSetDirty;

		// Token: 0x0400012A RID: 298
		private bool m_DoesParentExist;

		// Token: 0x0400012B RID: 299
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0200003B RID: 59
		public enum AspectMode
		{
			// Token: 0x0400012D RID: 301
			None,
			// Token: 0x0400012E RID: 302
			WidthControlsHeight,
			// Token: 0x0400012F RID: 303
			HeightControlsWidth,
			// Token: 0x04000130 RID: 304
			FitInParent,
			// Token: 0x04000131 RID: 305
			EnvelopeParent
		}
	}
}

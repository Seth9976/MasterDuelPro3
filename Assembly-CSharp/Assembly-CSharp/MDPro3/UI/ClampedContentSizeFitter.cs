using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013F3 RID: 5107
	public class ClampedContentSizeFitter : ContentSizeFitter
	{
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x0600941C RID: 37916 RVA: 0x00151BA8 File Offset: 0x0014FDA8
		// (set) Token: 0x0600941D RID: 37917 RVA: 0x00151BB0 File Offset: 0x0014FDB0
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x0600941E RID: 37918 RVA: 0x00151BB9 File Offset: 0x0014FDB9
		// (set) Token: 0x0600941F RID: 37919 RVA: 0x00151BC1 File Offset: 0x0014FDC1
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06009420 RID: 37920 RVA: 0x00151BCC File Offset: 0x0014FDCC
		private RectTransform RectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_Rect) == null)
				{
					rectTransform = (this.m_Rect = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x06009421 RID: 37921 RVA: 0x00151BF2 File Offset: 0x0014FDF2
		protected override void OnEnable()
		{
			base.OnEnable();
			base.SetDirty();
		}

		// Token: 0x06009422 RID: 37922 RVA: 0x00151C00 File Offset: 0x0014FE00
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06009423 RID: 37923 RVA: 0x00151C14 File Offset: 0x0014FE14
		public override void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			if (base.horizontalFit == ContentSizeFitter.FitMode.Unconstrained)
			{
				this.m_Tracker.Add(this, this.RectTransform, DrivenTransformProperties.None);
				return;
			}
			this.m_Tracker.Add(this, this.RectTransform, DrivenTransformProperties.SizeDeltaX);
			float targetWidth = ((base.horizontalFit == ContentSizeFitter.FitMode.MinSize) ? LayoutUtility.GetMinWidth(this.RectTransform) : LayoutUtility.GetPreferredWidth(this.RectTransform));
			if (this.m_MinWidth > 0f)
			{
				targetWidth = Mathf.Max(targetWidth, this.m_MinWidth);
			}
			if (this.m_MaxWidth > 0f)
			{
				targetWidth = Mathf.Min(targetWidth, this.m_MaxWidth);
			}
			this.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
		}

		// Token: 0x0400D26F RID: 53871
		[SerializeField]
		private float m_MaxWidth = -1f;

		// Token: 0x0400D270 RID: 53872
		[SerializeField]
		private float m_MinWidth;

		// Token: 0x0400D271 RID: 53873
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400D272 RID: 53874
		private RectTransform m_Rect;
	}
}

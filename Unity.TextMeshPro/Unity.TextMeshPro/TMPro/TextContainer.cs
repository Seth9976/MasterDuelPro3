using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x020000B6 RID: 182
	[RequireComponent(typeof(RectTransform))]
	public class TextContainer : UIBehaviour
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0002EE69 File Offset: 0x0002D069
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0002EE71 File Offset: 0x0002D071
		public bool hasChanged
		{
			get
			{
				return this.m_hasChanged;
			}
			set
			{
				this.m_hasChanged = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0002EE7A File Offset: 0x0002D07A
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0002EE82 File Offset: 0x0002D082
		public Vector2 pivot
		{
			get
			{
				return this.m_pivot;
			}
			set
			{
				if (this.m_pivot != value)
				{
					this.m_pivot = value;
					this.m_anchorPosition = this.GetAnchorPosition(this.m_pivot);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		public TextContainerAnchors anchorPosition
		{
			get
			{
				return this.m_anchorPosition;
			}
			set
			{
				if (this.m_anchorPosition != value)
				{
					this.m_anchorPosition = value;
					this.m_pivot = this.GetPivot(this.m_anchorPosition);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0002EEF1 File Offset: 0x0002D0F1
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0002EEF9 File Offset: 0x0002D0F9
		public Rect rect
		{
			get
			{
				return this.m_rect;
			}
			set
			{
				if (this.m_rect != value)
				{
					this.m_rect = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0002EF1D File Offset: 0x0002D11D
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0002EF3C File Offset: 0x0002D13C
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_rect.width, this.m_rect.height);
			}
			set
			{
				if (new Vector2(this.m_rect.width, this.m_rect.height) != value)
				{
					this.SetRect(value);
					this.m_hasChanged = true;
					this.m_isDefaultWidth = false;
					this.m_isDefaultHeight = false;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0002EF8E File Offset: 0x0002D18E
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0002EF9B File Offset: 0x0002D19B
		public float width
		{
			get
			{
				return this.m_rect.width;
			}
			set
			{
				this.SetRect(new Vector2(value, this.m_rect.height));
				this.m_hasChanged = true;
				this.m_isDefaultWidth = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0002EFD5 File Offset: 0x0002D1D5
		public float height
		{
			get
			{
				return this.m_rect.height;
			}
			set
			{
				this.SetRect(new Vector2(this.m_rect.width, value));
				this.m_hasChanged = true;
				this.m_isDefaultHeight = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0002F002 File Offset: 0x0002D202
		public bool isDefaultWidth
		{
			get
			{
				return this.m_isDefaultWidth;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0002F00A File Offset: 0x0002D20A
		public bool isDefaultHeight
		{
			get
			{
				return this.m_isDefaultHeight;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0002F012 File Offset: 0x0002D212
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0002F01A File Offset: 0x0002D21A
		public bool isAutoFitting
		{
			get
			{
				return this.m_isAutoFitting;
			}
			set
			{
				this.m_isAutoFitting = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002F023 File Offset: 0x0002D223
		public Vector3[] corners
		{
			get
			{
				return this.m_corners;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0002F02B File Offset: 0x0002D22B
		public Vector3[] worldCorners
		{
			get
			{
				return this.m_worldCorners;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002F033 File Offset: 0x0002D233
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0002F03B File Offset: 0x0002D23B
		public Vector4 margins
		{
			get
			{
				return this.m_margins;
			}
			set
			{
				if (this.m_margins != value)
				{
					this.m_margins = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002F05F File Offset: 0x0002D25F
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0002F081 File Offset: 0x0002D281
		public TextMeshPro textMeshPro
		{
			get
			{
				if (this.m_textMeshPro == null)
				{
					this.m_textMeshPro = base.GetComponent<TextMeshPro>();
				}
				return this.m_textMeshPro;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002F0A3 File Offset: 0x0002D2A3
		protected override void Awake()
		{
			Debug.LogWarning("The Text Container component is now Obsolete and can safely be removed from [" + base.gameObject.name + "].", this);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002F0C5 File Offset: 0x0002D2C5
		protected override void OnEnable()
		{
			this.OnContainerChanged();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected override void OnDisable()
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		private void OnContainerChanged()
		{
			this.UpdateCorners();
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.sizeDelta = this.size;
				this.m_rectTransform.hasChanged = true;
			}
			if (this.textMeshPro != null)
			{
				this.m_textMeshPro.SetVerticesDirty();
				this.m_textMeshPro.margin = this.m_margins;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002F138 File Offset: 0x0002D338
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.rectTransform == null)
			{
				this.m_rectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			if (this.m_rectTransform.sizeDelta != TextContainer.k_defaultSize)
			{
				this.size = this.m_rectTransform.sizeDelta;
			}
			this.pivot = this.m_rectTransform.pivot;
			this.m_hasChanged = true;
			this.OnContainerChanged();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002F1AA File Offset: 0x0002D3AA
		private void SetRect(Vector2 size)
		{
			this.m_rect = new Rect(this.m_rect.x, this.m_rect.y, size.x, size.y);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0002F1DC File Offset: 0x0002D3DC
		private void UpdateCorners()
		{
			this.m_corners[0] = new Vector3(-this.m_pivot.x * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			this.m_corners[1] = new Vector3(-this.m_pivot.x * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[2] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[3] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.pivot = this.m_pivot;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002F320 File Offset: 0x0002D520
		private Vector2 GetPivot(TextContainerAnchors anchor)
		{
			Vector2 pivot = Vector2.zero;
			switch (anchor)
			{
			case TextContainerAnchors.TopLeft:
				pivot = new Vector2(0f, 1f);
				break;
			case TextContainerAnchors.Top:
				pivot = new Vector2(0.5f, 1f);
				break;
			case TextContainerAnchors.TopRight:
				pivot = new Vector2(1f, 1f);
				break;
			case TextContainerAnchors.Left:
				pivot = new Vector2(0f, 0.5f);
				break;
			case TextContainerAnchors.Middle:
				pivot = new Vector2(0.5f, 0.5f);
				break;
			case TextContainerAnchors.Right:
				pivot = new Vector2(1f, 0.5f);
				break;
			case TextContainerAnchors.BottomLeft:
				pivot = new Vector2(0f, 0f);
				break;
			case TextContainerAnchors.Bottom:
				pivot = new Vector2(0.5f, 0f);
				break;
			case TextContainerAnchors.BottomRight:
				pivot = new Vector2(1f, 0f);
				break;
			}
			return pivot;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0002F414 File Offset: 0x0002D614
		private TextContainerAnchors GetAnchorPosition(Vector2 pivot)
		{
			if (pivot == new Vector2(0f, 1f))
			{
				return TextContainerAnchors.TopLeft;
			}
			if (pivot == new Vector2(0.5f, 1f))
			{
				return TextContainerAnchors.Top;
			}
			if (pivot == new Vector2(1f, 1f))
			{
				return TextContainerAnchors.TopRight;
			}
			if (pivot == new Vector2(0f, 0.5f))
			{
				return TextContainerAnchors.Left;
			}
			if (pivot == new Vector2(0.5f, 0.5f))
			{
				return TextContainerAnchors.Middle;
			}
			if (pivot == new Vector2(1f, 0.5f))
			{
				return TextContainerAnchors.Right;
			}
			if (pivot == new Vector2(0f, 0f))
			{
				return TextContainerAnchors.BottomLeft;
			}
			if (pivot == new Vector2(0.5f, 0f))
			{
				return TextContainerAnchors.Bottom;
			}
			if (pivot == new Vector2(1f, 0f))
			{
				return TextContainerAnchors.BottomRight;
			}
			return TextContainerAnchors.Custom;
		}

		// Token: 0x04000635 RID: 1589
		private bool m_hasChanged;

		// Token: 0x04000636 RID: 1590
		[SerializeField]
		private Vector2 m_pivot;

		// Token: 0x04000637 RID: 1591
		[SerializeField]
		private TextContainerAnchors m_anchorPosition = TextContainerAnchors.Middle;

		// Token: 0x04000638 RID: 1592
		[SerializeField]
		private Rect m_rect;

		// Token: 0x04000639 RID: 1593
		private bool m_isDefaultWidth;

		// Token: 0x0400063A RID: 1594
		private bool m_isDefaultHeight;

		// Token: 0x0400063B RID: 1595
		private bool m_isAutoFitting;

		// Token: 0x0400063C RID: 1596
		private Vector3[] m_corners = new Vector3[4];

		// Token: 0x0400063D RID: 1597
		private Vector3[] m_worldCorners = new Vector3[4];

		// Token: 0x0400063E RID: 1598
		[SerializeField]
		private Vector4 m_margins;

		// Token: 0x0400063F RID: 1599
		private RectTransform m_rectTransform;

		// Token: 0x04000640 RID: 1600
		private static Vector2 k_defaultSize = new Vector2(100f, 100f);

		// Token: 0x04000641 RID: 1601
		private TextMeshPro m_textMeshPro;
	}
}

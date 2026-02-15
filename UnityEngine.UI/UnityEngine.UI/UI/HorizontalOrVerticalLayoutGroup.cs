using System;

namespace UnityEngine.UI
{
	// Token: 0x02000047 RID: 71
	[ExecuteAlways]
	public abstract class HorizontalOrVerticalLayoutGroup : LayoutGroup
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000E4DB File Offset: 0x0000C6DB
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000E4FA File Offset: 0x0000C6FA
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000E509 File Offset: 0x0000C709
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000E511 File Offset: 0x0000C711
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000E520 File Offset: 0x0000C720
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000E528 File Offset: 0x0000C728
		public bool childControlWidth
		{
			get
			{
				return this.m_ChildControlWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlWidth, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000E537 File Offset: 0x0000C737
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000E53F File Offset: 0x0000C73F
		public bool childControlHeight
		{
			get
			{
				return this.m_ChildControlHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlHeight, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000E54E File Offset: 0x0000C74E
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000E556 File Offset: 0x0000C756
		public bool childScaleWidth
		{
			get
			{
				return this.m_ChildScaleWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleWidth, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000E565 File Offset: 0x0000C765
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000E56D File Offset: 0x0000C76D
		public bool childScaleHeight
		{
			get
			{
				return this.m_ChildScaleHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleHeight, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000E57C File Offset: 0x0000C77C
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000E584 File Offset: 0x0000C784
		public bool reverseArrangement
		{
			get
			{
				return this.m_ReverseArrangement;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ReverseArrangement, value);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E594 File Offset: 0x0000C794
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float combinedPadding = (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
			bool controlSize = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool useScale = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool childForceExpandSize = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float totalMin = combinedPadding;
			float totalPreferred = combinedPadding;
			float totalFlexible = 0f;
			bool alongOtherAxis = isVertical ^ (axis == 1);
			int rectChildrenCount = base.rectChildren.Count;
			for (int i = 0; i < rectChildrenCount; i++)
			{
				RectTransform child = base.rectChildren[i];
				float min;
				float preferred;
				float flexible;
				this.GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
				if (useScale)
				{
					float scaleFactor = child.localScale[axis];
					min *= scaleFactor;
					preferred *= scaleFactor;
					flexible *= scaleFactor;
				}
				if (alongOtherAxis)
				{
					totalMin = Mathf.Max(min + combinedPadding, totalMin);
					totalPreferred = Mathf.Max(preferred + combinedPadding, totalPreferred);
					totalFlexible = Mathf.Max(flexible, totalFlexible);
				}
				else
				{
					totalMin += min + this.spacing;
					totalPreferred += preferred + this.spacing;
					totalFlexible += flexible;
				}
			}
			if (!alongOtherAxis && base.rectChildren.Count > 0)
			{
				totalMin -= this.spacing;
				totalPreferred -= this.spacing;
			}
			totalPreferred = Mathf.Max(totalMin, totalPreferred);
			base.SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, axis);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E708 File Offset: 0x0000C908
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float size = base.rectTransform.rect.size[axis];
			bool controlSize = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool useScale = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool childForceExpandSize = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float alignmentOnAxis = base.GetAlignmentOnAxis(axis);
			bool flag = isVertical ^ (axis == 1);
			int startIndex = (this.m_ReverseArrangement ? (base.rectChildren.Count - 1) : 0);
			int endIndex = (this.m_ReverseArrangement ? 0 : base.rectChildren.Count);
			int increment = (this.m_ReverseArrangement ? (-1) : 1);
			if (flag)
			{
				float innerSize = size - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
				int i = startIndex;
				while (this.m_ReverseArrangement ? (i >= endIndex) : (i < endIndex))
				{
					RectTransform child = base.rectChildren[i];
					float min;
					float preferred;
					float flexible;
					this.GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
					float scaleFactor = (useScale ? child.localScale[axis] : 1f);
					float requiredSpace = Mathf.Clamp(innerSize, min, (flexible > 0f) ? size : preferred);
					float startOffset = base.GetStartOffset(axis, requiredSpace * scaleFactor);
					if (controlSize)
					{
						base.SetChildAlongAxisWithScale(child, axis, startOffset, requiredSpace, scaleFactor);
					}
					else
					{
						float offsetInCell = (requiredSpace - child.sizeDelta[axis]) * alignmentOnAxis;
						base.SetChildAlongAxisWithScale(child, axis, startOffset + offsetInCell, scaleFactor);
					}
					i += increment;
				}
				return;
			}
			float pos = (float)((axis == 0) ? base.padding.left : base.padding.top);
			float itemFlexibleMultiplier = 0f;
			float surplusSpace = size - base.GetTotalPreferredSize(axis);
			if (surplusSpace > 0f)
			{
				if (base.GetTotalFlexibleSize(axis) == 0f)
				{
					pos = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical));
				}
				else if (base.GetTotalFlexibleSize(axis) > 0f)
				{
					itemFlexibleMultiplier = surplusSpace / base.GetTotalFlexibleSize(axis);
				}
			}
			float minMaxLerp = 0f;
			if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
			{
				minMaxLerp = Mathf.Clamp01((size - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
			}
			int j = startIndex;
			while (this.m_ReverseArrangement ? (j >= endIndex) : (j < endIndex))
			{
				RectTransform child2 = base.rectChildren[j];
				float min2;
				float preferred2;
				float flexible2;
				this.GetChildSizes(child2, axis, controlSize, childForceExpandSize, out min2, out preferred2, out flexible2);
				float scaleFactor2 = (useScale ? child2.localScale[axis] : 1f);
				float childSize = Mathf.Lerp(min2, preferred2, minMaxLerp);
				childSize += flexible2 * itemFlexibleMultiplier;
				if (controlSize)
				{
					base.SetChildAlongAxisWithScale(child2, axis, pos, childSize, scaleFactor2);
				}
				else
				{
					float offsetInCell2 = (childSize - child2.sizeDelta[axis]) * alignmentOnAxis;
					base.SetChildAlongAxisWithScale(child2, axis, pos + offsetInCell2, scaleFactor2);
				}
				pos += childSize * scaleFactor2 + this.spacing;
				j += increment;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000EA48 File Offset: 0x0000CC48
		private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand, out float min, out float preferred, out float flexible)
		{
			if (!controlSize)
			{
				min = child.sizeDelta[axis];
				preferred = min;
				flexible = 0f;
			}
			else
			{
				min = LayoutUtility.GetMinSize(child, axis);
				preferred = LayoutUtility.GetPreferredSize(child, axis);
				flexible = LayoutUtility.GetFlexibleSize(child, axis);
			}
			if (childForceExpand)
			{
				flexible = Mathf.Max(flexible, 1f);
			}
		}

		// Token: 0x04000169 RID: 361
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		protected bool m_ChildControlWidth = true;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		protected bool m_ChildControlHeight = true;

		// Token: 0x0400016E RID: 366
		[SerializeField]
		protected bool m_ChildScaleWidth;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		protected bool m_ChildScaleHeight;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		protected bool m_ReverseArrangement;
	}
}

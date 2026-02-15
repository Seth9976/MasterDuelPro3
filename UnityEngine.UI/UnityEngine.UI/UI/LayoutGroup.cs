using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x0200004E RID: 78
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public abstract class LayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000EC5B File Offset: 0x0000CE5B
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000EC63 File Offset: 0x0000CE63
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000EC72 File Offset: 0x0000CE72
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000EC7A File Offset: 0x0000CE7A
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000EC89 File Offset: 0x0000CE89
		protected RectTransform rectTransform
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000ECAB File Offset: 0x0000CEAB
		protected List<RectTransform> rectChildren
		{
			get
			{
				return this.m_RectChildren;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_RectChildren.Clear();
			List<Component> toIgnoreList = CollectionPool<List<Component>, Component>.Get();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				RectTransform rect = this.rectTransform.GetChild(i) as RectTransform;
				if (!(rect == null) && rect.gameObject.activeInHierarchy)
				{
					rect.GetComponents(typeof(ILayoutIgnorer), toIgnoreList);
					if (toIgnoreList.Count == 0)
					{
						this.m_RectChildren.Add(rect);
					}
					else
					{
						for (int j = 0; j < toIgnoreList.Count; j++)
						{
							if (!((ILayoutIgnorer)toIgnoreList[j]).ignoreLayout)
							{
								this.m_RectChildren.Add(rect);
								break;
							}
						}
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(toIgnoreList);
			this.m_Tracker.Clear();
		}

		// Token: 0x060002DB RID: 731
		public abstract void CalculateLayoutInputVertical();

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000ED89 File Offset: 0x0000CF89
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000ED92 File Offset: 0x0000CF92
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000ED9B File Offset: 0x0000CF9B
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000EDAD File Offset: 0x0000CFAD
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000093DE File Offset: 0x000075DE
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060002E3 RID: 739
		public abstract void SetLayoutHorizontal();

		// Token: 0x060002E4 RID: 740
		public abstract void SetLayoutVertical();

		// Token: 0x060002E5 RID: 741 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		protected LayoutGroup()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EE15 File Offset: 0x0000D015
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EE23 File Offset: 0x0000D023
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EE41 File Offset: 0x0000D041
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EE49 File Offset: 0x0000D049
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EE57 File Offset: 0x0000D057
		protected float GetTotalPreferredSize(int axis)
		{
			return this.m_TotalPreferredSize[axis];
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000EE65 File Offset: 0x0000D065
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000EE74 File Offset: 0x0000D074
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float requiredSpace = requiredSpaceWithoutPadding + (float)((axis == 0) ? this.padding.horizontal : this.padding.vertical);
			float surplusSpace = this.rectTransform.rect.size[axis] - requiredSpace;
			float alignmentOnAxis = this.GetAlignmentOnAxis(axis);
			return (float)((axis == 0) ? this.padding.left : this.padding.top) + surplusSpace * alignmentOnAxis;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		protected float GetAlignmentOnAxis(int axis)
		{
			if (axis == 0)
			{
				return (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000EF0C File Offset: 0x0000D10C
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000EF38 File Offset: 0x0000D138
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, 1f);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000EF54 File Offset: 0x0000D154
		protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? DrivenTransformProperties.AnchoredPositionX : DrivenTransformProperties.AnchoredPositionY));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = ((axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (-pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor));
			rect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F005 File Offset: 0x0000D205
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, size, 1f);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F024 File Offset: 0x0000D224
		protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float size, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? (DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.SizeDeltaX) : (DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.SizeDeltaY)));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 sizeDelta = rect.sizeDelta;
			sizeDelta[axis] = size;
			rect.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = ((axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (-pos - size * (1f - rect.pivot[axis]) * scaleFactor));
			rect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F0DB File Offset: 0x0000D2DB
		private bool isRootLayoutGroup
		{
			get
			{
				return base.transform.parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000F112 File Offset: 0x0000D312
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000EE41 File Offset: 0x0000D041
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F128 File Offset: 0x0000D328
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F179 File Offset: 0x0000D379
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (!CanvasUpdateRegistry.IsRebuildingLayout())
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
				return;
			}
			base.StartCoroutine(this.DelayedSetDirty(this.rectTransform));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F1AA File Offset: 0x0000D3AA
		private IEnumerator DelayedSetDirty(RectTransform rectTransform)
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			yield break;
		}

		// Token: 0x04000179 RID: 377
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x0400017A RID: 378
		[SerializeField]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x0400017B RID: 379
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x0400017C RID: 380
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400017D RID: 381
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x0400017E RID: 382
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x0400017F RID: 383
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x04000180 RID: 384
		[NonSerialized]
		private List<RectTransform> m_RectChildren = new List<RectTransform>();
	}
}

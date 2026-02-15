using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200004D RID: 77
	[AddComponentMenu("Layout/Layout Element", 140)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000EAEE File Offset: 0x0000CCEE
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000EAF6 File Offset: 0x0000CCF6
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000EB2A File Offset: 0x0000CD2A
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000EB32 File Offset: 0x0000CD32
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000EB48 File Offset: 0x0000CD48
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000EB66 File Offset: 0x0000CD66
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000EB6E File Offset: 0x0000CD6E
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000EB84 File Offset: 0x0000CD84
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000EBA2 File Offset: 0x0000CDA2
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		public virtual int layoutPriority
		{
			get
			{
				return this.m_LayoutPriority;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_LayoutPriority, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		protected LayoutElement()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000EC32 File Offset: 0x0000CE32
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000EC40 File Offset: 0x0000CE40
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x04000171 RID: 369
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x04000174 RID: 372
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x04000175 RID: 373
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x04000176 RID: 374
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x04000177 RID: 375
		[SerializeField]
		private float m_FlexibleHeight = -1f;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		private int m_LayoutPriority = 1;
	}
}

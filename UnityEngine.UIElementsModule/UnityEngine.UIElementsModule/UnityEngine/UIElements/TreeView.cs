using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000160 RID: 352
	public class TreeView : BaseTreeView
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000336BF File Offset: 0x000318BF
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000336C8 File Offset: 0x000318C8
		[CreateProperty]
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = value != this.m_MakeItem;
				if (flag)
				{
					this.m_MakeItem = value;
					base.Rebuild();
					base.NotifyPropertyChanged(in TreeView.makeItemProperty);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00033702 File Offset: 0x00031902
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0003370C File Offset: 0x0003190C
		[CreateProperty]
		public VisualTreeAsset itemTemplate
		{
			get
			{
				return this.m_ItemTemplate;
			}
			set
			{
				bool flag = this.m_ItemTemplate == value;
				if (!flag)
				{
					this.m_ItemTemplate = value;
					bool flag2 = this.makeItem != this.m_TemplateMakeItem;
					if (flag2)
					{
						this.makeItem = this.m_TemplateMakeItem;
					}
					else
					{
						base.Rebuild();
					}
					base.NotifyPropertyChanged(in TreeView.itemTemplateProperty);
				}
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0003376C File Offset: 0x0003196C
		private VisualElement TemplateMakeItem()
		{
			bool flag = this.m_ItemTemplate != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_ItemTemplate.Instantiate();
			}
			else
			{
				visualElement = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
			}
			return visualElement;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x000337A7 File Offset: 0x000319A7
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x000337B0 File Offset: 0x000319B0
		[CreateProperty]
		public Action<VisualElement, int> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				bool flag = value != this.m_BindItem;
				if (flag)
				{
					this.m_BindItem = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in TreeView.bindItemProperty);
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x000337EA File Offset: 0x000319EA
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x000337F4 File Offset: 0x000319F4
		[CreateProperty]
		public Action<VisualElement, int> unbindItem
		{
			get
			{
				return this.m_UnbindItem;
			}
			set
			{
				bool flag = value != this.m_UnbindItem;
				if (flag)
				{
					this.m_UnbindItem = value;
					base.NotifyPropertyChanged(in TreeView.unbindItemProperty);
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00033827 File Offset: 0x00031A27
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00033830 File Offset: 0x00031A30
		[CreateProperty]
		public Action<VisualElement> destroyItem
		{
			get
			{
				return this.m_DestroyItem;
			}
			set
			{
				bool flag = value != this.m_DestroyItem;
				if (flag)
				{
					this.m_DestroyItem = value;
					base.NotifyPropertyChanged(in TreeView.destroyItemProperty);
				}
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00033864 File Offset: 0x00031A64
		internal override bool HasValidDataAndBindings()
		{
			return base.HasValidDataAndBindings() && this.makeItem != null == (this.bindItem != null);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00033895 File Offset: 0x00031A95
		protected override CollectionViewController CreateViewController()
		{
			return new DefaultTreeViewController<object>();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003389C File Offset: 0x00031A9C
		public TreeView()
			: this(null, null)
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000338A8 File Offset: 0x00031AA8
		public TreeView(Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
			: base(-1)
		{
			this.makeItem = makeItem;
			this.bindItem = bindItem;
			this.m_TemplateMakeItem = new Func<VisualElement>(this.TemplateMakeItem);
		}

		// Token: 0x040006DB RID: 1755
		internal static readonly BindingId itemTemplateProperty = "itemTemplate";

		// Token: 0x040006DC RID: 1756
		internal static readonly BindingId makeItemProperty = "makeItem";

		// Token: 0x040006DD RID: 1757
		internal static readonly BindingId bindItemProperty = "bindItem";

		// Token: 0x040006DE RID: 1758
		internal static readonly BindingId unbindItemProperty = "unbindItem";

		// Token: 0x040006DF RID: 1759
		internal static readonly BindingId destroyItemProperty = "destroyItem";

		// Token: 0x040006E0 RID: 1760
		private Func<VisualElement> m_MakeItem;

		// Token: 0x040006E1 RID: 1761
		private Func<VisualElement> m_TemplateMakeItem;

		// Token: 0x040006E2 RID: 1762
		private VisualTreeAsset m_ItemTemplate;

		// Token: 0x040006E3 RID: 1763
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x040006E4 RID: 1764
		private Action<VisualElement, int> m_UnbindItem;

		// Token: 0x040006E5 RID: 1765
		private Action<VisualElement> m_DestroyItem;

		// Token: 0x02000161 RID: 353
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TreeView, TreeView.UxmlTraits>
		{
		}

		// Token: 0x02000162 RID: 354
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseTreeView.UxmlTraits
		{
			// Token: 0x06000A9A RID: 2714 RVA: 0x0003393C File Offset: 0x00031B3C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TreeView view = ve as TreeView;
				VisualTreeAsset itemTemplate;
				bool flag = this.m_ItemTemplate.TryGetValueFromBag(bag, cc, out itemTemplate);
				if (flag)
				{
					view.itemTemplate = itemTemplate;
				}
			}

			// Token: 0x040006E6 RID: 1766
			private UxmlAssetAttributeDescription<VisualTreeAsset> m_ItemTemplate = new UxmlAssetAttributeDescription<VisualTreeAsset>
			{
				name = "item-template"
			};
		}
	}
}

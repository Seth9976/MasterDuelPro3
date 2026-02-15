using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F5 RID: 245
	public class ListView : BaseListView
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000245E1 File Offset: 0x000227E1
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x000245EC File Offset: 0x000227EC
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
					base.NotifyPropertyChanged(in ListView.makeItemProperty);
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00024626 File Offset: 0x00022826
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00024630 File Offset: 0x00022830
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
					bool flag2 = this.m_TemplateMakeItem != this.makeItem;
					if (flag2)
					{
						this.makeItem = this.m_TemplateMakeItem;
					}
					else
					{
						base.Rebuild();
					}
					base.NotifyPropertyChanged(in ListView.itemTemplateProperty);
				}
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00024690 File Offset: 0x00022890
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000246CB File Offset: 0x000228CB
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x000246D4 File Offset: 0x000228D4
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
					base.NotifyPropertyChanged(in ListView.bindItemProperty);
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0002470E File Offset: 0x0002290E
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00024718 File Offset: 0x00022918
		[CreateProperty]
		public Action<VisualElement, int> unbindItem
		{
			get
			{
				return this.m_UnbindItem;
			}
			set
			{
				bool flag = value == this.m_UnbindItem;
				if (!flag)
				{
					this.m_UnbindItem = value;
					base.NotifyPropertyChanged(in ListView.unbindItemProperty);
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0002474B File Offset: 0x0002294B
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x00024754 File Offset: 0x00022954
		[CreateProperty]
		public Action<VisualElement> destroyItem
		{
			get
			{
				return this.m_DestroyItem;
			}
			set
			{
				bool flag = value == this.m_DestroyItem;
				if (!flag)
				{
					this.m_DestroyItem = value;
					base.NotifyPropertyChanged(in ListView.destroyItemProperty);
				}
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00024788 File Offset: 0x00022988
		internal override bool HasValidDataAndBindings()
		{
			return base.HasValidDataAndBindings() && ((base.autoAssignSource && this.makeItem != null) || this.makeItem != null == (this.bindItem != null));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000247CC File Offset: 0x000229CC
		protected override CollectionViewController CreateViewController()
		{
			return new ListViewController();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000247D3 File Offset: 0x000229D3
		public ListView()
		{
			base.AddToClassList(BaseListView.ussClassName);
			this.m_TemplateMakeItem = new Func<VisualElement>(this.TemplateMakeItem);
		}

		// Token: 0x040004AB RID: 1195
		internal static readonly BindingId itemTemplateProperty = "itemTemplate";

		// Token: 0x040004AC RID: 1196
		internal static readonly BindingId makeItemProperty = "makeItem";

		// Token: 0x040004AD RID: 1197
		internal static readonly BindingId bindItemProperty = "bindItem";

		// Token: 0x040004AE RID: 1198
		internal static readonly BindingId unbindItemProperty = "unbindItem";

		// Token: 0x040004AF RID: 1199
		internal static readonly BindingId destroyItemProperty = "destroyItem";

		// Token: 0x040004B0 RID: 1200
		private Func<VisualElement> m_MakeItem;

		// Token: 0x040004B1 RID: 1201
		private Func<VisualElement> m_TemplateMakeItem;

		// Token: 0x040004B2 RID: 1202
		private VisualTreeAsset m_ItemTemplate;

		// Token: 0x040004B3 RID: 1203
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x040004B4 RID: 1204
		private Action<VisualElement, int> m_UnbindItem;

		// Token: 0x040004B5 RID: 1205
		private Action<VisualElement> m_DestroyItem;

		// Token: 0x020000F6 RID: 246
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ListView, ListView.UxmlTraits>
		{
		}

		// Token: 0x020000F7 RID: 247
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseListView.UxmlTraits
		{
			// Token: 0x0600079E RID: 1950 RVA: 0x00024860 File Offset: 0x00022A60
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ListView view = ve as ListView;
				VisualTreeAsset itemTemplate;
				bool flag = this.m_ItemTemplate.TryGetValueFromBag(bag, cc, out itemTemplate);
				if (flag)
				{
					view.itemTemplate = itemTemplate;
				}
			}

			// Token: 0x040004B6 RID: 1206
			private UxmlAssetAttributeDescription<VisualTreeAsset> m_ItemTemplate = new UxmlAssetAttributeDescription<VisualTreeAsset>
			{
				name = "item-template"
			};
		}
	}
}

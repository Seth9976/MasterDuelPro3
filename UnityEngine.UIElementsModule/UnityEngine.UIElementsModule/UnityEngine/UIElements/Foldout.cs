using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C8 RID: 200
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001D81E File Offset: 0x0001BA1E
		internal Toggle toggle
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Toggle;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001D826 File Offset: 0x0001BA26
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001D82E File Offset: 0x0001BA2E
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001D836 File Offset: 0x0001BA36
		public override bool focusable
		{
			get
			{
				return base.focusable;
			}
			set
			{
				base.focusable = value;
				this.m_Toggle.focusable = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001D84E File Offset: 0x0001BA4E
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001D85C File Offset: 0x0001BA5C
		[CreateProperty]
		public bool toggleOnLabelClick
		{
			get
			{
				return this.m_Toggle.toggleOnTextClick;
			}
			set
			{
				bool flag = this.m_Toggle.toggleOnTextClick == value;
				if (!flag)
				{
					this.m_Toggle.toggleOnTextClick = value;
					base.NotifyPropertyChanged(in Foldout.toggleOnLabelClickProperty);
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001D897 File Offset: 0x0001BA97
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		[CreateProperty]
		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				string previous = this.text;
				this.m_Toggle.text = value;
				VisualElement visualElement = this.m_Toggle.visualInput.Q(null, Toggle.textUssClassName);
				if (visualElement != null)
				{
					visualElement.AddToClassList(Foldout.textUssClassName);
				}
				bool flag = string.CompareOrdinal(previous, this.text) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in Foldout.textProperty);
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001D90C File Offset: 0x0001BB0C
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001D914 File Offset: 0x0001BB14
		[CreateProperty]
		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> evt = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						evt.elementTarget = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(evt);
						base.SaveViewData();
						base.NotifyPropertyChanged(in Foldout.valueProperty);
					}
				}
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001D988 File Offset: 0x0001BB88
		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.SetValueWithoutNotify(this.m_Value);
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
			bool value = this.m_Value;
			if (value)
			{
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.pseudoStates &= ~PseudoStates.Checked;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string key = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, key);
			this.SetValueWithoutNotify(this.m_Value);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
				this.focusController.IgnoreEvent(sourceEvent);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001DA60 File Offset: 0x0001BC60
		private bool Apply(KeyboardNavigationOperation op)
		{
			bool flag;
			switch (op)
			{
			case KeyboardNavigationOperation.SelectAll:
			case KeyboardNavigationOperation.Cancel:
			case KeyboardNavigationOperation.Submit:
			case KeyboardNavigationOperation.Previous:
			case KeyboardNavigationOperation.Next:
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.Begin:
			case KeyboardNavigationOperation.End:
				flag = false;
				break;
			case KeyboardNavigationOperation.MoveRight:
				this.SetValueWithoutNotify(true);
				flag = true;
				break;
			case KeyboardNavigationOperation.MoveLeft:
				this.SetValueWithoutNotify(false);
				flag = true;
				break;
			default:
				throw new ArgumentOutOfRangeException("op", op, null);
			}
			return flag;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001DADC File Offset: 0x0001BCDC
		public Foldout()
		{
			base.AddToClassList(Foldout.ussClassName);
			base.delegatesFocus = true;
			this.focusable = true;
			base.isEligibleToReceiveFocusFromDisabledChild = false;
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Toggle.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.SetValueWithoutNotify(true);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001DC14 File Offset: 0x0001BE14
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i.ToString());
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			this.m_Toggle.AssignInspectorStyleIfNecessary(Foldout.toggleInspectorUssClassName);
			int depth = this.GetFoldoutDepth();
			bool flag = depth > Foldout.ussFoldoutMaxDepth;
			if (flag)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + depth.ToString());
			}
		}

		// Token: 0x040003CE RID: 974
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040003CF RID: 975
		internal static readonly BindingId toggleOnLabelClickProperty = "toggleOnLabelClick";

		// Token: 0x040003D0 RID: 976
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040003D1 RID: 977
		private readonly Toggle m_Toggle = new Toggle();

		// Token: 0x040003D2 RID: 978
		private VisualElement m_Container;

		// Token: 0x040003D3 RID: 979
		[SerializeField]
		[DontCreateProperty]
		private bool m_Value;

		// Token: 0x040003D4 RID: 980
		public static readonly string ussClassName = "unity-foldout";

		// Token: 0x040003D5 RID: 981
		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		// Token: 0x040003D6 RID: 982
		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		// Token: 0x040003D7 RID: 983
		public static readonly string inputUssClassName = Foldout.ussClassName + "__input";

		// Token: 0x040003D8 RID: 984
		public static readonly string checkmarkUssClassName = Foldout.ussClassName + "__checkmark";

		// Token: 0x040003D9 RID: 985
		public static readonly string textUssClassName = Foldout.ussClassName + "__text";

		// Token: 0x040003DA RID: 986
		internal static readonly string toggleInspectorUssClassName = Foldout.toggleUssClassName + "--inspector";

		// Token: 0x040003DB RID: 987
		internal static readonly string ussFoldoutDepthClassName = Foldout.ussClassName + "--depth-";

		// Token: 0x040003DC RID: 988
		internal static readonly int ussFoldoutMaxDepth = 4;

		// Token: 0x040003DD RID: 989
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x020000C9 RID: 201
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
		}

		// Token: 0x020000CA RID: 202
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x0600064F RID: 1615 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout f = ve as Foldout;
				bool flag = f != null;
				if (flag)
				{
					f.text = this.m_Text.GetValueFromBag(bag, cc);
					f.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}

			// Token: 0x040003DE RID: 990
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040003DF RID: 991
			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};
		}
	}
}

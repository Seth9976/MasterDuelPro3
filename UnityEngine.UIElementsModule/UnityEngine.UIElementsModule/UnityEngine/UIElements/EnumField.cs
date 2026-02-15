using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C0 RID: 192
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class EnumField : BaseField<Enum>
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001CF53 File Offset: 0x0001B153
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool includeObsoleteValues
		{
			get
			{
				return this.m_IncludeObsoleteValues;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001CF5C File Offset: 0x0001B15C
		[CreateProperty(ReadOnly = true)]
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001CF7C File Offset: 0x0001B17C
		private void Initialize(Enum defaultValue)
		{
			this.m_TextElement = new TextElement();
			this.m_TextElement.AddToClassList(EnumField.textUssClassName);
			this.m_TextElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(EnumField.arrowUssClassName);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			bool flag = defaultValue != null;
			if (flag)
			{
				this.Init(defaultValue);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001D012 File Offset: 0x0001B212
		public EnumField()
			: this(null, null)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001D020 File Offset: 0x0001B220
		public EnumField(string label, Enum defaultValue = null)
			: base(label, null)
		{
			base.AddToClassList(EnumField.ussClassName);
			base.labelElement.AddToClassList(EnumField.labelUssClassName);
			base.visualInput.AddToClassList(EnumField.inputUssClassName);
			this.Initialize(defaultValue);
			base.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseDownEvent>(delegate(MouseDownEvent e)
			{
				bool flag = e.button == 0;
				if (flag)
				{
					e.StopPropagation();
				}
			}, TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		public void Init(Enum defaultValue)
		{
			this.Init(defaultValue, false);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		public void Init(Enum defaultValue, bool includeObsoleteValues)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this.m_IncludeObsoleteValues = includeObsoleteValues;
			this.PopulateDataFromType(defaultValue.GetType());
			bool flag2 = !object.Equals(base.rawValue, defaultValue);
			if (flag2)
			{
				this.SetValueWithoutNotify(defaultValue);
			}
			else
			{
				this.UpdateValueLabel(defaultValue);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001D137 File Offset: 0x0001B337
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void PopulateDataFromType(Type enumType)
		{
			this.m_EnumType = enumType;
			this.m_EnumData = EnumDataUtility.GetCachedEnumData(this.m_EnumType, this.includeObsoleteValues ? EnumDataUtility.CachedType.IncludeObsoleteExceptErrors : EnumDataUtility.CachedType.ExcludeObsolete, new Func<string, string>(NameFormatter.FormatVariableName));
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001D16C File Offset: 0x0001B36C
		public override void SetValueWithoutNotify(Enum newValue)
		{
			bool flag = !object.Equals(base.rawValue, newValue);
			if (flag)
			{
				base.SetValueWithoutNotify(newValue);
				bool flag2 = this.m_EnumType == null;
				if (!flag2)
				{
					this.UpdateValueLabel(newValue);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		private void UpdateValueLabel(Enum value)
		{
			int idx = Array.IndexOf<Enum>(this.m_EnumData.values, value);
			bool flag = (idx >= 0) & (idx < this.m_EnumData.values.Length);
			if (flag)
			{
				this.m_TextElement.text = this.m_EnumData.displayNames[idx];
			}
			else
			{
				this.m_TextElement.text = string.Empty;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001D220 File Offset: 0x0001B420
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			this.ProcessPointerDown<PointerDownEvent>(evt);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001D22C File Offset: 0x0001B42C
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) != 0;
				if (flag2)
				{
					this.ProcessPointerDown<PointerMoveEvent>(evt);
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001D264 File Offset: 0x0001B464
		private bool ContainsPointer(int pointerId)
		{
			VisualElement elementUnderPointer = base.elementPanel.GetTopElementUnderPointer(pointerId);
			return this == elementUnderPointer || base.visualInput == elementUnderPointer;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001D294 File Offset: 0x0001B494
		private void ProcessPointerDown<T>(PointerEventBase<T> evt) where T : PointerEventBase<T>, new()
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = this.ContainsPointer(evt.pointerId);
				if (flag2)
				{
					base.schedule.Execute(new Action(this.ShowMenu));
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001D2E2 File Offset: 0x0001B4E2
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ShowMenu();
			evt.StopPropagation();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		private void ShowMenu()
		{
			bool flag = this.m_EnumType == null;
			if (!flag)
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				bool isPlayer = elementPanel != null && elementPanel.contextType == ContextType.Player;
				bool flag2 = this.createMenuCallback != null;
				IGenericMenu menu;
				if (flag2)
				{
					menu = this.createMenuCallback();
				}
				else
				{
					IGenericMenu genericMenu;
					if (!isPlayer)
					{
						genericMenu = DropdownUtility.CreateDropdown();
					}
					else
					{
						IGenericMenu genericMenu2 = new GenericDropdownMenu();
						genericMenu = genericMenu2;
					}
					menu = genericMenu;
				}
				int selectedIndex = Array.IndexOf<Enum>(this.m_EnumData.values, this.value);
				for (int i = 0; i < this.m_EnumData.values.Length; i++)
				{
					bool isSelected = selectedIndex == i;
					menu.AddItem(this.m_EnumData.displayNames[i], isSelected, delegate(object contentView)
					{
						this.ChangeValueFromMenu(contentView);
					}, this.m_EnumData.values[i]);
				}
				menu.DropDown(base.visualInput.worldBound, this, true);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001D3E9 File Offset: 0x0001B5E9
		private void ChangeValueFromMenu(object menuItem)
		{
			this.value = menuItem as Enum;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_TextElement.text = BaseField<Enum>.mixedValueString;
			}
			else
			{
				this.UpdateValueLabel(this.value);
			}
			this.m_TextElement.EnableInClassList(EnumField.labelUssClassName, base.showMixedValue);
			this.m_TextElement.EnableInClassList(BaseField<Enum>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x040003BA RID: 954
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040003BB RID: 955
		private Type m_EnumType;

		// Token: 0x040003BC RID: 956
		private bool m_IncludeObsoleteValues;

		// Token: 0x040003BD RID: 957
		private TextElement m_TextElement;

		// Token: 0x040003BE RID: 958
		private VisualElement m_ArrowElement;

		// Token: 0x040003BF RID: 959
		private EnumData m_EnumData;

		// Token: 0x040003C0 RID: 960
		internal Func<IGenericMenu> createMenuCallback;

		// Token: 0x040003C1 RID: 961
		public new static readonly string ussClassName = "unity-enum-field";

		// Token: 0x040003C2 RID: 962
		public static readonly string textUssClassName = EnumField.ussClassName + "__text";

		// Token: 0x040003C3 RID: 963
		public static readonly string arrowUssClassName = EnumField.ussClassName + "__arrow";

		// Token: 0x040003C4 RID: 964
		public new static readonly string labelUssClassName = EnumField.ussClassName + "__label";

		// Token: 0x040003C5 RID: 965
		public new static readonly string inputUssClassName = EnumField.ussClassName + "__input";

		// Token: 0x020000C1 RID: 193
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<EnumField, EnumField.UxmlTraits>
		{
		}

		// Token: 0x020000C2 RID: 194
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Enum>.UxmlTraits
		{
			// Token: 0x06000627 RID: 1575 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Type resEnumType;
				Enum resEnumValue;
				bool resIncludeObsoleteValues;
				bool flag = EnumFieldHelpers.ExtractValue(bag, cc, out resEnumType, out resEnumValue, out resIncludeObsoleteValues);
				if (flag)
				{
					EnumField enumField = (EnumField)ve;
					enumField.Init(resEnumValue, resIncludeObsoleteValues);
				}
				else
				{
					bool flag2 = null != resEnumType;
					if (flag2)
					{
						EnumField enumField2 = (EnumField)ve;
						enumField2.m_EnumType = resEnumType;
						bool flag3 = enumField2.m_EnumType != null;
						if (flag3)
						{
							enumField2.PopulateDataFromType(enumField2.m_EnumType);
						}
						enumField2.value = null;
					}
					else
					{
						EnumField enumField3 = (EnumField)ve;
						enumField3.m_EnumType = null;
						enumField3.value = null;
					}
				}
			}

			// Token: 0x040003C6 RID: 966
			private UxmlTypeAttributeDescription<Enum> m_Type = EnumFieldHelpers.type;

			// Token: 0x040003C7 RID: 967
			private UxmlStringAttributeDescription m_Value = EnumFieldHelpers.value;

			// Token: 0x040003C8 RID: 968
			private UxmlBoolAttributeDescription m_IncludeObsoleteValues = EnumFieldHelpers.includeObsoleteValues;
		}
	}
}

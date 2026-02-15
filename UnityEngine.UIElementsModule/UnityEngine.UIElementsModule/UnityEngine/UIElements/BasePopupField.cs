using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000076 RID: 118
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BasePopupField<TValueType, TValueChoice> : BaseField<TValueType>
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015CA0 File Offset: 0x00013EA0
		protected TextElement textElement
		{
			get
			{
				return this.m_TextElement;
			}
		}

		// Token: 0x06000452 RID: 1106
		internal abstract string GetValueToDisplay();

		// Token: 0x06000453 RID: 1107
		internal abstract string GetListItemToDisplay(TValueType item);

		// Token: 0x06000454 RID: 1108
		internal abstract void AddMenuItems(IGenericMenu menu);

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015CB8 File Offset: 0x00013EB8
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015CD0 File Offset: 0x00013ED0
		[CreateProperty]
		public virtual List<TValueChoice> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Choices = value;
				this.SetValueWithoutNotify(base.rawValue);
				base.NotifyPropertyChanged(in BasePopupField<TValueType, TValueChoice>.choicesProperty);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00015D11 File Offset: 0x00013F11
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			base.SetValueWithoutNotify(newValue);
			((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(this.GetValueToDisplay());
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00015D30 File Offset: 0x00013F30
		[CreateProperty(ReadOnly = true)]
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00015D50 File Offset: 0x00013F50
		internal BasePopupField(string label)
			: base(label, null)
		{
			base.AddToClassList(BasePopupField<TValueType, TValueChoice>.ussClassName);
			base.labelElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.labelUssClassName);
			this.m_TextElement = new BasePopupField<TValueType, TValueChoice>.PopupTextElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_TextElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.textUssClassName);
			base.visualInput.AddToClassList(BasePopupField<TValueType, TValueChoice>.inputUssClassName);
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.arrowUssClassName);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			this.choices = new List<TValueChoice>();
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

		// Token: 0x0600045A RID: 1114 RVA: 0x00015E7C File Offset: 0x0001407C
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			this.ProcessPointerDown<PointerDownEvent>(evt);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00015E88 File Offset: 0x00014088
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

		// Token: 0x0600045C RID: 1116 RVA: 0x00015EC0 File Offset: 0x000140C0
		private bool ContainsPointer(int pointerId)
		{
			VisualElement elementUnderPointer = base.elementPanel.GetTopElementUnderPointer(pointerId);
			return this == elementUnderPointer || base.visualInput == elementUnderPointer;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015EF0 File Offset: 0x000140F0
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

		// Token: 0x0600045E RID: 1118 RVA: 0x00015F3E File Offset: 0x0001413E
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ShowMenu();
			evt.StopPropagation();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015F50 File Offset: 0x00014150
		internal void ShowMenu()
		{
			bool flag = this.createMenuCallback != null;
			IGenericMenu menu;
			if (flag)
			{
				menu = this.createMenuCallback();
			}
			else
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				IGenericMenu genericMenu;
				if (elementPanel == null || elementPanel.contextType != ContextType.Player)
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
			this.AddMenuItems(menu);
			menu.DropDown(base.visualInput.worldBound, this, true);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015FC0 File Offset: 0x000141C0
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(BaseField<TValueType>.mixedValueString);
			}
			this.textElement.EnableInClassList(BaseField<TValueType>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x04000275 RID: 629
		internal static readonly BindingId choicesProperty = "choices";

		// Token: 0x04000276 RID: 630
		internal static readonly BindingId textProperty = "text";

		// Token: 0x04000277 RID: 631
		internal List<TValueChoice> m_Choices;

		// Token: 0x04000278 RID: 632
		private TextElement m_TextElement;

		// Token: 0x04000279 RID: 633
		private VisualElement m_ArrowElement;

		// Token: 0x0400027A RID: 634
		internal Func<TValueChoice, string> m_FormatSelectedValueCallback;

		// Token: 0x0400027B RID: 635
		internal Func<TValueChoice, string> m_FormatListItemCallback;

		// Token: 0x0400027C RID: 636
		internal Func<IGenericMenu> createMenuCallback;

		// Token: 0x0400027D RID: 637
		internal bool m_AutoCloseMenu = true;

		// Token: 0x0400027E RID: 638
		public new static readonly string ussClassName = "unity-base-popup-field";

		// Token: 0x0400027F RID: 639
		public static readonly string textUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__text";

		// Token: 0x04000280 RID: 640
		public static readonly string arrowUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__arrow";

		// Token: 0x04000281 RID: 641
		public new static readonly string labelUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__label";

		// Token: 0x04000282 RID: 642
		public new static readonly string inputUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__input";

		// Token: 0x02000077 RID: 119
		private class PopupTextElement : TextElement
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x0001608C File Offset: 0x0001428C
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string textToMeasure = this.text;
				bool flag = string.IsNullOrEmpty(textToMeasure);
				if (flag)
				{
					textToMeasure = " ";
				}
				return base.MeasureTextSize(textToMeasure, desiredWidth, widthMode, desiredHeight, heightMode);
			}
		}
	}
}

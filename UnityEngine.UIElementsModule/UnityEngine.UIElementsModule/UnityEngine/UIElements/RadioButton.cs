using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012B RID: 299
	public class RadioButton : BaseBoolField, IGroupBoxOption
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002B88A File Offset: 0x00029A8A
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0002B894 File Offset: 0x00029A94
		public override bool value
		{
			get
			{
				return base.value;
			}
			set
			{
				bool flag = base.value != value;
				if (flag)
				{
					base.value = value;
					this.UpdateCheckmark();
					if (value)
					{
						this.OnOptionSelected<RadioButton>();
					}
				}
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002B8D1 File Offset: 0x00029AD1
		public RadioButton()
			: this(null)
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002B8DC File Offset: 0x00029ADC
		public RadioButton(string label)
			: base(label)
		{
			base.AddToClassList(RadioButton.ussClassName);
			base.visualInput.AddToClassList(RadioButton.inputUssClassName);
			base.labelElement.AddToClassList(RadioButton.labelUssClassName);
			this.m_CheckMark.RemoveFromHierarchy();
			this.m_CheckmarkBackground = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_CheckmarkBackground.Add(this.m_CheckMark);
			this.m_CheckmarkBackground.AddToClassList(RadioButton.checkmarkBackgroundUssClassName);
			this.m_CheckMark.AddToClassList(RadioButton.checkmarkUssClassName);
			base.visualInput.Add(this.m_CheckmarkBackground);
			this.UpdateCheckmark();
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnOptionAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnOptionDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		private void OnOptionAttachToPanel(AttachToPanelEvent evt)
		{
			this.RegisterGroupBoxOption<RadioButton>();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002B9BE File Offset: 0x00029BBE
		private void OnOptionDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.UnregisterGroupBoxOption<RadioButton>();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(RadioButton.textUssClassName);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		protected override void ToggleValue()
		{
			bool flag = !this.value;
			if (flag)
			{
				this.value = true;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002BA09 File Offset: 0x00029C09
		void IGroupBoxOption.SetSelected(bool selected)
		{
			this.value = selected;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002BA14 File Offset: 0x00029C14
		public override void SetValueWithoutNotify(bool newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateCheckmark();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002BA26 File Offset: 0x00029C26
		private void UpdateCheckmark()
		{
			this.m_CheckMark.style.display = (this.value ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002BA4C File Offset: 0x00029C4C
		protected override void UpdateMixedValueContent()
		{
			base.UpdateMixedValueContent();
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_CheckmarkBackground.RemoveFromHierarchy();
			}
			else
			{
				this.m_CheckmarkBackground.Add(this.m_CheckMark);
				base.visualInput.Add(this.m_CheckmarkBackground);
			}
		}

		// Token: 0x040005BB RID: 1467
		public new static readonly string ussClassName = "unity-radio-button";

		// Token: 0x040005BC RID: 1468
		public new static readonly string labelUssClassName = RadioButton.ussClassName + "__label";

		// Token: 0x040005BD RID: 1469
		public new static readonly string inputUssClassName = RadioButton.ussClassName + "__input";

		// Token: 0x040005BE RID: 1470
		public static readonly string checkmarkBackgroundUssClassName = RadioButton.ussClassName + "__checkmark-background";

		// Token: 0x040005BF RID: 1471
		public static readonly string checkmarkUssClassName = RadioButton.ussClassName + "__checkmark";

		// Token: 0x040005C0 RID: 1472
		public static readonly string textUssClassName = RadioButton.ussClassName + "__text";

		// Token: 0x040005C1 RID: 1473
		private VisualElement m_CheckmarkBackground;

		// Token: 0x0200012C RID: 300
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RadioButton, RadioButton.UxmlTraits>
		{
		}

		// Token: 0x0200012D RID: 301
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x0600092E RID: 2350 RVA: 0x0002BB28 File Offset: 0x00029D28
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((RadioButton)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x040005C2 RID: 1474
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}

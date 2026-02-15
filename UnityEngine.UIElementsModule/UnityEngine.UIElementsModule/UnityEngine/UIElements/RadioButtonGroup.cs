using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012E RID: 302
	public class RadioButtonGroup : BaseField<int>, IGroupBox
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002BB70 File Offset: 0x00029D70
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0002BB90 File Offset: 0x00029D90
		[CreateProperty]
		public IEnumerable<string> choices
		{
			get
			{
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					yield return radioButton.text;
					radioButton = null;
				}
				List<RadioButton>.Enumerator enumerator = default(List<RadioButton>.Enumerator);
				yield break;
				yield break;
			}
			set
			{
				bool flag = !value.HasValues();
				if (flag)
				{
					this.m_RadioButtonContainer.Clear();
					bool flag2 = base.panel != null;
					if (!flag2)
					{
						foreach (RadioButton radioButton in this.m_RadioButtons)
						{
							radioButton.UnregisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
						}
						this.m_RadioButtons.Clear();
					}
				}
				else
				{
					int i = 0;
					foreach (string choice in value)
					{
						bool flag3 = i < this.m_RadioButtons.Count;
						if (flag3)
						{
							this.m_RadioButtons[i].text = choice;
							this.m_RadioButtonContainer.Insert(i, this.m_RadioButtons[i]);
						}
						else
						{
							RadioButton radioButton2 = new RadioButton
							{
								text = choice
							};
							radioButton2.RegisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
							this.m_RadioButtons.Add(radioButton2);
							this.m_RadioButtonContainer.Add(radioButton2);
						}
						i++;
					}
					int lastIndex = this.m_RadioButtons.Count - 1;
					for (int j = lastIndex; j >= i; j--)
					{
						this.m_RadioButtons[j].RemoveFromHierarchy();
					}
					this.UpdateRadioButtons();
					base.NotifyPropertyChanged(in RadioButtonGroup.choicesProperty);
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002BD48 File Offset: 0x00029F48
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_RadioButtonContainer ?? this;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002BD55 File Offset: 0x00029F55
		public RadioButtonGroup()
			: this(null, null)
		{
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002BD64 File Offset: 0x00029F64
		public RadioButtonGroup(string label, List<string> radioButtonChoices = null)
			: base(label, null)
		{
			base.AddToClassList(RadioButtonGroup.ussClassName);
			VisualElement visualInput = base.visualInput;
			VisualElement visualElement = new VisualElement();
			visualElement.name = RadioButtonGroup.containerUssClassName;
			VisualElement visualElement2 = visualElement;
			this.m_RadioButtonContainer = visualElement;
			visualInput.Add(visualElement2);
			this.m_RadioButtonContainer.AddToClassList(RadioButtonGroup.containerUssClassName);
			this.m_RadioButtonValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.RadioButtonValueChangedCallback);
			this.choices = radioButtonChoices;
			this.value = -1;
			base.visualInput.focusable = false;
			base.delegatesFocus = true;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002BE00 File Offset: 0x0002A000
		private void RadioButtonValueChangedCallback(ChangeEvent<bool> evt)
		{
			bool newValue = evt.newValue;
			if (newValue)
			{
				this.value = this.m_RadioButtons.IndexOf(evt.target as RadioButton);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002BE3E File Offset: 0x0002A03E
		public override void SetValueWithoutNotify(int newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateRadioButtons();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002BE50 File Offset: 0x0002A050
		private void UpdateRadioButtons()
		{
			bool flag = this.value >= 0 && this.value < this.m_RadioButtons.Count;
			if (flag)
			{
				this.m_RadioButtons[this.value].value = true;
			}
			else
			{
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					radioButton.value = false;
				}
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
			RadioButton radioButton = option as RadioButton;
			bool flag = radioButton == null;
			if (flag)
			{
				throw new ArgumentException("[UI Toolkit] Internal group box error. Expected a radio button element. Please report this using Help -> Report a bug...");
			}
			bool flag2 = this.m_RadioButtons.Contains(radioButton);
			if (!flag2)
			{
				radioButton.RegisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
				int indexInContainer = this.m_RadioButtonContainer.IndexOf(radioButton);
				bool flag3 = indexInContainer < 0 || indexInContainer > this.m_RadioButtons.Count;
				if (flag3)
				{
					this.m_RadioButtons.Add(radioButton);
					this.m_RadioButtonContainer.Add(radioButton);
				}
				else
				{
					this.m_RadioButtons.Insert(indexInContainer, radioButton);
				}
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002BF8C File Offset: 0x0002A18C
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
			RadioButton radioButton = option as RadioButton;
			bool flag = radioButton == null;
			if (flag)
			{
				throw new ArgumentException("[UI Toolkit] Internal group box error. Expected a radio button element. Please report this using Help -> Report a bug...");
			}
			int index = this.m_RadioButtons.IndexOf(radioButton);
			radioButton.UnregisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
			this.m_RadioButtons.Remove(radioButton);
			bool flag2 = this.value == index;
			if (flag2)
			{
				this.value = -1;
			}
		}

		// Token: 0x040005C3 RID: 1475
		internal static readonly BindingId choicesProperty = "choices";

		// Token: 0x040005C4 RID: 1476
		public new static readonly string ussClassName = "unity-radio-button-group";

		// Token: 0x040005C5 RID: 1477
		public static readonly string containerUssClassName = RadioButtonGroup.ussClassName + "__container";

		// Token: 0x040005C6 RID: 1478
		private List<RadioButton> m_RadioButtons = new List<RadioButton>();

		// Token: 0x040005C7 RID: 1479
		private EventCallback<ChangeEvent<bool>> m_RadioButtonValueChangedCallback;

		// Token: 0x040005C8 RID: 1480
		private VisualElement m_RadioButtonContainer;

		// Token: 0x0200012F RID: 303
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RadioButtonGroup, RadioButtonGroup.UxmlTraits>
		{
		}

		// Token: 0x02000130 RID: 304
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x0600093C RID: 2364 RVA: 0x0002C02C File Offset: 0x0002A22C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RadioButtonGroup f = (RadioButtonGroup)ve;
				f.choices = UxmlUtility.ParseStringListAttribute(this.m_Choices.GetValueFromBag(bag, cc));
			}

			// Token: 0x040005C9 RID: 1481
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}

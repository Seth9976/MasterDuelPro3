using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000156 RID: 342
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class TextValueField<TValueType> : TextInputBaseField<TValueType>, IValueField<TValueType>
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00032183 File Offset: 0x00030383
		private TextValueField<TValueType>.TextValueInput textValueInput
		{
			get
			{
				return (TextValueField<TValueType>.TextValueInput)base.textInputBase;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00032190 File Offset: 0x00030390
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x000321A0 File Offset: 0x000303A0
		[CreateProperty]
		public string formatString
		{
			get
			{
				return this.textValueInput.formatString;
			}
			set
			{
				bool flag = this.textValueInput.formatString != value;
				if (flag)
				{
					this.textValueInput.formatString = value;
					base.textEdition.UpdateText(this.ValueToString(base.rawValue));
					base.NotifyPropertyChanged(in TextValueField<TValueType>.formatStringProperty);
				}
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000321F8 File Offset: 0x000303F8
		protected TextValueField(string label, int maxLength, TextValueField<TValueType>.TextValueInput textValueInput)
			: base(label, maxLength, '\0', textValueInput)
		{
			this.m_UpdateTextFromValue = true;
			base.textEdition.UpdateText(this.ValueToString(base.rawValue));
			base.onIsReadOnlyChanged = (Action<bool>)Delegate.Combine(base.onIsReadOnlyChanged, new Action<bool>(this.OnIsReadOnlyChanged));
		}

		// Token: 0x06000A41 RID: 2625
		public abstract void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue);

		// Token: 0x06000A42 RID: 2626 RVA: 0x00032254 File Offset: 0x00030454
		public void StartDragging()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.value = default(TValueType);
			}
			this.textValueInput.StartDragging();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0003228A File Offset: 0x0003048A
		public void StopDragging()
		{
			this.textValueInput.StopDragging();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0003229C File Offset: 0x0003049C
		internal override void UpdateValueFromText()
		{
			base.UpdatePlaceholderClassList(null);
			this.m_UpdateTextFromValue = false;
			try
			{
				this.value = this.StringToValue(base.text);
			}
			finally
			{
				this.m_UpdateTextFromValue = true;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000322EC File Offset: 0x000304EC
		internal override void UpdateTextFromValue()
		{
			base.text = this.ValueToString(base.rawValue);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00032302 File Offset: 0x00030502
		private void OnIsReadOnlyChanged(bool newValue)
		{
			this.EnableLabelDragger(!newValue);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		internal virtual bool CanTryParse(string textString)
		{
			return false;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00032310 File Offset: 0x00030510
		protected void AddLabelDragger<TDraggerType>()
		{
			this.m_Dragger = new FieldMouseDragger<TDraggerType>((IValueField<TDraggerType>)this);
			this.EnableLabelDragger(!base.isReadOnly);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00032334 File Offset: 0x00030534
		private void EnableLabelDragger(bool enable)
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.SetDragZone(enable ? base.labelElement : null);
				base.labelElement.EnableInClassList(BaseField<TValueType>.labelDraggerVariantUssClassName, enable);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0003237C File Offset: 0x0003057C
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			bool displayNeedsUpdate = this.m_ForceUpdateDisplay || (this.m_UpdateTextFromValue && !EqualityComparer<TValueType>.Default.Equals(base.rawValue, newValue));
			base.SetValueWithoutNotify(newValue);
			bool flag = displayNeedsUpdate;
			if (flag)
			{
				base.textEdition.UpdateText(this.ValueToString(base.rawValue));
			}
			this.m_ForceUpdateDisplay = false;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000323E4 File Offset: 0x000305E4
		[EventInterest(new Type[]
		{
			typeof(BlurEvent),
			typeof(FocusEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool showPlaceholderText = string.IsNullOrEmpty(base.text) && !string.IsNullOrEmpty(base.textEdition.placeholder);
			bool flag = showPlaceholderText;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					bool showMixedValue = base.showMixedValue;
					if (showMixedValue)
					{
						this.UpdateMixedValueContent();
					}
					else
					{
						bool flag3 = string.IsNullOrEmpty(base.text);
						if (flag3)
						{
							base.textInputBase.UpdateTextFromValue();
						}
						else
						{
							base.textInputBase.UpdateValueFromText();
							base.textInputBase.UpdateTextFromValue();
						}
					}
				}
				else
				{
					bool flag4 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag4)
					{
						bool showMixedValue2 = base.showMixedValue;
						if (showMixedValue2)
						{
							base.textInputBase.text = "";
						}
					}
				}
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000324C2 File Offset: 0x000306C2
		internal override void OnViewDataReady()
		{
			this.m_ForceUpdateDisplay = true;
			base.OnViewDataReady();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000324D3 File Offset: 0x000306D3
		internal override void RegisterEditingCallbacks()
		{
			base.RegisterEditingCallbacks();
			base.labelElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.labelElement.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0003250F File Offset: 0x0003070F
		internal override void UnregisterEditingCallbacks()
		{
			base.UnregisterEditingCallbacks();
			base.labelElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.labelElement.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x040006B5 RID: 1717
		internal static readonly BindingId formatStringProperty = "formatString";

		// Token: 0x040006B6 RID: 1718
		private BaseFieldMouseDragger m_Dragger;

		// Token: 0x040006B7 RID: 1719
		internal bool m_UpdateTextFromValue;

		// Token: 0x040006B8 RID: 1720
		private bool m_ForceUpdateDisplay;

		// Token: 0x02000157 RID: 343
		protected abstract class TextValueInput : TextInputBaseField<TValueType>.TextInputBase
		{
			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0003255C File Offset: 0x0003075C
			private TextValueField<TValueType> textValueFieldParent
			{
				get
				{
					return (TextValueField<TValueType>)base.parent;
				}
			}

			// Token: 0x06000A51 RID: 2641 RVA: 0x00032569 File Offset: 0x00030769
			protected TextValueInput()
			{
				base.textEdition.AcceptCharacter = new Func<char, bool>(this.AcceptCharacter);
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x0003258C File Offset: 0x0003078C
			internal override bool AcceptCharacter(char c)
			{
				return base.AcceptCharacter(c) && c != '\0' && this.allowedCharacters.IndexOf(c) != -1;
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000A53 RID: 2643
			protected abstract string allowedCharacters { get; }

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000325BF File Offset: 0x000307BF
			// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000325C7 File Offset: 0x000307C7
			public string formatString { get; set; }

			// Token: 0x06000A56 RID: 2646
			public abstract void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue);

			// Token: 0x06000A57 RID: 2647 RVA: 0x000325D0 File Offset: 0x000307D0
			public void StartDragging()
			{
				base.isDragging = true;
				base.textSelection.SelectNone();
				base.MarkDirtyRepaint();
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x000325F0 File Offset: 0x000307F0
			public void StopDragging()
			{
				bool isDelayed = this.textValueFieldParent.isDelayed;
				if (isDelayed)
				{
					base.UpdateValueFromText();
				}
				base.isDragging = false;
				base.textSelection.SelectAll();
				base.MarkDirtyRepaint();
			}

			// Token: 0x06000A59 RID: 2649
			protected abstract string ValueToString(TValueType value);

			// Token: 0x06000A5A RID: 2650 RVA: 0x00032634 File Offset: 0x00030834
			protected override TValueType StringToValue(string str)
			{
				return base.StringToValue(str);
			}
		}
	}
}

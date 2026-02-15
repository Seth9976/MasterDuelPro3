using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006E RID: 110
	public abstract class BaseBoolField : BaseField<bool>
	{
		// Token: 0x1700008A RID: 138
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00013834 File Offset: 0x00011A34
		internal bool acceptClicksIfDisabled
		{
			set
			{
				this.m_Clickable.acceptClicksIfDisabled = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00013843 File Offset: 0x00011A43
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001384B File Offset: 0x00011A4B
		[CreateProperty]
		public bool toggleOnLabelClick { get; set; } = true;

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013854 File Offset: 0x00011A54
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001385C File Offset: 0x00011A5C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool toggleOnTextClick { get; set; } = true;

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013868 File Offset: 0x00011A68
		public BaseBoolField(string label)
			: base(label, null)
		{
			this.m_CheckMark = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_CheckMark);
			base.visualInput.pickingMode = PickingMode.Position;
			base.labelElement.focusable = false;
			this.text = null;
			this.AddManipulator(this.m_Clickable = new Clickable(new Action<EventBase>(this.OnClickEvent)));
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013915 File Offset: 0x00011B15
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ToggleValue();
			evt.StopPropagation();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00013928 File Offset: 0x00011B28
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001394C File Offset: 0x00011B4C
		[CreateProperty]
		public string text
		{
			get
			{
				Label label = this.m_Label;
				return (label != null) ? label.text : null;
			}
			set
			{
				Label label = this.m_Label;
				bool flag = string.CompareOrdinal((label != null) ? label.text : null, value) == 0;
				if (!flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.InitLabel();
						this.m_Label.text = value;
					}
					else
					{
						bool flag3 = this.m_Label != null;
						if (flag3)
						{
							this.m_Label.RemoveFromHierarchy();
							this.m_Label.text = value;
						}
					}
					base.NotifyPropertyChanged(in BaseBoolField.textProperty);
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000139D4 File Offset: 0x00011BD4
		protected virtual void InitLabel()
		{
			bool flag = this.m_Label == null;
			if (flag)
			{
				this.m_Label = new Label();
			}
			else
			{
				bool flag2 = this.m_Label.parent != null;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.m_CheckMark.hierarchy.parent != base.visualInput;
			if (flag3)
			{
				base.visualInput.Add(this.m_Label);
			}
			else
			{
				int checkmarkIndex = base.visualInput.IndexOf(this.m_CheckMark);
				base.visualInput.Insert(checkmarkIndex + 1, this.m_Label);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013A74 File Offset: 0x00011C74
		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013AE0 File Offset: 0x00011CE0
		private void OnClickEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
			if (flag)
			{
				IMouseEvent ce = (IMouseEvent)evt;
				bool flag2 = this.ShouldIgnoreClick(ce.mousePosition);
				if (!flag2)
				{
					bool flag3 = ce.button == 0;
					if (flag3)
					{
						this.ToggleValue();
					}
				}
			}
			else
			{
				bool flag4 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() || evt.eventTypeId == EventBase<ClickEvent>.TypeId();
				if (flag4)
				{
					IPointerEvent ce2 = (IPointerEvent)evt;
					bool flag5 = this.ShouldIgnoreClick(ce2.position);
					if (!flag5)
					{
						bool flag6 = ce2.button == 0;
						if (flag6)
						{
							this.ToggleValue();
						}
					}
				}
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013B94 File Offset: 0x00011D94
		private bool ShouldIgnoreClick(Vector3 position)
		{
			bool flag = !this.toggleOnLabelClick && base.labelElement.worldBound.Contains(position);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3;
				if (!this.toggleOnTextClick)
				{
					Label label = this.m_Label;
					flag3 = label != null && label.worldBound.Contains(position);
				}
				else
				{
					flag3 = false;
				}
				bool flag4 = flag3;
				flag2 = flag4;
			}
			return flag2;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013BFC File Offset: 0x00011DFC
		protected virtual void ToggleValue()
		{
			this.value = !this.value;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013C10 File Offset: 0x00011E10
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.RemoveFromHierarchy();
				base.visualInput.Add(base.mixedValueLabel);
				this.m_OriginalText = this.text;
				this.text = "";
			}
			else
			{
				base.mixedValueLabel.RemoveFromHierarchy();
				base.visualInput.Add(this.m_CheckMark);
				bool flag = this.m_OriginalText != null;
				if (flag)
				{
					this.text = this.m_OriginalText;
				}
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013CC1 File Offset: 0x00011EC1
		internal override void RegisterEditingCallbacks()
		{
			base.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.StartEditing), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013CEC File Offset: 0x00011EEC
		internal override void UnregisterEditingCallbacks()
		{
			base.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.StartEditing), TrickleDown.NoTrickleDown);
			base.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x04000209 RID: 521
		internal static readonly BindingId textProperty = "text";

		// Token: 0x0400020A RID: 522
		internal static readonly BindingId toggleOnLabelClickProperty = "toggleOnLabelClick";

		// Token: 0x0400020B RID: 523
		protected Label m_Label;

		// Token: 0x0400020C RID: 524
		protected internal readonly VisualElement m_CheckMark;

		// Token: 0x0400020D RID: 525
		internal readonly Clickable m_Clickable;

		// Token: 0x04000210 RID: 528
		private string m_OriginalText;
	}
}

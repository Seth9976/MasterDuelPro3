using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000447 RID: 1095
	internal class TextEditingManipulator
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x00073D9E File Offset: 0x00071F9E
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00073DA8 File Offset: 0x00071FA8
		internal TextEditorEventHandler editingEventHandler
		{
			get
			{
				return this.m_EditingEventHandler;
			}
			set
			{
				bool flag = this.m_EditingEventHandler == value;
				if (!flag)
				{
					TextEditorEventHandler editingEventHandler = this.m_EditingEventHandler;
					if (editingEventHandler != null)
					{
						editingEventHandler.UnregisterCallbacksFromTarget(this.m_TextElement);
					}
					this.m_EditingEventHandler = value;
					TextEditorEventHandler editingEventHandler2 = this.m_EditingEventHandler;
					if (editingEventHandler2 != null)
					{
						editingEventHandler2.RegisterCallbacksOnTarget(this.m_TextElement);
					}
				}
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x00073DFC File Offset: 0x00071FFC
		private bool touchScreenTextFieldChanged
		{
			get
			{
				bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
				TextEditingUtilities textEditingUtilities = this.editingUtilities;
				bool? flag = ((textEditingUtilities != null) ? new bool?(textEditingUtilities.TouchScreenKeyboardShouldBeUsed()) : null);
				return !((touchScreenTextFieldInitialized == flag.GetValueOrDefault()) & (flag != null));
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00073E44 File Offset: 0x00072044
		public TextEditingManipulator(TextElement textElement)
		{
			this.m_TextElement = textElement;
			this.editingUtilities = new TextEditingUtilities(textElement.selectingManipulator.m_SelectingUtilities, textElement.uitkTextHandle, textElement.text);
			this.InitTextEditorEventHandler();
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00073E90 File Offset: 0x00072090
		public void Reset()
		{
			this.editingEventHandler = null;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00073E9C File Offset: 0x0007209C
		private void InitTextEditorEventHandler()
		{
			TextEditingUtilities textEditingUtilities = this.editingUtilities;
			this.m_TouchScreenTextFieldInitialized = textEditingUtilities != null && textEditingUtilities.TouchScreenKeyboardShouldBeUsed();
			bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
			if (touchScreenTextFieldInitialized)
			{
				this.editingEventHandler = new TouchScreenTextEditorEventHandler(this.m_TextElement, this.editingUtilities);
			}
			else
			{
				this.editingEventHandler = new KeyboardTextEditorEventHandler(this.m_TextElement, this.editingUtilities);
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00073F04 File Offset: 0x00072104
		internal void HandleEventBubbleUp(EventBase evt)
		{
			bool isReadOnly = this.m_TextElement.edition.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt is BlurEvent;
				if (flag)
				{
					this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				}
				else
				{
					bool flag2 = (!(evt is PointerMoveEvent) && !(evt is MouseMoveEvent)) || this.m_TextElement.selectingManipulator.isClicking;
					if (flag2)
					{
						this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					}
				}
				if (!(evt is FocusInEvent))
				{
					if (evt is FocusOutEvent)
					{
						this.OnFocusOutEvent();
					}
				}
				else
				{
					this.OnFocusInEvent();
				}
				TextEditorEventHandler editingEventHandler = this.editingEventHandler;
				if (editingEventHandler != null)
				{
					editingEventHandler.HandleEventBubbleUp(evt);
				}
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00073FC4 File Offset: 0x000721C4
		private void OnFocusInEvent()
		{
			this.m_TextElement.edition.SaveValueAndText();
			this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			bool touchScreenTextFieldChanged = this.touchScreenTextFieldChanged;
			if (touchScreenTextFieldChanged)
			{
				this.InitTextEditorEventHandler();
			}
			bool flag = this.m_HardwareKeyboardPoller == null;
			if (flag)
			{
				this.m_HardwareKeyboardPoller = this.m_TextElement.schedule.Execute(delegate
				{
					bool touchScreenTextFieldChanged2 = this.touchScreenTextFieldChanged;
					if (touchScreenTextFieldChanged2)
					{
						this.InitTextEditorEventHandler();
						this.m_TextElement.Blur();
					}
				}).Every(250L);
			}
			else
			{
				this.m_HardwareKeyboardPoller.Resume();
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00074057 File Offset: 0x00072257
		private void OnFocusOutEvent()
		{
			IVisualElementScheduledItem hardwareKeyboardPoller = this.m_HardwareKeyboardPoller;
			if (hardwareKeyboardPoller != null)
			{
				hardwareKeyboardPoller.Pause();
			}
			this.editingUtilities.OnBlur();
		}

		// Token: 0x04000E05 RID: 3589
		private readonly TextElement m_TextElement;

		// Token: 0x04000E06 RID: 3590
		private TextEditorEventHandler m_EditingEventHandler;

		// Token: 0x04000E07 RID: 3591
		internal TextEditingUtilities editingUtilities;

		// Token: 0x04000E08 RID: 3592
		private bool m_TouchScreenTextFieldInitialized;

		// Token: 0x04000E09 RID: 3593
		private IVisualElementScheduledItem m_HardwareKeyboardPoller = null;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000ED RID: 237
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00023B87 File Offset: 0x00021D87
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00023B8E File Offset: 0x00021D8E
		internal static long Frame { get; private set; }

		// Token: 0x17000131 RID: 305
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x00023B96 File Offset: 0x00021D96
		private static TouchScreenKeyboard activeTouchScreenKeyboard
		{
			[CompilerGenerated]
			set
			{
				TouchScreenTextEditorEventHandler.<activeTouchScreenKeyboard>k__BackingField = value;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00023B9E File Offset: 0x00021D9E
		public TouchScreenTextEditorEventHandler(TextElement textElement, TextEditingUtilities editingUtilities)
			: base(textElement, editingUtilities)
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00023BC0 File Offset: 0x00021DC0
		private void PollTouchScreenKeyboard()
		{
			this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			bool flag = TouchScreenKeyboard.isSupported && !this.m_TouchKeyboardAllowsInPlaceEditing;
			if (flag)
			{
				bool flag2 = this.m_TouchKeyboardPoller == null;
				if (flag2)
				{
					TextElement textElement = this.textElement;
					this.m_TouchKeyboardPoller = ((textElement != null) ? textElement.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00023C40 File Offset: 0x00021E40
		private void DoPollTouchScreenKeyboard()
		{
			TouchScreenTextEditorEventHandler.Frame += 1L;
			bool flag = this.editingUtilities.TouchScreenKeyboardShouldBeUsed();
			if (flag)
			{
				bool flag2 = this.textElement.m_TouchScreenKeyboard == null;
				if (!flag2)
				{
					ITextEdition edition = this.textElement.edition;
					TouchScreenKeyboard touchKeyboard = this.textElement.m_TouchScreenKeyboard;
					string touchKeyboardText = touchKeyboard.text;
					bool flag3 = touchKeyboard.status > TouchScreenKeyboard.Status.Visible;
					if (flag3)
					{
						bool flag4 = touchKeyboard.status == TouchScreenKeyboard.Status.Canceled;
						if (flag4)
						{
							edition.RestoreValueAndText();
						}
						else
						{
							touchKeyboardText = touchKeyboard.text;
							bool flag5 = this.editingUtilities.text != touchKeyboardText;
							if (flag5)
							{
								edition.UpdateText(touchKeyboardText);
								this.textElement.uitkTextHandle.Update();
							}
						}
						this.CloseTouchScreenKeyboard();
						bool flag6 = !edition.isDelayed;
						if (flag6)
						{
							Action updateValueFromText = edition.UpdateValueFromText;
							if (updateValueFromText != null)
							{
								updateValueFromText();
							}
						}
						Action updateTextFromValue = edition.UpdateTextFromValue;
						if (updateTextFromValue != null)
						{
							updateTextFromValue();
						}
						this.textElement.Blur();
					}
					else
					{
						bool flag7 = this.editingUtilities.text == touchKeyboardText;
						if (!flag7)
						{
							bool hideMobileInput = edition.hideMobileInput;
							if (hideMobileInput)
							{
								bool flag8 = this.editingUtilities.text != touchKeyboardText;
								if (flag8)
								{
									bool changed = false;
									this.editingUtilities.text = "";
									foreach (char character in touchKeyboardText)
									{
										bool flag9 = !edition.AcceptCharacter(character);
										if (flag9)
										{
											return;
										}
										bool flag10 = character > '\0';
										if (flag10)
										{
											TextEditingUtilities editingUtilities = this.editingUtilities;
											editingUtilities.text += character.ToString();
											changed = true;
										}
									}
									bool flag11 = changed;
									if (flag11)
									{
										this.UpdateStringPositionFromKeyboard();
									}
									edition.UpdateText(this.editingUtilities.text);
									this.textElement.uitkTextHandle.ComputeSettingsAndUpdate();
								}
								else
								{
									bool flag12 = !this.m_IsClicking && touchKeyboard != null && touchKeyboard.canGetSelection;
									if (flag12)
									{
										this.UpdateStringPositionFromKeyboard();
									}
								}
							}
							else
							{
								edition.UpdateText(touchKeyboardText);
								this.textElement.uitkTextHandle.ComputeSettingsAndUpdate();
							}
							bool flag13 = !edition.isDelayed;
							if (flag13)
							{
								Action updateValueFromText2 = edition.UpdateValueFromText;
								if (updateValueFromText2 != null)
								{
									updateValueFromText2();
								}
							}
							Action updateTextFromValue2 = edition.UpdateTextFromValue;
							if (updateTextFromValue2 != null)
							{
								updateTextFromValue2();
							}
							Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
							if (updateScrollOffset != null)
							{
								updateScrollOffset(false);
							}
						}
					}
				}
			}
			else
			{
				this.CloseTouchScreenKeyboard();
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00023EFC File Offset: 0x000220FC
		private void UpdateStringPositionFromKeyboard()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard == null;
			if (!flag)
			{
				RangeInt selectionRange = this.textElement.m_TouchScreenKeyboard.selection;
				int selectionStart = selectionRange.start;
				int selectionEnd = selectionRange.end;
				bool flag2 = this.textElement.selection.selectIndex != selectionStart;
				if (flag2)
				{
					this.textElement.selection.selectIndex = selectionStart;
				}
				bool flag3 = this.textElement.selection.cursorIndex != selectionEnd;
				if (flag3)
				{
					this.textElement.selection.cursorIndex = selectionEnd;
				}
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00023F9C File Offset: 0x0002219C
		private void CloseTouchScreenKeyboard()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard != null;
			if (flag)
			{
				this.textElement.m_TouchScreenKeyboard.active = false;
				this.textElement.m_TouchScreenKeyboard = null;
				IVisualElementScheduledItem touchKeyboardPoller = this.m_TouchKeyboardPoller;
				if (touchKeyboardPoller != null)
				{
					touchKeyboardPoller.Pause();
				}
				TouchScreenKeyboard.hideInput = true;
			}
			TouchScreenTextEditorEventHandler.activeTouchScreenKeyboard = null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00023FFC File Offset: 0x000221FC
		private void OpenTouchScreenKeyboard()
		{
			ITextEdition edition = this.textElement.edition;
			TouchScreenKeyboard.hideInput = edition.hideMobileInput;
			this.textElement.m_TouchScreenKeyboard = TouchScreenKeyboard.Open(this.textElement.text, edition.keyboardType, !edition.isPassword && edition.autoCorrection, edition.multiline, edition.isPassword);
			bool hideMobileInput = edition.hideMobileInput;
			if (hideMobileInput)
			{
				int selectIndex = this.textElement.selection.selectIndex;
				int cursorIndex = this.textElement.selection.cursorIndex;
				int length = ((selectIndex < cursorIndex) ? (cursorIndex - selectIndex) : (selectIndex - cursorIndex));
				int start = ((selectIndex < cursorIndex) ? selectIndex : cursorIndex);
				this.textElement.m_TouchScreenKeyboard.selection = new RangeInt(start, length);
			}
			else
			{
				TouchScreenKeyboard touchScreenKeyboard = this.textElement.m_TouchScreenKeyboard;
				string text = this.textElement.m_TouchScreenKeyboard.text;
				touchScreenKeyboard.selection = new RangeInt((text != null) ? text.Length : 0, 0);
			}
			TouchScreenTextEditorEventHandler.activeTouchScreenKeyboard = this.textElement.m_TouchScreenKeyboard;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00024108 File Offset: 0x00022308
		public override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = !this.editingUtilities.TouchScreenKeyboardShouldBeUsed() || this.textElement.edition.isReadOnly;
			if (!flag)
			{
				if (!(evt is PointerDownEvent))
				{
					PointerUpEvent pue = evt as PointerUpEvent;
					if (pue == null)
					{
						if (!(evt is FocusInEvent))
						{
							FocusOutEvent foe = evt as FocusOutEvent;
							if (foe != null)
							{
								this.OnFocusOutEvent(foe);
							}
						}
						else
						{
							this.OnFocusInEvent();
						}
					}
					else
					{
						this.OnPointerUpEvent(pue);
					}
				}
				else
				{
					this.OnPointerDownEvent();
				}
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002419C File Offset: 0x0002239C
		private void OnPointerDownEvent()
		{
			this.m_IsClicking = true;
			bool flag = this.textElement.m_TouchScreenKeyboard != null && this.textElement.edition.hideMobileInput;
			if (flag)
			{
				int selectionStart = this.textElement.selection.cursorIndex;
				string text = this.textElement.m_TouchScreenKeyboard.text;
				int softKeyboardStringLength = ((text != null) ? text.Length : 0);
				bool flag2 = selectionStart < 0;
				if (flag2)
				{
					selectionStart = 0;
				}
				bool flag3 = selectionStart > softKeyboardStringLength;
				if (flag3)
				{
					selectionStart = softKeyboardStringLength;
				}
				this.textElement.m_TouchScreenKeyboard.selection = new RangeInt(selectionStart, 0);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00024233 File Offset: 0x00022433
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			this.m_IsClicking = false;
			evt.StopPropagation();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00024244 File Offset: 0x00022444
		private void OnFocusInEvent()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard != null;
			if (!flag)
			{
				this.OpenTouchScreenKeyboard();
				bool flag2 = this.textElement.m_TouchScreenKeyboard != null;
				if (flag2)
				{
					this.PollTouchScreenKeyboard();
				}
				this.textElement.edition.SaveValueAndText();
				Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000242B4 File Offset: 0x000224B4
		private void OnFocusOutEvent(FocusOutEvent evt)
		{
			TextElement currentFocusedTextElement = (TextElement)evt.target;
			TextElement pendingFocusedTextElement = currentFocusedTextElement.focusController.m_LastPendingFocusedElement as TextElement;
			bool flag = pendingFocusedTextElement == currentFocusedTextElement || pendingFocusedTextElement == null || pendingFocusedTextElement.edition.keyboardType != currentFocusedTextElement.edition.keyboardType || pendingFocusedTextElement.edition.multiline != currentFocusedTextElement.edition.multiline || pendingFocusedTextElement.edition.hideMobileInput != currentFocusedTextElement.edition.hideMobileInput;
			if (flag)
			{
				this.CloseTouchScreenKeyboard();
			}
			else
			{
				this.textElement.m_TouchScreenKeyboard = null;
				IVisualElementScheduledItem touchKeyboardPoller = this.m_TouchKeyboardPoller;
				if (touchKeyboardPoller != null)
				{
					touchKeyboardPoller.Pause();
				}
			}
		}

		// Token: 0x040004A2 RID: 1186
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;

		// Token: 0x040004A3 RID: 1187
		private bool m_TouchKeyboardAllowsInPlaceEditing = false;

		// Token: 0x040004A4 RID: 1188
		private bool m_IsClicking = false;
	}
}

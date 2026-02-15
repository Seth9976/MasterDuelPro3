using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E4 RID: 228
	internal class KeyboardTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00021136 File Offset: 0x0001F336
		public KeyboardTextEditorEventHandler(TextElement textElement, TextEditingUtilities editingUtilities)
			: base(textElement, editingUtilities)
		{
			editingUtilities.multiline = textElement.edition.multiline;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00021160 File Offset: 0x0001F360
		public override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			KeyDownEvent kde = evt as KeyDownEvent;
			if (kde == null)
			{
				ValidateCommandEvent vce = evt as ValidateCommandEvent;
				if (vce == null)
				{
					ExecuteCommandEvent ece = evt as ExecuteCommandEvent;
					if (ece == null)
					{
						FocusEvent fe = evt as FocusEvent;
						if (fe == null)
						{
							BlurEvent be = evt as BlurEvent;
							if (be == null)
							{
								NavigationMoveEvent ne = evt as NavigationMoveEvent;
								if (ne == null)
								{
									NavigationSubmitEvent ne2 = evt as NavigationSubmitEvent;
									if (ne2 == null)
									{
										NavigationCancelEvent ne3 = evt as NavigationCancelEvent;
										if (ne3 != null)
										{
											this.OnNavigationEvent<NavigationCancelEvent>(ne3);
										}
									}
									else
									{
										this.OnNavigationEvent<NavigationSubmitEvent>(ne2);
									}
								}
								else
								{
									this.OnNavigationEvent<NavigationMoveEvent>(ne);
								}
							}
							else
							{
								this.OnBlur(be);
							}
						}
						else
						{
							this.OnFocus(fe);
						}
					}
					else
					{
						this.OnExecuteCommandEvent(ece);
					}
				}
				else
				{
					this.OnValidateCommandEvent(vce);
				}
			}
			else
			{
				this.OnKeyDown(kde);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00021243 File Offset: 0x0001F443
		private void OnFocus(FocusEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.On;
			this.textElement.edition.SaveValueAndText();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0002125E File Offset: 0x0001F45E
		private void OnBlur(BlurEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.Auto;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00021268 File Offset: 0x0001F468
		private void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = !this.textElement.hasFocus;
			if (!flag)
			{
				this.m_Changed = false;
				evt.GetEquivalentImguiEvent(this.m_ImguiEvent);
				bool generatePreview = false;
				bool flag2 = this.editingUtilities.HandleKeyEvent(this.m_ImguiEvent);
				if (flag2)
				{
					bool flag3 = this.textElement.text != this.editingUtilities.text;
					if (flag3)
					{
						this.m_Changed = true;
					}
					evt.StopPropagation();
				}
				else
				{
					char c = evt.character;
					bool flag4 = evt.actionKey && (!evt.altKey || c == '\0');
					if (flag4)
					{
						return;
					}
					bool flag5 = c == '\t' && evt.keyCode == KeyCode.None && evt.modifiers == EventModifiers.None;
					if (flag5)
					{
						return;
					}
					bool flag6 = evt.keyCode == KeyCode.Tab || (evt.keyCode == KeyCode.Tab && evt.character == '\t' && evt.modifiers == EventModifiers.Shift);
					if (flag6)
					{
						bool flag7 = !this.textElement.edition.multiline || evt.shiftKey;
						if (flag7)
						{
							bool flag8 = evt.ShouldSendNavigationMoveEvent();
							if (flag8)
							{
								this.textElement.focusController.FocusNextInDirection(this.textElement, evt.shiftKey ? VisualElementFocusChangeDirection.left : VisualElementFocusChangeDirection.right);
								evt.StopPropagation();
							}
							return;
						}
						bool flag9 = !evt.ShouldSendNavigationMoveEvent();
						if (flag9)
						{
							return;
						}
					}
					bool flag10 = !this.textElement.edition.multiline && (evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return);
					if (flag10)
					{
						Action updateValueFromText = this.textElement.edition.UpdateValueFromText;
						if (updateValueFromText != null)
						{
							updateValueFromText();
						}
					}
					evt.StopPropagation();
					bool flag11 = (this.textElement.edition.multiline ? (c == '\n' && evt.shiftKey) : ((c == '\n' || c == '\r' || c == '\n') && !evt.altKey));
					if (flag11)
					{
						Action moveFocusToCompositeRoot = this.textElement.edition.MoveFocusToCompositeRoot;
						if (moveFocusToCompositeRoot != null)
						{
							moveFocusToCompositeRoot();
						}
						return;
					}
					bool flag12 = evt.keyCode == KeyCode.Escape;
					if (flag12)
					{
						this.textElement.edition.RestoreValueAndText();
						Action updateValueFromText2 = this.textElement.edition.UpdateValueFromText;
						if (updateValueFromText2 != null)
						{
							updateValueFromText2();
						}
						Action moveFocusToCompositeRoot2 = this.textElement.edition.MoveFocusToCompositeRoot;
						if (moveFocusToCompositeRoot2 != null)
						{
							moveFocusToCompositeRoot2();
						}
					}
					bool flag13 = evt.keyCode == KeyCode.Tab;
					if (flag13)
					{
						c = '\t';
					}
					bool flag14 = !this.textElement.edition.AcceptCharacter(c);
					if (flag14)
					{
						return;
					}
					bool flag15 = c >= ' ' || evt.keyCode == KeyCode.Tab || (this.textElement.edition.multiline && !evt.altKey && (c == '\n' || c == '\r' || c == '\n'));
					if (flag15)
					{
						this.m_Changed = this.editingUtilities.Insert(c);
					}
					else
					{
						bool oldIsCompositionActive = this.editingUtilities.isCompositionActive;
						generatePreview = true;
						bool flag16 = this.editingUtilities.UpdateImeState() || oldIsCompositionActive != this.editingUtilities.isCompositionActive;
						if (flag16)
						{
							this.m_Changed = true;
						}
					}
				}
				bool changed = this.m_Changed;
				if (changed)
				{
					this.UpdateLabel(generatePreview);
				}
				Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(evt.keyCode == KeyCode.Backspace);
				}
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00021624 File Offset: 0x0001F824
		private void UpdateLabel(bool generatePreview)
		{
			string oldText = this.editingUtilities.text;
			bool imeEnabled = this.editingUtilities.UpdateImeState();
			bool flag = imeEnabled && this.editingUtilities.ShouldUpdateImeWindowPosition();
			if (flag)
			{
				this.editingUtilities.SetImeWindowPosition(new Vector2(this.textElement.worldBound.x, this.textElement.worldBound.y));
			}
			string fullText = (generatePreview ? this.editingUtilities.GeneratePreviewString(this.textElement.enableRichText) : this.editingUtilities.text);
			this.textElement.edition.UpdateText(fullText);
			bool flag2 = !this.textElement.edition.isDelayed;
			if (flag2)
			{
				Action updateValueFromText = this.textElement.edition.UpdateValueFromText;
				if (updateValueFromText != null)
				{
					updateValueFromText();
				}
			}
			bool flag3 = imeEnabled;
			if (flag3)
			{
				this.editingUtilities.text = oldText;
				this.editingUtilities.EnableCursorPreviewState();
			}
			this.textElement.uitkTextHandle.ComputeSettingsAndUpdate();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0002173C File Offset: 0x0001F93C
		private void OnValidateCommandEvent(ValidateCommandEvent evt)
		{
			bool flag = !this.textElement.hasFocus;
			if (!flag)
			{
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "Copy") && !(text == "SelectAll"))
				{
					if (!(text == "Cut"))
					{
						if (!(text == "Paste"))
						{
							if (!(text == "Delete"))
							{
								if (!(text == "UndoRedoPerformed"))
								{
								}
							}
						}
						else
						{
							bool flag2 = !this.editingUtilities.CanPaste();
							if (flag2)
							{
								return;
							}
						}
					}
					else
					{
						bool flag3 = !this.textElement.selection.HasSelection();
						if (flag3)
						{
							return;
						}
					}
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000217FC File Offset: 0x0001F9FC
		private void OnExecuteCommandEvent(ExecuteCommandEvent evt)
		{
			bool flag = !this.textElement.hasFocus;
			if (!flag)
			{
				this.m_Changed = false;
				bool mayHaveChanged = false;
				string oldText = this.editingUtilities.text;
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "OnLostFocus"))
				{
					if (!(text == "Cut"))
					{
						if (!(text == "Paste"))
						{
							if (text == "Delete")
							{
								this.editingUtilities.Cut();
								mayHaveChanged = true;
								evt.StopPropagation();
							}
						}
						else
						{
							this.editingUtilities.Paste();
							mayHaveChanged = true;
							evt.StopPropagation();
						}
					}
					else
					{
						this.editingUtilities.Cut();
						mayHaveChanged = true;
						evt.StopPropagation();
					}
					bool flag2 = mayHaveChanged;
					if (flag2)
					{
						bool flag3 = oldText != this.editingUtilities.text;
						if (flag3)
						{
							this.m_Changed = true;
						}
						evt.StopPropagation();
					}
					bool changed = this.m_Changed;
					if (changed)
					{
						this.UpdateLabel(true);
					}
					Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
					if (updateScrollOffset != null)
					{
						updateScrollOffset(false);
					}
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00021928 File Offset: 0x0001FB28
		private void OnNavigationEvent<TEvent>(NavigationEventBase<TEvent> evt) where TEvent : NavigationEventBase<TEvent>, new()
		{
			bool flag = evt.deviceType == NavigationDeviceType.Keyboard || evt.deviceType == NavigationDeviceType.Unknown;
			if (flag)
			{
				evt.StopPropagation();
				this.textElement.focusController.IgnoreEvent(evt);
			}
		}

		// Token: 0x04000454 RID: 1108
		private readonly Event m_ImguiEvent = new Event();

		// Token: 0x04000455 RID: 1109
		internal bool m_Changed;
	}
}

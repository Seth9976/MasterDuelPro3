using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000449 RID: 1097
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class TextSelectingManipulator
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00074B42 File Offset: 0x00072D42
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x00074B4C File Offset: 0x00072D4C
		internal bool isClicking
		{
			get
			{
				return this.m_IsClicking;
			}
			private set
			{
				bool flag = this.m_IsClicking == value;
				if (!flag)
				{
					this.m_IsClicking = value;
				}
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00074B70 File Offset: 0x00072D70
		public TextSelectingManipulator(TextElement textElement)
		{
			this.m_TextElement = textElement;
			this.m_SelectingUtilities = new TextSelectingUtilities(this.m_TextElement.uitkTextHandle);
			TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
			selectingUtilities.OnCursorIndexChange = (Action)Delegate.Combine(selectingUtilities.OnCursorIndexChange, new Action(this.OnCursorIndexChange));
			TextSelectingUtilities selectingUtilities2 = this.m_SelectingUtilities;
			selectingUtilities2.OnSelectIndexChange = (Action)Delegate.Combine(selectingUtilities2.OnSelectIndexChange, new Action(this.OnSelectIndexChange));
			TextSelectingUtilities selectingUtilities3 = this.m_SelectingUtilities;
			selectingUtilities3.OnRevealCursorChange = (Action)Delegate.Combine(selectingUtilities3.OnRevealCursorChange, new Action(this.OnRevealCursor));
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00074C2A File Offset: 0x00072E2A
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x00074C3E File Offset: 0x00072E3E
		internal int cursorIndex
		{
			get
			{
				TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
				return (selectingUtilities != null) ? selectingUtilities.cursorIndex : (-1);
			}
			set
			{
				this.m_SelectingUtilities.cursorIndex = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x00074C4D File Offset: 0x00072E4D
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x00074C61 File Offset: 0x00072E61
		internal int selectIndex
		{
			get
			{
				TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
				return (selectingUtilities != null) ? selectingUtilities.selectIndex : (-1);
			}
			set
			{
				this.m_SelectingUtilities.selectIndex = value;
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00074C70 File Offset: 0x00072E70
		private void OnRevealCursor()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00074C84 File Offset: 0x00072E84
		private void OnSelectIndexChange()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
			bool flag = this.HasSelection() && this.m_TextElement.focusController != null;
			if (flag)
			{
				this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			}
			bool revealCursor = this.m_SelectingUtilities.revealCursor;
			if (revealCursor)
			{
				Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00074D04 File Offset: 0x00072F04
		private void OnCursorIndexChange()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
			bool flag = this.HasSelection() && this.m_TextElement.focusController != null;
			if (flag)
			{
				this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			}
			bool revealCursor = this.m_SelectingUtilities.revealCursor;
			if (revealCursor)
			{
				Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00074D84 File Offset: 0x00072F84
		internal bool RevealCursor()
		{
			return this.m_SelectingUtilities.revealCursor;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00074DA4 File Offset: 0x00072FA4
		internal bool HasSelection()
		{
			return this.m_SelectingUtilities.hasSelection;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00074DC4 File Offset: 0x00072FC4
		internal bool HasFocus()
		{
			return this.m_TextElement.hasFocus;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00074DE4 File Offset: 0x00072FE4
		internal void HandleEventBubbleUp(EventBase evt)
		{
			bool flag = evt is BlurEvent;
			if (flag)
			{
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
			}
			else
			{
				bool flag2 = (!(evt is PointerMoveEvent) && !(evt is MouseMoveEvent)) || this.isClicking;
				if (flag2)
				{
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
				}
			}
			if (!(evt is FocusEvent))
			{
				if (!(evt is BlurEvent))
				{
					ValidateCommandEvent vce = evt as ValidateCommandEvent;
					if (vce == null)
					{
						ExecuteCommandEvent ece = evt as ExecuteCommandEvent;
						if (ece == null)
						{
							KeyDownEvent kde = evt as KeyDownEvent;
							if (kde == null)
							{
								PointerDownEvent pde = evt as PointerDownEvent;
								if (pde == null)
								{
									PointerMoveEvent pme = evt as PointerMoveEvent;
									if (pme == null)
									{
										PointerUpEvent pue = evt as PointerUpEvent;
										if (pue != null)
										{
											this.OnPointerUpEvent(pue);
										}
									}
									else
									{
										this.OnPointerMoveEvent(pme);
									}
								}
								else
								{
									this.OnPointerDownEvent(pde);
								}
							}
							else
							{
								this.OnKeyDown(kde);
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
					this.OnBlurEvent();
				}
			}
			else
			{
				this.OnFocusEvent();
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00074F08 File Offset: 0x00073108
		private void OnFocusEvent()
		{
			this.selectAllOnMouseUp = false;
			bool flag = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId) != 0 || (this.m_TextElement.panel.contextType == ContextType.Editor && Event.current == null);
			if (flag)
			{
				this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp;
			}
			this.m_SelectingUtilities.OnFocus(this.m_TextElement.selection.selectAllOnFocus && !this.isClicking);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00074F8E File Offset: 0x0007318E
		private void OnBlurEvent()
		{
			this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00074FA8 File Offset: 0x000731A8
		private void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				evt.GetEquivalentImguiEvent(this.m_ImguiEvent);
				bool flag2 = this.m_SelectingUtilities.HandleKeyEvent(this.m_ImguiEvent);
				if (flag2)
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00074FF4 File Offset: 0x000731F4
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			Vector3 pointerPosition = evt.localPosition - this.m_TextElement.contentRect.min;
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = evt.timestamp - this.m_LastMouseDownTimeStamp < (long)Event.GetDoubleClickTime();
				if (flag2)
				{
					this.m_ConsecutiveMouseDownCount++;
				}
				else
				{
					this.m_ConsecutiveMouseDownCount = 1;
				}
				bool flag3 = this.m_ConsecutiveMouseDownCount == 2 && this.m_TextElement.selection.doubleClickSelectsWord;
				if (flag3)
				{
					bool flag4 = this.cursorIndex == 0 && this.cursorIndex != this.selectIndex;
					if (flag4)
					{
						this.m_SelectingUtilities.MoveCursorToPosition_Internal(pointerPosition, evt.shiftKey);
					}
					this.m_SelectingUtilities.SelectCurrentWord();
					this.m_SelectingUtilities.MouseDragSelectsWholeWords(true);
					this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
				}
				else
				{
					bool flag5 = this.m_ConsecutiveMouseDownCount == 3 && this.m_TextElement.selection.tripleClickSelectsLine;
					if (flag5)
					{
						this.m_SelectingUtilities.SelectCurrentParagraph();
						this.m_SelectingUtilities.MouseDragSelectsWholeWords(true);
						this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					else
					{
						this.m_SelectingUtilities.MoveCursorToPosition_Internal(pointerPosition, evt.shiftKey);
						Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
						if (updateScrollOffset != null)
						{
							updateScrollOffset(false);
						}
						this.m_SelectingUtilities.MouseDragSelectsWholeWords(false);
						this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
					}
				}
				this.m_LastMouseDownTimeStamp = evt.timestamp;
				this.isClicking = true;
				this.m_TextElement.CapturePointer(evt.pointerId);
				this.m_ClickStartPosition = pointerPosition;
				evt.StopPropagation();
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000751C8 File Offset: 0x000733C8
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = !this.isClicking;
			if (!flag)
			{
				Vector3 pointerPosition = evt.localPosition - this.m_TextElement.contentRect.min;
				this.m_Dragged = this.m_Dragged || this.MoveDistanceQualifiesForDrag(this.m_ClickStartPosition, pointerPosition);
				bool dragged = this.m_Dragged;
				if (dragged)
				{
					this.m_SelectingUtilities.SelectToPosition(pointerPosition);
					Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
					if (updateScrollOffset != null)
					{
						updateScrollOffset(false);
					}
					this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp && !this.m_SelectingUtilities.hasSelection;
				}
				evt.StopPropagation();
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007529C File Offset: 0x0007349C
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = evt.button != 0 || !this.isClicking;
			if (!flag)
			{
				bool flag2 = this.selectAllOnMouseUp;
				if (flag2)
				{
					this.m_SelectingUtilities.SelectAll();
				}
				this.selectAllOnMouseUp = false;
				this.m_Dragged = false;
				this.isClicking = false;
				this.m_TextElement.ReleasePointer(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0007530C File Offset: 0x0007350C
		private void OnValidateCommandEvent(ValidateCommandEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "Cut") && !(text == "Paste") && !(text == "Delete") && !(text == "UndoRedoPerformed"))
				{
					if (!(text == "Copy"))
					{
						if (!(text == "SelectAll"))
						{
						}
					}
					else
					{
						bool flag2 = !this.m_SelectingUtilities.hasSelection;
						if (flag2)
						{
							return;
						}
					}
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000753A8 File Offset: 0x000735A8
		private void OnExecuteCommandEvent(ExecuteCommandEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "OnLostFocus"))
				{
					if (!(text == "Copy"))
					{
						if (text == "SelectAll")
						{
							this.m_SelectingUtilities.SelectAll();
							evt.StopPropagation();
						}
					}
					else
					{
						this.m_SelectingUtilities.Copy();
						evt.StopPropagation();
					}
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00075430 File Offset: 0x00073630
		private bool MoveDistanceQualifiesForDrag(Vector2 start, Vector2 current)
		{
			return (start - current).sqrMagnitude >= 16f;
		}

		// Token: 0x04000E17 RID: 3607
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal TextSelectingUtilities m_SelectingUtilities;

		// Token: 0x04000E18 RID: 3608
		private bool selectAllOnMouseUp;

		// Token: 0x04000E19 RID: 3609
		private TextElement m_TextElement;

		// Token: 0x04000E1A RID: 3610
		private Vector2 m_ClickStartPosition;

		// Token: 0x04000E1B RID: 3611
		private bool m_Dragged;

		// Token: 0x04000E1C RID: 3612
		private bool m_IsClicking;

		// Token: 0x04000E1D RID: 3613
		private const int k_DragThresholdSqr = 16;

		// Token: 0x04000E1E RID: 3614
		private int m_ConsecutiveMouseDownCount;

		// Token: 0x04000E1F RID: 3615
		private long m_LastMouseDownTimeStamp = 0L;

		// Token: 0x04000E20 RID: 3616
		private readonly Event m_ImguiEvent = new Event();
	}
}

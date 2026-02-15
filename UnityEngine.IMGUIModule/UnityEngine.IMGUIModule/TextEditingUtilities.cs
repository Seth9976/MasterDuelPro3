using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextEditingUtilities
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008C23 File Offset: 0x00006E23
		private bool hasSelection
		{
			get
			{
				return this.m_TextSelectingUtility.hasSelection;
			}
		}

		// Token: 0x17000077 RID: 119
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00008C30 File Offset: 0x00006E30
		internal bool revealCursor
		{
			set
			{
				this.m_TextSelectingUtility.revealCursor = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008C3F File Offset: 0x00006E3F
		internal int stringCursorIndex
		{
			get
			{
				return this.textHandle.GetCorrespondingStringIndex(this.cursorIndex);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008C52 File Offset: 0x00006E52
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00008C5F File Offset: 0x00006E5F
		private int cursorIndex
		{
			get
			{
				return this.m_TextSelectingUtility.cursorIndex;
			}
			set
			{
				this.m_TextSelectingUtility.cursorIndex = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008C6E File Offset: 0x00006E6E
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00008C7B File Offset: 0x00006E7B
		private int cursorIndexNoValidation
		{
			get
			{
				return this.m_TextSelectingUtility.cursorIndexNoValidation;
			}
			set
			{
				this.m_TextSelectingUtility.cursorIndexNoValidation = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00008C8A File Offset: 0x00006E8A
		private int selectIndexNoValidation
		{
			set
			{
				this.m_TextSelectingUtility.selectIndexNoValidation = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008C99 File Offset: 0x00006E99
		internal int stringSelectIndex
		{
			get
			{
				return this.textHandle.GetCorrespondingStringIndex(this.selectIndex);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008CAC File Offset: 0x00006EAC
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00008CB9 File Offset: 0x00006EB9
		private int selectIndex
		{
			get
			{
				return this.m_TextSelectingUtility.selectIndex;
			}
			set
			{
				this.m_TextSelectingUtility.selectIndex = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008CC8 File Offset: 0x00006EC8
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				bool flag = value == this.m_Text;
				if (!flag)
				{
					this.m_Text = value ?? string.Empty;
					Action onTextChanged = this.OnTextChanged;
					if (onTextChanged != null)
					{
						onTextChanged();
					}
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008D12 File Offset: 0x00006F12
		internal void SetTextWithoutNotify(string value)
		{
			this.m_Text = value;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008D1C File Offset: 0x00006F1C
		public TextEditingUtilities(TextSelectingUtilities selectingUtilities, TextHandle textHandle, string text)
		{
			this.m_TextSelectingUtility = selectingUtilities;
			this.textHandle = textHandle;
			this.m_Text = text;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008D4C File Offset: 0x00006F4C
		public bool UpdateImeState()
		{
			bool flag = GUIUtility.compositionString.Length > 0;
			if (flag)
			{
				bool flag2 = !this.isCompositionActive;
				if (flag2)
				{
					this.m_UpdateImeWindowPosition = true;
					this.ReplaceSelection(string.Empty);
				}
				this.isCompositionActive = true;
			}
			else
			{
				this.isCompositionActive = false;
			}
			return this.isCompositionActive;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008DAC File Offset: 0x00006FAC
		public bool ShouldUpdateImeWindowPosition()
		{
			return this.m_UpdateImeWindowPosition;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public void SetImeWindowPosition(Vector2 worldPosition)
		{
			Vector2 cursorPos = this.textHandle.GetCursorPositionFromStringIndexUsingCharacterHeight(this.cursorIndex, true);
			GUIUtility.compositionCursorPos = worldPosition + cursorPos;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public string GeneratePreviewString(bool richText)
		{
			this.RestoreCursorState();
			string compositionString = GUIUtility.compositionString;
			bool flag = this.isCompositionActive;
			string text;
			if (flag)
			{
				text = (richText ? this.text.Insert(this.stringCursorIndex, "<u>" + compositionString + "</u>") : this.text.Insert(this.stringCursorIndex, compositionString));
			}
			else
			{
				text = this.text;
			}
			return text;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008E60 File Offset: 0x00007060
		public void EnableCursorPreviewState()
		{
			bool flag = this.m_CursorIndexSavedState != -1;
			if (!flag)
			{
				this.m_CursorIndexSavedState = this.m_TextSelectingUtility.cursorIndexNoValidation;
				this.cursorIndexNoValidation = (this.selectIndexNoValidation = this.m_CursorIndexSavedState + GUIUtility.compositionString.Length);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008EB4 File Offset: 0x000070B4
		public void RestoreCursorState()
		{
			bool flag = this.m_CursorIndexSavedState == -1;
			if (!flag)
			{
				this.cursorIndex = (this.selectIndex = this.m_CursorIndexSavedState);
				this.m_CursorIndexSavedState = -1;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008EF0 File Offset: 0x000070F0
		[VisibleToOtherModules]
		internal bool HandleKeyEvent(Event e)
		{
			this.RestoreCursorState();
			this.InitKeyActions();
			EventModifiers i = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			bool flag = TextEditingUtilities.s_KeyEditOps.ContainsKey(e);
			bool flag2;
			if (flag)
			{
				TextEditOp op = TextEditingUtilities.s_KeyEditOps[e];
				this.PerformOperation(op);
				e.modifiers = i;
				flag2 = true;
			}
			else
			{
				e.modifiers = i;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008F60 File Offset: 0x00007160
		private void PerformOperation(TextEditOp operation)
		{
			this.revealCursor = true;
			switch (operation)
			{
			case TextEditOp.MoveLeft:
				this.m_TextSelectingUtility.MoveLeft();
				return;
			case TextEditOp.MoveRight:
				this.m_TextSelectingUtility.MoveRight();
				return;
			case TextEditOp.MoveUp:
				this.m_TextSelectingUtility.MoveUp();
				return;
			case TextEditOp.MoveDown:
				this.m_TextSelectingUtility.MoveDown();
				return;
			case TextEditOp.MoveLineStart:
				this.m_TextSelectingUtility.MoveLineStart();
				return;
			case TextEditOp.MoveLineEnd:
				this.m_TextSelectingUtility.MoveLineEnd();
				return;
			case TextEditOp.MoveTextStart:
				this.m_TextSelectingUtility.MoveTextStart();
				return;
			case TextEditOp.MoveTextEnd:
				this.m_TextSelectingUtility.MoveTextEnd();
				return;
			case TextEditOp.MoveGraphicalLineStart:
				this.m_TextSelectingUtility.MoveGraphicalLineStart();
				return;
			case TextEditOp.MoveGraphicalLineEnd:
				this.m_TextSelectingUtility.MoveGraphicalLineEnd();
				return;
			case TextEditOp.MoveWordLeft:
				this.m_TextSelectingUtility.MoveWordLeft();
				return;
			case TextEditOp.MoveWordRight:
				this.m_TextSelectingUtility.MoveWordRight();
				return;
			case TextEditOp.MoveParagraphForward:
				this.m_TextSelectingUtility.MoveParagraphForward();
				return;
			case TextEditOp.MoveParagraphBackward:
				this.m_TextSelectingUtility.MoveParagraphBackward();
				return;
			case TextEditOp.MoveToStartOfNextWord:
				this.m_TextSelectingUtility.MoveToStartOfNextWord();
				return;
			case TextEditOp.MoveToEndOfPreviousWord:
				this.m_TextSelectingUtility.MoveToEndOfPreviousWord();
				return;
			case TextEditOp.Delete:
				this.Delete();
				return;
			case TextEditOp.Backspace:
				this.Backspace();
				return;
			case TextEditOp.DeleteWordBack:
				this.DeleteWordBack();
				return;
			case TextEditOp.DeleteWordForward:
				this.DeleteWordForward();
				return;
			case TextEditOp.DeleteLineBack:
				this.DeleteLineBack();
				return;
			case TextEditOp.Cut:
				this.Cut();
				return;
			case TextEditOp.Paste:
				this.Paste();
				return;
			}
			Debug.Log("Unimplemented: " + operation.ToString());
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000914E File Offset: 0x0000734E
		private static void MapKey(string key, TextEditOp action)
		{
			TextEditingUtilities.s_KeyEditOps[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009164 File Offset: 0x00007364
		private void InitKeyActions()
		{
			bool flag = TextEditingUtilities.s_KeyEditOps != null;
			if (!flag)
			{
				TextEditingUtilities.s_KeyEditOps = new Dictionary<Event, TextEditOp>();
				TextEditingUtilities.MapKey("left", TextEditOp.MoveLeft);
				TextEditingUtilities.MapKey("right", TextEditOp.MoveRight);
				TextEditingUtilities.MapKey("up", TextEditOp.MoveUp);
				TextEditingUtilities.MapKey("down", TextEditOp.MoveDown);
				TextEditingUtilities.MapKey("delete", TextEditOp.Delete);
				TextEditingUtilities.MapKey("backspace", TextEditOp.Backspace);
				TextEditingUtilities.MapKey("#backspace", TextEditOp.Backspace);
				bool flag2 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
				if (flag2)
				{
					TextEditingUtilities.MapKey("^left", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("^right", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("&left", TextEditOp.MoveWordLeft);
					TextEditingUtilities.MapKey("&right", TextEditOp.MoveWordRight);
					TextEditingUtilities.MapKey("&up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("&down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("%left", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("%right", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("%up", TextEditOp.MoveTextStart);
					TextEditingUtilities.MapKey("%down", TextEditOp.MoveTextEnd);
					TextEditingUtilities.MapKey("%x", TextEditOp.Cut);
					TextEditingUtilities.MapKey("%v", TextEditOp.Paste);
					TextEditingUtilities.MapKey("^d", TextEditOp.Delete);
					TextEditingUtilities.MapKey("^h", TextEditOp.Backspace);
					TextEditingUtilities.MapKey("^b", TextEditOp.MoveLeft);
					TextEditingUtilities.MapKey("^f", TextEditOp.MoveRight);
					TextEditingUtilities.MapKey("^a", TextEditOp.MoveLineStart);
					TextEditingUtilities.MapKey("^e", TextEditOp.MoveLineEnd);
					TextEditingUtilities.MapKey("&delete", TextEditOp.DeleteWordForward);
					TextEditingUtilities.MapKey("&backspace", TextEditOp.DeleteWordBack);
					TextEditingUtilities.MapKey("%backspace", TextEditOp.DeleteLineBack);
				}
				else
				{
					TextEditingUtilities.MapKey("home", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("end", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("%left", TextEditOp.MoveWordLeft);
					TextEditingUtilities.MapKey("%right", TextEditOp.MoveWordRight);
					TextEditingUtilities.MapKey("%up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("%down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("^left", TextEditOp.MoveToEndOfPreviousWord);
					TextEditingUtilities.MapKey("^right", TextEditOp.MoveToStartOfNextWord);
					TextEditingUtilities.MapKey("^up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("^down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("^delete", TextEditOp.DeleteWordForward);
					TextEditingUtilities.MapKey("^backspace", TextEditOp.DeleteWordBack);
					TextEditingUtilities.MapKey("%backspace", TextEditOp.DeleteLineBack);
					TextEditingUtilities.MapKey("^x", TextEditOp.Cut);
					TextEditingUtilities.MapKey("^v", TextEditOp.Paste);
					TextEditingUtilities.MapKey("#delete", TextEditOp.Cut);
					TextEditingUtilities.MapKey("#insert", TextEditOp.Paste);
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000093E4 File Offset: 0x000075E4
		public bool DeleteLineBack()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool useAdvancedText = this.textHandle.useAdvancedText;
				if (useAdvancedText)
				{
					int start = this.textHandle.GetFirstCharacterIndexOnLine(this.cursorIndex);
					bool flag2 = start != this.cursorIndex;
					if (flag2)
					{
						this.text = this.text.Remove(start, this.stringCursorIndex - start);
						this.cursorIndex = (this.selectIndex = start);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					LineInfo currentLineInfo = this.textHandle.GetLineInfoFromCharacterIndex(this.cursorIndex);
					int startIndex = currentLineInfo.firstCharacterIndex;
					int stringStartIndex = this.textHandle.GetCorrespondingStringIndex(startIndex);
					bool flag3 = startIndex != this.cursorIndex;
					if (flag3)
					{
						this.text = this.text.Remove(stringStartIndex, this.stringCursorIndex - stringStartIndex);
						this.cursorIndex = (this.selectIndex = startIndex);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000094FC File Offset: 0x000076FC
		public bool DeleteWordBack()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int prevWordEnd = this.m_TextSelectingUtility.FindEndOfPreviousWord(this.cursorIndex);
				bool flag2 = this.cursorIndex != prevWordEnd;
				if (flag2)
				{
					int prevWordEndString = this.textHandle.GetCorrespondingStringIndex(prevWordEnd);
					this.text = this.text.Remove(prevWordEndString, this.stringCursorIndex - prevWordEndString);
					this.selectIndex = (this.cursorIndex = prevWordEnd);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009594 File Offset: 0x00007794
		public bool DeleteWordForward()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int nextWordStart = this.m_TextSelectingUtility.FindStartOfNextWord(this.cursorIndex);
				bool flag2 = this.cursorIndex < this.text.Length;
				if (flag2)
				{
					int nextWordStartString = this.textHandle.GetCorrespondingStringIndex(nextWordStart);
					this.text = this.text.Remove(this.stringCursorIndex, nextWordStartString - this.stringCursorIndex);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009624 File Offset: 0x00007824
		public bool Delete()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.stringCursorIndex < this.text.Length;
				if (flag2)
				{
					bool useAdvancedText = this.textHandle.useAdvancedText;
					int count;
					if (useAdvancedText)
					{
						count = this.textHandle.NextCodePointIndex(this.cursorIndex) - this.cursorIndex;
					}
					else
					{
						count = this.textHandle.textInfo.textElementInfo[this.cursorIndex].stringLength;
					}
					this.text = this.text.Remove(this.stringCursorIndex, count);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000096DC File Offset: 0x000078DC
		public bool Backspace()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.cursorIndex > 0;
				if (flag2)
				{
					int startIndex = this.m_TextSelectingUtility.PreviousCodePointIndex(this.cursorIndex);
					bool useAdvancedText = this.textHandle.useAdvancedText;
					int count;
					if (useAdvancedText)
					{
						count = (char.IsSurrogate(this.text[this.cursorIndex - 1]) ? 2 : 1);
					}
					else
					{
						count = this.textHandle.textInfo.textElementInfo[this.cursorIndex - 1].stringLength;
					}
					this.text = this.text.Remove(this.stringCursorIndex - count, count);
					this.cursorIndex = (this.textHandle.useAdvancedText ? Math.Max(0, this.cursorIndex - count) : startIndex);
					this.selectIndex = (this.textHandle.useAdvancedText ? Math.Max(0, this.selectIndex - count) : startIndex);
					this.m_TextSelectingUtility.ClearCursorPos();
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009804 File Offset: 0x00007A04
		public bool DeleteSelection()
		{
			bool flag = this.cursorIndex == this.selectIndex;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.cursorIndex < this.selectIndex;
				if (flag3)
				{
					this.text = this.text.Substring(0, this.stringCursorIndex) + this.text.Substring(this.stringSelectIndex, this.text.Length - this.stringSelectIndex);
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.text = this.text.Substring(0, this.stringSelectIndex) + this.text.Substring(this.stringCursorIndex, this.text.Length - this.stringCursorIndex);
					this.cursorIndex = this.selectIndex;
				}
				this.m_TextSelectingUtility.ClearCursorPos();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000098F0 File Offset: 0x00007AF0
		public void ReplaceSelection(string replace)
		{
			this.RestoreCursorState();
			this.DeleteSelection();
			this.text = this.text.Insert(this.stringCursorIndex, replace);
			int length = (this.textHandle.useAdvancedText ? replace.Length : new StringInfo(replace).LengthInTextElements);
			int newIndex = this.cursorIndexNoValidation + length;
			this.cursorIndexNoValidation = newIndex;
			this.selectIndexNoValidation = newIndex;
			this.m_TextSelectingUtility.ClearCursorPos();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000996C File Offset: 0x00007B6C
		public bool Insert(char c)
		{
			bool flag = char.IsHighSurrogate(c);
			bool flag2;
			if (flag)
			{
				this.m_HighSurrogate = c;
				flag2 = false;
			}
			else
			{
				bool flag3 = char.IsLowSurrogate(c);
				if (flag3)
				{
					char lowSurrogate = c;
					string combinedString = new string(new char[] { this.m_HighSurrogate, lowSurrogate });
					this.ReplaceSelection(combinedString.ToString());
					flag2 = true;
				}
				else
				{
					this.ReplaceSelection(c.ToString());
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000099DC File Offset: 0x00007BDC
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000099FC File Offset: 0x00007BFC
		public bool Cut()
		{
			this.m_TextSelectingUtility.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009A20 File Offset: 0x00007C20
		public bool Paste()
		{
			this.RestoreCursorState();
			string pasteval = GUIUtility.systemCopyBuffer;
			bool flag = pasteval != "";
			bool flag3;
			if (flag)
			{
				bool flag2 = !this.multiline;
				if (flag2)
				{
					pasteval = TextEditingUtilities.ReplaceNewlinesWithSpaces(pasteval);
				}
				this.ReplaceSelection(pasteval);
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009A70 File Offset: 0x00007C70
		private static string ReplaceNewlinesWithSpaces(string value)
		{
			value = value.Replace("\r\n", " ");
			value = value.Replace('\n', ' ');
			value = value.Replace('\r', ' ');
			return value;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009AAD File Offset: 0x00007CAD
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void OnBlur()
		{
			this.revealCursor = false;
			this.m_TextSelectingUtility.SelectNone();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009AC4 File Offset: 0x00007CC4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool TouchScreenKeyboardShouldBeUsed()
		{
			RuntimePlatform platform = Application.platform;
			RuntimePlatform runtimePlatform = platform;
			RuntimePlatform runtimePlatform2 = runtimePlatform;
			bool flag;
			if (runtimePlatform2 != RuntimePlatform.Android && runtimePlatform2 != RuntimePlatform.WebGLPlayer)
			{
				flag = TouchScreenKeyboard.isSupported;
			}
			else
			{
				flag = !TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			return flag;
		}

		// Token: 0x0400012D RID: 301
		private TextSelectingUtilities m_TextSelectingUtility;

		// Token: 0x0400012E RID: 302
		internal TextHandle textHandle;

		// Token: 0x0400012F RID: 303
		private int m_CursorIndexSavedState = -1;

		// Token: 0x04000130 RID: 304
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool isCompositionActive;

		// Token: 0x04000131 RID: 305
		private bool m_UpdateImeWindowPosition;

		// Token: 0x04000132 RID: 306
		internal Action OnTextChanged;

		// Token: 0x04000133 RID: 307
		public bool multiline = false;

		// Token: 0x04000134 RID: 308
		private string m_Text;

		// Token: 0x04000135 RID: 309
		private static Dictionary<Event, TextEditOp> s_KeyEditOps;

		// Token: 0x04000136 RID: 310
		private char m_HighSurrogate;
	}
}

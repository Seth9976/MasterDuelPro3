using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal class TextSelectingUtilities
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000A0EC File Offset: 0x000082EC
		public bool hasSelection
		{
			get
			{
				return this.cursorIndex != this.selectIndex;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A0FF File Offset: 0x000082FF
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000A108 File Offset: 0x00008308
		public bool revealCursor
		{
			get
			{
				return this.m_RevealCursor;
			}
			set
			{
				bool flag = this.m_RevealCursor != value;
				if (flag)
				{
					this.m_RevealCursor = value;
					Action onRevealCursorChange = this.OnRevealCursorChange;
					if (onRevealCursorChange != null)
					{
						onRevealCursorChange();
					}
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A141 File Offset: 0x00008341
		private int m_CharacterCount
		{
			get
			{
				return this.textHandle.characterCount;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A150 File Offset: 0x00008350
		private int characterCount
		{
			get
			{
				return (!this.textHandle.useAdvancedText && this.m_CharacterCount > 0 && this.textHandle.textInfo.textElementInfo[this.m_CharacterCount - 1].character == 8203U) ? (this.m_CharacterCount - 1) : this.m_CharacterCount;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A1AC File Offset: 0x000083AC
		private TextElementInfo[] m_TextElementInfos
		{
			get
			{
				return this.textHandle.textInfo.textElementInfo;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A1BE File Offset: 0x000083BE
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A1DC File Offset: 0x000083DC
		public int cursorIndex
		{
			get
			{
				return this.textHandle.IsPlaceholder ? 0 : this.ClampTextIndex(this.m_CursorIndex);
			}
			set
			{
				bool flag = this.m_CursorIndex != value;
				if (flag)
				{
					this.m_CursorIndex = value;
					Action onCursorIndexChange = this.OnCursorIndexChange;
					if (onCursorIndexChange != null)
					{
						onCursorIndexChange();
					}
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A218 File Offset: 0x00008418
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A230 File Offset: 0x00008430
		internal int cursorIndexNoValidation
		{
			get
			{
				return this.m_CursorIndex;
			}
			set
			{
				bool flag = this.m_CursorIndex != value;
				if (flag)
				{
					this.SetCursorIndexWithoutNotify(value);
					Action onCursorIndexChange = this.OnCursorIndexChange;
					if (onCursorIndexChange != null)
					{
						onCursorIndexChange();
					}
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A26A File Offset: 0x0000846A
		internal void SetCursorIndexWithoutNotify(int index)
		{
			this.m_CursorIndex = index;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A274 File Offset: 0x00008474
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A294 File Offset: 0x00008494
		public int selectIndex
		{
			get
			{
				return this.textHandle.IsPlaceholder ? 0 : this.ClampTextIndex(this.m_SelectIndex);
			}
			set
			{
				bool flag = this.m_SelectIndex != value;
				if (flag)
				{
					this.SetSelectIndexWithoutNotify(value);
					Action onSelectIndexChange = this.OnSelectIndexChange;
					if (onSelectIndexChange != null)
					{
						onSelectIndexChange();
					}
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A2D0 File Offset: 0x000084D0
		internal int selectIndexNoValidation
		{
			set
			{
				bool flag = this.m_SelectIndex != value;
				if (flag)
				{
					this.SetSelectIndexWithoutNotify(value);
					Action onSelectIndexChange = this.OnSelectIndexChange;
					if (onSelectIndexChange != null)
					{
						onSelectIndexChange();
					}
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A30A File Offset: 0x0000850A
		internal void SetSelectIndexWithoutNotify(int index)
		{
			this.m_SelectIndex = index;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A314 File Offset: 0x00008514
		public string selectedText
		{
			get
			{
				bool flag = this.cursorIndex == this.selectIndex;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					bool flag2 = this.cursorIndex < this.selectIndex;
					if (flag2)
					{
						text = this.textHandle.Substring(this.cursorIndex, this.selectIndex - this.cursorIndex);
					}
					else
					{
						text = this.textHandle.Substring(this.selectIndex, this.cursorIndex - this.selectIndex);
					}
				}
				return text;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A394 File Offset: 0x00008594
		public TextSelectingUtilities(TextHandle textHandle)
		{
			this.textHandle = textHandle;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A3F0 File Offset: 0x000085F0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers i = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			bool flag = TextSelectingUtilities.s_KeySelectOps.ContainsKey(e);
			bool flag2;
			if (flag)
			{
				TextSelectOp op = TextSelectingUtilities.s_KeySelectOps[e];
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

		// Token: 0x06000215 RID: 533 RVA: 0x0000A45C File Offset: 0x0000865C
		private bool PerformOperation(TextSelectOp operation)
		{
			switch (operation)
			{
			case TextSelectOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextSelectOp.SelectRight:
				this.SelectRight();
				return false;
			case TextSelectOp.SelectUp:
				this.SelectUp();
				return false;
			case TextSelectOp.SelectDown:
				this.SelectDown();
				return false;
			case TextSelectOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextSelectOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextSelectOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextSelectOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextSelectOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextSelectOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextSelectOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextSelectOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextSelectOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextSelectOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextSelectOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextSelectOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextSelectOp.Copy:
				this.Copy();
				return false;
			case TextSelectOp.SelectAll:
				this.SelectAll();
				return false;
			case TextSelectOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation.ToString());
			return false;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A5B4 File Offset: 0x000087B4
		private static void MapKey(string key, TextSelectOp action)
		{
			TextSelectingUtilities.s_KeySelectOps[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A5CC File Offset: 0x000087CC
		private void InitKeyActions()
		{
			bool flag = TextSelectingUtilities.s_KeySelectOps != null;
			if (!flag)
			{
				TextSelectingUtilities.s_KeySelectOps = new Dictionary<Event, TextSelectOp>();
				TextSelectingUtilities.MapKey("#left", TextSelectOp.SelectLeft);
				TextSelectingUtilities.MapKey("#right", TextSelectOp.SelectRight);
				TextSelectingUtilities.MapKey("#up", TextSelectOp.SelectUp);
				TextSelectingUtilities.MapKey("#down", TextSelectOp.SelectDown);
				bool flag2 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
				if (flag2)
				{
					TextSelectingUtilities.MapKey("#home", TextSelectOp.SelectTextStart);
					TextSelectingUtilities.MapKey("#end", TextSelectOp.SelectTextEnd);
					TextSelectingUtilities.MapKey("#^left", TextSelectOp.ExpandSelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#^right", TextSelectOp.ExpandSelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("#^up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#^down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#&left", TextSelectOp.SelectWordLeft);
					TextSelectingUtilities.MapKey("#&right", TextSelectOp.SelectWordRight);
					TextSelectingUtilities.MapKey("#&up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#&down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#%left", TextSelectOp.ExpandSelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#%right", TextSelectOp.ExpandSelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("#%up", TextSelectOp.SelectTextStart);
					TextSelectingUtilities.MapKey("#%down", TextSelectOp.SelectTextEnd);
					TextSelectingUtilities.MapKey("%a", TextSelectOp.SelectAll);
					TextSelectingUtilities.MapKey("%c", TextSelectOp.Copy);
				}
				else
				{
					TextSelectingUtilities.MapKey("#^left", TextSelectOp.SelectToEndOfPreviousWord);
					TextSelectingUtilities.MapKey("#^right", TextSelectOp.SelectToStartOfNextWord);
					TextSelectingUtilities.MapKey("#^up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#^down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#home", TextSelectOp.SelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#end", TextSelectOp.SelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("^a", TextSelectOp.SelectAll);
					TextSelectingUtilities.MapKey("^c", TextSelectOp.Copy);
					TextSelectingUtilities.MapKey("^insert", TextSelectOp.Copy);
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A779 File Offset: 0x00008979
		public void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.iAltCursorPos = -1;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A78C File Offset: 0x0000898C
		public void OnFocus(bool selectAll = true)
		{
			if (selectAll)
			{
				this.SelectAll();
			}
			this.revealCursor = true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A7AE File Offset: 0x000089AE
		public void SelectAll()
		{
			this.cursorIndex = 0;
			this.selectIndex = int.MaxValue;
			this.ClearCursorPos();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A7CC File Offset: 0x000089CC
		public void SelectNone()
		{
			this.selectIndex = this.cursorIndex;
			this.ClearCursorPos();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public void SelectLeft()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex > this.selectIndex;
				if (flag)
				{
					int tmp = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = tmp;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A848 File Offset: 0x00008A48
		public void SelectRight()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex < this.selectIndex;
				if (flag)
				{
					int tmp = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = tmp;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A8AA File Offset: 0x00008AAA
		public void SelectUp()
		{
			this.cursorIndex = this.textHandle.LineUpCharacterPosition(this.cursorIndex);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		public void SelectDown()
		{
			this.cursorIndex = this.textHandle.LineDownCharacterPosition(this.cursorIndex);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public void SelectTextEnd()
		{
			this.cursorIndex = this.characterCount;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public void SelectTextStart()
		{
			this.cursorIndex = 0;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A8FB File Offset: 0x00008AFB
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindStartOfNextWord(this.cursorIndex);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A918 File Offset: 0x00008B18
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A938 File Offset: 0x00008B38
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int cachedPos = this.selectIndex;
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = cachedPos;
				this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = cachedPos;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int cachedPos = this.selectIndex;
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = cachedPos;
				this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = cachedPos;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000AA50 File Offset: 0x00008C50
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000AA6D File Offset: 0x00008C6D
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool wasBehind = this.cursorIndex < this.selectIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorTempIndex = this.cursorIndex;
				this.textHandle.SelectToNextParagraph(ref cursorTempIndex);
				this.cursorIndex = cursorTempIndex;
			}
			else
			{
				bool flag = this.cursorIndex < this.characterCount;
				if (flag)
				{
					this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1);
					bool flag2 = wasBehind && this.cursorIndex > this.selectIndex;
					if (flag2)
					{
						this.cursorIndex = this.selectIndex;
					}
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AB30 File Offset: 0x00008D30
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool wasInFront = this.cursorIndex > this.selectIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorTempIndex = this.cursorIndex;
				this.textHandle.SelectToPreviousParagraph(ref cursorTempIndex);
				this.cursorIndex = cursorTempIndex;
			}
			else
			{
				bool flag = this.cursorIndex > 1;
				if (flag)
				{
					this.cursorIndex = this.textHandle.LastIndexOf('\n', this.cursorIndex - 2) + 1;
					bool flag2 = wasInFront && this.cursorIndex < this.selectIndex;
					if (flag2)
					{
						this.cursorIndex = this.selectIndex;
					}
				}
				else
				{
					this.selectIndex = (this.cursorIndex = 0);
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public void SelectCurrentWord()
		{
			int index = this.cursorIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursor = 0;
				int select = 0;
				this.textHandle.SelectCurrentWord(index, ref cursor, ref select);
				bool flag = this.cursorIndex < this.selectIndex;
				if (flag)
				{
					this.cursorIndex = cursor;
					this.selectIndex = select;
				}
				else
				{
					this.cursorIndex = select;
					this.selectIndex = cursor;
				}
			}
			else
			{
				bool flag2 = this.cursorIndex < this.selectIndex;
				if (flag2)
				{
					this.cursorIndex = this.FindEndOfClassification(index, TextSelectingUtilities.Direction.Backward);
					this.selectIndex = this.FindEndOfClassification(index, TextSelectingUtilities.Direction.Forward);
				}
				else
				{
					this.cursorIndex = this.FindEndOfClassification(index, TextSelectingUtilities.Direction.Forward);
					this.selectIndex = this.FindEndOfClassification(index, TextSelectingUtilities.Direction.Backward);
				}
			}
			this.ClearCursorPos();
			this.m_bJustSelected = true;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int textLen = this.characterCount;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorTempIndex = this.cursorIndex;
				int selectTempIndex = this.selectIndex;
				this.textHandle.SelectCurrentParagraph(ref cursorTempIndex, ref selectTempIndex);
				this.cursorIndex = cursorTempIndex;
				this.selectIndex = selectTempIndex;
			}
			else
			{
				bool flag = this.cursorIndex < textLen;
				if (flag)
				{
					this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex);
				}
				bool flag2 = this.selectIndex != 0;
				if (flag2)
				{
					this.selectIndex = this.textHandle.LastIndexOf('\n', this.selectIndex - 1) + 1;
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public void MoveRight()
		{
			this.ClearCursorPos();
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.cursorIndex = this.selectIndex;
				}
				else
				{
					this.selectIndex = this.cursorIndex;
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public void MoveLeft()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.cursorIndex = this.selectIndex;
				}
			}
			this.ClearCursorPos();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AE74 File Offset: 0x00009074
		public void MoveUp()
		{
			bool flag = this.selectIndex < this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.cursorIndex = (this.selectIndex = this.textHandle.LineUpCharacterPosition(this.cursorIndex));
			bool flag2 = this.cursorIndex <= 0;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AEEC File Offset: 0x000090EC
		public void MoveDown()
		{
			bool flag = this.selectIndex > this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.cursorIndex = (this.selectIndex = this.textHandle.LineDownCharacterPosition(this.cursorIndex));
			bool flag2 = this.cursorIndex == this.characterCount;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AF64 File Offset: 0x00009164
		public void MoveLineStart()
		{
			int p = ((this.selectIndex < this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int i = p;
			while (i-- != 0)
			{
				bool flag = this.m_TextElementInfos[i].character == 10U;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = i + 1);
					return;
				}
			}
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public void MoveLineEnd()
		{
			int p = ((this.selectIndex > this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int i = p;
			int strlen = this.characterCount;
			while (i < strlen)
			{
				bool flag = this.m_TextElementInfos[i].character == 10U;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = i);
					return;
				}
				i++;
			}
			this.selectIndex = (this.cursorIndex = strlen);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B070 File Offset: 0x00009270
		public void MoveGraphicalLineStart()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineStart((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public void MoveGraphicalLineEnd()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineEnd((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public void MoveTextStart()
		{
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B118 File Offset: 0x00009318
		public void MoveTextEnd()
		{
			this.selectIndex = (this.cursorIndex = this.characterCount);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B140 File Offset: 0x00009340
		public void MoveParagraphForward()
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorTempIndex = this.cursorIndex;
				this.textHandle.SelectToNextParagraph(ref cursorTempIndex);
				this.cursorIndex = (this.selectIndex = cursorTempIndex);
			}
			else
			{
				this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
				bool flag = this.cursorIndex < this.characterCount;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1));
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public void MoveParagraphBackward()
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorTempIndex = this.cursorIndex;
				this.textHandle.SelectToPreviousParagraph(ref cursorTempIndex);
				this.cursorIndex = (this.selectIndex = cursorTempIndex);
			}
			else
			{
				this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
				bool flag = this.cursorIndex > 1;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = this.textHandle.LastIndexOf('\n', this.cursorIndex - 2) + 1);
				}
				else
				{
					this.selectIndex = (this.cursorIndex = 0);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B298 File Offset: 0x00009498
		public void MoveWordRight()
		{
			this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				this.cursorIndex = (this.selectIndex = this.FindStartOfNextWord(this.cursorIndex));
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindNextSeperator(this.cursorIndex));
			}
			this.ClearCursorPos();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B31C File Offset: 0x0000951C
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveRight();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindStartOfNextWord(this.cursorIndex));
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B370 File Offset: 0x00009570
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveLeft();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindEndOfPreviousWord(this.cursorIndex));
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B3C4 File Offset: 0x000095C4
		public void MoveWordLeft()
		{
			this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
			}
			else
			{
				this.cursorIndex = this.FindPrevSeperator(this.cursorIndex);
			}
			this.selectIndex = this.cursorIndex;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B43C File Offset: 0x0000963C
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPosStart = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			this.m_DblClickInitPosEnd = ((this.cursorIndex < this.selectIndex) ? this.selectIndex : this.cursorIndex);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B498 File Offset: 0x00009698
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
			}
			else
			{
				int temp = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineStart(this.selectIndex);
				this.selectIndex = temp;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
			}
			else
			{
				int temp = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineEnd(this.selectIndex);
				this.selectIndex = temp;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B558 File Offset: 0x00009758
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.dblClickSnap = snapping;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B564 File Offset: 0x00009764
		protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
		{
			this.selectIndex = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
			bool flag = !shift;
			if (flag)
			{
				this.cursorIndex = this.selectIndex;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public void SelectToPosition(Vector2 cursorPosition)
		{
			bool flag = !this.m_MouseDragSelectsWholeWords;
			if (flag)
			{
				this.cursorIndex = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
			}
			else
			{
				int p = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
				bool flag2 = this.dblClickSnap == TextEditor.DblClickSnapping.WORDS;
				if (flag2)
				{
					bool flag3 = p <= this.m_DblClickInitPosStart;
					if (flag3)
					{
						bool useAdvancedText = this.textHandle.useAdvancedText;
						if (useAdvancedText)
						{
							this.selectIndex = Mathf.Max(this.selectIndex, this.cursorIndex);
							this.cursorIndex = this.textHandle.GetEndOfPreviousWord(p);
						}
						else
						{
							this.cursorIndex = this.FindEndOfClassification(p, TextSelectingUtilities.Direction.Backward);
							this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPosEnd - 1, TextSelectingUtilities.Direction.Forward);
						}
					}
					else
					{
						bool flag4 = p >= this.m_DblClickInitPosEnd;
						if (flag4)
						{
							bool useAdvancedText2 = this.textHandle.useAdvancedText;
							if (useAdvancedText2)
							{
								this.selectIndex = Mathf.Min(this.selectIndex, this.cursorIndex);
								this.cursorIndex = this.textHandle.GetStartOfNextWord(p - 1);
							}
							else
							{
								this.cursorIndex = this.FindEndOfClassification(p - 1, TextSelectingUtilities.Direction.Forward);
								this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPosStart + 1, TextSelectingUtilities.Direction.Backward);
							}
						}
						else
						{
							this.cursorIndex = this.m_DblClickInitPosStart;
							this.selectIndex = this.m_DblClickInitPosEnd;
						}
					}
				}
				else
				{
					bool flag5 = (!this.textHandle.useAdvancedText && p <= this.m_DblClickInitPosStart) || (this.textHandle.useAdvancedText && p < this.m_DblClickInitPosStart);
					if (flag5)
					{
						bool useAdvancedText3 = this.textHandle.useAdvancedText;
						if (useAdvancedText3)
						{
							int selectTempIndex = p;
							this.textHandle.SelectToStartOfParagraph(ref selectTempIndex);
							this.selectIndex = selectTempIndex;
						}
						else
						{
							bool flag6 = p > 0;
							if (flag6)
							{
								this.cursorIndex = this.textHandle.LastIndexOf('\n', Mathf.Max(0, p - 1)) + 1;
							}
							else
							{
								this.cursorIndex = 0;
							}
							this.selectIndex = this.textHandle.LastIndexOf('\n', Mathf.Min(this.characterCount - 1, this.m_DblClickInitPosEnd + 1));
						}
					}
					else
					{
						bool flag7 = p >= this.m_DblClickInitPosEnd;
						if (flag7)
						{
							bool useAdvancedText4 = this.textHandle.useAdvancedText;
							if (useAdvancedText4)
							{
								int cursorTempIndex = p;
								this.textHandle.SelectToEndOfParagraph(ref cursorTempIndex);
								this.cursorIndex = cursorTempIndex;
							}
							else
							{
								bool flag8 = p < this.characterCount;
								if (flag8)
								{
									this.cursorIndex = this.IndexOfEndOfLine(p);
								}
								else
								{
									this.cursorIndex = this.characterCount;
								}
								this.selectIndex = this.textHandle.LastIndexOf('\n', Mathf.Max(0, this.m_DblClickInitPosEnd - 2)) + 1;
							}
						}
						else
						{
							bool useAdvancedText5 = this.textHandle.useAdvancedText;
							if (useAdvancedText5)
							{
								this.cursorIndex = this.m_DblClickInitPosEnd;
								this.selectIndex = this.m_DblClickInitPosStart;
							}
							else
							{
								this.cursorIndex = this.m_DblClickInitPosStart;
								this.selectIndex = this.m_DblClickInitPosEnd;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		private int FindNextSeperator(int startPos)
		{
			int textLen = this.characterCount;
			while (startPos < textLen && this.ClassifyChar(startPos) > TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			while (startPos < textLen && this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			return startPos;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B92C File Offset: 0x00009B2C
		private int FindPrevSeperator(int startPos)
		{
			startPos = this.PreviousCodePointIndex(startPos);
			while (startPos > 0 && this.ClassifyChar(startPos) > TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.PreviousCodePointIndex(startPos);
			}
			bool flag = startPos == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				while (startPos > 0 && this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike)
				{
					startPos = this.PreviousCodePointIndex(startPos);
				}
				bool flag2 = this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike;
				if (flag2)
				{
					num = startPos;
				}
				else
				{
					num = this.NextCodePointIndex(startPos);
				}
			}
			return num;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public int FindStartOfNextWord(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetStartOfNextWord(p);
			}
			else
			{
				int textLen = this.characterCount;
				bool flag = p == textLen;
				if (flag)
				{
					num = p;
				}
				else
				{
					TextSelectingUtilities.CharacterType t = this.ClassifyChar(p);
					bool flag2 = t != TextSelectingUtilities.CharacterType.WhiteSpace;
					if (flag2)
					{
						p = this.NextCodePointIndex(p);
						while (p < textLen && this.ClassifyChar(p) == t)
						{
							p = this.NextCodePointIndex(p);
						}
					}
					else
					{
						bool flag3 = this.m_TextElementInfos[p].character == 9U || this.m_TextElementInfos[p].character == 10U;
						if (flag3)
						{
							return this.NextCodePointIndex(p);
						}
					}
					bool flag4 = p == textLen;
					if (flag4)
					{
						num = p;
					}
					else
					{
						bool flag5 = this.m_TextElementInfos[p].character == 32U;
						if (flag5)
						{
							while (p < textLen && this.ClassifyChar(p) == TextSelectingUtilities.CharacterType.WhiteSpace)
							{
								p = this.NextCodePointIndex(p);
							}
						}
						else
						{
							bool flag6 = this.m_TextElementInfos[p].character == 9U || this.m_TextElementInfos[p].character == 10U;
							if (flag6)
							{
								return p;
							}
						}
						num = p;
					}
				}
			}
			return num;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public int FindEndOfPreviousWord(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetEndOfPreviousWord(p);
			}
			else
			{
				bool flag = p == 0;
				if (flag)
				{
					num = p;
				}
				else
				{
					p = this.PreviousCodePointIndex(p);
					while (p > 0 && this.m_TextElementInfos[p].character == 32U)
					{
						p = this.PreviousCodePointIndex(p);
					}
					TextSelectingUtilities.CharacterType t = this.ClassifyChar(p);
					bool flag2 = t != TextSelectingUtilities.CharacterType.WhiteSpace;
					if (flag2)
					{
						while (p > 0 && this.ClassifyChar(this.PreviousCodePointIndex(p)) == t)
						{
							p = this.PreviousCodePointIndex(p);
						}
					}
					num = p;
				}
			}
			return num;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		private int FindEndOfClassification(int p, TextSelectingUtilities.Direction dir)
		{
			bool flag = this.characterCount == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = p >= this.characterCount;
				if (flag2)
				{
					p = this.characterCount - 1;
				}
				TextSelectingUtilities.CharacterType t = this.ClassifyChar(p);
				bool flag3 = t == TextSelectingUtilities.CharacterType.NewLine;
				if (flag3)
				{
					num = p;
				}
				else
				{
					for (;;)
					{
						if (dir != TextSelectingUtilities.Direction.Forward)
						{
							if (dir == TextSelectingUtilities.Direction.Backward)
							{
								p = this.PreviousCodePointIndex(p);
								bool flag4 = p == 0;
								if (flag4)
								{
									break;
								}
							}
						}
						else
						{
							p = this.NextCodePointIndex(p);
							bool flag5 = p >= this.characterCount;
							if (flag5)
							{
								goto Block_8;
							}
						}
						if (this.ClassifyChar(p) != t)
						{
							goto Block_9;
						}
					}
					return (this.ClassifyChar(0) == t) ? 0 : this.NextCodePointIndex(0);
					Block_8:
					return this.characterCount;
					Block_9:
					bool flag6 = dir == TextSelectingUtilities.Direction.Forward;
					if (flag6)
					{
						num = p;
					}
					else
					{
						num = this.NextCodePointIndex(p);
					}
				}
			}
			return num;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000BCAC File Offset: 0x00009EAC
		private int ClampTextIndex(int index)
		{
			return Mathf.Clamp(index, 0, this.characterCount);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000BCCC File Offset: 0x00009ECC
		private int IndexOfEndOfLine(int startIndex)
		{
			int index = this.textHandle.IndexOf('\n', startIndex);
			return (index != -1) ? index : this.characterCount;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public int PreviousCodePointIndex(int index)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.PreviousCodePointIndex(index);
			}
			else
			{
				bool flag = index > 0;
				if (flag)
				{
					index--;
				}
				num = index;
			}
			return num;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public int NextCodePointIndex(int index)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.NextCodePointIndex(index);
			}
			else
			{
				bool flag = index < this.characterCount;
				if (flag)
				{
					index++;
				}
				num = index;
			}
			return num;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000BD80 File Offset: 0x00009F80
		private int GetGraphicalLineStart(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetFirstCharacterIndexOnLine(p);
			}
			else
			{
				Vector2 point = this.textHandle.GetCursorPositionFromStringIndexUsingLineHeight(p, false, true);
				point.y -= 1f / GUIUtility.pixelsPerPoint;
				point.x = 0f;
				num = this.textHandle.GetCursorIndexFromPosition(point, true);
			}
			return num;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		private int GetGraphicalLineEnd(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetLastCharacterIndexOnLine(p);
			}
			else
			{
				Vector2 point = this.textHandle.GetCursorPositionFromStringIndexUsingLineHeight(p, false, true);
				point.y -= 1f / GUIUtility.pixelsPerPoint;
				point.x += 5000f;
				num = this.textHandle.GetCursorIndexFromPosition(point, true);
			}
			return num;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000BE64 File Offset: 0x0000A064
		public void Copy()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (!flag)
			{
				GUIUtility.systemCopyBuffer = this.selectedText;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000BE94 File Offset: 0x0000A094
		private TextSelectingUtilities.CharacterType ClassifyChar(int index)
		{
			char c = (char)this.m_TextElementInfos[index].character;
			bool flag = c == '\n';
			TextSelectingUtilities.CharacterType characterType;
			if (flag)
			{
				characterType = TextSelectingUtilities.CharacterType.NewLine;
			}
			else
			{
				bool flag2 = char.IsWhiteSpace(c);
				if (flag2)
				{
					characterType = TextSelectingUtilities.CharacterType.WhiteSpace;
				}
				else
				{
					bool flag3 = char.IsLetterOrDigit(c) || this.m_TextElementInfos[index].character == 39U;
					if (flag3)
					{
						characterType = TextSelectingUtilities.CharacterType.LetterLike;
					}
					else
					{
						characterType = TextSelectingUtilities.CharacterType.Symbol;
					}
				}
			}
			return characterType;
		}

		// Token: 0x04000149 RID: 329
		public TextEditor.DblClickSnapping dblClickSnap = TextEditor.DblClickSnapping.WORDS;

		// Token: 0x0400014A RID: 330
		public int iAltCursorPos = -1;

		// Token: 0x0400014B RID: 331
		public bool hasHorizontalCursorPos = false;

		// Token: 0x0400014C RID: 332
		private bool m_bJustSelected = false;

		// Token: 0x0400014D RID: 333
		private bool m_MouseDragSelectsWholeWords = false;

		// Token: 0x0400014E RID: 334
		private int m_DblClickInitPosStart = 0;

		// Token: 0x0400014F RID: 335
		private int m_DblClickInitPosEnd = 0;

		// Token: 0x04000150 RID: 336
		public TextHandle textHandle;

		// Token: 0x04000151 RID: 337
		private const int kMoveDownHeight = 5;

		// Token: 0x04000152 RID: 338
		private const char kNewLineChar = '\n';

		// Token: 0x04000153 RID: 339
		private bool m_RevealCursor;

		// Token: 0x04000154 RID: 340
		private int m_CursorIndex = 0;

		// Token: 0x04000155 RID: 341
		internal int m_SelectIndex = 0;

		// Token: 0x04000156 RID: 342
		private static Dictionary<Event, TextSelectOp> s_KeySelectOps;

		// Token: 0x04000157 RID: 343
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnCursorIndexChange;

		// Token: 0x04000158 RID: 344
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnSelectIndexChange;

		// Token: 0x04000159 RID: 345
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnRevealCursorChange;

		// Token: 0x02000030 RID: 48
		private enum CharacterType
		{
			// Token: 0x0400015B RID: 347
			LetterLike,
			// Token: 0x0400015C RID: 348
			Symbol,
			// Token: 0x0400015D RID: 349
			Symbol2,
			// Token: 0x0400015E RID: 350
			WhiteSpace,
			// Token: 0x0400015F RID: 351
			NewLine
		}

		// Token: 0x02000031 RID: 49
		private enum Direction
		{
			// Token: 0x04000161 RID: 353
			Forward,
			// Token: 0x04000162 RID: 354
			Backward
		}
	}
}

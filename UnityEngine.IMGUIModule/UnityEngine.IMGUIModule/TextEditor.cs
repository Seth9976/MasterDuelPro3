using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	public class TextEditor
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009AFE File Offset: 0x00007CFE
		public bool showCursor
		{
			get
			{
				return this.m_TextSelecting.revealCursor;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009B0B File Offset: 0x00007D0B
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00009B18 File Offset: 0x00007D18
		public string text
		{
			get
			{
				return this.m_TextEditing.text;
			}
			set
			{
				string newValue = value ?? "";
				bool flag = this.m_TextEditing.text == newValue;
				if (!flag)
				{
					this.m_TextEditing.SetTextWithoutNotify(newValue);
					this.m_Content.SetTextWithoutNotify(newValue);
					this.textWithWhitespace = newValue;
					this.UpdateTextHandle();
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009B71 File Offset: 0x00007D71
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009B8D File Offset: 0x00007D8D
		internal string textWithWhitespace
		{
			get
			{
				return string.IsNullOrEmpty(this.m_TextWithWhitespace) ? GUIContent.k_ZeroWidthSpace : this.m_TextWithWhitespace;
			}
			set
			{
				this.m_TextWithWhitespace = value + GUIContent.k_ZeroWidthSpace;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009BA0 File Offset: 0x00007DA0
		public Rect position { get; }

		// Token: 0x060001FC RID: 508 RVA: 0x00009BA8 File Offset: 0x00007DA8
		[RequiredByNativeCode]
		public TextEditor()
		{
			GUIStyle style = GUIStyle.none;
			this.m_TextHandle = IMGUITextHandle.GetTextHandle(style, this.position, this.textWithWhitespace, Color.white);
			this.m_TextHandle.AddTextInfoToPermanentCache();
			this.m_TextSelecting = new TextSelectingUtilities(this.m_TextHandle);
			this.m_TextEditing = new TextEditingUtilities(this.m_TextSelecting, this.m_TextHandle, this.m_Content.text);
			this.m_Content.OnTextChanged += this.OnContentTextChangedHandle;
			TextEditingUtilities textEditing = this.m_TextEditing;
			textEditing.OnTextChanged = (Action)Delegate.Combine(textEditing.OnTextChanged, new Action(this.OnTextChangedHandle));
			this.style = style;
			TextSelectingUtilities textSelecting = this.m_TextSelecting;
			textSelecting.OnCursorIndexChange = (Action)Delegate.Combine(textSelecting.OnCursorIndexChange, new Action(this.OnCursorIndexChange));
			TextSelectingUtilities textSelecting2 = this.m_TextSelecting;
			textSelecting2.OnSelectIndexChange = (Action)Delegate.Combine(textSelecting2.OnSelectIndexChange, new Action(this.OnSelectIndexChange));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009CF7 File Offset: 0x00007EF7
		private void OnTextChangedHandle()
		{
			this.m_Content.SetTextWithoutNotify(this.text);
			this.textWithWhitespace = this.text;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D19 File Offset: 0x00007F19
		private void OnContentTextChangedHandle()
		{
			this.text = this.m_Content.text;
			this.textWithWhitespace = this.text;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal void UpdateTextHandle()
		{
			this.m_TextHandle = IMGUITextHandle.GetTextHandle(this.style, this.style.padding.Remove(this.position), this.textWithWhitespace, Color.white);
			this.m_TextHandle.AddTextInfoToPermanentCache();
			this.m_TextEditing.textHandle = this.m_TextHandle;
			this.m_TextSelecting.textHandle = this.m_TextHandle;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009DB0 File Offset: 0x00007FB0
		[VisibleToOtherModules]
		internal void UpdateScrollOffset()
		{
			float newXOffset = this.scrollOffset.x;
			float newYOffset = this.scrollOffset.y;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, this.m_TextSelecting.cursorIndexNoValidation);
			Rect viewRect = this.style.padding.Remove(this.position);
			Vector2 localGraphicalCursorPos = this.graphicalCursorPos;
			localGraphicalCursorPos.x -= (float)this.style.padding.left;
			localGraphicalCursorPos.y -= (float)this.style.padding.top;
			Vector2 contentSize = (this.previousContentSize = this.style.GetPreferredSize(this.m_Content.textWithWhitespace, this.position));
			bool flag = contentSize.x < viewRect.width;
			if (flag)
			{
				newXOffset = 0f;
			}
			else
			{
				bool showCursor = this.showCursor;
				if (showCursor)
				{
					bool flag2 = localGraphicalCursorPos.x > this.scrollOffset.x + viewRect.width - 1f;
					if (flag2)
					{
						newXOffset = localGraphicalCursorPos.x - viewRect.width + 1f;
					}
					else
					{
						bool flag3 = localGraphicalCursorPos.x < this.scrollOffset.x;
						if (flag3)
						{
							newXOffset = Mathf.Max(localGraphicalCursorPos.x, 0f);
						}
						else
						{
							bool flag4 = this.previousContentSize.x != contentSize.x && localGraphicalCursorPos.x < viewRect.x + Math.Abs(contentSize.x + 1f - viewRect.width);
							if (flag4)
							{
								newXOffset = Mathf.Max(viewRect.width - localGraphicalCursorPos.x, 0f);
							}
						}
					}
				}
			}
			bool flag5 = Mathf.Round(contentSize.y) <= Mathf.Round(viewRect.height) || viewRect.height == 0f;
			if (flag5)
			{
				newYOffset = 0f;
			}
			else
			{
				bool flag6 = this.showCursor && Math.Abs(this.lastCursorPos.y - localGraphicalCursorPos.y) > 0.05f;
				if (flag6)
				{
					bool flag7 = localGraphicalCursorPos.y + this.style.lineHeight > this.scrollOffset.y + viewRect.height;
					if (flag7)
					{
						newYOffset = localGraphicalCursorPos.y - viewRect.height + this.style.lineHeight;
					}
					else
					{
						bool flag8 = localGraphicalCursorPos.y < this.style.lineHeight + this.scrollOffset.y;
						if (flag8)
						{
							newYOffset = localGraphicalCursorPos.y - this.style.lineHeight;
						}
					}
				}
			}
			bool flag9 = this.scrollOffset.x != newXOffset || this.scrollOffset.y != newYOffset;
			if (flag9)
			{
				this.scrollOffset = new Vector2(newXOffset, (newYOffset < 0f) ? 0f : newYOffset);
			}
			this.lastCursorPos = localGraphicalCursorPos;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A0E2 File Offset: 0x000082E2
		internal virtual void OnCursorIndexChange()
		{
			this.UpdateScrollOffset();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A0E2 File Offset: 0x000082E2
		internal virtual void OnSelectIndexChange()
		{
			this.UpdateScrollOffset();
		}

		// Token: 0x04000137 RID: 311
		private readonly GUIContent m_Content = new GUIContent();

		// Token: 0x04000138 RID: 312
		private TextSelectingUtilities m_TextSelecting;

		// Token: 0x04000139 RID: 313
		internal TextEditingUtilities m_TextEditing;

		// Token: 0x0400013A RID: 314
		internal IMGUITextHandle m_TextHandle;

		// Token: 0x0400013B RID: 315
		public TouchScreenKeyboard keyboardOnScreen = null;

		// Token: 0x0400013C RID: 316
		public int controlID = 0;

		// Token: 0x0400013D RID: 317
		public GUIStyle style;

		// Token: 0x0400013E RID: 318
		[Obsolete("'hasHorizontalCursorPos' has been deprecated. Changes to this member will not be observed. Use 'hasHorizontalCursor' instead.", true)]
		public bool hasHorizontalCursorPos = false;

		// Token: 0x0400013F RID: 319
		public bool isPasswordField = false;

		// Token: 0x04000140 RID: 320
		public Vector2 scrollOffset;

		// Token: 0x04000141 RID: 321
		private string m_TextWithWhitespace;

		// Token: 0x04000143 RID: 323
		public Vector2 graphicalCursorPos;

		// Token: 0x04000144 RID: 324
		private Vector2 lastCursorPos = Vector2.zero;

		// Token: 0x04000145 RID: 325
		private Vector2 previousContentSize = Vector2.zero;

		// Token: 0x0200002E RID: 46
		public enum DblClickSnapping : byte
		{
			// Token: 0x04000147 RID: 327
			WORDS,
			// Token: 0x04000148 RID: 328
			PARAGRAPHS
		}
	}
}

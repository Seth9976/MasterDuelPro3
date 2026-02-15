using System;
using System.Collections;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200019E RID: 414
	internal class UndoManager
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0004F356 File Offset: 0x0004D556
		internal UndoManager(Document document)
		{
			this.document = document;
			this.undo_actions = new Stack(50);
			this.redo_actions = new Stack(50);
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0004F37F File Offset: 0x0004D57F
		internal bool CanUndo
		{
			get
			{
				return this.undo_actions.Count > 0;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004F390 File Offset: 0x0004D590
		internal bool Undo()
		{
			bool flag = false;
			if (this.undo_actions.Count == 0)
			{
				return false;
			}
			this.locked = true;
			do
			{
				UndoManager.Action action = (UndoManager.Action)this.undo_actions.Pop();
				this.redo_actions.Push(action);
				switch (action.type)
				{
				case UndoManager.ActionType.Typing:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.document.DeleteMultiline(line, action.pos, ((StringBuilder)action.data).Length);
					this.document.PositionCaret(line, action.pos);
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					flag = true;
					break;
				}
				case UndoManager.ActionType.InsertString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.document.DeleteMultiline(line, action.pos, ((string)action.data).Length + 1);
					this.document.PositionCaret(line, action.pos);
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.DeleteString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.Insert(line, action.pos, (Line)action.data, true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.UserActionBegin:
					flag = true;
					break;
				}
			}
			while (!flag && this.undo_actions.Count > 0);
			this.locked = false;
			return true;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004F53C File Offset: 0x0004D73C
		public void BeginUserAction(string name)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.UserActionBegin;
			action.data = name;
			this.undo_actions.Push(action);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004F580 File Offset: 0x0004D780
		public void EndUserAction()
		{
			if (this.locked)
			{
				return;
			}
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.UserActionEnd;
			this.undo_actions.Push(action);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
		public void RecordDeleteString(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.DeleteString;
			action.line_no = start_line.line_no;
			action.pos = start_pos;
			action.data = this.Duplicate(start_line, start_pos, end_line, end_pos);
			this.undo_actions.Push(action);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004F610 File Offset: 0x0004D810
		public void RecordInsertString(Line line, int pos, string str)
		{
			if (this.locked || str.Length == 0)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.InsertString;
			action.data = str;
			action.line_no = line.line_no;
			action.pos = pos;
			this.undo_actions.Push(action);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004F66C File Offset: 0x0004D86C
		public void RecordTyping(Line line, int pos, char ch)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = null;
			if (this.undo_actions.Count > 0)
			{
				action = (UndoManager.Action)this.undo_actions.Peek();
			}
			if (action == null || action.type != UndoManager.ActionType.Typing)
			{
				action = new UndoManager.Action();
				action.type = UndoManager.ActionType.Typing;
				action.data = new StringBuilder();
				action.line_no = line.line_no;
				action.pos = pos;
				this.undo_actions.Push(action);
			}
			((StringBuilder)action.data).Append(ch);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004F704 File Offset: 0x0004D904
		public Line Duplicate(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			Line line = new Line(start_line.document, start_line.ending);
			Line line2 = line;
			for (int i = start_line.line_no; i <= end_line.line_no; i++)
			{
				Line line3 = this.document.GetLine(i);
				int num;
				if (start_line.line_no == i)
				{
					num = start_pos;
				}
				else
				{
					num = 0;
				}
				int num2;
				if (end_line.line_no == i)
				{
					num2 = end_pos;
				}
				else
				{
					num2 = line3.text.Length;
				}
				if (end_pos != 0)
				{
					line.text = new StringBuilder(line3.text.ToString(num, num2 - num));
					LineTag lineTag = line3.FindTag(num + 1);
					while (lineTag != null && lineTag.Start <= num2)
					{
						int num3;
						if (lineTag.Start <= num && num < lineTag.Start + lineTag.Length)
						{
							num3 = num;
						}
						else
						{
							num3 = lineTag.Start;
						}
						LineTag lineTag2 = new LineTag(line, num3 - num + 1);
						lineTag2.CopyFormattingFrom(lineTag);
						lineTag = lineTag.Next;
						if (line.tags == null)
						{
							line.tags = lineTag2;
						}
						else
						{
							LineTag lineTag3 = line.tags;
							while (lineTag3.Next != null)
							{
								lineTag3 = lineTag3.Next;
							}
							lineTag3.Next = lineTag2;
							lineTag2.Previous = lineTag3;
						}
					}
					if (i + 1 <= end_line.line_no)
					{
						line.ending = line3.ending;
						line.right = new Line(start_line.document, start_line.ending);
						line.right.left = line;
						line = line.right;
					}
				}
			}
			return line2;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004F898 File Offset: 0x0004DA98
		internal void Insert(Line line, int pos, Line insert, bool select)
		{
			LineTag lineTag;
			int num2;
			if (insert.right != null)
			{
				Line line2 = line;
				int num = 1;
				Line line3 = insert;
				while (line3 != null)
				{
					if (line3 == insert)
					{
						this.document.Split(line.line_no, pos);
						lineTag = line.tags;
						if (lineTag != null && lineTag.Length != 0)
						{
							while (lineTag.Next != null)
							{
								lineTag = lineTag.Next;
							}
							num2 = lineTag.Start + lineTag.Length - 1;
							lineTag.Next = line3.tags;
							lineTag.Next.Previous = lineTag;
							lineTag = lineTag.Next;
						}
						else
						{
							num2 = 0;
							line.tags = line3.tags;
							line.tags.Previous = null;
							lineTag = line.tags;
						}
						line.ending = line3.ending;
					}
					else
					{
						this.document.Split(line.line_no, 0);
						num2 = 0;
						line.tags = line3.tags;
						line.tags.Previous = null;
						line.ending = line3.ending;
						lineTag = line.tags;
					}
					while (lineTag != null)
					{
						lineTag.Start += num2 - 1;
						lineTag.Line = line;
						lineTag = lineTag.Next;
					}
					line.text.Insert(num2, line3.text.ToString());
					line.Grow(line.text.Length);
					line.recalc = true;
					line = this.document.GetLine(line.line_no + 1);
					if (line3.right == null && line3.tags.Length != 0)
					{
						this.document.Combine(line.line_no - 1, line.line_no);
					}
					line3 = line3.right;
					num++;
				}
				this.document.UpdateView(line2, num, pos);
				return;
			}
			this.document.Split(line, pos);
			if (insert.tags == null)
			{
				return;
			}
			lineTag = line.tags;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			num2 = lineTag.Start + lineTag.Length - 1;
			lineTag.Next = insert.tags;
			line.text.Insert(num2, insert.text.ToString());
			for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Start += num2;
				lineTag.Line = line;
			}
			this.document.Combine(line.line_no, line.line_no + 1);
			if (select)
			{
				this.document.SetSelectionStart(line, pos, false);
				this.document.SetSelectionEnd(line, pos + insert.text.Length, false);
			}
			this.document.UpdateView(line, pos);
		}

		// Token: 0x04000ADE RID: 2782
		private Document document;

		// Token: 0x04000ADF RID: 2783
		private Stack undo_actions;

		// Token: 0x04000AE0 RID: 2784
		private Stack redo_actions;

		// Token: 0x04000AE1 RID: 2785
		private bool locked;

		// Token: 0x0200019F RID: 415
		internal enum ActionType
		{
			// Token: 0x04000AE3 RID: 2787
			Typing,
			// Token: 0x04000AE4 RID: 2788
			InsertString,
			// Token: 0x04000AE5 RID: 2789
			DeleteString,
			// Token: 0x04000AE6 RID: 2790
			UserActionBegin,
			// Token: 0x04000AE7 RID: 2791
			UserActionEnd
		}

		// Token: 0x020001A0 RID: 416
		internal class Action
		{
			// Token: 0x04000AE8 RID: 2792
			internal UndoManager.ActionType type;

			// Token: 0x04000AE9 RID: 2793
			internal int line_no;

			// Token: 0x04000AEA RID: 2794
			internal int pos;

			// Token: 0x04000AEB RID: 2795
			internal object data;
		}
	}
}

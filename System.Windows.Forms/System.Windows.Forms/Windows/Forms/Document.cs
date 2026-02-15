using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	// Token: 0x0200019B RID: 411
	internal class Document : ICloneable, IEnumerable
	{
		// Token: 0x06000FAF RID: 4015 RVA: 0x00049190 File Offset: 0x00047390
		internal Document(TextBoxBase owner)
		{
			this.lines = 0;
			this.owner = owner;
			this.multiline = true;
			this.password_char = "";
			this.calc_pass = false;
			this.recalc_pending = false;
			this.sentinel = new Line(this, LineEnding.None);
			this.sentinel.color = LineColor.Black;
			this.document = this.sentinel;
			owner.HandleCreated += this.owner_HandleCreated;
			owner.VisibleChanged += this.owner_VisibleChanged;
			this.Add(1, string.Empty, owner.Font, owner.ForeColor, LineEnding.None);
			this.undo = new UndoManager(this);
			this.selection_visible = false;
			this.selection_start.line = this.document;
			this.selection_start.pos = 0;
			this.selection_start.tag = this.selection_start.line.tags;
			this.selection_end.line = this.document;
			this.selection_end.pos = 0;
			this.selection_end.tag = this.selection_end.line.tags;
			this.selection_anchor.line = this.document;
			this.selection_anchor.pos = 0;
			this.selection_anchor.tag = this.selection_anchor.line.tags;
			this.caret.line = this.document;
			this.caret.pos = 0;
			this.caret.tag = this.caret.line.tags;
			this.viewport_x = 0;
			this.viewport_y = 0;
			this.offset_x = 0;
			this.offset_y = 0;
			this.crlf_size = 2;
			this.document_id = this.random.Next();
			Document.string_format.Trimming = StringTrimming.None;
			Document.string_format.FormatFlags = StringFormatFlags.DisplayFormatControl;
			this.UpdateMargins();
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000493A5 File Offset: 0x000475A5
		internal Line Root
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000493AD File Offset: 0x000475AD
		internal int Lines
		{
			get
			{
				return this.lines;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000493B5 File Offset: 0x000475B5
		internal Line CaretLine
		{
			get
			{
				return this.caret.line;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000493C2 File Offset: 0x000475C2
		internal int CaretPosition
		{
			get
			{
				return this.caret.pos;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000493D0 File Offset: 0x000475D0
		internal Point Caret
		{
			get
			{
				return new Point((int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X, this.caret.line.Y);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00049425 File Offset: 0x00047625
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00049432 File Offset: 0x00047632
		internal LineTag CaretTag
		{
			get
			{
				return this.caret.tag;
			}
			set
			{
				this.caret.tag = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00049440 File Offset: 0x00047640
		internal bool EnableLinks
		{
			get
			{
				return this.enable_links;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00049448 File Offset: 0x00047648
		internal string PasswordChar
		{
			set
			{
				this.password_char = value;
				this.PasswordCache.Length = 0;
				if (this.password_char.Length != 0 && this.password_char[0] != '\0')
				{
					this.calc_pass = true;
					return;
				}
				this.calc_pass = false;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00049487 File Offset: 0x00047687
		private StringBuilder PasswordCache
		{
			get
			{
				if (this.password_cache == null)
				{
					this.password_cache = new StringBuilder();
				}
				return this.password_cache;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x000494A2 File Offset: 0x000476A2
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x000494AA File Offset: 0x000476AA
		internal int ViewPortX
		{
			get
			{
				return this.viewport_x;
			}
			set
			{
				this.viewport_x = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x000494B3 File Offset: 0x000476B3
		internal int Length
		{
			get
			{
				return this.char_count + this.lines - 1;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000494C4 File Offset: 0x000476C4
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x000494CC File Offset: 0x000476CC
		private int CharCount
		{
			get
			{
				return this.char_count;
			}
			set
			{
				this.char_count = value;
				if (this.LengthChanged != null)
				{
					this.LengthChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000494EE File Offset: 0x000476EE
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000494F6 File Offset: 0x000476F6
		internal int ViewPortY
		{
			get
			{
				return this.viewport_y;
			}
			set
			{
				this.viewport_y = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000494FF File Offset: 0x000476FF
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00049507 File Offset: 0x00047707
		internal int OffsetX
		{
			get
			{
				return this.offset_x;
			}
			set
			{
				this.offset_x = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00049510 File Offset: 0x00047710
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00049518 File Offset: 0x00047718
		internal int ViewPortWidth
		{
			get
			{
				return this.viewport_width;
			}
			set
			{
				this.viewport_width = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00049521 File Offset: 0x00047721
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00049529 File Offset: 0x00047729
		internal int ViewPortHeight
		{
			get
			{
				return this.viewport_height;
			}
			set
			{
				this.viewport_height = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00049532 File Offset: 0x00047732
		internal int Width
		{
			get
			{
				return this.document_x;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0004953A File Offset: 0x0004773A
		internal int Height
		{
			get
			{
				return this.document_y;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00049542 File Offset: 0x00047742
		internal bool Wrap
		{
			set
			{
				this.wrap = value;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004954C File Offset: 0x0004774C
		internal void UpdateMargins()
		{
			switch (this.owner.actual_border_style)
			{
			case BorderStyle.None:
				this.left_margin = 0;
				this.top_margin = 0;
				this.right_margin = 1;
				return;
			case BorderStyle.FixedSingle:
				this.left_margin = 2;
				this.top_margin = 2;
				this.right_margin = 3;
				return;
			case BorderStyle.Fixed3D:
				this.left_margin = 1;
				this.top_margin = 1;
				this.right_margin = 2;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000495B9 File Offset: 0x000477B9
		internal void SuspendRecalc()
		{
			if (this.recalc_suspended == 0)
			{
				this.recalc_start = int.MaxValue;
				this.recalc_end = int.MinValue;
			}
			this.recalc_suspended++;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000495E8 File Offset: 0x000477E8
		internal void ResumeRecalc(bool immediate_update)
		{
			if (this.recalc_suspended > 0)
			{
				this.recalc_suspended--;
			}
			if (this.recalc_suspended == 0 && (immediate_update || this.recalc_pending) && (this.recalc_start != 2147483647 || this.recalc_end != -2147483648))
			{
				this.RecalculateDocument(this.owner.CreateGraphicsInternal(), this.recalc_start, this.recalc_end, this.recalc_optimize);
				this.recalc_pending = false;
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00049664 File Offset: 0x00047864
		internal void SuspendUpdate()
		{
			this.update_suspended++;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00049674 File Offset: 0x00047874
		internal void ResumeUpdate(bool immediate_update)
		{
			if (this.update_suspended > 0)
			{
				this.update_suspended--;
			}
			if (immediate_update && this.update_suspended == 0 && this.update_pending)
			{
				this.UpdateView(this.GetLine(this.update_start), 0);
				this.update_pending = false;
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000496C8 File Offset: 0x000478C8
		private void SetSelectionVisible(bool value)
		{
			bool flag = this.selection_visible;
			this.selection_visible = value;
			if (this.owner.IsHandleCreated && !this.owner.show_caret_w_selection)
			{
				XplatUI.CaretVisible(this.owner.Handle, !this.selection_visible);
			}
			if (this.UIASelectionChanged != null && (this.selection_visible || flag))
			{
				this.UIASelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004973C File Offset: 0x0004793C
		private void DecrementLines(int line_no)
		{
			for (int i = line_no; i <= this.lines; i++)
			{
				this.GetLine(i).line_no--;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00049770 File Offset: 0x00047970
		private void IncrementLines(int line_no)
		{
			for (int i = this.lines; i >= line_no; i--)
			{
				this.GetLine(i).line_no++;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000497A4 File Offset: 0x000479A4
		private void RebalanceAfterAdd(Line line1)
		{
			while (line1 != this.document && line1.parent.color == LineColor.Red)
			{
				if (line1.parent == line1.parent.parent.left)
				{
					Line line2 = line1.parent.parent.right;
					if (line2 != null && line2.color == LineColor.Red)
					{
						line1.parent.color = LineColor.Black;
						line2.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						line1 = line1.parent.parent;
					}
					else
					{
						if (line1 == line1.parent.right)
						{
							line1 = line1.parent;
							this.RotateLeft(line1);
						}
						line1.parent.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						this.RotateRight(line1.parent.parent);
					}
				}
				else
				{
					Line line2 = line1.parent.parent.left;
					if (line2 != null && line2.color == LineColor.Red)
					{
						line1.parent.color = LineColor.Black;
						line2.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						line1 = line1.parent.parent;
					}
					else
					{
						if (line1 == line1.parent.left)
						{
							line1 = line1.parent;
							this.RotateRight(line1);
						}
						line1.parent.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						this.RotateLeft(line1.parent.parent);
					}
				}
			}
			this.document.color = LineColor.Black;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00049934 File Offset: 0x00047B34
		private void RebalanceAfterDelete(Line line1)
		{
			while (line1 != this.document && line1.color == LineColor.Black)
			{
				if (line1 == line1.parent.left)
				{
					Line line2 = line1.parent.right;
					if (line2.color == LineColor.Red)
					{
						line2.color = LineColor.Black;
						line1.parent.color = LineColor.Red;
						this.RotateLeft(line1.parent);
						line2 = line1.parent.right;
					}
					if (line2.left.color == LineColor.Black && line2.right.color == LineColor.Black)
					{
						line2.color = LineColor.Red;
						line1 = line1.parent;
					}
					else
					{
						if (line2.right.color == LineColor.Black)
						{
							line2.left.color = LineColor.Black;
							line2.color = LineColor.Red;
							this.RotateRight(line2);
							line2 = line1.parent.right;
						}
						line2.color = line1.parent.color;
						line1.parent.color = LineColor.Black;
						line2.right.color = LineColor.Black;
						this.RotateLeft(line1.parent);
						line1 = this.document;
					}
				}
				else
				{
					Line line2 = line1.parent.left;
					if (line2.color == LineColor.Red)
					{
						line2.color = LineColor.Black;
						line1.parent.color = LineColor.Red;
						this.RotateRight(line1.parent);
						line2 = line1.parent.left;
					}
					if (line2.right.color == LineColor.Black && line2.left.color == LineColor.Black)
					{
						line2.color = LineColor.Red;
						line1 = line1.parent;
					}
					else
					{
						if (line2.left.color == LineColor.Black)
						{
							line2.right.color = LineColor.Black;
							line2.color = LineColor.Red;
							this.RotateLeft(line2);
							line2 = line1.parent.left;
						}
						line2.color = line1.parent.color;
						line1.parent.color = LineColor.Black;
						line2.left.color = LineColor.Black;
						this.RotateRight(line1.parent);
						line1 = this.document;
					}
				}
			}
			line1.color = LineColor.Black;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00049B38 File Offset: 0x00047D38
		private void RotateLeft(Line line1)
		{
			Line right = line1.right;
			line1.right = right.left;
			if (right.left != this.sentinel)
			{
				right.left.parent = line1;
			}
			if (right != this.sentinel)
			{
				right.parent = line1.parent;
			}
			if (line1.parent != null)
			{
				if (line1 == line1.parent.left)
				{
					line1.parent.left = right;
				}
				else
				{
					line1.parent.right = right;
				}
			}
			else
			{
				this.document = right;
			}
			right.left = line1;
			if (line1 != this.sentinel)
			{
				line1.parent = right;
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00049BD8 File Offset: 0x00047DD8
		private void RotateRight(Line line1)
		{
			Line left = line1.left;
			line1.left = left.right;
			if (left.right != this.sentinel)
			{
				left.right.parent = line1;
			}
			if (left != this.sentinel)
			{
				left.parent = line1.parent;
			}
			if (line1.parent != null)
			{
				if (line1 == line1.parent.right)
				{
					line1.parent.right = left;
				}
				else
				{
					line1.parent.left = left;
				}
			}
			else
			{
				this.document = left;
			}
			left.right = line1;
			if (line1 != this.sentinel)
			{
				line1.parent = left;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00049C78 File Offset: 0x00047E78
		internal void UpdateView(Line line, int pos)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.update_suspended > 0)
			{
				this.update_start = Math.Min(this.update_start, line.line_no);
				this.update_pending = true;
				return;
			}
			if (this.RecalculateDocument(this.owner.CreateGraphicsInternal(), line.line_no, line.line_no, true))
			{
				if (line.Y - this.viewport_y >= 0)
				{
					this.owner.Invalidate(new Rectangle(this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, this.owner.Height - (line.Y - this.viewport_y)));
					return;
				}
				this.owner.Invalidate();
				return;
			}
			else
			{
				switch (line.alignment)
				{
				case HorizontalAlignment.Left:
					this.owner.Invalidate(new Rectangle(line.X + ((int)line.widths[pos] - this.viewport_x - 1) + this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, line.height + 1));
					return;
				case HorizontalAlignment.Right:
					this.owner.Invalidate(new Rectangle(line.X + this.offset_x, line.Y - this.viewport_y + this.offset_y, (int)line.widths[pos + 1] - this.viewport_x + line.X, line.height + 1));
					return;
				case HorizontalAlignment.Center:
					this.owner.Invalidate(new Rectangle(line.X + this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, line.height + 1));
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00049E40 File Offset: 0x00048040
		internal void UpdateView(Line line, int line_count, int pos)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.recalc_suspended > 0)
			{
				this.recalc_start = Math.Min(this.recalc_start, line.line_no);
				this.recalc_end = Math.Max(this.recalc_end, line.line_no + line_count);
				this.recalc_optimize = true;
				this.recalc_pending = true;
				return;
			}
			int y = line.Y;
			Line line2 = this.GetLine(line.line_no + line_count);
			if (line2 == null)
			{
				line2 = this.GetLine(this.lines);
			}
			if (line2 == null)
			{
				return;
			}
			int num = line2.Y + line2.height;
			if (!this.RecalculateDocument(this.owner.CreateGraphicsInternal(), line.line_no, line.line_no + line_count, true))
			{
				int num2 = 0 - this.viewport_x + this.offset_x;
				int num3 = this.viewport_width;
				int num4 = Math.Min(y - this.viewport_y, line.Y - this.viewport_y) + this.offset_y;
				int num5 = Math.Max(num - num4, line2.Y + line2.height - num4);
				this.owner.Invalidate(new Rectangle(num2, num4, num3, num5));
				return;
			}
			if (line.Y - this.viewport_y >= 0)
			{
				this.owner.Invalidate(new Rectangle(this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, this.owner.Height - (line.Y - this.viewport_y)));
				return;
			}
			this.owner.Invalidate();
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00049FD0 File Offset: 0x000481D0
		private void ScanForLinks(Line start_line, ref bool link_changed)
		{
			Line line = start_line;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			arrayList.Add(0);
			while (line != null)
			{
				stringBuilder.Append(line.text);
				if (!link_changed)
				{
					line.LinkRecord(stringBuilder2);
				}
				line.ClearLinks();
				arrayList.Add(stringBuilder.Length);
				if (line.ending != LineEnding.Wrap)
				{
					break;
				}
				line = this.GetLine(line.LineNo + 1);
			}
			string[] array = new string[] { "www.", "http:/", "ftp:/", "https:/" };
			int num = 0;
			string text = stringBuilder.ToString();
			int i = 0;
			while (i < text.Length)
			{
				int num2 = this.FirstIndexOfAny(text, array, i, out num);
				if (num2 == -1)
				{
					break;
				}
				if (num == 0)
				{
					if (text.Length == num2 + array[0].Length)
					{
						break;
					}
					if (!char.IsLetterOrDigit(text[num2 + array[0].Length]) && "@/~".IndexOf(text[num2 + array[0].Length].ToString()) == -1)
					{
						i = num2 + array[0].Length;
						continue;
					}
				}
				int num3 = text.Length - 1;
				i = text.Length;
				for (int j = num2 + array[num].Length; j < text.Length; j++)
				{
					if (text[j - 1] == '.')
					{
						if (!char.IsLetterOrDigit(text[j]) && "@/~".IndexOf(text[j].ToString()) == -1)
						{
							num3 = j - 1;
							i = j;
							break;
						}
					}
					else if (!char.IsLetterOrDigit(text[j]) && "@-/:~.?=_&".IndexOf(text[j].ToString()) == -1)
					{
						num3 = j - 1;
						i = j;
						break;
					}
				}
				string text2 = text.Substring(num2, num3 - num2 + 1);
				int num4 = 1;
				while (num4 < arrayList.Count && (int)arrayList[num4] <= num2)
				{
					num4++;
				}
				line = this.GetLine(start_line.LineNo + num4 - 1);
				LineTag lineTag = line.FindTag(num2 - (int)arrayList[num4 - 1] + 1);
				if (lineTag.Start != num2 - (int)arrayList[num4 - 1] + 1)
				{
					if (lineTag == this.CaretTag)
					{
						flag = true;
					}
					lineTag = lineTag.Break(num2 - (int)arrayList[num4 - 1] + 1);
				}
				lineTag.IsLink = true;
				lineTag.LinkText = text2;
				for (int k = 1; k < text2.Length; k++)
				{
					if ((int)arrayList[num4] <= num2 + k)
					{
						line = this.GetLine(start_line.LineNo + num4++);
						lineTag = line.FindTag(num2 + k - (int)arrayList[num4 - 1] + 1);
						lineTag.IsLink = true;
						lineTag.LinkText = text2;
					}
					else if (lineTag.End < num2 + 1 + k - (int)arrayList[num4 - 1])
					{
						do
						{
							lineTag = lineTag.Next;
						}
						while (lineTag.Length == 0);
						lineTag.IsLink = true;
						lineTag.LinkText = text2;
					}
				}
				if (lineTag.End > num2 + text2.Length + 1 - (int)arrayList[num4 - 1])
				{
					if (lineTag == this.CaretTag)
					{
						flag = true;
					}
					lineTag.Break(num2 + text2.Length + 1 - (int)arrayList[num4 - 1]);
				}
			}
			if (flag)
			{
				this.CaretTag = LineTag.FindTag(this.CaretLine, this.CaretPosition);
				link_changed = true;
				return;
			}
			if (!link_changed)
			{
				line = start_line;
				StringBuilder stringBuilder3 = new StringBuilder();
				while (line != null)
				{
					line.LinkRecord(stringBuilder3);
					if (line.ending != LineEnding.Wrap)
					{
						break;
					}
					line = this.GetLine(line.LineNo + 1);
				}
				if (!stringBuilder3.Equals(stringBuilder2))
				{
					link_changed = true;
				}
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004A41C File Offset: 0x0004861C
		private int FirstIndexOfAny(string haystack, string[] needles, int start_index, out int term_found)
		{
			term_found = -1;
			int num = -1;
			for (int i = 0; i < needles.Length; i++)
			{
				int num2 = haystack.IndexOf(needles[i], start_index, StringComparison.InvariantCultureIgnoreCase);
				if (num2 > -1)
				{
					if (term_found > -1)
					{
						if (num2 < num)
						{
							num = num2;
							term_found = i;
						}
					}
					else
					{
						num = num2;
						term_found = i;
					}
				}
			}
			return num;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004A468 File Offset: 0x00048668
		private void InvalidateLinks(Rectangle clip)
		{
			for (int i = this.owner.list_links.Count - 1; i >= 0; i--)
			{
				TextBoxBase.LinkRectangle linkRectangle = (TextBoxBase.LinkRectangle)this.owner.list_links[i];
				if (clip.IntersectsWith(linkRectangle.LinkAreaRectangle))
				{
					this.owner.list_links.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0004A4CC File Offset: 0x000486CC
		internal void ScanForLinks(int start, int end, ref bool link_changed)
		{
			LineEnding lineEnding = LineEnding.Rich;
			while (start != 1 && this.GetLine(start - 1).ending == LineEnding.Wrap)
			{
				start--;
			}
			int num = start;
			while (num <= end && num <= this.lines)
			{
				Line line = this.GetLine(num);
				if (lineEnding != LineEnding.Wrap)
				{
					this.ScanForLinks(line, ref link_changed);
				}
				lineEnding = line.ending;
				if (lineEnding == LineEnding.Wrap && num + 1 <= end)
				{
					end++;
				}
				num++;
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0004A53C File Offset: 0x0004873C
		internal void Empty()
		{
			this.document = this.sentinel;
			this.lines = 0;
			this.Add(1, string.Empty, this.owner.Font, this.owner.ForeColor, LineEnding.None);
			this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			this.PositionCaret(0, 0);
			this.SetSelectionVisible(false);
			this.selection_start.line = this.document;
			this.selection_start.pos = 0;
			this.selection_start.tag = this.selection_start.line.tags;
			this.selection_end.line = this.document;
			this.selection_end.pos = 0;
			this.selection_end.tag = this.selection_end.line.tags;
			this.char_count = 0;
			this.viewport_x = 0;
			this.viewport_y = 0;
			this.document_x = 0;
			this.document_y = 0;
			if (this.owner.IsHandleCreated)
			{
				this.owner.Invalidate();
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004A64C File Offset: 0x0004884C
		internal void PositionCaret(Line line, int pos)
		{
			this.caret.tag = line.FindTag(pos);
			this.MoveCaretToTextTag();
			this.caret.line = line;
			this.caret.pos = pos;
			if (this.owner.IsHandleCreated)
			{
				if (this.owner.Focused)
				{
					if (this.caret.height != this.caret.tag.Height)
					{
						XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
					}
					XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				}
				if (this.CaretMoved != null)
				{
					this.CaretMoved(this, EventArgs.Empty);
				}
			}
			this.caret.height = this.caret.tag.Height;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0004A7A0 File Offset: 0x000489A0
		internal void PositionCaret(int x, int y)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			this.caret.tag = this.FindCursor(x, y, out this.caret.pos);
			this.MoveCaretToTextTag();
			this.caret.line = this.caret.tag.Line;
			this.caret.height = this.caret.tag.Height;
			if (this.owner.ShowSelection && (!this.selection_visible || this.owner.show_caret_w_selection))
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x + this.offset_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0004A8FC File Offset: 0x00048AFC
		internal void CaretHasFocus()
		{
			if (this.caret.tag != null && this.owner.IsHandleCreated)
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.owner.IsHandleCreated && this.SelectionLength() > 0)
			{
				this.InvalidateSelectionArea();
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004A9F1 File Offset: 0x00048BF1
		internal void CaretLostFocus()
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			XplatUI.DestroyCaret(this.owner.Handle);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004AA11 File Offset: 0x00048C11
		internal void AlignCaret()
		{
			this.AlignCaret(true);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004AA1C File Offset: 0x00048C1C
		internal void AlignCaret(bool changeCaretTag)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (changeCaretTag)
			{
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				this.MoveCaretToTextTag();
			}
			if (this.caret.tag.Height > this.caret.tag.Line.Height)
			{
				this.caret.height = this.caret.line.height;
			}
			else
			{
				this.caret.height = this.caret.tag.Height;
			}
			if (this.owner.Focused)
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004AB8C File Offset: 0x00048D8C
		internal void UpdateCaret()
		{
			if (!this.owner.IsHandleCreated || this.caret.tag == null)
			{
				return;
			}
			this.MoveCaretToTextTag();
			if (this.caret.tag.Height != this.caret.height)
			{
				this.caret.height = this.caret.tag.Height;
				if (this.owner.Focused)
				{
					XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				}
			}
			if (this.owner.Focused)
			{
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004ACD4 File Offset: 0x00048ED4
		internal void DisplayCaret()
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.owner.ShowSelection && (!this.selection_visible || this.owner.show_caret_w_selection))
			{
				XplatUI.CaretVisible(this.owner.Handle, true);
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004AD24 File Offset: 0x00048F24
		internal void MoveCaretToTextTag()
		{
			if (this.caret.tag == null || this.caret.tag.IsTextTag)
			{
				return;
			}
			if (this.caret.pos < this.caret.tag.Start)
			{
				this.caret.tag = this.caret.tag.Previous;
				return;
			}
			this.caret.tag = this.caret.tag.Next;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004ADA8 File Offset: 0x00048FA8
		internal void MoveCaret(CaretDirection direction)
		{
			bool flag = false;
			switch (direction)
			{
			case CaretDirection.CharForward:
				break;
			case CaretDirection.CharBack:
				goto IL_0162;
			case CaretDirection.LineUp:
				if (this.caret.line.line_no > 1)
				{
					int num = (int)this.caret.line.widths[this.caret.pos];
					this.PositionCaret(num, this.GetLine(this.caret.line.line_no - 1).Y);
					this.DisplayCaret();
				}
				return;
			case CaretDirection.LineDown:
				if (this.caret.line.line_no < this.lines)
				{
					int num2 = (int)this.caret.line.widths[this.caret.pos];
					this.PositionCaret(num2, this.GetLine(this.caret.line.line_no + 1).Y);
					this.DisplayCaret();
				}
				return;
			case CaretDirection.Home:
				if (this.caret.pos > 0)
				{
					this.caret.pos = 0;
					this.caret.tag = this.caret.line.tags;
					this.UpdateCaret();
				}
				return;
			case CaretDirection.End:
				if (this.caret.pos < this.caret.line.TextLengthWithoutEnding())
				{
					this.caret.pos = this.caret.line.TextLengthWithoutEnding();
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
					this.UpdateCaret();
				}
				return;
			case CaretDirection.PgUp:
			{
				if (this.caret.line.line_no == 1 && this.owner.richtext)
				{
					this.owner.vscroll.Value = 0;
					Line line = this.GetLine(1);
					this.PositionCaret(line, 0);
				}
				int num3 = this.caret.line.Y + this.caret.line.height - 1 - this.viewport_y;
				int num4;
				LineTag lineTag = this.FindCursor((int)this.caret.line.widths[this.caret.pos], this.viewport_y - this.viewport_height, out num4);
				this.owner.vscroll.Value = Math.Min(lineTag.Line.Y, this.owner.vscroll.Maximum - this.viewport_height);
				this.PositionCaret((int)this.caret.line.widths[this.caret.pos], num3 + this.viewport_y);
				return;
			}
			case CaretDirection.PgDn:
			{
				if (this.caret.line.line_no == this.lines && this.owner.richtext)
				{
					this.owner.vscroll.Value = this.owner.vscroll.Maximum - this.viewport_height + 1;
					Line line2 = this.GetLine(this.lines);
					this.PositionCaret(line2, line2.TextLengthWithoutEnding());
				}
				int num5 = this.caret.line.Y - this.viewport_y;
				int num6;
				LineTag lineTag2 = this.FindCursor((int)this.caret.line.widths[this.caret.pos], this.viewport_y + this.viewport_height, out num6);
				this.owner.vscroll.Value = Math.Min(lineTag2.Line.Y, this.owner.vscroll.Maximum - this.viewport_height);
				this.PositionCaret((int)this.caret.line.widths[this.caret.pos], num5 + this.viewport_y);
				return;
			}
			case CaretDirection.CtrlPgUp:
				this.PositionCaret(0, this.viewport_y);
				this.DisplayCaret();
				return;
			case CaretDirection.CtrlPgDn:
			{
				int num7;
				LineTag lineTag3 = this.FindCursor(0, this.viewport_y + this.viewport_height, out num7);
				Line line3;
				if (lineTag3.Line.line_no > 1)
				{
					line3 = this.GetLine(lineTag3.Line.line_no - 1);
				}
				else
				{
					line3 = lineTag3.Line;
				}
				this.PositionCaret(line3, line3.Text.Length);
				this.DisplayCaret();
				return;
			}
			case CaretDirection.CtrlHome:
				this.caret.line = this.GetLine(1);
				this.caret.pos = 0;
				this.caret.tag = this.caret.line.tags;
				this.UpdateCaret();
				return;
			case CaretDirection.CtrlEnd:
				this.caret.line = this.GetLine(this.lines);
				this.caret.pos = this.caret.line.TextLengthWithoutEnding();
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				this.UpdateCaret();
				return;
			case CaretDirection.WordBack:
				if (this.caret.pos > 0)
				{
					this.caret.pos = this.caret.pos - 1;
					while (this.caret.pos > 0)
					{
						if (this.caret.line.text[this.caret.pos] != ' ')
						{
							break;
						}
						this.caret.pos = this.caret.pos - 1;
					}
					while (this.caret.pos > 0 && this.caret.line.text[this.caret.pos] != ' ')
					{
						this.caret.pos = this.caret.pos - 1;
					}
					if (this.caret.line.text.ToString(this.caret.pos, 1) == " ")
					{
						if (this.caret.pos != 0)
						{
							this.caret.pos = this.caret.pos + 1;
						}
						else
						{
							this.caret.line = this.GetLine(this.caret.line.line_no - 1);
							this.caret.pos = this.caret.line.text.Length;
						}
					}
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				else if (this.caret.line.line_no > 1)
				{
					this.caret.line = this.GetLine(this.caret.line.line_no - 1);
					this.caret.pos = this.caret.line.text.Length;
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				this.UpdateCaret();
				return;
			case CaretDirection.WordForward:
			{
				int length = this.caret.line.text.Length;
				if (this.caret.pos < length)
				{
					while (this.caret.pos < length && this.caret.line.text[this.caret.pos] != ' ')
					{
						this.caret.pos = this.caret.pos + 1;
					}
					if (this.caret.pos < length)
					{
						while (this.caret.pos < length && this.caret.line.text[this.caret.pos] == ' ')
						{
							this.caret.pos = this.caret.pos + 1;
						}
					}
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				else if (this.caret.line.line_no < this.lines)
				{
					this.caret.line = this.GetLine(this.caret.line.line_no + 1);
					this.caret.pos = 0;
					this.caret.tag = this.caret.line.tags;
				}
				this.UpdateCaret();
				return;
			}
			case CaretDirection.SelectionStart:
				this.caret.line = this.selection_start.line;
				this.caret.pos = this.selection_start.pos;
				this.caret.tag = this.selection_start.tag;
				this.UpdateCaret();
				return;
			case CaretDirection.SelectionEnd:
				this.caret.line = this.selection_end.line;
				this.caret.pos = this.selection_end.pos;
				this.caret.tag = this.selection_end.tag;
				this.UpdateCaret();
				return;
			case CaretDirection.CharForwardNoWrap:
				flag = true;
				break;
			case CaretDirection.CharBackNoWrap:
				flag = true;
				goto IL_0162;
			default:
				return;
			}
			this.caret.pos = this.caret.pos + 1;
			if (this.caret.pos > this.caret.line.TextLengthWithoutEnding())
			{
				if (!flag)
				{
					if (this.caret.line.line_no < this.lines)
					{
						this.caret.line = this.GetLine(this.caret.line.line_no + 1);
						this.caret.pos = 0;
						this.caret.tag = this.caret.line.tags;
					}
					else
					{
						this.caret.pos = this.caret.pos - 1;
					}
				}
				else
				{
					this.caret.pos = this.caret.pos - 1;
				}
			}
			else if (this.caret.tag.Start - 1 + this.caret.tag.Length < this.caret.pos)
			{
				this.caret.tag = this.caret.tag.Next;
			}
			this.UpdateCaret();
			return;
			IL_0162:
			if (this.caret.pos > 0)
			{
				int num8 = this.caret.pos - 1;
				this.caret.pos = num8;
				if (num8 > 0 && this.caret.tag.Start > this.caret.pos)
				{
					this.caret.tag = this.caret.tag.Previous;
				}
			}
			else if (this.caret.line.line_no > 1 && !flag)
			{
				this.caret.line = this.GetLine(this.caret.line.line_no - 1);
				this.caret.pos = this.caret.line.TextLengthWithoutEnding();
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
			}
			this.UpdateCaret();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004B85C File Offset: 0x00049A5C
		internal void GetVisibleLineIndexes(Rectangle clip, out int start, out int end)
		{
			if (this.multiline)
			{
				start = this.GetLineByPixel(clip.Top + this.viewport_y - this.offset_y - 1, false).line_no;
				end = this.GetLineByPixel(clip.Bottom + this.viewport_y - this.offset_y + 1, false).line_no;
				return;
			}
			start = this.GetLineByPixel(clip.Left + this.viewport_x - this.offset_x, false).line_no;
			end = this.GetLineByPixel(clip.Right + this.viewport_x - this.offset_x, false).line_no;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004B904 File Offset: 0x00049B04
		internal void Draw(Graphics g, Rectangle clip)
		{
			int num;
			int num2;
			this.GetVisibleLineIndexes(clip, out num, out num2);
			this.InvalidateLinks(clip);
			if (this.owner.actual_border_style == BorderStyle.FixedSingle)
			{
				ControlPaint.DrawBorder(g, this.owner.ClientRectangle, global::System.Drawing.Color.Black, ButtonBorderStyle.Solid);
			}
			Line line = this.GetLine(num2 - 1);
			if (line != null && clip.Bottom == this.offset_y + line.Y + line.height - this.viewport_y)
			{
				num2--;
			}
			int i = num;
			if (!this.multiline && this.selection_visible && this.owner.ShowSelection)
			{
				g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), (float)this.offset_x + this.selection_start.line.widths[this.selection_start.pos] + (float)this.selection_start.line.X - (float)this.viewport_x, (float)(this.offset_y + this.selection_start.line.Y), (float)this.selection_end.line.X + this.selection_end.line.widths[this.selection_end.pos] - ((float)this.selection_start.line.X + this.selection_start.line.widths[this.selection_start.pos]), (float)this.selection_start.line.height);
			}
			while (i <= num2)
			{
				line = this.GetLine(i);
				float num3 = (float)(line.Y - this.viewport_y + this.offset_y);
				LineTag lineTag = line.tags;
				StringBuilder stringBuilder;
				if (!this.calc_pass)
				{
					stringBuilder = line.text;
				}
				else
				{
					if (this.PasswordCache.Length < line.text.Length)
					{
						this.PasswordCache.Append(char.Parse(this.password_char), line.text.Length - this.PasswordCache.Length);
					}
					else if (this.PasswordCache.Length > line.text.Length)
					{
						this.PasswordCache.Remove(line.text.Length, this.PasswordCache.Length - line.text.Length);
					}
					stringBuilder = this.PasswordCache;
				}
				int num4 = stringBuilder.Length + 1;
				int num5 = stringBuilder.Length + 1;
				if (this.selection_visible && this.owner.ShowSelection && i >= this.selection_start.line.line_no && i <= this.selection_end.line.line_no)
				{
					if (i == this.selection_start.line.line_no)
					{
						num4 = this.selection_start.pos + 1;
					}
					else
					{
						num4 = 1;
					}
					if (i == this.selection_end.line.line_no)
					{
						num5 = this.selection_end.pos + 1;
					}
					else
					{
						num5 = stringBuilder.Length + 1;
					}
					if (num5 == num4)
					{
						num4 = stringBuilder.Length + 1;
						num5 = num4;
					}
					else if (this.multiline)
					{
						g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), (float)this.offset_x + line.widths[num4 - 1] + (float)line.X - (float)this.viewport_x, num3, line.widths[num5 - 1] - line.widths[num4 - 1], (float)line.height);
					}
				}
				global::System.Drawing.Color color = line.tags.ColorToDisplay;
				while (lineTag != null)
				{
					if (lineTag.Length == 0)
					{
						lineTag = lineTag.Next;
					}
					else if (lineTag.X + lineTag.Width < (float)(clip.Left - this.viewport_x - this.offset_x) && lineTag.X > (float)(clip.Right - this.viewport_x - this.offset_x))
					{
						lineTag = lineTag.Next;
					}
					else
					{
						if (lineTag.BackColor != global::System.Drawing.Color.Empty)
						{
							g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(lineTag.BackColor), (float)this.offset_x + lineTag.X + (float)line.X - (float)this.viewport_x, num3 + (float)lineTag.Shift, lineTag.Width, (float)line.height);
						}
						global::System.Drawing.Color color2 = lineTag.ColorToDisplay;
						if (!this.owner.Enabled)
						{
							global::System.Drawing.Color color3 = lineTag.Color;
							global::System.Drawing.Color colorWindowText = ThemeEngine.Current.ColorWindowText;
							if (color3.R == colorWindowText.R && color3.G == colorWindowText.G && color3.B == colorWindowText.B)
							{
								color2 = ThemeEngine.Current.ColorGrayText;
							}
						}
						int j = lineTag.Start;
						while (j < lineTag.Start + lineTag.Length)
						{
							int num6 = j;
							if (j >= num4 && j < num5)
							{
								color = ThemeEngine.Current.ColorHighlightText;
								j = Math.Min(lineTag.End, num5);
							}
							else if (j < num4)
							{
								color = color2;
								j = Math.Min(lineTag.End, num4);
							}
							else
							{
								color = color2;
								j = lineTag.End;
							}
							Rectangle rectangle;
							lineTag.Draw(g, color, (float)(this.offset_x + line.X - this.viewport_x), num3 + (float)lineTag.Shift, num6 - 1, Math.Min(lineTag.Start + lineTag.Length, j) - 1, stringBuilder.ToString(), out rectangle, lineTag.IsLink);
							if (lineTag.IsLink)
							{
								TextBoxBase.LinkRectangle linkRectangle = new TextBoxBase.LinkRectangle(rectangle);
								linkRectangle.LinkTag = lineTag;
								this.owner.list_links.Add(linkRectangle);
							}
						}
						lineTag = lineTag.Next;
					}
				}
				line.DrawEnding(g, num3);
				i++;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004BEF4 File Offset: 0x0004A0F4
		private int GetLineEnding(string line, int start, out LineEnding ending)
		{
			if (start >= line.Length)
			{
				ending = LineEnding.Wrap;
				return -1;
			}
			int num = line.IndexOf('\r', start);
			int num2 = line.IndexOf('\n', start);
			if (num != -1 && num2 != -1 && num2 < num)
			{
				ending = LineEnding.Rich;
				return num2;
			}
			if (num != -1)
			{
				if (num + 2 < line.Length && line[num + 1] == '\r' && line[num + 2] == '\n')
				{
					ending = LineEnding.Soft;
					return num;
				}
				if (num + 1 < line.Length && line[num + 1] == '\n')
				{
					ending = LineEnding.Hard;
					return num;
				}
				ending = LineEnding.Limp;
				return num;
			}
			else
			{
				if (num2 != -1)
				{
					ending = LineEnding.Rich;
					return num2;
				}
				ending = LineEnding.Wrap;
				return line.Length;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004BF98 File Offset: 0x0004A198
		private int GetLineEnding(string line, int start, out LineEnding ending, LineEnding type)
		{
			int num = start;
			int num2 = 0;
			do
			{
				num = this.GetLineEnding(line, num + num2, out ending);
				num2 = this.LineEndingLength(ending);
			}
			while ((ending & type) != ending && num != -1);
			if (num != -1)
			{
				return num;
			}
			return line.Length;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0004BFD7 File Offset: 0x0004A1D7
		internal int LineEndingLength(LineEnding ending)
		{
			if (ending <= LineEnding.Hard)
			{
				if (ending != LineEnding.Limp)
				{
					if (ending != LineEnding.Hard)
					{
						return 0;
					}
					return 2;
				}
			}
			else
			{
				if (ending == LineEnding.Soft)
				{
					return 3;
				}
				if (ending != LineEnding.Rich)
				{
					return 0;
				}
			}
			return 1;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0004BFF7 File Offset: 0x0004A1F7
		internal string LineEndingToString(LineEnding ending)
		{
			if (ending <= LineEnding.Hard)
			{
				if (ending == LineEnding.Limp)
				{
					return "\r";
				}
				if (ending == LineEnding.Hard)
				{
					return "\r\n";
				}
			}
			else
			{
				if (ending == LineEnding.Soft)
				{
					return "\r\r\n";
				}
				if (ending == LineEnding.Rich)
				{
					return "\n";
				}
			}
			return string.Empty;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004C030 File Offset: 0x0004A230
		internal LineEnding StringToLineEnding(string ending)
		{
			if (ending == "\r")
			{
				return LineEnding.Limp;
			}
			if (ending == "\r\n")
			{
				return LineEnding.Hard;
			}
			if (ending == "\r\r\n")
			{
				return LineEnding.Soft;
			}
			if (!(ending == "\n"))
			{
				return LineEnding.None;
			}
			return LineEnding.Rich;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0004C07D File Offset: 0x0004A27D
		internal void Insert(Line line, int pos, bool update_caret, string s)
		{
			this.Insert(line, pos, update_caret, s, line.FindTag(pos));
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0004C094 File Offset: 0x0004A294
		internal void Insert(Line line, int pos, bool update_caret, string s, LineTag tag)
		{
			int num = 1;
			this.SuspendRecalc();
			int line_no = line.line_no;
			int num2 = this.lines;
			int num3 = s.IndexOf('\0');
			if (num3 != -1)
			{
				s = s.Substring(0, num3);
			}
			LineEnding lineEnding;
			int num4 = this.GetLineEnding(s, 0, out lineEnding, (LineEnding)20);
			if (num4 == s.Length)
			{
				line.InsertString(pos, s, tag);
			}
			else
			{
				line.InsertString(pos, s.Substring(0, num4 + this.LineEndingLength(lineEnding)), tag);
				this.Split(line, pos + (num4 + this.LineEndingLength(lineEnding)));
				line.ending = lineEnding;
				num4 += this.LineEndingLength(lineEnding);
				Line line2 = this.GetLine(line.line_no + 1);
				for (;;)
				{
					int lineEnding2 = this.GetLineEnding(s, num4, out lineEnding, (LineEnding)20);
					if (lineEnding2 == s.Length)
					{
						break;
					}
					string text = s.Substring(num4, lineEnding2 - num4 + this.LineEndingLength(lineEnding));
					this.Add(line_no + num, text, line.alignment, tag.Font, tag.Color, lineEnding);
					this.GetLine(line_no + num).ending = lineEnding;
					num++;
					num4 = lineEnding2 + this.LineEndingLength(lineEnding);
				}
				line2.InsertString(0, s.Substring(num4));
			}
			this.ResumeRecalc(false);
			this.CharCount += s.Length;
			this.UpdateView(line, this.lines - num2 + 1, pos);
			if (update_caret)
			{
				Line line3 = this.GetLine(line.line_no + this.lines - num2);
				this.PositionCaret(line3, line3.text.Length);
				this.DisplayCaret();
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0004C230 File Offset: 0x0004A430
		internal void InsertString(Line line, int pos, string s)
		{
			this.CharCount += s.Length;
			line.InsertString(pos, s);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0004C250 File Offset: 0x0004A450
		internal void InsertCharAtCaret(char ch, bool move_caret)
		{
			this.caret.line.InsertString(this.caret.pos, ch.ToString(), this.caret.tag);
			int charCount = this.CharCount;
			this.CharCount = charCount + 1;
			this.undo.RecordTyping(this.caret.line, this.caret.pos, ch);
			this.UpdateView(this.caret.line, this.caret.pos);
			if (move_caret)
			{
				this.caret.pos = this.caret.pos + 1;
				this.UpdateCaret();
				this.SetSelectionToCaret(true);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004C2F8 File Offset: 0x0004A4F8
		internal void InsertPicture(Line line, int pos, Picture picture)
		{
			int num = 1;
			line.text.Insert(pos, "I");
			PictureTag pictureTag = new PictureTag(line, pos + 1, picture);
			LineTag lineTag = LineTag.FindTag(line, pos);
			pictureTag.CopyFormattingFrom(lineTag);
			lineTag.Break(pos + 1);
			pictureTag.Previous = lineTag;
			pictureTag.Next = lineTag.Next;
			lineTag.Next = pictureTag;
			if (pictureTag.Next == null)
			{
				pictureTag.Next = new LineTag(line, pos + 1);
				pictureTag.Next.CopyFormattingFrom(lineTag);
				pictureTag.Next.Previous = pictureTag;
			}
			for (lineTag = pictureTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Start += num;
			}
			line.Grow(num);
			line.recalc = true;
			this.UpdateView(line, pos);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		internal void DeleteMultiline(Line start_line, int pos, int length)
		{
			Document.Marker marker = default(Document.Marker);
			Document.Marker marker2 = default(Document.Marker);
			int num = this.LineTagToCharIndex(start_line, pos);
			marker.line = start_line;
			marker.pos = pos;
			marker.tag = LineTag.FindTag(start_line, pos);
			this.CharIndexToLineTag(num + length, out marker2.line, out marker2.tag, out marker2.pos);
			this.SuspendUpdate();
			if (marker.line == marker2.line)
			{
				this.DeleteChars(marker.line, pos, marker2.pos - pos);
			}
			else
			{
				this.DeleteChars(marker.line, marker.pos, marker.line.text.Length - marker.pos);
				this.DeleteChars(marker2.line, 0, marker2.pos);
				int num2 = marker.line.line_no + 1;
				if (num2 < marker2.line.line_no)
				{
					for (int i = marker2.line.line_no - 1; i >= num2; i--)
					{
						this.Delete(i);
					}
				}
				this.Combine(marker.line.line_no, num2);
			}
			this.ResumeUpdate(true);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		public void DeleteChars(Line line, int pos, int count)
		{
			this.CharCount -= count;
			line.DeleteCharacters(pos, count);
			if (pos >= line.TextLengthWithoutEnding())
			{
				LineEnding ending = line.ending;
				this.GetLineEnding(line.text.ToString(), 0, out ending);
				if (ending != line.ending)
				{
					line.ending = ending;
					if (!this.multiline)
					{
						this.UpdateView(line, this.lines, pos);
						this.owner.Invalidate();
						return;
					}
				}
			}
			if (!this.multiline)
			{
				this.UpdateView(line, this.lines, pos);
				this.owner.Invalidate();
				return;
			}
			this.UpdateView(line, pos);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0004C58C File Offset: 0x0004A78C
		public void DeleteChar(Line line, int pos, bool forward)
		{
			if ((pos == 0 && !forward) || (pos == line.text.Length && forward))
			{
				return;
			}
			this.undo.BeginUserAction("Delete");
			if (forward)
			{
				this.undo.RecordDeleteString(line, pos, line, pos + 1);
				this.DeleteChars(line, pos, 1);
			}
			else
			{
				this.undo.RecordDeleteString(line, pos - 1, line, pos);
				this.DeleteChars(line, pos - 1, 1);
			}
			this.undo.EndUserAction();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0004C608 File Offset: 0x0004A808
		internal void Combine(int FirstLine, int SecondLine)
		{
			this.Combine(this.GetLine(FirstLine), this.GetLine(SecondLine));
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0004C620 File Offset: 0x0004A820
		internal void Combine(Line first, Line second)
		{
			first.text.Length = first.text.Length - this.LineEndingLength(first.ending);
			LineTag lineTag = first.tags;
			first.ending = second.ending;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			int num = lineTag.Start + lineTag.Length - 1;
			lineTag.Next = second.tags;
			lineTag.Next.Previous = lineTag;
			for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Line = first;
				lineTag.Start += num;
			}
			first.text.Insert(first.text.Length, second.text.ToString());
			first.Grow(first.text.Length);
			second.tags = null;
			this.DecrementLines(first.line_no + 2);
			first.recalc = true;
			first.height = 0;
			first.Streamline(this.lines);
			if (this.caret.line == second)
			{
				this.caret.Combine(first, num);
			}
			if (this.selection_anchor.line == second)
			{
				this.selection_anchor.Combine(first, num);
			}
			if (this.selection_start.line == second)
			{
				this.selection_start.Combine(first, num);
			}
			if (this.selection_end.line == second)
			{
				this.selection_end.Combine(first, num);
			}
			this.Delete(second);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0004C79C File Offset: 0x0004A99C
		internal void Split(int LineNo, int pos)
		{
			Line line = this.GetLine(LineNo);
			LineTag lineTag = LineTag.FindTag(line, pos);
			this.Split(line, lineTag, pos);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0004C7C4 File Offset: 0x0004A9C4
		internal void Split(Line line, int pos)
		{
			LineTag lineTag = LineTag.FindTag(line, pos);
			this.Split(line, lineTag, pos);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004C7E4 File Offset: 0x0004A9E4
		internal void Split(Line line, LineTag tag, int pos)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (this.caret.line == line && this.caret.pos >= pos)
			{
				flag = true;
			}
			if (this.selection_start.line == line && this.selection_start.pos > pos)
			{
				flag2 = true;
			}
			if (this.selection_end.line == line && this.selection_end.pos > pos)
			{
				flag3 = true;
			}
			Line line2;
			if (pos == line.text.Length)
			{
				this.Add(line.line_no + 1, string.Empty, line.alignment, tag.Font, tag.Color, line.ending);
				line2 = this.GetLine(line.line_no + 1);
				if (flag)
				{
					this.caret.line = line2;
					this.caret.tag = line2.tags;
					this.caret.pos = 0;
					if (!this.selection_visible)
					{
						this.SetSelectionToCaret(true);
					}
				}
				if (flag2)
				{
					this.selection_start.line = line2;
					this.selection_start.pos = 0;
					this.selection_start.tag = line2.tags;
				}
				if (flag3)
				{
					this.selection_end.line = line2;
					this.selection_end.pos = 0;
					this.selection_end.tag = line2.tags;
				}
				return;
			}
			this.Add(line.line_no + 1, line.text.ToString(pos, line.text.Length - pos), line.alignment, tag.Font, tag.Color, line.ending);
			line2 = this.GetLine(line.line_no + 1);
			line.recalc = true;
			line2.recalc = true;
			if (tag.Next != null && tag.Next.Start - 1 == pos)
			{
				tag = tag.Next;
			}
			if (tag.Start - 1 == pos)
			{
				if (tag == line.tags)
				{
					LineTag lineTag = new LineTag(line, 1);
					lineTag.CopyFormattingFrom(tag);
					line.tags = lineTag;
				}
				if (tag.Previous != null)
				{
					tag.Previous.Next = null;
				}
				line2.tags = tag;
				tag.Previous = null;
				tag.Line = line2;
				int num = tag.Start - 1;
				for (LineTag lineTag = tag; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= num;
					lineTag.Line = line2;
				}
			}
			else
			{
				LineTag lineTag = new LineTag(line2, 1);
				lineTag.Next = tag.Next;
				lineTag.CopyFormattingFrom(tag);
				line2.tags = lineTag;
				if (lineTag.Next != null)
				{
					lineTag.Next.Previous = lineTag;
				}
				tag.Next = null;
				for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= pos;
					lineTag.Line = line2;
				}
			}
			if (flag)
			{
				this.caret.line = line2;
				this.caret.pos = this.caret.pos - pos;
				this.caret.tag = this.caret.line.FindTag(this.caret.pos);
				if (!this.selection_visible)
				{
					this.SetSelectionToCaret(true);
					flag2 = false;
					flag3 = false;
				}
			}
			if (flag2)
			{
				this.selection_start.line = line2;
				this.selection_start.pos = this.selection_start.pos - pos;
				if (this.selection_start.Equals(this.selection_end))
				{
					this.selection_start.tag = line2.FindTag(this.selection_start.pos);
				}
				else
				{
					this.selection_start.tag = line2.FindTag(this.selection_start.pos + 1);
				}
			}
			if (flag3)
			{
				this.selection_end.line = line2;
				this.selection_end.pos = this.selection_end.pos - pos;
				this.selection_end.tag = line2.FindTag(this.selection_end.pos);
			}
			this.CharCount -= line.text.Length - pos;
			line.text.Remove(pos, line.text.Length - pos);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004CBF5 File Offset: 0x0004ADF5
		internal void Add(int LineNo, string Text, global::System.Drawing.Font font, global::System.Drawing.Color color, LineEnding ending)
		{
			this.Add(LineNo, Text, this.alignment, font, color, ending);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004CC0C File Offset: 0x0004AE0C
		internal void Add(int LineNo, string Text, HorizontalAlignment align, global::System.Drawing.Font font, global::System.Drawing.Color color, LineEnding ending)
		{
			this.CharCount += Text.Length;
			if (LineNo >= 1 && Text != null)
			{
				Line line = new Line(this, LineNo, Text, align, font, color, ending);
				Line line2 = this.document;
				while (line2 != this.sentinel)
				{
					line.parent = line2;
					int line_no = line2.line_no;
					if (LineNo > line_no)
					{
						line2 = line2.right;
					}
					else if (LineNo < line_no)
					{
						line2 = line2.left;
					}
					else
					{
						this.IncrementLines(line2.line_no);
						line2 = line2.left;
					}
				}
				line.left = this.sentinel;
				line.right = this.sentinel;
				if (line.parent != null)
				{
					if (LineNo > line.parent.line_no)
					{
						line.parent.right = line;
					}
					else
					{
						line.parent.left = line;
					}
				}
				else
				{
					this.document = line;
				}
				this.RebalanceAfterAdd(line);
				this.lines++;
				return;
			}
			if (LineNo < 1)
			{
				throw new ArgumentNullException("LineNo", "Line numbers must be positive");
			}
			throw new ArgumentNullException("Text", "Cannot insert NULL line");
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0004CD1A File Offset: 0x0004AF1A
		public virtual object Clone()
		{
			return new Document(null)
			{
				lines = this.lines,
				document = (Line)this.document.Clone()
			};
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0004CD44 File Offset: 0x0004AF44
		private void Delete(int LineNo)
		{
			if (LineNo > this.lines)
			{
				return;
			}
			Line line = this.GetLine(LineNo);
			this.CharCount -= line.text.Length;
			this.DecrementLines(LineNo + 1);
			this.Delete(line);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004CD8C File Offset: 0x0004AF8C
		private void Delete(Line line1)
		{
			Line line2;
			if (line1.left == this.sentinel || line1.right == this.sentinel)
			{
				line2 = line1;
			}
			else
			{
				line2 = line1.right;
				while (line2.left != this.sentinel)
				{
					line2 = line2.left;
				}
			}
			Line line3;
			if (line2.left != this.sentinel)
			{
				line3 = line2.left;
			}
			else
			{
				line3 = line2.right;
			}
			line3.parent = line2.parent;
			if (line2.parent != null)
			{
				if (line2 == line2.parent.left)
				{
					line2.parent.left = line3;
				}
				else
				{
					line2.parent.right = line3;
				}
			}
			else
			{
				this.document = line3;
			}
			if (line2 != line1)
			{
				if (this.selection_start.line == line2)
				{
					this.selection_start.line = line1;
				}
				if (this.selection_end.line == line2)
				{
					this.selection_end.line = line1;
				}
				if (this.selection_anchor.line == line2)
				{
					this.selection_anchor.line = line1;
				}
				if (this.caret.line == line2)
				{
					this.caret.line = line1;
				}
				line1.alignment = line2.alignment;
				line1.ascent = line2.ascent;
				line1.hanging_indent = line2.hanging_indent;
				line1.height = line2.height;
				line1.indent = line2.indent;
				line1.line_no = line2.line_no;
				line1.recalc = line2.recalc;
				line1.right_indent = line2.right_indent;
				line1.ending = line2.ending;
				line1.space = line2.space;
				line1.tags = line2.tags;
				line1.text = line2.text;
				line1.widths = line2.widths;
				line1.offset = line2.offset;
				for (LineTag lineTag = line1.tags; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Line = line1;
				}
			}
			if (line2.color == LineColor.Black)
			{
				this.RebalanceAfterDelete(line3);
			}
			this.lines--;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004CF89 File Offset: 0x0004B189
		internal void InvalidateLinesAfter(Line start)
		{
			this.owner.Invalidate(new Rectangle(0, start.Y - this.viewport_y, this.viewport_width, this.viewport_height - start.Y));
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004CFBC File Offset: 0x0004B1BC
		internal void Invalidate(Line start, int start_pos, Line end, int end_pos)
		{
			if (start == end && start_pos == end_pos)
			{
				return;
			}
			if (end_pos == -1)
			{
				end_pos = end.text.Length;
			}
			Line line;
			int num;
			Line line2;
			int num2;
			if (start.line_no < end.line_no)
			{
				line = start;
				num = start_pos;
				line2 = end;
				num2 = end_pos;
			}
			else
			{
				if (start.line_no <= end.line_no)
				{
					if (start_pos < end_pos)
					{
						line = start;
						num = start_pos;
						num2 = end_pos;
					}
					else
					{
						line = end;
						num = end_pos;
						num2 = start_pos;
					}
					int num3 = (int)line.widths[num2];
					if (num2 == line.text.Length + 1)
					{
						num3 = this.viewport_width;
					}
					this.owner.Invalidate(new Rectangle(this.offset_x + (int)line.widths[num] + line.X - this.viewport_x, this.offset_y + line.Y - this.viewport_y, num3 - (int)line.widths[num] + 1, line.height));
					return;
				}
				line = end;
				num = end_pos;
				line2 = start;
				num2 = start_pos;
			}
			this.owner.Invalidate(new Rectangle(this.offset_x + (int)line.widths[num] + line.X - this.viewport_x, this.offset_y + line.Y - this.viewport_y, this.viewport_width, line.height));
			if (line.line_no + 1 < line2.line_no)
			{
				int y = this.GetLine(line.line_no + 1).Y;
				this.owner.Invalidate(new Rectangle(this.offset_x, this.offset_y + y - this.viewport_y, this.viewport_width, line2.Y - y));
			}
			this.owner.Invalidate(new Rectangle(this.offset_x + (int)line2.widths[0] + line2.X - this.viewport_x, this.offset_y + line2.Y - this.viewport_y, (int)line2.widths[num2] + 1, line2.height));
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004D1AC File Offset: 0x0004B3AC
		internal void ExpandSelection(CaretSelection mode, bool to_caret)
		{
			if (to_caret)
			{
				switch (mode)
				{
				case CaretSelection.Position:
					this.SetSelectionToCaret(false);
					return;
				case CaretSelection.Word:
				{
					int num = this.FindWordSeparator(this.caret.line, this.caret.pos, false);
					int num2 = this.FindWordSeparator(this.caret.line, this.caret.pos, true);
					if (this.caret > this.selection_prev)
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.pos, this.caret.line, num2);
					}
					else
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.pos, this.caret.line, num);
					}
					if (this.caret < this.selection_anchor)
					{
						this.selection_start.line = this.caret.line;
						this.selection_start.tag = this.caret.line.FindTag(num + 1);
						this.selection_start.pos = num;
						this.selection_end.line = this.selection_anchor.line;
						this.selection_end.tag = this.selection_anchor.tag;
						this.selection_end.pos = this.selection_anchor.pos;
						this.selection_prev.line = this.caret.line;
						this.selection_prev.tag = this.caret.tag;
						this.selection_prev.pos = num;
						this.selection_end_anchor = true;
					}
					else
					{
						this.selection_start.line = this.selection_anchor.line;
						this.selection_start.pos = this.selection_anchor.height;
						this.selection_start.tag = this.selection_anchor.line.FindTag(this.selection_anchor.height + 1);
						this.selection_end.line = this.caret.line;
						this.selection_end.tag = this.caret.line.FindTag(num2);
						this.selection_end.pos = num2;
						this.selection_prev.line = this.caret.line;
						this.selection_prev.tag = this.caret.tag;
						this.selection_prev.pos = num2;
						this.selection_end_anchor = false;
					}
					break;
				}
				case CaretSelection.Line:
					if (this.caret > this.selection_prev)
					{
						this.Invalidate(this.selection_prev.line, 0, this.caret.line, this.caret.line.text.Length);
					}
					else
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.line.text.Length, this.caret.line, 0);
					}
					if (this.caret.line.line_no <= this.selection_anchor.line.line_no)
					{
						this.selection_start.line = this.caret.line;
						this.selection_start.tag = this.caret.line.tags;
						this.selection_start.pos = 0;
						this.selection_end.line = this.selection_anchor.line;
						this.selection_end.tag = this.selection_anchor.tag;
						this.selection_end.pos = this.selection_anchor.pos;
						this.selection_end_anchor = true;
					}
					else
					{
						this.selection_start.line = this.selection_anchor.line;
						this.selection_start.pos = this.selection_anchor.height;
						this.selection_start.tag = this.selection_anchor.line.FindTag(this.selection_anchor.height + 1);
						this.selection_end.line = this.caret.line;
						this.selection_end.tag = this.caret.line.tags;
						this.selection_end.pos = this.caret.line.text.Length;
						this.selection_end_anchor = false;
					}
					this.selection_prev.line = this.caret.line;
					this.selection_prev.tag = this.caret.tag;
					this.selection_prev.pos = this.caret.pos;
					break;
				}
			}
			else if (mode != CaretSelection.Word)
			{
				if (mode == CaretSelection.Line)
				{
					this.Invalidate(this.caret.line, 0, this.caret.line, this.caret.line.text.Length);
					this.selection_start.line = this.caret.line;
					this.selection_start.tag = this.caret.line.tags;
					this.selection_start.pos = 0;
					this.selection_end.line = this.caret.line;
					this.selection_end.pos = this.caret.line.text.Length;
					this.selection_end.tag = this.caret.line.FindTag(this.selection_end.pos);
					this.selection_anchor.line = this.selection_end.line;
					this.selection_anchor.tag = this.selection_end.tag;
					this.selection_anchor.pos = this.selection_end.pos;
					this.selection_anchor.height = 0;
					this.selection_prev.line = this.caret.line;
					this.selection_prev.tag = this.caret.tag;
					this.selection_prev.pos = this.caret.pos;
					this.selection_end_anchor = true;
				}
			}
			else
			{
				int num3 = this.FindWordSeparator(this.caret.line, this.caret.pos, false);
				int num4 = this.FindWordSeparator(this.caret.line, this.caret.pos, true);
				this.Invalidate(this.selection_start.line, num3, this.caret.line, num4);
				this.selection_start.line = this.caret.line;
				this.selection_start.tag = this.caret.line.FindTag(num3 + 1);
				this.selection_start.pos = num3;
				this.selection_end.line = this.caret.line;
				this.selection_end.tag = this.caret.line.FindTag(num4);
				this.selection_end.pos = num4;
				this.selection_anchor.line = this.selection_end.line;
				this.selection_anchor.tag = this.selection_end.tag;
				this.selection_anchor.pos = this.selection_end.pos;
				this.selection_anchor.height = num3;
				this.selection_prev.line = this.caret.line;
				this.selection_prev.tag = this.caret.tag;
				this.selection_prev.pos = this.caret.pos;
				this.selection_end_anchor = true;
			}
			this.SetSelectionVisible(!(this.selection_start == this.selection_end));
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004D94C File Offset: 0x0004BB4C
		internal void SetSelectionToCaret(bool start)
		{
			if (start)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
				this.selection_start.line = this.caret.line;
				this.selection_start.tag = this.caret.tag;
				this.selection_start.pos = this.caret.pos;
				this.selection_end.line = this.caret.line;
				this.selection_end.tag = this.caret.tag;
				this.selection_end.pos = this.caret.pos;
				this.selection_anchor.line = this.caret.line;
				this.selection_anchor.tag = this.caret.tag;
				this.selection_anchor.pos = this.caret.pos;
			}
			else
			{
				if (this.selection_end_anchor)
				{
					if (this.selection_start != this.caret)
					{
						this.Invalidate(this.selection_start.line, this.selection_start.pos, this.caret.line, this.caret.pos);
					}
				}
				else if (this.selection_end != this.caret)
				{
					this.Invalidate(this.selection_end.line, this.selection_end.pos, this.caret.line, this.caret.pos);
				}
				if (this.caret < this.selection_anchor)
				{
					this.selection_start.line = this.caret.line;
					this.selection_start.tag = this.caret.tag;
					this.selection_start.pos = this.caret.pos;
					this.selection_end.line = this.selection_anchor.line;
					this.selection_end.tag = this.selection_anchor.tag;
					this.selection_end.pos = this.selection_anchor.pos;
					this.selection_end_anchor = true;
				}
				else
				{
					this.selection_start.line = this.selection_anchor.line;
					this.selection_start.tag = this.selection_anchor.tag;
					this.selection_start.pos = this.selection_anchor.pos;
					this.selection_end.line = this.caret.line;
					this.selection_end.tag = this.caret.tag;
					this.selection_end.pos = this.caret.pos;
					this.selection_end_anchor = false;
				}
			}
			this.SetSelectionVisible(!(this.selection_start == this.selection_end));
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004DC3C File Offset: 0x0004BE3C
		internal void SetSelection(Line start, int start_pos, Line end, int end_pos)
		{
			if (this.selection_visible)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
			if (end.line_no < start.line_no || (end == start && end_pos <= start_pos))
			{
				this.selection_start.line = end;
				this.selection_start.tag = LineTag.FindTag(end, end_pos);
				this.selection_start.pos = end_pos;
				this.selection_end.line = start;
				this.selection_end.tag = LineTag.FindTag(start, start_pos);
				this.selection_end.pos = start_pos;
				this.selection_end_anchor = true;
			}
			else
			{
				this.selection_start.line = start;
				this.selection_start.tag = LineTag.FindTag(start, start_pos);
				this.selection_start.pos = start_pos;
				this.selection_end.line = end;
				this.selection_end.tag = LineTag.FindTag(end, end_pos);
				this.selection_end.pos = end_pos;
				this.selection_end_anchor = false;
			}
			this.selection_anchor.line = start;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_anchor.pos = start_pos;
			if ((start == end && start_pos == end_pos) || start == null || end == null)
			{
				this.SetSelectionVisible(false);
				return;
			}
			this.SetSelectionVisible(true);
			this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
		internal void SetSelectionStart(Line start, int start_pos, bool invalidate)
		{
			if (invalidate)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, start, start_pos);
			}
			this.selection_start.line = start;
			this.selection_start.pos = start_pos;
			this.selection_start.tag = LineTag.FindTag(start, start_pos);
			this.selection_anchor.line = start;
			this.selection_anchor.pos = start_pos;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_end_anchor = false;
			if (this.selection_end.line != this.selection_start.line || this.selection_end.pos != this.selection_start.pos)
			{
				this.SetSelectionVisible(true);
			}
			else
			{
				this.SetSelectionVisible(false);
			}
			if (invalidate)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0004DED8 File Offset: 0x0004C0D8
		internal void SetSelectionStart(int character_index, bool invalidate)
		{
			if (character_index < 0)
			{
				return;
			}
			Line line;
			LineTag lineTag;
			int num;
			this.CharIndexToLineTag(character_index, out line, out lineTag, out num);
			this.SetSelectionStart(line, num, invalidate);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0004DF00 File Offset: 0x0004C100
		internal void SetSelectionEnd(Line end, int end_pos, bool invalidate)
		{
			if (end == this.selection_end.line && end_pos == this.selection_start.pos)
			{
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.tag = this.selection_start.tag;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_end.line = this.selection_start.line;
				this.selection_end.tag = this.selection_start.tag;
				this.selection_end.pos = this.selection_start.pos;
				this.selection_end_anchor = false;
			}
			else if (end.line_no < this.selection_anchor.line.line_no || (end == this.selection_anchor.line && end_pos <= this.selection_anchor.pos))
			{
				this.selection_start.line = end;
				this.selection_start.tag = LineTag.FindTag(end, end_pos);
				this.selection_start.pos = end_pos;
				this.selection_end.line = this.selection_anchor.line;
				this.selection_end.tag = this.selection_anchor.tag;
				this.selection_end.pos = this.selection_anchor.pos;
				this.selection_end_anchor = true;
			}
			else
			{
				this.selection_start.line = this.selection_anchor.line;
				this.selection_start.tag = this.selection_anchor.tag;
				this.selection_start.pos = this.selection_anchor.pos;
				this.selection_end.line = end;
				this.selection_end.tag = LineTag.FindTag(end, end_pos);
				this.selection_end.pos = end_pos;
				this.selection_end_anchor = false;
			}
			if (this.selection_end.line != this.selection_start.line || this.selection_end.pos != this.selection_start.pos)
			{
				this.SetSelectionVisible(true);
				if (invalidate)
				{
					this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
					return;
				}
			}
			else
			{
				this.SetSelectionVisible(false);
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0004E154 File Offset: 0x0004C354
		internal void SetSelectionEnd(int character_index, bool invalidate)
		{
			if (character_index < 0)
			{
				return;
			}
			Line line;
			LineTag lineTag;
			int num;
			this.CharIndexToLineTag(character_index, out line, out lineTag, out num);
			this.SetSelectionEnd(line, num, invalidate);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0004E17C File Offset: 0x0004C37C
		internal void SetSelection(Line start, int start_pos)
		{
			if (this.selection_visible)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
			this.selection_start.line = start;
			this.selection_start.pos = start_pos;
			this.selection_start.tag = LineTag.FindTag(start, start_pos);
			this.selection_end.line = start;
			this.selection_end.tag = this.selection_start.tag;
			this.selection_end.pos = start_pos;
			this.selection_anchor.line = start;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_anchor.pos = start_pos;
			this.selection_end_anchor = false;
			this.SetSelectionVisible(false);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004E257 File Offset: 0x0004C457
		internal void InvalidateSelectionArea()
		{
			this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004E28C File Offset: 0x0004C48C
		internal string GetSelection()
		{
			if (this.selection_start.pos == this.selection_end.pos && this.selection_start.line == this.selection_end.line)
			{
				return string.Empty;
			}
			if (this.selection_start.line == this.selection_end.line)
			{
				return this.selection_start.line.text.ToString(this.selection_start.pos, this.selection_end.pos - this.selection_start.pos);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int line_no = this.selection_start.line.line_no;
			int line_no2 = this.selection_end.line.line_no;
			stringBuilder.Append(this.selection_start.line.text.ToString(this.selection_start.pos, this.selection_start.line.text.Length - this.selection_start.pos));
			if (line_no + 1 < line_no2)
			{
				for (int i = line_no + 1; i < line_no2; i++)
				{
					stringBuilder.Append(this.GetLine(i).text.ToString());
				}
			}
			stringBuilder.Append(this.selection_end.line.text.ToString(0, this.selection_end.pos));
			return stringBuilder.ToString();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		internal void ReplaceSelection(string s, bool select_new)
		{
			int num = this.LineTagToCharIndex(this.selection_start.line, this.selection_start.pos);
			this.SuspendRecalc();
			if (this.selection_start.pos != this.selection_end.pos || this.selection_start.line != this.selection_end.line)
			{
				if (this.selection_start.line == this.selection_end.line)
				{
					this.undo.RecordDeleteString(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
					this.DeleteChars(this.selection_start.line, this.selection_start.pos, this.selection_end.pos - this.selection_start.pos);
					this.selection_start.tag = this.selection_start.line.FindTag(this.selection_start.pos + 1);
				}
				else
				{
					int num2 = this.selection_start.line.line_no;
					int line_no = this.selection_end.line.line_no;
					this.undo.RecordDeleteString(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
					this.InvalidateLinesAfter(this.selection_start.line);
					this.DeleteChars(this.selection_start.line, this.selection_start.pos, this.selection_start.line.text.Length - this.selection_start.pos);
					this.selection_start.line.recalc = true;
					this.DeleteChars(this.selection_end.line, 0, this.selection_end.pos);
					num2++;
					if (num2 < line_no)
					{
						for (int i = line_no - 1; i >= num2; i--)
						{
							this.Delete(i);
						}
					}
					this.Combine(this.selection_start.line.line_no, num2);
				}
			}
			this.Insert(this.selection_start.line, this.selection_start.pos, false, s);
			this.undo.RecordInsertString(this.selection_start.line, this.selection_start.pos, s);
			this.ResumeRecalc(false);
			Line line = this.selection_start.line;
			int pos = this.selection_start.pos;
			if (!select_new)
			{
				this.CharIndexToLineTag(num + s.Length, out this.selection_start.line, out this.selection_start.tag, out this.selection_start.pos);
				this.selection_end.line = this.selection_start.line;
				this.selection_end.pos = this.selection_start.pos;
				this.selection_end.tag = this.selection_start.tag;
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_anchor.tag = this.selection_start.tag;
				this.SetSelectionVisible(false);
			}
			else
			{
				this.CharIndexToLineTag(num, out this.selection_start.line, out this.selection_start.tag, out this.selection_start.pos);
				this.CharIndexToLineTag(num + s.Length, out this.selection_end.line, out this.selection_end.tag, out this.selection_end.pos);
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_anchor.tag = this.selection_start.tag;
				this.SetSelectionVisible(true);
			}
			this.PositionCaret(this.selection_start.line, this.selection_start.pos);
			this.UpdateView(line, this.selection_end.line.line_no - line.line_no, pos);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004E814 File Offset: 0x0004CA14
		internal void CharIndexToLineTag(int index, out Line line_out, out LineTag tag_out, out int pos)
		{
			int num = 0;
			LineTag lineTag;
			for (int i = 1; i <= this.lines; i++)
			{
				Line line = this.GetLine(i);
				int num2 = num;
				num += line.text.Length;
				if (index <= num)
				{
					lineTag = line.tags;
					while (lineTag != null)
					{
						if (index < num2 + lineTag.Start + lineTag.Length - 1)
						{
							line_out = line;
							tag_out = LineTag.GetFinalTag(lineTag);
							pos = index - num2;
							return;
						}
						if (lineTag.Next == null)
						{
							Line line2 = this.GetLine(line.line_no + 1);
							if (line2 != null)
							{
								line_out = line2;
								tag_out = LineTag.GetFinalTag(line2.tags);
								pos = 0;
								return;
							}
							line_out = line;
							tag_out = LineTag.GetFinalTag(lineTag);
							pos = line_out.text.Length;
							return;
						}
						else
						{
							lineTag = lineTag.Next;
						}
					}
				}
			}
			line_out = this.GetLine(this.lines);
			lineTag = line_out.tags;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			tag_out = lineTag;
			pos = line_out.text.Length;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004E91C File Offset: 0x0004CB1C
		internal int LineTagToCharIndex(Line line, int pos)
		{
			int num = 0;
			for (int i = 1; i < line.line_no; i++)
			{
				num += this.GetLine(i).text.Length;
			}
			return num + pos;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0004E958 File Offset: 0x0004CB58
		internal int SelectionLength()
		{
			if (this.selection_start.pos == this.selection_end.pos && this.selection_start.line == this.selection_end.line)
			{
				return 0;
			}
			if (this.selection_start.line == this.selection_end.line)
			{
				return this.selection_end.pos - this.selection_start.pos;
			}
			int num = this.selection_start.line.text.Length - this.selection_start.pos + this.selection_end.pos + this.crlf_size;
			int num2 = this.selection_start.line.line_no + 1;
			int line_no = this.selection_end.line.line_no;
			if (num2 < line_no)
			{
				for (int i = num2; i < line_no; i++)
				{
					Line line = this.GetLine(i);
					num += line.text.Length + this.LineEndingLength(line.ending);
				}
			}
			return num;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004EA58 File Offset: 0x0004CC58
		internal Line GetLine(int LineNo)
		{
			Line line = this.document;
			while (line != this.sentinel)
			{
				if (LineNo == line.line_no)
				{
					return line;
				}
				if (LineNo < line.line_no)
				{
					line = line.left;
				}
				else
				{
					line = line.right;
				}
			}
			return null;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004EA9C File Offset: 0x0004CC9C
		internal Line GetLineByPixel(int offset, bool exact)
		{
			Line line = this.document;
			Line line2 = null;
			if (this.multiline)
			{
				while (line != this.sentinel)
				{
					line2 = line;
					if (offset >= line.Y && offset < line.Y + line.height)
					{
						return line;
					}
					if (offset < line.Y)
					{
						line = line.left;
					}
					else
					{
						line = line.right;
					}
				}
			}
			else
			{
				while (line != this.sentinel)
				{
					line2 = line;
					if (offset >= line.X && offset < line.X + line.Width)
					{
						return line;
					}
					if (offset < line.X)
					{
						line = line.left;
					}
					else
					{
						line = line.right;
					}
				}
			}
			if (exact)
			{
				return null;
			}
			return line2;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004EB44 File Offset: 0x0004CD44
		internal LineTag FindCursor(int x, int y, out int index)
		{
			x -= this.offset_x;
			y -= this.offset_y;
			Line lineByPixel = this.GetLineByPixel(this.multiline ? y : x, false);
			LineTag tag = lineByPixel.GetTag(x);
			if (tag.Length == 0 && tag.Start == 1)
			{
				index = 0;
			}
			else
			{
				index = tag.GetCharIndex(x - lineByPixel.align_shift);
			}
			return tag;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0004EBA8 File Offset: 0x0004CDA8
		internal void FormatText(Line start_line, int start_pos, Line end_line, int end_pos, global::System.Drawing.Font font, global::System.Drawing.Color color, global::System.Drawing.Color back_color, FormatSpecified specified)
		{
			if (start_line != end_line)
			{
				LineTag.FormatText(start_line, start_pos, start_line.text.Length - start_pos + 1, font, color, back_color, specified);
				LineTag.FormatText(end_line, 1, end_pos, font, color, back_color, specified);
				for (int i = start_line.line_no + 1; i < end_line.line_no; i++)
				{
					Line line = this.GetLine(i);
					LineTag.FormatText(line, 1, line.text.Length, font, color, back_color, specified);
				}
				return;
			}
			LineTag.FormatText(start_line, start_pos, end_pos - start_pos, font, color, back_color, specified);
			if (end_pos - start_pos == 0 && this.CaretTag.Length != 0)
			{
				this.CaretTag = this.CaretTag.Next;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004EC60 File Offset: 0x0004CE60
		internal void RecalculateAlignments()
		{
			for (int i = 1; i <= this.lines; i++)
			{
				Line line = this.GetLine(i);
				if (line != null)
				{
					switch (line.alignment)
					{
					case HorizontalAlignment.Left:
						line.align_shift = 0;
						break;
					case HorizontalAlignment.Right:
						line.align_shift = this.viewport_width - (int)line.widths[line.text.Length] - this.right_margin;
						break;
					case HorizontalAlignment.Center:
						line.align_shift = (this.viewport_width - (int)line.widths[line.text.Length]) / 2;
						break;
					}
				}
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004ECFB File Offset: 0x0004CEFB
		internal bool RecalculateDocument(Graphics g)
		{
			return this.RecalculateDocument(g, 1, this.lines, false);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		internal bool RecalculateDocument(Graphics g, int start, int end, bool optimize)
		{
			if (this.recalc_suspended > 0)
			{
				this.recalc_pending = true;
				this.recalc_start = Math.Min(this.recalc_start, start);
				this.recalc_end = Math.Max(this.recalc_end, end);
				this.recalc_optimize = optimize;
				return false;
			}
			start = Math.Max(start, 1);
			end = Math.Min(end, this.lines);
			int num = this.GetLine(start).offset;
			int i = start;
			int num2 = 0;
			int num3 = this.lines;
			bool flag = !optimize;
			Line line;
			while (i <= end + this.lines - num3)
			{
				line = this.GetLine(i++);
				line.offset = num;
				if (!this.calc_pass)
				{
					if (!optimize)
					{
						line.RecalculateLine(g, this);
					}
					else if (line.recalc && line.RecalculateLine(g, this))
					{
						flag = true;
						end = this.lines;
						num3 = this.lines;
					}
				}
				else if (!optimize)
				{
					line.RecalculatePasswordLine(g, this);
				}
				else if (line.recalc && line.RecalculatePasswordLine(g, this))
				{
					flag = true;
					end = this.lines;
					num3 = this.lines;
				}
				if (line.widths[line.text.Length] > (float)num2)
				{
					num2 = (int)line.widths[line.text.Length];
				}
				if (line.alignment != HorizontalAlignment.Left)
				{
					if (line.alignment == HorizontalAlignment.Center)
					{
						line.align_shift = (this.viewport_width - (int)line.widths[line.text.Length]) / 2;
					}
					else
					{
						line.align_shift = this.viewport_width - (int)line.widths[line.text.Length] - 1;
					}
				}
				if (this.multiline)
				{
					num += line.height;
				}
				else
				{
					num += (int)line.widths[line.text.Length];
				}
				if (i > this.lines)
				{
					break;
				}
			}
			if (this.document_x != num2)
			{
				this.document_x = num2;
				if (this.WidthChanged != null)
				{
					this.WidthChanged(this, null);
				}
			}
			this.RecalculateAlignments();
			line = this.GetLine(this.lines);
			if (this.document_y != line.Y + line.height)
			{
				this.document_y = line.Y + line.height;
				if (this.HeightChanged != null)
				{
					this.HeightChanged(this, null);
				}
			}
			if (this.EnableLinks)
			{
				this.ScanForLinks(start, end, ref flag);
			}
			this.UpdateCaret();
			return flag;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0004EF73 File Offset: 0x0004D173
		private void owner_HandleCreated(object sender, EventArgs e)
		{
			this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			this.AlignCaret();
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0004EF8D File Offset: 0x0004D18D
		private void owner_VisibleChanged(object sender, EventArgs e)
		{
			if (this.owner.Visible)
			{
				this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0004EFAE File Offset: 0x0004D1AE
		internal static bool IsWordSeparator(char ch)
		{
			if (ch <= ' ')
			{
				switch (ch)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					return false;
				default:
					if (ch != ' ')
					{
						return false;
					}
					break;
				}
			}
			else if (ch != '(' && ch != ')')
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004EFE8 File Offset: 0x0004D1E8
		internal int FindWordSeparator(Line line, int pos, bool forward)
		{
			int length = line.text.Length;
			if (forward)
			{
				for (int i = pos + 1; i < length; i++)
				{
					if (Document.IsWordSeparator(line.Text[i]))
					{
						return i + 1;
					}
				}
				return length;
			}
			for (int j = pos - 1; j > 0; j--)
			{
				if (Document.IsWordSeparator(line.Text[j - 1]))
				{
					return j;
				}
			}
			return 0;
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600101B RID: 4123 RVA: 0x0004F054 File Offset: 0x0004D254
		// (remove) Token: 0x0600101C RID: 4124 RVA: 0x0004F08C File Offset: 0x0004D28C
		internal event EventHandler WidthChanged;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600101D RID: 4125 RVA: 0x0004F0C4 File Offset: 0x0004D2C4
		// (remove) Token: 0x0600101E RID: 4126 RVA: 0x0004F0FC File Offset: 0x0004D2FC
		internal event EventHandler HeightChanged;

		// Token: 0x0600101F RID: 4127 RVA: 0x00003C7A File Offset: 0x00001E7A
		public IEnumerator GetEnumerator()
		{
			return null;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004F131 File Offset: 0x0004D331
		public override bool Equals(object obj)
		{
			return obj != null && obj is Document && (obj == this || this.ToString().Equals(((Document)obj).ToString()));
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004F163 File Offset: 0x0004D363
		public override int GetHashCode()
		{
			return this.document_id;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004F16B File Offset: 0x0004D36B
		public override string ToString()
		{
			return "document " + this.document_id;
		}

		// Token: 0x04000AA7 RID: 2727
		private Line document;

		// Token: 0x04000AA8 RID: 2728
		private int lines;

		// Token: 0x04000AA9 RID: 2729
		private Line sentinel;

		// Token: 0x04000AAA RID: 2730
		private int document_id;

		// Token: 0x04000AAB RID: 2731
		private Random random = new Random();

		// Token: 0x04000AAC RID: 2732
		internal string password_char;

		// Token: 0x04000AAD RID: 2733
		private StringBuilder password_cache;

		// Token: 0x04000AAE RID: 2734
		private bool calc_pass;

		// Token: 0x04000AAF RID: 2735
		private int char_count;

		// Token: 0x04000AB0 RID: 2736
		private bool enable_links;

		// Token: 0x04000AB1 RID: 2737
		public static readonly StringFormat string_format = new StringFormat(StringFormat.GenericTypographic);

		// Token: 0x04000AB2 RID: 2738
		private int recalc_suspended;

		// Token: 0x04000AB3 RID: 2739
		private bool recalc_pending;

		// Token: 0x04000AB4 RID: 2740
		private int recalc_start = 1;

		// Token: 0x04000AB5 RID: 2741
		private int recalc_end;

		// Token: 0x04000AB6 RID: 2742
		private bool recalc_optimize;

		// Token: 0x04000AB7 RID: 2743
		private int update_suspended;

		// Token: 0x04000AB8 RID: 2744
		private bool update_pending;

		// Token: 0x04000AB9 RID: 2745
		private int update_start = 1;

		// Token: 0x04000ABA RID: 2746
		internal bool multiline;

		// Token: 0x04000ABB RID: 2747
		internal HorizontalAlignment alignment;

		// Token: 0x04000ABC RID: 2748
		internal bool wrap;

		// Token: 0x04000ABD RID: 2749
		internal UndoManager undo;

		// Token: 0x04000ABE RID: 2750
		internal Document.Marker caret;

		// Token: 0x04000ABF RID: 2751
		internal Document.Marker selection_start;

		// Token: 0x04000AC0 RID: 2752
		internal Document.Marker selection_end;

		// Token: 0x04000AC1 RID: 2753
		internal bool selection_visible;

		// Token: 0x04000AC2 RID: 2754
		internal Document.Marker selection_anchor;

		// Token: 0x04000AC3 RID: 2755
		internal Document.Marker selection_prev;

		// Token: 0x04000AC4 RID: 2756
		internal bool selection_end_anchor;

		// Token: 0x04000AC5 RID: 2757
		internal int viewport_x;

		// Token: 0x04000AC6 RID: 2758
		internal int viewport_y;

		// Token: 0x04000AC7 RID: 2759
		internal int offset_x;

		// Token: 0x04000AC8 RID: 2760
		internal int offset_y;

		// Token: 0x04000AC9 RID: 2761
		internal int viewport_width;

		// Token: 0x04000ACA RID: 2762
		internal int viewport_height;

		// Token: 0x04000ACB RID: 2763
		internal int document_x;

		// Token: 0x04000ACC RID: 2764
		internal int document_y;

		// Token: 0x04000ACD RID: 2765
		internal int crlf_size;

		// Token: 0x04000ACE RID: 2766
		internal TextBoxBase owner;

		// Token: 0x04000ACF RID: 2767
		internal static int caret_width = 1;

		// Token: 0x04000AD0 RID: 2768
		internal static int caret_shift = 1;

		// Token: 0x04000AD1 RID: 2769
		internal int left_margin = 2;

		// Token: 0x04000AD2 RID: 2770
		internal int top_margin = 2;

		// Token: 0x04000AD3 RID: 2771
		internal int right_margin = 2;

		// Token: 0x04000AD4 RID: 2772
		[CompilerGenerated]
		private EventHandler CaretMoved;

		// Token: 0x04000AD7 RID: 2775
		[CompilerGenerated]
		private EventHandler LengthChanged;

		// Token: 0x04000AD8 RID: 2776
		[CompilerGenerated]
		private EventHandler UIASelectionChanged;

		// Token: 0x0200019C RID: 412
		internal struct Marker
		{
			// Token: 0x06001024 RID: 4132 RVA: 0x0004F1A0 File Offset: 0x0004D3A0
			public static bool operator <(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no < rhs.line.line_no || (lhs.line.line_no == rhs.line.line_no && lhs.pos < rhs.pos);
			}

			// Token: 0x06001025 RID: 4133 RVA: 0x0004F1F0 File Offset: 0x0004D3F0
			public static bool operator >(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no > rhs.line.line_no || (lhs.line.line_no == rhs.line.line_no && lhs.pos > rhs.pos);
			}

			// Token: 0x06001026 RID: 4134 RVA: 0x0004F240 File Offset: 0x0004D440
			public static bool operator ==(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no == rhs.line.line_no && lhs.pos == rhs.pos;
			}

			// Token: 0x06001027 RID: 4135 RVA: 0x0004F26B File Offset: 0x0004D46B
			public static bool operator !=(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no != rhs.line.line_no || lhs.pos != rhs.pos;
			}

			// Token: 0x06001028 RID: 4136 RVA: 0x0004F296 File Offset: 0x0004D496
			public void Combine(Line move_to_line, int move_to_line_length)
			{
				this.line = move_to_line;
				this.pos += move_to_line_length;
				this.tag = LineTag.FindTag(this.line, this.pos);
			}

			// Token: 0x06001029 RID: 4137 RVA: 0x0004F2C4 File Offset: 0x0004D4C4
			public override bool Equals(object obj)
			{
				return this == (Document.Marker)obj;
			}

			// Token: 0x0600102A RID: 4138 RVA: 0x0004F2D7 File Offset: 0x0004D4D7
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x0600102B RID: 4139 RVA: 0x0004F2E9 File Offset: 0x0004D4E9
			public override string ToString()
			{
				return string.Concat(new object[] { "Marker Line ", this.line, ", Position ", this.pos });
			}

			// Token: 0x04000AD9 RID: 2777
			internal Line line;

			// Token: 0x04000ADA RID: 2778
			internal LineTag tag;

			// Token: 0x04000ADB RID: 2779
			internal int pos;

			// Token: 0x04000ADC RID: 2780
			internal int height;
		}
	}
}

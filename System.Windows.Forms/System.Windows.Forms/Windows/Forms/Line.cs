using System;
using System.Collections;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000100 RID: 256
	internal class Line : ICloneable, IComparable
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0002579C File Offset: 0x0002399C
		internal Line(Document document, LineEnding ending)
		{
			this.document = document;
			this.color = LineColor.Red;
			this.left = null;
			this.right = null;
			this.parent = null;
			this.text = null;
			this.recalc = true;
			this.alignment = document.alignment;
			this.ending = ending;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000257F4 File Offset: 0x000239F4
		internal Line(Document document, int LineNo, string Text, HorizontalAlignment align, Font font, Color color, LineEnding ending)
			: this(document, ending)
		{
			this.space = ((Text.Length > Line.DEFAULT_TEXT_LEN) ? (Text.Length + 1) : Line.DEFAULT_TEXT_LEN);
			this.text = new StringBuilder(Text, this.space);
			this.line_no = LineNo;
			this.ending = ending;
			this.alignment = align;
			this.widths = new float[this.space + 1];
			this.tags = new LineTag(this, 1);
			this.tags.Font = font;
			this.tags.Color = color;
		}

		// Token: 0x1700022F RID: 559
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0002588F File Offset: 0x00023A8F
		internal HorizontalAlignment Alignment
		{
			set
			{
				if (this.alignment != value)
				{
					this.alignment = value;
					this.recalc = true;
				}
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000258A8 File Offset: 0x00023AA8
		internal int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000258B0 File Offset: 0x00023AB0
		internal int LineNo
		{
			get
			{
				return this.line_no;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000258B8 File Offset: 0x00023AB8
		internal int Width
		{
			get
			{
				return (int)this.widths[this.text.Length];
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000258CD File Offset: 0x00023ACD
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x000258DC File Offset: 0x00023ADC
		internal string Text
		{
			get
			{
				return this.text.ToString();
			}
			set
			{
				int length = this.text.Length;
				this.text = new StringBuilder(value, (value.Length > Line.DEFAULT_TEXT_LEN) ? (value.Length + 1) : Line.DEFAULT_TEXT_LEN);
				if (this.text.Length > length)
				{
					this.Grow(this.text.Length - length);
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002593E File Offset: 0x00023B3E
		internal int X
		{
			get
			{
				if (this.document.multiline)
				{
					return this.align_shift;
				}
				return this.offset + this.align_shift;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00025961 File Offset: 0x00023B61
		internal int Y
		{
			get
			{
				if (!this.document.multiline)
				{
					return this.document.top_margin;
				}
				return this.document.top_margin + this.offset;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00025990 File Offset: 0x00023B90
		internal void LinkRecord(StringBuilder linkRecord)
		{
			for (LineTag next = this.tags; next != null; next = next.Next)
			{
				if (next.IsLink)
				{
					linkRecord.Append("L");
				}
				else
				{
					linkRecord.Append("N");
				}
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000259D4 File Offset: 0x00023BD4
		internal void ClearLinks()
		{
			for (LineTag next = this.tags; next != null; next = next.Next)
			{
				next.IsLink = false;
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000259FC File Offset: 0x00023BFC
		public void DeleteCharacters(int pos, int count)
		{
			bool flag = false;
			if (pos >= this.text.Length)
			{
				return;
			}
			LineTag lineTag = this.FindTag(pos + 1);
			this.text.Remove(pos, count);
			if (lineTag == null)
			{
				return;
			}
			if (pos + count > lineTag.Start + lineTag.Length - 1)
			{
				flag = true;
				int num = count - (lineTag.Start + lineTag.Length - pos - 1);
				lineTag = lineTag.Next;
				while (lineTag != null)
				{
					if (num <= 0)
					{
						break;
					}
					int length = lineTag.Length;
					lineTag.Start -= count - num;
					if (length > num)
					{
						num = 0;
					}
					else
					{
						num -= length;
						lineTag = lineTag.Next;
					}
				}
			}
			else if (lineTag.Length == 0)
			{
				flag = true;
			}
			LineTag lineTag2 = lineTag;
			while (lineTag2 != null && lineTag2.Next != null && lineTag2.Next.Length == 0)
			{
				LineTag lineTag3 = lineTag2;
				lineTag2.Next = lineTag2.Next.Next;
				if (lineTag2.Next != null)
				{
					lineTag2.Next.Previous = lineTag3;
				}
				lineTag2 = lineTag2.Next;
			}
			if (lineTag != null)
			{
				for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= count;
				}
			}
			this.recalc = true;
			if (flag)
			{
				this.Streamline(this.document.Lines);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00025B38 File Offset: 0x00023D38
		internal void DrawEnding(Graphics dc, float y)
		{
			if (this.document.multiline)
			{
				return;
			}
			LineTag next = this.tags;
			while (next.Next != null)
			{
				next = next.Next;
			}
			string text = null;
			switch (this.document.LineEndingLength(this.ending))
			{
			case 0:
				return;
			case 1:
				text = "\u0013";
				break;
			case 2:
				text = "\u0013\u0013";
				break;
			case 3:
				text = "\u0013\u0013\u0013";
				break;
			}
			TextBoxTextRenderer.DrawText(dc, text, next.Font, next.Color, (float)this.X + this.widths[this.TextLengthWithoutEnding()] - (float)this.document.viewport_x + (float)this.document.OffsetX, y, true);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00025BF4 File Offset: 0x00023DF4
		internal LineTag FindTag(int pos)
		{
			if (pos == 0)
			{
				return this.tags;
			}
			LineTag next = this.tags;
			if (pos >= this.text.Length)
			{
				pos = this.text.Length - 1;
			}
			while (next != null)
			{
				if (next.Start - 1 <= pos && pos <= next.Start + next.Length - 1)
				{
					return LineTag.GetFinalTag(next);
				}
				next = next.Next;
			}
			return null;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00025C60 File Offset: 0x00023E60
		public LineTag GetTag(int x)
		{
			LineTag next = this.tags;
			if ((float)x < next.X)
			{
				return LineTag.GetFinalTag(next);
			}
			while ((float)x < next.X || (float)x >= next.X + next.Width)
			{
				if (next.Next == null)
				{
					return LineTag.GetFinalTag(next);
				}
				next = next.Next;
			}
			return next;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00025CBC File Offset: 0x00023EBC
		internal void Grow(int minimum)
		{
			int length = this.text.Length;
			if (length + minimum > this.space)
			{
				float[] array;
				if (length + minimum > this.space * 2)
				{
					array = new float[length + minimum * 2 + 1];
					this.space = length + minimum * 2;
				}
				else
				{
					array = new float[this.space * 2 + 1];
					this.space *= 2;
				}
				this.widths.CopyTo(array, 0);
				this.widths = array;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00025D39 File Offset: 0x00023F39
		public void InsertString(int pos, string s)
		{
			this.InsertString(pos, s, this.FindTag(pos));
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00025D4C File Offset: 0x00023F4C
		public void InsertString(int pos, string s, LineTag tag)
		{
			int length = s.Length;
			this.text.Insert(pos, s);
			for (tag = tag.Next; tag != null; tag = tag.Next)
			{
				tag.Start += length;
			}
			this.Grow(length);
			this.recalc = true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00025D9F File Offset: 0x00023F9F
		internal bool RecalculateLine(Graphics g, Document doc)
		{
			return this.RecalculateLine(g, doc, Line.kerning_fonts.ContainsKey(this.tags.Font.GetHashCode()));
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00025DC8 File Offset: 0x00023FC8
		private bool RecalculateLine(Graphics g, Document doc, bool handleKerning)
		{
			int i = 0;
			int num = this.text.Length;
			LineTag lineTag = this.tags;
			int num2 = this.offset;
			int num3 = this.height;
			int num4 = this.ascent;
			this.height = 0;
			this.ascent = 0;
			lineTag.Shift = 0;
			if (this.ending == LineEnding.Wrap)
			{
				this.widths[0] = (float)(this.document.left_margin + this.hanging_indent);
			}
			else
			{
				this.widths[0] = (float)(this.document.left_margin + this.indent);
			}
			this.recalc = false;
			bool flag = false;
			bool flag2 = false;
			int num5 = 0;
			while (i < num)
			{
				while (lineTag.Length == 0)
				{
					lineTag.Shift = (lineTag.Line.ascent - lineTag.Ascent) / 72;
					lineTag = lineTag.Next;
				}
				float num6;
				if (handleKerning && !char.IsWhiteSpace(this.text[i]))
				{
					SizeF sizeF = TextBoxTextRenderer.MeasureText(g, this.text.ToString(0, i + 1), lineTag.Font);
					num6 = this.widths[0] + sizeF.Width;
				}
				else
				{
					float width = lineTag.SizeOfPosition(g, i).Width;
					num6 = this.widths[i] + width;
				}
				if (char.IsWhiteSpace(this.text[i]))
				{
					num5 = i + 1;
				}
				if (doc.wrap)
				{
					if (num5 > 0 && num5 != num && num6 + 5f > (float)(doc.viewport_width - this.right_indent))
					{
						this.widths[i + 1] = num6;
						i = num5;
						num = this.text.Length;
						doc.Split(this, lineTag, i);
						this.ending = LineEnding.Wrap;
						num = this.text.Length;
						flag = true;
						flag2 = true;
					}
					else if (i > 1 && num6 > (float)(doc.viewport_width - this.right_indent))
					{
						this.widths[i + 1] = num6;
						doc.Split(this, lineTag, i);
						this.ending = LineEnding.Wrap;
						num = this.text.Length;
						flag = true;
						flag2 = true;
					}
				}
				if (!flag2)
				{
					i++;
					this.widths[i] = num6;
					if (i == num && doc.GetLine(this.line_no + 1) != null && (this.ending == LineEnding.Wrap || this.ending == LineEnding.None))
					{
						doc.Combine(this.line_no, this.line_no + 1);
						num = this.text.Length;
						flag = true;
					}
				}
				if (i == lineTag.Start - 1 + lineTag.Length)
				{
					lineTag.Height = lineTag.MaxHeight();
					if (lineTag.Height > this.height)
					{
						this.height = lineTag.Height;
					}
					if (lineTag.Ascent > this.ascent)
					{
						LineTag next = this.tags;
						while (next != null && next != lineTag)
						{
							next.Shift = (lineTag.Ascent - next.Ascent) / 72;
							next = next.Next;
						}
						this.ascent = lineTag.Ascent;
					}
					else
					{
						lineTag.Shift = (this.ascent - lineTag.Ascent) / 72;
					}
					lineTag = lineTag.Next;
					if (lineTag != null)
					{
						lineTag.Shift = 0;
						num5 = i;
					}
				}
			}
			string text = this.text.ToString();
			if (!handleKerning && text.Length > 1 && !flag2)
			{
				float num7 = TextBoxTextRenderer.MeasureText(g, text, this.tags.Font).Width + this.widths[0];
				int length = text.TrimEnd(Array.Empty<char>()).Length;
				float num8 = this.widths[length];
				if (num7 != num8)
				{
					Line.kerning_fonts.Add(this.tags.Font.GetHashCode(), true);
				}
			}
			while (lineTag != null)
			{
				lineTag.Shift = (lineTag.Line.ascent - lineTag.Ascent) / 72;
				lineTag = lineTag.Next;
			}
			if (this.height == 0)
			{
				this.height = this.tags.Font.Height;
				this.tags.Height = this.height;
				this.tags.Shift = 0;
			}
			if (num2 != this.offset || num3 != this.height || num4 != this.ascent)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000261F4 File Offset: 0x000243F4
		internal bool RecalculatePasswordLine(Graphics g, Document doc)
		{
			int i = 0;
			int length = this.text.Length;
			LineTag lineTag = this.tags;
			this.ascent = 0;
			lineTag.Shift = 0;
			this.recalc = false;
			this.widths[0] = (float)(this.document.left_margin + this.indent);
			float width = TextBoxTextRenderer.MeasureText(g, doc.password_char, this.tags.Font).Width;
			bool flag = this.height != lineTag.Font.Height;
			this.height = lineTag.Font.Height;
			lineTag.Height = this.height;
			this.ascent = lineTag.Ascent;
			while (i < length)
			{
				i++;
				this.widths[i] = this.widths[i - 1] + width;
			}
			return flag;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000262CC File Offset: 0x000244CC
		internal void Streamline(int lines)
		{
			LineTag lineTag = this.tags;
			LineTag lineTag2 = lineTag.Next;
			while (lineTag.Length == 0 && lineTag2 != null && lineTag2.IsTextTag)
			{
				this.tags = lineTag2;
				this.tags.Previous = null;
				lineTag = lineTag2;
				lineTag2 = lineTag.Next;
			}
			if (lineTag2 == null)
			{
				return;
			}
			while (lineTag2 != null)
			{
				if (lineTag.IsTextTag && lineTag2.Length == 0 && lineTag2.IsTextTag && (lineTag2.Next != null || this.line_no != lines))
				{
					lineTag.Next = lineTag2.Next;
					if (lineTag.Next != null)
					{
						lineTag.Next.Previous = lineTag;
					}
					lineTag2 = lineTag.Next;
				}
				else if (lineTag.Combine(lineTag2))
				{
					lineTag2 = lineTag.Next;
				}
				else
				{
					lineTag = lineTag.Next;
					lineTag2 = lineTag.Next;
				}
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00026391 File Offset: 0x00024591
		internal int TextLengthWithoutEnding()
		{
			return this.text.Length - this.document.LineEndingLength(this.ending);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000263B0 File Offset: 0x000245B0
		internal string TextWithoutEnding()
		{
			return this.text.ToString(0, this.text.Length - this.document.LineEndingLength(this.ending));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000263DC File Offset: 0x000245DC
		public object Clone()
		{
			Line line = new Line(this.document, this.ending);
			line.text = this.text;
			if (this.left != null)
			{
				line.left = (Line)this.left.Clone();
			}
			if (this.left != null)
			{
				line.left = (Line)this.left.Clone();
			}
			return line;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00026444 File Offset: 0x00024644
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Line))
			{
				throw new ArgumentException("Object is not of type Line", "obj");
			}
			if (this.line_no < ((Line)obj).line_no)
			{
				return -1;
			}
			if (this.line_no > ((Line)obj).line_no)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00026499 File Offset: 0x00024699
		public override bool Equals(object obj)
		{
			return obj != null && obj is Line && (obj == this || this.line_no == ((Line)obj).line_no);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000264C6 File Offset: 0x000246C6
		public override string ToString()
		{
			return string.Format("Line {0}", this.line_no);
		}

		// Token: 0x04000660 RID: 1632
		internal Document document;

		// Token: 0x04000661 RID: 1633
		internal StringBuilder text;

		// Token: 0x04000662 RID: 1634
		internal float[] widths;

		// Token: 0x04000663 RID: 1635
		internal int space;

		// Token: 0x04000664 RID: 1636
		internal int line_no;

		// Token: 0x04000665 RID: 1637
		internal LineTag tags;

		// Token: 0x04000666 RID: 1638
		internal int offset;

		// Token: 0x04000667 RID: 1639
		internal int height;

		// Token: 0x04000668 RID: 1640
		internal int ascent;

		// Token: 0x04000669 RID: 1641
		internal HorizontalAlignment alignment;

		// Token: 0x0400066A RID: 1642
		internal int align_shift;

		// Token: 0x0400066B RID: 1643
		internal int indent;

		// Token: 0x0400066C RID: 1644
		internal int hanging_indent;

		// Token: 0x0400066D RID: 1645
		internal int right_indent;

		// Token: 0x0400066E RID: 1646
		internal LineEnding ending;

		// Token: 0x0400066F RID: 1647
		internal Line parent;

		// Token: 0x04000670 RID: 1648
		internal Line left;

		// Token: 0x04000671 RID: 1649
		internal Line right;

		// Token: 0x04000672 RID: 1650
		internal LineColor color;

		// Token: 0x04000673 RID: 1651
		private static int DEFAULT_TEXT_LEN = 0;

		// Token: 0x04000674 RID: 1652
		internal bool recalc;

		// Token: 0x04000675 RID: 1653
		private static Hashtable kerning_fonts = new Hashtable();
	}
}

using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000101 RID: 257
	internal class LineTag
	{
		// Token: 0x060008FE RID: 2302 RVA: 0x000264EF File Offset: 0x000246EF
		public LineTag(Line line, int start)
		{
			this.line = line;
			this.Start = start;
			this.link_font = null;
			this.is_link = false;
			this.link_text = null;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0002651A File Offset: 0x0002471A
		public int Ascent
		{
			get
			{
				return this.ascent;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00026522 File Offset: 0x00024722
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0002652A File Offset: 0x0002472A
		public Color ColorToDisplay
		{
			get
			{
				if (this.IsLink)
				{
					return Color.Blue;
				}
				return this.color;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00026540 File Offset: 0x00024740
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00026548 File Offset: 0x00024748
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00026551 File Offset: 0x00024751
		public int End
		{
			get
			{
				return this.start + this.Length;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00026560 File Offset: 0x00024760
		public Font FontToDisplay
		{
			get
			{
				if (this.IsLink)
				{
					if (this.link_font == null)
					{
						this.link_font = new Font(this.font.FontFamily, this.font.Size, this.font.Style | FontStyle.Underline);
					}
					return this.link_font;
				}
				return this.font;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x000265B8 File Offset: 0x000247B8
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x000265C0 File Offset: 0x000247C0
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				if (this.font != value)
				{
					this.link_font = null;
					this.font = value;
					this.height = this.Font.Height;
					XplatUI.GetFontMetrics(Hwnd.GraphicsContext, this.Font, out this.ascent, out this.descent);
					this.line.recalc = true;
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0002661E File Offset: 0x0002481E
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00026626 File Offset: 0x00024826
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00006F54 File Offset: 0x00005154
		public virtual bool IsTextTag
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00026630 File Offset: 0x00024830
		public int Length
		{
			get
			{
				int num;
				if (this.next != null)
				{
					num = this.next.start - this.start;
				}
				else
				{
					num = this.line.text.Length - (this.start - 1);
				}
				if (num <= 0)
				{
					return 0;
				}
				return num;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0002667D File Offset: 0x0002487D
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00026685 File Offset: 0x00024885
		public Line Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0002668E File Offset: 0x0002488E
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x00026696 File Offset: 0x00024896
		public LineTag Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002669F File Offset: 0x0002489F
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x000266A7 File Offset: 0x000248A7
		public LineTag Previous
		{
			get
			{
				return this.previous;
			}
			set
			{
				this.previous = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000266B0 File Offset: 0x000248B0
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x000266B8 File Offset: 0x000248B8
		public int Shift
		{
			get
			{
				return this.shift;
			}
			set
			{
				this.shift = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000266C1 File Offset: 0x000248C1
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x000266C9 File Offset: 0x000248C9
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000266D4 File Offset: 0x000248D4
		public float Width
		{
			get
			{
				if (this.Length == 0)
				{
					return 0f;
				}
				return this.line.widths[this.start + this.Length - 1] - ((this.start != 0) ? this.line.widths[this.start - 1] : 0f);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002672E File Offset: 0x0002492E
		public float X
		{
			get
			{
				if (this.start == 0)
				{
					return (float)this.line.X;
				}
				return (float)this.line.X + this.line.widths[this.start - 1];
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00026766 File Offset: 0x00024966
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0002676E File Offset: 0x0002496E
		public bool IsLink
		{
			get
			{
				return this.is_link;
			}
			set
			{
				this.is_link = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00026777 File Offset: 0x00024977
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0002677F File Offset: 0x0002497F
		public string LinkText
		{
			get
			{
				return this.link_text;
			}
			set
			{
				this.link_text = value;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00026788 File Offset: 0x00024988
		public LineTag Break(int pos)
		{
			LineTag lineTag = new LineTag(this.line, pos);
			lineTag.CopyFormattingFrom(this);
			lineTag.next = this.next;
			this.next = lineTag;
			lineTag.previous = this;
			if (lineTag.next != null)
			{
				lineTag.next.previous = lineTag;
			}
			return lineTag;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000267D8 File Offset: 0x000249D8
		public bool Combine(LineTag other)
		{
			if (!this.Equals(other))
			{
				return false;
			}
			this.next = other.next;
			if (this.next != null)
			{
				this.next.previous = this;
			}
			return true;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00026806 File Offset: 0x00024A06
		public void CopyFormattingFrom(LineTag other)
		{
			this.Font = other.font;
			this.color = other.color;
			this.back_color = other.back_color;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002682C File Offset: 0x00024A2C
		public virtual void Draw(Graphics dc, Color color, float xoff, float y, int drawStart, int drawEnd, string text, out Rectangle measuredText, bool measureText)
		{
			if (measureText)
			{
				int num = (int)this.line.widths[drawStart] + (int)xoff;
				int num2 = (int)this.line.widths[drawEnd] - (int)this.line.widths[drawStart];
				int num3 = (int)y;
				int num4 = (int)TextBoxTextRenderer.MeasureText(dc, this.Text(), this.FontToDisplay).Height;
				measuredText = new Rectangle(num, num3, num2, num4);
			}
			else
			{
				measuredText = default(Rectangle);
			}
			while (drawStart < drawEnd)
			{
				int num5 = text.IndexOf("\t", drawStart);
				if (num5 == -1)
				{
					num5 = drawEnd;
				}
				TextBoxTextRenderer.DrawText(dc, text.Substring(drawStart, num5 - drawStart).Replace("\r", string.Empty), this.FontToDisplay, color, xoff + this.line.widths[drawStart], y, false);
				if (!this.line.document.multiline && num5 != drawEnd)
				{
					TextBoxTextRenderer.DrawText(dc, "\u0013", this.FontToDisplay, color, xoff + this.line.widths[num5], y, true);
				}
				drawStart = num5 + 1;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00026958 File Offset: 0x00024B58
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is LineTag))
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			LineTag lineTag = (LineTag)obj;
			return lineTag.IsTextTag == this.IsTextTag && this.IsLink == lineTag.IsLink && !(this.LinkText != lineTag.LinkText) && (this.font.Equals(lineTag.font) && this.color.Equals(lineTag.color));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000269EC File Offset: 0x00024BEC
		public static LineTag FindTag(Line line, int pos)
		{
			LineTag tags = line.tags;
			if (pos == 0)
			{
				return tags;
			}
			while (tags != null)
			{
				if (tags.start <= pos && pos < tags.End)
				{
					return LineTag.GetFinalTag(tags);
				}
				tags = tags.next;
			}
			return null;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00026A2C File Offset: 0x00024C2C
		public static bool FormatText(Line line, int formatStart, int length, Font font, Color color, Color backColor, FormatSpecified specified)
		{
			bool flag = false;
			if ((FormatSpecified.Font & specified) == FormatSpecified.Font && font.Height != line.height)
			{
				flag = true;
			}
			line.recalc = true;
			if (length > line.text.Length)
			{
				length = line.text.Length;
			}
			LineTag lineTag = line.tags;
			int num = formatStart + length;
			if (formatStart == 1 && length == lineTag.Length)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			if (formatStart == 1 && length == 0)
			{
				line.tags.Break(1);
				LineTag.SetFormat(line.tags, font, color, backColor, specified);
				return flag;
			}
			LineTag lineTag2 = LineTag.FindTag(line, formatStart - 1);
			if (lineTag2.End == formatStart && length == 0 && lineTag2.Next != null && lineTag2.Next.Length == 0)
			{
				LineTag.SetFormat(lineTag2.Next, font, color, backColor, specified);
				return flag;
			}
			while (lineTag2.End == formatStart && lineTag2.Next != null)
			{
				lineTag2 = lineTag2.Next;
			}
			lineTag = lineTag2.Break(formatStart);
			if (lineTag.Length == 0)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			if (length == 0)
			{
				lineTag.Break(formatStart);
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			while (lineTag != null && lineTag.End <= num)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				lineTag = lineTag.next;
			}
			if (lineTag != null && lineTag.End == num)
			{
				return flag;
			}
			LineTag lineTag3 = LineTag.FindTag(line, num - 1);
			if (lineTag3 != null)
			{
				lineTag3.Break(num);
				LineTag.SetFormat(lineTag3, font, color, backColor, specified);
			}
			return flag;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public int GetCharIndex(int x)
		{
			int i = this.start;
			int num = i + this.Length;
			int num2 = this.line.TextLengthWithoutEnding();
			if (this.Length == 0)
			{
				return i - 1;
			}
			if (num2 == 0)
			{
				return 0;
			}
			if ((float)x < this.line.widths[i])
			{
				if (i == 1 && (float)x > this.line.widths[1] / 2f)
				{
					return i;
				}
				return i - 1;
			}
			else
			{
				if ((float)x > this.line.widths[num2])
				{
					return num2;
				}
				while (i < num - 1)
				{
					int num3 = (num + i) / 2;
					if (this.line.widths[num3] < (float)x)
					{
						i = num3;
					}
					else
					{
						num = num3;
					}
				}
				float num4 = this.line.widths[num] - this.line.widths[i];
				if ((float)x - this.line.widths[i] >= num4 / 2f)
				{
					return num;
				}
				return i;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00026C90 File Offset: 0x00024E90
		public static LineTag GetFinalTag(LineTag tag)
		{
			LineTag lineTag = tag;
			while (lineTag.Length == 0 && lineTag.next != null && lineTag.next.Length == 0)
			{
				lineTag = lineTag.next;
			}
			return lineTag;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00026CC6 File Offset: 0x00024EC6
		internal virtual int MaxHeight()
		{
			return this.font.Height;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00026CD3 File Offset: 0x00024ED3
		private static void SetFormat(LineTag tag, Font font, Color color, Color back_color, FormatSpecified specified)
		{
			if ((FormatSpecified.Font & specified) == FormatSpecified.Font)
			{
				tag.Font = font;
			}
			if ((FormatSpecified.Color & specified) == FormatSpecified.Color)
			{
				tag.color = color;
			}
			if ((FormatSpecified.BackColor & specified) == FormatSpecified.BackColor)
			{
				tag.back_color = back_color;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00026D00 File Offset: 0x00024F00
		public virtual SizeF SizeOfPosition(Graphics dc, int pos)
		{
			if (pos >= this.line.TextLengthWithoutEnding() && this.line.document.multiline)
			{
				return SizeF.Empty;
			}
			string text = this.line.text.ToString(pos, 1);
			switch (text[0])
			{
			case '\t':
				if (this.line.document.multiline)
				{
					SizeF sizeF = TextBoxTextRenderer.MeasureText(dc, " ", this.font);
					sizeF.Width *= 8f;
					return sizeF;
				}
				break;
			case '\n':
			case '\r':
				break;
			case '\v':
			case '\f':
				goto IL_00AB;
			default:
				goto IL_00AB;
			}
			return TextBoxTextRenderer.MeasureText(dc, "\r", this.font);
			IL_00AB:
			return TextBoxTextRenderer.MeasureText(dc, text, this.font);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00026DC5 File Offset: 0x00024FC5
		public virtual string Text()
		{
			return this.line.text.ToString(this.start - 1, this.Length);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00026DE8 File Offset: 0x00024FE8
		public override string ToString()
		{
			if (this.Length > 0)
			{
				return string.Format("{0} Tag starts at index: {1}, length: {2}, text: {3}, font: {4}", new object[]
				{
					base.GetType(),
					this.start,
					this.Length,
					this.Text(),
					this.font.ToString()
				});
			}
			return string.Format("Zero Length tag at index: {0}", this.start);
		}

		// Token: 0x04000676 RID: 1654
		private Font font;

		// Token: 0x04000677 RID: 1655
		private Color color;

		// Token: 0x04000678 RID: 1656
		private Color back_color;

		// Token: 0x04000679 RID: 1657
		private Font link_font;

		// Token: 0x0400067A RID: 1658
		private bool is_link;

		// Token: 0x0400067B RID: 1659
		private string link_text;

		// Token: 0x0400067C RID: 1660
		private int start;

		// Token: 0x0400067D RID: 1661
		private int height;

		// Token: 0x0400067E RID: 1662
		private int ascent;

		// Token: 0x0400067F RID: 1663
		private int descent;

		// Token: 0x04000680 RID: 1664
		private int shift;

		// Token: 0x04000681 RID: 1665
		private Line line;

		// Token: 0x04000682 RID: 1666
		private LineTag next;

		// Token: 0x04000683 RID: 1667
		private LineTag previous;
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows rich text box control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000173 RID: 371
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Docking(DockingBehavior.Ask)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.RichTextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class RichTextBox : TextBoxBase
	{
		/// <summary>Gets or sets the currently selected rich text format (RTF) formatted text in the control.</summary>
		/// <returns>The selected RTF text in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0003F334 File Offset: 0x0003D534
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0003F38C File Offset: 0x0003D58C
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedRtf
		{
			get
			{
				return this.GenerateRTF(this.document.selection_start.line, this.document.selection_start.pos, this.document.selection_end.line, this.document.selection_end.pos).ToString();
			}
			set
			{
				if (this.document.selection_visible)
				{
					this.document.ReplaceSelection("", false);
				}
				int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
				MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(value), false);
				int pos = this.document.selection_start.pos;
				int line_no = this.document.selection_start.line.line_no;
				if (pos == 0)
				{
					this.reuse_line = true;
				}
				int num2;
				int num3;
				int num4;
				this.InsertRTFFromStream(memoryStream, pos, line_no, out num2, out num3, out num4);
				memoryStream.Close();
				int num5 = this.document.LineEndingLength(XplatUI.RunningOnUnix ? LineEnding.Rich : LineEnding.Hard);
				Line line;
				LineTag lineTag;
				this.document.CharIndexToLineTag(num + num4 + (num3 - this.document.selection_start.line.line_no) * num5, out line, out lineTag, out num);
				if (num >= line.text.Length)
				{
					num = line.text.Length - 1;
				}
				this.document.SetSelection(line, num);
				this.document.PositionCaret(line, num);
				this.document.DisplayCaret();
				base.ScrollToCaret();
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003F4E0 File Offset: 0x0003D6E0
		private void HandleGroup(RTF rtf)
		{
			if (this.rtf_section_stack == null)
			{
				this.rtf_section_stack = new Stack();
			}
			if (rtf.Major == Major.BeginGroup)
			{
				this.rtf_section_stack.Push(this.rtf_style.Clone());
				this.rtf_skip_count = 0;
				return;
			}
			if (rtf.Major == Major.EndGroup && this.rtf_section_stack.Count > 0)
			{
				this.FlushText(rtf, false);
				this.rtf_style = (RichTextBox.RtfSectionStyle)this.rtf_section_stack.Pop();
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003F55C File Offset: 0x0003D75C
		[MonoInternalNote("Add QuadJust support for justified alignment")]
		private void HandleControl(RTF rtf)
		{
			Major major = rtf.Major;
			if (major <= Major.SpecialChar)
			{
				if (major == Major.Destination)
				{
					rtf.SkipGroup();
					return;
				}
				if (major != Major.SpecialChar)
				{
					return;
				}
				this.SpecialChar(rtf);
			}
			else
			{
				switch (major)
				{
				case Major.ParAttr:
				{
					Minor minor = rtf.Minor;
					if (minor != Minor.ParDef)
					{
						switch (minor)
						{
						case Minor.QuadLeft:
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
							return;
						case Minor.QuadRight:
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtfalign = HorizontalAlignment.Right;
							return;
						case Minor.QuadJust:
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
							return;
						case Minor.QuadCenter:
							break;
						case Minor.FirstIndent:
							return;
						case Minor.LeftIndent:
						{
							using (Graphics graphics = base.CreateGraphics())
							{
								this.rtf_style.rtf_par_line_left_indent = (int)((float)rtf.Param / 1440f * graphics.DpiX + 0.5f);
								return;
							}
							break;
						}
						default:
							return;
						}
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
						return;
					}
					this.FlushText(rtf, false);
					this.rtf_style.rtf_par_line_left_indent = 0;
					this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
					return;
				}
				case Major.CharAttr:
				{
					Minor minor = rtf.Minor;
					if (minor <= Minor.Italic)
					{
						if (minor == Minor.Plain)
						{
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtfstyle = FontStyle.Regular;
							return;
						}
						if (minor != Minor.Bold)
						{
							switch (minor)
							{
							case Minor.FontNum:
							{
								global::System.Windows.Forms.RTF.Font font = global::System.Windows.Forms.RTF.Font.GetFont(rtf, rtf.Param);
								if (font != null)
								{
									this.FlushText(rtf, false);
									this.rtf_style.rtf_rtffont = font;
									return;
								}
								break;
							}
							case Minor.FontSize:
								this.FlushText(rtf, false);
								this.rtf_style.rtf_rtffont_size = rtf.Param / 2;
								return;
							case Minor.Italic:
								this.FlushText(rtf, false);
								if (rtf.Param == -1000000)
								{
									this.rtf_style.rtf_rtfstyle |= FontStyle.Italic;
									return;
								}
								this.rtf_style.rtf_rtfstyle &= ~FontStyle.Italic;
								return;
							default:
								return;
							}
						}
						else
						{
							this.FlushText(rtf, false);
							if (rtf.Param == -1000000)
							{
								this.rtf_style.rtf_rtfstyle |= FontStyle.Bold;
								return;
							}
							this.rtf_style.rtf_rtfstyle &= ~FontStyle.Bold;
							return;
						}
					}
					else
					{
						switch (minor)
						{
						case Minor.StrikeThru:
							this.FlushText(rtf, false);
							if (rtf.Param == -1000000)
							{
								this.rtf_style.rtf_rtfstyle |= FontStyle.Strikeout;
								return;
							}
							this.rtf_style.rtf_rtfstyle &= ~FontStyle.Strikeout;
							return;
						case Minor.Underline:
							this.FlushText(rtf, false);
							if (rtf.Param == -1000000)
							{
								this.rtf_style.rtf_rtfstyle |= FontStyle.Underline;
								return;
							}
							this.rtf_style.rtf_rtfstyle = this.rtf_style.rtf_rtfstyle & ~FontStyle.Underline;
							return;
						case Minor.DotUnderline:
						case Minor.DbUnderline:
							break;
						case Minor.NoUnderline:
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtfstyle &= ~FontStyle.Underline;
							return;
						default:
							if (minor == Minor.Invisible)
							{
								this.FlushText(rtf, false);
								this.rtf_style.rtf_visible = false;
								return;
							}
							if (minor == Minor.ForeColor)
							{
								global::System.Windows.Forms.RTF.Color color = global::System.Windows.Forms.RTF.Color.GetColor(rtf, rtf.Param);
								if (color != null)
								{
									this.FlushText(rtf, false);
									if (color.Red == -1 && color.Green == -1 && color.Blue == -1)
									{
										this.rtf_style.rtf_color = this.ForeColor;
									}
									else
									{
										this.rtf_style.rtf_color = global::System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);
									}
									this.FlushText(rtf, false);
									return;
								}
							}
							break;
						}
					}
					break;
				}
				case Major.PictAttr:
					if (rtf.Picture != null && rtf.Picture.IsValid())
					{
						Line line = this.document.GetLine(this.rtf_cursor_y);
						this.document.InsertPicture(line, 0, rtf.Picture);
						this.rtf_cursor_x++;
						this.FlushText(rtf, true);
						rtf.Picture = null;
						return;
					}
					break;
				default:
					if (major == Major.Unicode)
					{
						Minor minor = rtf.Minor;
						if (minor == Minor.UnicodeCharBytes)
						{
							this.rtf_style.rtf_skip_width = rtf.Param;
							return;
						}
						if (minor != Minor.UnicodeChar)
						{
							return;
						}
						this.FlushText(rtf, false);
						this.rtf_skip_count += this.rtf_style.rtf_skip_width;
						this.rtf_line.Append((char)rtf.Param);
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003F9D4 File Offset: 0x0003DBD4
		private void SpecialChar(RTF rtf)
		{
			switch (rtf.Minor)
			{
			case Minor.Cell:
				Console.Write(" ");
				return;
			case Minor.Row:
			case Minor.Par:
			case Minor.Sect:
			case Minor.Page:
			case Minor.Line:
				this.FlushText(rtf, true);
				return;
			case Minor.Column:
			case Minor.SoftPage:
			case Minor.SoftColumn:
			case Minor.SoftLine:
			case Minor.SoftLineHt:
			case Minor.EmSpace:
			case Minor.EnSpace:
			case Minor.LQuote:
			case Minor.RQuote:
			case Minor.LDblQuote:
			case Minor.RDblQuote:
			case Minor.Formula:
				break;
			case Minor.Tab:
				this.rtf_line.Append("\t");
				return;
			case Minor.EmDash:
				this.rtf_line.Append("—");
				return;
			case Minor.EnDash:
				this.rtf_line.Append("–");
				break;
			case Minor.Bullet:
				Console.WriteLine("*");
				return;
			case Minor.NoBrkSpace:
				Console.Write(" ");
				return;
			case Minor.NoReqHyphen:
			case Minor.NoBrkHyphen:
				this.rtf_line.Append("-");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		private void HandleText(RTF rtf)
		{
			string text = rtf.EncodedText;
			if (this.rtf_skip_count > 0 && text.Length > 0)
			{
				int num = Math.Min(this.rtf_skip_count, text.Length);
				text = text.Substring(num);
				this.rtf_skip_count -= num;
			}
			if (this.rtf_style.rtf_visible)
			{
				this.rtf_line.Append(text);
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003FB38 File Offset: 0x0003DD38
		private void FlushText(RTF rtf, bool newline)
		{
			int length = this.rtf_line.Length;
			if (!newline && length == 0)
			{
				return;
			}
			if (this.rtf_style.rtf_rtffont == null)
			{
				this.rtf_style.rtf_rtffont = global::System.Windows.Forms.RTF.Font.GetFont(rtf, 0);
			}
			global::System.Drawing.Font font = new global::System.Drawing.Font(this.rtf_style.rtf_rtffont.Name, (float)this.rtf_style.rtf_rtffont_size, this.rtf_style.rtf_rtfstyle);
			if (this.rtf_style.rtf_color == global::System.Drawing.Color.Empty)
			{
				global::System.Windows.Forms.RTF.Color color = global::System.Windows.Forms.RTF.Color.GetColor(rtf, 0);
				if (color == null || (color.Red == -1 && color.Green == -1 && color.Blue == -1))
				{
					this.rtf_style.rtf_color = this.ForeColor;
				}
				else
				{
					this.rtf_style.rtf_color = global::System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);
				}
			}
			this.rtf_chars += this.rtf_line.Length;
			if (this.rtf_cursor_x == 0 && !this.reuse_line)
			{
				if (newline && !this.rtf_line.ToString().EndsWith(Environment.NewLine))
				{
					this.rtf_line.Append(Environment.NewLine);
				}
				this.document.Add(this.rtf_cursor_y, this.rtf_line.ToString(), this.rtf_style.rtf_rtfalign, font, this.rtf_style.rtf_color, newline ? LineEnding.Rich : LineEnding.Wrap);
				if (this.rtf_style.rtf_par_line_left_indent != 0)
				{
					this.document.GetLine(this.rtf_cursor_y).indent = this.rtf_style.rtf_par_line_left_indent;
				}
			}
			else
			{
				Line line = this.document.GetLine(this.rtf_cursor_y);
				line.indent = this.rtf_style.rtf_par_line_left_indent;
				if (this.rtf_line.Length > 0)
				{
					this.document.InsertString(line, this.rtf_cursor_x, this.rtf_line.ToString());
					this.document.FormatText(line, this.rtf_cursor_x + 1, line, this.rtf_cursor_x + 1 + length, font, this.rtf_style.rtf_color, global::System.Drawing.Color.Empty, FormatSpecified.Font | FormatSpecified.Color);
				}
				if (newline)
				{
					line = this.document.GetLine(this.rtf_cursor_y);
					line.ending = LineEnding.Rich;
					if (!line.Text.EndsWith(Environment.NewLine))
					{
						Line line2 = line;
						line2.Text += Environment.NewLine;
					}
				}
				this.reuse_line = false;
			}
			if (newline)
			{
				this.rtf_cursor_x = 0;
				this.rtf_cursor_y++;
			}
			else
			{
				this.rtf_cursor_x += length;
			}
			this.rtf_line.Length = 0;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		private void InsertRTFFromStream(Stream data, int cursor_x, int cursor_y, out int to_x, out int to_y, out int chars)
		{
			RTF rtf = new RTF(data);
			rtf.ClassCallback[TokenClass.Text] = new ClassDelegate(this.HandleText);
			rtf.ClassCallback[TokenClass.Control] = new ClassDelegate(this.HandleControl);
			rtf.ClassCallback[TokenClass.Group] = new ClassDelegate(this.HandleGroup);
			this.rtf_skip_count = 0;
			this.rtf_line = new StringBuilder();
			this.rtf_style.rtf_color = global::System.Drawing.Color.Empty;
			this.rtf_style.rtf_rtffont_size = (int)this.Font.Size;
			this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
			this.rtf_style.rtf_rtfstyle = FontStyle.Regular;
			this.rtf_style.rtf_rtffont = null;
			this.rtf_style.rtf_visible = true;
			this.rtf_style.rtf_skip_width = 1;
			this.rtf_cursor_x = cursor_x;
			this.rtf_cursor_y = cursor_y;
			this.rtf_chars = 0;
			rtf.DefaultFont(this.Font.Name);
			this.rtf_text_map = new TextMap();
			TextMap.SetupStandardTable(this.rtf_text_map.Table);
			this.document.SuspendRecalc();
			try
			{
				rtf.Read();
				this.FlushText(rtf, false);
			}
			catch (RTFException ex)
			{
				Console.WriteLine("RTF Parsing failure: {0}", ex.Message);
			}
			to_x = this.rtf_cursor_x;
			to_y = this.rtf_cursor_y;
			chars = this.rtf_chars;
			if (this.rtf_section_stack != null)
			{
				this.rtf_section_stack.Clear();
			}
			this.document.RecalculateDocument(base.CreateGraphicsInternal(), cursor_y, this.document.Lines, false);
			this.document.ResumeRecalc(true);
			this.document.Invalidate(this.document.GetLine(cursor_y), 0, this.document.GetLine(this.document.Lines), -1);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		private void EmitRTFFontProperties(StringBuilder rtf, int prev_index, int font_index, global::System.Drawing.Font prev_font, global::System.Drawing.Font font)
		{
			if (prev_index != font_index)
			{
				rtf.Append(string.Format("\\f{0}", font_index));
			}
			if (prev_font == null || prev_font.Size != font.Size)
			{
				rtf.Append(string.Format("\\fs{0}", (int)(font.Size * 2f)));
			}
			if (prev_font == null || font.Bold != prev_font.Bold)
			{
				if (font.Bold)
				{
					rtf.Append("\\b");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\b0");
				}
			}
			if (prev_font == null || font.Italic != prev_font.Italic)
			{
				if (font.Italic)
				{
					rtf.Append("\\i");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\i0");
				}
			}
			if (prev_font == null || font.Strikeout != prev_font.Strikeout)
			{
				if (font.Strikeout)
				{
					rtf.Append("\\strike");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\strike0");
				}
			}
			if (prev_font == null || font.Underline != prev_font.Underline)
			{
				if (font.Underline)
				{
					rtf.Append("\\ul");
					return;
				}
				if (prev_font != null)
				{
					rtf.Append("\\ul0");
				}
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00040104 File Offset: 0x0003E304
		private void EmitRTFText(StringBuilder rtf, string text)
		{
			int length = rtf.Length;
			int length2 = text.Length;
			this.EmitEscapedUnicode(rtf, text);
			if (text.IndexOfAny(RichTextBox.ReservedRTFChars) > -1)
			{
				rtf.Replace("\\", "\\\\", length, length2);
				rtf.Replace("{", "\\{", length, length2);
				rtf.Replace("}", "\\}", length, length2);
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00040170 File Offset: 0x0003E370
		private void EmitEscapedUnicode(StringBuilder sb, string text)
		{
			int num = 0;
			int num2;
			while ((num2 = this.IndexOfNonAscii(text, num)) > -1)
			{
				sb.Append(text, num, num2 - num);
				int num3 = (int)text[num2];
				sb.Append("\\'");
				sb.Append(num3.ToString("X"));
				num = num2 + 1;
			}
			if (num < text.Length)
			{
				sb.Append(text, num, text.Length - num);
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000401E0 File Offset: 0x0003E3E0
		private int IndexOfNonAscii(string text, int startIndex)
		{
			for (int i = startIndex; i < text.Length; i++)
			{
				int num = (int)text[i];
				if (num < 0 || num >= 128)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00040218 File Offset: 0x0003E418
		private StringBuilder GenerateRTF(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList(10);
			ArrayList arrayList2 = new ArrayList(10);
			int i = start_line.line_no;
			int j = start_pos;
			LineTag lineTag = LineTag.FindTag(start_line, j);
			global::System.Drawing.Font font = lineTag.Font;
			global::System.Drawing.Color color = lineTag.Color;
			arrayList.Add(font.Name);
			arrayList2.Add(color);
			while (i <= end_line.line_no)
			{
				Line line = this.document.GetLine(i);
				lineTag = LineTag.FindTag(line, j);
				int num;
				if (i != end_line.line_no)
				{
					num = line.text.Length;
				}
				else
				{
					num = end_pos;
				}
				while (j < num)
				{
					if (lineTag.Font.Name != font.Name)
					{
						font = lineTag.Font;
						if (!arrayList.Contains(font.Name))
						{
							arrayList.Add(font.Name);
						}
					}
					if (lineTag.Color != color)
					{
						color = lineTag.Color;
						if (!arrayList2.Contains(color))
						{
							arrayList2.Add(color);
						}
					}
					j = lineTag.Start + lineTag.Length - 1;
					lineTag = lineTag.Next;
				}
				j = 0;
				i++;
			}
			stringBuilder.Append("{\\rtf1\\ansi");
			stringBuilder.Append("\\ansicpg1252");
			stringBuilder.Append(string.Format("\\deff{0}", arrayList.IndexOf(this.Font.Name)));
			stringBuilder.Append("\\deflang1033" + Environment.NewLine);
			stringBuilder.Append("{\\fonttbl");
			for (int k = 0; k < arrayList.Count; k++)
			{
				stringBuilder.Append(string.Format("{{\\f{0}", k));
				stringBuilder.Append("\\fnil");
				stringBuilder.Append("\\fcharset0 ");
				stringBuilder.Append((string)arrayList[k]);
				stringBuilder.Append(";}");
			}
			stringBuilder.Append("}");
			stringBuilder.Append(Environment.NewLine);
			if (arrayList2.Count > 1 || ((global::System.Drawing.Color)arrayList2[0]).R != this.ForeColor.R || ((global::System.Drawing.Color)arrayList2[0]).G != this.ForeColor.G || ((global::System.Drawing.Color)arrayList2[0]).B != this.ForeColor.B)
			{
				stringBuilder.Append("{\\colortbl ");
				for (int k = 0; k < arrayList2.Count; k++)
				{
					stringBuilder.Append(string.Format("\\red{0}", ((global::System.Drawing.Color)arrayList2[k]).R));
					stringBuilder.Append(string.Format("\\green{0}", ((global::System.Drawing.Color)arrayList2[k]).G));
					stringBuilder.Append(string.Format("\\blue{0}", ((global::System.Drawing.Color)arrayList2[k]).B));
					stringBuilder.Append(";");
				}
				stringBuilder.Append("}");
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.Append("{\\*\\generator Mono RichTextBox;}");
			lineTag = LineTag.FindTag(start_line, start_pos);
			stringBuilder.Append("\\pard");
			this.EmitRTFFontProperties(stringBuilder, -1, arrayList.IndexOf(lineTag.Font.Name), null, lineTag.Font);
			stringBuilder.Append(" ");
			font = lineTag.Font;
			color = (global::System.Drawing.Color)arrayList2[0];
			i = start_line.line_no;
			j = start_pos;
			while (i <= end_line.line_no)
			{
				Line line = this.document.GetLine(i);
				lineTag = LineTag.FindTag(line, j);
				int num;
				if (i != end_line.line_no)
				{
					num = line.text.Length;
				}
				else
				{
					num = end_pos;
				}
				while (j < num)
				{
					int length = stringBuilder.Length;
					if (lineTag.Font != font)
					{
						this.EmitRTFFontProperties(stringBuilder, arrayList.IndexOf(font.Name), arrayList.IndexOf(lineTag.Font.Name), font, lineTag.Font);
						font = lineTag.Font;
					}
					if (lineTag.Color != color)
					{
						color = lineTag.Color;
						stringBuilder.Append(string.Format("\\cf{0}", arrayList2.IndexOf(color)));
					}
					if (length != stringBuilder.Length)
					{
						stringBuilder.Append(" ");
					}
					if (i != end_line.line_no)
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, lineTag.Start + lineTag.Length - j - 1));
					}
					else if (end_pos < lineTag.Start + lineTag.Length - 1)
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, end_pos - j));
					}
					else
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, lineTag.Start + lineTag.Length - j - 1));
					}
					j = lineTag.Start + lineTag.Length - 1;
					lineTag = lineTag.Next;
				}
				if (j >= line.text.Length && line.ending != LineEnding.Wrap)
				{
					stringBuilder.Append("\\par");
					stringBuilder.Append(Environment.NewLine);
				}
				j = 0;
				i++;
			}
			stringBuilder.Append("}");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder;
		}

		// Token: 0x040008EF RID: 2287
		private bool reuse_line;

		// Token: 0x040008F0 RID: 2288
		private StringBuilder rtf_line;

		// Token: 0x040008F1 RID: 2289
		private RichTextBox.RtfSectionStyle rtf_style;

		// Token: 0x040008F2 RID: 2290
		private Stack rtf_section_stack;

		// Token: 0x040008F3 RID: 2291
		private TextMap rtf_text_map;

		// Token: 0x040008F4 RID: 2292
		private int rtf_skip_count;

		// Token: 0x040008F5 RID: 2293
		private int rtf_cursor_x;

		// Token: 0x040008F6 RID: 2294
		private int rtf_cursor_y;

		// Token: 0x040008F7 RID: 2295
		private int rtf_chars;

		// Token: 0x040008F8 RID: 2296
		private static object ContentsResizedEvent = new object();

		// Token: 0x040008F9 RID: 2297
		private static object HScrollEvent = new object();

		// Token: 0x040008FA RID: 2298
		private static object ImeChangeEvent = new object();

		// Token: 0x040008FB RID: 2299
		private static object LinkClickedEvent = new object();

		// Token: 0x040008FC RID: 2300
		private static object ProtectedEvent = new object();

		// Token: 0x040008FD RID: 2301
		private static object SelectionChangedEvent = new object();

		// Token: 0x040008FE RID: 2302
		private static object VScrollEvent = new object();

		// Token: 0x040008FF RID: 2303
		private static readonly char[] ReservedRTFChars = new char[] { '\\', '{', '}' };

		// Token: 0x02000174 RID: 372
		private class RtfSectionStyle : ICloneable
		{
			// Token: 0x06000E1C RID: 3612 RVA: 0x0004087C File Offset: 0x0003EA7C
			public object Clone()
			{
				return new RichTextBox.RtfSectionStyle
				{
					rtf_color = this.rtf_color,
					rtf_par_line_left_indent = this.rtf_par_line_left_indent,
					rtf_rtfalign = this.rtf_rtfalign,
					rtf_rtffont = this.rtf_rtffont,
					rtf_rtffont_size = this.rtf_rtffont_size,
					rtf_rtfstyle = this.rtf_rtfstyle,
					rtf_visible = this.rtf_visible,
					rtf_skip_width = this.rtf_skip_width
				};
			}

			// Token: 0x04000900 RID: 2304
			internal global::System.Drawing.Color rtf_color;

			// Token: 0x04000901 RID: 2305
			internal global::System.Windows.Forms.RTF.Font rtf_rtffont;

			// Token: 0x04000902 RID: 2306
			internal int rtf_rtffont_size;

			// Token: 0x04000903 RID: 2307
			internal FontStyle rtf_rtfstyle;

			// Token: 0x04000904 RID: 2308
			internal HorizontalAlignment rtf_rtfalign;

			// Token: 0x04000905 RID: 2309
			internal int rtf_par_line_left_indent;

			// Token: 0x04000906 RID: 2310
			internal bool rtf_visible;

			// Token: 0x04000907 RID: 2311
			internal int rtf_skip_width;
		}
	}
}

using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000389 RID: 905
	internal class RTF
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x0008F988 File Offset: 0x0008DB88
		static RTF()
		{
			for (int i = 0; i < RTF.Keys.Length; i++)
			{
				RTF.key_table[RTF.Keys[i].Symbol] = RTF.Keys[i];
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0008F9EC File Offset: 0x0008DBEC
		public RTF(Stream stream)
		{
			this.source = new StreamReader(stream);
			this.text_buffer = new StringBuilder(1024);
			this.rtf_class = TokenClass.None;
			this.pushed_class = TokenClass.None;
			this.pushed_char = char.MaxValue;
			this.line_num = 0;
			this.line_pos = 0;
			this.prev_char = char.MaxValue;
			this.bump_line = false;
			this.font_list = null;
			this.charset_stack = null;
			this.cur_charset = new Charset();
			this.destination_callbacks = new DestinationCallback();
			this.class_callbacks = new ClassCallback();
			this.destination_callbacks[Minor.OptDest] = new DestinationDelegate(this.HandleOptDest);
			this.destination_callbacks[Minor.FontTbl] = new DestinationDelegate(this.ReadFontTbl);
			this.destination_callbacks[Minor.ColorTbl] = new DestinationDelegate(this.ReadColorTbl);
			this.destination_callbacks[Minor.StyleSheet] = new DestinationDelegate(this.ReadStyleSheet);
			this.destination_callbacks[Minor.Info] = new DestinationDelegate(this.ReadInfoGroup);
			this.destination_callbacks[Minor.Pict] = new DestinationDelegate(this.ReadPictGroup);
			this.destination_callbacks[Minor.Object] = new DestinationDelegate(this.ReadObjGroup);
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0008FB3F File Offset: 0x0008DD3F
		public TokenClass TokenClass
		{
			get
			{
				return this.rtf_class;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0008FB47 File Offset: 0x0008DD47
		public Major Major
		{
			get
			{
				return this.major;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0008FB4F File Offset: 0x0008DD4F
		public Minor Minor
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0008FB57 File Offset: 0x0008DD57
		public int Param
		{
			get
			{
				return this.param;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0008FB5F File Offset: 0x0008DD5F
		public string Text
		{
			get
			{
				return this.text_buffer.ToString();
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0008FB6C File Offset: 0x0008DD6C
		public string EncodedText
		{
			get
			{
				return this.encoded_text;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0008FB74 File Offset: 0x0008DD74
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0008FB7C File Offset: 0x0008DD7C
		public Picture Picture
		{
			get
			{
				return this.picture;
			}
			set
			{
				this.picture = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0008FB85 File Offset: 0x0008DD85
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0008FB8D File Offset: 0x0008DD8D
		public Color Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0008FB96 File Offset: 0x0008DD96
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0008FB9E File Offset: 0x0008DD9E
		public Style Styles
		{
			get
			{
				return this.styles;
			}
			set
			{
				this.styles = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0008FBA7 File Offset: 0x0008DDA7
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0008FBAF File Offset: 0x0008DDAF
		public Font Fonts
		{
			get
			{
				return this.fonts;
			}
			set
			{
				this.fonts = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0008FBB8 File Offset: 0x0008DDB8
		public ClassCallback ClassCallback
		{
			get
			{
				return this.class_callbacks;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		public int LineNumber
		{
			get
			{
				return this.line_num;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0008FBC8 File Offset: 0x0008DDC8
		public int LinePos
		{
			get
			{
				return this.line_pos;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0008FBD0 File Offset: 0x0008DDD0
		public void DefaultFont(string name)
		{
			Font font = new Font(this);
			font.Num = 0;
			font.Name = name;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0008FBE5 File Offset: 0x0008DDE5
		private char GetChar()
		{
			return this.GetChar(true);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008FBF0 File Offset: 0x0008DDF0
		private char GetChar(bool skipCrLf)
		{
			int num;
			bool flag;
			for (;;)
			{
				if ((num = this.source.Read()) != -1)
				{
					this.text_buffer.Append((char)num);
				}
				if (this.prev_char == '\uffff')
				{
					this.bump_line = true;
				}
				flag = this.bump_line;
				this.bump_line = false;
				if (!skipCrLf)
				{
					break;
				}
				if (num == 13)
				{
					this.bump_line = true;
					StringBuilder stringBuilder = this.text_buffer;
					int num2 = stringBuilder.Length;
					stringBuilder.Length = num2 - 1;
				}
				else
				{
					if (num != 10)
					{
						break;
					}
					this.bump_line = true;
					if (this.prev_char == '\r')
					{
					}
					StringBuilder stringBuilder2 = this.text_buffer;
					int num2 = stringBuilder2.Length;
					stringBuilder2.Length = num2 - 1;
				}
			}
			this.line_pos++;
			if (flag)
			{
				this.line_num++;
				this.line_pos = 1;
			}
			this.prev_char = (char)num;
			return (char)num;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0008FCC5 File Offset: 0x0008DEC5
		public void Read()
		{
			while (this.GetToken() != TokenClass.EOF)
			{
				this.RouteToken();
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0008FCD8 File Offset: 0x0008DED8
		public void RouteToken()
		{
			if (this.CheckCM(TokenClass.Control, Major.Destination))
			{
				DestinationDelegate destinationDelegate = this.destination_callbacks[this.minor];
				if (destinationDelegate != null)
				{
					destinationDelegate(this);
				}
			}
			ClassDelegate classDelegate = this.class_callbacks[this.rtf_class];
			if (classDelegate != null)
			{
				classDelegate(this);
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0008FD28 File Offset: 0x0008DF28
		public void SkipGroup()
		{
			int num = 1;
			while (this.GetToken() != TokenClass.EOF)
			{
				if (this.rtf_class == TokenClass.Group)
				{
					if (this.major == Major.BeginGroup)
					{
						num++;
					}
					else if (this.major == Major.EndGroup)
					{
						num--;
						if (num < 1)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0008FD6C File Offset: 0x0008DF6C
		public TokenClass GetToken()
		{
			if (this.pushed_class != TokenClass.None)
			{
				this.rtf_class = this.pushed_class;
				this.major = this.pushed_major;
				this.minor = this.pushed_minor;
				this.param = this.pushed_param;
				this.pushed_class = TokenClass.None;
				return this.rtf_class;
			}
			this.GetToken2();
			if (this.rtf_class == TokenClass.Text)
			{
				this.minor = (Minor)this.cur_charset[(int)this.major];
				if (this.encoding == null)
				{
					this.encoding = Encoding.GetEncoding(this.encoding_code_page);
				}
				this.encoded_text = new string(this.encoding.GetChars(new byte[] { (byte)this.major }));
			}
			if (this.cur_charset.Flags == CharsetFlags.None)
			{
				return this.rtf_class;
			}
			if (this.CheckCMM(TokenClass.Control, Major.Unicode, Minor.UnicodeAnsiCodepage))
			{
				this.encoding_code_page = this.param;
				if (this.encoding_code_page < 0 || this.encoding_code_page > 65535)
				{
					this.encoding_code_page = 1252;
				}
			}
			if ((this.cur_charset.Flags & CharsetFlags.Read) != CharsetFlags.None && this.CheckCM(TokenClass.Control, Major.CharSet))
			{
				this.cur_charset.ReadMap();
			}
			else if ((this.cur_charset.Flags & CharsetFlags.Switch) != CharsetFlags.None && this.CheckCMM(TokenClass.Control, Major.CharAttr, Minor.FontNum))
			{
				Font font = Font.GetFont(this.font_list, this.param);
				if (font != null)
				{
					if (font.Name.StartsWith("Symbol"))
					{
						this.cur_charset.ID = CharsetType.Symbol;
					}
					else
					{
						this.cur_charset.ID = CharsetType.General;
					}
				}
				else if ((this.cur_charset.Flags & CharsetFlags.Switch) != CharsetFlags.None && this.rtf_class == TokenClass.Group)
				{
					Major major = this.major;
					if (major != Major.BeginGroup)
					{
						if (major == Major.EndGroup)
						{
							this.cur_charset = (Charset)this.charset_stack.Pop();
						}
					}
					else
					{
						this.charset_stack.Push(this.cur_charset);
					}
				}
			}
			return this.rtf_class;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008FF60 File Offset: 0x0008E160
		private void GetToken2()
		{
			this.rtf_class = TokenClass.Unknown;
			this.param = -1000000;
			this.text_buffer.Length = 0;
			char c;
			if (this.pushed_char != '\uffff')
			{
				c = this.pushed_char;
				this.text_buffer.Append(c);
				this.pushed_char = char.MaxValue;
			}
			else if ((c = this.GetChar()) == '\uffff')
			{
				this.rtf_class = TokenClass.EOF;
				return;
			}
			if (c == '{')
			{
				this.rtf_class = TokenClass.Group;
				this.major = Major.BeginGroup;
				return;
			}
			if (c == '}')
			{
				this.rtf_class = TokenClass.Group;
				this.major = Major.EndGroup;
				return;
			}
			if (c != '\\')
			{
				if (c != '\t')
				{
					this.rtf_class = TokenClass.Text;
					this.major = (Major)c;
					return;
				}
				this.rtf_class = TokenClass.Control;
				this.major = Major.SpecialChar;
				this.minor = Minor.Tab;
				return;
			}
			else
			{
				if ((c = this.GetChar()) == '\uffff')
				{
					return;
				}
				if (char.IsLetter(c))
				{
					while (char.IsLetter(c) && (c = this.GetChar(false)) != '\uffff')
					{
					}
					if (c != '\uffff')
					{
						StringBuilder stringBuilder = this.text_buffer;
						int num = stringBuilder.Length;
						stringBuilder.Length = num - 1;
					}
					this.Lookup(this.text_buffer.ToString());
					if (c != '\uffff')
					{
						this.text_buffer.Append(c);
					}
					int num2 = 1;
					if (c == '-')
					{
						num2 = -1;
						c = this.GetChar();
					}
					if (c != '\uffff' && char.IsDigit(c) && this.minor != Minor.PngBlip)
					{
						this.param = 0;
						while (char.IsDigit(c))
						{
							this.param = this.param * 10 + (int)Convert.ToByte(c) - 48;
							if ((c = this.GetChar()) == '\uffff')
							{
								break;
							}
						}
						this.param *= num2;
					}
					if (c != '\uffff')
					{
						if (c != ' ' && c != '\r' && c != '\n')
						{
							this.pushed_char = c;
						}
						StringBuilder stringBuilder2 = this.text_buffer;
						int num = stringBuilder2.Length;
						stringBuilder2.Length = num - 1;
					}
					return;
				}
				if (c == '\'')
				{
					if ((c = this.GetChar()) == '\uffff')
					{
						return;
					}
					char @char;
					if ((@char = this.GetChar()) == '\uffff')
					{
						return;
					}
					this.rtf_class = TokenClass.Text;
					this.major = (Major)((ushort)(Convert.ToByte(c.ToString(), 16) * 16 + Convert.ToByte(@char.ToString(), 16)));
					return;
				}
				else
				{
					if (c == ':' || c == '{' || c == '}' || c == '\\')
					{
						this.rtf_class = TokenClass.Text;
						this.major = (Major)c;
						return;
					}
					this.Lookup(this.text_buffer.ToString());
					return;
				}
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000901D4 File Offset: 0x0008E3D4
		public void SetToken(TokenClass cl, Major maj, Minor min, int par, string text)
		{
			this.rtf_class = cl;
			this.major = maj;
			this.minor = min;
			this.param = par;
			if (par == -1000000)
			{
				this.text_buffer = new StringBuilder(text);
				return;
			}
			this.text_buffer = new StringBuilder(text + par.ToString());
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00090230 File Offset: 0x0008E430
		public void UngetToken()
		{
			if (this.pushed_class != TokenClass.None)
			{
				throw new RTFException(this, "Cannot unget more than one token");
			}
			if (this.rtf_class == TokenClass.None)
			{
				throw new RTFException(this, "No token to unget");
			}
			this.pushed_class = this.rtf_class;
			this.pushed_major = this.major;
			this.pushed_minor = this.minor;
			this.pushed_param = this.param;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00090298 File Offset: 0x0008E498
		public void Lookup(string token)
		{
			object obj = RTF.key_table[token.Substring(1)];
			if (obj == null)
			{
				this.rtf_class = TokenClass.Unknown;
				this.major = this.Major - 1;
				this.minor = this.Minor - 1;
				return;
			}
			KeyStruct keyStruct = (KeyStruct)obj;
			this.rtf_class = TokenClass.Control;
			this.major = keyStruct.Major;
			this.minor = keyStruct.Minor;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00090304 File Offset: 0x0008E504
		public bool CheckCM(TokenClass rtf_class, Major major)
		{
			return this.rtf_class == rtf_class && this.major == major;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0009031B File Offset: 0x0008E51B
		public bool CheckCMM(TokenClass rtf_class, Major major, Minor minor)
		{
			return this.rtf_class == rtf_class && this.major == major && this.minor == minor;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0009033B File Offset: 0x0008E53B
		public bool CheckMM(Major major, Minor minor)
		{
			return this.major == major && this.minor == minor;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00090354 File Offset: 0x0008E554
		private void HandleOptDest(RTF rtf)
		{
			int num = 1;
			for (;;)
			{
				this.GetToken();
				if (rtf.CheckCMM(TokenClass.Control, Major.Destination, Minor.Pict))
				{
					break;
				}
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup) && --num == 0)
				{
					return;
				}
				if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
				{
					num++;
				}
			}
			this.ReadPictGroup(rtf);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000903A0 File Offset: 0x0008E5A0
		private void ReadFontTbl(RTF rtf)
		{
			int num = -1;
			Font font = null;
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto IL_020F;
				}
				if (num < 0)
				{
					if (rtf.CheckCMM(TokenClass.Control, Major.CharAttr, Minor.FontNum))
					{
						num = 1;
					}
					else
					{
						if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
						{
							break;
						}
						num = 0;
					}
				}
				if (num == 0)
				{
					if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						goto Block_6;
					}
					rtf.GetToken();
				}
				font = new Font(rtf);
				while (rtf.rtf_class != TokenClass.EOF && !rtf.CheckCM(TokenClass.Text, (Major)59) && !rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					if (rtf.rtf_class == TokenClass.Control)
					{
						Major major = rtf.major;
						if (major != Major.FontFamily)
						{
							if (major != Major.CharAttr)
							{
								if (major == Major.FontAttr)
								{
									switch (rtf.minor)
									{
									case Minor.FontCharSet:
										font.Charset = (CharsetType)rtf.param;
										break;
									case Minor.FontPitch:
										font.Pitch = rtf.param;
										break;
									case Minor.FontCodePage:
										font.Codepage = rtf.param;
										break;
									case Minor.FTypeNil:
									case Minor.FTypeTrueType:
										font.Type = rtf.param;
										break;
									}
								}
							}
							else
							{
								Minor minor = rtf.minor;
								if (minor == Minor.FontNum)
								{
									font.Num = rtf.param;
								}
							}
						}
						else
						{
							font.Family = (int)rtf.minor;
						}
					}
					else if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						rtf.SkipGroup();
					}
					else if (rtf.rtf_class == TokenClass.Text)
					{
						StringBuilder stringBuilder = new StringBuilder();
						while (rtf.rtf_class != TokenClass.EOF && !rtf.CheckCM(TokenClass.Text, (Major)59) && !rtf.CheckCM(TokenClass.Group, Major.EndGroup) && !rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
						{
							stringBuilder.Append((char)rtf.major);
							rtf.GetToken();
						}
						if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
						{
							rtf.UngetToken();
						}
						font.Name = stringBuilder.ToString();
						continue;
					}
					rtf.GetToken();
				}
				if (num == 0)
				{
					rtf.GetToken();
					if (!rtf.CheckCM(TokenClass.Group, Major.EndGroup))
					{
						goto Block_22;
					}
				}
			}
			throw new RTFException(rtf, "Cannot determine format");
			Block_6:
			throw new RTFException(rtf, "missing \"{\"");
			Block_22:
			throw new RTFException(rtf, "Missing \"}\"");
			IL_020F:
			if (font == null)
			{
				throw new RTFException(rtf, "No font created");
			}
			if (font.Num == -1)
			{
				throw new RTFException(rtf, "Missing font number");
			}
			rtf.RouteToken();
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000905E8 File Offset: 0x0008E7E8
		private void ReadColorTbl(RTF rtf)
		{
			int num = 0;
			do
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto IL_009B;
				}
				Color color = new Color(rtf);
				color.Num = num++;
				while (rtf.CheckCM(TokenClass.Control, Major.ColorName))
				{
					switch (rtf.minor)
					{
					case Minor.Red:
						color.Red = rtf.param;
						break;
					case Minor.Green:
						color.Green = rtf.param;
						break;
					case Minor.Blue:
						color.Blue = rtf.param;
						break;
					}
					rtf.GetToken();
				}
			}
			while (rtf.CheckCM(TokenClass.Text, (Major)59));
			throw new RTFException(rtf, "Malformed color entry");
			IL_009B:
			rtf.RouteToken();
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00090698 File Offset: 0x0008E898
		private void ReadStyleSheet(RTF rtf)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto IL_024F;
				}
				Style style = new Style(rtf);
				if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
				{
					break;
				}
				for (;;)
				{
					rtf.GetToken();
					if (rtf.rtf_class == TokenClass.EOF || rtf.CheckCM(TokenClass.Text, (Major)59))
					{
						break;
					}
					if (rtf.rtf_class == TokenClass.Control)
					{
						if (rtf.CheckMM(Major.ParAttr, Minor.StyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Paragraph;
						}
						else if (rtf.CheckMM(Major.CharAttr, Minor.CharStyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Character;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.SectStyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Section;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.BasedOn))
						{
							style.BasedOn = rtf.param;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.Additive))
						{
							style.Additive = true;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.Next))
						{
							style.NextPar = rtf.param;
						}
						else
						{
							new StyleElement(style, rtf.rtf_class, rtf.major, rtf.minor, rtf.param, rtf.text_buffer.ToString());
						}
					}
					else if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						rtf.SkipGroup();
					}
					else if (rtf.rtf_class == TokenClass.Text)
					{
						while (rtf.rtf_class == TokenClass.Text)
						{
							if (rtf.major == (Major)59)
							{
								rtf.UngetToken();
								break;
							}
							stringBuilder.Append((char)rtf.major);
							rtf.GetToken();
						}
						style.Name = stringBuilder.ToString();
					}
				}
				rtf.GetToken();
				if (!rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto Block_15;
				}
				if (style.Name == null)
				{
					goto Block_16;
				}
				if (style.Num < 0)
				{
					if (!stringBuilder.ToString().StartsWith("Normal") && !stringBuilder.ToString().StartsWith("Standard"))
					{
						goto Block_19;
					}
					style.Num = 0;
				}
				if (style.NextPar == -1)
				{
					style.NextPar = style.Num;
				}
			}
			throw new RTFException(rtf, "Missing \"{\"");
			Block_15:
			throw new RTFException(rtf, "Missing EndGroup (\"}\"");
			Block_16:
			throw new RTFException(rtf, "Style must have name");
			Block_19:
			throw new RTFException(rtf, "Missing style number");
			IL_024F:
			rtf.RouteToken();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000908FA File Offset: 0x0008EAFA
		private void ReadInfoGroup(RTF rtf)
		{
			rtf.SkipGroup();
			rtf.RouteToken();
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00090908 File Offset: 0x0008EB08
		private void ReadPictGroup(RTF rtf)
		{
			bool flag = false;
			Picture picture = new Picture();
			do
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto IL_022A;
				}
				switch (this.minor)
				{
				case Minor.WinMetafile:
					picture.ImageType = this.minor;
					flag = true;
					continue;
				case Minor.PngBlip:
					picture.ImageType = this.minor;
					flag = true;
					break;
				case Minor.PicWid:
				case Minor.PicHt:
					continue;
				case Minor.PicGoalWid:
					picture.SetWidthFromTwips(this.param);
					continue;
				case Minor.PicGoalHt:
					picture.SetHeightFromTwips(this.param);
					continue;
				}
			}
			while (!flag || rtf.rtf_class != TokenClass.Text);
			picture.Data.Seek(0L, SeekOrigin.Begin);
			char c = (char)rtf.major;
			for (;;)
			{
				if (c == '\n' || c == '\r')
				{
					c = (char)this.source.Peek();
					if (c != '}')
					{
						c = (char)this.source.Read();
						continue;
					}
				}
				char c2 = (char)this.source.Peek();
				if (c2 == '}')
				{
					break;
				}
				c2 = (char)this.source.Read();
				while (c2 == '\n' || c2 == '\r')
				{
					c2 = (char)this.source.Peek();
					if (c2 == '}')
					{
						break;
					}
					c2 = (char)this.source.Read();
				}
				uint num;
				if (char.IsDigit(c))
				{
					num = (uint)(c - '0');
				}
				else if (char.IsLower(c))
				{
					num = (uint)(c - 'a' + '\n');
				}
				else if (char.IsUpper(c))
				{
					num = (uint)(c - 'A' + '\n');
				}
				else
				{
					if (c == '\n')
					{
						continue;
					}
					if (c == '\r')
					{
						continue;
					}
					break;
				}
				uint num2;
				if (char.IsDigit(c2))
				{
					num2 = (uint)(c2 - '0');
				}
				else if (char.IsLower(c2))
				{
					num2 = (uint)(c2 - 'a' + '\n');
				}
				else if (char.IsUpper(c2))
				{
					num2 = (uint)(c2 - 'A' + '\n');
				}
				else
				{
					if (c2 == '\n')
					{
						continue;
					}
					if (c2 == '\r')
					{
						continue;
					}
					break;
				}
				picture.Data.WriteByte((byte)(checked(num * 16U + num2)));
				c = (char)this.source.Peek();
				if (c == '}')
				{
					break;
				}
				c = (char)this.source.Read();
			}
			flag = false;
			IL_022A:
			if (picture.ImageType != Minor.Undefined && !flag)
			{
				this.picture = picture;
				this.SetToken(TokenClass.Control, Major.PictAttr, picture.ImageType, 0, string.Empty);
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000908FA File Offset: 0x0008EAFA
		private void ReadObjGroup(RTF rtf)
		{
			rtf.SkipGroup();
			rtf.RouteToken();
		}

		// Token: 0x04001B35 RID: 6965
		private TokenClass rtf_class;

		// Token: 0x04001B36 RID: 6966
		private Major major;

		// Token: 0x04001B37 RID: 6967
		private Minor minor;

		// Token: 0x04001B38 RID: 6968
		private int param;

		// Token: 0x04001B39 RID: 6969
		private string encoded_text;

		// Token: 0x04001B3A RID: 6970
		private Encoding encoding;

		// Token: 0x04001B3B RID: 6971
		private int encoding_code_page = 1252;

		// Token: 0x04001B3C RID: 6972
		private StringBuilder text_buffer;

		// Token: 0x04001B3D RID: 6973
		private Picture picture;

		// Token: 0x04001B3E RID: 6974
		private int line_num;

		// Token: 0x04001B3F RID: 6975
		private int line_pos;

		// Token: 0x04001B40 RID: 6976
		private char pushed_char;

		// Token: 0x04001B41 RID: 6977
		private TokenClass pushed_class;

		// Token: 0x04001B42 RID: 6978
		private Major pushed_major;

		// Token: 0x04001B43 RID: 6979
		private Minor pushed_minor;

		// Token: 0x04001B44 RID: 6980
		private int pushed_param;

		// Token: 0x04001B45 RID: 6981
		private char prev_char;

		// Token: 0x04001B46 RID: 6982
		private bool bump_line;

		// Token: 0x04001B47 RID: 6983
		private Font font_list;

		// Token: 0x04001B48 RID: 6984
		private Charset cur_charset;

		// Token: 0x04001B49 RID: 6985
		private Stack charset_stack;

		// Token: 0x04001B4A RID: 6986
		private Style styles;

		// Token: 0x04001B4B RID: 6987
		private Color colors;

		// Token: 0x04001B4C RID: 6988
		private Font fonts;

		// Token: 0x04001B4D RID: 6989
		private StreamReader source;

		// Token: 0x04001B4E RID: 6990
		private static Hashtable key_table = new Hashtable(RTF.Keys.Length);

		// Token: 0x04001B4F RID: 6991
		private static KeyStruct[] Keys = KeysInit.Init();

		// Token: 0x04001B50 RID: 6992
		private DestinationCallback destination_callbacks;

		// Token: 0x04001B51 RID: 6993
		private ClassCallback class_callbacks;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x0200004A RID: 74
	internal class SmallXmlParser
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00002D98 File Offset: 0x00000F98
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002DAC File Offset: 0x00000FAC
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002DF4 File Offset: 0x00000FF4
		private bool IsNameChar(char c, bool start)
		{
			if (c <= '.')
			{
				if (c == '-' || c == '.')
				{
					return !start;
				}
			}
			else if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ՙ' || c == 'ۥ' || c == 'ۦ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002E96 File Offset: 0x00001096
		private bool IsWhitespace(int c)
		{
			return c - 9 <= 1 || c == 13 || c == 32;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002EAC File Offset: 0x000010AC
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002EB5 File Offset: 0x000010B5
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002EF4 File Offset: 0x000010F4
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002F37 File Offset: 0x00001137
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002F44 File Offset: 0x00001144
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
				return num;
			}
			this.column++;
			return num;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002F9C File Offset: 0x0000119C
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002FE0 File Offset: 0x000011E0
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string text = this.buffer.ToString();
					this.buffer.Length = 0;
					return text;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003040 File Offset: 0x00001240
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] array = new char[num * 2];
					Array.Copy(this.nameBuffer, array, num);
					this.nameBuffer = array;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000030F0 File Offset: 0x000012F0
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003160 File Offset: 0x00001360
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000031C4 File Offset: 0x000013C4
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000345C File Offset: 0x0000165C
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000034C0 File Offset: 0x000016C0
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				if (num == -1)
				{
					break;
				}
				if (num != 38)
				{
					if (num == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003510 File Offset: 0x00001710
		private void ReadReference()
		{
			if (this.Peek() == 35)
			{
				this.Read();
				this.ReadCharacterReference();
				return;
			}
			string text = this.ReadName();
			this.Expect(59);
			if (text == "amp")
			{
				this.buffer.Append('&');
				return;
			}
			if (text == "quot")
			{
				this.buffer.Append('"');
				return;
			}
			if (text == "apos")
			{
				this.buffer.Append('\'');
				return;
			}
			if (text == "lt")
			{
				this.buffer.Append('<');
				return;
			}
			if (!(text == "gt"))
			{
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.buffer.Append('>');
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000035E0 File Offset: 0x000017E0
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				int num2 = this.Peek();
				while (num2 >= 0 && 48 <= num2 && num2 <= 57)
				{
					num <<= 4 + num2 - 48;
					this.Read();
					num2 = this.Peek();
				}
			}
			return num;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000369C File Offset: 0x0000189C
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string text2;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				text2 = this.ReadUntil('\'', true);
			}
			else
			{
				text2 = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = text2;
			}
			a.Add(text, text2);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003734 File Offset: 0x00001934
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000037B4 File Offset: 0x000019B4
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			while (this.Read() != 45 || this.Read() != 45)
			{
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x04000141 RID: 321
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x04000142 RID: 322
		private TextReader reader;

		// Token: 0x04000143 RID: 323
		private Stack elementNames = new Stack();

		// Token: 0x04000144 RID: 324
		private Stack xmlSpaces = new Stack();

		// Token: 0x04000145 RID: 325
		private string xmlSpace;

		// Token: 0x04000146 RID: 326
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x04000147 RID: 327
		private char[] nameBuffer = new char[30];

		// Token: 0x04000148 RID: 328
		private bool isWhitespace;

		// Token: 0x04000149 RID: 329
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x0400014A RID: 330
		private int line = 1;

		// Token: 0x0400014B RID: 331
		private int column;

		// Token: 0x0400014C RID: 332
		private bool resetColumn;

		// Token: 0x0200004B RID: 75
		public interface IContentHandler
		{
			// Token: 0x060000B2 RID: 178
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x060000B3 RID: 179
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x060000B4 RID: 180
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x060000B5 RID: 181
			void OnEndElement(string name);

			// Token: 0x060000B6 RID: 182
			void OnProcessingInstruction(string name, string text);

			// Token: 0x060000B7 RID: 183
			void OnChars(string text);

			// Token: 0x060000B8 RID: 184
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x0200004C RID: 76
		public interface IAttrList
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000B9 RID: 185
			int Length { get; }

			// Token: 0x060000BA RID: 186
			string GetName(int i);

			// Token: 0x060000BB RID: 187
			string GetValue(int i);

			// Token: 0x060000BC RID: 188
			string GetValue(string name);

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000BD RID: 189
			string[] Names { get; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000BE RID: 190
			string[] Values { get; }
		}

		// Token: 0x0200004D RID: 77
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000BF RID: 191 RVA: 0x000037F0 File Offset: 0x000019F0
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x000037FD File Offset: 0x000019FD
			public string GetName(int i)
			{
				return this.attrNames[i];
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x0000380B File Offset: 0x00001A0B
			public string GetValue(int i)
			{
				return this.attrValues[i];
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x0000381C File Offset: 0x00001A1C
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if (this.attrNames[i] == name)
					{
						return this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003861 File Offset: 0x00001A61
			public string[] Names
			{
				get
				{
					return this.attrNames.ToArray();
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000386E File Offset: 0x00001A6E
			public string[] Values
			{
				get
				{
					return this.attrValues.ToArray();
				}
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x0000387B File Offset: 0x00001A7B
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00003893 File Offset: 0x00001A93
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x0400014D RID: 333
			private List<string> attrNames = new List<string>();

			// Token: 0x0400014E RID: 334
			private List<string> attrValues = new List<string>();
		}
	}
}

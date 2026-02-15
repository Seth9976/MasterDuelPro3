using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200006E RID: 110
	internal class XmlTextEncoder
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00018A42 File Offset: 0x00016C42
		internal XmlTextEncoder(TextWriter textWriter)
		{
			this.textWriter = textWriter;
			this.quoteChar = '"';
			this.xmlCharType = XmlCharType.Instance;
		}

		// Token: 0x170000E7 RID: 231
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00018A64 File Offset: 0x00016C64
		internal char QuoteChar
		{
			set
			{
				this.quoteChar = value;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018A6D File Offset: 0x00016C6D
		internal void StartAttribute(bool cacheAttrValue)
		{
			this.inAttribute = true;
			this.cacheAttrValue = cacheAttrValue;
			if (cacheAttrValue)
			{
				if (this.attrValue == null)
				{
					this.attrValue = new StringBuilder();
					return;
				}
				this.attrValue.Length = 0;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018AA0 File Offset: 0x00016CA0
		internal void EndAttribute()
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Length = 0;
			}
			this.inAttribute = false;
			this.cacheAttrValue = false;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00018AC4 File Offset: 0x00016CC4
		internal string AttributeValue
		{
			get
			{
				if (this.cacheAttrValue)
				{
					return this.attrValue.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018ADF File Offset: 0x00016CDF
		internal void WriteSurrogateChar(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			this.textWriter.Write(highChar);
			this.textWriter.Write(lowChar);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00018B14 File Offset: 0x00016D14
		internal void Write(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			int num = offset + count;
			int num2 = offset;
			char c = '\0';
			for (;;)
			{
				int num3 = num2;
				while (num2 < num && (this.xmlCharType.charProperties[(int)(c = array[num2])] & 128) != 0)
				{
					num2++;
				}
				if (num3 < num2)
				{
					this.textWriter.Write(array, num3, num2 - num3);
				}
				if (num2 == num)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_01A9;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_01A9;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_01A9;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_01ED:
				num2++;
				continue;
				IL_01A9:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (num2 + 1 < num)
					{
						this.WriteSurrogateChar(array[++num2], c);
						goto IL_01ED;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_23;
					}
					this.WriteCharEntityImpl(c);
					goto IL_01ED;
				}
			}
			throw new ArgumentException(Res.GetString("The second character surrogate pair is not in the input buffer to be written."));
			Block_23:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00018D18 File Offset: 0x00016F18
		internal void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(highChar);
				this.attrValue.Append(lowChar);
			}
			this.textWriter.Write("&#x");
			this.textWriter.Write(num.ToString("X", NumberFormatInfo.InvariantInfo));
			this.textWriter.Write(';');
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018DA0 File Offset: 0x00016FA0
		internal void Write(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int i = 0;
			int num = 0;
			char c = '\0';
			for (;;)
			{
				if (i >= length || (this.xmlCharType.charProperties[(int)(c = text[i])] & 128) == 0)
				{
					if (i == length)
					{
						break;
					}
					if (this.inAttribute)
					{
						if (c != '\t')
						{
							goto IL_0090;
						}
						i++;
					}
					else
					{
						if (c != '\t' && c != '\n' && c != '\r' && c != '"' && c != '\'')
						{
							goto IL_0090;
						}
						i++;
					}
				}
				else
				{
					i++;
				}
			}
			this.textWriter.Write(text);
			return;
			IL_0090:
			char[] array = new char[256];
			for (;;)
			{
				if (num < i)
				{
					this.WriteStringFragment(text, num, i - num, array);
				}
				if (i == length)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_01BF;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_01BF;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_01BF;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_0205:
				i++;
				num = i;
				while (i < length)
				{
					if ((this.xmlCharType.charProperties[(int)(c = text[i])] & 128) == 0)
					{
						break;
					}
					i++;
				}
				continue;
				IL_01BF:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (i + 1 < length)
					{
						this.WriteSurrogateChar(text[++i], c);
						goto IL_0205;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_27;
					}
					this.WriteCharEntityImpl(c);
					goto IL_0205;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(text[i], c);
			Block_27:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018FE8 File Offset: 0x000171E8
		internal void WriteRawWithSurrogateChecking(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int num = 0;
			char c = '\0';
			char c2;
			for (;;)
			{
				if (num >= length || ((this.xmlCharType.charProperties[(int)(c = text[num])] & 16) == 0 && c >= ' '))
				{
					if (num == length)
					{
						goto IL_00A4;
					}
					if (XmlCharType.IsHighSurrogate((int)c))
					{
						if (num + 1 >= length)
						{
							goto IL_007F;
						}
						c2 = text[num + 1];
						if (!XmlCharType.IsLowSurrogate((int)c2))
						{
							break;
						}
						num += 2;
					}
					else
					{
						if (XmlCharType.IsLowSurrogate((int)c))
						{
							goto Block_9;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(c2, c);
			IL_007F:
			throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			Block_9:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
			IL_00A4:
			this.textWriter.Write(text);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000190A8 File Offset: 0x000172A8
		internal void WriteRaw(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			this.textWriter.Write(array, offset, count);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001911C File Offset: 0x0001731C
		internal void WriteCharEntity(char ch)
		{
			if (XmlCharType.IsSurrogate((int)ch))
			{
				throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			}
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append("&#x");
				this.attrValue.Append(text);
				this.attrValue.Append(';');
			}
			this.WriteCharEntityImpl(text);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019190 File Offset: 0x00017390
		internal void WriteEntityRef(string name)
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Append('&');
				this.attrValue.Append(name);
				this.attrValue.Append(';');
			}
			this.WriteEntityRefImpl(name);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000191CC File Offset: 0x000173CC
		private void WriteStringFragment(string str, int offset, int count, char[] helperBuffer)
		{
			int num = helperBuffer.Length;
			while (count > 0)
			{
				int num2 = count;
				if (num2 > num)
				{
					num2 = num;
				}
				str.CopyTo(offset, helperBuffer, 0, num2);
				this.textWriter.Write(helperBuffer, 0, num2);
				offset += num2;
				count -= num2;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00019210 File Offset: 0x00017410
		private void WriteCharEntityImpl(char ch)
		{
			int num = (int)ch;
			this.WriteCharEntityImpl(num.ToString("X", NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00019236 File Offset: 0x00017436
		private void WriteCharEntityImpl(string strVal)
		{
			this.textWriter.Write("&#x");
			this.textWriter.Write(strVal);
			this.textWriter.Write(';');
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019261 File Offset: 0x00017461
		private void WriteEntityRefImpl(string name)
		{
			this.textWriter.Write('&');
			this.textWriter.Write(name);
			this.textWriter.Write(';');
		}

		// Token: 0x0400029A RID: 666
		private TextWriter textWriter;

		// Token: 0x0400029B RID: 667
		private bool inAttribute;

		// Token: 0x0400029C RID: 668
		private char quoteChar;

		// Token: 0x0400029D RID: 669
		private StringBuilder attrValue;

		// Token: 0x0400029E RID: 670
		private bool cacheAttrValue;

		// Token: 0x0400029F RID: 671
		private XmlCharType xmlCharType;
	}
}

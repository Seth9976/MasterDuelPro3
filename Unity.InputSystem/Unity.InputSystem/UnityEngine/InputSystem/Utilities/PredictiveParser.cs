using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200025B RID: 603
	internal struct PredictiveParser
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x00062A80 File Offset: 0x00060C80
		public unsafe void ExpectSingleChar(ReadOnlySpan<char> str, char c)
		{
			if (*str[this.m_Position] != (ushort)c)
			{
				throw new InvalidOperationException(string.Format("Expected a '{0}' character at position {1} in : {2}", c, this.m_Position, str.ToString()));
			}
			this.m_Position++;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00062ADC File Offset: 0x00060CDC
		public unsafe int ExpectInt(ReadOnlySpan<char> str)
		{
			int pos = this.m_Position;
			int sign = 1;
			if (*str[pos] == 45)
			{
				sign = -1;
				pos++;
			}
			int value = 0;
			for (;;)
			{
				char i = (char)(*str[pos]);
				if (i < '0' || i > '9')
				{
					break;
				}
				value *= 10;
				value += (int)(i - '0');
				pos++;
			}
			if (this.m_Position == pos)
			{
				throw new InvalidOperationException(string.Format("Expected an int at position {0} in {1}", this.m_Position, str.ToString()));
			}
			this.m_Position = pos;
			return value * sign;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00062B68 File Offset: 0x00060D68
		public unsafe ReadOnlySpan<char> ExpectString(ReadOnlySpan<char> str)
		{
			int startPos = this.m_Position;
			if (*str[startPos] != 34)
			{
				throw new InvalidOperationException(string.Format("Expected a '\"' character at position {0} in {1}", this.m_Position, str.ToString()));
			}
			this.m_Position++;
			for (;;)
			{
				char c = (char)(*str[this.m_Position]);
				c |= ' ';
				if (c < 'a' || c > 'z')
				{
					break;
				}
				this.m_Position++;
			}
			if (*str[this.m_Position] != 34)
			{
				throw new InvalidOperationException(string.Format("Expected a closing '\"' character at position {0} in string: {1}", this.m_Position, str.ToString()));
			}
			if (this.m_Position - startPos == 1)
			{
				return ReadOnlySpan<char>.Empty;
			}
			ReadOnlySpan<char> readOnlySpan = str.Slice(startPos + 1, this.m_Position - startPos - 1);
			this.m_Position++;
			return readOnlySpan;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00062C5B File Offset: 0x00060E5B
		public unsafe bool AcceptSingleChar(ReadOnlySpan<char> str, char c)
		{
			if (*str[this.m_Position] != (ushort)c)
			{
				return false;
			}
			this.m_Position++;
			return true;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00062C80 File Offset: 0x00060E80
		public unsafe bool AcceptString(ReadOnlySpan<char> input, out ReadOnlySpan<char> output)
		{
			output = default(ReadOnlySpan<char>);
			int startPos = this.m_Position;
			int endPos = startPos;
			if (*input[endPos] != 34)
			{
				return false;
			}
			endPos++;
			for (;;)
			{
				char c = (char)(*input[endPos]);
				c |= ' ';
				if (c < 'a' || c > 'z')
				{
					break;
				}
				endPos++;
			}
			if (*input[endPos] != 34)
			{
				return false;
			}
			if (this.m_Position - startPos == 1)
			{
				output = ReadOnlySpan<char>.Empty;
			}
			else
			{
				output = input.Slice(startPos + 1, endPos - startPos - 1);
			}
			this.m_Position = endPos + 1;
			return true;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00062D18 File Offset: 0x00060F18
		public unsafe void AcceptInt(ReadOnlySpan<char> str)
		{
			if (*str[this.m_Position] == 45)
			{
				this.m_Position++;
			}
			for (;;)
			{
				char i = (char)(*str[this.m_Position]);
				if (i < '0' || i > '9')
				{
					break;
				}
				this.m_Position++;
			}
		}

		// Token: 0x04000C97 RID: 3223
		private int m_Position;
	}
}

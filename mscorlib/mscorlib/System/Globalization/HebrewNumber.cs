using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200069E RID: 1694
	internal class HebrewNumber
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x000CC110 File Offset: 0x000CA310
		internal static string ToString(int Number)
		{
			char c = '\0';
			StringBuilder stringBuilder = new StringBuilder();
			if (Number > 5000)
			{
				Number -= 5000;
			}
			int num = Number / 100;
			if (num > 0)
			{
				Number -= num * 100;
				for (int i = 0; i < num / 4; i++)
				{
					stringBuilder.Append('ת');
				}
				int num2 = num % 4;
				if (num2 > 0)
				{
					stringBuilder.Append((char)(1510 + num2));
				}
			}
			int num3 = Number / 10;
			Number %= 10;
			switch (num3)
			{
			case 0:
				c = '\0';
				break;
			case 1:
				c = 'י';
				break;
			case 2:
				c = 'כ';
				break;
			case 3:
				c = 'ל';
				break;
			case 4:
				c = 'מ';
				break;
			case 5:
				c = 'נ';
				break;
			case 6:
				c = 'ס';
				break;
			case 7:
				c = 'ע';
				break;
			case 8:
				c = 'פ';
				break;
			case 9:
				c = 'צ';
				break;
			}
			char c2 = (char)((Number > 0) ? (1488 + Number - 1) : 0);
			if (c2 == 'ה' && c == 'י')
			{
				c2 = 'ו';
				c = 'ט';
			}
			if (c2 == 'ו' && c == 'י')
			{
				c2 = 'ז';
				c = 'ט';
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '"');
			}
			else
			{
				stringBuilder.Append('\'');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000CC29C File Offset: 0x000CA49C
		internal static HebrewNumberParsingState ParseByChar(char ch, ref HebrewNumberParsingContext context)
		{
			HebrewNumber.HebrewToken hebrewToken;
			if (ch == '\'')
			{
				hebrewToken = HebrewNumber.HebrewToken.SingleQuote;
			}
			else if (ch == '"')
			{
				hebrewToken = HebrewNumber.HebrewToken.DoubleQuote;
			}
			else
			{
				int num = (int)(ch - 'א');
				if (num < 0 || num >= HebrewNumber.s_hebrewValues.Length)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				hebrewToken = HebrewNumber.s_hebrewValues[num].token;
				if (hebrewToken == HebrewNumber.HebrewToken.Invalid)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				context.result += (int)HebrewNumber.s_hebrewValues[num].value;
			}
			context.state = HebrewNumber.s_numberPasingState[(int)(context.state * HebrewNumber.HS.X00 + (sbyte)hebrewToken)];
			if (context.state == HebrewNumber.HS._err)
			{
				return HebrewNumberParsingState.InvalidHebrewNumber;
			}
			if (context.state == HebrewNumber.HS.END)
			{
				return HebrewNumberParsingState.FoundEndOfHebrewNumber;
			}
			return HebrewNumberParsingState.ContinueParsing;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000CC336 File Offset: 0x000CA536
		internal static bool IsDigit(char ch)
		{
			if (ch >= 'א' && ch <= HebrewNumber.s_maxHebrewNumberCh)
			{
				return HebrewNumber.s_hebrewValues[(int)(ch - 'א')].value >= 0;
			}
			return ch == '\'' || ch == '"';
		}

		// Token: 0x04001C3E RID: 7230
		private static readonly HebrewNumber.HebrewValue[] s_hebrewValues = new HebrewNumber.HebrewValue[]
		{
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 2),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 3),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 4),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 5),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 6),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 7),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 8),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit9, 9),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 10),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 20),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 30),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 40),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 50),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 60),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 70),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 80),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 90),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit100, 100),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 200),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 300),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit400, 400)
		};

		// Token: 0x04001C3F RID: 7231
		private static char s_maxHebrewNumberCh = (char)(1488 + HebrewNumber.s_hebrewValues.Length - 1);

		// Token: 0x04001C40 RID: 7232
		private static readonly HebrewNumber.HS[] s_numberPasingState = new HebrewNumber.HS[]
		{
			HebrewNumber.HS.S400,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X0,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_100,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err
		};

		// Token: 0x0200069F RID: 1695
		private enum HebrewToken : short
		{
			// Token: 0x04001C42 RID: 7234
			Invalid = -1,
			// Token: 0x04001C43 RID: 7235
			Digit400,
			// Token: 0x04001C44 RID: 7236
			Digit200_300,
			// Token: 0x04001C45 RID: 7237
			Digit100,
			// Token: 0x04001C46 RID: 7238
			Digit10,
			// Token: 0x04001C47 RID: 7239
			Digit1,
			// Token: 0x04001C48 RID: 7240
			Digit6_7,
			// Token: 0x04001C49 RID: 7241
			Digit7,
			// Token: 0x04001C4A RID: 7242
			Digit9,
			// Token: 0x04001C4B RID: 7243
			SingleQuote,
			// Token: 0x04001C4C RID: 7244
			DoubleQuote
		}

		// Token: 0x020006A0 RID: 1696
		private struct HebrewValue
		{
			// Token: 0x06003537 RID: 13623 RVA: 0x000CC55F File Offset: 0x000CA75F
			internal HebrewValue(HebrewNumber.HebrewToken token, short value)
			{
				this.token = token;
				this.value = value;
			}

			// Token: 0x04001C4D RID: 7245
			internal HebrewNumber.HebrewToken token;

			// Token: 0x04001C4E RID: 7246
			internal short value;
		}

		// Token: 0x020006A1 RID: 1697
		internal enum HS : sbyte
		{
			// Token: 0x04001C50 RID: 7248
			_err = -1,
			// Token: 0x04001C51 RID: 7249
			Start,
			// Token: 0x04001C52 RID: 7250
			S400,
			// Token: 0x04001C53 RID: 7251
			S400_400,
			// Token: 0x04001C54 RID: 7252
			S400_X00,
			// Token: 0x04001C55 RID: 7253
			S400_X0,
			// Token: 0x04001C56 RID: 7254
			X00_DQ,
			// Token: 0x04001C57 RID: 7255
			S400_X00_X0,
			// Token: 0x04001C58 RID: 7256
			X0_DQ,
			// Token: 0x04001C59 RID: 7257
			X,
			// Token: 0x04001C5A RID: 7258
			X0,
			// Token: 0x04001C5B RID: 7259
			X00,
			// Token: 0x04001C5C RID: 7260
			S400_DQ,
			// Token: 0x04001C5D RID: 7261
			S400_400_DQ,
			// Token: 0x04001C5E RID: 7262
			S400_400_100,
			// Token: 0x04001C5F RID: 7263
			S9,
			// Token: 0x04001C60 RID: 7264
			X00_S9,
			// Token: 0x04001C61 RID: 7265
			S9_DQ,
			// Token: 0x04001C62 RID: 7266
			END = 100
		}
	}
}

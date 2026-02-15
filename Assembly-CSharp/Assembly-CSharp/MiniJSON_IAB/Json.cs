using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniJSON_IAB
{
	// Token: 0x02001196 RID: 4502
	public static class Json
	{
		// Token: 0x060086C4 RID: 34500 RVA: 0x0000216A File Offset: 0x0000036A
		public static object Deserialize(string json)
		{
			return null;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Serialize(object obj)
		{
			return null;
		}

		// Token: 0x02001197 RID: 4503
		private sealed class Parser : IDisposable
		{
			// Token: 0x17001111 RID: 4369
			// (get) Token: 0x060086C6 RID: 34502 RVA: 0x000029CC File Offset: 0x00000BCC
			private char PeekChar
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x17001112 RID: 4370
			// (get) Token: 0x060086C7 RID: 34503 RVA: 0x000029CC File Offset: 0x00000BCC
			private char NextChar
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x060086C8 RID: 34504 RVA: 0x0000216A File Offset: 0x0000036A
			private string NextWord
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x060086C9 RID: 34505 RVA: 0x000029CC File Offset: 0x00000BCC
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					return Json.Parser.TOKEN.NONE;
				}
			}

			// Token: 0x060086CA RID: 34506 RVA: 0x000029CC File Offset: 0x00000BCC
			public static bool IsWordBreak(char c)
			{
				return false;
			}

			// Token: 0x060086CB RID: 34507 RVA: 0x00002739 File Offset: 0x00000939
			private Parser(string jsonString)
			{
			}

			// Token: 0x060086CC RID: 34508 RVA: 0x0000216A File Offset: 0x0000036A
			public static object Parse(string jsonString)
			{
				return null;
			}

			// Token: 0x060086CD RID: 34509 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x060086CE RID: 34510 RVA: 0x0000216A File Offset: 0x0000036A
			private Dictionary<string, object> ParseObject()
			{
				return null;
			}

			// Token: 0x060086CF RID: 34511 RVA: 0x0000216A File Offset: 0x0000036A
			private List<object> ParseArray()
			{
				return null;
			}

			// Token: 0x060086D0 RID: 34512 RVA: 0x0000216A File Offset: 0x0000036A
			private object ParseValue()
			{
				return null;
			}

			// Token: 0x060086D1 RID: 34513 RVA: 0x0000216A File Offset: 0x0000036A
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				return null;
			}

			// Token: 0x060086D2 RID: 34514 RVA: 0x0000216A File Offset: 0x0000036A
			private string ParseString()
			{
				return null;
			}

			// Token: 0x060086D3 RID: 34515 RVA: 0x0000216A File Offset: 0x0000036A
			private object ParseNumber()
			{
				return null;
			}

			// Token: 0x060086D4 RID: 34516 RVA: 0x0000216D File Offset: 0x0000036D
			private void EatWhitespace()
			{
			}

			// Token: 0x0400C14D RID: 49485
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x0400C14E RID: 49486
			private StringReader json;

			// Token: 0x02001198 RID: 4504
			private enum TOKEN
			{
				// Token: 0x0400C150 RID: 49488
				NONE,
				// Token: 0x0400C151 RID: 49489
				CURLY_OPEN,
				// Token: 0x0400C152 RID: 49490
				CURLY_CLOSE,
				// Token: 0x0400C153 RID: 49491
				SQUARED_OPEN,
				// Token: 0x0400C154 RID: 49492
				SQUARED_CLOSE,
				// Token: 0x0400C155 RID: 49493
				COLON,
				// Token: 0x0400C156 RID: 49494
				COMMA,
				// Token: 0x0400C157 RID: 49495
				STRING,
				// Token: 0x0400C158 RID: 49496
				NUMBER,
				// Token: 0x0400C159 RID: 49497
				TRUE,
				// Token: 0x0400C15A RID: 49498
				FALSE,
				// Token: 0x0400C15B RID: 49499
				NULL
			}
		}

		// Token: 0x02001199 RID: 4505
		private sealed class Serializer
		{
			// Token: 0x060086D5 RID: 34517 RVA: 0x00002739 File Offset: 0x00000939
			private Serializer()
			{
			}

			// Token: 0x060086D6 RID: 34518 RVA: 0x0000216A File Offset: 0x0000036A
			public static string Serialize(object obj)
			{
				return null;
			}

			// Token: 0x060086D7 RID: 34519 RVA: 0x0000216D File Offset: 0x0000036D
			private void SerializeValue(object value)
			{
			}

			// Token: 0x060086D8 RID: 34520 RVA: 0x0000216D File Offset: 0x0000036D
			private void SerializeObject(IDictionary obj)
			{
			}

			// Token: 0x060086D9 RID: 34521 RVA: 0x0000216D File Offset: 0x0000036D
			private void SerializeArray(IList anArray)
			{
			}

			// Token: 0x060086DA RID: 34522 RVA: 0x0000216D File Offset: 0x0000036D
			private void SerializeString(string str)
			{
			}

			// Token: 0x060086DB RID: 34523 RVA: 0x0000216D File Offset: 0x0000036D
			private void SerializeOther(object value)
			{
			}

			// Token: 0x0400C15C RID: 49500
			private StringBuilder builder;
		}
	}
}

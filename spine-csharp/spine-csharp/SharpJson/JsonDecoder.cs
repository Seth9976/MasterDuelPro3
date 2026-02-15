using System;
using System.Collections.Generic;

namespace SharpJson
{
	// Token: 0x02000006 RID: 6
	public class JsonDecoder
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002689 File Offset: 0x00000889
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002691 File Offset: 0x00000891
		public string errorMessage { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000269A File Offset: 0x0000089A
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000026A2 File Offset: 0x000008A2
		public bool parseNumbersAsFloat { get; set; }

		// Token: 0x06000017 RID: 23 RVA: 0x000026AB File Offset: 0x000008AB
		public JsonDecoder()
		{
			this.errorMessage = null;
			this.parseNumbersAsFloat = false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C1 File Offset: 0x000008C1
		public object Decode(string text)
		{
			this.errorMessage = null;
			this.lexer = new Lexer(text);
			this.lexer.parseNumbersAsFloat = this.parseNumbersAsFloat;
			return this.ParseValue();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026ED File Offset: 0x000008ED
		public static object DecodeText(string text)
		{
			return new JsonDecoder().Decode(text);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026FC File Offset: 0x000008FC
		private IDictionary<string, object> ParseObject()
		{
			Dictionary<string, object> table = new Dictionary<string, object>();
			this.lexer.NextToken();
			for (;;)
			{
				Lexer.Token token = this.lexer.LookAhead();
				if (token == Lexer.Token.None)
				{
					break;
				}
				if (token != Lexer.Token.Comma)
				{
					if (token == Lexer.Token.CurlyClose)
					{
						goto IL_0047;
					}
					string name = this.EvalLexer<string>(this.lexer.ParseString());
					if (this.errorMessage != null)
					{
						goto Block_4;
					}
					token = this.lexer.NextToken();
					if (token != Lexer.Token.Colon)
					{
						goto Block_5;
					}
					object value = this.ParseValue();
					if (this.errorMessage != null)
					{
						goto Block_6;
					}
					table[name] = value;
				}
				else
				{
					this.lexer.NextToken();
				}
			}
			this.TriggerError("Invalid token");
			return null;
			IL_0047:
			this.lexer.NextToken();
			return table;
			Block_4:
			return null;
			Block_5:
			this.TriggerError("Invalid token; expected ':'");
			return null;
			Block_6:
			return null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027B4 File Offset: 0x000009B4
		private IList<object> ParseArray()
		{
			List<object> array = new List<object>();
			this.lexer.NextToken();
			for (;;)
			{
				Lexer.Token token = this.lexer.LookAhead();
				if (token == Lexer.Token.None)
				{
					break;
				}
				if (token != Lexer.Token.Comma)
				{
					if (token == Lexer.Token.SquaredClose)
					{
						goto IL_0047;
					}
					object value = this.ParseValue();
					if (this.errorMessage != null)
					{
						goto Block_4;
					}
					array.Add(value);
				}
				else
				{
					this.lexer.NextToken();
				}
			}
			this.TriggerError("Invalid token");
			return null;
			IL_0047:
			this.lexer.NextToken();
			return array;
			Block_4:
			return null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002830 File Offset: 0x00000A30
		private object ParseValue()
		{
			switch (this.lexer.LookAhead())
			{
			case Lexer.Token.Null:
				this.lexer.NextToken();
				return null;
			case Lexer.Token.True:
				this.lexer.NextToken();
				return true;
			case Lexer.Token.False:
				this.lexer.NextToken();
				return false;
			case Lexer.Token.String:
				return this.EvalLexer<string>(this.lexer.ParseString());
			case Lexer.Token.Number:
				if (this.parseNumbersAsFloat)
				{
					return this.EvalLexer<float>(this.lexer.ParseFloatNumber());
				}
				return this.EvalLexer<double>(this.lexer.ParseDoubleNumber());
			case Lexer.Token.CurlyOpen:
				return this.ParseObject();
			case Lexer.Token.SquaredOpen:
				return this.ParseArray();
			}
			this.TriggerError("Unable to parse value");
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002916 File Offset: 0x00000B16
		private void TriggerError(string message)
		{
			this.errorMessage = string.Format("Error: '{0}' at line {1}", message, this.lexer.lineNumber);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002939 File Offset: 0x00000B39
		private T EvalLexer<T>(T value)
		{
			if (this.lexer.hasError)
			{
				this.TriggerError("Lexical error ocurred");
			}
			return value;
		}

		// Token: 0x0400001B RID: 27
		private Lexer lexer;
	}
}

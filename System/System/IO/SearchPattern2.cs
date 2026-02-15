using System;

namespace System.IO
{
	// Token: 0x02000368 RID: 872
	internal class SearchPattern2
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0005C4D1 File Offset: 0x0005A6D1
		public SearchPattern2(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0005C4DB File Offset: 0x0005A6DB
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005C4F8 File Offset: 0x0005A6F8
		public bool IsMatch(string text, bool ignorecase)
		{
			if (!this.hasWildcard && string.Compare(this.pattern, text, ignorecase) == 0)
			{
				return true;
			}
			string fileName = Path.GetFileName(text);
			if (!this.hasWildcard)
			{
				return string.Compare(this.pattern, fileName, ignorecase) == 0;
			}
			return this.Match(this.ops, fileName, 0);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005C54F File Offset: 0x0005A74F
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0005C55E File Offset: 0x0005A75E
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005C568 File Offset: 0x0005A768
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
				}
				else
				{
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
				return;
			}
			op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005C694 File Offset: 0x0005A894
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04000D00 RID: 3328
		private SearchPattern2.Op ops;

		// Token: 0x04000D01 RID: 3329
		private bool ignore;

		// Token: 0x04000D02 RID: 3330
		private bool hasWildcard;

		// Token: 0x04000D03 RID: 3331
		private string pattern;

		// Token: 0x04000D04 RID: 3332
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x04000D05 RID: 3333
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x02000369 RID: 873
		private class Op
		{
			// Token: 0x06001581 RID: 5505 RVA: 0x0005C796 File Offset: 0x0005A996
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04000D06 RID: 3334
			public SearchPattern2.OpCode Code;

			// Token: 0x04000D07 RID: 3335
			public string Argument;

			// Token: 0x04000D08 RID: 3336
			public SearchPattern2.Op Next;
		}

		// Token: 0x0200036A RID: 874
		private enum OpCode
		{
			// Token: 0x04000D0A RID: 3338
			ExactString,
			// Token: 0x04000D0B RID: 3339
			AnyChar,
			// Token: 0x04000D0C RID: 3340
			AnyString,
			// Token: 0x04000D0D RID: 3341
			End,
			// Token: 0x04000D0E RID: 3342
			True
		}
	}
}

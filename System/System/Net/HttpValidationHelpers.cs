using System;

namespace System.Net
{
	// Token: 0x0200037D RID: 893
	internal static class HttpValidationHelpers
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x0005E600 File Offset: 0x0005C800
		public static bool IsInvalidMethodOrHeaderString(string stringValue)
		{
			foreach (char c in stringValue)
			{
				if (c <= '/')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\r':
							return true;
						case '\v':
						case '\f':
							break;
						default:
							if (c == ' ')
							{
								return true;
							}
							break;
						}
					}
					else
					{
						if (c == '"')
						{
							return true;
						}
						switch (c)
						{
						case '\'':
						case '(':
						case ')':
						case ',':
						case '/':
							return true;
						}
					}
				}
				else if (c <= ']')
				{
					switch (c)
					{
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
						return true;
					default:
						switch (c)
						{
						case '[':
						case '\\':
						case ']':
							return true;
						}
						break;
					}
				}
				else if (c == '{' || c == '}')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000D4D RID: 3405
		private static readonly char[] s_httpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };
	}
}

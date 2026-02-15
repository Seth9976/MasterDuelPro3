using System;
using System.Collections;

namespace SevenZip.CommandLineParser
{
	// Token: 0x020001AA RID: 426
	public class Parser
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
		public Parser(int numSwitches)
		{
			this._switches = new SwitchResult[numSwitches];
			for (int i = 0; i < numSwitches; i++)
			{
				this._switches[i] = new SwitchResult();
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		private bool ParseString(string srcString, SwitchForm[] switchForms)
		{
			int len = srcString.Length;
			if (len == 0)
			{
				return false;
			}
			int pos = 0;
			if (!Parser.IsItSwitchChar(srcString[pos]))
			{
				return false;
			}
			while (pos < len)
			{
				if (Parser.IsItSwitchChar(srcString[pos]))
				{
					pos++;
				}
				int matchedSwitchIndex = 0;
				int maxLen = -1;
				for (int switchIndex = 0; switchIndex < this._switches.Length; switchIndex++)
				{
					int switchLen = switchForms[switchIndex].IDString.Length;
					if (switchLen > maxLen && pos + switchLen <= len && string.Compare(switchForms[switchIndex].IDString, 0, srcString, pos, switchLen, true) == 0)
					{
						matchedSwitchIndex = switchIndex;
						maxLen = switchLen;
					}
				}
				if (maxLen == -1)
				{
					throw new Exception("maxLen == kNoLen");
				}
				SwitchResult matchedSwitch = this._switches[matchedSwitchIndex];
				SwitchForm switchForm = switchForms[matchedSwitchIndex];
				if (!switchForm.Multi && matchedSwitch.ThereIs)
				{
					throw new Exception("switch must be single");
				}
				matchedSwitch.ThereIs = true;
				pos += maxLen;
				int tailSize = len - pos;
				SwitchType type = switchForm.Type;
				switch (type)
				{
				case SwitchType.PostMinus:
					if (tailSize == 0)
					{
						matchedSwitch.WithMinus = false;
					}
					else
					{
						matchedSwitch.WithMinus = srcString[pos] == '-';
						if (matchedSwitch.WithMinus)
						{
							pos++;
						}
					}
					break;
				case SwitchType.LimitedPostString:
				case SwitchType.UnLimitedPostString:
				{
					int minLen = switchForm.MinLen;
					if (tailSize < minLen)
					{
						throw new Exception("switch is not full");
					}
					if (type == SwitchType.UnLimitedPostString)
					{
						matchedSwitch.PostStrings.Add(srcString.Substring(pos));
						return true;
					}
					string stringSwitch = srcString.Substring(pos, minLen);
					pos += minLen;
					int i = minLen;
					while (i < switchForm.MaxLen && pos < len)
					{
						char c = srcString[pos];
						if (Parser.IsItSwitchChar(c))
						{
							break;
						}
						stringSwitch += c.ToString();
						i++;
						pos++;
					}
					matchedSwitch.PostStrings.Add(stringSwitch);
					break;
				}
				case SwitchType.PostChar:
				{
					if (tailSize < switchForm.MinLen)
					{
						throw new Exception("switch is not full");
					}
					string charSet = switchForm.PostCharSet;
					if (tailSize == 0)
					{
						matchedSwitch.PostCharIndex = -1;
					}
					else
					{
						int index = charSet.IndexOf(srcString[pos]);
						if (index < 0)
						{
							matchedSwitch.PostCharIndex = -1;
						}
						else
						{
							matchedSwitch.PostCharIndex = index;
							pos++;
						}
					}
					break;
				}
				}
			}
			return true;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001F838 File Offset: 0x0001DA38
		public void ParseStrings(SwitchForm[] switchForms, string[] commandStrings)
		{
			int numCommandStrings = commandStrings.Length;
			bool stopSwitch = false;
			for (int i = 0; i < numCommandStrings; i++)
			{
				string s = commandStrings[i];
				if (stopSwitch)
				{
					this.NonSwitchStrings.Add(s);
				}
				else if (s == "--")
				{
					stopSwitch = true;
				}
				else if (!this.ParseString(s, switchForms))
				{
					this.NonSwitchStrings.Add(s);
				}
			}
		}

		// Token: 0x17000087 RID: 135
		public SwitchResult this[int index]
		{
			get
			{
				return this._switches[index];
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		public static int ParseCommand(CommandForm[] commandForms, string commandString, out string postString)
		{
			for (int i = 0; i < commandForms.Length; i++)
			{
				string id = commandForms[i].IDString;
				if (commandForms[i].PostStringMode)
				{
					if (commandString.IndexOf(id) == 0)
					{
						postString = commandString.Substring(id.Length);
						return i;
					}
				}
				else if (commandString == id)
				{
					postString = "";
					return i;
				}
			}
			postString = "";
			return -1;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001F904 File Offset: 0x0001DB04
		private static bool ParseSubCharsCommand(int numForms, CommandSubCharsSet[] forms, string commandString, ArrayList indices)
		{
			indices.Clear();
			int numUsedChars = 0;
			for (int i = 0; i < numForms; i++)
			{
				CommandSubCharsSet charsSet = forms[i];
				int currentIndex = -1;
				int len = charsSet.Chars.Length;
				for (int j = 0; j < len; j++)
				{
					char c = charsSet.Chars[j];
					int newIndex = commandString.IndexOf(c);
					if (newIndex >= 0)
					{
						if (currentIndex >= 0)
						{
							return false;
						}
						if (commandString.IndexOf(c, newIndex + 1) >= 0)
						{
							return false;
						}
						currentIndex = j;
						numUsedChars++;
					}
				}
				if (currentIndex == -1 && !charsSet.EmptyAllowed)
				{
					return false;
				}
				indices.Add(currentIndex);
			}
			return numUsedChars == commandString.Length;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001F9AC File Offset: 0x0001DBAC
		private static bool IsItSwitchChar(char c)
		{
			return c == '-' || c == '/';
		}

		// Token: 0x04000B20 RID: 2848
		public ArrayList NonSwitchStrings = new ArrayList();

		// Token: 0x04000B21 RID: 2849
		private SwitchResult[] _switches;

		// Token: 0x04000B22 RID: 2850
		private const char kSwitchID1 = '-';

		// Token: 0x04000B23 RID: 2851
		private const char kSwitchID2 = '/';

		// Token: 0x04000B24 RID: 2852
		private const char kSwitchMinus = '-';

		// Token: 0x04000B25 RID: 2853
		private const string kStopSwitchParsing = "--";
	}
}

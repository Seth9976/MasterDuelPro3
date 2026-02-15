using System;
using System.Collections;

namespace SevenZip.CommandLineParser
{
	// Token: 0x02000029 RID: 41
	public class Parser
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00008420 File Offset: 0x00006620
		public Parser(int numSwitches)
		{
			this._switches = new SwitchResult[numSwitches];
			for (int i = 0; i < numSwitches; i++)
			{
				this._switches[i] = new SwitchResult();
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000846C File Offset: 0x0000666C
		private bool ParseString(string srcString, SwitchForm[] switchForms)
		{
			int length = srcString.Length;
			bool flag = length == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int i = 0;
				bool flag3 = !Parser.IsItSwitchChar(srcString[i]);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					while (i < length)
					{
						bool flag4 = Parser.IsItSwitchChar(srcString[i]);
						if (flag4)
						{
							i++;
						}
						int num = 0;
						int num2 = -1;
						for (int j = 0; j < this._switches.Length; j++)
						{
							int length2 = switchForms[j].IDString.Length;
							bool flag5 = length2 <= num2 || i + length2 > length;
							if (!flag5)
							{
								bool flag6 = string.Compare(switchForms[j].IDString, 0, srcString, i, length2, true) == 0;
								if (flag6)
								{
									num = j;
									num2 = length2;
								}
							}
						}
						bool flag7 = num2 == -1;
						if (flag7)
						{
							throw new Exception("maxLen == kNoLen");
						}
						SwitchResult switchResult = this._switches[num];
						SwitchForm switchForm = switchForms[num];
						bool flag8 = !switchForm.Multi && switchResult.ThereIs;
						if (flag8)
						{
							throw new Exception("switch must be single");
						}
						switchResult.ThereIs = true;
						i += num2;
						int num3 = length - i;
						SwitchType type = switchForm.Type;
						switch (type)
						{
						case SwitchType.PostMinus:
						{
							bool flag9 = num3 == 0;
							if (flag9)
							{
								switchResult.WithMinus = false;
							}
							else
							{
								switchResult.WithMinus = srcString[i] == '-';
								bool withMinus = switchResult.WithMinus;
								if (withMinus)
								{
									i++;
								}
							}
							break;
						}
						case SwitchType.LimitedPostString:
						case SwitchType.UnLimitedPostString:
						{
							int minLen = switchForm.MinLen;
							bool flag10 = num3 < minLen;
							if (flag10)
							{
								throw new Exception("switch is not full");
							}
							bool flag11 = type == SwitchType.UnLimitedPostString;
							if (flag11)
							{
								switchResult.PostStrings.Add(srcString.Substring(i));
								return true;
							}
							string text = srcString.Substring(i, minLen);
							i += minLen;
							int num4 = minLen;
							while (num4 < switchForm.MaxLen && i < length)
							{
								char c = srcString[i];
								bool flag12 = Parser.IsItSwitchChar(c);
								if (flag12)
								{
									break;
								}
								text += c.ToString();
								num4++;
								i++;
							}
							switchResult.PostStrings.Add(text);
							break;
						}
						case SwitchType.PostChar:
						{
							bool flag13 = num3 < switchForm.MinLen;
							if (flag13)
							{
								throw new Exception("switch is not full");
							}
							string postCharSet = switchForm.PostCharSet;
							bool flag14 = num3 == 0;
							if (flag14)
							{
								switchResult.PostCharIndex = -1;
							}
							else
							{
								int num5 = postCharSet.IndexOf(srcString[i]);
								bool flag15 = num5 < 0;
								if (flag15)
								{
									switchResult.PostCharIndex = -1;
								}
								else
								{
									switchResult.PostCharIndex = num5;
									i++;
								}
							}
							break;
						}
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008750 File Offset: 0x00006950
		public void ParseStrings(SwitchForm[] switchForms, string[] commandStrings)
		{
			int num = commandStrings.Length;
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				string text = commandStrings[i];
				bool flag2 = flag;
				if (flag2)
				{
					this.NonSwitchStrings.Add(text);
				}
				else
				{
					bool flag3 = text == "--";
					if (flag3)
					{
						flag = true;
					}
					else
					{
						bool flag4 = !this.ParseString(text, switchForms);
						if (flag4)
						{
							this.NonSwitchStrings.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x17000001 RID: 1
		public SwitchResult this[int index]
		{
			get
			{
				return this._switches[index];
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000087E4 File Offset: 0x000069E4
		public static int ParseCommand(CommandForm[] commandForms, string commandString, out string postString)
		{
			for (int i = 0; i < commandForms.Length; i++)
			{
				string idstring = commandForms[i].IDString;
				bool postStringMode = commandForms[i].PostStringMode;
				if (postStringMode)
				{
					bool flag = commandString.IndexOf(idstring) == 0;
					if (flag)
					{
						postString = commandString.Substring(idstring.Length);
						return i;
					}
				}
				else
				{
					bool flag2 = commandString == idstring;
					if (flag2)
					{
						postString = "";
						return i;
					}
				}
			}
			postString = "";
			return -1;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000886C File Offset: 0x00006A6C
		private static bool ParseSubCharsCommand(int numForms, CommandSubCharsSet[] forms, string commandString, ArrayList indices)
		{
			indices.Clear();
			int num = 0;
			int i = 0;
			while (i < numForms)
			{
				CommandSubCharsSet commandSubCharsSet = forms[i];
				int num2 = -1;
				int length = commandSubCharsSet.Chars.Length;
				for (int j = 0; j < length; j++)
				{
					char c = commandSubCharsSet.Chars[j];
					int num3 = commandString.IndexOf(c);
					bool flag = num3 >= 0;
					if (flag)
					{
						bool flag2 = num2 >= 0;
						if (flag2)
						{
							return false;
						}
						bool flag3 = commandString.IndexOf(c, num3 + 1) >= 0;
						if (flag3)
						{
							return false;
						}
						num2 = j;
						num++;
					}
				}
				bool flag4 = num2 == -1 && !commandSubCharsSet.EmptyAllowed;
				if (!flag4)
				{
					indices.Add(num2);
					i++;
					continue;
				}
				return false;
			}
			return num == commandString.Length;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008960 File Offset: 0x00006B60
		private static bool IsItSwitchChar(char c)
		{
			return c == '-' || c == '/';
		}

		// Token: 0x040000FE RID: 254
		public ArrayList NonSwitchStrings = new ArrayList();

		// Token: 0x040000FF RID: 255
		private SwitchResult[] _switches;

		// Token: 0x04000100 RID: 256
		private const char kSwitchID1 = '-';

		// Token: 0x04000101 RID: 257
		private const char kSwitchID2 = '/';

		// Token: 0x04000102 RID: 258
		private const char kSwitchMinus = '-';

		// Token: 0x04000103 RID: 259
		private const string kStopSwitchParsing = "--";
	}
}

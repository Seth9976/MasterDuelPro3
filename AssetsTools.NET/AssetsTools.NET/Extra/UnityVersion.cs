using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000089 RID: 137
	public class UnityVersion
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00010A77 File Offset: 0x0000EC77
		public UnityVersion()
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001BC48 File Offset: 0x00019E48
		public UnityVersion(string version)
		{
			string[] array = version.Split(new char[] { '.' });
			this.major = int.Parse(array[0]);
			this.minor = int.Parse(array[1]);
			int num = array[2].IndexOfAny(new char[] { 'f', 'p', 'a', 'b', 'c', 'x' });
			bool flag = num != -1;
			if (flag)
			{
				this.type = array[2][num].ToString();
				this.patch = int.Parse(array[2].Substring(0, num));
				string text = array[2].Substring(num + 1);
				bool flag2 = !int.TryParse(text, out this.typeNum);
				if (flag2)
				{
					string text2 = "";
					for (int i = 0; i < text.Length; i++)
					{
						bool flag3 = text[i] >= '0' && text[i] <= '9';
						if (!flag3)
						{
							break;
						}
						text2 += text[i].ToString();
					}
					bool flag4 = text2.Length > 0;
					if (flag4)
					{
						this.typeNum = int.Parse(text2);
					}
				}
			}
			else
			{
				this.patch = int.Parse(array[2]);
				this.type = "";
				this.typeNum = 0;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public override string ToString()
		{
			bool flag = this.type == string.Empty;
			string text;
			if (flag)
			{
				text = string.Format("{0}.{1}.{2}", this.major, this.minor, this.patch);
			}
			else
			{
				text = string.Format("{0}.{1}.{2}{3}{4}", new object[] { this.major, this.minor, this.patch, this.type, this.typeNum });
			}
			return text;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001BE5C File Offset: 0x0001A05C
		public ulong ToUInt64()
		{
			string text = this.type;
			if (!true)
			{
			}
			byte b;
			if (!(text == "a"))
			{
				if (!(text == "b"))
				{
					if (!(text == "c"))
					{
						if (!(text == "f"))
						{
							if (!(text == "p"))
							{
								if (!(text == "x"))
								{
									b = byte.MaxValue;
								}
								else
								{
									b = 5;
								}
							}
							else
							{
								b = 4;
							}
						}
						else
						{
							b = 3;
						}
					}
					else
					{
						b = 2;
					}
				}
				else
				{
					b = 1;
				}
			}
			else
			{
				b = 0;
			}
			if (!true)
			{
			}
			byte b2 = b;
			return (ulong)(((long)this.major << 48) | ((long)this.minor << 32) | ((long)this.patch << 16) | (long)((long)((ulong)b2) << 8) | (long)((ulong)this.typeNum));
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001BF1C File Offset: 0x0001A11C
		public static UnityVersion FromUInt64(ulong data)
		{
			UnityVersion unityVersion = new UnityVersion();
			unityVersion.major = (int)((data >> 48) & 65535UL);
			unityVersion.minor = (int)((data >> 32) & 65535UL);
			unityVersion.patch = (int)((data >> 16) & 65535UL);
			UnityVersion unityVersion2 = unityVersion;
			ulong num = (data >> 8) & 255UL;
			if (!true)
			{
			}
			ulong num2 = num;
			string text;
			if (num2 <= 5UL)
			{
				switch ((uint)num2)
				{
				case 0U:
					text = "a";
					goto IL_00B4;
				case 1U:
					text = "b";
					goto IL_00B4;
				case 2U:
					text = "c";
					goto IL_00B4;
				case 3U:
					text = "f";
					goto IL_00B4;
				case 4U:
					text = "p";
					goto IL_00B4;
				case 5U:
					text = "x";
					goto IL_00B4;
				}
			}
			text = "?";
			IL_00B4:
			if (!true)
			{
			}
			unityVersion2.type = text;
			unityVersion.typeNum = (int)(data & 255UL);
			return unityVersion;
		}

		// Token: 0x04000427 RID: 1063
		public int major;

		// Token: 0x04000428 RID: 1064
		public int minor;

		// Token: 0x04000429 RID: 1065
		public int patch;

		// Token: 0x0400042A RID: 1066
		public string type;

		// Token: 0x0400042B RID: 1067
		public int typeNum;
	}
}

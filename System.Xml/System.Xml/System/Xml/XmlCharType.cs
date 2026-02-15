using System;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000111 RID: 273
	internal struct XmlCharType
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00048A0C File Offset: 0x00046C0C
		private static object StaticLock
		{
			get
			{
				if (XmlCharType.s_Lock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref XmlCharType.s_Lock, obj, null);
				}
				return XmlCharType.s_Lock;
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00048A38 File Offset: 0x00046C38
		private static void InitInstance()
		{
			object staticLock = XmlCharType.StaticLock;
			lock (staticLock)
			{
				if (XmlCharType.s_CharProperties == null)
				{
					byte[] array = new byte[65536];
					XmlCharType.SetProperties(array, "\t\n\r\r  ", 1);
					XmlCharType.SetProperties(array, "AZazÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 2);
					XmlCharType.SetProperties(array, "AZ__azÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 4);
					XmlCharType.SetProperties(array, "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣", 8);
					XmlCharType.SetProperties(array, "\t\n\r\r \ud7ff\ue000\ufffd", 16);
					XmlCharType.SetProperties(array, "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣", 32);
					XmlCharType.SetProperties(array, " %';=\\^\ud7ff\ue000\ufffd", 64);
					XmlCharType.SetProperties(array, " !#%(;==?\ud7ff\ue000\ufffd", 128);
					Thread.MemoryBarrier();
					XmlCharType.s_CharProperties = array;
				}
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00048AFC File Offset: 0x00046CFC
		private static void SetProperties(byte[] chProps, string ranges, byte value)
		{
			for (int i = 0; i < ranges.Length; i += 2)
			{
				int j = (int)ranges[i];
				int num = (int)ranges[i + 1];
				while (j <= num)
				{
					int num2 = j;
					chProps[num2] |= value;
					j++;
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00048B43 File Offset: 0x00046D43
		private XmlCharType(byte[] charProperties)
		{
			this.charProperties = charProperties;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00048B4C File Offset: 0x00046D4C
		public static XmlCharType Instance
		{
			get
			{
				if (XmlCharType.s_CharProperties == null)
				{
					XmlCharType.InitInstance();
				}
				return new XmlCharType(XmlCharType.s_CharProperties);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00048B68 File Offset: 0x00046D68
		public bool IsWhiteSpace(char ch)
		{
			return (this.charProperties[(int)ch] & 1) > 0;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00048B77 File Offset: 0x00046D77
		public bool IsNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 8) > 0;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00048B86 File Offset: 0x00046D86
		public bool IsStartNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 4) > 0;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00048B95 File Offset: 0x00046D95
		public bool IsNameSingleChar(char ch)
		{
			return this.IsNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00048BA7 File Offset: 0x00046DA7
		public bool IsCharData(char ch)
		{
			return (this.charProperties[(int)ch] & 16) > 0;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00048BB7 File Offset: 0x00046DB7
		public bool IsPubidChar(char ch)
		{
			return ch < '\u0080' && ((int)"␀\0ﾻ꿿\uffff蟿\ufffe߿"[(int)(ch >> 4)] & (1 << (int)(ch & '\u000f'))) != 0;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00048BDD File Offset: 0x00046DDD
		internal bool IsTextChar(char ch)
		{
			return (this.charProperties[(int)ch] & 64) > 0;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00048BED File Offset: 0x00046DED
		public bool IsLetter(char ch)
		{
			return (this.charProperties[(int)ch] & 2) > 0;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00048BFC File Offset: 0x00046DFC
		public bool IsNCNameCharXml4e(char ch)
		{
			return (this.charProperties[(int)ch] & 32) > 0;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00048C0C File Offset: 0x00046E0C
		public bool IsStartNCNameCharXml4e(char ch)
		{
			return this.IsLetter(ch) || ch == '_';
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00048C1E File Offset: 0x00046E1E
		public bool IsNameCharXml4e(char ch)
		{
			return this.IsNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00048C30 File Offset: 0x00046E30
		public static bool IsDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00048C3C File Offset: 0x00046E3C
		internal static bool IsHighSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 56319);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00048C4E File Offset: 0x00046E4E
		internal static bool IsLowSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 56320, 57343);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00048C60 File Offset: 0x00046E60
		internal static bool IsSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 57343);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00048C72 File Offset: 0x00046E72
		internal static int CombineSurrogateChar(int lowChar, int highChar)
		{
			return (lowChar - 56320) | ((highChar - 55296 << 10) + 65536);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00048C8C File Offset: 0x00046E8C
		internal static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
		{
			int num = combinedChar - 65536;
			lowChar = (char)(56320 + num % 1024);
			highChar = (char)(55296 + num / 1024);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00048CC1 File Offset: 0x00046EC1
		internal bool IsOnlyWhitespace(string str)
		{
			return this.IsOnlyWhitespaceWithPos(str) == -1;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00048CD0 File Offset: 0x00046ED0
		internal int IsOnlyWhitespaceWithPos(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 1) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00048D08 File Offset: 0x00046F08
		internal int IsOnlyCharData(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 16) == 0)
					{
						if (i + 1 >= str.Length || !XmlCharType.IsHighSurrogate((int)str[i]) || !XmlCharType.IsLowSurrogate((int)str[i + 1]))
						{
							return i;
						}
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00048D6C File Offset: 0x00046F6C
		internal static bool IsOnlyDigits(string str, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00048D98 File Offset: 0x00046F98
		internal int IsPublicId(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!this.IsPubidChar(str[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00048DCB File Offset: 0x00046FCB
		private static bool InRange(int value, int start, int end)
		{
			return value - start <= end - start;
		}

		// Token: 0x04000722 RID: 1826
		private static object s_Lock;

		// Token: 0x04000723 RID: 1827
		private static volatile byte[] s_CharProperties;

		// Token: 0x04000724 RID: 1828
		internal byte[] charProperties;
	}
}

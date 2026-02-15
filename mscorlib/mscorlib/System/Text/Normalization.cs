using System;
using System.Runtime.CompilerServices;
using Mono.Globalization.Unicode;

namespace System.Text
{
	// Token: 0x0200030C RID: 780
	internal class Normalization
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x0006DF65 File Offset: 0x0006C165
		private unsafe static uint PropValue(int cp)
		{
			return (uint)Normalization.props[NormalizationTableUtil.PropIdx(cp)];
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0006DF74 File Offset: 0x0006C174
		private unsafe static int CharMapIdx(int cp)
		{
			return (int)Normalization.charMapIndex[NormalizationTableUtil.MapIdx(cp)];
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0006DF86 File Offset: 0x0006C186
		private unsafe static byte GetCombiningClass(int c)
		{
			return Normalization.combiningClass[NormalizationTableUtil.Combining.ToIndex(c)];
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0006DF9A File Offset: 0x0006C19A
		private unsafe static int GetPrimaryCompositeFromMapIndex(int src)
		{
			return (int)Normalization.mapIdxToComposite[NormalizationTableUtil.Composite.ToIndex(src)];
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006DFB1 File Offset: 0x0006C1B1
		private unsafe static int GetPrimaryCompositeHelperIndex(int cp)
		{
			return (int)Normalization.helperIndex[NormalizationTableUtil.Helper.ToIndex(cp)];
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006DFC8 File Offset: 0x0006C1C8
		private static string Compose(string source, int checkType)
		{
			StringBuilder stringBuilder = null;
			Normalization.Decompose(source, ref stringBuilder, (checkType == 2) ? 3 : 1);
			if (stringBuilder == null)
			{
				stringBuilder = Normalization.Combine(source, 0, checkType);
			}
			else
			{
				Normalization.Combine(stringBuilder, 0, checkType);
			}
			if (stringBuilder == null)
			{
				return source;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006E008 File Offset: 0x0006C208
		private static StringBuilder Combine(string source, int start, int checkType)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (Normalization.QuickCheck(source[i], checkType) != NormalizationCheck.Yes)
				{
					StringBuilder stringBuilder = new StringBuilder(source.Length + source.Length / 10);
					stringBuilder.Append(source);
					Normalization.Combine(stringBuilder, i, checkType);
					return stringBuilder;
				}
			}
			return null;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0006E05C File Offset: 0x0006C25C
		private static void Combine(StringBuilder sb, int i, int checkType)
		{
			Normalization.CombineHangul(sb, null, (i > 0) ? (i - 1) : i);
			while (i < sb.Length)
			{
				if (Normalization.QuickCheck(sb[i], checkType) == NormalizationCheck.Yes)
				{
					i++;
				}
				else
				{
					i = Normalization.TryComposeWithPreviousStarter(sb, null, i);
				}
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0006E09C File Offset: 0x0006C29C
		private static int CombineHangul(StringBuilder sb, string s, int current)
		{
			int num = ((sb != null) ? sb.Length : s.Length);
			int num2 = Normalization.Fetch(sb, s, current);
			int i = current + 1;
			while (i < num)
			{
				int num3 = Normalization.Fetch(sb, s, i);
				int num4 = num2 - 4352;
				if (0 > num4 || num4 >= 19)
				{
					goto IL_008A;
				}
				int num5 = num3 - 4449;
				if (0 > num5 || num5 >= 21)
				{
					goto IL_008A;
				}
				if (sb == null)
				{
					return -1;
				}
				num2 = 44032 + (num4 * 21 + num5) * 28;
				sb[i - 1] = (char)num2;
				sb.Remove(i, 1);
				i--;
				num--;
				IL_00E6:
				i++;
				continue;
				IL_008A:
				int num6 = num2 - 44032;
				if (0 <= num6 && num6 < 11172 && num6 % 28 == 0)
				{
					int num7 = num3 - 4519;
					if (0 < num7 && num7 < 28)
					{
						if (sb == null)
						{
							return -1;
						}
						num2 += num7;
						sb[i - 1] = (char)num2;
						sb.Remove(i, 1);
						i--;
						num--;
						goto IL_00E6;
					}
				}
				num2 = num3;
				goto IL_00E6;
			}
			return num;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0006E19B File Offset: 0x0006C39B
		private static int Fetch(StringBuilder sb, string s, int i)
		{
			if (sb == null)
			{
				return (int)s[i];
			}
			return (int)sb[i];
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0006E1B0 File Offset: 0x0006C3B0
		private static int TryComposeWithPreviousStarter(StringBuilder sb, string s, int current)
		{
			int num = current - 1;
			if (Normalization.GetCombiningClass(Normalization.Fetch(sb, s, current)) == 0)
			{
				if (num < 0 || Normalization.GetCombiningClass(Normalization.Fetch(sb, s, num)) != 0)
				{
					return current + 1;
				}
			}
			else
			{
				while (num >= 0 && Normalization.GetCombiningClass(Normalization.Fetch(sb, s, num)) != 0)
				{
					num--;
				}
				if (num < 0)
				{
					return current + 1;
				}
			}
			int num2 = Normalization.Fetch(sb, s, num);
			int primaryCompositeHelperIndex = Normalization.GetPrimaryCompositeHelperIndex(num2);
			if (primaryCompositeHelperIndex == 0)
			{
				return current + 1;
			}
			int num3 = ((sb != null) ? sb.Length : s.Length);
			int num4 = -1;
			for (int i = num + 1; i < num3; i++)
			{
				int num5 = Normalization.Fetch(sb, s, i);
				int num6 = (int)Normalization.GetCombiningClass(num5);
				if (num6 != num4)
				{
					int num7 = Normalization.TryCompose(primaryCompositeHelperIndex, num2, num5);
					if (num7 != 0)
					{
						if (sb == null)
						{
							return -1;
						}
						sb[num] = (char)num7;
						sb.Remove(i, 1);
						return current;
					}
					else
					{
						if (num6 == 0)
						{
							return i + 1;
						}
						num4 = num6;
					}
				}
			}
			return num3;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006E298 File Offset: 0x0006C498
		private unsafe static int TryCompose(int i, int starter, int candidate)
		{
			while (Normalization.mappedChars[i] == starter)
			{
				if (Normalization.mappedChars[i + 1] == candidate && Normalization.mappedChars[i + 2] == 0)
				{
					int primaryCompositeFromMapIndex = Normalization.GetPrimaryCompositeFromMapIndex(i);
					if ((Normalization.PropValue(primaryCompositeFromMapIndex) & 64U) == 0U)
					{
						return primaryCompositeFromMapIndex;
					}
				}
				while (Normalization.mappedChars[i] != 0)
				{
					i++;
				}
				i++;
			}
			return 0;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006E300 File Offset: 0x0006C500
		private static string Decompose(string source, int checkType)
		{
			StringBuilder stringBuilder = null;
			Normalization.Decompose(source, ref stringBuilder, checkType);
			if (stringBuilder == null)
			{
				return source;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006E324 File Offset: 0x0006C524
		private static void Decompose(string source, ref StringBuilder sb, int checkType)
		{
			int[] array = null;
			int num = 0;
			for (int i = 0; i < source.Length; i++)
			{
				if (Normalization.QuickCheck(source[i], checkType) == NormalizationCheck.No)
				{
					Normalization.DecomposeChar(ref sb, ref array, source, i, checkType, ref num);
				}
			}
			if (sb != null)
			{
				sb.Append(source, num, source.Length - num);
			}
			Normalization.ReorderCanonical(source, ref sb, 1);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006E384 File Offset: 0x0006C584
		private static void ReorderCanonical(string src, ref StringBuilder sb, int start)
		{
			if (sb == null)
			{
				for (int i = 1; i < src.Length; i++)
				{
					int num = (int)Normalization.GetCombiningClass((int)src[i]);
					if (num != 0 && (int)Normalization.GetCombiningClass((int)src[i - 1]) > num)
					{
						sb = new StringBuilder(src.Length);
						sb.Append(src, 0, src.Length);
						Normalization.ReorderCanonical(src, ref sb, i);
						return;
					}
				}
				return;
			}
			int j = start;
			while (j < sb.Length)
			{
				int num2 = (int)Normalization.GetCombiningClass((int)sb[j]);
				if (num2 == 0 || (int)Normalization.GetCombiningClass((int)sb[j - 1]) <= num2)
				{
					j++;
				}
				else
				{
					char c = sb[j - 1];
					sb[j - 1] = sb[j];
					sb[j] = c;
					if (j > 1)
					{
						j--;
					}
				}
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006E458 File Offset: 0x0006C658
		private static void DecomposeChar(ref StringBuilder sb, ref int[] buf, string s, int i, int checkType, ref int start)
		{
			if (sb == null)
			{
				sb = new StringBuilder(s.Length + 100);
			}
			sb.Append(s, start, i - start);
			if (buf == null)
			{
				buf = new int[19];
			}
			int canonical = Normalization.GetCanonical((int)s[i], buf, 0, checkType);
			for (int j = 0; j < canonical; j++)
			{
				if (buf[j] < 65535)
				{
					sb.Append((char)buf[j]);
				}
				else
				{
					sb.Append((char)(buf[j] >> 10));
					sb.Append((char)((buf[j] & 4095) + 56320));
				}
			}
			start = i + 1;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0006E500 File Offset: 0x0006C700
		public static NormalizationCheck QuickCheck(char c, int type)
		{
			switch (type)
			{
			case 1:
				if ('가' <= c && c <= '힣')
				{
					return NormalizationCheck.No;
				}
				if ((Normalization.PropValue((int)c) & 1U) == 0U)
				{
					return NormalizationCheck.Yes;
				}
				return NormalizationCheck.No;
			case 2:
			{
				uint num = Normalization.PropValue((int)c);
				if ((num & 16U) != 0U)
				{
					return NormalizationCheck.No;
				}
				if ((num & 32U) == 0U)
				{
					return NormalizationCheck.Yes;
				}
				return NormalizationCheck.Maybe;
			}
			case 3:
				if ('가' <= c && c <= '힣')
				{
					return NormalizationCheck.No;
				}
				if ((Normalization.PropValue((int)c) & 2U) == 0U)
				{
					return NormalizationCheck.Yes;
				}
				return NormalizationCheck.No;
			default:
			{
				uint num = Normalization.PropValue((int)c);
				if ((num & 4U) != 0U)
				{
					return NormalizationCheck.No;
				}
				if ((num & 8U) != 0U)
				{
					return NormalizationCheck.Maybe;
				}
				return NormalizationCheck.Yes;
			}
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0006E590 File Offset: 0x0006C790
		private static int GetCanonicalHangul(int s, int[] buf, int bufIdx)
		{
			int num = s - 44032;
			if (num < 0 || num >= 11172)
			{
				return bufIdx;
			}
			int num2 = 4352 + num / 588;
			int num3 = 4449 + num % 588 / 28;
			int num4 = 4519 + num % 28;
			buf[bufIdx++] = num2;
			buf[bufIdx++] = num3;
			if (num4 != 4519)
			{
				buf[bufIdx++] = num4;
			}
			buf[bufIdx] = 0;
			return bufIdx;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0006E608 File Offset: 0x0006C808
		private unsafe static int GetCanonical(int c, int[] buf, int bufIdx, int checkType)
		{
			int canonicalHangul = Normalization.GetCanonicalHangul(c, buf, bufIdx);
			if (canonicalHangul > bufIdx)
			{
				return canonicalHangul;
			}
			int num = Normalization.CharMapIdx(c);
			if (num == 0 || Normalization.mappedChars[num] == c)
			{
				buf[bufIdx++] = c;
			}
			else
			{
				while (Normalization.mappedChars[num] != 0)
				{
					int num2 = Normalization.mappedChars[num];
					if (num2 <= 65535 && Normalization.QuickCheck((char)num2, checkType) == NormalizationCheck.Yes)
					{
						buf[bufIdx++] = num2;
					}
					else
					{
						bufIdx = Normalization.GetCanonical(num2, buf, bufIdx, checkType);
					}
					num++;
				}
			}
			return bufIdx;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0006E68D File Offset: 0x0006C88D
		public static bool IsNormalized(string source, NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.IsNormalized(source, 1);
			default:
				return Normalization.IsNormalized(source, 0);
			case NormalizationForm.FormKC:
				return Normalization.IsNormalized(source, 2);
			case NormalizationForm.FormKD:
				return Normalization.IsNormalized(source, 3);
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0006E6CC File Offset: 0x0006C8CC
		public static bool IsNormalized(string source, int type)
		{
			int num = -1;
			int i = 0;
			while (i < source.Length)
			{
				int num2 = (int)Normalization.GetCombiningClass((int)source[i]);
				if (num2 != 0 && num2 < num)
				{
					return false;
				}
				num = num2;
				switch (Normalization.QuickCheck(source[i], type))
				{
				case NormalizationCheck.Yes:
					i++;
					break;
				case NormalizationCheck.No:
					return false;
				case NormalizationCheck.Maybe:
					if (type == 0 || type == 2)
					{
						return source == Normalization.Normalize(source, type);
					}
					i = Normalization.CombineHangul(null, source, (i > 0) ? (i - 1) : i);
					if (i < 0)
					{
						return false;
					}
					i = Normalization.TryComposeWithPreviousStarter(null, source, i);
					if (i < 0)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0006E76A File Offset: 0x0006C96A
		public static string Normalize(string source, NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.Normalize(source, 1);
			default:
				return Normalization.Normalize(source, 0);
			case NormalizationForm.FormKC:
				return Normalization.Normalize(source, 2);
			case NormalizationForm.FormKD:
				return Normalization.Normalize(source, 3);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0006E7A7 File Offset: 0x0006C9A7
		public static string Normalize(string source, int type)
		{
			switch (type)
			{
			case 1:
			case 3:
				return Normalization.Decompose(source, type);
			default:
				return Normalization.Compose(source, type);
			}
		}

		// Token: 0x06001C42 RID: 7234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void load_normalization_resource(out IntPtr props, out IntPtr mappedChars, out IntPtr charMapIndex, out IntPtr helperIndex, out IntPtr mapIdxToComposite, out IntPtr combiningClass);

		// Token: 0x06001C43 RID: 7235 RVA: 0x0006E7CC File Offset: 0x0006C9CC
		unsafe static Normalization()
		{
			object obj = Normalization.forLock;
			lock (obj)
			{
				IntPtr intPtr;
				IntPtr intPtr2;
				IntPtr intPtr3;
				IntPtr intPtr4;
				IntPtr intPtr5;
				IntPtr intPtr6;
				Normalization.load_normalization_resource(out intPtr, out intPtr2, out intPtr3, out intPtr4, out intPtr5, out intPtr6);
				Normalization.props = (byte*)(void*)intPtr;
				Normalization.mappedChars = (int*)(void*)intPtr2;
				Normalization.charMapIndex = (short*)(void*)intPtr3;
				Normalization.helperIndex = (short*)(void*)intPtr4;
				Normalization.mapIdxToComposite = (ushort*)(void*)intPtr5;
				Normalization.combiningClass = (byte*)(void*)intPtr6;
			}
			Normalization.isReady = true;
		}

		// Token: 0x04000CF4 RID: 3316
		private unsafe static byte* props;

		// Token: 0x04000CF5 RID: 3317
		private unsafe static int* mappedChars;

		// Token: 0x04000CF6 RID: 3318
		private unsafe static short* charMapIndex;

		// Token: 0x04000CF7 RID: 3319
		private unsafe static short* helperIndex;

		// Token: 0x04000CF8 RID: 3320
		private unsafe static ushort* mapIdxToComposite;

		// Token: 0x04000CF9 RID: 3321
		private unsafe static byte* combiningClass;

		// Token: 0x04000CFA RID: 3322
		private static object forLock = new object();

		// Token: 0x04000CFB RID: 3323
		public static readonly bool isReady;
	}
}

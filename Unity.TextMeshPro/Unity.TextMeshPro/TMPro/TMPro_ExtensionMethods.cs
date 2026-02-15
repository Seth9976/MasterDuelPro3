using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000A8 RID: 168
	public static class TMPro_ExtensionMethods
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0002E4E1 File Offset: 0x0002C6E1
		internal static int TagToInt(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}
			return (int)(((int)s[0] << 24) | ((int)s[1] << 16) | ((int)s[2] << 8) | s[3]);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002E514 File Offset: 0x0002C714
		public static int[] ToIntArray(this string text)
		{
			int[] intArray = new int[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				intArray[i] = (int)text[i];
			}
			return intArray;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002E54C File Offset: 0x0002C74C
		public static string ArrayToString(this char[] chars)
		{
			string s = string.Empty;
			int i = 0;
			while (i < chars.Length && chars[i] != '\0')
			{
				s += chars[i].ToString();
				i++;
			}
			return s;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002E588 File Offset: 0x0002C788
		public static string IntToString(this int[] unicodes)
		{
			char[] chars = new char[unicodes.Length];
			for (int i = 0; i < unicodes.Length; i++)
			{
				chars[i] = (char)unicodes[i];
			}
			return new string(chars);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		internal static string UintToString(this List<uint> unicodes)
		{
			char[] chars = new char[unicodes.Count];
			for (int i = 0; i < unicodes.Count; i++)
			{
				chars[i] = (char)unicodes[i];
			}
			return new string(chars);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
		public static string IntToString(this int[] unicodes, int start, int length)
		{
			if (start > unicodes.Length)
			{
				return string.Empty;
			}
			int end = Mathf.Min(start + length, unicodes.Length);
			char[] chars = new char[end - start];
			int writeIndex = 0;
			for (int i = start; i < end; i++)
			{
				chars[writeIndex++] = (char)unicodes[i];
			}
			return new string(chars);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002E648 File Offset: 0x0002C848
		public static int FindInstanceID<T>(this List<T> list, T target) where T : global::UnityEngine.Object
		{
			int targetID = target.GetInstanceID();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetInstanceID() == targetID)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002E689 File Offset: 0x0002C889
		public static bool Compare(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002E6C5 File Offset: 0x0002C8C5
		public static bool CompareRGB(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002E6F3 File Offset: 0x0002C8F3
		public static bool Compare(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002E72F File Offset: 0x0002C92F
		public static bool CompareRGB(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002E760 File Offset: 0x0002C960
		public static Color32 Multiply(this Color32 c1, Color32 c2)
		{
			byte b2 = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b2, g, b, a);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002E800 File Offset: 0x0002CA00
		public static Color32 Tint(this Color32 c1, Color32 c2)
		{
			byte b2 = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b2, g, b, a);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		public static Color32 Tint(this Color32 c1, float tint)
		{
			byte b2 = (byte)Mathf.Clamp((float)c1.r / 255f * tint * 255f, 0f, 255f);
			byte g = (byte)Mathf.Clamp((float)c1.g / 255f * tint * 255f, 0f, 255f);
			byte b = (byte)Mathf.Clamp((float)c1.b / 255f * tint * 255f, 0f, 255f);
			byte a = (byte)Mathf.Clamp((float)c1.a / 255f * tint * 255f, 0f, 255f);
			return new Color32(b2, g, b, a);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0002E94C File Offset: 0x0002CB4C
		internal static Color32 GammaToLinear(this Color32 c)
		{
			return new Color32(TMPro_ExtensionMethods.GammaToLinear(c.r), TMPro_ExtensionMethods.GammaToLinear(c.g), TMPro_ExtensionMethods.GammaToLinear(c.b), c.a);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002E97C File Offset: 0x0002CB7C
		private static byte GammaToLinear(byte value)
		{
			float v = (float)value / 255f;
			if (v <= 0.04045f)
			{
				return (byte)(v / 12.92f * 255f);
			}
			if (v < 1f)
			{
				return (byte)(Mathf.Pow((v + 0.055f) / 1.055f, 2.4f) * 255f);
			}
			if (v == 1f)
			{
				return byte.MaxValue;
			}
			return (byte)(Mathf.Pow(v, 2.2f) * 255f);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002E9F0 File Offset: 0x0002CBF0
		public static Color MinAlpha(this Color c1, Color c2)
		{
			float a = ((c1.a < c2.a) ? c1.a : c2.a);
			return new Color(c1.r, c1.g, c1.b, a);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0002EA34 File Offset: 0x0002CC34
		public static bool Compare(this Vector3 v1, Vector3 v2, int accuracy)
		{
			bool flag = (int)(v1.x * (float)accuracy) == (int)(v2.x * (float)accuracy);
			bool y = (int)(v1.y * (float)accuracy) == (int)(v2.y * (float)accuracy);
			bool z = (int)(v1.z * (float)accuracy) == (int)(v2.z * (float)accuracy);
			return flag && y && z;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002EA8C File Offset: 0x0002CC8C
		public static bool Compare(this Quaternion q1, Quaternion q2, int accuracy)
		{
			bool flag = (int)(q1.x * (float)accuracy) == (int)(q2.x * (float)accuracy);
			bool y = (int)(q1.y * (float)accuracy) == (int)(q2.y * (float)accuracy);
			bool z = (int)(q1.z * (float)accuracy) == (int)(q2.z * (float)accuracy);
			bool w = (int)(q1.w * (float)accuracy) == (int)(q2.w * (float)accuracy);
			return flag && y && z && w;
		}
	}
}

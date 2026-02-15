using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Utility;

namespace YgomSystem.Utility
{
	// Token: 0x02000549 RID: 1353
	public static class StringUtil
	{
		// Token: 0x06002B0C RID: 11020 RVA: 0x0000216A File Offset: 0x0000036A
		public static string NumToCommaString(int number)
		{
			return null;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0000216A File Offset: 0x0000036A
		public static string NumToCommaString(long number)
		{
			return null;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0000216A File Offset: 0x0000036A
		public static string BinaryToString(byte[] src)
		{
			return null;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0000216A File Offset: 0x0000036A
		public static string CreateText(int textId)
		{
			return null;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0000216A File Offset: 0x0000036A
		public static string CreateTextFromListItems(int listIdx)
		{
			return null;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0000216A File Offset: 0x0000036A
		public static string CreateTextImpl(int textId, Func<int> mixNumFunc, Func<int, Engine.DialogMixTextType> mixTypeFunc, Func<int, int> mixDataFunc)
		{
			return null;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMixedTextContainsTextID(int textID)
		{
			return false;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ChangeEffectColor(this string text, int eff)
		{
			return null;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Coloring(this string str, string color)
		{
			return null;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Resizing(this string str, int size)
		{
			return null;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Scaling(this string str, float scale)
		{
			return null;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Coloring(this string str, int color)
		{
			return null;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Coloring(this string str, Color color)
		{
			return null;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MergeTextWithSeparateline(this string orgstr, string targetstr)
		{
			return null;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Italic(this string str)
		{
			return null;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Bold(this string str)
		{
			return null;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Bracket(this string str)
		{
			return null;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0000216A File Offset: 0x0000036A
		public static string LenticularBracket(this string str)
		{
			return null;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCounterName(Engine.CounterType counter)
		{
			return null;
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0000216A File Offset: 0x0000036A
		public static string WildCardToRegEx(string pattern, bool notMatch = false)
		{
			return null;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MakeFormatedText(string baseText, TextGroupLoadHolder textGroupLoadHolder, List<object> args = null)
		{
			return null;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0000216A File Offset: 0x0000036A
		private static string InnerMakeFormatedText(string baseText, TextGroupLoadHolder textGroupLoadHolder, List<object> args)
		{
			return null;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0000216A File Offset: 0x0000036A
		private static string InnerMakeFormatedText(TextGroupLoadHolder textGroupLoadHolder, object source)
		{
			return null;
		}

		// Token: 0x04002A19 RID: 10777
		public const string ColorTagRed = "<color=#F39700>";

		// Token: 0x04002A1A RID: 10778
		public const string ColorTagBlue = "<color=#00D2FF>";

		// Token: 0x04002A1B RID: 10779
		public const string ColorTagOrange = "<color=#FFC200>";

		// Token: 0x04002A1C RID: 10780
		public const string EFFECTSEPERATER_TAG = "\ufffd";
	}
}

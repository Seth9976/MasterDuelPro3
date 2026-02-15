using System;
using System.Collections.Generic;

namespace YgomGame.Utility
{
	// Token: 0x0200082F RID: 2095
	public class RichTextExtensionTags
	{
		// Token: 0x06004096 RID: 16534 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DefineExtensionMarkupReplace(string src, string dst)
		{
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetExtensionMarkupReplace(string src)
		{
			return null;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveExtensionMarkupReplace(string src)
		{
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetHtArgs(Dictionary<string, object> args)
		{
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetHtArgs()
		{
			return null;
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0000216A File Offset: 0x0000036A
		public static object ConvertStringCommand(string str)
		{
			return null;
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0000216A File Offset: 0x0000036A
		private static object ProcArg(Dictionary<string, string> param, string aname)
		{
			return null;
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0000216A File Offset: 0x0000036A
		private static string ProcArgAndEnc(string replace, Dictionary<string, string> param)
		{
			return null;
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Init()
		{
		}

		// Token: 0x0400399B RID: 14747
		private static readonly string keywordCwork;

		// Token: 0x0400399C RID: 14748
		private static readonly string keywordHtarg;

		// Token: 0x0400399D RID: 14749
		private static Dictionary<string, string> replaceDefine;

		// Token: 0x0400399E RID: 14750
		private static Dictionary<string, object> htargs;
	}
}

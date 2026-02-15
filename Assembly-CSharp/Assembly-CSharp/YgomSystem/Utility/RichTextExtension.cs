using System;
using System.Collections.Generic;
using System.Text;

namespace YgomSystem.Utility
{
	// Token: 0x0200053A RID: 1338
	public class RichTextExtension
	{
		// Token: 0x06002AC5 RID: 10949 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsSupportedTag(string tag, string[] supportedTags)
		{
			return false;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0000216D File Offset: 0x0000036D
		private static void StartTags(StringBuilder outstr, List<KeyValuePair<string, string>> tagstack)
		{
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0000216D File Offset: 0x0000036D
		private static void EndTags(StringBuilder outstr, List<KeyValuePair<string, string>> tagstack)
		{
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Substring(string text, int startIndex, int length, string[] supportedTags)
		{
			return null;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0000216A File Offset: 0x0000036A
		public static string RemoveMarkup(string text, string[] supportedTags)
		{
			return null;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetInnermark(string text, int startIndex, int endIndex)
		{
			return null;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0000216A File Offset: 0x0000036A
		private static string RemoveInnermarkEndToken(string innermark)
		{
			return null;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetTag(string innermark)
		{
			return null;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Length(string text, string[] supportedTags)
		{
			return 0;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0000216A File Offset: 0x0000036A
		private static string xmlDecode(string src)
		{
			return null;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0000216A File Offset: 0x0000036A
		private static string xmlEncode(string src)
		{
			return null;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0000216D File Offset: 0x0000036D
		private static void passSpace(string param, ref int i)
		{
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0000216A File Offset: 0x0000036A
		private static string getId(string param, ref int i)
		{
			return null;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0000216A File Offset: 0x0000036A
		private static string getValue(string param, ref int i)
		{
			return null;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, string> GetParams(string param)
		{
			return null;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddMarkupTag(string tag, RichTextExtension.replaceMarkup func)
		{
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ProcessMarkup(string text)
		{
			return null;
		}

		// Token: 0x040029DD RID: 10717
		public static string[] UguiSupportedTags;

		// Token: 0x040029DE RID: 10718
		private static Dictionary<string, RichTextExtension.replaceMarkup> registTag;

		// Token: 0x0200053B RID: 1339
		// (Invoke) Token: 0x06002AD8 RID: 10968
		public delegate string replaceMarkup(ref string data, string value, string tag, Dictionary<string, string> param);
	}
}

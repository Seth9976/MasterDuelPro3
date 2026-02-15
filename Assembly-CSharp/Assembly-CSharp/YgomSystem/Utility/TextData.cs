using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YgomSystem.Utility
{
	// Token: 0x02000551 RID: 1361
	public sealed class TextData
	{
		// Token: 0x06002B83 RID: 11139 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load<T>()
		{
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync<T>()
		{
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(string groupid)
		{
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync(string groupid)
		{
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadGroup(string group)
		{
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadGroupAsync(string group)
		{
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload<T>()
		{
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload(string groupid)
		{
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UnloadGroup(string group)
		{
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reload()
		{
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reload<T>()
		{
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ReloadGroup(string group)
		{
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoad<T>()
		{
			return false;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoad(string groupid)
		{
			return false;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDone<T>()
		{
			return false;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDoneWithName(string fullTextId)
		{
			return false;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsErrorWithName(string fullTextId)
		{
			return false;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDoneWithGroup(string groupId)
		{
			return false;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsErrorWithGroup(string groupId)
		{
			return false;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsTextId(string fullTextId)
		{
			return false;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0000216A File Offset: 0x0000036A
		public static string EnumToFullTextId<T>(T textEnum)
		{
			return null;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000F1CB6 File Offset: 0x000EFEB6
		public static bool ParseTextId(string fullTextId, out string groupId, out string textId)
		{
			groupId = null;
			textId = null;
			return false;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MakeGroupId(string groupId)
		{
			return null;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MakeTextId(string groupId, string textId)
		{
			return null;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MakeTextId(string textId)
		{
			return null;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsText<T>(T TextEnum)
		{
			return false;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetText<T>(T TextEnum, bool richTextEx = false)
		{
			return null;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetReplaceText(string label, string text)
		{
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetReplaceText(string label)
		{
			return null;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReloadAllText()
		{
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLanguage(string lang, bool forceReload = false)
		{
		}

		// Token: 0x04002A34 RID: 10804
		private static string s_lang;

		// Token: 0x04002A35 RID: 10805
		private static Dictionary<string, TextData.TextGroup> s_text;

		// Token: 0x04002A36 RID: 10806
		private static Dictionary<string, string> s_replace;

		// Token: 0x02000552 RID: 1362
		internal class TextGroup
		{
			// Token: 0x06002BA3 RID: 11171 RVA: 0x0000216A File Offset: 0x0000036A
			internal string getResourcePath(string typename)
			{
				return null;
			}

			// Token: 0x06002BA4 RID: 11172 RVA: 0x00002739 File Offset: 0x00000939
			public TextGroup(string tp, bool async = false)
			{
			}

			// Token: 0x06002BA5 RID: 11173 RVA: 0x00002739 File Offset: 0x00000939
			public TextGroup(Type tp, bool async = false)
			{
			}

			// Token: 0x06002BA6 RID: 11174 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reload()
			{
			}

			// Token: 0x06002BA7 RID: 11175 RVA: 0x0000216D File Offset: 0x0000036D
			public void addRef()
			{
			}

			// Token: 0x06002BA8 RID: 11176 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool delRef()
			{
				return false;
			}

			// Token: 0x06002BA9 RID: 11177 RVA: 0x000029CC File Offset: 0x00000BCC
			public int getRef()
			{
				return 0;
			}

			// Token: 0x06002BAA RID: 11178 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isDone()
			{
				return false;
			}

			// Token: 0x06002BAB RID: 11179 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isError()
			{
				return false;
			}

			// Token: 0x06002BAC RID: 11180 RVA: 0x0000216D File Offset: 0x0000036D
			private void LoadRequestCompleteHandler(string path)
			{
			}

			// Token: 0x06002BAD RID: 11181 RVA: 0x0000216D File Offset: 0x0000036D
			private void initProc()
			{
			}

			// Token: 0x06002BAE RID: 11182 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ContainsText(string name)
			{
				return false;
			}

			// Token: 0x06002BAF RID: 11183 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetText(string name, bool richTextEx)
			{
				return null;
			}

			// Token: 0x04002A37 RID: 10807
			private int refCount;

			// Token: 0x04002A38 RID: 10808
			private string type;

			// Token: 0x04002A39 RID: 10809
			private string resourcePath;

			// Token: 0x04002A3A RID: 10810
			private Dictionary<string, string> texts;

			// Token: 0x04002A3B RID: 10811
			private bool scrambling;

			// Token: 0x04002A3C RID: 10812
			private bool m_isDone;

			// Token: 0x04002A3D RID: 10813
			private bool m_isError;

			// Token: 0x04002A3E RID: 10814
			private static Regex reg;

			// Token: 0x04002A3F RID: 10815
			private int SaftyRecursiveCheck;
		}
	}
}

using System;
using System.Collections.Generic;

namespace YgomSystem.Extension
{
	// Token: 0x0200076C RID: 1900
	public static class DictionaryExtension
	{
		// Token: 0x06003B2C RID: 15148 RVA: 0x000F3880 File Offset: 0x000F1A80
		public static T GetValue<T>(this Dictionary<string, object> dic, string key, Func<T, bool> predicator)
		{
			return default(T);
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000F3898 File Offset: 0x000F1A98
		public static T GetValueOrDefault<T>(this Dictionary<string, object> dic, string key, T defaultValue, Func<T, bool> predicator)
		{
			return default(T);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000F38B0 File Offset: 0x000F1AB0
		public static T GetValueOrNull<T>(this Dictionary<string, object> dic, string key, Func<T, bool> predicator) where T : class
		{
			return default(T);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000F37B2 File Offset: 0x000F19B2
		public static bool TryGetValue<T>(this Dictionary<string, object> dic, string key, out T value, T defaultValue = default(T))
		{
			value = default(T);
			return false;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetBool(this Dictionary<string, object> dic, string key)
		{
			return false;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetBoolOrDefault(this Dictionary<string, object> dic, string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000F38C6 File Offset: 0x000F1AC6
		public static bool TryGetBool(this Dictionary<string, object> dic, string key, out bool value, bool defaultValue = false)
		{
			value = false;
			return false;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetLong(this Dictionary<string, object> dic, string key)
		{
			return 0L;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetLongOrDefault(this Dictionary<string, object> dic, string key, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000F38CC File Offset: 0x000F1ACC
		public static bool TryGetLong(this Dictionary<string, object> dic, string key, out long value, long defaultValue = 0L)
		{
			value = 0L;
			return false;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetInt(this Dictionary<string, object> dic, string key)
		{
			return 0;
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetIntOrDefault(this Dictionary<string, object> dic, string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000F38D3 File Offset: 0x000F1AD3
		public static bool TryGetInt(this Dictionary<string, object> dic, string key, out int value, int defaultValue = 0)
		{
			value = 0;
			return false;
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetString(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStringOrEmpty(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStringOrNull(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000F38DC File Offset: 0x000F1ADC
		public static T GetEnum<T>(this Dictionary<string, object> dic, string key) where T : Enum
		{
			return default(T);
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000F38F4 File Offset: 0x000F1AF4
		public static T GetEnumOrDefault<T>(this Dictionary<string, object> dic, string key, T defaultValue = default(T)) where T : Enum
		{
			return default(T);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000F37B2 File Offset: 0x000F19B2
		public static bool TryGetEnum<T>(this Dictionary<string, object> dic, string key, out T value, T defaultValue = default(T)) where T : Enum
		{
			value = default(T);
			return false;
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetList(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetListOrEmpty(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetListOrNull(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDic(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDicOrEmpty(this Dictionary<string, object> dic, string key)
		{
			return null;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDicOrNull(this Dictionary<string, object> dic, string key)
		{
			return null;
		}
	}
}

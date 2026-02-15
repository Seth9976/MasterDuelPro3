using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000522 RID: 1314
	public static class JsonPath
	{
		// Token: 0x06002A3B RID: 10811 RVA: 0x0000216A File Offset: 0x0000036A
		private static string procForm(string jsonPath, object scan)
		{
			return null;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> parth(string jsonPath, object scan)
		{
			return null;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000F1B4E File Offset: 0x000EFD4E
		public static object get(object scan, string jsonPath, out object lastparent, out string lastkey)
		{
			lastparent = null;
			lastkey = null;
			return null;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0000216A File Offset: 0x0000036A
		public static object get(object scan, string jsonPath)
		{
			return null;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool set(object dst, string jsonPath, object value)
		{
			return false;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool contains(object scan, string jsonPath)
		{
			return false;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool getBool(object scan, string jsonPath, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int getInt(object scan, string jsonPath, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long getLong(object scan, string jsonPath, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float getFloat(object scan, string jsonPath, float defaultValue = 0f)
		{
			return 0f;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getString(object scan, string jsonPath, string defaultValue = "")
		{
			return null;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000F1B58 File Offset: 0x000EFD58
		public static T getEnum<T>(object scan, string jsonPath, T defaultValue)
		{
			return default(T);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> getDictionary(object scan, string jsonPath, Dictionary<string, object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> getList(object scan, string jsonPath, List<object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000F1B70 File Offset: 0x000EFD70
		public static Vector2 getVector2(object scan, string jsonPath, Vector2 defalutValie)
		{
			return default(Vector2);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000F1B88 File Offset: 0x000EFD88
		public static Vector3 getVector3(object scan, string jsonPath, Vector3 defalutValie)
		{
			return default(Vector3);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000F1BA0 File Offset: 0x000EFDA0
		public static Vector4 getVector4(object scan, string jsonPath, Vector4 defalutValie)
		{
			return default(Vector4);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000F1BB8 File Offset: 0x000EFDB8
		internal static T getTyped<T>(object scan, string jsonPath, T defaultValue)
		{
			return default(T);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool objectToBool(object i, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float objectToFloat(object i, float defaultValue = 0f)
		{
			return 0f;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int objectToInt(object i, int detaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0000216A File Offset: 0x0000036A
		public static string objectToString(object i, string detaultValue = null)
		{
			return null;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long objectToLong(object i, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000F1BD0 File Offset: 0x000EFDD0
		public static Vector2 objectToVector2(object scan, Vector2 defalutValie)
		{
			return default(Vector2);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000F1BE8 File Offset: 0x000EFDE8
		public static Vector3 objectToVector3(object scan, Vector3 defalutValie)
		{
			return default(Vector3);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000F1C00 File Offset: 0x000EFE00
		public static Vector4 objectToVector4(object scan, Vector4 defalutValie)
		{
			return default(Vector4);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000F1C18 File Offset: 0x000EFE18
		public static Color objectToColor(object c, Color degalutValue)
		{
			return default(Color);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> objectToStringList(object obj)
		{
			return null;
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0000216A File Offset: 0x0000036A
		private static string jsonString(string str)
		{
			return null;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0000216A File Offset: 0x0000036A
		public static string objectToJson(object obj)
		{
			return null;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0000216A File Offset: 0x0000036A
		public static string objectToFormatJson(object obj, int tabdepth = 0)
		{
			return null;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0000216A File Offset: 0x0000036A
		public static string objectToFormatJsonFistComma(object obj, int tabdepth = 0)
		{
			return null;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0000216A File Offset: 0x0000036A
		public static string objectToYaml(object obj, int tabdepth = 0)
		{
			return null;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0000216A File Offset: 0x0000036A
		public static object copyObject(object src)
		{
			return null;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0000216D File Offset: 0x0000036D
		public static void margeList(List<object> dst, List<object> src)
		{
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0000216D File Offset: 0x0000036D
		public static void margeDictionary(Dictionary<string, object> dst, Dictionary<string, object> src)
		{
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0000216D File Offset: 0x0000036D
		public static void margeDictionaryWithAlias(Dictionary<string, object> dst, Dictionary<string, object> src, Dictionary<string, object> alias)
		{
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> joinDictionary(Dictionary<string, object> dic1, Dictionary<string, object> dic2)
		{
			return null;
		}

		// Token: 0x04002987 RID: 10631
		private static Regex makupRegex;

		// Token: 0x04002988 RID: 10632
		private const string onetab = "\t";

		// Token: 0x04002989 RID: 10633
		private static char[] strescape;

		// Token: 0x0400298A RID: 10634
		private const string yamltab = "  ";
	}
}

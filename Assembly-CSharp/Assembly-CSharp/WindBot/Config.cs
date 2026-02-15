using System;
using System.Collections.Generic;
using System.IO;

namespace WindBot
{
	// Token: 0x020001E8 RID: 488
	public static class Config
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x00027C00 File Offset: 0x00025E00
		public static void Load(string[] args)
		{
			Config._integerCache = new Dictionary<string, int>();
			Config._booleanCache = new Dictionary<string, bool>();
			Config._fields = Config.LoadArgs(args);
			string filename = Config.GetString(Config.CONFIG_FILE_OPTION, null);
			if (filename != null)
			{
				foreach (KeyValuePair<string, string> pair in Config.LoadFile(filename))
				{
					if (!Config._fields.ContainsKey(pair.Key))
					{
						Config._fields.Add(pair.Key, pair.Value);
					}
				}
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00027CA8 File Offset: 0x00025EA8
		private static Dictionary<string, string> LoadArgs(string[] args)
		{
			Dictionary<string, string> fields = new Dictionary<string, string>();
			foreach (string option in args)
			{
				int position = option.IndexOf(Config.SEPARATOR_CHAR);
				if (position == -1)
				{
					throw new Exception("Invalid argument '" + option + "': no key/value separator");
				}
				string key = option.Substring(0, position).Trim().ToUpper();
				string value = option.Substring(position + 1).Trim();
				if (fields.ContainsKey(key))
				{
					throw new Exception(string.Concat(new string[] { "Invalid argument '", option, "': duplicate key '", key, "'" }));
				}
				fields.Add(key, value);
			}
			return fields;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00027D64 File Offset: 0x00025F64
		private static Dictionary<string, string> LoadFile(string filename)
		{
			Dictionary<string, string> fields = new Dictionary<string, string>();
			using (StreamReader reader = new StreamReader(filename))
			{
				int lineNumber = 0;
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine().Trim();
					lineNumber++;
					if (line.Length != 0 && line[0] != Config.COMMENT_CHAR)
					{
						int position = line.IndexOf(Config.SEPARATOR_CHAR);
						if (position == -1)
						{
							throw new Exception("Invalid configuration file: no key/value separator line " + lineNumber.ToString());
						}
						string key = line.Substring(0, position).Trim().ToUpper();
						string value = line.Substring(position + 1).Trim();
						if (fields.ContainsKey(key))
						{
							throw new Exception("Invalid configuration file: duplicate key '" + key + "' line " + lineNumber.ToString());
						}
						fields.Add(key, value);
					}
				}
			}
			return fields;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00027E5C File Offset: 0x0002605C
		public static string GetString(string key, string defaultValue = null)
		{
			key = key.ToUpper();
			if (Config._fields.ContainsKey(key))
			{
				return Config._fields[key];
			}
			return defaultValue;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00027E80 File Offset: 0x00026080
		public static int GetInt(string key, int defaultValue = 0)
		{
			key = key.ToUpper();
			if (Config._integerCache.ContainsKey(key))
			{
				return Config._integerCache[key];
			}
			int value = defaultValue;
			if (Config._fields.ContainsKey(key))
			{
				if (Config._fields[key].StartsWith("0x"))
				{
					value = Convert.ToInt32(Config._fields[key], 16);
				}
				else
				{
					value = Convert.ToInt32(Config._fields[key]);
				}
			}
			Config._integerCache.Add(key, value);
			return value;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00027F07 File Offset: 0x00026107
		public static uint GetUInt(string key, uint defaultValue = 0U)
		{
			return (uint)Config.GetInt(key, (int)defaultValue);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00027F10 File Offset: 0x00026110
		public static bool GetBool(string key, bool defaultValue = false)
		{
			key = key.ToUpper();
			if (Config._booleanCache.ContainsKey(key))
			{
				return Config._booleanCache[key];
			}
			bool value = defaultValue;
			if (Config._fields.ContainsKey(key))
			{
				value = Convert.ToBoolean(Config._fields[key]);
			}
			Config._booleanCache.Add(key, value);
			return value;
		}

		// Token: 0x04000D28 RID: 3368
		private static string CONFIG_FILE_OPTION = "Config";

		// Token: 0x04000D29 RID: 3369
		private static char SEPARATOR_CHAR = '=';

		// Token: 0x04000D2A RID: 3370
		private static char COMMENT_CHAR = '#';

		// Token: 0x04000D2B RID: 3371
		private static Dictionary<string, string> _fields;

		// Token: 0x04000D2C RID: 3372
		private static Dictionary<string, int> _integerCache;

		// Token: 0x04000D2D RID: 3373
		private static Dictionary<string, bool> _booleanCache;
	}
}

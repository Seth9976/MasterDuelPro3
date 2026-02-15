using System;
using System.Collections.Generic;
using System.IO;

namespace YGOSharp
{
	// Token: 0x020001B2 RID: 434
	public static class Config
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0001FF18 File Offset: 0x0001E118
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

		// Token: 0x06000684 RID: 1668 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
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

		// Token: 0x06000685 RID: 1669 RVA: 0x0002007C File Offset: 0x0001E27C
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

		// Token: 0x06000686 RID: 1670 RVA: 0x00020174 File Offset: 0x0001E374
		public static string GetString(string key, string defaultValue = null)
		{
			key = key.ToUpper();
			if (Config._fields.ContainsKey(key))
			{
				return Config._fields[key];
			}
			return defaultValue;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00020198 File Offset: 0x0001E398
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

		// Token: 0x06000688 RID: 1672 RVA: 0x0002021F File Offset: 0x0001E41F
		public static uint GetUInt(string key, uint defaultValue = 0U)
		{
			return (uint)Config.GetInt(key, (int)defaultValue);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00020228 File Offset: 0x0001E428
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

		// Token: 0x04000B37 RID: 2871
		private static string CONFIG_FILE_OPTION = "Config";

		// Token: 0x04000B38 RID: 2872
		private static char SEPARATOR_CHAR = '=';

		// Token: 0x04000B39 RID: 2873
		private static char COMMENT_CHAR = '#';

		// Token: 0x04000B3A RID: 2874
		private static Dictionary<string, string> _fields;

		// Token: 0x04000B3B RID: 2875
		private static Dictionary<string, int> _integerCache;

		// Token: 0x04000B3C RID: 2876
		private static Dictionary<string, bool> _booleanCache;
	}
}

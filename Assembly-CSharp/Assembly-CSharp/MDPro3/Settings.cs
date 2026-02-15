using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MDPro3.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MDPro3
{
	// Token: 0x02001240 RID: 4672
	public static class Settings
	{
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06008A05 RID: 35333 RVA: 0x0010EEC4 File Offset: 0x0010D0C4
		public static SettingData Data
		{
			get
			{
				if (Settings._data == null)
				{
					Settings.Initialize();
				}
				return Settings._data;
			}
		}

		// Token: 0x06008A06 RID: 35334 RVA: 0x0010EED8 File Offset: 0x0010D0D8
		public static void Initialize()
		{
			if (!File.Exists("Data/Settings.json"))
			{
				Settings._data = new SettingData();
				Settings.SaveSettings(Settings._data);
				return;
			}
			string json = File.ReadAllText("Data/Settings.json");
			try
			{
				Settings._data = Settings.EnsureDefaultValues(json);
			}
			catch (JsonReaderException ex)
			{
				MessageManager.Cast("Failed to parse Settings.json: " + ex.Message);
				Settings._data = new SettingData();
			}
		}

		// Token: 0x06008A07 RID: 35335 RVA: 0x0010EF50 File Offset: 0x0010D150
		public static string GetPrereleasePackUrl()
		{
			string config = Language.GetConfig();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(config);
			if (num <= 2194893224U)
			{
				if (num <= 1434653370U)
				{
					if (num != 376747596U)
					{
						if (num == 1434653370U)
						{
							if (config == "it-IT")
							{
								return Settings.Data.PrereleasePackUrl_IT;
							}
						}
					}
					else if (config == "fr-FR")
					{
						return Settings.Data.PrereleasePackUrl_FR;
					}
				}
				else if (num != 1969244152U)
				{
					if (num == 2194893224U)
					{
						if (config == "es-ES")
						{
							return Settings.Data.PrereleasePackUrl_ES;
						}
					}
				}
				else if (config == "pt-PT")
				{
					return Settings.Data.PrereleasePackUrl_PT;
				}
			}
			else if (num <= 2328506441U)
			{
				if (num != 2196609786U)
				{
					if (num == 2328506441U)
					{
						if (config == "ja-JP")
						{
							return Settings.Data.PrereleasePackUrl_JP;
						}
					}
				}
				else if (config == "de-DE")
				{
					return Settings.Data.PrereleasePackUrl_DE;
				}
			}
			else if (num != 2586248143U)
			{
				if (num != 2723579257U)
				{
					if (num == 3973517379U)
					{
						if (config == "zh-TW")
						{
							return Settings.Data.PrereleasePackUrl_TW;
						}
					}
				}
				else if (config == "en-US")
				{
					return Settings.Data.PrereleasePackUrl_EN;
				}
			}
			else if (config == "ko-KR")
			{
				return Settings.Data.PrereleasePackUrl_KR;
			}
			return Settings.Data.PrereleasePackUrl;
		}

		// Token: 0x06008A08 RID: 35336 RVA: 0x0010F11C File Offset: 0x0010D31C
		public static string GetPrereleasePackVersionUrl()
		{
			string config = Language.GetConfig();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(config);
			if (num <= 2194893224U)
			{
				if (num <= 1434653370U)
				{
					if (num != 376747596U)
					{
						if (num == 1434653370U)
						{
							if (config == "it-IT")
							{
								return Settings.Data.PrereleasePackVersionUrl_IT;
							}
						}
					}
					else if (config == "fr-FR")
					{
						return Settings.Data.PrereleasePackVersionUrl_FR;
					}
				}
				else if (num != 1969244152U)
				{
					if (num == 2194893224U)
					{
						if (config == "es-ES")
						{
							return Settings.Data.PrereleasePackVersionUrl_ES;
						}
					}
				}
				else if (config == "pt-PT")
				{
					return Settings.Data.PrereleasePackVersionUrl_PT;
				}
			}
			else if (num <= 2328506441U)
			{
				if (num != 2196609786U)
				{
					if (num == 2328506441U)
					{
						if (config == "ja-JP")
						{
							return Settings.Data.PrereleasePackVersionUrl_JP;
						}
					}
				}
				else if (config == "de-DE")
				{
					return Settings.Data.PrereleasePackVersionUrl_DE;
				}
			}
			else if (num != 2586248143U)
			{
				if (num != 2723579257U)
				{
					if (num == 3973517379U)
					{
						if (config == "zh-TW")
						{
							return Settings.Data.PrereleasePackVersionUrl_TW;
						}
					}
				}
				else if (config == "en-US")
				{
					return Settings.Data.PrereleasePackVersionUrl_EN;
				}
			}
			else if (config == "ko-KR")
			{
				return Settings.Data.PrereleasePackVersionUrl_KR;
			}
			return Settings.Data.PrereleasePackVersionUrl;
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x0010F2E8 File Offset: 0x0010D4E8
		private static void SaveSettings(SettingData data)
		{
			string json = JsonConvert.SerializeObject(data, Formatting.Indented);
			File.WriteAllText("Data/Settings.json", json);
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x0010F308 File Offset: 0x0010D508
		private static SettingData EnsureDefaultValues(string json)
		{
			SettingData data = JsonConvert.DeserializeObject<SettingData>(json);
			bool needOverwrite = false;
			if (Settings.GetMissingFields<SettingData>(JObject.Parse(json)).Count > 0)
			{
				needOverwrite = true;
			}
			if (data.MDPro3VersionUrl == "https://code.moenext.com/sherry_chaos/MDPro3/-/raw/master/Version.txt" || data.MDPro3VersionUrl == "https://code.moenext.com/sherry_chaos/MDPro3/-/raw/master/version.txt")
			{
				data.MDPro3VersionUrl = "https://cdn02.moecube.com:444/mdpro3-data/Version.txt";
				needOverwrite = true;
			}
			if (needOverwrite)
			{
				Settings.SaveSettings(data);
			}
			return data;
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x0010F370 File Offset: 0x0010D570
		private static List<string> GetMissingFields<T>(JObject jObject)
		{
			List<string> missingFields = new List<string>();
			foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!jObject.ContainsKey(field.Name))
				{
					missingFields.Add(field.Name);
				}
			}
			return missingFields;
		}

		// Token: 0x0400C540 RID: 50496
		private const string JsonPath = "Data/Settings.json";

		// Token: 0x0400C541 RID: 50497
		private const string MDPRO3_VERSION_URL_OLD = "https://code.moenext.com/sherry_chaos/MDPro3/-/raw/master/Version.txt";

		// Token: 0x0400C542 RID: 50498
		private const string MDPRO3_VERSION_URL_FALSE = "https://code.moenext.com/sherry_chaos/MDPro3/-/raw/master/version.txt";

		// Token: 0x0400C543 RID: 50499
		private const string MDPRO3_VERSION_URL_DEFAULT = "https://cdn02.moecube.com:444/mdpro3-data/Version.txt";

		// Token: 0x0400C544 RID: 50500
		private static SettingData _data;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MDPro3
{
	// Token: 0x02001294 RID: 4756
	[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
	public class Items : ScriptableObject
	{
		// Token: 0x06008B7E RID: 35710 RVA: 0x0011C91C File Offset: 0x0011AB1C
		public void Initialize()
		{
			if (!Items.initialized)
			{
				Items.instance = this;
				this.kinds = new List<List<Items.Item>> { this.wallpapers, this.faces, this.frames, this.protectors, this.mats, this.graves, this.stands, this.mates, this.cases };
				Items.initialized = true;
			}
			string currentLanguage = Language.GetConfig();
			if (Items.language != currentLanguage)
			{
				Items.language = currentLanguage;
				this.Load();
			}
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x0011C9D8 File Offset: 0x0011ABD8
		private void Load()
		{
			this.LoadText("Data/locales/" + Items.language + "/IDS/IDS_ITEM.txt");
			this.LoadText("Data/locales/" + Items.language + "/IDS/IDS_ITEMDESC.txt");
			this.LoadText("Data/locales/" + Items.language + "/IDS/IDS_CATEGORY.txt");
		}

		// Token: 0x06008B80 RID: 35712 RVA: 0x0011CA34 File Offset: 0x0011AC34
		private void LoadText(string path)
		{
			int type = 0;
			if (path.EndsWith("IDS_ITEMDESC.txt"))
			{
				type = 1;
			}
			if (path.EndsWith("IDS_CATEGORY.txt"))
			{
				type = 2;
			}
			Dictionary<int, string> targetDic = this.GetDic(type);
			targetDic.Clear();
			string[] array = File.ReadAllText(path).Replace("\r", string.Empty).Split('\n', StringSplitOptions.None);
			int currentKey = 0;
			List<string> currentValue = null;
			bool notNeed = false;
			foreach (string line in array)
			{
				int key;
				if (this.TryParseKey(line, out key, type))
				{
					if (currentValue != null)
					{
						targetDic.Add(currentKey, string.Join(Environment.NewLine, currentValue));
					}
					currentKey = key;
					currentValue = new List<string>();
					notNeed = false;
				}
				else if (line.StartsWith("[IDS_ITEM."))
				{
					notNeed = true;
				}
				else if (!notNeed && currentValue != null)
				{
					currentValue.Add(line);
				}
			}
			if (currentValue != null)
			{
				targetDic.Add(currentKey, string.Join(Environment.NewLine, currentValue));
			}
		}

		// Token: 0x06008B81 RID: 35713 RVA: 0x0011CB18 File Offset: 0x0011AD18
		private Dictionary<int, string> GetDic(int type)
		{
			Dictionary<int, string> dictionary;
			switch (type)
			{
			case 0:
				dictionary = this.names;
				break;
			case 1:
				dictionary = this.descriptions;
				break;
			case 2:
				dictionary = this.categories;
				break;
			default:
				throw new ArgumentOutOfRangeException("type", "Invalid type for dictionary retrieval.");
			}
			return dictionary;
		}

		// Token: 0x06008B82 RID: 35714 RVA: 0x0011CB68 File Offset: 0x0011AD68
		private bool TryParseKey(string line, out int key, int type)
		{
			Match match = this.GetMatch(line, type);
			if (match.Success)
			{
				key = int.Parse(match.Groups[1].Value);
				return true;
			}
			key = 0;
			return false;
		}

		// Token: 0x06008B83 RID: 35715 RVA: 0x0011CBA4 File Offset: 0x0011ADA4
		private Match GetMatch(string line, int type)
		{
			Match match;
			switch (type)
			{
			case 0:
				match = Regex.Match(line, "^\\[IDS_ITEM\\.ID(\\d+)\\]$");
				break;
			case 1:
				match = Regex.Match(line, "^\\[IDS_ITEMDESC\\.ID(\\d+)\\]$");
				break;
			case 2:
				match = Regex.Match(line, "^\\[IDS_CATEGORY.NAME_(\\d+)\\]$");
				break;
			default:
				throw new ArgumentOutOfRangeException("type", "Invalid type for match retrieval.");
			}
			return match;
		}

		// Token: 0x06008B84 RID: 35716 RVA: 0x0011CC00 File Offset: 0x0011AE00
		private string GetName(int code, string mName)
		{
			string returnValue;
			this.names.TryGetValue(code, out returnValue);
			if (string.IsNullOrEmpty(returnValue))
			{
				if (string.IsNullOrEmpty(mName))
				{
					returnValue = "coming soon";
				}
				else
				{
					returnValue = mName;
				}
			}
			return Cid2Ydk.ReplaceWithCardName(returnValue);
		}

		// Token: 0x06008B85 RID: 35717 RVA: 0x0011CC3C File Offset: 0x0011AE3C
		private string GetDescription(int code)
		{
			string returnValue;
			this.descriptions.TryGetValue(code, out returnValue);
			if (string.IsNullOrEmpty(returnValue))
			{
				return "coming soon";
			}
			returnValue = this.ReplaceWithCategory(returnValue);
			return Cid2Ydk.ReplaceWithCardName(returnValue);
		}

		// Token: 0x06008B86 RID: 35718 RVA: 0x0011CC74 File Offset: 0x0011AE74
		private string ReplaceWithCategory(string text)
		{
			return Regex.Replace(text, "<category id='(\\d+)'/>", new MatchEvaluator(this.EvaluatorReplaceCategory));
		}

		// Token: 0x06008B87 RID: 35719 RVA: 0x0011CC90 File Offset: 0x0011AE90
		private string EvaluatorReplaceCategory(Match match)
		{
			string key = match.Groups[1].Value;
			string value;
			if (this.categories.TryGetValue(int.Parse(key), out value))
			{
				return value;
			}
			return match.Value;
		}

		// Token: 0x06008B88 RID: 35720 RVA: 0x0011CCCC File Offset: 0x0011AECC
		public Items.Item GetRandomItem(Items.ItemType type)
		{
			Items.Item item;
			switch (type)
			{
			case Items.ItemType.Wallpaper:
				item = this.wallpapers[global::UnityEngine.Random.Range(0, this.wallpapers.Count)];
				break;
			case Items.ItemType.Face:
				item = this.faces[global::UnityEngine.Random.Range(0, this.faces.Count)];
				break;
			case Items.ItemType.Frame:
				item = this.frames[global::UnityEngine.Random.Range(0, this.frames.Count)];
				break;
			case Items.ItemType.Protector:
				item = this.protectors[global::UnityEngine.Random.Range(0, this.protectors.Count)];
				break;
			case Items.ItemType.Mat:
				item = this.mats[global::UnityEngine.Random.Range(0, this.mats.Count)];
				break;
			case Items.ItemType.Grave:
				item = this.graves[global::UnityEngine.Random.Range(0, this.graves.Count)];
				break;
			case Items.ItemType.Stand:
				item = this.stands[global::UnityEngine.Random.Range(0, this.stands.Count)];
				break;
			case Items.ItemType.Mate:
				item = this.mates[global::UnityEngine.Random.Range(0, this.mates.Count)];
				break;
			case Items.ItemType.Case:
				item = this.cases[global::UnityEngine.Random.Range(0, this.cases.Count)];
				break;
			default:
				item = this.mats[global::UnityEngine.Random.Range(0, this.mats.Count)];
				break;
			}
			Items.Item result = item;
			if (result.notReady)
			{
				return this.GetRandomItem(type);
			}
			return result;
		}

		// Token: 0x06008B89 RID: 35721 RVA: 0x0011CE60 File Offset: 0x0011B060
		public string GetWallpaperPath(string code)
		{
			if (code == 9999.ToString())
			{
				return this.GetRandomItem(Items.ItemType.Wallpaper).path;
			}
			foreach (Items.Item item in this.wallpapers)
			{
				int id = item.id;
				if (id.ToString() == code)
				{
					return item.path;
				}
			}
			return "Wallpaper/Front0001";
		}

		// Token: 0x06008B8A RID: 35722 RVA: 0x0011CEF4 File Offset: 0x0011B0F4
		public string GetSameCode(Items.ItemType type, string mapCode)
		{
			if (mapCode.Length != 7)
			{
				mapCode = "1090001";
			}
			if (mapCode == "1098001" && type != Items.ItemType.Mat)
			{
				mapCode = "1090009";
			}
			if (mapCode == "1098002" && type != Items.ItemType.Mat)
			{
				mapCode = "1090003";
			}
			if (type == Items.ItemType.Grave)
			{
				string text = "110";
				string text2 = mapCode;
				return text + text2.Substring(3, text2.Length - 3);
			}
			if (type == Items.ItemType.Stand)
			{
				string text3 = "111";
				string text2 = mapCode;
				return text3 + text2.Substring(3, text2.Length - 3);
			}
			if (type == Items.ItemType.Mat)
			{
				this.lastMat1 = this.lastMat0;
				return this.lastMat0;
			}
			return mapCode;
		}

		// Token: 0x06008B8B RID: 35723 RVA: 0x0011CF9C File Offset: 0x0011B19C
		public string GetAssetPath(string code, Items.ItemType type, int player = 0)
		{
			if (code == 9999.ToString())
			{
				Items.Item item = this.GetRandomItem(type);
				if (type == Items.ItemType.Mat)
				{
					if (player == 0)
					{
						this.lastMat0 = item.id.ToString();
					}
					else
					{
						this.lastMat1 = item.id.ToString();
					}
				}
				return item.path;
			}
			if (type == Items.ItemType.Mat)
			{
				if (player == 0)
				{
					this.lastMat0 = code;
				}
				else
				{
					this.lastMat1 = code;
				}
			}
			if (code == 8888.ToString())
			{
				code = this.GetSameCode(type, (player == 0) ? this.lastMat0 : this.lastMat1);
			}
			if (type == Items.ItemType.Unknown)
			{
				return Items.GetIconAddress(code, 0);
			}
			foreach (List<Items.Item> list in this.kinds)
			{
				foreach (Items.Item item2 in list)
				{
					int id = item2.id;
					if (id.ToString() == code)
					{
						return item2.path;
					}
				}
			}
			switch (type)
			{
			case Items.ItemType.Wallpaper:
				return this.wallpapers[0].path;
			case Items.ItemType.Face:
				return this.faces[0].path;
			case Items.ItemType.Frame:
				return this.frames[0].path;
			case Items.ItemType.Protector:
				return this.protectors[0].path;
			case Items.ItemType.Mat:
				return this.mats[0].path;
			case Items.ItemType.Grave:
				return this.graves[0].path;
			case Items.ItemType.Stand:
				return this.stands[0].path;
			case Items.ItemType.Mate:
				return this.mates[0].path;
			case Items.ItemType.Case:
				return this.cases[0].path;
			default:
				return this.mats[0].path;
			}
			string text;
			return text;
		}

		// Token: 0x06008B8C RID: 35724 RVA: 0x0011D1CC File Offset: 0x0011B3CC
		public static string GetIconAddress(string id, int size = 0)
		{
			if (id == 9999.ToString())
			{
				return "Menu-Random";
			}
			if (id == 8888.ToString())
			{
				return "Menu-Same";
			}
			if (id == 0.ToString())
			{
				return "Menu-NoImage";
			}
			if (id == 9998.ToString())
			{
				return "Menu-DIY";
			}
			string type = id.Substring(0, 3);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
			string pathPrefix;
			string pathSuffix;
			if (num <= 1781782869U)
			{
				if (num <= 1731450012U)
				{
					if (num != 1714672393U)
					{
						if (num == 1731450012U)
						{
							if (type == "100")
							{
								pathPrefix = string.Empty;
								pathSuffix = string.Empty;
								goto IL_02EB;
							}
						}
					}
					else if (type == "107")
					{
						pathPrefix = "ProtectorIcon";
						pathSuffix = string.Empty;
						goto IL_02EB;
					}
				}
				else if (num != 1748227631U)
				{
					if (num == 1781782869U)
					{
						if (type == "103")
						{
							pathPrefix = "ProfileFrame";
							string text;
							if (size != 1)
							{
								if (size != 2)
								{
									text = string.Empty;
								}
								else
								{
									text = "_L_HD";
								}
							}
							else
							{
								text = "_L_SD";
							}
							pathSuffix = text;
							goto IL_02EB;
						}
					}
				}
				else if (type == "101")
				{
					pathPrefix = "ProfileIcon";
					string text;
					if (size != 1)
					{
						if (size != 2)
						{
							text = string.Empty;
						}
						else
						{
							text = "_L_HD";
						}
					}
					else
					{
						text = "_L_SD";
					}
					pathSuffix = text;
					goto IL_02EB;
				}
			}
			else if (num <= 1815485202U)
			{
				if (num != 1781929964U)
				{
					if (num == 1815485202U)
					{
						if (type == "111")
						{
							pathPrefix = "FieldAvatarBaseIcon";
							string text;
							if (size == 2)
							{
								text = "_HD";
							}
							else
							{
								text = "_SD";
							}
							pathSuffix = text;
							goto IL_02EB;
						}
					}
				}
				else if (type == "113")
				{
					pathPrefix = "WallPaperIcon";
					pathSuffix = string.Empty;
					goto IL_02EB;
				}
			}
			else if (num != 1832262821U)
			{
				if (num != 1865670964U)
				{
					if (num == 1882448583U)
					{
						if (type == "109")
						{
							pathPrefix = "FieldIcon";
							string text;
							if (size == 2)
							{
								text = "_HD";
							}
							else
							{
								text = "_SD";
							}
							pathSuffix = text;
							goto IL_02EB;
						}
					}
				}
				else if (type == "108")
				{
					pathPrefix = "DeckCase";
					string text;
					if (size != 1)
					{
						if (size != 2)
						{
							text = string.Empty;
						}
						else
						{
							text = "_L_HD";
						}
					}
					else
					{
						text = "_L_SD";
					}
					pathSuffix = text;
					goto IL_02EB;
				}
			}
			else if (type == "110")
			{
				pathPrefix = "FieldObjIcon";
				string text;
				if (size == 2)
				{
					text = "_HD";
				}
				else
				{
					text = "_SD";
				}
				pathSuffix = text;
				goto IL_02EB;
			}
			pathPrefix = string.Empty;
			pathSuffix = string.Empty;
			IL_02EB:
			if (type == "108")
			{
				return pathPrefix + id.Substring(3, id.Length - 3) + pathSuffix;
			}
			return pathPrefix + id + pathSuffix;
		}

		// Token: 0x06008B8D RID: 35725 RVA: 0x0011D4F8 File Offset: 0x0011B6F8
		public UniTask<Sprite> LoadItemIconAsync(string id, Items.ItemType type)
		{
			Items.<LoadItemIconAsync>d__47 <LoadItemIconAsync>d__;
			<LoadItemIconAsync>d__.<>t__builder = AsyncUniTaskMethodBuilder<Sprite>.Create();
			<LoadItemIconAsync>d__.<>4__this = this;
			<LoadItemIconAsync>d__.id = id;
			<LoadItemIconAsync>d__.<>1__state = -1;
			<LoadItemIconAsync>d__.<>t__builder.Start<Items.<LoadItemIconAsync>d__47>(ref <LoadItemIconAsync>d__);
			return <LoadItemIconAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06008B8E RID: 35726 RVA: 0x0011D544 File Offset: 0x0011B744
		public async UniTask<Sprite> LoadConcreteItemIconAsync(string id, Items.ItemType type, int player = 0)
		{
			if (id == 9999.ToString())
			{
				Items.Item item = this.GetRandomItem(type);
				id = item.id.ToString();
				if (type == Items.ItemType.Frame)
				{
					Items.lastRandomFrameID = id;
				}
			}
			if (id == 9998.ToString())
			{
				string path = "Picture/DIY/";
				switch (player)
				{
				case 0:
					path += "Me";
					break;
				case 1:
					path += "Op";
					break;
				case 2:
					path += "MeTag";
					break;
				case 3:
					path += "OpTag";
					break;
				}
				if (File.Exists(path + ".png"))
				{
					return TextureManager.Texture2Sprite(await TextureManager.LoadPicFromFileAsync(path + ".png"));
				}
				if (File.Exists(path + ".jpg"))
				{
					return TextureManager.Texture2Sprite(await TextureManager.LoadPicFromFileAsync(path + ".jpg"));
				}
				Items.Item item = this.faces[0];
				id = item.id.ToString();
			}
			return await this.LoadItemIconAsync(id, type);
		}

		// Token: 0x06008B8F RID: 35727 RVA: 0x0011D5A0 File Offset: 0x0011B7A0
		public bool ListHaveRandom(List<Items.Item> target)
		{
			return target == this.wallpapers || target == this.faces || target == this.frames || target == this.protectors || target == this.mats || target == this.graves || target == this.stands || target == this.mates || target == this.cases;
		}

		// Token: 0x06008B90 RID: 35728 RVA: 0x0011D611 File Offset: 0x0011B811
		public bool ListHaveSame(List<Items.Item> target)
		{
			return target == this.graves || target == this.stands;
		}

		// Token: 0x06008B91 RID: 35729 RVA: 0x0011D62C File Offset: 0x0011B82C
		public bool ListHaveNone(List<Items.Item> target)
		{
			if (target == this.wallpapers)
			{
				return true;
			}
			if (target == this.faces)
			{
				return false;
			}
			if (target == this.frames)
			{
				return false;
			}
			if (target == this.protectors)
			{
				return false;
			}
			if (target == this.mats)
			{
				return false;
			}
			if (target == this.graves)
			{
				return false;
			}
			if (target == this.stands)
			{
				return true;
			}
			if (target == this.mates)
			{
				return true;
			}
			List<Items.Item> list = this.cases;
			return false;
		}

		// Token: 0x06008B92 RID: 35730 RVA: 0x0011D69B File Offset: 0x0011B89B
		public bool ListHaveDIY(List<Items.Item> target)
		{
			return target == this.faces;
		}

		// Token: 0x06008B93 RID: 35731 RVA: 0x0011D6AC File Offset: 0x0011B8AC
		public async UniTask<Sprite> LoadDeckCaseIconAsync(int code, string suffix)
		{
			int num = 0;
			try
			{
				string text = "DeckCase";
				string text2 = code.ToString();
				return await this.LoadAddressableSprite(text + text2.Substring(3, text2.Length - 3) + suffix);
			}
			catch
			{
				num = 1;
			}
			Sprite sprite;
			if (num == 1)
			{
				Debug.LogError("Addressables Not Found: " + string.Format("DeckCase {0}_{1}", code, suffix));
				sprite = await this.LoadAddressableSprite("DeckCase0001_L");
			}
			return sprite;
		}

		// Token: 0x06008B94 RID: 35732 RVA: 0x0011D700 File Offset: 0x0011B900
		private async UniTask<Sprite> LoadAddressableSprite(string address)
		{
			return await Addressables.LoadAssetAsync<Sprite>(address);
		}

		// Token: 0x0400C767 RID: 51047
		public List<Items.Item> mates;

		// Token: 0x0400C768 RID: 51048
		public List<Items.Item> faces;

		// Token: 0x0400C769 RID: 51049
		public List<Items.Item> frames;

		// Token: 0x0400C76A RID: 51050
		public List<Items.Item> protectors;

		// Token: 0x0400C76B RID: 51051
		public List<Items.Item> cases;

		// Token: 0x0400C76C RID: 51052
		public List<Items.Item> mats;

		// Token: 0x0400C76D RID: 51053
		public List<Items.Item> graves;

		// Token: 0x0400C76E RID: 51054
		public List<Items.Item> stands;

		// Token: 0x0400C76F RID: 51055
		public List<Items.Item> wallpapers;

		// Token: 0x0400C770 RID: 51056
		public List<List<Items.Item>> kinds;

		// Token: 0x0400C771 RID: 51057
		private const string ADDRESS_DEFAULT_DECK_CASE = "DeckCase0001_L";

		// Token: 0x0400C772 RID: 51058
		public const string STRING_NULL = "coming soon";

		// Token: 0x0400C773 RID: 51059
		public const int CODE_NONE = 0;

		// Token: 0x0400C774 RID: 51060
		public const int CODE_RANDOM = 9999;

		// Token: 0x0400C775 RID: 51061
		public const int CODE_SAME = 8888;

		// Token: 0x0400C776 RID: 51062
		public const int CODE_DIY = 9998;

		// Token: 0x0400C777 RID: 51063
		public const string PATH_ICON_NONE = "Menu-NoImage";

		// Token: 0x0400C778 RID: 51064
		public const string PATH_ICON_RANDOM = "Menu-Random";

		// Token: 0x0400C779 RID: 51065
		public const string PATH_ICON_SAME = "Menu-Same";

		// Token: 0x0400C77A RID: 51066
		public const string PATH_ICON_DIY = "Menu-DIY";

		// Token: 0x0400C77B RID: 51067
		public static bool initialized = false;

		// Token: 0x0400C77C RID: 51068
		private static string language = string.Empty;

		// Token: 0x0400C77D RID: 51069
		private static Items instance;

		// Token: 0x0400C77E RID: 51070
		private readonly Dictionary<int, string> names = new Dictionary<int, string>();

		// Token: 0x0400C77F RID: 51071
		private readonly Dictionary<int, string> descriptions = new Dictionary<int, string>();

		// Token: 0x0400C780 RID: 51072
		private readonly Dictionary<int, string> categories = new Dictionary<int, string>();

		// Token: 0x0400C781 RID: 51073
		private readonly Dictionary<string, Sprite> cachedIcons = new Dictionary<string, Sprite>();

		// Token: 0x0400C782 RID: 51074
		private string lastMat0;

		// Token: 0x0400C783 RID: 51075
		private string lastMat1;

		// Token: 0x0400C784 RID: 51076
		public static string lastRandomFrameID;

		// Token: 0x02001295 RID: 4757
		[Serializable]
		public struct Item
		{
			// Token: 0x1700116F RID: 4463
			// (get) Token: 0x06008B97 RID: 35735 RVA: 0x0011D78C File Offset: 0x0011B98C
			// (set) Token: 0x06008B98 RID: 35736 RVA: 0x0011D7E6 File Offset: 0x0011B9E6
			public string name
			{
				get
				{
					if (!this.nameLoaded)
					{
						string listName = Items.instance.GetName(this.id, this.m_name);
						if (listName != "coming soon" || string.IsNullOrEmpty(this.m_name))
						{
							this.m_name = listName;
						}
						this.nameLoaded = true;
					}
					return this.m_name;
				}
				set
				{
					this.m_name = value;
				}
			}

			// Token: 0x17001170 RID: 4464
			// (get) Token: 0x06008B99 RID: 35737 RVA: 0x0011D7F0 File Offset: 0x0011B9F0
			// (set) Token: 0x06008B9A RID: 35738 RVA: 0x0011D890 File Offset: 0x0011BA90
			public string description
			{
				get
				{
					if (!this.diy && !this.descriptionLoaded)
					{
						string listDescription = Items.instance.GetDescription(this.id);
						if (listDescription != "coming soon")
						{
							this.m_description = listDescription;
						}
						if (string.IsNullOrEmpty(this.m_description))
						{
							this.m_description = "coming soon";
						}
						this.descriptionLoaded = true;
					}
					if (this.diy && !this.descriptionLoaded && this.m_description.Contains("@"))
					{
						this.m_description = InterString.Get("由「[?]」投稿。", this.m_description, 0);
					}
					return this.m_description;
				}
				set
				{
					this.m_description = value;
				}
			}

			// Token: 0x0400C785 RID: 51077
			public int id;

			// Token: 0x0400C786 RID: 51078
			private bool nameLoaded;

			// Token: 0x0400C787 RID: 51079
			public string m_name;

			// Token: 0x0400C788 RID: 51080
			private bool descriptionLoaded;

			// Token: 0x0400C789 RID: 51081
			public string m_description;

			// Token: 0x0400C78A RID: 51082
			public string path;

			// Token: 0x0400C78B RID: 51083
			public bool secondFace;

			// Token: 0x0400C78C RID: 51084
			public bool diy;

			// Token: 0x0400C78D RID: 51085
			public bool notReady;
		}

		// Token: 0x02001296 RID: 4758
		public enum ItemType
		{
			// Token: 0x0400C78F RID: 51087
			Unknown,
			// Token: 0x0400C790 RID: 51088
			Wallpaper,
			// Token: 0x0400C791 RID: 51089
			Face,
			// Token: 0x0400C792 RID: 51090
			Frame,
			// Token: 0x0400C793 RID: 51091
			Protector,
			// Token: 0x0400C794 RID: 51092
			Mat,
			// Token: 0x0400C795 RID: 51093
			Grave,
			// Token: 0x0400C796 RID: 51094
			Stand,
			// Token: 0x0400C797 RID: 51095
			Mate,
			// Token: 0x0400C798 RID: 51096
			Case
		}
	}
}

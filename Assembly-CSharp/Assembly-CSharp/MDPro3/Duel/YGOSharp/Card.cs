using System;
using System.Data;
using MDPro3.Net;
using MDPro3.Utility;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200151E RID: 5406
	public class Card
	{
		// Token: 0x06009D38 RID: 40248 RVA: 0x00195CEC File Offset: 0x00193EEC
		public Card()
		{
			this.Id = 0;
			this.Str = new string[16];
			this.Name = CardsManager.nullName;
			this.Desc = CardsManager.nullString;
		}

		// Token: 0x06009D39 RID: 40249 RVA: 0x00195D58 File Offset: 0x00193F58
		public Card Clone()
		{
			Card r = new Card();
			r.Id = this.Id;
			r.Ot = this.Ot;
			r.Alias = this.Alias;
			r.Setcode = this.Setcode;
			r.Type = this.Type;
			r.Level = this.Level;
			r.LScale = this.LScale;
			r.RScale = this.RScale;
			r.LinkMarker = this.LinkMarker;
			r.Attribute = this.Attribute;
			r.Race = this.Race;
			r.Attack = this.Attack;
			r.Defense = this.Defense;
			r.rAttack = this.rAttack;
			r.rDefense = this.rDefense;
			r.Category = this.Category;
			r.Name = this.Name;
			r.Desc = this.Desc;
			r.Str = new string[this.Str.Length];
			r.isPre = this.isPre;
			for (int ii = 0; ii < this.Str.Length; ii++)
			{
				r.Str[ii] = this.Str[ii];
			}
			return r;
		}

		// Token: 0x06009D3A RID: 40250 RVA: 0x00195E88 File Offset: 0x00194088
		public void CloneTo(Card r)
		{
			r.Id = this.Id;
			r.Ot = this.Ot;
			r.Alias = this.Alias;
			r.Setcode = this.Setcode;
			r.Type = this.Type;
			r.Level = this.Level;
			r.LScale = this.LScale;
			r.RScale = this.RScale;
			r.Attribute = this.Attribute;
			r.Race = this.Race;
			r.Attack = this.Attack;
			r.Defense = this.Defense;
			r.rAttack = this.rAttack;
			r.rDefense = this.rDefense;
			r.Category = this.Category;
			r.Name = this.Name;
			r.Desc = this.Desc;
			r.Str = new string[this.Str.Length];
			r.isPre = this.isPre;
			for (int ii = 0; ii < this.Str.Length; ii++)
			{
				r.Str[ii] = this.Str[ii];
			}
		}

		// Token: 0x06009D3B RID: 40251 RVA: 0x00195FA3 File Offset: 0x001941A3
		public static Card Get(int id)
		{
			return CardsManager.GetCard(id);
		}

		// Token: 0x06009D3C RID: 40252 RVA: 0x00195FAC File Offset: 0x001941AC
		internal Card(IDataRecord reader)
		{
			this.Str = new string[16];
			this.Id = (int)reader.GetInt64(0);
			this.Ot = reader.GetInt32(1);
			this.Alias = (int)reader.GetInt64(2);
			this.Setcode = reader.GetInt64(3);
			this.Type = (int)reader.GetInt64(4);
			this.Attack = reader.GetInt32(5);
			this.Defense = reader.GetInt32(6);
			this.rAttack = this.Attack;
			this.rDefense = this.Defense;
			long Level_raw = reader.GetInt64(7);
			this.Level = (int)Level_raw & 255;
			this.LScale = (int)((Level_raw >> 24) & 255L);
			this.RScale = (int)((Level_raw >> 16) & 255L);
			this.LinkMarker = this.Defense;
			this.Race = reader.GetInt32(8);
			this.Attribute = reader.GetInt32(9);
			this.Category = reader.GetInt64(10);
			this.Name = reader.GetString(12);
			this.Desc = reader.GetString(13);
			for (int ii = 0; ii < 16; ii++)
			{
				this.Str[ii] = reader.GetString(14 + ii);
			}
		}

		// Token: 0x06009D3D RID: 40253 RVA: 0x00196118 File Offset: 0x00194318
		public bool HasType(CardType type)
		{
			return (this.Type & (int)type) != 0;
		}

		// Token: 0x06009D3E RID: 40254 RVA: 0x00196125 File Offset: 0x00194325
		public bool HasLinkMarker(CardLinkMarker dir)
		{
			return (this.LinkMarker & (int)dir) != 0;
		}

		// Token: 0x06009D3F RID: 40255 RVA: 0x00196132 File Offset: 0x00194332
		public bool IsExtraCard()
		{
			return this.HasType(CardType.Fusion) || this.HasType(CardType.Synchro) || this.HasType(CardType.Xyz) || this.HasType(CardType.Link);
		}

		// Token: 0x06009D40 RID: 40256 RVA: 0x00196165 File Offset: 0x00194365
		public bool IsSameCard(Card data)
		{
			return this.GetOriginalID() == data.GetOriginalID();
		}

		// Token: 0x06009D41 RID: 40257 RVA: 0x00196178 File Offset: 0x00194378
		public int GetLinkCount()
		{
			int returnValue = 0;
			for (int i = 0; i < 9; i++)
			{
				if (((long)(this.LinkMarker >> i) & 1L) > 0L && i != 4)
				{
					returnValue++;
				}
			}
			return returnValue;
		}

		// Token: 0x06009D42 RID: 40258 RVA: 0x001961B0 File Offset: 0x001943B0
		public int GetOriginalID()
		{
			if (this.Alias > 0)
			{
				return this.Alias;
			}
			return this.Id;
		}

		// Token: 0x06009D43 RID: 40259 RVA: 0x001961C8 File Offset: 0x001943C8
		public Card.LevelType GetLevelType()
		{
			if (this.HasType(CardType.Link))
			{
				return Card.LevelType.Link;
			}
			if (this.HasType(CardType.Xyz))
			{
				return Card.LevelType.Rank;
			}
			return Card.LevelType.Level;
		}

		// Token: 0x06009D44 RID: 40260 RVA: 0x001961E9 File Offset: 0x001943E9
		public bool IsHighLevel()
		{
			return (this.HasType(CardType.Link) && this.GetLinkCount() > 2) || (this.HasType(CardType.Xyz) && this.Level > 3) || this.Level > 6;
		}

		// Token: 0x06009D45 RID: 40261 RVA: 0x00196227 File Offset: 0x00194427
		public bool IsAttribute(CardAttribute attribute)
		{
			return ((long)this.Attribute & (long)((ulong)attribute)) > 0L;
		}

		// Token: 0x06009D46 RID: 40262 RVA: 0x00196237 File Offset: 0x00194437
		public int GetGenesysPoint()
		{
			return OnlineService.GetGenesysPoint(this.GetOriginalID());
		}

		// Token: 0x06009D47 RID: 40263 RVA: 0x00196244 File Offset: 0x00194444
		public string GetAttackString()
		{
			if (this.Attack != -2)
			{
				return this.Attack.ToString();
			}
			return "?";
		}

		// Token: 0x06009D48 RID: 40264 RVA: 0x00196261 File Offset: 0x00194461
		public string GetDefenseString()
		{
			if (this.Defense != -2)
			{
				return this.Defense.ToString();
			}
			return "?";
		}

		// Token: 0x06009D49 RID: 40265 RVA: 0x00196280 File Offset: 0x00194480
		public string GetDescription(bool withSetName = false)
		{
			if (!this.HasType(CardType.Pendulum))
			{
				return (withSetName ? this.GetSetNameWithColor() : string.Empty) + this.Desc;
			}
			string setName = (withSetName ? this.GetSetNameWithColor() : string.Empty);
			string[] texts = this.GetDescriptionSplit(false);
			string text = texts[0];
			string pendulumText = ((text != null) ? text.Trim(new char[] { '\r', '\n' }) : null);
			string text2 = texts[1];
			string monsterText = ((text2 != null) ? text2.Trim(new char[] { '\r', '\n' }) : null);
			if (Language.GetConfig() != "ko-KR")
			{
				string pendulumHeader = Card.NormalizeBracketLabel(InterString.Get("【灵摆效果】", 0));
				string result = setName + (monsterText ?? string.Empty);
				if (!string.IsNullOrEmpty(pendulumText))
				{
					if (!string.IsNullOrEmpty(monsterText))
					{
						result = result + "\n" + Card.PendulumSeparatorLine + "\n";
					}
					else if (!string.IsNullOrEmpty(result))
					{
						result += "\n";
					}
					result = result + pendulumHeader + "\n" + pendulumText;
				}
				return result;
			}
			string monsterHeader = InterString.Get("【怪兽效果】", 0);
			if (!this.HasType(CardType.Effect))
			{
				monsterHeader = InterString.Get("【怪兽描述】", 0);
			}
			string pendulumHeaderK = Card.NormalizeBracketLabel(InterString.Get("【灵摆效果】", 0));
			monsterHeader = Card.NormalizeBracketLabel(monsterHeader);
			return string.Concat(new string[]
			{
				setName,
				pendulumHeaderK,
				"\n",
				pendulumText ?? string.Empty,
				"\n",
				monsterHeader,
				"\n",
				monsterText ?? string.Empty
			});
		}

		// Token: 0x06009D4A RID: 40266 RVA: 0x00196424 File Offset: 0x00194624
		private static string NormalizeBracketLabel(string label)
		{
			if (string.IsNullOrEmpty(label))
			{
				return string.Empty;
			}
			label = label.Replace("[ ", "[").Replace(" ]", "]").Replace("【 ", "【")
				.Replace(" 】", "】");
			string left = Language.GetLeftBracket();
			string right = Language.GetRightBracket().TrimEnd();
			label = label.Replace("【", left).Replace("】", right).Replace("[", left)
				.Replace("]", right);
			return label.Trim();
		}

		// Token: 0x06009D4B RID: 40267 RVA: 0x001964C4 File Offset: 0x001946C4
		public string GetMonsterDescription(bool render = false)
		{
			if (this.HasType(CardType.Pendulum))
			{
				return this.GetDescriptionSplit(render)[1];
			}
			return this.Desc;
		}

		// Token: 0x06009D4C RID: 40268 RVA: 0x001964E3 File Offset: 0x001946E3
		public string GetPendulumDescription(bool render = false)
		{
			if (this.HasType(CardType.Pendulum))
			{
				return this.GetDescriptionSplit(render)[0];
			}
			return string.Empty;
		}

		// Token: 0x06009D4D RID: 40269 RVA: 0x00196504 File Offset: 0x00194704
		public string[] GetDescriptionSplit(bool render = false)
		{
			string[] returnValue = new string[]
			{
				string.Empty,
				string.Empty
			};
			string[] lines = this.Desc.Replace("\r", "").Split('\n', StringSplitOptions.None);
			string language = (render ? Language.GetCardConfig() : Language.GetConfig());
			int beforePendulum = 1;
			int splitLines = 1;
			string symbol = "【";
			int monsterStart = 0;
			if (language == "en-US" || language == "pt-PT" || language == "fr-FR" || language == "de-DE" || language == "it-IT")
			{
				beforePendulum = 2;
				splitLines = 2;
				symbol = "[";
			}
			else if (language == "es-ES")
			{
				beforePendulum = 2;
				splitLines = 2;
			}
			else if (language == "zh-TW")
			{
				beforePendulum = 0;
			}
			for (int i = beforePendulum; i < lines.Length; i++)
			{
				if (lines[i].StartsWith(symbol))
				{
					monsterStart = i;
					break;
				}
			}
			for (int j = beforePendulum; j < lines.Length; j++)
			{
				if (j <= monsterStart - splitLines)
				{
					if (monsterStart - j == splitLines)
					{
						string[] array = returnValue;
						int num = 0;
						array[num] += lines[j];
					}
					else
					{
						string[] array2 = returnValue;
						int num2 = 0;
						array2[num2] = array2[num2] + lines[j] + "\r\n";
					}
				}
				else if (j > monsterStart)
				{
					if (j == lines.Length - 1)
					{
						string[] array3 = returnValue;
						int num3 = 1;
						array3[num3] += lines[j];
					}
					else
					{
						string[] array4 = returnValue;
						int num4 = 1;
						array4[num4] = array4[num4] + lines[j] + "\r\n";
					}
				}
			}
			if (language == "es-ES")
			{
				returnValue[0] = returnValue[0].Replace("-n/a-", string.Empty);
			}
			return returnValue;
		}

		// Token: 0x06009D4E RID: 40270 RVA: 0x001966B4 File Offset: 0x001948B4
		public string GetSetName()
		{
			return StringHelper.GetSetName(this.Setcode);
		}

		// Token: 0x06009D4F RID: 40271 RVA: 0x001966C4 File Offset: 0x001948C4
		public string GetSetNameWithColor()
		{
			string returnValue = this.GetSetName();
			if (returnValue.Length > 0)
			{
				returnValue = "<color=#FFF000>" + StringHelper.GetUnsafe(1329, 0) + returnValue + "</color>\r\n";
			}
			return returnValue;
		}

		// Token: 0x06009D50 RID: 40272 RVA: 0x00196700 File Offset: 0x00194900
		public string GetSetNameWithBracket()
		{
			string returnValue = this.GetSetName();
			if (returnValue.Length > 0)
			{
				returnValue = Language.GetLeftBracket() + returnValue + Language.GetRightBracket();
			}
			return returnValue;
		}

		// Token: 0x06009D51 RID: 40273 RVA: 0x00196730 File Offset: 0x00194930
		public string GetIdWithBracket()
		{
			string re = string.Format("{0}{1}", Language.GetLeftBracket(), this.Id);
			if (this.Alias != 0 && this.Alias != this.Id)
			{
				re += string.Format("/{0}", this.Alias);
			}
			return re + Language.GetRightBracket();
		}

		// Token: 0x06009D52 RID: 40274 RVA: 0x00196798 File Offset: 0x00194998
		public string GetAttributeString(bool render = false)
		{
			int type = (render ? 1 : 0);
			if (render && this.isPre)
			{
				type = 2;
			}
			return StringHelper.Attribute((long)this.Attribute, type);
		}

		// Token: 0x06009D53 RID: 40275 RVA: 0x001967C8 File Offset: 0x001949C8
		public string GetRaceString(bool render = false)
		{
			int type = (render ? 1 : 0);
			if (render && this.isPre)
			{
				type = 2;
			}
			return StringHelper.Race((long)this.Race, type);
		}

		// Token: 0x06009D54 RID: 40276 RVA: 0x001967F8 File Offset: 0x001949F8
		public string GetMainTypeString(bool render = false)
		{
			int type = (render ? 1 : 0);
			if (render && this.isPre)
			{
				type = 2;
			}
			return StringHelper.MainType((long)this.Type, type);
		}

		// Token: 0x06009D55 RID: 40277 RVA: 0x00196828 File Offset: 0x00194A28
		public string GetSecondType(bool render = false)
		{
			int type = (render ? 1 : 0);
			if (render && this.isPre)
			{
				type = 2;
			}
			return StringHelper.SecondType((long)this.Type, type);
		}

		// Token: 0x06009D56 RID: 40278 RVA: 0x00196858 File Offset: 0x00194A58
		public string GetSpellTrapType(bool render = false)
		{
			int type = 0;
			if (render)
			{
				type = 1;
				if (this.isPre)
				{
					type = 2;
				}
			}
			return Card.GetSpellTrapType(this.Type, type);
		}

		// Token: 0x06009D57 RID: 40279 RVA: 0x00196884 File Offset: 0x00194A84
		public static string GetSpellTrapType(int cardType, int type = 0)
		{
			if ((cardType & 2) > 0)
			{
				if ((cardType & 524288) > 0)
				{
					return InterString.Get("场地魔法", type);
				}
				if ((cardType & 65536) > 0)
				{
					return InterString.Get("速攻魔法", type);
				}
				if ((cardType & 131072) > 0)
				{
					return InterString.Get("永续魔法", type);
				}
				if ((cardType & 262144) > 0)
				{
					return InterString.Get("装备魔法", type);
				}
				if ((cardType & 128) > 0)
				{
					return InterString.Get("仪式魔法", type);
				}
				return InterString.Get("通常魔法", type);
			}
			else
			{
				if ((cardType & 4) <= 0)
				{
					return string.Empty;
				}
				if ((cardType & 131072) > 0)
				{
					return InterString.Get("永续陷阱", type);
				}
				if ((cardType & 1048576) > 0)
				{
					return InterString.Get("反击陷阱", type);
				}
				return InterString.Get("通常陷阱", type);
			}
		}

		// Token: 0x06009D58 RID: 40280 RVA: 0x00196954 File Offset: 0x00194B54
		public string GetTypeForUI()
		{
			string re = string.Empty;
			if (this.Id == 0)
			{
				return re;
			}
			string bracketLeft = Language.GetLeftBracket();
			string bracketRight = Language.GetRightBracket();
			if (this.HasType(CardType.Monster))
			{
				re = string.Concat(new string[]
				{
					bracketLeft,
					InterString.Get("[?]族", this.GetRaceString(false), 0),
					"/",
					this.GetSecondType(false),
					bracketRight
				});
			}
			else
			{
				re = bracketLeft + StringHelper.MainType((long)this.Type, 0) + bracketRight;
			}
			return re;
		}

		// Token: 0x06009D59 RID: 40281 RVA: 0x001969DC File Offset: 0x00194BDC
		public string GetTypeForRushDuelRender()
		{
			string re = string.Empty;
			if (this.Id == 0)
			{
				return re;
			}
			string bracketLeft = "【";
			string bracketRight = "】";
			if (Language.NeedSmallBracket(this.isPre ? Language.GetPrereleaseConfig() : Language.GetCardConfig()))
			{
				bracketLeft = "[";
				bracketRight = "] ";
			}
			if (this.HasType(CardType.Monster))
			{
				re = string.Concat(new string[]
				{
					bracketLeft,
					InterString.Get("[?]族", this.GetRaceString(true), this.isPre ? 2 : 1),
					"/",
					this.GetSecondType(true),
					bracketRight
				});
			}
			else
			{
				int type = 1;
				if (this.isPre)
				{
					type = 2;
				}
				re = bracketLeft;
				if (this.HasType(CardType.Spell))
				{
					re += InterString.Get("魔法卡", type);
				}
				else
				{
					re += InterString.Get("陷阱卡", type);
				}
				string secondType = this.GetSecondType(true);
				if (secondType != StringHelper.GetUnsafe(1054, type))
				{
					re = re + "/" + secondType + this.GetSpellTrapTypeIconCode();
				}
				re += bracketRight;
			}
			return re.Replace("/", (this.isPre ? Language.UseLatin(Language.GetPrereleaseConfig()) : Language.CardUseLatin()) ? " / " : "／");
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x00196B28 File Offset: 0x00194D28
		public string GetSpellTypeForOCGRender()
		{
			string re = string.Empty;
			if (this.Id == 0 || this.HasType(CardType.Monster))
			{
				return re;
			}
			string bracketLeft = "【";
			string bracketRight = "】";
			if (Language.NeedSmallBracket(this.isPre ? Language.GetPrereleaseConfig() : Language.GetCardConfig()))
			{
				bracketLeft = "[";
				bracketRight = "]";
			}
			re = bracketLeft;
			if (this.HasType(CardType.Spell))
			{
				re += InterString.Get("魔法卡", this.isPre ? 2 : 1);
			}
			else
			{
				re += InterString.Get("陷阱卡", this.isPre ? 2 : 1);
			}
			return re + this.GetSpellTrapTypeIconCode() + bracketRight;
		}

		// Token: 0x06009D5B RID: 40283 RVA: 0x00196BD8 File Offset: 0x00194DD8
		private string GetSpellTrapTypeIconCode()
		{
			string re = string.Empty;
			if (this.HasType(CardType.Equip))
			{
				re += "<Sprite=0>";
			}
			if (this.HasType(CardType.QuickPlay))
			{
				re += "<Sprite=1>";
			}
			if (this.HasType(CardType.Field))
			{
				re += "<Sprite=2>";
			}
			if (this.HasType(CardType.Ritual))
			{
				re += "<Sprite=3>";
			}
			if (this.HasType(CardType.Continuous))
			{
				re += "<Sprite=4>";
			}
			if (this.HasType(CardType.Counter))
			{
				re += "<Sprite=5>";
			}
			return re;
		}

		// Token: 0x0400DB43 RID: 56131
		public int Id;

		// Token: 0x0400DB44 RID: 56132
		public int Ot;

		// Token: 0x0400DB45 RID: 56133
		public int Alias;

		// Token: 0x0400DB46 RID: 56134
		public long Setcode;

		// Token: 0x0400DB47 RID: 56135
		public int Type;

		// Token: 0x0400DB48 RID: 56136
		public int Level;

		// Token: 0x0400DB49 RID: 56137
		public int LScale;

		// Token: 0x0400DB4A RID: 56138
		public int RScale;

		// Token: 0x0400DB4B RID: 56139
		public int LinkMarker;

		// Token: 0x0400DB4C RID: 56140
		public int Attribute;

		// Token: 0x0400DB4D RID: 56141
		public int Race;

		// Token: 0x0400DB4E RID: 56142
		public int Attack;

		// Token: 0x0400DB4F RID: 56143
		public int Defense;

		// Token: 0x0400DB50 RID: 56144
		public int rAttack;

		// Token: 0x0400DB51 RID: 56145
		public int rDefense;

		// Token: 0x0400DB52 RID: 56146
		public int Reason;

		// Token: 0x0400DB53 RID: 56147
		public int ReasonCard;

		// Token: 0x0400DB54 RID: 56148
		public long Category;

		// Token: 0x0400DB55 RID: 56149
		public string Name;

		// Token: 0x0400DB56 RID: 56150
		public string Desc;

		// Token: 0x0400DB57 RID: 56151
		public string[] Str;

		// Token: 0x0400DB58 RID: 56152
		public string packShortName = "";

		// Token: 0x0400DB59 RID: 56153
		public string packFullName = "";

		// Token: 0x0400DB5A RID: 56154
		public string reality = "";

		// Token: 0x0400DB5B RID: 56155
		public string strSetName = "";

		// Token: 0x0400DB5C RID: 56156
		public int year;

		// Token: 0x0400DB5D RID: 56157
		public int month;

		// Token: 0x0400DB5E RID: 56158
		public int day;

		// Token: 0x0400DB5F RID: 56159
		public bool isPre;

		// Token: 0x0400DB60 RID: 56160
		private static readonly string PendulumSeparatorLine = new string('─', 14);

		// Token: 0x0200151F RID: 5407
		public enum LevelType
		{
			// Token: 0x0400DB62 RID: 56162
			Level,
			// Token: 0x0400DB63 RID: 56163
			Rank,
			// Token: 0x0400DB64 RID: 56164
			Link
		}
	}
}

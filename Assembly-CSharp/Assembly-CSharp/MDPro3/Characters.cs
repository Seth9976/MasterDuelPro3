using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x0200128F RID: 4751
	[CreateAssetMenu(fileName = "Characters", menuName = "Scriptable Objects/Characters")]
	public class Characters : ScriptableObject
	{
		// Token: 0x06008B71 RID: 35697 RVA: 0x0011BB30 File Offset: 0x00119D30
		public string GetCharacterSeries(string charaID)
		{
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.dm.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "00";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.gx.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "01";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this._5ds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "02";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.dsod.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "03";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.zexal.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "04";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.arcv.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "05";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.vrains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "06";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.sevens.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "07";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.npc.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "08";
					}
				}
			}
			using (List<Characters.SeriesCharacter>.Enumerator enumerator = this.gorush.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == charaID)
					{
						return "09";
					}
				}
			}
			return "00";
		}

		// Token: 0x06008B72 RID: 35698 RVA: 0x0011BE78 File Offset: 0x0011A078
		public List<Characters.SeriesCharacter> GetSeriesCharacters(string serial)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(serial);
			if (num <= 485321326U)
			{
				if (num <= 434988469U)
				{
					if (num != 418210850U)
					{
						if (num == 434988469U)
						{
							if (serial == "08")
							{
								return this.npc;
							}
						}
					}
					else if (serial == "09")
					{
						return this.gorush;
					}
				}
				else if (num != 451766088U)
				{
					if (num != 468543707U)
					{
						if (num == 485321326U)
						{
							if (serial == "05")
							{
								return this.arcv;
							}
						}
					}
					else if (serial == "06")
					{
						return this.vrains;
					}
				}
				else if (serial == "07")
				{
					return this.sevens;
				}
			}
			else if (num <= 518876564U)
			{
				if (num != 502098945U)
				{
					if (num == 518876564U)
					{
						if (serial == "03")
						{
							return this.dsod;
						}
					}
				}
				else if (serial == "04")
				{
					return this.zexal;
				}
			}
			else if (num != 535654183U)
			{
				if (num != 552431802U)
				{
					if (num == 569209421U)
					{
						if (serial == "00")
						{
							return this.dm;
						}
					}
				}
				else if (serial == "01")
				{
					return this.gx;
				}
			}
			else if (serial == "02")
			{
				return this._5ds;
			}
			return this.dm;
		}

		// Token: 0x06008B73 RID: 35699 RVA: 0x0011C030 File Offset: 0x0011A230
		public string GetCharacterOriginalId(string charaID)
		{
			foreach (List<Characters.SeriesCharacter> list in this.characters)
			{
				foreach (Characters.SeriesCharacter ch in list)
				{
					if (ch.id == charaID)
					{
						return ch.GetOriginalId();
					}
				}
			}
			return charaID;
		}

		// Token: 0x06008B74 RID: 35700 RVA: 0x0011C0CC File Offset: 0x0011A2CC
		public string GetCharacterDescriptionId(string charaID)
		{
			foreach (List<Characters.SeriesCharacter> list in this.characters)
			{
				foreach (Characters.SeriesCharacter ch in list)
				{
					if (ch.id == charaID)
					{
						return ch.descriptionId;
					}
				}
			}
			return charaID;
		}

		// Token: 0x06008B75 RID: 35701 RVA: 0x0011C168 File Offset: 0x0011A368
		public void Initialize()
		{
			if (Characters.initialized)
			{
				return;
			}
			string path = "Data/DuelLinks_NPC_NAME.json";
			this.names = JsonConvert.DeserializeObject<NPC_Names>(File.ReadAllText(path));
			path = "Data/DuelLinks_Profile.json";
			this.profiles = JsonConvert.DeserializeObject<NPC_Profiles>(File.ReadAllText(path));
			this.characters = new List<List<Characters.SeriesCharacter>> { this.dm, this.gx, this._5ds, this.dsod, this.zexal, this.arcv, this.vrains, this.sevens, this.npc, this.gorush };
			Characters.initialized = true;
			Characters.instance = this;
		}

		// Token: 0x06008B76 RID: 35702 RVA: 0x0011C23A File Offset: 0x0011A43A
		public void ChangeLanguage(string language)
		{
			this.language = language;
		}

		// Token: 0x06008B77 RID: 35703 RVA: 0x0011C244 File Offset: 0x0011A444
		public string GetName(string id)
		{
			if (!Characters.initialized)
			{
				this.Initialize();
			}
			NPC_Data data;
			if (this.names.NPC_NAME.TryGetValue("NAME_ID" + id, out data))
			{
				string text = this.language;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1664981344U)
				{
					if (num <= 376747596U)
					{
						if (num != 83303646U)
						{
							if (num == 376747596U)
							{
								if (text == "fr-FR")
								{
									return data.french;
								}
							}
						}
						else if (text == "ru-RU")
						{
							return data.russian;
						}
					}
					else if (num != 637978675U)
					{
						if (num != 1434653370U)
						{
							if (num == 1664981344U)
							{
								if (text == "pt-BR")
								{
									return data.portuguese;
								}
							}
						}
						else if (text == "it-IT")
						{
							return data.italian;
						}
					}
					else if (text == "zh-CN")
					{
						return data.sChinese;
					}
				}
				else if (num <= 2328506441U)
				{
					if (num != 2194893224U)
					{
						if (num != 2196609786U)
						{
							if (num == 2328506441U)
							{
								if (text == "ja-JP")
								{
									return data.japanese;
								}
							}
						}
						else if (text == "de-DE")
						{
							return data.german;
						}
					}
					else if (text == "es-ES")
					{
						return data.spanish;
					}
				}
				else if (num != 2586248143U)
				{
					if (num != 2723579257U)
					{
						if (num == 3973517379U)
						{
							if (text == "zh-TW")
							{
								return data.tChinese;
							}
						}
					}
					else if (text == "en-US")
					{
						return data.english;
					}
				}
				else if (text == "ko-KR")
				{
					return data.korean;
				}
				return data.english;
			}
			return string.Empty;
		}

		// Token: 0x06008B78 RID: 35704 RVA: 0x0011C470 File Offset: 0x0011A670
		public string GetProfile(string id)
		{
			if (!Characters.initialized)
			{
				this.Initialize();
			}
			string value = string.Empty;
			NPC_Data data;
			if (this.profiles.PROFILE.TryGetValue("ID" + id, out data))
			{
				string text = this.language;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				string text2;
				if (num <= 1664981344U)
				{
					if (num <= 376747596U)
					{
						if (num != 83303646U)
						{
							if (num == 376747596U)
							{
								if (text == "fr-FR")
								{
									text2 = data.french;
									goto IL_022D;
								}
							}
						}
						else if (text == "ru-RU")
						{
							text2 = data.russian;
							goto IL_022D;
						}
					}
					else if (num != 637978675U)
					{
						if (num != 1434653370U)
						{
							if (num == 1664981344U)
							{
								if (text == "pt-BR")
								{
									text2 = data.portuguese;
									goto IL_022D;
								}
							}
						}
						else if (text == "it-IT")
						{
							text2 = data.italian;
							goto IL_022D;
						}
					}
					else if (text == "zh-CN")
					{
						text2 = data.sChinese;
						goto IL_022D;
					}
				}
				else if (num <= 2328506441U)
				{
					if (num != 2194893224U)
					{
						if (num != 2196609786U)
						{
							if (num == 2328506441U)
							{
								if (text == "ja-JP")
								{
									text2 = data.japanese;
									goto IL_022D;
								}
							}
						}
						else if (text == "de-DE")
						{
							text2 = data.german;
							goto IL_022D;
						}
					}
					else if (text == "es-ES")
					{
						text2 = data.spanish;
						goto IL_022D;
					}
				}
				else if (num != 2586248143U)
				{
					if (num != 2723579257U)
					{
						if (num == 3973517379U)
						{
							if (text == "zh-TW")
							{
								text2 = data.tChinese;
								goto IL_022D;
							}
						}
					}
					else if (text == "en-US")
					{
						text2 = data.english;
						goto IL_022D;
					}
				}
				else if (text == "ko-KR")
				{
					text2 = data.korean;
					goto IL_022D;
				}
				text2 = data.english;
				IL_022D:
				value = text2;
			}
			if (string.IsNullOrEmpty(value))
			{
				string dID = this.GetCharacterDescriptionId(id);
				NPC_Data data2;
				if (this.profiles.PROFILE.TryGetValue("ID" + dID, out data2))
				{
					string text2 = this.language;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
					string text;
					if (num <= 1664981344U)
					{
						if (num <= 376747596U)
						{
							if (num != 83303646U)
							{
								if (num == 376747596U)
								{
									if (text2 == "fr-FR")
									{
										text = data2.french;
										goto IL_046D;
									}
								}
							}
							else if (text2 == "ru-RU")
							{
								text = data2.russian;
								goto IL_046D;
							}
						}
						else if (num != 637978675U)
						{
							if (num != 1434653370U)
							{
								if (num == 1664981344U)
								{
									if (text2 == "pt-BR")
									{
										text = data2.portuguese;
										goto IL_046D;
									}
								}
							}
							else if (text2 == "it-IT")
							{
								text = data2.italian;
								goto IL_046D;
							}
						}
						else if (text2 == "zh-CN")
						{
							text = data2.sChinese;
							goto IL_046D;
						}
					}
					else if (num <= 2328506441U)
					{
						if (num != 2194893224U)
						{
							if (num != 2196609786U)
							{
								if (num == 2328506441U)
								{
									if (text2 == "ja-JP")
									{
										text = data2.japanese;
										goto IL_046D;
									}
								}
							}
							else if (text2 == "de-DE")
							{
								text = data2.german;
								goto IL_046D;
							}
						}
						else if (text2 == "es-ES")
						{
							text = data2.spanish;
							goto IL_046D;
						}
					}
					else if (num != 2586248143U)
					{
						if (num != 2723579257U)
						{
							if (num == 3973517379U)
							{
								if (text2 == "zh-TW")
								{
									text = data2.tChinese;
									goto IL_046D;
								}
							}
						}
						else if (text2 == "en-US")
						{
							text = data2.english;
							goto IL_046D;
						}
					}
					else if (text2 == "ko-KR")
					{
						text = data2.korean;
						goto IL_046D;
					}
					text = data2.english;
					IL_046D:
					value = text;
				}
			}
			return value;
		}

		// Token: 0x0400C743 RID: 51011
		public List<Characters.SeriesCharacter> dm;

		// Token: 0x0400C744 RID: 51012
		public List<Characters.SeriesCharacter> gx;

		// Token: 0x0400C745 RID: 51013
		public List<Characters.SeriesCharacter> _5ds;

		// Token: 0x0400C746 RID: 51014
		public List<Characters.SeriesCharacter> dsod;

		// Token: 0x0400C747 RID: 51015
		public List<Characters.SeriesCharacter> zexal;

		// Token: 0x0400C748 RID: 51016
		public List<Characters.SeriesCharacter> arcv;

		// Token: 0x0400C749 RID: 51017
		public List<Characters.SeriesCharacter> vrains;

		// Token: 0x0400C74A RID: 51018
		public List<Characters.SeriesCharacter> sevens;

		// Token: 0x0400C74B RID: 51019
		public List<Characters.SeriesCharacter> npc;

		// Token: 0x0400C74C RID: 51020
		public List<Characters.SeriesCharacter> gorush;

		// Token: 0x0400C74D RID: 51021
		private List<List<Characters.SeriesCharacter>> characters;

		// Token: 0x0400C74E RID: 51022
		private NPC_Names names;

		// Token: 0x0400C74F RID: 51023
		private NPC_Profiles profiles;

		// Token: 0x0400C750 RID: 51024
		public string language = "zh-CN";

		// Token: 0x0400C751 RID: 51025
		private static Characters instance;

		// Token: 0x0400C752 RID: 51026
		private static bool initialized;

		// Token: 0x02001290 RID: 4752
		[Serializable]
		public struct SeriesCharacter
		{
			// Token: 0x06008B7A RID: 35706 RVA: 0x0011C900 File Offset: 0x0011AB00
			public readonly string GetOriginalId()
			{
				if (!string.IsNullOrEmpty(this.originalId))
				{
					return this.originalId;
				}
				return this.id;
			}

			// Token: 0x0400C753 RID: 51027
			public string id;

			// Token: 0x0400C754 RID: 51028
			public string originalId;

			// Token: 0x0400C755 RID: 51029
			public string descriptionId;

			// Token: 0x0400C756 RID: 51030
			public bool notReady;
		}
	}
}

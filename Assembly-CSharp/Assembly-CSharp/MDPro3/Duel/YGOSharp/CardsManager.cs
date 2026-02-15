using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;
using MDPro3.Servant;
using MDPro3.Utility;
using Mono.Data.Sqlite;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001520 RID: 5408
	internal static class CardsManager
	{
		// Token: 0x06009D5D RID: 40285 RVA: 0x00196C98 File Offset: 0x00194E98
		internal static void Initialize()
		{
			CardsManager.nullName = InterString.Get("未知卡片", 0);
			CardsManager.nullString = string.Empty;
			string language = Language.GetConfig();
			string databaseFullPath = "Data/locales/" + language + "/cards.cdb";
			if (!File.Exists(databaseFullPath))
			{
				databaseFullPath = "Data/locales/zh-CN/cards.cdb";
			}
			CardsManager._cards.Clear();
			CardsManager.LoadCDB(databaseFullPath, false, false);
			if (Config.Get("Expansions", "1") == "1")
			{
				string[] array = Directory.GetFiles("Expansions", "*.cdb");
				for (int i = 0; i < array.Length; i++)
				{
					CardsManager.LoadCDB(array[i], false, false);
				}
				foreach (ZipFile zip in ZipHelper.zips)
				{
					if (!zip.Name.ToLower().EndsWith("script.zip"))
					{
						foreach (string file in zip.EntryFileNames)
						{
							if (file.ToLower().EndsWith(".cdb"))
							{
								ZipEntry e = zip[file];
								if (!Directory.Exists("TempFolder/"))
								{
									Directory.CreateDirectory("TempFolder/");
								}
								string text = Path.Combine(Path.GetFullPath("TempFolder/"), file);
								e.Extract(Path.GetFullPath("TempFolder/"), ExtractExistingFileAction.OverwriteSilently);
								CardsManager.LoadCDB(text, false, Path.GetFileName(zip.Name) == "ygopro-super-pre.ypk" && file.ToLower().StartsWith("test-release"));
								File.Delete(text);
							}
						}
					}
				}
			}
			CardsManager.UpdateSetNames();
			PacksManager.Initialize();
			CardsManager._cardsForRender.Clear();
			string cardLanguage = Language.GetCardConfig();
			databaseFullPath = "Data/locales/" + cardLanguage + "/cards.cdb";
			if (!File.Exists(databaseFullPath))
			{
				databaseFullPath = "Data/locales/zh-CN/cards.cdb";
			}
			CardsManager.LoadCDB(databaseFullPath, true, false);
			if (Config.Get("Expansions", "1") == "1")
			{
				string[] array = Directory.GetFiles("Expansions", "*.cdb");
				for (int i = 0; i < array.Length; i++)
				{
					CardsManager.LoadCDB(array[i], true, false);
				}
				foreach (ZipFile zip2 in ZipHelper.zips)
				{
					if (!zip2.Name.ToLower().EndsWith("script.zip"))
					{
						foreach (string file2 in zip2.EntryFileNames)
						{
							if (file2.ToLower().EndsWith(".cdb"))
							{
								ZipEntry e2 = zip2[file2];
								if (!Directory.Exists("TempFolder/"))
								{
									Directory.CreateDirectory("TempFolder/");
								}
								string text2 = Path.Combine(Path.GetFullPath("TempFolder/"), file2);
								e2.Extract(Path.GetFullPath("TempFolder/"), ExtractExistingFileAction.OverwriteSilently);
								CardsManager.LoadCDB(text2, true, Path.GetFileName(zip2.Name) == "ygopro-super-pre.ypk");
								File.Delete(text2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x0019701C File Offset: 0x0019521C
		internal static void LoadCDB(string databaseFullPath, bool render = false, bool isPreCards = false)
		{
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + databaseFullPath))
			{
				connection.Open();
				using (IDbCommand command = new SqliteCommand("SELECT datas.*, texts.* FROM datas,texts WHERE datas.id=texts.id;", connection))
				{
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							CardsManager.LoadCard(reader, render, isPreCards);
						}
					}
				}
			}
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x001970B0 File Offset: 0x001952B0
		internal static void UpdateSetNames()
		{
			foreach (KeyValuePair<int, Card> item in CardsManager._cards)
			{
				Card value = item.Value;
				value.strSetName = StringHelper.GetSetName(value.Setcode);
			}
		}

		// Token: 0x06009D60 RID: 40288 RVA: 0x0019710C File Offset: 0x0019530C
		internal static Card GetCard(int id)
		{
			if (CardsManager._cards.ContainsKey(id))
			{
				return CardsManager._cards[id].Clone();
			}
			return null;
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x0019712D File Offset: 0x0019532D
		internal static Card GetRenderCard(int id)
		{
			if (CardsManager._cardsForRender.ContainsKey(id))
			{
				return CardsManager._cardsForRender[id].Clone();
			}
			return null;
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x0019714E File Offset: 0x0019534E
		internal static Card GetCardRaw(int id)
		{
			if (CardsManager._cards.ContainsKey(id))
			{
				return CardsManager._cards[id];
			}
			return null;
		}

		// Token: 0x06009D63 RID: 40291 RVA: 0x0019716C File Offset: 0x0019536C
		internal static Card Get(int id, bool noneIsZero = false)
		{
			Card returnValue = new Card();
			if (id > 0)
			{
				for (int i = 0; i < 10; i++)
				{
					returnValue = CardsManager.GetCard(id - i);
					if (returnValue != null)
					{
						break;
					}
				}
				if (returnValue == null)
				{
					returnValue = new Card();
					if (!noneIsZero)
					{
						returnValue.Id = id;
						returnValue.Desc = id.ToString();
					}
				}
			}
			return returnValue;
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x001971C0 File Offset: 0x001953C0
		internal static List<Card> GetAllCards()
		{
			List<Card> returnValue = new List<Card>();
			foreach (KeyValuePair<int, Card> card in CardsManager._cards)
			{
				returnValue.Add(card.Value);
			}
			return returnValue;
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x0019721C File Offset: 0x0019541C
		internal static List<int> GetAllCardCodes()
		{
			List<int> returnValue = new List<int>();
			foreach (KeyValuePair<int, Card> card in CardsManager._cards)
			{
				returnValue.Add(card.Key);
			}
			return returnValue;
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x00197278 File Offset: 0x00195478
		private static void LoadCard(IDataRecord reader, bool render = false, bool isPreCards = false)
		{
			Card card = new Card(reader)
			{
				isPre = isPreCards
			};
			if (!render)
			{
				CardsManager._cards[card.Id] = card;
				return;
			}
			CardsManager._cardsForRender[card.Id] = card;
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x001972BC File Offset: 0x001954BC
		public static List<string> GetMiddleStrings(string str, string start, string end)
		{
			List<string> returnValue = new List<string>();
			Regex reg = new Regex(string.Concat(new string[] { "(?<=(", start, "))[.\\s\\S]*?(?=(", end, "))" }), RegexOptions.RightToLeft);
			while (reg.Match(str).Value != "")
			{
				string s = reg.Match(str).Value;
				returnValue.Add(s);
				str = str.Replace(start + s + end, "");
			}
			return returnValue;
		}

		// Token: 0x06009D68 RID: 40296 RVA: 0x00197348 File Offset: 0x00195548
		private static List<string> GetSetNamesInDescription(string input)
		{
			List<string> returnValue = new List<string>();
			foreach (string s in CardsManager.setNameHead)
			{
				List<string> setNames = CardsManager.GetMiddleStrings(input, s + "「", "」");
				for (int i = 0; i < setNames.Count; i++)
				{
					if (!returnValue.Contains(setNames[i]))
					{
						returnValue.Add(setNames[i]);
					}
				}
			}
			foreach (string s2 in CardsManager.setNameTail)
			{
				List<string> setNames2 = CardsManager.GetMiddleStrings(input, "「", "」" + s2);
				for (int j = 0; j < setNames2.Count; j++)
				{
					if (!returnValue.Contains(setNames2[j]))
					{
						returnValue.Add(setNames2[j]);
					}
				}
			}
			return returnValue;
		}

		// Token: 0x06009D69 RID: 40297 RVA: 0x00197470 File Offset: 0x00195670
		internal static List<Card> Search(string getName, List<long> filters, Banlist banlist, string pack)
		{
			List<Card> returnValue = new List<Card>();
			string[] strings = getName.Split(' ', StringSplitOptions.None);
			CardsManager.nameInSearch = getName;
			foreach (KeyValuePair<int, Card> item in CardsManager._cards)
			{
				Card card = item.Value;
				if (!card.HasType(CardType.Token))
				{
					bool pass = true;
					foreach (string s in strings)
					{
						if (s.StartsWith("@"))
						{
							if (Regex.Replace(card.strSetName, s.Substring(1, s.Length - 1), "miaowu", RegexOptions.IgnoreCase) == card.strSetName)
							{
								pass = false;
								break;
							}
						}
						else if (s != "" && Regex.Replace(card.Name, s, "miaowu", RegexOptions.IgnoreCase) == card.Name && Regex.Replace(card.Desc, s, "miaowu", RegexOptions.IgnoreCase) == card.Desc && Regex.Replace(card.strSetName, s, "miaowu", RegexOptions.IgnoreCase) == card.strSetName && card.Id.ToString() != s)
						{
							pass = false;
							break;
						}
					}
					if (pass)
					{
						if (filters.Count == 0)
						{
							returnValue.Add(card);
						}
						else
						{
							pass = false;
							if (filters[0] == 0L)
							{
								pass = true;
							}
							if (!pass && ((long)card.Type & (long)((ulong)((uint)filters[0]))) > 0L)
							{
								if ((filters[0] & 128L) > 0L)
								{
									if ((filters[0] & 2L) > 0L && card.HasType(CardType.Spell))
									{
										pass = true;
									}
									if ((filters[0] & 4L) > 0L && card.HasType(CardType.Trap))
									{
										pass = true;
									}
									if (card.HasType(CardType.Monster))
									{
										pass = true;
									}
								}
								else
								{
									pass = true;
								}
							}
							if (pass)
							{
								pass = false;
								if (filters[1] == 0L)
								{
									pass = true;
								}
								if (!pass && card.HasType(CardType.Monster) && ((long)card.Attribute & (long)((ulong)((uint)filters[1]))) > 0L)
								{
									pass = true;
								}
								if (pass)
								{
									pass = false;
									if (filters[2] == 0L)
									{
										pass = true;
									}
									if (!pass)
									{
										if ((filters[2] & 2L) > 0L && card.Type == 2)
										{
											pass = true;
										}
										if ((filters[2] & 524288L) > 0L && card.HasType(CardType.Field))
										{
											pass = true;
										}
										if ((filters[2] & 262144L) > 0L && card.HasType(CardType.Equip))
										{
											pass = true;
										}
										if ((filters[2] & 134217728L) > 0L && card.HasType(CardType.Spell) && card.HasType(CardType.Continuous))
										{
											pass = true;
										}
										if ((filters[2] & 65536L) > 0L && card.HasType(CardType.QuickPlay))
										{
											pass = true;
										}
										if ((filters[2] & 128L) > 0L && card.HasType(CardType.Spell) && card.HasType(CardType.Ritual))
										{
											pass = true;
										}
										if ((filters[2] & 4L) > 0L && card.HasType(CardType.Trap) && !card.HasType(CardType.Continuous) && !card.HasType(CardType.Counter))
										{
											pass = true;
										}
										if ((filters[2] & 268435456L) > 0L && card.HasType(CardType.Trap) && card.HasType(CardType.Continuous))
										{
											pass = true;
										}
										if ((filters[2] & 1048576L) > 0L && card.HasType(CardType.Counter))
										{
											pass = true;
										}
									}
									if (pass)
									{
										pass = false;
										if (filters[3] == 0L)
										{
											pass = true;
										}
										if (!pass && card.HasType(CardType.Monster) && ((long)card.Race & filters[3]) > 0L)
										{
											pass = true;
										}
										if (pass)
										{
											pass = false;
											if (filters[4] == 0L)
											{
												pass = true;
											}
											if (!pass && card.HasType(CardType.Monster) && ((long)card.Type & filters[4]) > 0L)
											{
												pass = true;
											}
											if (!pass && (filters[4] & 134217728L) > 0L && card.HasType(CardType.Monster) && !card.HasType(CardType.Effect))
											{
												pass = true;
											}
											if (pass)
											{
												pass = false;
												if (filters[5] == 0L)
												{
													pass = true;
												}
												if (!pass)
												{
													int permit = banlist.GetQuantity(card.Id);
													if ((filters[5] & 1L) > 0L && permit == 0)
													{
														pass = true;
													}
													else if ((filters[5] & 2L) > 0L && permit == 1)
													{
														pass = true;
													}
													else if ((filters[5] & 4L) > 0L && permit == 2)
													{
														pass = true;
													}
													else if ((filters[5] & 8L) > 0L && permit == 3)
													{
														pass = true;
													}
												}
												if (pass)
												{
													pass = false;
													if (filters[6] == 0L)
													{
														pass = true;
													}
													if (!pass)
													{
														if ((filters[6] & (long)card.Ot) > 0L)
														{
															pass = true;
														}
														if ((filters[6] & 16L) > 0L && (card.Ot & 1) == 1 && (card.Ot & 2) == 0)
														{
															pass = true;
														}
														if ((filters[6] & 32L) > 0L && (card.Ot & 1) == 0 && (card.Ot & 2) == 2)
														{
															pass = true;
														}
														if ((filters[6] & 64L) > 0L && (card.Ot & 3) == 3)
														{
															pass = true;
														}
														if ((filters[6] & 128L) > 0L && card.isPre)
														{
															pass = true;
														}
													}
													if (pass)
													{
														pass = false;
														if (filters[7] == 0L)
														{
															pass = true;
														}
														if (!pass && (card.Category & (long)((ulong)((uint)filters[7]))) > 0L)
														{
															pass = true;
														}
														if (pass)
														{
															pass = false;
															if (filters[8] == 0L || filters[8] == 7L)
															{
																pass = true;
															}
															if (!pass && (filters[8] & (long)CardRarity.GetRarity(card.Id)) > 0L)
															{
																pass = true;
															}
															if (pass)
															{
																pass = false;
																if (filters[9] == 0L || filters[9] == 3L)
																{
																	pass = true;
																}
																if (!pass)
																{
																	bool found = false;
																	using (List<Card>.Enumerator enumerator2 = CutinViewer.cards.GetEnumerator())
																	{
																		while (enumerator2.MoveNext())
																		{
																			if (enumerator2.Current.Id == card.Id)
																			{
																				found = true;
																				break;
																			}
																		}
																	}
																	if ((filters[9] & 1L) > 0L && found)
																	{
																		pass = true;
																	}
																	if ((filters[9] & 2L) > 0L && !found)
																	{
																		pass = true;
																	}
																}
																if (pass)
																{
																	pass = false;
																	if (filters[10] == 0L || filters[10] == 3L)
																	{
																		pass = true;
																	}
																	if (!pass)
																	{
																		if (filters[10] == 1L && CardImageLoader.CardHasVideoArt(card.Id))
																		{
																			pass = true;
																		}
																		if (filters[10] == 2L && !CardImageLoader.CardHasVideoArt(card.Id))
																		{
																			pass = true;
																		}
																	}
																	if (pass)
																	{
																		pass = false;
																		if (filters[11] == 0L)
																		{
																			pass = true;
																		}
																		if (!pass && card.HasType(CardType.Link))
																		{
																			pass = true;
																			for (int i = 0; i < 9; i++)
																			{
																				if (((filters[11] >> i) & 1L) > 0L && ((card.LinkMarker >> i) & 1) == 0)
																				{
																					pass = false;
																				}
																			}
																		}
																		if (pass && CardsManager.JudgeInt((int)filters[12], (int)filters[13], card.Level) && CardsManager.JudgeInt((int)filters[14], (int)filters[15], card.Attack) && CardsManager.JudgeInt((int)filters[16], (int)filters[17], card.Defense) && CardsManager.JudgeInt((int)filters[18], (int)filters[19], card.LScale) && CardsManager.CheckGenesysPoint((int)filters[20], (int)filters[21], card) && CardsManager.JudgeInt((int)filters[22], (int)filters[23], card.year))
																		{
																			if (pack == string.Empty)
																			{
																				returnValue.Add(card);
																			}
																			else if (card.packFullName == pack)
																			{
																				returnValue.Add(card);
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return returnValue;
		}

		// Token: 0x06009D6A RID: 40298 RVA: 0x00197D28 File Offset: 0x00195F28
		private static bool CheckGenesysPoint(int min, int max, Card card)
		{
			return (min == -233 && max == -233) || CardsManager.JudgeInt(min, max, card.GetGenesysPoint());
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x00197D4C File Offset: 0x00195F4C
		internal static List<Card> AnnounceSearch(string announced, List<int> searchCodes)
		{
			List<Card> returnValue = new List<Card>();
			foreach (KeyValuePair<int, Card> item in CardsManager._cards)
			{
				Card card = item.Value;
				if ((announced == "" || Regex.Replace(card.Name, announced, "miaowu", RegexOptions.IgnoreCase) != card.Name || Regex.Replace(card.strSetName, announced, "miaowu", RegexOptions.IgnoreCase) != card.strSetName || card.Id.ToString() == announced) && (searchCodes.Count == 0 || CardsManager.IsDeclarable(card, searchCodes)))
				{
					returnValue.Add(card);
				}
			}
			CardsManager.nameInSearch = announced;
			returnValue.Sort(CardsManager.ComparisonOfCard());
			CardsManager.nameInSearch = "";
			return returnValue;
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x00197E38 File Offset: 0x00196038
		internal static List<Card> RelatedSearch(int code)
		{
			List<Card> cards = new List<Card>();
			Card card = CardsManager.GetCard(code);
			if (card == null)
			{
				return cards;
			}
			card.strSetName = StringHelper.GetSetName(card.Setcode).Replace("【", "").Replace("】", "");
			List<string> names = new List<string>();
			names.Add(card.Name);
			List<string> setNames = CardsManager.GetSetNamesInDescription(card.Desc);
			if (!setNames.Contains(card.strSetName))
			{
				setNames.Add(card.strSetName);
			}
			foreach (string match in CardsManager.GetMiddleStrings(card.Desc, "「", "」"))
			{
				if (!names.Contains(match.ToString()))
				{
					names.Add(match.ToString());
				}
			}
			foreach (string s in setNames)
			{
				if (names.Contains(s))
				{
					names.Remove(s);
				}
			}
			names.Remove("");
			setNames.Remove("");
			string result = "";
			foreach (string s2 in setNames)
			{
				result = result + s2 + "\r\n";
			}
			result = "";
			foreach (string s3 in names)
			{
				result = result + s3 + "\r\n";
			}
			List<int> setCodes = new List<int>();
			foreach (string s4 in setNames)
			{
				setCodes.Add(StringHelper.GetSetNameCode(s4));
			}
			foreach (KeyValuePair<int, Card> item in CardsManager._cards)
			{
				if (card.Id != item.Value.Id && !item.Value.HasType(CardType.Token))
				{
					bool pass = false;
					foreach (string i in names)
					{
						if (Regex.Replace(item.Value.Name, i, "miaowu", RegexOptions.IgnoreCase) != item.Value.Name || Regex.Replace(item.Value.Desc, "「" + i + "」", "miaowu", RegexOptions.IgnoreCase) != item.Value.Desc || Regex.Replace(item.Value.strSetName, i, "miaowu", RegexOptions.IgnoreCase) != item.Value.strSetName)
						{
							pass = true;
							break;
						}
					}
					if (!pass)
					{
						for (int j = 0; j < setNames.Count; j++)
						{
							if (Regex.Replace(item.Value.Desc, "「" + setNames[j] + "」", "miaowu", RegexOptions.IgnoreCase) != item.Value.Desc || (setCodes[j] != 0 && ((long)setCodes[j] - item.Value.Setcode == 0L || ~Math.Abs((long)setCodes[j] - item.Value.Setcode) == 2457L)))
							{
								pass = true;
								break;
							}
						}
					}
					if (pass)
					{
						cards.Add(item.Value);
					}
				}
			}
			cards.Sort(CardsManager.ComparisonOfCard());
			return cards;
		}

		// Token: 0x06009D6D RID: 40301 RVA: 0x001982E0 File Offset: 0x001964E0
		private static bool JudgeInt(int min, int max, int raw)
		{
			bool re = true;
			if (min == -233 && max == -233)
			{
				re = true;
			}
			if (min == -233 && max != -233)
			{
				re = max == raw;
			}
			if (min != -233 && max == -233)
			{
				re = min == raw;
			}
			if (min != -233 && max != -233)
			{
				re = min <= raw && raw <= max;
			}
			return re;
		}

		// Token: 0x06009D6E RID: 40302 RVA: 0x0019834C File Offset: 0x0019654C
		private static bool IsDeclarable(Card card, List<int> getsearchCode)
		{
			Stack<int> stack = new Stack<int>();
			for (int i = 0; i < getsearchCode.Count; i++)
			{
				int num = getsearchCode[i];
				switch (num)
				{
				case 1073741824:
					if (stack.Count >= 2)
					{
						int rhs = stack.Pop();
						int lhs = stack.Pop();
						stack.Push(lhs + rhs);
					}
					break;
				case 1073741825:
					if (stack.Count >= 2)
					{
						int rhs2 = stack.Pop();
						int lhs2 = stack.Pop();
						stack.Push(lhs2 - rhs2);
					}
					break;
				case 1073741826:
					if (stack.Count >= 2)
					{
						int rhs3 = stack.Pop();
						int lhs3 = stack.Pop();
						stack.Push(lhs3 * rhs3);
					}
					break;
				case 1073741827:
					if (stack.Count >= 2)
					{
						int rhs4 = stack.Pop();
						int lhs4 = stack.Pop();
						stack.Push(lhs4 / rhs4);
					}
					break;
				case 1073741828:
					if (stack.Count >= 2)
					{
						int num2 = stack.Pop();
						int lhs5 = stack.Pop();
						bool flag = num2 != 0;
						bool b = lhs5 != 0;
						if (flag && b)
						{
							stack.Push(1);
						}
						else
						{
							stack.Push(0);
						}
					}
					break;
				case 1073741829:
					if (stack.Count >= 2)
					{
						int num3 = stack.Pop();
						int lhs6 = stack.Pop();
						bool flag2 = num3 != 0;
						bool b2 = lhs6 != 0;
						if (flag2 || b2)
						{
							stack.Push(1);
						}
						else
						{
							stack.Push(0);
						}
					}
					break;
				case 1073741830:
					if (stack.Count >= 1)
					{
						int rhs5 = stack.Pop();
						stack.Push(-rhs5);
					}
					break;
				case 1073741831:
					if (stack.Count >= 1)
					{
						if (stack.Pop() != 0)
						{
							stack.Push(0);
						}
						else
						{
							stack.Push(1);
						}
					}
					break;
				default:
					switch (num)
					{
					case 1073742080:
						if (stack.Count >= 1)
						{
							if (stack.Pop() == card.Id)
							{
								stack.Push(1);
							}
							else
							{
								stack.Push(0);
							}
						}
						break;
					case 1073742081:
						if (stack.Count >= 1)
						{
							if (CardsManager.IfSetCard(stack.Pop(), card.Setcode))
							{
								stack.Push(1);
							}
							else
							{
								stack.Push(0);
							}
						}
						break;
					case 1073742082:
						if (stack.Count >= 1)
						{
							if ((stack.Pop() & card.Type) > 0)
							{
								stack.Push(1);
							}
							else
							{
								stack.Push(0);
							}
						}
						break;
					case 1073742083:
						if (stack.Count >= 1)
						{
							if ((stack.Pop() & card.Race) > 0)
							{
								stack.Push(1);
							}
							else
							{
								stack.Push(0);
							}
						}
						break;
					case 1073742084:
						if (stack.Count >= 1)
						{
							if ((stack.Pop() & card.Attribute) > 0)
							{
								stack.Push(1);
							}
							else
							{
								stack.Push(0);
							}
						}
						break;
					default:
						stack.Push(getsearchCode[i]);
						break;
					}
					break;
				}
			}
			return stack.Count == 1 && stack.Pop() != 0 && (card.Id == 78734254 || card.Id == 13857930 || (card.Alias == 0 && (card.Type & 16385) != 16385));
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x0019869C File Offset: 0x0019689C
		public static bool IfSetCard(int setCodeToAnalyse, long setCodeFromCard)
		{
			bool res = false;
			int settype = setCodeToAnalyse & 4095;
			int setsubtype = setCodeToAnalyse & 61440;
			for (long sc = setCodeFromCard; sc != 0L; sc >>= 16)
			{
				if ((sc & 4095L) == (long)settype && (sc & 61440L & (long)setsubtype) == (long)setsubtype)
				{
					res = true;
				}
			}
			return res;
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x001986E5 File Offset: 0x001968E5
		internal static Comparison<Card> ComparisonOfCard()
		{
			return delegate(Card left, Card right)
			{
				int a = 1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D71 RID: 40305 RVA: 0x00198706 File Offset: 0x00196906
		internal static Comparison<Card> ComparisonOfCardReverse()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = 1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = -1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D72 RID: 40306 RVA: 0x00198727 File Offset: 0x00196927
		internal static Comparison<Card> ComparisonOfCard_ATK_Down()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D73 RID: 40307 RVA: 0x00198748 File Offset: 0x00196948
		internal static Comparison<Card> ComparisonOfCard_ATK_Up()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = 1;
				}
				else if (left.Attack < right.Attack)
				{
					a = -1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = 1;
				}
				else if (left.Level < right.Level)
				{
					a = -1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D74 RID: 40308 RVA: 0x00198769 File Offset: 0x00196969
		internal static Comparison<Card> ComparisonOfCard_DEF_Down()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Defense > right.Defense)
				{
					a = -1;
				}
				else if (left.Defense < right.Defense)
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D75 RID: 40309 RVA: 0x0019878A File Offset: 0x0019698A
		internal static Comparison<Card> ComparisonOfCard_DEF_Up()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Defense > right.Defense)
				{
					a = 1;
				}
				else if (left.Defense < right.Defense)
				{
					a = -1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = 1;
				}
				else if (left.Level < right.Level)
				{
					a = -1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D76 RID: 40310 RVA: 0x001987AB File Offset: 0x001969AB
		internal static Comparison<Card> ComparisonOfCard_LV_Down()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D77 RID: 40311 RVA: 0x001987CC File Offset: 0x001969CC
		internal static Comparison<Card> ComparisonOfCard_LV_Up()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = 1;
				}
				else if (left.Level < right.Level)
				{
					a = -1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = 1;
				}
				else if (left.Attack < right.Attack)
				{
					a = -1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D78 RID: 40312 RVA: 0x001987ED File Offset: 0x001969ED
		internal static Comparison<Card> ComparisonOfCard_Rarity_Up()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else
				{
					CardRarity.GetRarity(left.Id);
					if (CardRarity.GetRarity(left.Id) > CardRarity.GetRarity(right.Id))
					{
						a = 1;
					}
					else if (CardRarity.GetRarity(left.Id) < CardRarity.GetRarity(right.Id))
					{
						a = -1;
					}
					else if ((left.Type & 7) < (right.Type & 7))
					{
						a = -1;
					}
					else if ((left.Type & 7) > (right.Type & 7))
					{
						a = 1;
					}
					else if ((left.Type & 92283120) < (right.Type & 92283120))
					{
						a = -1;
					}
					else if ((left.Type & 92283120) > (right.Type & 92283120))
					{
						a = 1;
					}
					else if (left.Level > right.Level)
					{
						a = -1;
					}
					else if (left.Level < right.Level)
					{
						a = 1;
					}
					else if (left.Attack > right.Attack)
					{
						a = -1;
					}
					else if (left.Attack < right.Attack)
					{
						a = 1;
					}
					else if (left.Attribute > right.Attribute)
					{
						a = 1;
					}
					else if (left.Attribute < right.Attribute)
					{
						a = -1;
					}
					else if (left.Race > right.Race)
					{
						a = 1;
					}
					else if (left.Race < right.Race)
					{
						a = -1;
					}
					else if (left.Category > right.Category)
					{
						a = 1;
					}
					else if (left.Category < right.Category)
					{
						a = -1;
					}
					else if (left.Id > right.Id)
					{
						a = 1;
					}
					else if (left.Id < right.Id)
					{
						a = -1;
					}
				}
				return a;
			};
		}

		// Token: 0x06009D79 RID: 40313 RVA: 0x0019880E File Offset: 0x00196A0E
		internal static Comparison<Card> ComparisonOfCard_Rarity_Down()
		{
			return delegate(Card left, Card right)
			{
				int a = -1;
				if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else
				{
					CardRarity.GetRarity(left.Id);
					if (CardRarity.GetRarity(left.Id) > CardRarity.GetRarity(right.Id))
					{
						a = -1;
					}
					else if (CardRarity.GetRarity(left.Id) < CardRarity.GetRarity(right.Id))
					{
						a = 1;
					}
					else if ((left.Type & 7) < (right.Type & 7))
					{
						a = -1;
					}
					else if ((left.Type & 7) > (right.Type & 7))
					{
						a = 1;
					}
					else if ((left.Type & 92283120) < (right.Type & 92283120))
					{
						a = -1;
					}
					else if ((left.Type & 92283120) > (right.Type & 92283120))
					{
						a = 1;
					}
					else if (left.Level > right.Level)
					{
						a = -1;
					}
					else if (left.Level < right.Level)
					{
						a = 1;
					}
					else if (left.Attack > right.Attack)
					{
						a = -1;
					}
					else if (left.Attack < right.Attack)
					{
						a = 1;
					}
					else if (left.Attribute > right.Attribute)
					{
						a = 1;
					}
					else if (left.Attribute < right.Attribute)
					{
						a = -1;
					}
					else if (left.Race > right.Race)
					{
						a = 1;
					}
					else if (left.Race < right.Race)
					{
						a = -1;
					}
					else if (left.Category > right.Category)
					{
						a = 1;
					}
					else if (left.Category < right.Category)
					{
						a = -1;
					}
					else if (left.Id > right.Id)
					{
						a = 1;
					}
					else if (left.Id < right.Id)
					{
						a = -1;
					}
				}
				return a;
			};
		}

		// Token: 0x06009D7A RID: 40314 RVA: 0x0019882F File Offset: 0x00196A2F
		internal static Comparison<Card> ComparisonOfCard_GP_Up()
		{
			return delegate(Card left, Card right)
			{
				int a = 1;
				if (left.GetGenesysPoint() < right.GetGenesysPoint())
				{
					a = -1;
				}
				else if (left.GetGenesysPoint() > right.GetGenesysPoint())
				{
					a = 1;
				}
				else if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x00198850 File Offset: 0x00196A50
		internal static Comparison<Card> ComparisonOfCard_GP_Down()
		{
			return delegate(Card left, Card right)
			{
				int a = 1;
				if (left.GetGenesysPoint() < right.GetGenesysPoint())
				{
					a = 1;
				}
				else if (left.GetGenesysPoint() > right.GetGenesysPoint())
				{
					a = -1;
				}
				else if (left.Name == CardsManager.nameInSearch && right.Name != CardsManager.nameInSearch)
				{
					a = -1;
				}
				else if (right.Name == CardsManager.nameInSearch && left.Name != CardsManager.nameInSearch)
				{
					a = 1;
				}
				else if ((left.Type & 7) < (right.Type & 7))
				{
					a = -1;
				}
				else if ((left.Type & 7) > (right.Type & 7))
				{
					a = 1;
				}
				else if ((left.Type & 92283120) < (right.Type & 92283120))
				{
					a = -1;
				}
				else if ((left.Type & 92283120) > (right.Type & 92283120))
				{
					a = 1;
				}
				else if (left.Level > right.Level)
				{
					a = -1;
				}
				else if (left.Level < right.Level)
				{
					a = 1;
				}
				else if (left.Attack > right.Attack)
				{
					a = -1;
				}
				else if (left.Attack < right.Attack)
				{
					a = 1;
				}
				else if (left.Attribute > right.Attribute)
				{
					a = 1;
				}
				else if (left.Attribute < right.Attribute)
				{
					a = -1;
				}
				else if (left.Race > right.Race)
				{
					a = 1;
				}
				else if (left.Race < right.Race)
				{
					a = -1;
				}
				else if (left.Category > right.Category)
				{
					a = 1;
				}
				else if (left.Category < right.Category)
				{
					a = -1;
				}
				else if (left.Id > right.Id)
				{
					a = 1;
				}
				else if (left.Id < right.Id)
				{
					a = -1;
				}
				return a;
			};
		}

		// Token: 0x0400DB65 RID: 56165
		public static string nullName = string.Empty;

		// Token: 0x0400DB66 RID: 56166
		public static string nullString = string.Empty;

		// Token: 0x0400DB67 RID: 56167
		public static string nameInSearch = string.Empty;

		// Token: 0x0400DB68 RID: 56168
		public static IDictionary<int, Card> _cards = new Dictionary<int, Card>();

		// Token: 0x0400DB69 RID: 56169
		public static IDictionary<int, Card> _cardsForRender = new Dictionary<int, Card>();

		// Token: 0x0400DB6A RID: 56170
		public static List<string> setNameHead = new List<string> { "带有" };

		// Token: 0x0400DB6B RID: 56171
		public static List<string> setNameTail = new List<string>
		{
			"、", "卡", "怪兽", "魔法", "陷阱", "通常", "效果怪兽", "融合", "仪式", "灵魂",
			"同盟", "二重", "调整", "同调", "衍生物", "速攻", "永续", "装备", "场地", "反击",
			"反转", "卡通", "超量", "灵摆", "连接"
		};
	}
}

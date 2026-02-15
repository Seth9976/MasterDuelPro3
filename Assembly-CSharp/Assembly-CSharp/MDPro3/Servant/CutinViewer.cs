using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace MDPro3.Servant
{
	// Token: 0x020012CF RID: 4815
	public class CutinViewer : Servant
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06008CBC RID: 36028 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06008CBD RID: 36029 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008CBE RID: 36030 RVA: 0x0012725B File Offset: 0x0012545B
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
			this.LoadCutins();
		}

		// Token: 0x06008CBF RID: 36031 RVA: 0x00127279 File Offset: 0x00125479
		protected override void ApplyHideArrangement(int nextDepth)
		{
			base.ApplyHideArrangement(nextDepth);
			UserInput.SetMoveRepeatRate(0.1f);
			if (this.randomBGMPlayed)
			{
				this.randomBGMPlayed = false;
				AudioManager.PlayBGM("BGM_MENU_01", 1f);
			}
			CameraManager.DuelOverlayEffect3DCount = 0;
			CameraManager.DuelOverlayEffect3DMinus();
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x001272B5 File Offset: 0x001254B5
		protected override void AfterHidingEvent()
		{
			Program.instance.UnloadUnusedAssets();
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x001272C1 File Offset: 0x001254C1
		public override void PerFrameFunction()
		{
			if (this.NeedResponseInput())
			{
				if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
				{
					this.OnReturn();
				}
				if (UserInput.WasGamepadButtonWestPressed)
				{
					this.GetUI<CutinViewerUI>().FocusOnInputField();
				}
				if (UserInput.WasGamepadButtonNorthPressed)
				{
					this.AutoPlay();
				}
			}
		}

		// Token: 0x06008CC2 RID: 36034 RVA: 0x00127300 File Offset: 0x00125500
		public override void OnReturn()
		{
			if (this.returnAction != null)
			{
				return;
			}
			if (this.inTransition)
			{
				return;
			}
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			if (this.cts != null)
			{
				this.cts.Cancel();
				this.cts.Dispose();
				this.cts = null;
				UIManager.ShowExitButton(this.TransitionTime, Ease.Linear);
				this.servantUI.CG.alpha = 1f;
				this.servantUI.CG.blocksRaycasts = true;
				return;
			}
			this.OnExit();
		}

		// Token: 0x06008CC3 RID: 36035 RVA: 0x0012738C File Offset: 0x0012558C
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			this.lastSelectedCutinItem.GetSelectable().Select();
		}

		// Token: 0x06008CC4 RID: 36036 RVA: 0x001273AC File Offset: 0x001255AC
		public void LoadCutins()
		{
			if (CutinViewer.dirInfos == null || CutinViewer.fileInfos == null)
			{
				string targetFolder = Program.root + "MonsterCutin";
				string targetFolder2 = Program.root + "MonsterCutin2";
				targetFolder = Path.Combine(Application.dataPath, Program.root + "MonsterCutin");
				targetFolder2 = Path.Combine(Application.dataPath, Program.root + "MonsterCutin2");
				if (!Directory.Exists(targetFolder))
				{
					Directory.CreateDirectory(targetFolder);
				}
				if (!Directory.Exists(targetFolder2))
				{
					Directory.CreateDirectory(targetFolder2);
				}
				CutinViewer.dirInfos = new DirectoryInfo(targetFolder).GetDirectories();
				CutinViewer.fileInfos = new DirectoryInfo(targetFolder2).GetFiles();
			}
			CutinViewer.cards.Clear();
			CutinViewer.codes.Clear();
			CutinViewer.codes2.Clear();
			for (int i = 0; i < CutinViewer.dirInfos.Length; i++)
			{
				int code;
				if (int.TryParse(CutinViewer.dirInfos[i].Name, out code))
				{
					Card card = CardsManager.Get(code, false);
					CutinViewer.cards.Add(card);
					CutinViewer.codes.Add(card.Id);
				}
			}
			for (int j = 0; j < CutinViewer.fileInfos.Length; j++)
			{
				int code2;
				if (int.TryParse(CutinViewer.fileInfos[j].Name, out code2) && !CutinViewer.codes.Contains(code2))
				{
					Card card2 = CardsManager.Get(code2, false);
					CutinViewer.cards.Add(card2);
					CutinViewer.codes2.Add(card2.Id);
				}
			}
			CutinViewer.cards.Sort(CardsManager.ComparisonOfCard());
			if (this.servantUI != null)
			{
				this.GetUI<CutinViewerUI>().Print("");
			}
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x00127555 File Offset: 0x00125755
		public void SelectLastCutinItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Select(false);
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x00127564 File Offset: 0x00125764
		public static bool HasCutin(int code)
		{
			if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelCutin", true))
			{
				return false;
			}
			if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("WatchCutin", true))
			{
				return false;
			}
			if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("ReplayCutin", true))
			{
				return false;
			}
			code = CutinViewer.AliasCode(code);
			bool returnValue = false;
			using (List<Card>.Enumerator enumerator = CutinViewer.cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == code)
					{
						returnValue = true;
						break;
					}
				}
			}
			return returnValue;
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x0012760C File Offset: 0x0012580C
		private static int AliasCode(int code)
		{
			if (code == 89631142 || code == 89631148)
			{
				return 89631141;
			}
			if (code == 89943725)
			{
				return 89943723;
			}
			if (code == 46986424 || code == 46986426)
			{
				return 46986417;
			}
			if (code == 74677425)
			{
				return 74677424;
			}
			if (code == 44508096)
			{
				return 44508094;
			}
			if (code == 84013240)
			{
				return 84013237;
			}
			if (code == 16178684)
			{
				return 16178681;
			}
			if (code == 5043013)
			{
				return 5043010;
			}
			return code;
		}

		// Token: 0x06008CC8 RID: 36040 RVA: 0x0012769C File Offset: 0x0012589C
		public static async UniTask Play(int code, int controller)
		{
			if (!CutinViewer.playing)
			{
				CutinViewer.playing = true;
				code = CutinViewer.AliasCode(code);
				Card card = CardsManager.Get(code, false);
				GameObject cutin = null;
				bool diy = false;
				if (CutinViewer.codes.Contains(code))
				{
					GameObject gameObject = await ABLoader.LoadMonsterCutinAsync(code, false);
					cutin = gameObject;
				}
				else
				{
					GameObject gameObject = await ABLoader.LoadFromFileAsync("MonsterCutin2/" + code.ToString(), false, true);
					cutin = gameObject;
					diy = true;
				}
				cutin.transform.SetParent(Program.instance.container_2D, false);
				if (!diy)
				{
					cutin.transform.localPosition = Vector3.zero;
					cutin.transform.GetComponent<PlayableDirector>().time = 0.0;
				}
				string backPath;
				if (card.IsAttribute(CardAttribute.Dark))
				{
					backPath = "SummonMonster_Bgdak_S2";
				}
				else if (card.IsAttribute(CardAttribute.Light))
				{
					backPath = "SummonMonster_Bglit_S2";
				}
				else if (card.IsAttribute(CardAttribute.Earth))
				{
					backPath = "SummonMonster_Bgeah_S2";
				}
				else if (card.IsAttribute(CardAttribute.Water))
				{
					backPath = "SummonMonster_Bgwtr_S2";
				}
				else if (card.IsAttribute(CardAttribute.Fire))
				{
					backPath = "SummonMonster_Bgfie_S2";
				}
				else if (card.IsAttribute(CardAttribute.Wind))
				{
					backPath = "SummonMonster_Bgwid_S2";
				}
				else
				{
					backPath = "SummonMonster_Bgdve_S2";
				}
				GameObject back = ABLoader.LoadMasterDuelOutDuelObject(backPath);
				back.transform.SetParent(Program.instance.container_2D, false);
				GameObject nameBar;
				if (controller == 0)
				{
					nameBar = ABLoader.LoadMasterDuelOutDuelObject("SummonMonster_Name_near");
				}
				else
				{
					nameBar = ABLoader.LoadMasterDuelOutDuelObject("SummonMonster_Name_far");
				}
				nameBar.transform.SetParent(Program.instance.container_2D, false);
				ElementObjectManager manager = nameBar.GetComponent<ElementObjectManager>();
				manager.GetElement<TextMeshPro>("Monster_Name_TMP").text = card.Name;
				string para = "ATK " + card.GetAttackString();
				if (!card.HasType(CardType.Link))
				{
					para = para + " DEF " + card.GetDefenseString();
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_LINK"));
				}
				else
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level"));
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level_Odd"));
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank"));
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank_Odd"));
					switch (card.GetLinkCount())
					{
					case 2:
						manager.GetElement<ElementObjectManager>("Icon_LINK").GetElement<SpriteRenderer>("LINK1").sprite = TextureManager.container.link2;
						break;
					case 3:
						manager.GetElement<ElementObjectManager>("Icon_LINK").GetElement<SpriteRenderer>("LINK1").sprite = TextureManager.container.link3;
						break;
					case 4:
						manager.GetElement<ElementObjectManager>("Icon_LINK").GetElement<SpriteRenderer>("LINK1").sprite = TextureManager.container.link4;
						break;
					case 5:
						manager.GetElement<ElementObjectManager>("Icon_LINK").GetElement<SpriteRenderer>("LINK1").sprite = TextureManager.container.link5;
						break;
					case 6:
						manager.GetElement<ElementObjectManager>("Icon_LINK").GetElement<SpriteRenderer>("LINK1").sprite = TextureManager.container.link6;
						break;
					}
				}
				ElementObjectManager subManager;
				if (!card.HasType(CardType.Xyz))
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank"));
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank_Odd"));
					if (card.Level % 2 == 0)
					{
						subManager = manager.GetElement<ElementObjectManager>("Icon_Level");
						global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level_Odd"));
					}
					else
					{
						subManager = manager.GetElement<ElementObjectManager>("Icon_Level_Odd");
						global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level"));
					}
				}
				else
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level"));
					global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Level_Odd"));
					if (card.Level % 2 == 0)
					{
						subManager = manager.GetElement<ElementObjectManager>("Icon_Rank");
						global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank_Odd"));
					}
					else
					{
						subManager = manager.GetElement<ElementObjectManager>("Icon_Rank_Odd");
						global::UnityEngine.Object.Destroy(manager.GetElement("Icon_Rank"));
					}
				}
				if (!card.HasType(CardType.Link))
				{
					for (int i = card.Level + 1; i < 14; i++)
					{
						global::UnityEngine.Object.Destroy(subManager.GetElement("Icon" + i.ToString()));
					}
				}
				manager.GetElement<TextMesh>("Monster_Para").text = para;
				GameObject frontEffect = ABLoader.LoadMasterDuelOutDuelObject("SummonMonster_Thunder_power");
				frontEffect.transform.SetParent(Program.instance.container_2D, false);
				if (Program.instance.ocgcore.showing)
				{
					AudioManager.PlayBgmKeyCard();
				}
				await UniTask.WaitForSeconds(1.6f, false, PlayerLoopTiming.Update, default(CancellationToken), false).ContinueWith(delegate
				{
					global::UnityEngine.Object.Destroy(cutin);
					global::UnityEngine.Object.Destroy(back);
					global::UnityEngine.Object.Destroy(nameBar);
					global::UnityEngine.Object.Destroy(frontEffect);
					CutinViewer.playing = false;
				});
			}
		}

		// Token: 0x06008CC9 RID: 36041 RVA: 0x001276E7 File Offset: 0x001258E7
		public void AutoPlay()
		{
			this.cts = new CancellationTokenSource();
			this.AutoPlayAsync(this.cts.Token);
		}

		// Token: 0x06008CCA RID: 36042 RVA: 0x00127708 File Offset: 0x00125908
		private async UniTask AutoPlayAsync(CancellationToken token)
		{
			await UniTask.WaitWhile(() => CutinViewer.playing, PlayerLoopTiming.Update, token, false);
			if (this.showing)
			{
				AudioManager.PlayRandomKeyCardBGM();
				this.randomBGMPlayed = true;
				this.servantUI.CG.alpha = 0f;
				this.servantUI.CG.blocksRaycasts = false;
				UIManager.HideExitButton(this.TransitionTime, Ease.Linear);
				int count = 0;
				foreach (Card card in CutinViewer.cards)
				{
					await CutinViewer.Play(card.Id, 0);
					count++;
					if (count % 20 == 0)
					{
						await Resources.UnloadUnusedAssets();
					}
					if (token.IsCancellationRequested)
					{
						return;
					}
				}
				List<Card>.Enumerator enumerator = default(List<Card>.Enumerator);
				this.servantUI.CG.alpha = 1f;
				this.servantUI.CG.blocksRaycasts = true;
				UIManager.ShowExitButton(this.TransitionTime, Ease.Linear);
				CutinViewer.autoPlaying = false;
				this.cts.Dispose();
				this.cts = null;
			}
		}

		// Token: 0x0400CADA RID: 51930
		public const float CUTIN_PLAY_TIME = 1.6f;

		// Token: 0x0400CADB RID: 51931
		public static int controller = 0;

		// Token: 0x0400CADC RID: 51932
		public static List<Card> cards = new List<Card>();

		// Token: 0x0400CADD RID: 51933
		public static List<int> codes = new List<int>();

		// Token: 0x0400CADE RID: 51934
		public static List<int> codes2 = new List<int>();

		// Token: 0x0400CADF RID: 51935
		private static DirectoryInfo[] dirInfos;

		// Token: 0x0400CAE0 RID: 51936
		private static FileInfo[] fileInfos;

		// Token: 0x0400CAE1 RID: 51937
		private bool randomBGMPlayed;

		// Token: 0x0400CAE2 RID: 51938
		[HideInInspector]
		public SelectionToggle_Cutin lastSelectedCutinItem;

		// Token: 0x0400CAE3 RID: 51939
		private static bool playing;

		// Token: 0x0400CAE4 RID: 51940
		private static bool autoPlaying;

		// Token: 0x0400CAE5 RID: 51941
		private CancellationTokenSource cts;
	}
}

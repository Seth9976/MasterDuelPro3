using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.AsyncOperations;
using YgomGame.Bg;
using YgomSystem.Effect;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;

namespace MDPro3.Duel
{
	// Token: 0x02001493 RID: 5267
	public class DuelBGManager
	{
		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06009A2F RID: 39471 RVA: 0x00170DA0 File Offset: 0x0016EFA0
		private OcgCore Core
		{
			get
			{
				return Program.instance.ocgcore;
			}
		}

		// Token: 0x06009A30 RID: 39472 RVA: 0x00170DAC File Offset: 0x0016EFAC
		public async UniTask LoadAssetsAsync()
		{
			this.loaded = false;
			this.deck = null;
			string deckName = Config.GetConfigDeckName(true);
			if (OcgCore.condition == OcgCore.Condition.Duel && !OcgCore.inPuzzle && File.Exists("Deck/" + deckName + ".ydk"))
			{
				this.deck = new Deck("Deck/" + deckName + ".ydk");
			}
			UIManager.UIBlackIn(this.Core.TransitionTime);
			await UniTask.WaitForSeconds(this.Core.TransitionTime, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			await UniTask.WaitUntil(() => Appearance.loaded, PlayerLoopTiming.Update, default(CancellationToken), false);
			await ABLoader.CacheMasterDuelBundles();
			Program.instance.ocgcore.LoadDuelButton();
			CameraManager.ShiftTo3D();
			UIManager.HideExitButton(0f, Ease.Linear);
			UIManager.HideLine(0f);
			AudioManager.StopBGM();
			if (this.attackLine == null)
			{
				this.attackLine = ABLoader.LoadMasterDuelGameObject("fxp_atk_select_arrow_001");
				this.attackLine.SetActive(false);
				await UniTask.Yield();
			}
			if (this.targetLine == null)
			{
				this.targetLine = ABLoader.LoadMasterDuelGameObject("fxp_target_arrow_001");
				this.targetLine.SetActive(false);
				await UniTask.Yield();
			}
			if (this.equipLine == null)
			{
				this.equipLine = ABLoader.LoadMasterDuelGameObject("fxp_equip_arrow_001");
				this.equipLine.SetActive(false);
				await UniTask.Yield();
			}
			if (this.fieldSummonRightInfo == null)
			{
				AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Prefab/FieldSummonRightInfo.prefab", null, false, true);
				await handle;
				this.fieldSummonRightInfo = handle.Result;
				this.fieldSummonRightInfo.SetActive(false);
				this.fieldSummonRightInfo.transform.SetParent(Program.instance.container_3D);
				handle = default(AsyncOperationHandle<GameObject>);
			}
			Items items = Program.items;
			string text = OcgCore.condition.ToString() + "Field0";
			Items.Item item = Program.items.mats[0];
			string path = items.GetAssetPath(Config.Get(text, item.id.ToString()), Items.ItemType.Mat, 0);
			if (this.deck != null && !Config.GetBool("OverrideDeckAppearance", false))
			{
				path = Program.items.GetAssetPath(this.deck.Field.ToString(), Items.ItemType.Mat, 0);
			}
			path = "MasterDuel/" + path;
			GameObject field0 = await ABLoader.LoadFromFileAsync(path + "_near", false, true);
			field0.transform.SetParent(Program.instance.container_3D, false);
			this.field0Manager = field0.GetComponent<BgEffectManager>();
			string text2 = "MasterDuel/";
			Items items2 = Program.items;
			string text3 = OcgCore.condition.ToString() + "Field1";
			item = Program.items.mats[0];
			GameObject field = await ABLoader.LoadFromFileAsync(text2 + items2.GetAssetPath(Config.Get(text3, item.id.ToString()), Items.ItemType.Mat, 1) + "_far", false, true);
			field.transform.SetParent(Program.instance.container_3D, false);
			this.field1Manager = field.GetComponent<BgEffectManager>();
			BoxCollider boxCollider = field0.AddComponent<BoxCollider>();
			boxCollider.center = new Vector3(38f, 5f, -10f);
			boxCollider.size = new Vector3(10f, 10f, 10f);
			BoxCollider boxCollider2 = field.AddComponent<BoxCollider>();
			boxCollider2.center = new Vector3(-38f, 5f, 10f);
			boxCollider2.size = new Vector3(10f, 10f, 10f);
			Transform pos_Grave_near = field0.transform.GetChildByName("POS_Grave_near");
			Transform pos_Grave_far = field.transform.GetChildByName("POS_Grave_far");
			Transform pos_AvatarStand_near = field0.transform.GetChildByName("POS_AvatarStand_near");
			Transform pos_AvatarStand_far = field.transform.GetChildByName("POS_AvatarStand_far");
			Transform pos_Avatar_near = field0.transform.GetChildByName("POS_Avatar_near");
			Transform pos_Avatar_far = field.transform.GetChildByName("POS_Avatar_far");
			this.allGameObjects.Add(field0);
			this.allGameObjects.Add(field);
			Items items3 = Program.items;
			string text4 = OcgCore.condition.ToString() + "Grave0";
			item = Program.items.graves[0];
			path = items3.GetAssetPath(Config.Get(text4, item.id.ToString()), Items.ItemType.Grave, 0);
			if (this.deck != null && !Config.GetBool("OverrideDeckAppearance", false))
			{
				path = Program.items.GetAssetPath(this.deck.Grave.ToString(), Items.ItemType.Grave, 0);
			}
			path = "MasterDuel/" + path;
			GameObject grave0 = await ABLoader.LoadFromFileAsync(path + "_near", false, true);
			grave0.transform.SetParent(pos_Grave_near, false);
			this.grave0Manager = grave0.GetComponent<BgEffectManager>();
			string text5 = "MasterDuel/";
			Items items4 = Program.items;
			string text6 = OcgCore.condition.ToString() + "Grave1";
			item = Program.items.graves[0];
			GameObject grave = await ABLoader.LoadFromFileAsync(text5 + items4.GetAssetPath(Config.Get(text6, item.id.ToString()), Items.ItemType.Grave, 1) + "_far", false, true);
			grave.transform.SetParent(pos_Grave_far, false);
			this.grave1Manager = grave.GetComponent<BgEffectManager>();
			Tools.PlayAnimation(grave0.transform, "StartToPhase1");
			Tools.PlayAnimation(grave.transform, "StartToPhase1");
			this.graves.Clear();
			GraveBehaviour g0 = grave0.AddComponent<GraveBehaviour>();
			g0.controller = 0;
			this.graves.Add(g0);
			GraveBehaviour g = grave.AddComponent<GraveBehaviour>();
			g.controller = 1;
			this.graves.Add(g);
			string text7 = OcgCore.condition.ToString() + "Stand0";
			item = Program.items.stands[0];
			string standConfig = Config.Get(text7, item.id.ToString());
			if (standConfig != 0.ToString() || this.deck != null)
			{
				path = Program.items.GetAssetPath(standConfig, Items.ItemType.Stand, 0);
				if (this.deck != null && !Config.GetBool("OverrideDeckAppearance", false))
				{
					path = Program.items.GetAssetPath(this.deck.Stand.ToString(), Items.ItemType.Stand, 0);
				}
				path = "MasterDuel/" + path;
				GameObject stand0 = await ABLoader.LoadFromFileAsync(path + "_near", false, true);
				stand0.transform.SetParent(pos_AvatarStand_near, false);
				pos_Avatar_near = stand0.transform.GetChildByName("POS_Avatar_near");
				Tools.PlayAnimation(stand0.transform, "StartToPhase1");
				this.stand0Manager = stand0.GetComponent<BgEffectManager>();
			}
			string text8 = OcgCore.condition.ToString() + "Stand1";
			item = Program.items.stands[0];
			standConfig = Config.Get(text8, item.id.ToString());
			if (standConfig != 0.ToString())
			{
				GameObject stand = await ABLoader.LoadFromFileAsync("MasterDuel/" + Program.items.GetAssetPath(standConfig, Items.ItemType.Stand, 1) + "_far", false, true);
				stand.transform.SetParent(pos_AvatarStand_far, false);
				pos_Avatar_far = stand.transform.GetChildByName("POS_Avatar_far");
				Tools.PlayAnimation(stand.transform, "StartToPhase1");
				this.stand1Manager = stand.GetComponent<BgEffectManager>();
			}
			string text9 = OcgCore.condition.ToString() + "Mate0";
			item = Program.items.mates[0];
			string mateConfig = Config.Get(text9, item.id.ToString());
			if (mateConfig != 0.ToString() || this.deck != null)
			{
				int mateCode = int.Parse(mateConfig);
				if (this.deck != null && !Config.GetBool("OverrideDeckAppearance", false))
				{
					mateCode = this.deck.Mate;
				}
				Mate mate = await ABLoader.LoadMateAsync(mateCode);
				if (mate != null)
				{
					this.mate0 = mate;
					this.mate0.parent = pos_Avatar_near;
					this.mate0.gameObject.SetActive(false);
				}
			}
			string text10 = OcgCore.condition.ToString() + "Mate1";
			item = Program.items.mates[0];
			mateConfig = Config.Get(text10, item.id.ToString());
			if (mateConfig != 0.ToString())
			{
				string text11 = OcgCore.condition.ToString() + "Mate1";
				item = Program.items.mates[0];
				Mate mate2 = await ABLoader.LoadMateAsync(int.Parse(Config.Get(text11, item.id.ToString())));
				if (mate2 != null)
				{
					this.mate1 = mate2;
					this.mate1.parent = pos_Avatar_far;
					this.mate1.gameObject.SetActive(false);
				}
			}
			GameObject matBack = ABLoader.LoadMasterDuelGameObject("CelestialSphere_c001");
			matBack.transform.SetParent(Program.instance.container_3D, false);
			matBack.transform.localScale = Vector3.one * 2f;
			this.allGameObjects.Add(matBack);
			if (DuelBGManager.MatIsSpecial(field.name))
			{
				this.phaseButton = await ABLoader.LoadFromFileAsync("MasterDuel/BG/Timer/PhaseButton_013", true, true);
				this.phaseButton.GetComponent<Animator>().SetTrigger("Start");
				Tools.PlayAnimation(this.phaseButton.transform, "StartToPhase1");
			}
			else
			{
				this.phaseButton = ABLoader.LoadMasterDuelGameObject("PhaseButton_c001");
				await UniTask.Yield();
			}
			this.phaseButton.transform.SetParent(Program.instance.container_3D, false);
			this.allGameObjects.Add(this.phaseButton);
			this.phaseButton.AddComponent<PhaseButtonHandler>();
			if (OcgCore.condition == OcgCore.Condition.Duel && !OcgCore.inputMode)
			{
				GameObject timer;
				if (DuelBGManager.MatIsSpecial(field.name))
				{
					timer = await ABLoader.LoadFromFileAsync("MasterDuel/BG/Timer/Timer_013", true, true);
				}
				else
				{
					timer = ABLoader.LoadMasterDuelGameObject("Timer_c001");
					await UniTask.Yield();
				}
				this.timerHandler = timer.AddComponent<TimerHandler>();
				timer.transform.SetParent(Program.instance.container_3D, false);
				this.timerHandler.timeLimit = OcgCore.timeLimit;
				this.timerHandler.time = OcgCore.timeLimit;
				this.allGameObjects.Add(timer);
				timer = null;
			}
			this.playableGuide = this.Create<PlayableGuide>(true);
			this.playableGuide.Load(field0.name, field.name);
			await UniTask.WaitUntil(() => this.playableGuide.loaded, PlayerLoopTiming.Update, default(CancellationToken), false);
			if (DeviceInfo.OnAndroid())
			{
				this.playableGuide.SetHeight(0.6f);
			}
			Material deckMat = Appearance.duelProtector0;
			if (this.deck != null && !Config.GetBool("OverrideDeckAppearance", false))
			{
				deckMat = await ABLoader.LoadProtectorMaterial(this.deck.Protector.ToString(), Application.exitCancellationToken);
			}
			if (OcgCore.condition == OcgCore.Condition.Duel)
			{
				OcgCore.myProtector = deckMat;
			}
			else if (OcgCore.condition == OcgCore.Condition.Watch)
			{
				OcgCore.myProtector = Appearance.watchProtector0;
			}
			else if (OcgCore.condition == OcgCore.Condition.Replay)
			{
				OcgCore.myProtector = Appearance.replayProtector0;
			}
			if (OcgCore.condition == OcgCore.Condition.Duel)
			{
				OcgCore.opProtector = Appearance.duelProtector1;
			}
			else if (OcgCore.condition == OcgCore.Condition.Watch)
			{
				OcgCore.opProtector = Appearance.watchProtector1;
			}
			else if (OcgCore.condition == OcgCore.Condition.Replay)
			{
				OcgCore.opProtector = Appearance.replayProtector1;
			}
			GameObject deckModel = ABLoader.LoadMasterDuelGameObject("DuelDeckAppearance");
			this.myDeck = deckModel.GetComponent<ElementObjectManager>();
			this.InitializeDeckModel(this.myDeck, 0, CardLocation.Deck);
			this.myExtra = global::UnityEngine.Object.Instantiate<GameObject>(deckModel).GetComponent<ElementObjectManager>();
			this.InitializeDeckModel(this.myExtra, 0, CardLocation.Extra);
			this.opDeck = global::UnityEngine.Object.Instantiate<GameObject>(deckModel).GetComponent<ElementObjectManager>();
			this.InitializeDeckModel(this.opDeck, 1, CardLocation.Deck);
			this.opExtra = global::UnityEngine.Object.Instantiate<GameObject>(deckModel).GetComponent<ElementObjectManager>();
			this.InitializeDeckModel(this.opExtra, 1, CardLocation.Extra);
			this.places.Clear();
			for (uint c = 0U; c < 2U; c += 1U)
			{
				this.CreatePlaceSelector(new GPS
				{
					controller = c,
					location = 1U
				});
				this.CreatePlaceSelector(new GPS
				{
					controller = c,
					location = 64U
				});
				uint s = 0U;
				while ((ulong)s < (ulong)((c == 0U) ? 7L : 5L))
				{
					this.CreatePlaceSelector(new GPS
					{
						controller = c,
						location = 4U,
						sequence = s
					});
					s += 1U;
				}
				for (uint s2 = 0U; s2 < 6U; s2 += 1U)
				{
					this.CreatePlaceSelector(new GPS
					{
						controller = c,
						location = 8U,
						sequence = s2
					});
				}
			}
			await this.processor.PreloadPlayerNames();
			if (this.Core.NeedVoice())
			{
				this.Core.GetUI<OcgCoreUI>().CG.alpha = 1f;
				this.Core.GetUI<OcgCoreUI>().CG.blocksRaycasts = true;
			}
			this.Core.GetUI<OcgCoreUI>().Buttons.SetActive(false);
			UIManager.ShowFPSLeft();
			UIManager.HideBlackBack(0f);
			UIManager.UIBlackOut(this.Core.TransitionTime);
			await UniTask.WaitForSeconds(this.Core.TransitionTime, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			this.backgroundFieldInitialize = false;
			this.BackgroundFieldInitialize();
			if ((OcgCore.condition == OcgCore.Condition.Duel && Config.GetBool("DuelAutoAcc", false)) || (OcgCore.condition == OcgCore.Condition.Watch && Config.GetBool("WatchAutoAcc", false)) || (OcgCore.condition == OcgCore.Condition.Replay && Config.GetBool("ReplayAutoAcc", false)))
			{
				this.Core.GetUI<OcgCoreUI>().OnAcc();
			}
			this.loaded = true;
		}

		// Token: 0x06009A31 RID: 39473 RVA: 0x00170DF0 File Offset: 0x0016EFF0
		public async UniTask ExitDuelAsync()
		{
			this.ClearResponse();
			CameraManager.BlackOut(0f, 0.3f);
			UIManager.UIBlackIn(this.Core.TransitionTime);
			this.Core.GetUI<OcgCoreUI>().CloseHint();
			this.HideAttackLine();
			this.HideDuelFinalBlowText();
			await UniTask.WaitForSeconds(this.Core.TransitionTime, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			this.Core.servantUI.ShutDown();
			OcgCore.NoMoreWait = true;
			OcgCore.packages.Clear();
			OcgCore.allPackages.Clear();
			AudioManager.ResetSESource();
			OcgCore.mycardDuel = false;
			this.Core.CloseCharaFace();
			this.Dispose();
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.Dispose();
			}
			OcgCore.cards.Clear();
			OcgCore.pause = false;
			OcgCore.nextMoveAction = null;
			this.Core.cachedCharaFaces.Clear();
			CameraManager.ShiftTo2D();
			this.Core.GetUI<OcgCoreUI>().Buttons.SetActive(false);
			this.Core.GetUI<OcgCoreUI>().DuelLog.ClearLog();
			Program.instance.ui_.chatPanel.Hide();
			await UniTask.WaitForSeconds(0.3f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			UIManager.UIBlackOut(this.Core.TransitionTime);
			await UniTask.WaitForSeconds(this.Core.TransitionTime, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			UIManager.ShowFPSRight();
			AudioManager.PlayBGM("BGM_MENU_01", 1f);
		}

		// Token: 0x06009A32 RID: 39474 RVA: 0x00170E34 File Offset: 0x0016F034
		private void InitializeDeckModel(ElementObjectManager deckManager, int player, CardLocation location)
		{
			deckManager.transform.SetParent((player == 0) ? this.field0Manager.transform : this.field1Manager.transform, false);
			deckManager.transform.localPosition = DuelBGManager._positionMap[new ValueTuple<int, CardLocation>(player, location)];
			deckManager.transform.localEulerAngles = DuelBGManager._angleMap[new ValueTuple<int, CardLocation>(player, location)];
			this.allGameObjects.Add(deckManager.gameObject);
			Material mat = ((player == 0) ? OcgCore.myProtector : OcgCore.opProtector);
			deckManager.GetNestedElement<MeshRenderer>("DummyDeck/DummyCardModel_back").material = mat;
			deckManager.GetNestedElement<MeshRenderer>("CardShuffleTop/CardModel01_back").material = mat;
			deckManager.GetNestedElement<MeshRenderer>("CardShuffleTop/CardModel02_back").material = mat;
			deckManager.GetNestedElement<MeshRenderer>("CardShuffleTop/CardModel03_back").material = mat;
			deckManager.GetNestedElement<MeshRenderer>("CardShuffleTop/CardModel04_back").material = mat;
			deckManager.gameObject.SetActive(false);
		}

		// Token: 0x06009A33 RID: 39475 RVA: 0x00170F24 File Offset: 0x0016F124
		public async UniTask ShowDecksAsync()
		{
			if (!(this.myDeck == null) && !(this.myExtra == null) && !(this.opDeck == null) && !(this.opExtra == null))
			{
				await this.ShowAllDeckModelsAsync();
				this.Core.GetUI<OcgCoreUI>().CG.alpha = 1f;
				this.Core.GetUI<OcgCoreUI>().CG.blocksRaycasts = true;
				this.Core.GetUI<OcgCoreUI>().Buttons.SetActive(true);
				AudioManager.PlayBgmNormal(Config.GetBool("BGMbyMySide", true) ? this.field0Manager.name : this.field1Manager.name);
			}
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x00170F68 File Offset: 0x0016F168
		public async UniTask ShowDecksWithDuelStartTextAsync()
		{
			if (!(this.myDeck == null) && !(this.myExtra == null) && !(this.opDeck == null) && !(this.opExtra == null))
			{
				await this.ShowAllDeckModelsAsync();
				GameObject effect = ABLoader.LoadMasterDuelGameObject("DuelTextStart");
				await effect.GetComponent<PlayableDirector>().WaitAsync(true, default(CancellationToken));
				global::UnityEngine.Object.Destroy(effect);
				this.Core.GetUI<OcgCoreUI>().CG.alpha = 1f;
				this.Core.GetUI<OcgCoreUI>().CG.blocksRaycasts = true;
				this.Core.GetUI<OcgCoreUI>().Buttons.SetActive(true);
				AudioManager.PlayBgmNormal(Config.GetBool("BGMbyMySide", true) ? this.field0Manager.name : this.field1Manager.name);
			}
		}

		// Token: 0x06009A35 RID: 39477 RVA: 0x00170FAC File Offset: 0x0016F1AC
		public void BackgroundFieldInitialize()
		{
			if (this.field0Manager == null || this.field1Manager == null)
			{
				return;
			}
			if (this.backgroundFieldInitialize)
			{
				this.field0Manager.gameObject.SetActive(false);
				this.field1Manager.gameObject.SetActive(false);
				this.field0Manager.gameObject.SetActive(true);
				this.field1Manager.gameObject.SetActive(true);
			}
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.StartToPhase1, "");
			this.grave0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.StartToPhase1, "");
			this.bgPhase0 = 1;
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.StartToPhase1, "");
			this.grave1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.StartToPhase1, "");
			this.bgPhase1 = 1;
			if (this.mate0 != null)
			{
				this.mate0.gameObject.SetActive(true);
				this.mate0.Play(Mate.MateAction.Entry);
			}
			if (this.mate1 != null)
			{
				this.mate1.gameObject.SetActive(true);
				this.mate1.Play(Mate.MateAction.Entry);
			}
			if (this.timerHandler != null)
			{
				this.timerHandler.DuelStart();
			}
			this.mate0Random = false;
			this.mate1Random = false;
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, (float)global::UnityEngine.Random.Range(8, 16)).OnComplete(delegate
			{
				this.mate0Random = true;
			});
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, (float)global::UnityEngine.Random.Range(8, 16)).OnComplete(delegate
			{
				this.mate1Random = true;
			});
			this.backgroundFieldInitialize = true;
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x0017118A File Offset: 0x0016F38A
		public void RefreshBgState()
		{
			this.ResizeDecks();
			this.RefreshGravesState();
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x00171198 File Offset: 0x0016F398
		public void ResizeDecks()
		{
			if (this.myDeck == null || this.myExtra == null || this.opDeck == null || this.opExtra == null)
			{
				return;
			}
			this.ResizeDeckModel(this.myDeck, this.Core.GetLocationCardCount(CardLocation.Deck, 0U));
			this.ResizeDeckModel(this.myExtra, this.Core.GetLocationCardCount(CardLocation.Extra, 0U));
			this.ResizeDeckModel(this.opDeck, this.Core.GetLocationCardCount(CardLocation.Deck, 1U));
			this.ResizeDeckModel(this.opExtra, this.Core.GetLocationCardCount(CardLocation.Extra, 1U));
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x00171244 File Offset: 0x0016F444
		public void RefreshGravesState()
		{
			if (this.grave0Manager == null || this.grave1Manager == null)
			{
				return;
			}
			this.RefreshGraveState(this.grave0Manager, this.Core.GetLocationCardCount(CardLocation.Grave, 0U));
			this.RefreshExcludeState(this.grave0Manager, this.Core.GetLocationCardCount(CardLocation.Removed, 0U));
			this.RefreshGraveState(this.grave1Manager, this.Core.GetLocationCardCount(CardLocation.Grave, 1U));
			this.RefreshExcludeState(this.grave1Manager, this.Core.GetLocationCardCount(CardLocation.Removed, 1U));
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x001712D8 File Offset: 0x0016F4D8
		private void ResizeDeckModel(ElementObjectManager deck, int count)
		{
			Transform deckSetOffset = deck.GetElement<Transform>("DeckSetOffset");
			if (count == 0)
			{
				deckSetOffset.transform.localScale = Vector3.zero;
			}
			else
			{
				deckSetOffset.transform.localScale = new Vector3(0.9f, (float)count / 40f, 0.9f);
			}
			Transform cardShuffle = deck.GetElement<Transform>("CardShuffleTop");
			if (count == 0)
			{
				cardShuffle.transform.localScale = Vector3.zero;
				return;
			}
			cardShuffle.transform.localScale = new Vector3(0.9f, (float)count / 40f, 0.9f);
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x0017136C File Offset: 0x0016F56C
		private void RefreshGraveState(ElementObjectManager grave, int count)
		{
			Dictionary<Func<int, bool>, string> dictionary = new Dictionary<Func<int, bool>, string>();
			dictionary.Add((int c) => c >= 20, "GraveIdleS3");
			dictionary.Add((int c) => c >= 10, "GraveIdleS2");
			dictionary.Add((int c) => c > 0, "GraveIdleS1");
			Dictionary<Func<int, bool>, string> stateMap = dictionary;
			Dictionary<string, ParticleSystem> particleSystems = new string[] { "GraveIdleS1", "GraveIdleS2", "GraveIdleS3" }.ToDictionary((string name) => name, new Func<string, ParticleSystem>(grave.GetElement<ParticleSystem>));
			foreach (ParticleSystem particleSystem in particleSystems.Values)
			{
				particleSystem.Stop();
			}
			string activeSystem = stateMap.FirstOrDefault((KeyValuePair<Func<int, bool>, string> kv) => kv.Key(count)).Value;
			ParticleSystem systemToPlay;
			if (activeSystem != null && particleSystems.TryGetValue(activeSystem, out systemToPlay))
			{
				systemToPlay.Play();
			}
			grave.GetElement<Renderer>("Material01").material.SetFloat("_GraveCardExist", (float)((count > 0) ? 1 : 0));
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x001714F8 File Offset: 0x0016F6F8
		private void RefreshExcludeState(ElementObjectManager exclude, int count)
		{
			Dictionary<Func<int, bool>, string> dictionary = new Dictionary<Func<int, bool>, string>();
			dictionary.Add((int c) => c >= 20, "ExcludeIdleS3");
			dictionary.Add((int c) => c >= 10, "ExcludeIdleS2");
			dictionary.Add((int c) => c > 0, "ExcludeIdleS1");
			Dictionary<Func<int, bool>, string> stateMap = dictionary;
			Dictionary<string, ParticleSystem> particleSystems = new string[] { "ExcludeIdleS1", "ExcludeIdleS2", "ExcludeIdleS3" }.ToDictionary((string name) => name, new Func<string, ParticleSystem>(exclude.GetElement<ParticleSystem>));
			foreach (ParticleSystem particleSystem in particleSystems.Values)
			{
				particleSystem.Stop();
			}
			string activeSystem = stateMap.FirstOrDefault((KeyValuePair<Func<int, bool>, string> kv) => kv.Key(count)).Value;
			ParticleSystem systemToPlay;
			if (activeSystem != null && particleSystems.TryGetValue(activeSystem, out systemToPlay))
			{
				systemToPlay.Play();
			}
			exclude.GetElement<Renderer>("Material01").material.SetFloat("_ExcludeCardExist", (float)((count > 0) ? 1 : 0));
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x00171684 File Offset: 0x0016F884
		public void UpdateBgEffects(int player, bool first = false)
		{
			if (this.myDeck == null || this.myExtra == null || this.opDeck == null || this.opExtra == null || this.grave0Manager == null || this.grave1Manager == null)
			{
				return;
			}
			if (player == 0)
			{
				this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.PhaseToDamagePhaseAll, "");
				if (this.mate0 != null && !first)
				{
					this.mate0.Play(Mate.MateAction.GetDamage);
				}
				if (this.bgPhase0 == 1 && (float)OcgCore.life0 < (float)OcgCore.lpLimit * 0.75f)
				{
					this.bgPhase0++;
					string seLabel = "SE_FIELD_MAT" + this.field0Manager.name.Substring(4, 3) + "_PHASE1_P";
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, seLabel);
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase1ToPhase2, "");
					this.grave0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
					if (this.stand0Manager != null)
					{
						this.stand0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
					}
				}
				if (this.bgPhase0 == 2 && (float)OcgCore.life0 < (float)OcgCore.lpLimit * 0.5f)
				{
					this.bgPhase0++;
					string seLabel2 = "SE_FIELD_MAT" + this.field0Manager.name.Substring(4, 3) + "_PHASE2_P";
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, seLabel2);
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase2ToPhase3, "");
					this.grave0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
					if (this.stand0Manager != null)
					{
						this.stand0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
					}
				}
				if (this.bgPhase0 == 3 && (float)OcgCore.life0 < (float)OcgCore.lpLimit * 0.25f)
				{
					this.bgPhase0++;
					string seLabel3 = "SE_FIELD_MAT" + this.field0Manager.name.Substring(4, 3) + "_PHASE3_P";
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, seLabel3);
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase3ToPhase4, "");
					this.grave0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
					if (this.stand0Manager != null)
					{
						this.stand0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
					}
					AudioManager.PlayBgmClimax();
				}
				if (this.bgPhase0 == 4)
				{
					int life = OcgCore.life0;
					return;
				}
			}
			else
			{
				this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.PhaseToDamagePhaseAll, "");
				if (this.mate1 != null && !first)
				{
					this.mate1.Play(Mate.MateAction.GetDamage);
				}
				if (this.bgPhase1 == 1 && (float)OcgCore.life1 < (float)OcgCore.lpLimit * 0.75f)
				{
					this.bgPhase1++;
					string seLabel4 = "SE_FIELD_MAT" + this.field1Manager.name.Substring(4, 3) + "_PHASE1_P";
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, seLabel4);
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase1ToPhase2, "");
					this.grave1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
					if (this.stand1Manager != null)
					{
						this.stand1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
					}
				}
				if (this.bgPhase1 == 2 && (float)OcgCore.life1 < (float)OcgCore.lpLimit * 0.5f)
				{
					this.bgPhase1++;
					string seLabel5 = "SE_FIELD_MAT" + this.field1Manager.name.Substring(4, 3) + "_PHASE2_P";
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, seLabel5);
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase2ToPhase3, "");
					this.grave1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
					if (this.stand1Manager != null)
					{
						this.stand1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
					}
				}
				if (this.bgPhase1 == 3 && (float)OcgCore.life1 < (float)OcgCore.lpLimit * 0.25f)
				{
					this.bgPhase1++;
					string seLabel6 = "SE_FIELD_MAT" + this.field1Manager.name.Substring(4, 3) + "_PHASE3_P";
					this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, seLabel6);
					this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.OtherSideDamagePhase3ToPhase4, "");
					this.grave1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
					if (this.stand1Manager != null)
					{
						this.stand1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
					}
					AudioManager.PlayBgmClimax();
				}
				if (this.bgPhase1 == 4)
				{
					int life2 = OcgCore.life1;
				}
			}
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x00171B48 File Offset: 0x0016FD48
		public void PlayGraveEffect(GPS p, bool isIn)
		{
			if (this.grave0Manager == null || this.grave1Manager == null)
			{
				return;
			}
			ElementObjectManager manager;
			if (p.InMyControl())
			{
				manager = this.grave0Manager;
			}
			else
			{
				manager = this.grave1Manager;
			}
			if (manager == null)
			{
				return;
			}
			string audio = string.Empty;
			BgEffectSetting effect;
			BgEffectSetting effectEnd;
			if ((p.location & 16U) > 0U)
			{
				if (isIn)
				{
					effect = manager.GetElement<BgEffectSetting>("GraveIn");
					effectEnd = manager.GetElement<BgEffectSetting>("GraveInend");
					audio = "SE_CEMETARY_ABSORB";
				}
				else
				{
					effect = manager.GetElement<BgEffectSetting>("GraveOut");
					effectEnd = manager.GetElement<BgEffectSetting>("GraveOutend");
					audio = "SE_CEMETARY_GOOUT";
				}
			}
			else if (isIn)
			{
				effect = manager.GetElement<BgEffectSetting>("ExcludeIn");
				effectEnd = manager.GetElement<BgEffectSetting>("ExcludeInend");
				audio = "SE_EXCLUSION_ABSORB";
			}
			else
			{
				effect = manager.GetElement<BgEffectSetting>("ExcludeOut");
				effectEnd = manager.GetElement<BgEffectSetting>("ExcludeOutend");
				audio = "SE_EXCLUSION_GOOUT";
			}
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, effect.delay).OnComplete(delegate
			{
				effect.particle.Play();
				AudioManager.PlaySE(audio, 1f);
			});
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, effectEnd.delay).OnComplete(delegate
			{
				effectEnd.particle.Play();
			});
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x00171D0C File Offset: 0x0016FF0C
		public void ShowBgHint()
		{
			if (this.myDeck == null || this.myExtra == null || this.opDeck == null || this.opExtra == null || this.grave0Manager == null || this.grave1Manager == null)
			{
				return;
			}
			bool haveHint = false;
			IEnumerable<GameCard> cardsActivated = OcgCore.cards.Where((GameCard c) => c.buttons.Count > 0);
			foreach (GameCard card in cardsActivated)
			{
				if (card.p.InLocation(CardLocation.Grave) && card.p.InMyControl())
				{
					GameObject gameObject = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_grave_001");
					gameObject.transform.SetParent(this.grave0Manager.GetElement<Transform>("GraveHighlightNear"), false);
					global::UnityEngine.Object.Destroy(gameObject, 3f);
					this.grave0Manager.GetElement<Animator>("GraveHighlightNear").SetBool("On", true);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card2 in cardsActivated)
			{
				if (card2.p.InLocation(CardLocation.Grave) && !card2.p.InMyControl())
				{
					GameObject gameObject2 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_grave_001");
					gameObject2.transform.SetParent(this.grave1Manager.GetElement<Transform>("GraveHighlightFar"), false);
					global::UnityEngine.Object.Destroy(gameObject2, 3f);
					this.grave1Manager.GetElement<Animator>("GraveHighlightFar").SetBool("On", true);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card3 in cardsActivated)
			{
				if (card3.p.InLocation(CardLocation.Removed) && card3.p.InMyControl())
				{
					GameObject gameObject3 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_exclude_001");
					gameObject3.transform.SetParent(this.grave0Manager.GetElement<Transform>("ExcludeHighlightNear"), false);
					global::UnityEngine.Object.Destroy(gameObject3, 3f);
					this.grave0Manager.GetElement<Animator>("ExcludeHighlightNear").SetBool("On", true);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card4 in cardsActivated)
			{
				if (card4.p.InLocation(CardLocation.Removed) && !card4.p.InMyControl())
				{
					GameObject gameObject4 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_exclude_001");
					gameObject4.transform.SetParent(this.grave1Manager.GetElement<Transform>("ExcludeHighlightFar"), false);
					global::UnityEngine.Object.Destroy(gameObject4, 3f);
					this.grave1Manager.GetElement<Animator>("ExcludeHighlightFar").SetBool("On", true);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card5 in cardsActivated)
			{
				if ((card5.p.location & 64U) > 0U && card5.p.InMyControl())
				{
					GameObject effect = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_Exdeck_001");
					effect.transform.SetParent(this.myExtra.transform, false);
					effect.transform.position = Tools.GetDeckModelTopPosition(this.myExtra);
					foreach (PlaceSelector placeSelector in this.places)
					{
						placeSelector.ShowHint(64U, 0U);
					}
					global::UnityEngine.Object.Destroy(effect, 3f);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card6 in cardsActivated)
			{
				if ((card6.p.location & 64U) > 0U && !card6.p.InMyControl())
				{
					GameObject effect2 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_Exdeck_001");
					effect2.transform.SetParent(this.opExtra.transform, false);
					effect2.transform.position = Tools.GetDeckModelTopPosition(this.opExtra);
					foreach (PlaceSelector placeSelector2 in this.places)
					{
						placeSelector2.ShowHint(64U, 1U);
					}
					global::UnityEngine.Object.Destroy(effect2, 3f);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card7 in cardsActivated)
			{
				if ((card7.p.location & 1U) > 0U && card7.p.controller == 0U)
				{
					GameObject effect3 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_Exdeck_001");
					effect3.transform.SetParent(this.myDeck.transform, false);
					effect3.transform.position = Tools.GetDeckModelTopPosition(this.myDeck);
					foreach (PlaceSelector placeSelector3 in this.places)
					{
						placeSelector3.ShowHint(1U, 0U);
					}
					global::UnityEngine.Object.Destroy(effect3, 3f);
					haveHint = true;
					break;
				}
			}
			foreach (GameCard card8 in cardsActivated)
			{
				if ((card8.p.location & 1U) > 0U && !card8.p.InMyControl())
				{
					GameObject effect4 = ABLoader.LoadMasterDuelGameObject("fxp_HL_active_Exdeck_001");
					effect4.transform.SetParent(this.opDeck.transform, false);
					effect4.transform.position = Tools.GetDeckModelTopPosition(this.opDeck);
					foreach (PlaceSelector placeSelector4 in this.places)
					{
						placeSelector4.ShowHint(1U, 1U);
					}
					global::UnityEngine.Object.Destroy(effect4, 3f);
					haveHint = true;
					break;
				}
			}
			if (haveHint)
			{
				AudioManager.PlaySE("SE_DUEL_ACTIVE_POSSIBLE", 1f);
			}
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x001723D0 File Offset: 0x001705D0
		public void ClearResponse()
		{
			int myMaxDeck = this.Core.GetLocationCardCount(CardLocation.Deck, 0U);
			int opMaxDeck = this.Core.GetLocationCardCount(CardLocation.Deck, 1U);
			foreach (GameCard card in OcgCore.cards)
			{
				card.effects.Clear();
				card.ClearButtons();
				if (card.forSelect)
				{
					card.forSelect = false;
					if (card.p.InLocation(CardLocation.Deck))
					{
						if (OcgCore.deckReserved)
						{
							if (card.p.controller == 0U && (ulong)card.p.sequence != (ulong)((long)(myMaxDeck - 1)))
							{
								card.EraseData();
							}
							if (card.p.controller == 1U && (ulong)card.p.sequence != (ulong)((long)(opMaxDeck - 1)))
							{
								card.EraseData();
							}
						}
						else
						{
							card.EraseData();
						}
					}
				}
			}
			foreach (PlaceSelector placeSelector in this.places)
			{
				placeSelector.StopResponse();
				placeSelector.HideHint();
				placeSelector.ClearButtons();
			}
			foreach (GraveBehaviour graveBehaviour in this.graves)
			{
				graveBehaviour.ClearGraveButtons();
				graveBehaviour.ClearExcludeButtons();
			}
			PhaseButtonHandler.battlePhase = false;
			PhaseButtonHandler.main2Phase = false;
			PhaseButtonHandler.endPhase = false;
			PhaseButtonHandler.CloseHint();
			this.CloseBgHint();
			this.FieldSelectReset();
			OcgCore.ES_selectHint = string.Empty;
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x00172584 File Offset: 0x00170784
		private void CloseBgHint()
		{
			if (this.grave0Manager == null || this.grave1Manager == null)
			{
				return;
			}
			this.grave0Manager.GetElement<Animator>("GraveHighlightNear").SetBool("On", false);
			this.grave0Manager.GetElement<Animator>("ExcludeHighlightNear").SetBool("On", false);
			this.grave1Manager.GetElement<Animator>("GraveHighlightFar").SetBool("On", false);
			this.grave1Manager.GetElement<Animator>("ExcludeHighlightFar").SetBool("On", false);
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x0017261C File Offset: 0x0017081C
		private void FieldSelectReset()
		{
			foreach (PlaceSelector placeSelector in this.places)
			{
				placeSelector.StopResponse();
			}
			OcgCore.btnConfirm.Hide();
			OcgCore.btnCancel.Hide();
			this.Core.GetUI<OcgCoreUI>().CloseHint();
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x00172690 File Offset: 0x00170890
		public void Dispose()
		{
			foreach (GameObject gameObject in this.turnEndDeleteObjects)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.turnEndDeleteObjects.Clear();
			foreach (GameObject gameObject2 in this.allGameObjects)
			{
				global::UnityEngine.Object.Destroy(gameObject2);
			}
			this.allGameObjects.Clear();
		}

		// Token: 0x06009A43 RID: 39491 RVA: 0x00172738 File Offset: 0x00170938
		public async UniTask ShowDuelResultText(string text)
		{
			GameObject go = ABLoader.LoadMasterDuelGameObject(text);
			this.allGameObjects.Add(go);
			await go.GetComponent<PlayableDirector>().WaitAsync(true, default(CancellationToken));
			global::UnityEngine.Object.Destroy(go);
		}

		// Token: 0x06009A44 RID: 39492 RVA: 0x00172783 File Offset: 0x00170983
		public void DuelEndEvent()
		{
			if (this.timerHandler != null)
			{
				this.timerHandler.DuelEnd();
			}
			if (this.playableGuide != null)
			{
				this.playableGuide.End();
			}
		}

		// Token: 0x06009A45 RID: 39493 RVA: 0x001727B8 File Offset: 0x001709B8
		public async UniTask PlayCommonSpecialWin(int[] codes)
		{
			int count = codes.Length;
			GameObject gameObject = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/SpecialWin/SpecialWinCommonCard0" + count.ToString(), false, true, null);
			GameObject go = gameObject;
			this.allGameObjects.Add(go);
			ElementObjectManager mner = go.GetComponent<ElementObjectManager>();
			foreach (Transform child in mner.transform.GetComponentsInChildren<Transform>(true))
			{
				if (child.name == "White")
				{
					child.gameObject.SetActive(false);
				}
			}
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard01"), codes[0], 0U, true, null, null);
			mner.GetElement<ElementObjectManager>("DummyCard01").GetElement<Renderer>("DummyCardModel_front").material.renderQueue = 4000;
			if (count > 1)
			{
				Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard02"), codes[1], 0U, true, null, null);
			}
			if (count > 2)
			{
				Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard03"), codes[2], 0U, true, null, null);
			}
			if (count > 3)
			{
				Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard04"), codes[3], 0U, true, null, null);
			}
			if (count > 4)
			{
				Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard05"), codes[4], 0U, true, null, null);
			}
			PlayableDirector component = mner.GetComponent<PlayableDirector>();
			component.Play();
			await component.WaitAsync(true, default(CancellationToken));
			global::UnityEngine.Object.Destroy(go);
		}

		// Token: 0x06009A46 RID: 39494 RVA: 0x00172804 File Offset: 0x00170A04
		public async UniTask PlaySpecialWin(string path, Action<ElementObjectManager> action = null)
		{
			GameObject gameObject = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/SpecialWin/" + path, false, true, null);
			GameObject go = gameObject;
			this.allGameObjects.Add(go);
			ElementObjectManager manager = go.GetComponent<ElementObjectManager>();
			if (action != null)
			{
				action(manager);
			}
			PlayableDirector component = go.GetComponent<PlayableDirector>();
			component.Play();
			await component.WaitAsync(true, default(CancellationToken));
			global::UnityEngine.Object.Destroy(go);
		}

		// Token: 0x06009A47 RID: 39495 RVA: 0x00172857 File Offset: 0x00170A57
		public void ShowBGEnd(OcgCore.DuelResult result)
		{
			if (result == OcgCore.DuelResult.Win)
			{
				this.<ShowBGEnd>g__HeroWin|66_0();
				this.<ShowBGEnd>g__RivalLose|66_3();
				return;
			}
			if (result == OcgCore.DuelResult.Lose)
			{
				this.<ShowBGEnd>g__HeroLose|66_1();
				this.<ShowBGEnd>g__RivalWin|66_2();
				return;
			}
			if (result == OcgCore.DuelResult.Draw)
			{
				this.<ShowBGEnd>g__HeroLose|66_1();
				this.<ShowBGEnd>g__RivalLose|66_3();
			}
		}

		// Token: 0x06009A48 RID: 39496 RVA: 0x0017288C File Offset: 0x00170A8C
		public async UniTask ShowChainStack()
		{
			int chain = OcgCore.cardsInChain.Count;
			if (chain != 1)
			{
				if (this.NeedChainAnimation())
				{
					GameObject animation;
					if (chain < 3)
					{
						animation = ABLoader.LoadMasterDuelGameObject("DuelChainStack01");
					}
					else
					{
						animation = ABLoader.LoadMasterDuelGameObject("DuelChainStack02");
						DOTween.To(delegate(float v)
						{
						}, 0f, 0f, 0.0166f).OnComplete(delegate
						{
							AudioManager.PlaySE("SE_DUELCHAIN_STACK02", 1f);
						});
						DOTween.To(delegate(float v)
						{
						}, 0f, 0f, 0.767f).OnComplete(delegate
						{
							if (chain == 3)
							{
								AudioManager.PlaySE("SE_DUEL_CHAIN_NUMEFF_01", 1f);
								return;
							}
							if (chain == 4)
							{
								AudioManager.PlaySE("SE_DUEL_CHAIN_NUMEFF_02", 1f);
								return;
							}
							AudioManager.PlaySE("SE_DUEL_CHAIN_NUMEFF_03", 1f);
						});
					}
					PlayableDirector director = animation.GetComponent<PlayableDirector>();
					ElementObjectManager manager = animation.GetComponent<ElementObjectManager>();
					if (chain >= 4)
					{
						director.GetTrackAsset("LCardLightSetScaleC03").muted = true;
						director.GetTrackAsset("LCardLightSetScaleC04").muted = false;
						director.GetTrackAsset("RCardLightSetScaleC03").muted = true;
						director.GetTrackAsset("RCardLightSetScaleC04").muted = false;
					}
					ElementObjectManager targetCardD;
					if (OcgCore.controllerInChain[chain - 1] == 0U)
					{
						targetCardD = manager.GetElement<ElementObjectManager>("DummyChainCardDL");
						manager.GetElement("ChainCardSetDROffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumDL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumDL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumDL_Tens"), chain);
					}
					else
					{
						targetCardD = manager.GetElement<ElementObjectManager>("DummyChainCardDR");
						manager.GetElement("ChainCardSetDLOffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumDR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumDR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumDR_Tens"), chain);
					}
					Program.instance.texture_.LoadDummyCard(targetCardD, OcgCore.codesInChain[chain - 1], 0U, true, null, null);
					if (OcgCore.controllerInChain[chain - 1] == OcgCore.controllerInChain[chain - 2])
					{
						manager.GetElement("ChainStraightCLtoDR").SetActive(false);
						manager.GetElement("ChainStraightCRtoDL").SetActive(false);
					}
					else if (OcgCore.controllerInChain[chain - 1] == 0U)
					{
						manager.GetElement("ChainStraightCLtoDR").SetActive(false);
					}
					else
					{
						manager.GetElement("ChainStraightCRtoDL").SetActive(false);
					}
					ElementObjectManager targetCardC;
					if (OcgCore.controllerInChain[chain - 2] == 0U)
					{
						targetCardC = manager.GetElement<ElementObjectManager>("DummyChainCardCL");
						manager.GetElement("ChainCardSetCROffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumCL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumCL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumCL_Tens"), chain - 1);
					}
					else
					{
						targetCardC = manager.GetElement<ElementObjectManager>("DummyChainCardCR");
						manager.GetElement("ChainCardSetCLOffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumCR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumCR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumCR_Tens"), chain - 1);
					}
					Program.instance.texture_.LoadDummyCard(targetCardC, OcgCore.codesInChain[chain - 2], 0U, true, null, null);
					if (chain > 2)
					{
						if (OcgCore.controllerInChain[chain - 2] == OcgCore.controllerInChain[chain - 3])
						{
							manager.GetElement("ChainStraightBLtoCR").SetActive(false);
							manager.GetElement("ChainStraightBRtoCL").SetActive(false);
						}
						else if (OcgCore.controllerInChain[chain - 2] == 0U)
						{
							manager.GetElement("ChainStraightBLtoCR").SetActive(false);
						}
						else
						{
							manager.GetElement("ChainStraightBRtoCL").SetActive(false);
						}
						ElementObjectManager targetCardB;
						if (OcgCore.controllerInChain[chain - 3] == 0U)
						{
							targetCardB = manager.GetElement<ElementObjectManager>("DummyChainCardBL");
							manager.GetElement("ChainCardSetBROffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumBL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumBL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumBL_Tens"), chain - 2);
						}
						else
						{
							targetCardB = manager.GetElement<ElementObjectManager>("DummyChainCardBR");
							manager.GetElement("ChainCardSetBLOffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumBR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumBR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumBR_Tens"), chain - 2);
						}
						Program.instance.texture_.LoadDummyCard(targetCardB, OcgCore.codesInChain[chain - 3], 0U, true, null, null);
						if (chain > 3)
						{
							if (OcgCore.controllerInChain[chain - 3] == OcgCore.controllerInChain[chain - 4])
							{
								manager.GetElement("ChainStraightALtoBR").SetActive(false);
								manager.GetElement("ChainStraightARtoBL").SetActive(false);
							}
							else if (OcgCore.controllerInChain[chain - 3] == 0U)
							{
								manager.GetElement("ChainStraightALtoBR").SetActive(false);
							}
							else
							{
								manager.GetElement("ChainStraightARtoBL").SetActive(false);
							}
							ElementObjectManager targetCardA;
							if (OcgCore.controllerInChain[chain - 4] == 0U)
							{
								targetCardA = manager.GetElement<ElementObjectManager>("DummyChainCardAL");
								manager.GetElement("ChainCardSetAROffset").SetActive(false);
								this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumAL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumAL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumAL_Tens"), chain - 3);
							}
							else
							{
								targetCardA = manager.GetElement<ElementObjectManager>("DummyChainCardAR");
								manager.GetElement("ChainCardSetALOffset").SetActive(false);
								this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumAR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumAR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumAR_Tens"), chain - 3);
							}
							Program.instance.texture_.LoadDummyCard(targetCardA, OcgCore.codesInChain[chain - 4], 0U, true, null, null);
						}
						else
						{
							manager.GetElement("ChainStraightALtoBR").SetActive(false);
							manager.GetElement("ChainStraightARtoBL").SetActive(false);
							manager.GetElement("ChainCardSetALOffset").SetActive(false);
							manager.GetElement("ChainCardSetAROffset").SetActive(false);
						}
					}
					await director.WaitAsync(true, default(CancellationToken));
					global::UnityEngine.Object.Destroy(animation);
				}
			}
		}

		// Token: 0x06009A49 RID: 39497 RVA: 0x001728D0 File Offset: 0x00170AD0
		public async UniTask ShowChainResolve()
		{
			int chain = OcgCore.chainSolvingIndex;
			if (OcgCore.cardsInChain.Count != 1)
			{
				if (this.NeedChainAnimation())
				{
					GameObject animation;
					if (chain == 1)
					{
						animation = ABLoader.LoadMasterDuelGameObject("DuelChainResolve01");
					}
					else if (chain == 2)
					{
						animation = ABLoader.LoadMasterDuelGameObject("DuelChainResolve02");
					}
					else
					{
						animation = ABLoader.LoadMasterDuelGameObject("DuelChainResolve03");
					}
					PlayableDirector component = animation.GetComponent<PlayableDirector>();
					ElementObjectManager manager = animation.GetComponent<ElementObjectManager>();
					ElementObjectManager targetCardD;
					if (OcgCore.controllerInChain[chain - 1] == 0U)
					{
						targetCardD = manager.GetElement<ElementObjectManager>("DummyChainCardDL");
						manager.GetElement("ChainCardSetDROffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumDL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumDL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumDL_Tens"), chain);
					}
					else
					{
						targetCardD = manager.GetElement<ElementObjectManager>("DummyChainCardDR");
						manager.GetElement("ChainCardSetDLOffset").SetActive(false);
						this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumDR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumDR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumDR_Tens"), chain);
					}
					Program.instance.texture_.LoadDummyCard(targetCardD, OcgCore.codesInChain[chain - 1], 0U, true, null, null);
					if (chain > 1)
					{
						if (chain != OcgCore.cardsInChain.Count)
						{
							manager.GetComponent<PlayableDirector>().time = 0.8299999833106995;
							manager.GetElement("ResolveTextSet").SetActive(false);
						}
						if (OcgCore.controllerInChain[chain - 1] == OcgCore.controllerInChain[chain - 2])
						{
							manager.GetElement("ChainStraightCLtoDR").SetActive(false);
							manager.GetElement("ChainStraightCRtoDL").SetActive(false);
						}
						else if (OcgCore.controllerInChain[chain - 1] == 0U)
						{
							manager.GetElement("ChainStraightCLtoDR").SetActive(false);
						}
						else
						{
							manager.GetElement("ChainStraightCRtoDL").SetActive(false);
						}
						ElementObjectManager targetCardC;
						if (OcgCore.controllerInChain[chain - 2] == 0U)
						{
							targetCardC = manager.GetElement<ElementObjectManager>("DummyChainCardCL");
							manager.GetElement("ChainCardSetCROffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumCL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumCL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumCL_Tens"), chain - 1);
						}
						else
						{
							targetCardC = manager.GetElement<ElementObjectManager>("DummyChainCardCR");
							manager.GetElement("ChainCardSetCLOffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumCR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumCR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumCR_Tens"), chain - 1);
						}
						Program.instance.texture_.LoadDummyCard(targetCardC, OcgCore.codesInChain[chain - 2], 0U, true, null, null);
					}
					if (chain > 2)
					{
						if (OcgCore.controllerInChain[chain - 2] == OcgCore.controllerInChain[chain - 3])
						{
							manager.GetElement("ChainStraightBLtoCR").SetActive(false);
							manager.GetElement("ChainStraightBRtoCL").SetActive(false);
						}
						else if (OcgCore.controllerInChain[chain - 2] == 0U)
						{
							manager.GetElement("ChainStraightBLtoCR").SetActive(false);
						}
						else
						{
							manager.GetElement("ChainStraightBRtoCL").SetActive(false);
						}
						ElementObjectManager targetCardB;
						if (OcgCore.controllerInChain[chain - 3] == 0U)
						{
							targetCardB = manager.GetElement<ElementObjectManager>("DummyChainCardBL");
							manager.GetElement("ChainCardSetBROffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumBL_Digit"), manager.GetElement<SpriteRenderer>("ChainNumBL_Ones"), manager.GetElement<SpriteRenderer>("ChainNumBL_Tens"), chain - 2);
						}
						else
						{
							targetCardB = manager.GetElement<ElementObjectManager>("DummyChainCardBR");
							manager.GetElement("ChainCardSetBLOffset").SetActive(false);
							this.ChangeChainNumber(manager.GetElement<SpriteRenderer>("ChainNumBR_Digit"), manager.GetElement<SpriteRenderer>("ChainNumBR_Ones"), manager.GetElement<SpriteRenderer>("ChainNumBR_Tens"), chain - 2);
						}
						Program.instance.texture_.LoadDummyCard(targetCardB, OcgCore.codesInChain[chain - 3], 0U, true, null, null);
					}
					await component.WaitAsync(true, default(CancellationToken));
					global::UnityEngine.Object.Destroy(animation);
				}
			}
		}

		// Token: 0x06009A4A RID: 39498 RVA: 0x00172914 File Offset: 0x00170B14
		public async UniTask ShowCardEffectAnimation()
		{
			DuelBGManager.<>c__DisplayClass69_0 CS$<>8__locals1 = new DuelBGManager.<>c__DisplayClass69_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.card = OcgCore.cardsInChain[OcgCore.chainSolvingIndex - 1];
			CS$<>8__locals1.card.ResolveChain(OcgCore.chainSolvingIndex);
			if (CS$<>8__locals1.card.GetData().Id == OcgCore.codesInChain[OcgCore.chainSolvingIndex - 1])
			{
				if (!OcgCore.negatedInChain.Contains(OcgCore.chainSolvingIndex) && !CS$<>8__locals1.card.disabledInChain)
				{
					if (OcgCore.negatedInChain.Contains(OcgCore.chainSolvingIndex) || this.Core.CurrentChainDisabled(OcgCore.chainSolvingIndex) || CS$<>8__locals1.card.negated || CS$<>8__locals1.card.Disabled)
					{
						CS$<>8__locals1.card.disabledInChain = true;
						await CS$<>8__locals1.card.AnimationNegate().WaitAsync(default(CancellationToken));
					}
					else if (OcgCore.condition != OcgCore.Condition.Duel || Config.GetBool("DuelEffect", true))
					{
						if (OcgCore.condition != OcgCore.Condition.Watch || Config.GetBool("WatchEffect", true))
						{
							if (OcgCore.condition != OcgCore.Condition.Replay || Config.GetBool("ReplayEffect", true))
							{
								CS$<>8__locals1.code = CS$<>8__locals1.card.GetData().GetOriginalID();
								if (CS$<>8__locals1.card.GetData().Id == 83764719)
								{
									CS$<>8__locals1.code = 83764719;
								}
								if (CS$<>8__locals1.card.GetData().Id == 63166096)
								{
									CS$<>8__locals1.code = 63166096;
								}
								if (CS$<>8__locals1.card.GetData().Id == 32807848)
								{
									CS$<>8__locals1.code = 32807848;
								}
								if (CS$<>8__locals1.card.GetData().Id == 49238329)
								{
									CS$<>8__locals1.code = 49238329;
								}
								if (CS$<>8__locals1.card.GetData().Id == 24224831)
								{
									CS$<>8__locals1.code = 24224831;
								}
								string targetFolder = Program.root + "MasterDuel/Card/" + CS$<>8__locals1.code.ToString();
								targetFolder = Path.Combine(Application.dataPath, targetFolder);
								if (Directory.Exists(targetFolder))
								{
									DuelBGManager.<>c__DisplayClass69_1 CS$<>8__locals2 = new DuelBGManager.<>c__DisplayClass69_1();
									CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
									if (!DuelBGManager.cardEffectCodes.Contains(CS$<>8__locals2.CS$<>8__locals1.code))
									{
										List<GameObject> prefabs = await ABLoader.LoadsFromFolderAsync<PlayableDirector>("MasterDuel/Card/" + CS$<>8__locals2.CS$<>8__locals1.code.ToString());
										if (CS$<>8__locals2.CS$<>8__locals1.code == 83764718)
										{
											prefabs[0].name = "Ef83764718";
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 83764719)
										{
											prefabs[0].name = "Ef83764719";
										}
										DuelBGManager.cardEffects.AddRange(prefabs);
										DuelBGManager.cardEffectCodes.Add(CS$<>8__locals2.CS$<>8__locals1.code);
									}
									CS$<>8__locals2.effect = null;
									if (CS$<>8__locals2.CS$<>8__locals1.code == 5318639)
									{
										if (CS$<>8__locals2.CS$<>8__locals1.card.effectTargets.Count <= 0 || !(CS$<>8__locals2.CS$<>8__locals1.card.effectTargets[0].model != null))
										{
											return;
										}
										AudioManager.PlaySE("SE_EV_CYCLONE", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef04909");
										CS$<>8__locals2.effect.transform.position = CS$<>8__locals2.CS$<>8__locals1.card.effectTargets[0].model.transform.position;
										if (CS$<>8__locals2.CS$<>8__locals1.card.p.controller != 0U)
										{
											CS$<>8__locals2.effect.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
										}
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 2263869)
									{
										if (CS$<>8__locals2.CS$<>8__locals1.card.effectTargets.Count <= 0 || !(CS$<>8__locals2.CS$<>8__locals1.card.effectTargets[0].model != null))
										{
											return;
										}
										AudioManager.PlaySE("SE_EV_ULTIMATE_SLAYER", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef17469");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 12580477)
									{
										AudioManager.PlaySE("SE_EV_RAIGEKI", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef04343_Far" : "Ef04343_Near");
										DOTween.To(delegate(float v)
										{
										}, 0f, 0f, 0.4f).OnComplete(delegate
										{
											CameraManager.ShakeCamera(true);
										});
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 14558127)
									{
										if (OcgCore.chainSolvingIndex <= 1)
										{
											return;
										}
										AudioManager.PlaySE("SE_EV_ASH_BLOSSOM_v2", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef03891");
										CS$<>8__locals2.effect.transform.localPosition = GameCard.GetCardPosition(OcgCore.cardsInChain[OcgCore.chainSolvingIndex - 2].p, null, null);
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 18144506)
									{
										AudioManager.PlaySE("SE_EV_HARPIESFEATHER_DUSTER_3D", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef04678" : "Ef04678Op");
										CS$<>8__locals2.effect.transform.DestroyChildrenByName("DistPlane");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 23002292)
									{
										AudioManager.PlaySE("SE_EV_REDREBOOT", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef13622");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 24224830)
									{
										AudioManager.PlaySE("SE_EV_CALLED_GRAVE", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef13619" : "Ef13619Op");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 24224831)
									{
										AudioManager.PlaySE(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "SE_EV_EF21233_R" : "SE_EV_EF21233_P", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? (Tools.IsAspectRatioWidescreen() ? "Ef21233_Op" : "Ef21233_Op_4x3") : "Ef21233");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 24299458)
									{
										AudioManager.PlaySE("SE_EV_FORBIDDEN_DROPLET", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef15299_Far" : "Ef15299_Near");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 25311006)
									{
										AudioManager.PlaySE("SE_EV_TRIPLETACTICS_TALENT", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef15296");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 41420027)
									{
										AudioManager.PlaySE("SE_EV_SOLEMNJUDGMENT", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef04861");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 44095762)
									{
										AudioManager.PlaySE("SE_EV_MIRRORFORCE", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef04887" : "Ef04887Op");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 53129443)
									{
										AudioManager.PlaySE("SE_EV_BLACKHOLE", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef04342");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 54693926)
									{
										AudioManager.PlaySE("SE_EV_DARKRULER_NOMORE", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef14742" : "Ef14742Op");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 61740673)
									{
										AudioManager.PlaySE("SE_EV_IMPERIAL_ORDER", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef04960");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 62279055)
									{
										AudioManager.PlaySE("SE_EV_MAGIC_CYLINDER", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef05124_near" : "Ef05124_far");
										ScreenEffect screenEffect = CS$<>8__locals2.effect.AddComponent<ScreenEffect>();
										screenEffect.cameraViewType = ScreenEffect.ViewType.View3D;
										screenEffect.useMainCameraSetting = true;
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 63391643)
									{
										if (CS$<>8__locals2.CS$<>8__locals1.card.effectTargets.Count <= 0 || !(CS$<>8__locals2.CS$<>8__locals1.card.effectTargets[0].model != null))
										{
											return;
										}
										AudioManager.PlaySE("SE_EV_THOUSANDKNIVES", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef05166");
										CS$<>8__locals2.effect.transform.position = CS$<>8__locals2.CS$<>8__locals1.card.effectTargets[0].model.transform.position;
										if (!CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl())
										{
											CS$<>8__locals2.effect.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
										}
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 65681983)
									{
										AudioManager.PlaySE("SE_EV_CROSSOUT_DESIGNATOR", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef14627_Far" : "Ef14627_Near");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 72302403)
									{
										AudioManager.PlaySE("SE_EV_GOFUKEN", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Ef04354" : "Ef04354Op");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 75500286)
									{
										AudioManager.PlaySE("SE_EV_GOLD_SARCOPHAGUS", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef06161");
									}
									else if (CS$<>8__locals2.CS$<>8__locals1.code == 83764718 || CS$<>8__locals2.CS$<>8__locals1.code == 83764719)
									{
										AudioManager.PlaySE("SE_EV_MONSTER_REBORN", 1f);
										CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef" + CS$<>8__locals2.CS$<>8__locals1.code.ToString());
									}
									else
									{
										if (CS$<>8__locals2.CS$<>8__locals1.code == 63166095 || CS$<>8__locals2.CS$<>8__locals1.code == 63166096)
										{
											OcgCore.nextMoveActionDuration = 7.2f;
											OcgCore.nextMoveNeedCode = true;
											OcgCore.nextMoveAction = delegate(int code2)
											{
												CS$<>8__locals2.effect = CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetCardEffectPrefab((CS$<>8__locals2.CS$<>8__locals1.code == 63166095) ? "Ef13671" : "Ef03434");
												Renderer renderer = (OcgCore.nextMoveManager = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>()).GetElement<Renderer>("SummonPosDummy");
												Program.instance.texture_.LoadCardToRendererWithMaterialAsync(renderer, code2, true);
												global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect, 2.2f);
											};
											OcgCore.nextEventAction = delegate
											{
												if (OcgCore.nextMoveManager == null)
												{
													return;
												}
												Transform target = OcgCore.nextMoveManager.GetElement<Transform>("DummyCard01");
												GameCard lastMoveCard = OcgCore.lastMoveCard;
												lastMoveCard.model.SetActive(true);
												lastMoveCard.ResetModelRotation();
												lastMoveCard.model.transform.position = target.position;
												lastMoveCard.model.transform.eulerAngles = new Vector3(-target.eulerAngles.x, 0f, 0f);
												OcgCore.nextMoveAction = null;
												OcgCore.nextEventAction = null;
												OcgCore.nextMoveManager = null;
												lastMoveCard.MoveAsync(lastMoveCard.p, false, 0f, 0.7f).ContinueWith(() => OcgCore.NoMoreWait = true);
											};
											return;
										}
										if (CS$<>8__locals2.CS$<>8__locals1.code == 19613556)
										{
											AudioManager.PlaySE("SE_EV_HEAVY_STORM", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef04891");
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 90448279)
										{
											AudioManager.PlaySE("SE_EV_AZEUS", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef15524");
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 32807846)
										{
											AudioManager.PlaySE("SE_EV_039_NORMAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef05328");
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 32807848)
										{
											AudioManager.PlaySE("SE_EV_039_SPECIAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef20040");
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 59438930)
										{
											if (OcgCore.chainSolvingIndex <= 1)
											{
												return;
											}
											GameCard targetCard = OcgCore.cardsInChain[OcgCore.chainSolvingIndex - 2];
											if (!targetCard.p.InLocation(CardLocation.Onfield))
											{
												return;
											}
											AudioManager.PlaySE("SE_EV_038_NORMAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef11708");
											ElementObjectManager component = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>();
											Transform element = component.GetElement<Transform>("EffectOffset");
											element.localPosition = GameCard.GetCardPosition(targetCard.p, null, null);
											element.localEulerAngles = GameCard.GetCardRotation(targetCard.p, 0);
											element.localScale = GameCard.GetCardScale(targetCard.p);
											component.GetNestedElement<MeshRenderer>("CardOffset/DummyCard/DummyCardModel_front").material = targetCard.GetMaterial();
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 84192580)
										{
											AudioManager.PlaySE("SE_EV_040_NORMAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef20206_Act01");
											global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect.GetComponent<ElementObjectManager>().GetElement(CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl() ? "Hand01" : "EnHand01"));
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 42141493)
										{
											AudioManager.PlaySE("SE_EV_041_NORMAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef20500_Act01");
											ElementObjectManager manager = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>();
											if (CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl())
											{
												global::UnityEngine.Object.Destroy(manager.GetElement("MainDeck"));
												global::UnityEngine.Object.Destroy(manager.GetElement("ExDeck"));
												DuelEffectUtil.SetDeckModelAppearance(manager.GetElement<ElementObjectManager>("EnMainDeck"), this.Core.GetLocationCardCount(CardLocation.Deck, 1U), this.opDeck);
												DuelEffectUtil.SetDeckModelAppearance(manager.GetElement<ElementObjectManager>("EnExDeck"), this.Core.GetLocationCardCount(CardLocation.Extra, 1U), this.opExtra);
											}
											else
											{
												global::UnityEngine.Object.Destroy(manager.GetElement("EnMainDeck"));
												global::UnityEngine.Object.Destroy(manager.GetElement("EnExDeck"));
												DuelEffectUtil.SetDeckModelAppearance(manager.GetElement<ElementObjectManager>("MainDeck"), this.Core.GetLocationCardCount(CardLocation.Deck, 0U), this.myDeck);
												DuelEffectUtil.SetDeckModelAppearance(manager.GetElement<ElementObjectManager>("ExDeck"), this.Core.GetLocationCardCount(CardLocation.Extra, 0U), this.myExtra);
											}
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.code == 87126721)
										{
											AudioManager.PlaySE("SE_EV_042_NORMAL", 1f);
											CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef20764_Act01");
											SpriteRenderer[] componentsInChildren = CS$<>8__locals2.effect.transform.GetComponentsInChildren<SpriteRenderer>(true);
											for (int i = 0; i < componentsInChildren.Length; i++)
											{
												componentsInChildren[i].maskInteraction = SpriteMaskInteraction.None;
											}
											if (CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl())
											{
												global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect.transform.GetChildByName("GraveSet").gameObject);
											}
											else
											{
												global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect.transform.GetChildByName("EnGraveSet").gameObject);
											}
										}
										else
										{
											if (CS$<>8__locals2.CS$<>8__locals1.code == 40366667)
											{
												if (OcgCore.chainSolvingIndex > 1)
												{
													OcgCore.nextNegateAction_Additional = delegate
													{
														AudioManager.PlaySE("SE_EV_044_NORMAL", 1f);
														CS$<>8__locals2.effect = CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetCardEffectPrefab("Ef20555");
														CS$<>8__locals2.effect.transform.DestroyChildByName("BG");
														ElementObjectManager component2 = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>();
														component2.GetNestedElement("CardOffset/DummyCard").SetActive(false);
														string targetEff = (CS$<>8__locals2.CS$<>8__locals1.card.setOverTurn ? "CardOffset/nomalEf" : "CardOffset/handEf");
														component2.GetNestedElement(targetEff).SetActive(true);
														OcgCore.nextNegateAction_AdditionalManager = component2;
														OcgCore.nextNegateAction_AdditionalTime = 0.5f;
														global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect, 2f);
													};
												}
												return;
											}
											if (CS$<>8__locals2.CS$<>8__locals1.code == 97045737)
											{
												if (OcgCore.chainSolvingIndex > 1)
												{
													OcgCore.nextNegateAction_Additional = delegate
													{
														AudioManager.PlaySE("SE_EV_043_NORMAL", 1f);
														CS$<>8__locals2.effect = CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetCardEffectPrefab("Ef20257");
														CS$<>8__locals2.effect.transform.DestroyChildByName("BG");
														ElementObjectManager component3 = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>();
														component3.GetElement("CardOffset").SetActive(false);
														OcgCore.nextNegateAction_AdditionalManager = component3;
														OcgCore.nextNegateAction_AdditionalTime = 0.8f;
														global::UnityEngine.Object.Destroy(CS$<>8__locals2.effect, 2f);
													};
												}
												return;
											}
											if (CS$<>8__locals2.CS$<>8__locals1.code == 94145021)
											{
												AudioManager.PlaySE("SE_EV_EF09279_v1", 1f);
												CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef09279_Act01");
												ElementObjectManager manager2 = CS$<>8__locals2.effect.GetComponent<ElementObjectManager>();
												if (CS$<>8__locals2.CS$<>8__locals1.card.p.InMyControl())
												{
													global::UnityEngine.Object.Destroy(manager2.GetElement("MainDeck"));
													DuelEffectUtil.SetDeckModelAppearance(manager2.GetElement<ElementObjectManager>("EnMainDeck"), this.Core.GetLocationCardCount(CardLocation.Deck, 1U), this.opDeck);
												}
												else
												{
													global::UnityEngine.Object.Destroy(manager2.GetElement("EnMainDeck"));
													DuelEffectUtil.SetDeckModelAppearance(manager2.GetElement<ElementObjectManager>("MainDeck"), this.Core.GetLocationCardCount(CardLocation.Deck, 0U), this.myDeck);
												}
											}
											else if (CS$<>8__locals2.CS$<>8__locals1.code == 49238329)
											{
												AudioManager.PlaySE("SE_EV_EF14144_v2", 1f);
												CS$<>8__locals2.effect = this.GetCardEffectPrefab("Ef21234");
											}
										}
									}
									await CS$<>8__locals2.effect.GetComponent<PlayableDirector>().AutoDestroy(true);
									CS$<>8__locals2 = null;
								}
								else if (CS$<>8__locals1.code == 82732705)
								{
									if (!(CS$<>8__locals1.card.model == null))
									{
										AudioManager.PlaySE("SE_EV_SKILLDRAIN", 1f);
										GameObject effArea = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_05740_Area", true, true, null);
										GameObject effCard = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_05740_Card", true, true, null);
										effCard.SetActive(false);
										effCard.transform.position = CS$<>8__locals1.card.model.transform.position;
										await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
										effCard.SetActive(true);
										await UniTask.WaitForSeconds(1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
										global::UnityEngine.Object.Destroy(effArea);
										global::UnityEngine.Object.Destroy(effCard);
										effArea = null;
										effCard = null;
									}
								}
								else if (CS$<>8__locals1.code == 10045474)
								{
									if (CS$<>8__locals1.card.effectTargets.Count != 0 && !(CS$<>8__locals1.card.effectTargets[0].model == null))
									{
										float time = 1.5f;
										AudioManager.PlaySE("SE_EV_INFINITE_IMPERMANENCE", 1f);
										GameObject effCard = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_13631_Card", true, true, null);
										effCard.transform.position = CS$<>8__locals1.card.effectTargets[0].model.transform.position;
										if (CS$<>8__locals1.card.effectTargets[0].p.InPosition(CardPosition.Attack))
										{
											global::UnityEngine.Object.Destroy(effCard.transform.GetChild(0).GetChild(1).gameObject);
										}
										else
										{
											global::UnityEngine.Object.Destroy(effCard.transform.GetChild(0).GetChild(0).gameObject);
										}
										if (CS$<>8__locals1.card.setOverTurn)
										{
											GameObject effArea = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_13631_Area", true, false, null);
											GameObject effAreaLoop = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_13631_Area_Loop", true, false, null);
											foreach (PlaceSelector place in this.places)
											{
												if (place.InTheSameLine(CS$<>8__locals1.card.p))
												{
													GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(effArea);
													gameObject.transform.position = place.transform.position;
													global::UnityEngine.Object.Destroy(gameObject, time);
													GameObject loop = global::UnityEngine.Object.Instantiate<GameObject>(effAreaLoop);
													loop.transform.SetParent(place.transform, false);
													if ((place.p.location & 4U) > 0U)
													{
														loop.transform.localScale = new Vector3(1f, 1f, 1.1f);
													}
													this.allGameObjects.Add(loop);
													this.turnEndDeleteObjects.Add(loop);
												}
											}
											effArea = null;
										}
										await UniTask.WaitForSeconds(time, false, PlayerLoopTiming.Update, default(CancellationToken), false);
										global::UnityEngine.Object.Destroy(effCard);
										effCard = null;
									}
								}
								else if (CS$<>8__locals1.code == 14532163)
								{
									GameObject effCard = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MagicTrapEffects/fxp_14876", true, true, null);
									ElementObjectManager manager3 = effCard.GetComponent<ElementObjectManager>();
									if (CS$<>8__locals1.card.p.InMyControl())
									{
										AudioManager.PlaySE("SE_EV_LIGHTNINGSTORM_P", 1f);
										manager3.GetElement("NearMonster").SetActive(false);
									}
									else
									{
										AudioManager.PlaySE("SE_EV_LIGHTNINGSTORM_R", 1f);
										manager3.GetElement("FarMonster").SetActive(false);
									}
									await UniTask.WaitForSeconds(1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
									global::UnityEngine.Object.Destroy(effCard);
									effCard = null;
								}
								else if (CS$<>8__locals1.code == 97268402)
								{
									if (CS$<>8__locals1.card.effectTargets.Count != 0 && !(CS$<>8__locals1.card.effectTargets[0].model == null))
									{
										AudioManager.PlaySE("SE_EV_EFFECT_VEILER", 1f);
										GameObject effCard = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MonsterEffectProcess/fxp_mep08933_01", true, true, null);
										effCard.transform.position = CS$<>8__locals1.card.effectTargets[0].model.transform.position;
										await UniTask.WaitForSeconds(1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
										global::UnityEngine.Object.Destroy(effCard);
										effCard = null;
									}
								}
								else if (CS$<>8__locals1.code == 73642296)
								{
									if (OcgCore.chainSolvingIndex >= 2)
									{
										await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Effects/MonsterEffectProcess/ef13587", true, false, null);
										if (!DuelBGManager.cardEffectCodes.Contains(73642297))
										{
											List<GameObject> prefabs2 = await ABLoader.LoadsFromFolderAsync<PlayableDirector>("MasterDuel/Card/73642297");
											DuelBGManager.cardEffects.AddRange(prefabs2);
											DuelBGManager.cardEffectCodes.Add(73642297);
										}
										OcgCore.nextNegateAction = delegate
										{
											AudioManager.PlaySE("SE_EV_GHOSTBELLE", 1f);
											GameCard targetCard2 = OcgCore.cardsInChain[OcgCore.chainSolvingIndex - 2];
											GameObject eff = ABLoader.LoadFromFolder<ParticleSystem>("MasterDuel/Effects/MonsterEffectProcess/ef13587", true, true);
											eff.transform.localPosition = GameCard.GetCardPosition(targetCard2.p, null, null);
											Tools.ChangeLayer(eff, "DuelOverlay3D", false);
											CameraManager.DuelOverlay3DPlus();
											if (CS$<>8__locals1.card.GetData().Id == 73642297)
											{
												AudioManager.PlaySE("SE_EV_037_SPECIAL", 1f);
												GameObject cardEffectPrefab = CS$<>8__locals1.<>4__this.GetCardEffectPrefab(CS$<>8__locals1.card.p.InMyControl() ? "Ef03892" : "Ef03892_Op");
												cardEffectPrefab.transform.localPosition = GameCard.GetCardPosition(targetCard2.p, null, null);
												Tools.ChangeLayer(cardEffectPrefab, "DuelOverlay3D", false);
												global::UnityEngine.Object.Destroy(cardEffectPrefab, 2f);
											}
											DOTween.To(delegate(float v)
											{
											}, 0f, 0f, 2f).OnComplete(delegate
											{
												global::UnityEngine.Object.Destroy(eff);
												CameraManager.DuelOverlay3DMinus();
											});
										};
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06009A4B RID: 39499 RVA: 0x00172958 File Offset: 0x00170B58
		public async UniTask ShowAllDeckModelsAsync()
		{
			if (!(this.myDeck == null) && !(this.myExtra == null) && !(this.opDeck == null) && !(this.opExtra == null))
			{
				this.myDeck.gameObject.SetActive(true);
				this.myExtra.gameObject.SetActive(true);
				this.opDeck.gameObject.SetActive(true);
				this.opExtra.gameObject.SetActive(true);
				await this.myDeck.GetComponent<PlayableDirector>().WaitAsync(true, default(CancellationToken));
				this.myDeck.GetElement("DeckSetOffset").SetActive(false);
				this.myDeck.GetElement("CardShuffleTop").SetActive(true);
				this.myExtra.GetElement("DeckSetOffset").SetActive(false);
				this.myExtra.GetElement("CardShuffleTop").SetActive(true);
				this.opDeck.GetElement("DeckSetOffset").SetActive(false);
				this.opDeck.GetElement("CardShuffleTop").SetActive(true);
				this.opExtra.GetElement("DeckSetOffset").SetActive(false);
				this.opExtra.GetElement("CardShuffleTop").SetActive(true);
			}
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x0017299C File Offset: 0x00170B9C
		public void ShowTargetLines(Vector3 start, List<GameCard> targets)
		{
			if (this.targetLine == null)
			{
				return;
			}
			foreach (GameCard card in targets)
			{
				if ((card.p.location & 12U) > 0U)
				{
					GameObject newLine = global::UnityEngine.Object.Instantiate<GameObject>(this.targetLine);
					newLine.SetActive(true);
					LineRenderer line = newLine.transform.GetChild(0).GetComponent<LineRenderer>();
					Vector3 end = card.model.transform.position;
					Vector3[] posArr = new Vector3[]
					{
						new Vector3(start.x, 1f, start.z),
						new Vector3(start.x + (end.x - start.x) * 0.125f, 5f, start.z + (end.z - start.z) * 0.125f),
						new Vector3(start.x + (end.x - start.x) * 0.25f, 9f, start.z + (end.z - start.z) * 0.25f),
						new Vector3(start.x + (end.x - start.x) * 0.375f, 11f, start.z + (end.z - start.z) * 0.375f),
						new Vector3(start.x + (end.x - start.x) * 0.5f, 12f, start.z + (end.z - start.z) * 0.5f),
						new Vector3(start.x + (end.x - start.x) * 0.625f, 11f, start.z + (end.z - start.z) * 0.625f),
						new Vector3(start.x + (end.x - start.x) * 0.75f, 9f, start.z + (end.z - start.z) * 0.75f),
						new Vector3(start.x + (end.x - start.x) * 0.875f, 5f, start.z + (end.z - start.z) * 0.875f),
						new Vector3(end.x, 1f, end.z)
					};
					line.SetPositions(posArr);
					this.targetLines.Add(newLine);
				}
			}
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x00172C9C File Offset: 0x00170E9C
		public void HideTargetLines()
		{
			foreach (GameObject gameObject in this.targetLines)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.targetLines.Clear();
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x00172CF8 File Offset: 0x00170EF8
		public void ShowEquipLine(Vector3 start, Vector3 end)
		{
			if (this.equipLine == null)
			{
				return;
			}
			LineRenderer line = this.equipLine.transform.GetChild(0).GetComponent<LineRenderer>();
			Vector3[] posArr = new Vector3[]
			{
				new Vector3(start.x, 1f, start.z),
				new Vector3(start.x + (end.x - start.x) * 0.125f, 1.5f, start.z + (end.z - start.z) * 0.125f),
				new Vector3(start.x + (end.x - start.x) * 0.25f, 2f, start.z + (end.z - start.z) * 0.25f),
				new Vector3(start.x + (end.x - start.x) * 0.375f, 2.5f, start.z + (end.z - start.z) * 0.375f),
				new Vector3(start.x + (end.x - start.x) * 0.5f, 2.8f, start.z + (end.z - start.z) * 0.5f),
				new Vector3(start.x + (end.x - start.x) * 0.625f, 2.5f, start.z + (end.z - start.z) * 0.625f),
				new Vector3(start.x + (end.x - start.x) * 0.75f, 2f, start.z + (end.z - start.z) * 0.75f),
				new Vector3(start.x + (end.x - start.x) * 0.875f, 1.5f, start.z + (end.z - start.z) * 0.875f),
				new Vector3(end.x, 1f, end.z)
			};
			line.SetPositions(posArr);
			this.equipLine.SetActive(true);
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x00172F63 File Offset: 0x00171163
		public void HideEquipLine()
		{
			if (this.equipLine == null)
			{
				return;
			}
			this.equipLine.SetActive(false);
		}

		// Token: 0x06009A50 RID: 39504 RVA: 0x00172F80 File Offset: 0x00171180
		public void ShowAttackLine(Vector3 end, Vector3 start)
		{
			if (this.attackLine == null)
			{
				return;
			}
			ElementObjectManager component = this.attackLine.GetComponent<ElementObjectManager>();
			LineRenderer line = component.GetElement<LineRenderer>("arrowlimeRollover");
			LineRenderer line2 = component.GetElement<LineRenderer>("arrowRollover");
			Vector3[] posArr = new Vector3[]
			{
				new Vector3(start.x, 5f, start.z),
				new Vector3(start.x + (end.x - start.x) * 0.125f, 5.8f, start.z + (end.z - start.z) * 0.125f),
				new Vector3(start.x + (end.x - start.x) * 0.25f, 6.3f, start.z + (end.z - start.z) * 0.25f),
				new Vector3(start.x + (end.x - start.x) * 0.375f, 6.5f, start.z + (end.z - start.z) * 0.375f),
				new Vector3(start.x + (end.x - start.x) * 0.5f, 6.5f, start.z + (end.z - start.z) * 0.5f),
				new Vector3(start.x + (end.x - start.x) * 0.625f, 6.5f, start.z + (end.z - start.z) * 0.625f),
				new Vector3(start.x + (end.x - start.x) * 0.75f, 6.3f, start.z + (end.z - start.z) * 0.75f),
				new Vector3(start.x + (end.x - start.x) * 0.875f, 5.8f, start.z + (end.z - start.z) * 0.875f),
				new Vector3(end.x, 5f, end.z)
			};
			line.SetPositions(posArr);
			line2.SetPositions(posArr);
			this.attackLine.SetActive(true);
		}

		// Token: 0x06009A51 RID: 39505 RVA: 0x001731FD File Offset: 0x001713FD
		public void HideAttackLine()
		{
			if (this.attackLine == null)
			{
				return;
			}
			this.attackLine.SetActive(false);
		}

		// Token: 0x06009A52 RID: 39506 RVA: 0x0017321A File Offset: 0x0017141A
		public void ShowDuelFinalBlowText()
		{
			if (this.duelFinalBlow != null)
			{
				global::UnityEngine.Object.Destroy(this.duelFinalBlow.gameObject);
			}
			this.duelFinalBlow = ABLoader.LoadMasterDuelGameObject("DuelFinalBlow").GetComponent<DuelFinalBlow>();
		}

		// Token: 0x06009A53 RID: 39507 RVA: 0x0017324F File Offset: 0x0017144F
		public void HideDuelFinalBlowText()
		{
			if (this.duelFinalBlow == null)
			{
				return;
			}
			this.duelFinalBlow.Destroy();
		}

		// Token: 0x06009A54 RID: 39508 RVA: 0x0017326B File Offset: 0x0017146B
		public bool IsFinalBlow()
		{
			return this.duelFinalBlow != null;
		}

		// Token: 0x06009A55 RID: 39509 RVA: 0x0017327C File Offset: 0x0017147C
		public async UniTask<bool> NeedSpecialFinalAttackAsync(GameCard attackCard, Vector3 attackedPosition)
		{
			Card data = attackCard.GetData();
			DuelBGManager.FinalAttackType returnValue = DuelBGManager.FinalAttackType.Normal;
			if (Settings.Data.FinalAttackBlueEyes.Contains(data.Id) || Settings.Data.FinalAttackBlueEyes.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.BlueEyes;
			}
			if (Settings.Data.FinalAttackDarkM.Contains(data.Id) || Settings.Data.FinalAttackDarkM.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.DarkM;
			}
			if (Settings.Data.FinalAttackRedEyes.Contains(data.Id) || Settings.Data.FinalAttackRedEyes.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.RedEyes;
			}
			if (Settings.Data.FinalAttackObelisk.Contains(data.Id) || Settings.Data.FinalAttackObelisk.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.Obelisk;
			}
			if (Settings.Data.FinalAttackRa.Contains(data.Id) || Settings.Data.FinalAttackRa.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.Ra;
			}
			if (Settings.Data.FinalAttackSlifer.Contains(data.Id) || Settings.Data.FinalAttackSlifer.Contains(data.Alias))
			{
				returnValue = DuelBGManager.FinalAttackType.Slifer;
			}
			bool flag;
			if (returnValue == DuelBGManager.FinalAttackType.Normal)
			{
				flag = false;
			}
			else
			{
				await this.AnimationFinalAttackAsync(returnValue, attackCard, attackedPosition);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06009A56 RID: 39510 RVA: 0x001732D0 File Offset: 0x001714D0
		public void FinishDamageEffect()
		{
			AudioManager.StopBGM();
			this.Core.GetUI<OcgCoreUI>().OnNor();
			Program.instance.TimeScale = 0.1f;
			DOTween.To(() => Program.instance.TimeScale, delegate(float x)
			{
				Program.instance.TimeScale = x;
			}, 1f, 0.85f).SetEase(Ease.InQuad);
			if (OcgCore.life0 <= 0)
			{
				GameObject gameObject = ABLoader.LoadMasterDuelGameObject("fxp_dithit_fin_near_001");
				gameObject.transform.position = new Vector3(0f, 15f, -25f);
				global::UnityEngine.Object.Destroy(gameObject, 10f);
			}
			if (OcgCore.life1 <= 0)
			{
				GameObject gameObject2 = ABLoader.LoadMasterDuelGameObject("fxp_dithit_fin_far_001");
				gameObject2.transform.position = new Vector3(0f, 15f, 25f);
				global::UnityEngine.Object.Destroy(gameObject2, 10f);
			}
		}

		// Token: 0x06009A57 RID: 39511 RVA: 0x001733CC File Offset: 0x001715CC
		public void ReleaseTurnObjects()
		{
			foreach (GameObject gameObject in this.turnEndDeleteObjects)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.turnEndDeleteObjects.Clear();
		}

		// Token: 0x06009A58 RID: 39512 RVA: 0x00173428 File Offset: 0x00171628
		public async UniTask ShowTurnChangeBanner(int player)
		{
			if (OcgCore.turns >= 2)
			{
				GameObject banner = ABLoader.LoadMasterDuelGameObject("DuelTurnChange0" + player.ToString());
				await banner.GetComponent<PlayableDirector>().WaitAsync(true, default(CancellationToken));
				global::UnityEngine.Object.Destroy(banner);
			}
		}

		// Token: 0x06009A59 RID: 39513 RVA: 0x0017346C File Offset: 0x0017166C
		public async UniTask ShowPhaseBanner(int player, DuelPhase phase)
		{
			if (phase != DuelPhase.BattleStep && phase != DuelPhase.Damage && phase != DuelPhase.DamageCal && phase != DuelPhase.Battle && phase != DuelPhase.Damage)
			{
				string tail = ((player == 0) ? "_near" : "_far");
				GameObject gameObject;
				switch (phase)
				{
				case DuelPhase.Draw:
					gameObject = ABLoader.LoadMasterDuelGameObject("DuelDrawPhase" + tail);
					goto IL_00FD;
				case DuelPhase.Standby:
					gameObject = ABLoader.LoadMasterDuelGameObject("DuelStanbyPhase" + tail);
					goto IL_00FD;
				case (DuelPhase)3:
					break;
				case DuelPhase.Main1:
					gameObject = ABLoader.LoadMasterDuelGameObject("DuelMain01Phase" + tail);
					goto IL_00FD;
				default:
					if (phase == DuelPhase.Main2)
					{
						gameObject = ABLoader.LoadMasterDuelGameObject("DuelMain02Phase" + tail);
						goto IL_00FD;
					}
					if (phase == DuelPhase.End)
					{
						gameObject = ABLoader.LoadMasterDuelGameObject("DuelEndPhase" + tail);
						goto IL_00FD;
					}
					break;
				}
				gameObject = ABLoader.LoadMasterDuelGameObject("DuelBattlePhase" + tail);
				IL_00FD:
				GameObject banner = gameObject;
				string text;
				switch (phase)
				{
				case DuelPhase.Draw:
					text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_DRAW";
					goto IL_022C;
				case DuelPhase.Standby:
					text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_STANDBY";
					goto IL_022C;
				case (DuelPhase)3:
					break;
				case DuelPhase.Main1:
					text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_MAIN1";
					goto IL_022C;
				default:
					if (phase == DuelPhase.Main2)
					{
						text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_MAIN2";
						goto IL_022C;
					}
					if (phase == DuelPhase.End)
					{
						text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_END";
						goto IL_022C;
					}
					break;
				}
				text = "SE_PHASE" + ((player == 0) ? string.Empty : "_OPP") + "_BATTLE";
				IL_022C:
				AudioManager.PlaySE(text, 1f);
				await banner.GetComponent<PlayableDirector>().WaitAsync(true, default(CancellationToken));
				global::UnityEngine.Object.Destroy(banner);
			}
		}

		// Token: 0x06009A5A RID: 39514 RVA: 0x001734B7 File Offset: 0x001716B7
		public void SetPlayableGuide(bool isMe)
		{
			if (this.playableGuide == null)
			{
				return;
			}
			this.playableGuide.Set(isMe);
		}

		// Token: 0x06009A5B RID: 39515 RVA: 0x001734D4 File Offset: 0x001716D4
		public async UniTask PlayShuffleDeckAsync(int player)
		{
			Animator animator = ((player == 0) ? this.myDeck : this.opDeck).GetElement<Animator>("CardShuffleTop");
			animator.speed = 2f;
			animator.SetTrigger("Shuffle");
			CameraManager.Overlay3DReset();
			CameraManager.DuelOverlay3DPlus();
			Tools.ChangeLayer(animator.gameObject, "DuelOverlay3D", false);
			Program.instance.audio_.PlayShuffleSE();
			await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			animator.SetTrigger("Idle");
			await UniTask.Yield();
			CameraManager.DuelOverlay3DMinus();
			Tools.ChangeLayer(animator.gameObject, "Default", false);
		}

		// Token: 0x06009A5C RID: 39516 RVA: 0x00173520 File Offset: 0x00171720
		public async UniTask PlaySummonPendulum()
		{
			this.Core.GetUI<OcgCoreUI>().CardDescription.Hide();
			GameObject pendulum = ABLoader.LoadMasterDuelGameObject("SummonPendulum01");
			ElementObjectManager manager = pendulum.GetComponent<ElementObjectManager>();
			manager = manager.GetElement<ElementObjectManager>("SummonPendulumShowCard");
			pendulum.transform.SetParent(Program.instance.container_3D, false);
			ElementObjectManager card = manager.GetElement<ElementObjectManager>("DummyCard01");
			ElementObjectManager card2 = manager.GetElement<ElementObjectManager>("DummyCard02");
			Program.instance.texture_.LoadDummyCard(card, OcgCore.cardsBeTarget[0].GetData().Id, OcgCore.cardsBeTarget[0].p.controller, false, null, null);
			Program.instance.texture_.LoadDummyCard(card2, OcgCore.cardsBeTarget[1].GetData().Id, OcgCore.cardsBeTarget[1].p.controller, false, null, null);
			int scale = OcgCore.cardsBeTarget[0].GetData().LScale;
			int scale2 = OcgCore.cardsBeTarget[1].GetData().RScale;
			if (scale < 10)
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00Ones"));
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00Tens"));
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00OnesA"));
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00TensA"));
				manager.GetElement<MeshRenderer>("LPendulumNum00Digit").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum0" + scale.ToString());
				manager.GetElement<MeshRenderer>("LPendulumNum00DigitA").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum0" + scale.ToString());
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00Digit"));
				global::UnityEngine.Object.Destroy(manager.GetElement("LPendulumNum00DigitA"));
				manager.GetElement<MeshRenderer>("LPendulumNum00Tens").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum01");
				manager.GetElement<MeshRenderer>("LPendulumNum00TensA").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum01");
				manager.GetElement<MeshRenderer>("LPendulumNum00Ones").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum0" + (scale - 10).ToString());
				manager.GetElement<MeshRenderer>("LPendulumNum00OnesA").material.mainTexture = ABLoader.LoadMasterDuelTexture("LPendulumNum0" + (scale - 10).ToString());
			}
			if (scale2 < 10)
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00Ones"));
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00Tens"));
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00OnesA"));
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00TensA"));
				manager.GetElement<MeshRenderer>("RPendulumNum00Digit").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum0" + scale2.ToString());
				manager.GetElement<MeshRenderer>("RPendulumNum00DigitA").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum0" + scale2.ToString());
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00Digit"));
				global::UnityEngine.Object.Destroy(manager.GetElement("RPendulumNum00DigitA"));
				manager.GetElement<MeshRenderer>("RPendulumNum00Tens").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum01");
				manager.GetElement<MeshRenderer>("RPendulumNum00TensA").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum01");
				manager.GetElement<MeshRenderer>("RPendulumNum00Ones").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum0" + (scale2 - 10).ToString());
				manager.GetElement<MeshRenderer>("RPendulumNum00OnesA").material.mainTexture = ABLoader.LoadMasterDuelTexture("RPendulumNum0" + (scale2 - 10).ToString());
			}
			if (OcgCore.MasterRule >= 4)
			{
				GameObject scaleSet = ABLoader.LoadMasterDuelGameObject("SummonPendulumScaleSet");
				scaleSet.transform.SetParent(Program.instance.container_3D);
				if (!OcgCore.cardsBeTarget[0].p.InMyControl())
				{
					scaleSet.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
				}
				LoopTrackManager ltm = scaleSet.GetComponent<LoopTrackManager>();
				await UniTask.WaitForSeconds(3.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				ltm.StopLoop();
				await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				global::UnityEngine.Object.Destroy(scaleSet);
				global::UnityEngine.Object.Destroy(pendulum);
				scaleSet = null;
				ltm = null;
			}
			else
			{
				await UniTask.WaitForSeconds(4f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				global::UnityEngine.Object.Destroy(pendulum);
			}
		}

		// Token: 0x06009A5D RID: 39517 RVA: 0x00173563 File Offset: 0x00171763
		public bool HoveringField0()
		{
			return !(this.field0Manager == null) && UserInput.HoverObject == this.field0Manager.gameObject;
		}

		// Token: 0x06009A5E RID: 39518 RVA: 0x0017358F File Offset: 0x0017178F
		public bool HoveringField1()
		{
			return !(this.field1Manager == null) && UserInput.HoverObject == this.field1Manager.gameObject;
		}

		// Token: 0x06009A5F RID: 39519 RVA: 0x001735BB File Offset: 0x001717BB
		public void TapField0()
		{
			if (this.field0Manager == null)
			{
				return;
			}
			if (Time.time - this.field0TapTime < 1f)
			{
				return;
			}
			this.field0TapTime = Time.time;
			this.field0Manager.PlayTapAnimation();
		}

		// Token: 0x06009A60 RID: 39520 RVA: 0x001735F6 File Offset: 0x001717F6
		public void TapField1()
		{
			if (this.field1Manager == null)
			{
				return;
			}
			if (Time.time - this.field1TapTime < 1f)
			{
				return;
			}
			this.field1TapTime = Time.time;
			this.field1Manager.PlayTapAnimation();
		}

		// Token: 0x06009A61 RID: 39521 RVA: 0x00173631 File Offset: 0x00171831
		public bool HoveringMate0()
		{
			return !(this.mate0 == null) && UserInput.HoverObject == this.mate0.gameObject;
		}

		// Token: 0x06009A62 RID: 39522 RVA: 0x0017365D File Offset: 0x0017185D
		public bool HoveringMate1()
		{
			return !(this.mate1 == null) && UserInput.HoverObject == this.mate1.gameObject;
		}

		// Token: 0x06009A63 RID: 39523 RVA: 0x00173689 File Offset: 0x00171889
		public void TapMate0()
		{
			if (this.mate0 == null)
			{
				return;
			}
			if (Time.time - this.mate0TapTime < 1f)
			{
				return;
			}
			this.mate0TapTime = Time.time;
			this.mate0.Play(Mate.MateAction.Tap);
		}

		// Token: 0x06009A64 RID: 39524 RVA: 0x001736C5 File Offset: 0x001718C5
		public void TapMate1()
		{
			if (this.mate1 == null)
			{
				return;
			}
			if (Time.time - this.mate1TapTime < 1f)
			{
				return;
			}
			this.mate1TapTime = Time.time;
			this.mate1.Play(Mate.MateAction.Tap);
		}

		// Token: 0x06009A65 RID: 39525 RVA: 0x00173704 File Offset: 0x00171904
		public void PlayMate0Random()
		{
			if (this.mate0 == null)
			{
				return;
			}
			if (this.mate0Random)
			{
				this.mate0Random = false;
				this.mate0.Play(Mate.MateAction.Random);
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, (float)global::UnityEngine.Random.Range(8, 16)).OnComplete(delegate
				{
					this.mate0Random = true;
				});
			}
		}

		// Token: 0x06009A66 RID: 39526 RVA: 0x00173784 File Offset: 0x00171984
		public void PlayMate1Random()
		{
			if (this.mate1 == null)
			{
				return;
			}
			if (this.mate1Random)
			{
				this.mate1Random = false;
				this.mate1.Play(Mate.MateAction.Random);
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, (float)global::UnityEngine.Random.Range(8, 16)).OnComplete(delegate
				{
					this.mate1Random = true;
				});
			}
		}

		// Token: 0x06009A67 RID: 39527 RVA: 0x00173804 File Offset: 0x00171A04
		public void SetBgTimeScale(float timeScale)
		{
			Tools.SetAnimatorTimescale(this.field0Manager.transform, timeScale);
			Tools.SetAnimatorTimescale(this.field1Manager.transform, timeScale);
			Tools.SetAnimatorTimescale(this.phaseButton.transform, timeScale);
			if (this.timerHandler != null)
			{
				Tools.SetAnimatorTimescale(this.timerHandler.transform, timeScale);
			}
			Tools.SetParticleSystemSimulationSpeed(this.field0Manager.transform, timeScale);
			Tools.SetParticleSystemSimulationSpeed(this.field1Manager.transform, timeScale);
		}

		// Token: 0x06009A68 RID: 39528 RVA: 0x00173885 File Offset: 0x00171A85
		public void SetTimeLimit(int player, int time)
		{
			if (this.timerHandler == null)
			{
				return;
			}
			this.timerHandler.time = time;
			this.timerHandler.player = player;
		}

		// Token: 0x06009A69 RID: 39529 RVA: 0x001738B0 File Offset: 0x00171AB0
		public void ResetFields()
		{
			this.field0Manager.gameObject.SetActive(false);
			this.field1Manager.gameObject.SetActive(false);
			this.field0Manager.gameObject.SetActive(true);
			this.field1Manager.gameObject.SetActive(true);
		}

		// Token: 0x06009A6A RID: 39530 RVA: 0x00173904 File Offset: 0x00171B04
		public void SetExDeckTop(GameCard card)
		{
			if (this.exTopCts != null)
			{
				this.exTopCts.Cancel();
				this.exTopCts.Dispose();
			}
			this.exTopCts = new CancellationTokenSource();
			Component component = (card.p.InMyControl() ? this.myExtra : this.opExtra);
			int code = card.GetData().Id;
			Material targetMat = MaterialLoader.GetCardMaterial(code, true);
			foreach (Renderer r in component.transform.GetComponentsInChildren<Renderer>(true))
			{
				if (r.name.Contains("back"))
				{
					r.material = targetMat;
				}
			}
			this.SetCardToMaterialAsync(targetMat, code, this.exTopCts.Token);
		}

		// Token: 0x06009A6B RID: 39531 RVA: 0x001739B8 File Offset: 0x00171BB8
		private async UniTask SetCardToMaterialAsync(Material mat, int code, CancellationToken token)
		{
			Texture tex = await CardImageLoader.LoadCardAsync(code, true, token, false);
			if (!(mat == null))
			{
				mat.mainTexture = tex;
			}
		}

		// Token: 0x06009A6C RID: 39532 RVA: 0x00173A0C File Offset: 0x00171C0C
		public void UpdateExDeckTop(uint controller)
		{
			GameCard topCard = null;
			int extraCount = this.Core.GetLocationCardCount(CardLocation.Extra, controller);
			foreach (GameCard c in OcgCore.cards)
			{
				if (c.p.InSequence(controller, CardLocation.Extra, extraCount - 1))
				{
					topCard = c;
					break;
				}
			}
			if (topCard != null && topCard.p.InPosition(CardPosition.FaceUp) && topCard.GetData().Id != 0)
			{
				this.SetExDeckTop(topCard);
				return;
			}
			Component component = ((controller == 0U) ? this.myExtra : this.opExtra);
			Material targetMat = ((controller == 0U) ? OcgCore.myProtector : OcgCore.opProtector);
			foreach (Renderer r in component.transform.GetComponentsInChildren<Renderer>(true))
			{
				if (r.name.Contains("back"))
				{
					r.material = targetMat;
				}
			}
		}

		// Token: 0x06009A6D RID: 39533 RVA: 0x00173B14 File Offset: 0x00171D14
		public static bool MatIsSpecial(string matName)
		{
			return matName.StartsWith("Mat_013") || matName.StartsWith("Mat_045");
		}

		// Token: 0x06009A6E RID: 39534 RVA: 0x00173B34 File Offset: 0x00171D34
		private T Create<T>(bool addToList = true) where T : MonoBehaviour
		{
			GameObject obj = new GameObject(typeof(T).Name);
			T t = obj.AddComponent<T>();
			if (addToList)
			{
				this.allGameObjects.Add(obj);
			}
			return t;
		}

		// Token: 0x06009A6F RID: 39535 RVA: 0x00173B6C File Offset: 0x00171D6C
		private void CreatePlaceSelector(GPS p)
		{
			GameObject go = new GameObject("PlaceSelector");
			PlaceSelector mono = go.AddComponent<PlaceSelector>();
			mono.p = p;
			go.transform.SetParent(Program.instance.container_3D);
			this.allGameObjects.Add(go);
			this.places.Add(mono);
		}

		// Token: 0x06009A70 RID: 39536 RVA: 0x00173BC0 File Offset: 0x00171DC0
		private bool NeedChainAnimation()
		{
			bool config = true;
			if (OcgCore.condition == OcgCore.Condition.Duel && Config.Get("DuelChain", "1") == "0")
			{
				config = false;
			}
			else if (OcgCore.condition == OcgCore.Condition.Watch && Config.Get("WatchChain", "1") == "0")
			{
				config = false;
			}
			else if (OcgCore.condition == OcgCore.Condition.Replay && Config.Get("ReplayChain", "1") == "0")
			{
				config = false;
			}
			return config;
		}

		// Token: 0x06009A71 RID: 39537 RVA: 0x00173C44 File Offset: 0x00171E44
		private void ChangeChainNumber(SpriteRenderer digit, SpriteRenderer one, SpriteRenderer ten, int number)
		{
			if (number < 10)
			{
				one.gameObject.SetActive(false);
				ten.gameObject.SetActive(false);
				digit.sprite = TextureManager.container.GetChainNumSprite(number);
				return;
			}
			digit.gameObject.SetActive(false);
			one.sprite = TextureManager.container.GetChainNumSprite(number % 10);
			ten.sprite = TextureManager.container.GetChainNumSprite(number / 10 % 10);
		}

		// Token: 0x06009A72 RID: 39538 RVA: 0x00173CBC File Offset: 0x00171EBC
		private GameObject GetCardEffectPrefab(string name)
		{
			foreach (GameObject go in DuelBGManager.cardEffects)
			{
				if (go.name == name)
				{
					return global::UnityEngine.Object.Instantiate<GameObject>(go);
				}
			}
			return null;
		}

		// Token: 0x06009A73 RID: 39539 RVA: 0x00173D24 File Offset: 0x00171F24
		private async UniTask AnimationFinalAttackAsync(DuelBGManager.FinalAttackType type, GameCard attackCard, Vector3 attackedPosition)
		{
			if (type != DuelBGManager.FinalAttackType.Normal)
			{
				Sequence sequence;
				if (type == DuelBGManager.FinalAttackType.BlueEyes)
				{
					sequence = await this.AnimationFinalAttack_BlueEyes(attackCard, attackedPosition);
				}
				else if (type == DuelBGManager.FinalAttackType.DarkM)
				{
					sequence = await this.AnimationFinalAttack_DarkM(attackCard, attackedPosition);
				}
				else if (type == DuelBGManager.FinalAttackType.RedEyes)
				{
					sequence = await this.AnimationFinalAttack_RedEyes(attackCard, attackedPosition);
				}
				else if (type == DuelBGManager.FinalAttackType.Ra)
				{
					sequence = await this.AnimationFinalAttack_Ra(attackCard, attackedPosition);
				}
				else if (type == DuelBGManager.FinalAttackType.Slifer)
				{
					sequence = await this.AnimationFinalAttack_Slifer(attackCard, attackedPosition);
				}
				else
				{
					sequence = await this.AnimationFinalAttack_Obelisk(attackCard, attackedPosition);
				}
				await sequence.WaitAsync(default(CancellationToken));
			}
		}

		// Token: 0x06009A74 RID: 39540 RVA: 0x00173D80 File Offset: 0x00171F80
		private async UniTask<Sequence> AnimationFinalAttack_BlueEyes(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/BlueEyes/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/BlueEyes/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/BlueEyes/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/FinalAttack/BlueEyes/Beam", true, false, null);
			Vector3 attackRotation = (attackCard.p.InMyControl() ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/BlueEyes/CardSet", true, true, null);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager cardSetManager = attackTransform.GetComponent<ElementObjectManager>();
			ElementObjectManager subManager = cardSetManager.GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			attackCard.model.SetActive(false);
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/BlueEyes/ScreenEffect", true, true, null);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/BlueEyes/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, true, null);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			GameObject beam = await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/FinalAttack/BlueEyes/Beam", true, true, null);
			beam.transform.SetParent(cardSetManager.transform, false);
			beam.transform.localPosition = new Vector3(0f, 1f, 0f);
			beam.GetComponent<PlayableDirector>().enabled = true;
			beam.GetComponent<PlayableDirector>().playOnAwake = true;
			beam.SetActive(false);
			Vector3 offset = new Vector3(0f, 5f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_BE_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = ((faceAngle.y > 0f && faceAngle.y < 180f) ? (-60f) : 60f);
			offset = new Vector3(0f, 5f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.6f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(45f, 0f, 0f), 0.6f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			offset = new Vector3(0f, 5f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.3f, false).SetEase(Ease.InOutCubic));
			faceAngle.z = 0f;
			sequence.Join(attackTransform.DORotate(faceAngle, 0.3f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.1f).OnComplete(delegate
			{
				beam.SetActive(true);
			}));
			offset = new Vector3(0f, 3f, 8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = -8f;
			}
			sequence.Append(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.1f, false));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.15f).OnComplete(delegate
			{
				hit.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_BE_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			}));
			sequence.AppendInterval(1f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
			});
			return sequence;
		}

		// Token: 0x06009A75 RID: 39541 RVA: 0x00173DCC File Offset: 0x00171FCC
		private async UniTask<Sequence> AnimationFinalAttack_DarkM(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/DarkM/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/LineRendererDA", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/TargetPoint", true, false, null);
			Vector3 attackRotation = (attackCard.p.InMyControl() ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			CameraManager.DuelOverlay3DPlus();
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/CardSet", true, true, null);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/DarkM/ScreenEffect", true, true, null);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, true, null);
			GameObject lineRendererDA = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/LineRendererDA", true, true, null);
			GameObject targetPoint = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/DarkM/TargetPoint", true, true, null);
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			Tools.ChangeLayer(screenEffect, "DuelOverlay3D", false);
			Tools.ChangeLayer(hit, "DuelOverlay3D", false);
			Tools.ChangeLayer(lineRendererDA, "DuelOverlay3D", false);
			Tools.ChangeLayer(targetPoint, "DuelOverlay3D", false);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager subManager = attackTransform.GetComponent<ElementObjectManager>().GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			attackCard.model.SetActive(false);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			targetPoint.transform.position = attackedPosition;
			targetPoint.GetComponent<PlayableDirector>().playOnAwake = true;
			targetPoint.transform.LookAt(attackCard.model.transform);
			targetPoint.transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			targetPoint.SetActive(false);
			ElementObjectManager component = lineRendererDA.GetComponent<ElementObjectManager>();
			LineRenderer line = component.GetElement<LineRenderer>("Line01");
			LineRenderer line2 = component.GetElement<LineRenderer>("Line02");
			lineRendererDA.SetActive(false);
			Vector3 offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			Vector3[] positions = new Vector3[]
			{
				attackTransform.position + new Vector3(0f, 1f, 0f),
				attackedPosition
			};
			line.SetPositions(positions);
			line2.SetPositions(positions);
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_BM_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = ((faceAngle.y > 0f && faceAngle.y < 180f) ? (-60f) : 60f);
			offset = new Vector3(0f, 40f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.8f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(45f, 0f, 0f), 0.8f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.3f, false).SetEase(Ease.InOutCubic));
			faceAngle.z = 0f;
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(30f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.1f).OnComplete(delegate
			{
				lineRendererDA.SetActive(true);
				global::UnityEngine.Object.Destroy(lineRendererDA, 0.58f);
			}));
			offset = new Vector3(0f, 3f, 8f);
			if (attackCard.p.controller != 0U)
			{
				offset.z = -8f;
			}
			sequence.Append(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.1f, false));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.15f).OnComplete(delegate
			{
				hit.SetActive(true);
				targetPoint.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_BM_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			}));
			sequence.AppendInterval(0.6f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				global::UnityEngine.Object.Destroy(targetPoint);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
				CameraManager.DuelOverlay3DMinus();
			});
			return sequence;
		}

		// Token: 0x06009A76 RID: 39542 RVA: 0x00173E18 File Offset: 0x00172018
		private async UniTask<Sequence> AnimationFinalAttack_RedEyes(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/RedEyes/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/RedEyes/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/RedEyes/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Timeline/FinalAttack/RedEyes/Bless", true, false, null);
			Vector3 attackRotation = ((attackCard.p.controller == 0U) ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			CameraManager.DuelOverlay3DPlus();
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/RedEyes/CardSet", true, true, null);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/RedEyes/ScreenEffect", true, true, null);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/RedEyes/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, true, null);
			GameObject bless = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Timeline/FinalAttack/RedEyes/Bless", true, true, null);
			bless.SetActive(false);
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager component = attackTransform.GetComponent<ElementObjectManager>();
			ElementObjectManager subManager = component.GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			component.GetComponent<PlayableDirector>().Play();
			attackCard.model.SetActive(false);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			Vector3 offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_RE_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = ((faceAngle.y > 0f && faceAngle.y < 180f) ? (-60f) : 60f);
			offset = new Vector3(0f, 40f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.6f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(45f, 0f, 0f), 0.6f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.3f, false).SetEase(Ease.InOutCubic));
			faceAngle.z = 0f;
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(30f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			offset = new Vector3(0f, 3f, 8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = -8f;
			}
			sequence.Append(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.3f, false));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.05f).OnComplete(delegate
			{
				bless.SetActive(true);
				bless.transform.position = attackTransform.position;
				bless.transform.LookAt(attackedPosition);
				bless.transform.DOMove(attackedPosition, 0.3f, false);
			}));
			sequence.AppendCallback(delegate
			{
				hit.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_RE_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			});
			sequence.AppendInterval(0.6f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				global::UnityEngine.Object.Destroy(bless);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
				CameraManager.DuelOverlay3DMinus();
			});
			return sequence;
		}

		// Token: 0x06009A77 RID: 39543 RVA: 0x00173E64 File Offset: 0x00172064
		private async UniTask<Sequence> AnimationFinalAttack_Ra(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Ra/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Ra/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Ra/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Timeline/FinalAttack/Ra/Bless", true, false, null);
			Vector3 attackRotation = ((attackCard.p.controller == 0U) ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			CameraManager.DuelOverlay3DPlus();
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Ra/CardSet", true, true, null);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Ra/ScreenEffect", true, true, null);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Ra/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, true, null);
			GameObject bless = await ABLoader.LoadFromFolderAsync<ParticleSystem>("MasterDuel/Timeline/FinalAttack/Ra/Bless", true, true, null);
			Tools.SetParticleSystemSimulationSpeed(bless.transform, 0.5f);
			bless.SetActive(false);
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager component = attackTransform.GetComponent<ElementObjectManager>();
			ElementObjectManager subManager = component.GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			component.GetComponent<PlayableDirector>().Play();
			attackCard.model.SetActive(false);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			Vector3 offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_RA_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = 0f;
			offset = new Vector3(0f, 40f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 1f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(-30f, 0f, 0f), 1f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.AppendCallback(delegate
			{
				bless.SetActive(true);
				bless.transform.position = attackTransform.position;
				bless.transform.LookAt(attackedPosition);
				bless.transform.DOMove(attackedPosition, 0.3f, false);
				offset = new Vector3(0f, 3f, 8f);
				if (attackCard.p.controller != 0U)
				{
					offset.z = -8f;
				}
				Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.3f, false);
			});
			offset = new Vector3(0f, 20f, 0f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 0f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.3f, false).SetEase(Ease.OutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(30f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.OutCubic));
			sequence.AppendCallback(delegate
			{
				hit.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_RA_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			});
			sequence.AppendInterval(0.6f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				global::UnityEngine.Object.Destroy(bless);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
				CameraManager.DuelOverlay3DMinus();
			});
			return sequence;
		}

		// Token: 0x06009A78 RID: 39544 RVA: 0x00173EB0 File Offset: 0x001720B0
		private async UniTask<Sequence> AnimationFinalAttack_Slifer(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Slifer/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Slifer/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Slifer/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/FinalAttack/Slifer/Beam", true, false, null);
			Vector3 attackRotation = (attackCard.p.InMyControl() ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			CameraManager.DuelOverlay3DPlus();
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Slifer/CardSet", true, true, null);
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager cardSetManager = attackTransform.GetComponent<ElementObjectManager>();
			ElementObjectManager subManager = cardSetManager.GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			attackCard.model.SetActive(false);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Slifer/ScreenEffect", true, true, null);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			screenEffect.SetActive(false);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Slifer/Hit" + (attackCard.p.InMyControl() ? "Far" : "Near"), true, true, null);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			GameObject beam = await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/FinalAttack/Slifer/Beam", true, true, null);
			beam.transform.SetParent(cardSetManager.transform, false);
			beam.transform.localPosition = new Vector3(0f, 1f, 0f);
			beam.GetComponent<PlayableDirector>().enabled = true;
			beam.GetComponent<PlayableDirector>().playOnAwake = true;
			beam.SetActive(false);
			Vector3 offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_SLIFER_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = 0f;
			offset = new Vector3(0f, 40f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.8f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(30f, 0f, 0f), 0.8f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.4f).OnComplete(delegate
			{
				screenEffect.SetActive(true);
			}));
			offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.3f, false).SetEase(Ease.InOutCubic));
			faceAngle.z = 0f;
			sequence.Join(attackTransform.DORotate(faceAngle, 0.3f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 0.1f).OnComplete(delegate
			{
				beam.SetActive(true);
			}));
			sequence.AppendCallback(delegate
			{
				offset = new Vector3(0f, 3f, 8f);
				if (attackCard.p.controller != 0U)
				{
					offset.z = -8f;
				}
				Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.1f, false);
			});
			sequence.AppendInterval(0.1f);
			sequence.AppendCallback(delegate
			{
				hit.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_SLIFER_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			});
			sequence.AppendInterval(0.6f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
				CameraManager.DuelOverlay3DMinus();
			});
			return sequence;
		}

		// Token: 0x06009A79 RID: 39545 RVA: 0x00173EFC File Offset: 0x001720FC
		private async UniTask<Sequence> AnimationFinalAttack_Obelisk(GameCard attackCard, Vector3 attackedPosition)
		{
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/CardSet", true, false, null);
			await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Obelisk/ScreenEffect", true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, false, null);
			await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/Punch", true, false, null);
			Vector3 attackRotation = ((attackCard.p.controller == 0U) ? Vector3.zero : new Vector3(0f, 180f, 0f));
			CameraManager.Duel3DOverlayStickWithMain(true);
			CameraManager.DuelOverlay3DPlus();
			GameObject cardSet = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/CardSet", true, true, null);
			GameObject screenEffect = await ABLoader.LoadFromFolderAsync<ScreenEffect>("MasterDuel/Timeline/FinalAttack/Obelisk/ScreenEffect", true, true, null);
			GameObject hit = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/Hit" + ((attackCard.p.controller == 0U) ? "Far" : "Near"), true, true, null);
			GameObject punch = await ABLoader.LoadFromFolderAsync<ElementObjectManager>("MasterDuel/Timeline/FinalAttack/Obelisk/Punch", true, true, null);
			punch.GetComponent<PlayableDirector>().enabled = true;
			if (!attackCard.p.InMyControl())
			{
				punch.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			}
			Tools.ChangeLayer(cardSet, "DuelOverlay3D", false);
			Tools.ChangeLayer(punch, "DuelOverlay3D", false);
			Transform attackTransform = cardSet.transform;
			ElementObjectManager component = attackTransform.GetComponent<ElementObjectManager>();
			ElementObjectManager subManager = component.GetElement<ElementObjectManager>("Card");
			Program.instance.texture_.LoadDummyCard(subManager, attackCard.GetData().Id, attackCard.p.controller, false, null, null);
			component.GetComponent<PlayableDirector>().Play();
			attackCard.model.SetActive(false);
			screenEffect.transform.SetParent(Program.instance.camera_.cameraDuelOverlay3D.transform, true);
			hit.transform.position = attackedPosition;
			hit.SetActive(false);
			punch.transform.position = attackedPosition;
			punch.SetActive(false);
			Vector3 offset = new Vector3(0f, 5f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			Vector3 attackPosition = attackCard.model.transform.position;
			attackTransform.position = attackPosition + offset;
			attackTransform.LookAt(attackedPosition);
			Vector3 faceAngle = attackTransform.eulerAngles;
			faceAngle.x = 0f;
			attackTransform.eulerAngles = attackRotation;
			attackTransform.position = attackPosition;
			AudioManager.PlaySE("SE_MONSTERATTACK_OBELISK_01", 1f);
			Sequence sequence = DOTween.Sequence();
			faceAngle.z = 0f;
			offset = new Vector3(5f, 40f, -15f);
			if (!attackCard.p.InMyControl())
			{
				offset.x = -5f;
				offset.z = 15f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 1.5f, false).SetEase(Ease.InOutCubic));
			offset = new Vector3(-30f, 35f, 0f);
			sequence.Join(attackTransform.DORotate(faceAngle + offset, 1.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 1f).OnComplete(delegate
			{
				punch.SetActive(true);
			}));
			offset = new Vector3(0f, 20f, -8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = 8f;
			}
			sequence.Append(attackTransform.DOMove(attackPosition + offset, 0.4f, false).SetEase(Ease.InOutCubic));
			faceAngle.z = 0f;
			offset = new Vector3(20f, 0f, 0f);
			sequence.Join(attackTransform.DORotate(faceAngle + offset, 0.4f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.GetChild(0).DOLocalMoveZ(10f, 0.4f, false));
			sequence.Join(attackTransform.GetChild(0).DOLocalRotate(new Vector3(0f, -30f, 0f), 0.4f, RotateMode.Fast));
			offset = new Vector3(0f, 3f, 8f);
			if (!attackCard.p.InMyControl())
			{
				offset.z = -8f;
			}
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition + offset, 0.4f, false));
			sequence.AppendInterval(0.1f);
			sequence.AppendCallback(delegate
			{
				hit.SetActive(true);
				AudioManager.PlaySE("SE_MONSTERATTACK_OBELISK_02", 1f);
				if (OcgCore.NextMessageIs(GameMessage.Damage))
				{
					OcgCore.NoMoreWait = true;
				}
				CameraManager.ShakeCamera(true);
			});
			sequence.AppendInterval(0.6f);
			sequence.Append(attackTransform.DOMove(attackPosition, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.DORotate(attackRotation, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.GetChild(0).DOLocalMove(Vector3.zero, 0.5f, false).SetEase(Ease.InOutCubic));
			sequence.Join(attackTransform.GetChild(0).DOLocalRotate(Vector3.zero, 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
			sequence.Join(Program.instance.camera_.cameraMain.transform.DOLocalMove(CameraManager.mainCameraDefaultPosition, 0.2f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(cardSet);
				global::UnityEngine.Object.Destroy(hit);
				global::UnityEngine.Object.Destroy(screenEffect);
				global::UnityEngine.Object.Destroy(punch);
				attackCard.model.SetActive(true);
				CameraManager.Duel3DOverlayStickWithMain(false);
				CameraManager.DuelOverlay3DMinus();
			});
			return sequence;
		}

		// Token: 0x06009A7B RID: 39547 RVA: 0x00173FB0 File Offset: 0x001721B0
		// Note: this type is marked as 'beforefieldinit'.
		static DuelBGManager()
		{
			Dictionary<ValueTuple<int, CardLocation>, Vector3> dictionary = new Dictionary<ValueTuple<int, CardLocation>, Vector3>();
			ValueTuple<int, CardLocation> valueTuple = new ValueTuple<int, CardLocation>(0, CardLocation.Deck);
			dictionary[valueTuple] = new Vector3(26.6f, 1.5f, -23.5f);
			ValueTuple<int, CardLocation> valueTuple2 = new ValueTuple<int, CardLocation>(0, CardLocation.Extra);
			dictionary[valueTuple2] = new Vector3(-26.6f, 1.5f, -23.5f);
			ValueTuple<int, CardLocation> valueTuple3 = new ValueTuple<int, CardLocation>(1, CardLocation.Deck);
			dictionary[valueTuple3] = new Vector3(-26.6f, 1.5f, 23.5f);
			ValueTuple<int, CardLocation> valueTuple4 = new ValueTuple<int, CardLocation>(1, CardLocation.Extra);
			dictionary[valueTuple4] = new Vector3(26.6f, 1.5f, 23.5f);
			DuelBGManager._positionMap = dictionary;
			Dictionary<ValueTuple<int, CardLocation>, Vector3> dictionary2 = new Dictionary<ValueTuple<int, CardLocation>, Vector3>();
			valueTuple4 = new ValueTuple<int, CardLocation>(0, CardLocation.Deck);
			dictionary2[valueTuple4] = new Vector3(0f, -20f, 0f);
			valueTuple3 = new ValueTuple<int, CardLocation>(0, CardLocation.Extra);
			dictionary2[valueTuple3] = new Vector3(0f, 20f, 0f);
			valueTuple2 = new ValueTuple<int, CardLocation>(1, CardLocation.Deck);
			dictionary2[valueTuple2] = new Vector3(0f, 160f, 0f);
			valueTuple = new ValueTuple<int, CardLocation>(1, CardLocation.Extra);
			dictionary2[valueTuple] = new Vector3(0f, -160f, 0f);
			DuelBGManager._angleMap = dictionary2;
		}

		// Token: 0x06009A7F RID: 39551 RVA: 0x00174120 File Offset: 0x00172320
		[CompilerGenerated]
		private void <ShowBGEnd>g__HeroWin|66_0()
		{
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.EndWin, "");
			if (this.mate0 != null)
			{
				this.mate0.Play(Mate.MateAction.Victory);
			}
		}

		// Token: 0x06009A80 RID: 39552 RVA: 0x00174150 File Offset: 0x00172350
		[CompilerGenerated]
		private void <ShowBGEnd>g__HeroLose|66_1()
		{
			this.bgPhase0 = 4;
			string seLabel = "SE_FIELD_MAT" + this.field0Manager.name.Substring(4, 3) + "_PHASE4_P";
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToEnd, seLabel);
			this.field0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.EndLose, "");
			if (this.stand0Manager != null)
			{
				this.stand0Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToEnd, "");
			}
			if (this.mate0 != null)
			{
				this.mate0.Play(Mate.MateAction.Defeat);
			}
		}

		// Token: 0x06009A81 RID: 39553 RVA: 0x00174214 File Offset: 0x00172414
		[CompilerGenerated]
		private void <ShowBGEnd>g__RivalWin|66_2()
		{
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.EndWin, "");
			if (this.mate1 != null)
			{
				this.mate1.Play(Mate.MateAction.Victory);
			}
		}

		// Token: 0x06009A82 RID: 39554 RVA: 0x00174244 File Offset: 0x00172444
		[CompilerGenerated]
		private void <ShowBGEnd>g__RivalLose|66_3()
		{
			this.bgPhase1 = 4;
			string seLabel = "SE_FIELD_MAT" + this.field0Manager.name.Substring(4, 3) + "_PHASE4_R";
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, "");
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, "");
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, "");
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToEnd, seLabel);
			this.field1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.EndLose, "");
			if (this.stand1Manager != null)
			{
				this.stand1Manager.PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToEnd, "");
			}
			if (this.mate1 != null)
			{
				this.mate1.Play(Mate.MateAction.Defeat);
			}
		}

		// Token: 0x0400D7CF RID: 55247
		private Deck deck;

		// Token: 0x0400D7D0 RID: 55248
		private bool mate0Random = true;

		// Token: 0x0400D7D1 RID: 55249
		private bool mate1Random = true;

		// Token: 0x0400D7D2 RID: 55250
		private int bgPhase0 = 1;

		// Token: 0x0400D7D3 RID: 55251
		private int bgPhase1 = 1;

		// Token: 0x0400D7D4 RID: 55252
		private bool backgroundFieldInitialize;

		// Token: 0x0400D7D5 RID: 55253
		public bool loaded;

		// Token: 0x0400D7D6 RID: 55254
		private float field0TapTime;

		// Token: 0x0400D7D7 RID: 55255
		private float field1TapTime;

		// Token: 0x0400D7D8 RID: 55256
		private float mate0TapTime;

		// Token: 0x0400D7D9 RID: 55257
		private float mate1TapTime;

		// Token: 0x0400D7DA RID: 55258
		public DuelMessage processor;

		// Token: 0x0400D7DB RID: 55259
		public readonly List<GameObject> allGameObjects = new List<GameObject>();

		// Token: 0x0400D7DC RID: 55260
		private readonly List<GameObject> turnEndDeleteObjects = new List<GameObject>();

		// Token: 0x0400D7DD RID: 55261
		private static readonly List<int> cardEffectCodes = new List<int>();

		// Token: 0x0400D7DE RID: 55262
		private static readonly List<GameObject> cardEffects = new List<GameObject>();

		// Token: 0x0400D7DF RID: 55263
		private GameObject attackLine;

		// Token: 0x0400D7E0 RID: 55264
		private GameObject targetLine;

		// Token: 0x0400D7E1 RID: 55265
		private readonly List<GameObject> targetLines = new List<GameObject>();

		// Token: 0x0400D7E2 RID: 55266
		private GameObject equipLine;

		// Token: 0x0400D7E3 RID: 55267
		public GameObject fieldSummonRightInfo;

		// Token: 0x0400D7E4 RID: 55268
		public BgEffectManager field0Manager;

		// Token: 0x0400D7E5 RID: 55269
		public BgEffectManager field1Manager;

		// Token: 0x0400D7E6 RID: 55270
		private BgEffectManager grave0Manager;

		// Token: 0x0400D7E7 RID: 55271
		private BgEffectManager grave1Manager;

		// Token: 0x0400D7E8 RID: 55272
		private readonly List<GraveBehaviour> graves = new List<GraveBehaviour>();

		// Token: 0x0400D7E9 RID: 55273
		private BgEffectManager stand0Manager;

		// Token: 0x0400D7EA RID: 55274
		private BgEffectManager stand1Manager;

		// Token: 0x0400D7EB RID: 55275
		private Mate mate0;

		// Token: 0x0400D7EC RID: 55276
		private Mate mate1;

		// Token: 0x0400D7ED RID: 55277
		private GameObject phaseButton;

		// Token: 0x0400D7EE RID: 55278
		private TimerHandler timerHandler;

		// Token: 0x0400D7EF RID: 55279
		private PlayableGuide playableGuide;

		// Token: 0x0400D7F0 RID: 55280
		public ElementObjectManager myDeck;

		// Token: 0x0400D7F1 RID: 55281
		public ElementObjectManager myExtra;

		// Token: 0x0400D7F2 RID: 55282
		public ElementObjectManager opDeck;

		// Token: 0x0400D7F3 RID: 55283
		public ElementObjectManager opExtra;

		// Token: 0x0400D7F4 RID: 55284
		public List<PlaceSelector> places = new List<PlaceSelector>();

		// Token: 0x0400D7F5 RID: 55285
		private DuelFinalBlow duelFinalBlow;

		// Token: 0x0400D7F6 RID: 55286
		private static readonly Dictionary<ValueTuple<int, CardLocation>, Vector3> _positionMap;

		// Token: 0x0400D7F7 RID: 55287
		private static readonly Dictionary<ValueTuple<int, CardLocation>, Vector3> _angleMap;

		// Token: 0x0400D7F8 RID: 55288
		private CancellationTokenSource exTopCts;

		// Token: 0x02001494 RID: 5268
		private enum FinalAttackType
		{
			// Token: 0x0400D7FA RID: 55290
			BlueEyes,
			// Token: 0x0400D7FB RID: 55291
			DarkM,
			// Token: 0x0400D7FC RID: 55292
			RedEyes,
			// Token: 0x0400D7FD RID: 55293
			Slifer,
			// Token: 0x0400D7FE RID: 55294
			Obelisk,
			// Token: 0x0400D7FF RID: 55295
			Ra,
			// Token: 0x0400D800 RID: 55296
			Normal
		}
	}
}

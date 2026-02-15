using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.Servant
{
	// Token: 0x020012C9 RID: 4809
	public class Appearance : Servant
	{
		// Token: 0x06008C9F RID: 35999 RVA: 0x00124D06 File Offset: 0x00122F06
		public void SwitchCondition(Appearance.Condition condition)
		{
			Appearance.condition = condition;
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06008CA0 RID: 36000 RVA: 0x00124D0E File Offset: 0x00122F0E
		public override int Depth
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06008CA1 RID: 36001 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06008CA2 RID: 36002 RVA: 0x00124D11 File Offset: 0x00122F11
		protected override float SubBlackAlpha
		{
			get
			{
				return 0.9f;
			}
		}

		// Token: 0x06008CA3 RID: 36003 RVA: 0x00124D18 File Offset: 0x00122F18
		public override void Initialize()
		{
			base.Initialize();
			this.LoadSettingAssets();
		}

		// Token: 0x06008CA4 RID: 36004 RVA: 0x00124D28 File Offset: 0x00122F28
		public override void OnReturn()
		{
			if (this.inTransition)
			{
				return;
			}
			if (this.returnAction != null)
			{
				this.returnAction();
				return;
			}
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			GameObject selected = EventSystem.current.currentSelectedGameObject;
			if (selected == null)
			{
				this.OnExit();
				return;
			}
			if (Cursor.lockState == CursorLockMode.None)
			{
				this.OnExit();
				return;
			}
			SelectionToggle_AppearanceItem selectionToggle_AppearanceItem;
			if (!selected.TryGetComponent<SelectionToggle_AppearanceItem>(out selectionToggle_AppearanceItem) && !(selected == this.GetUI<AppearanceUI>().InputPlayerName.gameObject))
			{
				this.OnExit();
				return;
			}
			if (this.lastSelectedToggle != null)
			{
				this.lastSelectedToggle.GetSelectable().Select();
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008CA5 RID: 36005 RVA: 0x00124DE0 File Offset: 0x00122FE0
		public override void OnExit()
		{
			if (Appearance.condition == Appearance.Condition.DeckEditor)
			{
				Program.instance.ShiftToServant(Program.instance.deckEditor);
				return;
			}
			if (Program.instance.currentSubServant == this)
			{
				Program.instance.ShowSubServant(Program.instance.setting);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.setting);
		}

		// Token: 0x06008CA6 RID: 36006 RVA: 0x00124E48 File Offset: 0x00123048
		public override void PerFrameFunction()
		{
			if (this.NeedResponseInput())
			{
				if (UserInput.WasLeftShoulderPressed && this.GetUI<AppearanceUI>().CanSwitchPlayer())
				{
					this.GetUI<AppearanceUI>().OnPlayerLeft();
				}
				if (UserInput.WasRightShoulderPressed && this.GetUI<AppearanceUI>().CanSwitchPlayer())
				{
					this.GetUI<AppearanceUI>().OnPlayerRight();
				}
				if (UserInput.WasGamepadButtonNorthPressed && this.GetUI<AppearanceUI>().InPickupPage())
				{
					this.GetUI<AppearanceUI>().OnPickupChange();
				}
				if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
				{
					this.OnReturn();
				}
			}
		}

		// Token: 0x06008CA7 RID: 36007 RVA: 0x00124ED0 File Offset: 0x001230D0
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (!(this.lastSelectable != null))
			{
				this.servantUI.SelectDefaultSelectable();
				return;
			}
			SelectionToggle_CharacterItem selectionToggle_CharacterItem;
			SelectionToggle_CharacterSeries selectionToggle_CharacterSeries;
			if (this.lastSelectable.TryGetComponent<SelectionToggle_CharacterItem>(out selectionToggle_CharacterItem) || this.lastSelectable.TryGetComponent<SelectionToggle_CharacterSeries>(out selectionToggle_CharacterSeries))
			{
				this.lastSelectable.Select();
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008CA8 RID: 36008 RVA: 0x00124F38 File Offset: 0x00123138
		public async UniTask LoadSettingAssets()
		{
			Appearance.loaded = false;
			object obj = await AssetBundle.LoadFromFileAsync(Program.root + "MasterDuel/Frame/ProfileFrameMat1030001");
			Appearance.matForFace = obj.LoadAsset<Material>("ProfileFrameMat1030001");
			obj.Unload(false);
			Items items = Program.items;
			string text = "DuelFace0";
			Items.Item item = Program.items.faces[0];
			UniTask<Sprite>.Awaiter awaiter = items.LoadConcreteItemIconAsync(Config.Get(text, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			UniTask<Sprite>.Awaiter awaiter2;
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.duelFace0 = awaiter.GetResult();
			Items items2 = Program.items;
			string text2 = "DuelFace1";
			item = Program.items.faces[0];
			awaiter = items2.LoadConcreteItemIconAsync(Config.Get(text2, item.id.ToString()), Items.ItemType.Face, 1).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.duelFace1 = awaiter.GetResult();
			Items items3 = Program.items;
			string text3 = "DuelFace0Tag";
			item = Program.items.faces[0];
			awaiter = items3.LoadConcreteItemIconAsync(Config.Get(text3, item.id.ToString()), Items.ItemType.Face, 2).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.duelFace0Tag = awaiter.GetResult();
			Items items4 = Program.items;
			string text4 = "DuelFace1Tag";
			item = Program.items.faces[0];
			awaiter = items4.LoadConcreteItemIconAsync(Config.Get(text4, item.id.ToString()), Items.ItemType.Face, 3).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.duelFace1Tag = awaiter.GetResult();
			Items items5 = Program.items;
			string text5 = "WatchFace0";
			item = Program.items.faces[0];
			awaiter = items5.LoadConcreteItemIconAsync(Config.Get(text5, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.watchFace0 = awaiter.GetResult();
			Items items6 = Program.items;
			string text6 = "WatchFace1";
			item = Program.items.faces[0];
			awaiter = items6.LoadConcreteItemIconAsync(Config.Get(text6, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.watchFace1 = awaiter.GetResult();
			Items items7 = Program.items;
			string text7 = "WatchFace0Tag";
			item = Program.items.faces[0];
			awaiter = items7.LoadConcreteItemIconAsync(Config.Get(text7, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.watchFace0Tag = awaiter.GetResult();
			Items items8 = Program.items;
			string text8 = "WatchFace1Tag";
			item = Program.items.faces[0];
			awaiter = items8.LoadConcreteItemIconAsync(Config.Get(text8, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.watchFace1Tag = awaiter.GetResult();
			Items items9 = Program.items;
			string text9 = "ReplayFace0";
			item = Program.items.faces[0];
			awaiter = items9.LoadConcreteItemIconAsync(Config.Get(text9, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.replayFace0 = awaiter.GetResult();
			Items items10 = Program.items;
			string text10 = "ReplayFace1";
			item = Program.items.faces[0];
			awaiter = items10.LoadConcreteItemIconAsync(Config.Get(text10, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.replayFace1 = awaiter.GetResult();
			Items items11 = Program.items;
			string text11 = "ReplayFace0Tag";
			item = Program.items.faces[0];
			awaiter = items11.LoadConcreteItemIconAsync(Config.Get(text11, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.replayFace0Tag = awaiter.GetResult();
			Items items12 = Program.items;
			string text12 = "ReplayFace1Tag";
			item = Program.items.faces[0];
			awaiter = items12.LoadConcreteItemIconAsync(Config.Get(text12, item.id.ToString()), Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.replayFace1Tag = awaiter.GetResult();
			awaiter = Program.items.LoadConcreteItemIconAsync("1010039", Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.defaultFace0 = awaiter.GetResult();
			awaiter = Program.items.LoadConcreteItemIconAsync("1010001", Items.ItemType.Face, 0).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<Sprite>.Awaiter);
			}
			Appearance.defaultFace1 = awaiter.GetResult();
			Items items13 = Program.items;
			string text13 = "DuelFrame0";
			item = Program.items.frames[0];
			Sprite duelFrame0 = await items13.LoadConcreteItemIconAsync(Config.Get(text13, item.id.ToString()), Items.ItemType.Frame, 0);
			string text14 = "DuelFrame0";
			item = Program.items.frames[0];
			UniTask<Material>.Awaiter awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text14, item.id.ToString())).GetAwaiter();
			UniTask<Material>.Awaiter awaiter4;
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelFrameMat0 = awaiter3.GetResult();
			Appearance.duelFrameMat0.SetTexture("_ProfileFrameTex", duelFrame0.texture);
			Items items14 = Program.items;
			string text15 = "DuelFrame1";
			item = Program.items.frames[0];
			Sprite duelFrame = await items14.LoadConcreteItemIconAsync(Config.Get(text15, item.id.ToString()), Items.ItemType.Frame, 0);
			string text16 = "DuelFrame1";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text16, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelFrameMat1 = awaiter3.GetResult();
			Appearance.duelFrameMat1.SetTexture("_ProfileFrameTex", duelFrame.texture);
			Items items15 = Program.items;
			string text17 = "DuelFrame0Tag";
			item = Program.items.frames[0];
			Sprite duelFrame0Tag = await items15.LoadConcreteItemIconAsync(Config.Get(text17, item.id.ToString()), Items.ItemType.Frame, 0);
			string text18 = "DuelFrame0Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text18, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelFrameMat0Tag = awaiter3.GetResult();
			Appearance.duelFrameMat0Tag.SetTexture("_ProfileFrameTex", duelFrame0Tag.texture);
			Items items16 = Program.items;
			string text19 = "DuelFrame1Tag";
			item = Program.items.frames[0];
			Sprite duelFrame1Tag = await items16.LoadConcreteItemIconAsync(Config.Get(text19, item.id.ToString()), Items.ItemType.Frame, 0);
			string text20 = "DuelFrame1Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text20, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelFrameMat1Tag = awaiter3.GetResult();
			Appearance.duelFrameMat1Tag.SetTexture("_ProfileFrameTex", duelFrame1Tag.texture);
			Items items17 = Program.items;
			string text21 = "WatchFrame0";
			item = Program.items.frames[0];
			Sprite watchFrame0 = await items17.LoadConcreteItemIconAsync(Config.Get(text21, item.id.ToString()), Items.ItemType.Frame, 0);
			string text22 = "WatchFrame0";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text22, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchFrameMat0 = awaiter3.GetResult();
			Appearance.watchFrameMat0.SetTexture("_ProfileFrameTex", watchFrame0.texture);
			Items items18 = Program.items;
			string text23 = "WatchFrame1";
			item = Program.items.frames[0];
			Sprite watchFrame = await items18.LoadConcreteItemIconAsync(Config.Get(text23, item.id.ToString()), Items.ItemType.Frame, 0);
			string text24 = "WatchFrame1";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text24, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchFrameMat1 = awaiter3.GetResult();
			Appearance.watchFrameMat1.SetTexture("_ProfileFrameTex", watchFrame.texture);
			Items items19 = Program.items;
			string text25 = "WatchFrame0Tag";
			item = Program.items.frames[0];
			Sprite watchFrame0Tag = await items19.LoadConcreteItemIconAsync(Config.Get(text25, item.id.ToString()), Items.ItemType.Frame, 0);
			string text26 = "WatchFrame0Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text26, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchFrameMat0Tag = awaiter3.GetResult();
			Appearance.watchFrameMat0Tag.SetTexture("_ProfileFrameTex", watchFrame0Tag.texture);
			Items items20 = Program.items;
			string text27 = "WatchFrame1Tag";
			item = Program.items.frames[0];
			Sprite watchFrame1Tag = await items20.LoadConcreteItemIconAsync(Config.Get(text27, item.id.ToString()), Items.ItemType.Frame, 0);
			string text28 = "WatchFrame1Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text28, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchFrameMat1Tag = awaiter3.GetResult();
			Appearance.watchFrameMat1Tag.SetTexture("_ProfileFrameTex", watchFrame1Tag.texture);
			Items items21 = Program.items;
			string text29 = "ReplayFrame0";
			item = Program.items.frames[0];
			Sprite replayFrame0 = await items21.LoadConcreteItemIconAsync(Config.Get(text29, item.id.ToString()), Items.ItemType.Frame, 0);
			string text30 = "ReplayFrame0";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text30, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayFrameMat0 = awaiter3.GetResult();
			Appearance.replayFrameMat0.SetTexture("_ProfileFrameTex", replayFrame0.texture);
			Items items22 = Program.items;
			string text31 = "ReplayFrame1";
			item = Program.items.frames[0];
			Sprite replayFrame = await items22.LoadConcreteItemIconAsync(Config.Get(text31, item.id.ToString()), Items.ItemType.Frame, 0);
			string text32 = "ReplayFrame1";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text32, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayFrameMat1 = awaiter3.GetResult();
			Appearance.replayFrameMat1.SetTexture("_ProfileFrameTex", replayFrame.texture);
			Items items23 = Program.items;
			string text33 = "ReplayFrame0Tag";
			item = Program.items.frames[0];
			Sprite replayFrame0Tag = await items23.LoadConcreteItemIconAsync(Config.Get(text33, item.id.ToString()), Items.ItemType.Frame, 0);
			string text34 = "ReplayFrame0Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text34, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayFrameMat0Tag = awaiter3.GetResult();
			Appearance.replayFrameMat0Tag.SetTexture("_ProfileFrameTex", replayFrame0Tag.texture);
			Items items24 = Program.items;
			string text35 = "ReplayFrame1Tag";
			item = Program.items.frames[0];
			Sprite replayFrame1Tag = await items24.LoadConcreteItemIconAsync(Config.Get(text35, item.id.ToString()), Items.ItemType.Frame, 0);
			string text36 = "ReplayFrame1Tag";
			item = Program.items.frames[0];
			awaiter3 = ABLoader.LoadFrameMaterial(Config.Get(text36, item.id.ToString())).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayFrameMat1Tag = awaiter3.GetResult();
			Appearance.replayFrameMat1Tag.SetTexture("_ProfileFrameTex", replayFrame1Tag.texture);
			string text37 = "DuelProtector0";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text37, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelProtector0 = awaiter3.GetResult();
			string text38 = "DuelProtector1";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text38, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelProtector1 = awaiter3.GetResult();
			string text39 = "DuelProtector0Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text39, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelProtector0Tag = awaiter3.GetResult();
			string text40 = "DuelProtector1Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text40, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.duelProtector1Tag = awaiter3.GetResult();
			string text41 = "WatchProtector0";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text41, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchProtector0 = awaiter3.GetResult();
			string text42 = "WatchProtector1";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text42, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchProtector1 = awaiter3.GetResult();
			string text43 = "WatchProtector0Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text43, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchProtector0Tag = awaiter3.GetResult();
			string text44 = "WatchProtector1Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text44, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.watchProtector1Tag = awaiter3.GetResult();
			string text45 = "ReplayProtector0";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text45, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayProtector0 = awaiter3.GetResult();
			string text46 = "ReplayProtector1";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text46, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayProtector1 = awaiter3.GetResult();
			string text47 = "ReplayProtector0Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text47, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayProtector0Tag = awaiter3.GetResult();
			string text48 = "ReplayProtector1Tag";
			item = Program.items.protectors[0];
			awaiter3 = ABLoader.LoadProtectorMaterial(Config.Get(text48, item.id.ToString()), default(CancellationToken)).GetAwaiter();
			if (!awaiter3.IsCompleted)
			{
				await awaiter3;
				awaiter3 = awaiter4;
				awaiter4 = default(UniTask<Material>.Awaiter);
			}
			Appearance.replayProtector1Tag = awaiter3.GetResult();
			Appearance.loaded = true;
			if (Program.instance.currentServant == Program.instance.room)
			{
				Program.instance.room.Realize();
			}
		}

		// Token: 0x0400CA89 RID: 51849
		[Header("Appearance")]
		[HideInInspector]
		public SelectionToggle_AppearanceGenre lastSelectedToggle;

		// Token: 0x0400CA8A RID: 51850
		[HideInInspector]
		public SelectionToggle_AppearanceItem lastSelectedItem;

		// Token: 0x0400CA8B RID: 51851
		public static Sprite duelFace0;

		// Token: 0x0400CA8C RID: 51852
		public static Sprite duelFace1;

		// Token: 0x0400CA8D RID: 51853
		public static Sprite watchFace0;

		// Token: 0x0400CA8E RID: 51854
		public static Sprite watchFace1;

		// Token: 0x0400CA8F RID: 51855
		public static Sprite replayFace0;

		// Token: 0x0400CA90 RID: 51856
		public static Sprite replayFace1;

		// Token: 0x0400CA91 RID: 51857
		public static Sprite duelFace0Tag;

		// Token: 0x0400CA92 RID: 51858
		public static Sprite duelFace1Tag;

		// Token: 0x0400CA93 RID: 51859
		public static Sprite watchFace0Tag;

		// Token: 0x0400CA94 RID: 51860
		public static Sprite watchFace1Tag;

		// Token: 0x0400CA95 RID: 51861
		public static Sprite replayFace0Tag;

		// Token: 0x0400CA96 RID: 51862
		public static Sprite replayFace1Tag;

		// Token: 0x0400CA97 RID: 51863
		public static Sprite defaultFace0;

		// Token: 0x0400CA98 RID: 51864
		public static Sprite defaultFace1;

		// Token: 0x0400CA99 RID: 51865
		public static Material duelFrameMat0;

		// Token: 0x0400CA9A RID: 51866
		public static Material duelFrameMat1;

		// Token: 0x0400CA9B RID: 51867
		public static Material watchFrameMat0;

		// Token: 0x0400CA9C RID: 51868
		public static Material watchFrameMat1;

		// Token: 0x0400CA9D RID: 51869
		public static Material replayFrameMat0;

		// Token: 0x0400CA9E RID: 51870
		public static Material replayFrameMat1;

		// Token: 0x0400CA9F RID: 51871
		public static Material duelFrameMat0Tag;

		// Token: 0x0400CAA0 RID: 51872
		public static Material duelFrameMat1Tag;

		// Token: 0x0400CAA1 RID: 51873
		public static Material watchFrameMat0Tag;

		// Token: 0x0400CAA2 RID: 51874
		public static Material watchFrameMat1Tag;

		// Token: 0x0400CAA3 RID: 51875
		public static Material replayFrameMat0Tag;

		// Token: 0x0400CAA4 RID: 51876
		public static Material replayFrameMat1Tag;

		// Token: 0x0400CAA5 RID: 51877
		public static Material duelProtector0;

		// Token: 0x0400CAA6 RID: 51878
		public static Material duelProtector1;

		// Token: 0x0400CAA7 RID: 51879
		public static Material watchProtector0;

		// Token: 0x0400CAA8 RID: 51880
		public static Material watchProtector1;

		// Token: 0x0400CAA9 RID: 51881
		public static Material replayProtector0;

		// Token: 0x0400CAAA RID: 51882
		public static Material replayProtector1;

		// Token: 0x0400CAAB RID: 51883
		public static Material duelProtector0Tag;

		// Token: 0x0400CAAC RID: 51884
		public static Material duelProtector1Tag;

		// Token: 0x0400CAAD RID: 51885
		public static Material watchProtector0Tag;

		// Token: 0x0400CAAE RID: 51886
		public static Material watchProtector1Tag;

		// Token: 0x0400CAAF RID: 51887
		public static Material replayProtector0Tag;

		// Token: 0x0400CAB0 RID: 51888
		public static Material replayProtector1Tag;

		// Token: 0x0400CAB1 RID: 51889
		public static Material matForFace;

		// Token: 0x0400CAB2 RID: 51890
		public static string player = "0";

		// Token: 0x0400CAB3 RID: 51891
		public const string meString = "Me";

		// Token: 0x0400CAB4 RID: 51892
		public const string opString = "Op";

		// Token: 0x0400CAB5 RID: 51893
		public const string meTagString = "MeTag";

		// Token: 0x0400CAB6 RID: 51894
		public const string opTagString = "OpTag";

		// Token: 0x0400CAB7 RID: 51895
		public static Appearance.Condition condition = Appearance.Condition.Duel;

		// Token: 0x0400CAB8 RID: 51896
		public static bool loaded;

		// Token: 0x020012CA RID: 4810
		public enum Condition
		{
			// Token: 0x0400CABA RID: 51898
			Duel,
			// Token: 0x0400CABB RID: 51899
			Watch,
			// Token: 0x0400CABC RID: 51900
			Replay,
			// Token: 0x0400CABD RID: 51901
			DeckEditor
		}
	}
}

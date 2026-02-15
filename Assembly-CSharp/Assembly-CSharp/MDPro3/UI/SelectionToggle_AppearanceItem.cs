using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013B4 RID: 5044
	public class SelectionToggle_AppearanceItem : SelectionToggle
	{
		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06009236 RID: 37430 RVA: 0x0014670C File Offset: 0x0014490C
		private CanvasGroup CG
		{
			get
			{
				return this.m_CG = ((this.m_CG != null) ? this.m_CG : base.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06009237 RID: 37431 RVA: 0x00146740 File Offset: 0x00144940
		private RawImage Protector
		{
			get
			{
				return this.m_Protector = ((this.m_Protector != null) ? this.m_Protector : base.Manager.GetElement<RawImage>("Protector"));
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06009238 RID: 37432 RVA: 0x0014677C File Offset: 0x0014497C
		private Image WallpaperBG
		{
			get
			{
				return this.m_WallpaperBG = ((this.m_WallpaperBG != null) ? this.m_WallpaperBG : base.Manager.GetElement<Image>("WallpaperBG"));
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06009239 RID: 37433 RVA: 0x001467B8 File Offset: 0x001449B8
		private GridLayoutGroup Grid
		{
			get
			{
				return this.m_Grid = ((this.m_Grid != null) ? this.m_Grid : Program.instance.appearance.GetUI<AppearanceUI>().ScrollRect.content.GetComponent<GridLayoutGroup>());
			}
		}

		// Token: 0x0600923A RID: 37434 RVA: 0x00146802 File Offset: 0x00144A02
		protected override void Awake()
		{
			base.Awake();
			this.HoverOff(false);
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.manuallySetNavigation = false;
		}

		// Token: 0x0600923B RID: 37435 RVA: 0x00146826 File Offset: 0x00144A26
		public void Refresh()
		{
			this.RefreshAsync();
		}

		// Token: 0x0600923C RID: 37436 RVA: 0x00146830 File Offset: 0x00144A30
		private async UniTask RefreshAsync()
		{
			for (int i = 0; i < this.index; i++)
			{
				await UniTask.Yield();
			}
			if (this.path.StartsWith("Protector"))
			{
				RawImage rawImage = this.Protector;
				rawImage.material = await ABLoader.LoadProtectorMaterial(this.itemID.ToString(), base.destroyCancellationToken);
				rawImage = null;
				this.Protector.material.renderQueue = 3000;
				this.Protector.color = Color.white;
				base.Icon.gameObject.SetActive(false);
			}
			else if (this.path.Length > 0)
			{
				Image image = base.Icon;
				image.sprite = await Program.items.LoadItemIconAsync(this.itemID.ToString(), Items.ItemType.Unknown);
				image = null;
				if (base.Manager == null)
				{
					return;
				}
				base.Icon.color = Color.white;
				if (this.path.StartsWith("ProfileFrame"))
				{
					base.Icon.rectTransform.localScale = Vector3.one * 0.8f;
					image = base.Icon;
					image.material = await ABLoader.LoadFrameMaterial(this.itemID.ToString());
					image = null;
					base.Icon.material.SetTexture("_ProfileFrameTex", base.Icon.sprite.texture);
					base.Icon.sprite = TextureManager.container.black;
					base.Icon.color = Color.white;
				}
				else if (this.path.StartsWith("DeckCase"))
				{
					base.Icon.transform.localPosition = new Vector3(0f, 15f, 0f);
				}
				else if (this.path.StartsWith("WallPaperIcon"))
				{
					this.WallpaperBG.gameObject.SetActive(true);
				}
				this.Protector.gameObject.SetActive(false);
			}
			else
			{
				Texture2D art = await CardImageLoader.LoadArtAsync(this.itemID, true, base.destroyCancellationToken);
				base.Icon.color = Color.white;
				base.Icon.sprite = TextureManager.Texture2Sprite(art);
				this.Protector.gameObject.SetActive(false);
			}
			if (this.path.StartsWith("ProfileIcon"))
			{
				base.Icon.material = Appearance.matForFace;
			}
			this.loaded = true;
			this.refreshCoroutine = null;
		}

		// Token: 0x0600923D RID: 37437 RVA: 0x00146873 File Offset: 0x00144A73
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			Program.instance.appearance.GetUI<AppearanceUI>().SetHoverText(this.itemName);
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x00146898 File Offset: 0x00144A98
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			this.CallHoverOnEvent();
			Program.instance.appearance.GetUI<AppearanceUI>().Detail.SetItem(this.itemID, this.itemName, this.description, this.path == string.Empty);
			Program.instance.appearance.GetUI<AppearanceUI>().SetHoverText(this.itemName);
			Program.instance.appearance.lastSelectedItem = this;
			Program.instance.currentServant.lastSelectable = base.Selectable;
			base.GetSelectable().Select();
			if (Appearance.condition == Appearance.Condition.DeckEditor)
			{
				if (this.path.StartsWith("DeckCase"))
				{
					if (DeckEditor.Deck.Case != this.itemID)
					{
						DeckEditor.Deck.Case = this.itemID;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
						Program.instance.deckEditor.GetUI<DeckEditorUI>().IconCase.sprite = base.Icon.sprite;
					}
				}
				else if (this.path.StartsWith("Protector"))
				{
					if (DeckEditor.Deck.Protector != this.itemID)
					{
						DeckEditor.Deck.Protector = this.itemID;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
						Program.instance.deckEditor.GetUI<DeckEditorUI>().IconProtector.material = this.Protector.material;
					}
				}
				else if (this.path.StartsWith("FieldIcon"))
				{
					if (DeckEditor.Deck.Field != this.itemID)
					{
						DeckEditor.Deck.Field = this.itemID;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
						Program.instance.deckEditor.GetUI<DeckEditorUI>().IconField.sprite = base.Icon.sprite;
					}
				}
				else if (this.path.StartsWith("FieldObj"))
				{
					if (DeckEditor.Deck.Grave != this.itemID)
					{
						DeckEditor.Deck.Grave = this.itemID;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
						Program.instance.deckEditor.GetUI<DeckEditorUI>().IconGrave.sprite = base.Icon.sprite;
					}
				}
				else if (this.path.StartsWith("FieldAvatarBase"))
				{
					if (DeckEditor.Deck.Stand != this.itemID)
					{
						DeckEditor.Deck.Stand = this.itemID;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
						Program.instance.deckEditor.GetUI<DeckEditorUI>().IconStand.sprite = base.Icon.sprite;
					}
				}
				else if (DeckEditor.Deck.Mate != this.itemID)
				{
					DeckEditor.Deck.Mate = this.itemID;
					Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
					Program.instance.deckEditor.GetUI<DeckEditorUI>().IconMate.sprite = base.Icon.sprite;
				}
			}
			else if (AppearanceUI.currentContent == "Wallpaper")
			{
				Config.Set("Wallpaper", this.itemID.ToString());
			}
			else
			{
				Config.Set(Appearance.condition.ToString() + AppearanceUI.currentContent + Appearance.player, this.itemID.ToString());
			}
			base.StartCoroutine(this.ConfigSetAsync());
		}

		// Token: 0x0600923F RID: 37439 RVA: 0x00146C6C File Offset: 0x00144E6C
		private IEnumerator ConfigSetAsync()
		{
			while (!this.loaded)
			{
				yield return null;
			}
			if (!base.Icon.gameObject.activeSelf)
			{
				if (Appearance.player == "0")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelProtector0 = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchProtector0 = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayProtector0 = this.Protector.material;
					}
				}
				else if (Appearance.player == "1")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelProtector1 = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchProtector1 = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayProtector1 = this.Protector.material;
					}
				}
				else if (Appearance.player == "0Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelProtector0Tag = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchProtector0Tag = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayProtector0Tag = this.Protector.material;
					}
				}
				else if (Appearance.player == "1Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelProtector1Tag = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchProtector1Tag = this.Protector.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayProtector1Tag = this.Protector.material;
					}
				}
			}
			else if (this.path.StartsWith("ProfileIcon"))
			{
				if (Appearance.player == "0")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFace0 = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFace0 = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFace0 = base.Icon.sprite;
					}
				}
				else if (Appearance.player == "1")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFace1 = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFace1 = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFace1 = base.Icon.sprite;
					}
				}
				else if (Appearance.player == "0Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFace0Tag = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFace0Tag = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFace0Tag = base.Icon.sprite;
					}
				}
				else if (Appearance.player == "1Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFace1Tag = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFace1Tag = base.Icon.sprite;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFace1Tag = base.Icon.sprite;
					}
				}
			}
			else if (this.path.StartsWith("ProfileFrame"))
			{
				if (Appearance.player == "0")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFrameMat0 = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFrameMat0 = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFrameMat0 = base.Icon.material;
					}
				}
				else if (Appearance.player == "1")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFrameMat1 = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFrameMat1 = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFrameMat1 = base.Icon.material;
					}
				}
				else if (Appearance.player == "0Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFrameMat0Tag = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFrameMat0Tag = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFrameMat0Tag = base.Icon.material;
					}
				}
				else if (Appearance.player == "1Tag")
				{
					if (Appearance.condition == Appearance.Condition.Duel)
					{
						Appearance.duelFrameMat1Tag = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Watch)
					{
						Appearance.watchFrameMat1Tag = base.Icon.material;
					}
					else if (Appearance.condition == Appearance.Condition.Replay)
					{
						Appearance.replayFrameMat1Tag = base.Icon.material;
					}
				}
			}
			yield break;
		}

		// Token: 0x06009240 RID: 37440 RVA: 0x00146C7B File Offset: 0x00144E7B
		protected override void OnClick()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			this.SetToggleOn(true);
			Program.instance.currentServant.lastSelectable = base.Selectable;
		}

		// Token: 0x06009241 RID: 37441 RVA: 0x00146CA9 File Offset: 0x00144EA9
		protected override int GetButtonsCount()
		{
			return Program.instance.appearance.GetUI<AppearanceUI>().GetCurrentGenreCount();
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x00146CC0 File Offset: 0x00144EC0
		protected override int GetColumnsCount()
		{
			return this.Grid.Size().x;
		}

		// Token: 0x06009243 RID: 37443 RVA: 0x00146CE0 File Offset: 0x00144EE0
		public void Hide()
		{
			if (this.hideCoroutine != null || !base.gameObject.activeSelf)
			{
				return;
			}
			this.hideCoroutine = base.StartCoroutine(this.HideAsync());
			base.GetComponent<LayoutElement>().ignoreLayout = true;
			base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}

		// Token: 0x06009244 RID: 37444 RVA: 0x00146D31 File Offset: 0x00144F31
		private IEnumerator HideAsync()
		{
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = false;
			while (!this.loaded)
			{
				yield return null;
			}
			this.hideCoroutine = null;
			base.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x00146D40 File Offset: 0x00144F40
		public void Show()
		{
			if (this.hideCoroutine != null)
			{
				base.StopCoroutine(this.hideCoroutine);
				this.hideCoroutine = null;
			}
			base.gameObject.SetActive(true);
			this.CG.alpha = 1f;
			this.CG.blocksRaycasts = true;
			base.GetComponent<LayoutElement>().ignoreLayout = false;
			base.transform.SetSiblingIndex(this.index);
		}

		// Token: 0x06009246 RID: 37446 RVA: 0x00146DAD File Offset: 0x00144FAD
		public void Dispose()
		{
			if (this.refreshCoroutine != null)
			{
				base.StopCoroutine(this.refreshCoroutine);
			}
			if (this.hideCoroutine != null)
			{
				base.StopCoroutine(this.hideCoroutine);
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0400D0BD RID: 53437
		private CanvasGroup m_CG;

		// Token: 0x0400D0BE RID: 53438
		private const string LABEL_RIMG_PROTECTOR = "Protector";

		// Token: 0x0400D0BF RID: 53439
		private RawImage m_Protector;

		// Token: 0x0400D0C0 RID: 53440
		private const string LABEL_IMG_WALLPAPER_BG = "WallpaperBG";

		// Token: 0x0400D0C1 RID: 53441
		private Image m_WallpaperBG;

		// Token: 0x0400D0C2 RID: 53442
		private GridLayoutGroup m_Grid;

		// Token: 0x0400D0C3 RID: 53443
		public int itemID;

		// Token: 0x0400D0C4 RID: 53444
		public string itemName;

		// Token: 0x0400D0C5 RID: 53445
		public string description;

		// Token: 0x0400D0C6 RID: 53446
		public string path;

		// Token: 0x0400D0C7 RID: 53447
		private bool loaded;

		// Token: 0x0400D0C8 RID: 53448
		private Coroutine refreshCoroutine;

		// Token: 0x0400D0C9 RID: 53449
		private Coroutine hideCoroutine;
	}
}

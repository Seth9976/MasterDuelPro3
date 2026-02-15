using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013A8 RID: 5032
	public class SelectionButton_DeckSelector : SelectionButton
	{
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060091C6 RID: 37318 RVA: 0x0014419C File Offset: 0x0014239C
		private TextMeshProUGUI TextDeckName
		{
			get
			{
				return this.m_TextDeckName = ((this.m_TextDeckName != null) ? this.m_TextDeckName : base.Manager.GetElement<TextMeshProUGUI>("TextDeckName"));
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x060091C7 RID: 37319 RVA: 0x001441D8 File Offset: 0x001423D8
		private Image ImageDeck
		{
			get
			{
				return this.m_ImageDeck = ((this.m_ImageDeck != null) ? this.m_ImageDeck : base.Manager.GetElement<Image>("DeckImage"));
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060091C8 RID: 37320 RVA: 0x00144214 File Offset: 0x00142414
		private RawImage ImageCard0
		{
			get
			{
				return this.m_ImageCard0 = ((this.m_ImageCard0 != null) ? this.m_ImageCard0 : base.Manager.GetElement<RawImage>("CardImage0"));
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060091C9 RID: 37321 RVA: 0x00144250 File Offset: 0x00142450
		private RectTransform CardPos0RT
		{
			get
			{
				return this.m_CardPos0RT = ((this.m_CardPos0RT != null) ? this.m_CardPos0RT : base.Manager.GetElement<RectTransform>("CardImage0"));
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060091CA RID: 37322 RVA: 0x0014428C File Offset: 0x0014248C
		private RawImage ImageCard1
		{
			get
			{
				return this.m_ImageCard1 = ((this.m_ImageCard1 != null) ? this.m_ImageCard1 : base.Manager.GetElement<RawImage>("CardImage1"));
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060091CB RID: 37323 RVA: 0x001442C8 File Offset: 0x001424C8
		private RectTransform CardPos1RT
		{
			get
			{
				return this.m_CardPos1RT = ((this.m_CardPos1RT != null) ? this.m_CardPos1RT : base.Manager.GetElement<RectTransform>("CardImage1"));
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060091CC RID: 37324 RVA: 0x00144304 File Offset: 0x00142504
		private RawImage ImageCard2
		{
			get
			{
				return this.m_ImageCard2 = ((this.m_ImageCard2 != null) ? this.m_ImageCard2 : base.Manager.GetElement<RawImage>("CardImage2"));
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060091CD RID: 37325 RVA: 0x00144340 File Offset: 0x00142540
		private RectTransform CardPos2RT
		{
			get
			{
				return this.m_CardPos2RT = ((this.m_CardPos2RT != null) ? this.m_CardPos2RT : base.Manager.GetElement<RectTransform>("CardImage2"));
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060091CE RID: 37326 RVA: 0x0014437C File Offset: 0x0014257C
		private CanvasGroup CardPos0
		{
			get
			{
				return this.m_CardPos0 = ((this.m_CardPos0 != null) ? this.m_CardPos0 : base.Manager.GetElement<CanvasGroup>("CardPos0"));
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x060091CF RID: 37327 RVA: 0x001443B8 File Offset: 0x001425B8
		private CanvasGroup CardPos1
		{
			get
			{
				return this.m_CardPos1 = ((this.m_CardPos1 != null) ? this.m_CardPos1 : base.Manager.GetElement<CanvasGroup>("CardPos1"));
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x060091D0 RID: 37328 RVA: 0x001443F4 File Offset: 0x001425F4
		private CanvasGroup CardPos2
		{
			get
			{
				return this.m_CardPos2 = ((this.m_CardPos2 != null) ? this.m_CardPos2 : base.Manager.GetElement<CanvasGroup>("CardPos2"));
			}
		}

		// Token: 0x060091D1 RID: 37329 RVA: 0x00144430 File Offset: 0x00142630
		protected override void Awake()
		{
			base.Awake();
			this.HidePickup();
		}

		// Token: 0x060091D2 RID: 37330 RVA: 0x0014443E File Offset: 0x0014263E
		private void OnEnable()
		{
			if (!this.refreshed)
			{
				this.SetDeck(this.deck, this.TextDeckName.text);
			}
		}

		// Token: 0x060091D3 RID: 37331 RVA: 0x0014445F File Offset: 0x0014265F
		protected override void OnDisable()
		{
			base.OnDisable();
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			CancellationTokenSource cancellationTokenSource2 = this.cts;
			if (cancellationTokenSource2 == null)
			{
				return;
			}
			cancellationTokenSource2.Dispose();
		}

		// Token: 0x060091D4 RID: 37332 RVA: 0x00144488 File Offset: 0x00142688
		public void SetConfigDeck(string hint)
		{
			string configDeck = Config.GetConfigDeckName(true);
			string path = "Deck/" + configDeck + ".ydk";
			if (!File.Exists(path))
			{
				this.SetDeck(null, hint);
				return;
			}
			this.SetDeck(new Deck(path), configDeck);
		}

		// Token: 0x060091D5 RID: 37333 RVA: 0x001444CB File Offset: 0x001426CB
		public void SetDeck(Deck deck, string deckName)
		{
			this.deck = deck;
			this.deckName = deckName;
			if (base.gameObject.activeInHierarchy)
			{
				this.SetDeckInteral();
			}
		}

		// Token: 0x060091D6 RID: 37334 RVA: 0x001444F0 File Offset: 0x001426F0
		private void SetDeckInteral()
		{
			if (this.deckName.StartsWith("/"))
			{
				this.TextDeckName.text = Path.GetFileNameWithoutExtension(this.deckName);
			}
			else
			{
				this.TextDeckName.text = this.deckName;
			}
			if (this.deck == null)
			{
				this.RefreshAsync(1080001, 1070001, 0, 0, 0);
				return;
			}
			this.RefreshAsync(this.deck.Case, this.deck.Protector, (this.deck.Pickup.Count > 0) ? this.deck.Pickup[0] : 0, (this.deck.Pickup.Count > 1) ? this.deck.Pickup[1] : 0, (this.deck.Pickup.Count > 2) ? this.deck.Pickup[2] : 0);
		}

		// Token: 0x060091D7 RID: 37335 RVA: 0x001445E8 File Offset: 0x001427E8
		private async UniTask RefreshAsync(int deckCase = 1080001, int protector = 1070001, int card0 = 0, int card1 = 0, int card2 = 0)
		{
			this.refreshed = false;
			this.cts = new CancellationTokenSource();
			await UniTask.WaitUntil(() => Items.initialized, PlayerLoopTiming.Update, this.cts.Token, false);
			await UniTask.WaitWhile(() => TextureManager.container == null, PlayerLoopTiming.Update, this.cts.Token, false);
			this.ImageDeck.color = Color.clear;
			this.ImageCard0.color = Color.clear;
			this.ImageCard1.color = Color.clear;
			this.ImageCard2.color = Color.clear;
			Image image = this.ImageDeck;
			image.sprite = await Program.items.LoadDeckCaseIconAsync(deckCase, "_L_SD");
			image = null;
			this.ImageDeck.color = Color.white;
			if (card0 == 0)
			{
				this.ImageCard0.texture = null;
				RawImage rawImage = this.ImageCard0;
				rawImage.material = await ABLoader.LoadProtectorMaterial(protector.ToString(), this.cts.Token);
				rawImage = null;
			}
			else
			{
				this.ImageCard0.material = MaterialLoader.GetCardMaterial(card0, false);
				RawImage rawImage = this.ImageCard0;
				rawImage.texture = await CardImageLoader.LoadCardAsync(card0, true, default(CancellationToken), false);
				rawImage = null;
			}
			this.ImageCard0.color = Color.white;
			if (card1 == 0)
			{
				this.ImageCard1.texture = null;
				RawImage rawImage = this.ImageCard1;
				rawImage.material = await ABLoader.LoadProtectorMaterial(protector.ToString(), this.cts.Token);
				rawImage = null;
			}
			else
			{
				this.ImageCard1.material = MaterialLoader.GetCardMaterial(card1, false);
				RawImage rawImage = this.ImageCard1;
				rawImage.texture = await CardImageLoader.LoadCardAsync(card1, true, default(CancellationToken), false);
				rawImage = null;
			}
			this.ImageCard1.color = Color.white;
			if (card2 == 0)
			{
				this.ImageCard2.texture = null;
				RawImage rawImage = this.ImageCard2;
				rawImage.material = await ABLoader.LoadProtectorMaterial(protector.ToString(), this.cts.Token);
				rawImage = null;
			}
			else
			{
				this.ImageCard2.material = MaterialLoader.GetCardMaterial(card2, false);
				RawImage rawImage = this.ImageCard2;
				rawImage.texture = await CardImageLoader.LoadCardAsync(card2, true, default(CancellationToken), false);
				rawImage = null;
			}
			this.ImageCard2.color = Color.white;
			this.refreshed = true;
		}

		// Token: 0x060091D8 RID: 37336 RVA: 0x00144655 File Offset: 0x00142855
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			this.ShowPickup();
		}

		// Token: 0x060091D9 RID: 37337 RVA: 0x00144663 File Offset: 0x00142863
		protected override void CallHoverOffEvent()
		{
			base.CallHoverOffEvent();
			this.HidePickup();
		}

		// Token: 0x060091DA RID: 37338 RVA: 0x00144674 File Offset: 0x00142874
		private void ShowPickup()
		{
			foreach (Tweener tween in this.pickdownTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.pickdownTweens.Clear();
			TweenerCore<float, float, FloatOptions> tween2 = this.CardPos0.DOFade(1f, 0.2f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween2);
			TweenerCore<float, float, FloatOptions> tween3 = this.CardPos1.DOFade(1f, 0.22f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween3);
			TweenerCore<float, float, FloatOptions> tween4 = this.CardPos2.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween4);
			TweenerCore<Vector3, Vector3, VectorOptions> tween5 = this.CardPos0RT.DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.2f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween5);
			TweenerCore<Vector3, Vector3, VectorOptions> tween6 = this.CardPos1RT.DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.22f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween6);
			TweenerCore<Vector3, Vector3, VectorOptions> tween7 = this.CardPos2RT.DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.24f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween7);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween8 = this.CardPos0RT.DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween8);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween9 = this.CardPos2RT.DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween9);
		}

		// Token: 0x060091DB RID: 37339 RVA: 0x00144860 File Offset: 0x00142A60
		private void HidePickup()
		{
			foreach (Tweener tween in this.pickupTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.pickupTweens.Clear();
			TweenerCore<float, float, FloatOptions> tween2 = this.CardPos0.DOFade(0f, 0.2f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween2);
			TweenerCore<float, float, FloatOptions> tween3 = this.CardPos1.DOFade(0f, 0.22f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween3);
			TweenerCore<float, float, FloatOptions> tween4 = this.CardPos2.DOFade(0f, 0.24f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween4);
			TweenerCore<Vector3, Vector3, VectorOptions> tween5 = this.CardPos0RT.DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.2f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween5);
			TweenerCore<Vector3, Vector3, VectorOptions> tween6 = this.CardPos1RT.DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.22f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween6);
			TweenerCore<Vector3, Vector3, VectorOptions> tween7 = this.CardPos2RT.DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.24f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween7);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween8 = this.CardPos0RT.DOLocalRotate(new Vector3(0f, 0f, -20f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween8);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween9 = this.CardPos2RT.DOLocalRotate(new Vector3(0f, 0f, 20f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween9);
		}

		// Token: 0x0400D069 RID: 53353
		private const string LABEL_TEXT_DECK_NAME = "TextDeckName";

		// Token: 0x0400D06A RID: 53354
		private TextMeshProUGUI m_TextDeckName;

		// Token: 0x0400D06B RID: 53355
		private const string LABEL_IMG_DECK = "DeckImage";

		// Token: 0x0400D06C RID: 53356
		private Image m_ImageDeck;

		// Token: 0x0400D06D RID: 53357
		private const string LABEL_RIMG_CARD0 = "CardImage0";

		// Token: 0x0400D06E RID: 53358
		private RawImage m_ImageCard0;

		// Token: 0x0400D06F RID: 53359
		private RectTransform m_CardPos0RT;

		// Token: 0x0400D070 RID: 53360
		private const string LABEL_RIMG_CARD1 = "CardImage1";

		// Token: 0x0400D071 RID: 53361
		private RawImage m_ImageCard1;

		// Token: 0x0400D072 RID: 53362
		private RectTransform m_CardPos1RT;

		// Token: 0x0400D073 RID: 53363
		private const string LABEL_RIMG_CARD2 = "CardImage2";

		// Token: 0x0400D074 RID: 53364
		private RawImage m_ImageCard2;

		// Token: 0x0400D075 RID: 53365
		private RectTransform m_CardPos2RT;

		// Token: 0x0400D076 RID: 53366
		private const string LABEL_CG_CARD_POS0 = "CardPos0";

		// Token: 0x0400D077 RID: 53367
		private CanvasGroup m_CardPos0;

		// Token: 0x0400D078 RID: 53368
		private const string LABEL_CG_CARD_POS1 = "CardPos1";

		// Token: 0x0400D079 RID: 53369
		private CanvasGroup m_CardPos1;

		// Token: 0x0400D07A RID: 53370
		private const string LABEL_CG_CARD_POS2 = "CardPos2";

		// Token: 0x0400D07B RID: 53371
		private CanvasGroup m_CardPos2;

		// Token: 0x0400D07C RID: 53372
		private readonly List<Tweener> pickupTweens = new List<Tweener>();

		// Token: 0x0400D07D RID: 53373
		private readonly List<Tweener> pickdownTweens = new List<Tweener>();

		// Token: 0x0400D07E RID: 53374
		private CancellationTokenSource cts;

		// Token: 0x0400D07F RID: 53375
		private Deck deck;

		// Token: 0x0400D080 RID: 53376
		private string deckName;

		// Token: 0x0400D081 RID: 53377
		private bool refreshed;
	}
}

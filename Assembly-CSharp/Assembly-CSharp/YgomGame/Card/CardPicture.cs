using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Card
{
	// Token: 0x02001108 RID: 4360
	public class CardPicture : MonoBehaviour
	{
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060081BD RID: 33213 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject cachedGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060081BE RID: 33214 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060081BF RID: 33215 RVA: 0x0000216D File Offset: 0x0000036D
		public static float FontSize
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060081C0 RID: 33216 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060081C1 RID: 33217 RVA: 0x0000216D File Offset: 0x0000036D
		public static float FontSize_P
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPicture Create(Transform parent)
		{
			return null;
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateAsync(Transform parent, Action<CardPicture> callback, bool bestFit = false)
		{
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetUpCardText(int mrk)
		{
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupCardAsync(int mrk, UnityAction<bool> onFinished)
		{
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupText(int mrk, bool maskmode)
		{
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x0000216A File Offset: 0x0000036A
		public RawImage GetImage()
		{
			return null;
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFrame(int mrk)
		{
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupIcon(int cardid, bool maskmode)
		{
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAttrIcon(int cardid)
		{
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMagicTypeIcon(int cardid)
		{
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLinkIcon(int linkmask)
		{
		}

		// Token: 0x060081CF RID: 33231 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUpCardIllustAsync(int mrk, UnityAction onFinished)
		{
		}

		// Token: 0x0400BA18 RID: 47640
		public CardPictureTop topArea;

		// Token: 0x0400BA19 RID: 47641
		public Transform mainArea;

		// Token: 0x0400BA1A RID: 47642
		public Transform bottomArea;

		// Token: 0x0400BA1B RID: 47643
		private const float MAGICTYPETAGBIAS_GENERIC = -67.48f;

		// Token: 0x0400BA1C RID: 47644
		private const float MAGICTYPETAGBIAS_CH = -50f;

		// Token: 0x0400BA1D RID: 47645
		private const float MAGICTYPETAGBIAS_EN = -76.3f;

		// Token: 0x0400BA1E RID: 47646
		private const int PENDULUMTAGINDEX = 3;

		// Token: 0x0400BA1F RID: 47647
		[SerializeField]
		private RawImage cardframe;

		// Token: 0x0400BA20 RID: 47648
		[SerializeField]
		private RawImage illust_normal;

		// Token: 0x0400BA21 RID: 47649
		[SerializeField]
		private RawImage illust_pendulum;

		// Token: 0x0400BA22 RID: 47650
		[SerializeField]
		private ExtendedTextMeshProUGUI attrRuby;

		// Token: 0x0400BA23 RID: 47651
		[SerializeField]
		private ExtendedTextMeshProUGUI cardText;

		// Token: 0x0400BA24 RID: 47652
		[SerializeField]
		private ExtendedTextMeshProUGUI pdlmText;

		// Token: 0x0400BA25 RID: 47653
		[SerializeField]
		private MDText pdlmScaleL;

		// Token: 0x0400BA26 RID: 47654
		[SerializeField]
		private MDText pdlmScaleR;

		// Token: 0x0400BA27 RID: 47655
		[SerializeField]
		private GameObject nonEffectArea;

		// Token: 0x0400BA28 RID: 47656
		[SerializeField]
		private GameObject atkRoot;

		// Token: 0x0400BA29 RID: 47657
		[SerializeField]
		private GameObject defRoot;

		// Token: 0x0400BA2A RID: 47658
		[SerializeField]
		private MDText atkText;

		// Token: 0x0400BA2B RID: 47659
		[SerializeField]
		private MDText defText;

		// Token: 0x0400BA2C RID: 47660
		[SerializeField]
		private Image linkNum;

		// Token: 0x0400BA2D RID: 47661
		[SerializeField]
		private ExtendedTextMeshProUGUI spelltrapText;

		// Token: 0x0400BA2E RID: 47662
		[SerializeField]
		private Sprite[] AttrSprites;

		// Token: 0x0400BA2F RID: 47663
		[SerializeField]
		private Sprite[] IconSprites;

		// Token: 0x0400BA30 RID: 47664
		[SerializeField]
		private Image AttrIcon;

		// Token: 0x0400BA31 RID: 47665
		[SerializeField]
		private Image SpellTrapIcon;

		// Token: 0x0400BA32 RID: 47666
		[SerializeField]
		private GameObject LinkRoot;

		// Token: 0x0400BA33 RID: 47667
		[SerializeField]
		private GameObject LinkDarkRoot;

		// Token: 0x0400BA34 RID: 47668
		[SerializeField]
		private Image[] LinkIcons;

		// Token: 0x0400BA35 RID: 47669
		[SerializeField]
		private Image Separator;

		// Token: 0x0400BA36 RID: 47670
		private bool materialSetuped;

		// Token: 0x0400BA37 RID: 47671
		private GameObject _cachedGo;

		// Token: 0x0400BA38 RID: 47672
		private int cardId;

		// Token: 0x0400BA39 RID: 47673
		private const string prefabPath = "Prefabs/Duel/CardPicture";

		// Token: 0x0400BA3A RID: 47674
		private const string question = "?";

		// Token: 0x0400BA3B RID: 47675
		private static CardPicture instance;
	}
}

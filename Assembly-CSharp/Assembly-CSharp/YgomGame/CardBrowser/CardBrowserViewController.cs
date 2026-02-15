using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.CardBrowser
{
	// Token: 0x020010E5 RID: 4325
	public class CardBrowserViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x060080AD RID: 32941 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsValidIdx(int idx)
		{
			return false;
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEnablePrev()
		{
			return false;
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEnableNext()
		{
			return false;
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenDL(int mrk, int styleid = 1, int regulationId = -1, Action action = null)
		{
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int mrk, int styleid = 1, int regulationId = -1, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int idx, IReadOnlyList<int> mrks, IReadOnlyList<int> styleIds = null, int regulationId = -1, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int idx, List<object> mrks, List<object> styleIds = null, int regulationId = -1, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open2DL(int idx, List<object> mrks, List<object> styleIds = null, int regulationId = -1, Action action = null)
		{
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeContent(Action onComplete)
		{
			return null;
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity)
		{
		}

		// Token: 0x060080BC RID: 32956 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSetEntity(GameObject entity, int idx)
		{
		}

		// Token: 0x060080BD RID: 32957 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollValueChanged(Vector2 normalize)
		{
		}

		// Token: 0x060080BE RID: 32958 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ToNextPage()
		{
			return false;
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ToPrevPage()
		{
			return false;
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNext()
		{
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPrev()
		{
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged()
		{
		}

		// Token: 0x0400B8F5 RID: 47349
		private const string k_ArgKeyStartIdx = "startIdx";

		// Token: 0x0400B8F6 RID: 47350
		private const string k_ArgKeyMrks = "mrks";

		// Token: 0x0400B8F7 RID: 47351
		private const string k_ArgKeyStyleIds = "styleIds";

		// Token: 0x0400B8F8 RID: 47352
		private const string k_ArgKeyRegulationId = "regulationId";

		// Token: 0x0400B8F9 RID: 47353
		private const string k_ArgKeyPrefab = "prefab";

		// Token: 0x0400B8FA RID: 47354
		private const string k_ArgKeyOpenOnContent = "openContent";

		// Token: 0x0400B8FB RID: 47355
		private const string k_ArgKeyOnCreatedViewCallBack = "DownloadCallBack";

		// Token: 0x0400B8FC RID: 47356
		internal const string k_ArgKeySwap = "swap";

		// Token: 0x0400B8FD RID: 47357
		internal const string k_ArgKey_SkipSwapTransition = "SkipSwapTransition";

		// Token: 0x0400B8FE RID: 47358
		internal const string k_ArgRequestCardInfo = "requestCardInfo";

		// Token: 0x0400B8FF RID: 47359
		internal const string k_ArgShowReleasedDate = "showReleasedDate";

		// Token: 0x0400B900 RID: 47360
		internal const string k_ArgShowRelativeCard = "showRelativeCard";

		// Token: 0x0400B901 RID: 47361
		private readonly string k_ELabelCloseButton;

		// Token: 0x0400B902 RID: 47362
		private readonly string k_ELabelPrevButton;

		// Token: 0x0400B903 RID: 47363
		private readonly string k_ELabelNextButton;

		// Token: 0x0400B904 RID: 47364
		private readonly string k_ELabelScrollView;

		// Token: 0x0400B905 RID: 47365
		private List<CardBrowserViewController.CardContext> m_CardContexts;

		// Token: 0x0400B906 RID: 47366
		private int m_RegulationId;

		// Token: 0x0400B907 RID: 47367
		private SelectionButton m_PrevButton;

		// Token: 0x0400B908 RID: 47368
		private SelectionButton m_NextButton;

		// Token: 0x0400B909 RID: 47369
		private bool m_IsReady;

		// Token: 0x0400B90A RID: 47370
		private bool m_Aborted;

		// Token: 0x0400B90B RID: 47371
		private bool downloadPrefab;

		// Token: 0x0400B90C RID: 47372
		private SnapContentManager m_SnapContentManager;

		// Token: 0x0400B90D RID: 47373
		private Dictionary<GameObject, CardBrowserViewController.CardDetailWidget> m_EntityMap;

		// Token: 0x0400B90E RID: 47374
		private List<CardInfoDetail> m_RentedCardInfoDetails;

		// Token: 0x0400B90F RID: 47375
		private bool m_Dirty;

		// Token: 0x020010E6 RID: 4326
		private class CardContext
		{
			// Token: 0x060080C4 RID: 32964 RVA: 0x00002739 File Offset: 0x00000939
			public CardContext(int mrk, int styleId)
			{
			}

			// Token: 0x0400B910 RID: 47376
			public readonly int mrk;

			// Token: 0x0400B911 RID: 47377
			public readonly int styleId;

			// Token: 0x0400B912 RID: 47378
			public string cardBottomText;
		}

		// Token: 0x020010E7 RID: 4327
		private class CardDetailWidget
		{
			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x060080C5 RID: 32965 RVA: 0x0000216A File Offset: 0x0000036A
			public Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x060080C6 RID: 32966 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject bg
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x060080C7 RID: 32967 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject headerArea
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x060080C8 RID: 32968 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject backButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060080C9 RID: 32969 RVA: 0x00002739 File Offset: 0x00000939
			public CardDetailWidget(CardInfoDetail cardInfoDetail)
			{
			}

			// Token: 0x060080CA RID: 32970 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetScrollPos()
			{
			}

			// Token: 0x0400B913 RID: 47379
			private readonly string k_ELabelBackButton;

			// Token: 0x0400B914 RID: 47380
			private readonly string k_ELabelCardNameText;

			// Token: 0x0400B915 RID: 47381
			private readonly string k_ELabelDescText;

			// Token: 0x0400B916 RID: 47382
			private readonly string k_ELabelPendulumDescText;

			// Token: 0x0400B917 RID: 47383
			private readonly string k_ELabelCardNameScroll;

			// Token: 0x0400B918 RID: 47384
			private readonly string k_ELabelDescScroll;

			// Token: 0x0400B919 RID: 47385
			private readonly string k_ELabelDescPendulumScroll;

			// Token: 0x0400B91A RID: 47386
			private readonly string k_ELabelRelativeCardButton;

			// Token: 0x0400B91B RID: 47387
			private readonly string k_ELabelCardBottomText;

			// Token: 0x0400B91C RID: 47388
			public readonly ElementObjectManager eom;

			// Token: 0x0400B91D RID: 47389
			public readonly CardInfoDetail cardInfoDetail;

			// Token: 0x0400B91E RID: 47390
			public readonly RubyTextGX cardNameText;

			// Token: 0x0400B91F RID: 47391
			public readonly TextMeshProUGUI descText;

			// Token: 0x0400B920 RID: 47392
			public readonly TextMeshProUGUI pendulumDescText;

			// Token: 0x0400B921 RID: 47393
			public readonly ScrollRect nameScrollRect;

			// Token: 0x0400B922 RID: 47394
			public readonly ScrollRect descScrollRect;

			// Token: 0x0400B923 RID: 47395
			public readonly ScrollRect pendulumDescScrollRect;

			// Token: 0x0400B924 RID: 47396
			public readonly TMP_Text cardBottomText;

			// Token: 0x0400B925 RID: 47397
			public readonly SelectionButton relativeCardButton;

			// Token: 0x0400B926 RID: 47398
			public int idx;
		}
	}
}

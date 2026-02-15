using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F8C RID: 3980
	public class DeckBrowserOptionWidget : ElementWidgetBase
	{
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074B8 RID: 29880 RVA: 0x0000216D File Offset: 0x0000036D
		public bool fiveDrawButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074BA RID: 29882 RVA: 0x0000216D File Offset: 0x0000036D
		public string fiveDrawText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060074BB RID: 29883 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074BC RID: 29884 RVA: 0x0000216D File Offset: 0x0000036D
		public bool m_ExportButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060074BD RID: 29885 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074BE RID: 29886 RVA: 0x0000216D File Offset: 0x0000036D
		public string ExportText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (set) Token: 0x060074BF RID: 29887 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite ExportIcon
		{
			set
			{
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060074C0 RID: 29888 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074C1 RID: 29889 RVA: 0x0000216D File Offset: 0x0000036D
		public bool regulationButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074C3 RID: 29891 RVA: 0x0000216D File Offset: 0x0000036D
		public string regulationText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060074C4 RID: 29892 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074C5 RID: 29893 RVA: 0x0000216D File Offset: 0x0000036D
		public bool hasCardOnlyToggleEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060074C6 RID: 29894 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074C7 RID: 29895 RVA: 0x0000216D File Offset: 0x0000036D
		public bool hasCardOnlyToggleIsOn
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060074C8 RID: 29896 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074C9 RID: 29897 RVA: 0x0000216D File Offset: 0x0000036D
		public bool copyButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060074CA RID: 29898 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074CB RID: 29899 RVA: 0x0000216D File Offset: 0x0000036D
		public string copyButtonText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x060074CC RID: 29900 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074CD RID: 29901 RVA: 0x0000216D File Offset: 0x0000036D
		public bool deleteButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x060074CE RID: 29902 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074CF RID: 29903 RVA: 0x0000216D File Offset: 0x0000036D
		public string deleteButtonText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x060074D0 RID: 29904 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074D1 RID: 29905 RVA: 0x0000216D File Offset: 0x0000036D
		public bool descTextEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x060074D2 RID: 29906 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060074D3 RID: 29907 RVA: 0x0000216D File Offset: 0x0000036D
		public string descText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x060074D4 RID: 29908 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074D5 RID: 29909 RVA: 0x0000216D File Offset: 0x0000036D
		public bool upperMenuEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060074D6 RID: 29910 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074D7 RID: 29911 RVA: 0x0000216D File Offset: 0x0000036D
		public bool footerMenuEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060074D8 RID: 29912 RVA: 0x0000216A File Offset: 0x0000036A
		public ShortcutKeySetter ShortcutSettings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFiveDrawButtonImage(Sprite sprite, Sprite spriteOver)
		{
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCopyButtonImage(Sprite sprite, Sprite spriteOver)
		{
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<DeckBrowserOptionWidget> onCreated)
		{
		}

		// Token: 0x060074DC RID: 29916 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public DeckBrowserOptionWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x0000216D File Offset: 0x0000036D
		public void CopyButtonUpdate()
		{
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x0000216D File Offset: 0x0000036D
		public void CopyButtonSetColor(bool active)
		{
		}

		// Token: 0x0400ADB5 RID: 44469
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOption";

		// Token: 0x0400ADB6 RID: 44470
		protected string k_ELabelFiveDrawButton;

		// Token: 0x0400ADB7 RID: 44471
		protected string k_ELabelRegulationButton;

		// Token: 0x0400ADB8 RID: 44472
		protected string k_ELabelRegulationText;

		// Token: 0x0400ADB9 RID: 44473
		protected string k_ELabelHasCardOnlyToggle;

		// Token: 0x0400ADBA RID: 44474
		protected string k_ELabelCopyButton;

		// Token: 0x0400ADBB RID: 44475
		protected string k_ELabelDeleteButton;

		// Token: 0x0400ADBC RID: 44476
		protected string k_ELabelDescText;

		// Token: 0x0400ADBD RID: 44477
		protected string k_ELabelUpperMenus;

		// Token: 0x0400ADBE RID: 44478
		protected string k_ELabelFooterMenus;

		// Token: 0x0400ADBF RID: 44479
		protected string k_ELabelExportButton;

		// Token: 0x0400ADC0 RID: 44480
		protected string k_ELabelButtonIconCardDB;

		// Token: 0x0400ADC1 RID: 44481
		protected SelectionButton m_RegulationButton;

		// Token: 0x0400ADC2 RID: 44482
		protected TextMeshProUGUI m_RegulationText;

		// Token: 0x0400ADC3 RID: 44483
		protected SelectionButton m_HasCardOnlyButton;

		// Token: 0x0400ADC4 RID: 44484
		protected ToggleWidget m_HasCardOnlyToggle;

		// Token: 0x0400ADC5 RID: 44485
		protected SelectionButton m_CopyButton;

		// Token: 0x0400ADC6 RID: 44486
		protected TextMeshProUGUI m_CopyText;

		// Token: 0x0400ADC7 RID: 44487
		protected SelectionButton m_DeleteButton;

		// Token: 0x0400ADC8 RID: 44488
		protected TextMeshProUGUI m_DeleteText;

		// Token: 0x0400ADC9 RID: 44489
		protected SelectionButton m_FiveDrawButton;

		// Token: 0x0400ADCA RID: 44490
		protected TextMeshProUGUI m_FiveDrawText;

		// Token: 0x0400ADCB RID: 44491
		protected SelectionButton m_ExportButton;

		// Token: 0x0400ADCC RID: 44492
		protected TextMeshProUGUI m_ExportText;

		// Token: 0x0400ADCD RID: 44493
		protected Image m_ExportIconCardDB;

		// Token: 0x0400ADCE RID: 44494
		protected TextMeshProUGUI m_DescText;

		// Token: 0x0400ADCF RID: 44495
		protected Transform m_UpperMenus;

		// Token: 0x0400ADD0 RID: 44496
		protected Transform m_FooterMenus;

		// Token: 0x0400ADD1 RID: 44497
		private ShortcutKeySetter m_ShortcutSettings;

		// Token: 0x0400ADD2 RID: 44498
		public Action onClickFiveDrawCallback;

		// Token: 0x0400ADD3 RID: 44499
		public Action onClickExportCallback;

		// Token: 0x0400ADD4 RID: 44500
		public Action onClickRegulationCallback;

		// Token: 0x0400ADD5 RID: 44501
		public Action<bool> onClickHasCardOnlyCallback;

		// Token: 0x0400ADD6 RID: 44502
		public Action onClickCopyCallback;

		// Token: 0x0400ADD7 RID: 44503
		public Action onClickDeleteCallback;
	}
}

using System;
using System.Collections.Generic;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B83 RID: 2947
	public class MDMarkupAssetViewController : BaseBlurOverlayViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool defaultBlurOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushByContainer(ViewControllerManager vcm, IMDMarkupContainer container, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenByAsset(string assetPath, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SwapOpenByAsset(ViewController target, string assetPath, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenByAsset(MDMarkupAsset asset, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SwapOpenByAsset(ViewController target, MDMarkupAsset asset, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenByJson(string json, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedMDMarkupAsset()
		{
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x040091E0 RID: 37344
		private const string k_PrefPath = "MDMarkupAsset";

		// Token: 0x040091E1 RID: 37345
		public const string k_ArgKeyOpenOnHome = "openOnHome";

		// Token: 0x040091E2 RID: 37346
		private const string k_ArgKeySourceAsset = "sourceAsset";

		// Token: 0x040091E3 RID: 37347
		private const string k_ArgKeySourceJson = "sourceJson";

		// Token: 0x040091E4 RID: 37348
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x040091E5 RID: 37349
		public const string k_ArgKeyCloseButtonType = "closeButtonType";

		// Token: 0x040091E6 RID: 37350
		public const string k_ArgKeyTitle = "title";

		// Token: 0x040091E7 RID: 37351
		public const string k_ArgKeyOptionalText = "optionalText";

		// Token: 0x040091E8 RID: 37352
		public const string k_ArgKeyBadge = "badge";

		// Token: 0x040091E9 RID: 37353
		private readonly string k_ViewLabelBoard;

		// Token: 0x040091EA RID: 37354
		private readonly string k_ViewLabelPager;

		// Token: 0x040091EB RID: 37355
		private readonly string k_ViewLabelTabs;

		// Token: 0x040091EC RID: 37356
		private readonly string k_ViewLabelBoardPager;

		// Token: 0x040091ED RID: 37357
		private MDMarkupAssetViewController.InitFlags m_InitStep;

		// Token: 0x040091EE RID: 37358
		private MDMarkupAsset m_MDMarkupAsset;

		// Token: 0x040091EF RID: 37359
		private MDMarkupGraphFactory m_MDMarkupGraphFactory;

		// Token: 0x040091F0 RID: 37360
		private IMDMarkupContainerWidget m_ContainerWidget;

		// Token: 0x040091F1 RID: 37361
		private List<string> m_LoadedTextGroups;

		// Token: 0x02000B84 RID: 2948
		private enum InitFlags
		{
			// Token: 0x040091F3 RID: 37363
			None,
			// Token: 0x040091F4 RID: 37364
			LoadMarkupAsset,
			// Token: 0x040091F5 RID: 37365
			InitializedMarkupFactory,
			// Token: 0x040091F6 RID: 37366
			CreateViewStart = 4,
			// Token: 0x040091F7 RID: 37367
			CreateViewEnd = 8,
			// Token: 0x040091F8 RID: 37368
			MarkupTextLoad = 16,
			// Token: 0x040091F9 RID: 37369
			MarkupPreLoadStart = 32,
			// Token: 0x040091FA RID: 37370
			MarkupPreLoadEnd = 64,
			// Token: 0x040091FB RID: 37371
			MarkupOutputGraphStart = 128,
			// Token: 0x040091FC RID: 37372
			MarkupOutputGraphEnd = 256
		}
	}
}

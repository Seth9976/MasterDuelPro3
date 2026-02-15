using System;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A6E RID: 2670
	public class DuelistCupResultViewController : BaseMenuViewController, IFadeSupported
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0000216D File Offset: 0x0000036D
		private void clickButtonBackArea()
		{
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Util.GameMode gameMode, Action callback = null)
		{
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkDuelResultType()
		{
			return false;
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x000F4AE0 File Offset: 0x000F2CE0
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x04008BB6 RID: 35766
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x04008BB7 RID: 35767
		private const string k_ArgkeyGameMode = "gameMode";

		// Token: 0x04008BB8 RID: 35768
		private readonly string k_ELabelBackShortcutButton;

		// Token: 0x04008BB9 RID: 35769
		private readonly string DP_INFO_LABEL;

		// Token: 0x04008BBA RID: 35770
		private readonly string DP_INFOROOT_LABEL;

		// Token: 0x04008BBB RID: 35771
		private readonly string TW_WIN_LABEL;

		// Token: 0x04008BBC RID: 35772
		private readonly string TW_DROW_LABEL;

		// Token: 0x04008BBD RID: 35773
		private readonly string TW_LOSE_LABEL;

		// Token: 0x04008BBE RID: 35774
		private readonly string TEXT_TOTAL_DP_LABEL;

		// Token: 0x04008BBF RID: 35775
		private readonly string TEXT_ADD_DP_LABEL;

		// Token: 0x04008BC0 RID: 35776
		private readonly string TEXT_STAGE_LABEL;

		// Token: 0x04008BC1 RID: 35777
		private readonly string TEXT_RESULTLABEL_LABEL;

		// Token: 0x04008BC2 RID: 35778
		private readonly string TEXT_WIN_LABEL;

		// Token: 0x04008BC3 RID: 35779
		private readonly string TEXT_WIN_NUM_LABEL;

		// Token: 0x04008BC4 RID: 35780
		private readonly string TEXT_BEFORE_DP_LABEL;

		// Token: 0x04008BC5 RID: 35781
		private readonly string IMAGE_ARROW_LABEL;

		// Token: 0x04008BC6 RID: 35782
		private GameObject m_View3D;

		// Token: 0x04008BC7 RID: 35783
		private bool is_initialised;

		// Token: 0x04008BC8 RID: 35784
		private ElementObjectManager m_TargetEom;

		// Token: 0x04008BC9 RID: 35785
		private ElementObjectManager eom;

		// Token: 0x04008BCA RID: 35786
		private ElementObject bg;

		// Token: 0x04008BCB RID: 35787
		private Tween BeforeDPTextTween;

		// Token: 0x04008BCC RID: 35788
		private Tween DiffDPTextTween;

		// Token: 0x04008BCD RID: 35789
		private Tween TotalDPTextTween;

		// Token: 0x04008BCE RID: 35790
		private SelectionButton buttonBackArea;

		// Token: 0x04008BCF RID: 35791
		private Engine.ResultType resultType;

		// Token: 0x04008BD0 RID: 35792
		private bool isStartTween;

		// Token: 0x04008BD1 RID: 35793
		private bool isPlaySE;

		// Token: 0x04008BD2 RID: 35794
		private bool ForceTweenFinished;

		// Token: 0x04008BD3 RID: 35795
		private bool isPlayTween;

		// Token: 0x04008BD4 RID: 35796
		private int bDP;

		// Token: 0x04008BD5 RID: 35797
		private int aDP;

		// Token: 0x04008BD6 RID: 35798
		private Util.GameMode gameMode;

		// Token: 0x04008BD7 RID: 35799
		private string gameModePath;
	}
}

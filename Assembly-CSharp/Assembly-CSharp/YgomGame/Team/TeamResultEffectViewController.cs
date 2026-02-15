using System;
using UnityEngine.Playables;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008D1 RID: 2257
	public class TeamResultEffectViewController : BaseMenuViewController
	{
		// Token: 0x06004207 RID: 16903 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, int result, int cardMrk, Action callback = null)
		{
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTimeLine()
		{
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x0000216A File Offset: 0x0000036A
		private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
		{
			return null;
		}

		// Token: 0x04008051 RID: 32849
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x04008052 RID: 32850
		private const string k_ArgKeyResult = "result";

		// Token: 0x04008053 RID: 32851
		private const string k_ArgKeyCardMrk = "cardMrk";

		// Token: 0x04008054 RID: 32852
		private readonly string k_ELabelBackShortcutButton;

		// Token: 0x04008055 RID: 32853
		private TimelineObject teamResultEffectTimeLine;

		// Token: 0x04008056 RID: 32854
		private EventPlayableAsset eventPlayableAsset;

		// Token: 0x04008057 RID: 32855
		private ElementObjectManager eom;

		// Token: 0x04008058 RID: 32856
		private SelectionButton backButton;

		// Token: 0x04008059 RID: 32857
		private bool isFinishedSE;

		// Token: 0x0400805A RID: 32858
		private bool isFinishedResultEffect;

		// Token: 0x0400805B RID: 32859
		private int resultStatus;

		// Token: 0x0400805C RID: 32860
		private int cardMrk;
	}
}

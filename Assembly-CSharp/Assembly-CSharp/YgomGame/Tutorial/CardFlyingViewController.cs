using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;
using YgomGame.Menu;
using YgomGame.Solo;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;

namespace YgomGame.Tutorial
{
	// Token: 0x02000837 RID: 2103
	public class CardFlyingViewController : BaseMenuViewController
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060040D1 RID: 16593 RVA: 0x0000216A File Offset: 0x0000036A
		public static PlayableDirector bgDirector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Start(IList<string> msgList, UnityAction onFinish)
		{
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBlackoutEnd()
		{
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTimeline()
		{
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowTelopRoutine(IList<string> messages)
		{
			return null;
		}

		// Token: 0x040039D4 RID: 14804
		private const string PATH = "Tutorial/CardFlying";

		// Token: 0x040039D5 RID: 14805
		private const string TWEEN_LABEL_SHOW = "Show";

		// Token: 0x040039D6 RID: 14806
		private const string TWEEN_LABEL_HIDE = "Hide";

		// Token: 0x040039D7 RID: 14807
		private const string ELEOBJ_LABEL_MSG = "Message";

		// Token: 0x040039D8 RID: 14808
		private const string ARGS_KEY_TEXTLIST = "TextList";

		// Token: 0x040039D9 RID: 14809
		private const string ARGS_KEY_FINISHCALLBACK = "onFinish";

		// Token: 0x040039DA RID: 14810
		private const string ARGS_KEY_AUTOMODE = "AutoMode";

		// Token: 0x040039DB RID: 14811
		private const string ANDROID_BACK_KEY_LABEL = "AndroidBackKey";

		// Token: 0x040039DC RID: 14812
		private ElementObjectManager _ui;

		// Token: 0x040039DD RID: 14813
		private SoloFlyingCardSettings _flyingCardSetting;

		// Token: 0x040039DE RID: 14814
		private LabeledPlayableController _labelCtrl;

		// Token: 0x040039DF RID: 14815
		private static PlayableDirector _director;
	}
}

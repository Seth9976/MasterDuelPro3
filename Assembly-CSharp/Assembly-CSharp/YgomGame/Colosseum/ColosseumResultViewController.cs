using System;
using System.Collections.Generic;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x0200105C RID: 4188
	public class ColosseumResultViewController : BaseMenuViewController
	{
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06007DCD RID: 32205 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(string tournamentName, ColosseumResultViewController.AwardType awardType, int dispOrder, bool existNumLogo = false, UnityAction onFinish = null)
		{
			return null;
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string GetBgPath()
		{
			return null;
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager GetEomRootRank(ColosseumResultViewController.AwardType awardType)
		{
			return null;
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void SetCupImage(ElementObjectManager targetEom, bool existNumLogo, ColosseumResultViewController.AwardType awardType)
		{
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string GetLogoName(bool existNumLogo, ColosseumResultViewController.AwardType awardType)
		{
			return null;
		}

		// Token: 0x0400B617 RID: 46615
		private readonly string BTN_CLOSE_LABEL;

		// Token: 0x0400B618 RID: 46616
		private readonly string BTN_CLOSE_CENTER_LABEL;

		// Token: 0x0400B619 RID: 46617
		private readonly string ROOT_FIRST_LABEL;

		// Token: 0x0400B61A RID: 46618
		private readonly string ROOT_SECOND_LABEL;

		// Token: 0x0400B61B RID: 46619
		private readonly string ROOT_THIRD_LABEL;

		// Token: 0x0400B61C RID: 46620
		private readonly string ROOT_RANKED_LABEL;

		// Token: 0x0400B61D RID: 46621
		private readonly string ROOT_PARTICIPATE_LABEL;

		// Token: 0x0400B61E RID: 46622
		private readonly string IMG_ICON_LABEL;

		// Token: 0x0400B61F RID: 46623
		protected readonly string IMG_CUP_LABEL;

		// Token: 0x0400B620 RID: 46624
		private readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400B621 RID: 46625
		private readonly string TXT_TOURNAMENT_NAME_LABEL;

		// Token: 0x0400B622 RID: 46626
		private readonly string TXT_RANK_LABEL;

		// Token: 0x0400B623 RID: 46627
		protected const string KEY_TOURNAMENT_NAME = "TournamentName";

		// Token: 0x0400B624 RID: 46628
		protected const string KEY_AWARD_TYPE = "AwardType";

		// Token: 0x0400B625 RID: 46629
		protected const string KEY_ORDER = "Order";

		// Token: 0x0400B626 RID: 46630
		private const string KEY_EXIST_NUM_LOGO = "ExistNumLogo";

		// Token: 0x0400B627 RID: 46631
		protected const string KEY_FINISH_CALLBACK = "FinishCallback";

		// Token: 0x0400B628 RID: 46632
		private const string PATH_ATLAS = "Images/Colosseum/All/ColosseumAtlasAll";

		// Token: 0x0400B629 RID: 46633
		protected BindingGameObjectEx backGroundBGO;

		// Token: 0x0400B62A RID: 46634
		protected ColosseumResultViewController.AwardType awardType;

		// Token: 0x0200105D RID: 4189
		public enum AwardType
		{
			// Token: 0x0400B62C RID: 46636
			NONE,
			// Token: 0x0400B62D RID: 46637
			FIRST,
			// Token: 0x0400B62E RID: 46638
			SECOND,
			// Token: 0x0400B62F RID: 46639
			THIRD,
			// Token: 0x0400B630 RID: 46640
			OTHER,
			// Token: 0x0400B631 RID: 46641
			PARTICIPATE
		}
	}
}

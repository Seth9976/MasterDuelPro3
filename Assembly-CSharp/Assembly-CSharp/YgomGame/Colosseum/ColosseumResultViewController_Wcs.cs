using System;
using System.Collections.Generic;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x0200105E RID: 4190
	public class ColosseumResultViewController_Wcs : ColosseumResultViewController
	{
		// Token: 0x06007DD8 RID: 32216 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetBgPath()
		{
			return null;
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(string tournamentName, ColosseumResultViewController.AwardType awardType, int dispOrder, int logoId, UnityAction onFinish = null)
		{
			return null;
		}

		// Token: 0x06007DDA RID: 32218 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetCupImage(ElementObjectManager targetEom, bool existNumLogo, ColosseumResultViewController.AwardType awardType)
		{
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetLogoName(bool existNumLogo, ColosseumResultViewController.AwardType awardType)
		{
			return null;
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTweenLavel(ColosseumResultViewController.AwardType awardType)
		{
			return null;
		}

		// Token: 0x0400B632 RID: 46642
		protected const string KEY_LOGO_ID = "LogoId";
	}
}

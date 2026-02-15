using System;
using YgomGame.Menu;

namespace YgomGame.Colosseum
{
	// Token: 0x02001077 RID: 4215
	public class ColosseumStartViewController : BaseMenuViewController
	{
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06007E38 RID: 32312 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ColosseumStartViewController.PrefabType prefabType, string tournamentName = "", int logoId = 0, int identifier = 0, Action onFinish = null)
		{
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBackButton()
		{
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStandardView()
		{
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTouramentView()
		{
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWCSView()
		{
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTouramentNoMessageView()
		{
		}

		// Token: 0x0400B6B5 RID: 46773
		private readonly string BTN_CLOSE_LABEL;

		// Token: 0x0400B6B6 RID: 46774
		private readonly string IMG_TOURNAMENT_LABEL;

		// Token: 0x0400B6B7 RID: 46775
		private readonly string TXT_NAME_LABEL;

		// Token: 0x0400B6B8 RID: 46776
		private readonly string E_TextDescription;

		// Token: 0x0400B6B9 RID: 46777
		private readonly string E_Logo;

		// Token: 0x0400B6BA RID: 46778
		private const string KEY_NAME = "TournamentName";

		// Token: 0x0400B6BB RID: 46779
		private const string KEY_LOGO = "LogoId";

		// Token: 0x0400B6BC RID: 46780
		private const string KEY_TYPE = "PrefabType";

		// Token: 0x0400B6BD RID: 46781
		private const string KEY_IDENTIFIER = "Identifier";

		// Token: 0x0400B6BE RID: 46782
		public const string KEY_ENDACTION_QUEUE = "EndActionQueue";

		// Token: 0x0400B6BF RID: 46783
		public const string KEY_ENDACTION = "EndAction";

		// Token: 0x0400B6C0 RID: 46784
		private ColosseumStartViewController.PrefabType prefabType;

		// Token: 0x02001078 RID: 4216
		public enum PrefabType
		{
			// Token: 0x0400B6C2 RID: 46786
			STANDARD,
			// Token: 0x0400B6C3 RID: 46787
			TOURNAMENT,
			// Token: 0x0400B6C4 RID: 46788
			WCS,
			// Token: 0x0400B6C5 RID: 46789
			TOURNAMENT_NOMESSAGE
		}
	}
}

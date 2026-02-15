using System;
using System.Collections.Generic;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x02001076 RID: 4214
	public class ColosseumSelectVersusViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06007E30 RID: 32304 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(int versus_id, Action<int, ViewController> onDecide)
		{
			return null;
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitView()
		{
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetButton(ElementObjectManager eom, int logoId, int groupId)
		{
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartPerformance()
		{
		}

		// Token: 0x0400B6A6 RID: 46758
		public const string PREF_PATH = "Colosseum/ColosseumSelectVersus";

		// Token: 0x0400B6A7 RID: 46759
		private const string E_RootButton = "RootButton";

		// Token: 0x0400B6A8 RID: 46760
		private const string E_Button = "Button";

		// Token: 0x0400B6A9 RID: 46761
		private const string E_Image = "Image";

		// Token: 0x0400B6AA RID: 46762
		private const string E_ImageParticipate = "ImageParticipate";

		// Token: 0x0400B6AB RID: 46763
		private const string E_Text = "Text";

		// Token: 0x0400B6AC RID: 46764
		private const string E_TextHeadline = "TextHeadline";

		// Token: 0x0400B6AD RID: 46765
		private const string E_TextParticipate = "TextParticipate";

		// Token: 0x0400B6AE RID: 46766
		private const string E_ImageBg = "ImageBg";

		// Token: 0x0400B6AF RID: 46767
		private const string E_ImageIcon = "ImageIcon";

		// Token: 0x0400B6B0 RID: 46768
		private const string E_ImageMonster = "ImageMonster";

		// Token: 0x0400B6B1 RID: 46769
		private const string ARGKEY_VERSUSID = "ArgKeyVersusId";

		// Token: 0x0400B6B2 RID: 46770
		private const string ARGKEY_ONCLICKBUTTON = "ArgKeyOnClickButton";

		// Token: 0x0400B6B3 RID: 46771
		private int versus_id;

		// Token: 0x0400B6B4 RID: 46772
		private Action<int, ViewController> onDecide;
	}
}

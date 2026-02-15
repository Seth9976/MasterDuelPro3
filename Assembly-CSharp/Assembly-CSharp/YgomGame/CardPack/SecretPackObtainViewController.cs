using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Menu;

namespace YgomGame.CardPack
{
	// Token: 0x020010AC RID: 4268
	public class SecretPackObtainViewController : BaseMenuViewController
	{
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06007EF2 RID: 32498 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(bool isExtend, Action callback = null)
		{
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(bool isExtend, string packNameTextId, int thumbType, string thumbData, Action callback = null)
		{
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTMPaused(PlayableDirector director)
		{
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B78A RID: 46986
		private const string k_ArgKeyIsExtend = "isExtend";

		// Token: 0x0400B78B RID: 46987
		private const string k_ArgKeyNameTextId = "nameTextId";

		// Token: 0x0400B78C RID: 46988
		private const string k_ArgKeyThumbType = "thumbType";

		// Token: 0x0400B78D RID: 46989
		private const string k_ArgKeyThumbData = "thumbData";

		// Token: 0x0400B78E RID: 46990
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x0400B78F RID: 46991
		private readonly string k_ELabelBG3D;

		// Token: 0x0400B790 RID: 46992
		private readonly string k_ELabelFoundTextLabel;

		// Token: 0x0400B791 RID: 46993
		private readonly string k_ELabelPackThumbRoot;

		// Token: 0x0400B792 RID: 46994
		private readonly string k_ELabelPackThumbHolder;

		// Token: 0x0400B793 RID: 46995
		private readonly string k_ELabelPackNameTMP;

		// Token: 0x0400B794 RID: 46996
		private readonly string k_ELabelBackShortcutButton;

		// Token: 0x0400B795 RID: 46997
		private readonly string k_ELabelSmallBand;

		// Token: 0x0400B796 RID: 46998
		private readonly string k_ELabelLargeBand;

		// Token: 0x0400B797 RID: 46999
		private GameObject m_View3D;

		// Token: 0x0400B798 RID: 47000
		private string m_PackName;
	}
}

using System;
using System.Collections;
using UnityEngine;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu
{
	// Token: 0x02000AF2 RID: 2802
	public class StructureDeckObtainViewController : InformDialogViewControllerBase<int, Action>
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int arg1
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Action arg2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int structureDeckId, Action callback)
		{
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x04008FD7 RID: 36823
		[SerializeField]
		private float m_AlphaMessageWaitSec;

		// Token: 0x04008FD8 RID: 36824
		private readonly string k_ELabelDeckBox;

		// Token: 0x04008FD9 RID: 36825
		private StructureBoxWidget m_DeckWidget;

		// Token: 0x04008FDA RID: 36826
		private const string ANDROID_BACK_KEY_LABEL = "AndroidBackKey";
	}
}

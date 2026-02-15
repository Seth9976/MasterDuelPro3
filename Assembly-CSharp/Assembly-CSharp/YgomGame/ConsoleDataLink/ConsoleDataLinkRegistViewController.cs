using System;
using System.Collections;
using UnityEngine.Events;
using YgomGame.Menu;

namespace YgomGame.ConsoleDataLink
{
	// Token: 0x02001015 RID: 4117
	public class ConsoleDataLinkRegistViewController : ConsoleDataLinkViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06007BCD RID: 31693 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x0000216D File Offset: 0x0000036D
		private void Regist()
		{
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckRegistered(UnityAction unregesteredEvent)
		{
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator KonamiIDCheckLink_Polling()
		{
			return null;
		}

		// Token: 0x0400B3B3 RID: 46003
		private string m_url;
	}
}

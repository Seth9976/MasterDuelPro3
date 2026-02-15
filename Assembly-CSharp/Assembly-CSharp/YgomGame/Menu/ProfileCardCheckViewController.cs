using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AC0 RID: 2752
	public class ProfileCardCheckViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06005020 RID: 20512 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayedInvokeCallback(Action action)
		{
			return null;
		}

		// Token: 0x04008E31 RID: 36401
		private readonly string k_ELabelProfileCardRoot;

		// Token: 0x04008E32 RID: 36402
		private readonly string k_ELabelBackButton;

		// Token: 0x04008E33 RID: 36403
		public const string k_ArgKeyPcode = "pcode";

		// Token: 0x04008E34 RID: 36404
		public const string k_ArgKeyCallAPI = "callAPI";

		// Token: 0x04008E35 RID: 36405
		private GameObject m_ProfileCardParent;

		// Token: 0x04008E36 RID: 36406
		private SelectionButton m_BackButton;

		// Token: 0x04008E37 RID: 36407
		private long pcode;

		// Token: 0x04008E38 RID: 36408
		private bool callAPI;

		// Token: 0x04008E39 RID: 36409
		private Dictionary<string, object> profileDic;
	}
}

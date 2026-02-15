using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005AC RID: 1452
	public class PlatformPlayerIcon : MonoBehaviour
	{
		// Token: 0x06002DF5 RID: 11765 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(bool isSamePlatfrom)
		{
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisp(bool disp)
		{
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIcon(bool isSamePlatform)
		{
		}

		// Token: 0x04002BA5 RID: 11173
		private readonly string k_CurrentPlatformIconPath;

		// Token: 0x04002BA6 RID: 11174
		private readonly string k_OtherPlatformIconPath;

		// Token: 0x04002BA7 RID: 11175
		[SerializeField]
		private GameObject m_TargetRoot;

		// Token: 0x04002BA8 RID: 11176
		[SerializeField]
		private Image m_TargetIconImage;
	}
}

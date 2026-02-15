using System;
using UnityEngine;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000676 RID: 1654
	public class PlatformOverriderGroup : MonoBehaviour
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject[] activeOverriders
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600334E RID: 13134 RVA: 0x0000216D File Offset: 0x0000036D
		public string switchLabel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImmediateApply(bool frag = false)
		{
		}

		// Token: 0x04002F8D RID: 12173
		[SerializeField]
		private string m_SwitchLabel;

		// Token: 0x04002F8E RID: 12174
		[SerializeField]
		private GameObject[] m_ActiveOverriders;
	}
}

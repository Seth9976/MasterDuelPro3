using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000AEA RID: 2794
	public class QRCodeCreator : MonoBehaviour
	{
		// Token: 0x0600515A RID: 20826 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x04008FA1 RID: 36769
		private readonly string k_ElabelQRCodeImage;

		// Token: 0x04008FA2 RID: 36770
		private readonly string k_ElabelTextURL;

		// Token: 0x04008FA3 RID: 36771
		private readonly string k_ElabelURLButton;

		// Token: 0x04008FA4 RID: 36772
		[SerializeField]
		private string clientWorkPath;

		// Token: 0x04008FA5 RID: 36773
		private ElementObjectManager m_RootEom;

		// Token: 0x04008FA6 RID: 36774
		private RawImage m_QRCodeImage;

		// Token: 0x04008FA7 RID: 36775
		private TextMeshProUGUI m_TextURL;

		// Token: 0x04008FA8 RID: 36776
		private Button m_URLButton;
	}
}

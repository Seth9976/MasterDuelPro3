using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013E6 RID: 5094
	public class DuelErrorLog : MonoBehaviour
	{
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x0600937D RID: 37757 RVA: 0x0014DB90 File Offset: 0x0014BD90
		private CanvasGroup CG
		{
			get
			{
				return this.cg = ((this.cg != null) ? this.cg : base.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x0600937E RID: 37758 RVA: 0x0014DBC4 File Offset: 0x0014BDC4
		private ScrollRect ScrollRect
		{
			get
			{
				return this.scrollRect = ((this.scrollRect != null) ? this.scrollRect : base.GetComponent<ScrollRect>());
			}
		}

		// Token: 0x0600937F RID: 37759 RVA: 0x0014DBF6 File Offset: 0x0014BDF6
		public void Hide()
		{
			this.CG.alpha = 0f;
			this.cg.blocksRaycasts = false;
		}

		// Token: 0x06009380 RID: 37760 RVA: 0x0014DC14 File Offset: 0x0014BE14
		public void Show(string log)
		{
			log = "[OcgCore Message]: " + log;
			this.lastMessage = this.text.text;
			this.text.text = this.lastMessage + "\r\n" + log;
			this.CG.alpha = 1f;
			this.cg.blocksRaycasts = true;
			this.ScrollRect.DOVerticalNormalizedPos(0f, 0.1f, false);
		}

		// Token: 0x0400D1E4 RID: 53732
		private CanvasGroup cg;

		// Token: 0x0400D1E5 RID: 53733
		private ScrollRect scrollRect;

		// Token: 0x0400D1E6 RID: 53734
		public TextMeshProUGUI text;

		// Token: 0x0400D1E7 RID: 53735
		private string lastMessage;
	}
}

using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013EF RID: 5103
	public class TimerHandler : MonoBehaviour
	{
		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x060093FA RID: 37882 RVA: 0x00150E4F File Offset: 0x0014F04F
		// (set) Token: 0x060093FB RID: 37883 RVA: 0x00150E57 File Offset: 0x0014F057
		public int time
		{
			get
			{
				return this.m_time;
			}
			set
			{
				this.pastTime = 0f;
				this.m_time = value;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x060093FC RID: 37884 RVA: 0x00150E6B File Offset: 0x0014F06B
		// (set) Token: 0x060093FD RID: 37885 RVA: 0x00150E73 File Offset: 0x0014F073
		public int player
		{
			get
			{
				return this.m_player;
			}
			set
			{
				this.m_player = value;
				this.ChangePlayer();
			}
		}

		// Token: 0x060093FE RID: 37886 RVA: 0x00150E84 File Offset: 0x0014F084
		private void Awake()
		{
			this.manager = base.GetComponent<ElementObjectManager>();
			this.text = this.manager.GetElement<TextMeshPro>("Text");
			this.text.font = Program.instance.ui_.jpMenuTmpFont;
			this.material = this.manager.GetElement<Renderer>("Timer").materials[1];
			this.material.SetFloat("_AddTime", 0f);
		}

		// Token: 0x060093FF RID: 37887 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06009400 RID: 37888 RVA: 0x00150F00 File Offset: 0x0014F100
		private void Update()
		{
			if (!this.duelStart || this.duelEnd)
			{
				return;
			}
			if (this.timeLimit == 0)
			{
				this.DuelEnd();
			}
			this.pastTime += Time.unscaledDeltaTime;
			int remainTime = Mathf.CeilToInt((float)this.time - this.pastTime);
			this.text.text = remainTime.ToString();
			this.material.SetFloat("_MaxTime", ((float)this.time - this.pastTime < 0f) ? 0f : (((float)this.time - this.pastTime) / (float)this.timeLimit));
		}

		// Token: 0x06009401 RID: 37889 RVA: 0x00150FA6 File Offset: 0x0014F1A6
		public void DuelStart()
		{
			this.text.gameObject.SetActive(true);
			Tools.PlayAnimation(base.transform, "StartToPhase1");
			this.duelStart = true;
		}

		// Token: 0x06009402 RID: 37890 RVA: 0x00150FD0 File Offset: 0x0014F1D0
		public void DuelEnd()
		{
			this.text.gameObject.SetActive(false);
			this.duelEnd = true;
		}

		// Token: 0x06009403 RID: 37891 RVA: 0x00150FEC File Offset: 0x0014F1EC
		private void ChangePlayer()
		{
			if (this.player == 0)
			{
				this.material.SetColor("_MaxTimeColor01", new Color(0.2117f, 0.6784f, 1f, 0f));
				this.material.SetColor("_MaxTimeColor02", new Color(0.1098f, 0.4745f, 1f, 0f));
				return;
			}
			this.material.SetColor("_MaxTimeColor01", new Color(1f, 0.2194f, 0.2117f, 0f));
			this.material.SetColor("_MaxTimeColor02", new Color(1f, 0.1362f, 0.1098f, 0f));
		}

		// Token: 0x0400D257 RID: 53847
		private bool duelStart;

		// Token: 0x0400D258 RID: 53848
		private bool duelEnd;

		// Token: 0x0400D259 RID: 53849
		public int timeLimit;

		// Token: 0x0400D25A RID: 53850
		private int m_time;

		// Token: 0x0400D25B RID: 53851
		private int m_player;

		// Token: 0x0400D25C RID: 53852
		private float pastTime;

		// Token: 0x0400D25D RID: 53853
		private ElementObjectManager manager;

		// Token: 0x0400D25E RID: 53854
		private TextMeshPro text;

		// Token: 0x0400D25F RID: 53855
		private Material material;
	}
}

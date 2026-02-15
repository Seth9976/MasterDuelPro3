using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Stats;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

// Token: 0x02000019 RID: 25
public class CardReportTelopController : MonoBehaviour
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600004F RID: 79 RVA: 0x000029CC File Offset: 0x00000BCC
	// (set) Token: 0x06000050 RID: 80 RVA: 0x0000216D File Offset: 0x0000036D
	public CardReportTelopController.Step step
	{
		[CompilerGenerated]
		get
		{
			return CardReportTelopController.Step.Idle;
		}
		[CompilerGenerated]
		private set
		{
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0000216D File Offset: 0x0000036D
	private void Awake()
	{
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000216D File Offset: 0x0000036D
	private void FixedUpdate()
	{
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetStateContent(int cardid, string message, string messageNum, string messageUnit, CardStatsData.CARD_STATS_EFFECT_TYPE effecttype)
	{
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000216D File Offset: 0x0000036D
	public void Show(int duration)
	{
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000216D File Offset: 0x0000036D
	public void Hide()
	{
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000216D File Offset: 0x0000036D
	public void HideEffect()
	{
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnHide()
	{
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnShow()
	{
	}

	// Token: 0x04000041 RID: 65
	private ElementObjectManager m_EOManager;

	// Token: 0x04000042 RID: 66
	private ExtendedTextMeshProUGUI m_ItemName;

	// Token: 0x04000043 RID: 67
	private ExtendedTextMeshProUGUI m_ItemBody;

	// Token: 0x04000044 RID: 68
	private GameObject m_Effect1;

	// Token: 0x04000045 RID: 69
	private GameObject m_Effect2;

	// Token: 0x04000046 RID: 70
	private GameObject m_Bg0;

	// Token: 0x04000047 RID: 71
	private GameObject m_Bg1;

	// Token: 0x04000048 RID: 72
	private GameObject m_Bg2;

	// Token: 0x04000049 RID: 73
	private Tween m_Tween;

	// Token: 0x0400004A RID: 74
	private int m_Countdown;

	// Token: 0x0200001A RID: 26
	public enum Step
	{
		// Token: 0x0400004C RID: 76
		Idle,
		// Token: 0x0400004D RID: 77
		Opening,
		// Token: 0x0400004E RID: 78
		Showing,
		// Token: 0x0400004F RID: 79
		Closing
	}
}

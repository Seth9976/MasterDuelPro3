using System;
using System.Collections;
using UnityEngine;
using YgomGame.Utility;
using YgomSystem.YGomTMPro;

// Token: 0x02000041 RID: 65
public class PvpMenuMatchingViewController : PvpMenuMatchingViewControllerBase
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000FE RID: 254 RVA: 0x0000216A File Offset: 0x0000036A
	protected override Type[] textIds
	{
		get
		{
			return null;
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void Init()
	{
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000216D File Offset: 0x0000036D
	private void SetActiveView(PvpMenuMatchingViewController.View state)
	{
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000216A File Offset: 0x0000036A
	private string ConvertDispTime(float elapsedTime)
	{
		return null;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000216A File Offset: 0x0000036A
	protected override IEnumerator yMatch()
	{
		return null;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnClickBackButton(bool requestBack = true)
	{
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yPopView()
	{
		return null;
	}

	// Token: 0x04000187 RID: 391
	private readonly string BTN_CANCEL_LABEL;

	// Token: 0x04000188 RID: 392
	private readonly string BTN_BACK_LABEL;

	// Token: 0x04000189 RID: 393
	private readonly string BTN_RESEARCH_LABEL;

	// Token: 0x0400018A RID: 394
	private readonly string TXT_TIME_LABEL;

	// Token: 0x0400018B RID: 395
	private readonly string TXT_TIPS_LABEL;

	// Token: 0x0400018C RID: 396
	private readonly string ROOT_SEARCH_LABEL;

	// Token: 0x0400018D RID: 397
	private readonly string ROOT_MATCH_LABEL;

	// Token: 0x0400018E RID: 398
	private readonly string ROOT_TIMEOUT_LABEL;

	// Token: 0x0400018F RID: 399
	private readonly string IMG_RANK_LABEL;

	// Token: 0x04000190 RID: 400
	private readonly string IMG_EVENT_LABEL;

	// Token: 0x04000191 RID: 401
	private readonly string IMG_FREE_LABEL;

	// Token: 0x04000192 RID: 402
	private bool m_bRequestBack;

	// Token: 0x04000193 RID: 403
	private bool m_bRequestResearch;

	// Token: 0x04000194 RID: 404
	private bool m_bIsDispTimeout;

	// Token: 0x04000195 RID: 405
	private float m_ResearchTime;

	// Token: 0x04000196 RID: 406
	private DefinitionSetting m_MatchingDefine;

	// Token: 0x04000197 RID: 407
	private ExtendedTextMeshProUGUI m_TextTime;

	// Token: 0x04000198 RID: 408
	private float m_StartTime;

	// Token: 0x04000199 RID: 409
	private GameObject m_rootSearch;

	// Token: 0x0400019A RID: 410
	private GameObject m_rootMatch;

	// Token: 0x0400019B RID: 411
	private GameObject m_rootTimeout;

	// Token: 0x0400019C RID: 412
	private IEnumerator m_yPopViewRoutine;

	// Token: 0x0400019D RID: 413
	private const float LIMIT_POP_TIME = 15f;

	// Token: 0x0400019E RID: 414
	private PvpMenuMatchingViewController.View m_currentView;

	// Token: 0x02000042 RID: 66
	public enum View
	{
		// Token: 0x040001A0 RID: 416
		SEARCHING,
		// Token: 0x040001A1 RID: 417
		MATCHING,
		// Token: 0x040001A2 RID: 418
		TIMEOUT
	}
}

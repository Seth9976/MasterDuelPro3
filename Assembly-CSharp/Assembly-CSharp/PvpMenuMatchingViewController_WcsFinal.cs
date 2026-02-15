using System;
using System.Collections;
using UnityEngine.Events;

// Token: 0x02000047 RID: 71
public class PvpMenuMatchingViewController_WcsFinal : PvpMenuMatchingViewControllerBase
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600012A RID: 298 RVA: 0x0000216A File Offset: 0x0000036A
	protected override Type[] textIds
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void Init()
	{
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000216A File Offset: 0x0000036A
	protected override IEnumerator yMatch()
	{
		return null;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnClickBackButton()
	{
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yPopView()
	{
		return null;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000216D File Offset: 0x0000036D
	private void PopViewControllerSafety(UnityAction onSafetyPopAction = null)
	{
	}

	// Token: 0x040001BE RID: 446
	private readonly string BTN_CANCEL_LABEL;

	// Token: 0x040001BF RID: 447
	private bool m_bCalledPop;

	// Token: 0x040001C0 RID: 448
	private IEnumerator m_yPopViewRoutine;

	// Token: 0x040001C1 RID: 449
	private const float LIMIT_POP_TIME = 15f;

	// Token: 0x040001C2 RID: 450
	private PvpMenuMatchingViewController_WcsFinal.View m_currentView;

	// Token: 0x02000048 RID: 72
	private enum View
	{
		// Token: 0x040001C4 RID: 452
		INIT,
		// Token: 0x040001C5 RID: 453
		SEARCHING,
		// Token: 0x040001C6 RID: 454
		MATCHING,
		// Token: 0x040001C7 RID: 455
		TIMEOUT
	}
}

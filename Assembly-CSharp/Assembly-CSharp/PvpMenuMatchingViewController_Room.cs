using System;
using System.Collections;
using UnityEngine.Events;

// Token: 0x02000044 RID: 68
public class PvpMenuMatchingViewController_Room : PvpMenuMatchingViewControllerBase
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000118 RID: 280 RVA: 0x0000216A File Offset: 0x0000036A
	protected override Type[] textIds
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void Init()
	{
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000216A File Offset: 0x0000036A
	protected override IEnumerator yMatch()
	{
		return null;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnClickBackButton()
	{
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yPopView()
	{
		return null;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000216D File Offset: 0x0000036D
	private void PopViewControllerSafety(UnityAction onSafetyPopAction = null)
	{
	}

	// Token: 0x040001AD RID: 429
	private readonly string BTN_CANCEL_LABEL;

	// Token: 0x040001AE RID: 430
	private bool m_bCalledPop;

	// Token: 0x040001AF RID: 431
	private IEnumerator m_yPopViewRoutine;

	// Token: 0x040001B0 RID: 432
	private const float LIMIT_POP_TIME = 15f;

	// Token: 0x040001B1 RID: 433
	private PvpMenuMatchingViewController_Room.View m_currentView;

	// Token: 0x02000045 RID: 69
	private enum View
	{
		// Token: 0x040001B3 RID: 435
		INIT,
		// Token: 0x040001B4 RID: 436
		SEARCHING,
		// Token: 0x040001B5 RID: 437
		MATCHING,
		// Token: 0x040001B6 RID: 438
		TIMEOUT
	}
}

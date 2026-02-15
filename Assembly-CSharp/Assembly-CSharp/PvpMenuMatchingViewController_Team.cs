using System;
using System.Collections;
using UnityEngine.Events;

// Token: 0x02000046 RID: 70
public class PvpMenuMatchingViewController_Team : PvpMenuMatchingViewControllerBase
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000121 RID: 289 RVA: 0x0000216A File Offset: 0x0000036A
	protected override Type[] textIds
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void Init()
	{
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000216A File Offset: 0x0000036A
	protected override IEnumerator yMatch()
	{
		return null;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnClickBackButton()
	{
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yPopView()
	{
		return null;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000216D File Offset: 0x0000036D
	private void PopViewControllerSafety(UnityAction onSafetyPopAction = null)
	{
	}

	// Token: 0x040001B7 RID: 439
	private readonly string BTN_CANCEL_LABEL;

	// Token: 0x040001B8 RID: 440
	private readonly string E_TextSearching;

	// Token: 0x040001B9 RID: 441
	private bool m_bCalledPop;

	// Token: 0x040001BA RID: 442
	private bool m_bIsTimeOut;

	// Token: 0x040001BB RID: 443
	private bool m_bIsLeader;

	// Token: 0x040001BC RID: 444
	private IEnumerator m_yPopViewRoutine;

	// Token: 0x040001BD RID: 445
	private const float LIMIT_POP_TIME = 15f;
}

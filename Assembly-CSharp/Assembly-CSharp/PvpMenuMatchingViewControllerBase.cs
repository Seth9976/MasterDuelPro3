using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;

// Token: 0x02000043 RID: 67
public abstract class PvpMenuMatchingViewControllerBase : BaseMenuViewController
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000108 RID: 264 RVA: 0x000029CC File Offset: 0x00000BCC
	// (set) Token: 0x06000109 RID: 265 RVA: 0x0000216D File Offset: 0x0000036D
	public int ShowProgressCount
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	// Token: 0x0600010A RID: 266
	protected abstract IEnumerator yMatch();

	// Token: 0x0600010B RID: 267 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void Init()
	{
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000216D File Offset: 0x0000036D
	public override void NotificationStackEntry()
	{
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000216D File Offset: 0x0000036D
	public override void NotificationStackRemove()
	{
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator Start()
	{
		return null;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000216A File Offset: 0x0000036A
	protected IEnumerator yInit()
	{
		return null;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000216A File Offset: 0x0000036A
	protected IEnumerator yCancelMatching()
	{
		return null;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000216A File Offset: 0x0000036A
	protected IEnumerator yStopByFatalError()
	{
		return null;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000216D File Offset: 0x0000036D
	protected void ShowSystemProgress()
	{
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000216D File Offset: 0x0000036D
	protected void HideSystemProgress()
	{
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000216D File Offset: 0x0000036D
	private void ResetProgress()
	{
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000216A File Offset: 0x0000036A
	public Coroutine WrapStartCoroutine(IEnumerator routine)
	{
		return null;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000216D File Offset: 0x0000036D
	public void WrapStopCoroutine(IEnumerator routine)
	{
	}

	// Token: 0x040001A3 RID: 419
	protected bool m_bInit;

	// Token: 0x040001A4 RID: 420
	protected bool m_bAbort;

	// Token: 0x040001A5 RID: 421
	protected bool m_bStartDuel;

	// Token: 0x040001A6 RID: 422
	protected PvpMenuDefine.MatchingType m_MatchingType;

	// Token: 0x040001A7 RID: 423
	protected Dictionary<string, object> m_MatchingParam;

	// Token: 0x040001A8 RID: 424
	protected Dictionary<string, object> m_DuelParam;

	// Token: 0x040001A9 RID: 425
	protected PvpMenuMatchingBase m_MatchingComponent;

	// Token: 0x040001AA RID: 426
	protected IEnumerator m_MatchingCoroutine;

	// Token: 0x040001AB RID: 427
	protected IEnumerator m_AbortCoroutine;

	// Token: 0x040001AC RID: 428
	private int showProgressCount;
}

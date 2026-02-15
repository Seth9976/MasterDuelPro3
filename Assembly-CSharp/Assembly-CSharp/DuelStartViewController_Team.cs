using System;
using YgomSystem.UI;

// Token: 0x02000024 RID: 36
public class DuelStartViewController_Team : DuelStartViewController
{
	// Token: 0x060000A1 RID: 161 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void Init()
	{
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void WaitInit()
	{
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void ControllVSImage()
	{
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void InitTimeLine()
	{
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void StartDuel()
	{
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void CauseError()
	{
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000029CC File Offset: 0x00000BCC
	public override bool OnResult(ViewController from, object value)
	{
		return false;
	}

	// Token: 0x040000CA RID: 202
	private bool isFinishedOverlay;

	// Token: 0x040000CB RID: 203
	private bool isCalledDuelBegin;
}

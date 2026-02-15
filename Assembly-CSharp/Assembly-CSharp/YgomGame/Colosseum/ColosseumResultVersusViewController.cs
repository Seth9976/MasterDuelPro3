using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x0200105B RID: 4187
	public class ColosseumResultVersusViewController : BaseMenuViewController
	{
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06007DC3 RID: 32195 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(int logoId, UnityAction onFinished)
		{
			return null;
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetGroup(int groupNo, long ownPercent, long rivalPercent, long totalPoint, bool isPlayEffect)
		{
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400B603 RID: 46595
		public const string PREF_PATH = "Colosseum/ColosseumResultVersus";

		// Token: 0x0400B604 RID: 46596
		private static readonly string ARGS_VERSUSID;

		// Token: 0x0400B605 RID: 46597
		private static readonly string ARGS_CALLBACK;

		// Token: 0x0400B606 RID: 46598
		private readonly string E_Image;

		// Token: 0x0400B607 RID: 46599
		private readonly string E_Button;

		// Token: 0x0400B608 RID: 46600
		private readonly string E_TextMyPoint;

		// Token: 0x0400B609 RID: 46601
		private readonly string E_TextMyPointLabel;

		// Token: 0x0400B60A RID: 46602
		private readonly string E_TextTotalPoint;

		// Token: 0x0400B60B RID: 46603
		private readonly string E_TextTotalPointLabel;

		// Token: 0x0400B60C RID: 46604
		private readonly string E_TextPercent;

		// Token: 0x0400B60D RID: 46605
		private readonly string E_TextBase;

		// Token: 0x0400B60E RID: 46606
		private readonly string E_Root;

		// Token: 0x0400B60F RID: 46607
		private readonly string E_ImageBg;

		// Token: 0x0400B610 RID: 46608
		private readonly string E_ImageMonster;

		// Token: 0x0400B611 RID: 46609
		private readonly string E_ImageIconEffLoop;

		// Token: 0x0400B612 RID: 46610
		private readonly string E_ImageIcon;

		// Token: 0x0400B613 RID: 46611
		private readonly string E_ImageIconEff;

		// Token: 0x0400B614 RID: 46612
		[SerializeField]
		private float totalAnimationTime;

		// Token: 0x0400B615 RID: 46613
		private int versus_id;

		// Token: 0x0400B616 RID: 46614
		private bool isStartedTween;
	}
}

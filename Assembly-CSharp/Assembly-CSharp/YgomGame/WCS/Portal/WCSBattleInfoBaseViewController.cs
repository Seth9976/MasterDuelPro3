using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.WCS.Portal
{
	// Token: 0x0200080D RID: 2061
	public class WCSBattleInfoBaseViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Go(WCSBattleInfoBaseViewController.ViewType type, ViewControllerManager manager)
		{
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartPolling()
		{
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndPolling()
		{
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Polling()
		{
			return null;
		}

		// Token: 0x040038F5 RID: 14581
		private const string ARG_KEY_PAGETYPE = "pagetype";

		// Token: 0x040038F6 RID: 14582
		private static readonly string[] VC_PATH;

		// Token: 0x040038F7 RID: 14583
		[SerializeField]
		private WCSBattleInfoBaseViewController.ViewType _viewType;

		// Token: 0x040038F8 RID: 14584
		private ElementObjectManager _scrollEom;

		// Token: 0x040038F9 RID: 14585
		private WCSBattleInfoBaseViewController.View _innerView;

		// Token: 0x040038FA RID: 14586
		private Func<Handle> _callPollingAPI;

		// Token: 0x040038FB RID: 14587
		private IEnumerator _pollingRoutine;

		// Token: 0x040038FC RID: 14588
		private Func<int> _pollingPeriodUpdater;

		// Token: 0x040038FD RID: 14589
		private bool _dryrun;

		// Token: 0x0200080E RID: 2062
		public enum ViewType
		{
			// Token: 0x040038FF RID: 14591
			LEAGUE,
			// Token: 0x04003900 RID: 14592
			FINAL
		}

		// Token: 0x0200080F RID: 2063
		public abstract class View
		{
			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x0000216D File Offset: 0x0000036D
			public string cwJsonPath
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x0000216D File Offset: 0x0000036D
			public Func<bool> isHoldingChecker
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x06003FD8 RID: 16344 RVA: 0x00002739 File Offset: 0x00000939
			public View(ElementObjectManager eom, ViewControllerManager manager)
			{
			}

			// Token: 0x06003FD9 RID: 16345 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Terminate()
			{
			}

			// Token: 0x06003FDA RID: 16346 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplyData()
			{
			}

			// Token: 0x06003FDB RID: 16347
			protected abstract void ApplyFromCW(object baseData);

			// Token: 0x04003901 RID: 14593
			protected ViewControllerManager _manager;

			// Token: 0x04003902 RID: 14594
			protected ElementObjectManager _eom;
		}
	}
}

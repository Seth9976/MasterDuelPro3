using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EF2 RID: 3826
	public class RunEffectWorker : AbstractRunEffectWorker
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06006F78 RID: 28536 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F79 RID: 28537 RVA: 0x0000216D File Offset: 0x0000036D
		public List<Engine.ViewType> busyEffectList
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06006F7A RID: 28538 RVA: 0x000029CC File Offset: 0x00000BCC
		public int runEffectParamsCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06006F7B RID: 28539 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F7C RID: 28540 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.ViewType currentViewType
		{
			[CompilerGenerated]
			get
			{
				return Engine.ViewType.Null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06006F7D RID: 28541 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInputViewType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06006F7E RID: 28542 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F7F RID: 28543 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.ViewType prevViewType
		{
			[CompilerGenerated]
			get
			{
				return Engine.ViewType.Null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F81 RID: 28545 RVA: 0x0000216D File Offset: 0x0000036D
		public int prevViewParam1
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06006F82 RID: 28546 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F83 RID: 28547 RVA: 0x0000216D File Offset: 0x0000036D
		public int prevViewParam2
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06006F84 RID: 28548 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F85 RID: 28549 RVA: 0x0000216D File Offset: 0x0000036D
		public int prevViewParam3
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F87 RID: 28551 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06006F88 RID: 28552 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelHUD duelHUD
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06006F89 RID: 28553 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F8A RID: 28554 RVA: 0x0000216D File Offset: 0x0000036D
		public bool selectAttacked
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06006F8B RID: 28555 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F8C RID: 28556 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelOkDialog okDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06006F8D RID: 28557 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F8E RID: 28558 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelConfirmDialog confirmDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06006F8F RID: 28559 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F90 RID: 28560 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelSelectDialog selectDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06006F91 RID: 28561 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F92 RID: 28562 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelPullDownDialog pullDownDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06006F93 RID: 28563 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F94 RID: 28564 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelDiceDialog diceDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06006F95 RID: 28565 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F96 RID: 28566 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelCoinDialog coinDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F98 RID: 28568 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelRitualDialog ritualDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F9A RID: 28570 RVA: 0x0000216D File Offset: 0x0000036D
		public InstantMessage instantMessage
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06006F9B RID: 28571 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F9C RID: 28572 RVA: 0x0000216D File Offset: 0x0000036D
		public InstantCardDisplay instantCardDisplay
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06006F9D RID: 28573 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F9E RID: 28574 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelInfoDialog infoDialog
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06006F9F RID: 28575 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FA0 RID: 28576 RVA: 0x0000216D File Offset: 0x0000036D
		public bool fieldViewing
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06006FA1 RID: 28577 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06006FA2 RID: 28578 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FA3 RID: 28579 RVA: 0x0000216D File Offset: 0x0000036D
		private bool inputting
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06006FA4 RID: 28580 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FA5 RID: 28581 RVA: 0x0000216D File Offset: 0x0000036D
		private string infoMessage
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

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06006FA6 RID: 28582 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FA7 RID: 28583 RVA: 0x0000216D File Offset: 0x0000036D
		public bool blockAutoSurrener
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FA9 RID: 28585 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRetryRequired
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06006FAA RID: 28586 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FAB RID: 28587 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isDuelLiveContinuousRequired
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06006FAC RID: 28588 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FAD RID: 28589 RVA: 0x0000216D File Offset: 0x0000036D
		public bool playingPhaseChangeEffect
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FAF RID: 28591 RVA: 0x0000216D File Offset: 0x0000036D
		public bool duelOver
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06006FB0 RID: 28592 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FB1 RID: 28593 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.MenuActType currentActType
		{
			[CompilerGenerated]
			get
			{
				return Engine.MenuActType.Null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FB3 RID: 28595 RVA: 0x0000216D File Offset: 0x0000036D
		public RunEffectWorker.CountDialogType currentCountDialog
		{
			[CompilerGenerated]
			get
			{
				return RunEffectWorker.CountDialogType.None;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FB5 RID: 28597 RVA: 0x0000216D File Offset: 0x0000036D
		public bool discardReady
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FB7 RID: 28599 RVA: 0x0000216D File Offset: 0x0000036D
		public int discardRemain
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06006FB8 RID: 28600 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FB9 RID: 28601 RVA: 0x0000216D File Offset: 0x0000036D
		public string discardMessage
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

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06006FBA RID: 28602 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FBB RID: 28603 RVA: 0x0000216D File Offset: 0x0000036D
		public int totalLevel
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FBD RID: 28605 RVA: 0x0000216D File Offset: 0x0000036D
		public int ritualRemain
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06006FBE RID: 28606 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FBF RID: 28607 RVA: 0x0000216D File Offset: 0x0000036D
		public int dlgTextId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06006FC0 RID: 28608 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FC1 RID: 28609 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.DialogRitualType ritualType
		{
			[CompilerGenerated]
			get
			{
				return Engine.DialogRitualType.Ritual;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06006FC2 RID: 28610 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FC3 RID: 28611 RVA: 0x0000216D File Offset: 0x0000036D
		public int lastDialogTextId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FC5 RID: 28613 RVA: 0x0000216D File Offset: 0x0000036D
		public int lastYesNoDialogEffectTextid
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06006FC6 RID: 28614 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FC7 RID: 28615 RVA: 0x0000216D File Offset: 0x0000036D
		public bool openCardInfoRByEffectIdFlag
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06006FC8 RID: 28616 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FC9 RID: 28617 RVA: 0x0000216D File Offset: 0x0000036D
		public bool dlgTypeOkResult
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06006FCA RID: 28618 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FCB RID: 28619 RVA: 0x0000216D File Offset: 0x0000036D
		public bool surrendered
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06006FCC RID: 28620 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FCD RID: 28621 RVA: 0x0000216D File Offset: 0x0000036D
		public bool duelStart
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06006FCE RID: 28622 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FCF RID: 28623 RVA: 0x0000216D File Offset: 0x0000036D
		private Dictionary<int, int> lp
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

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FD1 RID: 28625 RVA: 0x0000216D File Offset: 0x0000036D
		private Dictionary<int, int> lpMin
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

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06006FD2 RID: 28626 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FD3 RID: 28627 RVA: 0x0000216D File Offset: 0x0000036D
		public int battleSrcPlayer
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06006FD4 RID: 28628 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FD5 RID: 28629 RVA: 0x0000216D File Offset: 0x0000036D
		public int battleSrcPosition
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06006FD6 RID: 28630 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FD7 RID: 28631 RVA: 0x0000216D File Offset: 0x0000036D
		public int battleDstPlayer
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06006FD8 RID: 28632 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FD9 RID: 28633 RVA: 0x0000216D File Offset: 0x0000036D
		public int battleDstPosition
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06006FDA RID: 28634 RVA: 0x000F61C8 File Offset: 0x000F43C8
		// (set) Token: 0x06006FDB RID: 28635 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 attackDirection
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06006FDC RID: 28636 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FDD RID: 28637 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.CutinSummonType cutinSummonType
		{
			[CompilerGenerated]
			get
			{
				return Engine.CutinSummonType.Normal;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06006FDE RID: 28638 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FDF RID: 28639 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentSelectingPlayer
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06006FE0 RID: 28640 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FE1 RID: 28641 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentSelectingPosition
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006FE3 RID: 28643 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentSelectingIndex
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FE5 RID: 28645 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionZoneIconController selectionZoneIcon
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FE7 RID: 28647 RVA: 0x0000216D File Offset: 0x0000036D
		public CommandOperation commandOperation
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FE9 RID: 28649 RVA: 0x0000216D File Offset: 0x0000036D
		public DecideOperation decideOperation
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FEB RID: 28651 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectStandOperation selectStandOperation
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FED RID: 28653 RVA: 0x0000216D File Offset: 0x0000036D
		public PhaseSelect3D phaseSelect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FEF RID: 28655 RVA: 0x0000216D File Offset: 0x0000036D
		public FusionEffect fusionEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x0000216D File Offset: 0x0000036D
		public XyzEffect xyzEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x0000216D File Offset: 0x0000036D
		public LinkEffect linkEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x0000216D File Offset: 0x0000036D
		public SynchroEffect synchroEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FF7 RID: 28663 RVA: 0x0000216D File Offset: 0x0000036D
		public RitualEffect ritualEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FF9 RID: 28665 RVA: 0x0000216D File Offset: 0x0000036D
		public PendulumEffect pendulumEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06006FFA RID: 28666 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FFB RID: 28667 RVA: 0x0000216D File Offset: 0x0000036D
		public PendulumReadyEffect pendulumReadyEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06006FFC RID: 28668 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FFD RID: 28669 RVA: 0x0000216D File Offset: 0x0000036D
		public MonsterCutinEffect monsterCutinEffect
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

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06006FFE RID: 28670 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006FFF RID: 28671 RVA: 0x0000216D File Offset: 0x0000036D
		public RunCoin runCoin
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

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06007000 RID: 28672 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007001 RID: 28673 RVA: 0x0000216D File Offset: 0x0000036D
		public RunDice runDice
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06007002 RID: 28674 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007003 RID: 28675 RVA: 0x0000216D File Offset: 0x0000036D
		public ResidualEffect residualEffect
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

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06007004 RID: 28676 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007005 RID: 28677 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelChainManager chainManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06007006 RID: 28678 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007007 RID: 28679 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlayingMonsterCutin
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007009 RID: 28681 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMonsterCutinDone
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600700A RID: 28682 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600700B RID: 28683 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSpSummonFromExDeckMyself
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x0600700C RID: 28684 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600700D RID: 28685 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSpSummonFromExDeckRival
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600700E RID: 28686 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600700F RID: 28687 RVA: 0x0000216D File Offset: 0x0000036D
		public int lastCardID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06007010 RID: 28688 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007011 RID: 28689 RVA: 0x0000216D File Offset: 0x0000036D
		public int lastUniqueID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06007012 RID: 28690 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007013 RID: 28691 RVA: 0x0000216D File Offset: 0x0000036D
		private int attackerUniqueID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06007014 RID: 28692 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007015 RID: 28693 RVA: 0x0000216D File Offset: 0x0000036D
		public bool inputAvailable
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06007016 RID: 28694 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007017 RID: 28695 RVA: 0x0000216D File Offset: 0x0000036D
		public FinalBlowEffect finalBlow
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06007018 RID: 28696 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007019 RID: 28697 RVA: 0x0000216D File Offset: 0x0000036D
		public LethalEffect lethalEffect
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600701A RID: 28698 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool lethalEffectPlayed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600701B RID: 28699 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600701C RID: 28700 RVA: 0x0000216D File Offset: 0x0000036D
		public bool lethalEffectPlayedMyself
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600701D RID: 28701 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600701E RID: 28702 RVA: 0x0000216D File Offset: 0x0000036D
		public bool lethalEffectPlayedRival
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600701F RID: 28703 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007020 RID: 28704 RVA: 0x0000216D File Offset: 0x0000036D
		public SpecialWinBase specialWinEffect
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

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06007021 RID: 28705 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007022 RID: 28706 RVA: 0x0000216D File Offset: 0x0000036D
		public DrawOperation drawOperation
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

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06007023 RID: 28707 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool drawOperationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06007024 RID: 28708 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007025 RID: 28709 RVA: 0x0000216D File Offset: 0x0000036D
		public int preCardMoveFromPlayer
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06007026 RID: 28710 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007027 RID: 28711 RVA: 0x0000216D File Offset: 0x0000036D
		public int preCardMoveFromPosition
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPreparedToDuel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isShownUp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600702A RID: 28714 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600702B RID: 28715 RVA: 0x0000216D File Offset: 0x0000036D
		public override bool isTerminated
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x000F61DE File Offset: 0x000F43DE
		public RunEffectWorker(DuelClient host)
			: base(null)
		{
		}

		// Token: 0x0600702D RID: 28717 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeProcess()
		{
			return null;
		}

		// Token: 0x0600702E RID: 28718 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareToDuel()
		{
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGoManager(DuelGameObjectManager goManager)
		{
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PrepareToDuelProcess()
		{
			return null;
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06007033 RID: 28723 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPreRunEffect(Engine.ViewType viewtype, int param1, int param2, int param3)
		{
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPostRunEffect(Engine.ViewType viewtype, int param1, int param2, int param3)
		{
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsBusyEffect(Engine.ViewType viewType)
		{
			return false;
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowUpOnStartDuel(bool playEffect)
		{
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x0000216D File Offset: 0x0000036D
		public void DispSelectingCursor(int team, int position, int index)
		{
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideSelectingCursor()
		{
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshSelectingCursor()
		{
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenPvpNoResponse()
		{
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x0000216D File Offset: 0x0000036D
		public void PvpResponsed()
		{
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClosedPvpNoResponse()
		{
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdate()
		{
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecDuelStep()
		{
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueTask(Engine.ViewType viewType, RunEffectWorker.createDelegate createFunc, int param1, int param2, int param3)
		{
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueTask(Engine.ViewType viewType, RunEffectWorker.preCreateDelegate preCreateFunc, RunEffectWorker.createDelegateAdvanced createFuncAdvanced, int param1, int param2, int param3)
		{
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueTaskImpl(RunEffectWorker.RunEffectParam runEffectParam)
		{
		}

		// Token: 0x06007044 RID: 28740 RVA: 0x0000216D File Offset: 0x0000036D
		private void DequeueTask()
		{
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBusyIds()
		{
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x0000216D File Offset: 0x0000036D
		private void PhaseChange(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x0000216D File Offset: 0x0000036D
		private void TurnChange(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x0000216D File Offset: 0x0000036D
		private void TurnChangeMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelStart(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeOnDuelStart(bool playBGM, bool playEntryAnime)
		{
		}

		// Token: 0x0600704B RID: 28747 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelEnd(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600704C RID: 28748 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleAttack(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600704D RID: 28749 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleAttackMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x0000216D File Offset: 0x0000036D
		private void LifeSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x0000216D File Offset: 0x0000036D
		private void LifeSetMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x0000216D File Offset: 0x0000036D
		private void LifeDamage(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x0000216D File Offset: 0x0000036D
		private void LifeDamageMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardIncTurn(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunFusion(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinDraw(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinSummon(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinActivate(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007058 RID: 28760 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinActivateMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007059 RID: 28761 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinTurnEnd(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinTurnEndMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x0000216D File Offset: 0x0000036D
		private void ManaSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x0000216D File Offset: 0x0000036D
		private void TuningSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x0000216D File Offset: 0x0000036D
		private void TuningReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x0000216D File Offset: 0x0000036D
		private void TuningRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007060 RID: 28768 RVA: 0x0000216D File Offset: 0x0000036D
		private void CutinCoinDice(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitFrame(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitInput(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007063 RID: 28771 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunDialog(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunList(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleInit(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleInitMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleEnd(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x0000216D File Offset: 0x0000036D
		private void HandShuffle(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x0000216D File Offset: 0x0000036D
		private void HandOpen(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeckShuffle(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeckFlipTop(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeckReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x0000216D File Offset: 0x0000036D
		private void GraveTop(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardMove(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007070 RID: 28784 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardMoveMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardSwap(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardFlipTurn(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardFlipTurnMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardCheat(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardVanish(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardBreak(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardBreakMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardExplosion(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007079 RID: 28793 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardExclude(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707A RID: 28794 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardDisable(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707B RID: 28795 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardEquip(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707C RID: 28796 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardUpdate(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707D RID: 28797 RVA: 0x0000216D File Offset: 0x0000036D
		private void MonstShuffle(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707E RID: 28798 RVA: 0x0000216D File Offset: 0x0000036D
		private void TributeSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600707F RID: 28799 RVA: 0x0000216D File Offset: 0x0000036D
		private void TributeReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x0000216D File Offset: 0x0000036D
		private void TributeRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x0000216D File Offset: 0x0000036D
		private void MaterialSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x0000216D File Offset: 0x0000036D
		private void MaterialReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x0000216D File Offset: 0x0000036D
		private void MaterialRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainRunMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunSummon(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunSpSummon(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunCoin(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007089 RID: 28809 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunDice(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708A RID: 28810 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunSpecialWin(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x0000216D File Offset: 0x0000036D
		private void OverlaySet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x0000216D File Offset: 0x0000036D
		private void OverlayReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x0000216D File Offset: 0x0000036D
		private void OverlayRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x0000216D File Offset: 0x0000036D
		private void LinkSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x0000216D File Offset: 0x0000036D
		private void LinkReset(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x0000216D File Offset: 0x0000036D
		private void LinkRun(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainStep(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunJanken(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunSpecialefx(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunSpecialefxMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunVija(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x0000216D File Offset: 0x0000036D
		private void BattleSelect(int player, int param2, int param3)
		{
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardLockon(int player, int posIdx, int type)
		{
		}

		// Token: 0x06007098 RID: 28824 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardHappen(int param1, int param2, int param3)
		{
		}

		// Token: 0x06007099 RID: 28825 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardHappenMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainSet(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainSetMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainEnd(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChainEndMinimum(int param1, int param2, int param3)
		{
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x0000216D File Offset: 0x0000036D
		private void CpuThinking(int iPlayer, int end, int param3)
		{
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetIndexByViewIndex(int player, int position, int viewIndex)
		{
			return 0;
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetViewIndex(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x0000216D File Offset: 0x0000036D
		private void TapLocatorUnknown(int player, int position, int index)
		{
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTapDownField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A3 RID: 28835 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTapUpField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCursorEnter(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCursorExit(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselectField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFocusField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayScreenEffect()
		{
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStopScreenEffect()
		{
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAudienceReplayFinished()
		{
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupInfoDialogShowPos(int focusTeam, int focusPosition, bool immediate)
		{
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUnfocusField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDecideField(int team, int position, int viewIndex)
		{
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDoubleClickField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDragFieldBegin(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDragField(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060070B2 RID: 28850 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDragFieldEnd(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHoldFieldBegin(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDecideAttackTarget(int attackerPlayer, int attackerPosition, int attackerIndex, int targetPlayer, int targetPosition, int targetIndex)
		{
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ExecuteCommandLocation()
		{
			return false;
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetCommandOperation()
		{
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnDialogTrue()
		{
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnDialogFalse()
		{
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnDialogTrueAndFree(bool abort)
		{
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnDialogFalseAndFree(bool abort)
		{
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnDialogTrueAndFreeWithoutSave(bool abort)
		{
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInfoMessage(string text, bool isForever = false)
		{
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x0000216A File Offset: 0x0000036A
		public string UseInfoMessage()
		{
			return null;
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearInfoMessage(bool alsoForever = false)
		{
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x0000216D File Offset: 0x0000036D
		private void CloseEmotionalList(bool forceclose = false)
		{
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x0000216D File Offset: 0x0000036D
		private void ListClosed()
		{
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseSpecialDialog()
		{
		}

		// Token: 0x060070C2 RID: 28866 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResumeSpecialDialog()
		{
		}

		// Token: 0x060070C3 RID: 28867 RVA: 0x0000216D File Offset: 0x0000036D
		public void RestartSpecialDialog()
		{
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAnyDialogShowing(bool ignoreFieldViewing = false)
		{
			return false;
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnBackEvent()
		{
			return false;
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x0000216D File Offset: 0x0000036D
		private void listCardSelected(Engine.CardStatus cs, bool isKnown)
		{
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStartInput()
		{
		}

		// Token: 0x060070C8 RID: 28872 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateTimeLimit()
		{
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCheckTimeOver()
		{
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateNoOperation()
		{
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartInput(bool changeCameraView, bool fromPvpNoReponse = false)
		{
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishInput(bool changeCameraView, bool fromPvpNoResponce = false)
		{
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowTimer()
		{
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeFieldViewMode(bool bView)
		{
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x0000216D File Offset: 0x0000036D
		public void DuelEnd()
		{
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x0000216D File Offset: 0x0000036D
		public void AbortInput(bool allHUD = true)
		{
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSurrender(int param1, int param2, int param3)
		{
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSurrender()
		{
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenConfirmDialog(string message, string rightButtonText, string leftButtonText, Action<DuelConfirmDialog.Result, bool> resultCallback, Action openCallback, bool useFieldView)
		{
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCommandDecideInExceptField()
		{
			return false;
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x0000216D File Offset: 0x0000036D
		public void BattlePositionSelectStart(int uid, int face)
		{
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ExecuteSpSummonLocation()
		{
			return false;
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetCursorToListIfOpenning()
		{
			return false;
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetCursorToCommandIfOpenning()
		{
			return false;
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x0000216D File Offset: 0x0000036D
		public void FocusCard(int team, int position, int index, DuelClient.FocusCardSituation situation)
		{
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnfocusCard()
		{
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnShowAffectEffect(int team, int position, int index)
		{
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHideAffectEffect()
		{
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginAttackTargeting(int attackPlayer, int attackPosition, int targetPlayer, int targetPosition)
		{
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAttackTargetingLineDisp(bool disp)
		{
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x000F61E8 File Offset: 0x000F43E8
		public ValueTuple<int, int, int, int> GetCurrentAttackTargetingInfo()
		{
			return default(ValueTuple<int, int, int, int>);
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndAttackTargeting()
		{
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishWaitInput()
		{
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x0000216D File Offset: 0x0000036D
		public void HighlightAvailablePlaces(bool enable, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x0000216A File Offset: 0x0000036A
		public SummonEffectBase GetPlayingSummonEffect()
		{
			return null;
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x000F6200 File Offset: 0x000F4400
		public ValueTuple<Vector3, Quaternion, Vector3> PlayDecideEffect(int player, int position, bool ignoreCard, Action onFinished)
		{
			return default(ValueTuple<Vector3, Quaternion, Vector3>);
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectLastHappenedCard()
		{
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAttackReady(int player, int position)
		{
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAttackReady(int uniqueID)
		{
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishAttackReady()
		{
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAttackReadyCard(int uniqueID)
		{
			return false;
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLethalEffect(int loser, Action onFinished, bool useEffect, Vector3 effectPosition, bool draw, bool isDeckOut, LethalEffect.EffectType type = LethalEffect.EffectType.Normal)
		{
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLethalEffectPlayedPlayer(int player)
		{
			return false;
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndCommand()
		{
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLocalLP(int player)
		{
			return 0;
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddDamageLocalLP(int player, int damage)
		{
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLocalLP(int player, int lp)
		{
		}

		// Token: 0x060070F0 RID: 28912 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupLocalLP()
		{
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupLocalLPMin()
		{
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMinLP(int player, int lpt, bool force)
		{
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x0000216A File Offset: 0x0000036A
		public ZoneCard GetZoneCard(int player, int position, ZoneCard.Mode mode)
		{
			return null;
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assert(string msg)
		{
		}

		// Token: 0x0400AA75 RID: 43637
		private RunEffectWorker.Step step;

		// Token: 0x0400AA76 RID: 43638
		private List<RunEffectWorker.RunEffectParam> runEffectParams;

		// Token: 0x0400AA77 RID: 43639
		private Coroutine initializeCoroutine;

		// Token: 0x0400AA78 RID: 43640
		private Coroutine prepareToDuelCoroutine;

		// Token: 0x0400AA79 RID: 43641
		private bool initialized;

		// Token: 0x0400AA7A RID: 43642
		private MessageDialog messageDialogNoResponse;

		// Token: 0x0400AA7B RID: 43643
		private MessageDialog messageDialogNoOperation;

		// Token: 0x0400AA7C RID: 43644
		private bool isForeverInfoMessage;

		// Token: 0x0400AA7D RID: 43645
		private float noOperationTimer;

		// Token: 0x0400AA7E RID: 43646
		private const float noOperationWarningSecond = 30f;

		// Token: 0x0400AA7F RID: 43647
		private const float noOperationSurrenderSecond = 40f;

		// Token: 0x0400AA80 RID: 43648
		public const int SELECT_LOCATION_TEXT_ID = 326;

		// Token: 0x0400AA81 RID: 43649
		public const int UNUSED_ZONE_TEXT_ID = 155;

		// Token: 0x0400AA82 RID: 43650
		public const int SELECT_SIMPLE_TEXT_ID = 591;

		// Token: 0x0400AA83 RID: 43651
		public int attackedMonster;

		// Token: 0x0400AA84 RID: 43652
		public bool autoAttack;

		// Token: 0x0400AA85 RID: 43653
		private Engine.StepType m_BPStep;

		// Token: 0x0400AA86 RID: 43654
		private Engine.DmgStepType m_DMGStep;

		// Token: 0x0400AA87 RID: 43655
		public int[,] tmpFacedCard;

		// Token: 0x0400AA88 RID: 43656
		private float localLeftTime;

		// Token: 0x0400AA89 RID: 43657
		private float localTotalTime;

		// Token: 0x0400AA8A RID: 43658
		private float localTurn;

		// Token: 0x0400AA8B RID: 43659
		private Engine.Phase localPhase;

		// Token: 0x0400AA8C RID: 43660
		private bool cardHappenTaskEnable;

		// Token: 0x0400AA8D RID: 43661
		public SelectingCursorManager selCursorMan;

		// Token: 0x0400AA8E RID: 43662
		private AttackTargetingOperation attackTargetingOperation;

		// Token: 0x0400AA8F RID: 43663
		private bool inputBlockActivated;

		// Token: 0x0400AA90 RID: 43664
		private ZoneCard zoneCardNearGraveIn;

		// Token: 0x0400AA91 RID: 43665
		private ZoneCard zoneCardNearGraveOut;

		// Token: 0x0400AA92 RID: 43666
		private ZoneCard zoneCardNearExcludeIn;

		// Token: 0x0400AA93 RID: 43667
		private ZoneCard zoneCardNearExcludeOut;

		// Token: 0x0400AA94 RID: 43668
		private ZoneCard zoneCardFarGraveIn;

		// Token: 0x0400AA95 RID: 43669
		private ZoneCard zoneCardFarGraveOut;

		// Token: 0x0400AA96 RID: 43670
		private ZoneCard zoneCardFarExcludeIn;

		// Token: 0x0400AA97 RID: 43671
		private ZoneCard zoneCardFarExcludeOut;

		// Token: 0x0400AA98 RID: 43672
		private bool preparedToDuel;

		// Token: 0x0400AA99 RID: 43673
		private bool _isTerminated;

		// Token: 0x02000EF3 RID: 3827
		private enum Step
		{
			// Token: 0x0400AA9B RID: 43675
			Initializing,
			// Token: 0x0400AA9C RID: 43676
			Initialized,
			// Token: 0x0400AA9D RID: 43677
			Preparing,
			// Token: 0x0400AA9E RID: 43678
			ExecDuel,
			// Token: 0x0400AA9F RID: 43679
			Terminating,
			// Token: 0x0400AAA0 RID: 43680
			Finish
		}

		// Token: 0x02000EF4 RID: 3828
		// (Invoke) Token: 0x060070F6 RID: 28918
		private delegate Dictionary<string, object> preCreateDelegate(RunEffectWorker worker, int param1, int param2, int param3);

		// Token: 0x02000EF5 RID: 3829
		// (Invoke) Token: 0x060070FA RID: 28922
		private delegate EffectTask createDelegate(RunEffectWorker worker, int param1, int param2, int param3);

		// Token: 0x02000EF6 RID: 3830
		// (Invoke) Token: 0x060070FE RID: 28926
		private delegate EffectTask createDelegateAdvanced(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork);

		// Token: 0x02000EF7 RID: 3831
		private class RunEffectParam
		{
			// Token: 0x0400AAA1 RID: 43681
			public Engine.ViewType viewType;

			// Token: 0x0400AAA2 RID: 43682
			public RunEffectWorker.createDelegate createFunc;

			// Token: 0x0400AAA3 RID: 43683
			public RunEffectWorker.createDelegateAdvanced createFuncAdvanced;

			// Token: 0x0400AAA4 RID: 43684
			public int param1;

			// Token: 0x0400AAA5 RID: 43685
			public int param2;

			// Token: 0x0400AAA6 RID: 43686
			public int param3;

			// Token: 0x0400AAA7 RID: 43687
			public EffectTask task;

			// Token: 0x0400AAA8 RID: 43688
			public Dictionary<string, object> immediateWork;
		}

		// Token: 0x02000EF8 RID: 3832
		public enum CountDialogType
		{
			// Token: 0x0400AAAA RID: 43690
			None,
			// Token: 0x0400AAAB RID: 43691
			Discard,
			// Token: 0x0400AAAC RID: 43692
			Ritual
		}
	}
}

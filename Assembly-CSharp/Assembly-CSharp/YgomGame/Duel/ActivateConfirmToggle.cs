using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Settings;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000C72 RID: 3186
	public class ActivateConfirmToggle : MonoBehaviour
	{
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0000216D File Offset: 0x0000036D
		public bool forceCancelMode
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

		// Token: 0x06005B4D RID: 23373 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ElementObjectManager ui, DuelHUD duelHUD)
		{
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickToggle(bool playActionAnime)
		{
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMode(DuelClient.ActivateConfirmMode mode)
		{
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetManualType(SettingsUtil.DuelParam.MANUAL_TYPE type)
		{
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIcon(DuelClient.ActivateConfirmMode mode)
		{
		}

		// Token: 0x0400966C RID: 38508
		private DuelHUD duelHUD;

		// Token: 0x0400966D RID: 38509
		private ElementObjectManager ui;

		// Token: 0x0400966E RID: 38510
		private SelectionButton button;

		// Token: 0x0400966F RID: 38511
		private SelectionItem shortcut;

		// Token: 0x04009670 RID: 38512
		private GameObject autoIcon;

		// Token: 0x04009671 RID: 38513
		private GameObject onIcon;

		// Token: 0x04009672 RID: 38514
		private GameObject offIcon;

		// Token: 0x04009673 RID: 38515
		private GameObject shortcutIcon;

		// Token: 0x04009674 RID: 38516
		private Image autoIconImage;

		// Token: 0x04009675 RID: 38517
		private Image onIconImage;

		// Token: 0x04009676 RID: 38518
		private Image offIconImage;

		// Token: 0x04009677 RID: 38519
		private DuelClient.ActivateConfirmMode reqMode;

		// Token: 0x04009678 RID: 38520
		private DuelClient.ActivateConfirmMode buttonMode;

		// Token: 0x04009679 RID: 38521
		private float setOnTimer;

		// Token: 0x0400967A RID: 38522
		private float setOffTimer;

		// Token: 0x0400967B RID: 38523
		private const float setDelayTime = 0.2f;

		// Token: 0x0400967C RID: 38524
		private bool reqDelaySet;

		// Token: 0x0400967D RID: 38525
		private bool callbackRegisted;

		// Token: 0x0400967E RID: 38526
		private List<uint> callbackIDs;
	}
}

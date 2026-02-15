using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI
{
	// Token: 0x02000602 RID: 1538
	public class SystemDialog : MonoBehaviour
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistsDialog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000029CC File Offset: 0x00000BCC
		private int defaultMinHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OpenFatalErrorDialog(string message, string label1 = null, Action action1 = null, Dictionary<string, object> args = null)
		{
			return false;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetMaintenanceDialogArgs()
		{
			return null;
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenOperationMainteDialog(Dictionary<string, object> defaultArgs = null, Dictionary<string, object> overrideArgs = null)
		{
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OpenMaintenanceDialog(Dictionary<string, object> args)
		{
			return false;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OpenSystemDialog(List<IEntryData> entryDatas, Dictionary<string, object> args = null)
		{
			return false;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator waitLocalizedFontsLoad(Action callback)
		{
			return null;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSystemDialogCore(List<IEntryData> entryDatas, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0000216D File Offset: 0x0000036D
		private void CloseSystemDialog()
		{
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearCallbacks()
		{
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnReboot()
		{
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickReboot()
		{
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickAction1()
		{
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickAction2()
		{
		}

		// Token: 0x04002D9D RID: 11677
		public const string k_ArgKeyTitle = "title";

		// Token: 0x04002D9E RID: 11678
		public const string k_ArgKeyUpperMessage = "upperMessage";

		// Token: 0x04002D9F RID: 11679
		public const string k_ArgKeyLowerMessage = "lowerMessage";

		// Token: 0x04002DA0 RID: 11680
		public const string k_ArgKeyLowerMessageScrollHeight = "lowerMessageScrollHeight";

		// Token: 0x04002DA1 RID: 11681
		public const string k_ArgKeyTime = "time";

		// Token: 0x04002DA2 RID: 11682
		public const string k_ArgKeyImagePath = "imagePath";

		// Token: 0x04002DA3 RID: 11683
		public const string k_ArgKeyImageScale = "imageScale";

		// Token: 0x04002DA4 RID: 11684
		public const string k_ArgKeyImageVisible = "imageVisible";

		// Token: 0x04002DA5 RID: 11685
		public const string k_ArgKeyButtonLabel = "buttonLabel";

		// Token: 0x04002DA6 RID: 11686
		public const string k_ArgKeyAction = "action";

		// Token: 0x04002DA7 RID: 11687
		public const string k_ArgKeyButtonLabel2 = "buttonLabel2";

		// Token: 0x04002DA8 RID: 11688
		public const string k_ArgKeyAction2 = "action2";

		// Token: 0x04002DA9 RID: 11689
		public const string k_ArgKeyRebootButtonVisible = "rebootVisible";

		// Token: 0x04002DAA RID: 11690
		public const string k_ArgKeyWindowWidth = "windowWidth";

		// Token: 0x04002DAB RID: 11691
		public const string k_ArgKeyOpenSe = "opense";

		// Token: 0x04002DAC RID: 11692
		private readonly string k_MaintenanceBannerPath;

		// Token: 0x04002DAD RID: 11693
		[SerializeField]
		private ElementObjectManager m_SystemDialogPref;

		// Token: 0x04002DAE RID: 11694
		private CommonDialogContentContainerWidget m_SystemDialogWidget;

		// Token: 0x04002DAF RID: 11695
		private IEnumerator m_FontLoadCoroutine;

		// Token: 0x04002DB0 RID: 11696
		private Action m_ActionCallback1;

		// Token: 0x04002DB1 RID: 11697
		private Action m_ActionCallback2;
	}
}

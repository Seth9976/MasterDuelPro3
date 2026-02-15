using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

// Token: 0x02000057 RID: 87
public class TutorialNavigatorTest : MonoBehaviour
{
	// Token: 0x0600018E RID: 398 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnDestroy()
	{
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000216D File Offset: 0x0000036D
	private void PushMessage(string message)
	{
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000216D File Offset: 0x0000036D
	private void ClearMessages()
	{
	}

	// Token: 0x04000216 RID: 534
	private const string MSG_STACK_BTN_LABEL = "Stack";

	// Token: 0x04000217 RID: 535
	private const string STACK_MSG_AREA_TEXT_LABEL = "StackMsg";

	// Token: 0x04000218 RID: 536
	[SerializeField]
	private float _centerMsgDelay;

	// Token: 0x04000219 RID: 537
	private ElementObjectManager _eoManager;

	// Token: 0x0400021A RID: 538
	private ElementObjectManager _inputFieldMgr;

	// Token: 0x0400021B RID: 539
	private ExtendedInputField _inputField;

	// Token: 0x0400021C RID: 540
	private MDText _stackMsgText;

	// Token: 0x0400021D RID: 541
	private IList<string> _messages;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.TextIDs;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Tutorial
{
	// Token: 0x0200083C RID: 2108
	public class TutorialNavigator : MonoBehaviour
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool topMsgShowing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool centerMsgShowing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Transform topMsgUIParent = null, Transform centerMsgUIParent = null)
		{
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Close()
		{
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayTopMsg(IDS_TUTORIAL id, float delay = 0f)
		{
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayTopMsg(string message, float delay = 0f)
		{
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopTopMsg()
		{
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayCenterMsg(IList<IDS_TUTORIAL> msgIDs, UnityAction onMsgEnd, float delay = 0f)
		{
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayCenterMsg(IList<string> messages, UnityAction onMsgEnd, float delay = 0f)
		{
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopCenterMsg()
		{
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Setup(int selectorPriority)
		{
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(Transform topMsgUIParent, Transform centerMsgUIParent)
		{
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectorPriority(int priority)
		{
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x0000216D File Offset: 0x0000036D
		public void Dispose()
		{
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowTopMessageInternal(float delay)
		{
			return null;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopCenterMsgCore(bool disableActive = true)
		{
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayCenterMsgCore(UnityAction onMsgEnd, float delay, UnityAction onPreProcess)
		{
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowCenterMessageInternal(UnityAction onMsgEnd, float delay)
		{
			return null;
		}

		// Token: 0x04003A01 RID: 14849
		private const string VC_PREFAB_PATH = "Tutorial/TutorialNavigator";

		// Token: 0x04003A02 RID: 14850
		private const string TOP_MESSAGE_LABEL = "TopMessage";

		// Token: 0x04003A03 RID: 14851
		private const string CENTER_MESSAGE_LABEL = "CenterMessage";

		// Token: 0x04003A04 RID: 14852
		private const string TOP_MSG_TEXT_LABEL = "TopMsgText";

		// Token: 0x04003A05 RID: 14853
		private const string CENTER_MSG_TEXT_LABEL = "CenterMsgText";

		// Token: 0x04003A06 RID: 14854
		public static readonly int DEFAULT_SELECTOR_PRIORITY;

		// Token: 0x04003A07 RID: 14855
		private static TutorialNavigator _instance;

		// Token: 0x04003A08 RID: 14856
		[SerializeField]
		private ElementObjectManager _prefabUI;

		// Token: 0x04003A09 RID: 14857
		private ElementObjectManager _ui;

		// Token: 0x04003A0A RID: 14858
		private ElementObject _topUI;

		// Token: 0x04003A0B RID: 14859
		private ElementObject _centerUI;

		// Token: 0x04003A0C RID: 14860
		private ExtendedTextMeshProUGUI _topText;

		// Token: 0x04003A0D RID: 14861
		private ExtendedTextMeshProUGUI _centerText;

		// Token: 0x04003A0E RID: 14862
		private IEnumerator _coroutineTopMsgShowing;

		// Token: 0x04003A0F RID: 14863
		private IEnumerator _coroutineCenterMsgShowing;

		// Token: 0x04003A10 RID: 14864
		private string _tmpTopMsg;

		// Token: 0x04003A11 RID: 14865
		private List<string> _tmpCenterMsgList;

		// Token: 0x04003A12 RID: 14866
		private UnityAction _onEndOfCenterMsg;

		// Token: 0x04003A13 RID: 14867
		private int _setupCount;
	}
}

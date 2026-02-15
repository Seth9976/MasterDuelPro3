using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame
{
	// Token: 0x020007D4 RID: 2004
	public class PopUpTextManager
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Initialized
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

		// Token: 0x06003E7A RID: 15994 RVA: 0x0000216A File Offset: 0x0000036A
		public static PopUpTextManager Create(UnityAction onFinish = null)
		{
			return null;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(UnityAction onFinish)
		{
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegistPopUpCallback(SelectionButton sbtn, Func<string> text, bool isforui = true, PopUpTextManager.Mode mode = PopUpTextManager.Mode.OnPointerEnter)
		{
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnterPopUpArea(SelectionButton sbtn, PopUpTextManager.Mode mode = PopUpTextManager.Mode.OnPointerEnter)
		{
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnExitPopUpArea(SelectionButton sbtn)
		{
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemovePopUpCallback(SelectionButton sbtn, PopUpTextManager.Mode mode = PopUpTextManager.Mode.OnPointerEnter)
		{
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdatePopUpText(SelectionButton sbtn, Func<string> text, PopUpTextManager.Mode mode = PopUpTextManager.Mode.OnPointerEnter)
		{
			return false;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateTextData(SelectionButton sbtn, PopUpTextManager.Mode mode, Func<string> text)
		{
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0000216D File Offset: 0x0000036D
		public void HidePopUpText()
		{
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0000216A File Offset: 0x0000036A
		private PopUpText GetPutInstance()
		{
			return null;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreatePutInstance()
		{
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnInstance(PopUpText instance)
		{
		}

		// Token: 0x0400377A RID: 14202
		private GameObject m_Template;

		// Token: 0x0400377B RID: 14203
		private Queue<PopUpText> m_FreeInstanceQueue;

		// Token: 0x0400377C RID: 14204
		private Queue<PopUpText> m_AllInstanceQueue;

		// Token: 0x0400377D RID: 14205
		private Dictionary<PopUpTextManager.Mode, Dictionary<SelectionButton, PopUpTextManager.PUTData>> m_PopUpBtnTable;

		// Token: 0x0400377E RID: 14206
		private Dictionary<SelectionButton, PopUpText> m_SbtnPutTable;

		// Token: 0x020007D5 RID: 2005
		private struct PUTData
		{
			// Token: 0x0400377F RID: 14207
			public Func<string> text;

			// Token: 0x04003780 RID: 14208
			public bool isforui;
		}

		// Token: 0x020007D6 RID: 2006
		public enum Mode
		{
			// Token: 0x04003782 RID: 14210
			OnPointerEnter,
			// Token: 0x04003783 RID: 14211
			OnSelected
		}
	}
}

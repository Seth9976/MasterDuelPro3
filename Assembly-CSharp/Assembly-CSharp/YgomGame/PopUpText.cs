using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame
{
	// Token: 0x020007D1 RID: 2001
	public class PopUpText : MonoBehaviour
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsShowing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnable(bool enable)
		{
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowText(string text, Vector3 worldPos, bool isforui)
		{
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideText()
		{
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateText(string text)
		{
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(UnityAction onClosedEvent)
		{
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePosition()
		{
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04003756 RID: 14166
		[SerializeField]
		private int m_FontSizeForString;

		// Token: 0x04003757 RID: 14167
		[SerializeField]
		private int m_FontSizeForNumber;

		// Token: 0x04003758 RID: 14168
		[SerializeField]
		private int m_FontSizeForString_Mobile;

		// Token: 0x04003759 RID: 14169
		[SerializeField]
		private int m_FontSizeForNumber_Mobile;

		// Token: 0x0400375A RID: 14170
		private Queue<string> m_TaskQueue;

		// Token: 0x0400375B RID: 14171
		private bool m_Showing;

		// Token: 0x0400375C RID: 14172
		private Image m_PopUpBaseNW;

		// Token: 0x0400375D RID: 14173
		private Image m_PopUpBaseNE;

		// Token: 0x0400375E RID: 14174
		private Image m_PopUpBaseSW;

		// Token: 0x0400375F RID: 14175
		private Image m_PopUpBaseSE;

		// Token: 0x04003760 RID: 14176
		private ElementObjectManager m_EOManager;

		// Token: 0x04003761 RID: 14177
		private ExtendedTextMeshProUGUI m_PopUpText;

		// Token: 0x04003762 RID: 14178
		private ExtendedTextMeshProUGUI m_PopUpText_Preset;

		// Token: 0x04003763 RID: 14179
		private RectTransform m_Rt;

		// Token: 0x04003764 RID: 14180
		private bool m_Isforui;

		// Token: 0x04003765 RID: 14181
		private Vector3 m_WorldPos;

		// Token: 0x04003766 RID: 14182
		private bool m_UpdateSize;

		// Token: 0x04003767 RID: 14183
		public UnityAction onClosed;
	}
}

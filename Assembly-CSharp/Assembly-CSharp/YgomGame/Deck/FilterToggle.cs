using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FEB RID: 4075
	public class FilterToggle : MonoBehaviour
	{
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06007AE2 RID: 31458 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007AE3 RID: 31459 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOn
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

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06007AE4 RID: 31460 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007AE5 RID: 31461 RVA: 0x0000216D File Offset: 0x0000036D
		public string m_Label
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

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007AE7 RID: 31463 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton m_Button
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

		// Token: 0x06007AE8 RID: 31464 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElements()
		{
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(string label, string text, bool isOn)
		{
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x0000216D File Offset: 0x0000036D
		private void Toggle()
		{
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOn(bool isOn)
		{
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x0400B226 RID: 45606
		private UnityAction m_OnClickAction;

		// Token: 0x0400B227 RID: 45607
		private const string LABEL_RT_IMAGEOFF = "ImageOff";

		// Token: 0x0400B228 RID: 45608
		private const string LABEL_RT_IMAGEON = "ImageOn";

		// Token: 0x0400B229 RID: 45609
		private const string LABEL_TXT = "Text";

		// Token: 0x0400B22A RID: 45610
		private ElementObjectManager m_Eom;

		// Token: 0x0400B22B RID: 45611
		private RectTransform m_Off;

		// Token: 0x0400B22C RID: 45612
		private RectTransform m_On;

		// Token: 0x0400B22D RID: 45613
		private ExtendedTextMeshProUGUI m_TextTMP;

		// Token: 0x0400B22E RID: 45614
		private bool isInitilaized;
	}
}

using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FEE RID: 4078
	public class OptionToggle : MonoBehaviour
	{
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06007B0E RID: 31502 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager m_eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06007B0F RID: 31503 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_Off
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06007B10 RID: 31504 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_On
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06007B11 RID: 31505 RVA: 0x0000216A File Offset: 0x0000036A
		private MDText m_DescText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06007B12 RID: 31506 RVA: 0x0000216A File Offset: 0x0000036A
		private MDText m_ItemText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x0000216D File Offset: 0x0000036D
		private void toggle()
		{
		}

		// Token: 0x06007B14 RID: 31508 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(string title, string desc, bool b)
		{
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetButtonLabel()
		{
			return null;
		}

		// Token: 0x06007B16 RID: 31510 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetEnabledState()
		{
			return false;
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetButton()
		{
			return null;
		}

		// Token: 0x0400B246 RID: 45638
		private string m_ButtonLabel;

		// Token: 0x0400B247 RID: 45639
		private UnityAction m_OnClickAction;

		// Token: 0x0400B248 RID: 45640
		private const string LABEL_SBN_BODY = "Body";

		// Token: 0x0400B249 RID: 45641
		private const string LABEL_RT_IMAGEOFF = "ImageOff";

		// Token: 0x0400B24A RID: 45642
		private const string LABEL_TXT_ITEMTEXT = "TextItem";

		// Token: 0x0400B24B RID: 45643
		private const string LABEL_RT_IMAGEON = "ImageOn";

		// Token: 0x0400B24C RID: 45644
		private const string LABEL_TXT_DESCTEXT = "TextDescription";

		// Token: 0x0400B24D RID: 45645
		private bool isOn;
	}
}

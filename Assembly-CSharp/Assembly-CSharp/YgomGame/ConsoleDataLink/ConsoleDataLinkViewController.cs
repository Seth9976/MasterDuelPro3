using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.YGomTMPro;

namespace YgomGame.ConsoleDataLink
{
	// Token: 0x02001016 RID: 4118
	public class ConsoleDataLinkViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06007BD3 RID: 31699 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUpView()
		{
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetIconArrow(Sprite arrow)
		{
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetIconLeft(Sprite leftIcon)
		{
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetIconRight(Sprite rightIcon)
		{
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetImage(Sprite sprite, Image image)
		{
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PlayColorTween(string label)
		{
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetText(int index, string text)
		{
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCaution(string text)
		{
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x0000216D File Offset: 0x0000036D
		protected void HideText(int index)
		{
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x0000216D File Offset: 0x0000036D
		protected void HideCaution()
		{
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x0000216D File Offset: 0x0000036D
		private void JumpToLink()
		{
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetURL(string url)
		{
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetKonamiIDButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSocialPlatformButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OpenUnavailableDialog(string explanation)
		{
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OpenFailureDialog(string explanation)
		{
		}

		// Token: 0x0400B3B4 RID: 46004
		private readonly string TEXT_TOP_LABEL;

		// Token: 0x0400B3B5 RID: 46005
		private readonly string TEXT_DESC1_LABEL;

		// Token: 0x0400B3B6 RID: 46006
		private readonly string TEXT_CAUTION_LABEL;

		// Token: 0x0400B3B7 RID: 46007
		private readonly string BUTTON_LINK_LABEL;

		// Token: 0x0400B3B8 RID: 46008
		private readonly string TEXT_DESC3_LABEL;

		// Token: 0x0400B3B9 RID: 46009
		private readonly string IMAGE_ICON_LEFT;

		// Token: 0x0400B3BA RID: 46010
		private readonly string IMAGE_ICON_RIGHT;

		// Token: 0x0400B3BB RID: 46011
		private readonly string IMAGE_ICON_ARROW;

		// Token: 0x0400B3BC RID: 46012
		private readonly string IMAGE_TEXT_TOP_BG;

		// Token: 0x0400B3BD RID: 46013
		private Image m_imageIconLeft;

		// Token: 0x0400B3BE RID: 46014
		private Image m_imageIconRight;

		// Token: 0x0400B3BF RID: 46015
		private Image m_imageIconArrow;

		// Token: 0x0400B3C0 RID: 46016
		private Image m_imageTextTopBG;

		// Token: 0x0400B3C1 RID: 46017
		[SerializeField]
		protected Sprite iconCloud;

		// Token: 0x0400B3C2 RID: 46018
		[SerializeField]
		protected Sprite iconUser;

		// Token: 0x0400B3C3 RID: 46019
		[SerializeField]
		protected Sprite iconArrow;

		// Token: 0x0400B3C4 RID: 46020
		private readonly List<string> m_labelList;

		// Token: 0x0400B3C5 RID: 46021
		private List<ExtendedTextMeshProUGUI> m_textList;

		// Token: 0x0400B3C6 RID: 46022
		private ExtendedTextMeshProUGUI m_urlText;

		// Token: 0x0400B3C7 RID: 46023
		private GameObject m_QRRootGO;

		// Token: 0x0400B3C8 RID: 46024
		private GameObject m_ButtonRootGO;

		// Token: 0x0400B3C9 RID: 46025
		private readonly string ROOT_QR_LABEL;

		// Token: 0x0400B3CA RID: 46026
		private readonly string TEXT_URL_LABEL;

		// Token: 0x0400B3CB RID: 46027
		private readonly string RAWIMAGE_QRCODE_LABEL;

		// Token: 0x0400B3CC RID: 46028
		private string m_url;

		// Token: 0x0400B3CD RID: 46029
		private readonly string ROOT_ID_LABEL;

		// Token: 0x0400B3CE RID: 46030
		private readonly string BUTTON_KONAMIID_LABEL;

		// Token: 0x0400B3CF RID: 46031
		private readonly string BUTTON_GOOGLEPLAY_LABEL;

		// Token: 0x0400B3D0 RID: 46032
		private readonly string TEXT_LABEL;

		// Token: 0x0400B3D1 RID: 46033
		protected bool m_isMobile;
	}
}

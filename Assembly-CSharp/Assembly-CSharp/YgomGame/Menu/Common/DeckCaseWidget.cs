using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.YGomTMPro;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B2B RID: 2859
	public class DeckCaseWidget : ElementWidgetBase
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x0000216A File Offset: 0x0000036A
		public Image image
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x000029CC File Offset: 0x00000BCC
		public int caseID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x000029CC File Offset: 0x00000BCC
		public int protectorId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600534F RID: 21327 RVA: 0x0000216A File Offset: 0x0000036A
		public string deckName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06005350 RID: 21328 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] pickupCards
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06005351 RID: 21329 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] pickupDecos
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardImage(int index, int id, int decoration, bool addProgress)
		{
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0000216D File Offset: 0x0000036D
		private void setAnimation(bool isOpen, bool immediate = false)
		{
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0000216D File Offset: 0x0000036D
		private void playTweenEndOfLabel(GameObject target, string label)
		{
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0000216D File Offset: 0x0000036D
		private static void traverseTweenTree(GameObject target, string label, Action<Tween> action, bool recursive)
		{
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public DeckCaseWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCaseWidget Binding(int caseId, int protectorId, string deckName, int[] pickupCards_, int[] pickupDecos_, bool opened, bool isLarge = false, bool isDestroyTweens = false)
		{
			return null;
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeCardImage(int index, int id, int decoration)
		{
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeDeckName(string name)
		{
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeDeckCaseImage(int caseId)
		{
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeProtectorImage(int protectorId)
		{
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOpen(bool immediate = false)
		{
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClose(bool immediate = false)
		{
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject LoadPrefabFromResource()
		{
			return null;
		}

		// Token: 0x0400911C RID: 37148
		private int m_caseID;

		// Token: 0x0400911D RID: 37149
		private int m_protectorId;

		// Token: 0x0400911E RID: 37150
		private string m_deckName;

		// Token: 0x0400911F RID: 37151
		private int[] m_pickupCards;

		// Token: 0x04009120 RID: 37152
		private int[] m_pickupDecos;

		// Token: 0x04009121 RID: 37153
		private Image m_deckImage;

		// Token: 0x04009122 RID: 37154
		private Image m_deckOpenedImage;

		// Token: 0x04009123 RID: 37155
		private RawImage[] m_cardImages;

		// Token: 0x04009124 RID: 37156
		private ExtendedTextMeshProUGUI m_nameText;

		// Token: 0x04009125 RID: 37157
		private const string openTweenLabel = "select";

		// Token: 0x04009126 RID: 37158
		private const string closeTweenLabel = "deselect";

		// Token: 0x04009127 RID: 37159
		public static readonly string prefabResourcePath;
	}
}

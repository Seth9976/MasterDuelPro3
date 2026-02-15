using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B52 RID: 2898
	public class PublicDeckCaseWidget : ElementWidgetBase
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x000029CC File Offset: 0x00000BCC
		public int caseID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x000029CC File Offset: 0x00000BCC
		public int pickupCard
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardImage(int id, bool addProgress)
		{
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0000216D File Offset: 0x0000036D
		private void setAnimation(bool isOpen, bool immediate = false)
		{
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0000216D File Offset: 0x0000036D
		private void playTweenEndOfLabel(GameObject target, string label)
		{
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0000216D File Offset: 0x0000036D
		private static void traverseTweenTree(GameObject target, string label, Action<Tween> action, bool recursive)
		{
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public PublicDeckCaseWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0000216A File Offset: 0x0000036A
		public PublicDeckCaseWidget Binding(int caseId, int pickupCard)
		{
			return null;
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeCardImage(int id)
		{
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject LoadPrefabFromResource()
		{
			return null;
		}

		// Token: 0x04009167 RID: 37223
		private int m_caseID;

		// Token: 0x04009168 RID: 37224
		private int m_pickupCard;

		// Token: 0x04009169 RID: 37225
		private Image m_deckImage;

		// Token: 0x0400916A RID: 37226
		private RawImage m_cardImage;

		// Token: 0x0400916B RID: 37227
		private const string openTweenLabel = "select";

		// Token: 0x0400916C RID: 37228
		private const string closeTweenLabel = "deselect";

		// Token: 0x0400916D RID: 37229
		public static readonly string prefabResourcePath;
	}
}

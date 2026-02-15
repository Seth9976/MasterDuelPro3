using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Card;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E96 RID: 3734
	public class GhostCard : MonoBehaviour
	{
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06006C82 RID: 27778 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006C83 RID: 27779 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isShowing
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

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06006C84 RID: 27780 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006C85 RID: 27781 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isActivate
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

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06006C86 RID: 27782 RVA: 0x000F600C File Offset: 0x000F420C
		private Quaternion parentRotation
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x0000216A File Offset: 0x0000036A
		public static GhostCard Create(Transform parent)
		{
			return null;
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(int cardID, int sleeveID, int rareID, Action onCreated = null)
		{
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminateCard()
		{
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTargetPosition(Vector3 position, bool immediate)
		{
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColor(Color color)
		{
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActive(bool active)
		{
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTargetRotation(Quaternion rotation, bool immediate)
		{
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton SetupSelectionButton()
		{
			return null;
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400A7E2 RID: 42978
		private int cardID;

		// Token: 0x0400A7E3 RID: 42979
		private int sleeveID;

		// Token: 0x0400A7E4 RID: 42980
		private int rareID;

		// Token: 0x0400A7E5 RID: 42981
		private bool cardCreated;

		// Token: 0x0400A7E6 RID: 42982
		private GameObject cardParent;

		// Token: 0x0400A7E7 RID: 42983
		private CardModel card;

		// Token: 0x0400A7E8 RID: 42984
		private Vector3 targetPosition;

		// Token: 0x0400A7E9 RID: 42985
		private Quaternion targetCardRotation;

		// Token: 0x0400A7EA RID: 42986
		private Color cardColor;

		// Token: 0x0400A7EB RID: 42987
		private const float moveFactor = 0.8f;

		// Token: 0x0400A7EC RID: 42988
		private const float rotFactor = 0.5f;

		// Token: 0x0400A7ED RID: 42989
		private const float turnFactor = 0.8f;

		// Token: 0x0400A7EE RID: 42990
		private const float powerFactor = 10f;

		// Token: 0x0400A7EF RID: 42991
		private const float powerRotLimit = 30f;
	}
}

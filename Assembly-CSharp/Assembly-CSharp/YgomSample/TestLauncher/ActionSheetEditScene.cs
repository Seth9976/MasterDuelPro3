using System;
using UnityEngine;
using YgomGame.Menu;

namespace YgomSample.TestLauncher
{
	// Token: 0x0200079C RID: 1948
	public class ActionSheetEditScene : MonoBehaviour
	{
		// Token: 0x06003C9A RID: 15514 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenWithEmbedObject()
		{
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenEntitys()
		{
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCustomSheet()
		{
		}

		// Token: 0x0400350C RID: 13580
		[SerializeField]
		private GameObject m_CustomPositiveButton;

		// Token: 0x0400350D RID: 13581
		[SerializeField]
		private GameObject m_CustomDestructiveButton;

		// Token: 0x0400350E RID: 13582
		[SerializeField]
		private GameObject m_CustomDisableButton;

		// Token: 0x0400350F RID: 13583
		[SerializeField]
		private GameObject m_EmbedGameObject;

		// Token: 0x04003510 RID: 13584
		[SerializeField]
		private string m_Title;

		// Token: 0x04003511 RID: 13585
		[SerializeField]
		private string m_Messages;

		// Token: 0x04003512 RID: 13586
		[SerializeField]
		private string[] m_EntryTexts;

		// Token: 0x04003513 RID: 13587
		[SerializeField]
		private int m_DestructiveLength;

		// Token: 0x04003514 RID: 13588
		[SerializeField]
		private ActionSheetViewController.EntryData[] m_EntryDatas;
	}
}

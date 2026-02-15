using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000ED4 RID: 3796
	public class MessageDialog : MonoBehaviour
	{
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06006E91 RID: 28305 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006E92 RID: 28306 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06006E93 RID: 28307 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateNoResponse(Transform parent, Action<MessageDialog> onLoaded)
		{
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateNoOperation(Transform parent, Action<MessageDialog> onLoaded)
		{
		}

		// Token: 0x06006E95 RID: 28309 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(string prefabPath, Transform parent, Action<MessageDialog> onLoaded)
		{
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close(bool force = false)
		{
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMessage(string msg)
		{
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIcon(bool disp, Sprite icon = null)
		{
		}

		// Token: 0x0400A973 RID: 43379
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400A974 RID: 43380
		private ElementObjectManager ui;

		// Token: 0x0400A975 RID: 43381
		private ExtendedTextMeshProUGUI msgText;

		// Token: 0x0400A976 RID: 43382
		private Image icon;

		// Token: 0x0400A977 RID: 43383
		private const string prefabPathNoResponse = "Prefabs/Duel/MessageDialogNoResponse";

		// Token: 0x0400A978 RID: 43384
		private const string prefabPathNoOperation = "Prefabs/Duel/MessageDialogNoOperation";
	}
}

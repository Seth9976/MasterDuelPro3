using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200011B RID: 283
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/UISupport.html#multiplayer-uis")]
	public class MultiplayerEventSystem : EventSystem
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00044DAC File Offset: 0x00042FAC
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00044DB4 File Offset: 0x00042FB4
		public GameObject playerRoot
		{
			get
			{
				return this.m_PlayerRoot;
			}
			set
			{
				this.m_PlayerRoot = value;
				this.InitializePlayerRoot();
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00044DC3 File Offset: 0x00042FC3
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitializePlayerRoot();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00044DD1 File Offset: 0x00042FD1
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00044DDC File Offset: 0x00042FDC
		private void InitializePlayerRoot()
		{
			if (this.m_PlayerRoot == null)
			{
				return;
			}
			InputSystemUIInputModule inputModule = base.GetComponent<InputSystemUIInputModule>();
			if (inputModule != null)
			{
				inputModule.localMultiPlayerRoot = this.m_PlayerRoot;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00044E14 File Offset: 0x00043014
		protected override void Update()
		{
			EventSystem originalCurrent = EventSystem.current;
			EventSystem.current = this;
			try
			{
				base.Update();
			}
			finally
			{
				EventSystem.current = originalCurrent;
			}
		}

		// Token: 0x04000682 RID: 1666
		[Tooltip("If set, only process mouse and navigation events for any game objects which are children of this game object.")]
		[SerializeField]
		private GameObject m_PlayerRoot;
	}
}

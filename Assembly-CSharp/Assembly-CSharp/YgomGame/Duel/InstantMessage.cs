using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000EA8 RID: 3752
	public class InstantMessage : DuelUIBase
	{
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06006D50 RID: 27984 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override UITransitionUtil.BlockType openCloseBlockType
		{
			get
			{
				return UITransitionUtil.BlockType.None;
			}
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<InstantMessage> finishCallback)
		{
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize(RunEffectWorker effectWorker)
		{
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateUI()
		{
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOpen(string message, float showTime = 3f)
		{
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqForceClose()
		{
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x0000216D File Offset: 0x0000036D
		private void Open(string message)
		{
		}

		// Token: 0x06006D59 RID: 27993 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Update()
		{
		}

		// Token: 0x06006D5A RID: 27994 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateOperation()
		{
		}

		// Token: 0x0400A841 RID: 43073
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400A842 RID: 43074
		private ElementObjectManager ui;

		// Token: 0x0400A843 RID: 43075
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x0400A844 RID: 43076
		private float currentTime;

		// Token: 0x0400A845 RID: 43077
		private const string prefabPath = "Prefabs/Duel/InstantMessage";

		// Token: 0x0400A846 RID: 43078
		private const float DefaultShowTime = 3f;

		// Token: 0x0400A847 RID: 43079
		private Queue<InstantMessage.OperationInfo> operationQueue;

		// Token: 0x0400A848 RID: 43080
		private InstantMessage.OperationInfo currentOperation;

		// Token: 0x02000EA9 RID: 3753
		private class OperationInfo
		{
			// Token: 0x06006D5C RID: 27996 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantMessage.OperationInfo OpenOperation(string message)
			{
				return null;
			}

			// Token: 0x06006D5D RID: 27997 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantMessage.OperationInfo CloseOperation()
			{
				return null;
			}

			// Token: 0x06006D5E RID: 27998 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantMessage.OperationInfo WaitOperation(float time)
			{
				return null;
			}

			// Token: 0x0400A849 RID: 43081
			public InstantMessage.OperationInfo.Operation operation;

			// Token: 0x0400A84A RID: 43082
			public string message;

			// Token: 0x0400A84B RID: 43083
			public float waitTime;

			// Token: 0x02000EAA RID: 3754
			public enum Operation
			{
				// Token: 0x0400A84D RID: 43085
				Open,
				// Token: 0x0400A84E RID: 43086
				Wait,
				// Token: 0x0400A84F RID: 43087
				Close
			}
		}
	}
}

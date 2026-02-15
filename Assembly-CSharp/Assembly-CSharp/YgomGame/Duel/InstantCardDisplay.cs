using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000EA5 RID: 3749
	public class InstantCardDisplay : DuelUIBase
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06006D3F RID: 27967 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override UITransitionUtil.BlockType openCloseBlockType
		{
			get
			{
				return UITransitionUtil.BlockType.None;
			}
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<InstantCardDisplay> finishCallback)
		{
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize(RunEffectWorker effectWorker)
		{
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateUI()
		{
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOpen(int cardID, bool applyEffect, float showTime = 3f)
		{
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x0000216D File Offset: 0x0000036D
		private void Open(int cardID)
		{
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x0000216D File Offset: 0x0000036D
		private void Wait(bool applyEffect)
		{
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqForceClose()
		{
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Update()
		{
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateOperation()
		{
		}

		// Token: 0x0400A82F RID: 43055
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400A830 RID: 43056
		private ElementObjectManager ui;

		// Token: 0x0400A831 RID: 43057
		private RawImage cardPicture;

		// Token: 0x0400A832 RID: 43058
		private ParticleSystem applyEffectRoot;

		// Token: 0x0400A833 RID: 43059
		private float showTime;

		// Token: 0x0400A834 RID: 43060
		private float currentTime;

		// Token: 0x0400A835 RID: 43061
		private const string prefabPath = "Prefabs/Duel/InstantCardDisplay";

		// Token: 0x0400A836 RID: 43062
		private const float DefaultShowTime = 3f;

		// Token: 0x0400A837 RID: 43063
		private Queue<InstantCardDisplay.OperationInfo> operationQueue;

		// Token: 0x0400A838 RID: 43064
		private InstantCardDisplay.OperationInfo currentOperation;

		// Token: 0x02000EA6 RID: 3750
		private class OperationInfo
		{
			// Token: 0x06006D4C RID: 27980 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantCardDisplay.OperationInfo OpenOperation(int cardID)
			{
				return null;
			}

			// Token: 0x06006D4D RID: 27981 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantCardDisplay.OperationInfo CloseOperation()
			{
				return null;
			}

			// Token: 0x06006D4E RID: 27982 RVA: 0x0000216A File Offset: 0x0000036A
			public static InstantCardDisplay.OperationInfo WaitOperation(float time, bool applyEffect)
			{
				return null;
			}

			// Token: 0x0400A839 RID: 43065
			public InstantCardDisplay.OperationInfo.Operation operation;

			// Token: 0x0400A83A RID: 43066
			public int cardID;

			// Token: 0x0400A83B RID: 43067
			public float waitTime;

			// Token: 0x0400A83C RID: 43068
			public bool applyEffect;

			// Token: 0x02000EA7 RID: 3751
			public enum Operation
			{
				// Token: 0x0400A83E RID: 43070
				Open,
				// Token: 0x0400A83F RID: 43071
				Wait,
				// Token: 0x0400A840 RID: 43072
				Close
			}
		}
	}
}

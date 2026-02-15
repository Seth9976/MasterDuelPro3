using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CDE RID: 3294
	public class CardInstancePool : MonoBehaviour
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E24 RID: 24100 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E26 RID: 24102 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005E28 RID: 24104 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardInstancePool Create(DuelGameObjectManager goManager, GameObject root, string name)
		{
			return null;
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRoot RentInstance()
		{
			return null;
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnInstance(CardRoot cardRoot)
		{
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingStep()
		{
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0000216D File Offset: 0x0000036D
		private void InstantiateStep()
		{
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateInstance()
		{
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueInstance(CardRoot cardRoot)
		{
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEffectPlaying(CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEffectPlaying(Type type, CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsZoneEffectPlaying(ZoneCard.Zone zone, ZoneCard.Mode mode, CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMoveEffectRequested(CardRoot excludeCard = null)
		{
			return false;
		}

		// Token: 0x040099B8 RID: 39352
		private CardInstancePool.Step step;

		// Token: 0x040099B9 RID: 39353
		private global::UnityEngine.Object srcObject;

		// Token: 0x040099BA RID: 39354
		private int counter;

		// Token: 0x040099BB RID: 39355
		private List<CardRoot> list;

		// Token: 0x040099BC RID: 39356
		private Queue<CardRoot> queue;

		// Token: 0x02000CDF RID: 3295
		private enum Step
		{
			// Token: 0x040099BE RID: 39358
			Initializing,
			// Token: 0x040099BF RID: 39359
			Instantiate,
			// Token: 0x040099C0 RID: 39360
			Idle,
			// Token: 0x040099C1 RID: 39361
			Terminating
		}
	}
}

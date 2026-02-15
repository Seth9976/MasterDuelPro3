using System;
using System.Runtime.CompilerServices;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010C7 RID: 4295
	public abstract class SequenceBehaviour : ISequenceBehaviour
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06007F8E RID: 32654 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007F8F RID: 32655 RVA: 0x0000216D File Offset: 0x0000036D
		public SequenceBehaviour.State state
		{
			[CompilerGenerated]
			get
			{
				return SequenceBehaviour.State.None;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06007F90 RID: 32656 RVA: 0x0000216A File Offset: 0x0000036A
		public string name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06007F91 RID: 32657 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x00002739 File Offset: 0x00000939
		public SequenceBehaviour(SequenceBehaviourWork sequenceBehaviourWork)
		{
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x0000216D File Offset: 0x0000036D
		public void Begin()
		{
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnBegin()
		{
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update()
		{
			return false;
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x0000216D File Offset: 0x0000036D
		public void End()
		{
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnEnd()
		{
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInputAccept()
		{
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B82C RID: 47148
		protected readonly SequenceBehaviourWork m_Work;

		// Token: 0x020010C8 RID: 4296
		public enum State
		{
			// Token: 0x0400B82E RID: 47150
			None,
			// Token: 0x0400B82F RID: 47151
			Active,
			// Token: 0x0400B830 RID: 47152
			End
		}
	}
}

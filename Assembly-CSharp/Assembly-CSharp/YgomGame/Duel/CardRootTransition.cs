using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Duel
{
	// Token: 0x02000CF6 RID: 3318
	public abstract class CardRootTransition
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06005F90 RID: 24464 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F91 RID: 24465 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isFinished
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

		// Token: 0x06005F92 RID: 24466 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnImmediate()
		{
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(CardRoot cardRoot, bool immediate)
		{
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetCardLocator(CardLocator fromLocator, CardLocator toLocator)
		{
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x04009A68 RID: 39528
		protected CardRoot cardRoot;

		// Token: 0x04009A69 RID: 39529
		protected CardLocator fromLocator;

		// Token: 0x04009A6A RID: 39530
		protected CardLocator toLocator;

		// Token: 0x04009A6B RID: 39531
		private bool immediate;
	}
}

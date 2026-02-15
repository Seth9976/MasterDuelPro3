using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Duel
{
	// Token: 0x02000CBC RID: 3260
	public abstract class CardEffectBase
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06005CF4 RID: 23796 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CF5 RID: 23797 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot cardRoot
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005CF7 RID: 23799 RVA: 0x0000216D File Offset: 0x0000036D
		public bool finished
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CF9 RID: 23801 RVA: 0x0000216D File Offset: 0x0000036D
		protected Action onFinished
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06005CFA RID: 23802
		public abstract void StartEffect();

		// Token: 0x06005CFB RID: 23803 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFinished()
		{
		}
	}
}

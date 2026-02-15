using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Duel
{
	// Token: 0x02000C71 RID: 3185
	public abstract class AbstractRunEffectWorker
	{
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06005B3D RID: 23357 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005B3E RID: 23358 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool isInitialized
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005B40 RID: 23360 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool isPreparedToDuel
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool isTerminated
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005B44 RID: 23364 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelClient host
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

		// Token: 0x06005B45 RID: 23365 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void PrepareToDuel()
		{
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Terminate()
		{
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnDestroy()
		{
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public virtual void RunEffect(Engine.ViewType viewType, int param1, int param2, int param3)
		{
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsBusyEffect(Engine.ViewType viewType)
		{
			return false;
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x00002739 File Offset: 0x00000939
		public AbstractRunEffectWorker(DuelClient host)
		{
		}
	}
}

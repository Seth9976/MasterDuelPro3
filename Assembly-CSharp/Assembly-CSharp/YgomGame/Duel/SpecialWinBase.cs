using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000F1D RID: 3869
	public abstract class SpecialWinBase
	{
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060071E7 RID: 29159
		protected abstract string prefabPath { get; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060071E8 RID: 29160 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060071E9 RID: 29161 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isReady
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

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060071EA RID: 29162 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060071EB RID: 29163 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool finished
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

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060071EC RID: 29164 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060071ED RID: 29165 RVA: 0x0000216D File Offset: 0x0000036D
		public bool goNext
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

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060071EE RID: 29166
		protected abstract List<string> seList { get; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060071EF RID: 29167 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool destroyOnWinStart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize()
		{
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LoadSE()
		{
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LoadTimeine(Action<PlayableDirector> onLoaded)
		{
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Setup(PlayableDirector timeline)
		{
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Action onWinStart)
		{
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnFinished()
		{
		}

		// Token: 0x0400ABBC RID: 43964
		protected PlayableDirector timeline;
	}
}

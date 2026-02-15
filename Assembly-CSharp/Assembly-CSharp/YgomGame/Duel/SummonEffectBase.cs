using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomGame.Duel
{
	// Token: 0x02000F29 RID: 3881
	public abstract class SummonEffectBase
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06007231 RID: 29233 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007232 RID: 29234 RVA: 0x0000216D File Offset: 0x0000036D
		public MonsterCutinEffect monsterCutinEffect
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

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06007233 RID: 29235 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007235 RID: 29237 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isEffectReady
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

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06007236 RID: 29238 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007237 RID: 29239 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isEffectPlaying
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

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06007238 RID: 29240 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007239 RID: 29241 RVA: 0x0000216D File Offset: 0x0000036D
		public int destCardUniqueID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600723A RID: 29242
		public abstract Engine.SpSummonType spSummonType { get; }

		// Token: 0x0600723B RID: 29243 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LoadCardFront()
		{
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LoadCardBack(int sleeveID, UnityAction<Material> onFinished)
		{
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x0000216D File Offset: 0x0000036D
		protected void TerminateCard()
		{
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Action onFinished, Action onStartCard)
		{
		}

		// Token: 0x06007240 RID: 29248
		protected abstract bool PlayEffect(Action onFinished);

		// Token: 0x06007241 RID: 29249 RVA: 0x0000216A File Offset: 0x0000036A
		protected PlayableDirector PlayTimeline(string label, Action onFinished)
		{
			return null;
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PlayTimeline(string path, UnityAction<PlayableDirector> onLoaded, Action onFinished)
		{
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x0000216D File Offset: 0x0000036D
		protected void LoadTimeline(string path)
		{
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x000F6428 File Offset: 0x000F4628
		public ValueTuple<Vector3, Quaternion, Vector3> GetDestCardPlace()
		{
			return default(ValueTuple<Vector3, Quaternion, Vector3>);
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDestCardPlace()
		{
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetupEventCallback()
		{
		}

		// Token: 0x06007247 RID: 29255 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayStartCard()
		{
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayMonsterCutin()
		{
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Finish()
		{
		}

		// Token: 0x0600724A RID: 29258 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadResources()
		{
		}

		// Token: 0x0600724B RID: 29259 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool Skip()
		{
			return false;
		}

		// Token: 0x0600724C RID: 29260 RVA: 0x0000216A File Offset: 0x0000036A
		protected TimelineClip GetStrongSummonEvent(PlayableDirector timeline)
		{
			return null;
		}

		// Token: 0x0600724D RID: 29261 RVA: 0x0000216D File Offset: 0x0000036D
		protected void StopSE(PlayableDirector timeline)
		{
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400ABD4 RID: 43988
		protected Texture2D destTextureFront;

		// Token: 0x0400ABD5 RID: 43989
		protected Material destProtectorMaterial;

		// Token: 0x0400ABD6 RID: 43990
		protected Texture2D[] matTextureFront;

		// Token: 0x0400ABD7 RID: 43991
		protected Material[] matProtectorMaterials;

		// Token: 0x0400ABD8 RID: 43992
		protected Transform destCard;

		// Token: 0x0400ABD9 RID: 43993
		private Vector3 destCardPosition;

		// Token: 0x0400ABDA RID: 43994
		private Quaternion destCardRotation;

		// Token: 0x0400ABDB RID: 43995
		private Vector3 destCardScale;

		// Token: 0x0400ABDC RID: 43996
		protected Action onStartCard;

		// Token: 0x0400ABDD RID: 43997
		protected Action onFinished;

		// Token: 0x0400ABDE RID: 43998
		protected int materialNum;

		// Token: 0x0400ABDF RID: 43999
		protected int loadCounter;

		// Token: 0x0400ABE0 RID: 44000
		protected PlayableDirector mainTimeline;

		// Token: 0x0400ABE1 RID: 44001
		protected GameObject autoReleaseCardPicture;

		// Token: 0x0400ABE2 RID: 44002
		protected List<int> cardidList;

		// Token: 0x0400ABE3 RID: 44003
		protected List<UnityAction<Texture2D>> onFinishList;

		// Token: 0x0400ABE4 RID: 44004
		protected bool monsterCutinInvoked;

		// Token: 0x0400ABE5 RID: 44005
		private bool startCardInvoked;

		// Token: 0x0400ABE6 RID: 44006
		protected bool skipped;

		// Token: 0x0400ABE7 RID: 44007
		protected bool isTerminated;

		// Token: 0x0400ABE8 RID: 44008
		protected List<string> loadedTimelineList;
	}
}

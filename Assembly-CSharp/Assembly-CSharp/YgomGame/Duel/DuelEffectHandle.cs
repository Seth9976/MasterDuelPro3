using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D62 RID: 3426
	public class DuelEffectHandle : MonoBehaviour
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060063B9 RID: 25529 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060063BA RID: 25530 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelEffectPool effectPool
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

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060063BC RID: 25532 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060063BD RID: 25533 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelEffectPool.Type type
		{
			[CompilerGenerated]
			get
			{
				return (DuelEffectPool.Type)0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelEffectPool effectPool, DuelEffectPool.Type type, GameObject target, bool autoQuit, Action onFinished)
		{
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnTerminate()
		{
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Play()
		{
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnPlay()
		{
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnStop()
		{
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqQuit(float timeToQuit)
		{
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnUpdate()
		{
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0000216D File Offset: 0x0000036D
		private void QuitImmediate()
		{
		}

		// Token: 0x04009DD3 RID: 40403
		protected GameObject target;

		// Token: 0x04009DD4 RID: 40404
		private float time;

		// Token: 0x04009DD5 RID: 40405
		private float timeToQuit;

		// Token: 0x04009DD6 RID: 40406
		private bool reqQuit;

		// Token: 0x04009DD7 RID: 40407
		protected bool autoQuit;

		// Token: 0x04009DD8 RID: 40408
		private Action onFinished;

		// Token: 0x04009DD9 RID: 40409
		private Vector3 defaultPosition;

		// Token: 0x04009DDA RID: 40410
		private Quaternion defaultRotation;

		// Token: 0x04009DDB RID: 40411
		private Vector3 defaultScale;
	}
}

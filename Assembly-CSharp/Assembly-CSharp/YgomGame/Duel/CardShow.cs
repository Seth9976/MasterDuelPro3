using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D0D RID: 3341
	public class CardShow
	{
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06006035 RID: 24629 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006036 RID: 24630 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnInitCallback
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06006037 RID: 24631 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006038 RID: 24632 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnMoveCallback
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06006039 RID: 24633 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600603A RID: 24634 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnWaitCallback
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600603C RID: 24636 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnBackCallback
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600603E RID: 24638 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnFinishCallback
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006040 RID: 24640 RVA: 0x0000216D File Offset: 0x0000036D
		public CardShow.Step step
		{
			[CompilerGenerated]
			get
			{
				return CardShow.Step.Init;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(CardRoot setCardRoot, CardShow.Mode mode, int team, int position)
		{
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateStatus()
		{
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init()
		{
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitLoad()
		{
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x0000216D File Offset: 0x0000036D
		private void Move()
		{
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x0000216D File Offset: 0x0000036D
		private void Wait()
		{
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0000216D File Offset: 0x0000036D
		private void Back()
		{
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x0000216D File Offset: 0x0000036D
		private void Finish()
		{
		}

		// Token: 0x04009B62 RID: 39778
		private CardRoot cardRoot;

		// Token: 0x04009B63 RID: 39779
		private bool cardFace;

		// Token: 0x04009B64 RID: 39780
		private CardShow.Mode mode;

		// Token: 0x04009B65 RID: 39781
		private int team;

		// Token: 0x04009B66 RID: 39782
		private int position;

		// Token: 0x04009B67 RID: 39783
		private float time;

		// Token: 0x04009B68 RID: 39784
		private Vector3 startPosition;

		// Token: 0x04009B69 RID: 39785
		private Quaternion startRotation;

		// Token: 0x04009B6A RID: 39786
		private Vector3 startScale;

		// Token: 0x04009B6B RID: 39787
		private Vector3 movedPosition;

		// Token: 0x04009B6C RID: 39788
		private Quaternion movedRotation;

		// Token: 0x04009B6D RID: 39789
		private Vector3 movedScale;

		// Token: 0x04009B6E RID: 39790
		private Vector3 waitedPosition;

		// Token: 0x04009B6F RID: 39791
		private Quaternion waitedRotation;

		// Token: 0x04009B70 RID: 39792
		private Vector3 waitedScale;

		// Token: 0x04009B71 RID: 39793
		private Vector3 destScale;

		// Token: 0x04009B72 RID: 39794
		private BezierMotionSetting motionShow;

		// Token: 0x04009B73 RID: 39795
		private BezierMotionSetting motionWait;

		// Token: 0x04009B74 RID: 39796
		private BezierMotionSetting motionBack;

		// Token: 0x02000D0E RID: 3342
		public enum Step
		{
			// Token: 0x04009B76 RID: 39798
			Init,
			// Token: 0x04009B77 RID: 39799
			WaitLoad,
			// Token: 0x04009B78 RID: 39800
			Move,
			// Token: 0x04009B79 RID: 39801
			Wait,
			// Token: 0x04009B7A RID: 39802
			Back,
			// Token: 0x04009B7B RID: 39803
			Finish
		}

		// Token: 0x02000D0F RID: 3343
		public enum Mode
		{
			// Token: 0x04009B7D RID: 39805
			Happen,
			// Token: 0x04009B7E RID: 39806
			Disabled,
			// Token: 0x04009B7F RID: 39807
			Apply
		}
	}
}

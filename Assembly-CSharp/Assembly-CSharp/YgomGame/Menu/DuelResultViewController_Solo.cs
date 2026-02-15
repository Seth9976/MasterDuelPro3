using System;
using System.Collections;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A68 RID: 2664
	public class DuelResultViewController_Solo : BaseMenuViewController
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetRemainAddRange()
		{
			return 0;
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator StartResult()
		{
			return null;
		}

		// Token: 0x04008B99 RID: 35737
		private readonly string LEVELINFO_LABEL;

		// Token: 0x04008B9A RID: 35738
		private readonly string BTN_RETRY_LABEL;

		// Token: 0x04008B9B RID: 35739
		private readonly string BTN_SAVE_LABEL;

		// Token: 0x04008B9C RID: 35740
		private readonly string BTN_BACK_LABEL;

		// Token: 0x04008B9D RID: 35741
		[SerializeField]
		private DuelResultViewController_Solo.LevelUpPlayer m_LevelupPlayer;

		// Token: 0x04008B9E RID: 35742
		private SelectionButton RetryButton;

		// Token: 0x04008B9F RID: 35743
		private SelectionButton SaveButton;

		// Token: 0x04008BA0 RID: 35744
		private SelectionButton BackButton;

		// Token: 0x04008BA1 RID: 35745
		private Util.GameMode m_GameMode;

		// Token: 0x04008BA2 RID: 35746
		private static IEnumerator coroutine;

		// Token: 0x04008BA3 RID: 35747
		private int remainAddRangeCount;

		// Token: 0x02000A69 RID: 2665
		public interface IResultPlayer
		{
			// Token: 0x06004DC8 RID: 19912
			IEnumerator Play();
		}

		// Token: 0x02000A6A RID: 2666
		[Serializable]
		private class LevelUpPlayer : DuelResultViewController_Solo.TweenResultPlayer
		{
			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06004DCA RID: 19914 RVA: 0x0000216A File Offset: 0x0000036A
			protected override Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004DCB RID: 19915 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Initialize(ElementObjectManager eom, int selectorGroupPriority)
			{
			}

			// Token: 0x06004DCC RID: 19916 RVA: 0x0000216D File Offset: 0x0000036D
			public override void ImportWork(object workData)
			{
			}

			// Token: 0x06004DCD RID: 19917 RVA: 0x0000216A File Offset: 0x0000036A
			public override IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004DCE RID: 19918 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickClose()
			{
			}

			// Token: 0x04008BA4 RID: 35748
			[SerializeField]
			private float m_GaugeSpeedPerUnitLevel;

			// Token: 0x04008BA5 RID: 35749
			private float m_increasedExpAmount;

			// Token: 0x04008BA6 RID: 35750
			private int m_bLevel;

			// Token: 0x04008BA7 RID: 35751
			private float m_bExpPercent;

			// Token: 0x04008BA8 RID: 35752
			private int m_aLevel;

			// Token: 0x04008BA9 RID: 35753
			private int m_aExp;

			// Token: 0x04008BAA RID: 35754
			private int m_aNeedExp;

			// Token: 0x04008BAB RID: 35755
			private bool m_isFinish;
		}

		// Token: 0x02000A6B RID: 2667
		private abstract class TweenResultPlayer : DuelResultViewController_Solo.IResultPlayer
		{
			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual Selector selector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x000029CC File Offset: 0x00000BCC
			protected virtual bool isFinish
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004DD2 RID: 19922 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Initialize(ElementObjectManager eom, int selectorGroupPriority)
			{
			}

			// Token: 0x06004DD3 RID: 19923
			public abstract void ImportWork(object workData);

			// Token: 0x06004DD4 RID: 19924 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual IEnumerator Play()
			{
				return null;
			}

			// Token: 0x06004DD5 RID: 19925 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClickClose()
			{
			}

			// Token: 0x04008BAC RID: 35756
			protected readonly string k_TweenOpenKey;

			// Token: 0x04008BAD RID: 35757
			protected readonly string k_TweenCloseKey;

			// Token: 0x04008BAE RID: 35758
			protected readonly string k_CloseButtonLabel;

			// Token: 0x04008BAF RID: 35759
			protected ElementObjectManager m_Eom;
		}
	}
}

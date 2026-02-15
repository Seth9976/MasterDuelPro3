using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomGame.Utility;
using YgomSystem;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A4C RID: 2636
	public class BaseMenuViewController : TweenViewController
	{
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual ResourceManager.UnloadCheckLevel unloadCheckLevel
		{
			get
			{
				return ResourceManager.UnloadCheckLevel.None;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool isExistsLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004CCF RID: 19663 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsLoading
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x0000216D File Offset: 0x0000036D
		protected Selector m_Selector
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetStartedProgressCallback(UnityAction callback = null)
		{
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ResetStartedProgressCallback()
		{
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetVisibleOnInitialize(bool visible)
		{
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CreateView()
		{
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void SetSelectorLabelAsUnique()
		{
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnCreatedView()
		{
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AssignProgressContent(IAsyncProgressContainer progressContainer)
		{
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddLoadingCount()
		{
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0000216D File Offset: 0x0000036D
		protected void DecLoadingCount()
		{
		}

		// Token: 0x04008A9B RID: 35483
		private const string k_ArgKeyUIPrefOverride = "UIPref";

		// Token: 0x04008A9C RID: 35484
		[SerializeField]
		protected ViewCreater m_ViewCreater;

		// Token: 0x04008A9D RID: 35485
		protected ElementObjectManager m_View;

		// Token: 0x04008A9E RID: 35486
		private Selector m_ViewSelector;

		// Token: 0x04008A9F RID: 35487
		protected List<GameObject> m_AdditionalTweenTarget;

		// Token: 0x04008AA0 RID: 35488
		private List<IAsyncProgressContent> m_AsyncProgressContents;

		// Token: 0x04008AA1 RID: 35489
		private List<IAsyncProgressContent> m_AsyncProgressDeleteCache;

		// Token: 0x04008AA2 RID: 35490
		private int m_LoadingCount;

		// Token: 0x04008AA3 RID: 35491
		private bool isLoading;

		// Token: 0x04008AA4 RID: 35492
		private bool isStartedProgress;

		// Token: 0x04008AA5 RID: 35493
		private bool isDispedProgress;

		// Token: 0x04008AA6 RID: 35494
		private UnityAction startedProgressCallback;

		// Token: 0x04008AA7 RID: 35495
		protected TextGroupLoadHolder m_TextGroupLoadHolder;
	}
}

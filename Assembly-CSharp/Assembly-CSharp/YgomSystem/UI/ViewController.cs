using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200064D RID: 1613
	public class ViewController : MonoBehaviour
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x0000216D File Offset: 0x0000036D
		[HideInInspector]
		public Dictionary<string, object> Args
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x0000216D File Offset: 0x0000036D
		private protected int selectorPriorityBase
		{
			[CompilerGenerated]
			protected get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int selectorPriorityMax
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSelectorPriorityAdditional(int addPos)
		{
			return 0;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareStackEntry()
		{
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetVisibleOnInitialize(bool visible)
		{
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void NotificationStackEntry()
		{
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void NotificationStackRemove()
		{
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000029C5 File Offset: 0x00000BC5
		public virtual float Progress()
		{
			return 0f;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ProgressUpdate()
		{
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x0000216A File Offset: 0x0000036A
		protected ViewControllerManager GetRootManager()
		{
			return null;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnBack()
		{
			return false;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController SendResult(object value)
		{
			return null;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendBack()
		{
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopViewController()
		{
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushViewController(string prefabname, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushViewController(GameObject prefab)
		{
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapViewController(string prefabname, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapViewController(GameObject prefab)
		{
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDoneBindingUnSleep()
		{
			return false;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallUnSleepStart()
		{
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallUnSleepUpdate()
		{
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x0000216D File Offset: 0x0000036D
		private void MakeUnSleepArray()
		{
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> CreateParameterArg<T>(T parameter) where T : class
		{
			return null;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000F29A8 File Offset: 0x000F0BA8
		protected T GetParameterArg<T>() where T : class
		{
			return default(T);
		}

		// Token: 0x04002F03 RID: 12035
		public ViewController.ViewStyle viewStyle;

		// Token: 0x04002F04 RID: 12036
		public bool acceptBack;

		// Token: 0x04002F05 RID: 12037
		public bool securitySingle031;

		// Token: 0x04002F06 RID: 12038
		public bool parallelTransition;

		// Token: 0x04002F07 RID: 12039
		public bool uniqueView;

		// Token: 0x04002F08 RID: 12040
		private Dictionary<string, object> args;

		// Token: 0x04002F09 RID: 12041
		[HideInInspector]
		public ViewControllerManager manager;

		// Token: 0x04002F0A RID: 12042
		private List<MonoBehaviour> unSleepArray;

		// Token: 0x04002F0B RID: 12043
		protected static readonly string ParameterArgKey;

		// Token: 0x0200064E RID: 1614
		public enum TransitionType
		{
			// Token: 0x04002F0D RID: 12045
			Push,
			// Token: 0x04002F0E RID: 12046
			Pop,
			// Token: 0x04002F0F RID: 12047
			Cover,
			// Token: 0x04002F10 RID: 12048
			Uncover,
			// Token: 0x04002F11 RID: 12049
			SwapIn,
			// Token: 0x04002F12 RID: 12050
			SwapOut,
			// Token: 0x04002F13 RID: 12051
			Max
		}

		// Token: 0x0200064F RID: 1615
		public enum ViewStyle
		{
			// Token: 0x04002F15 RID: 12053
			Part,
			// Token: 0x04002F16 RID: 12054
			Full
		}
	}
}

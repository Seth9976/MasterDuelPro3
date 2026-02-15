using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000651 RID: 1617
	public class ViewControllerManager : ViewController
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x0000216D File Offset: 0x0000036D
		public string ManagerName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int selectorRootPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReservedTransPush()
		{
			return false;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartTrans()
		{
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndTrans()
		{
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndTransAction()
		{
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEndTransAction(Action action)
		{
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public static void SetEventSystemEnabled(bool enabled)
		{
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x0000216D File Offset: 0x0000036D
		private void PushTransCache(ViewControllerManager.TransCache.Type type, GameObject prefab, ViewController target, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Awake()
		{
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnDestroy()
		{
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Update()
		{
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void FadeIn(ViewController hideView, ViewController.TransitionType hideTrans, ViewController dispView, ViewController.TransitionType dispTrans)
		{
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void FadeOut(ViewController hideView, ViewController.TransitionType hideTrans, ViewController dispView, ViewController.TransitionType dispTrans)
		{
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual ViewControllerManager.FadeState GetFadeState()
		{
			return ViewControllerManager.FadeState.None;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendNotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool InsertViewStack(ViewController vc)
		{
			return false;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x0000216D File Offset: 0x0000036D
		private void CleanupViewController(ViewController vc)
		{
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x0000216D File Offset: 0x0000036D
		private void AbordStack(int index, int count)
		{
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceClearViewControllerStack()
		{
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool InsertTransform(GameObject vcgo)
		{
			return false;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReadyTransition()
		{
			return false;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject LoadViewControllerPrefab(string prefabpath)
		{
			return null;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x0000216D File Offset: 0x0000036D
		public void AbortChildViewController(ViewControllerAbortRequest abortRequest)
		{
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yAbortdViewController()
		{
			return null;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopChildViewController()
		{
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopChildViewController(ViewController popTatget)
		{
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewController(string prefabpath)
		{
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewController(GameObject prefab)
		{
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewControllerParam<T>(string prefabpath, T parameter) where T : class
		{
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewControllerParam<T>(GameObject prefab, T parameter) where T : class
		{
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewController(string prefabpath, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x0000216D File Offset: 0x0000036D
		public void PushChildViewController(GameObject prefab, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0000216A File Offset: 0x0000036A
		private ViewController CreateViewController(GameObject prefab, Dictionary<string, object> args)
		{
			return null;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewController(string prefabpath)
		{
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewController(GameObject prefab)
		{
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewControllerParam<T>(string prefabpath, T parameter) where T : class
		{
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewControllerParam<T>(GameObject prefab, T parameter) where T : class
		{
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewController(string prefabpath, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTopChildViewController(GameObject prefab, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewController(string prefabpath)
		{
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewController(GameObject prefab)
		{
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewControllerParam<T>(string prefabpath, T parameter) where T : class
		{
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewControllerParam<T>(GameObject prefab, T parameter) where T : class
		{
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewController(string prefabpath, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBottomChildViewController(GameObject prefab, Dictionary<string, object> args)
		{
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapChildViewControllerParam<T>(ViewController swapTatget, string prefabpath, T parameter) where T : class
		{
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapChildViewController(ViewController swapTatget, string prefabpath, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapChildViewController(ViewController swapTatget, GameObject prefab, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0000216D File Offset: 0x0000036D
		private void doSwapChildViewController(ViewController.TransitionType disptrans, ViewController.TransitionType hidetrans, ViewController swapTatget, GameObject prefab, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertChildViewController(string prefabpath, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertChildViewController(GameObject prefab, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController SendChildResult(ViewController child, ViewController from, object value)
		{
			return null;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController FindViewController(string name, bool activeOnly = false)
		{
			return null;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsViewController(ViewController vc)
		{
			return false;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetStackTopViewController()
		{
			return null;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetStackBottomViewController()
		{
			return null;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetStackViewController(int index)
		{
			return null;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetStackViewController(string name)
		{
			return null;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000F29C0 File Offset: 0x000F0BC0
		public T GetViewController<T>() where T : ViewController
		{
			return default(T);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetStackCount()
		{
			return 0;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetStackIndex(ViewController vc)
		{
			return 0;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetFocusViewController(bool includeInactive = false)
		{
			return null;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0000216A File Offset: 0x0000036A
		public ViewController GetStyleViewController(ViewController.ViewStyle style, int depth)
		{
			return null;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x0000216D File Offset: 0x0000036D
		public void InstantiateSetup()
		{
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterTransitionAction(Action<ViewController.TransitionType, ViewController, ViewController> act)
		{
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterTransitionAction(Action<ViewController.TransitionType, ViewController, ViewController> act)
		{
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0000216A File Offset: 0x0000036A
		public static ViewControllerManager GetViewControllerManagerWithName(string name)
		{
			return null;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SendStackAction(Dictionary<string, object> dic)
		{
		}

		// Token: 0x04002F18 RID: 12056
		protected static Dictionary<string, ViewControllerManager> namedManager;

		// Token: 0x04002F19 RID: 12057
		protected List<ViewController> viewStack;

		// Token: 0x04002F1A RID: 12058
		private Action<ViewController.TransitionType, ViewController, ViewController> transitionAction;

		// Token: 0x04002F1B RID: 12059
		private ViewController.TransitionType transDispType;

		// Token: 0x04002F1C RID: 12060
		private ViewController.TransitionType transHideType;

		// Token: 0x04002F1D RID: 12061
		private ViewController transDispView;

		// Token: 0x04002F1E RID: 12062
		private ViewController transHideView;

		// Token: 0x04002F1F RID: 12063
		private bool transDispStart;

		// Token: 0x04002F20 RID: 12064
		private bool transHideStart;

		// Token: 0x04002F21 RID: 12065
		private bool transDispEnd;

		// Token: 0x04002F22 RID: 12066
		private bool transHideEnd;

		// Token: 0x04002F23 RID: 12067
		private int firstSiblingIndex;

		// Token: 0x04002F24 RID: 12068
		private List<Action> endTransActions;

		// Token: 0x04002F25 RID: 12069
		[SerializeField]
		private string managerName;

		// Token: 0x04002F26 RID: 12070
		private List<ViewControllerManager.TransCache> transCache;

		// Token: 0x04002F27 RID: 12071
		private List<ViewControllerManager.TransInsert> transInsert;

		// Token: 0x04002F28 RID: 12072
		private ViewControllerManager.FadeState dummyFadeState;

		// Token: 0x04002F29 RID: 12073
		private static string DefaultPrefabPath;

		// Token: 0x04002F2A RID: 12074
		private Coroutine m_AbordViewControllerRoutine;

		// Token: 0x02000652 RID: 1618
		public enum FadeState
		{
			// Token: 0x04002F2C RID: 12076
			None,
			// Token: 0x04002F2D RID: 12077
			FadeOuting,
			// Token: 0x04002F2E RID: 12078
			FadeOutRequired,
			// Token: 0x04002F2F RID: 12079
			FadeOut,
			// Token: 0x04002F30 RID: 12080
			FadeInning
		}

		// Token: 0x02000653 RID: 1619
		private struct TransCache
		{
			// Token: 0x04002F31 RID: 12081
			public ViewControllerManager.TransCache.Type type;

			// Token: 0x04002F32 RID: 12082
			public GameObject prefab;

			// Token: 0x04002F33 RID: 12083
			public ViewController target;

			// Token: 0x04002F34 RID: 12084
			public Dictionary<string, object> args;

			// Token: 0x02000654 RID: 1620
			public enum Type
			{
				// Token: 0x04002F36 RID: 12086
				Pop,
				// Token: 0x04002F37 RID: 12087
				Push,
				// Token: 0x04002F38 RID: 12088
				Swap,
				// Token: 0x04002F39 RID: 12089
				Insert
			}
		}

		// Token: 0x02000655 RID: 1621
		private struct TransInsert
		{
			// Token: 0x04002F3A RID: 12090
			public GameObject prefab;

			// Token: 0x04002F3B RID: 12091
			public Dictionary<string, object> args;
		}
	}
}

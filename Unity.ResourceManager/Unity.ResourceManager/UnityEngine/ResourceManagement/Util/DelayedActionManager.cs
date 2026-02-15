using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200002A RID: 42
	internal class DelayedActionManager : ComponentSingleton<DelayedActionManager>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005CD9 File Offset: 0x00003ED9
		private LinkedListNode<DelayedActionManager.DelegateInfo> GetNode(ref DelayedActionManager.DelegateInfo del)
		{
			if (this.m_NodeCache.Count > 0)
			{
				LinkedListNode<DelayedActionManager.DelegateInfo> linkedListNode = this.m_NodeCache.Pop();
				linkedListNode.Value = del;
				return linkedListNode;
			}
			return new LinkedListNode<DelayedActionManager.DelegateInfo>(del);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005D0C File Offset: 0x00003F0C
		public static void Clear()
		{
			if (ComponentSingleton<DelayedActionManager>.Exists)
			{
				ComponentSingleton<DelayedActionManager>.Instance.DestroyWhenComplete();
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005D1F File Offset: 0x00003F1F
		private void DestroyWhenComplete()
		{
			this.m_DestroyOnCompletion = true;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005D28 File Offset: 0x00003F28
		public static void AddAction(Delegate action, float delay = 0f, params object[] parameters)
		{
			ComponentSingleton<DelayedActionManager>.Instance.AddActionInternal(action, delay, parameters);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005D38 File Offset: 0x00003F38
		private void AddActionInternal(Delegate action, float delay, params object[] parameters)
		{
			DelayedActionManager.DelegateInfo del = new DelayedActionManager.DelegateInfo(action, Time.unscaledTime + delay, parameters);
			if (delay <= 0f)
			{
				this.m_Actions[this.m_CollectionIndex].Add(del);
				return;
			}
			if (this.m_DelayedActions.Count == 0)
			{
				this.m_DelayedActions.AddFirst(this.GetNode(ref del));
				return;
			}
			LinkedListNode<DelayedActionManager.DelegateInfo> i = this.m_DelayedActions.Last;
			while (i != null && i.Value.InvocationTime > del.InvocationTime)
			{
				i = i.Previous;
			}
			if (i == null)
			{
				this.m_DelayedActions.AddFirst(this.GetNode(ref del));
				return;
			}
			this.m_DelayedActions.AddBefore(i, this.GetNode(ref del));
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005DEC File Offset: 0x00003FEC
		public static bool IsActive
		{
			get
			{
				if (!ComponentSingleton<DelayedActionManager>.Exists)
				{
					return false;
				}
				if (ComponentSingleton<DelayedActionManager>.Instance.m_DelayedActions.Count > 0)
				{
					return true;
				}
				for (int i = 0; i < ComponentSingleton<DelayedActionManager>.Instance.m_Actions.Length; i++)
				{
					if (ComponentSingleton<DelayedActionManager>.Instance.m_Actions[i].Count > 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005E44 File Offset: 0x00004044
		public static bool Wait(float timeout = 0f, float timeAdvanceAmount = 0f)
		{
			if (!DelayedActionManager.IsActive)
			{
				return true;
			}
			Stopwatch timer = new Stopwatch();
			timer.Start();
			float t = Time.unscaledTime;
			do
			{
				ComponentSingleton<DelayedActionManager>.Instance.InternalLateUpdate(t);
				if (timeAdvanceAmount >= 0f)
				{
					t += timeAdvanceAmount;
				}
				else
				{
					t = Time.unscaledTime;
				}
			}
			while (DelayedActionManager.IsActive && (timeout <= 0f || timer.Elapsed.TotalSeconds < (double)timeout));
			return !DelayedActionManager.IsActive;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005EB4 File Offset: 0x000040B4
		private void LateUpdate()
		{
			this.InternalLateUpdate(Time.unscaledTime);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005EC4 File Offset: 0x000040C4
		private void InternalLateUpdate(float t)
		{
			int iterationCount = 0;
			while (this.m_DelayedActions.Count > 0 && this.m_DelayedActions.First.Value.InvocationTime <= t)
			{
				this.m_Actions[this.m_CollectionIndex].Add(this.m_DelayedActions.First.Value);
				this.m_NodeCache.Push(this.m_DelayedActions.First);
				this.m_DelayedActions.RemoveFirst();
			}
			do
			{
				int invokeIndex = this.m_CollectionIndex;
				this.m_CollectionIndex = (this.m_CollectionIndex + 1) % 2;
				List<DelayedActionManager.DelegateInfo> list = this.m_Actions[invokeIndex];
				if (list.Count > 0)
				{
					for (int i = 0; i < list.Count; i++)
					{
						list[i].Invoke();
					}
					list.Clear();
				}
				iterationCount++;
			}
			while (this.m_Actions[this.m_CollectionIndex].Count > 0);
			if (this.m_DestroyOnCompletion && !DelayedActionManager.IsActive)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005FC8 File Offset: 0x000041C8
		private void OnApplicationQuit()
		{
			if (ComponentSingleton<DelayedActionManager>.Exists)
			{
				Object.Destroy(ComponentSingleton<DelayedActionManager>.Instance.gameObject);
			}
		}

		// Token: 0x04000074 RID: 116
		private List<DelayedActionManager.DelegateInfo>[] m_Actions = new List<DelayedActionManager.DelegateInfo>[]
		{
			new List<DelayedActionManager.DelegateInfo>(),
			new List<DelayedActionManager.DelegateInfo>()
		};

		// Token: 0x04000075 RID: 117
		private LinkedList<DelayedActionManager.DelegateInfo> m_DelayedActions = new LinkedList<DelayedActionManager.DelegateInfo>();

		// Token: 0x04000076 RID: 118
		private Stack<LinkedListNode<DelayedActionManager.DelegateInfo>> m_NodeCache = new Stack<LinkedListNode<DelayedActionManager.DelegateInfo>>(10);

		// Token: 0x04000077 RID: 119
		private int m_CollectionIndex;

		// Token: 0x04000078 RID: 120
		private bool m_DestroyOnCompletion;

		// Token: 0x0200002B RID: 43
		private struct DelegateInfo
		{
			// Token: 0x06000115 RID: 277 RVA: 0x0000601C File Offset: 0x0000421C
			public DelegateInfo(Delegate d, float invocationTime, params object[] p)
			{
				this.m_Delegate = d;
				this.m_Id = DelayedActionManager.DelegateInfo.s_Id++;
				this.m_Target = p;
				this.InvocationTime = invocationTime;
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00006046 File Offset: 0x00004246
			// (set) Token: 0x06000117 RID: 279 RVA: 0x0000604E File Offset: 0x0000424E
			public float InvocationTime { readonly get; private set; }

			// Token: 0x06000118 RID: 280 RVA: 0x00006058 File Offset: 0x00004258
			public override string ToString()
			{
				if (this.m_Delegate == null || this.m_Delegate.Method.DeclaringType == null)
				{
					return "Null m_delegate for " + this.m_Id.ToString();
				}
				string[] array = new string[8];
				array[0] = this.m_Id.ToString();
				array[1] = " (target=";
				int num = 2;
				object target = this.m_Delegate.Target;
				array[num] = ((target != null) ? target.ToString() : null);
				array[3] = ") ";
				array[4] = this.m_Delegate.Method.DeclaringType.Name;
				array[5] = ".";
				array[6] = this.m_Delegate.Method.Name;
				array[7] = "(";
				string i = string.Concat(array);
				string sep = "";
				foreach (object o in this.m_Target)
				{
					i = i + sep + ((o != null) ? o.ToString() : null);
					sep = ", ";
				}
				return i + ") @" + this.InvocationTime.ToString();
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00006170 File Offset: 0x00004370
			public void Invoke()
			{
				try
				{
					this.m_Delegate.DynamicInvoke(this.m_Target);
				}
				catch (Exception e)
				{
					Debug.LogErrorFormat("Exception thrown in DynamicInvoke: {0} {1}", new object[] { e, this });
				}
			}

			// Token: 0x04000079 RID: 121
			private static int s_Id;

			// Token: 0x0400007A RID: 122
			private int m_Id;

			// Token: 0x0400007B RID: 123
			private Delegate m_Delegate;

			// Token: 0x0400007C RID: 124
			private object[] m_Target;
		}
	}
}

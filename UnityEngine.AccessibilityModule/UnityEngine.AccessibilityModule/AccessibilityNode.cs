using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000010 RID: 16
	public class AccessibilityNode
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002C7C File Offset: 0x00000E7C
		internal void FreeNative(bool freeChildren)
		{
			if (freeChildren)
			{
				foreach (AccessibilityNode child in this.m_Children)
				{
					child.FreeNative(true);
				}
			}
			this.m_Children.listChanged -= this.ChildrenChanged;
			this.m_Actions.listChanged -= this.ActionsChanged;
			bool flag = this.IsInActiveHierarchy();
			if (flag)
			{
				AccessibilityNode parent = this.parent;
				int parentId = ((parent != null) ? parent.id : (-1));
				AccessibilityNodeManager.DestroyNativeNode(this.id, parentId);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D34 File Offset: 0x00000F34
		public int id { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002D3C File Offset: 0x00000F3C
		public string label
		{
			get
			{
				return this.m_Label;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D44 File Offset: 0x00000F44
		public string value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D4C File Offset: 0x00000F4C
		public string hint
		{
			get
			{
				return this.m_Hint;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D54 File Offset: 0x00000F54
		public bool isActive
		{
			get
			{
				return this.m_IsActive;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D5C File Offset: 0x00000F5C
		public AccessibilityRole role
		{
			get
			{
				return this.m_Role;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002D64 File Offset: 0x00000F64
		public bool allowsDirectInteraction
		{
			get
			{
				return this.m_AllowsDirectInteraction;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D6C File Offset: 0x00000F6C
		public AccessibilityState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002D74 File Offset: 0x00000F74
		public AccessibilityNode parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002D7C File Offset: 0x00000F7C
		internal IList<AccessibilityNode> childList
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D84 File Offset: 0x00000F84
		public Rect frame
		{
			get
			{
				bool flag = this.m_Frame == default(Rect);
				if (flag)
				{
					this.CalculateFrame();
				}
				return this.m_Frame;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private void SetFrame(Rect frame)
		{
			bool flag = this.m_Frame == frame;
			if (!flag)
			{
				this.m_Frame = frame;
				bool flag2 = this.IsInActiveHierarchy();
				if (flag2)
				{
					AccessibilityNodeManager.SetFrame(this.id, frame);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E01 File Offset: 0x00001001
		public Func<Rect> frameGetter
		{
			get
			{
				return this.m_FrameGetter;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E09 File Offset: 0x00001009
		internal void CalculateFrame()
		{
			Func<Rect> frameGetter = this.frameGetter;
			this.SetFrame((frameGetter != null) ? frameGetter() : Rect.zero);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002E29 File Offset: 0x00001029
		internal SystemLanguage language
		{
			get
			{
				return this.m_Language;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E34 File Offset: 0x00001034
		internal void GetNodeData(ref AccessibilityNodeData nodeData)
		{
			nodeData.id = this.id;
			nodeData.isActive = this.isActive;
			nodeData.label = this.label;
			nodeData.value = this.value;
			nodeData.hint = this.hint;
			nodeData.role = this.role;
			nodeData.allowsDirectInteraction = this.allowsDirectInteraction;
			nodeData.state = this.state;
			nodeData.frame = this.frame;
			AccessibilityNode parent = this.parent;
			nodeData.parentId = ((parent != null) ? parent.id : (-1));
			int[] nodeChildIds = new int[this.m_Children.Count];
			for (int i = 0; i < this.m_Children.Count; i++)
			{
				nodeChildIds[i] = this.m_Children[i].id;
			}
			nodeData.childIds = nodeChildIds;
			nodeData.language = this.language;
			nodeData.implementsSelected = this.selected != null;
			nodeData.implementsDismissed = this.dismissed != null;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F48 File Offset: 0x00001148
		private void ChildrenChanged()
		{
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				int[] nodeChildIds = new int[this.m_Children.Count];
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					nodeChildIds[i] = this.m_Children[i].id;
				}
				AccessibilityNodeManager.SetChildren(this.id, nodeChildIds);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002FB4 File Offset: 0x000011B4
		private void ActionsChanged()
		{
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				AccessibilityAction[] nodeActions = new AccessibilityAction[this.m_Actions.Count];
				for (int i = 0; i < this.m_Actions.Count; i++)
				{
					nodeActions[i] = this.m_Actions[i];
				}
				AccessibilityNodeManager.SetActions(this.id, nodeActions);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000301C File Offset: 0x0000121C
		private bool IsInActiveHierarchy()
		{
			return this.m_Hierarchy != null && AssistiveSupport.activeHierarchy == this.m_Hierarchy;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003048 File Offset: 0x00001248
		internal void NotifyFocusChanged(bool isNodeFocused)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = (isNodeFocused ? AccessibilityNotification.ElementFocused : AccessibilityNotification.ElementUnfocused),
				currentNode = this
			});
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000307D File Offset: 0x0000127D
		internal void InvokeFocusChanged(bool isNodeFocused)
		{
			Action<AccessibilityNode, bool> action = this.focusChanged;
			if (action != null)
			{
				action(this, isNodeFocused);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003094 File Offset: 0x00001294
		internal bool InvokeSelected()
		{
			Func<bool> func = this.selected;
			return func != null && func();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030B8 File Offset: 0x000012B8
		internal void InvokeIncremented()
		{
			Action action = this.incremented;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030CD File Offset: 0x000012CD
		internal void InvokeDecremented()
		{
			Action action = this.decremented;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000030E4 File Offset: 0x000012E4
		internal bool Dismissed()
		{
			Func<bool> func = this.dismissed;
			return func != null && func();
		}

		// Token: 0x0400004D RID: 77
		private Func<Rect> m_FrameGetter;

		// Token: 0x0400004E RID: 78
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<AccessibilityNode, bool> focusChanged;

		// Token: 0x0400004F RID: 79
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<bool> selected;

		// Token: 0x04000050 RID: 80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action incremented;

		// Token: 0x04000051 RID: 81
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action decremented;

		// Token: 0x04000052 RID: 82
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<bool> dismissed;

		// Token: 0x04000053 RID: 83
		private string m_Label;

		// Token: 0x04000054 RID: 84
		private string m_Value;

		// Token: 0x04000055 RID: 85
		private string m_Hint;

		// Token: 0x04000056 RID: 86
		private bool m_IsActive;

		// Token: 0x04000057 RID: 87
		private AccessibilityRole m_Role;

		// Token: 0x04000058 RID: 88
		private bool m_AllowsDirectInteraction;

		// Token: 0x04000059 RID: 89
		private AccessibilityState m_State;

		// Token: 0x0400005A RID: 90
		private AccessibilityNode m_Parent;

		// Token: 0x0400005B RID: 91
		private AccessibilityNode.ObservableList<AccessibilityNode> m_Children;

		// Token: 0x0400005C RID: 92
		private AccessibilityNode.ObservableList<AccessibilityAction> m_Actions;

		// Token: 0x0400005D RID: 93
		private Rect m_Frame;

		// Token: 0x0400005E RID: 94
		private SystemLanguage m_Language;

		// Token: 0x0400005F RID: 95
		private AccessibilityHierarchy m_Hierarchy;

		// Token: 0x02000011 RID: 17
		private class ObservableList<T>
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00003108 File Offset: 0x00001308
			public int Count
			{
				get
				{
					return this.m_Items.Count;
				}
			}

			// Token: 0x17000031 RID: 49
			public T this[int index]
			{
				get
				{
					return this.m_Items[index];
				}
			}

			// Token: 0x0600007F RID: 127 RVA: 0x00003123 File Offset: 0x00001323
			public IEnumerator<T> GetEnumerator()
			{
				return this.m_Items.GetEnumerator();
			}

			// Token: 0x14000003 RID: 3
			// (add) Token: 0x06000080 RID: 128 RVA: 0x00003138 File Offset: 0x00001338
			// (remove) Token: 0x06000081 RID: 129 RVA: 0x00003170 File Offset: 0x00001370
			[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Action listChanged;

			// Token: 0x04000060 RID: 96
			private readonly List<T> m_Items;
		}
	}
}

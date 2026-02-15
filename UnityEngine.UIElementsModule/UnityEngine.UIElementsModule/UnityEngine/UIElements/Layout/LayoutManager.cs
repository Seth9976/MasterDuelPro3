using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000573 RID: 1395
	internal class LayoutManager : IDisposable
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00098A53 File Offset: 0x00096C53
		public static LayoutManager SharedManager
		{
			get
			{
				return LayoutManager.s_SharedInstance;
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00098A5A File Offset: 0x00096C5A
		static LayoutManager()
		{
			LayoutManager.Initialize();
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00098A70 File Offset: 0x00096C70
		private static void Initialize()
		{
			bool flag = LayoutManager.s_Initialized;
			if (!flag)
			{
				LayoutManager.s_Initialized = true;
				bool flag2 = !LayoutManager.s_AppDomainUnloadRegistered;
				if (flag2)
				{
					AppDomain.CurrentDomain.DomainUnload += delegate(object _, EventArgs __)
					{
						bool flag3 = LayoutManager.s_Initialized;
						if (flag3)
						{
							LayoutManager.Shutdown();
						}
					};
					LayoutManager.s_AppDomainUnloadRegistered = true;
				}
				LayoutManager.s_SharedInstance = new LayoutManager(Allocator.Persistent);
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00098AD8 File Offset: 0x00096CD8
		private static void Shutdown()
		{
			bool flag = !LayoutManager.s_Initialized;
			if (!flag)
			{
				LayoutManager.s_Initialized = false;
				LayoutManager.s_SharedInstance.Dispose();
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00098B05 File Offset: 0x00096D05
		internal static LayoutManager GetManager(int index)
		{
			return ((ulong)index < (ulong)((long)LayoutManager.s_Managers.Count)) ? LayoutManager.s_Managers[index] : null;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00098B24 File Offset: 0x00096D24
		public LayoutManager(Allocator allocator)
		{
			this.m_Index = LayoutManager.s_Managers.Count;
			LayoutManager.s_Managers.Add(this);
			ComponentType[] nodeComponentTypes = new ComponentType[]
			{
				ComponentType.Create<LayoutNodeData>(),
				ComponentType.Create<LayoutStyleData>(),
				ComponentType.Create<LayoutComputedData>(),
				ComponentType.Create<LayoutCacheData>()
			};
			ComponentType[] configComponentTypes = new ComponentType[] { ComponentType.Create<LayoutConfigData>() };
			this.m_Nodes = new LayoutDataStore(nodeComponentTypes, 65536, allocator);
			this.m_Configs = new LayoutDataStore(configComponentTypes, 32, allocator);
			this.m_DefaultConfig = this.CreateConfig().Handle;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00098C14 File Offset: 0x00096E14
		public unsafe void Dispose()
		{
			LayoutManager.s_Managers[this.m_Index] = null;
			for (int i = 0; i <= this.m_HighMark; i++)
			{
				LayoutNodeData* data = (LayoutNodeData*)this.m_Nodes.GetComponentDataPtr(i, 0);
				bool flag = !data->Children.IsCreated;
				if (!flag)
				{
					data->Children.Dispose();
					data->Children = new LayoutList<LayoutHandle>();
				}
			}
			this.m_Nodes.Dispose();
			this.m_Configs.Dispose();
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00098CA4 File Offset: 0x00096EA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private LayoutDataAccess GetAccess()
		{
			return new LayoutDataAccess(this.m_Index, this.m_Nodes, this.m_Configs);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00098CD0 File Offset: 0x00096ED0
		public LayoutConfig GetDefaultConfig()
		{
			return new LayoutConfig(this.GetAccess(), this.m_DefaultConfig);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00098CF4 File Offset: 0x00096EF4
		public LayoutConfig CreateConfig()
		{
			LayoutDataAccess access = this.GetAccess();
			LayoutConfigData @default = LayoutConfigData.Default;
			return new LayoutConfig(access, this.m_Configs.Allocate<LayoutConfigData>(in @default));
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00098D24 File Offset: 0x00096F24
		public void DestroyConfig(ref LayoutConfig config)
		{
			LayoutHandle handle = config.Handle;
			this.m_Configs.Free(in handle);
			config = LayoutConfig.Undefined;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00098D54 File Offset: 0x00096F54
		public LayoutNode CreateNode()
		{
			return this.CreateNodeInternal(this.m_DefaultConfig);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00098D74 File Offset: 0x00096F74
		private LayoutNode CreateNodeInternal(LayoutHandle configHandle)
		{
			this.TryFreeNodes();
			LayoutNodeData layoutNodeData = default(LayoutNodeData);
			layoutNodeData.Config = configHandle;
			layoutNodeData.Children = new LayoutList<LayoutHandle>();
			LayoutComputedData @default = LayoutComputedData.Default;
			LayoutHandle handle = this.m_Nodes.Allocate<LayoutNodeData, LayoutStyleData, LayoutComputedData, LayoutCacheData>(in layoutNodeData, in LayoutStyleData.Default, in @default, in LayoutCacheData.Default);
			bool flag = handle.Index > this.m_HighMark;
			if (flag)
			{
				this.m_HighMark = handle.Index;
			}
			LayoutNode node = new LayoutNode(this.GetAccess(), handle);
			Debug.Assert(!this.GetAccess().GetNodeData(handle).Children.IsCreated, "memory is not initialized");
			return node;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00098E24 File Offset: 0x00097024
		private void TryFreeNodes()
		{
			bool @lock = false;
			try
			{
				Monitor.TryEnter(this.m_SyncRoot, ref @lock);
				bool flag = @lock;
				if (flag)
				{
					while (this.m_NodesToFree.Count > 0)
					{
						this.FreeNode(this.m_NodesToFree.Pop());
					}
				}
			}
			finally
			{
				bool flag2 = @lock;
				if (flag2)
				{
					Monitor.Exit(this.m_SyncRoot);
				}
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00098E98 File Offset: 0x00097098
		public void DestroyNode(ref LayoutNode node)
		{
			bool isUndefined = node.IsUndefined;
			if (!isUndefined)
			{
				LayoutDataAccess access = this.GetAccess();
				bool flag = !access.IsValid;
				if (!flag)
				{
					ref LayoutNodeData data = ref access.GetNodeData(node.Handle);
					bool isCreated = data.Children.IsCreated;
					if (isCreated)
					{
						data.Children.Dispose();
						data.Children = new LayoutList<LayoutHandle>();
					}
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_NodesToFree.Push(node.Handle);
					}
					node = LayoutNode.Undefined;
				}
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00098F58 File Offset: 0x00097158
		private void FreeNode(LayoutHandle handle)
		{
			ref LayoutNodeData data = ref this.GetAccess().GetNodeData(handle);
			this.m_ManagedMeasureFunctions.UpdateValue(ref data.ManagedMeasureFunctionIndex, null);
			this.m_ManagedBaselineFunctions.UpdateValue(ref data.ManagedBaselineFunctionIndex, null);
			this.m_ManagedOwners.UpdateValue(ref data.ManagedOwnerIndex, null);
			this.m_Nodes.Free(in handle);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00098FC0 File Offset: 0x000971C0
		public LayoutMeasureFunction GetMeasureFunction(LayoutHandle handle)
		{
			return this.m_ManagedMeasureFunctions.GetValue(this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00098FF4 File Offset: 0x000971F4
		public void SetMeasureFunction(LayoutHandle handle, LayoutMeasureFunction value)
		{
			ref int index = ref this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex;
			this.m_ManagedMeasureFunctions.UpdateValue(ref index, value);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00099028 File Offset: 0x00097228
		public VisualElement GetOwner(LayoutHandle handle)
		{
			bool flag = this.GetAccess().GetNodeData(handle).ManagedOwnerIndex == 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				VisualElement ve;
				this.m_ManagedOwners.GetValue(this.GetAccess().GetNodeData(handle).ManagedOwnerIndex).TryGetTarget(out ve);
				visualElement = ve;
			}
			return visualElement;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00099084 File Offset: 0x00097284
		public void SetOwner(LayoutHandle handle, VisualElement value)
		{
			ref int index = ref this.GetAccess().GetNodeData(handle).ManagedOwnerIndex;
			this.m_ManagedOwners.UpdateValue(ref index, new WeakReference<VisualElement>(value));
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000990BC File Offset: 0x000972BC
		public LayoutBaselineFunction GetBaselineFunction(LayoutHandle handle)
		{
			return this.m_ManagedBaselineFunctions.GetValue(this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex);
		}

		// Token: 0x04001368 RID: 4968
		private static bool s_Initialized;

		// Token: 0x04001369 RID: 4969
		private static bool s_AppDomainUnloadRegistered;

		// Token: 0x0400136A RID: 4970
		private static LayoutManager s_SharedInstance;

		// Token: 0x0400136B RID: 4971
		private static readonly List<LayoutManager> s_Managers = new List<LayoutManager>();

		// Token: 0x0400136C RID: 4972
		private readonly int m_Index;

		// Token: 0x0400136D RID: 4973
		private LayoutDataStore m_Nodes;

		// Token: 0x0400136E RID: 4974
		private LayoutDataStore m_Configs;

		// Token: 0x0400136F RID: 4975
		private readonly object m_SyncRoot = new object();

		// Token: 0x04001370 RID: 4976
		private readonly Stack<LayoutHandle> m_NodesToFree = new Stack<LayoutHandle>();

		// Token: 0x04001371 RID: 4977
		private readonly LayoutHandle m_DefaultConfig;

		// Token: 0x04001372 RID: 4978
		private readonly ManagedObjectStore<LayoutMeasureFunction> m_ManagedMeasureFunctions = new ManagedObjectStore<LayoutMeasureFunction>();

		// Token: 0x04001373 RID: 4979
		private readonly ManagedObjectStore<LayoutBaselineFunction> m_ManagedBaselineFunctions = new ManagedObjectStore<LayoutBaselineFunction>();

		// Token: 0x04001374 RID: 4980
		private readonly ManagedObjectStore<WeakReference<VisualElement>> m_ManagedOwners = new ManagedObjectStore<WeakReference<VisualElement>>();

		// Token: 0x04001375 RID: 4981
		private int m_HighMark = -1;
	}
}

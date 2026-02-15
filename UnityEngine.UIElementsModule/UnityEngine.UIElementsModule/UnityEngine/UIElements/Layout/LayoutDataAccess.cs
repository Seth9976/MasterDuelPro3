using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000585 RID: 1413
	[RequiredByNativeCode]
	internal readonly struct LayoutDataAccess
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0009A55C File Offset: 0x0009875C
		public bool IsValid
		{
			get
			{
				return this.m_Nodes.IsValid && this.m_Configs.IsValid;
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0009A58A File Offset: 0x0009878A
		internal LayoutDataAccess(int manager, LayoutDataStore nodes, LayoutDataStore configs)
		{
			this.m_Manager = manager;
			this.m_Nodes = nodes;
			this.m_Configs = configs;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0009A5A2 File Offset: 0x000987A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe ref T GetTypedNodeDataRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(LayoutHandle handle, LayoutNodeDataType type) where T : struct, ValueType
		{
			return ref *(T*)this.m_Nodes.GetComponentDataPtr(handle.Index, (int)type);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0009A5B6 File Offset: 0x000987B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe ref T GetTypedConfigDataRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(LayoutHandle handle, LayoutConfigDataType type) where T : struct, ValueType
		{
			return ref *(T*)this.m_Configs.GetComponentDataPtr(handle.Index, (int)type);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0009A5CA File Offset: 0x000987CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutNodeData GetNodeData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutNodeData>(handle, LayoutNodeDataType.Node);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0009A5D4 File Offset: 0x000987D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutStyleData GetStyleData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutStyleData>(handle, LayoutNodeDataType.Style);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0009A5DE File Offset: 0x000987DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutComputedData GetComputedData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutComputedData>(handle, LayoutNodeDataType.Computed);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0009A5E8 File Offset: 0x000987E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutConfigData GetConfigData(LayoutHandle handle)
		{
			return this.GetTypedConfigDataRef<LayoutConfigData>(handle, LayoutConfigDataType.Config);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0009A5F2 File Offset: 0x000987F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LayoutMeasureFunction GetMeasureFunction(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetMeasureFunction(handle);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0009A605 File Offset: 0x00098805
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMeasureFunction(LayoutHandle handle, LayoutMeasureFunction value)
		{
			LayoutManager.GetManager(this.m_Manager).SetMeasureFunction(handle, value);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0009A61A File Offset: 0x0009881A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public VisualElement GetOwner(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetOwner(handle);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0009A62D File Offset: 0x0009882D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetOwner(LayoutHandle handle, VisualElement value)
		{
			LayoutManager.GetManager(this.m_Manager).SetOwner(handle, value);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0009A642 File Offset: 0x00098842
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LayoutBaselineFunction GetBaselineFunction(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetBaselineFunction(handle);
		}

		// Token: 0x040013A4 RID: 5028
		private readonly int m_Manager;

		// Token: 0x040013A5 RID: 5029
		private readonly LayoutDataStore m_Nodes;

		// Token: 0x040013A6 RID: 5030
		private readonly LayoutDataStore m_Configs;
	}
}

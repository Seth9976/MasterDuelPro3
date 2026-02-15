using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyViewModel.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyViewModelBindings.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyViewModel : IDisposable
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003ED6 File Offset: 0x000020D6
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003EE8 File Offset: 0x000020E8
		public int Count
		{
			get
			{
				return this.m_NodesCount;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003EF0 File Offset: 0x000020F0
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003F12 File Offset: 0x00002112
		public HierarchyFlattened HierarchyFlattened
		{
			get
			{
				return this.m_HierarchyFlattened;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003F1A File Offset: 0x0000211A
		internal int Version
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000031 RID: 49
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003F22 File Offset: 0x00002122
		internal IHierarchySearchQueryParser QueryParser
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
			[CompilerGenerated]
			set
			{
				this.<QueryParser>k__BackingField = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003F2C File Offset: 0x0000212C
		internal HierarchySearchQueryDescriptor Query
		{
			[NativeMethod(IsThreadSafe = true)]
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_Query_Injected(intPtr);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F50 File Offset: 0x00002150
		public HierarchyViewModel(HierarchyFlattened hierarchyFlattened, HierarchyNodeFlags defaultFlags = HierarchyNodeFlags.None)
		{
			IntPtr nodesPtr;
			int nodesCount;
			int version;
			this.m_Ptr = HierarchyViewModel.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), hierarchyFlattened, defaultFlags, out nodesPtr, out nodesCount, out version);
			this.m_Hierarchy = hierarchyFlattened.Hierarchy;
			this.m_HierarchyFlattened = hierarchyFlattened;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = true;
			this.QueryParser = new DefaultHierarchySearchQueryParser();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003FC0 File Offset: 0x000021C0
		private HierarchyViewModel(IntPtr nativePtr, HierarchyFlattened hierarchyFlattened, IntPtr nodesPtr, int nodesCount, int version)
		{
			this.m_Ptr = nativePtr;
			this.m_Hierarchy = hierarchyFlattened.Hierarchy;
			this.m_HierarchyFlattened = hierarchyFlattened;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = false;
			this.QueryParser = new DefaultHierarchySearchQueryParser();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000401C File Offset: 0x0000221C
		~HierarchyViewModel()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004050 File Offset: 0x00002250
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004064 File Offset: 0x00002264
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyViewModel.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000033 RID: 51
		public unsafe ref HierarchyNode this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= this.m_NodesCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return HierarchyFlattenedNode.GetNodeByRef(this.m_HierarchyFlattened[*(int*)((byte*)(void*)this.m_NodesPtr + (IntPtr)index * 4)]);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004104 File Offset: 0x00002304
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int IndexOf(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.IndexOf_Injected(intPtr, in node);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004128 File Offset: 0x00002328
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Contains(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.Contains_Injected(intPtr, in node);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000414C File Offset: 0x0000234C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000416F File Offset: 0x0000236F
		public void SetFlags(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			this.SetFlagsNode(in node, flags, recurse);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000417B File Offset: 0x0000237B
		public bool HasAllFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsNode(in node, flags);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004185 File Offset: 0x00002385
		public void ClearFlags(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			this.ClearFlagsNode(in node, flags, recurse);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004191 File Offset: 0x00002391
		public HierarchyViewNodesEnumerable EnumerateNodesWithAllFlags(HierarchyNodeFlags flags)
		{
			return new HierarchyViewNodesEnumerable(this, flags, new HierarchyViewNodesEnumerable.Predicate(this.HasAllFlags));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000041A8 File Offset: 0x000023A8
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.Update_Injected(intPtr);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000041CA File Offset: 0x000023CA
		public HierarchyViewModel.Enumerator GetEnumerator()
		{
			return new HierarchyViewModel.Enumerator(this);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000041D4 File Offset: 0x000023D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyViewModel FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyViewModel)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004204 File Offset: 0x00002404
		[FreeFunction("HierarchyViewModelBindings::Create", IsThreadSafe = true)]
		private static IntPtr Create(IntPtr handlePtr, HierarchyFlattened hierarchyFlattened, HierarchyNodeFlags defaultFlags, out IntPtr nodesPtr, out int nodesCount, out int version)
		{
			return HierarchyViewModel.Create_Injected(handlePtr, (hierarchyFlattened == null) ? ((IntPtr)0) : HierarchyFlattened.BindingsMarshaller.ConvertToNative(hierarchyFlattened), defaultFlags, out nodesPtr, out nodesCount, out version);
		}

		// Token: 0x06000102 RID: 258
		[FreeFunction("HierarchyViewModelBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x06000103 RID: 259 RVA: 0x0000422C File Offset: 0x0000242C
		[FreeFunction("HierarchyViewModelBindings::SetFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void SetFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.SetFlagsNode_Injected(intPtr, in node, flags, recurse);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004254 File Offset: 0x00002454
		[FreeFunction("HierarchyViewModelBindings::HasAllFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool HasAllFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAllFlagsNode_Injected(intPtr, in node, flags);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004278 File Offset: 0x00002478
		[FreeFunction("HierarchyViewModelBindings::ClearFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void ClearFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.ClearFlagsNode_Injected(intPtr, in node, flags, recurse);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000429D File Offset: 0x0000249D
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchyViewModel(IntPtr nativePtr, IntPtr flattenedPtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyViewModel(nativePtr, HierarchyFlattened.FromIntPtr(flattenedPtr), nodesPtr, nodesCount, version)));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000042BC File Offset: 0x000024BC
		[RequiredByNativeCode]
		private static void UpdateHierarchyViewModel(IntPtr handlePtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			HierarchyViewModel viewModel = HierarchyViewModel.FromIntPtr(handlePtr);
			viewModel.m_NodesPtr = nodesPtr;
			viewModel.m_NodesCount = nodesCount;
			viewModel.m_Version = version;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000042E8 File Offset: 0x000024E8
		[RequiredByNativeCode]
		private static void SearchBegin(IntPtr handlePtr)
		{
			HierarchyViewModel viewModel = HierarchyViewModel.FromIntPtr(handlePtr);
			foreach (HierarchyNodeTypeHandlerBase handler in viewModel.m_Hierarchy.EnumerateNodeTypeHandlersBase())
			{
				handler.Internal_SearchBegin(viewModel.Query);
			}
		}

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HierarchySearchQueryDescriptor get_Query_Injected(IntPtr _unity_self);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IndexOf_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(IntPtr handlePtr, IntPtr hierarchyFlattened, HierarchyNodeFlags defaultFlags, out IntPtr nodesPtr, out int nodesCount, out int version);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags, bool recurse);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAllFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags, bool recurse);

		// Token: 0x04000075 RID: 117
		private IntPtr m_Ptr;

		// Token: 0x04000076 RID: 118
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000077 RID: 119
		private readonly HierarchyFlattened m_HierarchyFlattened;

		// Token: 0x04000078 RID: 120
		private IntPtr m_NodesPtr;

		// Token: 0x04000079 RID: 121
		private int m_NodesCount;

		// Token: 0x0400007A RID: 122
		private int m_Version;

		// Token: 0x0400007B RID: 123
		private readonly bool m_IsOwner;

		// Token: 0x02000029 RID: 41
		internal static class BindingsMarshaller
		{
			// Token: 0x06000113 RID: 275 RVA: 0x00004354 File Offset: 0x00002554
			public static IntPtr ConvertToNative(HierarchyViewModel viewModel)
			{
				return viewModel.m_Ptr;
			}
		}

		// Token: 0x0200002A RID: 42
		public struct Enumerator
		{
			// Token: 0x06000114 RID: 276 RVA: 0x0000435C File Offset: 0x0000255C
			internal unsafe Enumerator(HierarchyViewModel hierarchyViewModel)
			{
				this.m_ViewModel = hierarchyViewModel;
				this.m_HierarchyFlattened = hierarchyViewModel.HierarchyFlattened;
				this.m_NodesPtr = (int*)(void*)hierarchyViewModel.m_NodesPtr;
				this.m_NodesCount = hierarchyViewModel.Count;
				this.m_Version = hierarchyViewModel.Version;
				this.m_Index = -1;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000115 RID: 277 RVA: 0x000043B0 File Offset: 0x000025B0
			public unsafe readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					bool flag = this.m_Version != this.m_ViewModel.m_Version;
					if (flag)
					{
						throw new InvalidOperationException("HierarchyViewModel was modified.");
					}
					return HierarchyFlattenedNode.GetNodeByRef(this.m_HierarchyFlattened[this.m_NodesPtr[this.m_Index]]);
				}
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00004408 File Offset: 0x00002608
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_NodesCount;
			}

			// Token: 0x0400007D RID: 125
			private readonly HierarchyViewModel m_ViewModel;

			// Token: 0x0400007E RID: 126
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x0400007F RID: 127
			private unsafe readonly int* m_NodesPtr;

			// Token: 0x04000080 RID: 128
			private readonly int m_NodesCount;

			// Token: 0x04000081 RID: 129
			private readonly int m_Version;

			// Token: 0x04000082 RID: 130
			private int m_Index;
		}
	}
}

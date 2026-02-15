using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeTypeHandlerBase.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyNodeTypeHandlerBaseBindings.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class HierarchyNodeTypeHandlerBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002487 File Offset: 0x00000687
		protected virtual void Initialize()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002487 File Offset: 0x00000687
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000248C File Offset: 0x0000068C
		[NativeMethod(IsThreadSafe = true)]
		public virtual string GetNodeTypeName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				HierarchyNodeTypeHandlerBase.GetNodeTypeName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024CC File Offset: 0x000006CC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public virtual HierarchyNodeFlags GetDefaultNodeFlags(in HierarchyNode node, HierarchyNodeFlags defaultFlags = HierarchyNodeFlags.None)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.GetDefaultNodeFlags_Injected(intPtr, in node, defaultFlags);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024F0 File Offset: 0x000006F0
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchBegin", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual void SearchBegin(HierarchySearchQueryDescriptor query)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeTypeHandlerBase.SearchBegin_Injected(intPtr, query);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002514 File Offset: 0x00000714
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchMatch", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual bool SearchMatch(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.SearchMatch_Injected(intPtr, in node);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002538 File Offset: 0x00000738
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchEnd", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual void SearchEnd()
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeTypeHandlerBase.SearchEnd_Injected(intPtr);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000255C File Offset: 0x0000075C
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyNodeTypeHandlerBase FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyNodeTypeHandlerBase)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000258C File Offset: 0x0000078C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Internal_SearchBegin(HierarchySearchQueryDescriptor query)
		{
			this.SearchBegin(query);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002598 File Offset: 0x00000798
		[RequiredByNativeCode]
		private static IntPtr CreateNodeTypeHandlerFromType(IntPtr nativePtr, Type handlerType, IntPtr hierarchyPtr, IntPtr cmdListPtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException("nativePtr");
			}
			bool flag2 = hierarchyPtr == IntPtr.Zero;
			if (flag2)
			{
				throw new ArgumentNullException("hierarchyPtr");
			}
			bool flag3 = cmdListPtr == IntPtr.Zero;
			if (flag3)
			{
				throw new ArgumentNullException("cmdListPtr");
			}
			Hierarchy hierarchy = Hierarchy.FromIntPtr(hierarchyPtr);
			HierarchyCommandList cmdList = HierarchyCommandList.FromIntPtr(cmdListPtr);
			IntPtr intPtr;
			using (new HierarchyNodeTypeHandlerBase.ConstructorScope(nativePtr, hierarchy, cmdList))
			{
				BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
				HierarchyNodeTypeHandlerBase handler = (HierarchyNodeTypeHandlerBase)Activator.CreateInstance(handlerType, flags, null, null, null);
				bool flag4 = handler == null;
				if (flag4)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					handler.Initialize();
					intPtr = GCHandle.ToIntPtr(GCHandle.Alloc(handler));
				}
			}
			return intPtr;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002674 File Offset: 0x00000874
		[RequiredByNativeCode]
		private static bool TryGetStaticNodeType(Type handlerType, out int nodeType)
		{
			bool flag = HierarchyNodeTypeHandlerBase.s_NodeTypes.TryGetValue(handlerType, out nodeType);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				MethodInfo method = handlerType.GetMethod("GetStaticNodeType", BindingFlags.Static | BindingFlags.NonPublic);
				bool flag3 = method != null;
				if (flag3)
				{
					nodeType = (int)method.Invoke(null, null);
					HierarchyNodeTypeHandlerBase.s_NodeTypes.Add(handlerType, nodeType);
					flag2 = true;
				}
				else
				{
					nodeType = 0;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026D9 File Offset: 0x000008D9
		[RequiredByNativeCode]
		private static void InvokeInitialize(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).Initialize();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026E8 File Offset: 0x000008E8
		[RequiredByNativeCode]
		private static void InvokeDispose(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase handler = HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr);
			handler.Dispose(true);
			GC.SuppressFinalize(handler);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000270C File Offset: 0x0000090C
		[RequiredByNativeCode]
		private static string InvokeGetNodeTypeName(IntPtr handlePtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).GetNodeTypeName();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002719 File Offset: 0x00000919
		[RequiredByNativeCode]
		private static HierarchyNodeFlags InvokeGetDefaultNodeFlags(IntPtr handlePtr, in HierarchyNode node, HierarchyNodeFlags defaultFlags)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).GetDefaultNodeFlags(in node, defaultFlags);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002728 File Offset: 0x00000928
		[RequiredByNativeCode]
		private static bool InvokeChangesPending(IntPtr handlePtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).ChangesPending();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002735 File Offset: 0x00000935
		[RequiredByNativeCode]
		private static bool InvokeIntegrateChanges(IntPtr handlePtr, IntPtr cmdListPtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).IntegrateChanges(HierarchyCommandList.FromIntPtr(cmdListPtr));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002748 File Offset: 0x00000948
		[RequiredByNativeCode]
		private static bool InvokeSearchMatch(IntPtr handlePtr, in HierarchyNode node)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).SearchMatch(in node);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002756 File Offset: 0x00000956
		[RequiredByNativeCode]
		private static void InvokeSearchEnd(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).SearchEnd();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002764 File Offset: 0x00000964
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::ChangesPending", HasExplicitThis = true, IsThreadSafe = true)]
		[Obsolete("ChangesPending is obsolete, it is replaced by adding commands into the hierarchy node type handler's CommandList.", false)]
		protected virtual bool ChangesPending()
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.ChangesPending_Injected(intPtr);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002788 File Offset: 0x00000988
		[Obsolete("IntegrateChanges is obsolete, it is replaced by adding commands into the hierarchy node type handler's CommandList.", false)]
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::IntegrateChanges", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual bool IntegrateChanges(HierarchyCommandList cmdList)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.IntegrateChanges_Injected(intPtr, (cmdList == null) ? ((IntPtr)0) : HierarchyCommandList.BindingsMarshaller.ConvertToNative(cmdList));
		}

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeTypeName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HierarchyNodeFlags GetDefaultNodeFlags_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags defaultFlags);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SearchBegin_Injected(IntPtr _unity_self, HierarchySearchQueryDescriptor query);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SearchMatch_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SearchEnd_Injected(IntPtr _unity_self);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ChangesPending_Injected(IntPtr _unity_self);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IntegrateChanges_Injected(IntPtr _unity_self, IntPtr cmdList);

		// Token: 0x04000017 RID: 23
		internal readonly IntPtr m_Ptr;

		// Token: 0x04000018 RID: 24
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000019 RID: 25
		private readonly HierarchyCommandList m_CommandList;

		// Token: 0x0400001A RID: 26
		private static readonly Dictionary<Type, int> s_NodeTypes = new Dictionary<Type, int>();

		// Token: 0x0200000B RID: 11
		internal static class BindingsMarshaller
		{
			// Token: 0x0600002C RID: 44 RVA: 0x000027C5 File Offset: 0x000009C5
			public static IntPtr ConvertToNative(HierarchyNodeTypeHandlerBase handler)
			{
				return handler.m_Ptr;
			}
		}

		// Token: 0x0200000C RID: 12
		private struct ConstructorScope : IDisposable
		{
			// Token: 0x17000003 RID: 3
			// (set) Token: 0x0600002D RID: 45 RVA: 0x000027CD File Offset: 0x000009CD
			private static IntPtr Ptr
			{
				set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_Ptr = value;
				}
			}

			// Token: 0x17000004 RID: 4
			// (set) Token: 0x0600002E RID: 46 RVA: 0x000027D5 File Offset: 0x000009D5
			private static Hierarchy Hierarchy
			{
				set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_Hierarchy = value;
				}
			}

			// Token: 0x17000005 RID: 5
			// (set) Token: 0x0600002F RID: 47 RVA: 0x000027DD File Offset: 0x000009DD
			private static HierarchyCommandList CommandList
			{
				set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_CommandList = value;
				}
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000027E5 File Offset: 0x000009E5
			public ConstructorScope(IntPtr nativePtr, Hierarchy hierarchy, HierarchyCommandList cmdList)
			{
				HierarchyNodeTypeHandlerBase.ConstructorScope.Ptr = nativePtr;
				HierarchyNodeTypeHandlerBase.ConstructorScope.Hierarchy = hierarchy;
				HierarchyNodeTypeHandlerBase.ConstructorScope.CommandList = cmdList;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x000027FD File Offset: 0x000009FD
			public void Dispose()
			{
				HierarchyNodeTypeHandlerBase.ConstructorScope.Ptr = IntPtr.Zero;
				HierarchyNodeTypeHandlerBase.ConstructorScope.Hierarchy = null;
				HierarchyNodeTypeHandlerBase.ConstructorScope.CommandList = null;
			}

			// Token: 0x0400001B RID: 27
			[ThreadStatic]
			private static IntPtr m_Ptr;

			// Token: 0x0400001C RID: 28
			[ThreadStatic]
			private static Hierarchy m_Hierarchy;

			// Token: 0x0400001D RID: 29
			[ThreadStatic]
			private static HierarchyCommandList m_CommandList;
		}
	}
}

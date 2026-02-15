using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/Public/Hierarchy.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeTypeHandlerBase.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Hierarchy : IDisposable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C63 File Offset: 0x00000E63
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C78 File Offset: 0x00000E78
		public unsafe readonly ref HierarchyNode Root
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (void*)this.m_RootPtr;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C98 File Offset: 0x00000E98
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Hierarchy.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002CBC File Offset: 0x00000EBC
		internal unsafe int Version
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return *(int*)(void*)this.m_VersionPtr;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CDC File Offset: 0x00000EDC
		public Hierarchy()
		{
			IntPtr rootPtr;
			IntPtr versionPtr;
			this.m_Ptr = Hierarchy.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), out rootPtr, out versionPtr);
			this.m_RootPtr = rootPtr;
			this.m_VersionPtr = versionPtr;
			this.m_IsOwner = true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D20 File Offset: 0x00000F20
		private Hierarchy(IntPtr nativePtr, IntPtr rootPtr, IntPtr versionPtr)
		{
			this.m_Ptr = nativePtr;
			this.m_RootPtr = rootPtr;
			this.m_VersionPtr = versionPtr;
			this.m_IsOwner = false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D48 File Offset: 0x00000F48
		~Hierarchy()
		{
			this.Dispose(false);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D7C File Offset: 0x00000F7C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D90 File Offset: 0x00000F90
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					Hierarchy.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public HierarchyNodeTypeHandlerBaseEnumerable EnumerateNodeTypeHandlersBase()
		{
			return new HierarchyNodeTypeHandlerBaseEnumerable(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DE0 File Offset: 0x00000FE0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Exists(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.Exists_Injected(intPtr, in node);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E03 File Offset: 0x00001003
		public HierarchyNode Add(in HierarchyNode parent)
		{
			return this.AddNode(in parent);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E0C File Offset: 0x0000100C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool SetParent(in HierarchyNode node, in HierarchyNode parent)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.SetParent_Injected(intPtr, in node, in parent);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E30 File Offset: 0x00001030
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetParent(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.GetParent_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E58 File Offset: 0x00001058
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode[] GetChildren(in HierarchyNode node)
		{
			HierarchyNode[] array2;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Hierarchy.GetChildren_Injected(intPtr, in node, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				HierarchyNode[] array;
				blittableArrayWrapper.Unmarshal<HierarchyNode>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E9C File Offset: 0x0000109C
		public HierarchyNodeChildren EnumerateChildren(in HierarchyNode node)
		{
			return new HierarchyNodeChildren(this, this.EnumerateChildrenPtr(in node));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002EAC File Offset: 0x000010AC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002ED0 File Offset: 0x000010D0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void SetSortIndex(in HierarchyNode node, int sortIndex)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SetSortIndex_Injected(intPtr, in node, sortIndex);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002EF4 File Offset: 0x000010F4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void SortChildren(in HierarchyNode node, bool recurse = false)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SortChildren_Injected(intPtr, in node, recurse);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F18 File Offset: 0x00001118
		public HierarchyPropertyUnmanaged<T> GetOrCreatePropertyUnmanaged<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(string name, HierarchyPropertyStorageType type = HierarchyPropertyStorageType.Dense) where T : struct, ValueType
		{
			HierarchyPropertyDescriptor hierarchyPropertyDescriptor = default(HierarchyPropertyDescriptor);
			hierarchyPropertyDescriptor.Size = UnsafeUtility.SizeOf<T>();
			hierarchyPropertyDescriptor.Type = type;
			HierarchyPropertyId property = this.GetOrCreateProperty(name, in hierarchyPropertyDescriptor);
			return new HierarchyPropertyUnmanaged<T>(this, in property);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F5C File Offset: 0x0000115C
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.Update_Injected(intPtr);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F80 File Offset: 0x00001180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Hierarchy FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((Hierarchy)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000063 RID: 99
		[FreeFunction("HierarchyBindings::Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(IntPtr handlePtr, out IntPtr rootPtr, out IntPtr versionPtr);

		// Token: 0x06000064 RID: 100
		[FreeFunction("HierarchyBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x06000065 RID: 101 RVA: 0x00002FB0 File Offset: 0x000011B0
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlersBaseCount", HasExplicitThis = true, IsThreadSafe = true)]
		internal int GetNodeTypeHandlersBaseCount()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetNodeTypeHandlersBaseCount_Injected(intPtr);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FD4 File Offset: 0x000011D4
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlersBaseSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe int GetNodeTypeHandlersBaseSpan(Span<IntPtr> outHandlers)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<IntPtr> span = outHandlers;
			int nodeTypeHandlersBaseSpan_Injected;
			fixed (IntPtr* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodeTypeHandlersBaseSpan_Injected = Hierarchy.GetNodeTypeHandlersBaseSpan_Injected(intPtr, ref managedSpanWrapper);
			}
			return nodeTypeHandlersBaseSpan_Injected;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003018 File Offset: 0x00001218
		[FreeFunction("HierarchyBindings::AddNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private HierarchyNode AddNode(in HierarchyNode parent)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.AddNode_Injected(intPtr, in parent, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003040 File Offset: 0x00001240
		[FreeFunction("HierarchyBindings::EnumerateChildrenPtr", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private IntPtr EnumerateChildrenPtr(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.EnumerateChildrenPtr_Injected(intPtr, in node);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003064 File Offset: 0x00001264
		[FreeFunction("HierarchyBindings::GetOrCreateProperty", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe HierarchyPropertyId GetOrCreateProperty(string name, in HierarchyPropertyDescriptor descriptor)
		{
			HierarchyPropertyId hierarchyPropertyId2;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				HierarchyPropertyId hierarchyPropertyId;
				Hierarchy.GetOrCreateProperty_Injected(intPtr, ref managedSpanWrapper, in descriptor, out hierarchyPropertyId);
			}
			finally
			{
				char* ptr = null;
				HierarchyPropertyId hierarchyPropertyId;
				hierarchyPropertyId2 = hierarchyPropertyId;
			}
			return hierarchyPropertyId2;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030D0 File Offset: 0x000012D0
		[FreeFunction("HierarchyBindings::SetPropertyRaw", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe void SetPropertyRaw(in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SetPropertyRaw_Injected(intPtr, in property, in node, ptr, size);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030F8 File Offset: 0x000012F8
		[FreeFunction("HierarchyBindings::GetPropertyRaw", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe void* GetPropertyRaw(in HierarchyPropertyId property, in HierarchyNode node, out int size)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetPropertyRaw_Injected(intPtr, in property, in node, out size);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000311D File Offset: 0x0000131D
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchy(IntPtr nativePtr, IntPtr rootPtr, IntPtr versionPtr)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new Hierarchy(nativePtr, rootPtr, versionPtr)));
		}

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Exists_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetParent_Injected(IntPtr _unity_self, in HierarchyNode node, in HierarchyNode parent);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParent_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChildren_Injected(IntPtr _unity_self, in HierarchyNode node, out BlittableArrayWrapper ret);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSortIndex_Injected(IntPtr _unity_self, in HierarchyNode node, int sortIndex);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SortChildren_Injected(IntPtr _unity_self, in HierarchyNode node, bool recurse);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeTypeHandlersBaseCount_Injected(IntPtr _unity_self);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeTypeHandlersBaseSpan_Injected(IntPtr _unity_self, ref ManagedSpanWrapper outHandlers);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNode_Injected(IntPtr _unity_self, in HierarchyNode parent, out HierarchyNode ret);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr EnumerateChildrenPtr_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOrCreateProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, in HierarchyPropertyDescriptor descriptor, out HierarchyPropertyId ret);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetPropertyRaw_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* GetPropertyRaw_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, out int size);

		// Token: 0x0400002F RID: 47
		private IntPtr m_Ptr;

		// Token: 0x04000030 RID: 48
		private readonly IntPtr m_RootPtr;

		// Token: 0x04000031 RID: 49
		private readonly IntPtr m_VersionPtr;

		// Token: 0x04000032 RID: 50
		private readonly bool m_IsOwner;

		// Token: 0x02000017 RID: 23
		internal static class BindingsMarshaller
		{
			// Token: 0x0600007D RID: 125 RVA: 0x00003131 File Offset: 0x00001331
			public static IntPtr ConvertToNative(Hierarchy hierarchy)
			{
				return hierarchy.m_Ptr;
			}
		}
	}
}

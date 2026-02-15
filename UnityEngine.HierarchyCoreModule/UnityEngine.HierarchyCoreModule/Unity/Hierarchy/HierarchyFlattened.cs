using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/HierarchyFlattenedBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyFlattened.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyFlattened : IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000322E File Offset: 0x0000142E
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003240 File Offset: 0x00001440
		public int Count
		{
			get
			{
				return this.m_NodesCount;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003248 File Offset: 0x00001448
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyFlattened.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000326A File Offset: 0x0000146A
		public Hierarchy Hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003272 File Offset: 0x00001472
		internal unsafe HierarchyFlattenedNode* NodesPtr
		{
			get
			{
				return (HierarchyFlattenedNode*)(void*)this.m_NodesPtr;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000327F File Offset: 0x0000147F
		internal int Version
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003288 File Offset: 0x00001488
		public HierarchyFlattened(Hierarchy hierarchy)
		{
			IntPtr nodesPtr;
			int nodesCount;
			int version;
			this.m_Ptr = HierarchyFlattened.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), hierarchy, out nodesPtr, out nodesCount, out version);
			this.m_Hierarchy = hierarchy;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = true;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032DD File Offset: 0x000014DD
		private HierarchyFlattened(IntPtr nativePtr, Hierarchy hierarchy, IntPtr nodesPtr, int nodesCount, int version)
		{
			this.m_Ptr = nativePtr;
			this.m_Hierarchy = hierarchy;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003314 File Offset: 0x00001514
		~HierarchyFlattened()
		{
			this.Dispose(false);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003348 File Offset: 0x00001548
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000335C File Offset: 0x0000155C
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyFlattened.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000012 RID: 18
		public unsafe ref HierarchyFlattenedNode this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= this.m_NodesCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (byte*)(void*)this.m_NodesPtr + (IntPtr)index * (IntPtr)sizeof(HierarchyFlattenedNode);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033F0 File Offset: 0x000015F0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int IndexOf(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.IndexOf_Injected(intPtr, in node);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003414 File Offset: 0x00001614
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Contains(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.Contains_Injected(intPtr, in node);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003437 File Offset: 0x00001637
		public HierarchyFlattenedNodeChildren EnumerateChildren(in HierarchyNode node)
		{
			return new HierarchyFlattenedNodeChildren(this, in node);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003440 File Offset: 0x00001640
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003464 File Offset: 0x00001664
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyFlattened.Update_Injected(intPtr);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003486 File Offset: 0x00001686
		public HierarchyFlattened.Enumerator GetEnumerator()
		{
			return new HierarchyFlattened.Enumerator(this);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003490 File Offset: 0x00001690
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyFlattened FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyFlattened)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034C0 File Offset: 0x000016C0
		[FreeFunction("HierarchyFlattenedBindings::Create", IsThreadSafe = true)]
		private static IntPtr Create(IntPtr handlePtr, Hierarchy hierarchy, out IntPtr nodesPtr, out int nodesCount, out int version)
		{
			return HierarchyFlattened.Create_Injected(handlePtr, (hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), out nodesPtr, out nodesCount, out version);
		}

		// Token: 0x0600009A RID: 154
		[FreeFunction("HierarchyFlattenedBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x0600009B RID: 155 RVA: 0x000034E6 File Offset: 0x000016E6
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchyFlattened(IntPtr nativePtr, IntPtr hierarchyPtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyFlattened(nativePtr, Hierarchy.FromIntPtr(hierarchyPtr), nodesPtr, nodesCount, version)));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003504 File Offset: 0x00001704
		[RequiredByNativeCode]
		private static void UpdateHierarchyFlattened(IntPtr handlePtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			HierarchyFlattened hierarchyFlattened = HierarchyFlattened.FromIntPtr(handlePtr);
			hierarchyFlattened.m_NodesPtr = nodesPtr;
			hierarchyFlattened.m_NodesCount = nodesCount;
			hierarchyFlattened.m_Version = version;
		}

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IndexOf_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(IntPtr handlePtr, IntPtr hierarchy, out IntPtr nodesPtr, out int nodesCount, out int version);

		// Token: 0x04000035 RID: 53
		private IntPtr m_Ptr;

		// Token: 0x04000036 RID: 54
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000037 RID: 55
		private IntPtr m_NodesPtr;

		// Token: 0x04000038 RID: 56
		private int m_NodesCount;

		// Token: 0x04000039 RID: 57
		private int m_Version;

		// Token: 0x0400003A RID: 58
		private readonly bool m_IsOwner;

		// Token: 0x0200001B RID: 27
		internal static class BindingsMarshaller
		{
			// Token: 0x060000A3 RID: 163 RVA: 0x0000352E File Offset: 0x0000172E
			public static IntPtr ConvertToNative(HierarchyFlattened hierarchyFlattened)
			{
				return hierarchyFlattened.m_Ptr;
			}
		}

		// Token: 0x0200001C RID: 28
		public struct Enumerator
		{
			// Token: 0x060000A4 RID: 164 RVA: 0x00003536 File Offset: 0x00001736
			internal unsafe Enumerator(HierarchyFlattened hierarchyFlattened)
			{
				this.m_HierarchyFlattened = hierarchyFlattened;
				this.m_NodesPtr = (HierarchyFlattenedNode*)(void*)hierarchyFlattened.m_NodesPtr;
				this.m_NodesCount = hierarchyFlattened.m_NodesCount;
				this.m_Version = hierarchyFlattened.Version;
				this.m_Index = -1;
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003570 File Offset: 0x00001770
			public readonly ref HierarchyFlattenedNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					bool flag = this.m_Version != this.m_HierarchyFlattened.m_Version;
					if (flag)
					{
						throw new InvalidOperationException("HierarchyFlattened was modified.");
					}
					return this.m_NodesPtr + this.m_Index;
				}
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x000035BC File Offset: 0x000017BC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_NodesCount;
			}

			// Token: 0x0400003B RID: 59
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x0400003C RID: 60
			private unsafe readonly HierarchyFlattenedNode* m_NodesPtr;

			// Token: 0x0400003D RID: 61
			private readonly int m_NodesCount;

			// Token: 0x0400003E RID: 62
			private readonly int m_Version;

			// Token: 0x0400003F RID: 63
			private int m_Index;
		}
	}
}

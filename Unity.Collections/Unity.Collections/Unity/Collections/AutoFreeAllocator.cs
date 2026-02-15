using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;

namespace Unity.Collections
{
	// Token: 0x02000034 RID: 52
	[BurstCompile]
	internal struct AutoFreeAllocator : AllocatorManager.IAllocator, IDisposable
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003EA4 File Offset: 0x000020A4
		public unsafe void Update()
		{
			int i = this.m_tofree.Length;
			while (i-- > 0)
			{
				int j = this.m_allocated.Length;
				while (j-- > 0)
				{
					if (*this.m_allocated[j] == *this.m_tofree[i])
					{
						Memory.Unmanaged.Free((void*)(*this.m_tofree[i]), this.m_backingAllocatorHandle);
						this.m_allocated.RemoveAtSwapBack(j);
						break;
					}
				}
			}
			this.m_tofree.Rewind();
			this.m_allocated.TrimExcess();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003F3F File Offset: 0x0000213F
		public void Initialize(AllocatorManager.AllocatorHandle backingAllocatorHandle)
		{
			this.m_allocated = new ArrayOfArrays<IntPtr>(1048576, backingAllocatorHandle, 12);
			this.m_tofree = new ArrayOfArrays<IntPtr>(131072, backingAllocatorHandle, 12);
			this.m_backingAllocatorHandle = backingAllocatorHandle;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003F70 File Offset: 0x00002170
		public unsafe void FreeAll()
		{
			this.Update();
			this.m_handle.Rewind();
			for (int i = 0; i < this.m_allocated.Length; i++)
			{
				Memory.Unmanaged.Free((void*)(*this.m_allocated[i]), this.m_backingAllocatorHandle);
			}
			this.m_allocated.Rewind();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003FCC File Offset: 0x000021CC
		public void Dispose()
		{
			this.FreeAll();
			this.m_tofree.Dispose();
			this.m_allocated.Dispose();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003FEA File Offset: 0x000021EA
		public AllocatorManager.TryFunction Function
		{
			get
			{
				return new AllocatorManager.TryFunction(AutoFreeAllocator.Try);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003FF8 File Offset: 0x000021F8
		public unsafe int Try(ref AllocatorManager.Block block)
		{
			if (block.Range.Pointer == IntPtr.Zero)
			{
				if (block.Bytes == 0L)
				{
					return 0;
				}
				byte* ptr = (byte*)Memory.Unmanaged.Allocate(block.Bytes, block.Alignment, this.m_backingAllocatorHandle);
				block.Range.Pointer = (IntPtr)((void*)ptr);
				block.AllocatedItems = block.Range.Items;
				this.m_allocated.LockfreeAdd(block.Range.Pointer);
				return 0;
			}
			else
			{
				if (block.Range.Items == 0)
				{
					this.m_tofree.LockfreeAdd(block.Range.Pointer);
					block.Range.Pointer = IntPtr.Zero;
					block.AllocatedItems = 0;
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000040B5 File Offset: 0x000022B5
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		internal static int Try(IntPtr state, ref AllocatorManager.Block block)
		{
			return AutoFreeAllocator.Try_000000E3$BurstDirectCall.Invoke(state, ref block);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000040BE File Offset: 0x000022BE
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000040C6 File Offset: 0x000022C6
		public AllocatorManager.AllocatorHandle Handle
		{
			get
			{
				return this.m_handle;
			}
			set
			{
				this.m_handle = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000040CF File Offset: 0x000022CF
		public Allocator ToAllocator
		{
			get
			{
				return this.m_handle.ToAllocator;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000040DC File Offset: 0x000022DC
		public bool IsCustomAllocator
		{
			get
			{
				return this.m_handle.IsCustomAllocator;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000040E9 File Offset: 0x000022E9
		public bool IsAutoDispose
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000040EC File Offset: 0x000022EC
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int Try$BurstManaged(IntPtr state, ref AllocatorManager.Block block)
		{
			return ((AutoFreeAllocator*)(void*)state)->Try(ref block);
		}

		// Token: 0x0400007D RID: 125
		private ArrayOfArrays<IntPtr> m_allocated;

		// Token: 0x0400007E RID: 126
		private ArrayOfArrays<IntPtr> m_tofree;

		// Token: 0x0400007F RID: 127
		private AllocatorManager.AllocatorHandle m_handle;

		// Token: 0x04000080 RID: 128
		private AllocatorManager.AllocatorHandle m_backingAllocatorHandle;

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x060000FB RID: 251
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int Try_000000E3$PostfixBurstDelegate(IntPtr state, ref AllocatorManager.Block block);

		// Token: 0x02000036 RID: 54
		internal static class Try_000000E3$BurstDirectCall
		{
			// Token: 0x060000FE RID: 254 RVA: 0x000040FC File Offset: 0x000022FC
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (AutoFreeAllocator.Try_000000E3$BurstDirectCall.Pointer == 0)
				{
					AutoFreeAllocator.Try_000000E3$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<AutoFreeAllocator.Try_000000E3$PostfixBurstDelegate>(new AutoFreeAllocator.Try_000000E3$PostfixBurstDelegate(AutoFreeAllocator.Try)).Value;
				}
				A_0 = AutoFreeAllocator.Try_000000E3$BurstDirectCall.Pointer;
			}

			// Token: 0x060000FF RID: 255 RVA: 0x0000413C File Offset: 0x0000233C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				AutoFreeAllocator.Try_000000E3$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00004154 File Offset: 0x00002354
			public static int Invoke(IntPtr state, ref AllocatorManager.Block block)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = AutoFreeAllocator.Try_000000E3$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), state, ref block, functionPointer);
					}
				}
				return AutoFreeAllocator.Try$BurstManaged(state, ref block);
			}

			// Token: 0x04000081 RID: 129
			private static IntPtr Pointer;
		}
	}
}

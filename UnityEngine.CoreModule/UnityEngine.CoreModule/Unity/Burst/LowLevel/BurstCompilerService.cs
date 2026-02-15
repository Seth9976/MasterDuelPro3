using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Burst.LowLevel
{
	// Token: 0x02000079 RID: 121
	[NativeHeader("Runtime/Burst/BurstDelegateCache.h")]
	[NativeHeader("Runtime/Burst/Burst.h")]
	[StaticAccessor("BurstCompilerService::Get()", StaticAccessorType.Arrow)]
	internal static class BurstCompilerService
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00004790 File Offset: 0x00002990
		[ThreadSafe]
		public unsafe static string GetDisassembly(MethodInfo m, string compilerOptions)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(compilerOptions, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = compilerOptions.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				BurstCompilerService.GetDisassembly_Injected(m, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000047F4 File Offset: 0x000029F4
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static int CompileAsyncDelegateMethod(object delegateMethod, string compilerOptions)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(compilerOptions, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = compilerOptions.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = BurstCompilerService.CompileAsyncDelegateMethod_Injected(delegateMethod, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000177 RID: 375
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetAsyncCompiledAsyncDelegateMethod(int userID);

		// Token: 0x06000178 RID: 376
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetOrCreateSharedMemory(ref Hash128 key, uint size_of, uint alignment);

		// Token: 0x06000179 RID: 377
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCurrentExecutionMode(uint environment);

		// Token: 0x0600017A RID: 378
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCurrentExecutionMode();

		// Token: 0x0600017B RID: 379
		[FreeFunction("DefaultBurstLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Log(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x0600017C RID: 380
		[FreeFunction("DefaultBurstRuntimeLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void RuntimeLog(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x0600017D RID: 381 RVA: 0x0000484C File Offset: 0x00002A4C
		public unsafe static bool LoadBurstLibrary(string fullPathToLibBurstGenerated)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fullPathToLibBurstGenerated, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fullPathToLibBurstGenerated.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = BurstCompilerService.LoadBurstLibrary_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600017E RID: 382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDisassembly_Injected(MethodInfo m, ref ManagedSpanWrapper compilerOptions, out ManagedSpanWrapper ret);

		// Token: 0x0600017F RID: 383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CompileAsyncDelegateMethod_Injected(object delegateMethod, ref ManagedSpanWrapper compilerOptions);

		// Token: 0x06000180 RID: 384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadBurstLibrary_Injected(ref ManagedSpanWrapper fullPathToLibBurstGenerated);

		// Token: 0x0200007A RID: 122
		public enum BurstLogType
		{
			// Token: 0x04000115 RID: 277
			Info,
			// Token: 0x04000116 RID: 278
			Warning,
			// Token: 0x04000117 RID: 279
			Error
		}
	}
}

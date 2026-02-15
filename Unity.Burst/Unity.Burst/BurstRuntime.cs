using System;
using System.Diagnostics;
using Unity.Burst.LowLevel;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000017 RID: 23
	public static class BurstRuntime
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002FD0 File Offset: 0x000011D0
		public static int GetHashCode32<T>()
		{
			return BurstRuntime.HashCode32<T>.Value;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FD7 File Offset: 0x000011D7
		public static int GetHashCode32(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A32(type.AssemblyQualifiedName);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002FEB File Offset: 0x000011EB
		public static long GetHashCode64(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A64(type.AssemblyQualifiedName);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal static int HashStringWithFNV1A32(string text)
		{
			uint result = 2166136261U;
			foreach (char c in text)
			{
				result = 16777619U * (result ^ (uint)((byte)(c & 'ÿ')));
				result = 16777619U * (result ^ (uint)((byte)(c >> 8)));
			}
			return (int)result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003048 File Offset: 0x00001248
		internal static long HashStringWithFNV1A64(string text)
		{
			ulong result = 14695981039346656037UL;
			foreach (char c in text)
			{
				result = 1099511628211UL * (result ^ (ulong)((byte)(c & 'ÿ')));
				result = 1099511628211UL * (result ^ (ulong)((byte)(c >> 8)));
			}
			return (long)result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000030A3 File Offset: 0x000012A3
		public static bool LoadAdditionalLibrary(string pathToLibBurstGenerated)
		{
			return BurstCompiler.IsLoadAdditionalLibrarySupported() && BurstRuntime.LoadAdditionalLibraryInternal(pathToLibBurstGenerated);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030B4 File Offset: 0x000012B4
		internal static bool LoadAdditionalLibraryInternal(string pathToLibBurstGenerated)
		{
			return (bool)typeof(BurstCompilerService).GetMethod("LoadBurstLibrary").Invoke(null, new object[] { pathToLibBurstGenerated });
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030DF File Offset: 0x000012DF
		[BurstRuntime.PreserveAttribute]
		internal unsafe static void RuntimeLog(byte* message, int logType, byte* fileName, int lineNumber)
		{
			BurstCompilerService.RuntimeLog(null, (BurstCompilerService.BurstLogType)logType, message, fileName, lineNumber);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Initialize()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030EC File Offset: 0x000012EC
		[BurstRuntime.PreserveAttribute]
		internal static void PreventRequiredAttributeStrip()
		{
			new BurstDiscardAttribute();
			new ConditionalAttribute("HEJSA");
			new JobProducerTypeAttribute(typeof(BurstRuntime));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000310F File Offset: 0x0000130F
		[BurstRuntime.PreserveAttribute]
		internal unsafe static void Log(byte* message, int logType, byte* fileName, int lineNumber)
		{
			BurstCompilerService.Log(null, (BurstCompilerService.BurstLogType)logType, message, null, lineNumber);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000311D File Offset: 0x0000131D
		public unsafe static byte* GetUTF8LiteralPointer(string str, out int byteCount)
		{
			throw new NotImplementedException("This function only works from Burst");
		}

		// Token: 0x02000018 RID: 24
		private struct HashCode32<T>
		{
			// Token: 0x040000C6 RID: 198
			public static readonly int Value = BurstRuntime.HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000019 RID: 25
		private struct HashCode64<T>
		{
			// Token: 0x040000C7 RID: 199
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x0200001A RID: 26
		internal class PreserveAttribute : Attribute
		{
		}
	}
}

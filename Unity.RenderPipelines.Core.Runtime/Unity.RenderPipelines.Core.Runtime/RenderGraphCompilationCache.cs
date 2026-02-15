using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler;

// Token: 0x02000004 RID: 4
internal class RenderGraphCompilationCache
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	private static int HashEntryComparer<T>(RenderGraphCompilationCache.HashEntry<T> a, RenderGraphCompilationCache.HashEntry<T> b)
	{
		if (a.lastFrameUsed < b.lastFrameUsed)
		{
			return -1;
		}
		if (a.lastFrameUsed > b.lastFrameUsed)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
	public RenderGraphCompilationCache()
	{
		for (int i = 0; i < 20; i++)
		{
			this.m_CompiledGraphPool.Push(new RenderGraph.CompiledGraph());
			this.m_NativeCompiledGraphPool.Push(new CompilerContextData(100));
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
	private bool GetCompilationCache<T>(int hash, int frameIndex, out T outGraph, DynamicArray<RenderGraphCompilationCache.HashEntry<T>> hashEntries, Stack<T> pool, DynamicArray<RenderGraphCompilationCache.HashEntry<T>>.SortComparer comparer) where T : RenderGraph.ICompiledGraph
	{
		RenderGraphCompilationCache.s_Hash = hash;
		int index = hashEntries.FindIndex(0, hashEntries.size, (RenderGraphCompilationCache.HashEntry<T> value) => value.hash == RenderGraphCompilationCache.s_Hash);
		if (index != -1)
		{
			ref RenderGraphCompilationCache.HashEntry<T> entry = ref hashEntries[index];
			outGraph = entry.compiledGraph;
			entry.lastFrameUsed = frameIndex;
			return true;
		}
		if (pool.Count != 0)
		{
			RenderGraphCompilationCache.HashEntry<T> newEntry = new RenderGraphCompilationCache.HashEntry<T>
			{
				hash = hash,
				lastFrameUsed = frameIndex,
				compiledGraph = pool.Pop()
			};
			hashEntries.Add(in newEntry);
			outGraph = newEntry.compiledGraph;
			return false;
		}
		hashEntries.QuickSort(comparer);
		ref RenderGraphCompilationCache.HashEntry<T> oldestEntry = ref hashEntries[0];
		oldestEntry.hash = hash;
		oldestEntry.lastFrameUsed = frameIndex;
		oldestEntry.compiledGraph.Clear();
		outGraph = oldestEntry.compiledGraph;
		return false;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021DC File Offset: 0x000003DC
	public bool GetCompilationCache(int hash, int frameIndex, out RenderGraph.CompiledGraph outGraph)
	{
		return this.GetCompilationCache<RenderGraph.CompiledGraph>(hash, frameIndex, out outGraph, this.m_HashEntries, this.m_CompiledGraphPool, RenderGraphCompilationCache.s_EntryComparer);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021F8 File Offset: 0x000003F8
	public bool GetCompilationCache(int hash, int frameIndex, out CompilerContextData outGraph)
	{
		return this.GetCompilationCache<CompilerContextData>(hash, frameIndex, out outGraph, this.m_NativeHashEntries, this.m_NativeCompiledGraphPool, RenderGraphCompilationCache.s_NativeEntryComparer);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002214 File Offset: 0x00000414
	public void Clear()
	{
		for (int i = 0; i < this.m_HashEntries.size; i++)
		{
			this.m_CompiledGraphPool.Push(this.m_HashEntries[i].compiledGraph);
		}
		this.m_HashEntries.Clear();
		for (int j = 0; j < this.m_NativeHashEntries.size; j++)
		{
			this.m_NativeCompiledGraphPool.Push(this.m_NativeHashEntries[j].compiledGraph);
		}
		this.m_NativeHashEntries.Clear();
	}

	// Token: 0x04000001 RID: 1
	private DynamicArray<RenderGraphCompilationCache.HashEntry<RenderGraph.CompiledGraph>> m_HashEntries = new DynamicArray<RenderGraphCompilationCache.HashEntry<RenderGraph.CompiledGraph>>();

	// Token: 0x04000002 RID: 2
	private DynamicArray<RenderGraphCompilationCache.HashEntry<CompilerContextData>> m_NativeHashEntries = new DynamicArray<RenderGraphCompilationCache.HashEntry<CompilerContextData>>();

	// Token: 0x04000003 RID: 3
	private Stack<RenderGraph.CompiledGraph> m_CompiledGraphPool = new Stack<RenderGraph.CompiledGraph>();

	// Token: 0x04000004 RID: 4
	private Stack<CompilerContextData> m_NativeCompiledGraphPool = new Stack<CompilerContextData>();

	// Token: 0x04000005 RID: 5
	private static DynamicArray<RenderGraphCompilationCache.HashEntry<RenderGraph.CompiledGraph>>.SortComparer s_EntryComparer = new DynamicArray<RenderGraphCompilationCache.HashEntry<RenderGraph.CompiledGraph>>.SortComparer(RenderGraphCompilationCache.HashEntryComparer<RenderGraph.CompiledGraph>);

	// Token: 0x04000006 RID: 6
	private static DynamicArray<RenderGraphCompilationCache.HashEntry<CompilerContextData>>.SortComparer s_NativeEntryComparer = new DynamicArray<RenderGraphCompilationCache.HashEntry<CompilerContextData>>.SortComparer(RenderGraphCompilationCache.HashEntryComparer<CompilerContextData>);

	// Token: 0x04000007 RID: 7
	private const int k_CachedGraphCount = 20;

	// Token: 0x04000008 RID: 8
	private static int s_Hash;

	// Token: 0x02000005 RID: 5
	private struct HashEntry<T>
	{
		// Token: 0x04000009 RID: 9
		public int hash;

		// Token: 0x0400000A RID: 10
		public int lastFrameUsed;

		// Token: 0x0400000B RID: 11
		public T compiledGraph;
	}
}

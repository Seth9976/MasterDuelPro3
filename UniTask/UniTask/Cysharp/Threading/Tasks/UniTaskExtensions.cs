using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000127 RID: 295
	public static class UniTaskExtensions
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x00021354 File Offset: 0x0001F554
		public static UniTask.Awaiter GetAwaiter(this UniTask[] tasks)
		{
			return UniTask.WhenAll(tasks).GetAwaiter();
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00021370 File Offset: 0x0001F570
		public static UniTask.Awaiter GetAwaiter(this IEnumerable<UniTask> tasks)
		{
			return UniTask.WhenAll(tasks).GetAwaiter();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002138C File Offset: 0x0001F58C
		public static UniTask<T[]>.Awaiter GetAwaiter<T>(this UniTask<T>[] tasks)
		{
			return UniTask.WhenAll<T>(tasks).GetAwaiter();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000213A8 File Offset: 0x0001F5A8
		public static UniTask<T[]>.Awaiter GetAwaiter<T>(this IEnumerable<UniTask<T>> tasks)
		{
			return UniTask.WhenAll<T>(tasks).GetAwaiter();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000213C4 File Offset: 0x0001F5C4
		public static UniTask<ValueTuple<T1, T2>>.Awaiter GetAwaiter<T1, T2>([TupleElementNames(new string[] { "task1", "task2" })] this ValueTuple<UniTask<T1>, UniTask<T2>> tasks)
		{
			return UniTask.WhenAll<T1, T2>(tasks.Item1, tasks.Item2).GetAwaiter();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000213EC File Offset: 0x0001F5EC
		public static UniTask<ValueTuple<T1, T2, T3>>.Awaiter GetAwaiter<T1, T2, T3>([TupleElementNames(new string[] { "task1", "task2", "task3" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3>(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00021418 File Offset: 0x0001F618
		public static UniTask<ValueTuple<T1, T2, T3, T4>>.Awaiter GetAwaiter<T1, T2, T3, T4>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4).GetAwaiter();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002144C File Offset: 0x0001F64C
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5).GetAwaiter();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00021484 File Offset: 0x0001F684
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6).GetAwaiter();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000214C4 File Offset: 0x0001F6C4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7).GetAwaiter();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00021508 File Offset: 0x0001F708
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", null })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1).GetAwaiter();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00021558 File Offset: 0x0001F758
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", null,
			null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2).GetAwaiter();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000215B4 File Offset: 0x0001F7B4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3).GetAwaiter();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002161C File Offset: 0x0001F81C
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4).GetAwaiter();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0002168C File Offset: 0x0001F88C
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", null, null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5).GetAwaiter();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00021708 File Offset: 0x0001F908
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", null, null, null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6).GetAwaiter();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00021790 File Offset: 0x0001F990
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", null, null, null, null, null, null,
			null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>, UniTask<T14>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6, tasks.Rest.Item7).GetAwaiter();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00021824 File Offset: 0x0001FA24
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", "task15", null, null, null, null, null,
			null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>, UniTask<T14>, ValueTuple<UniTask<T15>>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6, tasks.Rest.Item7, tasks.Rest.Rest.Item1).GetAwaiter();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000218C8 File Offset: 0x0001FAC8
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2" })] this ValueTuple<UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2 }).GetAwaiter();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00021904 File Offset: 0x0001FB04
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3" })] this ValueTuple<UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3 }).GetAwaiter();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002194C File Offset: 0x0001FB4C
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4 }).GetAwaiter();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000219A0 File Offset: 0x0001FBA0
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5 }).GetAwaiter();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00021A04 File Offset: 0x0001FC04
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6 }).GetAwaiter();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00021A74 File Offset: 0x0001FC74
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7 }).GetAwaiter();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", null })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1
			}).GetAwaiter();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00021B80 File Offset: 0x0001FD80
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", null,
			null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2
			}).GetAwaiter();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00021C20 File Offset: 0x0001FE20
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3
			}).GetAwaiter();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4
			}).GetAwaiter();
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00021D9C File Offset: 0x0001FF9C
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", null, null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5
			}).GetAwaiter();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00021E78 File Offset: 0x00020078
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", null, null, null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6
			}).GetAwaiter();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00021F64 File Offset: 0x00020164
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", null, null, null, null, null, null,
			null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6,
				tasks.Rest.Item7
			}).GetAwaiter();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00022064 File Offset: 0x00020264
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", "task15", null, null, null, null, null,
			null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask>>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6,
				tasks.Rest.Item7,
				tasks.Rest.Rest.Item1
			}).GetAwaiter();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002217C File Offset: 0x0002037C
		public static UniTask<T> AsUniTask<T>(this Task<T> task, bool useCurrentSynchronizationContext = true)
		{
			UniTaskCompletionSource<T> promise = new UniTaskCompletionSource<T>();
			task.ContinueWith(delegate(Task<T> x, object state)
			{
				UniTaskCompletionSource<T> p = (UniTaskCompletionSource<T>)state;
				switch (x.Status)
				{
				case TaskStatus.RanToCompletion:
					p.TrySetResult(x.Result);
					return;
				case TaskStatus.Canceled:
					p.TrySetCanceled(default(CancellationToken));
					return;
				case TaskStatus.Faulted:
					p.TrySetException(x.Exception.InnerException ?? x.Exception);
					return;
				default:
					throw new NotSupportedException();
				}
			}, promise, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);
			return promise.Task;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000221CC File Offset: 0x000203CC
		public static UniTask AsUniTask(this Task task, bool useCurrentSynchronizationContext = true)
		{
			UniTaskCompletionSource promise = new UniTaskCompletionSource();
			task.ContinueWith(delegate(Task x, object state)
			{
				UniTaskCompletionSource p = (UniTaskCompletionSource)state;
				switch (x.Status)
				{
				case TaskStatus.RanToCompletion:
					p.TrySetResult();
					return;
				case TaskStatus.Canceled:
					p.TrySetCanceled(default(CancellationToken));
					return;
				case TaskStatus.Faulted:
					p.TrySetException(x.Exception.InnerException ?? x.Exception);
					return;
				default:
					throw new NotSupportedException();
				}
			}, promise, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);
			return promise.Task;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002221C File Offset: 0x0002041C
		public static Task<T> AsTask<T>(this UniTask<T> task)
		{
			Task<T> task2;
			try
			{
				UniTask<T>.Awaiter awaiter;
				try
				{
					awaiter = task.GetAwaiter();
				}
				catch (Exception ex)
				{
					return Task.FromException<T>(ex);
				}
				if (awaiter.IsCompleted)
				{
					try
					{
						return Task.FromResult<T>(awaiter.GetResult());
					}
					catch (Exception ex2)
					{
						return Task.FromException<T>(ex2);
					}
				}
				TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
				awaiter.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<TaskCompletionSource<T>, UniTask<T>.Awaiter> tuple = (StateTuple<TaskCompletionSource<T>, UniTask<T>.Awaiter>)state)
					{
						TaskCompletionSource<T> taskCompletionSource;
						UniTask<T>.Awaiter awaiter2;
						tuple.Deconstruct(out taskCompletionSource, out awaiter2);
						TaskCompletionSource<T> inTcs = taskCompletionSource;
						UniTask<T>.Awaiter inAwaiter = awaiter2;
						try
						{
							T result = inAwaiter.GetResult();
							inTcs.SetResult(result);
						}
						catch (Exception ex4)
						{
							inTcs.SetException(ex4);
						}
					}
				}, StateTuple.Create<TaskCompletionSource<T>, UniTask<T>.Awaiter>(tcs, awaiter));
				task2 = tcs.Task;
			}
			catch (Exception ex3)
			{
				task2 = Task.FromException<T>(ex3);
			}
			return task2;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000222C8 File Offset: 0x000204C8
		public static Task AsTask(this UniTask task)
		{
			Task task2;
			try
			{
				UniTask.Awaiter awaiter;
				try
				{
					awaiter = task.GetAwaiter();
				}
				catch (Exception ex)
				{
					return Task.FromException(ex);
				}
				if (awaiter.IsCompleted)
				{
					try
					{
						awaiter.GetResult();
						return Task.CompletedTask;
					}
					catch (Exception ex2)
					{
						return Task.FromException(ex2);
					}
				}
				TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
				awaiter.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<TaskCompletionSource<object>, UniTask.Awaiter> tuple = (StateTuple<TaskCompletionSource<object>, UniTask.Awaiter>)state)
					{
						TaskCompletionSource<object> taskCompletionSource;
						UniTask.Awaiter awaiter2;
						tuple.Deconstruct(out taskCompletionSource, out awaiter2);
						TaskCompletionSource<object> inTcs = taskCompletionSource;
						UniTask.Awaiter inAwaiter = awaiter2;
						try
						{
							inAwaiter.GetResult();
							inTcs.SetResult(null);
						}
						catch (Exception ex4)
						{
							inTcs.SetException(ex4);
						}
					}
				}, StateTuple.Create<TaskCompletionSource<object>, UniTask.Awaiter>(tcs, awaiter));
				task2 = tcs.Task;
			}
			catch (Exception ex3)
			{
				task2 = Task.FromException(ex3);
			}
			return task2;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00022374 File Offset: 0x00020574
		public static AsyncLazy ToAsyncLazy(this UniTask task)
		{
			return new AsyncLazy(task);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0002237C File Offset: 0x0002057C
		public static AsyncLazy<T> ToAsyncLazy<T>(this UniTask<T> task)
		{
			return new AsyncLazy<T>(task);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00022384 File Offset: 0x00020584
		public static UniTask AttachExternalCancellation(this UniTask task, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled)
			{
				return task;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				task.Forget();
				return UniTask.FromCanceled(cancellationToken);
			}
			if (task.Status.IsCompleted())
			{
				return task;
			}
			return new UniTask(new UniTaskExtensions.AttachExternalCancellationSource(task, cancellationToken), 0);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000223C4 File Offset: 0x000205C4
		public static UniTask<T> AttachExternalCancellation<T>(this UniTask<T> task, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled)
			{
				return task;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				task.Forget<T>();
				return UniTask.FromCanceled<T>(cancellationToken);
			}
			if (task.Status.IsCompleted())
			{
				return task;
			}
			return new UniTask<T>(new UniTaskExtensions.AttachExternalCancellationSource<T>(task, cancellationToken), 0);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00022404 File Offset: 0x00020604
		public static IEnumerator ToCoroutine<T>(this UniTask<T> task, Action<T> resultHandler = null, Action<Exception> exceptionHandler = null)
		{
			return new UniTaskExtensions.ToCoroutineEnumerator<T>(task, resultHandler, exceptionHandler);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002240E File Offset: 0x0002060E
		public static IEnumerator ToCoroutine(this UniTask task, Action<Exception> exceptionHandler = null)
		{
			return new UniTaskExtensions.ToCoroutineEnumerator(task, exceptionHandler);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00022418 File Offset: 0x00020618
		public static async UniTask Timeout(this UniTask task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> timeoutTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int winArgIndex;
			bool taskResultIsCanceled;
			try
			{
				object obj = await UniTask.WhenAny<bool, bool>(task.SuppressCancellationThrow(), timeoutTask);
				winArgIndex = obj.Item1;
				taskResultIsCanceled = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				throw;
			}
			if (winArgIndex == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				string text = "Exceed Timeout:";
				TimeSpan timeSpan = timeout;
				throw new TimeoutException(text + timeSpan.ToString());
			}
			delayCancellationTokenSource.Cancel();
			delayCancellationTokenSource.Dispose();
			if (taskResultIsCanceled)
			{
				Error.ThrowOperationCanceledException();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002247C File Offset: 0x0002067C
		public static async UniTask<T> Timeout<T>(this UniTask<T> task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> timeoutTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int winArgIndex;
			ValueTuple<bool, T> taskResult;
			try
			{
				object obj = await UniTask.WhenAny<ValueTuple<bool, T>, bool>(task.SuppressCancellationThrow(), timeoutTask);
				winArgIndex = obj.Item1;
				taskResult = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				throw;
			}
			if (winArgIndex == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				string text = "Exceed Timeout:";
				TimeSpan timeSpan = timeout;
				throw new TimeoutException(text + timeSpan.ToString());
			}
			delayCancellationTokenSource.Cancel();
			delayCancellationTokenSource.Dispose();
			if (taskResult.Item1)
			{
				Error.ThrowOperationCanceledException();
			}
			return taskResult.Item2;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000224E0 File Offset: 0x000206E0
		public static async UniTask<bool> TimeoutWithoutException(this UniTask task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> timeoutTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int winArgIndex;
			bool taskResultIsCanceled;
			try
			{
				object obj = await UniTask.WhenAny<bool, bool>(task.SuppressCancellationThrow(), timeoutTask);
				winArgIndex = obj.Item1;
				taskResultIsCanceled = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				return 1;
			}
			bool flag;
			if (winArgIndex == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				flag = true;
			}
			else
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				if (taskResultIsCanceled)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00022544 File Offset: 0x00020744
		[return: TupleElementNames(new string[] { "IsTimeout", "Result" })]
		public static async UniTask<ValueTuple<bool, T>> TimeoutWithoutException<T>(this UniTask<T> task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> timeoutTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int winArgIndex;
			ValueTuple<bool, T> taskResult;
			try
			{
				object obj = await UniTask.WhenAny<ValueTuple<bool, T>, bool>(task.SuppressCancellationThrow(), timeoutTask);
				winArgIndex = obj.Item1;
				taskResult = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				return new ValueTuple<bool, T>(true, default(T));
			}
			ValueTuple<bool, T> valueTuple;
			if (winArgIndex == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				valueTuple = new ValueTuple<bool, T>(true, default(T));
			}
			else
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				if (taskResult.Item1)
				{
					valueTuple = new ValueTuple<bool, T>(true, default(T));
				}
				else
				{
					valueTuple = new ValueTuple<bool, T>(false, taskResult.Item2);
				}
			}
			return valueTuple;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000225A8 File Offset: 0x000207A8
		public static void Forget(this UniTask task)
		{
			UniTask.Awaiter awaiter = task.GetAwaiter();
			if (awaiter.IsCompleted)
			{
				try
				{
					awaiter.GetResult();
					return;
				}
				catch (Exception ex)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex);
					return;
				}
			}
			awaiter.SourceOnCompleted(delegate(object state)
			{
				using (StateTuple<UniTask.Awaiter> t = (StateTuple<UniTask.Awaiter>)state)
				{
					try
					{
						t.Item1.GetResult();
					}
					catch (Exception ex2)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
					}
				}
			}, StateTuple.Create<UniTask.Awaiter>(awaiter));
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00022614 File Offset: 0x00020814
		public static void Forget(this UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
		{
			if (exceptionHandler == null)
			{
				task.Forget();
				return;
			}
			UniTaskExtensions.ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002263C File Offset: 0x0002083C
		private static async UniTaskVoid ForgetCoreWithCatch(UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
		{
			int num = 0;
			try
			{
				await task;
			}
			catch (Exception obj)
			{
				num = 1;
			}
			object obj;
			if (num == 1)
			{
				Exception ex = (Exception)obj;
				try
				{
					if (handleExceptionOnMainThread)
					{
						await UniTask.SwitchToMainThread(default(CancellationToken));
					}
					exceptionHandler(ex);
				}
				catch (Exception ex2)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex2);
				}
				ex = null;
			}
			obj = null;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00022690 File Offset: 0x00020890
		public static void Forget<T>(this UniTask<T> task)
		{
			UniTask<T>.Awaiter awaiter = task.GetAwaiter();
			if (awaiter.IsCompleted)
			{
				try
				{
					awaiter.GetResult();
					return;
				}
				catch (Exception ex)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex);
					return;
				}
			}
			awaiter.SourceOnCompleted(delegate(object state)
			{
				using (StateTuple<UniTask<T>.Awaiter> t = (StateTuple<UniTask<T>.Awaiter>)state)
				{
					try
					{
						t.Item1.GetResult();
					}
					catch (Exception ex2)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
					}
				}
			}, StateTuple.Create<UniTask<T>.Awaiter>(awaiter));
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000226FC File Offset: 0x000208FC
		public static void Forget<T>(this UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
		{
			if (exceptionHandler == null)
			{
				task.Forget<T>();
				return;
			}
			UniTaskExtensions.ForgetCoreWithCatch<T>(task, exceptionHandler, handleExceptionOnMainThread).Forget();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00022724 File Offset: 0x00020924
		private static async UniTaskVoid ForgetCoreWithCatch<T>(UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
		{
			int num = 0;
			try
			{
				await task;
			}
			catch (Exception obj)
			{
				num = 1;
			}
			object obj;
			if (num == 1)
			{
				Exception ex = (Exception)obj;
				try
				{
					if (handleExceptionOnMainThread)
					{
						await UniTask.SwitchToMainThread(default(CancellationToken));
					}
					exceptionHandler(ex);
				}
				catch (Exception ex2)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex2);
				}
				ex = null;
			}
			obj = null;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00022778 File Offset: 0x00020978
		public static async UniTask ContinueWith<T>(this UniTask<T> task, Action<T> continuationFunction)
		{
			Action<T> action = continuationFunction;
			T t = await task;
			action(t);
			action = null;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000227C4 File Offset: 0x000209C4
		public static async UniTask ContinueWith<T>(this UniTask<T> task, Func<T, UniTask> continuationFunction)
		{
			Func<T, UniTask> func = continuationFunction;
			T t = await task;
			await func(t);
			func = null;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00022810 File Offset: 0x00020A10
		public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, TR> continuationFunction)
		{
			T t = await task;
			return continuationFunction(t);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002285C File Offset: 0x00020A5C
		public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, UniTask<TR>> continuationFunction)
		{
			Func<T, UniTask<TR>> func = continuationFunction;
			T t = await task;
			UniTask<TR> uniTask = await func(t);
			func = null;
			return uniTask;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000228A8 File Offset: 0x00020AA8
		public static async UniTask ContinueWith(this UniTask task, Action continuationFunction)
		{
			await task;
			continuationFunction();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000228F4 File Offset: 0x00020AF4
		public static async UniTask ContinueWith(this UniTask task, Func<UniTask> continuationFunction)
		{
			await task;
			await continuationFunction();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00022940 File Offset: 0x00020B40
		public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<T> continuationFunction)
		{
			await task;
			return continuationFunction();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002298C File Offset: 0x00020B8C
		public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<UniTask<T>> continuationFunction)
		{
			await task;
			return await continuationFunction();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000229D8 File Offset: 0x00020BD8
		public static async UniTask<T> Unwrap<T>(this UniTask<UniTask<T>> task)
		{
			return await (await task);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00022A1C File Offset: 0x00020C1C
		public static async UniTask Unwrap(this UniTask<UniTask> task)
		{
			await (await task);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00022A60 File Offset: 0x00020C60
		public static async UniTask<T> Unwrap<T>(this Task<UniTask<T>> task)
		{
			return await (await task);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00022AA4 File Offset: 0x00020CA4
		public static async UniTask<T> Unwrap<T>(this Task<UniTask<T>> task, bool continueOnCapturedContext)
		{
			return await (await task.ConfigureAwait(continueOnCapturedContext));
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00022AF0 File Offset: 0x00020CF0
		public static async UniTask Unwrap(this Task<UniTask> task)
		{
			await (await task);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00022B34 File Offset: 0x00020D34
		public static async UniTask Unwrap(this Task<UniTask> task, bool continueOnCapturedContext)
		{
			await (await task.ConfigureAwait(continueOnCapturedContext));
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00022B80 File Offset: 0x00020D80
		public static async UniTask<T> Unwrap<T>(this UniTask<Task<T>> task)
		{
			return await (await task);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public static async UniTask<T> Unwrap<T>(this UniTask<Task<T>> task, bool continueOnCapturedContext)
		{
			return await (await task).ConfigureAwait(continueOnCapturedContext);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00022C10 File Offset: 0x00020E10
		public static async UniTask Unwrap(this UniTask<Task> task)
		{
			await (await task);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00022C54 File Offset: 0x00020E54
		public static async UniTask Unwrap(this UniTask<Task> task, bool continueOnCapturedContext)
		{
			await (await task).ConfigureAwait(continueOnCapturedContext);
		}

		// Token: 0x02000128 RID: 296
		private sealed class AttachExternalCancellationSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x00022CA0 File Offset: 0x00020EA0
			public AttachExternalCancellationSource(UniTask task, CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				this.tokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskExtensions.AttachExternalCancellationSource.cancellationCallbackDelegate, this);
				this.RunTask(task).Forget();
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x00022CDC File Offset: 0x00020EDC
			private async UniTaskVoid RunTask(UniTask task)
			{
				try
				{
					await task;
					this.core.TrySetResult(AsyncUnit.Default);
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
				}
				finally
				{
					this.tokenRegistration.Dispose();
				}
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00022D28 File Offset: 0x00020F28
			private static void CancellationCallback(object state)
			{
				UniTaskExtensions.AttachExternalCancellationSource self = (UniTaskExtensions.AttachExternalCancellationSource)state;
				self.core.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x00022D4E File Offset: 0x00020F4E
			public void GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x00022D5D File Offset: 0x00020F5D
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x00022D6B File Offset: 0x00020F6B
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x00022D7B File Offset: 0x00020F7B
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x0400045A RID: 1114
			private static readonly Action<object> cancellationCallbackDelegate = new Action<object>(UniTaskExtensions.AttachExternalCancellationSource.CancellationCallback);

			// Token: 0x0400045B RID: 1115
			private CancellationToken cancellationToken;

			// Token: 0x0400045C RID: 1116
			private CancellationTokenRegistration tokenRegistration;

			// Token: 0x0400045D RID: 1117
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x0200012A RID: 298
		private sealed class AttachExternalCancellationSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x06000770 RID: 1904 RVA: 0x00022EB4 File Offset: 0x000210B4
			public AttachExternalCancellationSource(UniTask<T> task, CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				this.tokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskExtensions.AttachExternalCancellationSource<T>.cancellationCallbackDelegate, this);
				this.RunTask(task).Forget();
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x00022EF0 File Offset: 0x000210F0
			private async UniTaskVoid RunTask(UniTask<T> task)
			{
				try
				{
					T t = await task;
					this.core.TrySetResult(t);
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
				}
				finally
				{
					this.tokenRegistration.Dispose();
				}
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00022F3C File Offset: 0x0002113C
			private static void CancellationCallback(object state)
			{
				UniTaskExtensions.AttachExternalCancellationSource<T> self = (UniTaskExtensions.AttachExternalCancellationSource<T>)state;
				self.core.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x00022F62 File Offset: 0x00021162
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x00022F71 File Offset: 0x00021171
			public T GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00022F7F File Offset: 0x0002117F
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x00022F8D File Offset: 0x0002118D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x00022F9D File Offset: 0x0002119D
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x04000463 RID: 1123
			private static readonly Action<object> cancellationCallbackDelegate = new Action<object>(UniTaskExtensions.AttachExternalCancellationSource<T>.CancellationCallback);

			// Token: 0x04000464 RID: 1124
			private CancellationToken cancellationToken;

			// Token: 0x04000465 RID: 1125
			private CancellationTokenRegistration tokenRegistration;

			// Token: 0x04000466 RID: 1126
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x0200012C RID: 300
		private sealed class ToCoroutineEnumerator : IEnumerator
		{
			// Token: 0x0600077B RID: 1915 RVA: 0x000230D6 File Offset: 0x000212D6
			public ToCoroutineEnumerator(UniTask task, Action<Exception> exceptionHandler)
			{
				this.completed = false;
				this.exceptionHandler = exceptionHandler;
				this.task = task;
			}

			// Token: 0x0600077C RID: 1916 RVA: 0x000230F4 File Offset: 0x000212F4
			private async UniTaskVoid RunTask(UniTask task)
			{
				try
				{
					await task;
				}
				catch (Exception ex)
				{
					if (this.exceptionHandler != null)
					{
						this.exceptionHandler(ex);
					}
					else
					{
						this.exception = ExceptionDispatchInfo.Capture(ex);
					}
				}
				finally
				{
					this.completed = true;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600077D RID: 1917 RVA: 0x0002313F File Offset: 0x0002133F
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600077E RID: 1918 RVA: 0x00023144 File Offset: 0x00021344
			public bool MoveNext()
			{
				if (!this.isStarted)
				{
					this.isStarted = true;
					this.RunTask(this.task).Forget();
				}
				if (this.exception != null)
				{
					this.exception.Throw();
					return false;
				}
				return !this.completed;
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x000030EE File Offset: 0x000012EE
			void IEnumerator.Reset()
			{
			}

			// Token: 0x0400046C RID: 1132
			private bool completed;

			// Token: 0x0400046D RID: 1133
			private UniTask task;

			// Token: 0x0400046E RID: 1134
			private Action<Exception> exceptionHandler;

			// Token: 0x0400046F RID: 1135
			private bool isStarted;

			// Token: 0x04000470 RID: 1136
			private ExceptionDispatchInfo exception;
		}

		// Token: 0x0200012E RID: 302
		private sealed class ToCoroutineEnumerator<T> : IEnumerator
		{
			// Token: 0x06000782 RID: 1922 RVA: 0x000232AA File Offset: 0x000214AA
			public ToCoroutineEnumerator(UniTask<T> task, Action<T> resultHandler, Action<Exception> exceptionHandler)
			{
				this.completed = false;
				this.task = task;
				this.resultHandler = resultHandler;
				this.exceptionHandler = exceptionHandler;
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x000232D0 File Offset: 0x000214D0
			private async UniTaskVoid RunTask(UniTask<T> task)
			{
				try
				{
					T value = await task;
					this.current = value;
					if (this.resultHandler != null)
					{
						this.resultHandler(value);
					}
				}
				catch (Exception ex)
				{
					if (this.exceptionHandler != null)
					{
						this.exceptionHandler(ex);
					}
					else
					{
						this.exception = ExceptionDispatchInfo.Capture(ex);
					}
				}
				finally
				{
					this.completed = true;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002331B File Offset: 0x0002151B
			public object Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x00023324 File Offset: 0x00021524
			public bool MoveNext()
			{
				if (!this.isStarted)
				{
					this.isStarted = true;
					this.RunTask(this.task).Forget();
				}
				if (this.exception != null)
				{
					this.exception.Throw();
					return false;
				}
				return !this.completed;
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x000030EE File Offset: 0x000012EE
			void IEnumerator.Reset()
			{
			}

			// Token: 0x04000476 RID: 1142
			private bool completed;

			// Token: 0x04000477 RID: 1143
			private Action<T> resultHandler;

			// Token: 0x04000478 RID: 1144
			private Action<Exception> exceptionHandler;

			// Token: 0x04000479 RID: 1145
			private bool isStarted;

			// Token: 0x0400047A RID: 1146
			private UniTask<T> task;

			// Token: 0x0400047B RID: 1147
			private object current;

			// Token: 0x0400047C RID: 1148
			private ExceptionDispatchInfo exception;
		}
	}
}

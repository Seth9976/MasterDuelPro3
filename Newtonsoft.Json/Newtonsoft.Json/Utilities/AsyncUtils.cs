using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000093 RID: 147
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AsyncUtils
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0001A711 File Offset: 0x00018911
		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return AsyncUtils.False;
			}
			return AsyncUtils.True;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001A721 File Offset: 0x00018921
		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001A734 File Offset: 0x00018934
		[NullableContext(2)]
		[return: Nullable(new byte[] { 2, 1 })]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001A747 File Offset: 0x00018947
		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(delegate
			{
			}, cancellationToken);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001A770 File Offset: 0x00018970
		public static Task<T> FromCanceled<[Nullable(2)] T>(this CancellationToken cancellationToken)
		{
			Func<T> func;
			if ((func = AsyncUtils.<>c__6<T>.<>9__6_0) == null)
			{
				Func<T> func2 = (AsyncUtils.<>c__6<T>.<>9__6_0 = () => default(T));
				func = func2;
			}
			return new Task<T>(func, cancellationToken);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001A7A4 File Offset: 0x000189A4
		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001A7BD File Offset: 0x000189BD
		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001A7D6 File Offset: 0x000189D6
		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001A7F2 File Offset: 0x000189F2
		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001A80E File Offset: 0x00018A0E
		public static bool IsCompletedSuccessfully(this Task task)
		{
			return task.Status == TaskStatus.RanToCompletion;
		}

		// Token: 0x04000367 RID: 871
		public static readonly Task<bool> False = Task.FromResult<bool>(false);

		// Token: 0x04000368 RID: 872
		public static readonly Task<bool> True = Task.FromResult<bool>(true);

		// Token: 0x04000369 RID: 873
		internal static readonly Task CompletedTask = Task.Delay(0);
	}
}

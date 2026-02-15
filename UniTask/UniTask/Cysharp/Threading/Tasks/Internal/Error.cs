using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000231 RID: 561
	internal static class Error
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x0002CA7F File Offset: 0x0002AC7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ThrowArgumentNullException<T>(T value, string paramName) where T : class
		{
			if (value == null)
			{
				Error.ThrowArgumentNullExceptionCore(paramName);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002CA8F File Offset: 0x0002AC8F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowArgumentNullExceptionCore(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002CA97 File Offset: 0x0002AC97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002CA9F File Offset: 0x0002AC9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception NoElements()
		{
			return new InvalidOperationException("Source sequence doesn't contain any elements.");
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002CAAB File Offset: 0x0002ACAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception MoreThanOneElement()
		{
			return new InvalidOperationException("Source sequence contains more than one element.");
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002CAB7 File Offset: 0x0002ACB7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowArgumentException(string message)
		{
			throw new ArgumentException(message);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002CABF File Offset: 0x0002ACBF
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowNotYetCompleted()
		{
			throw new InvalidOperationException("Not yet completed.");
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002CABF File Offset: 0x0002ACBF
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T ThrowNotYetCompleted<T>()
		{
			throw new InvalidOperationException("Not yet completed.");
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002CACB File Offset: 0x0002ACCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ThrowWhenContinuationIsAlreadyRegistered<T>(T continuationField) where T : class
		{
			if (continuationField != null)
			{
				Error.ThrowInvalidOperationExceptionCore("continuation is already registered.");
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002CADF File Offset: 0x0002ACDF
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInvalidOperationExceptionCore(string message)
		{
			throw new InvalidOperationException(message);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002CAE7 File Offset: 0x0002ACE7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException();
		}
	}
}

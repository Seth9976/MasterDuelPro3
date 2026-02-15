using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000250 RID: 592
	public static class Observable
	{
		// Token: 0x06001593 RID: 5523 RVA: 0x0006261C File Offset: 0x0006081C
		public static IObservable<TValue> Where<TValue>(this IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereObservable<TValue>(source, predicate);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00062641 File Offset: 0x00060841
		public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00062666 File Offset: 0x00060866
		public static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectManyObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0006268B File Offset: 0x0006088B
		public static IObservable<TValue> Take<TValue>(this IObservable<TValue> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new TakeNObservable<TValue>(source, count);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000626B1 File Offset: 0x000608B1
		public static IObservable<InputEventPtr> ForDevice(this IObservable<InputEventPtr> source, InputDevice device)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, null, device);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000626C9 File Offset: 0x000608C9
		public static IObservable<InputEventPtr> ForDevice<TDevice>(this IObservable<InputEventPtr> source) where TDevice : InputDevice
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, typeof(TDevice), null);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000626EC File Offset: 0x000608EC
		public static IDisposable CallOnce<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			IDisposable subscription = null;
			subscription = source.Take(1).Subscribe(new Observer<TValue>(action, delegate
			{
				IDisposable subscription2 = subscription;
				if (subscription2 == null)
				{
					return;
				}
				subscription2.Dispose();
			}));
			return subscription;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0006274C File Offset: 0x0006094C
		public static IDisposable Call<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return source.Subscribe(new Observer<TValue>(action, null));
		}
	}
}

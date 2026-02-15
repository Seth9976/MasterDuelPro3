using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System
{
	// Token: 0x02000116 RID: 278
	internal class LazyHelper
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00028051 File Offset: 0x00026251
		internal LazyState State { get; }

		// Token: 0x06000917 RID: 2327 RVA: 0x00028059 File Offset: 0x00026259
		internal LazyHelper(LazyState state)
		{
			this.State = state;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00028068 File Offset: 0x00026268
		internal LazyHelper(LazyThreadSafetyMode mode, Exception exception)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				this.State = LazyState.NoneException;
				break;
			case LazyThreadSafetyMode.PublicationOnly:
				this.State = LazyState.PublicationOnlyException;
				break;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				this.State = LazyState.ExecutionAndPublicationException;
				break;
			}
			this._exceptionDispatch = ExceptionDispatchInfo.Capture(exception);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000280B5 File Offset: 0x000262B5
		internal void ThrowException()
		{
			this._exceptionDispatch.Throw();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000280C4 File Offset: 0x000262C4
		internal static LazyHelper Create(LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				if (!useDefaultConstructor)
				{
					return LazyHelper.NoneViaFactory;
				}
				return LazyHelper.NoneViaConstructor;
			case LazyThreadSafetyMode.PublicationOnly:
				if (!useDefaultConstructor)
				{
					return LazyHelper.PublicationOnlyViaFactory;
				}
				return LazyHelper.PublicationOnlyViaConstructor;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				return new LazyHelper(useDefaultConstructor ? LazyState.ExecutionAndPublicationViaConstructor : LazyState.ExecutionAndPublicationViaFactory);
			default:
				throw new ArgumentOutOfRangeException("mode", "The mode argument specifies an invalid value.");
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00028120 File Offset: 0x00026320
		internal static object CreateViaDefaultConstructor(Type type)
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
			}
			return obj;
		}

		// Token: 0x0400043B RID: 1083
		internal static readonly LazyHelper NoneViaConstructor = new LazyHelper(LazyState.NoneViaConstructor);

		// Token: 0x0400043C RID: 1084
		internal static readonly LazyHelper NoneViaFactory = new LazyHelper(LazyState.NoneViaFactory);

		// Token: 0x0400043D RID: 1085
		internal static readonly LazyHelper PublicationOnlyViaConstructor = new LazyHelper(LazyState.PublicationOnlyViaConstructor);

		// Token: 0x0400043E RID: 1086
		internal static readonly LazyHelper PublicationOnlyViaFactory = new LazyHelper(LazyState.PublicationOnlyViaFactory);

		// Token: 0x0400043F RID: 1087
		internal static readonly LazyHelper PublicationOnlyWaitForOtherThreadToPublish = new LazyHelper(LazyState.PublicationOnlyWait);

		// Token: 0x04000441 RID: 1089
		private readonly ExceptionDispatchInfo _exceptionDispatch;
	}
}

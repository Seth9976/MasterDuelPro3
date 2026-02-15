using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Net.Security
{
	// Token: 0x02000062 RID: 98
	internal abstract class AsyncProtocolRequest
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004ECA File Offset: 0x000030CA
		public MobileAuthenticatedStream Parent { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004ED2 File Offset: 0x000030D2
		public bool RunSynchronously { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004EDA File Offset: 0x000030DA
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004EE7 File Offset: 0x000030E7
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004EEF File Offset: 0x000030EF
		public int UserResult { get; protected set; }

		// Token: 0x06000126 RID: 294 RVA: 0x00004EF8 File Offset: 0x000030F8
		public AsyncProtocolRequest(MobileAuthenticatedStream parent, bool sync)
		{
			this.Parent = parent;
			this.RunSynchronously = sync;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004F1C File Offset: 0x0000311C
		internal void RequestRead(int size)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.RequestedSize += size;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004F64 File Offset: 0x00003164
		internal void RequestWrite()
		{
			this.WriteRequested = 1;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004F70 File Offset: 0x00003170
		internal async Task<AsyncProtocolResult> StartOperation(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.Started, 1, 0) != 0)
			{
				throw new InvalidOperationException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				await this.ProcessOperation(cancellationToken).ConfigureAwait(false);
				asyncProtocolResult = new AsyncProtocolResult(this.UserResult);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.Parent.SetException(ex));
			}
			return asyncProtocolResult;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004FBC File Offset: 0x000031BC
		private async Task ProcessOperation(CancellationToken cancellationToken)
		{
			AsyncOperationStatus status = AsyncOperationStatus.Initialize;
			while (status != AsyncOperationStatus.Complete)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int? num = await this.InnerRead(cancellationToken).ConfigureAwait(false);
				if (num != null)
				{
					int? num2 = num;
					int num3 = 0;
					if ((num2.GetValueOrDefault() == num3) & (num2 != null))
					{
						status = AsyncOperationStatus.ReadDone;
					}
					else
					{
						num2 = num;
						num3 = 0;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new IOException("Remote prematurely closed connection.");
						}
					}
				}
				if (status <= AsyncOperationStatus.ReadDone)
				{
					AsyncOperationStatus newStatus;
					try
					{
						newStatus = this.Run(status);
						goto IL_011C;
					}
					catch (Exception ex)
					{
						throw MobileAuthenticatedStream.GetSSPIException(ex);
					}
					goto IL_0116;
					IL_011C:
					if (Interlocked.Exchange(ref this.WriteRequested, 0) != 0)
					{
						await this.Parent.InnerWrite(this.RunSynchronously, cancellationToken).ConfigureAwait(false);
					}
					status = newStatus;
					continue;
				}
				IL_0116:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005008 File Offset: 0x00003208
		private async Task<int?> InnerRead(CancellationToken cancellationToken)
		{
			int? totalRead = null;
			int num2;
			for (int requestedSize = Interlocked.Exchange(ref this.RequestedSize, 0); requestedSize > 0; requestedSize += num2)
			{
				int num = await this.Parent.InnerRead(this.RunSynchronously, requestedSize, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					return new int?(num);
				}
				if (num > requestedSize)
				{
					throw new InvalidOperationException();
				}
				totalRead += num;
				requestedSize -= num;
				num2 = Interlocked.Exchange(ref this.RequestedSize, 0);
			}
			return totalRead;
		}

		// Token: 0x0600012C RID: 300
		protected abstract AsyncOperationStatus Run(AsyncOperationStatus status);

		// Token: 0x0600012D RID: 301 RVA: 0x00005053 File Offset: 0x00003253
		public override string ToString()
		{
			return string.Format("[{0}]", this.Name);
		}

		// Token: 0x040000F2 RID: 242
		private int Started;

		// Token: 0x040000F3 RID: 243
		private int RequestedSize;

		// Token: 0x040000F4 RID: 244
		private int WriteRequested;

		// Token: 0x040000F5 RID: 245
		private readonly object locker = new object();
	}
}

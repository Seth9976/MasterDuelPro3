using System;

namespace Mono.Net.Security
{
	// Token: 0x02000068 RID: 104
	internal class AsyncReadRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x0600013B RID: 315 RVA: 0x000055D2 File Offset: 0x000037D2
		public AsyncReadRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000055E4 File Offset: 0x000037E4
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessRead(base.UserBuffer);
			int item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			if (item < 0)
			{
				base.UserResult = -1;
				return AsyncOperationStatus.Complete;
			}
			base.CurrentSize += item;
			base.UserBuffer.Offset += item;
			base.UserBuffer.Size -= item;
			if (item2 && base.CurrentSize == 0)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}

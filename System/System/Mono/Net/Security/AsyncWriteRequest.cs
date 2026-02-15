using System;

namespace Mono.Net.Security
{
	// Token: 0x02000069 RID: 105
	internal class AsyncWriteRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x0600013D RID: 317 RVA: 0x000055D2 File Offset: 0x000037D2
		public AsyncWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000566C File Offset: 0x0000386C
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			if (base.UserBuffer.Size == 0)
			{
				base.UserResult = base.CurrentSize;
				return AsyncOperationStatus.Complete;
			}
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessWrite(base.UserBuffer);
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
			if (item2)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}

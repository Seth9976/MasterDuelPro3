using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000021 RID: 33
	public abstract class ChannelWriter<T>
	{
		// Token: 0x060000B7 RID: 183
		public abstract bool TryWrite(T item);

		// Token: 0x060000B8 RID: 184
		public abstract bool TryComplete(Exception error = null);

		// Token: 0x060000B9 RID: 185 RVA: 0x00003672 File Offset: 0x00001872
		public void Complete(Exception error = null)
		{
			if (!this.TryComplete(error))
			{
				throw new ChannelClosedException();
			}
		}
	}
}

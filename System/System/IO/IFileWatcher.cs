using System;

namespace System.IO
{
	// Token: 0x02000359 RID: 857
	internal interface IFileWatcher
	{
		// Token: 0x06001544 RID: 5444
		void StartDispatching(object fsw);

		// Token: 0x06001545 RID: 5445
		void StopDispatching(object fsw);

		// Token: 0x06001546 RID: 5446
		void Dispose(object fsw);
	}
}

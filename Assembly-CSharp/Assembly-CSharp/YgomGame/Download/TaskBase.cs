using System;

namespace YgomGame.Download
{
	// Token: 0x02000F5A RID: 3930
	public interface TaskBase
	{
		// Token: 0x060073C7 RID: 29639
		bool IsDone();

		// Token: 0x060073C8 RID: 29640
		bool IsSuccess();

		// Token: 0x060073C9 RID: 29641
		bool IsError();

		// Token: 0x060073CA RID: 29642
		void Exec();
	}
}

using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EA2 RID: 3746
	public interface IMainCameraOperation
	{
		// Token: 0x06006D3B RID: 27963
		void UpdateOperation(MainCameraOrganizer mainCamera);

		// Token: 0x06006D3C RID: 27964
		void LateUpdateOperation(MainCameraOrganizer mainCamera);
	}
}

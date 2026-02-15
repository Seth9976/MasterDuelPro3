using System;

namespace YgomSystem.Billing
{
	// Token: 0x02000795 RID: 1941
	public enum ResultCode
	{
		// Token: 0x040034E9 RID: 13545
		NONE,
		// Token: 0x040034EA RID: 13546
		Success,
		// Token: 0x040034EB RID: 13547
		NotEnable,
		// Token: 0x040034EC RID: 13548
		Error,
		// Token: 0x040034ED RID: 13549
		ReservationError,
		// Token: 0x040034EE RID: 13550
		Cancel,
		// Token: 0x040034EF RID: 13551
		PurchaseError,
		// Token: 0x040034F0 RID: 13552
		DateLimitOverError,
		// Token: 0x040034F1 RID: 13553
		LimitExceededError,
		// Token: 0x040034F2 RID: 13554
		LimitError,
		// Token: 0x040034F3 RID: 13555
		RegisterAgeError,
		// Token: 0x040034F4 RID: 13556
		AddItemError,
		// Token: 0x040034F5 RID: 13557
		RestoreError,
		// Token: 0x040034F6 RID: 13558
		SectionMainte,
		// Token: 0x040034F7 RID: 13559
		StoreMainte,
		// Token: 0x040034F8 RID: 13560
		Pending,
		// Token: 0x040034F9 RID: 13561
		SteamOverlayCaution,
		// Token: 0x040034FA RID: 13562
		VoidedPurchaseError,
		// Token: 0x040034FB RID: 13563
		AdminFinishTransaction
	}
}

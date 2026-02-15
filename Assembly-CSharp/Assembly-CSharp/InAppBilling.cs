using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class InAppBilling : MonoBehaviour
{
	// Token: 0x060000B8 RID: 184 RVA: 0x0000216A File Offset: 0x0000036A
	private string decode(string str)
	{
		return null;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000216D File Offset: 0x0000036D
	private void Awake()
	{
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000216D File Offset: 0x0000036D
	public static void RunOnMainThread(InAppBilling.Func f)
	{
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x0400010D RID: 269
	private const string version = "0.5.20230216rc0";

	// Token: 0x0400010E RID: 270
	public const int SUCCESS = 0;

	// Token: 0x0400010F RID: 271
	public const int ERR_REQUIRE_AGE = -1;

	// Token: 0x04000110 RID: 272
	public const int ERR_INVALID_RECEIPT = -2;

	// Token: 0x04000111 RID: 273
	public const int ERR_BAD_REQUEST = -3;

	// Token: 0x04000112 RID: 274
	public const int ERR_ALREADY_FINISHED = -4;

	// Token: 0x04000113 RID: 275
	public const int ERR_SERVER = -5;

	// Token: 0x04000114 RID: 276
	public const int ERR_USER_CANNOT_BUY = -6;

	// Token: 0x04000115 RID: 277
	public const int ERR_MAINTENANCE = -7;

	// Token: 0x04000116 RID: 278
	public const int ERR_NOT_ENOUGH_POINT = -8;

	// Token: 0x04000117 RID: 279
	public const int ERR_BAD_USER = -9;

	// Token: 0x04000118 RID: 280
	public const int ERR_PURCHASE_CANCELED = -13;

	// Token: 0x04000119 RID: 281
	public const int ERR_NO_SUBSCRIPTIONS = -14;

	// Token: 0x0400011A RID: 282
	public const int ERR_VOIDED_PURCHASE = -15;

	// Token: 0x0400011B RID: 283
	public const int ERR_PURCHASE_PENDING = -16;

	// Token: 0x0400011C RID: 284
	[HideInInspector]
	public string base64EncodedPublicKey;

	// Token: 0x0400011D RID: 285
	public bool debuggable;

	// Token: 0x0400011E RID: 286
	private static Queue<InAppBilling.Func> queue;

	// Token: 0x0200002C RID: 44
	// (Invoke) Token: 0x060000BE RID: 190
	public delegate void Func();
}

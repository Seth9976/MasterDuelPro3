using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000086 RID: 134
	internal enum DnsQType : ushort
	{
		// Token: 0x04000198 RID: 408
		A = 1,
		// Token: 0x04000199 RID: 409
		NS,
		// Token: 0x0400019A RID: 410
		[Obsolete]
		MD,
		// Token: 0x0400019B RID: 411
		[Obsolete]
		MF,
		// Token: 0x0400019C RID: 412
		CNAME,
		// Token: 0x0400019D RID: 413
		SOA,
		// Token: 0x0400019E RID: 414
		[Obsolete]
		MB,
		// Token: 0x0400019F RID: 415
		[Obsolete]
		MG,
		// Token: 0x040001A0 RID: 416
		[Obsolete]
		MR,
		// Token: 0x040001A1 RID: 417
		[Obsolete]
		NULL,
		// Token: 0x040001A2 RID: 418
		[Obsolete]
		WKS,
		// Token: 0x040001A3 RID: 419
		PTR,
		// Token: 0x040001A4 RID: 420
		[Obsolete]
		HINFO,
		// Token: 0x040001A5 RID: 421
		[Obsolete]
		MINFO,
		// Token: 0x040001A6 RID: 422
		MX,
		// Token: 0x040001A7 RID: 423
		TXT,
		// Token: 0x040001A8 RID: 424
		[Obsolete]
		RP,
		// Token: 0x040001A9 RID: 425
		AFSDB,
		// Token: 0x040001AA RID: 426
		[Obsolete]
		X25,
		// Token: 0x040001AB RID: 427
		[Obsolete]
		ISDN,
		// Token: 0x040001AC RID: 428
		[Obsolete]
		RT,
		// Token: 0x040001AD RID: 429
		[Obsolete]
		NSAP,
		// Token: 0x040001AE RID: 430
		[Obsolete]
		NSAPPTR,
		// Token: 0x040001AF RID: 431
		SIG,
		// Token: 0x040001B0 RID: 432
		KEY,
		// Token: 0x040001B1 RID: 433
		[Obsolete]
		PX,
		// Token: 0x040001B2 RID: 434
		[Obsolete]
		GPOS,
		// Token: 0x040001B3 RID: 435
		AAAA,
		// Token: 0x040001B4 RID: 436
		LOC,
		// Token: 0x040001B5 RID: 437
		[Obsolete]
		NXT,
		// Token: 0x040001B6 RID: 438
		[Obsolete]
		EID,
		// Token: 0x040001B7 RID: 439
		[Obsolete]
		NIMLOC,
		// Token: 0x040001B8 RID: 440
		SRV,
		// Token: 0x040001B9 RID: 441
		[Obsolete]
		ATMA,
		// Token: 0x040001BA RID: 442
		NAPTR,
		// Token: 0x040001BB RID: 443
		KX,
		// Token: 0x040001BC RID: 444
		CERT,
		// Token: 0x040001BD RID: 445
		[Obsolete]
		A6,
		// Token: 0x040001BE RID: 446
		DNAME,
		// Token: 0x040001BF RID: 447
		[Obsolete]
		SINK,
		// Token: 0x040001C0 RID: 448
		OPT,
		// Token: 0x040001C1 RID: 449
		[Obsolete]
		APL,
		// Token: 0x040001C2 RID: 450
		DS,
		// Token: 0x040001C3 RID: 451
		SSHFP,
		// Token: 0x040001C4 RID: 452
		IPSECKEY,
		// Token: 0x040001C5 RID: 453
		RRSIG,
		// Token: 0x040001C6 RID: 454
		NSEC,
		// Token: 0x040001C7 RID: 455
		DNSKEY,
		// Token: 0x040001C8 RID: 456
		DHCID,
		// Token: 0x040001C9 RID: 457
		NSEC3,
		// Token: 0x040001CA RID: 458
		NSEC3PARAM,
		// Token: 0x040001CB RID: 459
		HIP = 55,
		// Token: 0x040001CC RID: 460
		NINFO,
		// Token: 0x040001CD RID: 461
		RKEY,
		// Token: 0x040001CE RID: 462
		TALINK,
		// Token: 0x040001CF RID: 463
		SPF = 99,
		// Token: 0x040001D0 RID: 464
		[Obsolete]
		UINFO,
		// Token: 0x040001D1 RID: 465
		[Obsolete]
		UID,
		// Token: 0x040001D2 RID: 466
		[Obsolete]
		GID,
		// Token: 0x040001D3 RID: 467
		[Obsolete]
		UNSPEC,
		// Token: 0x040001D4 RID: 468
		TKEY = 249,
		// Token: 0x040001D5 RID: 469
		TSIG,
		// Token: 0x040001D6 RID: 470
		IXFR,
		// Token: 0x040001D7 RID: 471
		AXFR,
		// Token: 0x040001D8 RID: 472
		[Obsolete]
		MAILB,
		// Token: 0x040001D9 RID: 473
		[Obsolete]
		MAILA,
		// Token: 0x040001DA RID: 474
		ALL,
		// Token: 0x040001DB RID: 475
		URI,
		// Token: 0x040001DC RID: 476
		TA = 32768,
		// Token: 0x040001DD RID: 477
		DLV
	}
}

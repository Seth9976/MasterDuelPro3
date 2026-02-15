using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000091 RID: 145
	internal enum DnsType : ushort
	{
		// Token: 0x04000206 RID: 518
		A = 1,
		// Token: 0x04000207 RID: 519
		NS,
		// Token: 0x04000208 RID: 520
		[Obsolete]
		MD,
		// Token: 0x04000209 RID: 521
		[Obsolete]
		MF,
		// Token: 0x0400020A RID: 522
		CNAME,
		// Token: 0x0400020B RID: 523
		SOA,
		// Token: 0x0400020C RID: 524
		[Obsolete]
		MB,
		// Token: 0x0400020D RID: 525
		[Obsolete]
		MG,
		// Token: 0x0400020E RID: 526
		[Obsolete]
		MR,
		// Token: 0x0400020F RID: 527
		[Obsolete]
		NULL,
		// Token: 0x04000210 RID: 528
		[Obsolete]
		WKS,
		// Token: 0x04000211 RID: 529
		PTR,
		// Token: 0x04000212 RID: 530
		[Obsolete]
		HINFO,
		// Token: 0x04000213 RID: 531
		[Obsolete]
		MINFO,
		// Token: 0x04000214 RID: 532
		MX,
		// Token: 0x04000215 RID: 533
		TXT,
		// Token: 0x04000216 RID: 534
		[Obsolete]
		RP,
		// Token: 0x04000217 RID: 535
		AFSDB,
		// Token: 0x04000218 RID: 536
		[Obsolete]
		X25,
		// Token: 0x04000219 RID: 537
		[Obsolete]
		ISDN,
		// Token: 0x0400021A RID: 538
		[Obsolete]
		RT,
		// Token: 0x0400021B RID: 539
		[Obsolete]
		NSAP,
		// Token: 0x0400021C RID: 540
		[Obsolete]
		NSAPPTR,
		// Token: 0x0400021D RID: 541
		SIG,
		// Token: 0x0400021E RID: 542
		KEY,
		// Token: 0x0400021F RID: 543
		[Obsolete]
		PX,
		// Token: 0x04000220 RID: 544
		[Obsolete]
		GPOS,
		// Token: 0x04000221 RID: 545
		AAAA,
		// Token: 0x04000222 RID: 546
		LOC,
		// Token: 0x04000223 RID: 547
		[Obsolete]
		NXT,
		// Token: 0x04000224 RID: 548
		[Obsolete]
		EID,
		// Token: 0x04000225 RID: 549
		[Obsolete]
		NIMLOC,
		// Token: 0x04000226 RID: 550
		SRV,
		// Token: 0x04000227 RID: 551
		[Obsolete]
		ATMA,
		// Token: 0x04000228 RID: 552
		NAPTR,
		// Token: 0x04000229 RID: 553
		KX,
		// Token: 0x0400022A RID: 554
		CERT,
		// Token: 0x0400022B RID: 555
		[Obsolete]
		A6,
		// Token: 0x0400022C RID: 556
		DNAME,
		// Token: 0x0400022D RID: 557
		[Obsolete]
		SINK,
		// Token: 0x0400022E RID: 558
		OPT,
		// Token: 0x0400022F RID: 559
		[Obsolete]
		APL,
		// Token: 0x04000230 RID: 560
		DS,
		// Token: 0x04000231 RID: 561
		SSHFP,
		// Token: 0x04000232 RID: 562
		IPSECKEY,
		// Token: 0x04000233 RID: 563
		RRSIG,
		// Token: 0x04000234 RID: 564
		NSEC,
		// Token: 0x04000235 RID: 565
		DNSKEY,
		// Token: 0x04000236 RID: 566
		DHCID,
		// Token: 0x04000237 RID: 567
		NSEC3,
		// Token: 0x04000238 RID: 568
		NSEC3PARAM,
		// Token: 0x04000239 RID: 569
		HIP = 55,
		// Token: 0x0400023A RID: 570
		NINFO,
		// Token: 0x0400023B RID: 571
		RKEY,
		// Token: 0x0400023C RID: 572
		TALINK,
		// Token: 0x0400023D RID: 573
		SPF = 99,
		// Token: 0x0400023E RID: 574
		[Obsolete]
		UINFO,
		// Token: 0x0400023F RID: 575
		[Obsolete]
		UID,
		// Token: 0x04000240 RID: 576
		[Obsolete]
		GID,
		// Token: 0x04000241 RID: 577
		[Obsolete]
		UNSPEC,
		// Token: 0x04000242 RID: 578
		TKEY = 249,
		// Token: 0x04000243 RID: 579
		TSIG,
		// Token: 0x04000244 RID: 580
		IXFR,
		// Token: 0x04000245 RID: 581
		AXFR,
		// Token: 0x04000246 RID: 582
		[Obsolete]
		MAILB,
		// Token: 0x04000247 RID: 583
		[Obsolete]
		MAILA,
		// Token: 0x04000248 RID: 584
		URI = 256,
		// Token: 0x04000249 RID: 585
		TA = 32768,
		// Token: 0x0400024A RID: 586
		DLV
	}
}

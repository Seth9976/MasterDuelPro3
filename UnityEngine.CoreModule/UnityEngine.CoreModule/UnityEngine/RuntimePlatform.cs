using System;

namespace UnityEngine
{
	// Token: 0x0200009C RID: 156
	public enum RuntimePlatform
	{
		// Token: 0x04000182 RID: 386
		OSXEditor,
		// Token: 0x04000183 RID: 387
		OSXPlayer,
		// Token: 0x04000184 RID: 388
		WindowsPlayer,
		// Token: 0x04000185 RID: 389
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer,
		// Token: 0x04000186 RID: 390
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer,
		// Token: 0x04000187 RID: 391
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer,
		// Token: 0x04000188 RID: 392
		WindowsEditor = 7,
		// Token: 0x04000189 RID: 393
		IPhonePlayer,
		// Token: 0x0400018A RID: 394
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		// Token: 0x0400018B RID: 395
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		// Token: 0x0400018C RID: 396
		Android = 11,
		// Token: 0x0400018D RID: 397
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		// Token: 0x0400018E RID: 398
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		// Token: 0x0400018F RID: 399
		LinuxPlayer = 13,
		// Token: 0x04000190 RID: 400
		LinuxEditor = 16,
		// Token: 0x04000191 RID: 401
		WebGLPlayer,
		// Token: 0x04000192 RID: 402
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		// Token: 0x04000193 RID: 403
		WSAPlayerX86 = 18,
		// Token: 0x04000194 RID: 404
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		// Token: 0x04000195 RID: 405
		WSAPlayerX64 = 19,
		// Token: 0x04000196 RID: 406
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		// Token: 0x04000197 RID: 407
		WSAPlayerARM = 20,
		// Token: 0x04000198 RID: 408
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player,
		// Token: 0x04000199 RID: 409
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer,
		// Token: 0x0400019A RID: 410
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer,
		// Token: 0x0400019B RID: 411
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2,
		// Token: 0x0400019C RID: 412
		PS4,
		// Token: 0x0400019D RID: 413
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM,
		// Token: 0x0400019E RID: 414
		XboxOne,
		// Token: 0x0400019F RID: 415
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer,
		// Token: 0x040001A0 RID: 416
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		// Token: 0x040001A1 RID: 417
		tvOS,
		// Token: 0x040001A2 RID: 418
		Switch,
		// Token: 0x040001A3 RID: 419
		[Obsolete("Lumin is no longer supported in Unity 2022.2")]
		Lumin,
		// Token: 0x040001A4 RID: 420
		[Obsolete("Stadia is no longer supported in Unity 2023.1")]
		Stadia,
		// Token: 0x040001A5 RID: 421
		[Obsolete("CloudRendering is deprecated, please use LinuxHeadlessSimulation (UnityUpgradable) -> LinuxHeadlessSimulation", false)]
		CloudRendering = -1,
		// Token: 0x040001A6 RID: 422
		LinuxHeadlessSimulation = 35,
		// Token: 0x040001A7 RID: 423
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x040001A8 RID: 424
		GameCoreXboxSeries = 36,
		// Token: 0x040001A9 RID: 425
		GameCoreXboxOne,
		// Token: 0x040001AA RID: 426
		PS5,
		// Token: 0x040001AB RID: 427
		EmbeddedLinuxArm64,
		// Token: 0x040001AC RID: 428
		EmbeddedLinuxArm32,
		// Token: 0x040001AD RID: 429
		EmbeddedLinuxX64,
		// Token: 0x040001AE RID: 430
		EmbeddedLinuxX86,
		// Token: 0x040001AF RID: 431
		LinuxServer,
		// Token: 0x040001B0 RID: 432
		WindowsServer,
		// Token: 0x040001B1 RID: 433
		OSXServer,
		// Token: 0x040001B2 RID: 434
		QNXArm32,
		// Token: 0x040001B3 RID: 435
		QNXArm64,
		// Token: 0x040001B4 RID: 436
		QNXX64,
		// Token: 0x040001B5 RID: 437
		QNXX86,
		// Token: 0x040001B6 RID: 438
		VisionOS,
		// Token: 0x040001B7 RID: 439
		ReservedCFE
	}
}

using System;
using System.ComponentModel;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003FC RID: 1020
	public enum GraphicsFormat
	{
		// Token: 0x04000DB3 RID: 3507
		None,
		// Token: 0x04000DB4 RID: 3508
		R8_SRGB,
		// Token: 0x04000DB5 RID: 3509
		R8G8_SRGB,
		// Token: 0x04000DB6 RID: 3510
		R8G8B8_SRGB,
		// Token: 0x04000DB7 RID: 3511
		R8G8B8A8_SRGB,
		// Token: 0x04000DB8 RID: 3512
		R8_UNorm,
		// Token: 0x04000DB9 RID: 3513
		R8G8_UNorm,
		// Token: 0x04000DBA RID: 3514
		R8G8B8_UNorm,
		// Token: 0x04000DBB RID: 3515
		R8G8B8A8_UNorm,
		// Token: 0x04000DBC RID: 3516
		R8_SNorm,
		// Token: 0x04000DBD RID: 3517
		R8G8_SNorm,
		// Token: 0x04000DBE RID: 3518
		R8G8B8_SNorm,
		// Token: 0x04000DBF RID: 3519
		R8G8B8A8_SNorm,
		// Token: 0x04000DC0 RID: 3520
		R8_UInt,
		// Token: 0x04000DC1 RID: 3521
		R8G8_UInt,
		// Token: 0x04000DC2 RID: 3522
		R8G8B8_UInt,
		// Token: 0x04000DC3 RID: 3523
		R8G8B8A8_UInt,
		// Token: 0x04000DC4 RID: 3524
		R8_SInt,
		// Token: 0x04000DC5 RID: 3525
		R8G8_SInt,
		// Token: 0x04000DC6 RID: 3526
		R8G8B8_SInt,
		// Token: 0x04000DC7 RID: 3527
		R8G8B8A8_SInt,
		// Token: 0x04000DC8 RID: 3528
		R16_UNorm,
		// Token: 0x04000DC9 RID: 3529
		R16G16_UNorm,
		// Token: 0x04000DCA RID: 3530
		R16G16B16_UNorm,
		// Token: 0x04000DCB RID: 3531
		R16G16B16A16_UNorm,
		// Token: 0x04000DCC RID: 3532
		R16_SNorm,
		// Token: 0x04000DCD RID: 3533
		R16G16_SNorm,
		// Token: 0x04000DCE RID: 3534
		R16G16B16_SNorm,
		// Token: 0x04000DCF RID: 3535
		R16G16B16A16_SNorm,
		// Token: 0x04000DD0 RID: 3536
		R16_UInt,
		// Token: 0x04000DD1 RID: 3537
		R16G16_UInt,
		// Token: 0x04000DD2 RID: 3538
		R16G16B16_UInt,
		// Token: 0x04000DD3 RID: 3539
		R16G16B16A16_UInt,
		// Token: 0x04000DD4 RID: 3540
		R16_SInt,
		// Token: 0x04000DD5 RID: 3541
		R16G16_SInt,
		// Token: 0x04000DD6 RID: 3542
		R16G16B16_SInt,
		// Token: 0x04000DD7 RID: 3543
		R16G16B16A16_SInt,
		// Token: 0x04000DD8 RID: 3544
		R32_UInt,
		// Token: 0x04000DD9 RID: 3545
		R32G32_UInt,
		// Token: 0x04000DDA RID: 3546
		R32G32B32_UInt,
		// Token: 0x04000DDB RID: 3547
		R32G32B32A32_UInt,
		// Token: 0x04000DDC RID: 3548
		R32_SInt,
		// Token: 0x04000DDD RID: 3549
		R32G32_SInt,
		// Token: 0x04000DDE RID: 3550
		R32G32B32_SInt,
		// Token: 0x04000DDF RID: 3551
		R32G32B32A32_SInt,
		// Token: 0x04000DE0 RID: 3552
		R16_SFloat,
		// Token: 0x04000DE1 RID: 3553
		R16G16_SFloat,
		// Token: 0x04000DE2 RID: 3554
		R16G16B16_SFloat,
		// Token: 0x04000DE3 RID: 3555
		R16G16B16A16_SFloat,
		// Token: 0x04000DE4 RID: 3556
		R32_SFloat,
		// Token: 0x04000DE5 RID: 3557
		R32G32_SFloat,
		// Token: 0x04000DE6 RID: 3558
		R32G32B32_SFloat,
		// Token: 0x04000DE7 RID: 3559
		R32G32B32A32_SFloat,
		// Token: 0x04000DE8 RID: 3560
		B8G8R8_SRGB = 56,
		// Token: 0x04000DE9 RID: 3561
		B8G8R8A8_SRGB,
		// Token: 0x04000DEA RID: 3562
		B8G8R8_UNorm,
		// Token: 0x04000DEB RID: 3563
		B8G8R8A8_UNorm,
		// Token: 0x04000DEC RID: 3564
		B8G8R8_SNorm,
		// Token: 0x04000DED RID: 3565
		B8G8R8A8_SNorm,
		// Token: 0x04000DEE RID: 3566
		B8G8R8_UInt,
		// Token: 0x04000DEF RID: 3567
		B8G8R8A8_UInt,
		// Token: 0x04000DF0 RID: 3568
		B8G8R8_SInt,
		// Token: 0x04000DF1 RID: 3569
		B8G8R8A8_SInt,
		// Token: 0x04000DF2 RID: 3570
		R4G4B4A4_UNormPack16,
		// Token: 0x04000DF3 RID: 3571
		B4G4R4A4_UNormPack16,
		// Token: 0x04000DF4 RID: 3572
		R5G6B5_UNormPack16,
		// Token: 0x04000DF5 RID: 3573
		B5G6R5_UNormPack16,
		// Token: 0x04000DF6 RID: 3574
		R5G5B5A1_UNormPack16,
		// Token: 0x04000DF7 RID: 3575
		B5G5R5A1_UNormPack16,
		// Token: 0x04000DF8 RID: 3576
		A1R5G5B5_UNormPack16,
		// Token: 0x04000DF9 RID: 3577
		E5B9G9R9_UFloatPack32,
		// Token: 0x04000DFA RID: 3578
		B10G11R11_UFloatPack32,
		// Token: 0x04000DFB RID: 3579
		A2B10G10R10_UNormPack32,
		// Token: 0x04000DFC RID: 3580
		A2B10G10R10_UIntPack32,
		// Token: 0x04000DFD RID: 3581
		A2B10G10R10_SIntPack32,
		// Token: 0x04000DFE RID: 3582
		A2R10G10B10_UNormPack32,
		// Token: 0x04000DFF RID: 3583
		A2R10G10B10_UIntPack32,
		// Token: 0x04000E00 RID: 3584
		A2R10G10B10_SIntPack32,
		// Token: 0x04000E01 RID: 3585
		A2R10G10B10_XRSRGBPack32,
		// Token: 0x04000E02 RID: 3586
		A2R10G10B10_XRUNormPack32,
		// Token: 0x04000E03 RID: 3587
		R10G10B10_XRSRGBPack32,
		// Token: 0x04000E04 RID: 3588
		R10G10B10_XRUNormPack32,
		// Token: 0x04000E05 RID: 3589
		A10R10G10B10_XRSRGBPack32,
		// Token: 0x04000E06 RID: 3590
		A10R10G10B10_XRUNormPack32,
		// Token: 0x04000E07 RID: 3591
		D16_UNorm = 90,
		// Token: 0x04000E08 RID: 3592
		D24_UNorm,
		// Token: 0x04000E09 RID: 3593
		D24_UNorm_S8_UInt,
		// Token: 0x04000E0A RID: 3594
		D32_SFloat,
		// Token: 0x04000E0B RID: 3595
		D32_SFloat_S8_UInt,
		// Token: 0x04000E0C RID: 3596
		S8_UInt,
		// Token: 0x04000E0D RID: 3597
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_SRGB has been deprecated. Use GraphicsFormat.RGBA_DXT1_SRGB instead (UnityUpgradable) -> RGBA_DXT1_SRGB", true)]
		RGB_DXT1_SRGB,
		// Token: 0x04000E0E RID: 3598
		RGBA_DXT1_SRGB = 96,
		// Token: 0x04000E0F RID: 3599
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_UNorm has been deprecated. Use GraphicsFormat.RGBA_DXT1_UNorm instead (UnityUpgradable) -> RGBA_DXT1_UNorm", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RGB_DXT1_UNorm,
		// Token: 0x04000E10 RID: 3600
		RGBA_DXT1_UNorm = 97,
		// Token: 0x04000E11 RID: 3601
		RGBA_DXT3_SRGB,
		// Token: 0x04000E12 RID: 3602
		RGBA_DXT3_UNorm,
		// Token: 0x04000E13 RID: 3603
		RGBA_DXT5_SRGB,
		// Token: 0x04000E14 RID: 3604
		RGBA_DXT5_UNorm,
		// Token: 0x04000E15 RID: 3605
		R_BC4_UNorm,
		// Token: 0x04000E16 RID: 3606
		R_BC4_SNorm,
		// Token: 0x04000E17 RID: 3607
		RG_BC5_UNorm,
		// Token: 0x04000E18 RID: 3608
		RG_BC5_SNorm,
		// Token: 0x04000E19 RID: 3609
		RGB_BC6H_UFloat,
		// Token: 0x04000E1A RID: 3610
		RGB_BC6H_SFloat,
		// Token: 0x04000E1B RID: 3611
		RGBA_BC7_SRGB,
		// Token: 0x04000E1C RID: 3612
		RGBA_BC7_UNorm,
		// Token: 0x04000E1D RID: 3613
		RGB_PVRTC_2Bpp_SRGB,
		// Token: 0x04000E1E RID: 3614
		RGB_PVRTC_2Bpp_UNorm,
		// Token: 0x04000E1F RID: 3615
		RGB_PVRTC_4Bpp_SRGB,
		// Token: 0x04000E20 RID: 3616
		RGB_PVRTC_4Bpp_UNorm,
		// Token: 0x04000E21 RID: 3617
		RGBA_PVRTC_2Bpp_SRGB,
		// Token: 0x04000E22 RID: 3618
		RGBA_PVRTC_2Bpp_UNorm,
		// Token: 0x04000E23 RID: 3619
		RGBA_PVRTC_4Bpp_SRGB,
		// Token: 0x04000E24 RID: 3620
		RGBA_PVRTC_4Bpp_UNorm,
		// Token: 0x04000E25 RID: 3621
		RGB_ETC_UNorm,
		// Token: 0x04000E26 RID: 3622
		RGB_ETC2_SRGB,
		// Token: 0x04000E27 RID: 3623
		RGB_ETC2_UNorm,
		// Token: 0x04000E28 RID: 3624
		RGB_A1_ETC2_SRGB,
		// Token: 0x04000E29 RID: 3625
		RGB_A1_ETC2_UNorm,
		// Token: 0x04000E2A RID: 3626
		RGBA_ETC2_SRGB,
		// Token: 0x04000E2B RID: 3627
		RGBA_ETC2_UNorm,
		// Token: 0x04000E2C RID: 3628
		R_EAC_UNorm,
		// Token: 0x04000E2D RID: 3629
		R_EAC_SNorm,
		// Token: 0x04000E2E RID: 3630
		RG_EAC_UNorm,
		// Token: 0x04000E2F RID: 3631
		RG_EAC_SNorm,
		// Token: 0x04000E30 RID: 3632
		RGBA_ASTC4X4_SRGB,
		// Token: 0x04000E31 RID: 3633
		RGBA_ASTC4X4_UNorm,
		// Token: 0x04000E32 RID: 3634
		RGBA_ASTC5X5_SRGB,
		// Token: 0x04000E33 RID: 3635
		RGBA_ASTC5X5_UNorm,
		// Token: 0x04000E34 RID: 3636
		RGBA_ASTC6X6_SRGB,
		// Token: 0x04000E35 RID: 3637
		RGBA_ASTC6X6_UNorm,
		// Token: 0x04000E36 RID: 3638
		RGBA_ASTC8X8_SRGB,
		// Token: 0x04000E37 RID: 3639
		RGBA_ASTC8X8_UNorm,
		// Token: 0x04000E38 RID: 3640
		RGBA_ASTC10X10_SRGB,
		// Token: 0x04000E39 RID: 3641
		RGBA_ASTC10X10_UNorm,
		// Token: 0x04000E3A RID: 3642
		RGBA_ASTC12X12_SRGB,
		// Token: 0x04000E3B RID: 3643
		RGBA_ASTC12X12_UNorm,
		// Token: 0x04000E3C RID: 3644
		YUV2,
		// Token: 0x04000E3D RID: 3645
		[Obsolete("Enum member GraphicsFormat.DepthAuto is obsolete. Use GraphicsFormat.None as a color format to indicate depth only rendering and DefaultFormat to get the default depth buffer format.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		DepthAuto,
		// Token: 0x04000E3E RID: 3646
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member GraphicsFormat.ShadowAuto is obsolete. Use GraphicsFormat.None as a color format to indicate depth only rendering, DefaultFormat to get the default shadow buffer format and ShadowSamplingMode.CompareDepths to enable shadowmap sampling.", true)]
		ShadowAuto,
		// Token: 0x04000E3F RID: 3647
		[Obsolete("Enum member GraphicsFormat.VideoAuto is obsolete. Use DefaultFormat instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		VideoAuto,
		// Token: 0x04000E40 RID: 3648
		RGBA_ASTC4X4_UFloat,
		// Token: 0x04000E41 RID: 3649
		RGBA_ASTC5X5_UFloat,
		// Token: 0x04000E42 RID: 3650
		RGBA_ASTC6X6_UFloat,
		// Token: 0x04000E43 RID: 3651
		RGBA_ASTC8X8_UFloat,
		// Token: 0x04000E44 RID: 3652
		RGBA_ASTC10X10_UFloat,
		// Token: 0x04000E45 RID: 3653
		RGBA_ASTC12X12_UFloat,
		// Token: 0x04000E46 RID: 3654
		D16_UNorm_S8_UInt
	}
}

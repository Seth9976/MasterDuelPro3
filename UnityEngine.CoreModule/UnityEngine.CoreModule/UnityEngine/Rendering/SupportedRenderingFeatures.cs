using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D0 RID: 976
	public class SupportedRenderingFeatures
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0003A6D4 File Offset: 0x000388D4
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0003A701 File Offset: 0x00038901
		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0003A70A File Offset: 0x0003890A
		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0003A712 File Offset: 0x00038912
		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes { get; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0003A71A File Offset: 0x0003891A
		public LightmapBakeType lightmapBakeTypes { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0003A722 File Offset: 0x00038922
		public LightmapsMode lightmapsModes { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0003A72A File Offset: 0x0003892A
		public bool enlighten { get; }

		// Token: 0x17000417 RID: 1047
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x0003A732 File Offset: 0x00038932
		public bool skyOcclusion
		{
			[CompilerGenerated]
			set
			{
				this.<skyOcclusion>k__BackingField = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0003A73B File Offset: 0x0003893B
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0003A743 File Offset: 0x00038943
		public bool rendersUIOverlay { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0003A74C File Offset: 0x0003894C
		public bool ambientProbeBaking { get; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0003A754 File Offset: 0x00038954
		public bool defaultReflectionProbeBaking { get; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0003A75C File Offset: 0x0003895C
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0003A764 File Offset: 0x00038964
		public bool overridesLightProbeSystem { get; set; }

		// Token: 0x1700041C RID: 1052
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x0003A76D File Offset: 0x0003896D
		public bool supportsHDR
		{
			[CompilerGenerated]
			set
			{
				this.<supportsHDR>k__BackingField = value;
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0003A778 File Offset: 0x00038978
		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* fallbackMode = (MixedLightingMode*)(void*)fallbackModePtr;
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				SupportedRenderingFeatures.LightmapMixedBakeModes lightmapMixedBakeModes = defaultMixedLightingModes;
				if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*fallbackMode = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*fallbackMode = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*fallbackMode = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*fallbackMode = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*fallbackMode = MixedLightingMode.Subtractive;
					}
					else
					{
						*fallbackMode = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0003A814 File Offset: 0x00038A14
		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool isSupported;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&isSupported)));
			return isSupported;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0003A838 File Offset: 0x00038A38
		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*isSupported = false;
			}
			else
			{
				*isSupported = (mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask);
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0003A8A0 File Offset: 0x00038AA0
		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool isSupported;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&isSupported)));
			return isSupported;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool isBakedSupported = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag2 = !isBakedSupported || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag2)
				{
					*isSupported = false;
					return;
				}
			}
			*isSupported = (SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType;
			bool flag3 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag3)
			{
				*isSupported = false;
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0003A938 File Offset: 0x00038B38
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			*isSupported = (SupportedRenderingFeatures.active.lightmapsModes & mode) == mode;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0003A960 File Offset: 0x00038B60
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			*isSupported = lightmapper != 0;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0003A97C File Offset: 0x00038B7C
		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			*isSupported = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		[RequiredByNativeCode]
		internal unsafe static void IsAmbientProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			*isSupported = SupportedRenderingFeatures.active.ambientProbeBaking;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0003A9C4 File Offset: 0x00038BC4
		[RequiredByNativeCode]
		internal unsafe static void IsDefaultReflectionProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* isSupported = (bool*)(void*)isSupportedPtr;
			*isSupported = SupportedRenderingFeatures.active.defaultReflectionProbeBaking;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0003A9E8 File Offset: 0x00038BE8
		[RequiredByNativeCode]
		internal unsafe static void OverridesLightProbeSystem(IntPtr overridesPtr)
		{
			bool* overrides = (bool*)(void*)overridesPtr;
			*overrides = SupportedRenderingFeatures.active.overridesLightProbeSystem;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0003AA0C File Offset: 0x00038C0C
		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* lightmapper = (int*)(void*)lightmapperPtr;
			*lightmapper = 1;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0003AA24 File Offset: 0x00038C24
		public SupportedRenderingFeatures()
		{
			this.<reflectionProbeModes>k__BackingField = SupportedRenderingFeatures.ReflectionProbeModes.None;
			this.defaultMixedLightingModes = SupportedRenderingFeatures.LightmapMixedBakeModes.None;
			this.mixedLightingModes = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask;
			this.lightmapBakeTypes = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;
			this.lightmapsModes = LightmapsMode.CombinedDirectional;
			this.<enlightenLightmapper>k__BackingField = false;
			this.enlighten = true;
			this.skyOcclusion = false;
			this.<lightProbeProxyVolumes>k__BackingField = true;
			this.<motionVectors>k__BackingField = true;
			this.<receiveShadows>k__BackingField = true;
			this.<reflectionProbes>k__BackingField = true;
			this.<reflectionProbesBlendDistance>k__BackingField = true;
			this.<rendererPriority>k__BackingField = false;
			this.rendersUIOverlay = false;
			this.<overridesEnvironmentLighting>k__BackingField = false;
			this.<overridesFog>k__BackingField = false;
			this.<overridesRealtimeReflectionProbes>k__BackingField = false;
			this.<overridesOtherLightingSettings>k__BackingField = false;
			this.<editableMaterialRenderQueue>k__BackingField = true;
			this.<overridesLODBias>k__BackingField = false;
			this.<overridesMaximumLODLevel>k__BackingField = false;
			this.<overridesEnableLODCrossFade>k__BackingField = false;
			this.<rendererProbes>k__BackingField = true;
			this.<particleSystemInstancing>k__BackingField = true;
			this.ambientProbeBaking = true;
			this.defaultReflectionProbeBaking = true;
			this.<overridesShadowmask>k__BackingField = false;
			this.overridesLightProbeSystem = false;
			this.supportsHDR = false;
			this.<supportsClouds>k__BackingField = false;
			this.<overridesLightProbeSystemWarningMessage>k__BackingField = "Light Probe Groups are unavailable as Probe Volumes have been enabled by the current Render Pipeline.";
			base..ctor();
		}

		// Token: 0x04000CA7 RID: 3239
		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		// Token: 0x020003D1 RID: 977
		[Flags]
		public enum ReflectionProbeModes
		{
			// Token: 0x04000CC9 RID: 3273
			None = 0,
			// Token: 0x04000CCA RID: 3274
			Rotation = 1
		}

		// Token: 0x020003D2 RID: 978
		[Flags]
		public enum LightmapMixedBakeModes
		{
			// Token: 0x04000CCC RID: 3276
			None = 0,
			// Token: 0x04000CCD RID: 3277
			IndirectOnly = 1,
			// Token: 0x04000CCE RID: 3278
			Subtractive = 2,
			// Token: 0x04000CCF RID: 3279
			Shadowmask = 4
		}
	}
}

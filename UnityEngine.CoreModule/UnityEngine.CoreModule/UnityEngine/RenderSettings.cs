using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000EE RID: 238
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/QualitySettingsTypes.h")]
	public sealed class RenderSettings : Object
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00004DC7 File Offset: 0x00002FC7
		private RenderSettings()
		{
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006B9 RID: 1721
		// (set) Token: 0x060006BA RID: 1722
		[NativeProperty("UseFog")]
		public static extern bool fog
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006BB RID: 1723
		// (set) Token: 0x060006BC RID: 1724
		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006BD RID: 1725
		// (set) Token: 0x060006BE RID: 1726
		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006BF RID: 1727
		// (set) Token: 0x060006C0 RID: 1728
		public static extern FogMode fogMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		public static Color fogColor
		{
			get
			{
				Color color;
				RenderSettings.get_fogColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_fogColor_Injected(ref value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006C3 RID: 1731
		// (set) Token: 0x060006C4 RID: 1732
		public static extern float fogDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006C5 RID: 1733
		// (set) Token: 0x060006C6 RID: 1734
		public static extern AmbientMode ambientMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000DB00 File Offset: 0x0000BD00
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public static Color ambientSkyColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientSkyColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientSkyColor_Injected(ref value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public static Color ambientEquatorColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientEquatorColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientEquatorColor_Injected(ref value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0000DB58 File Offset: 0x0000BD58
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public static Color ambientGroundColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientGroundColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientGroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006CD RID: 1741
		// (set) Token: 0x060006CE RID: 1742
		public static extern float ambientIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000DB84 File Offset: 0x0000BD84
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				Color color;
				RenderSettings.get_ambientLight_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientLight_Injected(ref value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		public static Color subtractiveShadowColor
		{
			get
			{
				Color color;
				RenderSettings.get_subtractiveShadowColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_subtractiveShadowColor_Injected(ref value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		[NativeProperty("SkyboxMaterial")]
		public static Material skybox
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(RenderSettings.get_skybox_Injected());
			}
			set
			{
				RenderSettings.set_skybox_Injected(Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0000DC24 File Offset: 0x0000BE24
		public static Light sun
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Light>(RenderSettings.get_sun_Injected());
			}
			set
			{
				RenderSettings.set_sun_Injected(Object.MarshalledUnityObject.Marshal<Light>(value));
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public static SphericalHarmonicsL2 ambientProbe
		{
			[NativeMethod("GetFinalAmbientProbe")]
			get
			{
				SphericalHarmonicsL2 sphericalHarmonicsL;
				RenderSettings.get_ambientProbe_Injected(out sphericalHarmonicsL);
				return sphericalHarmonicsL;
			}
			set
			{
				RenderSettings.set_ambientProbe_Injected(ref value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000DC68 File Offset: 0x0000BE68
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0000DC9D File Offset: 0x0000BE9D
		[Obsolete("RenderSettings.customReflection has been deprecated in favor of RenderSettings.customReflectionTexture.", false)]
		public static Cubemap customReflection
		{
			get
			{
				Cubemap cube = RenderSettings.customReflectionTexture as Cubemap;
				bool flag = cube == null;
				if (flag)
				{
					throw new ArgumentException("RenderSettings.customReflection is currently not referencing a cubemap.");
				}
				return cube;
			}
			[NativeThrows]
			set
			{
				RenderSettings.customReflectionTexture = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		[NativeProperty("CustomReflection")]
		public static Texture customReflectionTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture>(RenderSettings.get_customReflectionTexture_Injected());
			}
			[NativeThrows]
			set
			{
				RenderSettings.set_customReflectionTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006DD RID: 1757
		// (set) Token: 0x060006DE RID: 1758
		public static extern float reflectionIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006DF RID: 1759
		// (set) Token: 0x060006E0 RID: 1760
		public static extern int reflectionBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		[NativeProperty("GeneratedSkyboxReflection")]
		internal static Cubemap defaultReflection
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Cubemap>(RenderSettings.get_defaultReflection_Injected());
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006E2 RID: 1762
		// (set) Token: 0x060006E3 RID: 1763
		public static extern DefaultReflectionMode defaultReflectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006E4 RID: 1764
		// (set) Token: 0x060006E5 RID: 1765
		public static extern int defaultReflectionResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006E6 RID: 1766
		// (set) Token: 0x060006E7 RID: 1767
		public static extern float haloStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006E8 RID: 1768
		// (set) Token: 0x060006E9 RID: 1769
		public static extern float flareStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006EA RID: 1770
		// (set) Token: 0x060006EB RID: 1771
		public static extern float flareFadeSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		[FreeFunction("GetRenderSettings")]
		internal static Object GetRenderSettings()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(RenderSettings.GetRenderSettings_Injected());
		}

		// Token: 0x060006ED RID: 1773
		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x060006EE RID: 1774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_fogColor_Injected(out Color ret);

		// Token: 0x060006EF RID: 1775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fogColor_Injected([In] ref Color value);

		// Token: 0x060006F0 RID: 1776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		// Token: 0x060006F1 RID: 1777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientSkyColor_Injected([In] ref Color value);

		// Token: 0x060006F2 RID: 1778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		// Token: 0x060006F3 RID: 1779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientEquatorColor_Injected([In] ref Color value);

		// Token: 0x060006F4 RID: 1780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		// Token: 0x060006F5 RID: 1781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientGroundColor_Injected([In] ref Color value);

		// Token: 0x060006F6 RID: 1782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientLight_Injected(out Color ret);

		// Token: 0x060006F7 RID: 1783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientLight_Injected([In] ref Color value);

		// Token: 0x060006F8 RID: 1784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		// Token: 0x060006F9 RID: 1785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subtractiveShadowColor_Injected([In] ref Color value);

		// Token: 0x060006FA RID: 1786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_skybox_Injected();

		// Token: 0x060006FB RID: 1787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skybox_Injected(IntPtr value);

		// Token: 0x060006FC RID: 1788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sun_Injected();

		// Token: 0x060006FD RID: 1789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sun_Injected(IntPtr value);

		// Token: 0x060006FE RID: 1790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		// Token: 0x060006FF RID: 1791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientProbe_Injected([In] ref SphericalHarmonicsL2 value);

		// Token: 0x06000700 RID: 1792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_customReflectionTexture_Injected();

		// Token: 0x06000701 RID: 1793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_customReflectionTexture_Injected(IntPtr value);

		// Token: 0x06000702 RID: 1794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_defaultReflection_Injected();

		// Token: 0x06000703 RID: 1795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRenderSettings_Injected();
	}
}

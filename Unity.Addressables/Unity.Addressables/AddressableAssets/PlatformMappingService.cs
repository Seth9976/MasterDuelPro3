using System;
using System.Collections.Generic;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000041 RID: 65
	public class PlatformMappingService
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00007109 File Offset: 0x00005309
		internal static AddressablesPlatform GetAddressablesPlatformInternal(RuntimePlatform platform)
		{
			if (PlatformMappingService.s_RuntimeTargetMapping.ContainsKey(platform))
			{
				return PlatformMappingService.s_RuntimeTargetMapping[platform];
			}
			return AddressablesPlatform.Unknown;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007128 File Offset: 0x00005328
		internal static string GetAddressablesPlatformPathInternal(RuntimePlatform platform)
		{
			if (PlatformMappingService.s_RuntimeTargetMapping.ContainsKey(platform))
			{
				return PlatformMappingService.s_RuntimeTargetMapping[platform].ToString();
			}
			return platform.ToString();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007169 File Offset: 0x00005369
		public static string GetPlatformPathSubFolder()
		{
			return PlatformMappingService.GetAddressablesPlatformPathInternal(Application.platform);
		}

		// Token: 0x040000D8 RID: 216
		internal static readonly Dictionary<RuntimePlatform, AddressablesPlatform> s_RuntimeTargetMapping = new Dictionary<RuntimePlatform, AddressablesPlatform>
		{
			{
				RuntimePlatform.XboxOne,
				AddressablesPlatform.XboxOne
			},
			{
				RuntimePlatform.Switch,
				AddressablesPlatform.Switch
			},
			{
				RuntimePlatform.PS4,
				AddressablesPlatform.PS4
			},
			{
				RuntimePlatform.IPhonePlayer,
				AddressablesPlatform.iOS
			},
			{
				RuntimePlatform.Android,
				AddressablesPlatform.Android
			},
			{
				RuntimePlatform.WebGLPlayer,
				AddressablesPlatform.WebGL
			},
			{
				RuntimePlatform.WindowsPlayer,
				AddressablesPlatform.Windows
			},
			{
				RuntimePlatform.OSXPlayer,
				AddressablesPlatform.OSX
			},
			{
				RuntimePlatform.LinuxPlayer,
				AddressablesPlatform.Linux
			},
			{
				RuntimePlatform.WindowsEditor,
				AddressablesPlatform.Windows
			},
			{
				RuntimePlatform.OSXEditor,
				AddressablesPlatform.OSX
			},
			{
				RuntimePlatform.LinuxEditor,
				AddressablesPlatform.Linux
			},
			{
				RuntimePlatform.MetroPlayerARM,
				AddressablesPlatform.WindowsUniversal
			},
			{
				RuntimePlatform.MetroPlayerX64,
				AddressablesPlatform.WindowsUniversal
			},
			{
				RuntimePlatform.MetroPlayerX86,
				AddressablesPlatform.WindowsUniversal
			}
		};
	}
}

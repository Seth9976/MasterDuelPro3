using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D4 RID: 468
	internal static class PlatformAutoDetect
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x000335A1 File Offset: 0x000317A1
		internal static void Initialize()
		{
			PlatformAutoDetect.isXRMobile = false;
			PlatformAutoDetect.isShaderAPIMobileDefined = GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.SHADER_API_MOBILE);
			PlatformAutoDetect.isSwitch = Application.platform == RuntimePlatform.Switch;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000335C3 File Offset: 0x000317C3
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x000335CA File Offset: 0x000317CA
		internal static bool isXRMobile { get; private set; } = false;

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x000335D2 File Offset: 0x000317D2
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x000335D9 File Offset: 0x000317D9
		internal static bool isShaderAPIMobileDefined { get; private set; } = false;

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x000335E1 File Offset: 0x000317E1
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x000335E8 File Offset: 0x000317E8
		internal static bool isSwitch { get; private set; } = false;

		// Token: 0x06000A5C RID: 2652 RVA: 0x000335F0 File Offset: 0x000317F0
		internal static ShEvalMode ShAutoDetect(ShEvalMode mode)
		{
			if (mode != ShEvalMode.Auto)
			{
				return mode;
			}
			if (PlatformAutoDetect.isXRMobile || PlatformAutoDetect.isShaderAPIMobileDefined || PlatformAutoDetect.isSwitch)
			{
				return ShEvalMode.PerVertex;
			}
			return ShEvalMode.PerPixel;
		}

		// Token: 0x04000B4D RID: 2893
		internal static bool isRunningOnPowerVRGPU = SystemInfo.graphicsDeviceName.Contains("PowerVR");
	}
}

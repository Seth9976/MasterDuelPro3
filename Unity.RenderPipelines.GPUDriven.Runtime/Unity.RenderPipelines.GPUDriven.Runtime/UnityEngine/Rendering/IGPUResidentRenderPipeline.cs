using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000032 RID: 50
	public interface IGPUResidentRenderPipeline
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F1 RID: 241
		GPUResidentDrawerSettings gpuResidentDrawerSettings { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		GPUResidentDrawerMode gpuResidentDrawerMode { get; set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x000059C3 File Offset: 0x00003BC3
		public static void ReinitializeGPUResidentDrawer()
		{
			GPUResidentDrawer.Reinitialize();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000059CC File Offset: 0x00003BCC
		bool IsGPUResidentDrawerSupportedBySRP(bool logReason = false)
		{
			string message;
			LogType severity;
			bool supported = this.IsGPUResidentDrawerSupportedBySRP(out message, out severity);
			if (logReason && !supported)
			{
				GPUResidentDrawer.LogMessage(message, severity);
			}
			return supported;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000059F2 File Offset: 0x00003BF2
		bool IsGPUResidentDrawerSupportedBySRP(out string message, out LogType severity)
		{
			message = string.Empty;
			severity = LogType.Log;
			return true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005A00 File Offset: 0x00003C00
		public static bool IsGPUResidentDrawerSupportedByProjectConfiguration(bool logReason = false)
		{
			string message;
			LogType severity;
			bool flag = GPUResidentDrawer.IsProjectSupported(out message, out severity);
			if (logReason && !string.IsNullOrEmpty(message))
			{
				Debug.LogWarning(message);
			}
			return flag;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000033F8 File Offset: 0x000015F8
		public static bool IsGPUResidentDrawerEnabled()
		{
			return GPUResidentDrawer.IsEnabled();
		}
	}
}

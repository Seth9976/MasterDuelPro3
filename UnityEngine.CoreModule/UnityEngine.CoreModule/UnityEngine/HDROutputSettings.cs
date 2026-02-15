using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E3 RID: 227
	[UsedByNativeCode]
	[NativeHeader("Runtime/GfxDevice/HDROutputSettings.h")]
	public class HDROutputSettings
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0000CAB1 File Offset: 0x0000ACB1
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal HDROutputSettings()
		{
			this.m_DisplayIndex = 0;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal HDROutputSettings(int displayIndex)
		{
			this.m_DisplayIndex = displayIndex;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public static HDROutputSettings main
		{
			get
			{
				return HDROutputSettings._mainDisplay;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public bool active
		{
			get
			{
				return HDROutputSettings.GetActive(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public bool available
		{
			get
			{
				return HDROutputSettings.GetAvailable(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000CB49 File Offset: 0x0000AD49
		public bool automaticHDRTonemapping
		{
			get
			{
				return HDROutputSettings.GetAutomaticHDRTonemapping(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetAutomaticHDRTonemapping(this.m_DisplayIndex, value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public ColorGamut displayColorGamut
		{
			get
			{
				return HDROutputSettings.GetDisplayColorGamut(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public float paperWhiteNits
		{
			get
			{
				return HDROutputSettings.GetPaperWhiteNits(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public int maxFullFrameToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxFullFrameToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public int maxToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public int minToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMinToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public bool HDRModeChangeRequested
		{
			get
			{
				return HDROutputSettings.GetHDRModeChangeRequested(this.m_DisplayIndex);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000CC39 File Offset: 0x0000AE39
		public void RequestHDRModeChange(bool enabled)
		{
			HDROutputSettings.RequestHDRModeChangeInternal(this.m_DisplayIndex, enabled);
		}

		// Token: 0x060005F5 RID: 1525
		[FreeFunction("HDROutputSettingsBindings::GetActive", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActive(int displayIndex);

		// Token: 0x060005F6 RID: 1526
		[FreeFunction("HDROutputSettingsBindings::GetAvailable", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAvailable(int displayIndex);

		// Token: 0x060005F7 RID: 1527
		[FreeFunction("HDROutputSettingsBindings::GetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAutomaticHDRTonemapping(int displayIndex);

		// Token: 0x060005F8 RID: 1528
		[FreeFunction("HDROutputSettingsBindings::SetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutomaticHDRTonemapping(int displayIndex, bool scripted);

		// Token: 0x060005F9 RID: 1529
		[FreeFunction("HDROutputSettingsBindings::GetDisplayColorGamut", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorGamut GetDisplayColorGamut(int displayIndex);

		// Token: 0x060005FA RID: 1530
		[FreeFunction("HDROutputSettingsBindings::GetGraphicsFormat", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat(int displayIndex);

		// Token: 0x060005FB RID: 1531
		[FreeFunction("HDROutputSettingsBindings::GetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPaperWhiteNits(int displayIndex);

		// Token: 0x060005FC RID: 1532
		[FreeFunction("HDROutputSettingsBindings::GetMaxFullFrameToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxFullFrameToneMapLuminance(int displayIndex);

		// Token: 0x060005FD RID: 1533
		[FreeFunction("HDROutputSettingsBindings::GetMaxToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxToneMapLuminance(int displayIndex);

		// Token: 0x060005FE RID: 1534
		[FreeFunction("HDROutputSettingsBindings::GetMinToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMinToneMapLuminance(int displayIndex);

		// Token: 0x060005FF RID: 1535
		[FreeFunction("HDROutputSettingsBindings::GetHDRModeChangeRequested", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHDRModeChangeRequested(int displayIndex);

		// Token: 0x06000600 RID: 1536
		[FreeFunction("HDROutputSettingsBindings::RequestHDRModeChange", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestHDRModeChangeInternal(int displayIndex, bool enabled);

		// Token: 0x040002A7 RID: 679
		private int m_DisplayIndex;

		// Token: 0x040002A8 RID: 680
		public static HDROutputSettings[] displays = new HDROutputSettings[]
		{
			new HDROutputSettings()
		};

		// Token: 0x040002A9 RID: 681
		private static HDROutputSettings _mainDisplay = HDROutputSettings.displays[0];
	}
}

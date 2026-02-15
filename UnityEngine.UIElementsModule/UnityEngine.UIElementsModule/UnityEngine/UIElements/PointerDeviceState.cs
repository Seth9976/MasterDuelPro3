using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000217 RID: 535
	internal static class PointerDeviceState
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x00040C1C File Offset: 0x0003EE1C
		internal static void RemovePanelData(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				bool flag = PointerDeviceState.s_PlayerPointerLocations[i].Panel == panel;
				if (flag)
				{
					PointerDeviceState.s_PlayerPointerLocations[i].SetLocation(Vector2.zero, null);
				}
				bool flag2 = PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] == panel;
				if (flag2)
				{
					PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] = null;
				}
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00040C84 File Offset: 0x0003EE84
		public static void SavePointerPosition(int pointerId, Vector2 position, IPanel panel, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			PointerDeviceState.s_PlayerPointerLocations[pointerId].SetLocation(position, panel);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00040CB1 File Offset: 0x0003EEB1
		public static void PressButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0, "PressButton expects buttonId >= 0");
			Debug.Assert(buttonId < 32, "PressButton expects buttonId < 32");
			PointerDeviceState.s_PressedButtons[pointerId] |= 1 << buttonId;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00040CEB File Offset: 0x0003EEEB
		public static void ReleaseButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0, "ReleaseButton expects buttonId >= 0");
			Debug.Assert(buttonId < 32, "ReleaseButton expects buttonId < 32");
			PointerDeviceState.s_PressedButtons[pointerId] &= ~(1 << buttonId);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00040D26 File Offset: 0x0003EF26
		public static void ReleaseAllButtons(int pointerId)
		{
			PointerDeviceState.s_PressedButtons[pointerId] = 0;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00040D34 File Offset: 0x0003EF34
		public static Vector2 GetPointerPosition(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Position;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00040D60 File Offset: 0x0003EF60
		public static IPanel GetPanel(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Panel;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00040D8C File Offset: 0x0003EF8C
		private static bool HasFlagFast(PointerDeviceState.LocationFlag flagSet, PointerDeviceState.LocationFlag flag)
		{
			return (flagSet & flag) == flag;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00040DA4 File Offset: 0x0003EFA4
		public static bool HasLocationFlag(int pointerId, ContextType contextType, PointerDeviceState.LocationFlag flag)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.HasFlagFast(PointerDeviceState.s_PlayerPointerLocations[pointerId].Flags, flag);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00040DD8 File Offset: 0x0003EFD8
		public static int GetPressedButtons(int pointerId)
		{
			return PointerDeviceState.s_PressedButtons[pointerId];
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		internal static bool HasAdditionalPressedButtons(int pointerId, int exceptButtonId)
		{
			return (PointerDeviceState.s_PressedButtons[pointerId] & ~(1 << exceptButtonId)) != 0;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00040E18 File Offset: 0x0003F018
		internal static void SetPlayerPanelWithSoftPointerCapture(int pointerId, IPanel panel)
		{
			PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId] = panel;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00040E24 File Offset: 0x0003F024
		internal static IPanel GetPlayerPanelWithSoftPointerCapture(int pointerId)
		{
			return PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId];
		}

		// Token: 0x04000883 RID: 2179
		private static PointerDeviceState.PointerLocation[] s_PlayerPointerLocations = new PointerDeviceState.PointerLocation[PointerId.maxPointers];

		// Token: 0x04000884 RID: 2180
		private static int[] s_PressedButtons = new int[PointerId.maxPointers];

		// Token: 0x04000885 RID: 2181
		private static readonly IPanel[] s_PlayerPanelWithSoftPointerCapture = new IPanel[PointerId.maxPointers];

		// Token: 0x02000218 RID: 536
		[Flags]
		internal enum LocationFlag
		{
			// Token: 0x04000887 RID: 2183
			None = 0,
			// Token: 0x04000888 RID: 2184
			OutsidePanel = 1
		}

		// Token: 0x02000219 RID: 537
		private struct PointerLocation
		{
			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00040E6C File Offset: 0x0003F06C
			// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00040E74 File Offset: 0x0003F074
			internal Vector2 Position { readonly get; private set; }

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00040E7D File Offset: 0x0003F07D
			// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00040E85 File Offset: 0x0003F085
			internal IPanel Panel { readonly get; private set; }

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00040E8E File Offset: 0x0003F08E
			// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00040E96 File Offset: 0x0003F096
			internal PointerDeviceState.LocationFlag Flags { readonly get; private set; }

			// Token: 0x06000E9F RID: 3743 RVA: 0x00040EA0 File Offset: 0x0003F0A0
			internal void SetLocation(Vector2 position, IPanel panel)
			{
				this.Position = position;
				this.Panel = panel;
				this.Flags = PointerDeviceState.LocationFlag.None;
				bool flag = panel == null || !panel.visualTree.layout.Contains(position);
				if (flag)
				{
					this.Flags |= PointerDeviceState.LocationFlag.OutsidePanel;
				}
			}
		}
	}
}

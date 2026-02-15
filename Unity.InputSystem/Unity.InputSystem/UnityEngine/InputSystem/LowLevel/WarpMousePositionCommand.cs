using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200018C RID: 396
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct WarpMousePositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0004DC69 File Offset: 0x0004BE69
		public static FourCC Type
		{
			get
			{
				return new FourCC('W', 'P', 'M', 'S');
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0004DC78 File Offset: 0x0004BE78
		public FourCC typeStatic
		{
			get
			{
				return WarpMousePositionCommand.Type;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004DC80 File Offset: 0x0004BE80
		public static WarpMousePositionCommand Create(Vector2 position)
		{
			return new WarpMousePositionCommand
			{
				baseCommand = new InputDeviceCommand(WarpMousePositionCommand.Type, 16),
				warpPositionInPlayerDisplaySpace = position
			};
		}

		// Token: 0x04000960 RID: 2400
		internal const int kSize = 16;

		// Token: 0x04000961 RID: 2401
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000962 RID: 2402
		[FieldOffset(8)]
		public Vector2 warpPositionInPlayerDisplaySpace;
	}
}

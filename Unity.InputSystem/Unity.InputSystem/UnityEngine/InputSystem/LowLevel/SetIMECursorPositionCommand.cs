using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200018A RID: 394
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct SetIMECursorPositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'P');
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0004DBDF File Offset: 0x0004BDDF
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0004DBE7 File Offset: 0x0004BDE7
		public FourCC typeStatic
		{
			get
			{
				return SetIMECursorPositionCommand.Type;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
		public static SetIMECursorPositionCommand Create(Vector2 cursorPosition)
		{
			return new SetIMECursorPositionCommand
			{
				baseCommand = new InputDeviceCommand(SetIMECursorPositionCommand.Type, 16),
				m_Position = cursorPosition
			};
		}

		// Token: 0x0400095A RID: 2394
		internal const int kSize = 16;

		// Token: 0x0400095B RID: 2395
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400095C RID: 2396
		[FieldOffset(8)]
		private Vector2 m_Position;
	}
}

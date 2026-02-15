using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct EnableIMECompositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0004D630 File Offset: 0x0004B830
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'M');
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0004D63F File Offset: 0x0004B83F
		public bool imeEnabled
		{
			get
			{
				return this.m_ImeEnabled > 0;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0004D64A File Offset: 0x0004B84A
		public FourCC typeStatic
		{
			get
			{
				return EnableIMECompositionCommand.Type;
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0004D654 File Offset: 0x0004B854
		public static EnableIMECompositionCommand Create(bool enabled)
		{
			return new EnableIMECompositionCommand
			{
				baseCommand = new InputDeviceCommand(EnableIMECompositionCommand.Type, 9),
				m_ImeEnabled = (enabled ? byte.MaxValue : 0)
			};
		}

		// Token: 0x0400091E RID: 2334
		internal const int kSize = 12;

		// Token: 0x0400091F RID: 2335
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000920 RID: 2336
		[FieldOffset(8)]
		private byte m_ImeEnabled;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000203 RID: 515
	internal class WindowsConsoleDriver : IConsoleDriver
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x0004F7F0 File Offset: 0x0004D9F0
		public WindowsConsoleDriver()
		{
			this.outputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_OUTPUT);
			this.inputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_INPUT);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			this.defaultAttribute = consoleScreenBufferInfo.Attribute;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004F83F File Offset: 0x0004DA3F
		private static short GetAttrForeground(int attr, ConsoleColor color)
		{
			attr &= -16;
			return (short)(attr | (int)color);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004F84C File Offset: 0x0004DA4C
		private static short GetAttrBackground(int attr, ConsoleColor color)
		{
			attr &= -241;
			int num = (int)((int)color << 4);
			return (short)(attr | num);
		}

		// Token: 0x170001FF RID: 511
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004F86C File Offset: 0x0004DA6C
		public ConsoleColor BackgroundColor
		{
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrBackground = WindowsConsoleDriver.GetAttrBackground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrBackground);
			}
		}

		// Token: 0x17000200 RID: 512
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x0004F8AC File Offset: 0x0004DAAC
		public ConsoleColor ForegroundColor
		{
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrForeground = WindowsConsoleDriver.GetAttrForeground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrForeground);
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004F8EC File Offset: 0x0004DAEC
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			InputRecord inputRecord = default(InputRecord);
			int num;
			while (WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
			{
				if (inputRecord.KeyDown && inputRecord.EventType == 1 && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
				{
					bool flag = (inputRecord.ControlKeyState & 3) != 0;
					bool flag2 = (inputRecord.ControlKeyState & 12) != 0;
					bool flag3 = (inputRecord.ControlKeyState & 16) != 0;
					return new ConsoleKeyInfo(inputRecord.Character, (ConsoleKey)inputRecord.VirtualKeyCode, flag3, flag, flag2);
				}
			}
			throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error().ToString());
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004F98B File Offset: 0x0004DB8B
		public void ResetColor()
		{
			WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, this.defaultAttribute);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004F99F File Offset: 0x0004DB9F
		private static bool IsModifierKey(short virtualKeyCode)
		{
			return virtualKeyCode - 16 <= 2 || virtualKeyCode == 20 || virtualKeyCode - 144 <= 1;
		}

		// Token: 0x06001381 RID: 4993
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetStdHandle(Handles handle);

		// Token: 0x06001382 RID: 4994
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

		// Token: 0x06001383 RID: 4995
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr handle, short attribute);

		// Token: 0x06001384 RID: 4996
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ReadConsoleInput(IntPtr handle, out InputRecord record, int length, out int nread);

		// Token: 0x040009B2 RID: 2482
		private IntPtr inputHandle;

		// Token: 0x040009B3 RID: 2483
		private IntPtr outputHandle;

		// Token: 0x040009B4 RID: 2484
		private short defaultAttribute;
	}
}

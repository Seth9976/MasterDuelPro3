using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200023C RID: 572
	internal class X11Keyboard : IDisposable
	{
		// Token: 0x060016EA RID: 5866 RVA: 0x00072228 File Offset: 0x00070428
		public X11Keyboard(IntPtr display, IntPtr clientWindow)
		{
			this.display = display;
			this.lookup_buffer = new StringBuilder(24);
			this.EnsureLayoutInitialized();
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x000722AB File Offset: 0x000704AB
		private Encoding AnsiEncoding
		{
			get
			{
				if (this.encoding == null)
				{
					this.encoding = Encoding.GetEncoding(new CultureInfo(this.lcid).TextInfo.ANSICodePage);
				}
				return this.encoding;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x000722DB File Offset: 0x000704DB
		public IntPtr ClientWindow
		{
			get
			{
				return this.client_window;
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000722E4 File Offset: 0x000704E4
		void IDisposable.Dispose()
		{
			if (this.xim != IntPtr.Zero)
			{
				foreach (object obj in this.xic_table.Values)
				{
					X11Keyboard.XDestroyIC((IntPtr)obj);
				}
				this.xic_table.Clear();
				X11Keyboard.XCloseIM(this.xim);
				this.xim = IntPtr.Zero;
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00072374 File Offset: 0x00070574
		public void DestroyICForWindow(IntPtr window)
		{
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				this.xic_table.Remove((long)window);
				X11Keyboard.XDestroyIC(xic);
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000723B4 File Offset: 0x000705B4
		public void EnsureLayoutInitialized()
		{
			if (this.initialized)
			{
				return;
			}
			KeyboardLayouts keyboardLayouts = new KeyboardLayouts();
			KeyboardLayout keyboardLayout = this.DetectLayout(keyboardLayouts);
			this.lcid = keyboardLayout.Lcid;
			this.CreateConversionArray(keyboardLayouts, keyboardLayout);
			this.SetupXIM();
			this.initialized = true;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000723FC File Offset: 0x000705FC
		private void SetupXIM()
		{
			this.xim = IntPtr.Zero;
			if (!X11Keyboard.XSupportsLocale())
			{
				Console.Error.WriteLine("X does not support your locale");
				return;
			}
			if (!X11Keyboard.XSetLocaleModifiers(string.Empty))
			{
				Console.Error.WriteLine("Could not set X locale modifiers");
				return;
			}
			if (Environment.GetEnvironmentVariable("MONO_WINFORMS_XIM_STYLE") == "disabled")
			{
				return;
			}
			this.xim = X11Keyboard.XOpenIM(this.display, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this.xim == IntPtr.Zero)
			{
				Console.Error.WriteLine("Could not get XIM");
			}
			this.initialized = true;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000724A8 File Offset: 0x000706A8
		private void CreateXicForWindow(IntPtr window)
		{
			IntPtr intPtr = this.CreateXic(window, this.xim);
			this.xic_table[(long)window] = intPtr;
			if (intPtr == IntPtr.Zero)
			{
				Console.Error.WriteLine("Could not get XIC");
				return;
			}
			if (X11Keyboard.XGetICValues(intPtr, "filterEvents", out this.xic_event_mask, IntPtr.Zero) != null)
			{
				Console.Error.WriteLine("Could not get XIC values");
			}
			EventMask eventMask = EventMask.KeyPressMask | EventMask.ExposureMask | EventMask.FocusChangeMask;
			if ((this.xic_event_mask | eventMask) == this.xic_event_mask)
			{
				this.xic_event_mask |= eventMask;
				object xlibLock = X11Keyboard.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XSelectInput(this.display, window, new IntPtr((int)this.xic_event_mask));
				}
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0007258C File Offset: 0x0007078C
		public EventMask KeyEventMask
		{
			get
			{
				return this.xic_event_mask;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00072594 File Offset: 0x00070794
		public Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if ((this.key_state_table[16] & 128) != 0)
				{
					keys |= Keys.Shift;
				}
				if ((this.key_state_table[17] & 128) != 0)
				{
					keys |= Keys.Control;
				}
				if ((this.key_state_table[18] & 128) != 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000725F0 File Offset: 0x000707F0
		private IntPtr GetXic(IntPtr window)
		{
			if (this.xim != IntPtr.Zero && this.xic_table.ContainsKey((long)window))
			{
				return (IntPtr)this.xic_table[(long)window];
			}
			return IntPtr.Zero;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00072648 File Offset: 0x00070848
		private bool FilterKey(XEvent e, int vkey)
		{
			if (XplatUI.key_filters.Count == 0)
			{
				return false;
			}
			KeyFilterData keyFilterData;
			keyFilterData.Down = e.type == XEventName.KeyPress;
			keyFilterData.ModifierKeys = this.ModifierKeys;
			XKeySym xkeySym;
			XLookupStatus xlookupStatus;
			this.LookupString(ref e, 0, out xkeySym, out xlookupStatus);
			keyFilterData.keysym = (int)xkeySym;
			keyFilterData.keycode = e.KeyEvent.keycode;
			keyFilterData.str = this.lookup_buffer.ToString(0, this.lookup_buffer.Length);
			return XplatUI.FilterKey(keyFilterData);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000726CC File Offset: 0x000708CC
		public void FocusIn(IntPtr window)
		{
			this.client_window = window;
			if (this.xim == IntPtr.Zero)
			{
				return;
			}
			if (!this.xic_table.ContainsKey((long)window))
			{
				this.CreateXicForWindow(window);
			}
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				X11Keyboard.XSetICFocus(xic);
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00072730 File Offset: 0x00070930
		public void FocusOut(IntPtr window)
		{
			this.client_window = IntPtr.Zero;
			if (this.xim == IntPtr.Zero)
			{
				return;
			}
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				if (this.have_Xutf8ResetIC)
				{
					try
					{
						X11Keyboard.Xutf8ResetIC(xic);
					}
					catch (EntryPointNotFoundException)
					{
						this.have_Xutf8ResetIC = false;
					}
				}
				X11Keyboard.XUnsetICFocus(xic);
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000727A4 File Offset: 0x000709A4
		public bool ResetKeyState(IntPtr hwnd, ref MSG msg)
		{
			if ((this.key_state_table[16] & 128) != 0)
			{
				byte[] array = this.key_state_table;
				int num = 16;
				array[num] &= 127;
			}
			if ((this.key_state_table[17] & 128) != 0)
			{
				byte[] array2 = this.key_state_table;
				int num2 = 17;
				array2[num2] &= 127;
			}
			if ((this.key_state_table[18] & 128) != 0)
			{
				byte[] array3 = this.key_state_table;
				int num3 = 18;
				array3[num3] &= 127;
			}
			return false;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00072824 File Offset: 0x00070A24
		public void PreFilter(XEvent xevent)
		{
			if (xevent.KeyEvent.keycode >= this.keyc2vkey.Length)
			{
				return;
			}
			int num = this.keyc2vkey[xevent.KeyEvent.keycode];
			XEventName type = xevent.type;
			if (type != XEventName.KeyPress)
			{
				if (type == XEventName.KeyRelease)
				{
					byte[] array = this.key_state_table;
					int num2 = num & 255;
					array[num2] &= 127;
					return;
				}
			}
			else
			{
				if ((this.key_state_table[num & 255] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					int num3 = num & 255;
					array2[num3] ^= 1;
				}
				byte[] array3 = this.key_state_table;
				int num4 = num & 255;
				array3[num4] |= 128;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000728D0 File Offset: 0x00070AD0
		public void KeyEvent(IntPtr hwnd, XEvent xevent, ref MSG msg)
		{
			XKeySym xkeySym;
			XLookupStatus xlookupStatus;
			int num = this.LookupString(ref xevent, 24, out xkeySym, out xlookupStatus);
			if ((xkeySym >= (XKeySym)65025U && xkeySym <= (XKeySym)65039U) || xkeySym == (XKeySym)65406U)
			{
				this.UpdateKeyState(xevent);
				return;
			}
			if (xevent.KeyEvent.keycode >> 8 == 16)
			{
				xevent.KeyEvent.keycode = xevent.KeyEvent.keycode & 255;
			}
			int num2 = (int)xevent.KeyEvent.time;
			if (xlookupStatus == XLookupStatus.XLookupChars)
			{
				msg = this.SendImeComposition(this.lookup_buffer.ToString(0, this.lookup_buffer.Length));
				msg.hwnd = hwnd;
				return;
			}
			this.AltGrMask = xevent.KeyEvent.state & 24824;
			int num3 = this.EventToVkey(xevent);
			if (num3 == 0 && num != 0)
			{
				num3 = 252;
			}
			if (this.FilterKey(xevent, num3))
			{
				return;
			}
			VirtualKeys virtualKeys = (VirtualKeys)(num3 & 255);
			if (virtualKeys == VirtualKeys.VK_CAPITAL)
			{
				this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, xevent.type, num2);
				return;
			}
			if (virtualKeys == VirtualKeys.VK_NUMLOCK)
			{
				this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, xevent.type, num2);
				return;
			}
			if ((this.key_state_table[144] & 1) == 0 != ((xevent.KeyEvent.state & this.NumLockMask) == 0))
			{
				this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, XEventName.KeyPress, num2);
				this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, XEventName.KeyRelease, num2);
			}
			if ((this.key_state_table[20] & 1) == 0 != ((xevent.KeyEvent.state & 2) == 0))
			{
				this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, XEventName.KeyPress, num2);
				this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, XEventName.KeyRelease, num2);
			}
			this.num_state = false;
			this.cap_state = false;
			int num4 = this.keyc2scan[xevent.KeyEvent.keycode] & 255;
			KeybdEventFlags keybdEventFlags = KeybdEventFlags.None;
			if (xevent.type == XEventName.KeyRelease)
			{
				keybdEventFlags |= KeybdEventFlags.KeyUp;
			}
			if ((num3 & 256) != 0)
			{
				keybdEventFlags |= KeybdEventFlags.ExtendedKey;
			}
			msg = this.SendKeyboardInput((VirtualKeys)(num3 & 255), num4, xevent.KeyEvent.keycode, keybdEventFlags, num2);
			msg.hwnd = hwnd;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00072B2C File Offset: 0x00070D2C
		public bool TranslateMessage(ref MSG msg)
		{
			bool flag = false;
			if (msg.message >= Msg.WM_KEYDOWN && msg.message <= Msg.WM_KEYLAST)
			{
				flag = true;
			}
			if (msg.message == Msg.WM_SYSKEYUP && msg.wParam == (IntPtr)18 && this.menu_state)
			{
				msg.message = Msg.WM_KEYUP;
				this.menu_state = false;
			}
			if (msg.message != Msg.WM_KEYDOWN && msg.message != Msg.WM_SYSKEYDOWN)
			{
				return flag;
			}
			if ((this.key_state_table[18] & 128) != 0 && msg.wParam != (IntPtr)18)
			{
				this.menu_state = true;
			}
			this.EnsureLayoutInitialized();
			string text;
			int num = this.ToUnicode((int)msg.wParam, Control.HighOrder((long)(int)msg.lParam), out text);
			Msg msg2;
			if (num != -1)
			{
				if (num == 1)
				{
					msg2 = ((msg.message == Msg.WM_KEYDOWN) ? Msg.WM_CHAR : Msg.WM_SYSCHAR);
					XplatUI.PostMessage(msg.hwnd, msg2, (IntPtr)((int)text[0]), msg.lParam);
				}
				return flag;
			}
			msg2 = ((msg.message == Msg.WM_KEYDOWN) ? Msg.WM_DEADCHAR : Msg.WM_SYSDEADCHAR);
			XplatUI.PostMessage(msg.hwnd, msg2, (IntPtr)((int)text[0]), msg.lParam);
			return true;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00072C84 File Offset: 0x00070E84
		public int ToUnicode(int vkey, int scan, out string buffer)
		{
			if ((scan & 32768) != 0)
			{
				buffer = string.Empty;
				return 0;
			}
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.KeyPress;
			xevent.KeyEvent.display = this.display;
			xevent.KeyEvent.keycode = 0;
			xevent.KeyEvent.state = 0;
			if ((this.key_state_table[16] & 128) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 1;
			}
			if ((this.key_state_table[20] & 1) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 2;
			}
			if ((this.key_state_table[17] & 128) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 4;
			}
			if ((this.key_state_table[144] & 1) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | this.NumLockMask;
			}
			xevent.KeyEvent.state = xevent.KeyEvent.state | this.AltGrMask;
			int num = this.min_keycode;
			while (num <= this.max_keycode && xevent.KeyEvent.keycode == 0)
			{
				if ((this.keyc2vkey[num] & 255) == vkey)
				{
					xevent.KeyEvent.keycode = num;
					if ((this.EventToVkey(xevent) & 255) != vkey)
					{
						xevent.KeyEvent.keycode = 0;
					}
				}
				num++;
			}
			if (vkey >= 96 && vkey <= 105)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, vkey - 96 + 65456);
			}
			if (vkey == 110)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, 65454);
			}
			if (vkey == 108)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, 65452);
			}
			if (xevent.KeyEvent.keycode == 0 && vkey != 252)
			{
				Console.Error.WriteLine("unknown virtual key {0:X}", vkey);
				buffer = string.Empty;
				return vkey;
			}
			XKeySym xkeySym;
			XLookupStatus xlookupStatus;
			int num2 = this.LookupString(ref xevent, 24, out xkeySym, out xlookupStatus);
			int num3 = (int)xkeySym;
			buffer = string.Empty;
			if (num2 == 0)
			{
				int num4 = this.MapDeadKeySym(num3);
				if (num4 != 0)
				{
					byte[] array = new byte[] { (byte)num4 };
					buffer = new string(this.AnsiEncoding.GetChars(array));
					num2 = -1;
				}
			}
			else
			{
				if ((xevent.KeyEvent.state & this.NumLockMask) == 0 && (xevent.KeyEvent.state & 1) != 0 && num3 >= 65456 && num3 <= 65465)
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if ((xevent.KeyEvent.state & 4) != 0 && ((num3 >= 33 && num3 < 65) || (num3 > 90 && num3 < 97)))
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if (num3 == 65535)
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if (num3 == 65288 && (this.key_state_table[17] & 128) != 0)
				{
					buffer = new string(new char[] { '\u007f' });
					return 1;
				}
				if (num3 == 65288)
				{
					buffer = new string(new char[] { '\b' });
					return 1;
				}
				if (num3 == 65293)
				{
					buffer = new string(new char[] { '\r' });
					return 1;
				}
				if (num2 != 0)
				{
					buffer = this.lookup_buffer.ToString();
					num2 = buffer.Length;
				}
			}
			return num2;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00072FDD File Offset: 0x000711DD
		internal string GetCompositionString()
		{
			return this.stored_keyevent_string;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00072FE8 File Offset: 0x000711E8
		private MSG SendImeComposition(string s)
		{
			MSG msg = default(MSG);
			msg.message = Msg.WM_IME_COMPOSITION;
			msg.refobject = s;
			this.stored_keyevent_string = s;
			return msg;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0007301C File Offset: 0x0007121C
		private MSG SendKeyboardInput(VirtualKeys vkey, int scan, int keycode, KeybdEventFlags dw_flags, int time)
		{
			Msg msg;
			if ((dw_flags & KeybdEventFlags.KeyUp) != KeybdEventFlags.None)
			{
				bool flag = (this.key_state_table[18] & 128) != 0 && (this.key_state_table[17] & 128) == 0;
				byte[] array = this.key_state_table;
				array[(int)vkey] = array[(int)vkey] & 127;
				msg = (flag ? Msg.WM_SYSKEYUP : Msg.WM_KEYUP);
			}
			else
			{
				if ((this.key_state_table[(int)vkey] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					array2[(int)vkey] = array2[(int)vkey] ^ 1;
				}
				byte[] array3 = this.key_state_table;
				array3[(int)vkey] = array3[(int)vkey] | 128;
				msg = (((this.key_state_table[18] & 128) != 0 && (this.key_state_table[17] & 128) == 0) ? Msg.WM_SYSKEYDOWN : Msg.WM_KEYDOWN);
			}
			MSG msg2 = default(MSG);
			msg2.message = msg;
			msg2.wParam = (IntPtr)((int)vkey);
			msg2.lParam = this.GenerateLParam(msg2, keycode);
			return msg2;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00073114 File Offset: 0x00071314
		private IntPtr GenerateLParam(MSG m, int keyCode)
		{
			byte b = 0;
			if (m.message == Msg.WM_SYSKEYUP || m.message == Msg.WM_KEYUP)
			{
				b |= 128;
			}
			b |= 64;
			if ((this.key_state_table[165] & 128) != 0 || (this.key_state_table[164] & 128) != 0 || (this.key_state_table[18] & 128) != 0)
			{
				b |= 32;
			}
			if ((this.key_state_table[45] & 128) != 0 || (this.key_state_table[46] & 128) != 0 || (this.key_state_table[36] & 128) != 0 || (this.key_state_table[35] & 128) != 0 || (this.key_state_table[38] & 128) != 0 || (this.key_state_table[40] & 128) != 0 || (this.key_state_table[37] & 128) != 0 || (this.key_state_table[39] & 128) != 0 || (this.key_state_table[17] & 128) != 0 || (this.key_state_table[18] & 128) != 0 || (this.key_state_table[144] & 128) != 0 || (this.key_state_table[42] & 128) != 0 || (this.key_state_table[13] & 128) != 0 || (this.key_state_table[111] & 128) != 0 || (this.key_state_table[33] & 128) != 0 || (this.key_state_table[34] & 128) != 0)
			{
				b |= 1;
			}
			return (IntPtr)(((int)(b & byte.MaxValue) << 24) | ((keyCode & 255) << 16) | 1);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000732D8 File Offset: 0x000714D8
		private void GenerateMessage(VirtualKeys vkey, int scan, int key_code, XEventName type, int event_time)
		{
			if ((vkey == VirtualKeys.VK_NUMLOCK) ? this.num_state : this.cap_state)
			{
				this.SetState(vkey, false);
				return;
			}
			KeybdEventFlags keybdEventFlags = ((vkey == VirtualKeys.VK_NUMLOCK) ? KeybdEventFlags.ExtendedKey : KeybdEventFlags.None);
			KeybdEventFlags keybdEventFlags2 = ((vkey == VirtualKeys.VK_NUMLOCK) ? KeybdEventFlags.ExtendedKey : KeybdEventFlags.None) | KeybdEventFlags.KeyUp;
			if ((this.key_state_table[(int)vkey] & 1) != 0)
			{
				if (type != XEventName.KeyPress)
				{
					this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags, event_time);
					this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags2, event_time);
					this.SetState(vkey, false);
					byte[] array = this.key_state_table;
					array[(int)vkey] = array[(int)vkey] & 254;
					return;
				}
			}
			else if (type == XEventName.KeyPress)
			{
				this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags, event_time);
				this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags2, event_time);
				this.SetState(vkey, true);
				byte[] array2 = this.key_state_table;
				array2[(int)vkey] = array2[(int)vkey] | 1;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000733A8 File Offset: 0x000715A8
		private void UpdateKeyState(XEvent xevent)
		{
			int num = this.EventToVkey(xevent);
			XEventName type = xevent.type;
			if (type != XEventName.KeyPress)
			{
				if (type == XEventName.KeyRelease)
				{
					byte[] array = this.key_state_table;
					int num2 = num & 255;
					array[num2] &= 127;
					return;
				}
			}
			else
			{
				if ((this.key_state_table[num & 255] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					int num3 = num & 255;
					array2[num3] ^= 1;
				}
				byte[] array3 = this.key_state_table;
				int num4 = num & 255;
				array3[num4] |= 128;
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00073430 File Offset: 0x00071630
		private void SetState(VirtualKeys key, bool state)
		{
			if (VirtualKeys.VK_NUMLOCK == key)
			{
				this.num_state = state;
				return;
			}
			this.cap_state = state;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0007344C File Offset: 0x0007164C
		public int EventToVkey(XEvent e)
		{
			XKeySym xkeySym;
			XLookupStatus xlookupStatus;
			this.LookupString(ref e, 0, out xkeySym, out xlookupStatus);
			int num = (int)xkeySym;
			if ((e.KeyEvent.state & this.NumLockMask) != 0 && (num == 65452 || num == 65454 || (num >= 65456 && num <= 65465)))
			{
				return X11Keyboard.nonchar_key_vkey[num & 255];
			}
			return this.keyc2vkey[e.KeyEvent.keycode];
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000734C0 File Offset: 0x000716C0
		private void CreateConversionArray(KeyboardLayouts layouts, KeyboardLayout layout)
		{
			XEvent xevent = default(XEvent);
			int[] array = new int[4];
			xevent.KeyEvent.display = this.display;
			xevent.KeyEvent.state = 0;
			for (int i = this.min_keycode; i <= this.max_keycode; i++)
			{
				int num = 0;
				int num2 = 0;
				xevent.KeyEvent.keycode = i;
				XKeySym xkeySym;
				XLookupStatus xlookupStatus;
				this.LookupString(ref xevent, 0, out xkeySym, out xlookupStatus);
				uint num3 = (uint)xkeySym;
				if (num3 != 0U)
				{
					if (num3 >> 8 == 255U)
					{
						num = X11Keyboard.nonchar_key_vkey[(int)(num3 & 255U)];
						num2 = X11Keyboard.nonchar_key_scan[(int)(num3 & 255U)];
						if ((num2 & 256) != 0)
						{
							num |= 256;
						}
					}
					else if (num3 == 32U)
					{
						num = 32;
						num2 = 57;
					}
					else
					{
						int num4 = 0;
						int num5 = -1;
						for (int j = 0; j < this.syms; j++)
						{
							num3 = X11Keyboard.XKeycodeToKeysym(this.display, i, j);
							if (num3 < 2048U && num3 != 32U)
							{
								array[j] = (int)((sbyte)(num3 & 255U));
							}
							else
							{
								array[j] = (int)((sbyte)this.MapDeadKeySym((int)num3));
							}
						}
						for (int k = 0; k < layout.Keys.Length; k++)
						{
							int num6 = Math.Min(layout.Keys[k].Length, 4);
							int num7 = -1;
							int num8 = 0;
							while (num7 != 0 && num8 < num6)
							{
								if ((int)((sbyte)layout.Keys[k][num8]) != array[num8])
								{
									num7 = 0;
								}
								if (num7 != 0 || num8 > num4)
								{
									num4 = num8;
									num5 = k;
								}
								if (num7 != 0)
								{
									break;
								}
								num8++;
							}
						}
						if (num5 >= 0)
						{
							if (num5 < layouts.scan_table[(int)layout.ScanIndex].Length)
							{
								num2 = (int)layouts.scan_table[(int)layout.ScanIndex][num5];
							}
							if (num5 < layouts.vkey_table[(int)layout.VKeyIndex].Length)
							{
								num = layouts.vkey_table[(int)layout.VKeyIndex][num5];
							}
						}
					}
				}
				this.keyc2vkey[xevent.KeyEvent.keycode] = num;
				this.keyc2scan[xevent.KeyEvent.keycode] = num2;
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000736D0 File Offset: 0x000718D0
		private KeyboardLayout DetectLayout(KeyboardLayouts layouts)
		{
			X11Keyboard.XDisplayKeycodes(this.display, out this.min_keycode, out this.max_keycode);
			IntPtr intPtr = X11Keyboard.XGetKeyboardMapping(this.display, (byte)this.min_keycode, this.max_keycode + 1 - this.min_keycode, out this.keysyms_per_keycode);
			object xlibLock = X11Keyboard.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XFree(intPtr);
			}
			this.syms = this.keysyms_per_keycode;
			if (this.syms > 4)
			{
				this.syms = 2;
			}
			XModifierKeymap xmodifierKeymap = default(XModifierKeymap);
			IntPtr intPtr2 = X11Keyboard.XGetModifierMapping(this.display);
			xmodifierKeymap = (XModifierKeymap)Marshal.PtrToStructure(intPtr2, typeof(XModifierKeymap));
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				int j = 0;
				while (j < xmodifierKeymap.max_keypermod)
				{
					byte b = Marshal.ReadByte(xmodifierKeymap.modifiermap, num);
					if (b != 0)
					{
						for (int k = 0; k < this.keysyms_per_keycode; k++)
						{
							if (X11Keyboard.XKeycodeToKeysym(this.display, (int)b, k) == 65407U)
							{
								this.NumLockMask = 1 << i;
							}
						}
					}
					j++;
					num++;
				}
			}
			X11Keyboard.XFreeModifiermap(intPtr2);
			int[] array = new int[4];
			KeyboardLayout keyboardLayout = null;
			int num2 = 0;
			int num3 = 0;
			foreach (KeyboardLayout keyboardLayout2 in layouts.Layouts)
			{
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				int num9 = -1;
				int m = this.min_keycode;
				for (int n = this.min_keycode; n <= this.max_keycode; n++)
				{
					for (int num10 = 0; num10 < this.syms; num10++)
					{
						uint num11 = X11Keyboard.XKeycodeToKeysym(this.display, n, num10);
						if (num11 < 2048U && num11 != 32U)
						{
							array[num10] = (int)((sbyte)(num11 & 255U));
						}
						else
						{
							array[num10] = (int)((sbyte)this.MapDeadKeySym((int)num11));
						}
					}
					if (array[0] != 0)
					{
						for (m = 0; m < keyboardLayout2.Keys.Length; m++)
						{
							int num12 = Math.Min(this.syms, keyboardLayout2.Keys[m].Length);
							num4 = 0;
							int num10 = 0;
							while (num4 >= 0 && num10 < num12)
							{
								sbyte b2 = (sbyte)keyboardLayout2.Keys[m][num10];
								if (b2 != 0 && (int)b2 == array[num10])
								{
									num4++;
								}
								if (b2 != 0 && (int)b2 != array[num10])
								{
									num4 = -1;
								}
								num10++;
							}
							if (num4 > 0)
							{
								num5 += num4;
								break;
							}
						}
						if (num4 > 0)
						{
							num6++;
							if (m > num9)
							{
								num8++;
							}
							num9 = m;
						}
						else
						{
							num7++;
							num5 -= this.syms;
						}
					}
				}
				if (num5 > num2 || (num5 == num2 && num8 > num3))
				{
					keyboardLayout = keyboardLayout2;
					num2 = num5;
					num3 = num8;
				}
			}
			if (keyboardLayout != null)
			{
				return keyboardLayout;
			}
			Console.WriteLine(Locale.GetText("Keyboard layout not recognized, using default layout: " + layouts.Layouts[0].Name));
			return layouts.Layouts[0];
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000739F0 File Offset: 0x00071BF0
		private int MapDeadKeySym(int val)
		{
			if (val <= 268500519)
			{
				switch (val)
				{
				case 65104:
					return 96;
				case 65105:
					break;
				case 65106:
					return 94;
				case 65107:
					return 126;
				case 65108:
					return 45;
				case 65109:
					return 162;
				case 65110:
					return 255;
				case 65111:
					return 168;
				case 65112:
					return 48;
				case 65113:
					return 189;
				case 65114:
					return 183;
				case 65115:
					return 184;
				case 65116:
					return 178;
				default:
					if (val == 268500514)
					{
						return 168;
					}
					if (val != 268500519)
					{
						return 0;
					}
					break;
				}
				return 180;
			}
			if (val == 268500574)
			{
				return 94;
			}
			if (val == 268500576)
			{
				return 96;
			}
			if (val != 268500606)
			{
				return 0;
			}
			return 126;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00073AB0 File Offset: 0x00071CB0
		private XIMProperties[] GetSupportedInputStyles(IntPtr xim)
		{
			IntPtr intPtr;
			if (X11Keyboard.XGetIMValues(xim, "queryInputStyle", out intPtr, IntPtr.Zero) != null || intPtr == IntPtr.Zero)
			{
				return new XIMProperties[0];
			}
			XIMStyles ximstyles = (XIMStyles)Marshal.PtrToStructure(intPtr, typeof(XIMStyles));
			XIMProperties[] array = new XIMProperties[(int)ximstyles.count_styles];
			for (int i = 0; i < (int)ximstyles.count_styles; i++)
			{
				array[i] = (XIMProperties)Marshal.PtrToStructure(new IntPtr((long)ximstyles.supported_styles + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), typeof(XIMProperties));
			}
			object xlibLock = X11Keyboard.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XFree(intPtr);
			}
			return array;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00073B8C File Offset: 0x00071D8C
		private XIMProperties[] GetPreferredStyles()
		{
			string text = Environment.GetEnvironmentVariable("MONO_WINFORMS_XIM_STYLE");
			if (text == null)
			{
				text = "over-the-spot";
			}
			string[] array = text.Split(new char[] { ' ' });
			XIMProperties[] array2 = new XIMProperties[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (!(text2 == "over-the-spot"))
				{
					if (!(text2 == "on-the-spot"))
					{
						if (text2 == "root")
						{
							array2[i] = XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing;
						}
					}
					else
					{
						array2[i] = XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing;
					}
				}
				else
				{
					array2[i] = XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing;
				}
			}
			return array2;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00073C24 File Offset: 0x00071E24
		private IEnumerable GetMatchingStylesInPreferredOrder(IntPtr xim)
		{
			XIMProperties[] supportedStyles = this.GetSupportedInputStyles(xim);
			foreach (XIMProperties ximproperties in this.GetPreferredStyles())
			{
				if (Array.IndexOf<XIMProperties>(supportedStyles, ximproperties) >= 0)
				{
					yield return ximproperties;
				}
			}
			XIMProperties[] array = null;
			yield break;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00073C3C File Offset: 0x00071E3C
		private IntPtr CreateXic(IntPtr window, IntPtr xim)
		{
			IntPtr intPtr = IntPtr.Zero;
			foreach (object obj in this.GetMatchingStylesInPreferredOrder(xim))
			{
				XIMProperties ximproperties = (XIMProperties)obj;
				this.ximStyle = ximproperties;
				if (ximproperties != (XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing))
				{
					if (ximproperties != (XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing))
					{
						if (ximproperties == (XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing))
						{
							intPtr = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing, "clientWindow", window, IntPtr.Zero);
						}
					}
					else
					{
						intPtr = this.CreateOverTheSpotXic(window, xim);
						if (intPtr != IntPtr.Zero)
						{
						}
					}
				}
				else
				{
					intPtr = this.CreateOnTheSpotXic(window, xim);
					if (intPtr != IntPtr.Zero)
					{
					}
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				this.ximStyle = XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing;
				intPtr = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing, "clientWindow", window, "focusWindow", window, IntPtr.Zero);
			}
			return intPtr;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00073D40 File Offset: 0x00071F40
		private IntPtr CreateOverTheSpotXic(IntPtr window, IntPtr xim)
		{
			Control control = Control.FromHandle(window);
			string text = string.Format("-*-*-*-*-*-*-{0}-*-*-*-*-*-*-*", (int)control.Font.Size);
			IntPtr intPtr2;
			int num;
			IntPtr intPtr = X11Keyboard.XCreateFontSet(this.display, text, out intPtr2, out num, IntPtr.Zero);
			XPoint xpoint = new XPoint();
			xpoint.X = 0;
			xpoint.Y = 0;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr intPtr6;
			try
			{
				intPtr3 = Marshal.StringToHGlobalAnsi("spotLocation");
				intPtr4 = Marshal.StringToHGlobalAnsi("fontSet");
				IntPtr intPtr5 = X11Keyboard.XVaCreateNestedList(0, intPtr3, xpoint, intPtr4, intPtr, IntPtr.Zero);
				intPtr6 = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing, "clientWindow", window, "preeditAttributes", intPtr5, IntPtr.Zero);
			}
			finally
			{
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr4 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr4);
				}
				X11Keyboard.XFreeStringList(intPtr2);
			}
			return intPtr6;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00073E44 File Offset: 0x00072044
		private IntPtr CreateOnTheSpotXic(IntPtr window, IntPtr xim)
		{
			this.callbackContext = new X11Keyboard.XIMCallbackContext(window);
			return this.callbackContext.CreateXic(window, xim);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00073E60 File Offset: 0x00072060
		internal void SetCaretPos(CaretStruct caret, IntPtr handle, int x, int y)
		{
			if (this.ximStyle != (XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing))
			{
				return;
			}
			if (this.positionContext == null)
			{
				this.positionContext = new X11Keyboard.XIMPositionContext();
			}
			this.positionContext.Caret = caret;
			this.positionContext.X = x;
			this.positionContext.Y = y + caret.Height;
			this.MoveCurrentCaretPos();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00073EC0 File Offset: 0x000720C0
		internal void MoveCurrentCaretPos()
		{
			if (this.positionContext == null || this.ximStyle != (XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing) || this.client_window == IntPtr.Zero)
			{
				return;
			}
			int x = this.positionContext.X;
			int y = this.positionContext.Y;
			CaretStruct caret = this.positionContext.Caret;
			IntPtr xic = this.GetXic(this.client_window);
			if (xic == IntPtr.Zero)
			{
				return;
			}
			Control control = Control.FromHandle(this.client_window);
			if (control == null || !control.IsHandleCreated)
			{
				return;
			}
			control = Control.FromHandle(caret.Hwnd);
			if (control == null || !control.IsHandleCreated)
			{
				return;
			}
			if (!Hwnd.ObjectFromHandle(this.client_window).mapped)
			{
				return;
			}
			object xlibLock = X11Keyboard.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(this.display, this.client_window, this.client_window, x, y, out num, out num2, out intPtr);
			}
			XPoint xpoint = new XPoint();
			xpoint.X = (short)num;
			xpoint.Y = (short)num2;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr2 = Marshal.StringToHGlobalAnsi("spotLocation");
				IntPtr intPtr3 = X11Keyboard.XVaCreateNestedList(0, intPtr2, xpoint, IntPtr.Zero);
				X11Keyboard.XSetICValues(xic, "preeditAttributes", intPtr3, IntPtr.Zero);
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00074044 File Offset: 0x00072244
		private int LookupString(ref XEvent xevent, int len, out XKeySym keysym, out XLookupStatus status)
		{
			status = XLookupStatus.XLookupNone;
			IntPtr xic = this.GetXic(this.client_window);
			IntPtr intPtr;
			int num;
			if (xic != IntPtr.Zero && this.have_Xutf8LookupString && xevent.type == XEventName.KeyPress)
			{
				for (;;)
				{
					try
					{
						num = X11Keyboard.Xutf8LookupString(xic, ref xevent, this.lookup_byte_buffer, 100, out intPtr, out status);
					}
					catch (EntryPointNotFoundException)
					{
						this.have_Xutf8LookupString = false;
						return this.LookupString(ref xevent, len, out keysym, out status);
					}
					if (status != XLookupStatus.XBufferOverflow)
					{
						break;
					}
					this.lookup_byte_buffer = new byte[this.lookup_byte_buffer.Length << 1];
				}
				this.lookup_buffer.Length = 0;
				string @string = Encoding.UTF8.GetString(this.lookup_byte_buffer, 0, num);
				this.lookup_buffer.Append(@string);
				keysym = (XKeySym)intPtr.ToInt32();
				return @string.Length;
			}
			IntPtr zero = IntPtr.Zero;
			this.lookup_buffer.Length = 0;
			num = X11Keyboard.XLookupString(ref xevent, this.lookup_buffer, len, out intPtr, out zero);
			keysym = (XKeySym)intPtr.ToInt32();
			return num;
		}

		// Token: 0x06001711 RID: 5905
		[DllImport("libX11")]
		private static extern IntPtr XOpenIM(IntPtr display, IntPtr rdb, IntPtr res_name, IntPtr res_class);

		// Token: 0x06001712 RID: 5906
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr XCreateIC(IntPtr xim, string name, XIMProperties im_style, string name2, IntPtr value2, IntPtr terminator);

		// Token: 0x06001713 RID: 5907
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr XCreateIC(IntPtr xim, string name, XIMProperties im_style, string name2, IntPtr value2, string name3, IntPtr value3, IntPtr terminator);

		// Token: 0x06001714 RID: 5908
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, XPoint value0, IntPtr terminator);

		// Token: 0x06001715 RID: 5909
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, XPoint value0, IntPtr name1, IntPtr value1, IntPtr terminator);

		// Token: 0x06001716 RID: 5910
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, IntPtr value0, IntPtr name1, IntPtr value1, IntPtr name2, IntPtr value2, IntPtr name3, IntPtr value3, IntPtr terminator);

		// Token: 0x06001717 RID: 5911
		[DllImport("libX11")]
		private static extern IntPtr XCreateFontSet(IntPtr display, string name, out IntPtr list, out int count, IntPtr terminator);

		// Token: 0x06001718 RID: 5912
		[DllImport("libX11")]
		private static extern void XFreeStringList(IntPtr ptr);

		// Token: 0x06001719 RID: 5913
		[DllImport("libX11")]
		private static extern void XCloseIM(IntPtr xim);

		// Token: 0x0600171A RID: 5914
		[DllImport("libX11")]
		private static extern void XDestroyIC(IntPtr xic);

		// Token: 0x0600171B RID: 5915
		[DllImport("libX11")]
		private static extern string XGetIMValues(IntPtr xim, string name, out IntPtr value, IntPtr terminator);

		// Token: 0x0600171C RID: 5916
		[DllImport("libX11")]
		private static extern string XGetICValues(IntPtr xic, string name, out EventMask value, IntPtr terminator);

		// Token: 0x0600171D RID: 5917
		[DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
		private static extern void XSetICValues(IntPtr xic, string name, IntPtr value, IntPtr terminator);

		// Token: 0x0600171E RID: 5918
		[DllImport("libX11")]
		private static extern void XSetICFocus(IntPtr xic);

		// Token: 0x0600171F RID: 5919
		[DllImport("libX11")]
		private static extern void XUnsetICFocus(IntPtr xic);

		// Token: 0x06001720 RID: 5920
		[DllImport("libX11")]
		private static extern string Xutf8ResetIC(IntPtr xic);

		// Token: 0x06001721 RID: 5921
		[DllImport("libX11")]
		private static extern bool XSupportsLocale();

		// Token: 0x06001722 RID: 5922
		[DllImport("libX11")]
		private static extern bool XSetLocaleModifiers(string mods);

		// Token: 0x06001723 RID: 5923
		[DllImport("libX11")]
		internal static extern int XLookupString(ref XEvent xevent, StringBuilder buffer, int num_bytes, out IntPtr keysym, out IntPtr status);

		// Token: 0x06001724 RID: 5924
		[DllImport("libX11")]
		internal static extern int Xutf8LookupString(IntPtr xic, ref XEvent xevent, byte[] buffer, int num_bytes, out IntPtr keysym, out XLookupStatus status);

		// Token: 0x06001725 RID: 5925
		[DllImport("libX11")]
		private static extern IntPtr XGetKeyboardMapping(IntPtr display, byte first_keycode, int keycode_count, out int keysyms_per_keycode_return);

		// Token: 0x06001726 RID: 5926
		[DllImport("libX11")]
		private static extern void XDisplayKeycodes(IntPtr display, out int min, out int max);

		// Token: 0x06001727 RID: 5927
		[DllImport("libX11")]
		private static extern uint XKeycodeToKeysym(IntPtr display, int keycode, int index);

		// Token: 0x06001728 RID: 5928
		[DllImport("libX11")]
		private static extern int XKeysymToKeycode(IntPtr display, IntPtr keysym);

		// Token: 0x06001729 RID: 5929 RVA: 0x00074158 File Offset: 0x00072358
		private static int XKeysymToKeycode(IntPtr display, int keysym)
		{
			return X11Keyboard.XKeysymToKeycode(display, (IntPtr)keysym);
		}

		// Token: 0x0600172A RID: 5930
		[DllImport("libX11")]
		internal static extern IntPtr XGetModifierMapping(IntPtr display);

		// Token: 0x0600172B RID: 5931
		[DllImport("libX11")]
		internal static extern int XFreeModifiermap(IntPtr modmap);

		// Token: 0x04000E12 RID: 3602
		internal static object XlibLock;

		// Token: 0x04000E13 RID: 3603
		private IntPtr display;

		// Token: 0x04000E14 RID: 3604
		private IntPtr client_window;

		// Token: 0x04000E15 RID: 3605
		private IntPtr xim;

		// Token: 0x04000E16 RID: 3606
		private Hashtable xic_table = new Hashtable();

		// Token: 0x04000E17 RID: 3607
		private X11Keyboard.XIMPositionContext positionContext;

		// Token: 0x04000E18 RID: 3608
		private X11Keyboard.XIMCallbackContext callbackContext;

		// Token: 0x04000E19 RID: 3609
		private XIMProperties ximStyle;

		// Token: 0x04000E1A RID: 3610
		private EventMask xic_event_mask;

		// Token: 0x04000E1B RID: 3611
		private StringBuilder lookup_buffer;

		// Token: 0x04000E1C RID: 3612
		private byte[] lookup_byte_buffer = new byte[100];

		// Token: 0x04000E1D RID: 3613
		private int min_keycode;

		// Token: 0x04000E1E RID: 3614
		private int max_keycode;

		// Token: 0x04000E1F RID: 3615
		private int keysyms_per_keycode;

		// Token: 0x04000E20 RID: 3616
		private int syms;

		// Token: 0x04000E21 RID: 3617
		private int[] keyc2vkey = new int[256];

		// Token: 0x04000E22 RID: 3618
		private int[] keyc2scan = new int[256];

		// Token: 0x04000E23 RID: 3619
		private byte[] key_state_table = new byte[256];

		// Token: 0x04000E24 RID: 3620
		private int lcid;

		// Token: 0x04000E25 RID: 3621
		private bool num_state;

		// Token: 0x04000E26 RID: 3622
		private bool cap_state;

		// Token: 0x04000E27 RID: 3623
		private bool initialized;

		// Token: 0x04000E28 RID: 3624
		private bool menu_state;

		// Token: 0x04000E29 RID: 3625
		private Encoding encoding;

		// Token: 0x04000E2A RID: 3626
		private int NumLockMask;

		// Token: 0x04000E2B RID: 3627
		private int AltGrMask;

		// Token: 0x04000E2C RID: 3628
		private bool have_Xutf8ResetIC = true;

		// Token: 0x04000E2D RID: 3629
		private string stored_keyevent_string;

		// Token: 0x04000E2E RID: 3630
		private bool have_Xutf8LookupString = true;

		// Token: 0x04000E2F RID: 3631
		private static readonly int[] nonchar_key_vkey = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 8, 9,
			0, 12, 0, 13, 0, 0, 0, 0, 0, 19,
			145, 0, 0, 0, 0, 0, 0, 27, 0, 0,
			0, 0, 0, 0, 29, 28, 0, 0, 0, 0,
			0, 0, 243, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			36, 37, 38, 39, 40, 33, 34, 35, 0, 0,
			0, 0, 0, 0, 0, 0, 41, 44, 43, 45,
			0, 0, 0, 0, 3, 47, 3, 3, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 144, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 13, 0, 0, 0, 0, 0, 0, 0, 36,
			37, 38, 39, 40, 33, 34, 35, 0, 45, 46,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			106, 107, 108, 109, 110, 111, 96, 97, 98, 99,
			100, 101, 102, 103, 104, 105, 0, 0, 0, 0,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 121,
			122, 123, 124, 125, 126, 127, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 16, 16, 17, 17, 20,
			0, 18, 18, 18, 18, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 46
		};

		// Token: 0x04000E30 RID: 3632
		private static readonly int[] nonchar_key_scan = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 14, 15,
			0, 0, 0, 28, 0, 0, 0, 0, 0, 69,
			70, 0, 0, 0, 0, 0, 0, 1, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			327, 331, 328, 333, 336, 329, 337, 335, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 311, 0, 338,
			0, 0, 0, 0, 0, 0, 56, 326, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 312, 325, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 284, 0, 0, 0, 0, 0, 0, 0, 71,
			75, 72, 77, 80, 73, 81, 79, 76, 82, 83,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			55, 78, 0, 74, 83, 309, 82, 79, 80, 81,
			75, 76, 77, 71, 72, 73, 0, 0, 0, 0,
			59, 60, 61, 62, 63, 64, 65, 66, 67, 68,
			87, 88, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 42, 54, 29, 285, 58,
			0, 56, 312, 56, 312, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 339
		};

		// Token: 0x04000E31 RID: 3633
		private static readonly int[] nonchar_vkey_key = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 65288, 65289,
			0, 0, 65291, 65293, 0, 0, 65505, 65507, 65383, 0,
			65509, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 65367, 65360, 65361, 65362, 65363,
			65364, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 65511, 65512, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			65505, 65506, 65507, 65508, 65513, 65514, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0
		};

		// Token: 0x0200023D RID: 573
		private class XIMCallbackContext
		{
			// Token: 0x0600172D RID: 5933 RVA: 0x000741C4 File Offset: 0x000723C4
			public XIMCallbackContext(IntPtr clientWindow)
			{
				this.startCB = new XIMCallback(clientWindow, new XIMProc(this.DoPreeditStart));
				this.doneCB = new XIMCallback(clientWindow, new XIMProc(this.DoPreeditDone));
				this.drawCB = new XIMCallback(clientWindow, new XIMProc(this.DoPreeditDraw));
				this.caretCB = new XIMCallback(clientWindow, new XIMProc(this.DoPreeditCaret));
				this.pStartCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pDoneCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pDrawCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pCaretCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pStartCBN = Marshal.StringToHGlobalAnsi("preeditStartCallback");
				this.pDoneCBN = Marshal.StringToHGlobalAnsi("preeditDoneCallback");
				this.pDrawCBN = Marshal.StringToHGlobalAnsi("preeditDrawCallback");
				this.pCaretCBN = Marshal.StringToHGlobalAnsi("preeditCaretCallback");
			}

			// Token: 0x0600172E RID: 5934 RVA: 0x00074338 File Offset: 0x00072538
			~XIMCallbackContext()
			{
				if (this.pStartCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pStartCBN);
				}
				if (this.pDoneCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDoneCBN);
				}
				if (this.pDrawCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDrawCBN);
				}
				if (this.pCaretCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pCaretCBN);
				}
				if (this.pStartCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pStartCB);
				}
				if (this.pDoneCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDoneCB);
				}
				if (this.pDrawCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDrawCB);
				}
				if (this.pCaretCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pCaretCB);
				}
			}

			// Token: 0x0600172F RID: 5935 RVA: 0x00074448 File Offset: 0x00072648
			private int DoPreeditStart(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				XplatUI.SendMessage(clientData, Msg.WM_XIM_PREEDITSTART, clientData, callData);
				return 100;
			}

			// Token: 0x06001730 RID: 5936 RVA: 0x0007445A File Offset: 0x0007265A
			private int DoPreeditDone(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				XplatUI.SendMessage(clientData, Msg.WM_XIM_PREEDITDONE, clientData, callData);
				return 0;
			}

			// Token: 0x06001731 RID: 5937 RVA: 0x0007446B File Offset: 0x0007266B
			private int DoPreeditDraw(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				XplatUI.SendMessage(clientData, Msg.WM_XIM_PREEDITDRAW, clientData, callData);
				return 0;
			}

			// Token: 0x06001732 RID: 5938 RVA: 0x0007447C File Offset: 0x0007267C
			private int DoPreeditCaret(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				XplatUI.SendMessage(clientData, Msg.WM_XIM_PREEDITCARET, clientData, callData);
				return 0;
			}

			// Token: 0x06001733 RID: 5939 RVA: 0x00074490 File Offset: 0x00072690
			public IntPtr CreateXic(IntPtr window, IntPtr xim)
			{
				Marshal.StructureToPtr<XIMCallback>(this.startCB, this.pStartCB, false);
				Marshal.StructureToPtr<XIMCallback>(this.doneCB, this.pDoneCB, false);
				Marshal.StructureToPtr<XIMCallback>(this.drawCB, this.pDrawCB, false);
				Marshal.StructureToPtr<XIMCallback>(this.caretCB, this.pCaretCB, false);
				IntPtr intPtr = X11Keyboard.XVaCreateNestedList(0, this.pStartCBN, this.pStartCB, this.pDoneCBN, this.pDoneCB, this.pDrawCBN, this.pDrawCB, this.pCaretCBN, this.pCaretCB, IntPtr.Zero);
				return X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing, "clientWindow", window, "preeditAttributes", intPtr, IntPtr.Zero);
			}

			// Token: 0x04000E32 RID: 3634
			private XIMCallback startCB;

			// Token: 0x04000E33 RID: 3635
			private XIMCallback doneCB;

			// Token: 0x04000E34 RID: 3636
			private XIMCallback drawCB;

			// Token: 0x04000E35 RID: 3637
			private XIMCallback caretCB;

			// Token: 0x04000E36 RID: 3638
			private IntPtr pStartCB = IntPtr.Zero;

			// Token: 0x04000E37 RID: 3639
			private IntPtr pDoneCB = IntPtr.Zero;

			// Token: 0x04000E38 RID: 3640
			private IntPtr pDrawCB = IntPtr.Zero;

			// Token: 0x04000E39 RID: 3641
			private IntPtr pCaretCB = IntPtr.Zero;

			// Token: 0x04000E3A RID: 3642
			private IntPtr pStartCBN = IntPtr.Zero;

			// Token: 0x04000E3B RID: 3643
			private IntPtr pDoneCBN = IntPtr.Zero;

			// Token: 0x04000E3C RID: 3644
			private IntPtr pDrawCBN = IntPtr.Zero;

			// Token: 0x04000E3D RID: 3645
			private IntPtr pCaretCBN = IntPtr.Zero;
		}

		// Token: 0x0200023E RID: 574
		private class XIMPositionContext
		{
			// Token: 0x04000E3E RID: 3646
			public CaretStruct Caret;

			// Token: 0x04000E3F RID: 3647
			public int X;

			// Token: 0x04000E40 RID: 3648
			public int Y;
		}
	}
}

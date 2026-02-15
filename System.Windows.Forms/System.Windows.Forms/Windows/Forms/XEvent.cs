using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000260 RID: 608
	[StructLayout(LayoutKind.Explicit)]
	internal struct XEvent
	{
		// Token: 0x0600173D RID: 5949 RVA: 0x00074670 File Offset: 0x00072870
		public override string ToString()
		{
			switch (this.type)
			{
			case XEventName.ButtonPress:
			case XEventName.ButtonRelease:
				return XEvent.ToString(this.ButtonEvent);
			case XEventName.MotionNotify:
				return XEvent.ToString(this.MotionEvent);
			case XEventName.EnterNotify:
			case XEventName.LeaveNotify:
				return XEvent.ToString(this.CrossingEvent);
			case XEventName.FocusIn:
			case XEventName.FocusOut:
				return XEvent.ToString(this.FocusChangeEvent);
			case XEventName.KeymapNotify:
				return XEvent.ToString(this.KeymapEvent);
			case XEventName.Expose:
				return XEvent.ToString(this.ExposeEvent);
			case XEventName.GraphicsExpose:
				return XEvent.ToString(this.GraphicsExposeEvent);
			case XEventName.NoExpose:
				return XEvent.ToString(this.NoExposeEvent);
			case XEventName.VisibilityNotify:
				return XEvent.ToString(this.VisibilityEvent);
			case XEventName.CreateNotify:
				return XEvent.ToString(this.CreateWindowEvent);
			case XEventName.DestroyNotify:
				return XEvent.ToString(this.DestroyWindowEvent);
			case XEventName.UnmapNotify:
				return XEvent.ToString(this.UnmapEvent);
			case XEventName.MapNotify:
				return XEvent.ToString(this.MapEvent);
			case XEventName.MapRequest:
				return XEvent.ToString(this.MapRequestEvent);
			case XEventName.ReparentNotify:
				return XEvent.ToString(this.ReparentEvent);
			case XEventName.ConfigureNotify:
				return XEvent.ToString(this.ConfigureEvent);
			case XEventName.ConfigureRequest:
				return XEvent.ToString(this.ConfigureRequestEvent);
			case XEventName.GravityNotify:
				return XEvent.ToString(this.GravityEvent);
			case XEventName.ResizeRequest:
				return XEvent.ToString(this.ResizeRequestEvent);
			case XEventName.CirculateNotify:
			case XEventName.CirculateRequest:
				return XEvent.ToString(this.CirculateEvent);
			case XEventName.PropertyNotify:
				return XEvent.ToString(this.PropertyEvent);
			case XEventName.SelectionClear:
				return XEvent.ToString(this.SelectionClearEvent);
			case XEventName.SelectionRequest:
				return XEvent.ToString(this.SelectionRequestEvent);
			case XEventName.SelectionNotify:
				return XEvent.ToString(this.SelectionEvent);
			case XEventName.ColormapNotify:
				return XEvent.ToString(this.ColormapEvent);
			case XEventName.ClientMessage:
				return XEvent.ToString(this.ClientMessageEvent);
			case XEventName.MappingNotify:
				return XEvent.ToString(this.MappingEvent);
			default:
				return this.type.ToString();
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000748EC File Offset: 0x00072AEC
		public static string ToString(object ev)
		{
			string text = string.Empty;
			Type type = ev.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				object value = fields[i].GetValue(ev);
				text = text + fields[i].Name + "=" + ((value == null) ? "<null>" : value.ToString());
			}
			return type.Name + " (" + text + ")";
		}

		// Token: 0x04000FA1 RID: 4001
		[FieldOffset(0)]
		internal XEventName type;

		// Token: 0x04000FA2 RID: 4002
		[FieldOffset(0)]
		internal XAnyEvent AnyEvent;

		// Token: 0x04000FA3 RID: 4003
		[FieldOffset(0)]
		internal XKeyEvent KeyEvent;

		// Token: 0x04000FA4 RID: 4004
		[FieldOffset(0)]
		internal XButtonEvent ButtonEvent;

		// Token: 0x04000FA5 RID: 4005
		[FieldOffset(0)]
		internal XMotionEvent MotionEvent;

		// Token: 0x04000FA6 RID: 4006
		[FieldOffset(0)]
		internal XCrossingEvent CrossingEvent;

		// Token: 0x04000FA7 RID: 4007
		[FieldOffset(0)]
		internal XFocusChangeEvent FocusChangeEvent;

		// Token: 0x04000FA8 RID: 4008
		[FieldOffset(0)]
		internal XExposeEvent ExposeEvent;

		// Token: 0x04000FA9 RID: 4009
		[FieldOffset(0)]
		internal XGraphicsExposeEvent GraphicsExposeEvent;

		// Token: 0x04000FAA RID: 4010
		[FieldOffset(0)]
		internal XNoExposeEvent NoExposeEvent;

		// Token: 0x04000FAB RID: 4011
		[FieldOffset(0)]
		internal XVisibilityEvent VisibilityEvent;

		// Token: 0x04000FAC RID: 4012
		[FieldOffset(0)]
		internal XCreateWindowEvent CreateWindowEvent;

		// Token: 0x04000FAD RID: 4013
		[FieldOffset(0)]
		internal XDestroyWindowEvent DestroyWindowEvent;

		// Token: 0x04000FAE RID: 4014
		[FieldOffset(0)]
		internal XUnmapEvent UnmapEvent;

		// Token: 0x04000FAF RID: 4015
		[FieldOffset(0)]
		internal XMapEvent MapEvent;

		// Token: 0x04000FB0 RID: 4016
		[FieldOffset(0)]
		internal XMapRequestEvent MapRequestEvent;

		// Token: 0x04000FB1 RID: 4017
		[FieldOffset(0)]
		internal XReparentEvent ReparentEvent;

		// Token: 0x04000FB2 RID: 4018
		[FieldOffset(0)]
		internal XConfigureEvent ConfigureEvent;

		// Token: 0x04000FB3 RID: 4019
		[FieldOffset(0)]
		internal XGravityEvent GravityEvent;

		// Token: 0x04000FB4 RID: 4020
		[FieldOffset(0)]
		internal XResizeRequestEvent ResizeRequestEvent;

		// Token: 0x04000FB5 RID: 4021
		[FieldOffset(0)]
		internal XConfigureRequestEvent ConfigureRequestEvent;

		// Token: 0x04000FB6 RID: 4022
		[FieldOffset(0)]
		internal XCirculateEvent CirculateEvent;

		// Token: 0x04000FB7 RID: 4023
		[FieldOffset(0)]
		internal XCirculateRequestEvent CirculateRequestEvent;

		// Token: 0x04000FB8 RID: 4024
		[FieldOffset(0)]
		internal XPropertyEvent PropertyEvent;

		// Token: 0x04000FB9 RID: 4025
		[FieldOffset(0)]
		internal XSelectionClearEvent SelectionClearEvent;

		// Token: 0x04000FBA RID: 4026
		[FieldOffset(0)]
		internal XSelectionRequestEvent SelectionRequestEvent;

		// Token: 0x04000FBB RID: 4027
		[FieldOffset(0)]
		internal XSelectionEvent SelectionEvent;

		// Token: 0x04000FBC RID: 4028
		[FieldOffset(0)]
		internal XColormapEvent ColormapEvent;

		// Token: 0x04000FBD RID: 4029
		[FieldOffset(0)]
		internal XClientMessageEvent ClientMessageEvent;

		// Token: 0x04000FBE RID: 4030
		[FieldOffset(0)]
		internal XMappingEvent MappingEvent;

		// Token: 0x04000FBF RID: 4031
		[FieldOffset(0)]
		internal XErrorEvent ErrorEvent;

		// Token: 0x04000FC0 RID: 4032
		[FieldOffset(0)]
		internal XKeymapEvent KeymapEvent;

		// Token: 0x04000FC1 RID: 4033
		[FieldOffset(0)]
		internal XEventPad Pad;
	}
}

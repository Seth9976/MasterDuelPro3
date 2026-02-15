using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public enum EventType
	{
		// Token: 0x04000007 RID: 7
		MouseDown,
		// Token: 0x04000008 RID: 8
		MouseUp,
		// Token: 0x04000009 RID: 9
		MouseMove,
		// Token: 0x0400000A RID: 10
		MouseDrag,
		// Token: 0x0400000B RID: 11
		KeyDown,
		// Token: 0x0400000C RID: 12
		KeyUp,
		// Token: 0x0400000D RID: 13
		ScrollWheel,
		// Token: 0x0400000E RID: 14
		Repaint,
		// Token: 0x0400000F RID: 15
		Layout,
		// Token: 0x04000010 RID: 16
		DragUpdated,
		// Token: 0x04000011 RID: 17
		DragPerform,
		// Token: 0x04000012 RID: 18
		DragExited = 15,
		// Token: 0x04000013 RID: 19
		Ignore = 11,
		// Token: 0x04000014 RID: 20
		Used,
		// Token: 0x04000015 RID: 21
		ValidateCommand,
		// Token: 0x04000016 RID: 22
		ExecuteCommand,
		// Token: 0x04000017 RID: 23
		ContextClick = 16,
		// Token: 0x04000018 RID: 24
		MouseEnterWindow = 20,
		// Token: 0x04000019 RID: 25
		MouseLeaveWindow,
		// Token: 0x0400001A RID: 26
		TouchDown = 30,
		// Token: 0x0400001B RID: 27
		TouchUp,
		// Token: 0x0400001C RID: 28
		TouchMove,
		// Token: 0x0400001D RID: 29
		TouchEnter,
		// Token: 0x0400001E RID: 30
		TouchLeave,
		// Token: 0x0400001F RID: 31
		TouchStationary,
		// Token: 0x04000020 RID: 32
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MouseDown instead (UnityUpgradable) -> MouseDown", true)]
		mouseDown = 0,
		// Token: 0x04000021 RID: 33
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MouseUp instead (UnityUpgradable) -> MouseUp", true)]
		mouseUp,
		// Token: 0x04000022 RID: 34
		[Obsolete("Use MouseMove instead (UnityUpgradable) -> MouseMove", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		mouseMove,
		// Token: 0x04000023 RID: 35
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MouseDrag instead (UnityUpgradable) -> MouseDrag", true)]
		mouseDrag,
		// Token: 0x04000024 RID: 36
		[Obsolete("Use KeyDown instead (UnityUpgradable) -> KeyDown", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		keyDown,
		// Token: 0x04000025 RID: 37
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use KeyUp instead (UnityUpgradable) -> KeyUp", true)]
		keyUp,
		// Token: 0x04000026 RID: 38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ScrollWheel instead (UnityUpgradable) -> ScrollWheel", true)]
		scrollWheel,
		// Token: 0x04000027 RID: 39
		[Obsolete("Use Repaint instead (UnityUpgradable) -> Repaint", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		repaint,
		// Token: 0x04000028 RID: 40
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Layout instead (UnityUpgradable) -> Layout", true)]
		layout,
		// Token: 0x04000029 RID: 41
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use DragUpdated instead (UnityUpgradable) -> DragUpdated", true)]
		dragUpdated,
		// Token: 0x0400002A RID: 42
		[Obsolete("Use DragPerform instead (UnityUpgradable) -> DragPerform", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		dragPerform,
		// Token: 0x0400002B RID: 43
		[Obsolete("Use Ignore instead (UnityUpgradable) -> Ignore", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ignore,
		// Token: 0x0400002C RID: 44
		[Obsolete("Use Used instead (UnityUpgradable) -> Used", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		used
	}
}

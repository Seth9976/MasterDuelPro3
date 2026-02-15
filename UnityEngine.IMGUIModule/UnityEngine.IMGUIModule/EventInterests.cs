using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EventInterests
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003A1A File Offset: 0x00001C1A
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003A22 File Offset: 0x00001C22
		public bool wantsMouseMove { readonly get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003A33 File Offset: 0x00001C33
		public bool wantsMouseEnterLeaveWindow { readonly get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003A3C File Offset: 0x00001C3C
		public readonly bool wantsLessLayoutEvents { get; }

		// Token: 0x0600005D RID: 93 RVA: 0x00003A44 File Offset: 0x00001C44
		public bool WantsEvent(EventType type)
		{
			bool flag;
			if (type != EventType.MouseMove)
			{
				flag = type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow;
			}
			else
			{
				flag = this.wantsMouseMove;
			}
			return flag;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003A7C File Offset: 0x00001C7C
		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					return this.wantsMouseMove;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					goto IL_006C;
				case EventType.KeyDown:
				case EventType.KeyUp:
					return GUIUtility.textFieldInput;
				case EventType.Repaint:
					break;
				default:
					if (type != EventType.ExecuteCommand)
					{
						if (type - EventType.MouseEnterWindow > 1)
						{
							goto IL_006C;
						}
						return this.wantsMouseEnterLeaveWindow;
					}
					break;
				}
				return true;
				IL_006C:
				flag2 = false;
			}
			return flag2;
		}
	}
}

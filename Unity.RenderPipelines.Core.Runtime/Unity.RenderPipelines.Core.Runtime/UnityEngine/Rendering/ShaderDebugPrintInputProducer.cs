using System;
using UnityEngine.InputSystem;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E0 RID: 224
	public static class ShaderDebugPrintInputProducer
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x000110FC File Offset: 0x0000F2FC
		public static ShaderDebugPrintInput Get()
		{
			ShaderDebugPrintInput r = default(ShaderDebugPrintInput);
			Mouse mouse = Mouse.current;
			r.pos = mouse.position.ReadValue();
			r.leftDown = mouse.leftButton.isPressed;
			r.rightDown = mouse.rightButton.isPressed;
			r.middleDown = mouse.middleButton.isPressed;
			return r;
		}
	}
}

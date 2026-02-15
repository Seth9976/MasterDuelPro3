using System;
using System.Diagnostics;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000D4 RID: 212
	[DebuggerDisplay("Value = {Get()}")]
	public class InputValue
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x0003B154 File Offset: 0x00039354
		public object Get()
		{
			return this.m_Context.Value.ReadValueAsObject();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0003B174 File Offset: 0x00039374
		public TValue Get<TValue>() where TValue : struct
		{
			if (this.m_Context == null)
			{
				throw new InvalidOperationException("Values can only be retrieved while in message callbacks");
			}
			return this.m_Context.Value.ReadValue<TValue>();
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0003B1AC File Offset: 0x000393AC
		public bool isPressed
		{
			get
			{
				return this.Get<float>() >= ButtonControl.s_GlobalDefaultButtonPressPoint;
			}
		}

		// Token: 0x040004EF RID: 1263
		internal InputAction.CallbackContext? m_Context;
	}
}

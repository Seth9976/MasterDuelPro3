using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000114 RID: 276
	internal class ExtendedAxisEventData : AxisEventData
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x00041FB4 File Offset: 0x000401B4
		public ExtendedAxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00041FBD File Offset: 0x000401BD
		public override string ToString()
		{
			return string.Format("MoveDir: {0}\nMoveVector: {1}", base.moveDir, base.moveVector);
		}
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017C RID: 380
	internal class CursorManager : ICursorManager
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00035DFE File Offset: 0x00033FFE
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00035E06 File Offset: 0x00034006
		public bool isCursorOverriden { get; private set; }

		// Token: 0x06000B21 RID: 2849 RVA: 0x00035E10 File Offset: 0x00034010
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
				this.isCursorOverriden = true;
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00035E74 File Offset: 0x00034074
		public void ResetCursor()
		{
			bool isCursorOverriden = this.isCursorOverriden;
			if (isCursorOverriden)
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
			this.isCursorOverriden = false;
		}
	}
}

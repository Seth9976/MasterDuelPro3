using System;
using UnityEngine.InputSystem;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D7 RID: 215
	public class MousePositionDebug
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000104EF File Offset: 0x0000E6EF
		public static MousePositionDebug instance
		{
			get
			{
				if (MousePositionDebug.s_Instance == null)
				{
					MousePositionDebug.s_Instance = new MousePositionDebug();
				}
				return MousePositionDebug.s_Instance;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00005704 File Offset: 0x00003904
		public void Build()
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00005704 File Offset: 0x00003904
		public void Cleanup()
		{
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00010507 File Offset: 0x0000E707
		public Vector2 GetMousePosition(float ScreenHeight, bool sceneView)
		{
			return this.GetInputMousePosition();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001050F File Offset: 0x0000E70F
		private Vector2 GetInputMousePosition()
		{
			if (Pointer.current == null)
			{
				return new Vector2(-1f, -1f);
			}
			return Pointer.current.position.ReadValue();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00010537 File Offset: 0x0000E737
		public Vector2 GetMouseClickPosition(float ScreenHeight)
		{
			return Vector2.zero;
		}

		// Token: 0x04000292 RID: 658
		private static MousePositionDebug s_Instance;
	}
}

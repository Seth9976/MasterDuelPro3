using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000022 RID: 34
	public class GraphicRegistry
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006915 File Offset: 0x00004B15
		protected GraphicRegistry()
		{
			GC.KeepAlive(new Dictionary<Graphic, int>());
			GC.KeepAlive(new Dictionary<ICanvasElement, int>());
			GC.KeepAlive(new Dictionary<IClipper, int>());
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006951 File Offset: 0x00004B51
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000696C File Offset: 0x00004B6C
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out graphics);
			if (graphics != null)
			{
				graphics.AddUnique(graphic, true);
				GraphicRegistry.RegisterRaycastGraphicForCanvas(c, graphic);
				return;
			}
			graphics = new IndexedSet<Graphic>();
			graphics.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, graphics);
			GraphicRegistry.RegisterRaycastGraphicForCanvas(c, graphic);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static void RegisterRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null || !graphic.raycastTarget)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out graphics);
			if (graphics != null)
			{
				graphics.AddUnique(graphic, true);
				return;
			}
			graphics = new IndexedSet<Graphic>();
			graphics.Add(graphic);
			GraphicRegistry.instance.m_RaycastableGraphics.Add(c, graphics);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006A40 File Offset: 0x00004C40
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out graphics))
			{
				graphics.Remove(graphic);
				if (graphics.Capacity == 0)
				{
					GraphicRegistry.instance.m_Graphics.Remove(c);
				}
				GraphicRegistry.UnregisterRaycastGraphicForCanvas(c, graphic);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006A9C File Offset: 0x00004C9C
		public static void UnregisterRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out graphics))
			{
				graphics.Remove(graphic);
				if (graphics.Count == 0)
				{
					GraphicRegistry.instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static void DisableGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out graphics))
			{
				graphics.DisableItem(graphic);
				if (graphics.Capacity == 0)
				{
					GraphicRegistry.instance.m_Graphics.Remove(c);
				}
				GraphicRegistry.DisableRaycastGraphicForCanvas(c, graphic);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006B48 File Offset: 0x00004D48
		public static void DisableRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || !graphic.raycastTarget)
			{
				return;
			}
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out graphics))
			{
				graphics.DisableItem(graphic);
				if (graphics.Capacity == 0)
				{
					GraphicRegistry.instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006B9C File Offset: 0x00004D9C
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out graphics))
			{
				return graphics;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public static IList<Graphic> GetRaycastableGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> graphics;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(canvas, out graphics))
			{
				return graphics;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x04000092 RID: 146
		private static GraphicRegistry s_Instance;

		// Token: 0x04000093 RID: 147
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x04000094 RID: 148
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_RaycastableGraphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x04000095 RID: 149
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}

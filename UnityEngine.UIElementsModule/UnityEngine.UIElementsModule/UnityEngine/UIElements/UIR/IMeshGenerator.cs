using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000535 RID: 1333
	internal interface IMeshGenerator
	{
		// Token: 0x17000979 RID: 2425
		// (set) Token: 0x060024CA RID: 9418
		VisualElement currentElement { set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060024CB RID: 9419
		UITKTextJobSystem textJobSystem { get; }

		// Token: 0x060024CC RID: 9420
		void DrawText(List<NativeSlice<Vertex>> vertices, List<NativeSlice<ushort>> indices, List<Material> materials, List<GlyphRenderMode> renderModes);

		// Token: 0x060024CD RID: 9421
		void DrawNativeText(NativeTextInfo textInfo, Vector2 pos);

		// Token: 0x060024CE RID: 9422
		void DrawRectangle(MeshGenerator.RectangleParams rectParams);

		// Token: 0x060024CF RID: 9423
		void DrawBorder(MeshGenerator.BorderParams borderParams);

		// Token: 0x060024D0 RID: 9424
		void DrawRectangleRepeat(MeshGenerator.RectangleParams rectParams, Rect totalRect, float scaledPixelsPerPoint);

		// Token: 0x060024D1 RID: 9425
		void ScheduleJobs(MeshGenerationContext mgc);
	}
}

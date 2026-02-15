using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies the desired data or view aspect of the object when drawing or getting data.</summary>
	// Token: 0x02000114 RID: 276
	[Flags]
	public enum DVASPECT
	{
		/// <summary>A representation of an object that lets that object be displayed as an embedded object inside a container. This value is typically specified for compound document objects. The presentation can be provided for the screen or printer.</summary>
		// Token: 0x0400049E RID: 1182
		DVASPECT_CONTENT = 1,
		/// <summary>A thumbnail representation of an object that lets that object be displayed in a browsing tool. The thumbnail is approximately a 120 by 120 pixel, 16-color (recommended), device-independent bitmap potentially wrapped in a metafile.</summary>
		// Token: 0x0400049F RID: 1183
		DVASPECT_THUMBNAIL = 2,
		/// <summary>An iconic representation of an object.</summary>
		// Token: 0x040004A0 RID: 1184
		DVASPECT_ICON = 4,
		/// <summary>A representation of an object on the screen as though it were printed to a printer using the Print command from the File menu. The described data may represent a sequence of pages.</summary>
		// Token: 0x040004A1 RID: 1185
		DVASPECT_DOCPRINT = 8
	}
}

using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000008 RID: 8
	public enum FontEngineError
	{
		// Token: 0x04000039 RID: 57
		Success,
		// Token: 0x0400003A RID: 58
		Invalid_File_Path,
		// Token: 0x0400003B RID: 59
		Invalid_File_Format,
		// Token: 0x0400003C RID: 60
		Invalid_File_Structure,
		// Token: 0x0400003D RID: 61
		Invalid_File,
		// Token: 0x0400003E RID: 62
		Invalid_Table = 8,
		// Token: 0x0400003F RID: 63
		Invalid_Glyph_Index = 16,
		// Token: 0x04000040 RID: 64
		Invalid_Character_Code,
		// Token: 0x04000041 RID: 65
		Invalid_Pixel_Size = 23,
		// Token: 0x04000042 RID: 66
		Invalid_Library = 33,
		// Token: 0x04000043 RID: 67
		Invalid_Face = 35,
		// Token: 0x04000044 RID: 68
		Invalid_Library_or_Face = 41,
		// Token: 0x04000045 RID: 69
		Atlas_Generation_Cancelled = 100,
		// Token: 0x04000046 RID: 70
		Invalid_SharedTextureData,
		// Token: 0x04000047 RID: 71
		OpenTypeLayoutLookup_Mismatch = 116
	}
}

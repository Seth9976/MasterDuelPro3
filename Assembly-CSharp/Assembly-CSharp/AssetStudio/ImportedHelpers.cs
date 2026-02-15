using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000164 RID: 356
	public static class ImportedHelpers
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public static ImportedMesh FindMesh(string path, List<ImportedMesh> importedMeshList)
		{
			foreach (ImportedMesh mesh in importedMeshList)
			{
				if (mesh.Path == path)
				{
					return mesh;
				}
			}
			return null;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015B50 File Offset: 0x00013D50
		public static ImportedMaterial FindMaterial(string name, List<ImportedMaterial> importedMats)
		{
			foreach (ImportedMaterial mat in importedMats)
			{
				if (mat.Name == name)
				{
					return mat;
				}
			}
			return null;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015BAC File Offset: 0x00013DAC
		public static ImportedTexture FindTexture(string name, List<ImportedTexture> importedTextureList)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			foreach (ImportedTexture tex in importedTextureList)
			{
				if (tex.Name == name)
				{
					return tex;
				}
			}
			return null;
		}
	}
}

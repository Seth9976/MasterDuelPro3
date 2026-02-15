using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainCallbacks
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				foreach (Terrain user in terrainData.users)
				{
					TerrainCallbacks.heightmapChanged(user, heightRegion, synched);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				foreach (Terrain user in terrainData.users)
				{
					TerrainCallbacks.textureChanged(user, textureName, texelRegion, synched);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static TerrainCallbacks.TextureChangedCallback textureChanged;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x0600000E RID: 14
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000010 RID: 16
		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);
	}
}

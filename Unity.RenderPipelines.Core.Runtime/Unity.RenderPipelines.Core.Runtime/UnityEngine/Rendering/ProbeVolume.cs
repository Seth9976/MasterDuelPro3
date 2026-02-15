using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000121 RID: 289
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Adaptive Probe Volume")]
	public class ProbeVolume : MonoBehaviour
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0001E34C File Offset: 0x0001C54C
		private void Awake()
		{
			if (this.version == ProbeVolume.Version.Count)
			{
				return;
			}
			if (this.version == ProbeVolume.Version.Initial)
			{
				this.mode = (this.globalVolume ? ProbeVolume.Mode.Scene : ProbeVolume.Mode.Local);
				this.version++;
			}
			if (this.version == ProbeVolume.Version.LocalMode)
			{
				this.version++;
			}
		}

		// Token: 0x0400052B RID: 1323
		[SerializeField]
		private ProbeVolume.Version version;

		// Token: 0x0400052C RID: 1324
		[SerializeField]
		[Obsolete("Use mode instead")]
		public bool globalVolume;

		// Token: 0x0400052D RID: 1325
		[Tooltip("When set to Global this Probe Volume considers all renderers with Contribute Global Illumination enabled. Local only considers renderers in the scene.\nThis list updates every time the Scene is saved or the lighting is baked.")]
		public ProbeVolume.Mode mode = ProbeVolume.Mode.Local;

		// Token: 0x0400052E RID: 1326
		public Vector3 size = new Vector3(10f, 10f, 10f);

		// Token: 0x0400052F RID: 1327
		[HideInInspector]
		[Min(0f)]
		public bool overrideRendererFilters;

		// Token: 0x04000530 RID: 1328
		[HideInInspector]
		[Min(0f)]
		public float minRendererVolumeSize = 0.1f;

		// Token: 0x04000531 RID: 1329
		public LayerMask objectLayerMask = -1;

		// Token: 0x04000532 RID: 1330
		[HideInInspector]
		public int lowestSubdivLevelOverride;

		// Token: 0x04000533 RID: 1331
		[HideInInspector]
		public int highestSubdivLevelOverride = 7;

		// Token: 0x04000534 RID: 1332
		[HideInInspector]
		public bool overridesSubdivLevels;

		// Token: 0x04000535 RID: 1333
		[SerializeField]
		internal bool mightNeedRebaking;

		// Token: 0x04000536 RID: 1334
		[SerializeField]
		internal Matrix4x4 cachedTransform;

		// Token: 0x04000537 RID: 1335
		[SerializeField]
		internal int cachedHashCode;

		// Token: 0x04000538 RID: 1336
		[HideInInspector]
		[Tooltip("Whether Unity should fill empty space between renderers with bricks at the highest subdivision level.")]
		public bool fillEmptySpaces;

		// Token: 0x02000122 RID: 290
		private enum Version
		{
			// Token: 0x0400053A RID: 1338
			Initial,
			// Token: 0x0400053B RID: 1339
			LocalMode,
			// Token: 0x0400053C RID: 1340
			InvertOverrideLevels,
			// Token: 0x0400053D RID: 1341
			Count
		}

		// Token: 0x02000123 RID: 291
		public enum Mode
		{
			// Token: 0x0400053F RID: 1343
			Global,
			// Token: 0x04000540 RID: 1344
			Scene,
			// Token: 0x04000541 RID: 1345
			Local
		}
	}
}

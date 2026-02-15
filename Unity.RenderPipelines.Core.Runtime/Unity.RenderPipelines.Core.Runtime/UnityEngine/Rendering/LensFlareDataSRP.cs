using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public sealed class LensFlareDataSRP : ScriptableObject
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00024C9C File Offset: 0x00022E9C
		public LensFlareDataSRP()
		{
			this.elements = null;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00024CAC File Offset: 0x00022EAC
		public bool HasAModulateByLightColorElement()
		{
			if (this.elements != null)
			{
				LensFlareDataElementSRP[] array = this.elements;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].modulateByLightColor)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040006B7 RID: 1719
		public LensFlareDataElementSRP[] elements;
	}
}

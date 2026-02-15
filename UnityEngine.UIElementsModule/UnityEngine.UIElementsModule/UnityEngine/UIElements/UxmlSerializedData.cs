using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004AC RID: 1196
	[Serializable]
	public abstract class UxmlSerializedData
	{
		// Token: 0x0600222E RID: 8750
		public abstract object CreateInstance();

		// Token: 0x0600222F RID: 8751
		public abstract void Deserialize(object obj);

		// Token: 0x04000F18 RID: 3864
		internal const string AttributeFlagSuffix = "_UxmlAttributeFlags";

		// Token: 0x04000F19 RID: 3865
		private const UxmlSerializedData.UxmlAttributeFlags k_DefaultFlags = UxmlSerializedData.UxmlAttributeFlags.OverriddenInUxml;

		// Token: 0x04000F1A RID: 3866
		[SerializeField]
		[UxmlIgnore]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[HideInInspector]
		internal int uxmlAssetId;

		// Token: 0x04000F1B RID: 3867
		private static UxmlSerializedData.UxmlAttributeFlags s_CurrentDeserializeFlags = UxmlSerializedData.UxmlAttributeFlags.OverriddenInUxml;

		// Token: 0x020004AD RID: 1197
		[Flags]
		public enum UxmlAttributeFlags : byte
		{
			// Token: 0x04000F1D RID: 3869
			Ignore = 0,
			// Token: 0x04000F1E RID: 3870
			OverriddenInUxml = 1,
			// Token: 0x04000F1F RID: 3871
			DefaultValue = 2
		}
	}
}

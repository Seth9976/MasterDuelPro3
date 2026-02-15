using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004BE RID: 1214
	public struct CreationContext : IEquatable<CreationContext>
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0007FB8B File Offset: 0x0007DD8B
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x0007FB93 File Offset: 0x0007DD93
		public VisualElement target { readonly get; private set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x0007FB9C File Offset: 0x0007DD9C
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0007FBA4 File Offset: 0x0007DDA4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal List<int> veaIdsPath
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			readonly get;
			private set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0007FBAD File Offset: 0x0007DDAD
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x0007FBB5 File Offset: 0x0007DDB5
		public VisualTreeAsset visualTreeAsset { readonly get; private set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0007FBBE File Offset: 0x0007DDBE
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0007FBC6 File Offset: 0x0007DDC6
		public Dictionary<string, VisualElement> slotInsertionPoints { readonly get; private set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0007FBCF File Offset: 0x0007DDCF
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0007FBD7 File Offset: 0x0007DDD7
		internal List<CreationContext.AttributeOverrideRange> attributeOverrides { readonly get; private set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0007FBE0 File Offset: 0x0007DDE0
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x0007FBE8 File Offset: 0x0007DDE8
		internal List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides { readonly get; private set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x0007FBF1 File Offset: 0x0007DDF1
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x0007FBF9 File Offset: 0x0007DDF9
		internal List<string> namesPath { readonly get; private set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x0007FC02 File Offset: 0x0007DE02
		internal bool hasOverrides
		{
			get
			{
				List<CreationContext.AttributeOverrideRange> attributeOverrides = this.attributeOverrides;
				bool flag;
				if (attributeOverrides == null || attributeOverrides.Count <= 0)
				{
					List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides = this.serializedDataOverrides;
					flag = serializedDataOverrides != null && serializedDataOverrides.Count > 0;
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007FC33 File Offset: 0x0007DE33
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(VisualTreeAsset vta)
		{
			this = new CreationContext(null, vta, null);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007FC40 File Offset: 0x0007DE40
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides)
		{
			this = new CreationContext(slotInsertionPoints, attributeOverrides, null, null);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007FC4E File Offset: 0x0007DE4E
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, null, vta, target);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007FC5C File Offset: 0x0007DE5C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, attributeOverrides, null, vta, target, null, null);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0007FC6E File Offset: 0x0007DE6E
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides, List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides, VisualTreeAsset vta, VisualElement target, List<int> veaIdsPath, List<string> namesPath)
		{
			this.target = target;
			this.slotInsertionPoints = slotInsertionPoints;
			this.attributeOverrides = attributeOverrides;
			this.serializedDataOverrides = serializedDataOverrides;
			this.visualTreeAsset = vta;
			this.namesPath = namesPath;
			this.veaIdsPath = veaIdsPath;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
		public override bool Equals(object obj)
		{
			return obj is CreationContext && this.Equals((CreationContext)obj);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007FCDC File Offset: 0x0007DEDC
		public bool Equals(CreationContext other)
		{
			return EqualityComparer<VisualElement>.Default.Equals(this.target, other.target) && EqualityComparer<VisualTreeAsset>.Default.Equals(this.visualTreeAsset, other.visualTreeAsset) && EqualityComparer<Dictionary<string, VisualElement>>.Default.Equals(this.slotInsertionPoints, other.slotInsertionPoints);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007FD3C File Offset: 0x0007DF3C
		public override int GetHashCode()
		{
			int hashCode = -2123482148;
			hashCode = hashCode * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.target);
			hashCode = hashCode * -1521134295 + EqualityComparer<VisualTreeAsset>.Default.GetHashCode(this.visualTreeAsset);
			return hashCode * -1521134295 + EqualityComparer<Dictionary<string, VisualElement>>.Default.GetHashCode(this.slotInsertionPoints);
		}

		// Token: 0x04000F61 RID: 3937
		public static readonly CreationContext Default = default(CreationContext);

		// Token: 0x020004BF RID: 1215
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal struct AttributeOverrideRange
		{
			// Token: 0x060022B8 RID: 8888 RVA: 0x0007FDAD File Offset: 0x0007DFAD
			public AttributeOverrideRange(VisualTreeAsset sourceAsset, List<TemplateAsset.AttributeOverride> attributeOverrides)
			{
				this.sourceAsset = sourceAsset;
				this.attributeOverrides = attributeOverrides;
			}

			// Token: 0x04000F69 RID: 3945
			internal readonly VisualTreeAsset sourceAsset;

			// Token: 0x04000F6A RID: 3946
			internal readonly List<TemplateAsset.AttributeOverride> attributeOverrides;
		}

		// Token: 0x020004C0 RID: 1216
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal struct SerializedDataOverrideRange
		{
			// Token: 0x04000F6B RID: 3947
			internal readonly VisualTreeAsset sourceAsset;

			// Token: 0x04000F6C RID: 3948
			internal readonly int templateId;

			// Token: 0x04000F6D RID: 3949
			internal readonly List<TemplateAsset.UxmlSerializedDataOverride> attributeOverrides;
		}
	}
}

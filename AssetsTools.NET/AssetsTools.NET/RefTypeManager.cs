using System;
using System.Collections.Generic;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000059 RID: 89
	public class RefTypeManager
	{
		// Token: 0x06000315 RID: 789 RVA: 0x000141D8 File Offset: 0x000123D8
		public RefTypeManager()
		{
			this.typeTreeLookup = new Dictionary<AssetTypeReference, AssetTypeTemplateField>();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000141F0 File Offset: 0x000123F0
		public void Clear()
		{
			this.typeTreeLookup.Clear();
			bool flag = !this.isSharedMonoLookup;
			if (flag)
			{
				this.monoTemplateLookup.Clear();
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00014228 File Offset: 0x00012428
		public void FromTypeTree(AssetsFileMetadata metadata)
		{
			bool flag = !metadata.TypeTreeEnabled || metadata.RefTypes == null;
			if (!flag)
			{
				foreach (TypeTreeType typeTreeType in metadata.RefTypes)
				{
					bool flag2 = !typeTreeType.IsRefType;
					if (!flag2)
					{
						AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField();
						assetTypeTemplateField.FromTypeTree(typeTreeType);
						bool flag3 = assetTypeTemplateField.Children.Count > 0 && assetTypeTemplateField.Children[assetTypeTemplateField.Children.Count - 1].ValueType == AssetValueType.ManagedReferencesRegistry;
						if (flag3)
						{
							assetTypeTemplateField.Children.RemoveAt(assetTypeTemplateField.Children.Count - 1);
						}
						this.typeTreeLookup[typeTreeType.TypeReference] = assetTypeTemplateField;
					}
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00014324 File Offset: 0x00012524
		public void WithMonoTemplateGenerator(AssetsFileMetadata metadata, IMonoBehaviourTemplateGenerator monoTemplateGenerator, Dictionary<AssetTypeReference, AssetTypeTemplateField> monoTemplateFieldCache = null)
		{
			this.monoTemplateGenerator = monoTemplateGenerator;
			this.unityVersion = new UnityVersion(metadata.UnityVersion);
			this.monoTemplateLookup = monoTemplateFieldCache ?? new Dictionary<AssetTypeReference, AssetTypeTemplateField>();
			this.isSharedMonoLookup = this.monoTemplateLookup != null;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00014360 File Offset: 0x00012560
		public AssetTypeTemplateField GetTemplateField(AssetTypeReference type)
		{
			bool flag = type == null || (string.IsNullOrEmpty(type.ClassName) && string.IsNullOrEmpty(type.Namespace) && string.IsNullOrEmpty(type.AsmName)) || type.Equals(AssetTypeReference.TERMINUS);
			AssetTypeTemplateField assetTypeTemplateField;
			if (flag)
			{
				assetTypeTemplateField = null;
			}
			else
			{
				AssetTypeTemplateField assetTypeTemplateField2;
				bool flag2 = this.typeTreeLookup.TryGetValue(type, out assetTypeTemplateField2);
				if (flag2)
				{
					assetTypeTemplateField = assetTypeTemplateField2;
				}
				else
				{
					bool flag3 = this.monoTemplateGenerator != null;
					if (flag3)
					{
						bool flag4 = this.monoTemplateLookup.TryGetValue(type, out assetTypeTemplateField2);
						if (flag4)
						{
							return assetTypeTemplateField2;
						}
						assetTypeTemplateField2 = new AssetTypeTemplateField
						{
							Name = "Base",
							Type = type.ClassName,
							ValueType = AssetValueType.None,
							IsArray = false,
							IsAligned = false,
							HasValue = false,
							Children = new List<AssetTypeTemplateField>(0)
						};
						assetTypeTemplateField2 = this.monoTemplateGenerator.GetTemplateField(assetTypeTemplateField2, type.AsmName, type.Namespace, type.ClassName, this.unityVersion);
						bool flag5 = assetTypeTemplateField2 != null;
						if (flag5)
						{
							this.monoTemplateLookup[type] = assetTypeTemplateField2;
							return assetTypeTemplateField2;
						}
					}
					assetTypeTemplateField = null;
				}
			}
			return assetTypeTemplateField;
		}

		// Token: 0x040001F9 RID: 505
		private Dictionary<AssetTypeReference, AssetTypeTemplateField> typeTreeLookup;

		// Token: 0x040001FA RID: 506
		private Dictionary<AssetTypeReference, AssetTypeTemplateField> monoTemplateLookup;

		// Token: 0x040001FB RID: 507
		private IMonoBehaviourTemplateGenerator monoTemplateGenerator;

		// Token: 0x040001FC RID: 508
		private UnityVersion unityVersion;

		// Token: 0x040001FD RID: 509
		private bool isSharedMonoLookup;
	}
}

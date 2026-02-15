using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000049 RID: 73
	public class TypeTreeType
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000112D9 File Offset: 0x0000F4D9
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000112E1 File Offset: 0x0000F4E1
		public int TypeId { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000112EA File Offset: 0x0000F4EA
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000112F2 File Offset: 0x0000F4F2
		public bool IsStrippedType { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000112FB File Offset: 0x0000F4FB
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00011303 File Offset: 0x0000F503
		public ushort ScriptTypeIndex { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0001130C File Offset: 0x0000F50C
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00011314 File Offset: 0x0000F514
		public Hash128 ScriptIdHash { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0001131D File Offset: 0x0000F51D
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00011325 File Offset: 0x0000F525
		public Hash128 TypeHash { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0001132E File Offset: 0x0000F52E
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00011336 File Offset: 0x0000F536
		public List<TypeTreeNode> Nodes { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0001133F File Offset: 0x0000F53F
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00011347 File Offset: 0x0000F547
		public byte[] StringBufferBytes { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00011350 File Offset: 0x0000F550
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00011358 File Offset: 0x0000F558
		public bool IsRefType { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00011361 File Offset: 0x0000F561
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00011369 File Offset: 0x0000F569
		public int[] TypeDependencies { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00011372 File Offset: 0x0000F572
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0001137A File Offset: 0x0000F57A
		public AssetTypeReference TypeReference { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00011383 File Offset: 0x0000F583
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00011395 File Offset: 0x0000F595
		public string StringBuffer
		{
			get
			{
				return Encoding.UTF8.GetString(this.StringBufferBytes);
			}
			set
			{
				this.StringBufferBytes = Encoding.UTF8.GetBytes(value);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000113AC File Offset: 0x0000F5AC
		public void Read(AssetsFileReader reader, uint version, bool hasTypeTree, bool isRefType)
		{
			this.TypeId = reader.ReadInt32();
			bool flag = version >= 16U;
			if (flag)
			{
				this.IsStrippedType = reader.ReadBoolean();
			}
			bool flag2 = version >= 17U;
			if (flag2)
			{
				this.ScriptTypeIndex = reader.ReadUInt16();
			}
			else
			{
				this.ScriptTypeIndex = ushort.MaxValue;
			}
			bool flag3 = (version < 17U && this.TypeId < 0) || (version >= 17U && this.TypeId == 114) || (isRefType && this.ScriptTypeIndex != ushort.MaxValue);
			if (flag3)
			{
				this.ScriptIdHash = new Hash128(reader);
			}
			this.TypeHash = new Hash128(reader);
			this.IsRefType = isRefType;
			if (hasTypeTree)
			{
				int num = reader.ReadInt32();
				int num2 = reader.ReadInt32();
				this.Nodes = new List<TypeTreeNode>(num);
				for (int i = 0; i < num; i++)
				{
					TypeTreeNode typeTreeNode = new TypeTreeNode();
					typeTreeNode.Read(reader, version);
					this.Nodes.Add(typeTreeNode);
				}
				this.StringBufferBytes = reader.ReadBytes(num2);
				bool flag4 = version >= 21U;
				if (flag4)
				{
					bool flag5 = !isRefType;
					if (flag5)
					{
						int num3 = reader.ReadInt32();
						this.TypeDependencies = new int[num3];
						for (int j = 0; j < num3; j++)
						{
							this.TypeDependencies[j] = reader.ReadInt32();
						}
					}
					else
					{
						this.TypeReference = new AssetTypeReference();
						this.TypeReference.ReadMetadata(reader);
					}
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00011554 File Offset: 0x0000F754
		public void Write(AssetsFileWriter writer, uint version, bool hasTypeTree)
		{
			writer.Write(this.TypeId);
			bool flag = version >= 16U;
			if (flag)
			{
				writer.Write(this.IsStrippedType);
			}
			bool flag2 = version >= 17U;
			if (flag2)
			{
				writer.Write(this.ScriptTypeIndex);
			}
			bool flag3 = (version < 17U && this.TypeId < 0) || (version >= 17U && this.TypeId == 114) || (this.IsRefType && this.ScriptTypeIndex != ushort.MaxValue);
			if (flag3)
			{
				writer.Write(this.ScriptIdHash.data);
			}
			writer.Write(this.TypeHash.data);
			if (hasTypeTree)
			{
				writer.Write(this.Nodes.Count);
				writer.Write(this.StringBufferBytes.Length);
				for (int i = 0; i < this.Nodes.Count; i++)
				{
					this.Nodes[i].Write(writer, version);
				}
				writer.Write(this.StringBufferBytes);
				bool flag4 = version >= 21U;
				if (flag4)
				{
					bool flag5 = !this.IsRefType;
					if (flag5)
					{
						writer.Write(this.TypeDependencies.Length);
						for (int j = 0; j < this.TypeDependencies.Length; j++)
						{
							writer.Write(this.TypeDependencies[j]);
						}
					}
					else
					{
						this.TypeReference.WriteMetadata(writer);
					}
				}
			}
		}

		// Token: 0x040001B4 RID: 436
		public const string COMMON_STRING_TABLE = "AABB\0AnimationClip\0AnimationCurve\0AnimationState\0Array\0Base\0BitField\0bitset\0bool\0char\0ColorRGBA\0Component\0data\0deque\0double\0dynamic_array\0FastPropertyName\0first\0float\0Font\0GameObject\0Generic Mono\0GradientNEW\0GUID\0GUIStyle\0int\0list\0long long\0map\0Matrix4x4f\0MdFour\0MonoBehaviour\0MonoScript\0m_ByteSize\0m_Curve\0m_EditorClassIdentifier\0m_EditorHideFlags\0m_Enabled\0m_ExtensionPtr\0m_GameObject\0m_Index\0m_IsArray\0m_IsStatic\0m_MetaFlag\0m_Name\0m_ObjectHideFlags\0m_PrefabInternal\0m_PrefabParentObject\0m_Script\0m_StaticEditorFlags\0m_Type\0m_Version\0Object\0pair\0PPtr<Component>\0PPtr<GameObject>\0PPtr<Material>\0PPtr<MonoBehaviour>\0PPtr<MonoScript>\0PPtr<Object>\0PPtr<Prefab>\0PPtr<Sprite>\0PPtr<TextAsset>\0PPtr<Texture>\0PPtr<Texture2D>\0PPtr<Transform>\0Prefab\0Quaternionf\0Rectf\0RectInt\0RectOffset\0second\0set\0short\0size\0SInt16\0SInt32\0SInt64\0SInt8\0staticvector\0string\0TextAsset\0TextMesh\0Texture\0Texture2D\0Transform\0TypelessData\0UInt16\0UInt32\0UInt64\0UInt8\0unsigned int\0unsigned long long\0unsigned short\0vector\0Vector2f\0Vector3f\0Vector4f\0m_ScriptingClassIdentifier\0Gradient\0Type*\0int2_storage\0int3_storage\0BoundsInt\0m_CorrespondingSourceObject\0m_PrefabInstance\0m_PrefabAsset\0FileSize\0Hash128\0";
	}
}

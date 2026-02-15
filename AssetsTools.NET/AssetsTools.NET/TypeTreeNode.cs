using System;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000047 RID: 71
	public class TypeTreeNode
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0001105D File Offset: 0x0000F25D
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00011065 File Offset: 0x0000F265
		public ushort Version { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0001106E File Offset: 0x0000F26E
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00011076 File Offset: 0x0000F276
		public byte Level { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0001107F File Offset: 0x0000F27F
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00011087 File Offset: 0x0000F287
		public TypeTreeNodeFlags TypeFlags { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00011090 File Offset: 0x0000F290
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00011098 File Offset: 0x0000F298
		public uint TypeStrOffset { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000110A1 File Offset: 0x0000F2A1
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000110A9 File Offset: 0x0000F2A9
		public uint NameStrOffset { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000110B2 File Offset: 0x0000F2B2
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000110BA File Offset: 0x0000F2BA
		public int ByteSize { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000110C3 File Offset: 0x0000F2C3
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000110CB File Offset: 0x0000F2CB
		public uint Index { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000110D4 File Offset: 0x0000F2D4
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000110DC File Offset: 0x0000F2DC
		public uint MetaFlags { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000110E5 File Offset: 0x0000F2E5
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000110ED File Offset: 0x0000F2ED
		public ulong RefTypeHash { get; set; }

		// Token: 0x0600021B RID: 539 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public void Read(AssetsFileReader reader, uint version)
		{
			this.Version = reader.ReadUInt16();
			this.Level = reader.ReadByte();
			this.TypeFlags = (TypeTreeNodeFlags)reader.ReadByte();
			this.TypeStrOffset = reader.ReadUInt32();
			this.NameStrOffset = reader.ReadUInt32();
			this.ByteSize = reader.ReadInt32();
			this.Index = reader.ReadUInt32();
			this.MetaFlags = reader.ReadUInt32();
			bool flag = version >= 18U;
			if (flag)
			{
				this.RefTypeHash = reader.ReadUInt64();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001118C File Offset: 0x0000F38C
		public void Write(AssetsFileWriter writer, uint version)
		{
			writer.Write(this.Version);
			writer.Write(this.Level);
			writer.Write((byte)this.TypeFlags);
			writer.Write(this.TypeStrOffset);
			writer.Write(this.NameStrOffset);
			writer.Write(this.ByteSize);
			writer.Write(this.Index);
			writer.Write(this.MetaFlags);
			bool flag = version >= 18U;
			if (flag)
			{
				writer.Write(this.RefTypeHash);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00011220 File Offset: 0x0000F420
		public string GetTypeString(string stringTable, string commonStringTable = null)
		{
			return this.ReadStringTableString(stringTable, commonStringTable ?? "AABB\0AnimationClip\0AnimationCurve\0AnimationState\0Array\0Base\0BitField\0bitset\0bool\0char\0ColorRGBA\0Component\0data\0deque\0double\0dynamic_array\0FastPropertyName\0first\0float\0Font\0GameObject\0Generic Mono\0GradientNEW\0GUID\0GUIStyle\0int\0list\0long long\0map\0Matrix4x4f\0MdFour\0MonoBehaviour\0MonoScript\0m_ByteSize\0m_Curve\0m_EditorClassIdentifier\0m_EditorHideFlags\0m_Enabled\0m_ExtensionPtr\0m_GameObject\0m_Index\0m_IsArray\0m_IsStatic\0m_MetaFlag\0m_Name\0m_ObjectHideFlags\0m_PrefabInternal\0m_PrefabParentObject\0m_Script\0m_StaticEditorFlags\0m_Type\0m_Version\0Object\0pair\0PPtr<Component>\0PPtr<GameObject>\0PPtr<Material>\0PPtr<MonoBehaviour>\0PPtr<MonoScript>\0PPtr<Object>\0PPtr<Prefab>\0PPtr<Sprite>\0PPtr<TextAsset>\0PPtr<Texture>\0PPtr<Texture2D>\0PPtr<Transform>\0Prefab\0Quaternionf\0Rectf\0RectInt\0RectOffset\0second\0set\0short\0size\0SInt16\0SInt32\0SInt64\0SInt8\0staticvector\0string\0TextAsset\0TextMesh\0Texture\0Texture2D\0Transform\0TypelessData\0UInt16\0UInt32\0UInt64\0UInt8\0unsigned int\0unsigned long long\0unsigned short\0vector\0Vector2f\0Vector3f\0Vector4f\0m_ScriptingClassIdentifier\0Gradient\0Type*\0int2_storage\0int3_storage\0BoundsInt\0m_CorrespondingSourceObject\0m_PrefabInstance\0m_PrefabAsset\0FileSize\0Hash128\0", this.TypeStrOffset);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0001124C File Offset: 0x0000F44C
		public string GetNameString(string stringTable, string commonStringTable = null)
		{
			return this.ReadStringTableString(stringTable, commonStringTable ?? "AABB\0AnimationClip\0AnimationCurve\0AnimationState\0Array\0Base\0BitField\0bitset\0bool\0char\0ColorRGBA\0Component\0data\0deque\0double\0dynamic_array\0FastPropertyName\0first\0float\0Font\0GameObject\0Generic Mono\0GradientNEW\0GUID\0GUIStyle\0int\0list\0long long\0map\0Matrix4x4f\0MdFour\0MonoBehaviour\0MonoScript\0m_ByteSize\0m_Curve\0m_EditorClassIdentifier\0m_EditorHideFlags\0m_Enabled\0m_ExtensionPtr\0m_GameObject\0m_Index\0m_IsArray\0m_IsStatic\0m_MetaFlag\0m_Name\0m_ObjectHideFlags\0m_PrefabInternal\0m_PrefabParentObject\0m_Script\0m_StaticEditorFlags\0m_Type\0m_Version\0Object\0pair\0PPtr<Component>\0PPtr<GameObject>\0PPtr<Material>\0PPtr<MonoBehaviour>\0PPtr<MonoScript>\0PPtr<Object>\0PPtr<Prefab>\0PPtr<Sprite>\0PPtr<TextAsset>\0PPtr<Texture>\0PPtr<Texture2D>\0PPtr<Transform>\0Prefab\0Quaternionf\0Rectf\0RectInt\0RectOffset\0second\0set\0short\0size\0SInt16\0SInt32\0SInt64\0SInt8\0staticvector\0string\0TextAsset\0TextMesh\0Texture\0Texture2D\0Transform\0TypelessData\0UInt16\0UInt32\0UInt64\0UInt8\0unsigned int\0unsigned long long\0unsigned short\0vector\0Vector2f\0Vector3f\0Vector4f\0m_ScriptingClassIdentifier\0Gradient\0Type*\0int2_storage\0int3_storage\0BoundsInt\0m_CorrespondingSourceObject\0m_PrefabInstance\0m_PrefabAsset\0FileSize\0Hash128\0", this.NameStrOffset);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00011278 File Offset: 0x0000F478
		private string ReadStringTableString(string stringTable, string commonStringTable, uint offset)
		{
			bool flag = offset >= 2147483648U;
			if (flag)
			{
				offset &= 2147483647U;
				stringTable = commonStringTable;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = (int)offset;
			char c;
			while ((c = stringTable[num]) > '\0')
			{
				stringBuilder.Append(c);
				num++;
			}
			return stringBuilder.ToString();
		}
	}
}

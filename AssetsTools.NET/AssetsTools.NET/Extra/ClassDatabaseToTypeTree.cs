using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000085 RID: 133
	public class ClassDatabaseToTypeTree
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001A084 File Offset: 0x00018284
		public static TypeTreeType Convert(ClassDatabaseFile classes, string name, bool preferEditor = false)
		{
			ClassDatabaseType classDatabaseType = classes.FindAssetClassByName(name);
			return ClassDatabaseToTypeTree.Convert(classes, classDatabaseType, preferEditor);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001A0A8 File Offset: 0x000182A8
		public static TypeTreeType Convert(ClassDatabaseFile classes, int id, bool preferEditor = false)
		{
			ClassDatabaseType classDatabaseType = classes.FindAssetClassByID(id);
			return ClassDatabaseToTypeTree.Convert(classes, classDatabaseType, preferEditor);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001A0CC File Offset: 0x000182CC
		public static TypeTreeType Convert(ClassDatabaseFile classes, ClassDatabaseType type, bool preferEditor = false)
		{
			ClassDatabaseToTypeTree classDatabaseToTypeTree = new ClassDatabaseToTypeTree();
			return classDatabaseToTypeTree.ConvertInternal(classes, type, preferEditor);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001A0F0 File Offset: 0x000182F0
		private TypeTreeType ConvertInternal(ClassDatabaseFile classes, ClassDatabaseType type, bool preferEditor = false)
		{
			TypeTreeType typeTreeType = new TypeTreeType
			{
				TypeId = type.ClassId,
				ScriptTypeIndex = ushort.MaxValue,
				IsStrippedType = false,
				ScriptIdHash = Hash128.NewBlankHash(),
				TypeHash = Hash128.NewBlankHash(),
				TypeDependencies = new int[0]
			};
			this.cldbFile = classes;
			this.stringTablePos = 0U;
			this.stringTableLookup = new Dictionary<string, uint>();
			this.commonStringTableLookup = new Dictionary<string, uint>();
			this.typeTreeNodes = new List<TypeTreeNode>();
			this.InitializeDefaultStringTableIndices();
			ClassDatabaseTypeNode preferredNode = type.GetPreferredNode(preferEditor);
			this.ConvertFields(preferredNode, 0);
			StringBuilder stringBuilder = new StringBuilder();
			List<KeyValuePair<string, uint>> list = this.stringTableLookup.OrderBy((KeyValuePair<string, uint> n) => n.Value).ToList<KeyValuePair<string, uint>>();
			foreach (KeyValuePair<string, uint> keyValuePair in list)
			{
				stringBuilder.Append(keyValuePair.Key + "\0");
			}
			typeTreeType.StringBuffer = stringBuilder.ToString();
			typeTreeType.Nodes = this.typeTreeNodes;
			return typeTreeType;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001A24C File Offset: 0x0001844C
		private void InitializeDefaultStringTableIndices()
		{
			int num = 0;
			List<ushort> commonStringBufferIndices = this.cldbFile.CommonStringBufferIndices;
			foreach (ushort num2 in commonStringBufferIndices)
			{
				string @string = this.cldbFile.StringTable.GetString(num2);
				bool flag = @string != string.Empty;
				if (flag)
				{
					this.commonStringTableLookup.Add(@string, (uint)num);
					num += @string.Length + 1;
				}
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001A2E8 File Offset: 0x000184E8
		private void ConvertFields(ClassDatabaseTypeNode node, int depth)
		{
			string @string = this.cldbFile.GetString(node.FieldName);
			string string2 = this.cldbFile.GetString(node.TypeName);
			bool flag = this.stringTableLookup.ContainsKey(@string);
			uint num;
			if (flag)
			{
				num = this.stringTableLookup[@string];
			}
			else
			{
				bool flag2 = this.commonStringTableLookup.ContainsKey(@string);
				if (flag2)
				{
					num = this.commonStringTableLookup[@string] + 2147483648U;
				}
				else
				{
					num = this.stringTablePos;
					this.stringTableLookup.Add(@string, this.stringTablePos);
					this.stringTablePos += (uint)(@string.Length + 1);
				}
			}
			bool flag3 = this.stringTableLookup.ContainsKey(string2);
			uint num2;
			if (flag3)
			{
				num2 = this.stringTableLookup[string2];
			}
			else
			{
				bool flag4 = this.commonStringTableLookup.ContainsKey(string2);
				if (flag4)
				{
					num2 = this.commonStringTableLookup[string2] + 2147483648U;
				}
				else
				{
					num2 = this.stringTablePos;
					this.stringTableLookup.Add(string2, this.stringTablePos);
					this.stringTablePos += (uint)(string2.Length + 1);
				}
			}
			this.typeTreeNodes.Add(new TypeTreeNode
			{
				Level = (byte)depth,
				MetaFlags = node.MetaFlag,
				Index = (uint)this.typeTreeNodes.Count,
				TypeFlags = (TypeTreeNodeFlags)node.TypeFlags,
				NameStrOffset = num,
				ByteSize = node.ByteSize,
				TypeStrOffset = num2,
				Version = node.Version
			});
			foreach (ClassDatabaseTypeNode classDatabaseTypeNode in node.Children)
			{
				this.ConvertFields(classDatabaseTypeNode, depth + 1);
			}
		}

		// Token: 0x0400041B RID: 1051
		private ClassDatabaseFile cldbFile;

		// Token: 0x0400041C RID: 1052
		private uint stringTablePos;

		// Token: 0x0400041D RID: 1053
		private Dictionary<string, uint> stringTableLookup;

		// Token: 0x0400041E RID: 1054
		private Dictionary<string, uint> commonStringTableLookup;

		// Token: 0x0400041F RID: 1055
		private List<TypeTreeNode> typeTreeNodes;
	}
}

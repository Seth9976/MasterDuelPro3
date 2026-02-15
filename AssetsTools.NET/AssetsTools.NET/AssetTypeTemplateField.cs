using System;
using System.Collections.Generic;
using System.Linq;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000052 RID: 82
	public class AssetTypeTemplateField
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00012120 File Offset: 0x00010320
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00012128 File Offset: 0x00010328
		public string Name { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00012131 File Offset: 0x00010331
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00012139 File Offset: 0x00010339
		public string Type { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00012142 File Offset: 0x00010342
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0001214A File Offset: 0x0001034A
		public AssetValueType ValueType { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00012153 File Offset: 0x00010353
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0001215B File Offset: 0x0001035B
		public bool IsArray { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00012164 File Offset: 0x00010364
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0001216C File Offset: 0x0001036C
		public bool IsAligned { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00012175 File Offset: 0x00010375
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0001217D File Offset: 0x0001037D
		public bool HasValue { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00012186 File Offset: 0x00010386
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0001218E File Offset: 0x0001038E
		public List<AssetTypeTemplateField> Children { get; set; }

		// Token: 0x06000299 RID: 665 RVA: 0x00012198 File Offset: 0x00010398
		public void FromTypeTree(TypeTreeType typeTreeType)
		{
			int num = 0;
			this.FromTypeTree(typeTreeType, ref num);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000121B4 File Offset: 0x000103B4
		private void FromTypeTree(TypeTreeType typeTreeType, ref int fieldIndex)
		{
			TypeTreeNode typeTreeNode = typeTreeType.Nodes[fieldIndex];
			this.Name = typeTreeNode.GetNameString(typeTreeType.StringBuffer, null);
			this.Type = typeTreeNode.GetTypeString(typeTreeType.StringBuffer, null);
			this.ValueType = AssetTypeValueField.GetValueTypeByTypeName(this.Type);
			this.IsArray = Net35Polyfill.HasFlag(typeTreeNode.TypeFlags, TypeTreeNodeFlags.Array);
			this.IsAligned = (typeTreeNode.MetaFlags & 16384U) > 0U;
			this.HasValue = this.ValueType > AssetValueType.None;
			this.Children = new List<AssetTypeTemplateField>();
			for (fieldIndex++; fieldIndex < typeTreeType.Nodes.Count; fieldIndex++)
			{
				TypeTreeNode typeTreeNode2 = typeTreeType.Nodes[fieldIndex];
				bool flag = typeTreeNode2.Level <= typeTreeNode.Level;
				if (flag)
				{
					fieldIndex--;
					break;
				}
				AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField();
				assetTypeTemplateField.FromTypeTree(typeTreeType, ref fieldIndex);
				this.Children.Add(assetTypeTemplateField);
			}
			bool flag2 = this.ValueType == AssetValueType.String && !this.Children[0].IsArray && this.Children[0].ValueType > AssetValueType.None;
			if (flag2)
			{
				this.Type = this.Children[0].Type;
				this.ValueType = this.Children[0].ValueType;
				this.Children.Clear();
			}
			bool isArray = this.IsArray;
			if (isArray)
			{
				this.ValueType = ((this.Children[1].ValueType == AssetValueType.UInt8) ? AssetValueType.ByteArray : AssetValueType.Array);
			}
			this.Children.TrimExcess();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001237C File Offset: 0x0001057C
		public void FromClassDatabase(ClassDatabaseFile cldbFile, ClassDatabaseType cldbType, bool preferEditor = false)
		{
			bool flag = cldbType.EditorRootNode == null && cldbType.ReleaseRootNode == null;
			if (flag)
			{
				throw new Exception("No root nodes were found!");
			}
			ClassDatabaseTypeNode preferredNode = cldbType.GetPreferredNode(preferEditor);
			this.FromClassDatabase(cldbFile.StringTable, preferredNode);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000123C4 File Offset: 0x000105C4
		private void FromClassDatabase(ClassDatabaseStringTable strTable, ClassDatabaseTypeNode node)
		{
			this.Name = strTable.GetString(node.FieldName);
			this.Type = strTable.GetString(node.TypeName);
			bool flag = this.Type == "SInt32";
			if (flag)
			{
				this.Type = "int";
			}
			else
			{
				bool flag2 = this.Type == "UInt32";
				if (flag2)
				{
					this.Type = "unsigned int";
				}
			}
			this.ValueType = AssetTypeValueField.GetValueTypeByTypeName(this.Type);
			this.IsArray = node.TypeFlags == 1;
			this.IsAligned = (node.MetaFlag & 16384U) > 0U;
			this.HasValue = this.ValueType > AssetValueType.None;
			this.Children = new List<AssetTypeTemplateField>(node.Children.Count);
			foreach (ClassDatabaseTypeNode classDatabaseTypeNode in node.Children)
			{
				AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField();
				assetTypeTemplateField.FromClassDatabase(strTable, classDatabaseTypeNode);
				this.Children.Add(assetTypeTemplateField);
			}
			bool flag3 = this.ValueType == AssetValueType.String && !this.Children[0].IsArray && this.Children[0].ValueType > AssetValueType.None;
			if (flag3)
			{
				this.Type = this.Children[0].Type;
				this.ValueType = this.Children[0].ValueType;
				this.Children.Clear();
				this.Children.TrimExcess();
			}
			bool isArray = this.IsArray;
			if (isArray)
			{
				this.ValueType = ((this.Children[1].ValueType == AssetValueType.UInt8) ? AssetValueType.ByteArray : AssetValueType.Array);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000125B0 File Offset: 0x000107B0
		public AssetTypeValueField MakeValue(AssetsFileReader reader, RefTypeManager refMan = null)
		{
			AssetTypeValueField assetTypeValueField = new AssetTypeValueField
			{
				TemplateField = this
			};
			return this.ReadType(reader, assetTypeValueField, refMan);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000125DC File Offset: 0x000107DC
		public AssetTypeValueField MakeValue(AssetsFileReader reader, long position, RefTypeManager refMan = null)
		{
			reader.Position = position;
			return this.MakeValue(reader, refMan);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00012600 File Offset: 0x00010800
		public AssetTypeValueField ReadType(AssetsFileReader reader, AssetTypeValueField valueField, RefTypeManager refMan)
		{
			bool isArray = valueField.TemplateField.IsArray;
			if (isArray)
			{
				int count = valueField.TemplateField.Children.Count;
				bool flag = count != 2;
				if (flag)
				{
					throw new Exception(string.Format("Expected array to have two children, found {0} instead!", count));
				}
				AssetValueType valueType = valueField.TemplateField.Children[0].ValueType;
				bool flag2 = valueType != AssetValueType.Int32 && valueType != AssetValueType.UInt32;
				if (flag2)
				{
					throw new Exception(string.Format("Expected int array size type, found {0} instead!", valueType));
				}
				bool flag3 = valueField.TemplateField.ValueType == AssetValueType.ByteArray;
				if (flag3)
				{
					valueField.Children = new List<AssetTypeValueField>(0);
					int num = reader.ReadInt32();
					byte[] array = reader.ReadBytes(num);
					bool isAligned = valueField.TemplateField.IsAligned;
					if (isAligned)
					{
						reader.Align();
					}
					valueField.Value = new AssetTypeValue(AssetValueType.ByteArray, array);
				}
				else
				{
					int num2 = reader.ReadInt32();
					valueField.Children = new List<AssetTypeValueField>(num2);
					for (int i = 0; i < num2; i++)
					{
						AssetTypeValueField assetTypeValueField = new AssetTypeValueField();
						assetTypeValueField.TemplateField = valueField.TemplateField.Children[1];
						valueField.Children.Add(this.ReadType(reader, assetTypeValueField, refMan));
					}
					valueField.Children.TrimExcess();
					bool isAligned2 = valueField.TemplateField.IsAligned;
					if (isAligned2)
					{
						reader.Align();
					}
					AssetTypeArrayInfo assetTypeArrayInfo = new AssetTypeArrayInfo
					{
						size = num2
					};
					valueField.Value = new AssetTypeValue(AssetValueType.Array, assetTypeArrayInfo);
				}
			}
			else
			{
				AssetValueType valueType2 = valueField.TemplateField.ValueType;
				bool flag4 = valueType2 == AssetValueType.None;
				if (flag4)
				{
					int count2 = valueField.TemplateField.Children.Count;
					valueField.Children = new List<AssetTypeValueField>(count2);
					for (int j = 0; j < count2; j++)
					{
						AssetTypeValueField assetTypeValueField2 = new AssetTypeValueField();
						assetTypeValueField2.TemplateField = valueField.TemplateField.Children[j];
						valueField.Children.Add(this.ReadType(reader, assetTypeValueField2, refMan));
					}
					valueField.Children.TrimExcess();
					valueField.Value = null;
					bool isAligned3 = valueField.TemplateField.IsAligned;
					if (isAligned3)
					{
						reader.Align();
					}
				}
				else
				{
					bool flag5 = valueType2 == AssetValueType.String;
					if (flag5)
					{
						valueField.Children = new List<AssetTypeValueField>(0);
						int num3 = reader.ReadInt32();
						valueField.Value = new AssetTypeValue(reader.ReadBytes(num3), true);
						reader.Align();
					}
					else
					{
						bool flag6 = valueType2 == AssetValueType.ManagedReferencesRegistry;
						if (flag6)
						{
							bool flag7 = refMan == null;
							if (flag7)
							{
								throw new Exception("refMan MUST be set to deserialize objects with ref types!");
							}
							valueField.Children = new List<AssetTypeValueField>(0);
							ManagedReferencesRegistry managedReferencesRegistry = new ManagedReferencesRegistry();
							valueField.Value = new AssetTypeValue(managedReferencesRegistry);
							int count3 = valueField.TemplateField.Children.Count;
							bool flag8 = count3 != 2;
							if (flag8)
							{
								throw new Exception(string.Format("Expected ManagedReferencesRegistry to have two children, found {0} instead!", count3));
							}
							managedReferencesRegistry.version = reader.ReadInt32();
							managedReferencesRegistry.references = new List<AssetTypeReferencedObject>(0);
							bool flag9 = managedReferencesRegistry.version == 1;
							if (flag9)
							{
								for (;;)
								{
									AssetTypeReferencedObject assetTypeReferencedObject = this.MakeReferencedObject(reader, managedReferencesRegistry.version, managedReferencesRegistry.references.Count, refMan);
									bool flag10 = assetTypeReferencedObject.type.Equals(AssetTypeReference.TERMINUS);
									if (flag10)
									{
										break;
									}
									managedReferencesRegistry.references.Add(assetTypeReferencedObject);
								}
							}
							else
							{
								int num4 = reader.ReadInt32();
								for (int k = 0; k < num4; k++)
								{
									AssetTypeReferencedObject assetTypeReferencedObject2 = this.MakeReferencedObject(reader, managedReferencesRegistry.version, k, refMan);
									managedReferencesRegistry.references.Add(assetTypeReferencedObject2);
								}
							}
						}
						else
						{
							int count4 = valueField.TemplateField.Children.Count;
							bool flag11 = count4 == 0;
							if (flag11)
							{
								valueField.Children = new List<AssetTypeValueField>(0);
								switch (valueField.TemplateField.ValueType)
								{
								case AssetValueType.Bool:
									valueField.Value = new AssetTypeValue(reader.ReadBoolean());
									break;
								case AssetValueType.Int8:
									valueField.Value = new AssetTypeValue(reader.ReadSByte());
									break;
								case AssetValueType.UInt8:
									valueField.Value = new AssetTypeValue(reader.ReadByte());
									break;
								case AssetValueType.Int16:
									valueField.Value = new AssetTypeValue(reader.ReadInt16());
									break;
								case AssetValueType.UInt16:
									valueField.Value = new AssetTypeValue(reader.ReadUInt16());
									break;
								case AssetValueType.Int32:
									valueField.Value = new AssetTypeValue(reader.ReadInt32());
									break;
								case AssetValueType.UInt32:
									valueField.Value = new AssetTypeValue(reader.ReadUInt32());
									break;
								case AssetValueType.Int64:
									valueField.Value = new AssetTypeValue(reader.ReadInt64());
									break;
								case AssetValueType.UInt64:
									valueField.Value = new AssetTypeValue(reader.ReadUInt64());
									break;
								case AssetValueType.Float:
									valueField.Value = new AssetTypeValue(reader.ReadSingle());
									break;
								case AssetValueType.Double:
									valueField.Value = new AssetTypeValue(reader.ReadDouble());
									break;
								}
								bool isAligned4 = valueField.TemplateField.IsAligned;
								if (isAligned4)
								{
									reader.Align();
								}
							}
							else
							{
								bool flag12 = valueField.TemplateField.ValueType > AssetValueType.None;
								if (flag12)
								{
									throw new Exception("Cannot read value of field with children!");
								}
							}
						}
					}
				}
			}
			return valueField;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public AssetTypeTemplateField Clone()
		{
			AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField();
			assetTypeTemplateField.Name = this.Name;
			assetTypeTemplateField.Type = this.Type;
			assetTypeTemplateField.ValueType = this.ValueType;
			assetTypeTemplateField.IsArray = this.IsArray;
			assetTypeTemplateField.IsAligned = this.IsAligned;
			assetTypeTemplateField.HasValue = this.HasValue;
			assetTypeTemplateField.Children = this.Children.Select((AssetTypeTemplateField c) => c.Clone()).ToList<AssetTypeTemplateField>();
			return assetTypeTemplateField;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00012C44 File Offset: 0x00010E44
		private AssetTypeReferencedObject MakeReferencedObject(AssetsFileReader reader, int registryVersion, int referenceIndex, RefTypeManager refMan)
		{
			AssetTypeReferencedObject assetTypeReferencedObject = new AssetTypeReferencedObject();
			bool flag = registryVersion == 1;
			if (flag)
			{
				assetTypeReferencedObject.rid = (long)referenceIndex;
			}
			else
			{
				assetTypeReferencedObject.rid = reader.ReadInt64();
			}
			AssetTypeReference assetTypeReference = new AssetTypeReference();
			assetTypeReference.ReadAsset(reader);
			assetTypeReferencedObject.type = assetTypeReference;
			AssetTypeTemplateField templateField = refMan.GetTemplateField(assetTypeReference);
			bool flag2 = templateField != null;
			if (flag2)
			{
				assetTypeReferencedObject.data = new AssetTypeValueField
				{
					TemplateField = templateField
				};
				assetTypeReferencedObject.data = this.ReadType(reader, assetTypeReferencedObject.data, refMan);
			}
			else
			{
				assetTypeReferencedObject.data = AssetTypeValueField.DUMMY_FIELD;
			}
			return assetTypeReferencedObject;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00012CE4 File Offset: 0x00010EE4
		public override string ToString()
		{
			return this.Type + " " + this.Name;
		}
	}
}

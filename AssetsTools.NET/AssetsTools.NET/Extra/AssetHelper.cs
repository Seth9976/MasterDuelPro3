using System;
using System.Collections.Generic;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000075 RID: 117
	public static class AssetHelper
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00017C40 File Offset: 0x00015E40
		public static Dictionary<int, AssetTypeReference> GetAssetsFileScriptInfos(AssetsManager am, AssetsFileInstance inst)
		{
			Dictionary<int, AssetTypeReference> dictionary = new Dictionary<int, AssetTypeReference>();
			List<AssetPPtr> scriptTypes = inst.file.Metadata.ScriptTypes;
			for (int i = 0; i < scriptTypes.Count; i++)
			{
				AssetTypeReference assetsFileScriptInfo = AssetHelper.GetAssetsFileScriptInfo(am, inst, i);
				bool flag = assetsFileScriptInfo == null;
				if (!flag)
				{
					dictionary[i] = assetsFileScriptInfo;
				}
			}
			return dictionary;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00017CA4 File Offset: 0x00015EA4
		public static AssetTypeReference GetAssetsFileScriptInfo(AssetsManager am, AssetsFileInstance inst, int index)
		{
			List<AssetPPtr> scriptTypes = inst.file.Metadata.ScriptTypes;
			AssetPPtr assetPPtr = scriptTypes[index];
			AssetTypeValueField baseField;
			try
			{
				baseField = am.GetExtAsset(inst, assetPPtr.FileId, assetPPtr.PathId, false, AssetReadFlags.None).baseField;
				bool flag = baseField == null;
				if (flag)
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
			AssetTypeValueField assetTypeValueField = baseField["m_AssemblyName"];
			AssetTypeValueField assetTypeValueField2 = baseField["m_Namespace"];
			AssetTypeValueField assetTypeValueField3 = baseField["m_ClassName"];
			bool flag2 = assetTypeValueField.IsDummy || assetTypeValueField2.IsDummy || assetTypeValueField3.IsDummy;
			AssetTypeReference assetTypeReference;
			if (flag2)
			{
				assetTypeReference = null;
			}
			else
			{
				string asString = assetTypeValueField.AsString;
				string asString2 = assetTypeValueField2.AsString;
				string asString3 = assetTypeValueField3.AsString;
				AssetTypeReference assetTypeReference2 = new AssetTypeReference(asString3, asString2, asString);
				assetTypeReference = assetTypeReference2;
			}
			return assetTypeReference;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00017D90 File Offset: 0x00015F90
		public static string GetAssetNameFast(AssetsFile file, ClassDatabaseFile cldb, AssetFileInfo info)
		{
			ClassDatabaseType classDatabaseType = cldb.FindAssetClassByID(info.TypeId);
			AssetsFileReader reader = file.Reader;
			bool typeTreeEnabled = file.Metadata.TypeTreeEnabled;
			string text;
			if (typeTreeEnabled)
			{
				ushort scriptIndex = file.GetScriptIndex(info);
				TypeTreeType typeTreeType = file.Metadata.FindTypeTreeTypeByID(info.TypeId, scriptIndex);
				string typeString = typeTreeType.Nodes[0].GetTypeString(typeTreeType.StringBuffer, null);
				bool flag = typeTreeType.Nodes.Count == 0;
				if (flag)
				{
					text = cldb.GetString(classDatabaseType.Name);
				}
				else
				{
					bool flag2 = typeTreeType.Nodes.Count > 1 && typeTreeType.Nodes[1].GetNameString(typeTreeType.StringBuffer, null) == "m_Name";
					if (flag2)
					{
						reader.Position = info.AbsoluteByteStart;
						text = reader.ReadCountStringInt32();
					}
					else
					{
						bool flag3 = typeString == "GameObject";
						if (flag3)
						{
							reader.Position = info.AbsoluteByteStart;
							int num = reader.ReadInt32();
							int num2 = ((file.Header.Version > 16U) ? 12 : 16);
							reader.Position += (long)(num * num2);
							reader.Position += 4L;
							text = reader.ReadCountStringInt32();
						}
						else
						{
							bool flag4 = typeString == "MonoBehaviour";
							if (flag4)
							{
								reader.Position = info.AbsoluteByteStart;
								reader.Position += 28L;
								string text2 = reader.ReadCountStringInt32();
								bool flag5 = text2 != "";
								if (flag5)
								{
									return text2;
								}
							}
							text = typeString;
						}
					}
				}
			}
			else
			{
				string @string = cldb.GetString(classDatabaseType.Name);
				bool flag6 = classDatabaseType.ReleaseRootNode.Children.Count == 0;
				if (flag6)
				{
					text = @string;
				}
				else
				{
					bool flag7 = classDatabaseType.ReleaseRootNode.Children.Count > 1 && cldb.GetString(classDatabaseType.ReleaseRootNode.Children[0].FieldName) == "m_Name";
					if (flag7)
					{
						reader.Position = info.AbsoluteByteStart;
						text = reader.ReadCountStringInt32();
					}
					else
					{
						bool flag8 = @string == "GameObject";
						if (flag8)
						{
							reader.Position = info.AbsoluteByteStart;
							int num3 = reader.ReadInt32();
							int num4 = ((file.Header.Version > 16U) ? 12 : 16);
							reader.Position += (long)(num3 * num4);
							reader.Position += 4L;
							text = reader.ReadCountStringInt32();
						}
						else
						{
							bool flag9 = @string == "MonoBehaviour";
							if (flag9)
							{
								reader.Position = info.AbsoluteByteStart;
								reader.Position += 28L;
								string text3 = reader.ReadCountStringInt32();
								bool flag10 = text3 != "";
								if (flag10)
								{
									return text3;
								}
							}
							text = @string;
						}
					}
				}
			}
			return text;
		}
	}
}

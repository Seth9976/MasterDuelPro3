using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000087 RID: 135
	public static class CommonMonoTemplateHelper
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0001A4F0 File Offset: 0x000186F0
		public static string ConvertBaseToPrimitive(string name)
		{
			string text;
			bool flag = CommonMonoTemplateHelper.baseToPrimitive.TryGetValue(name, out text);
			string text2;
			if (flag)
			{
				text2 = text;
			}
			else
			{
				text2 = name;
			}
			return text2;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001A51C File Offset: 0x0001871C
		public static AssetValueType ConvertBaseToAssetValueType(string name)
		{
			AssetValueType assetValueType;
			bool flag = CommonMonoTemplateHelper.baseToAssetValueType.TryGetValue(name, out assetValueType);
			AssetValueType assetValueType2;
			if (flag)
			{
				assetValueType2 = assetValueType;
			}
			else
			{
				assetValueType2 = AssetValueType.None;
			}
			return assetValueType2;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001A548 File Offset: 0x00018748
		public static bool IsSpecialUnityType(string fullName)
		{
			return CommonMonoTemplateHelper.specialUnityTypes.Contains(fullName);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001A568 File Offset: 0x00018768
		public static bool IsAssemblyBlacklisted(string assembly, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.blacklistedAssemblies.Contains(assembly);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001A588 File Offset: 0x00018788
		public static bool IsPrimitiveType(string fullName)
		{
			return CommonMonoTemplateHelper.primitiveTypes.Contains(fullName);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001A5A8 File Offset: 0x000187A8
		public static bool TypeAligns(AssetValueType valueType)
		{
			return valueType.Equals(AssetValueType.Bool) || valueType.Equals(AssetValueType.Int8) || valueType.Equals(AssetValueType.UInt8) || valueType.Equals(AssetValueType.Int16) || valueType.Equals(AssetValueType.UInt16);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001A630 File Offset: 0x00018830
		public static int GetSerializationLimit(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major > 2020 || (unityVersion.major == 2020 && (unityVersion.minor >= 2 || (unityVersion.minor == 1 && unityVersion.patch >= 4))) || (unityVersion.major == 2019 && unityVersion.minor == 4 && unityVersion.patch >= 9);
			int num;
			if (flag)
			{
				num = 10;
			}
			else
			{
				num = 7;
			}
			return num;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001A6AB File Offset: 0x000188AB
		public static AssetTypeTemplateField Bool(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "bool", AssetValueType.Bool, false, align);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001A6BB File Offset: 0x000188BB
		public static AssetTypeTemplateField SByte(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "SInt8", AssetValueType.Int8, false, align);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001A6CB File Offset: 0x000188CB
		public static AssetTypeTemplateField Byte(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "UInt8", AssetValueType.UInt8, false, align);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001A6DB File Offset: 0x000188DB
		public static AssetTypeTemplateField CChar(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "char", AssetValueType.UInt8, false, align);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001A6EB File Offset: 0x000188EB
		public static AssetTypeTemplateField Char(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "UInt16", AssetValueType.UInt16, false, align);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A6FB File Offset: 0x000188FB
		public static AssetTypeTemplateField Short(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "SInt16", AssetValueType.Int16, false, align);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A6EB File Offset: 0x000188EB
		public static AssetTypeTemplateField UShort(string name, bool align = false)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "UInt16", AssetValueType.UInt16, false, align);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001A70B File Offset: 0x0001890B
		public static AssetTypeTemplateField Int(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "int", AssetValueType.Int32);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001A719 File Offset: 0x00018919
		public static AssetTypeTemplateField UInt(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "unsigned int", AssetValueType.UInt32);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001A727 File Offset: 0x00018927
		public static AssetTypeTemplateField Long(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "SInt64", AssetValueType.Int64);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001A735 File Offset: 0x00018935
		public static AssetTypeTemplateField ULong(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "UInt64", AssetValueType.UInt64);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001A744 File Offset: 0x00018944
		public static AssetTypeTemplateField Float(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "float", AssetValueType.Float);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001A753 File Offset: 0x00018953
		public static AssetTypeTemplateField Double(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "double", AssetValueType.Double);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001A764 File Offset: 0x00018964
		public static AssetTypeTemplateField String(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "string", AssetValueType.String, CommonMonoTemplateHelper.String());
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001A788 File Offset: 0x00018988
		public static List<AssetTypeTemplateField> String()
		{
			return CommonMonoTemplateHelper.Array(CommonMonoTemplateHelper.CChar("data", false));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001A7AC File Offset: 0x000189AC
		public static AssetTypeTemplateField Vector(AssetTypeTemplateField field)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(field.Name, "vector", CommonMonoTemplateHelper.Array(field));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001A7D4 File Offset: 0x000189D4
		public static AssetTypeTemplateField VectorWithType(AssetTypeTemplateField field)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(field.Name, field.Type, CommonMonoTemplateHelper.Array(field));
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001A800 File Offset: 0x00018A00
		public static List<AssetTypeTemplateField> Array(AssetTypeTemplateField field)
		{
			AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField
			{
				Name = "Array",
				Type = "Array",
				ValueType = ((field.ValueType == AssetValueType.UInt8) ? AssetValueType.ByteArray : AssetValueType.Array),
				IsArray = true,
				IsAligned = true,
				HasValue = true,
				Children = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Int("size"),
					CommonMonoTemplateHelper.CreateTemplateField("data", field.Type, field.ValueType, field.Children)
				}
			};
			return new List<AssetTypeTemplateField> { assetTypeTemplateField };
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001A8AB File Offset: 0x00018AAB
		public static AssetTypeTemplateField ManagedReference(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "managedReference", CommonMonoTemplateHelper.ManagedReference(unityVersion));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public static List<AssetTypeTemplateField> ManagedReference(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major > 2021 || (unityVersion.major == 2021 && unityVersion.minor >= 2);
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField> { CommonMonoTemplateHelper.Long("rid") };
			}
			else
			{
				list = new List<AssetTypeTemplateField> { CommonMonoTemplateHelper.Int("id") };
			}
			return list;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001A932 File Offset: 0x00018B32
		public static AssetTypeTemplateField ManagedReferencesRegistry(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "ManagedReferencesRegistry", AssetValueType.ManagedReferencesRegistry, CommonMonoTemplateHelper.ManagedReferencesRegistry(unityVersion));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001A948 File Offset: 0x00018B48
		public static List<AssetTypeTemplateField> ManagedReferencesRegistry(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major > 2021 || (unityVersion.major == 2021 && unityVersion.minor >= 2);
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Int("version"),
					CommonMonoTemplateHelper.Vector(CommonMonoTemplateHelper.ReferencedObject("RefIds", unityVersion))
				};
			}
			else
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Int("version"),
					CommonMonoTemplateHelper.ReferencedObject("00000000", unityVersion)
				};
			}
			return list;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001A9E3 File Offset: 0x00018BE3
		public static AssetTypeTemplateField ReferencedObject(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "ReferencedObject", CommonMonoTemplateHelper.ReferencedObject(unityVersion));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public static List<AssetTypeTemplateField> ReferencedObject(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major > 2021 || (unityVersion.major == 2021 && unityVersion.minor >= 2);
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Long("rid"),
					CommonMonoTemplateHelper.ReferencedManagedType("type"),
					CommonMonoTemplateHelper.CreateTemplateField("data", "ReferencedObjectData", AssetValueType.None)
				};
			}
			else
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.ReferencedManagedType("type"),
					CommonMonoTemplateHelper.CreateTemplateField("data", "ReferencedObjectData", AssetValueType.None)
				};
			}
			return list;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001AAA9 File Offset: 0x00018CA9
		public static AssetTypeTemplateField ReferencedManagedType(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "ReferencedManagedType", CommonMonoTemplateHelper.ReferencedManagedType());
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001AABC File Offset: 0x00018CBC
		public static List<AssetTypeTemplateField> ReferencedManagedType()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.String("class"),
				CommonMonoTemplateHelper.String("ns"),
				CommonMonoTemplateHelper.String("asm")
			};
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001AB06 File Offset: 0x00018D06
		public static AssetTypeTemplateField Gradient(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "Gradient", CommonMonoTemplateHelper.Gradient(unityVersion));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001AB1C File Offset: 0x00018D1C
		public static List<AssetTypeTemplateField> Gradient(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major > 2022 || (unityVersion.major == 2022 && unityVersion.minor >= 2);
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.RGBAf("key0"),
					CommonMonoTemplateHelper.RGBAf("key1"),
					CommonMonoTemplateHelper.RGBAf("key2"),
					CommonMonoTemplateHelper.RGBAf("key3"),
					CommonMonoTemplateHelper.RGBAf("key4"),
					CommonMonoTemplateHelper.RGBAf("key5"),
					CommonMonoTemplateHelper.RGBAf("key6"),
					CommonMonoTemplateHelper.RGBAf("key7"),
					CommonMonoTemplateHelper.UShort("ctime0", false),
					CommonMonoTemplateHelper.UShort("ctime1", false),
					CommonMonoTemplateHelper.UShort("ctime2", false),
					CommonMonoTemplateHelper.UShort("ctime3", false),
					CommonMonoTemplateHelper.UShort("ctime4", false),
					CommonMonoTemplateHelper.UShort("ctime5", false),
					CommonMonoTemplateHelper.UShort("ctime6", false),
					CommonMonoTemplateHelper.UShort("ctime7", false),
					CommonMonoTemplateHelper.UShort("atime0", false),
					CommonMonoTemplateHelper.UShort("atime1", false),
					CommonMonoTemplateHelper.UShort("atime2", false),
					CommonMonoTemplateHelper.UShort("atime3", false),
					CommonMonoTemplateHelper.UShort("atime4", false),
					CommonMonoTemplateHelper.UShort("atime5", false),
					CommonMonoTemplateHelper.UShort("atime6", false),
					CommonMonoTemplateHelper.UShort("atime7", false),
					CommonMonoTemplateHelper.Byte("m_Mode", false),
					CommonMonoTemplateHelper.SByte("m_ColorSpace", false),
					CommonMonoTemplateHelper.Byte("m_NumColorKeys", false),
					CommonMonoTemplateHelper.Byte("m_NumAlphaKeys", true)
				};
			}
			else
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.RGBAf("key0"),
					CommonMonoTemplateHelper.RGBAf("key1"),
					CommonMonoTemplateHelper.RGBAf("key2"),
					CommonMonoTemplateHelper.RGBAf("key3"),
					CommonMonoTemplateHelper.RGBAf("key4"),
					CommonMonoTemplateHelper.RGBAf("key5"),
					CommonMonoTemplateHelper.RGBAf("key6"),
					CommonMonoTemplateHelper.RGBAf("key7"),
					CommonMonoTemplateHelper.UShort("ctime0", false),
					CommonMonoTemplateHelper.UShort("ctime1", false),
					CommonMonoTemplateHelper.UShort("ctime2", false),
					CommonMonoTemplateHelper.UShort("ctime3", false),
					CommonMonoTemplateHelper.UShort("ctime4", false),
					CommonMonoTemplateHelper.UShort("ctime5", false),
					CommonMonoTemplateHelper.UShort("ctime6", false),
					CommonMonoTemplateHelper.UShort("ctime7", false),
					CommonMonoTemplateHelper.UShort("atime0", false),
					CommonMonoTemplateHelper.UShort("atime1", false),
					CommonMonoTemplateHelper.UShort("atime2", false),
					CommonMonoTemplateHelper.UShort("atime3", false),
					CommonMonoTemplateHelper.UShort("atime4", false),
					CommonMonoTemplateHelper.UShort("atime5", false),
					CommonMonoTemplateHelper.UShort("atime6", false),
					CommonMonoTemplateHelper.UShort("atime7", false),
					CommonMonoTemplateHelper.Int("m_Mode"),
					CommonMonoTemplateHelper.Byte("m_NumColorKeys", false),
					CommonMonoTemplateHelper.Byte("m_NumAlphaKeys", true)
				};
			}
			return list;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001AF3F File Offset: 0x0001913F
		public static AssetTypeTemplateField AnimationCurve(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "AnimationCurve", CommonMonoTemplateHelper.AnimationCurve(unityVersion));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001AF54 File Offset: 0x00019154
		public static List<AssetTypeTemplateField> AnimationCurve(UnityVersion unityVersion)
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Vector(CommonMonoTemplateHelper.Keyframe("m_Curve", unityVersion)),
				CommonMonoTemplateHelper.Int("m_PreInfinity"),
				CommonMonoTemplateHelper.Int("m_PostInfinity"),
				CommonMonoTemplateHelper.Int("m_RotationOrder")
			};
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001AFB5 File Offset: 0x000191B5
		public static AssetTypeTemplateField GUIStyle(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "GUIStyle", CommonMonoTemplateHelper.GUIStyle(unityVersion));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public static List<AssetTypeTemplateField> GUIStyle(UnityVersion unityVersion)
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.String("m_Name"),
				CommonMonoTemplateHelper.GUIStyleState("m_Normal", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_Hover", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_Active", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_Focused", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_OnNormal", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_OnHover", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_OnActive", unityVersion),
				CommonMonoTemplateHelper.GUIStyleState("m_OnFocused", unityVersion),
				CommonMonoTemplateHelper.RectOffset("m_Border"),
				CommonMonoTemplateHelper.RectOffset("m_Margin"),
				CommonMonoTemplateHelper.RectOffset("m_Padding"),
				CommonMonoTemplateHelper.RectOffset("m_Overflow"),
				CommonMonoTemplateHelper.PPtr("m_Font", "Font", unityVersion),
				CommonMonoTemplateHelper.Int("m_FontSize"),
				CommonMonoTemplateHelper.Int("m_FontStyle"),
				CommonMonoTemplateHelper.Int("m_Alignment"),
				CommonMonoTemplateHelper.Bool("m_WordWrap", false),
				CommonMonoTemplateHelper.Bool("m_RichText", true),
				CommonMonoTemplateHelper.Int("m_TextClipping"),
				CommonMonoTemplateHelper.Int("m_ImagePosition"),
				CommonMonoTemplateHelper.Vector2f("m_ContentOffset"),
				CommonMonoTemplateHelper.Float("m_FixedWidth"),
				CommonMonoTemplateHelper.Float("m_FixedHeight"),
				CommonMonoTemplateHelper.Bool("m_StretchWidth", false),
				CommonMonoTemplateHelper.Bool("m_StretchHeight", true)
			};
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001B1AB File Offset: 0x000193AB
		public static AssetTypeTemplateField Keyframe(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "Keyframe", CommonMonoTemplateHelper.Keyframe(unityVersion));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B1C0 File Offset: 0x000193C0
		public static List<AssetTypeTemplateField> Keyframe(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major >= 2018;
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Float("time"),
					CommonMonoTemplateHelper.Float("value"),
					CommonMonoTemplateHelper.Float("inSlope"),
					CommonMonoTemplateHelper.Float("outSlope"),
					CommonMonoTemplateHelper.Int("weightedMode"),
					CommonMonoTemplateHelper.Float("inWeight"),
					CommonMonoTemplateHelper.Float("outWeight")
				};
			}
			else
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Float("time"),
					CommonMonoTemplateHelper.Float("value"),
					CommonMonoTemplateHelper.Float("inSlope"),
					CommonMonoTemplateHelper.Float("outSlope")
				};
			}
			return list;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B2B2 File Offset: 0x000194B2
		public static AssetTypeTemplateField GUIStyleState(string name, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "GUIStyleState", CommonMonoTemplateHelper.GUIStyleState(unityVersion));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001B2C8 File Offset: 0x000194C8
		public static List<AssetTypeTemplateField> GUIStyleState(UnityVersion unityVersion)
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.PPtr("m_Background", "Texture2D", unityVersion),
				CommonMonoTemplateHelper.RGBAf("m_TextColor")
			};
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001B307 File Offset: 0x00019507
		public static AssetTypeTemplateField RGBAf(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "ColorRGBA", CommonMonoTemplateHelper.RGBAf());
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001B31C File Offset: 0x0001951C
		public static List<AssetTypeTemplateField> RGBAf()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Float("r"),
				CommonMonoTemplateHelper.Float("g"),
				CommonMonoTemplateHelper.Float("b"),
				CommonMonoTemplateHelper.Float("a")
			};
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001B377 File Offset: 0x00019577
		public static AssetTypeTemplateField RGBAi(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "ColorRGBA", CommonMonoTemplateHelper.RGBAi());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001B38C File Offset: 0x0001958C
		public static List<AssetTypeTemplateField> RGBAi()
		{
			return new List<AssetTypeTemplateField> { CommonMonoTemplateHelper.UInt("rgba") };
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public static AssetTypeTemplateField AABB(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "AABB", CommonMonoTemplateHelper.AABB());
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001B3C8 File Offset: 0x000195C8
		public static List<AssetTypeTemplateField> AABB()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Vector3f("m_Center"),
				CommonMonoTemplateHelper.Vector3f("m_Extent")
			};
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001B401 File Offset: 0x00019601
		public static AssetTypeTemplateField BoundsInt(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "BoundsInt", CommonMonoTemplateHelper.BoundsInt());
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001B414 File Offset: 0x00019614
		public static List<AssetTypeTemplateField> BoundsInt()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Vector3Int("m_Position"),
				CommonMonoTemplateHelper.Vector3Int("m_Size")
			};
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001B44D File Offset: 0x0001964D
		public static AssetTypeTemplateField BitField(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "BitField", CommonMonoTemplateHelper.BitField());
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001B460 File Offset: 0x00019660
		public static List<AssetTypeTemplateField> BitField()
		{
			return new List<AssetTypeTemplateField> { CommonMonoTemplateHelper.UInt("m_Bits") };
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001B488 File Offset: 0x00019688
		public static AssetTypeTemplateField Rectf(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "Rectf", CommonMonoTemplateHelper.Rectf());
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001B49C File Offset: 0x0001969C
		public static List<AssetTypeTemplateField> Rectf()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Float("x"),
				CommonMonoTemplateHelper.Float("y"),
				CommonMonoTemplateHelper.Float("width"),
				CommonMonoTemplateHelper.Float("height")
			};
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001B4F7 File Offset: 0x000196F7
		public static AssetTypeTemplateField RectOffset(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "RectOffset", CommonMonoTemplateHelper.RectOffset());
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001B50C File Offset: 0x0001970C
		public static List<AssetTypeTemplateField> RectOffset()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Int("m_Left"),
				CommonMonoTemplateHelper.Int("m_Right"),
				CommonMonoTemplateHelper.Int("m_Top"),
				CommonMonoTemplateHelper.Int("m_Bottom")
			};
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001B567 File Offset: 0x00019767
		public static AssetTypeTemplateField Vector2Int(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "int2_storage", CommonMonoTemplateHelper.Vector2Int());
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001B57C File Offset: 0x0001977C
		public static List<AssetTypeTemplateField> Vector2Int()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Int("x"),
				CommonMonoTemplateHelper.Int("y")
			};
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001B5B5 File Offset: 0x000197B5
		public static AssetTypeTemplateField Vector3Int(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "int3_storage", CommonMonoTemplateHelper.Vector3Int());
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001B5C8 File Offset: 0x000197C8
		public static List<AssetTypeTemplateField> Vector3Int()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Int("x"),
				CommonMonoTemplateHelper.Int("y"),
				CommonMonoTemplateHelper.Int("z")
			};
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001B612 File Offset: 0x00019812
		public static AssetTypeTemplateField Vector2f(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "Vector2f", CommonMonoTemplateHelper.Vector2f());
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001B624 File Offset: 0x00019824
		public static List<AssetTypeTemplateField> Vector2f()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Float("x"),
				CommonMonoTemplateHelper.Float("y")
			};
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001B65D File Offset: 0x0001985D
		public static AssetTypeTemplateField Vector3f(string name)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "Vector3f", CommonMonoTemplateHelper.Vector3f());
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001B670 File Offset: 0x00019870
		public static List<AssetTypeTemplateField> Vector3f()
		{
			return new List<AssetTypeTemplateField>
			{
				CommonMonoTemplateHelper.Float("x"),
				CommonMonoTemplateHelper.Float("y"),
				CommonMonoTemplateHelper.Float("z")
			};
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001B6BA File Offset: 0x000198BA
		public static AssetTypeTemplateField PPtr(string name, string typeName, UnityVersion unityVersion)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, "PPtr<" + typeName + ">", CommonMonoTemplateHelper.PPtr(unityVersion));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public static List<AssetTypeTemplateField> PPtr(UnityVersion unityVersion)
		{
			bool flag = unityVersion.major >= 5;
			List<AssetTypeTemplateField> list;
			if (flag)
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Int("m_FileID"),
					CommonMonoTemplateHelper.Long("m_PathID")
				};
			}
			else
			{
				list = new List<AssetTypeTemplateField>
				{
					CommonMonoTemplateHelper.Int("m_FileID"),
					CommonMonoTemplateHelper.Int("m_PathID")
				};
			}
			return list;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001B74C File Offset: 0x0001994C
		public static AssetTypeTemplateField CreateTemplateField(string name, string type, List<AssetTypeTemplateField> children)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, type, AssetValueType.None, false, false, children);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001B76C File Offset: 0x0001996C
		public static AssetTypeTemplateField CreateTemplateField(string name, string type, AssetValueType valueType)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, type, valueType, false, false, new List<AssetTypeTemplateField>(0));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001B790 File Offset: 0x00019990
		public static AssetTypeTemplateField CreateTemplateField(string name, string type, AssetValueType valueType, bool isArray, bool align)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, type, valueType, isArray, align, new List<AssetTypeTemplateField>(0));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001B7B4 File Offset: 0x000199B4
		public static AssetTypeTemplateField CreateTemplateField(string name, string type, AssetValueType valueType, List<AssetTypeTemplateField> children)
		{
			return CommonMonoTemplateHelper.CreateTemplateField(name, type, valueType, false, false, children);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001B7D4 File Offset: 0x000199D4
		public static AssetTypeTemplateField CreateTemplateField(string name, string type, AssetValueType valueType, bool isArray, bool align, List<AssetTypeTemplateField> children)
		{
			return new AssetTypeTemplateField
			{
				Name = name,
				Type = type,
				ValueType = valueType,
				IsArray = isArray,
				IsAligned = align,
				HasValue = (valueType > AssetValueType.None),
				Children = (children ?? new List<AssetTypeTemplateField>(0))
			};
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001B834 File Offset: 0x00019A34
		// Note: this type is marked as 'beforefieldinit'.
		static CommonMonoTemplateHelper()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["System.Boolean"] = "UInt8";
			dictionary["System.SByte"] = "SInt8";
			dictionary["System.Byte"] = "UInt8";
			dictionary["System.Char"] = "UInt16";
			dictionary["System.Int16"] = "SInt16";
			dictionary["System.UInt16"] = "UInt16";
			dictionary["System.Int32"] = "int";
			dictionary["System.UInt32"] = "unsigned int";
			dictionary["System.Int64"] = "SInt64";
			dictionary["System.UInt64"] = "UInt64";
			dictionary["System.Double"] = "double";
			dictionary["System.Single"] = "float";
			dictionary["System.String"] = "string";
			CommonMonoTemplateHelper.baseToPrimitive = dictionary;
			Dictionary<string, AssetValueType> dictionary2 = new Dictionary<string, AssetValueType>();
			dictionary2["System.Boolean"] = AssetValueType.UInt8;
			dictionary2["System.SByte"] = AssetValueType.Int8;
			dictionary2["System.Byte"] = AssetValueType.UInt8;
			dictionary2["System.Char"] = AssetValueType.UInt16;
			dictionary2["System.Int16"] = AssetValueType.Int16;
			dictionary2["System.UInt16"] = AssetValueType.UInt16;
			dictionary2["System.Int32"] = AssetValueType.Int32;
			dictionary2["System.UInt32"] = AssetValueType.UInt32;
			dictionary2["System.Int64"] = AssetValueType.Int64;
			dictionary2["System.UInt64"] = AssetValueType.UInt64;
			dictionary2["System.Double"] = AssetValueType.Double;
			dictionary2["System.Single"] = AssetValueType.Float;
			dictionary2["System.String"] = AssetValueType.String;
			CommonMonoTemplateHelper.baseToAssetValueType = dictionary2;
		}

		// Token: 0x04000422 RID: 1058
		private static readonly string[] blacklistedAssemblies = new string[]
		{
			"mscorlib", "mscorlib.dll", "netstandard", "netstandard.dll", "System.Core", "System.Core.dll", "System", "System.dll", "System.Private.CoreLib", "System.Private.CoreLib.dll",
			"System.Collections", "System.Collections.dll", "System.Collections.NonGeneric", "System.Collections.NonGeneric.dll"
		};

		// Token: 0x04000423 RID: 1059
		private static readonly string[] specialUnityTypes = new string[]
		{
			"UnityEngine.Color", "UnityEngine.Color32", "UnityEngine.Gradient", "UnityEngine.Vector2", "UnityEngine.Vector3", "UnityEngine.Vector4", "UnityEngine.LayerMask", "UnityEngine.Quaternion", "UnityEngine.Bounds", "UnityEngine.Rect",
			"UnityEngine.RectOffset", "UnityEngine.Matrix4x4", "UnityEngine.AnimationCurve", "UnityEngine.GUIStyle", "UnityEngine.Vector2Int", "UnityEngine.Vector3Int", "UnityEngine.BoundsInt"
		};

		// Token: 0x04000424 RID: 1060
		private static readonly string[] primitiveTypes = new string[]
		{
			"System.Boolean", "System.SByte", "System.Byte", "System.Char", "System.Int16", "System.UInt16", "System.Int32", "System.UInt32", "System.Int64", "System.UInt64",
			"System.Double", "System.Single"
		};

		// Token: 0x04000425 RID: 1061
		private static readonly Dictionary<string, string> baseToPrimitive;

		// Token: 0x04000426 RID: 1062
		private static readonly Dictionary<string, AssetValueType> baseToAssetValueType;
	}
}

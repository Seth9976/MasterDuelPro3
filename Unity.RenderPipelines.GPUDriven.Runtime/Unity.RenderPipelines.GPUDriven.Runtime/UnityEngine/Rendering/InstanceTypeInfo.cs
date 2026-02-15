using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008B RID: 139
	internal static class InstanceTypeInfo
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		static InstanceTypeInfo()
		{
			InstanceTypeInfo.InitParentTypes();
			InstanceTypeInfo.InitChildTypes();
			InstanceTypeInfo.ValidateTypeRelationsAreCorrectlySorted();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000FDDD File Offset: 0x0000DFDD
		private static void InitParentTypes()
		{
			InstanceTypeInfo.s_ParentTypes = new InstanceType[2];
			InstanceTypeInfo.s_ParentTypes[0] = InstanceType.MeshRenderer;
			InstanceTypeInfo.s_ParentTypes[1] = InstanceType.MeshRenderer;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		private static void InitChildTypes()
		{
			InstanceTypeInfo.s_ChildTypes = new List<InstanceType>[2];
			for (int i = 0; i < 2; i++)
			{
				InstanceTypeInfo.s_ChildTypes[i] = new List<InstanceType>();
			}
			for (int j = 0; j < 2; j++)
			{
				InstanceType type = (InstanceType)j;
				InstanceType parentType = InstanceTypeInfo.s_ParentTypes[(int)type];
				if (type != parentType)
				{
					InstanceTypeInfo.s_ChildTypes[(int)parentType].Add(type);
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000FE54 File Offset: 0x0000E054
		private static InstanceType GetMaxChildTypeRecursively(InstanceType type)
		{
			InstanceType maxChildType = type;
			foreach (InstanceType childType in InstanceTypeInfo.s_ChildTypes[(int)type])
			{
				maxChildType = (InstanceType)Mathf.Max((int)maxChildType, (int)InstanceTypeInfo.GetMaxChildTypeRecursively(childType));
			}
			return maxChildType;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		private static void FlattenChildInstanceTypes(InstanceType instanceType, NativeList<InstanceType> instanceTypes)
		{
			instanceTypes.Add(in instanceType);
			foreach (InstanceType instanceType2 in InstanceTypeInfo.s_ChildTypes[(int)instanceType])
			{
				InstanceTypeInfo.FlattenChildInstanceTypes(instanceType2, instanceTypes);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000FF10 File Offset: 0x0000E110
		private static void ValidateTypeRelationsAreCorrectlySorted()
		{
			NativeList<InstanceType> instanceTypesFlattened = new NativeList<InstanceType>(2, Allocator.Temp);
			for (int i = 0; i < 2; i++)
			{
				InstanceType instanceType = (InstanceType)i;
				if (instanceType == InstanceTypeInfo.s_ParentTypes[i])
				{
					InstanceTypeInfo.FlattenChildInstanceTypes(instanceType, instanceTypesFlattened);
				}
			}
			for (int j = 0; j < instanceTypesFlattened.Length; j++)
			{
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000FF5C File Offset: 0x0000E15C
		public static InstanceType GetParentType(InstanceType type)
		{
			return InstanceTypeInfo.s_ParentTypes[(int)type];
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000FF65 File Offset: 0x0000E165
		public static List<InstanceType> GetChildTypes(InstanceType type)
		{
			return InstanceTypeInfo.s_ChildTypes[(int)type];
		}

		// Token: 0x040002EA RID: 746
		public const int kInstanceTypeBitCount = 1;

		// Token: 0x040002EB RID: 747
		public const int kMaxInstanceTypesCount = 2;

		// Token: 0x040002EC RID: 748
		public const uint kInstanceTypeMask = 1U;

		// Token: 0x040002ED RID: 749
		private static InstanceType[] s_ParentTypes;

		// Token: 0x040002EE RID: 750
		private static List<InstanceType>[] s_ChildTypes;
	}
}

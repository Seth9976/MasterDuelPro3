using System;

namespace UnityEngine.Events
{
	// Token: 0x02000221 RID: 545
	internal class UnityEventTools
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0002A004 File Offset: 0x00028204
		internal static string TidyAssemblyTypeName(string assemblyTypeName)
		{
			bool flag = string.IsNullOrEmpty(assemblyTypeName);
			string text;
			if (flag)
			{
				text = assemblyTypeName;
			}
			else
			{
				int min = int.MaxValue;
				int i = assemblyTypeName.IndexOf(", Version=");
				bool flag2 = i != -1;
				if (flag2)
				{
					min = Math.Min(i, min);
				}
				i = assemblyTypeName.IndexOf(", Culture=");
				bool flag3 = i != -1;
				if (flag3)
				{
					min = Math.Min(i, min);
				}
				i = assemblyTypeName.IndexOf(", PublicKeyToken=");
				bool flag4 = i != -1;
				if (flag4)
				{
					min = Math.Min(i, min);
				}
				bool flag5 = min != int.MaxValue;
				if (flag5)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, min);
				}
				i = assemblyTypeName.IndexOf(", UnityEngine.");
				bool flag6 = i != -1 && assemblyTypeName.EndsWith("Module");
				if (flag6)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, i) + ", UnityEngine";
				}
				text = assemblyTypeName;
			}
			return text;
		}
	}
}

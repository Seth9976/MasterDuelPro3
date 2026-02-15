using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x020001D9 RID: 473
	internal static class DynamicAssemblies
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0008DD6C File Offset: 0x0008BF6C
		private static FileIOPermission UnrestrictedFileIOPermission
		{
			get
			{
				if (DynamicAssemblies.fileIOPermission == null)
				{
					DynamicAssemblies.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return DynamicAssemblies.fileIOPermission;
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008DD8C File Offset: 0x0008BF8C
		internal static bool IsTypeDynamic(Type type)
		{
			object obj = DynamicAssemblies.tableIsTypeDynamic[type];
			if (obj == null)
			{
				DynamicAssemblies.UnrestrictedFileIOPermission.Assert();
				Assembly assembly = type.Assembly;
				bool flag = assembly.IsDynamic || string.IsNullOrEmpty(assembly.Location);
				if (!flag)
				{
					if (type.IsArray)
					{
						flag = DynamicAssemblies.IsTypeDynamic(type.GetElementType());
					}
					else if (type.IsGenericType)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments != null)
						{
							foreach (Type type2 in genericArguments)
							{
								if (!(type2 == null) && !type2.IsGenericParameter)
								{
									flag = DynamicAssemblies.IsTypeDynamic(type2);
									if (flag)
									{
										break;
									}
								}
							}
						}
					}
				}
				obj = (DynamicAssemblies.tableIsTypeDynamic[type] = flag);
			}
			return (bool)obj;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008DE50 File Offset: 0x0008C050
		internal static bool IsTypeDynamic(Type[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				if (DynamicAssemblies.IsTypeDynamic(arguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008DE7C File Offset: 0x0008C07C
		internal static void Add(Assembly a)
		{
			Hashtable hashtable = DynamicAssemblies.nameToAssemblyMap;
			lock (hashtable)
			{
				if (DynamicAssemblies.assemblyToNameMap[a] == null)
				{
					Assembly assembly = DynamicAssemblies.nameToAssemblyMap[a.FullName] as Assembly;
					string text = null;
					if (assembly == null)
					{
						text = a.FullName;
					}
					else if (assembly != a)
					{
						text = a.FullName + ", " + DynamicAssemblies.nameToAssemblyMap.Count.ToString();
					}
					if (text != null)
					{
						DynamicAssemblies.nameToAssemblyMap.Add(text, a);
						DynamicAssemblies.assemblyToNameMap.Add(a, text);
					}
				}
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008DF48 File Offset: 0x0008C148
		internal static Assembly Get(string fullName)
		{
			if (DynamicAssemblies.nameToAssemblyMap == null)
			{
				return null;
			}
			return (Assembly)DynamicAssemblies.nameToAssemblyMap[fullName];
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008DF67 File Offset: 0x0008C167
		internal static string GetName(Assembly a)
		{
			if (DynamicAssemblies.assemblyToNameMap == null)
			{
				return null;
			}
			return (string)DynamicAssemblies.assemblyToNameMap[a];
		}

		// Token: 0x04000A80 RID: 2688
		private static ArrayList assembliesInConfig = new ArrayList();

		// Token: 0x04000A81 RID: 2689
		private static volatile Hashtable nameToAssemblyMap = new Hashtable();

		// Token: 0x04000A82 RID: 2690
		private static volatile Hashtable assemblyToNameMap = new Hashtable();

		// Token: 0x04000A83 RID: 2691
		private static Hashtable tableIsTypeDynamic = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000A84 RID: 2692
		private static volatile FileIOPermission fileIOPermission;
	}
}

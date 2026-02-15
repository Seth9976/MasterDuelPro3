using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine._Scripting.APIUpdating
{
	// Token: 0x020001EE RID: 494
	internal class APIUpdaterRuntimeHelpers
	{
		// Token: 0x06001382 RID: 4994 RVA: 0x00028FD8 File Offset: 0x000271D8
		[RequiredByNativeCode]
		internal static bool GetMovedFromAttributeDataForType(Type sourceType, out string assembly, out string nsp, out string klass)
		{
			klass = null;
			nsp = null;
			assembly = null;
			object[] attrs = sourceType.GetCustomAttributes(typeof(MovedFromAttribute), false);
			bool flag = attrs.Length != 1;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				MovedFromAttribute attr = (MovedFromAttribute)attrs[0];
				klass = attr.data.className;
				nsp = attr.data.nameSpace;
				assembly = attr.data.assembly;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00029048 File Offset: 0x00027248
		[RequiredByNativeCode]
		internal static bool GetObsoleteTypeRedirection(Type sourceType, out string assemblyName, out string nsp, out string className)
		{
			object[] attrs = sourceType.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			assemblyName = null;
			nsp = null;
			className = null;
			bool flag = attrs.Length != 1;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				ObsoleteAttribute attr = (ObsoleteAttribute)attrs[0];
				string x = attr.Message;
				bool flag3 = string.IsNullOrEmpty(x);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					string marker = "(UnityUpgradable) -> ";
					int index = x.IndexOf(marker);
					bool flag4 = index >= 0;
					if (flag4)
					{
						string upgradeMsg = x.Substring(index + marker.Length).Trim();
						bool flag5 = upgradeMsg.Length == 0;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool flag6 = upgradeMsg[0] == '[';
							int skip;
							if (flag6)
							{
								skip = upgradeMsg.IndexOf(']');
								bool flag7 = skip == -1;
								if (flag7)
								{
									return false;
								}
								assemblyName = upgradeMsg.Substring(1, skip - 1);
								upgradeMsg = upgradeMsg.Substring(skip + 1).Trim();
							}
							else
							{
								assemblyName = sourceType.Assembly.GetName().Name;
							}
							skip = upgradeMsg.LastIndexOf('.');
							bool flag8 = skip > -1;
							if (flag8)
							{
								className = upgradeMsg.Substring(skip + 1);
								upgradeMsg = upgradeMsg.Substring(0, skip);
							}
							else
							{
								className = upgradeMsg;
								upgradeMsg = "";
							}
							bool flag9 = upgradeMsg.Length > 0;
							if (flag9)
							{
								nsp = upgradeMsg;
							}
							else
							{
								nsp = sourceType.Namespace;
							}
							flag2 = true;
						}
					}
					else
					{
						flag2 = false;
					}
				}
			}
			return flag2;
		}
	}
}

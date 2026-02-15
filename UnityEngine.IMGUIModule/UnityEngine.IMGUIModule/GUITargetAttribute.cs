using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Method)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000620C File Offset: 0x0000440C
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool flag = method != null;
			if (flag)
			{
				object[] attrs = method.GetCustomAttributes(true);
				bool flag2 = attrs != null;
				if (flag2)
				{
					for (int i = 0; i < attrs.Length; i++)
					{
						bool flag3 = attrs[i].GetType() != typeof(GUITargetAttribute);
						if (!flag3)
						{
							GUITargetAttribute attr = attrs[i] as GUITargetAttribute;
							return attr.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x040000B4 RID: 180
		internal int displayMask;
	}
}

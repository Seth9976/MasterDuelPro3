using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000175 RID: 373
	internal class AttributeHelperEngine
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x000209F8 File Offset: 0x0001EBF8
		[RequiredByNativeCode]
		private static Type GetParentTypeDisallowingMultipleInclusion(Type type)
		{
			Type result = null;
			while (type != null && type != typeof(MonoBehaviour))
			{
				bool flag = Attribute.IsDefined(type, typeof(DisallowMultipleComponent));
				if (flag)
				{
					result = type;
				}
				type = type.BaseType;
			}
			return result;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00020A50 File Offset: 0x0001EC50
		[RequiredByNativeCode]
		private static Type[] GetRequiredComponents(Type klass)
		{
			List<Type> required = null;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				RequireComponent[] attrs = (RequireComponent[])klass.GetCustomAttributes(typeof(RequireComponent), false);
				Type baseType = klass.BaseType;
				foreach (RequireComponent attri in attrs)
				{
					bool flag = required == null && attrs.Length == 1 && baseType == typeof(MonoBehaviour);
					if (flag)
					{
						return new Type[] { attri.m_Type0, attri.m_Type1, attri.m_Type2 };
					}
					bool flag2 = required == null;
					if (flag2)
					{
						required = new List<Type>();
					}
					bool flag3 = attri.m_Type0 != null;
					if (flag3)
					{
						required.Add(attri.m_Type0);
					}
					bool flag4 = attri.m_Type1 != null;
					if (flag4)
					{
						required.Add(attri.m_Type1);
					}
					bool flag5 = attri.m_Type2 != null;
					if (flag5)
					{
						required.Add(attri.m_Type2);
					}
				}
				klass = baseType;
			}
			bool flag6 = required == null;
			if (flag6)
			{
				return null;
			}
			return required.ToArray();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00020BB4 File Offset: 0x0001EDB4
		private static int GetExecuteMode(Type klass)
		{
			object[] executeAlwaysAttributes = klass.GetCustomAttributes(typeof(ExecuteAlways), false);
			bool flag = executeAlwaysAttributes.Length != 0;
			int num;
			if (flag)
			{
				num = 2;
			}
			else
			{
				object[] executeInEditModeAttributes = klass.GetCustomAttributes(typeof(ExecuteInEditMode), false);
				bool flag2 = executeInEditModeAttributes.Length != 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00020C08 File Offset: 0x0001EE08
		[RequiredByNativeCode]
		private static int CheckIsEditorScript(Type klass)
		{
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				int executeMode = AttributeHelperEngine.GetExecuteMode(klass);
				bool flag = executeMode > 0;
				if (flag)
				{
					return executeMode;
				}
				klass = klass.BaseType;
			}
			return 0;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00020C5C File Offset: 0x0001EE5C
		[RequiredByNativeCode]
		private static int GetDefaultExecutionOrderFor(Type klass)
		{
			DefaultExecutionOrder attribute = AttributeHelperEngine.GetCustomAttributeOfType<DefaultExecutionOrder>(klass);
			bool flag = attribute == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = attribute.order;
			}
			return num;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00020C88 File Offset: 0x0001EE88
		private static T GetCustomAttributeOfType<T>(Type klass) where T : Attribute
		{
			Type attributeType = typeof(T);
			object[] attrs = klass.GetCustomAttributes(attributeType, true);
			bool flag = attrs != null && attrs.Length != 0;
			T t;
			if (flag)
			{
				t = (T)((object)attrs[0]);
			}
			else
			{
				t = default(T);
			}
			return t;
		}
	}
}

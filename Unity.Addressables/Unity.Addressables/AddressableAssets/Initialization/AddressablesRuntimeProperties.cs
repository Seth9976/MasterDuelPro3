using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.AddressableAssets.Initialization
{
	// Token: 0x0200005C RID: 92
	public static class AddressablesRuntimeProperties
	{
		// Token: 0x06000244 RID: 580 RVA: 0x000096AB File Offset: 0x000078AB
		private static Assembly[] GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000096B7 File Offset: 0x000078B7
		internal static int GetCachedValueCount()
		{
			return AddressablesRuntimeProperties.s_CachedValues.Count;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000096C3 File Offset: 0x000078C3
		public static void SetPropertyValue(string name, string val)
		{
			AddressablesRuntimeProperties.s_CachedValues[name] = val;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000096D1 File Offset: 0x000078D1
		public static void ClearCachedPropertyValues()
		{
			AddressablesRuntimeProperties.s_CachedValues.Clear();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000096E0 File Offset: 0x000078E0
		public static string EvaluateProperty(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			string cachedValue;
			if (AddressablesRuntimeProperties.s_CachedValues.TryGetValue(name, out cachedValue))
			{
				return cachedValue;
			}
			int i = name.LastIndexOf('.');
			if (i < 0)
			{
				return name;
			}
			string className = name.Substring(0, i);
			string propName = name.Substring(i + 1);
			Assembly[] assemblies = AddressablesRuntimeProperties.GetAssemblies();
			for (int j = 0; j < assemblies.Length; j++)
			{
				Type t = assemblies[j].GetType(className, false, false);
				if (!(t == null))
				{
					try
					{
						PropertyInfo pi = t.GetProperty(propName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
						if (pi != null)
						{
							object v = pi.GetValue(null, null);
							if (v != null)
							{
								AddressablesRuntimeProperties.s_CachedValues.Add(name, v.ToString());
								return v.ToString();
							}
						}
						FieldInfo fi = t.GetField(propName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
						if (fi != null)
						{
							object v2 = fi.GetValue(null);
							if (v2 != null)
							{
								AddressablesRuntimeProperties.s_CachedValues.Add(name, v2.ToString());
								return v2.ToString();
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return name;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009808 File Offset: 0x00007A08
		public static string EvaluateString(string input)
		{
			return AddressablesRuntimeProperties.EvaluateString(input, '{', '}', new Func<string, string>(AddressablesRuntimeProperties.EvaluateProperty));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009820 File Offset: 0x00007A20
		public static string EvaluateString(string inputString, char startDelimiter, char endDelimiter, Func<string, string> varFunc)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return string.Empty;
			}
			string originalString = inputString;
			Stack<string> tokenStack;
			Stack<int> tokenStartStack;
			if (!AddressablesRuntimeProperties.s_StaticStacksAreInUse)
			{
				tokenStack = AddressablesRuntimeProperties.s_TokenStack;
				tokenStartStack = AddressablesRuntimeProperties.s_TokenStartStack;
				AddressablesRuntimeProperties.s_StaticStacksAreInUse = true;
			}
			else
			{
				tokenStack = new Stack<string>(32);
				tokenStartStack = new Stack<int>(32);
			}
			tokenStack.Push(inputString);
			int popTokenAt = inputString.Length;
			char[] delimiters = new char[] { startDelimiter, endDelimiter };
			bool delimitersMatch = startDelimiter == endDelimiter;
			int i = inputString.IndexOf(startDelimiter);
			int prevIndex = -2;
			while (i >= 0)
			{
				char c = inputString[i];
				if (c == startDelimiter && (!delimitersMatch || tokenStartStack.Count == 0))
				{
					tokenStartStack.Push(i);
					i++;
				}
				else if (c == endDelimiter && tokenStartStack.Count > 0)
				{
					int start = tokenStartStack.Peek();
					string token = inputString.Substring(start + 1, i - start - 1);
					if (popTokenAt <= i)
					{
						tokenStack.Pop();
					}
					string tokenVal;
					if (tokenStack.Contains(token))
					{
						tokenVal = "#ERROR-CyclicToken#";
					}
					else
					{
						tokenVal = ((varFunc == null) ? string.Empty : varFunc(token));
						tokenStack.Push(token);
					}
					i = tokenStartStack.Pop();
					popTokenAt = i + tokenVal.Length + 1;
					if (i > 0)
					{
						int rhsStartIndex = i + token.Length + 2;
						if (rhsStartIndex == inputString.Length)
						{
							inputString = inputString.Substring(0, i) + tokenVal;
						}
						else
						{
							inputString = inputString.Substring(0, i) + tokenVal + inputString.Substring(rhsStartIndex);
						}
					}
					else
					{
						inputString = tokenVal + inputString.Substring(i + token.Length + 2);
					}
				}
				if (prevIndex == i)
				{
					return "#ERROR-" + originalString + " contains unmatched delimiters#";
				}
				prevIndex = i;
				i = inputString.IndexOfAny(delimiters, i);
			}
			tokenStack.Clear();
			tokenStartStack.Clear();
			if (tokenStack == AddressablesRuntimeProperties.s_TokenStack)
			{
				AddressablesRuntimeProperties.s_StaticStacksAreInUse = false;
			}
			return inputString;
		}

		// Token: 0x04000149 RID: 329
		private static Stack<string> s_TokenStack = new Stack<string>(32);

		// Token: 0x0400014A RID: 330
		private static Stack<int> s_TokenStartStack = new Stack<int>(32);

		// Token: 0x0400014B RID: 331
		private static bool s_StaticStacksAreInUse = false;

		// Token: 0x0400014C RID: 332
		private static Dictionary<string, string> s_CachedValues = new Dictionary<string, string>();
	}
}

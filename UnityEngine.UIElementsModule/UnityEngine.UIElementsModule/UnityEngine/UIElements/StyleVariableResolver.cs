using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	// Token: 0x0200043D RID: 1085
	internal class StyleVariableResolver
	{
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00071DA5 File Offset: 0x0006FFA5
		private StyleSheet currentSheet
		{
			get
			{
				return this.m_CurrentContext.sheet;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00071DB2 File Offset: 0x0006FFB2
		private StyleValueHandle[] currentHandles
		{
			get
			{
				return this.m_CurrentContext.handles;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00071DBF File Offset: 0x0006FFBF
		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00071DC7 File Offset: 0x0006FFC7
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x00071DCF File Offset: 0x0006FFCF
		public StyleVariableContext variableContext { get; set; }

		// Token: 0x06001F4C RID: 8012 RVA: 0x00071DD8 File Offset: 0x0006FFD8
		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_ContextStack.Clear();
			this.m_Property = property;
			this.PushContext(sheet, handles);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00071E04 File Offset: 0x00070004
		private void PushContext(StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_CurrentContext = new StyleVariableResolver.ResolveContext
			{
				sheet = sheet,
				handles = handles
			};
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00071E43 File Offset: 0x00070043
		private void PopContext()
		{
			this.m_ContextStack.Pop();
			this.m_CurrentContext = this.m_ContextStack.Peek();
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00071E64 File Offset: 0x00070064
		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.currentSheet,
				handle = handle
			});
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00071E9C File Offset: 0x0007009C
		public bool ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			int argc;
			string varName;
			StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out argc, out varName);
			StyleVariableResolver.Result result = this.ResolveVarFunction(ref index, argc, varName);
			return result == StyleVariableResolver.Result.Valid;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00071EE0 File Offset: 0x000700E0
		private StyleVariableResolver.Result ResolveVarFunction(ref int index, int argc, string varName)
		{
			StyleVariableResolver.Result result = this.ResolveVariable(varName);
			bool flag = result == StyleVariableResolver.Result.NotFound && argc > 1;
			if (flag)
			{
				StyleValueHandle[] currentHandles = this.currentHandles;
				int num = index + 1;
				index = num;
				StyleValueHandle h = currentHandles[num];
				Debug.Assert(h.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", h.valueType));
				bool flag2 = h.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
				if (flag2)
				{
					index++;
					result = this.ResolveFallback(ref index);
				}
			}
			return result;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00071F7C File Offset: 0x0007017C
		public bool ValidateResolvedValues()
		{
			bool isCustomProperty = this.m_Property.isCustomProperty;
			bool flag;
			if (isCustomProperty)
			{
				flag = true;
			}
			else
			{
				string syntax;
				bool flag2 = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out syntax);
				if (flag2)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					flag = false;
				}
				else
				{
					Expression validationExpression = StyleVariableResolver.s_SyntaxParser.Parse(syntax);
					flag = this.m_Matcher.Match(validationExpression, this.m_ResolvedValues).success;
				}
			}
			return flag;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00072008 File Offset: 0x00070208
		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable sv;
			bool flag = !this.variableContext.TryFindVariable(variableName, out sv);
			StyleVariableResolver.Result result2;
			if (flag)
			{
				result2 = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(sv.name);
				if (flag2)
				{
					result2 = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(sv.name);
					StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
					int i = 0;
					while (i < sv.handles.Length && result == StyleVariableResolver.Result.Valid)
					{
						bool flag3 = this.m_ResolvedValues.Count + 1 > 100;
						if (flag3)
						{
							return StyleVariableResolver.Result.Invalid;
						}
						StyleValueHandle h = sv.handles[i];
						bool flag4 = h.IsVarFunction();
						if (flag4)
						{
							this.PushContext(sv.sheet, sv.handles);
							int argc;
							string varName;
							StyleVariableResolver.ParseVarFunction(sv.sheet, sv.handles, ref i, out argc, out varName);
							result = this.ResolveVarFunction(ref i, argc, varName);
							this.PopContext();
						}
						else
						{
							this.m_ResolvedValues.Add(new StylePropertyValue
							{
								sheet = sv.sheet,
								handle = h
							});
						}
						i++;
					}
					this.m_ResolvedVarStack.Pop();
					result2 = result;
				}
			}
			return result2;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00072150 File Offset: 0x00070350
		private StyleVariableResolver.Result ResolveFallback(ref int index)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.currentHandles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle h = this.currentHandles[index];
				bool flag = h.IsVarFunction();
				if (flag)
				{
					int argc;
					string varName;
					StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out argc, out varName);
					result = this.ResolveVariable(varName);
					bool flag2 = result == StyleVariableResolver.Result.NotFound;
					if (flag2)
					{
						bool flag3 = argc > 1;
						if (flag3)
						{
							StyleValueHandle[] currentHandles = this.currentHandles;
							int num = index + 1;
							index = num;
							h = currentHandles[num];
							Debug.Assert(h.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", h.valueType));
							bool flag4 = h.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
							if (flag4)
							{
								index++;
								result = this.ResolveFallback(ref index);
							}
						}
					}
				}
				else
				{
					this.m_ResolvedValues.Add(new StylePropertyValue
					{
						sheet = this.currentSheet,
						handle = h
					});
				}
				index++;
			}
			return result;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00072288 File Offset: 0x00070488
		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}

		// Token: 0x04000DDA RID: 3546
		internal const int kMaxResolves = 100;

		// Token: 0x04000DDB RID: 3547
		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		// Token: 0x04000DDC RID: 3548
		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		// Token: 0x04000DDD RID: 3549
		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		// Token: 0x04000DDE RID: 3550
		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		// Token: 0x04000DDF RID: 3551
		private StyleProperty m_Property;

		// Token: 0x04000DE0 RID: 3552
		private Stack<StyleVariableResolver.ResolveContext> m_ContextStack = new Stack<StyleVariableResolver.ResolveContext>();

		// Token: 0x04000DE1 RID: 3553
		private StyleVariableResolver.ResolveContext m_CurrentContext;

		// Token: 0x0200043E RID: 1086
		private enum Result
		{
			// Token: 0x04000DE4 RID: 3556
			Valid,
			// Token: 0x04000DE5 RID: 3557
			Invalid,
			// Token: 0x04000DE6 RID: 3558
			NotFound
		}

		// Token: 0x0200043F RID: 1087
		private struct ResolveContext
		{
			// Token: 0x04000DE7 RID: 3559
			public StyleSheet sheet;

			// Token: 0x04000DE8 RID: 3560
			public StyleValueHandle[] handles;
		}
	}
}

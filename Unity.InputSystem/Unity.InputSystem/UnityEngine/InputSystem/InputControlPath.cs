using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200007C RID: 124
	public static class InputControlPath
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00017DC6 File Offset: 0x00015FC6
		internal static string CleanSlashes(this string pathComponent)
		{
			return pathComponent.Replace('/', ' ');
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00017DD4 File Offset: 0x00015FD4
		public static string Combine(InputControl parent, string path)
		{
			if (parent == null)
			{
				if (string.IsNullOrEmpty(path))
				{
					return string.Empty;
				}
				if (path[0] != '/')
				{
					return "/" + path;
				}
				return path;
			}
			else
			{
				if (string.IsNullOrEmpty(path))
				{
					return parent.path;
				}
				return parent.path + "/" + path;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00017E2C File Offset: 0x0001602C
		public static string ToHumanReadableString(string path, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			string text;
			string text2;
			return InputControlPath.ToHumanReadableString(path, out text, out text2, options, control);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00017E48 File Offset: 0x00016048
		public static string ToHumanReadableString(string path, out string deviceLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			deviceLayoutName = null;
			controlPath = null;
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (control != null)
			{
				InputControl matchedControl = InputControlPath.TryFindControl(control, path, 0) ?? (InputControlPath.Matches(path, control) ? control : null);
				if (matchedControl != null)
				{
					string text = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(matchedControl.shortDisplayName)) ? matchedControl.shortDisplayName : matchedControl.displayName);
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None)
					{
						text = text + " [" + matchedControl.device.displayName + "]";
					}
					deviceLayoutName = matchedControl.device.layout;
					if (!(matchedControl is InputDevice))
					{
						controlPath = matchedControl.path.Substring(matchedControl.device.path.Length + 1);
					}
					return text;
				}
			}
			StringBuilder buffer = new StringBuilder();
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			string text2;
			using (InputControlLayout.CacheRef())
			{
				if (parser.MoveToNextComponent())
				{
					string currentLayoutName;
					string device = parser.current.ToHumanReadableString(null, null, out currentLayoutName, out text2, options);
					deviceLayoutName = currentLayoutName;
					bool isFirstControlLevel = true;
					while (parser.MoveToNextComponent())
					{
						if (!isFirstControlLevel)
						{
							buffer.Append('/');
						}
						buffer.Append(parser.current.ToHumanReadableString(currentLayoutName, controlPath, out currentLayoutName, out controlPath, options));
						isFirstControlLevel = false;
					}
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(device))
					{
						buffer.Append(" [");
						buffer.Append(device);
						buffer.Append(']');
					}
				}
				if (buffer.Length == 0)
				{
					text2 = path;
				}
				else
				{
					text2 = buffer.ToString();
				}
			}
			return text2;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00017FDC File Offset: 0x000161DC
		public static string[] TryGetDeviceUsages(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			if (!parser.MoveToNextComponent())
			{
				return null;
			}
			if (parser.current.m_Usages.length > 0)
			{
				return parser.current.m_Usages.ToArray<string>((Substring x) => x.ToString());
			}
			return null;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018050 File Offset: 0x00016250
		public static string TryGetDeviceLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			if (!parser.MoveToNextComponent())
			{
				return null;
			}
			if (parser.current.m_Layout.length > 0)
			{
				return parser.current.m_Layout.ToString().Unescape("ntr\\\"", "\n\t\r\\\"");
			}
			if (parser.current.isWildcard)
			{
				return "*";
			}
			return null;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000180D0 File Offset: 0x000162D0
		public static string TryGetControlLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int pathLength = path.Length;
			int indexOfLastSlash = path.LastIndexOf('/');
			if (indexOfLastSlash == -1 || indexOfLastSlash == 0)
			{
				return null;
			}
			if (pathLength > indexOfLastSlash + 2 && path[indexOfLastSlash + 1] == '<' && path[pathLength - 1] == '>')
			{
				int layoutNameStart = indexOfLastSlash + 2;
				int layoutNameLength = pathLength - layoutNameStart - 1;
				return path.Substring(layoutNameStart, layoutNameLength);
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			if (!parser.MoveToNextComponent())
			{
				return null;
			}
			if (parser.current.isWildcard)
			{
				throw new NotImplementedException();
			}
			if (parser.current.m_Layout.length == 0)
			{
				return null;
			}
			string deviceLayoutName = parser.current.m_Layout.ToString();
			if (!parser.MoveToNextComponent())
			{
				return null;
			}
			if (parser.current.isWildcard)
			{
				return "*";
			}
			return InputControlPath.FindControlLayoutRecursive(ref parser, deviceLayoutName.Unescape("ntr\\\"", "\n\t\r\\\""));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000181C8 File Offset: 0x000163C8
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, string layoutName)
		{
			string text;
			using (InputControlLayout.CacheRef())
			{
				InputControlLayout layout = InputControlLayout.cache.FindOrLoadLayout(new InternedString(layoutName), false);
				if (layout == null)
				{
					text = null;
				}
				else
				{
					text = InputControlPath.FindControlLayoutRecursive(ref parser, layout);
				}
			}
			return text;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00018224 File Offset: 0x00016424
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, InputControlLayout layout)
		{
			string currentResult = null;
			int controlCount = layout.controls.Count;
			for (int i = 0; i < controlCount; i++)
			{
				if (InputControlPath.ControlLayoutMatchesPathComponent(ref layout.m_Controls[i], ref parser))
				{
					InternedString controlLayoutName = layout.m_Controls[i].layout;
					if (!parser.isAtEnd)
					{
						InputControlPath.PathParser childPathParser = parser;
						if (childPathParser.MoveToNextComponent())
						{
							string childControlLayoutName = InputControlPath.FindControlLayoutRecursive(ref childPathParser, controlLayoutName);
							if (childControlLayoutName != null)
							{
								if (currentResult != null && childControlLayoutName != currentResult)
								{
									return null;
								}
								currentResult = childControlLayoutName;
							}
						}
					}
					else
					{
						if (currentResult != null && controlLayoutName != currentResult)
						{
							return null;
						}
						currentResult = controlLayoutName.ToString();
					}
				}
			}
			return currentResult;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000182DC File Offset: 0x000164DC
		private static bool ControlLayoutMatchesPathComponent(ref InputControlLayout.ControlItem controlItem, ref InputControlPath.PathParser parser)
		{
			Substring layout = parser.current.m_Layout;
			if (layout.length > 0 && !InputControlPath.StringMatches(layout, controlItem.layout))
			{
				return false;
			}
			if (parser.current.m_Usages.length > 0)
			{
				for (int usageIndex = 0; usageIndex < parser.current.m_Usages.length; usageIndex++)
				{
					Substring usage = parser.current.m_Usages[usageIndex];
					if (usage.length > 0)
					{
						int usageCount = controlItem.usages.Count;
						bool anyUsageMatches = false;
						for (int i = 0; i < usageCount; i++)
						{
							if (InputControlPath.StringMatches(usage, controlItem.usages[i]))
							{
								anyUsageMatches = true;
								break;
							}
						}
						if (!anyUsageMatches)
						{
							return false;
						}
					}
				}
			}
			Substring name = parser.current.m_Name;
			return name.length <= 0 || InputControlPath.StringMatches(name, controlItem.name);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000183CC File Offset: 0x000165CC
		private static bool StringMatches(Substring str, InternedString matchTo)
		{
			int strLength = str.length;
			int matchToLength = matchTo.length;
			string matchToLowerCase = matchTo.ToLower();
			int posInMatchTo = 0;
			int posInStr = 0;
			while (posInStr < strLength && posInMatchTo < matchToLength)
			{
				char nextChar = str[posInStr];
				if (nextChar == '\\' && posInStr + 1 < strLength)
				{
					nextChar = str[++posInStr];
				}
				if (nextChar == '*')
				{
					if (posInStr == strLength - 1)
					{
						return true;
					}
					posInStr++;
					nextChar = char.ToLower(str[posInStr], CultureInfo.InvariantCulture);
					while (posInMatchTo < matchToLength && matchToLowerCase[posInMatchTo] != nextChar)
					{
						posInMatchTo++;
					}
					if (posInMatchTo == matchToLength)
					{
						return false;
					}
				}
				else if (char.ToLower(nextChar, CultureInfo.InvariantCulture) != matchToLowerCase[posInMatchTo])
				{
					return false;
				}
				posInMatchTo++;
				posInStr++;
			}
			return posInMatchTo == matchToLength && posInStr == strLength;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000184A0 File Offset: 0x000166A0
		public static InputControl TryFindControl(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindControl<InputControl>(control, path, indexInPath);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000184AC File Offset: 0x000166AC
		public static InputControl[] TryFindControls(InputControl control, string path, int indexInPath = 0)
		{
			InputControlList<InputControl> matches = new InputControlList<InputControl>(Allocator.Temp, 0);
			InputControl[] array;
			try
			{
				InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref matches);
				array = matches.ToArray(false);
			}
			finally
			{
				matches.Dispose();
			}
			return array;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000184F4 File Offset: 0x000166F4
		public static int TryFindControls(InputControl control, string path, ref InputControlList<InputControl> matches, int indexInPath = 0)
		{
			return InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref matches);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00018500 File Offset: 0x00016700
		public static TControl TryFindControl<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(path))
			{
				return default(TControl);
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			InputControlList<TControl> none = default(InputControlList<TControl>);
			return InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref none, false);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00018554 File Offset: 0x00016754
		public static int TryFindControls<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			int countBefore = matches.Count;
			InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref matches, true);
			return matches.Count - countBefore;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000185AA File Offset: 0x000167AA
		public static InputControl TryFindChild(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindChild<InputControl>(control, path, indexInPath);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000185B4 File Offset: 0x000167B4
		public static TControl TryFindChild<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputControl> children = control.children;
			int childCount = children.Count;
			for (int i = 0; i < childCount; i++)
			{
				TControl match = InputControlPath.TryFindControl<TControl>(children[i], path, indexInPath);
				if (match != null)
				{
					return match;
				}
			}
			return default(TControl);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001861C File Offset: 0x0001681C
		public static bool Matches(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref parser, control, false);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001865C File Offset: 0x0001685C
		internal static bool MatchControlComponent(ref InputControlPath.ParsedPathComponent expectedControlComponent, ref InputControlLayout.ControlItem controlItem, bool matchAlias = false)
		{
			bool controlItemNameMatched = false;
			bool anyUsageMatches = false;
			if (!expectedControlComponent.m_Name.isEmpty)
			{
				if (InputControlPath.StringMatches(expectedControlComponent.m_Name, controlItem.name))
				{
					controlItemNameMatched = true;
				}
				else
				{
					if (!matchAlias)
					{
						return false;
					}
					ReadOnlyArray<InternedString> aliases = controlItem.aliases;
					for (int i = 0; i < aliases.Count; i++)
					{
						if (InputControlPath.StringMatches(expectedControlComponent.m_Name, aliases[i]))
						{
							controlItemNameMatched = true;
							break;
						}
					}
				}
			}
			foreach (Substring usage in expectedControlComponent.m_Usages)
			{
				if (!usage.isEmpty)
				{
					int usageCount = controlItem.usages.Count;
					for (int j = 0; j < usageCount; j++)
					{
						if (InputControlPath.StringMatches(usage, controlItem.usages[j]))
						{
							anyUsageMatches = true;
							break;
						}
					}
				}
			}
			return controlItemNameMatched || anyUsageMatches;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00018758 File Offset: 0x00016958
		public static bool MatchesPrefix(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref parser, control, true) && parser.isAtEnd;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000187A8 File Offset: 0x000169A8
		private static bool MatchesRecursive(ref InputControlPath.PathParser parser, InputControl currentControl, bool prefixOnly = false)
		{
			InputControl parent = currentControl.parent;
			if (parent != null && !InputControlPath.MatchesRecursive(ref parser, parent, prefixOnly))
			{
				return false;
			}
			if (!parser.MoveToNextComponent())
			{
				return prefixOnly;
			}
			return parser.current.Matches(currentControl);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000187E4 File Offset: 0x000169E4
		private static TControl MatchControlsRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			int pathLength = path.Length;
			bool controlIsMatch = true;
			if (path[indexInPath] == '<')
			{
				indexInPath++;
				controlIsMatch = InputControlPath.MatchPathComponent(control.layout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
				if (!controlIsMatch)
				{
					InternedString baseLayout = control.m_Layout;
					while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(baseLayout, out baseLayout))
					{
						controlIsMatch = InputControlPath.MatchPathComponent(baseLayout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
						if (controlIsMatch)
						{
							break;
						}
					}
				}
			}
			while (indexInPath < pathLength && path[indexInPath] == '{' && controlIsMatch)
			{
				indexInPath++;
				for (int i = 0; i < control.usages.Count; i++)
				{
					controlIsMatch = InputControlPath.MatchPathComponent(control.usages[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0);
					if (controlIsMatch)
					{
						break;
					}
				}
			}
			if (indexInPath < pathLength - 1 && controlIsMatch && path[indexInPath] == '#' && path[indexInPath + 1] == '(')
			{
				indexInPath += 2;
				controlIsMatch = InputControlPath.MatchPathComponent(control.displayName, path, ref indexInPath, InputControlPath.PathComponentType.DisplayName, 0);
			}
			if (indexInPath < pathLength && controlIsMatch && path[indexInPath] != '/')
			{
				controlIsMatch = InputControlPath.MatchPathComponent(control.name, path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
				if (!controlIsMatch)
				{
					int j = 0;
					while (j < control.aliases.Count && !controlIsMatch)
					{
						controlIsMatch = InputControlPath.MatchPathComponent(control.aliases[j], path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
						j++;
					}
				}
			}
			if (controlIsMatch)
			{
				if (indexInPath < pathLength && path[indexInPath] == '*')
				{
					indexInPath++;
				}
				if (indexInPath == pathLength)
				{
					TControl match = control as TControl;
					if (match == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(match);
					}
					return match;
				}
				else if (path[indexInPath] == '/')
				{
					indexInPath++;
					if (indexInPath != pathLength)
					{
						TControl lastMatch;
						if (path[indexInPath] == '{')
						{
							lastMatch = InputControlPath.MatchByUsageAtDeviceRootRecursive<TControl>(control.device, path, indexInPath, ref matches, matchMultiple);
						}
						else
						{
							lastMatch = InputControlPath.MatchChildrenRecursive<TControl>(control, path, indexInPath, ref matches, matchMultiple);
						}
						return lastMatch;
					}
					TControl match2 = control as TControl;
					if (match2 == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(match2);
					}
					return match2;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018A18 File Offset: 0x00016C18
		private static TControl MatchByUsageAtDeviceRootRecursive<TControl>(InputDevice device, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			InternedString[] usages = device.m_UsagesForEachControl;
			if (usages == null)
			{
				return default(TControl);
			}
			int usageCount = device.m_UsageToControl.LengthSafe<InputControl>();
			int startIndex = indexInPath + 1;
			bool pathCanMatchMultiple = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			int pathLength = path.Length;
			indexInPath++;
			if (indexInPath == pathLength)
			{
				throw new ArgumentException("Invalid path spec '" + path + "'; trailing '{'", "path");
			}
			TControl lastMatch = default(TControl);
			for (int i = 0; i < usageCount; i++)
			{
				if (!InputControlPath.MatchPathComponent(usages[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0))
				{
					indexInPath = startIndex;
				}
				else
				{
					InputControl controlMatchedByUsage = device.m_UsageToControl[i];
					if (indexInPath < pathLength && path[indexInPath] == '/')
					{
						lastMatch = InputControlPath.MatchChildrenRecursive<TControl>(controlMatchedByUsage, path, indexInPath + 1, ref matches, matchMultiple);
						if (lastMatch != null && !pathCanMatchMultiple)
						{
							break;
						}
						if (lastMatch != null && !matchMultiple)
						{
							break;
						}
					}
					else
					{
						lastMatch = controlMatchedByUsage as TControl;
						if (lastMatch != null)
						{
							if (!matchMultiple)
							{
								break;
							}
							matches.Add(lastMatch);
						}
					}
				}
			}
			return lastMatch;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018B28 File Offset: 0x00016D28
		private static TControl MatchChildrenRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			ReadOnlyArray<InputControl> children = control.children;
			int childCount = children.Count;
			TControl lastMatch = default(TControl);
			bool pathCanMatchMultiple = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			for (int i = 0; i < childCount; i++)
			{
				TControl childMatch = InputControlPath.MatchControlsRecursive<TControl>(children[i], path, indexInPath, ref matches, matchMultiple);
				if (childMatch != null)
				{
					if (!pathCanMatchMultiple)
					{
						return childMatch;
					}
					if (!matchMultiple)
					{
						return childMatch;
					}
					lastMatch = childMatch;
				}
			}
			return lastMatch;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018B94 File Offset: 0x00016D94
		private static bool MatchPathComponent(string component, string path, ref int indexInPath, InputControlPath.PathComponentType componentType, int startIndexInComponent = 0)
		{
			int componentLength = component.Length;
			int pathLength = path.Length;
			int startIndex = indexInPath;
			int indexInComponent = startIndexInComponent;
			while (indexInPath < pathLength)
			{
				char nextCharInPath = path[indexInPath];
				if (nextCharInPath == '\\' && indexInPath + 1 < pathLength)
				{
					indexInPath++;
					nextCharInPath = path[indexInPath];
				}
				else
				{
					if (nextCharInPath == '/' && componentType == InputControlPath.PathComponentType.Name)
					{
						break;
					}
					if ((nextCharInPath == '>' && componentType == InputControlPath.PathComponentType.Layout) || (nextCharInPath == '}' && componentType == InputControlPath.PathComponentType.Usage) || (nextCharInPath == ')' && componentType == InputControlPath.PathComponentType.DisplayName))
					{
						indexInPath++;
						break;
					}
					if (nextCharInPath == '*')
					{
						int indexAfterWildcard = indexInPath + 1;
						if (indexInPath < pathLength - 1 && indexInComponent < componentLength && InputControlPath.MatchPathComponent(component, path, ref indexAfterWildcard, componentType, indexInComponent))
						{
							indexInPath = indexAfterWildcard;
							return true;
						}
						if (indexInComponent < componentLength)
						{
							indexInComponent++;
							continue;
						}
						return true;
					}
				}
				if (indexInComponent == componentLength)
				{
					indexInPath = startIndex;
					return false;
				}
				char charInComponent = component[indexInComponent];
				if (charInComponent != nextCharInPath && char.ToLower(charInComponent, CultureInfo.InvariantCulture) != char.ToLower(nextCharInPath, CultureInfo.InvariantCulture))
				{
					indexInPath = startIndex;
					return false;
				}
				indexInComponent++;
				indexInPath++;
			}
			if (indexInComponent == componentLength)
			{
				return true;
			}
			indexInPath = startIndex;
			return false;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018CA4 File Offset: 0x00016EA4
		private static bool PathComponentCanYieldMultipleMatches(string path, int indexInPath)
		{
			int indexOfNextSlash = path.IndexOf('/', indexInPath);
			if (indexOfNextSlash == -1)
			{
				return path.IndexOf('*', indexInPath) != -1 || path.IndexOf('<', indexInPath) != -1;
			}
			int length = indexOfNextSlash - indexInPath;
			return path.IndexOf('*', indexInPath, length) != -1 || path.IndexOf('<', indexInPath, length) != -1;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018D00 File Offset: 0x00016F00
		public static IEnumerable<InputControlPath.ParsedPathComponent> Parse(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			while (parser.MoveToNextComponent())
			{
				yield return parser.current;
			}
			yield break;
		}

		// Token: 0x040002C6 RID: 710
		public const string Wildcard = "*";

		// Token: 0x040002C7 RID: 711
		public const string DoubleWildcard = "**";

		// Token: 0x040002C8 RID: 712
		public const char Separator = '/';

		// Token: 0x040002C9 RID: 713
		internal const char SeparatorReplacement = ' ';

		// Token: 0x0200007D RID: 125
		[Flags]
		public enum HumanReadableStringOptions
		{
			// Token: 0x040002CB RID: 715
			None = 0,
			// Token: 0x040002CC RID: 716
			OmitDevice = 2,
			// Token: 0x040002CD RID: 717
			UseShortNames = 4
		}

		// Token: 0x0200007E RID: 126
		private enum PathComponentType
		{
			// Token: 0x040002CF RID: 719
			Name,
			// Token: 0x040002D0 RID: 720
			DisplayName,
			// Token: 0x040002D1 RID: 721
			Usage,
			// Token: 0x040002D2 RID: 722
			Layout
		}

		// Token: 0x0200007F RID: 127
		public struct ParsedPathComponent
		{
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x00018D10 File Offset: 0x00016F10
			public string layout
			{
				get
				{
					return this.m_Layout.ToString();
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x00018D23 File Offset: 0x00016F23
			public IEnumerable<string> usages
			{
				get
				{
					return this.m_Usages.Select((Substring x) => x.ToString());
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x00018D54 File Offset: 0x00016F54
			public string name
			{
				get
				{
					return this.m_Name.ToString();
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600060C RID: 1548 RVA: 0x00018D67 File Offset: 0x00016F67
			public string displayName
			{
				get
				{
					return this.m_DisplayName.ToString();
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x00018D7A File Offset: 0x00016F7A
			internal bool isWildcard
			{
				get
				{
					return this.m_Name == "*";
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x0600060E RID: 1550 RVA: 0x00018D91 File Offset: 0x00016F91
			internal bool isDoubleWildcard
			{
				get
				{
					return this.m_Name == "**";
				}
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x00018DA8 File Offset: 0x00016FA8
			internal string ToHumanReadableString(string parentLayoutName, string parentControlPath, out string referencedLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options)
			{
				referencedLayoutName = null;
				controlPath = null;
				string result = string.Empty;
				if (this.isWildcard)
				{
					result += "Any";
				}
				if (this.m_Usages.length > 0)
				{
					string combinedUsages = string.Empty;
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							if (combinedUsages != string.Empty)
							{
								combinedUsages = combinedUsages + " & " + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
							else
							{
								combinedUsages = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
						}
					}
					if (combinedUsages != string.Empty)
					{
						if (result != string.Empty)
						{
							result = result + " " + combinedUsages;
						}
						else
						{
							result += combinedUsages;
						}
					}
				}
				if (!this.m_Layout.isEmpty)
				{
					referencedLayoutName = this.m_Layout.ToString();
					InputControlLayout referencedLayout = InputControlLayout.cache.FindOrLoadLayout(referencedLayoutName, false);
					string layoutString;
					if (referencedLayout != null && !string.IsNullOrEmpty(referencedLayout.m_DisplayName))
					{
						layoutString = referencedLayout.m_DisplayName;
					}
					else
					{
						layoutString = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Layout);
					}
					if (!string.IsNullOrEmpty(result))
					{
						result = result + " " + layoutString;
					}
					else
					{
						result += layoutString;
					}
				}
				if (!this.m_Name.isEmpty && !this.isWildcard)
				{
					string nameString = null;
					if (!string.IsNullOrEmpty(parentLayoutName))
					{
						InputControlLayout parentLayout = InputControlLayout.cache.FindOrLoadLayout(new InternedString(parentLayoutName), false);
						if (parentLayout != null)
						{
							InternedString controlName = new InternedString(this.m_Name.ToString());
							int arrayIndex;
							InputControlLayout.ControlItem? control = parentLayout.FindControlIncludingArrayElements(controlName, out arrayIndex);
							if (control != null)
							{
								if (string.IsNullOrEmpty(parentControlPath))
								{
									if (arrayIndex != -1)
									{
										controlPath = string.Format("{0}{1}", control.Value.name, arrayIndex);
									}
									else
									{
										controlPath = control.Value.name;
									}
								}
								else if (arrayIndex != -1)
								{
									controlPath = string.Format("{0}/{1}{2}", parentControlPath, control.Value.name, arrayIndex);
								}
								else
								{
									controlPath = string.Format("{0}/{1}", parentControlPath, control.Value.name);
								}
								string shortDisplayName = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None) ? control.Value.shortDisplayName : null);
								string displayName = ((!string.IsNullOrEmpty(shortDisplayName)) ? shortDisplayName : control.Value.displayName);
								if (!string.IsNullOrEmpty(displayName))
								{
									if (arrayIndex != -1)
									{
										nameString = string.Format("{0} #{1}", displayName, arrayIndex);
									}
									else
									{
										nameString = displayName;
									}
								}
								if (string.IsNullOrEmpty(referencedLayoutName))
								{
									referencedLayoutName = control.Value.layout;
								}
							}
						}
					}
					if (nameString == null)
					{
						nameString = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Name);
					}
					if (!string.IsNullOrEmpty(result))
					{
						result = result + " " + nameString;
					}
					else
					{
						result += nameString;
					}
				}
				if (!this.m_DisplayName.isEmpty)
				{
					string str = "\"" + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_DisplayName) + "\"";
					if (!string.IsNullOrEmpty(result))
					{
						result = result + " " + str;
					}
					else
					{
						result += str;
					}
				}
				return result;
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x00019126 File Offset: 0x00017326
			private static string ToHumanReadableString(Substring substring)
			{
				return substring.ToString().Unescape("/*{<", "/*{<");
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00019144 File Offset: 0x00017344
			public bool Matches(InputControl control)
			{
				if (!this.m_Layout.isEmpty)
				{
					bool layoutMatches = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, control.layout);
					if (!layoutMatches)
					{
						InternedString baseLayout = control.m_Layout;
						while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(baseLayout, out baseLayout) && !layoutMatches)
						{
							layoutMatches = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, baseLayout.ToString());
						}
					}
					if (!layoutMatches)
					{
						return false;
					}
				}
				if (this.m_Usages.length > 0)
				{
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							ReadOnlyArray<InternedString> controlUsages = control.usages;
							bool haveUsageMatch = false;
							for (int ci = 0; ci < controlUsages.Count; ci++)
							{
								if (InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Usages[i], controlUsages[ci]))
								{
									haveUsageMatch = true;
									break;
								}
							}
							if (!haveUsageMatch)
							{
								return false;
							}
						}
					}
				}
				return (this.m_Name.isEmpty || this.isWildcard || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Name, control.name)) && (this.m_DisplayName.isEmpty || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_DisplayName, control.displayName));
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00019288 File Offset: 0x00017488
			private static bool ComparePathElementToString(Substring pathElement, string element)
			{
				int pathElementLength = pathElement.length;
				int elementLength = element.Length;
				int i = 0;
				int j = 0;
				bool pathElementDone;
				bool elementDone;
				for (;;)
				{
					pathElementDone = i == pathElementLength;
					elementDone = j == elementLength;
					if (pathElementDone || elementDone)
					{
						break;
					}
					char ch = pathElement[i];
					if (ch == '\\' && i + 1 < pathElementLength)
					{
						ch = pathElement[++i];
					}
					if (char.ToLowerInvariant(ch) != char.ToLowerInvariant(element[j]))
					{
						return false;
					}
					i++;
					j++;
				}
				return pathElementDone == elementDone;
			}

			// Token: 0x040002D3 RID: 723
			internal Substring m_Layout;

			// Token: 0x040002D4 RID: 724
			internal InlinedArray<Substring> m_Usages;

			// Token: 0x040002D5 RID: 725
			internal Substring m_Name;

			// Token: 0x040002D6 RID: 726
			internal Substring m_DisplayName;
		}

		// Token: 0x02000081 RID: 129
		private struct PathParser
		{
			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000616 RID: 1558 RVA: 0x00019321 File Offset: 0x00017521
			public bool isAtEnd
			{
				get
				{
					return this.rightIndexInPath == this.length;
				}
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x00019331 File Offset: 0x00017531
			public PathParser(string path)
			{
				this.path = path;
				this.length = path.Length;
				this.leftIndexInPath = 0;
				this.rightIndexInPath = 0;
				this.current = default(InputControlPath.ParsedPathComponent);
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00019360 File Offset: 0x00017560
			public bool MoveToNextComponent()
			{
				if (this.rightIndexInPath == this.length)
				{
					return false;
				}
				this.leftIndexInPath = this.rightIndexInPath;
				if (this.path[this.leftIndexInPath] == '/')
				{
					this.leftIndexInPath++;
					this.rightIndexInPath = this.leftIndexInPath;
					if (this.leftIndexInPath == this.length)
					{
						return false;
					}
				}
				Substring layout = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '<')
				{
					layout = this.ParseComponentPart('>');
				}
				InlinedArray<Substring> usages = default(InlinedArray<Substring>);
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '{')
				{
					usages.AppendWithCapacity(this.ParseComponentPart('}'), 10);
				}
				Substring displayName = default(Substring);
				if (this.rightIndexInPath < this.length - 1 && this.path[this.rightIndexInPath] == '#' && this.path[this.rightIndexInPath + 1] == '(')
				{
					this.rightIndexInPath++;
					displayName = this.ParseComponentPart(')');
				}
				Substring name = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != '/')
				{
					name = this.ParseComponentPart('/');
				}
				this.current = new InputControlPath.ParsedPathComponent
				{
					m_Layout = layout,
					m_Usages = usages,
					m_Name = name,
					m_DisplayName = displayName
				};
				return this.leftIndexInPath != this.rightIndexInPath;
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x0001950C File Offset: 0x0001770C
			private Substring ParseComponentPart(char terminator)
			{
				if (terminator != '/')
				{
					this.rightIndexInPath++;
				}
				int partStartIndex = this.rightIndexInPath;
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != terminator)
				{
					if (this.path[this.rightIndexInPath] == '\\' && this.rightIndexInPath + 1 < this.length)
					{
						this.rightIndexInPath++;
					}
					this.rightIndexInPath++;
				}
				int partLength = this.rightIndexInPath - partStartIndex;
				if (this.rightIndexInPath < this.length && terminator != '/')
				{
					this.rightIndexInPath++;
				}
				return new Substring(this.path, partStartIndex, partLength);
			}

			// Token: 0x040002D9 RID: 729
			private string path;

			// Token: 0x040002DA RID: 730
			private int length;

			// Token: 0x040002DB RID: 731
			private int leftIndexInPath;

			// Token: 0x040002DC RID: 732
			private int rightIndexInPath;

			// Token: 0x040002DD RID: 733
			public InputControlPath.ParsedPathComponent current;
		}
	}
}

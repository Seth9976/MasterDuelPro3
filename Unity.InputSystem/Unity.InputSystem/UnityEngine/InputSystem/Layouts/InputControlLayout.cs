using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x020001F5 RID: 501
	public class InputControlLayout
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x000552D8 File Offset: 0x000534D8
		public static InternedString DefaultVariant
		{
			get
			{
				return InputControlLayout.s_DefaultVariant;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000552DF File Offset: 0x000534DF
		public InternedString name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000552E7 File Offset: 0x000534E7
		public string displayName
		{
			get
			{
				return this.m_DisplayName ?? this.m_Name;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000552FE File Offset: 0x000534FE
		public Type type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00055306 File Offset: 0x00053506
		public InternedString variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0005530E File Offset: 0x0005350E
		public FourCC stateFormat
		{
			get
			{
				return this.m_StateFormat;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00055316 File Offset: 0x00053516
		public int stateSizeInBytes
		{
			get
			{
				return this.m_StateSizeInBytes;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0005531E File Offset: 0x0005351E
		public IEnumerable<InternedString> baseLayouts
		{
			get
			{
				return this.m_BaseLayouts;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0005532B File Offset: 0x0005352B
		public IEnumerable<InternedString> appliedOverrides
		{
			get
			{
				return this.m_AppliedOverrides;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00055338 File Offset: 0x00053538
		public ReadOnlyArray<InternedString> commonUsages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_CommonUsages);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00055345 File Offset: 0x00053545
		public ReadOnlyArray<InputControlLayout.ControlItem> controls
		{
			get
			{
				return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00055352 File Offset: 0x00053552
		public bool updateBeforeRender
		{
			get
			{
				return this.m_UpdateBeforeRender.GetValueOrDefault();
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0005535F File Offset: 0x0005355F
		public bool isDeviceLayout
		{
			get
			{
				return typeof(InputDevice).IsAssignableFrom(this.m_Type);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00055376 File Offset: 0x00053576
		public bool isControlLayout
		{
			get
			{
				return !this.isDeviceLayout;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00055381 File Offset: 0x00053581
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0005538E File Offset: 0x0005358E
		public bool isOverride
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsOverride) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsOverride;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsOverride;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000553B1 File Offset: 0x000535B1
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x000553BE File Offset: 0x000535BE
		public bool isGenericTypeOfDevice
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsGenericTypeOfDevice) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsGenericTypeOfDevice;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsGenericTypeOfDevice;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000553E1 File Offset: 0x000535E1
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x000553EE File Offset: 0x000535EE
		public bool hideInUI
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.HideInUI) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.HideInUI;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.HideInUI;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00055411 File Offset: 0x00053611
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x0005541F File Offset: 0x0005361F
		public bool isNoisy
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsNoisy) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsNoisy;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsNoisy;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00055444 File Offset: 0x00053644
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00055478 File Offset: 0x00053678
		public bool? canRunInBackground
		{
			get
			{
				if ((this.m_Flags & InputControlLayout.Flags.CanRunInBackgroundIsSet) == (InputControlLayout.Flags)0)
				{
					return null;
				}
				return new bool?((this.m_Flags & InputControlLayout.Flags.CanRunInBackground) > (InputControlLayout.Flags)0);
			}
			internal set
			{
				if (value == null)
				{
					this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackgroundIsSet;
					return;
				}
				this.m_Flags |= InputControlLayout.Flags.CanRunInBackgroundIsSet;
				if (value.Value)
				{
					this.m_Flags |= InputControlLayout.Flags.CanRunInBackground;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackground;
			}
		}

		// Token: 0x1700055C RID: 1372
		public InputControlLayout.ControlItem this[string path]
		{
			get
			{
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				if (this.m_Controls != null)
				{
					for (int i = 0; i < this.m_Controls.Length; i++)
					{
						if (this.m_Controls[i].name == path)
						{
							return this.m_Controls[i];
						}
					}
				}
				throw new KeyNotFoundException(string.Format("Cannot find control '{0}' in layout '{1}'", path, this.name));
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00055550 File Offset: 0x00053750
		public InputControlLayout.ControlItem? FindControl(InternedString path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (this.m_Controls == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				if (this.m_Controls[i].name == path)
				{
					return new InputControlLayout.ControlItem?(this.m_Controls[i]);
				}
			}
			return null;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000555D0 File Offset: 0x000537D0
		public InputControlLayout.ControlItem? FindControlIncludingArrayElements(string path, out int arrayIndex)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			arrayIndex = -1;
			if (this.m_Controls == null)
			{
				return null;
			}
			int arrayIndexAccumulated = 0;
			int lastDigitIndex = path.Length;
			while (lastDigitIndex > 0 && char.IsDigit(path[lastDigitIndex - 1]))
			{
				lastDigitIndex--;
				arrayIndexAccumulated *= 10;
				arrayIndexAccumulated += (int)(path[lastDigitIndex] - '0');
			}
			int arrayNameLength = 0;
			if (lastDigitIndex < path.Length && lastDigitIndex > 0)
			{
				arrayNameLength = lastDigitIndex;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				ref InputControlLayout.ControlItem control = ref this.m_Controls[i];
				if (string.Compare(control.name, path, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return new InputControlLayout.ControlItem?(control);
				}
				if (control.isArray && arrayNameLength > 0 && arrayNameLength == control.name.length && string.Compare(control.name.ToString(), 0, path, 0, arrayNameLength, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					arrayIndex = arrayIndexAccumulated;
					return new InputControlLayout.ControlItem?(control);
				}
			}
			return null;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000556F4 File Offset: 0x000538F4
		public Type GetValueType()
		{
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(this.type, typeof(InputControl<>), 0);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0005570C File Offset: 0x0005390C
		public static InputControlLayout FromType(string name, Type type)
		{
			List<InputControlLayout.ControlItem> controlLayouts = new List<InputControlLayout.ControlItem>();
			InputControlLayoutAttribute layoutAttribute = type.GetCustomAttribute(true);
			FourCC stateFormat = default(FourCC);
			if (layoutAttribute != null && layoutAttribute.stateType != null)
			{
				InputControlLayout.AddControlItems(layoutAttribute.stateType, controlLayouts, name);
				if (typeof(IInputStateTypeInfo).IsAssignableFrom(layoutAttribute.stateType))
				{
					stateFormat = ((IInputStateTypeInfo)Activator.CreateInstance(layoutAttribute.stateType)).format;
				}
			}
			else
			{
				InputControlLayout.AddControlItems(type, controlLayouts, name);
			}
			if (layoutAttribute != null && !string.IsNullOrEmpty(layoutAttribute.stateFormat))
			{
				stateFormat = new FourCC(layoutAttribute.stateFormat);
			}
			InternedString variants = default(InternedString);
			if (layoutAttribute != null)
			{
				variants = new InternedString(layoutAttribute.variants);
			}
			InputControlLayout layout = new InputControlLayout(name, type)
			{
				m_Controls = controlLayouts.ToArray(),
				m_StateFormat = stateFormat,
				m_Variants = variants,
				m_UpdateBeforeRender = ((layoutAttribute != null) ? layoutAttribute.updateBeforeRenderInternal : null),
				isGenericTypeOfDevice = (layoutAttribute != null && layoutAttribute.isGenericTypeOfDevice),
				hideInUI = (layoutAttribute != null && layoutAttribute.hideInUI),
				m_Description = ((layoutAttribute != null) ? layoutAttribute.description : null),
				m_DisplayName = ((layoutAttribute != null) ? layoutAttribute.displayName : null),
				canRunInBackground = ((layoutAttribute != null) ? layoutAttribute.canRunInBackgroundInternal : null),
				isNoisy = (layoutAttribute != null && layoutAttribute.isNoisy)
			};
			if (((layoutAttribute != null) ? layoutAttribute.commonUsages : null) != null)
			{
				layout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(layoutAttribute.commonUsages, (string x) => new InternedString(x));
			}
			return layout;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000558AD File Offset: 0x00053AAD
		public string ToJson()
		{
			return JsonUtility.ToJson(InputControlLayout.LayoutJson.FromLayout(this), true);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000558C0 File Offset: 0x00053AC0
		public static InputControlLayout FromJson(string json)
		{
			return JsonUtility.FromJson<InputControlLayout.LayoutJson>(json).ToLayout();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000558DB File Offset: 0x00053ADB
		private InputControlLayout(string name, Type type)
		{
			this.m_Name = new InternedString(name);
			this.m_Type = type;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000558F6 File Offset: 0x00053AF6
		private static void AddControlItems(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			InputControlLayout.AddControlItemsFromFields(type, controlLayouts, layoutName);
			InputControlLayout.AddControlItemsFromProperties(type, controlLayouts, layoutName);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00055908 File Offset: 0x00053B08
		private static void AddControlItemsFromFields(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(fields, controlLayouts, layoutName);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00055928 File Offset: 0x00053B28
		private static void AddControlItemsFromProperties(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(properties, controlLayouts, layoutName);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00055948 File Offset: 0x00053B48
		private static void AddControlItemsFromMembers(MemberInfo[] members, List<InputControlLayout.ControlItem> controlItems, string layoutName)
		{
			foreach (MemberInfo member in members)
			{
				if (!(member.DeclaringType == typeof(InputControl)))
				{
					Type valueType = TypeHelpers.GetValueType(member);
					if (valueType != null && valueType.IsValueType && typeof(IInputStateTypeInfo).IsAssignableFrom(valueType))
					{
						int controlCountBefore = controlItems.Count;
						InputControlLayout.AddControlItems(valueType, controlItems, layoutName);
						if (member as FieldInfo != null)
						{
							int fieldOffset = Marshal.OffsetOf(member.DeclaringType, member.Name).ToInt32();
							int controlCountAfter = controlItems.Count;
							for (int i = controlCountBefore; i < controlCountAfter; i++)
							{
								InputControlLayout.ControlItem controlLayout = controlItems[i];
								if (controlItems[i].offset != 4294967295U)
								{
									controlLayout.offset += (uint)fieldOffset;
									controlItems[i] = controlLayout;
								}
							}
						}
					}
					InputControlAttribute[] attributes = member.GetCustomAttributes(false).ToArray<InputControlAttribute>();
					if (attributes.Length != 0 || (!(valueType == null) && typeof(InputControl).IsAssignableFrom(valueType) && !(member is PropertyInfo)))
					{
						InputControlLayout.AddControlItemsFromMember(member, attributes, controlItems);
					}
				}
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00055A88 File Offset: 0x00053C88
		private static void AddControlItemsFromMember(MemberInfo member, InputControlAttribute[] attributes, List<InputControlLayout.ControlItem> controlItems)
		{
			if (attributes.Length == 0)
			{
				InputControlLayout.ControlItem controlItem = InputControlLayout.CreateControlItemFromMember(member, null);
				controlItems.Add(controlItem);
				return;
			}
			foreach (InputControlAttribute attribute in attributes)
			{
				InputControlLayout.ControlItem controlItem2 = InputControlLayout.CreateControlItemFromMember(member, attribute);
				controlItems.Add(controlItem2);
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00055AD0 File Offset: 0x00053CD0
		private static InputControlLayout.ControlItem CreateControlItemFromMember(MemberInfo member, InputControlAttribute attribute)
		{
			string name = ((attribute != null) ? attribute.name : null);
			if (string.IsNullOrEmpty(name))
			{
				name = member.Name;
			}
			bool isModifyingChildControlByPath = name.IndexOf('/') != -1;
			string displayName = ((attribute != null) ? attribute.displayName : null);
			string shortDisplayName = ((attribute != null) ? attribute.shortDisplayName : null);
			string layout = ((attribute != null) ? attribute.layout : null);
			if (string.IsNullOrEmpty(layout) && !isModifyingChildControlByPath && (!(member is FieldInfo) || member.GetCustomAttribute(false) == null))
			{
				layout = InputControlLayout.InferLayoutFromValueType(TypeHelpers.GetValueType(member));
			}
			string variants = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.variants))
			{
				variants = attribute.variants;
			}
			uint offset = uint.MaxValue;
			if (attribute != null && attribute.offset != 4294967295U)
			{
				offset = attribute.offset;
			}
			else if (member is FieldInfo && !isModifyingChildControlByPath)
			{
				offset = (uint)Marshal.OffsetOf(member.DeclaringType, member.Name).ToInt32();
			}
			uint bit = uint.MaxValue;
			if (attribute != null)
			{
				bit = attribute.bit;
			}
			uint sizeInBits = 0U;
			if (attribute != null)
			{
				sizeInBits = attribute.sizeInBits;
			}
			FourCC format = default(FourCC);
			if (attribute != null && !string.IsNullOrEmpty(attribute.format))
			{
				format = new FourCC(attribute.format);
			}
			else if (!isModifyingChildControlByPath && bit == 4294967295U)
			{
				format = InputStateBlock.GetPrimitiveFormatFromType(TypeHelpers.GetValueType(member));
			}
			InternedString[] aliases = null;
			if (attribute != null)
			{
				string[] joined = ArrayHelpers.Join<string>(attribute.alias, attribute.aliases);
				if (joined != null)
				{
					aliases = joined.Select((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			InternedString[] usages = null;
			if (attribute != null)
			{
				string[] joined2 = ArrayHelpers.Join<string>(attribute.usage, attribute.usages);
				if (joined2 != null)
				{
					usages = joined2.Select((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			NamedValue[] parameters = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.parameters))
			{
				parameters = NamedValue.ParseMultiple(attribute.parameters);
			}
			NameAndParameters[] processors = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.processors))
			{
				processors = NameAndParameters.ParseMultiple(attribute.processors).ToArray<NameAndParameters>();
			}
			string useStateFrom = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.useStateFrom))
			{
				useStateFrom = attribute.useStateFrom;
			}
			bool isNoisy = false;
			if (attribute != null)
			{
				isNoisy = attribute.noisy;
			}
			bool dontReset = false;
			if (attribute != null)
			{
				dontReset = attribute.dontReset;
			}
			bool isSynthetic = false;
			if (attribute != null)
			{
				isSynthetic = attribute.synthetic;
			}
			int arraySize = 0;
			if (attribute != null)
			{
				arraySize = attribute.arraySize;
			}
			PrimitiveValue defaultState = default(PrimitiveValue);
			if (attribute != null)
			{
				defaultState = PrimitiveValue.FromObject(attribute.defaultState);
			}
			PrimitiveValue minValue = default(PrimitiveValue);
			PrimitiveValue maxValue = default(PrimitiveValue);
			if (attribute != null)
			{
				minValue = PrimitiveValue.FromObject(attribute.minValue);
				maxValue = PrimitiveValue.FromObject(attribute.maxValue);
			}
			return new InputControlLayout.ControlItem
			{
				name = new InternedString(name),
				displayName = displayName,
				shortDisplayName = shortDisplayName,
				layout = new InternedString(layout),
				variants = new InternedString(variants),
				useStateFrom = useStateFrom,
				format = format,
				offset = offset,
				bit = bit,
				sizeInBits = sizeInBits,
				parameters = new ReadOnlyArray<NamedValue>(parameters),
				processors = new ReadOnlyArray<NameAndParameters>(processors),
				usages = new ReadOnlyArray<InternedString>(usages),
				aliases = new ReadOnlyArray<InternedString>(aliases),
				isModifyingExistingControl = isModifyingChildControlByPath,
				isFirstDefinedInThisLayout = true,
				isNoisy = isNoisy,
				dontReset = dontReset,
				isSynthetic = isSynthetic,
				arraySize = arraySize,
				defaultState = defaultState,
				minValue = minValue,
				maxValue = maxValue
			};
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00055E78 File Offset: 0x00054078
		private static string InferLayoutFromValueType(Type type)
		{
			InternedString layout = InputControlLayout.s_Layouts.TryFindLayoutForType(type);
			if (layout.IsEmpty())
			{
				InternedString typeName = new InternedString(type.Name);
				if (InputControlLayout.s_Layouts.HasLayout(typeName))
				{
					layout = typeName;
				}
				else if (type.Name.EndsWith("Control"))
				{
					typeName = new InternedString(type.Name.Substring(0, type.Name.Length - "Control".Length));
					if (InputControlLayout.s_Layouts.HasLayout(typeName))
					{
						layout = typeName;
					}
				}
			}
			return layout;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00055F08 File Offset: 0x00054108
		public void MergeLayout(InputControlLayout other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			bool? updateBeforeRender = this.m_UpdateBeforeRender;
			this.m_UpdateBeforeRender = ((updateBeforeRender != null) ? updateBeforeRender : other.m_UpdateBeforeRender);
			if (this.m_Variants.IsEmpty())
			{
				this.m_Variants = other.m_Variants;
			}
			if (this.m_Type == null)
			{
				this.m_Type = other.m_Type;
			}
			else if (this.m_Type.IsAssignableFrom(other.m_Type))
			{
				this.m_Type = other.m_Type;
			}
			bool layoutIsTargetingSpecificVariants = !this.m_Variants.IsEmpty();
			if (this.m_StateFormat == default(FourCC))
			{
				this.m_StateFormat = other.m_StateFormat;
			}
			this.m_CommonUsages = ArrayHelpers.Merge<InternedString>(other.m_CommonUsages, this.m_CommonUsages);
			this.m_AppliedOverrides.Merge(other.m_AppliedOverrides);
			if (string.IsNullOrEmpty(this.m_DisplayName))
			{
				this.m_DisplayName = other.m_DisplayName;
			}
			if (this.m_Controls == null)
			{
				this.m_Controls = other.m_Controls;
				return;
			}
			if (other.m_Controls != null)
			{
				InputControlLayout.ControlItem[] controls2 = other.m_Controls;
				List<InputControlLayout.ControlItem> controls = new List<InputControlLayout.ControlItem>();
				List<string> baseControlVariants = new List<string>();
				Dictionary<string, InputControlLayout.ControlItem> baseControlTable = InputControlLayout.CreateLookupTableForControls(controls2, baseControlVariants);
				foreach (KeyValuePair<string, InputControlLayout.ControlItem> pair in InputControlLayout.CreateLookupTableForControls(this.m_Controls, null))
				{
					InputControlLayout.ControlItem baseControlItem;
					if (baseControlTable.TryGetValue(pair.Key, out baseControlItem))
					{
						InputControlLayout.ControlItem mergedLayout = pair.Value.Merge(baseControlItem);
						controls.Add(mergedLayout);
						baseControlTable.Remove(pair.Key);
					}
					else if (pair.Value.variants.IsEmpty() || pair.Value.variants == InputControlLayout.DefaultVariant)
					{
						bool isTargetingVariants = false;
						if (layoutIsTargetingSpecificVariants)
						{
							for (int i = 0; i < baseControlVariants.Count; i++)
							{
								if (InputControlLayout.VariantsMatch(this.m_Variants.ToLower(), baseControlVariants[i]))
								{
									string key = pair.Key + "@" + baseControlVariants[i];
									if (baseControlTable.TryGetValue(key, out baseControlItem))
									{
										InputControlLayout.ControlItem mergedLayout2 = pair.Value.Merge(baseControlItem);
										controls.Add(mergedLayout2);
										baseControlTable.Remove(key);
										isTargetingVariants = true;
									}
								}
							}
						}
						else
						{
							foreach (string variant in baseControlVariants)
							{
								string key2 = pair.Key + "@" + variant;
								if (baseControlTable.TryGetValue(key2, out baseControlItem))
								{
									InputControlLayout.ControlItem mergedLayout3 = pair.Value.Merge(baseControlItem);
									controls.Add(mergedLayout3);
									baseControlTable.Remove(key2);
									isTargetingVariants = true;
								}
							}
						}
						if (!isTargetingVariants)
						{
							controls.Add(pair.Value);
						}
					}
					else if (baseControlTable.TryGetValue(pair.Value.name.ToLower(), out baseControlItem))
					{
						InputControlLayout.ControlItem mergedLayout4 = pair.Value.Merge(baseControlItem);
						controls.Add(mergedLayout4);
						baseControlTable.Remove(pair.Value.name.ToLower());
					}
					else if (InputControlLayout.VariantsMatch(this.m_Variants, pair.Value.variants))
					{
						controls.Add(pair.Value);
					}
				}
				if (!layoutIsTargetingSpecificVariants)
				{
					int count = controls.Count;
					controls.AddRange(baseControlTable.Values);
					for (int j = count; j < controls.Count; j++)
					{
						InputControlLayout.ControlItem control = controls[j];
						control.isFirstDefinedInThisLayout = false;
						controls[j] = control;
					}
				}
				else
				{
					int count2 = controls.Count;
					controls.AddRange(baseControlTable.Values.Where((InputControlLayout.ControlItem x) => InputControlLayout.VariantsMatch(this.m_Variants, x.variants)));
					for (int k = count2; k < controls.Count; k++)
					{
						InputControlLayout.ControlItem control2 = controls[k];
						control2.isFirstDefinedInThisLayout = false;
						controls[k] = control2;
					}
				}
				this.m_Controls = controls.ToArray();
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00056388 File Offset: 0x00054588
		private static Dictionary<string, InputControlLayout.ControlItem> CreateLookupTableForControls(InputControlLayout.ControlItem[] controlItems, List<string> variants = null)
		{
			Dictionary<string, InputControlLayout.ControlItem> table = new Dictionary<string, InputControlLayout.ControlItem>();
			int i = 0;
			while (i < controlItems.Length)
			{
				string key = controlItems[i].name.ToLower();
				InternedString itemVariants = controlItems[i].variants;
				if (itemVariants.IsEmpty() || !(itemVariants != InputControlLayout.DefaultVariant))
				{
					goto IL_00EC;
				}
				if (itemVariants.ToString().IndexOf(";"[0]) != -1)
				{
					foreach (string name in itemVariants.ToLower().Split(";"[0], StringSplitOptions.None))
					{
						if (variants != null)
						{
							variants.Add(name);
						}
						key = key + "@" + name;
						table[key] = controlItems[i];
					}
				}
				else
				{
					key = key + "@" + itemVariants.ToLower();
					if (variants != null)
					{
						variants.Add(itemVariants.ToLower());
						goto IL_00EC;
					}
					goto IL_00EC;
				}
				IL_00FA:
				i++;
				continue;
				IL_00EC:
				table[key] = controlItems[i];
				goto IL_00FA;
			}
			return table;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0005649D File Offset: 0x0005469D
		internal static bool VariantsMatch(InternedString expected, InternedString actual)
		{
			return InputControlLayout.VariantsMatch(expected.ToLower(), actual.ToLower());
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000564B2 File Offset: 0x000546B2
		internal static bool VariantsMatch(string expected, string actual)
		{
			return (actual != null && StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(InputControlLayout.DefaultVariant, actual, ";"[0])) || expected == null || actual == null || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(expected, actual, ";"[0]);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000564F4 File Offset: 0x000546F4
		internal static void ParseHeaderFieldsFromJson(string json, out InternedString name, out InlinedArray<InternedString> baseLayouts, out InputDeviceMatcher deviceMatcher)
		{
			InputControlLayout.LayoutJsonNameAndDescriptorOnly header = JsonUtility.FromJson<InputControlLayout.LayoutJsonNameAndDescriptorOnly>(json);
			name = new InternedString(header.name);
			baseLayouts = default(InlinedArray<InternedString>);
			if (!string.IsNullOrEmpty(header.extend))
			{
				baseLayouts.Append(new InternedString(header.extend));
			}
			if (header.extendMultiple != null)
			{
				foreach (string item in header.extendMultiple)
				{
					baseLayouts.Append(new InternedString(item));
				}
			}
			deviceMatcher = header.device.ToMatcher();
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0005657F File Offset: 0x0005477F
		internal static ref InputControlLayout.Cache cache
		{
			get
			{
				return ref InputControlLayout.s_CacheInstance;
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00056588 File Offset: 0x00054788
		internal static InputControlLayout.CacheRefInstance CacheRef()
		{
			InputControlLayout.s_CacheInstanceRef++;
			return new InputControlLayout.CacheRefInstance
			{
				valid = true
			};
		}

		// Token: 0x04000AF7 RID: 2807
		private static InternedString s_DefaultVariant = new InternedString("Default");

		// Token: 0x04000AF8 RID: 2808
		public const string VariantSeparator = ";";

		// Token: 0x04000AF9 RID: 2809
		private InternedString m_Name;

		// Token: 0x04000AFA RID: 2810
		private Type m_Type;

		// Token: 0x04000AFB RID: 2811
		private InternedString m_Variants;

		// Token: 0x04000AFC RID: 2812
		private FourCC m_StateFormat;

		// Token: 0x04000AFD RID: 2813
		internal int m_StateSizeInBytes;

		// Token: 0x04000AFE RID: 2814
		internal bool? m_UpdateBeforeRender;

		// Token: 0x04000AFF RID: 2815
		internal InlinedArray<InternedString> m_BaseLayouts;

		// Token: 0x04000B00 RID: 2816
		private InlinedArray<InternedString> m_AppliedOverrides;

		// Token: 0x04000B01 RID: 2817
		private InternedString[] m_CommonUsages;

		// Token: 0x04000B02 RID: 2818
		internal InputControlLayout.ControlItem[] m_Controls;

		// Token: 0x04000B03 RID: 2819
		internal string m_DisplayName;

		// Token: 0x04000B04 RID: 2820
		private string m_Description;

		// Token: 0x04000B05 RID: 2821
		private InputControlLayout.Flags m_Flags;

		// Token: 0x04000B06 RID: 2822
		internal static InputControlLayout.Collection s_Layouts;

		// Token: 0x04000B07 RID: 2823
		internal static InputControlLayout.Cache s_CacheInstance;

		// Token: 0x04000B08 RID: 2824
		internal static int s_CacheInstanceRef;

		// Token: 0x020001F6 RID: 502
		public struct ControlItem
		{
			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000565D7 File Offset: 0x000547D7
			// (set) Token: 0x060012A2 RID: 4770 RVA: 0x000565DF File Offset: 0x000547DF
			public InternedString name { readonly get; internal set; }

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060012A3 RID: 4771 RVA: 0x000565E8 File Offset: 0x000547E8
			// (set) Token: 0x060012A4 RID: 4772 RVA: 0x000565F0 File Offset: 0x000547F0
			public InternedString layout { readonly get; internal set; }

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060012A5 RID: 4773 RVA: 0x000565F9 File Offset: 0x000547F9
			// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00056601 File Offset: 0x00054801
			public InternedString variants { readonly get; internal set; }

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0005660A File Offset: 0x0005480A
			// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00056612 File Offset: 0x00054812
			public string useStateFrom { readonly get; internal set; }

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0005661B File Offset: 0x0005481B
			// (set) Token: 0x060012AA RID: 4778 RVA: 0x00056623 File Offset: 0x00054823
			public string displayName { readonly get; internal set; }

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x060012AB RID: 4779 RVA: 0x0005662C File Offset: 0x0005482C
			// (set) Token: 0x060012AC RID: 4780 RVA: 0x00056634 File Offset: 0x00054834
			public string shortDisplayName { readonly get; internal set; }

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x060012AD RID: 4781 RVA: 0x0005663D File Offset: 0x0005483D
			// (set) Token: 0x060012AE RID: 4782 RVA: 0x00056645 File Offset: 0x00054845
			public ReadOnlyArray<InternedString> usages { readonly get; internal set; }

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x060012AF RID: 4783 RVA: 0x0005664E File Offset: 0x0005484E
			// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00056656 File Offset: 0x00054856
			public ReadOnlyArray<InternedString> aliases { readonly get; internal set; }

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0005665F File Offset: 0x0005485F
			// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00056667 File Offset: 0x00054867
			public ReadOnlyArray<NamedValue> parameters { readonly get; internal set; }

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00056670 File Offset: 0x00054870
			// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00056678 File Offset: 0x00054878
			public ReadOnlyArray<NameAndParameters> processors { readonly get; internal set; }

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00056681 File Offset: 0x00054881
			// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00056689 File Offset: 0x00054889
			public uint offset { readonly get; internal set; }

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00056692 File Offset: 0x00054892
			// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0005669A File Offset: 0x0005489A
			public uint bit { readonly get; internal set; }

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000566A3 File Offset: 0x000548A3
			// (set) Token: 0x060012BA RID: 4794 RVA: 0x000566AB File Offset: 0x000548AB
			public uint sizeInBits { readonly get; internal set; }

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x060012BB RID: 4795 RVA: 0x000566B4 File Offset: 0x000548B4
			// (set) Token: 0x060012BC RID: 4796 RVA: 0x000566BC File Offset: 0x000548BC
			public FourCC format { readonly get; internal set; }

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x000566C5 File Offset: 0x000548C5
			// (set) Token: 0x060012BE RID: 4798 RVA: 0x000566CD File Offset: 0x000548CD
			private InputControlLayout.ControlItem.Flags flags { readonly get; set; }

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060012BF RID: 4799 RVA: 0x000566D6 File Offset: 0x000548D6
			// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000566DE File Offset: 0x000548DE
			public int arraySize { readonly get; internal set; }

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000566E7 File Offset: 0x000548E7
			// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000566EF File Offset: 0x000548EF
			public PrimitiveValue defaultState { readonly get; internal set; }

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000566F8 File Offset: 0x000548F8
			// (set) Token: 0x060012C4 RID: 4804 RVA: 0x00056700 File Offset: 0x00054900
			public PrimitiveValue minValue { readonly get; internal set; }

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00056709 File Offset: 0x00054909
			// (set) Token: 0x060012C6 RID: 4806 RVA: 0x00056711 File Offset: 0x00054911
			public PrimitiveValue maxValue { readonly get; internal set; }

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0005671A File Offset: 0x0005491A
			// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00056727 File Offset: 0x00054927
			public bool isModifyingExistingControl
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.isModifyingExistingControl) == InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0005674A File Offset: 0x0005494A
			// (set) Token: 0x060012CA RID: 4810 RVA: 0x00056757 File Offset: 0x00054957
			public bool isNoisy
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsNoisy) == InputControlLayout.ControlItem.Flags.IsNoisy;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsNoisy;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsNoisy;
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x060012CB RID: 4811 RVA: 0x0005677A File Offset: 0x0005497A
			// (set) Token: 0x060012CC RID: 4812 RVA: 0x00056787 File Offset: 0x00054987
			public bool isSynthetic
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsSynthetic) == InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsSynthetic;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x060012CD RID: 4813 RVA: 0x000567AA File Offset: 0x000549AA
			// (set) Token: 0x060012CE RID: 4814 RVA: 0x000567B9 File Offset: 0x000549B9
			public bool dontReset
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.DontReset) == InputControlLayout.ControlItem.Flags.DontReset;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.DontReset;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.DontReset;
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x060012CF RID: 4815 RVA: 0x000567DD File Offset: 0x000549DD
			// (set) Token: 0x060012D0 RID: 4816 RVA: 0x000567EA File Offset: 0x000549EA
			public bool isFirstDefinedInThisLayout
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout) > (InputControlLayout.ControlItem.Flags)0;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0005680D File Offset: 0x00054A0D
			public bool isArray
			{
				get
				{
					return this.arraySize != 0;
				}
			}

			// Token: 0x060012D2 RID: 4818 RVA: 0x00056818 File Offset: 0x00054A18
			public InputControlLayout.ControlItem Merge(InputControlLayout.ControlItem other)
			{
				InputControlLayout.ControlItem result = default(InputControlLayout.ControlItem);
				result.name = this.name;
				result.isModifyingExistingControl = this.isModifyingExistingControl;
				result.displayName = (string.IsNullOrEmpty(this.displayName) ? other.displayName : this.displayName);
				result.shortDisplayName = (string.IsNullOrEmpty(this.shortDisplayName) ? other.shortDisplayName : this.shortDisplayName);
				result.layout = (this.layout.IsEmpty() ? other.layout : this.layout);
				result.variants = (this.variants.IsEmpty() ? other.variants : this.variants);
				result.useStateFrom = this.useStateFrom ?? other.useStateFrom;
				result.arraySize = ((!this.isArray) ? other.arraySize : this.arraySize);
				result.isNoisy = this.isNoisy || other.isNoisy;
				result.dontReset = this.dontReset || other.dontReset;
				result.isSynthetic = this.isSynthetic || other.isSynthetic;
				result.isFirstDefinedInThisLayout = false;
				if (this.offset != 4294967295U)
				{
					result.offset = this.offset;
				}
				else
				{
					result.offset = other.offset;
				}
				if (this.bit != 4294967295U)
				{
					result.bit = this.bit;
				}
				else
				{
					result.bit = other.bit;
				}
				if (this.format != 0)
				{
					result.format = this.format;
				}
				else
				{
					result.format = other.format;
				}
				if (this.sizeInBits != 0U)
				{
					result.sizeInBits = this.sizeInBits;
				}
				else
				{
					result.sizeInBits = other.sizeInBits;
				}
				if (this.aliases.Count > 0)
				{
					result.aliases = this.aliases;
				}
				else
				{
					result.aliases = other.aliases;
				}
				if (this.usages.Count > 0)
				{
					result.usages = this.usages;
				}
				else
				{
					result.usages = other.usages;
				}
				if (this.parameters.Count == 0)
				{
					result.parameters = other.parameters;
				}
				else
				{
					result.parameters = this.parameters;
				}
				if (this.processors.Count == 0)
				{
					result.processors = other.processors;
				}
				else
				{
					result.processors = this.processors;
				}
				if (!string.IsNullOrEmpty(this.displayName))
				{
					result.displayName = this.displayName;
				}
				else
				{
					result.displayName = other.displayName;
				}
				if (!this.defaultState.isEmpty)
				{
					result.defaultState = this.defaultState;
				}
				else
				{
					result.defaultState = other.defaultState;
				}
				if (!this.minValue.isEmpty)
				{
					result.minValue = this.minValue;
				}
				else
				{
					result.minValue = other.minValue;
				}
				if (!this.maxValue.isEmpty)
				{
					result.maxValue = this.maxValue;
				}
				else
				{
					result.maxValue = other.maxValue;
				}
				return result;
			}

			// Token: 0x020001F7 RID: 503
			[Flags]
			private enum Flags
			{
				// Token: 0x04000B1D RID: 2845
				isModifyingExistingControl = 1,
				// Token: 0x04000B1E RID: 2846
				IsNoisy = 2,
				// Token: 0x04000B1F RID: 2847
				IsSynthetic = 4,
				// Token: 0x04000B20 RID: 2848
				IsFirstDefinedInThisLayout = 8,
				// Token: 0x04000B21 RID: 2849
				DontReset = 16
			}
		}

		// Token: 0x020001F8 RID: 504
		public class Builder
		{
			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00056B6F File Offset: 0x00054D6F
			// (set) Token: 0x060012D4 RID: 4820 RVA: 0x00056B77 File Offset: 0x00054D77
			public string name { get; set; }

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00056B80 File Offset: 0x00054D80
			// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00056B88 File Offset: 0x00054D88
			public string displayName { get; set; }

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00056B91 File Offset: 0x00054D91
			// (set) Token: 0x060012D8 RID: 4824 RVA: 0x00056B99 File Offset: 0x00054D99
			public Type type { get; set; }

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00056BA2 File Offset: 0x00054DA2
			// (set) Token: 0x060012DA RID: 4826 RVA: 0x00056BAA File Offset: 0x00054DAA
			public FourCC stateFormat { get; set; }

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x00056BB3 File Offset: 0x00054DB3
			// (set) Token: 0x060012DC RID: 4828 RVA: 0x00056BBB File Offset: 0x00054DBB
			public int stateSizeInBytes { get; set; }

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060012DD RID: 4829 RVA: 0x00056BC4 File Offset: 0x00054DC4
			// (set) Token: 0x060012DE RID: 4830 RVA: 0x00056BCC File Offset: 0x00054DCC
			public string extendsLayout
			{
				get
				{
					return this.m_ExtendsLayout;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						this.m_ExtendsLayout = value;
						return;
					}
					this.m_ExtendsLayout = null;
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060012DF RID: 4831 RVA: 0x00056BE5 File Offset: 0x00054DE5
			// (set) Token: 0x060012E0 RID: 4832 RVA: 0x00056BED File Offset: 0x00054DED
			public bool? updateBeforeRender { get; set; }

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00056BF6 File Offset: 0x00054DF6
			public ReadOnlyArray<InputControlLayout.ControlItem> controls
			{
				get
				{
					return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls, 0, this.m_ControlCount);
				}
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x00056C0C File Offset: 0x00054E0C
			public InputControlLayout.Builder.ControlBuilder AddControl(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentException(name);
				}
				int index = ArrayHelpers.AppendWithCapacity<InputControlLayout.ControlItem>(ref this.m_Controls, ref this.m_ControlCount, new InputControlLayout.ControlItem
				{
					name = new InternedString(name),
					isModifyingExistingControl = (name.IndexOf('/') != -1),
					offset = uint.MaxValue,
					bit = uint.MaxValue
				}, 10);
				return new InputControlLayout.Builder.ControlBuilder
				{
					builder = this,
					index = index
				};
			}

			// Token: 0x060012E3 RID: 4835 RVA: 0x00056C90 File Offset: 0x00054E90
			public InputControlLayout.Builder WithName(string name)
			{
				this.name = name;
				return this;
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x00056C9A File Offset: 0x00054E9A
			public InputControlLayout.Builder WithDisplayName(string displayName)
			{
				this.displayName = displayName;
				return this;
			}

			// Token: 0x060012E5 RID: 4837 RVA: 0x00056CA4 File Offset: 0x00054EA4
			public InputControlLayout.Builder WithType<T>() where T : InputControl
			{
				this.type = typeof(T);
				return this;
			}

			// Token: 0x060012E6 RID: 4838 RVA: 0x00056CB7 File Offset: 0x00054EB7
			public InputControlLayout.Builder WithFormat(FourCC format)
			{
				this.stateFormat = format;
				return this;
			}

			// Token: 0x060012E7 RID: 4839 RVA: 0x00056CC1 File Offset: 0x00054EC1
			public InputControlLayout.Builder WithFormat(string format)
			{
				return this.WithFormat(new FourCC(format));
			}

			// Token: 0x060012E8 RID: 4840 RVA: 0x00056CCF File Offset: 0x00054ECF
			public InputControlLayout.Builder WithSizeInBytes(int sizeInBytes)
			{
				this.stateSizeInBytes = sizeInBytes;
				return this;
			}

			// Token: 0x060012E9 RID: 4841 RVA: 0x00056CD9 File Offset: 0x00054ED9
			public InputControlLayout.Builder Extend(string baseLayoutName)
			{
				this.extendsLayout = baseLayoutName;
				return this;
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x00056CE4 File Offset: 0x00054EE4
			public InputControlLayout Build()
			{
				InputControlLayout.ControlItem[] controls = null;
				if (this.m_ControlCount > 0)
				{
					controls = new InputControlLayout.ControlItem[this.m_ControlCount];
					Array.Copy(this.m_Controls, controls, this.m_ControlCount);
				}
				return new InputControlLayout(new InternedString(this.name), (this.type == null && string.IsNullOrEmpty(this.extendsLayout)) ? typeof(InputDevice) : this.type)
				{
					m_DisplayName = this.displayName,
					m_StateFormat = this.stateFormat,
					m_StateSizeInBytes = this.stateSizeInBytes,
					m_BaseLayouts = ((!string.IsNullOrEmpty(this.extendsLayout)) ? new InlinedArray<InternedString>(new InternedString(this.extendsLayout)) : default(InlinedArray<InternedString>)),
					m_Controls = controls,
					m_UpdateBeforeRender = this.updateBeforeRender
				};
			}

			// Token: 0x04000B27 RID: 2855
			private string m_ExtendsLayout;

			// Token: 0x04000B29 RID: 2857
			private int m_ControlCount;

			// Token: 0x04000B2A RID: 2858
			private InputControlLayout.ControlItem[] m_Controls;

			// Token: 0x020001F9 RID: 505
			public struct ControlBuilder
			{
				// Token: 0x060012EC RID: 4844 RVA: 0x00056DC1 File Offset: 0x00054FC1
				public InputControlLayout.Builder.ControlBuilder WithDisplayName(string displayName)
				{
					this.builder.m_Controls[this.index].displayName = displayName;
					return this;
				}

				// Token: 0x060012ED RID: 4845 RVA: 0x00056DE8 File Offset: 0x00054FE8
				public InputControlLayout.Builder.ControlBuilder WithLayout(string layout)
				{
					if (string.IsNullOrEmpty(layout))
					{
						throw new ArgumentException("Layout name cannot be null or empty", "layout");
					}
					this.builder.m_Controls[this.index].layout = new InternedString(layout);
					return this;
				}

				// Token: 0x060012EE RID: 4846 RVA: 0x00056E34 File Offset: 0x00055034
				public InputControlLayout.Builder.ControlBuilder WithFormat(FourCC format)
				{
					this.builder.m_Controls[this.index].format = format;
					return this;
				}

				// Token: 0x060012EF RID: 4847 RVA: 0x00056E58 File Offset: 0x00055058
				public InputControlLayout.Builder.ControlBuilder WithFormat(string format)
				{
					return this.WithFormat(new FourCC(format));
				}

				// Token: 0x060012F0 RID: 4848 RVA: 0x00056E66 File Offset: 0x00055066
				public InputControlLayout.Builder.ControlBuilder WithByteOffset(uint offset)
				{
					this.builder.m_Controls[this.index].offset = offset;
					return this;
				}

				// Token: 0x060012F1 RID: 4849 RVA: 0x00056E8A File Offset: 0x0005508A
				public InputControlLayout.Builder.ControlBuilder WithBitOffset(uint bit)
				{
					this.builder.m_Controls[this.index].bit = bit;
					return this;
				}

				// Token: 0x060012F2 RID: 4850 RVA: 0x00056EAE File Offset: 0x000550AE
				public InputControlLayout.Builder.ControlBuilder IsSynthetic(bool value)
				{
					this.builder.m_Controls[this.index].isSynthetic = value;
					return this;
				}

				// Token: 0x060012F3 RID: 4851 RVA: 0x00056ED2 File Offset: 0x000550D2
				public InputControlLayout.Builder.ControlBuilder IsNoisy(bool value)
				{
					this.builder.m_Controls[this.index].isNoisy = value;
					return this;
				}

				// Token: 0x060012F4 RID: 4852 RVA: 0x00056EF6 File Offset: 0x000550F6
				public InputControlLayout.Builder.ControlBuilder DontReset(bool value)
				{
					this.builder.m_Controls[this.index].dontReset = value;
					return this;
				}

				// Token: 0x060012F5 RID: 4853 RVA: 0x00056F1A File Offset: 0x0005511A
				public InputControlLayout.Builder.ControlBuilder WithSizeInBits(uint sizeInBits)
				{
					this.builder.m_Controls[this.index].sizeInBits = sizeInBits;
					return this;
				}

				// Token: 0x060012F6 RID: 4854 RVA: 0x00056F40 File Offset: 0x00055140
				public InputControlLayout.Builder.ControlBuilder WithRange(float minValue, float maxValue)
				{
					this.builder.m_Controls[this.index].minValue = minValue;
					this.builder.m_Controls[this.index].maxValue = maxValue;
					return this;
				}

				// Token: 0x060012F7 RID: 4855 RVA: 0x00056F98 File Offset: 0x00055198
				public InputControlLayout.Builder.ControlBuilder WithUsages(params InternedString[] usages)
				{
					if (usages == null || usages.Length == 0)
					{
						return this;
					}
					for (int i = 0; i < usages.Length; i++)
					{
						if (usages[i].IsEmpty())
						{
							throw new ArgumentException(string.Format("Empty usage entry at index {0} for control '{1}' in layout '{2}'", i, this.builder.m_Controls[this.index].name, this.builder.name), "usages");
						}
					}
					this.builder.m_Controls[this.index].usages = new ReadOnlyArray<InternedString>(usages);
					return this;
				}

				// Token: 0x060012F8 RID: 4856 RVA: 0x0005703C File Offset: 0x0005523C
				public InputControlLayout.Builder.ControlBuilder WithUsages(IEnumerable<string> usages)
				{
					InternedString[] usagesArray = usages.Select((string x) => new InternedString(x)).ToArray<InternedString>();
					return this.WithUsages(usagesArray);
				}

				// Token: 0x060012F9 RID: 4857 RVA: 0x0005707B File Offset: 0x0005527B
				public InputControlLayout.Builder.ControlBuilder WithUsages(params string[] usages)
				{
					return this.WithUsages(usages);
				}

				// Token: 0x060012FA RID: 4858 RVA: 0x00057084 File Offset: 0x00055284
				public InputControlLayout.Builder.ControlBuilder WithParameters(string parameters)
				{
					if (string.IsNullOrEmpty(parameters))
					{
						return this;
					}
					NamedValue[] parsed = NamedValue.ParseMultiple(parameters);
					this.builder.m_Controls[this.index].parameters = new ReadOnlyArray<NamedValue>(parsed);
					return this;
				}

				// Token: 0x060012FB RID: 4859 RVA: 0x000570D0 File Offset: 0x000552D0
				public InputControlLayout.Builder.ControlBuilder WithProcessors(string processors)
				{
					if (string.IsNullOrEmpty(processors))
					{
						return this;
					}
					NameAndParameters[] parsed = NameAndParameters.ParseMultiple(processors).ToArray<NameAndParameters>();
					this.builder.m_Controls[this.index].processors = new ReadOnlyArray<NameAndParameters>(parsed);
					return this;
				}

				// Token: 0x060012FC RID: 4860 RVA: 0x0005711F File Offset: 0x0005531F
				public InputControlLayout.Builder.ControlBuilder WithDefaultState(PrimitiveValue value)
				{
					this.builder.m_Controls[this.index].defaultState = value;
					return this;
				}

				// Token: 0x060012FD RID: 4861 RVA: 0x00057143 File Offset: 0x00055343
				public InputControlLayout.Builder.ControlBuilder UsingStateFrom(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return this;
					}
					this.builder.m_Controls[this.index].useStateFrom = path;
					return this;
				}

				// Token: 0x060012FE RID: 4862 RVA: 0x00057176 File Offset: 0x00055376
				public InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(int arraySize)
				{
					this.builder.m_Controls[this.index].arraySize = arraySize;
					return this;
				}

				// Token: 0x04000B2B RID: 2859
				internal InputControlLayout.Builder builder;

				// Token: 0x04000B2C RID: 2860
				internal int index;
			}
		}

		// Token: 0x020001FB RID: 507
		[Flags]
		private enum Flags
		{
			// Token: 0x04000B30 RID: 2864
			IsGenericTypeOfDevice = 1,
			// Token: 0x04000B31 RID: 2865
			HideInUI = 2,
			// Token: 0x04000B32 RID: 2866
			IsOverride = 4,
			// Token: 0x04000B33 RID: 2867
			CanRunInBackground = 8,
			// Token: 0x04000B34 RID: 2868
			CanRunInBackgroundIsSet = 16,
			// Token: 0x04000B35 RID: 2869
			IsNoisy = 32
		}

		// Token: 0x020001FC RID: 508
		[Serializable]
		internal struct LayoutJsonNameAndDescriptorOnly
		{
			// Token: 0x04000B36 RID: 2870
			public string name;

			// Token: 0x04000B37 RID: 2871
			public string extend;

			// Token: 0x04000B38 RID: 2872
			public string[] extendMultiple;

			// Token: 0x04000B39 RID: 2873
			public InputDeviceMatcher.MatcherJson device;
		}

		// Token: 0x020001FD RID: 509
		[Serializable]
		private struct LayoutJson
		{
			// Token: 0x06001302 RID: 4866 RVA: 0x000571B0 File Offset: 0x000553B0
			public InputControlLayout ToLayout()
			{
				Type type = null;
				if (!string.IsNullOrEmpty(this.type))
				{
					type = Type.GetType(this.type, false);
					if (type == null)
					{
						Debug.Log(string.Concat(new string[] { "Cannot find type '", this.type, "' used by layout '", this.name, "'; falling back to using InputDevice" }));
						type = typeof(InputDevice);
					}
					else if (!typeof(InputControl).IsAssignableFrom(type))
					{
						throw new InvalidOperationException(string.Concat(new string[] { "'", this.type, "' used by layout '", this.name, "' is not an InputControl" }));
					}
				}
				else if (string.IsNullOrEmpty(this.extend))
				{
					type = typeof(InputDevice);
				}
				InputControlLayout inputControlLayout = new InputControlLayout(this.name, type);
				inputControlLayout.m_DisplayName = this.displayName;
				inputControlLayout.m_Description = this.description;
				inputControlLayout.isGenericTypeOfDevice = this.isGenericTypeOfDevice;
				inputControlLayout.hideInUI = this.hideInUI;
				inputControlLayout.m_Variants = new InternedString(this.variant);
				inputControlLayout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(this.commonUsages, (string x) => new InternedString(x));
				InputControlLayout layout = inputControlLayout;
				if (!string.IsNullOrEmpty(this.format))
				{
					layout.m_StateFormat = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.extend))
				{
					layout.m_BaseLayouts.Append(new InternedString(this.extend));
				}
				if (this.extendMultiple != null)
				{
					foreach (string element in this.extendMultiple)
					{
						layout.m_BaseLayouts.Append(new InternedString(element));
					}
				}
				if (!string.IsNullOrEmpty(this.beforeRender))
				{
					string beforeRenderLowerCase = this.beforeRender.ToLower();
					if (beforeRenderLowerCase == "ignore")
					{
						layout.m_UpdateBeforeRender = new bool?(false);
					}
					else
					{
						if (!(beforeRenderLowerCase == "update"))
						{
							throw new InvalidOperationException("Invalid beforeRender setting '" + this.beforeRender + "' (should be 'ignore' or 'update')");
						}
						layout.m_UpdateBeforeRender = new bool?(true);
					}
				}
				if (!string.IsNullOrEmpty(this.runInBackground))
				{
					string runInBackgroundLowerCase = this.runInBackground.ToLower();
					if (runInBackgroundLowerCase == "enabled")
					{
						layout.canRunInBackground = new bool?(true);
					}
					else
					{
						if (!(runInBackgroundLowerCase == "disabled"))
						{
							throw new InvalidOperationException("Invalid runInBackground setting '" + this.beforeRender + "' (should be 'enabled' or 'disabled')");
						}
						layout.canRunInBackground = new bool?(false);
					}
				}
				if (this.controls != null)
				{
					List<InputControlLayout.ControlItem> controlLayouts = new List<InputControlLayout.ControlItem>();
					foreach (InputControlLayout.ControlItemJson controlItemJson in this.controls)
					{
						if (string.IsNullOrEmpty(controlItemJson.name))
						{
							throw new InvalidOperationException("Control with no name in layout '" + this.name);
						}
						InputControlLayout.ControlItem controlLayout = controlItemJson.ToLayout();
						controlLayouts.Add(controlLayout);
					}
					layout.m_Controls = controlLayouts.ToArray();
				}
				return layout;
			}

			// Token: 0x06001303 RID: 4867 RVA: 0x000574CC File Offset: 0x000556CC
			public static InputControlLayout.LayoutJson FromLayout(InputControlLayout layout)
			{
				InputControlLayout.LayoutJson layoutJson = default(InputControlLayout.LayoutJson);
				layoutJson.name = layout.m_Name;
				Type type = layout.type;
				layoutJson.type = ((type != null) ? type.AssemblyQualifiedName : null);
				layoutJson.variant = layout.m_Variants;
				layoutJson.displayName = layout.m_DisplayName;
				layoutJson.description = layout.m_Description;
				layoutJson.isGenericTypeOfDevice = layout.isGenericTypeOfDevice;
				layoutJson.hideInUI = layout.hideInUI;
				layoutJson.extend = ((layout.m_BaseLayouts.length == 1) ? layout.m_BaseLayouts[0].ToString() : null);
				string[] array;
				if (layout.m_BaseLayouts.length <= 1)
				{
					array = null;
				}
				else
				{
					array = layout.m_BaseLayouts.ToArray<string>((InternedString x) => x.ToString());
				}
				layoutJson.extendMultiple = array;
				layoutJson.format = layout.stateFormat.ToString();
				layoutJson.commonUsages = ArrayHelpers.Select<InternedString, string>(layout.m_CommonUsages, (InternedString x) => x.ToString());
				layoutJson.controls = InputControlLayout.ControlItemJson.FromControlItems(layout.m_Controls);
				layoutJson.beforeRender = ((layout.m_UpdateBeforeRender != null) ? (layout.m_UpdateBeforeRender.Value ? "Update" : "Ignore") : null);
				return layoutJson;
			}

			// Token: 0x04000B3A RID: 2874
			public string name;

			// Token: 0x04000B3B RID: 2875
			public string extend;

			// Token: 0x04000B3C RID: 2876
			public string[] extendMultiple;

			// Token: 0x04000B3D RID: 2877
			public string format;

			// Token: 0x04000B3E RID: 2878
			public string beforeRender;

			// Token: 0x04000B3F RID: 2879
			public string runInBackground;

			// Token: 0x04000B40 RID: 2880
			public string[] commonUsages;

			// Token: 0x04000B41 RID: 2881
			public string displayName;

			// Token: 0x04000B42 RID: 2882
			public string description;

			// Token: 0x04000B43 RID: 2883
			public string type;

			// Token: 0x04000B44 RID: 2884
			public string variant;

			// Token: 0x04000B45 RID: 2885
			public bool isGenericTypeOfDevice;

			// Token: 0x04000B46 RID: 2886
			public bool hideInUI;

			// Token: 0x04000B47 RID: 2887
			public InputControlLayout.ControlItemJson[] controls;
		}

		// Token: 0x020001FF RID: 511
		[Serializable]
		private class ControlItemJson
		{
			// Token: 0x06001309 RID: 4873 RVA: 0x00057661 File Offset: 0x00055861
			public ControlItemJson()
			{
				this.offset = uint.MaxValue;
				this.bit = uint.MaxValue;
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x00057678 File Offset: 0x00055878
			public InputControlLayout.ControlItem ToLayout()
			{
				InputControlLayout.ControlItem layout = new InputControlLayout.ControlItem
				{
					name = new InternedString(this.name),
					layout = new InternedString(this.layout),
					variants = new InternedString(this.variants),
					displayName = this.displayName,
					shortDisplayName = this.shortDisplayName,
					offset = this.offset,
					useStateFrom = this.useStateFrom,
					bit = this.bit,
					sizeInBits = this.sizeInBits,
					isModifyingExistingControl = (this.name.IndexOf('/') != -1),
					isNoisy = this.noisy,
					dontReset = this.dontReset,
					isSynthetic = this.synthetic,
					isFirstDefinedInThisLayout = true,
					arraySize = this.arraySize
				};
				if (!string.IsNullOrEmpty(this.format))
				{
					layout.format = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.usage) || this.usages != null)
				{
					List<string> usagesList = new List<string>();
					if (!string.IsNullOrEmpty(this.usage))
					{
						usagesList.Add(this.usage);
					}
					if (this.usages != null)
					{
						usagesList.AddRange(this.usages);
					}
					layout.usages = new ReadOnlyArray<InternedString>(usagesList.Select((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.alias) || this.aliases != null)
				{
					List<string> aliasesList = new List<string>();
					if (!string.IsNullOrEmpty(this.alias))
					{
						aliasesList.Add(this.alias);
					}
					if (this.aliases != null)
					{
						aliasesList.AddRange(this.aliases);
					}
					layout.aliases = new ReadOnlyArray<InternedString>(aliasesList.Select((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.parameters))
				{
					layout.parameters = new ReadOnlyArray<NamedValue>(NamedValue.ParseMultiple(this.parameters));
				}
				if (!string.IsNullOrEmpty(this.processors))
				{
					layout.processors = new ReadOnlyArray<NameAndParameters>(NameAndParameters.ParseMultiple(this.processors).ToArray<NameAndParameters>());
				}
				if (this.defaultState != null)
				{
					layout.defaultState = PrimitiveValue.FromObject(this.defaultState);
				}
				if (this.minValue != null)
				{
					layout.minValue = PrimitiveValue.FromObject(this.minValue);
				}
				if (this.maxValue != null)
				{
					layout.maxValue = PrimitiveValue.FromObject(this.maxValue);
				}
				return layout;
			}

			// Token: 0x0600130B RID: 4875 RVA: 0x00057920 File Offset: 0x00055B20
			public static InputControlLayout.ControlItemJson[] FromControlItems(InputControlLayout.ControlItem[] items)
			{
				if (items == null)
				{
					return null;
				}
				int count = items.Length;
				InputControlLayout.ControlItemJson[] result = new InputControlLayout.ControlItemJson[count];
				for (int i = 0; i < count; i++)
				{
					InputControlLayout.ControlItem item = items[i];
					InputControlLayout.ControlItemJson[] array = result;
					int num = i;
					InputControlLayout.ControlItemJson controlItemJson = new InputControlLayout.ControlItemJson();
					controlItemJson.name = item.name;
					controlItemJson.layout = item.layout;
					controlItemJson.variants = item.variants;
					controlItemJson.displayName = item.displayName;
					controlItemJson.shortDisplayName = item.shortDisplayName;
					controlItemJson.bit = item.bit;
					controlItemJson.offset = item.offset;
					controlItemJson.sizeInBits = item.sizeInBits;
					controlItemJson.format = item.format.ToString();
					controlItemJson.parameters = string.Join(",", item.parameters.Select((NamedValue x) => x.ToString()).ToArray<string>());
					controlItemJson.processors = string.Join(",", item.processors.Select((NameAndParameters x) => x.ToString()).ToArray<string>());
					controlItemJson.usages = item.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.aliases = item.aliases.Select((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.noisy = item.isNoisy;
					controlItemJson.dontReset = item.dontReset;
					controlItemJson.synthetic = item.isSynthetic;
					controlItemJson.arraySize = item.arraySize;
					controlItemJson.defaultState = item.defaultState.ToString();
					controlItemJson.minValue = item.minValue.ToString();
					controlItemJson.maxValue = item.maxValue.ToString();
					array[num] = controlItemJson;
				}
				return result;
			}

			// Token: 0x04000B4C RID: 2892
			public string name;

			// Token: 0x04000B4D RID: 2893
			public string layout;

			// Token: 0x04000B4E RID: 2894
			public string variants;

			// Token: 0x04000B4F RID: 2895
			public string usage;

			// Token: 0x04000B50 RID: 2896
			public string alias;

			// Token: 0x04000B51 RID: 2897
			public string useStateFrom;

			// Token: 0x04000B52 RID: 2898
			public uint offset;

			// Token: 0x04000B53 RID: 2899
			public uint bit;

			// Token: 0x04000B54 RID: 2900
			public uint sizeInBits;

			// Token: 0x04000B55 RID: 2901
			public string format;

			// Token: 0x04000B56 RID: 2902
			public int arraySize;

			// Token: 0x04000B57 RID: 2903
			public string[] usages;

			// Token: 0x04000B58 RID: 2904
			public string[] aliases;

			// Token: 0x04000B59 RID: 2905
			public string parameters;

			// Token: 0x04000B5A RID: 2906
			public string processors;

			// Token: 0x04000B5B RID: 2907
			public string displayName;

			// Token: 0x04000B5C RID: 2908
			public string shortDisplayName;

			// Token: 0x04000B5D RID: 2909
			public bool noisy;

			// Token: 0x04000B5E RID: 2910
			public bool dontReset;

			// Token: 0x04000B5F RID: 2911
			public bool synthetic;

			// Token: 0x04000B60 RID: 2912
			public string defaultState;

			// Token: 0x04000B61 RID: 2913
			public string minValue;

			// Token: 0x04000B62 RID: 2914
			public string maxValue;
		}

		// Token: 0x02000201 RID: 513
		internal struct Collection
		{
			// Token: 0x06001314 RID: 4884 RVA: 0x00057BA8 File Offset: 0x00055DA8
			public void Allocate()
			{
				this.layoutTypes = new Dictionary<InternedString, Type>();
				this.layoutStrings = new Dictionary<InternedString, string>();
				this.layoutBuilders = new Dictionary<InternedString, Func<InputControlLayout>>();
				this.baseLayoutTable = new Dictionary<InternedString, InternedString>();
				this.layoutOverrides = new Dictionary<InternedString, InternedString[]>();
				this.layoutOverrideNames = new HashSet<InternedString>();
				this.layoutMatchers = new List<InputControlLayout.Collection.LayoutMatcher>();
				this.precompiledLayouts = new Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout>();
			}

			// Token: 0x06001315 RID: 4885 RVA: 0x00057C10 File Offset: 0x00055E10
			public InternedString TryFindLayoutForType(Type layoutType)
			{
				foreach (KeyValuePair<InternedString, Type> entry in this.layoutTypes)
				{
					if (entry.Value == layoutType)
					{
						return entry.Key;
					}
				}
				return default(InternedString);
			}

			// Token: 0x06001316 RID: 4886 RVA: 0x00057C80 File Offset: 0x00055E80
			public InternedString TryFindMatchingLayout(InputDeviceDescription deviceDescription)
			{
				float highestScore = 0f;
				InternedString highestScoringLayout = default(InternedString);
				int layoutMatcherCount = this.layoutMatchers.Count;
				for (int i = 0; i < layoutMatcherCount; i++)
				{
					InputDeviceMatcher matcher = this.layoutMatchers[i].deviceMatcher;
					float score = matcher.MatchPercentage(deviceDescription);
					if (score > 0f && !this.layoutBuilders.ContainsKey(this.layoutMatchers[i].layoutName))
					{
						score += 1f;
					}
					if (score > highestScore)
					{
						highestScore = score;
						highestScoringLayout = this.layoutMatchers[i].layoutName;
					}
				}
				return highestScoringLayout;
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x00057D1C File Offset: 0x00055F1C
			public bool HasLayout(InternedString name)
			{
				return this.layoutTypes.ContainsKey(name) || this.layoutStrings.ContainsKey(name) || this.layoutBuilders.ContainsKey(name);
			}

			// Token: 0x06001318 RID: 4888 RVA: 0x00057D48 File Offset: 0x00055F48
			private InputControlLayout TryLoadLayoutInternal(InternedString name)
			{
				string json;
				if (this.layoutStrings.TryGetValue(name, out json))
				{
					return InputControlLayout.FromJson(json);
				}
				Type type;
				if (this.layoutTypes.TryGetValue(name, out type))
				{
					return InputControlLayout.FromType(name, type);
				}
				Func<InputControlLayout> builder;
				if (!this.layoutBuilders.TryGetValue(name, out builder))
				{
					return null;
				}
				InputControlLayout inputControlLayout = builder();
				if (inputControlLayout == null)
				{
					throw new InvalidOperationException(string.Format("Layout builder '{0}' returned null when invoked", name));
				}
				return inputControlLayout;
			}

			// Token: 0x06001319 RID: 4889 RVA: 0x00057DBC File Offset: 0x00055FBC
			public InputControlLayout TryLoadLayout(InternedString name, Dictionary<InternedString, InputControlLayout> table = null)
			{
				InputControlLayout layout;
				if (table != null && table.TryGetValue(name, out layout))
				{
					return layout;
				}
				layout = this.TryLoadLayoutInternal(name);
				if (layout != null)
				{
					layout.m_Name = name;
					if (this.layoutOverrideNames.Contains(name))
					{
						layout.isOverride = true;
					}
					InternedString baseLayoutName = default(InternedString);
					if (!layout.isOverride && this.baseLayoutTable.TryGetValue(name, out baseLayoutName))
					{
						InputControlLayout baseLayout = this.TryLoadLayout(baseLayoutName, table);
						if (baseLayout == null)
						{
							throw new InputControlLayout.LayoutNotFoundException(string.Format("Cannot find base layout '{0}' of layout '{1}'", baseLayoutName, name));
						}
						layout.MergeLayout(baseLayout);
						if (layout.m_BaseLayouts.length == 0)
						{
							layout.m_BaseLayouts.Append(baseLayoutName);
						}
					}
					InternedString[] overrides;
					if (this.layoutOverrides.TryGetValue(name, out overrides))
					{
						foreach (InternedString overrideName in overrides)
						{
							InputControlLayout inputControlLayout = this.TryLoadLayout(overrideName, null);
							inputControlLayout.MergeLayout(layout);
							inputControlLayout.m_BaseLayouts.Clear();
							inputControlLayout.isOverride = false;
							inputControlLayout.isGenericTypeOfDevice = layout.isGenericTypeOfDevice;
							inputControlLayout.m_Name = layout.name;
							inputControlLayout.m_BaseLayouts = layout.m_BaseLayouts;
							layout = inputControlLayout;
							layout.m_AppliedOverrides.Append(overrideName);
						}
					}
					if (table != null)
					{
						table[name] = layout;
					}
				}
				return layout;
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x00057EFC File Offset: 0x000560FC
			public InternedString GetBaseLayoutName(InternedString layoutName)
			{
				InternedString baseLayoutName;
				if (this.baseLayoutTable.TryGetValue(layoutName, out baseLayoutName))
				{
					return baseLayoutName;
				}
				return default(InternedString);
			}

			// Token: 0x0600131B RID: 4891 RVA: 0x00057F24 File Offset: 0x00056124
			public InternedString GetRootLayoutName(InternedString layoutName)
			{
				InternedString baseLayout;
				while (this.baseLayoutTable.TryGetValue(layoutName, out baseLayout))
				{
					layoutName = baseLayout;
				}
				return layoutName;
			}

			// Token: 0x0600131C RID: 4892 RVA: 0x00057F48 File Offset: 0x00056148
			public bool ComputeDistanceInInheritanceHierarchy(InternedString firstLayout, InternedString secondLayout, out int distance)
			{
				distance = 0;
				int secondDistanceToFirst = 0;
				InternedString current = secondLayout;
				while (!current.IsEmpty() && current != firstLayout)
				{
					current = this.GetBaseLayoutName(current);
					secondDistanceToFirst++;
				}
				if (current == firstLayout)
				{
					distance = secondDistanceToFirst;
					return true;
				}
				int firstDistanceToSecond = 0;
				current = firstLayout;
				while (!current.IsEmpty() && current != secondLayout)
				{
					current = this.GetBaseLayoutName(current);
					firstDistanceToSecond++;
				}
				if (current == secondLayout)
				{
					distance = firstDistanceToSecond;
					return true;
				}
				return false;
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x00057FC0 File Offset: 0x000561C0
			public InternedString FindLayoutThatIntroducesControl(InputControl control, InputControlLayout.Cache cache)
			{
				InputControl topmostChild = control;
				while (topmostChild.parent != control.device)
				{
					topmostChild = topmostChild.parent;
				}
				InternedString deviceLayoutName = control.device.m_Layout;
				InternedString baseLayoutName = deviceLayoutName;
				while (this.baseLayoutTable.TryGetValue(baseLayoutName, out baseLayoutName))
				{
					if (cache.FindOrLoadLayout(baseLayoutName, true).FindControl(topmostChild.m_Name) != null)
					{
						deviceLayoutName = baseLayoutName;
					}
				}
				return deviceLayoutName;
			}

			// Token: 0x0600131E RID: 4894 RVA: 0x0005802C File Offset: 0x0005622C
			public Type GetControlTypeForLayout(InternedString layoutName)
			{
				while (this.layoutStrings.ContainsKey(layoutName))
				{
					InternedString baseLayout;
					if (!this.baseLayoutTable.TryGetValue(layoutName, out baseLayout))
					{
						return typeof(InputDevice);
					}
					layoutName = baseLayout;
				}
				Type result;
				this.layoutTypes.TryGetValue(layoutName, out result);
				return result;
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x0005807C File Offset: 0x0005627C
			public bool ValueTypeIsAssignableFrom(InternedString layoutName, Type valueType)
			{
				Type controlType = this.GetControlTypeForLayout(layoutName);
				if (controlType == null)
				{
					return false;
				}
				Type valueTypOfControl = TypeHelpers.GetGenericTypeArgumentFromHierarchy(controlType, typeof(InputControl<>), 0);
				return !(valueTypOfControl == null) && valueType.IsAssignableFrom(valueTypOfControl);
			}

			// Token: 0x06001320 RID: 4896 RVA: 0x000580C0 File Offset: 0x000562C0
			public bool IsGeneratedLayout(InternedString layout)
			{
				return this.layoutBuilders.ContainsKey(layout);
			}

			// Token: 0x06001321 RID: 4897 RVA: 0x000580CE File Offset: 0x000562CE
			public IEnumerable<InternedString> GetBaseLayouts(InternedString layout, bool includeSelf = true)
			{
				if (includeSelf)
				{
					yield return layout;
				}
				while (this.baseLayoutTable.TryGetValue(layout, out layout))
				{
					yield return layout;
				}
				yield break;
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x000580F4 File Offset: 0x000562F4
			public bool IsBasedOn(InternedString parentLayout, InternedString childLayout)
			{
				InternedString layout = childLayout;
				while (this.baseLayoutTable.TryGetValue(layout, out layout))
				{
					if (layout == parentLayout)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x00058124 File Offset: 0x00056324
			public void AddMatcher(InternedString layout, InputDeviceMatcher matcher)
			{
				int layoutMatcherCount = this.layoutMatchers.Count;
				for (int i = 0; i < layoutMatcherCount; i++)
				{
					if (this.layoutMatchers[i].deviceMatcher == matcher)
					{
						return;
					}
				}
				this.layoutMatchers.Add(new InputControlLayout.Collection.LayoutMatcher
				{
					layoutName = layout,
					deviceMatcher = matcher
				});
			}

			// Token: 0x04000B6A RID: 2922
			public const float kBaseScoreForNonGeneratedLayouts = 1f;

			// Token: 0x04000B6B RID: 2923
			public Dictionary<InternedString, Type> layoutTypes;

			// Token: 0x04000B6C RID: 2924
			public Dictionary<InternedString, string> layoutStrings;

			// Token: 0x04000B6D RID: 2925
			public Dictionary<InternedString, Func<InputControlLayout>> layoutBuilders;

			// Token: 0x04000B6E RID: 2926
			public Dictionary<InternedString, InternedString> baseLayoutTable;

			// Token: 0x04000B6F RID: 2927
			public Dictionary<InternedString, InternedString[]> layoutOverrides;

			// Token: 0x04000B70 RID: 2928
			public HashSet<InternedString> layoutOverrideNames;

			// Token: 0x04000B71 RID: 2929
			public Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts;

			// Token: 0x04000B72 RID: 2930
			public List<InputControlLayout.Collection.LayoutMatcher> layoutMatchers;

			// Token: 0x02000202 RID: 514
			public struct LayoutMatcher
			{
				// Token: 0x04000B73 RID: 2931
				public InternedString layoutName;

				// Token: 0x04000B74 RID: 2932
				public InputDeviceMatcher deviceMatcher;
			}

			// Token: 0x02000203 RID: 515
			public struct PrecompiledLayout
			{
				// Token: 0x04000B75 RID: 2933
				public Func<InputDevice> factoryMethod;

				// Token: 0x04000B76 RID: 2934
				public string metadata;
			}
		}

		// Token: 0x02000205 RID: 517
		public class LayoutNotFoundException : Exception
		{
			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x0600132C RID: 4908 RVA: 0x000582AF File Offset: 0x000564AF
			public string layout { get; }

			// Token: 0x0600132D RID: 4909 RVA: 0x000582B7 File Offset: 0x000564B7
			public LayoutNotFoundException()
			{
			}

			// Token: 0x0600132E RID: 4910 RVA: 0x000582BF File Offset: 0x000564BF
			public LayoutNotFoundException(string name, string message)
				: base(message)
			{
				this.layout = name;
			}

			// Token: 0x0600132F RID: 4911 RVA: 0x000582CF File Offset: 0x000564CF
			public LayoutNotFoundException(string name)
				: base("Cannot find control layout '" + name + "'")
			{
				this.layout = name;
			}

			// Token: 0x06001330 RID: 4912 RVA: 0x000582EE File Offset: 0x000564EE
			public LayoutNotFoundException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x000582F8 File Offset: 0x000564F8
			protected LayoutNotFoundException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		// Token: 0x02000206 RID: 518
		internal struct Cache
		{
			// Token: 0x06001332 RID: 4914 RVA: 0x00058302 File Offset: 0x00056502
			public void Clear()
			{
				this.table = null;
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x0005830C File Offset: 0x0005650C
			public InputControlLayout FindOrLoadLayout(string name, bool throwIfNotFound = true)
			{
				InternedString internedName = new InternedString(name);
				if (this.table == null)
				{
					this.table = new Dictionary<InternedString, InputControlLayout>();
				}
				InputControlLayout layout = InputControlLayout.s_Layouts.TryLoadLayout(internedName, this.table);
				if (layout != null)
				{
					return layout;
				}
				if (throwIfNotFound)
				{
					throw new InputControlLayout.LayoutNotFoundException(name);
				}
				return null;
			}

			// Token: 0x04000B81 RID: 2945
			public Dictionary<InternedString, InputControlLayout> table;
		}

		// Token: 0x02000207 RID: 519
		internal struct CacheRefInstance : IDisposable
		{
			// Token: 0x06001334 RID: 4916 RVA: 0x00058356 File Offset: 0x00056556
			public void Dispose()
			{
				if (!this.valid)
				{
					return;
				}
				InputControlLayout.s_CacheInstanceRef--;
				if (InputControlLayout.s_CacheInstanceRef <= 0)
				{
					InputControlLayout.s_CacheInstance = default(InputControlLayout.Cache);
					InputControlLayout.s_CacheInstanceRef = 0;
				}
				this.valid = false;
			}

			// Token: 0x04000B82 RID: 2946
			public bool valid;
		}
	}
}

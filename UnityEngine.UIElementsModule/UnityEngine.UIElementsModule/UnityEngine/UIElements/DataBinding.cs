using System;
using System.Reflection;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002D RID: 45
	[UxmlObject]
	public class DataBinding : Binding, IDataSourceProvider
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007411 File Offset: 0x00005611
		internal static MethodInfo updateUIMethod
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = DataBinding.s_UpdateUIMethodInfo) == null)
				{
					methodInfo = (DataBinding.s_UpdateUIMethodInfo = DataBinding.CacheReflectionInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007428 File Offset: 0x00005628
		private static MethodInfo CacheReflectionInfo()
		{
			foreach (MethodInfo method in typeof(DataBinding).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				bool flag = method.Name != "UpdateUI";
				if (!flag)
				{
					bool flag2 = method.GetParameters().Length != 2;
					if (!flag2)
					{
						return DataBinding.s_UpdateUIMethodInfo = method;
					}
				}
			}
			throw new InvalidOperationException("Could not find method UpdateUI by reflection. This is an internal bug. Please report using `Help > Report a Bug...` ");
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000074A0 File Offset: 0x000056A0
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000074A8 File Offset: 0x000056A8
		[CreateProperty]
		public object dataSource { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000074B1 File Offset: 0x000056B1
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000074B9 File Offset: 0x000056B9
		public Type dataSourceType { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000074C2 File Offset: 0x000056C2
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000074CA File Offset: 0x000056CA
		[CreateProperty]
		public PropertyPath dataSourcePath { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000074D3 File Offset: 0x000056D3
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000074DC File Offset: 0x000056DC
		[CreateProperty]
		public BindingMode bindingMode
		{
			get
			{
				return this.m_BindingMode;
			}
			set
			{
				bool flag = this.m_BindingMode == value;
				if (!flag)
				{
					this.m_BindingMode = value;
					base.MarkDirty();
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007508 File Offset: 0x00005708
		[CreateProperty(ReadOnly = true)]
		public ConverterGroup sourceToUiConverters
		{
			get
			{
				ConverterGroup converterGroup;
				if ((converterGroup = this.m_SourceToUiConverters) == null)
				{
					converterGroup = (this.m_SourceToUiConverters = new ConverterGroup(string.Empty, null, null));
				}
				return converterGroup;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000753C File Offset: 0x0000573C
		[CreateProperty(ReadOnly = true)]
		public ConverterGroup uiToSourceConverters
		{
			get
			{
				ConverterGroup converterGroup;
				if ((converterGroup = this.m_UiToSourceConverters) == null)
				{
					converterGroup = (this.m_UiToSourceConverters = new ConverterGroup(string.Empty, null, null));
				}
				return converterGroup;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007570 File Offset: 0x00005770
		public void ApplyConverterGroupToSource(ConverterGroup group)
		{
			ConverterGroup localToSource = this.uiToSourceConverters;
			localToSource.registry.Apply(group.registry);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000759C File Offset: 0x0000579C
		public void ApplyConverterGroupToUI(ConverterGroup group)
		{
			ConverterGroup localToUI = this.sourceToUiConverters;
			localToUI.registry.Apply(group.registry);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000075C8 File Offset: 0x000057C8
		protected internal virtual BindingResult UpdateUI<TValue>(in BindingContext context, ref TValue value)
		{
			VisualElement target = context.targetElement;
			FocusController focusController = target.focusController;
			bool flag;
			if (focusController != null && focusController.IsFocused(target))
			{
				IDelayedField delayedField = target as IDelayedField;
				flag = delayedField != null && delayedField.isDelayed;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				Focusable leaf = focusController.GetLeafFocusedElement();
				TextElement textElement = leaf as TextElement;
				bool flag3 = textElement != null && textElement.ClassListContains("unity-text-element--inner-input-field-component");
				if (flag3)
				{
					return new BindingResult(BindingStatus.Pending, null);
				}
			}
			ConverterGroup sourceToUiConverters = this.sourceToUiConverters;
			BindingId bindingId = context.bindingId;
			PropertyPath propertyPath = in bindingId;
			VisitReturnCode returnCode;
			bool succeeded = sourceToUiConverters.TrySetValue<VisualElement, TValue>(ref target, in propertyPath, value, out returnCode);
			bool flag4 = succeeded;
			BindingResult bindingResult;
			if (flag4)
			{
				bindingResult = default(BindingResult);
			}
			else
			{
				VisitReturnCode visitReturnCode = returnCode;
				object dataSource = context.dataSource;
				propertyPath = context.dataSourcePath;
				object obj = target;
				bindingId = context.bindingId;
				string message = DataBinding.GetSetValueErrorString<TValue>(visitReturnCode, dataSource, in propertyPath, obj, in bindingId, value);
				bindingResult = new BindingResult(BindingStatus.Failure, message);
			}
			return bindingResult;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000076BC File Offset: 0x000058BC
		protected internal virtual BindingResult UpdateSource<TValue>(in BindingContext context, ref TValue value)
		{
			object target = context.dataSource;
			ConverterGroup uiToSourceConverters = this.uiToSourceConverters;
			PropertyPath propertyPath = context.dataSourcePath;
			VisitReturnCode returnCode;
			bool succeeded = uiToSourceConverters.TrySetValue<object, TValue>(ref target, in propertyPath, value, out returnCode);
			bool flag = succeeded;
			BindingResult bindingResult;
			if (flag)
			{
				bindingResult = default(BindingResult);
			}
			else
			{
				VisitReturnCode visitReturnCode = returnCode;
				object targetElement = context.targetElement;
				BindingId bindingId = context.bindingId;
				propertyPath = in bindingId;
				object dataSource = context.dataSource;
				PropertyPath dataSourcePath = context.dataSourcePath;
				BindingId bindingId2 = in dataSourcePath;
				string message = DataBinding.GetSetValueErrorString<TValue>(visitReturnCode, targetElement, in propertyPath, dataSource, in bindingId2, value);
				bindingResult = new BindingResult(BindingStatus.Failure, message);
			}
			return bindingResult;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007754 File Offset: 0x00005954
		internal static string GetSetValueErrorString<TValue>(VisitReturnCode returnCode, object source, in PropertyPath sourcePath, object target, in BindingId targetPath, TValue extractedValueFromSource)
		{
			string prefix = string.Format("[UI Toolkit] Could not set value for target of type '<b>{0}</b>' at path '<b>{1}</b>':", target.GetType().Name, targetPath);
			string text;
			switch (returnCode)
			{
			case VisitReturnCode.Ok:
			case VisitReturnCode.NullContainer:
			case VisitReturnCode.InvalidContainerType:
				throw new InvalidOperationException(prefix + " internal data binding error. Please report this using the '<b>Help/Report a bug...</b>' menu item.");
			case VisitReturnCode.MissingPropertyBag:
				text = prefix + " the type '" + target.GetType().Name + "' is missing a property bag.";
				break;
			case VisitReturnCode.InvalidPath:
				text = prefix + " the path is either invalid or contains a null value.";
				break;
			case VisitReturnCode.InvalidCast:
			{
				bool isEmpty = sourcePath.IsEmpty;
				if (isEmpty)
				{
					object obj;
					bool flag = PropertyContainer.TryGetValue<object, object>(ref target, in targetPath, out obj) && obj != null;
					if (flag)
					{
						text = ((extractedValueFromSource == null) ? (prefix + " could not convert from '<b>null</b>' to '<b>" + obj.GetType().Name + "</b>'.") : string.Concat(new string[]
						{
							prefix,
							" could not convert from type '<b>",
							extractedValueFromSource.GetType().Name,
							"</b>' to type '<b>",
							obj.GetType().Name,
							"</b>'."
						}));
						break;
					}
				}
				IProperty property;
				bool flag2 = PropertyContainer.TryGetProperty<object>(ref source, in sourcePath, out property);
				if (flag2)
				{
					object obj2;
					bool flag3 = PropertyContainer.TryGetValue<object, object>(ref target, in targetPath, out obj2) && obj2 != null;
					if (flag3)
					{
						text = ((extractedValueFromSource == null) ? string.Concat(new string[]
						{
							prefix,
							" could not convert from '<b>null (",
							property.DeclaredValueType().Name,
							")</b>' to '<b>",
							obj2.GetType().Name,
							"</b>'."
						}) : string.Concat(new string[]
						{
							prefix,
							" could not convert from type '<b>",
							extractedValueFromSource.GetType().Name,
							"</b>' to type '<b>",
							obj2.GetType().Name,
							"</b>'."
						}));
						break;
					}
				}
				text = prefix + " conversion failed.";
				break;
			}
			case VisitReturnCode.AccessViolation:
				text = prefix + " the path is read-only.";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text;
		}

		// Token: 0x040000F6 RID: 246
		private static MethodInfo s_UpdateUIMethodInfo;

		// Token: 0x040000F7 RID: 247
		private BindingMode m_BindingMode;

		// Token: 0x040000F8 RID: 248
		private ConverterGroup m_SourceToUiConverters;

		// Token: 0x040000F9 RID: 249
		private ConverterGroup m_UiToSourceConverters;
	}
}

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000022 RID: 34
	internal class BindingUpdater
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000340C File Offset: 0x0000160C
		public bool ShouldProcessBindingAtStage(Binding bindingObject, BindingUpdateStage stage, bool versionChanged, bool dirty)
		{
			if (!true)
			{
			}
			DataBinding dataBinding = bindingObject as DataBinding;
			bool flag;
			if (dataBinding == null)
			{
				CustomBinding customBinding = bindingObject as CustomBinding;
				if (customBinding == null)
				{
					throw new InvalidOperationException("Binding type `" + TypeUtility.GetTypeDisplayName(bindingObject.GetType()) + "` is not supported. This is an internal bug. Please report using `Help > Report a Bug...` ");
				}
				flag = this.ShouldProcessBindingAtStage(customBinding, stage, versionChanged, dirty);
			}
			else
			{
				flag = BindingUpdater.ShouldProcessBindingAtStage(dataBinding, stage, versionChanged, dirty);
			}
			if (!true)
			{
			}
			return flag;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000347C File Offset: 0x0000167C
		private static bool ShouldProcessBindingAtStage(DataBinding dataBinding, BindingUpdateStage stage, bool versionChanged, bool dirty)
		{
			bool flag2;
			if (stage != BindingUpdateStage.UpdateUI)
			{
				if (stage != BindingUpdateStage.UpdateSource)
				{
					throw new ArgumentOutOfRangeException("stage", stage, null);
				}
				BindingMode bindingMode = dataBinding.bindingMode;
				bool flag = bindingMode == BindingMode.ToTarget || bindingMode == BindingMode.ToTargetOnce;
				flag2 = !flag;
			}
			else
			{
				bool flag3 = dataBinding.bindingMode == BindingMode.ToSource;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = dataBinding.updateTrigger == BindingUpdateTrigger.EveryUpdate || dirty;
					if (flag4)
					{
						flag2 = true;
					}
					else
					{
						bool flag5 = dataBinding.bindingMode == BindingMode.ToTargetOnce;
						flag2 = !flag5 && (dataBinding.updateTrigger == BindingUpdateTrigger.OnSourceChanged && versionChanged);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003520 File Offset: 0x00001720
		private bool ShouldProcessBindingAtStage(CustomBinding customBinding, BindingUpdateStage stage, bool versionChanged, bool dirty)
		{
			bool flag;
			if (stage != BindingUpdateStage.UpdateUI)
			{
				if (stage != BindingUpdateStage.UpdateSource)
				{
					throw new ArgumentOutOfRangeException("stage", stage, null);
				}
				flag = false;
			}
			else
			{
				BindingUpdateTrigger updateTrigger = customBinding.updateTrigger;
				if (!true)
				{
				}
				bool flag2;
				if (updateTrigger != BindingUpdateTrigger.OnSourceChanged)
				{
					if (updateTrigger == BindingUpdateTrigger.EveryUpdate)
					{
						flag2 = true;
						goto IL_003D;
					}
				}
				else if (versionChanged || dirty)
				{
					flag2 = true;
					goto IL_003D;
				}
				flag2 = dirty;
				IL_003D:
				if (!true)
				{
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000358C File Offset: 0x0000178C
		public BindingResult UpdateUI(in BindingContext context, Binding bindingObject)
		{
			if (!true)
			{
			}
			DataBinding dataBinding = bindingObject as DataBinding;
			BindingResult bindingResult;
			if (dataBinding == null)
			{
				CustomBinding customBinding = bindingObject as CustomBinding;
				if (customBinding == null)
				{
					throw new InvalidOperationException("Binding type `" + TypeUtility.GetTypeDisplayName(bindingObject.GetType()) + "` is not supported. This is an internal bug. Please report using `Help > Report a Bug...` ");
				}
				bindingResult = this.UpdateUI(in context, customBinding);
			}
			else
			{
				bindingResult = this.UpdateUI(in context, dataBinding);
			}
			if (!true)
			{
			}
			return bindingResult;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000035FC File Offset: 0x000017FC
		public BindingResult UpdateSource(in BindingContext context, Binding bindingObject)
		{
			if (!true)
			{
			}
			DataBinding dataBinding = bindingObject as DataBinding;
			BindingResult bindingResult;
			if (dataBinding == null)
			{
				CustomBinding customBinding = bindingObject as CustomBinding;
				if (customBinding == null)
				{
					throw new InvalidOperationException("Binding type `" + TypeUtility.GetTypeDisplayName(bindingObject.GetType()) + "` is not supported. This is an internal bug. Please report using `Help > Report a Bug...` ");
				}
				bindingResult = this.UpdateDataSource(in context, customBinding);
			}
			else
			{
				bindingResult = this.UpdateDataSource(in context, dataBinding);
			}
			if (!true)
			{
			}
			return bindingResult;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000366C File Offset: 0x0000186C
		private BindingResult UpdateUI(in BindingContext context, DataBinding dataBinding)
		{
			VisualElement target = context.targetElement;
			object resolvedDataSource = context.dataSource;
			bool flag = resolvedDataSource == null;
			BindingResult bindingResult;
			if (flag)
			{
				string name = (string.IsNullOrEmpty(target.name) ? TypeUtility.GetTypeDisplayName(target.GetType()) : target.name);
				string message = "[UI Toolkit] Could not bind '" + name + "' because there is no data source.";
				bindingResult = new BindingResult(BindingStatus.Pending, message);
			}
			else
			{
				PropertyPath propertyPath = context.dataSourcePath;
				bool isEmpty = propertyPath.IsEmpty;
				if (isEmpty)
				{
					bool flag2 = !TypeTraits.IsContainer(resolvedDataSource.GetType());
					if (flag2)
					{
						bindingResult = BindingUpdater.TryUpdateUIWithNonContainer(in context, dataBinding, resolvedDataSource);
					}
					else
					{
						ValueTuple<bool, VisitReturnCode, BindingResult> visitRoot = BindingUpdater.VisitRoot(dataBinding, ref resolvedDataSource, in context);
						bool flag3 = !visitRoot.Item1;
						if (flag3)
						{
							string message2 = BindingUpdater.GetVisitationErrorString(visitRoot.Item2, in context);
							bindingResult = new BindingResult(BindingStatus.Failure, message2);
						}
						else
						{
							bindingResult = BindingUpdater.s_VisitDataSourceAsRootVisitor.result;
						}
					}
				}
				else
				{
					BindingUpdateStage bindingUpdateStage = BindingUpdateStage.UpdateUI;
					propertyPath = context.dataSourcePath;
					ValueTuple<bool, VisitReturnCode, VisitReturnCode, BindingResult> visitAtPath = BindingUpdater.VisitAtPath<object>(dataBinding, bindingUpdateStage, ref resolvedDataSource, in propertyPath, in context);
					bool flag4 = !visitAtPath.Item1;
					if (flag4)
					{
						string message3 = BindingUpdater.GetVisitationErrorString(visitAtPath.Item2, in context);
						bindingResult = new BindingResult(BindingStatus.Failure, message3);
					}
					else
					{
						bool flag5 = visitAtPath.Item3 > VisitReturnCode.Ok;
						if (flag5)
						{
							VisitReturnCode item = visitAtPath.Item3;
							object dataSource = context.dataSource;
							propertyPath = context.dataSourcePath;
							string message4 = BindingUpdater.GetExtractValueErrorString(item, dataSource, in propertyPath);
							bindingResult = new BindingResult(BindingStatus.Failure, message4);
						}
						else
						{
							bindingResult = visitAtPath.Item4;
						}
					}
				}
			}
			return bindingResult;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000037E0 File Offset: 0x000019E0
		private BindingResult UpdateUI(in BindingContext context, CustomBinding customBinding)
		{
			return customBinding.Update(in context);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000037FC File Offset: 0x000019FC
		private BindingResult UpdateDataSource(in BindingContext context, DataBinding dataBinding)
		{
			VisualElement target = context.targetElement;
			object resolvedDataSource = context.dataSource;
			PropertyPath resolvedSourcePath = context.dataSourcePath;
			bool flag = resolvedDataSource == null;
			BindingResult bindingResult;
			if (flag)
			{
				string name = (string.IsNullOrEmpty(target.name) ? TypeUtility.GetTypeDisplayName(target.GetType()) : target.name);
				string message = "[UI Toolkit] Could not set value on '" + name + "' because there is no data source.";
				bindingResult = new BindingResult(BindingStatus.Pending, message);
			}
			else
			{
				bool isEmpty = resolvedSourcePath.IsEmpty;
				if (isEmpty)
				{
					string message2 = BindingUpdater.GetRootDataSourceError(resolvedDataSource);
					bindingResult = new BindingResult(BindingStatus.Failure, message2);
				}
				else
				{
					BindingUpdateStage bindingUpdateStage = BindingUpdateStage.UpdateSource;
					BindingId bindingId = context.bindingId;
					PropertyPath propertyPath = in bindingId;
					ValueTuple<bool, VisitReturnCode, VisitReturnCode, BindingResult> visitAtPath = BindingUpdater.VisitAtPath<VisualElement>(dataBinding, bindingUpdateStage, ref target, in propertyPath, in context);
					bool flag2 = !visitAtPath.Item1;
					if (flag2)
					{
						string message3 = BindingUpdater.GetVisitationErrorString(visitAtPath.Item2, in context);
						bindingResult = new BindingResult(BindingStatus.Failure, message3);
					}
					else
					{
						bool flag3 = visitAtPath.Item3 > VisitReturnCode.Ok;
						if (flag3)
						{
							VisitReturnCode item = visitAtPath.Item3;
							object obj = target;
							bindingId = context.bindingId;
							propertyPath = in bindingId;
							string message4 = BindingUpdater.GetExtractValueErrorString(item, obj, in propertyPath);
							bindingResult = new BindingResult(BindingStatus.Failure, message4);
						}
						else
						{
							bindingResult = visitAtPath.Item4;
						}
					}
				}
			}
			return bindingResult;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003924 File Offset: 0x00001B24
		private BindingResult UpdateDataSource(in BindingContext context, CustomBinding customBinding)
		{
			return new BindingResult(BindingStatus.Pending, null);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003940 File Offset: 0x00001B40
		private static BindingResult TryUpdateUIWithNonContainer(in BindingContext context, DataBinding binding, object value)
		{
			Type type = value.GetType();
			bool isEnum = type.IsEnum;
			BindingResult bindingResult;
			if (isEnum)
			{
				MethodInfo genericMethod = DataBinding.updateUIMethod.MakeGenericMethod(new Type[] { type });
				bindingResult = (BindingResult)genericMethod.Invoke(binding, new object[] { context, value });
			}
			else
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
				{
					bool v = (bool)value;
					return binding.UpdateUI<bool>(in context, ref v);
				}
				case TypeCode.Char:
				{
					char v2 = (char)value;
					return binding.UpdateUI<char>(in context, ref v2);
				}
				case TypeCode.SByte:
				{
					sbyte v3 = (sbyte)value;
					return binding.UpdateUI<sbyte>(in context, ref v3);
				}
				case TypeCode.Byte:
				{
					byte v4 = (byte)value;
					return binding.UpdateUI<byte>(in context, ref v4);
				}
				case TypeCode.Int16:
				{
					short v5 = (short)value;
					return binding.UpdateUI<short>(in context, ref v5);
				}
				case TypeCode.UInt16:
				{
					ushort v6 = (ushort)value;
					return binding.UpdateUI<ushort>(in context, ref v6);
				}
				case TypeCode.Int32:
				{
					int v7 = (int)value;
					return binding.UpdateUI<int>(in context, ref v7);
				}
				case TypeCode.UInt32:
				{
					uint v8 = (uint)value;
					return binding.UpdateUI<uint>(in context, ref v8);
				}
				case TypeCode.Int64:
				{
					long v9 = (long)value;
					return binding.UpdateUI<long>(in context, ref v9);
				}
				case TypeCode.UInt64:
				{
					ulong v10 = (ulong)value;
					return binding.UpdateUI<ulong>(in context, ref v10);
				}
				case TypeCode.Single:
				{
					float v11 = (float)value;
					return binding.UpdateUI<float>(in context, ref v11);
				}
				case TypeCode.Double:
				{
					double v12 = (double)value;
					return binding.UpdateUI<double>(in context, ref v12);
				}
				case TypeCode.String:
				{
					string v13 = (string)value;
					return binding.UpdateUI<string>(in context, ref v13);
				}
				}
				bindingResult = new BindingResult(BindingStatus.Failure, "[UI Toolkit] Unsupported primitive type");
			}
			return bindingResult;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B30 File Offset: 0x00001D30
		[return: TupleElementNames(new string[] { "succeeded", "visitationReturnCode", "bindingResult" })]
		private static ValueTuple<bool, VisitReturnCode, BindingResult> VisitRoot(DataBinding dataBinding, ref object container, in BindingContext context)
		{
			BindingUpdater.s_VisitDataSourceAsRootVisitor.Reset();
			BindingUpdater.s_VisitDataSourceAsRootVisitor.Binding = dataBinding;
			BindingUpdater.s_VisitDataSourceAsRootVisitor.bindingContext = context;
			VisitReturnCode returnCode;
			bool succeeded = PropertyContainer.TryAccept<object>(BindingUpdater.s_VisitDataSourceAsRootVisitor, ref container, out returnCode, default(VisitParameters));
			return new ValueTuple<bool, VisitReturnCode, BindingResult>(succeeded, returnCode, BindingUpdater.s_VisitDataSourceAsRootVisitor.result);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B94 File Offset: 0x00001D94
		[return: TupleElementNames(new string[] { "succeeded", "visitationReturnCode", "atPathReturnCode", "bindingResult" })]
		private static ValueTuple<bool, VisitReturnCode, VisitReturnCode, BindingResult> VisitAtPath<TContainer>(DataBinding dataBinding, BindingUpdateStage direction, ref TContainer container, in PropertyPath path, in BindingContext context)
		{
			BindingUpdater.s_VisitDataSourceAtPathVisitor.Reset();
			BindingUpdater.s_VisitDataSourceAtPathVisitor.binding = dataBinding;
			BindingUpdater.s_VisitDataSourceAtPathVisitor.direction = direction;
			BindingUpdater.s_VisitDataSourceAtPathVisitor.Path = path;
			BindingUpdater.s_VisitDataSourceAtPathVisitor.bindingContext = context;
			VisitReturnCode returnCode;
			bool succeeded = PropertyContainer.TryAccept<TContainer>(BindingUpdater.s_VisitDataSourceAtPathVisitor, ref container, out returnCode, default(VisitParameters));
			return new ValueTuple<bool, VisitReturnCode, VisitReturnCode, BindingResult>(succeeded, returnCode, BindingUpdater.s_VisitDataSourceAtPathVisitor.ReturnCode, BindingUpdater.s_VisitDataSourceAtPathVisitor.result);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C20 File Offset: 0x00001E20
		internal static string GetVisitationErrorString(VisitReturnCode returnCode, in BindingContext context)
		{
			string prefix = string.Format("[UI Toolkit] Could not bind target of type '<b>{0}</b>' at path '<b>{1}</b>':", context.targetElement.GetType().Name, context.bindingId);
			string text;
			switch (returnCode)
			{
			case VisitReturnCode.Ok:
			case VisitReturnCode.NullContainer:
			case VisitReturnCode.InvalidCast:
			case VisitReturnCode.AccessViolation:
				throw new InvalidOperationException(prefix + " internal data binding error. Please report this using the '<b>Help/Report a bug...</b>' menu item.");
			case VisitReturnCode.InvalidContainerType:
				text = prefix + " the data source cannot be a primitive, a string or an enum.";
				break;
			case VisitReturnCode.MissingPropertyBag:
				text = prefix + " the data source is missing a property bag.";
				break;
			case VisitReturnCode.InvalidPath:
				text = prefix + " the path from the data source to the target is either invalid or contains a null value.";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal static string GetExtractValueErrorString(VisitReturnCode returnCode, object target, in PropertyPath path)
		{
			string prefix = string.Format("[UI Toolkit] Could not retrieve the value at path '<b>{0}</b>' for source of type '<b>{1}</b>':", path, (target != null) ? target.GetType().Name : null);
			string text;
			switch (returnCode)
			{
			case VisitReturnCode.Ok:
			case VisitReturnCode.NullContainer:
			case VisitReturnCode.InvalidCast:
			case VisitReturnCode.AccessViolation:
				throw new InvalidOperationException(prefix + " internal data binding error. Please report this using the '<b>Help/Report a bug...</b>' menu item.");
			case VisitReturnCode.InvalidContainerType:
				text = prefix + " the source cannot be a primitive, a string or an enum.";
				break;
			case VisitReturnCode.MissingPropertyBag:
				text = prefix + " the source is missing a property bag.";
				break;
			case VisitReturnCode.InvalidPath:
				text = prefix + " the path from the source to the target is either invalid or contains a null value.";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D60 File Offset: 0x00001F60
		internal static string GetRootDataSourceError(object target)
		{
			return "[UI Toolkit] Could not set value for target of type '<b>" + target.GetType().Name + "</b>': no path was provided.";
		}

		// Token: 0x04000047 RID: 71
		private static readonly BindingUpdater.CastDataSourceVisitor s_VisitDataSourceAsRootVisitor = new BindingUpdater.CastDataSourceVisitor();

		// Token: 0x04000048 RID: 72
		private static readonly BindingUpdater.UIPathVisitor s_VisitDataSourceAtPathVisitor = new BindingUpdater.UIPathVisitor();

		// Token: 0x02000023 RID: 35
		private sealed class CastDataSourceVisitor : ConcreteTypeVisitor
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003DAB File Offset: 0x00001FAB
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003DB3 File Offset: 0x00001FB3
			public DataBinding Binding { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003DBC File Offset: 0x00001FBC
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003DC4 File Offset: 0x00001FC4
			public BindingContext bindingContext { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003DCD File Offset: 0x00001FCD
			// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003DD5 File Offset: 0x00001FD5
			public BindingResult result { get; set; }

			// Token: 0x060000A7 RID: 167 RVA: 0x00003DE0 File Offset: 0x00001FE0
			public void Reset()
			{
				this.Binding = null;
				this.bindingContext = default(BindingContext);
				this.result = default(BindingResult);
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x00003E18 File Offset: 0x00002018
			protected override void VisitContainer<TContainer>(ref TContainer container)
			{
				DataBinding binding = this.Binding;
				BindingContext bindingContext = this.bindingContext;
				this.result = binding.UpdateUI<TContainer>(in bindingContext, ref container);
			}
		}

		// Token: 0x02000024 RID: 36
		private sealed class UIPathVisitor : PathVisitor
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000AA RID: 170 RVA: 0x00003E4B File Offset: 0x0000204B
			// (set) Token: 0x060000AB RID: 171 RVA: 0x00003E53 File Offset: 0x00002053
			public DataBinding binding { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000AC RID: 172 RVA: 0x00003E5C File Offset: 0x0000205C
			// (set) Token: 0x060000AD RID: 173 RVA: 0x00003E64 File Offset: 0x00002064
			public BindingUpdateStage direction { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000AE RID: 174 RVA: 0x00003E6D File Offset: 0x0000206D
			// (set) Token: 0x060000AF RID: 175 RVA: 0x00003E75 File Offset: 0x00002075
			public BindingContext bindingContext { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003E7E File Offset: 0x0000207E
			// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003E86 File Offset: 0x00002086
			public BindingResult result { get; set; }

			// Token: 0x060000B2 RID: 178 RVA: 0x00003E90 File Offset: 0x00002090
			public override void Reset()
			{
				base.Reset();
				this.binding = null;
				this.direction = BindingUpdateStage.UpdateUI;
				this.bindingContext = default(BindingContext);
				this.result = default(BindingResult);
				base.ReadonlyVisit = true;
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00003EE0 File Offset: 0x000020E0
			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				BindingUpdateStage direction = this.direction;
				if (!true)
				{
				}
				BindingResult bindingResult;
				if (direction != BindingUpdateStage.UpdateUI)
				{
					if (direction != BindingUpdateStage.UpdateSource)
					{
						throw new ArgumentOutOfRangeException();
					}
					DataBinding binding = this.binding;
					BindingContext bindingContext = this.bindingContext;
					bindingResult = binding.UpdateSource<TValue>(in bindingContext, ref value);
				}
				else
				{
					DataBinding binding2 = this.binding;
					BindingContext bindingContext = this.bindingContext;
					bindingResult = binding2.UpdateUI<TValue>(in bindingContext, ref value);
				}
				if (!true)
				{
				}
				this.result = bindingResult;
			}
		}
	}
}

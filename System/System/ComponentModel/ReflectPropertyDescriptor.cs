using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020002C3 RID: 707
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x0004601C File Offset: 0x0004421C
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(SR.GetString("Invalid type for the {0} property.", new object[] { name }));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000460A0 File Offset: 0x000442A0
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0004611D File Offset: 0x0004431D
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0004615C File Offset: 0x0004435C
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x000461C9 File Offset: 0x000443C9
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x000461D4 File Offset: 0x000443D4
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00046290 File Offset: 0x00044490
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.componentClass.FullName + "." + this.Name }));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[] { this.receiverType }, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.Name }));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000463E1 File Offset: 0x000445E1
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x000463EF File Offset: 0x000445EF
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00046420 File Offset: 0x00044620
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00046428 File Offset: 0x00044628
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, array, typeof(void), false);
				}
				return this.resetMethod;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000464B4 File Offset: 0x000446B4
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type type = this.ComponentType.BaseType;
						while (type != null && type != typeof(object) && !(type == null))
						{
							PropertyInfo property = type.GetProperty(name, bindingFlags, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							type = type.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[] { this.receiverType, this.type }, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004666C File Offset: 0x0004486C
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, array, typeof(bool), false);
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000466F8 File Offset: 0x000448F8
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0004678C File Offset: 0x0004498C
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00046794 File Offset: 0x00044994
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004679C File Offset: 0x0004499C
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[] { component });
			}
			return null;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000467D0 File Offset: 0x000449D0
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[] { component });
					if (componentChangeService != null)
					{
						object obj2 = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000468D4 File Offset: 0x00044AD4
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[] { component, value });
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, value);
					}
				}
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0004698C File Offset: 0x00044B8C
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00046A68 File Offset: 0x00044C68
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00046B18 File Offset: 0x00044D18
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute attribute = (Attribute)obj;
				attributes.Add(attribute);
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type type = this.componentClass;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = type.GetMethod("Get" + this.Name, bindingFlags, null, new Type[] { this.receiverType }, null);
					}
					else
					{
						memberInfo = type.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						Attribute[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							AttributeProviderAttribute attributeProviderAttribute = array4[j] as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type2 = Type.GetType(attributeProviderAttribute.TypeName);
								if (type2 != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type2.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type2);
									}
									if (array5 != null)
									{
										foreach (Attribute attribute2 in array5)
										{
											attributes.Add(attribute2);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array7 in array)
				{
					if (array7 != null)
					{
						foreach (Attribute attribute3 in array7)
						{
							attributes.Add(attribute3);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00046DB0 File Offset: 0x00044FB0
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(SR.GetString("Property accessor '{0}' on object '{1}' threw the following exception:'{2}'", new object[] { this.Name, text, text2 }), innerException);
				}
			}
			return null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00046E8C File Offset: 0x0004508C
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00046EB0 File Offset: 0x000450B0
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object obj2 = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00046FAC File Offset: 0x000451AC
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						try
						{
							componentChangeService.OnComponentChanging(component, this);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
					}
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[] { value });
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000470A8 File Offset: 0x000452A8
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x04000A81 RID: 2689
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x04000A82 RID: 2690
		private static readonly object noValue = new object();

		// Token: 0x04000A83 RID: 2691
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x04000A84 RID: 2692
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x04000A85 RID: 2693
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x04000A86 RID: 2694
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x04000A87 RID: 2695
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x04000A88 RID: 2696
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x04000A89 RID: 2697
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x04000A8A RID: 2698
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x04000A8B RID: 2699
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x04000A8C RID: 2700
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x04000A8D RID: 2701
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x04000A8E RID: 2702
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x04000A8F RID: 2703
		private BitVector32 state;

		// Token: 0x04000A90 RID: 2704
		private Type componentClass;

		// Token: 0x04000A91 RID: 2705
		private Type type;

		// Token: 0x04000A92 RID: 2706
		private object defaultValue;

		// Token: 0x04000A93 RID: 2707
		private object ambientValue;

		// Token: 0x04000A94 RID: 2708
		private PropertyInfo propInfo;

		// Token: 0x04000A95 RID: 2709
		private MethodInfo getMethod;

		// Token: 0x04000A96 RID: 2710
		private MethodInfo setMethod;

		// Token: 0x04000A97 RID: 2711
		private MethodInfo shouldSerializeMethod;

		// Token: 0x04000A98 RID: 2712
		private MethodInfo resetMethod;

		// Token: 0x04000A99 RID: 2713
		private EventDescriptor realChangedEvent;

		// Token: 0x04000A9A RID: 2714
		private Type receiverType;
	}
}

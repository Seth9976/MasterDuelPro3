using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000297 RID: 663
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004360E File Offset: 0x0004180E
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo)
			: base(eventInfo.Name, Array.Empty<Attribute>())
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			this._componentClass = componentClass;
			this._realEvent = eventInfo;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0004364D File Offset: 0x0004184D
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this._type;
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0004365C File Offset: 0x0004185C
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
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
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.Format("Invalid event handler for the {0} event.", this.Name));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase addMethod = this._addMethod;
					object[] array = new Delegate[] { value };
					addMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00043770 File Offset: 0x00041970
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this._realEvent != null)
			{
				this.FillEventInfoAttribute(this._realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this._removeMethod, attributes);
				this.FillSingleMethodAttribute(this._addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000437C4 File Offset: 0x000419C4
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingFlags);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000438A0 File Offset: 0x00041AA0
		private void FillMethods()
		{
			if (this._filledMethods)
			{
				return;
			}
			if (this._realEvent != null)
			{
				this._addMethod = this._realEvent.GetAddMethod();
				this._removeMethod = this._realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this._addMethod == null || this._removeMethod == null)
				{
					Type baseType = this._componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this._realEvent.Name, bindingFlags);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this._addMethod = eventInfo.GetAddMethod();
					this._removeMethod = eventInfo.GetRemoveMethod();
					this._type = eventInfo.EventHandlerType;
				}
				else
				{
					this._type = this._realEvent.EventHandlerType;
				}
			}
			else
			{
				this._realEvent = this._componentClass.GetEvent(this.Name);
				if (this._realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] array = new Type[] { this._type };
				this._addMethod = MemberDescriptor.FindMethod(this._componentClass, "AddOn" + this.Name, array, typeof(void));
				this._removeMethod = MemberDescriptor.FindMethod(this._componentClass, "RemoveOn" + this.Name, array, typeof(void));
				if (this._addMethod == null || this._removeMethod == null)
				{
					throw new ArgumentException(SR.Format("Accessor methods for the {0} event are missing.", this.Name));
				}
			}
			this._filledMethods = true;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00043A70 File Offset: 0x00041C70
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingFlags);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00043B60 File Offset: 0x00041D60
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
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
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase removeMethod = this._removeMethod;
					object[] array = new Delegate[] { value };
					removeMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04000A3D RID: 2621
		private Type _type;

		// Token: 0x04000A3E RID: 2622
		private readonly Type _componentClass;

		// Token: 0x04000A3F RID: 2623
		private MethodInfo _addMethod;

		// Token: 0x04000A40 RID: 2624
		private MethodInfo _removeMethod;

		// Token: 0x04000A41 RID: 2625
		private EventInfo _realEvent;

		// Token: 0x04000A42 RID: 2626
		private bool _filledMethods;
	}
}

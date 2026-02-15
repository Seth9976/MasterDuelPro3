using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020002A2 RID: 674
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never null.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be null or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		// Token: 0x06001027 RID: 4135 RVA: 0x0004404B File Offset: 0x0004224B
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return Activator.CreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, null.</returns>
		/// <param name="instance">The object for which to get the cache.</param>
		// Token: 0x06001028 RID: 4136 RVA: 0x00044082 File Offset: 0x00042282
		public virtual IDictionary GetCache(object instance)
		{
			TypeDescriptionProvider parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetCache(instance);
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		// Token: 0x06001029 RID: 4137 RVA: 0x00044098 File Offset: 0x00042298
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor emptyCustomTypeDescriptor;
			if ((emptyCustomTypeDescriptor = this._emptyDescriptor) == null)
			{
				emptyCustomTypeDescriptor = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return emptyCustomTypeDescriptor;
		}

		/// <summary>Gets the extender providers for the specified object.</summary>
		/// <returns>An array of extender providers for <paramref name="instance" />.</returns>
		/// <param name="instance">The object to get extender providers for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600102A RID: 4138 RVA: 0x000440D2 File Offset: 0x000422D2
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return Array.Empty<IExtenderProvider>();
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null.</exception>
		// Token: 0x0600102B RID: 4139 RVA: 0x000440FC File Offset: 0x000422FC
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be null.</param>
		// Token: 0x0600102C RID: 4140 RVA: 0x00044106 File Offset: 0x00042306
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		// Token: 0x0600102D RID: 4141 RVA: 0x0004411F File Offset: 0x0004231F
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600102E RID: 4142 RVA: 0x00044129 File Offset: 0x00042329
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		// Token: 0x0600102F RID: 4143 RVA: 0x00044148 File Offset: 0x00042348
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor emptyCustomTypeDescriptor;
			if ((emptyCustomTypeDescriptor = this._emptyDescriptor) == null)
			{
				emptyCustomTypeDescriptor = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return emptyCustomTypeDescriptor;
		}

		// Token: 0x04000A52 RID: 2642
		private readonly TypeDescriptionProvider _parent;

		// Token: 0x04000A53 RID: 2643
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x020002A3 RID: 675
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}

using System;

namespace System.Runtime.Serialization
{
	/// <summary>Allows users to control class loading and mandate what class to load.</summary>
	// Token: 0x020004B1 RID: 1201
	[Serializable]
	public abstract class SerializationBinder
	{
		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <param name="serializedType">The type of the object the formatter creates a new instance of.</param>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x0600264F RID: 9807 RVA: 0x0009A9BB File Offset: 0x00098BBB
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <returns>The type of the object the formatter creates a new instance of.</returns>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x06002650 RID: 9808
		public abstract Type BindToType(string assemblyName, string typeName);
	}
}

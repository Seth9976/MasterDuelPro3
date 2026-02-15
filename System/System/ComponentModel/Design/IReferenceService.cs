using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for obtaining references to objects within a project by name or type, obtaining the name of a specified object, and for locating the parent of a specified object within a designer project.</summary>
	// Token: 0x020002E6 RID: 742
	public interface IReferenceService
	{
		/// <summary>Gets a reference to the component whose name matches the specified name.</summary>
		/// <returns>An object the specified name refers to, or null if no reference is found.</returns>
		/// <param name="name">The name of the component to return a reference to. </param>
		// Token: 0x060011F7 RID: 4599
		object GetReference(string name);

		/// <summary>Gets the name of the specified component.</summary>
		/// <returns>The name of the object referenced, or null if the object reference is not valid.</returns>
		/// <param name="reference">The object to return the name of. </param>
		// Token: 0x060011F8 RID: 4600
		string GetName(object reference);

		/// <summary>Gets all available references to components of the specified type.</summary>
		/// <returns>An array of all available objects of the specified type.</returns>
		/// <param name="baseType">The type of object to return references to instances of. </param>
		// Token: 0x060011F9 RID: 4601
		object[] GetReferences(Type baseType);
	}
}

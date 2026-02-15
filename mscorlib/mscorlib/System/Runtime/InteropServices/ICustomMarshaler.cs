using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides custom wrappers for handling method calls.</summary>
	// Token: 0x0200051F RID: 1311
	public interface ICustomMarshaler
	{
		/// <summary>Converts the unmanaged data to managed data.</summary>
		/// <returns>An object that represents the managed view of the COM data.</returns>
		/// <param name="pNativeData">A pointer to the unmanaged data to be wrapped. </param>
		// Token: 0x06002901 RID: 10497
		object MarshalNativeToManaged(IntPtr pNativeData);

		/// <summary>Converts the managed data to unmanaged data.</summary>
		/// <returns>A pointer to the COM view of the managed object.</returns>
		/// <param name="ManagedObj">The managed object to be converted. </param>
		// Token: 0x06002902 RID: 10498
		IntPtr MarshalManagedToNative(object ManagedObj);

		/// <summary>Performs necessary cleanup of the unmanaged data when it is no longer needed.</summary>
		/// <param name="pNativeData">A pointer to the unmanaged data to be destroyed. </param>
		// Token: 0x06002903 RID: 10499
		void CleanUpNativeData(IntPtr pNativeData);

		/// <summary>Performs necessary cleanup of the managed data when it is no longer needed.</summary>
		/// <param name="ManagedObj">The managed object to be destroyed. </param>
		// Token: 0x06002904 RID: 10500
		void CleanUpManagedData(object ManagedObj);

		/// <summary>Returns the size of the native data to be marshaled.</summary>
		/// <returns>The size, in bytes, of the native data.</returns>
		// Token: 0x06002905 RID: 10501
		int GetNativeDataSize();
	}
}

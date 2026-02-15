using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides methods to place data on and retrieve data from the system Clipboard. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000033 RID: 51
	public sealed class Clipboard
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004FFE File Offset: 0x000031FE
		private static bool ConvertToClipboardData(ref int type, object obj, out byte[] data)
		{
			data = null;
			return false;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005004 File Offset: 0x00003204
		private static bool ConvertFromClipboardData(int type, IntPtr data, out object obj)
		{
			obj = null;
			data == IntPtr.Zero;
			return false;
		}

		/// <summary>Retrieves the data that is currently on the system Clipboard.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IDataObject" /> that represents the data currently on the Clipboard, or null if there is no data on the Clipboard.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be retrieved from the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode and the <see cref="P:System.Windows.Forms.Application.MessageLoop" /> property value is true. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600011C RID: 284 RVA: 0x00005016 File Offset: 0x00003216
		public static IDataObject GetDataObject()
		{
			return Clipboard.GetDataObject(false);
		}

		/// <summary>Clears the Clipboard and then places nonpersistent data on it.</summary>
		/// <param name="data">The data to place on the Clipboard. </param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600011D RID: 285 RVA: 0x0000501E File Offset: 0x0000321E
		public static void SetDataObject(object data)
		{
			Clipboard.SetDataObject(data, false);
		}

		/// <summary>Clears the Clipboard and then places data on it and specifies whether the data should remain after the application exits.</summary>
		/// <param name="data">The data to place on the Clipboard. </param>
		/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false. </param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600011E RID: 286 RVA: 0x00005027 File Offset: 0x00003227
		public static void SetDataObject(object data, bool copy)
		{
			Clipboard.SetDataObject(data, copy, 10, 100);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005034 File Offset: 0x00003234
		internal static void SetDataObjectImpl(object data, bool copy)
		{
			XplatUI.ObjectToClipboard objectToClipboard = new XplatUI.ObjectToClipboard(Clipboard.ConvertToClipboardData);
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			XplatUI.ClipboardStore(intPtr, null, 0, null, copy);
			int num = -1;
			if (data is IDataObject)
			{
				IDataObject dataObject = data as IDataObject;
				string[] formats = dataObject.GetFormats();
				for (int i = 0; i < formats.Length; i++)
				{
					DataFormats.Format format = DataFormats.GetFormat(formats[i]);
					if (format != null && format.Name != DataFormats.StringFormat)
					{
						num = format.Id;
					}
					object data2 = dataObject.GetData(formats[i]);
					if (Clipboard.IsDataSerializable(data2))
					{
						format.is_serializable = true;
					}
					XplatUI.ClipboardStore(intPtr, data2, num, objectToClipboard, copy);
				}
			}
			else
			{
				DataFormats.Format format = DataFormats.Format.Find(data.GetType().FullName);
				if (format != null && format.Name != DataFormats.StringFormat)
				{
					num = format.Id;
				}
				XplatUI.ClipboardStore(intPtr, data, num, objectToClipboard, copy);
			}
			XplatUI.ClipboardClose(intPtr);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000511C File Offset: 0x0000331C
		private static bool IsDataSerializable(object obj)
		{
			return obj is ISerializable || TypeDescriptor.GetAttributes(obj)[typeof(SerializableAttribute)] != null;
		}

		/// <summary>Clears the Clipboard and then attempts to place data on it the specified number of times and with the specified delay between attempts, optionally leaving the data on the Clipboard after the application exits.</summary>
		/// <param name="data">The data to place on the Clipboard.</param>
		/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
		/// <param name="retryTimes">The number of times to attempt placing the data on the Clipboard.</param>
		/// <param name="retryDelay">The number of milliseconds to pause between attempts. </param>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retryTimes" /> is less than zero.-or-<paramref name="retryDelay" /> is less than zero.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		// Token: 0x06000121 RID: 289 RVA: 0x00005140 File Offset: 0x00003340
		public static void SetDataObject(object data, bool copy, int retryTimes, int retryDelay)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (retryTimes < 0)
			{
				throw new ArgumentOutOfRangeException("retryTimes");
			}
			if (retryDelay < 0)
			{
				throw new ArgumentOutOfRangeException("retryDelay");
			}
			bool flag = true;
			do
			{
				flag = false;
				retryTimes--;
				try
				{
					Clipboard.SetDataObjectImpl(data, copy);
				}
				catch (ExternalException)
				{
					if (retryTimes <= 0)
					{
						throw;
					}
					flag = true;
					Thread.Sleep(retryDelay);
				}
			}
			while (flag && retryTimes > 0);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000051B4 File Offset: 0x000033B4
		internal static IDataObject GetDataObject(bool primary_selection)
		{
			XplatUI.ClipboardToObject clipboardToObject = new XplatUI.ClipboardToObject(Clipboard.ConvertFromClipboardData);
			IntPtr intPtr = XplatUI.ClipboardOpen(primary_selection);
			int[] array = XplatUI.ClipboardAvailableFormats(intPtr);
			if (array == null)
			{
				return null;
			}
			DataObject dataObject = new DataObject();
			for (int i = 0; i < array.Length; i++)
			{
				DataFormats.Format format = DataFormats.GetFormat(array[i]);
				if (format != null)
				{
					object obj = XplatUI.ClipboardRetrieve(intPtr, array[i], clipboardToObject);
					if (obj != null)
					{
						dataObject.SetData(format.Name, obj);
						if (format.Name == DataFormats.Dib)
						{
							dataObject.SetData(DataFormats.Bitmap, obj);
						}
					}
				}
			}
			XplatUI.ClipboardClose(intPtr);
			return dataObject;
		}
	}
}

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	/// <summary>Represents a key-level node in the Windows registry. This class is a registry encapsulation.</summary>
	// Token: 0x0200007F RID: 127
	public sealed class RegistryKey : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000F505 File Offset: 0x0000D705
		private void ClosePerfDataKey()
		{
			Interop.Advapi32.RegCloseKey(RegistryKey.HKEY_PERFORMANCE_DATA);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F514 File Offset: 0x0000D714
		private RegistryKey CreateSubKeyInternalCore(string subkey, RegistryKeyPermissionCheck permissionCheck, object registrySecurityObj, RegistryOptions registryOptions)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES security_ATTRIBUTES = default(Interop.Kernel32.SECURITY_ATTRIBUTES);
			int num = 0;
			SafeRegistryHandle safeRegistryHandle = null;
			int num2 = Interop.Advapi32.RegCreateKeyEx(this._hkey, subkey, 0, null, (int)registryOptions, RegistryKey.GetRegistryKeyAccess(permissionCheck != RegistryKeyPermissionCheck.ReadSubTree) | (int)this._regView, ref security_ATTRIBUTES, out safeRegistryHandle, out num);
			if (num2 == 0 && !safeRegistryHandle.IsInvalid)
			{
				RegistryKey registryKey = new RegistryKey(safeRegistryHandle, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree, false, this._remoteKey, false, this._regView);
				registryKey._checkMode = permissionCheck;
				if (subkey.Length == 0)
				{
					registryKey._keyName = this._keyName;
				}
				else
				{
					registryKey._keyName = this._keyName + "\\" + subkey;
				}
				return registryKey;
			}
			if (num2 != 0)
			{
				this.Win32Error(num2, this._keyName + "\\" + subkey);
			}
			return null;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		private void DeleteValueCore(string name, bool throwOnMissingValue)
		{
			int num = Interop.Advapi32.RegDeleteValue(this._hkey, name);
			if (num == 2 || num == 206)
			{
				if (throwOnMissingValue)
				{
					ThrowHelper.ThrowArgumentException("No value exists with that name.");
					return;
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F624 File Offset: 0x0000D824
		private static RegistryKey OpenBaseKeyCore(RegistryHive hKeyHive, RegistryView view)
		{
			IntPtr intPtr = (IntPtr)((int)hKeyHive);
			int num = (int)intPtr & 268435455;
			bool flag = intPtr == RegistryKey.HKEY_PERFORMANCE_DATA;
			return new RegistryKey(new SafeRegistryHandle(intPtr, flag), true, true, false, flag, view)
			{
				_checkMode = RegistryKeyPermissionCheck.Default,
				_keyName = RegistryKey.s_hkeyNames[num]
			};
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F678 File Offset: 0x0000D878
		private RegistryKey InternalOpenSubKeyCore(string name, bool writable, bool throwOnPermissionFailure)
		{
			SafeRegistryHandle safeRegistryHandle = null;
			int num = Interop.Advapi32.RegOpenKeyEx(this._hkey, name, 0, RegistryKey.GetRegistryKeyAccess(writable) | (int)this._regView, out safeRegistryHandle);
			if (num == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle, writable, false, this._remoteKey, false, this._regView)
				{
					_checkMode = this.GetSubKeyPermissionCheck(writable),
					_keyName = this._keyName + "\\" + name
				};
			}
			if (throwOnPermissionFailure && (num == 5 || num == 1346))
			{
				ThrowHelper.ThrowSecurityException("Requested registry access is not allowed.");
			}
			return null;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F714 File Offset: 0x0000D914
		internal RegistryKey InternalOpenSubKeyWithoutSecurityChecksCore(string name, bool writable)
		{
			SafeRegistryHandle safeRegistryHandle = null;
			if (Interop.Advapi32.RegOpenKeyEx(this._hkey, name, 0, RegistryKey.GetRegistryKeyAccess(writable) | (int)this._regView, out safeRegistryHandle) == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle, writable, false, this._remoteKey, false, this._regView)
				{
					_keyName = this._keyName + "\\" + name
				};
			}
			return null;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F784 File Offset: 0x0000D984
		private int InternalSubKeyCountCore()
		{
			int num = 0;
			int num2 = 0;
			int num3 = Interop.Advapi32.RegQueryInfoKey(this._hkey, null, null, IntPtr.Zero, ref num, null, null, ref num2, null, null, null, null);
			if (num3 != 0)
			{
				this.Win32Error(num3, null);
			}
			return num;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private string[] InternalGetSubKeyNamesCore(int subkeys)
		{
			List<string> list = new List<string>(subkeys);
			char[] array = ArrayPool<char>.Shared.Rent(256);
			try
			{
				int num = array.Length;
				int num2;
				while ((num2 = Interop.Advapi32.RegEnumKeyEx(this._hkey, list.Count, array, ref num, null, null, null, null)) != 259)
				{
					if (num2 == 0)
					{
						list.Add(new string(array, 0, num));
						num = array.Length;
					}
					else
					{
						this.Win32Error(num2, null);
					}
				}
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return list.ToArray();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F854 File Offset: 0x0000DA54
		private int InternalValueCountCore()
		{
			int num = 0;
			int num2 = 0;
			int num3 = Interop.Advapi32.RegQueryInfoKey(this._hkey, null, null, IntPtr.Zero, ref num2, null, null, ref num, null, null, null, null);
			if (num3 != 0)
			{
				this.Win32Error(num3, null);
			}
			return num;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F890 File Offset: 0x0000DA90
		private unsafe string[] GetValueNamesCore(int values)
		{
			List<string> list = new List<string>(values);
			char[] array = ArrayPool<char>.Shared.Rent(100);
			try
			{
				int num = array.Length;
				int num2;
				while ((num2 = Interop.Advapi32.RegEnumValue(this._hkey, list.Count, array, ref num, IntPtr.Zero, null, null, null)) != 259)
				{
					if (num2 != 0)
					{
						if (num2 != 234)
						{
							this.Win32Error(num2, null);
						}
						else
						{
							if (this.IsPerfDataKey())
							{
								try
								{
									fixed (char* ptr = &array[0])
									{
										char* ptr2 = ptr;
										list.Add(new string(ptr2));
										goto IL_0092;
									}
								}
								finally
								{
									char* ptr = null;
								}
							}
							char[] array2 = array;
							int num3 = array2.Length;
							array = null;
							ArrayPool<char>.Shared.Return(array2, false);
							array = ArrayPool<char>.Shared.Rent(checked(num3 * 2));
						}
					}
					else
					{
						list.Add(new string(array, 0, num));
					}
					IL_0092:
					num = array.Length;
				}
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array, false);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F994 File Offset: 0x0000DB94
		private object InternalGetValueCore(string name, object defaultValue, bool doNotExpand)
		{
			object obj = defaultValue;
			int num = 0;
			int num2 = 0;
			int num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, null, ref num2);
			if (num3 != 0)
			{
				if (this.IsPerfDataKey())
				{
					int num4 = 65000;
					int num5 = num4;
					byte[] array = new byte[num4];
					int num6;
					while (234 == (num6 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array, ref num5)))
					{
						if (num4 == 2147483647)
						{
							this.Win32Error(num6, name);
						}
						else if (num4 > 1073741823)
						{
							num4 = int.MaxValue;
						}
						else
						{
							num4 *= 2;
						}
						num5 = num4;
						array = new byte[num4];
					}
					if (num6 != 0)
					{
						this.Win32Error(num6, name);
					}
					return array;
				}
				if (num3 != 234)
				{
					return obj;
				}
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			switch (num)
			{
			case 0:
			case 3:
			case 5:
				break;
			case 1:
			{
				char[] array2;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException ex)
						{
							throw new IOException("RegistryKey.GetValue does not allow a String that has a length greater than Int32.MaxValue.", ex);
						}
					}
					array2 = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array2, ref num2);
				}
				if (array2.Length != 0 && array2[array2.Length - 1] == '\0')
				{
					return new string(array2, 0, array2.Length - 1);
				}
				return new string(array2);
			}
			case 2:
			{
				char[] array3;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException ex2)
						{
							throw new IOException("RegistryKey.GetValue does not allow a String that has a length greater than Int32.MaxValue.", ex2);
						}
					}
					array3 = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array3, ref num2);
				}
				if (array3.Length != 0 && array3[array3.Length - 1] == '\0')
				{
					obj = new string(array3, 0, array3.Length - 1);
				}
				else
				{
					obj = new string(array3);
				}
				if (!doNotExpand)
				{
					return Environment.ExpandEnvironmentVariables((string)obj);
				}
				return obj;
			}
			case 4:
				if (num2 <= 4)
				{
					int num7 = 0;
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, ref num7, ref num2);
					return num7;
				}
				goto IL_0118;
			case 6:
			case 8:
			case 9:
			case 10:
				return obj;
			case 7:
			{
				char[] array4;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException ex3)
						{
							throw new IOException("RegistryKey.GetValue does not allow a String that has a length greater than Int32.MaxValue.", ex3);
						}
					}
					array4 = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array4, ref num2);
				}
				if (array4.Length != 0 && array4[array4.Length - 1] != '\0')
				{
					Array.Resize<char>(ref array4, array4.Length + 1);
				}
				string[] array5 = Array.Empty<string>();
				int num8 = 0;
				int num9 = 0;
				int num10 = array4.Length;
				while (num3 == 0 && num9 < num10)
				{
					int num11 = num9;
					while (num11 < num10 && array4[num11] != '\0')
					{
						num11++;
					}
					string text = null;
					if (num11 < num10)
					{
						if (num11 - num9 > 0)
						{
							text = new string(array4, num9, num11 - num9);
						}
						else if (num11 != num10 - 1)
						{
							text = string.Empty;
						}
					}
					else
					{
						text = new string(array4, num9, num10 - num9);
					}
					num9 = num11 + 1;
					if (text != null)
					{
						if (array5.Length == num8)
						{
							Array.Resize<string>(ref array5, (num8 > 0) ? (num8 * 2) : 4);
						}
						array5[num8++] = text;
					}
				}
				Array.Resize<string>(ref array5, num8);
				return array5;
			}
			case 11:
				goto IL_0118;
			default:
				return obj;
			}
			IL_00F2:
			byte[] array6 = new byte[num2];
			num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array6, ref num2);
			return array6;
			IL_0118:
			if (num2 > 8)
			{
				goto IL_00F2;
			}
			long num12 = 0L;
			num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, ref num12, ref num2);
			obj = num12;
			return obj;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000FD34 File Offset: 0x0000DF34
		private void SetValueCore(string name, object value, RegistryValueKind valueKind)
		{
			int num = 0;
			try
			{
				switch (valueKind)
				{
				case RegistryValueKind.None:
				case RegistryValueKind.Binary:
				{
					byte[] array = (byte[])value;
					num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, (valueKind == RegistryValueKind.None) ? RegistryValueKind.Unknown : RegistryValueKind.Binary, array, array.Length);
					break;
				}
				case RegistryValueKind.String:
				case RegistryValueKind.ExpandString:
				{
					string text = value.ToString();
					num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, valueKind, text, checked(text.Length * 2 + 2));
					break;
				}
				case RegistryValueKind.DWord:
				{
					int num2 = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, RegistryValueKind.DWord, ref num2, 4);
					break;
				}
				case RegistryValueKind.MultiString:
				{
					string[] array2 = (string[])((string[])value).Clone();
					int num3 = 1;
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i] == null)
						{
							ThrowHelper.ThrowArgumentException("RegistryKey.SetValue does not allow a String[] that contains a null String reference.");
						}
						checked
						{
							num3 += array2[i].Length + 1;
						}
					}
					int num4 = checked(num3 * 2);
					char[] array3 = new char[num3];
					int num5 = 0;
					for (int j = 0; j < array2.Length; j++)
					{
						int length = array2[j].Length;
						array2[j].CopyTo(0, array3, num5, length);
						num5 += length + 1;
					}
					num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, RegistryValueKind.MultiString, array3, num4);
					break;
				}
				case RegistryValueKind.QWord:
				{
					long num6 = Convert.ToInt64(value, CultureInfo.InvariantCulture);
					num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, RegistryValueKind.QWord, ref num6, 8);
					break;
				}
				}
			}
			catch (Exception ex) when (ex is OverflowException || ex is InvalidOperationException || ex is FormatException || ex is InvalidCastException)
			{
				ThrowHelper.ThrowArgumentException("The type of the value object did not match the specified RegistryValueKind or the object could not be properly converted.");
			}
			if (num == 0)
			{
				this.SetDirty();
				return;
			}
			this.Win32Error(num, null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000FF34 File Offset: 0x0000E134
		private void Win32Error(int errorCode, string str)
		{
			switch (errorCode)
			{
			case 2:
				throw new IOException("The specified registry key does not exist.", errorCode);
			case 5:
				throw (str != null) ? new UnauthorizedAccessException(SR.Format("Access to the registry key '{0}' is denied.", str)) : new UnauthorizedAccessException();
			case 6:
				if (!this.IsPerfDataKey())
				{
					this._hkey.SetHandleAsInvalid();
					this._hkey = null;
				}
				break;
			}
			throw new IOException(Interop.Kernel32.GetMessage(errorCode), errorCode);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		private static int GetRegistryKeyAccess(bool isWritable)
		{
			int num;
			if (!isWritable)
			{
				num = 131097;
			}
			else
			{
				num = 131103;
			}
			return num;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		private RegistryKey(SafeRegistryHandle hkey, bool writable, bool systemkey, bool remoteKey, bool isPerfData, RegistryView view)
		{
			RegistryKey.ValidateKeyView(view);
			this._hkey = hkey;
			this._keyName = "";
			this._remoteKey = remoteKey;
			this._regView = view;
			if (systemkey)
			{
				this._state |= RegistryKey.StateFlags.SystemKey;
			}
			if (writable)
			{
				this._state |= RegistryKey.StateFlags.WriteAccess;
			}
			if (isPerfData)
			{
				this._state |= RegistryKey.StateFlags.PerfData;
			}
		}

		/// <summary>Closes the key and flushes it to disk if its contents have been modified.</summary>
		// Token: 0x0600026D RID: 621 RVA: 0x00010058 File Offset: 0x0000E258
		public void Close()
		{
			this.Dispose();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:Microsoft.Win32.RegistryKey" /> class.</summary>
		// Token: 0x0600026E RID: 622 RVA: 0x00010060 File Offset: 0x0000E260
		public void Dispose()
		{
			if (this._hkey != null)
			{
				if (!this.IsSystemKey())
				{
					try
					{
						this._hkey.Dispose();
						return;
					}
					catch (IOException)
					{
						return;
					}
					finally
					{
						this._hkey = null;
					}
				}
				if (this.IsPerfDataKey())
				{
					this.ClosePerfDataKey();
				}
			}
		}

		/// <summary>Creates a new subkey or opens an existing subkey for write access.  </summary>
		/// <returns>The newly created subkey, or null if the operation failed. If a zero-length string is specified for <paramref name="subkey" />, the current <see cref="T:Microsoft.Win32.RegistryKey" /> object is returned.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. This string is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> on which this method is being invoked is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> cannot be written to; for example, it was not opened as a writable key , or the user does not have the necessary access rights. </exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key, or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600026F RID: 623 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public RegistryKey CreateSubKey(string subkey)
		{
			return this.CreateSubKey(subkey, this._checkMode);
		}

		/// <summary>Creates a new subkey or opens an existing subkey for write access, using the specified permission check option. </summary>
		/// <returns>The newly created subkey, or null if the operation failed. If a zero-length string is specified for <paramref name="subkey" />, the current <see cref="T:Microsoft.Win32.RegistryKey" /> object is returned.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. This string is not case-sensitive.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionCheck" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> on which this method is being invoked is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> cannot be written to; for example, it was not opened as a writable key, or the user does not have the necessary access rights. </exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key, or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root.</exception>
		// Token: 0x06000270 RID: 624 RVA: 0x000100D9 File Offset: 0x0000E2D9
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.CreateSubKeyInternal(subkey, permissionCheck, null, RegistryOptions.None);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private RegistryKey CreateSubKeyInternal(string subkey, RegistryKeyPermissionCheck permissionCheck, object registrySecurityObj, RegistryOptions registryOptions)
		{
			RegistryKey.ValidateKeyOptions(registryOptions);
			RegistryKey.ValidateKeyName(subkey);
			RegistryKey.ValidateKeyMode(permissionCheck);
			this.EnsureWriteable();
			subkey = RegistryKey.FixupName(subkey);
			if (!this._remoteKey)
			{
				RegistryKey registryKey = this.InternalOpenSubKeyWithoutSecurityChecks(subkey, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree);
				if (registryKey != null)
				{
					registryKey._checkMode = permissionCheck;
					return registryKey;
				}
			}
			return this.CreateSubKeyInternalCore(subkey, permissionCheck, registrySecurityObj, registryOptions);
		}

		/// <summary>Deletes the specified value from this key, and specifies whether an exception is raised if the value is not found.</summary>
		/// <param name="name">The name of the value to delete. </param>
		/// <param name="throwOnMissingValue">Indicates whether an exception should be raised if the specified value cannot be found. If this argument is true and the specified value does not exist, an exception is raised. If this argument is false and the specified value does not exist, no action is taken. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid reference to a value and <paramref name="throwOnMissingValue" /> is true. -or- <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the value. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is read-only. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000272 RID: 626 RVA: 0x00010148 File Offset: 0x0000E348
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			this.EnsureWriteable();
			this.DeleteValueCore(name, throwOnMissingValue);
		}

		/// <summary>Opens a new <see cref="T:Microsoft.Win32.RegistryKey" /> that represents the requested key on the local machine with the specified view.</summary>
		/// <returns>The requested registry key.</returns>
		/// <param name="hKey">The HKEY to open.</param>
		/// <param name="view">The registry view to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hKey" /> or <paramref name="view" /> is invalid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to perform this action.</exception>
		// Token: 0x06000273 RID: 627 RVA: 0x00010158 File Offset: 0x0000E358
		public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
		{
			RegistryKey.ValidateKeyView(view);
			return RegistryKey.OpenBaseKeyCore(hKey, view);
		}

		/// <summary>Retrieves a subkey as read-only.</summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">The name or path of the subkey to open as read-only. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read the registry key. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06000274 RID: 628 RVA: 0x00010167 File Offset: 0x0000E367
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		/// <summary>Retrieves a specified subkey, and specifies whether write access is to be applied to the key. </summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">Name or path of the subkey to open. </param>
		/// <param name="writable">Set to true if you need write access to the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to access the registry key in the specified mode. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000275 RID: 629 RVA: 0x00010171 File Offset: 0x0000E371
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			RegistryKey.ValidateKeyName(name);
			this.EnsureNotDisposed();
			name = RegistryKey.FixupName(name);
			return this.InternalOpenSubKeyCore(name, writable, true);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010190 File Offset: 0x0000E390
		internal RegistryKey InternalOpenSubKeyWithoutSecurityChecks(string name, bool writable)
		{
			RegistryKey.ValidateKeyName(name);
			this.EnsureNotDisposed();
			return this.InternalOpenSubKeyWithoutSecurityChecksCore(name, writable);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000101A6 File Offset: 0x0000E3A6
		private int InternalSubKeyCount()
		{
			this.EnsureNotDisposed();
			return this.InternalSubKeyCountCore();
		}

		/// <summary>Retrieves an array of strings that contains all the subkey names.</summary>
		/// <returns>An array of strings that contains the names of the subkeys for the current key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000278 RID: 632 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public string[] GetSubKeyNames()
		{
			return this.InternalGetSubKeyNames();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000101BC File Offset: 0x0000E3BC
		private string[] InternalGetSubKeyNames()
		{
			this.EnsureNotDisposed();
			int num = this.InternalSubKeyCount();
			if (num <= 0)
			{
				return Array.Empty<string>();
			}
			return this.InternalGetSubKeyNamesCore(num);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000101E7 File Offset: 0x0000E3E7
		private int InternalValueCount()
		{
			this.EnsureNotDisposed();
			return this.InternalValueCountCore();
		}

		/// <summary>Retrieves an array of strings that contains all the value names associated with this key.</summary>
		/// <returns>An array of strings that contains the value names for the current key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" />  being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred; for example, the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600027B RID: 635 RVA: 0x000101F8 File Offset: 0x0000E3F8
		public string[] GetValueNames()
		{
			this.EnsureNotDisposed();
			int num = this.InternalValueCount();
			if (num <= 0)
			{
				return Array.Empty<string>();
			}
			return this.GetValueNamesCore(num);
		}

		/// <summary>Retrieves the value associated with the specified name. Returns null if the name/value pair does not exist in the registry.</summary>
		/// <returns>The value associated with <paramref name="name" />, or null if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600027C RID: 636 RVA: 0x00010223 File Offset: 0x0000E423
		public object GetValue(string name)
		{
			return this.InternalGetValue(name, null, false, true);
		}

		/// <summary>Retrieves the value associated with the specified name. If the name is not found, returns the default value that you provide.</summary>
		/// <returns>The value associated with <paramref name="name" />, with any embedded environment variables left unexpanded, or <paramref name="defaultValue" /> if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <param name="defaultValue">The value to return if <paramref name="name" /> does not exist. </param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600027D RID: 637 RVA: 0x0001022F File Offset: 0x0000E42F
		public object GetValue(string name, object defaultValue)
		{
			return this.InternalGetValue(name, defaultValue, false, true);
		}

		/// <summary>Retrieves the value associated with the specified name and retrieval options. If the name is not found, returns the default value that you provide.</summary>
		/// <returns>The value associated with <paramref name="name" />, processed according to the specified <paramref name="options" />, or <paramref name="defaultValue" /> if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <param name="defaultValue">The value to return if <paramref name="name" /> does not exist. </param>
		/// <param name="options">One of the enumeration values that specifies optional processing of the retrieved value.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:Microsoft.Win32.RegistryValueOptions" /> value; for example, an invalid value is cast to <see cref="T:Microsoft.Win32.RegistryValueOptions" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600027E RID: 638 RVA: 0x0001023C File Offset: 0x0000E43C
		public object GetValue(string name, object defaultValue, RegistryValueOptions options)
		{
			if (options < RegistryValueOptions.None || options > RegistryValueOptions.DoNotExpandEnvironmentNames)
			{
				throw new ArgumentException(SR.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			bool flag = options == RegistryValueOptions.DoNotExpandEnvironmentNames;
			return this.InternalGetValue(name, defaultValue, flag, true);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001027B File Offset: 0x0000E47B
		private object InternalGetValue(string name, object defaultValue, bool doNotExpand, bool checkSecurity)
		{
			if (checkSecurity)
			{
				this.EnsureNotDisposed();
			}
			return this.InternalGetValueCore(name, defaultValue, doNotExpand);
		}

		/// <summary>Retrieves the name of the key.</summary>
		/// <returns>The absolute (qualified) name of the key.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00010290 File Offset: 0x0000E490
		public string Name
		{
			get
			{
				this.EnsureNotDisposed();
				return this._keyName;
			}
		}

		/// <summary>Sets the specified name/value pair.</summary>
		/// <param name="name">The name of the value to store. </param>
		/// <param name="value">The data to be stored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is an unsupported data type. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and cannot be written to; for example, the key has not been opened with write access. -or-The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows Millennium Edition or Windows 98.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows 2000, Windows XP, or Windows Server 2003.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000281 RID: 641 RVA: 0x000102A0 File Offset: 0x0000E4A0
		public void SetValue(string name, object value)
		{
			this.SetValue(name, value, RegistryValueKind.Unknown);
		}

		/// <summary>Sets the value of a name/value pair in the registry key, using the specified registry data type.</summary>
		/// <param name="name">The name of the value to be stored. </param>
		/// <param name="value">The data to be stored. </param>
		/// <param name="valueKind">The registry data type to use when storing the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of <paramref name="value" /> did not match the registry data type specified by <paramref name="valueKind" />, therefore the data could not be converted properly. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and cannot be written to; for example, the key has not been opened with write access.-or-The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows Millennium Edition or Windows 98. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows 2000, Windows XP, or Windows Server 2003.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000282 RID: 642 RVA: 0x000102AC File Offset: 0x0000E4AC
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			if (name != null && name.Length > 16383)
			{
				throw new ArgumentException("Registry value names should not be greater than 16,383 characters.", "name");
			}
			if (!Enum.IsDefined(typeof(RegistryValueKind), valueKind))
			{
				throw new ArgumentException("The specified RegistryValueKind is an invalid value.", "valueKind");
			}
			this.EnsureWriteable();
			if (valueKind == RegistryValueKind.Unknown)
			{
				valueKind = this.CalculateValueKind(value);
			}
			this.SetValueCore(name, value, valueKind);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00010328 File Offset: 0x0000E528
		private RegistryValueKind CalculateValueKind(object value)
		{
			if (value is int)
			{
				return RegistryValueKind.DWord;
			}
			if (!(value is Array))
			{
				return RegistryValueKind.String;
			}
			if (value is byte[])
			{
				return RegistryValueKind.Binary;
			}
			if (value is string[])
			{
				return RegistryValueKind.MultiString;
			}
			throw new ArgumentException(SR.Format("RegistryKey.SetValue does not support arrays of type '{0}'. Only Byte[] and String[] are supported.", value.GetType().Name));
		}

		/// <summary>Retrieves a string representation of this key.</summary>
		/// <returns>A string representing the key. If the specified key is invalid (cannot be found) then null is returned.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being accessed is closed (closed keys cannot be accessed). </exception>
		// Token: 0x06000284 RID: 644 RVA: 0x00010290 File Offset: 0x0000E490
		public override string ToString()
		{
			this.EnsureNotDisposed();
			return this._keyName;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00010378 File Offset: 0x0000E578
		private static string FixupName(string name)
		{
			if (name.IndexOf('\\') == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			RegistryKey.FixupPath(stringBuilder);
			int num = stringBuilder.Length - 1;
			if (num >= 0 && stringBuilder[num] == '\\')
			{
				stringBuilder.Length = num;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000103C4 File Offset: 0x0000E5C4
		private static void FixupPath(StringBuilder path)
		{
			int length = path.Length;
			bool flag = false;
			char maxValue = char.MaxValue;
			for (int i = 1; i < length - 1; i++)
			{
				if (path[i] == '\\')
				{
					i++;
					while (i < length && path[i] == '\\')
					{
						path[i] = maxValue;
						i++;
						flag = true;
					}
				}
			}
			if (flag)
			{
				int i = 0;
				int num = 0;
				while (i < length)
				{
					if (path[i] == maxValue)
					{
						i++;
					}
					else
					{
						path[num] = path[i];
						i++;
						num++;
					}
				}
				path.Length += num - i;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00010464 File Offset: 0x0000E664
		private void EnsureNotDisposed()
		{
			if (this._hkey == null)
			{
				ThrowHelper.ThrowObjectDisposedException(this._keyName, "Cannot access a closed registry key.");
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00010482 File Offset: 0x0000E682
		private void EnsureWriteable()
		{
			this.EnsureNotDisposed();
			if (!this.IsWritable())
			{
				ThrowHelper.ThrowUnauthorizedAccessException("Cannot write to the registry key.");
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001049C File Offset: 0x0000E69C
		private RegistryKeyPermissionCheck GetSubKeyPermissionCheck(bool subkeyWritable)
		{
			if (this._checkMode == RegistryKeyPermissionCheck.Default)
			{
				return this._checkMode;
			}
			if (subkeyWritable)
			{
				return RegistryKeyPermissionCheck.ReadWriteSubTree;
			}
			return RegistryKeyPermissionCheck.ReadSubTree;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000104B8 File Offset: 0x0000E6B8
		private static void ValidateKeyName(string name)
		{
			if (name == null)
			{
				ThrowHelper.ThrowArgumentNullException("name");
			}
			int num = name.IndexOf("\\", StringComparison.OrdinalIgnoreCase);
			int num2 = 0;
			while (num != -1)
			{
				if (num - num2 > 255)
				{
					ThrowHelper.ThrowArgumentException("Registry key names should not be greater than 255 characters.", "name");
				}
				num2 = num + 1;
				num = name.IndexOf("\\", num2, StringComparison.OrdinalIgnoreCase);
			}
			if (name.Length - num2 > 255)
			{
				ThrowHelper.ThrowArgumentException("Registry key names should not be greater than 255 characters.", "name");
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010530 File Offset: 0x0000E730
		private static void ValidateKeyMode(RegistryKeyPermissionCheck mode)
		{
			if (mode < RegistryKeyPermissionCheck.Default || mode > RegistryKeyPermissionCheck.ReadWriteSubTree)
			{
				ThrowHelper.ThrowArgumentException("The specified RegistryKeyPermissionCheck value is invalid.", "mode");
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010549 File Offset: 0x0000E749
		private static void ValidateKeyOptions(RegistryOptions options)
		{
			if (options < RegistryOptions.None || options > RegistryOptions.Volatile)
			{
				ThrowHelper.ThrowArgumentException("The specified RegistryOptions value is invalid.", "options");
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010562 File Offset: 0x0000E762
		private static void ValidateKeyView(RegistryView view)
		{
			if (view != RegistryView.Default && view != RegistryView.Registry32 && view != RegistryView.Registry64)
			{
				ThrowHelper.ThrowArgumentException("The specified RegistryView value is invalid.", "view");
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010586 File Offset: 0x0000E786
		private bool IsSystemKey()
		{
			return (this._state & RegistryKey.StateFlags.SystemKey) > (RegistryKey.StateFlags)0;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010595 File Offset: 0x0000E795
		private bool IsWritable()
		{
			return (this._state & RegistryKey.StateFlags.WriteAccess) > (RegistryKey.StateFlags)0;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000105A4 File Offset: 0x0000E7A4
		private bool IsPerfDataKey()
		{
			return (this._state & RegistryKey.StateFlags.PerfData) > (RegistryKey.StateFlags)0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000105B3 File Offset: 0x0000E7B3
		private void SetDirty()
		{
			this._state |= RegistryKey.StateFlags.Dirty;
		}

		// Token: 0x04000246 RID: 582
		internal static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(int.MinValue);

		// Token: 0x04000247 RID: 583
		internal static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);

		// Token: 0x04000248 RID: 584
		internal static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);

		// Token: 0x04000249 RID: 585
		internal static readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);

		// Token: 0x0400024A RID: 586
		internal static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);

		// Token: 0x0400024B RID: 587
		internal static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);

		// Token: 0x0400024C RID: 588
		internal static readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);

		// Token: 0x0400024D RID: 589
		private static readonly string[] s_hkeyNames = new string[] { "HKEY_CLASSES_ROOT", "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", "HKEY_USERS", "HKEY_PERFORMANCE_DATA", "HKEY_CURRENT_CONFIG", "HKEY_DYN_DATA" };

		// Token: 0x0400024E RID: 590
		private volatile SafeRegistryHandle _hkey;

		// Token: 0x0400024F RID: 591
		private volatile string _keyName;

		// Token: 0x04000250 RID: 592
		private volatile bool _remoteKey;

		// Token: 0x04000251 RID: 593
		private volatile RegistryKey.StateFlags _state;

		// Token: 0x04000252 RID: 594
		private volatile RegistryKeyPermissionCheck _checkMode;

		// Token: 0x04000253 RID: 595
		private volatile RegistryView _regView;

		// Token: 0x02000080 RID: 128
		[Flags]
		private enum StateFlags
		{
			// Token: 0x04000255 RID: 597
			Dirty = 1,
			// Token: 0x04000256 RID: 598
			SystemKey = 2,
			// Token: 0x04000257 RID: 599
			WriteAccess = 4,
			// Token: 0x04000258 RID: 600
			PerfData = 8
		}
	}
}

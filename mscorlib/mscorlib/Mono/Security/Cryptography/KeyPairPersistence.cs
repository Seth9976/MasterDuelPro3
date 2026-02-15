using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006D RID: 109
	internal class KeyPairPersistence
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000A760 File Offset: 0x00008960
		public KeyPairPersistence(CspParameters parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A76A File Offset: 0x0000896A
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000A794 File Offset: 0x00008994
		public string Filename
		{
			get
			{
				if (this._filename == null)
				{
					this._filename = string.Format(CultureInfo.InvariantCulture, "[{0}][{1}][{2}].xml", this._params.ProviderType, this.ContainerName, this._params.KeyNumber);
					if (this.UseMachineKeyStore)
					{
						this._filename = Path.Combine(KeyPairPersistence.MachinePath, this._filename);
					}
					else
					{
						this._filename = Path.Combine(KeyPairPersistence.UserPath, this._filename);
					}
				}
				return this._filename;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A820 File Offset: 0x00008A20
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000A828 File Offset: 0x00008A28
		public string KeyValue
		{
			get
			{
				return this._keyvalue;
			}
			set
			{
				if (this.CanChange)
				{
					this._keyvalue = value;
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A83C File Offset: 0x00008A3C
		public bool Load()
		{
			bool flag = File.Exists(this.Filename);
			if (flag)
			{
				using (StreamReader streamReader = File.OpenText(this.Filename))
				{
					this.FromXml(streamReader.ReadToEnd());
				}
			}
			return flag;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A890 File Offset: 0x00008A90
		public void Save()
		{
			using (FileStream fileStream = File.Open(this.Filename, FileMode.Create))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(this.ToXml());
				streamWriter.Close();
			}
			if (this.UseMachineKeyStore)
			{
				KeyPairPersistence.ProtectMachine(this.Filename);
				return;
			}
			KeyPairPersistence.ProtectUser(this.Filename);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A904 File Offset: 0x00008B04
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A914 File Offset: 0x00008B14
		private static string UserPath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._userPath == null || !KeyPairPersistence._userPathExists)
					{
						KeyPairPersistence._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						KeyPairPersistence._userPath = Path.Combine(KeyPairPersistence._userPath, "keypairs");
						KeyPairPersistence._userPathExists = Directory.Exists(KeyPairPersistence._userPath);
						if (!KeyPairPersistence._userPathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._userPath);
							}
							catch (Exception ex)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create user key store '{0}'."), KeyPairPersistence._userPath), ex);
							}
							KeyPairPersistence._userPathExists = true;
						}
					}
					if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath) && !KeyPairPersistence.ProtectUser(KeyPairPersistence._userPath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure user key store '{0}'."), KeyPairPersistence._userPath));
					}
				}
				if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected user's key pairs in '{0}'."), KeyPairPersistence._userPath));
				}
				return KeyPairPersistence._userPath;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000AA34 File Offset: 0x00008C34
		private static string MachinePath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._machinePath == null || !KeyPairPersistence._machinePathExists)
					{
						KeyPairPersistence._machinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
						KeyPairPersistence._machinePath = Path.Combine(KeyPairPersistence._machinePath, "keypairs");
						KeyPairPersistence._machinePathExists = Directory.Exists(KeyPairPersistence._machinePath);
						if (!KeyPairPersistence._machinePathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._machinePath);
							}
							catch (Exception ex)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create machine key store '{0}'."), KeyPairPersistence._machinePath), ex);
							}
							KeyPairPersistence._machinePathExists = true;
						}
					}
					if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath) && !KeyPairPersistence.ProtectMachine(KeyPairPersistence._machinePath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure machine key store '{0}'."), KeyPairPersistence._machinePath));
					}
				}
				if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected machine's key pairs in '{0}'."), KeyPairPersistence._machinePath));
				}
				return KeyPairPersistence._machinePath;
			}
		}

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _CanSecure(char* root);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _ProtectUser(char* path);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _ProtectMachine(char* path);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _IsUserProtected(char* path);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _IsMachineProtected(char* path);

		// Token: 0x060001AF RID: 431 RVA: 0x0000AB54 File Offset: 0x00008D54
		private unsafe static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return true;
			}
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return KeyPairPersistence._CanSecure(ptr);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000AB94 File Offset: 0x00008D94
		private unsafe static bool ProtectUser(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._ProtectUser(ptr);
			}
			return true;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		private unsafe static bool ProtectMachine(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._ProtectMachine(ptr);
			}
			return true;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		private unsafe static bool IsUserProtected(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._IsUserProtected(ptr);
			}
			return true;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000AC24 File Offset: 0x00008E24
		private unsafe static bool IsMachineProtected(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._IsMachineProtected(ptr);
			}
			return true;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000AC51 File Offset: 0x00008E51
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000AC5C File Offset: 0x00008E5C
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000AC6E File Offset: 0x00008E6E
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000AC80 File Offset: 0x00008E80
		private string ContainerName
		{
			get
			{
				if (this._container == null)
				{
					if (this.UseDefaultKeyContainer)
					{
						this._container = "default";
					}
					else if (this._params.KeyContainerName == null || this._params.KeyContainerName.Length == 0)
					{
						this._container = Guid.NewGuid().ToString();
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this._params.KeyContainerName);
						byte[] array = MD5.Create().ComputeHash(bytes);
						this._container = new Guid(array).ToString();
					}
				}
				return this._container;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AD29 File Offset: 0x00008F29
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AD5C File Offset: 0x00008F5C
		private void FromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag == "KeyPair")
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("KeyValue");
				if (securityElement2.Children.Count > 0)
				{
					this._keyvalue = securityElement2.Children[0].ToString();
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		private string ToXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<KeyPair>{0}\t<Properties>{0}\t\t<Provider ", Environment.NewLine);
			if (this._params.ProviderName != null && this._params.ProviderName.Length != 0)
			{
				stringBuilder.AppendFormat("Name=\"{0}\" ", this._params.ProviderName);
			}
			stringBuilder.AppendFormat("Type=\"{0}\" />{1}\t\t<Container ", this._params.ProviderType, Environment.NewLine);
			stringBuilder.AppendFormat("Name=\"{0}\" />{1}\t</Properties>{1}\t<KeyValue", this.ContainerName, Environment.NewLine);
			if (this._params.KeyNumber != -1)
			{
				stringBuilder.AppendFormat(" Id=\"{0}\" ", this._params.KeyNumber);
			}
			stringBuilder.AppendFormat(">{1}\t\t{0}{1}\t</KeyValue>{1}</KeyPair>{1}", this.KeyValue, Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040001F3 RID: 499
		private static bool _userPathExists;

		// Token: 0x040001F4 RID: 500
		private static string _userPath;

		// Token: 0x040001F5 RID: 501
		private static bool _machinePathExists;

		// Token: 0x040001F6 RID: 502
		private static string _machinePath;

		// Token: 0x040001F7 RID: 503
		private CspParameters _params;

		// Token: 0x040001F8 RID: 504
		private string _keyvalue;

		// Token: 0x040001F9 RID: 505
		private string _filename;

		// Token: 0x040001FA RID: 506
		private string _container;

		// Token: 0x040001FB RID: 507
		private static object lockobj = new object();
	}
}

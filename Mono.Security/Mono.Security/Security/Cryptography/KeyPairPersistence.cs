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
	// Token: 0x0200004B RID: 75
	public class KeyPairPersistence
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00009ED0 File Offset: 0x000080D0
		public KeyPairPersistence(CspParameters parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009EDA File Offset: 0x000080DA
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009F04 File Offset: 0x00008104
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

		// Token: 0x06000190 RID: 400 RVA: 0x00009F90 File Offset: 0x00008190
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009FE4 File Offset: 0x000081E4
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000A104 File Offset: 0x00008304
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

		// Token: 0x06000193 RID: 403 RVA: 0x00009861 File Offset: 0x00007A61
		internal unsafe static bool _CanSecure(char* root)
		{
			return true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009861 File Offset: 0x00007A61
		internal unsafe static bool _ProtectUser(char* path)
		{
			return true;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009861 File Offset: 0x00007A61
		internal unsafe static bool _ProtectMachine(char* path)
		{
			return true;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009861 File Offset: 0x00007A61
		internal unsafe static bool _IsUserProtected(char* path)
		{
			return true;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009861 File Offset: 0x00007A61
		internal unsafe static bool _IsMachineProtected(char* path)
		{
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000A224 File Offset: 0x00008424
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

		// Token: 0x06000199 RID: 409 RVA: 0x0000A264 File Offset: 0x00008464
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

		// Token: 0x0600019A RID: 410 RVA: 0x0000A294 File Offset: 0x00008494
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

		// Token: 0x0600019B RID: 411 RVA: 0x0000A2C4 File Offset: 0x000084C4
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

		// Token: 0x0600019C RID: 412 RVA: 0x0000A2F4 File Offset: 0x000084F4
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000A321 File Offset: 0x00008521
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000A333 File Offset: 0x00008533
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A348 File Offset: 0x00008548
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

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A3F1 File Offset: 0x000085F1
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A424 File Offset: 0x00008624
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

		// Token: 0x040001FC RID: 508
		private static bool _userPathExists;

		// Token: 0x040001FD RID: 509
		private static string _userPath;

		// Token: 0x040001FE RID: 510
		private static bool _machinePathExists;

		// Token: 0x040001FF RID: 511
		private static string _machinePath;

		// Token: 0x04000200 RID: 512
		private CspParameters _params;

		// Token: 0x04000201 RID: 513
		private string _keyvalue;

		// Token: 0x04000202 RID: 514
		private string _filename;

		// Token: 0x04000203 RID: 515
		private string _container;

		// Token: 0x04000204 RID: 516
		private static object lockobj = new object();
	}
}

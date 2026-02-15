using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	public struct InputDeviceDescription : IEquatable<InputDeviceDescription>
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0005A062 File Offset: 0x00058262
		public string interfaceName
		{
			get
			{
				return this.m_InterfaceName;
			}
			set
			{
				this.m_InterfaceName = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005A06B File Offset: 0x0005826B
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0005A073 File Offset: 0x00058273
		public string deviceClass
		{
			get
			{
				return this.m_DeviceClass;
			}
			set
			{
				this.m_DeviceClass = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0005A07C File Offset: 0x0005827C
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0005A084 File Offset: 0x00058284
		public string manufacturer
		{
			get
			{
				return this.m_Manufacturer;
			}
			set
			{
				this.m_Manufacturer = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0005A08D File Offset: 0x0005828D
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x0005A095 File Offset: 0x00058295
		public string product
		{
			get
			{
				return this.m_Product;
			}
			set
			{
				this.m_Product = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0005A09E File Offset: 0x0005829E
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x0005A0A6 File Offset: 0x000582A6
		public string serial
		{
			get
			{
				return this.m_Serial;
			}
			set
			{
				this.m_Serial = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0005A0AF File Offset: 0x000582AF
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0005A0B7 File Offset: 0x000582B7
		public string version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005A0C0 File Offset: 0x000582C0
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0005A0C8 File Offset: 0x000582C8
		public string capabilities
		{
			get
			{
				return this.m_Capabilities;
			}
			set
			{
				this.m_Capabilities = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0005A0D4 File Offset: 0x000582D4
		public bool empty
		{
			get
			{
				return string.IsNullOrEmpty(this.m_InterfaceName) && string.IsNullOrEmpty(this.m_DeviceClass) && string.IsNullOrEmpty(this.m_Manufacturer) && string.IsNullOrEmpty(this.m_Product) && string.IsNullOrEmpty(this.m_Serial) && string.IsNullOrEmpty(this.m_Version) && string.IsNullOrEmpty(this.m_Capabilities);
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0005A13C File Offset: 0x0005833C
		public override string ToString()
		{
			bool haveProduct = !string.IsNullOrEmpty(this.product);
			bool haveManufacturer = !string.IsNullOrEmpty(this.manufacturer);
			bool haveInterface = !string.IsNullOrEmpty(this.interfaceName);
			if (haveProduct && haveManufacturer)
			{
				if (haveInterface)
				{
					return string.Concat(new string[] { this.manufacturer, " ", this.product, " (", this.interfaceName, ")" });
				}
				return this.manufacturer + " " + this.product;
			}
			else if (haveProduct)
			{
				if (haveInterface)
				{
					return this.product + " (" + this.interfaceName + ")";
				}
				return this.product;
			}
			else if (!string.IsNullOrEmpty(this.deviceClass))
			{
				if (haveInterface)
				{
					return this.deviceClass + " (" + this.interfaceName + ")";
				}
				return this.deviceClass;
			}
			else if (!string.IsNullOrEmpty(this.capabilities))
			{
				string caps = this.capabilities;
				if (this.capabilities.Length > 40)
				{
					caps = caps.Substring(0, 40) + "...";
				}
				if (haveInterface)
				{
					return caps + " (" + this.interfaceName + ")";
				}
				return caps;
			}
			else
			{
				if (haveInterface)
				{
					return this.interfaceName;
				}
				return "<Empty Device Description>";
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0005A294 File Offset: 0x00058494
		public bool Equals(InputDeviceDescription other)
		{
			return this.m_InterfaceName.InvariantEqualsIgnoreCase(other.m_InterfaceName) && this.m_DeviceClass.InvariantEqualsIgnoreCase(other.m_DeviceClass) && this.m_Manufacturer.InvariantEqualsIgnoreCase(other.m_Manufacturer) && this.m_Product.InvariantEqualsIgnoreCase(other.m_Product) && this.m_Serial.InvariantEqualsIgnoreCase(other.m_Serial) && this.m_Version.InvariantEqualsIgnoreCase(other.m_Version) && this.m_Capabilities.InvariantEqualsIgnoreCase(other.m_Capabilities);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0005A328 File Offset: 0x00058528
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputDeviceDescription)
			{
				InputDeviceDescription description = (InputDeviceDescription)obj;
				return this.Equals(description);
			}
			return false;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0005A354 File Offset: 0x00058554
		public override int GetHashCode()
		{
			return (((((((((((((this.m_InterfaceName != null) ? this.m_InterfaceName.GetHashCode() : 0) * 397) ^ ((this.m_DeviceClass != null) ? this.m_DeviceClass.GetHashCode() : 0)) * 397) ^ ((this.m_Manufacturer != null) ? this.m_Manufacturer.GetHashCode() : 0)) * 397) ^ ((this.m_Product != null) ? this.m_Product.GetHashCode() : 0)) * 397) ^ ((this.m_Serial != null) ? this.m_Serial.GetHashCode() : 0)) * 397) ^ ((this.m_Version != null) ? this.m_Version.GetHashCode() : 0)) * 397) ^ ((this.m_Capabilities != null) ? this.m_Capabilities.GetHashCode() : 0);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0005A425 File Offset: 0x00058625
		public static bool operator ==(InputDeviceDescription left, InputDeviceDescription right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0005A42F File Offset: 0x0005862F
		public static bool operator !=(InputDeviceDescription left, InputDeviceDescription right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0005A43C File Offset: 0x0005863C
		public string ToJson()
		{
			return JsonUtility.ToJson(new InputDeviceDescription.DeviceDescriptionJson
			{
				@interface = this.interfaceName,
				type = this.deviceClass,
				product = this.product,
				manufacturer = this.manufacturer,
				serial = this.serial,
				version = this.version,
				capabilities = this.capabilities
			}, true);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0005A4B8 File Offset: 0x000586B8
		public static InputDeviceDescription FromJson(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			InputDeviceDescription.DeviceDescriptionJson data = JsonUtility.FromJson<InputDeviceDescription.DeviceDescriptionJson>(json);
			return new InputDeviceDescription
			{
				interfaceName = data.@interface,
				deviceClass = data.type,
				product = data.product,
				manufacturer = data.manufacturer,
				serial = data.serial,
				version = data.version,
				capabilities = data.capabilities
			};
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0005A540 File Offset: 0x00058740
		internal static bool ComparePropertyToDeviceDescriptor(string propertyName, JsonParser.JsonString propertyValue, string deviceDescriptor)
		{
			JsonParser json = new JsonParser(deviceDescriptor);
			if (!json.NavigateToProperty(propertyName))
			{
				return propertyValue.text.isEmpty;
			}
			return json.CurrentPropertyHasValueEqualTo(propertyValue);
		}

		// Token: 0x04000B9A RID: 2970
		[SerializeField]
		private string m_InterfaceName;

		// Token: 0x04000B9B RID: 2971
		[SerializeField]
		private string m_DeviceClass;

		// Token: 0x04000B9C RID: 2972
		[SerializeField]
		private string m_Manufacturer;

		// Token: 0x04000B9D RID: 2973
		[SerializeField]
		private string m_Product;

		// Token: 0x04000B9E RID: 2974
		[SerializeField]
		private string m_Serial;

		// Token: 0x04000B9F RID: 2975
		[SerializeField]
		private string m_Version;

		// Token: 0x04000BA0 RID: 2976
		[SerializeField]
		private string m_Capabilities;

		// Token: 0x0200020D RID: 525
		private struct DeviceDescriptionJson
		{
			// Token: 0x04000BA1 RID: 2977
			public string @interface;

			// Token: 0x04000BA2 RID: 2978
			public string type;

			// Token: 0x04000BA3 RID: 2979
			public string product;

			// Token: 0x04000BA4 RID: 2980
			public string serial;

			// Token: 0x04000BA5 RID: 2981
			public string version;

			// Token: 0x04000BA6 RID: 2982
			public string manufacturer;

			// Token: 0x04000BA7 RID: 2983
			public string capabilities;
		}
	}
}

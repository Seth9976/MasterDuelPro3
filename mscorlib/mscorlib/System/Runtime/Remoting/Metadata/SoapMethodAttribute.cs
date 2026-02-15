using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a method. This class cannot be inherited.</summary>
	// Token: 0x0200046D RID: 1133
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00096768 File Offset: 0x00094968
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of remote method calls of the target method.</summary>
		/// <returns>The XML namespace that is used during serialization of remote method calls of the target method.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00096770 File Offset: 0x00094970
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00096778 File Offset: 0x00094978
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x040011A7 RID: 4519
		private string _responseElement;

		// Token: 0x040011A8 RID: 4520
		private string _responseNamespace;

		// Token: 0x040011A9 RID: 4521
		private string _returnElement;

		// Token: 0x040011AA RID: 4522
		private string _soapAction;

		// Token: 0x040011AB RID: 4523
		private bool _useAttribute;

		// Token: 0x040011AC RID: 4524
		private string _namespace;
	}
}

/*
 * Copyright 2012 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.IO;
using System.Xml;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// Utility class to inspect soap request and response messages for a WCF service endpoint.
    /// </summary>
    public class SoapInspector
    {
        /// <summary>
        /// Specifies the endpoint to be inspected.
        /// </summary>
        /// <param name="endpoint">The service endpoint on which soap messages is to be inspected.</param>
        /// <param name="soapMessages">A SoapMessages instance which will contain the soap request and response messages on the endpoint.</param>
        public static void InspectEndpoint(ServiceEndpoint endpoint, SoapMessages soapMessages)
        {
            InspectorBehavior newBehavior = new InspectorBehavior(soapMessages);
            endpoint.Behaviors.Add(newBehavior);
        }

        /// <summary>
        /// Internal class to contain the soap request and response messages.
        /// </summary>
        public class SoapMessages
        { 
            /// <summary>
            /// SoapRequest XML.
            /// </summary>
            public string SoapRequest { get; set; }

            /// <summary>
            /// SoapResponse XML.
            /// </summary>
            public string SoapResponse { get; set; }
        }
        
        /// <summary>
        /// Implementation of a MessageInspector which populates a SoapMessages instance with the
        /// soap request and response messages.
        /// </summary>
        internal class MessageInspector : IClientMessageInspector
        {
            SoapMessages soapMessages;

            public MessageInspector(SoapMessages soapMessages)
            {
                this.soapMessages = soapMessages;
            }

            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
                MessageBuffer msgBuffer = reply.CreateBufferedCopy(int.MaxValue);

                MemoryStream ms = new MemoryStream();
                msgBuffer.WriteMessage(ms);
                
                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                string output = sr.ReadToEnd();
                sr.Close();
                //File.WriteAllText("C:\\Temp\\SOAP_Response.xml", output);

                soapMessages.SoapResponse = output;

                reply = msgBuffer.CreateMessage();

                msgBuffer.Close();
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
            {
                
                soapMessages.SoapRequest = request.ToString();

                return null;
            }
        }

        /// <summary>
        /// Implementation of a behavior that instantiates a MessageInspector.
        /// </summary>
        internal class InspectorBehavior : IEndpointBehavior
        {
            SoapMessages soapMessages;

            public InspectorBehavior(SoapMessages soapMessages)
            {
                this.soapMessages = soapMessages;
            }

            public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
            {
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
            {
                clientRuntime.MessageInspectors.Add(new MessageInspector(soapMessages));
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
            {
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }
        }
    }
}

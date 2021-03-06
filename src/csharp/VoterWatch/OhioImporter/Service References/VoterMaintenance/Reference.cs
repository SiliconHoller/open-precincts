﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OhioImporter.VoterMaintenance {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://siliconholler.net/geopolitical", ConfigurationName="VoterMaintenance.IVoterMaintenance")]
    public interface IVoterMaintenance {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/addVoter", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/addVoterResponse")]
        VoterWatch.dataclasses.voter addVoter(VoterWatch.dataclasses.voter nvoter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/delete", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/deleteResponse")]
        bool delete(VoterWatch.dataclasses.voter nvoter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/updateVoter", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/updateVoterResponse")]
        bool updateVoter(VoterWatch.dataclasses.voter uvoter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/addAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/addAddressResponse")]
        VoterWatch.dataclasses.address addAddress(VoterWatch.dataclasses.address addr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/updateAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/updateAddressResponse")]
        bool updateAddress(VoterWatch.dataclasses.address addr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/removeAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/removeAddressResponse")]
        bool removeAddress(VoterWatch.dataclasses.address addr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/addVoterAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/addVoterAddressResponse")]
        bool addVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/removeVoterAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/removeVoterAddressRespons" +
            "e")]
        bool removeVoterAddress(int vid, int addrid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/updateVoterAddress", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/updateVoterAddressRespons" +
            "e")]
        bool updateVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://siliconholler.net/geopolitical/IVoterMaintenance/setVoterDistrict", ReplyAction="http://siliconholler.net/geopolitical/IVoterMaintenance/setVoterDistrictResponse")]
        bool setVoterDistrict(int vid, int distid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IVoterMaintenanceChannel : OhioImporter.VoterMaintenance.IVoterMaintenance, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VoterMaintenanceClient : System.ServiceModel.ClientBase<OhioImporter.VoterMaintenance.IVoterMaintenance>, OhioImporter.VoterMaintenance.IVoterMaintenance {
        
        public VoterMaintenanceClient() {
        }
        
        public VoterMaintenanceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VoterMaintenanceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VoterMaintenanceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VoterMaintenanceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public VoterWatch.dataclasses.voter addVoter(VoterWatch.dataclasses.voter nvoter) {
            return base.Channel.addVoter(nvoter);
        }
        
        public bool delete(VoterWatch.dataclasses.voter nvoter) {
            return base.Channel.delete(nvoter);
        }
        
        public bool updateVoter(VoterWatch.dataclasses.voter uvoter) {
            return base.Channel.updateVoter(uvoter);
        }
        
        public VoterWatch.dataclasses.address addAddress(VoterWatch.dataclasses.address addr) {
            return base.Channel.addAddress(addr);
        }
        
        public bool updateAddress(VoterWatch.dataclasses.address addr) {
            return base.Channel.updateAddress(addr);
        }
        
        public bool removeAddress(VoterWatch.dataclasses.address addr) {
            return base.Channel.removeAddress(addr);
        }
        
        public bool addVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags) {
            return base.Channel.addVoterAddress(vid, addrid, tflags);
        }
        
        public bool removeVoterAddress(int vid, int addrid) {
            return base.Channel.removeVoterAddress(vid, addrid);
        }
        
        public bool updateVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags) {
            return base.Channel.updateVoterAddress(vid, addrid, tflags);
        }
        
        public bool setVoterDistrict(int vid, int distid) {
            return base.Channel.setVoterDistrict(vid, distid);
        }
    }
}

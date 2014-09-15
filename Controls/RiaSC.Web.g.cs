

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.ServiceModel.Web;

namespace RiaSC
{
    public sealed partial class WebContext : WebContextBase
    {

        #region Extensibility Method Definitions
        partial void OnCreated();

        #endregion
        public WebContext()
        {
            this.OnCreated();
        }
        public new static WebContext Current
        {
            get
            {
                return ((WebContext)(WebContextBase.Current));
            }
        }
    }
}
namespace RiaSC.Web
{
    public sealed partial class SCContext : DomainContext
    {

        #region Extensibility Method Definitions
        partial void OnCreated();

        #endregion
        public SCContext() :
            this(new WebDomainClient<ISCServiceContract>(new Uri("http://demos.devexpress.com/Services/RiaSC/RiaSC-Web-SCService.svc", UriKind.Absolute)))
        {
        }
        public SCContext(Uri serviceUri) :
            this(new WebDomainClient<ISCServiceContract>(serviceUri))
        {
        }
        public SCContext(DomainClient domainClient) :
            base(domainClient)
        {
            this.OnCreated();
        }
        public EntitySet<SCIssuesDemo> SCIssuesDemos
        {
            get
            {
                return base.EntityContainer.GetEntitySet<SCIssuesDemo>();
            }
        }
        public EntityQuery<SCIssuesDemo> GetSCIssuesDemoQuery()
        {
            this.ValidateMethod("GetSCIssuesDemoQuery", null);
            return base.CreateQuery<SCIssuesDemo>("GetSCIssuesDemo", null, false, true);
        }
        public EntityQuery<SCIssuesDemo> GetSCIssuesDemoByTechnologyQuery(string technologyName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("technologyName", technologyName);
            this.ValidateMethod("GetSCIssuesDemoByTechnologyQuery", parameters);
            return base.CreateQuery<SCIssuesDemo>("GetSCIssuesDemoByTechnology", parameters, false, true);
        }
        public InvokeOperation<string> GetSCIssuesDemoByTechnologyExtendedData(string technologyName, string extendedDataInfo, Action<InvokeOperation<string>> callback, object userState)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("technologyName", technologyName);
            parameters.Add("extendedDataInfo", extendedDataInfo);
            this.ValidateMethod("GetSCIssuesDemoByTechnologyExtendedData", parameters);
            return ((InvokeOperation<string>)(this.InvokeOperation("GetSCIssuesDemoByTechnologyExtendedData", typeof(string), parameters, true, callback, userState)));
        }
        public InvokeOperation<string> GetSCIssuesDemoByTechnologyExtendedData(string technologyName, string extendedDataInfo)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("technologyName", technologyName);
            parameters.Add("extendedDataInfo", extendedDataInfo);
            this.ValidateMethod("GetSCIssuesDemoByTechnologyExtendedData", parameters);
            return ((InvokeOperation<string>)(this.InvokeOperation("GetSCIssuesDemoByTechnologyExtendedData", typeof(string), parameters, true, null, null)));
        }
        public InvokeOperation<string> GetSCIssuesDemoExtendedData(string extendedDataInfo, Action<InvokeOperation<string>> callback, object userState)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("extendedDataInfo", extendedDataInfo);
            this.ValidateMethod("GetSCIssuesDemoExtendedData", parameters);
            return ((InvokeOperation<string>)(this.InvokeOperation("GetSCIssuesDemoExtendedData", typeof(string), parameters, true, callback, userState)));
        }
        public InvokeOperation<string> GetSCIssuesDemoExtendedData(string extendedDataInfo)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("extendedDataInfo", extendedDataInfo);
            this.ValidateMethod("GetSCIssuesDemoExtendedData", parameters);
            return ((InvokeOperation<string>)(this.InvokeOperation("GetSCIssuesDemoExtendedData", typeof(string), parameters, true, null, null)));
        }
        protected override EntityContainer CreateEntityContainer()
        {
            return new SCContextEntityContainer();
        }
        [ServiceContract()]
        public interface ISCServiceContract
        {
            [FaultContract(typeof(DomainServiceFault), Action = "http://tempuri.org/SCService/GetSCIssuesDemoDomainServiceFault", Name = "DomainServiceFault", Namespace = "DomainServices")]
            [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/SCService/GetSCIssuesDemo", ReplyAction = "http://tempuri.org/SCService/GetSCIssuesDemoResponse")]
            [WebGet()]
            IAsyncResult BeginGetSCIssuesDemo(AsyncCallback callback, object asyncState);
            QueryResult<SCIssuesDemo> EndGetSCIssuesDemo(IAsyncResult result);
            [FaultContract(typeof(DomainServiceFault), Action = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnologyDomainServiceFault", Name = "DomainServiceFault", Namespace = "DomainServices")]
            [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnology", ReplyAction = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnologyResponse")]
            [WebGet()]
            IAsyncResult BeginGetSCIssuesDemoByTechnology(string technologyName, AsyncCallback callback, object asyncState);
            QueryResult<SCIssuesDemo> EndGetSCIssuesDemoByTechnology(IAsyncResult result);
            [FaultContract(typeof(DomainServiceFault), Action = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnologyExtendedDataDomainService" +
                "Fault", Name = "DomainServiceFault", Namespace = "DomainServices")]
            [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnologyExtendedData", ReplyAction = "http://tempuri.org/SCService/GetSCIssuesDemoByTechnologyExtendedDataResponse")]
            IAsyncResult BeginGetSCIssuesDemoByTechnologyExtendedData(string technologyName, string extendedDataInfo, AsyncCallback callback, object asyncState);
            string EndGetSCIssuesDemoByTechnologyExtendedData(IAsyncResult result);
            [FaultContract(typeof(DomainServiceFault), Action = "http://tempuri.org/SCService/GetSCIssuesDemoExtendedDataDomainServiceFault", Name = "DomainServiceFault", Namespace = "DomainServices")]
            [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/SCService/GetSCIssuesDemoExtendedData", ReplyAction = "http://tempuri.org/SCService/GetSCIssuesDemoExtendedDataResponse")]
            IAsyncResult BeginGetSCIssuesDemoExtendedData(string extendedDataInfo, AsyncCallback callback, object asyncState);
            string EndGetSCIssuesDemoExtendedData(IAsyncResult result);
        }

        internal sealed class SCContextEntityContainer : EntityContainer
        {

            public SCContextEntityContainer()
            {
                this.CreateEntitySet<SCIssuesDemo>(EntitySetOperations.None);
            }
        }
    }
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/RiaSC.Web")]
    public sealed partial class SCIssuesDemo : Entity
    {

        private Nullable<DateTime> _createdOn;

        private string _id;

        private Nullable<DateTime> _modifiedOn;

        private Guid _oid;

        private string _productName;

        private string _status;

        private string _subject;

        private string _technologyName;

        private Nullable<bool> _urgent;

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void OnCreatedOnChanging(Nullable<DateTime> value);
        partial void OnCreatedOnChanged();
        partial void OnIDChanging(string value);
        partial void OnIDChanged();
        partial void OnModifiedOnChanging(Nullable<DateTime> value);
        partial void OnModifiedOnChanged();
        partial void OnOidChanging(Guid value);
        partial void OnOidChanged();
        partial void OnProductNameChanging(string value);
        partial void OnProductNameChanged();
        partial void OnStatusChanging(string value);
        partial void OnStatusChanged();
        partial void OnSubjectChanging(string value);
        partial void OnSubjectChanged();
        partial void OnTechnologyNameChanging(string value);
        partial void OnTechnologyNameChanged();
        partial void OnUrgentChanging(Nullable<bool> value);
        partial void OnUrgentChanged();

        #endregion
        public SCIssuesDemo()
        {
            this.OnCreated();
        }
        [DataMember()]
        public Nullable<DateTime> CreatedOn
        {
            get
            {
                return this._createdOn;
            }
            set
            {
                if ((this._createdOn != value))
                {
                    this.OnCreatedOnChanging(value);
                    this.RaiseDataMemberChanging("CreatedOn");
                    this.ValidateProperty("CreatedOn", value);
                    this._createdOn = value;
                    this.RaiseDataMemberChanged("CreatedOn");
                    this.OnCreatedOnChanged();
                }
            }
        }
        [DataMember()]
        [StringLength(100)]
        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    this.OnIDChanging(value);
                    this.RaiseDataMemberChanging("ID");
                    this.ValidateProperty("ID", value);
                    this._id = value;
                    this.RaiseDataMemberChanged("ID");
                    this.OnIDChanged();
                }
            }
        }
        [DataMember()]
        public Nullable<DateTime> ModifiedOn
        {
            get
            {
                return this._modifiedOn;
            }
            set
            {
                if ((this._modifiedOn != value))
                {
                    this.OnModifiedOnChanging(value);
                    this.RaiseDataMemberChanging("ModifiedOn");
                    this.ValidateProperty("ModifiedOn", value);
                    this._modifiedOn = value;
                    this.RaiseDataMemberChanged("ModifiedOn");
                    this.OnModifiedOnChanged();
                }
            }
        }
        [DataMember()]
        [Editable(false, AllowInitialValue = true)]
        [Key()]
        [RoundtripOriginal()]
        public Guid Oid
        {
            get
            {
                return this._oid;
            }
            set
            {
                if ((this._oid != value))
                {
                    this.OnOidChanging(value);
                    this.ValidateProperty("Oid", value);
                    this._oid = value;
                    this.RaisePropertyChanged("Oid");
                    this.OnOidChanged();
                }
            }
        }
        [DataMember()]
        [StringLength(100)]
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                if ((this._productName != value))
                {
                    this.OnProductNameChanging(value);
                    this.RaiseDataMemberChanging("ProductName");
                    this.ValidateProperty("ProductName", value);
                    this._productName = value;
                    this.RaiseDataMemberChanged("ProductName");
                    this.OnProductNameChanged();
                }
            }
        }
        [DataMember()]
        [StringLength(100)]
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if ((this._status != value))
                {
                    this.OnStatusChanging(value);
                    this.RaiseDataMemberChanging("Status");
                    this.ValidateProperty("Status", value);
                    this._status = value;
                    this.RaiseDataMemberChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }
        [DataMember()]
        [StringLength(255)]
        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                if ((this._subject != value))
                {
                    this.OnSubjectChanging(value);
                    this.RaiseDataMemberChanging("Subject");
                    this.ValidateProperty("Subject", value);
                    this._subject = value;
                    this.RaiseDataMemberChanged("Subject");
                    this.OnSubjectChanged();
                }
            }
        }
        [DataMember()]
        [StringLength(100)]
        public string TechnologyName
        {
            get
            {
                return this._technologyName;
            }
            set
            {
                if ((this._technologyName != value))
                {
                    this.OnTechnologyNameChanging(value);
                    this.RaiseDataMemberChanging("TechnologyName");
                    this.ValidateProperty("TechnologyName", value);
                    this._technologyName = value;
                    this.RaiseDataMemberChanged("TechnologyName");
                    this.OnTechnologyNameChanged();
                }
            }
        }
        [DataMember()]
        public Nullable<bool> Urgent
        {
            get
            {
                return this._urgent;
            }
            set
            {
                if ((this._urgent != value))
                {
                    this.OnUrgentChanging(value);
                    this.RaiseDataMemberChanging("Urgent");
                    this.ValidateProperty("Urgent", value);
                    this._urgent = value;
                    this.RaiseDataMemberChanged("Urgent");
                    this.OnUrgentChanged();
                }
            }
        }
        public override object GetIdentity()
        {
            return this._oid;
        }
    }
}

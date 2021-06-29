﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFJ.Repository.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WFJEntities : DbContext
    {
        public WFJEntities()
            : base("name=WFJEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AssociateCounsel> AssociateCounsels { get; set; }
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<clickCount> clickCounts { get; set; }
        public virtual DbSet<clientCollector> clientCollectors { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Clientsx> Clientsxes { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<columnInfo> columnInfos { get; set; }
        public virtual DbSet<contactType> contactTypes { get; set; }
        public virtual DbSet<currency> currencies { get; set; }
        public virtual DbSet<DocumentUsage> DocumentUsages { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<EMailCopy> EMailCopies { get; set; }
        public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }
        public virtual DbSet<EmployeeEvent> EmployeeEvents { get; set; }
        public virtual DbSet<EmployeeNew> EmployeeNews { get; set; }
        public virtual DbSet<EventCategory> EventCategories { get; set; }
        public virtual DbSet<EventDate> EventDates { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<FieldType> FieldTypes { get; set; }
        public virtual DbSet<FormAddressData> FormAddressDatas { get; set; }
        public virtual DbSet<FormData> FormDatas { get; set; }
        public virtual DbSet<formDataSheet> formDataSheets { get; set; }
        public virtual DbSet<FormField> FormFields { get; set; }
        public virtual DbSet<FormNotesUser> FormNotesUsers { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<formSection> formSections { get; set; }
        public virtual DbSet<FormSelectionList> FormSelectionLists { get; set; }
        public virtual DbSet<FormsX> FormsXes { get; set; }
        public virtual DbSet<FormType> FormTypes { get; set; }
        public virtual DbSet<FormUser> FormUsers { get; set; }
        public virtual DbSet<FormUserType> FormUserTypes { get; set; }
        public virtual DbSet<FunctionsTable> FunctionsTables { get; set; }
        public virtual DbSet<hrDocument> hrDocuments { get; set; }
        public virtual DbSet<LegalAssistant> LegalAssistants { get; set; }
        public virtual DbSet<Letter> Letters { get; set; }
        public virtual DbSet<LetterSchedule> LetterSchedules { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<ListField> ListFields { get; set; }
        public virtual DbSet<localCounselInvoice> localCounselInvoices { get; set; }
        public virtual DbSet<mailformResult> mailformResults { get; set; }
        public virtual DbSet<mailform> mailforms { get; set; }
        public virtual DbSet<mailingAd> mailingAds { get; set; }
        public virtual DbSet<mailingListName> mailingListNames { get; set; }
        public virtual DbSet<mailingList> mailingLists { get; set; }
        public virtual DbSet<mailing> mailings { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<PersonnelRequest> PersonnelRequests { get; set; }
        public virtual DbSet<picture> pictures { get; set; }
        public virtual DbSet<PracticeAreaPersonnel> PracticeAreaPersonnels { get; set; }
        public virtual DbSet<PracticeArea> PracticeAreas { get; set; }
        public virtual DbSet<PrepaidBenefit> PrepaidBenefits { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionnaireRespons> QuestionnaireResponses { get; set; }
        public virtual DbSet<QuestionnaireSection> QuestionnaireSections { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RequestDocument> RequestDocuments { get; set; }
        public virtual DbSet<RequestNotice> RequestNotices { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<SearchResult> SearchResults { get; set; }
        public virtual DbSet<StatusCode> StatusCodes { get; set; }
        public virtual DbSet<TableColumn> TableColumns { get; set; }
        public virtual DbSet<tableInfo> tableInfos { get; set; }
        public virtual DbSet<TableRow> TableRows { get; set; }
        public virtual DbSet<User_Function> User_Functions { get; set; }
        public virtual DbSet<User_Type> User_Types { get; set; }
        public virtual DbSet<UserLoginsTable> UserLoginsTables { get; set; }
        public virtual DbSet<UserRegion> UserRegions { get; set; }
        public virtual DbSet<UserStatusCodePermission> UserStatusCodePermissions { get; set; }
        public virtual DbSet<WebPage> WebPages { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<area> areas { get; set; }
        public virtual DbSet<distributionList> distributionLists { get; set; }
        public virtual DbSet<elementCategory> elementCategorys { get; set; }
        public virtual DbSet<element> elements { get; set; }
        public virtual DbSet<enews_clients> enews_clients { get; set; }
        public virtual DbSet<mailformType> mailformTypes { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<pageContent> pageContents { get; set; }
        public virtual DbSet<page> pages { get; set; }
        public virtual DbSet<sectionContent> sectionContents { get; set; }
        public virtual DbSet<section> sections { get; set; }
        public virtual DbSet<sequence> sequences { get; set; }
        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<UserClient> UserClients { get; set; }
        public virtual DbSet<UserLevel> UserLevels { get; set; }
        public virtual DbSet<documentClient> documentClients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<fieldSize> fieldSizes { get; set; }
        public virtual DbSet<hiddenRequestNote> hiddenRequestNotes { get; set; }
        public virtual DbSet<collectorStatusCode> collectorStatusCodes { get; set; }
        public virtual DbSet<RequestNote> RequestNotes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<ClientsLive> ClientsLives { get; set; }
        public virtual DbSet<localCounselStatus> localCounselStatuses { get; set; }
        public virtual DbSet<RecentAccountActivity> RecentAccountActivities { get; set; }
        public virtual DbSet<PersonnelClient> PersonnelClients { get; set; }
        public virtual DbSet<UserAttorney> UserAttorneys { get; set; }
    
        public virtual int dt_addtosourcecontrol(string vchSourceSafeINI, string vchProjectName, string vchComment, string vchLoginName, string vchPassword)
        {
            var vchSourceSafeINIParameter = vchSourceSafeINI != null ?
                new ObjectParameter("vchSourceSafeINI", vchSourceSafeINI) :
                new ObjectParameter("vchSourceSafeINI", typeof(string));
    
            var vchProjectNameParameter = vchProjectName != null ?
                new ObjectParameter("vchProjectName", vchProjectName) :
                new ObjectParameter("vchProjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_addtosourcecontrol", vchSourceSafeINIParameter, vchProjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter);
        }
    
        public virtual int dt_addtosourcecontrol_u(string vchSourceSafeINI, string vchProjectName, string vchComment, string vchLoginName, string vchPassword)
        {
            var vchSourceSafeINIParameter = vchSourceSafeINI != null ?
                new ObjectParameter("vchSourceSafeINI", vchSourceSafeINI) :
                new ObjectParameter("vchSourceSafeINI", typeof(string));
    
            var vchProjectNameParameter = vchProjectName != null ?
                new ObjectParameter("vchProjectName", vchProjectName) :
                new ObjectParameter("vchProjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_addtosourcecontrol_u", vchSourceSafeINIParameter, vchProjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter);
        }
    
        public virtual int dt_adduserobject()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_adduserobject");
        }
    
        public virtual int dt_adduserobject_vcs(string vchProperty)
        {
            var vchPropertyParameter = vchProperty != null ?
                new ObjectParameter("vchProperty", vchProperty) :
                new ObjectParameter("vchProperty", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_adduserobject_vcs", vchPropertyParameter);
        }
    
        public virtual int dt_checkinobject(string chObjectType, string vchObjectName, string vchComment, string vchLoginName, string vchPassword, Nullable<int> iVCSFlags, Nullable<int> iActionFlag, string txStream1, string txStream2, string txStream3)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iVCSFlagsParameter = iVCSFlags.HasValue ?
                new ObjectParameter("iVCSFlags", iVCSFlags) :
                new ObjectParameter("iVCSFlags", typeof(int));
    
            var iActionFlagParameter = iActionFlag.HasValue ?
                new ObjectParameter("iActionFlag", iActionFlag) :
                new ObjectParameter("iActionFlag", typeof(int));
    
            var txStream1Parameter = txStream1 != null ?
                new ObjectParameter("txStream1", txStream1) :
                new ObjectParameter("txStream1", typeof(string));
    
            var txStream2Parameter = txStream2 != null ?
                new ObjectParameter("txStream2", txStream2) :
                new ObjectParameter("txStream2", typeof(string));
    
            var txStream3Parameter = txStream3 != null ?
                new ObjectParameter("txStream3", txStream3) :
                new ObjectParameter("txStream3", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_checkinobject", chObjectTypeParameter, vchObjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter, iVCSFlagsParameter, iActionFlagParameter, txStream1Parameter, txStream2Parameter, txStream3Parameter);
        }
    
        public virtual int dt_checkinobject_u(string chObjectType, string vchObjectName, string vchComment, string vchLoginName, string vchPassword, Nullable<int> iVCSFlags, Nullable<int> iActionFlag, string txStream1, string txStream2, string txStream3)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iVCSFlagsParameter = iVCSFlags.HasValue ?
                new ObjectParameter("iVCSFlags", iVCSFlags) :
                new ObjectParameter("iVCSFlags", typeof(int));
    
            var iActionFlagParameter = iActionFlag.HasValue ?
                new ObjectParameter("iActionFlag", iActionFlag) :
                new ObjectParameter("iActionFlag", typeof(int));
    
            var txStream1Parameter = txStream1 != null ?
                new ObjectParameter("txStream1", txStream1) :
                new ObjectParameter("txStream1", typeof(string));
    
            var txStream2Parameter = txStream2 != null ?
                new ObjectParameter("txStream2", txStream2) :
                new ObjectParameter("txStream2", typeof(string));
    
            var txStream3Parameter = txStream3 != null ?
                new ObjectParameter("txStream3", txStream3) :
                new ObjectParameter("txStream3", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_checkinobject_u", chObjectTypeParameter, vchObjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter, iVCSFlagsParameter, iActionFlagParameter, txStream1Parameter, txStream2Parameter, txStream3Parameter);
        }
    
        public virtual int dt_checkoutobject(string chObjectType, string vchObjectName, string vchComment, string vchLoginName, string vchPassword, Nullable<int> iVCSFlags, Nullable<int> iActionFlag)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iVCSFlagsParameter = iVCSFlags.HasValue ?
                new ObjectParameter("iVCSFlags", iVCSFlags) :
                new ObjectParameter("iVCSFlags", typeof(int));
    
            var iActionFlagParameter = iActionFlag.HasValue ?
                new ObjectParameter("iActionFlag", iActionFlag) :
                new ObjectParameter("iActionFlag", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_checkoutobject", chObjectTypeParameter, vchObjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter, iVCSFlagsParameter, iActionFlagParameter);
        }
    
        public virtual int dt_checkoutobject_u(string chObjectType, string vchObjectName, string vchComment, string vchLoginName, string vchPassword, Nullable<int> iVCSFlags, Nullable<int> iActionFlag)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchCommentParameter = vchComment != null ?
                new ObjectParameter("vchComment", vchComment) :
                new ObjectParameter("vchComment", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iVCSFlagsParameter = iVCSFlags.HasValue ?
                new ObjectParameter("iVCSFlags", iVCSFlags) :
                new ObjectParameter("iVCSFlags", typeof(int));
    
            var iActionFlagParameter = iActionFlag.HasValue ?
                new ObjectParameter("iActionFlag", iActionFlag) :
                new ObjectParameter("iActionFlag", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_checkoutobject_u", chObjectTypeParameter, vchObjectNameParameter, vchCommentParameter, vchLoginNameParameter, vchPasswordParameter, iVCSFlagsParameter, iActionFlagParameter);
        }
    
        public virtual int dt_displayoaerror(Nullable<int> iObject, Nullable<int> iresult)
        {
            var iObjectParameter = iObject.HasValue ?
                new ObjectParameter("iObject", iObject) :
                new ObjectParameter("iObject", typeof(int));
    
            var iresultParameter = iresult.HasValue ?
                new ObjectParameter("iresult", iresult) :
                new ObjectParameter("iresult", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_displayoaerror", iObjectParameter, iresultParameter);
        }
    
        public virtual int dt_displayoaerror_u(Nullable<int> iObject, Nullable<int> iresult)
        {
            var iObjectParameter = iObject.HasValue ?
                new ObjectParameter("iObject", iObject) :
                new ObjectParameter("iObject", typeof(int));
    
            var iresultParameter = iresult.HasValue ?
                new ObjectParameter("iresult", iresult) :
                new ObjectParameter("iresult", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_displayoaerror_u", iObjectParameter, iresultParameter);
        }
    
        public virtual int dt_droppropertiesbyid(Nullable<int> id, string property)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_droppropertiesbyid", idParameter, propertyParameter);
        }
    
        public virtual int dt_dropuserobjectbyid(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_dropuserobjectbyid", idParameter);
        }
    
        public virtual int dt_generateansiname(ObjectParameter name)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_generateansiname", name);
        }
    
        public virtual ObjectResult<Nullable<int>> dt_getobjwithprop(string property, string value)
        {
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var valueParameter = value != null ?
                new ObjectParameter("value", value) :
                new ObjectParameter("value", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("dt_getobjwithprop", propertyParameter, valueParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> dt_getobjwithprop_u(string property, string uvalue)
        {
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var uvalueParameter = uvalue != null ?
                new ObjectParameter("uvalue", uvalue) :
                new ObjectParameter("uvalue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("dt_getobjwithprop_u", propertyParameter, uvalueParameter);
        }
    
        public virtual ObjectResult<dt_getpropertiesbyid_Result> dt_getpropertiesbyid(Nullable<int> id, string property)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dt_getpropertiesbyid_Result>("dt_getpropertiesbyid", idParameter, propertyParameter);
        }
    
        public virtual ObjectResult<dt_getpropertiesbyid_u_Result> dt_getpropertiesbyid_u(Nullable<int> id, string property)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dt_getpropertiesbyid_u_Result>("dt_getpropertiesbyid_u", idParameter, propertyParameter);
        }
    
        public virtual int dt_getpropertiesbyid_vcs(Nullable<int> id, string property, ObjectParameter value)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_getpropertiesbyid_vcs", idParameter, propertyParameter, value);
        }
    
        public virtual int dt_getpropertiesbyid_vcs_u(Nullable<int> id, string property, ObjectParameter value)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_getpropertiesbyid_vcs_u", idParameter, propertyParameter, value);
        }
    
        public virtual int dt_isundersourcecontrol(string vchLoginName, string vchPassword, Nullable<int> iWhoToo)
        {
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iWhoTooParameter = iWhoToo.HasValue ?
                new ObjectParameter("iWhoToo", iWhoToo) :
                new ObjectParameter("iWhoToo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_isundersourcecontrol", vchLoginNameParameter, vchPasswordParameter, iWhoTooParameter);
        }
    
        public virtual int dt_isundersourcecontrol_u(string vchLoginName, string vchPassword, Nullable<int> iWhoToo)
        {
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            var iWhoTooParameter = iWhoToo.HasValue ?
                new ObjectParameter("iWhoToo", iWhoToo) :
                new ObjectParameter("iWhoToo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_isundersourcecontrol_u", vchLoginNameParameter, vchPasswordParameter, iWhoTooParameter);
        }
    
        public virtual int dt_removefromsourcecontrol()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_removefromsourcecontrol");
        }
    
        public virtual int dt_setpropertybyid(Nullable<int> id, string property, string value, byte[] lvalue)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var valueParameter = value != null ?
                new ObjectParameter("value", value) :
                new ObjectParameter("value", typeof(string));
    
            var lvalueParameter = lvalue != null ?
                new ObjectParameter("lvalue", lvalue) :
                new ObjectParameter("lvalue", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_setpropertybyid", idParameter, propertyParameter, valueParameter, lvalueParameter);
        }
    
        public virtual int dt_setpropertybyid_u(Nullable<int> id, string property, string uvalue, byte[] lvalue)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var uvalueParameter = uvalue != null ?
                new ObjectParameter("uvalue", uvalue) :
                new ObjectParameter("uvalue", typeof(string));
    
            var lvalueParameter = lvalue != null ?
                new ObjectParameter("lvalue", lvalue) :
                new ObjectParameter("lvalue", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_setpropertybyid_u", idParameter, propertyParameter, uvalueParameter, lvalueParameter);
        }
    
        public virtual int dt_validateloginparams(string vchLoginName, string vchPassword)
        {
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_validateloginparams", vchLoginNameParameter, vchPasswordParameter);
        }
    
        public virtual int dt_validateloginparams_u(string vchLoginName, string vchPassword)
        {
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_validateloginparams_u", vchLoginNameParameter, vchPasswordParameter);
        }
    
        public virtual int dt_vcsenabled()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_vcsenabled");
        }
    
        public virtual ObjectResult<Nullable<int>> dt_verstamp006()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("dt_verstamp006");
        }
    
        public virtual ObjectResult<Nullable<int>> dt_verstamp007()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("dt_verstamp007");
        }
    
        public virtual int dt_whocheckedout(string chObjectType, string vchObjectName, string vchLoginName, string vchPassword)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_whocheckedout", chObjectTypeParameter, vchObjectNameParameter, vchLoginNameParameter, vchPasswordParameter);
        }
    
        public virtual int dt_whocheckedout_u(string chObjectType, string vchObjectName, string vchLoginName, string vchPassword)
        {
            var chObjectTypeParameter = chObjectType != null ?
                new ObjectParameter("chObjectType", chObjectType) :
                new ObjectParameter("chObjectType", typeof(string));
    
            var vchObjectNameParameter = vchObjectName != null ?
                new ObjectParameter("vchObjectName", vchObjectName) :
                new ObjectParameter("vchObjectName", typeof(string));
    
            var vchLoginNameParameter = vchLoginName != null ?
                new ObjectParameter("vchLoginName", vchLoginName) :
                new ObjectParameter("vchLoginName", typeof(string));
    
            var vchPasswordParameter = vchPassword != null ?
                new ObjectParameter("vchPassword", vchPassword) :
                new ObjectParameter("vchPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dt_whocheckedout_u", chObjectTypeParameter, vchObjectNameParameter, vchLoginNameParameter, vchPasswordParameter);
        }
    }
}

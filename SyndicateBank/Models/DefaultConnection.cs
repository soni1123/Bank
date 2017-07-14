using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.Areas.Admin.Models;


namespace SyndicateBank.Models
{
    public class DefaultConnection : DbContext
    {
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BranchModel> BranchMasters { get; set; }
        public virtual DbSet<BranchScrollStationeryModel> branchscrollstationery { get; set; }
        public virtual DbSet<BranchScrollTransactionModel> branchscrolltransaction{ get; set; }
        public virtual DbSet<BranchStockDetail_CustomerSell_MappingModel> BranchStockDetail_CustomerSell_Mapping { get; set; }
        public virtual DbSet<BranchTransferLogModel> BranchTransferLogs { get; set; }
        public virtual DbSet<CustomerSelllogModel> CustomerSelllogs { get; set; }
        public virtual DbSet<DistrictModel> DistrictMastes { get; set; }
        public virtual DbSet<RoleMasterModel> RoleMasters { get; set; }
        public virtual DbSet<StampDetailModel> StampDetails { get; set; }
        public virtual DbSet<StateModel> StateMasters { get; set; }
        public virtual DbSet<SupMainScrollStationeryModel> SupMainScrollStationary { get; set; }
        public virtual DbSet<UserRoleMappingModel> UserRoleMapping { get; set; }
        public virtual DbSet<UserDetailModel> UserDetails { get; set; }
        public virtual DbSet<UserLogDetailModel> UserLogDetails { get; set; }
        public virtual DbSet<UserLoginModel> UserLogins { get; set; }
        public virtual DbSet<ViewModel> ViewModels { get; set; }
        public virtual DbSet<CityModel> Cities { get; set; }
        public virtual DbSet<CountryModel> Countries { get; set; }
        public virtual DbSet<ReceiptTypeModel> ReceiptType { get; set; }
        public virtual DbSet<ArticleModel> Article { get; set; }
        public virtual DbSet<MovabilityModel> Movability { get; set; }
        public virtual DbSet<SupMainTransactionStationeryModel> SupMainTransactionStationerys { get; set; }

        public virtual DbSet<OfficeModel> Office { get; set; }

        public virtual DbSet<DutyPayerDetailModel> DutyPayerDetail { get; set; }
        public virtual DbSet<PropertyDetailModel> PropertyDetail { get; set; }
        public virtual DbSet<OtherPartyDetailModel> OtherPartyDetail { get; set; }
        public virtual DbSet<PaymentDetailsModel> PaymentDetails { get; set; }
    }
}
/*
 * Copyright 2013 NEHTA
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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
  /// <summary>
  /// Occupations
  /// </summary>
    [Serializable]
    [DataContract]
    public enum Occupation
    {
      /// <summary>
      /// Undefined, this is the default value if the enum is left unset.
      /// The validation engine uses this to test and assert that the enum has been set (if required)
      /// and is therefore valid.
      /// </summary>
        [EnumMember]
        Undefined,

        /// <summary>
        /// The managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "100000", Name = "Managers nfd", CodeSystem = "ANZSCO")]
        Managersnfd,
        /// <summary>
        /// The chief executives general managersand legislatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "111000", Name = "Chief Executives, General Managers and Legislators nfd", CodeSystem = "ANZSCO")]
        ChiefExecutivesGeneralManagersandLegislatorsnfd,
        /// <summary>
        /// The chief executiveor managing director
        /// </summary>
        [EnumMember]
        [Name(Code = "111111", Name = "Chief Executive or Managing Director", CodeSystem = "ANZSCO")]
        ChiefExecutiveorManagingDirector,
        /// <summary>
        /// The general managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "111200", Name = "General Managers nfd", CodeSystem = "ANZSCO")]
        GeneralManagersnfd,
        /// <summary>
        /// The corporate general manager
        /// </summary>
        [EnumMember]
        [Name(Code = "111211", Name = "Corporate General Manager", CodeSystem = "ANZSCO")]
        CorporateGeneralManager,
        /// <summary>
        /// The defence force senior officer
        /// </summary>
        [EnumMember]
        [Name(Code = "111212", Name = "Defence Force Senior Officer", CodeSystem = "ANZSCO")]
        DefenceForceSeniorOfficer,
        /// <summary>
        /// The legislatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "111300", Name = "Legislators nfd", CodeSystem = "ANZSCO")]
        Legislatorsnfd,
        /// <summary>
        /// The local government legislator
        /// </summary>
        [EnumMember]
        [Name(Code = "111311", Name = "Local Government Legislator", CodeSystem = "ANZSCO")]
        LocalGovernmentLegislator,
        /// <summary>
        /// The memberof parliament
        /// </summary>
        [EnumMember]
        [Name(Code = "111312", Name = "Member of Parliament", CodeSystem = "ANZSCO")]
        MemberofParliament,
        /// <summary>
        /// The legislatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "111399", Name = "Legislators nec", CodeSystem = "ANZSCO")]
        Legislatorsnec,
        /// <summary>
        /// The farmersand farm managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "121000", Name = "Farmers and Farm Managers nfd", CodeSystem = "ANZSCO")]
        FarmersandFarmManagersnfd,
        /// <summary>
        /// The aquaculture farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121111", Name = "Aquaculture Farmer", CodeSystem = "ANZSCO")]
        AquacultureFarmer,
        /// <summary>
        /// The crop farmersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "121200", Name = "Crop Farmers nfd", CodeSystem = "ANZSCO")]
        CropFarmersnfd,
        /// <summary>
        /// The cotton grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121211", Name = "Cotton Grower", CodeSystem = "ANZSCO")]
        CottonGrower,
        /// <summary>
        /// The flower grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121212", Name = "Flower Grower", CodeSystem = "ANZSCO")]
        FlowerGrower,
        /// <summary>
        /// The fruitor nut grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121213", Name = "Fruit or Nut Grower", CodeSystem = "ANZSCO")]
        FruitorNutGrower,
        /// <summary>
        /// The grain oilseedor pasture grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121214", Name = "Grain, Oilseed or Pasture Grower", CodeSystem = "ANZSCO")]
        GrainOilseedorPastureGrower,
        /// <summary>
        /// The grape grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121215", Name = "Grape Grower", CodeSystem = "ANZSCO")]
        GrapeGrower,
        /// <summary>
        /// The mixed crop farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121216", Name = "Mixed Crop Farmer", CodeSystem = "ANZSCO")]
        MixedCropFarmer,
        /// <summary>
        /// The sugar cane grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121217", Name = "Sugar Cane Grower", CodeSystem = "ANZSCO")]
        SugarCaneGrower,
        /// <summary>
        /// The turf grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121218", Name = "Turf Grower", CodeSystem = "ANZSCO")]
        TurfGrower,
        /// <summary>
        /// The vegetable grower
        /// </summary>
        [EnumMember]
        [Name(Code = "121221", Name = "Vegetable Grower", CodeSystem = "ANZSCO")]
        VegetableGrower,
        /// <summary>
        /// The crop farmersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "121299", Name = "Crop Farmers nec", CodeSystem = "ANZSCO")]
        CropFarmersnec,
        /// <summary>
        /// The livestock farmersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "121300", Name = "Livestock Farmers nfd", CodeSystem = "ANZSCO")]
        LivestockFarmersnfd,
        /// <summary>
        /// The apiarist
        /// </summary>
        [EnumMember]
        [Name(Code = "121311", Name = "Apiarist", CodeSystem = "ANZSCO")]
        Apiarist,
        /// <summary>
        /// The beef cattle farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121312", Name = "Beef Cattle Farmer", CodeSystem = "ANZSCO")]
        BeefCattleFarmer,
        /// <summary>
        /// The dairy cattle farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121313", Name = "Dairy Cattle Farmer", CodeSystem = "ANZSCO")]
        DairyCattleFarmer,
        /// <summary>
        /// The deer farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121314", Name = "Deer Farmer", CodeSystem = "ANZSCO")]
        DeerFarmer,
        /// <summary>
        /// The goat farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121315", Name = "Goat Farmer", CodeSystem = "ANZSCO")]
        GoatFarmer,
        /// <summary>
        /// The horse breeder
        /// </summary>
        [EnumMember]
        [Name(Code = "121316", Name = "Horse Breeder", CodeSystem = "ANZSCO")]
        HorseBreeder,
        /// <summary>
        /// The mixed livestock farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121317", Name = "Mixed Livestock Farmer", CodeSystem = "ANZSCO")]
        MixedLivestockFarmer,
        /// <summary>
        /// The pig farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121318", Name = "Pig Farmer", CodeSystem = "ANZSCO")]
        PigFarmer,
        /// <summary>
        /// The poultry farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121321", Name = "Poultry Farmer", CodeSystem = "ANZSCO")]
        PoultryFarmer,
        /// <summary>
        /// The sheep farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121322", Name = "Sheep Farmer", CodeSystem = "ANZSCO")]
        SheepFarmer,
        /// <summary>
        /// The livestock farmersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "121399", Name = "Livestock Farmers nec", CodeSystem = "ANZSCO")]
        LivestockFarmersnec,
        /// <summary>
        /// The mixed cropand livestock farmer
        /// </summary>
        [EnumMember]
        [Name(Code = "121411", Name = "Mixed Crop and Livestock Farmer", CodeSystem = "ANZSCO")]
        MixedCropandLivestockFarmer,
        /// <summary>
        /// The specialist managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "130000", Name = "Specialist Managers nfd", CodeSystem = "ANZSCO")]
        SpecialistManagersnfd,
        /// <summary>
        /// The advertising public relationsand sales managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "131100", Name = "Advertising, Public Relations and Sales Managers nfd", CodeSystem = "ANZSCO")]
        AdvertisingPublicRelationsandSalesManagersnfd,
        /// <summary>
        /// The salesand marketing manager
        /// </summary>
        [EnumMember]
        [Name(Code = "131112", Name = "Sales and Marketing Manager", CodeSystem = "ANZSCO")]
        SalesandMarketingManager,
        /// <summary>
        /// The advertising manager
        /// </summary>
        [EnumMember]
        [Name(Code = "131113", Name = "Advertising Manager", CodeSystem = "ANZSCO")]
        AdvertisingManager,
        /// <summary>
        /// The public relations manager
        /// </summary>
        [EnumMember]
        [Name(Code = "131114", Name = "Public Relations Manager", CodeSystem = "ANZSCO")]
        PublicRelationsManager,
        /// <summary>
        /// The business administration managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "132000", Name = "Business Administration Managers nfd", CodeSystem = "ANZSCO")]
        BusinessAdministrationManagersnfd,
        /// <summary>
        /// The corporate services manager
        /// </summary>
        [EnumMember]
        [Name(Code = "132111", Name = "Corporate Services Manager", CodeSystem = "ANZSCO")]
        CorporateServicesManager,
        /// <summary>
        /// The finance manager
        /// </summary>
        [EnumMember]
        [Name(Code = "132211", Name = "Finance Manager", CodeSystem = "ANZSCO")]
        FinanceManager,
        /// <summary>
        /// The human resource manager
        /// </summary>
        [EnumMember]
        [Name(Code = "132311", Name = "Human Resource Manager", CodeSystem = "ANZSCO")]
        HumanResourceManager,
        /// <summary>
        /// The policyand planning manager
        /// </summary>
        [EnumMember]
        [Name(Code = "132411", Name = "Policy and Planning Manager", CodeSystem = "ANZSCO")]
        PolicyandPlanningManager,
        /// <summary>
        /// The researchand development manager
        /// </summary>
        [EnumMember]
        [Name(Code = "132511", Name = "Research and Development Manager", CodeSystem = "ANZSCO")]
        ResearchandDevelopmentManager,
        /// <summary>
        /// The construction distributionand production managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "133000", Name = "Construction, Distribution and Production Managers nfd", CodeSystem = "ANZSCO")]
        ConstructionDistributionandProductionManagersnfd,
        /// <summary>
        /// The construction managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "133100", Name = "Construction Managers nfd", CodeSystem = "ANZSCO")]
        ConstructionManagersnfd,
        /// <summary>
        /// The construction project manager
        /// </summary>
        [EnumMember]
        [Name(Code = "133111", Name = "Construction Project Manager", CodeSystem = "ANZSCO")]
        ConstructionProjectManager,
        /// <summary>
        /// The project builder
        /// </summary>
        [EnumMember]
        [Name(Code = "133112", Name = "Project Builder", CodeSystem = "ANZSCO")]
        ProjectBuilder,
        /// <summary>
        /// The engineering manager
        /// </summary>
        [EnumMember]
        [Name(Code = "133211", Name = "Engineering Manager", CodeSystem = "ANZSCO")]
        EngineeringManager,
        /// <summary>
        /// The importers exportersand wholesalersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "133300", Name = "Importers, Exporters and Wholesalers nfd", CodeSystem = "ANZSCO")]
        ImportersExportersandWholesalersnfd,
        /// <summary>
        /// The importeror exporter
        /// </summary>
        [EnumMember]
        [Name(Code = "133311", Name = "Importer or Exporter", CodeSystem = "ANZSCO")]
        ImporterorExporter,
        /// <summary>
        /// The wholesaler
        /// </summary>
        [EnumMember]
        [Name(Code = "133312", Name = "Wholesaler", CodeSystem = "ANZSCO")]
        Wholesaler,
        /// <summary>
        /// The manufacturer
        /// </summary>
        [EnumMember]
        [Name(Code = "133411", Name = "Manufacturer", CodeSystem = "ANZSCO")]
        Manufacturer,
        /// <summary>
        /// The production managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "133500", Name = "Production Managers nfd", CodeSystem = "ANZSCO")]
        ProductionManagersnfd,
        /// <summary>
        /// The production manager forestry
        /// </summary>
        [EnumMember]
        [Name(Code = "133511", Name = "Production Manager (Forestry)", CodeSystem = "ANZSCO")]
        ProductionManagerForestry,
        /// <summary>
        /// The production manager manufacturing
        /// </summary>
        [EnumMember]
        [Name(Code = "133512", Name = "Production Manager (Manufacturing)", CodeSystem = "ANZSCO")]
        ProductionManagerManufacturing,
        /// <summary>
        /// The production manager mining
        /// </summary>
        [EnumMember]
        [Name(Code = "133513", Name = "Production Manager (Mining)", CodeSystem = "ANZSCO")]
        ProductionManagerMining,
        /// <summary>
        /// The supplyand distribution manager
        /// </summary>
        [EnumMember]
        [Name(Code = "133611", Name = "Supply and Distribution Manager", CodeSystem = "ANZSCO")]
        SupplyandDistributionManager,
        /// <summary>
        /// The education healthand welfare services managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "134000", Name = "Education, Health and Welfare Services Managers nfd", CodeSystem = "ANZSCO")]
        EducationHealthandWelfareServicesManagersnfd,
        /// <summary>
        /// The child care centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "134111", Name = "Child Care Centre Manager", CodeSystem = "ANZSCO")]
        ChildCareCentreManager,
/// <summary>
/// The healthand welfare services managersnfd
/// </summary>
        [EnumMember]
        [Name(Code = "134200", Name = "Health and Welfare Services Managers nfd", CodeSystem = "ANZSCO")]
        HealthandWelfareServicesManagersnfd,
        /// <summary>
        /// The medical administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "134211", Name = "Medical Administrator", CodeSystem = "ANZSCO")]
        MedicalAdministrator,
        /// <summary>
        /// The nursing clinical director
        /// </summary>
        [EnumMember]
        [Name(Code = "134212", Name = "Nursing Clinical Director", CodeSystem = "ANZSCO")]
        NursingClinicalDirector,
        /// <summary>
        /// The primary health organisation manager
        /// </summary>
        [EnumMember]
        [Name(Code = "134213", Name = "Primary Health Organisation Manager", CodeSystem = "ANZSCO")]
        PrimaryHealthOrganisationManager,
        /// <summary>
        /// The welfare centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "134214", Name = "Welfare Centre Manager", CodeSystem = "ANZSCO")]
        WelfareCentreManager,
        /// <summary>
        /// The healthand welfare services managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "134299", Name = "Health and Welfare Services Managers nec", CodeSystem = "ANZSCO")]
        HealthandWelfareServicesManagersnec,
        /// <summary>
        /// The school principal
        /// </summary>
        [EnumMember]
        [Name(Code = "134311", Name = "School Principal", CodeSystem = "ANZSCO")]
        SchoolPrincipal,
        /// <summary>
        /// The other education managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "134400", Name = "Other Education Managers nfd", CodeSystem = "ANZSCO")]
        OtherEducationManagersnfd,
        /// <summary>
        /// The faculty head
        /// </summary>
        [EnumMember]
        [Name(Code = "134411", Name = "Faculty Head", CodeSystem = "ANZSCO")]
        FacultyHead,
        /// <summary>
        /// The regional education manager
        /// </summary>
        [EnumMember]
        [Name(Code = "134412", Name = "Regional Education Manager", CodeSystem = "ANZSCO")]
        RegionalEducationManager,
        /// <summary>
        /// The education managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "134499", Name = "Education Managers nec", CodeSystem = "ANZSCO")]
        EducationManagersnec,
        /// <summary>
        /// The ICT managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "135100", Name = "ICT Managers nfd", CodeSystem = "ANZSCO")]
        ICTManagersnfd,
        /// <summary>
        /// The chief information officer
        /// </summary>
        [EnumMember]
        [Name(Code = "135111", Name = "Chief Information Officer", CodeSystem = "ANZSCO")]
        ChiefInformationOfficer,
        /// <summary>
        /// The ICT project manager
        /// </summary>
        [EnumMember]
        [Name(Code = "135112", Name = "ICT Project Manager", CodeSystem = "ANZSCO")]
        ICTProjectManager,
        /// <summary>
        /// The ICT managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "135199", Name = "ICT Managers nec", CodeSystem = "ANZSCO")]
        ICTManagersnec,
        /// <summary>
        /// The miscellaneous specialist managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "139000", Name = "Miscellaneous Specialist Managers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousSpecialistManagersnfd,
        /// <summary>
        /// The commissioned officers managementnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "139100", Name = "Commissioned Officers (Management) nfd", CodeSystem = "ANZSCO")]
        CommissionedOfficersManagementnfd,
        /// <summary>
        /// The commissioned defence force officer
        /// </summary>
        [EnumMember]
        [Name(Code = "139111", Name = "Commissioned Defence Force Officer", CodeSystem = "ANZSCO")]
        CommissionedDefenceForceOfficer,
        /// <summary>
        /// The commissioned fire officer
        /// </summary>
        [EnumMember]
        [Name(Code = "139112", Name = "Commissioned Fire Officer", CodeSystem = "ANZSCO")]
        CommissionedFireOfficer,
        /// <summary>
        /// The commissioned police officer
        /// </summary>
        [EnumMember]
        [Name(Code = "139113", Name = "Commissioned Police Officer", CodeSystem = "ANZSCO")]
        CommissionedPoliceOfficer,
        /// <summary>
        /// The senior noncommissioned defence force member
        /// </summary>
        [EnumMember]
        [Name(Code = "139211", Name = "Senior Non-commissioned Defence Force Member", CodeSystem = "ANZSCO")]
        SeniorNoncommissionedDefenceForceMember,
        /// <summary>
        /// The other specialist managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "139900", Name = "Other Specialist Managers nfd", CodeSystem = "ANZSCO")]
        OtherSpecialistManagersnfd,
        /// <summary>
        /// The arts administratoror manager
        /// </summary>
        [EnumMember]
        [Name(Code = "139911", Name = "Arts Administrator or Manager", CodeSystem = "ANZSCO")]
        ArtsAdministratororManager,
        /// <summary>
        /// The environmental manager
        /// </summary>
        [EnumMember]
        [Name(Code = "139912", Name = "Environmental Manager", CodeSystem = "ANZSCO")]
        EnvironmentalManager,
        /// <summary>
        /// The laboratory manager
        /// </summary>
        [EnumMember]
        [Name(Code = "139913", Name = "Laboratory Manager", CodeSystem = "ANZSCO")]
        LaboratoryManager,
        /// <summary>
        /// The quality assurance manager
        /// </summary>
        [EnumMember]
        [Name(Code = "139914", Name = "Quality Assurance Manager", CodeSystem = "ANZSCO")]
        QualityAssuranceManager,
        /// <summary>
        /// The sports administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "139915", Name = "Sports Administrator", CodeSystem = "ANZSCO")]
        SportsAdministrator,
        /// <summary>
        /// The specialist managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "139999", Name = "Specialist Managers nec", CodeSystem = "ANZSCO")]
        SpecialistManagersnec,
        /// <summary>
        /// The hospitality retailand service managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "140000", Name = "Hospitality, Retail and Service Managers nfd", CodeSystem = "ANZSCO")]
        HospitalityRetailandServiceManagersnfd,
        /// <summary>
        /// The accommodationand hospitality managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "141000", Name = "Accommodation and Hospitality Managers nfd", CodeSystem = "ANZSCO")]
        AccommodationandHospitalityManagersnfd,
        /// <summary>
        /// The cafeor restaurant manager
        /// </summary>
        [EnumMember]
        [Name(Code = "141111", Name = "Cafe or Restaurant Manager", CodeSystem = "ANZSCO")]
        CafeorRestaurantManager,
        /// <summary>
        /// The caravan parkand camping ground manager
        /// </summary>
        [EnumMember]
        [Name(Code = "141211", Name = "Caravan Park and Camping Ground Manager", CodeSystem = "ANZSCO")]
        CaravanParkandCampingGroundManager,
        /// <summary>
        /// The hotelor motel manager
        /// </summary>
        [EnumMember]
        [Name(Code = "141311", Name = "Hotel or Motel Manager", CodeSystem = "ANZSCO")]
        HotelorMotelManager,
        /// <summary>
        /// The licensed club manager
        /// </summary>
        [EnumMember]
        [Name(Code = "141411", Name = "Licensed Club Manager", CodeSystem = "ANZSCO")]
        LicensedClubManager,
        /// <summary>
        /// The other accommodationand hospitality managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "141900", Name = "Other Accommodation and Hospitality Managers nfd", CodeSystem = "ANZSCO")]
        OtherAccommodationandHospitalityManagersnfd,
        /// <summary>
        /// The bedand breakfast operator
        /// </summary>
        [EnumMember]
        [Name(Code = "141911", Name = "Bed and Breakfast Operator", CodeSystem = "ANZSCO")]
        BedandBreakfastOperator,
        /// <summary>
        /// The retirement village manager
        /// </summary>
        [EnumMember]
        [Name(Code = "141912", Name = "Retirement Village Manager", CodeSystem = "ANZSCO")]
        RetirementVillageManager,
        /// <summary>
        /// The accommodationand hospitality managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "141999", Name = "Accommodation and Hospitality Managers nec", CodeSystem = "ANZSCO")]
        AccommodationandHospitalityManagersnec,
        /// <summary>
        /// The retail managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "142100", Name = "Retail Managers nfd", CodeSystem = "ANZSCO")]
        RetailManagersnfd,
        /// <summary>
        /// The retail manager general
        /// </summary>
        [EnumMember]
        [Name(Code = "142111", Name = "Retail Manager (General)", CodeSystem = "ANZSCO")]
        RetailManagerGeneral,
        /// <summary>
        /// The antique dealer
        /// </summary>
        [EnumMember]
        [Name(Code = "142112", Name = "Antique Dealer", CodeSystem = "ANZSCO")]
        AntiqueDealer,
        /// <summary>
        /// The betting agency manager
        /// </summary>
        [EnumMember]
        [Name(Code = "142113", Name = "Betting Agency Manager", CodeSystem = "ANZSCO")]
        BettingAgencyManager,
        /// <summary>
        /// The hairor beauty salon manager
        /// </summary>
        [EnumMember]
        [Name(Code = "142114", Name = "Hair or Beauty Salon Manager", CodeSystem = "ANZSCO")]
        HairorBeautySalonManager,
        /// <summary>
        /// The post office manager
        /// </summary>
        [EnumMember]
        [Name(Code = "142115", Name = "Post Office Manager", CodeSystem = "ANZSCO")]
        PostOfficeManager,
        /// <summary>
        /// The travel agency manager
        /// </summary>
        [EnumMember]
        [Name(Code = "142116", Name = "Travel Agency Manager", CodeSystem = "ANZSCO")]
        TravelAgencyManager,
/// <summary>
/// The miscellaneous hospitality retailand service managersnfd
/// </summary>
        [EnumMember]
        [Name(Code = "149000", Name = "Miscellaneous Hospitality, Retail and Service Managers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousHospitalityRetailandServiceManagersnfd,
        /// <summary>
        /// The amusement fitnessand sports centre managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "149100", Name = "Amusement, Fitness and Sports Centre Managers nfd", CodeSystem = "ANZSCO")]
        AmusementFitnessandSportsCentreManagersnfd,
        /// <summary>
        /// The amusement centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149111", Name = "Amusement Centre Manager", CodeSystem = "ANZSCO")]
        AmusementCentreManager,
        /// <summary>
        /// The fitness centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149112", Name = "Fitness Centre Manager", CodeSystem = "ANZSCO")]
        FitnessCentreManager,
        /// <summary>
        /// The sports centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149113", Name = "Sports Centre Manager", CodeSystem = "ANZSCO")]
        SportsCentreManager,
        /// <summary>
        /// The callor contact centreand customer service managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "149200", Name = "Call or Contact Centre and Customer Service Managers nfd", CodeSystem = "ANZSCO")]
        CallorContactCentreandCustomerServiceManagersnfd,
        /// <summary>
        /// The callor contact centre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149211", Name = "Call or Contact Centre Manager", CodeSystem = "ANZSCO")]
        CallorContactCentreManager,
        /// <summary>
        /// The customer service manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149212", Name = "Customer Service Manager", CodeSystem = "ANZSCO")]
        CustomerServiceManager,
        /// <summary>
        /// The conferenceand event organiser
        /// </summary>
        [EnumMember]
        [Name(Code = "149311", Name = "Conference and Event Organiser", CodeSystem = "ANZSCO")]
        ConferenceandEventOrganiser,
        /// <summary>
        /// The transport services managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "149400", Name = "Transport Services Managers nfd", CodeSystem = "ANZSCO")]
        TransportServicesManagersnfd,
        /// <summary>
        /// The fleet manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149411", Name = "Fleet Manager", CodeSystem = "ANZSCO")]
        FleetManager,
        /// <summary>
        /// The railway station manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149412", Name = "Railway Station Manager", CodeSystem = "ANZSCO")]
        RailwayStationManager,
        /// <summary>
        /// The transport company manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149413", Name = "Transport Company Manager", CodeSystem = "ANZSCO")]
        TransportCompanyManager,
        /// <summary>
        /// The other hospitality retailand service managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "149900", Name = "Other Hospitality, Retail and Service Managers nfd", CodeSystem = "ANZSCO")]
        OtherHospitalityRetailandServiceManagersnfd,
        /// <summary>
        /// The boarding kennelor cattery operator
        /// </summary>
        [EnumMember]
        [Name(Code = "149911", Name = "Boarding Kennel or Cattery Operator", CodeSystem = "ANZSCO")]
        BoardingKennelorCatteryOperator,
        /// <summary>
        /// The cinemaor theatre manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149912", Name = "Cinema or Theatre Manager", CodeSystem = "ANZSCO")]
        CinemaorTheatreManager,
        /// <summary>
        /// The facilities manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149913", Name = "Facilities Manager", CodeSystem = "ANZSCO")]
        FacilitiesManager,
        /// <summary>
        /// The financial institution branch manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149914", Name = "Financial Institution Branch Manager", CodeSystem = "ANZSCO")]
        FinancialInstitutionBranchManager,
        /// <summary>
        /// The equipment hire manager
        /// </summary>
        [EnumMember]
        [Name(Code = "149915", Name = "Equipment Hire Manager", CodeSystem = "ANZSCO")]
        EquipmentHireManager,
        /// <summary>
        /// The hospitality retailand service managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "149999", Name = "Hospitality, Retail and Service Managers nec", CodeSystem = "ANZSCO")]
        HospitalityRetailandServiceManagersnec,
        /// <summary>
        /// The professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "200000", Name = "Professionals nfd", CodeSystem = "ANZSCO")]
        Professionalsnfd,
        /// <summary>
        /// The artsand media professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "210000", Name = "Arts and Media Professionals nfd", CodeSystem = "ANZSCO")]
        ArtsandMediaProfessionalsnfd,
/// <summary>
/// The arts professionalsnfd
/// </summary>
        [EnumMember]
        [Name(Code = "211000", Name = "Arts Professionals nfd", CodeSystem = "ANZSCO")]
        ArtsProfessionalsnfd,
        /// <summary>
        /// The actors dancersand other entertainersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "211100", Name = "Actors, Dancers and Other Entertainers nfd", CodeSystem = "ANZSCO")]
        ActorsDancersandOtherEntertainersnfd,
        /// <summary>
        /// The actor
        /// </summary>
        [EnumMember]
        [Name(Code = "211111", Name = "Actor", CodeSystem = "ANZSCO")]
        Actor,
        /// <summary>
        /// The danceror choreographer
        /// </summary>
        [EnumMember]
        [Name(Code = "211112", Name = "Dancer or Choreographer", CodeSystem = "ANZSCO")]
        DancerorChoreographer,
        /// <summary>
        /// The entertaineror variety artist
        /// </summary>
        [EnumMember]
        [Name(Code = "211113", Name = "Entertainer or Variety Artist", CodeSystem = "ANZSCO")]
        EntertainerorVarietyArtist,
        /// <summary>
        /// The actors dancersand other entertainersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "211199", Name = "Actors, Dancers and Other Entertainers nec", CodeSystem = "ANZSCO")]
        ActorsDancersandOtherEntertainersnec,
        /// <summary>
        /// The music professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "211200", Name = "Music Professionals nfd", CodeSystem = "ANZSCO")]
        MusicProfessionalsnfd,
        /// <summary>
        /// The composer
        /// </summary>
        [EnumMember]
        [Name(Code = "211211", Name = "Composer", CodeSystem = "ANZSCO")]
        Composer,
        /// <summary>
        /// The music director
        /// </summary>
        [EnumMember]
        [Name(Code = "211212", Name = "Music Director", CodeSystem = "ANZSCO")]
        MusicDirector,
        /// <summary>
        /// The musician instrumental
        /// </summary>
        [EnumMember]
        [Name(Code = "211213", Name = "Musician (Instrumental)", CodeSystem = "ANZSCO")]
        MusicianInstrumental,
        /// <summary>
        /// The singer
        /// </summary>
        [EnumMember]
        [Name(Code = "211214", Name = "Singer", CodeSystem = "ANZSCO")]
        Singer,
        /// <summary>
        /// The music professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "211299", Name = "Music Professionals nec", CodeSystem = "ANZSCO")]
        MusicProfessionalsnec,
        /// <summary>
        /// The photographer
        /// </summary>
        [EnumMember]
        [Name(Code = "211311", Name = "Photographer", CodeSystem = "ANZSCO")]
        Photographer,
        /// <summary>
        /// The visual artsand crafts professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "211400", Name = "Visual Arts and Crafts Professionals nfd", CodeSystem = "ANZSCO")]
        VisualArtsandCraftsProfessionalsnfd,
        /// <summary>
        /// The painter visual arts
        /// </summary>
        [EnumMember]
        [Name(Code = "211411", Name = "Painter (Visual Arts)", CodeSystem = "ANZSCO")]
        PainterVisualArts,
        /// <summary>
        /// The potteror ceramic artist
        /// </summary>
        [EnumMember]
        [Name(Code = "211412", Name = "Potter or Ceramic Artist", CodeSystem = "ANZSCO")]
        PotterorCeramicArtist,
        /// <summary>
        /// The sculptor
        /// </summary>
        [EnumMember]
        [Name(Code = "211413", Name = "Sculptor", CodeSystem = "ANZSCO")]
        Sculptor,
        /// <summary>
        /// The visual artsand crafts professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "211499", Name = "Visual Arts and Crafts Professionals nec", CodeSystem = "ANZSCO")]
        VisualArtsandCraftsProfessionalsnec,
        /// <summary>
        /// The media professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "212000", Name = "Media Professionals nfd", CodeSystem = "ANZSCO")]
        MediaProfessionalsnfd,
        /// <summary>
        /// The artistic directorsand media producersand presentersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "212100", Name = "Artistic Directors, and Media Producers and Presenters nfd", CodeSystem = "ANZSCO")]
        ArtisticDirectorsandMediaProducersandPresentersnfd,
        /// <summary>
        /// The artistic director
        /// </summary>
        [EnumMember]
        [Name(Code = "212111", Name = "Artistic Director", CodeSystem = "ANZSCO")]
        ArtisticDirector,
        /// <summary>
        /// The media producerexcluding video
        /// </summary>
        [EnumMember]
        [Name(Code = "212112", Name = "Media Producer (excluding Video)", CodeSystem = "ANZSCO")]
        MediaProducerexcludingVideo,
        /// <summary>
        /// The radio presenter
        /// </summary>
        [EnumMember]
        [Name(Code = "212113", Name = "Radio Presenter", CodeSystem = "ANZSCO")]
        RadioPresenter,
        /// <summary>
        /// The television presenter
        /// </summary>
        [EnumMember]
        [Name(Code = "212114", Name = "Television Presenter", CodeSystem = "ANZSCO")]
        TelevisionPresenter,
        /// <summary>
        /// The authorsand bookand script editorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "212200", Name = "Authors, and Book and Script Editors nfd", CodeSystem = "ANZSCO")]
        AuthorsandBookandScriptEditorsnfd,
        /// <summary>
        /// The author
        /// </summary>
        [EnumMember]
        [Name(Code = "212211", Name = "Author", CodeSystem = "ANZSCO")]
        Author,
        /// <summary>
        /// The bookor script editor
        /// </summary>
        [EnumMember]
        [Name(Code = "212212", Name = "Book or Script Editor", CodeSystem = "ANZSCO")]
        BookorScriptEditor,
        /// <summary>
        /// The film television radioand stage directorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "212300", Name = "Film, Television, Radio and Stage Directors nfd", CodeSystem = "ANZSCO")]
        FilmTelevisionRadioandStageDirectorsnfd,
        /// <summary>
        /// The art director film televisionor stage
        /// </summary>
        [EnumMember]
        [Name(Code = "212311", Name = "Art Director (Film, Television or Stage)", CodeSystem = "ANZSCO")]
        ArtDirectorFilmTelevisionorStage,
        /// <summary>
        /// The director film television radioor stage
        /// </summary>
        [EnumMember]
        [Name(Code = "212312", Name = "Director (Film, Television, Radio or Stage)", CodeSystem = "ANZSCO")]
        DirectorFilmTelevisionRadioorStage,
        /// <summary>
        /// The directorof photography
        /// </summary>
        [EnumMember]
        [Name(Code = "212313", Name = "Director of Photography", CodeSystem = "ANZSCO")]
        DirectorofPhotography,
        /// <summary>
        /// The filmand video editor
        /// </summary>
        [EnumMember]
        [Name(Code = "212314", Name = "Film and Video Editor", CodeSystem = "ANZSCO")]
        FilmandVideoEditor,
        /// <summary>
        /// The program director televisionor radio
        /// </summary>
        [EnumMember]
        [Name(Code = "212315", Name = "Program Director (Television or Radio)", CodeSystem = "ANZSCO")]
        ProgramDirectorTelevisionorRadio,
        /// <summary>
        /// The stage manager
        /// </summary>
        [EnumMember]
        [Name(Code = "212316", Name = "Stage Manager", CodeSystem = "ANZSCO")]
        StageManager,
        /// <summary>
        /// The technical director
        /// </summary>
        [EnumMember]
        [Name(Code = "212317", Name = "Technical Director", CodeSystem = "ANZSCO")]
        TechnicalDirector,
        /// <summary>
        /// The video producer
        /// </summary>
        [EnumMember]
        [Name(Code = "212318", Name = "Video Producer", CodeSystem = "ANZSCO")]
        VideoProducer,
        /// <summary>
        /// The film television radioand stage directorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "212399", Name = "Film, Television, Radio and Stage Directors nec", CodeSystem = "ANZSCO")]
        FilmTelevisionRadioandStageDirectorsnec,
        /// <summary>
        /// The journalistsand other writersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "212400", Name = "Journalists and Other Writers nfd", CodeSystem = "ANZSCO")]
        JournalistsandOtherWritersnfd,
        /// <summary>
        /// The copywriter
        /// </summary>
        [EnumMember]
        [Name(Code = "212411", Name = "Copywriter", CodeSystem = "ANZSCO")]
        Copywriter,
        /// <summary>
        /// The newspaperor periodical editor
        /// </summary>
        [EnumMember]
        [Name(Code = "212412", Name = "Newspaper or Periodical Editor", CodeSystem = "ANZSCO")]
        NewspaperorPeriodicalEditor,
        /// <summary>
        /// The print journalist
        /// </summary>
        [EnumMember]
        [Name(Code = "212413", Name = "Print Journalist", CodeSystem = "ANZSCO")]
        PrintJournalist,
        /// <summary>
        /// The radio journalist
        /// </summary>
        [EnumMember]
        [Name(Code = "212414", Name = "Radio Journalist", CodeSystem = "ANZSCO")]
        RadioJournalist,
        /// <summary>
        /// The technical writer
        /// </summary>
        [EnumMember]
        [Name(Code = "212415", Name = "Technical Writer", CodeSystem = "ANZSCO")]
        TechnicalWriter,
        /// <summary>
        /// The television journalist
        /// </summary>
        [EnumMember]
        [Name(Code = "212416", Name = "Television Journalist", CodeSystem = "ANZSCO")]
        TelevisionJournalist,
        /// <summary>
        /// The journalistsand other writersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "212499", Name = "Journalists and Other Writers nec", CodeSystem = "ANZSCO")]
        JournalistsandOtherWritersnec,
        /// <summary>
        /// The business human resourceand marketing professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "220000", Name = "Business, Human Resource and Marketing Professionals nfd", CodeSystem = "ANZSCO")]
        BusinessHumanResourceandMarketingProfessionalsnfd,
        /// <summary>
        /// The accountants auditorsand company secretariesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "221000", Name = "Accountants, Auditors and Company Secretaries nfd", CodeSystem = "ANZSCO")]
        AccountantsAuditorsandCompanySecretariesnfd,
        /// <summary>
        /// The accountantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "221100", Name = "Accountants nfd", CodeSystem = "ANZSCO")]
        Accountantsnfd,
        /// <summary>
        /// The accountant general
        /// </summary>
        [EnumMember]
        [Name(Code = "221111", Name = "Accountant (General)", CodeSystem = "ANZSCO")]
        AccountantGeneral,
        /// <summary>
        /// The management accountant
        /// </summary>
        [EnumMember]
        [Name(Code = "221112", Name = "Management Accountant", CodeSystem = "ANZSCO")]
        ManagementAccountant,
        /// <summary>
        /// The taxation accountant
        /// </summary>
        [EnumMember]
        [Name(Code = "221113", Name = "Taxation Accountant", CodeSystem = "ANZSCO")]
        TaxationAccountant,
        /// <summary>
        /// The auditors company secretariesand corporate treasurersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "221200", Name = "Auditors, Company Secretaries and Corporate Treasurers nfd", CodeSystem = "ANZSCO")]
        AuditorsCompanySecretariesandCorporateTreasurersnfd,
        /// <summary>
        /// The company secretary
        /// </summary>
        [EnumMember]
        [Name(Code = "221211", Name = "Company Secretary", CodeSystem = "ANZSCO")]
        CompanySecretary,
        /// <summary>
        /// The corporate treasurer
        /// </summary>
        [EnumMember]
        [Name(Code = "221212", Name = "Corporate Treasurer", CodeSystem = "ANZSCO")]
        CorporateTreasurer,
        /// <summary>
        /// The external auditor
        /// </summary>
        [EnumMember]
        [Name(Code = "221213", Name = "External Auditor", CodeSystem = "ANZSCO")]
        ExternalAuditor,
        /// <summary>
        /// The internal auditor
        /// </summary>
        [EnumMember]
        [Name(Code = "221214", Name = "Internal Auditor", CodeSystem = "ANZSCO")]
        InternalAuditor,
        /// <summary>
        /// The financial brokersand dealersand investment advisersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "222000", Name = "Financial Brokers and Dealers, and Investment Advisers nfd", CodeSystem = "ANZSCO")]
        FinancialBrokersandDealersandInvestmentAdvisersnfd,
        /// <summary>
        /// The financial brokersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "222100", Name = "Financial Brokers nfd", CodeSystem = "ANZSCO")]
        FinancialBrokersnfd,
        /// <summary>
        /// The commodities trader
        /// </summary>
        [EnumMember]
        [Name(Code = "222111", Name = "Commodities Trader", CodeSystem = "ANZSCO")]
        CommoditiesTrader,
        /// <summary>
        /// The finance broker
        /// </summary>
        [EnumMember]
        [Name(Code = "222112", Name = "Finance Broker", CodeSystem = "ANZSCO")]
        FinanceBroker,
        /// <summary>
        /// The insurance broker
        /// </summary>
        [EnumMember]
        [Name(Code = "222113", Name = "Insurance Broker", CodeSystem = "ANZSCO")]
        InsuranceBroker,
        /// <summary>
        /// The financial brokersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "222199", Name = "Financial Brokers nec", CodeSystem = "ANZSCO")]
        FinancialBrokersnec,
        /// <summary>
        /// The financial dealersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "222200", Name = "Financial Dealers nfd", CodeSystem = "ANZSCO")]
        FinancialDealersnfd,
        /// <summary>
        /// The financial market dealer
        /// </summary>
        [EnumMember]
        [Name(Code = "222211", Name = "Financial Market Dealer", CodeSystem = "ANZSCO")]
        FinancialMarketDealer,
        /// <summary>
        /// The futures trader
        /// </summary>
        [EnumMember]
        [Name(Code = "222212", Name = "Futures Trader", CodeSystem = "ANZSCO")]
        FuturesTrader,
        /// <summary>
        /// The stockbroking dealer
        /// </summary>
        [EnumMember]
        [Name(Code = "222213", Name = "Stockbroking Dealer", CodeSystem = "ANZSCO")]
        StockbrokingDealer,
        /// <summary>
        /// The financial dealersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "222299", Name = "Financial Dealers nec", CodeSystem = "ANZSCO")]
        FinancialDealersnec,
        /// <summary>
        /// The financial investment advisersand managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "222300", Name = "Financial Investment Advisers and Managers nfd", CodeSystem = "ANZSCO")]
        FinancialInvestmentAdvisersandManagersnfd,
        /// <summary>
        /// The financial investment adviser
        /// </summary>
        [EnumMember]
        [Name(Code = "222311", Name = "Financial Investment Adviser", CodeSystem = "ANZSCO")]
        FinancialInvestmentAdviser,
        /// <summary>
        /// The financial investment manager
        /// </summary>
        [EnumMember]
        [Name(Code = "222312", Name = "Financial Investment Manager", CodeSystem = "ANZSCO")]
        FinancialInvestmentManager,
        /// <summary>
        /// The human resourceand training professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "223000", Name = "Human Resource and Training Professionals nfd", CodeSystem = "ANZSCO")]
        HumanResourceandTrainingProfessionalsnfd,
        /// <summary>
        /// The human resource professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "223100", Name = "Human Resource Professionals nfd", CodeSystem = "ANZSCO")]
        HumanResourceProfessionalsnfd,
        /// <summary>
        /// The human resource adviser
        /// </summary>
        [EnumMember]
        [Name(Code = "223111", Name = "Human Resource Adviser", CodeSystem = "ANZSCO")]
        HumanResourceAdviser,
        /// <summary>
        /// The recruitment consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "223112", Name = "Recruitment Consultant", CodeSystem = "ANZSCO")]
        RecruitmentConsultant,
        /// <summary>
        /// The workplace relations adviser
        /// </summary>
        [EnumMember]
        [Name(Code = "223113", Name = "Workplace Relations Adviser", CodeSystem = "ANZSCO")]
        WorkplaceRelationsAdviser,
        /// <summary>
        /// The ICT trainer
        /// </summary>
        [EnumMember]
        [Name(Code = "223211", Name = "ICT Trainer", CodeSystem = "ANZSCO")]
        ICTTrainer,
        /// <summary>
        /// The trainingand development professional
        /// </summary>
        [EnumMember]
        [Name(Code = "223311", Name = "Training and Development Professional", CodeSystem = "ANZSCO")]
        TrainingandDevelopmentProfessional,
        /// <summary>
        /// The informationand organisation professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224000", Name = "Information and Organisation Professionals nfd", CodeSystem = "ANZSCO")]
        InformationandOrganisationProfessionalsnfd,
        /// <summary>
        /// The actuaries mathematiciansand statisticiansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224100", Name = "Actuaries, Mathematicians and Statisticians nfd", CodeSystem = "ANZSCO")]
        ActuariesMathematiciansandStatisticiansnfd,
        /// <summary>
        /// The actuary
        /// </summary>
        [EnumMember]
        [Name(Code = "224111", Name = "Actuary", CodeSystem = "ANZSCO")]
        Actuary,
        /// <summary>
        /// The mathematician
        /// </summary>
        [EnumMember]
        [Name(Code = "224112", Name = "Mathematician", CodeSystem = "ANZSCO")]
        Mathematician,
        /// <summary>
        /// The statistician
        /// </summary>
        [EnumMember]
        [Name(Code = "224113", Name = "Statistician", CodeSystem = "ANZSCO")]
        Statistician,
        /// <summary>
        /// The archivists curatorsand records managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224200", Name = "Archivists, Curators and Records Managers nfd", CodeSystem = "ANZSCO")]
        ArchivistsCuratorsandRecordsManagersnfd,
        /// <summary>
        /// The archivist
        /// </summary>
        [EnumMember]
        [Name(Code = "224211", Name = "Archivist", CodeSystem = "ANZSCO")]
        Archivist,
        /// <summary>
        /// The galleryor museum curator
        /// </summary>
        [EnumMember]
        [Name(Code = "224212", Name = "Gallery or Museum Curator", CodeSystem = "ANZSCO")]
        GalleryorMuseumCurator,
        /// <summary>
        /// The health information manager
        /// </summary>
        [EnumMember]
        [Name(Code = "224213", Name = "Health Information Manager", CodeSystem = "ANZSCO")]
        HealthInformationManager,
        /// <summary>
        /// The records manager
        /// </summary>
        [EnumMember]
        [Name(Code = "224214", Name = "Records Manager", CodeSystem = "ANZSCO")]
        RecordsManager,
        /// <summary>
        /// The economist
        /// </summary>
        [EnumMember]
        [Name(Code = "224311", Name = "Economist", CodeSystem = "ANZSCO")]
        Economist,
        /// <summary>
        /// The intelligenceand policy analystsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224400", Name = "Intelligence and Policy Analysts nfd", CodeSystem = "ANZSCO")]
        IntelligenceandPolicyAnalystsnfd,
        /// <summary>
        /// The intelligence officer
        /// </summary>
        [EnumMember]
        [Name(Code = "224411", Name = "Intelligence Officer", CodeSystem = "ANZSCO")]
        IntelligenceOfficer,
        /// <summary>
        /// The policy analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "224412", Name = "Policy Analyst", CodeSystem = "ANZSCO")]
        PolicyAnalyst,
        /// <summary>
        /// The land economistsand valuersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224500", Name = "Land Economists and Valuers nfd", CodeSystem = "ANZSCO")]
        LandEconomistsandValuersnfd,
        /// <summary>
        /// The land economist
        /// </summary>
        [EnumMember]
        [Name(Code = "224511", Name = "Land Economist", CodeSystem = "ANZSCO")]
        LandEconomist,
        /// <summary>
        /// The valuer
        /// </summary>
        [EnumMember]
        [Name(Code = "224512", Name = "Valuer", CodeSystem = "ANZSCO")]
        Valuer,
        /// <summary>
        /// The librarian
        /// </summary>
        [EnumMember]
        [Name(Code = "224611", Name = "Librarian", CodeSystem = "ANZSCO")]
        Librarian,
        /// <summary>
        /// The managementand organisation analystsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224700", Name = "Management and Organisation Analysts nfd", CodeSystem = "ANZSCO")]
        ManagementandOrganisationAnalystsnfd,
        /// <summary>
        /// The management consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "224711", Name = "Management Consultant", CodeSystem = "ANZSCO")]
        ManagementConsultant,
        /// <summary>
        /// The organisationand methods analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "224712", Name = "Organisation and Methods Analyst", CodeSystem = "ANZSCO")]
        OrganisationandMethodsAnalyst,
        /// <summary>
        /// The other informationand organisation professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "224900", Name = "Other Information and Organisation Professionals nfd", CodeSystem = "ANZSCO")]
        OtherInformationandOrganisationProfessionalsnfd,
        /// <summary>
        /// The electorate officer
        /// </summary>
        [EnumMember]
        [Name(Code = "224911", Name = "Electorate Officer", CodeSystem = "ANZSCO")]
        ElectorateOfficer,
        /// <summary>
        /// The liaison officer
        /// </summary>
        [EnumMember]
        [Name(Code = "224912", Name = "Liaison Officer", CodeSystem = "ANZSCO")]
        LiaisonOfficer,
        /// <summary>
        /// The migration agent
        /// </summary>
        [EnumMember]
        [Name(Code = "224913", Name = "Migration Agent", CodeSystem = "ANZSCO")]
        MigrationAgent,
        /// <summary>
        /// The patents examiner
        /// </summary>
        [EnumMember]
        [Name(Code = "224914", Name = "Patents Examiner", CodeSystem = "ANZSCO")]
        PatentsExaminer,
        /// <summary>
        /// The informationand organisation professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "224999", Name = "Information and Organisation Professionals nec", CodeSystem = "ANZSCO")]
        InformationandOrganisationProfessionalsnec,
        /// <summary>
        /// The sales marketingand public relations professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "225000", Name = "Sales, Marketing and Public Relations Professionals nfd", CodeSystem = "ANZSCO")]
        SalesMarketingandPublicRelationsProfessionalsnfd,
        /// <summary>
        /// The advertisingand marketing professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "225100", Name = "Advertising and Marketing Professionals nfd", CodeSystem = "ANZSCO")]
        AdvertisingandMarketingProfessionalsnfd,
        /// <summary>
        /// The advertising specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "225111", Name = "Advertising Specialist", CodeSystem = "ANZSCO")]
        AdvertisingSpecialist,
        /// <summary>
        /// The market research analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "225112", Name = "Market Research Analyst", CodeSystem = "ANZSCO")]
        MarketResearchAnalyst,
        /// <summary>
        /// The marketing specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "225113", Name = "Marketing Specialist", CodeSystem = "ANZSCO")]
        MarketingSpecialist,
        /// <summary>
        /// The ICT sales professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "225200", Name = "ICT Sales Professionals nfd", CodeSystem = "ANZSCO")]
        ICTSalesProfessionalsnfd,
        /// <summary>
        /// The ICT account manager
        /// </summary>
        [EnumMember]
        [Name(Code = "225211", Name = "ICT Account Manager", CodeSystem = "ANZSCO")]
        ICTAccountManager,
        /// <summary>
        /// The ICT business development manager
        /// </summary>
        [EnumMember]
        [Name(Code = "225212", Name = "ICT Business Development Manager", CodeSystem = "ANZSCO")]
        ICTBusinessDevelopmentManager,
        /// <summary>
        /// The ICT sales representative
        /// </summary>
        [EnumMember]
        [Name(Code = "225213", Name = "ICT Sales Representative", CodeSystem = "ANZSCO")]
        ICTSalesRepresentative,
        /// <summary>
        /// The public relations professional
        /// </summary>
        [EnumMember]
        [Name(Code = "225311", Name = "Public Relations Professional", CodeSystem = "ANZSCO")]
        PublicRelationsProfessional,
        /// <summary>
        /// The technical sales representativesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "225400", Name = "Technical Sales Representatives nfd", CodeSystem = "ANZSCO")]
        TechnicalSalesRepresentativesnfd,
        /// <summary>
        /// The sales representative industrial products
        /// </summary>
        [EnumMember]
        [Name(Code = "225411", Name = "Sales Representative (Industrial Products)", CodeSystem = "ANZSCO")]
        SalesRepresentativeIndustrialProducts,
        /// <summary>
        /// The sales representative medicaland pharmaceutical products
        /// </summary>
        [EnumMember]
        [Name(Code = "225412", Name = "Sales Representative (Medical and Pharmaceutical Products)", CodeSystem = "ANZSCO")]
        SalesRepresentativeMedicalandPharmaceuticalProducts,
        /// <summary>
        /// The technical sales representativesnec
        /// </summary>
        [EnumMember]
        [Name(Code = "225499", Name = "Technical Sales Representatives nec", CodeSystem = "ANZSCO")]
        TechnicalSalesRepresentativesnec,
        /// <summary>
        /// The design engineering scienceand transport professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "230000", Name = "Design, Engineering, Science and Transport Professionals nfd", CodeSystem = "ANZSCO")]
        DesignEngineeringScienceandTransportProfessionalsnfd,
        /// <summary>
        /// The airand marine transport professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "231000", Name = "Air and Marine Transport Professionals nfd", CodeSystem = "ANZSCO")]
        AirandMarineTransportProfessionalsnfd,
        /// <summary>
        /// The air transport professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "231100", Name = "Air Transport Professionals nfd", CodeSystem = "ANZSCO")]
        AirTransportProfessionalsnfd,
        /// <summary>
        /// The aeroplane pilot
        /// </summary>
        [EnumMember]
        [Name(Code = "231111", Name = "Aeroplane Pilot", CodeSystem = "ANZSCO")]
        AeroplanePilot,
        /// <summary>
        /// The air traffic controller
        /// </summary>
        [EnumMember]
        [Name(Code = "231112", Name = "Air Traffic Controller", CodeSystem = "ANZSCO")]
        AirTrafficController,
        /// <summary>
        /// The flying instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "231113", Name = "Flying Instructor", CodeSystem = "ANZSCO")]
        FlyingInstructor,
        /// <summary>
        /// The helicopter pilot
        /// </summary>
        [EnumMember]
        [Name(Code = "231114", Name = "Helicopter Pilot", CodeSystem = "ANZSCO")]
        HelicopterPilot,
        /// <summary>
        /// The air transport professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "231199", Name = "Air Transport Professionals nec", CodeSystem = "ANZSCO")]
        AirTransportProfessionalsnec,
        /// <summary>
        /// The marine transport professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "231200", Name = "Marine Transport Professionals nfd", CodeSystem = "ANZSCO")]
        MarineTransportProfessionalsnfd,
        /// <summary>
        /// The master fisher
        /// </summary>
        [EnumMember]
        [Name(Code = "231211", Name = "Master Fisher", CodeSystem = "ANZSCO")]
        MasterFisher,
        /// <summary>
        /// The ships engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "231212", Name = "Ship's Engineer", CodeSystem = "ANZSCO")]
        ShipsEngineer,
        /// <summary>
        /// The ships master
        /// </summary>
        [EnumMember]
        [Name(Code = "231213", Name = "Ship's Master", CodeSystem = "ANZSCO")]
        ShipsMaster,
        /// <summary>
        /// The ships officer
        /// </summary>
        [EnumMember]
        [Name(Code = "231214", Name = "Ship's Officer", CodeSystem = "ANZSCO")]
        ShipsOfficer,
        /// <summary>
        /// The ships surveyor
        /// </summary>
        [EnumMember]
        [Name(Code = "231215", Name = "Ship's Surveyor", CodeSystem = "ANZSCO")]
        ShipsSurveyor,
        /// <summary>
        /// The marine transport professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "231299", Name = "Marine Transport Professionals nec", CodeSystem = "ANZSCO")]
        MarineTransportProfessionalsnec,
        /// <summary>
        /// The architects designers plannersand surveyorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "232000", Name = "Architects, Designers, Planners and Surveyors nfd", CodeSystem = "ANZSCO")]
        ArchitectsDesignersPlannersandSurveyorsnfd,
        /// <summary>
        /// The architectsand landscape architectsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "232100", Name = "Architects and Landscape Architects nfd", CodeSystem = "ANZSCO")]
        ArchitectsandLandscapeArchitectsnfd,
        /// <summary>
        /// The architect
        /// </summary>
        [EnumMember]
        [Name(Code = "232111", Name = "Architect", CodeSystem = "ANZSCO")]
        Architect,
        /// <summary>
        /// The landscape architect
        /// </summary>
        [EnumMember]
        [Name(Code = "232112", Name = "Landscape Architect", CodeSystem = "ANZSCO")]
        LandscapeArchitect,
        /// <summary>
        /// The surveyorsand spatial scientistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "232200", Name = "Surveyors and Spatial Scientists nfd", CodeSystem = "ANZSCO")]
        SurveyorsandSpatialScientistsnfd,
        /// <summary>
        /// The surveyor
        /// </summary>
        [EnumMember]
        [Name(Code = "232212", Name = "Surveyor", CodeSystem = "ANZSCO")]
        Surveyor,
        /// <summary>
        /// The cartographer
        /// </summary>
        [EnumMember]
        [Name(Code = "232213", Name = "Cartographer", CodeSystem = "ANZSCO")]
        Cartographer,
        /// <summary>
        /// The other spatial scientist
        /// </summary>
        [EnumMember]
        [Name(Code = "232214", Name = "Other Spatial Scientist", CodeSystem = "ANZSCO")]
        OtherSpatialScientist,
        /// <summary>
        /// The fashion industrialand jewellery designersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "232300", Name = "Fashion, Industrial and Jewellery Designers nfd", CodeSystem = "ANZSCO")]
        FashionIndustrialandJewelleryDesignersnfd,
        /// <summary>
        /// The fashion designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232311", Name = "Fashion Designer", CodeSystem = "ANZSCO")]
        FashionDesigner,
        /// <summary>
        /// The industrial designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232312", Name = "Industrial Designer", CodeSystem = "ANZSCO")]
        IndustrialDesigner,
        /// <summary>
        /// The jewellery designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232313", Name = "Jewellery Designer", CodeSystem = "ANZSCO")]
        JewelleryDesigner,
        /// <summary>
        /// The graphicand web designersand illustratorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "232400", Name = "Graphic and Web Designers, and Illustrators nfd", CodeSystem = "ANZSCO")]
        GraphicandWebDesignersandIllustratorsnfd,
        /// <summary>
        /// The graphic designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232411", Name = "Graphic Designer", CodeSystem = "ANZSCO")]
        GraphicDesigner,
        /// <summary>
        /// The illustrator
        /// </summary>
        [EnumMember]
        [Name(Code = "232412", Name = "Illustrator", CodeSystem = "ANZSCO")]
        Illustrator,
        /// <summary>
        /// The multimedia designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232413", Name = "Multimedia Designer", CodeSystem = "ANZSCO")]
        MultimediaDesigner,
        /// <summary>
        /// The web designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232414", Name = "Web Designer", CodeSystem = "ANZSCO")]
        WebDesigner,
        /// <summary>
        /// The interior designer
        /// </summary>
        [EnumMember]
        [Name(Code = "232511", Name = "Interior Designer", CodeSystem = "ANZSCO")]
        InteriorDesigner,
        /// <summary>
        /// The urbanand regional planner
        /// </summary>
        [EnumMember]
        [Name(Code = "232611", Name = "Urban and Regional Planner", CodeSystem = "ANZSCO")]
        UrbanandRegionalPlanner,
        /// <summary>
        /// The engineering professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233000", Name = "Engineering Professionals nfd", CodeSystem = "ANZSCO")]
        EngineeringProfessionalsnfd,
        /// <summary>
        /// The chemicaland materials engineersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233100", Name = "Chemical and Materials Engineers nfd", CodeSystem = "ANZSCO")]
        ChemicalandMaterialsEngineersnfd,
        /// <summary>
        /// The chemical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233111", Name = "Chemical Engineer", CodeSystem = "ANZSCO")]
        ChemicalEngineer,
        /// <summary>
        /// The materials engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233112", Name = "Materials Engineer", CodeSystem = "ANZSCO")]
        MaterialsEngineer,
        /// <summary>
        /// The civil engineering professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233200", Name = "Civil Engineering Professionals nfd", CodeSystem = "ANZSCO")]
        CivilEngineeringProfessionalsnfd,
        /// <summary>
        /// The civil engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233211", Name = "Civil Engineer", CodeSystem = "ANZSCO")]
        CivilEngineer,
        /// <summary>
        /// The geotechnical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233212", Name = "Geotechnical Engineer", CodeSystem = "ANZSCO")]
        GeotechnicalEngineer,
        /// <summary>
        /// The quantity surveyor
        /// </summary>
        [EnumMember]
        [Name(Code = "233213", Name = "Quantity Surveyor", CodeSystem = "ANZSCO")]
        QuantitySurveyor,
        /// <summary>
        /// The structural engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233214", Name = "Structural Engineer", CodeSystem = "ANZSCO")]
        StructuralEngineer,
        /// <summary>
        /// The transport engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233215", Name = "Transport Engineer", CodeSystem = "ANZSCO")]
        TransportEngineer,
        /// <summary>
        /// The electrical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233311", Name = "Electrical Engineer", CodeSystem = "ANZSCO")]
        ElectricalEngineer,
        /// <summary>
        /// The electronics engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233411", Name = "Electronics Engineer", CodeSystem = "ANZSCO")]
        ElectronicsEngineer,
        /// <summary>
        /// The industrial mechanicaland production engineersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233500", Name = "Industrial, Mechanical and Production Engineers nfd", CodeSystem = "ANZSCO")]
        IndustrialMechanicalandProductionEngineersnfd,
        /// <summary>
        /// The industrial engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233511", Name = "Industrial Engineer", CodeSystem = "ANZSCO")]
        IndustrialEngineer,
        /// <summary>
        /// The mechanical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233512", Name = "Mechanical Engineer", CodeSystem = "ANZSCO")]
        MechanicalEngineer,
        /// <summary>
        /// The productionor plant engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233513", Name = "Production or Plant Engineer", CodeSystem = "ANZSCO")]
        ProductionorPlantEngineer,
        /// <summary>
        /// The mining engineersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233600", Name = "Mining Engineers nfd", CodeSystem = "ANZSCO")]
        MiningEngineersnfd,
        /// <summary>
        /// The mining engineerexcluding petroleum
        /// </summary>
        [EnumMember]
        [Name(Code = "233611", Name = "Mining Engineer (excluding Petroleum)", CodeSystem = "ANZSCO")]
        MiningEngineerexcludingPetroleum,
        /// <summary>
        /// The petroleum engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233612", Name = "Petroleum Engineer", CodeSystem = "ANZSCO")]
        PetroleumEngineer,
        /// <summary>
        /// The other engineering professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "233900", Name = "Other Engineering Professionals nfd", CodeSystem = "ANZSCO")]
        OtherEngineeringProfessionalsnfd,
        /// <summary>
        /// The aeronautical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233911", Name = "Aeronautical Engineer", CodeSystem = "ANZSCO")]
        AeronauticalEngineer,
        /// <summary>
        /// The agricultural engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233912", Name = "Agricultural Engineer", CodeSystem = "ANZSCO")]
        AgriculturalEngineer,
        /// <summary>
        /// The biomedical engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233913", Name = "Biomedical Engineer", CodeSystem = "ANZSCO")]
        BiomedicalEngineer,
        /// <summary>
        /// The engineering technologist
        /// </summary>
        [EnumMember]
        [Name(Code = "233914", Name = "Engineering Technologist", CodeSystem = "ANZSCO")]
        EngineeringTechnologist,
        /// <summary>
        /// The environmental engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "233915", Name = "Environmental Engineer", CodeSystem = "ANZSCO")]
        EnvironmentalEngineer,
        /// <summary>
        /// The naval architect
        /// </summary>
        [EnumMember]
        [Name(Code = "233916", Name = "Naval Architect", CodeSystem = "ANZSCO")]
        NavalArchitect,
        /// <summary>
        /// The engineering professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "233999", Name = "Engineering Professionals nec", CodeSystem = "ANZSCO")]
        EngineeringProfessionalsnec,
        /// <summary>
        /// The naturaland physical science professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234000", Name = "Natural and Physical Science Professionals nfd", CodeSystem = "ANZSCO")]
        NaturalandPhysicalScienceProfessionalsnfd,
        /// <summary>
        /// The agriculturaland forestry scientistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234100", Name = "Agricultural and Forestry Scientists nfd", CodeSystem = "ANZSCO")]
        AgriculturalandForestryScientistsnfd,
        /// <summary>
        /// The agricultural consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "234111", Name = "Agricultural Consultant", CodeSystem = "ANZSCO")]
        AgriculturalConsultant,
        /// <summary>
        /// The agricultural scientist
        /// </summary>
        [EnumMember]
        [Name(Code = "234112", Name = "Agricultural Scientist", CodeSystem = "ANZSCO")]
        AgriculturalScientist,
        /// <summary>
        /// The forester
        /// </summary>
        [EnumMember]
        [Name(Code = "234113", Name = "Forester", CodeSystem = "ANZSCO")]
        Forester,
        /// <summary>
        /// The chemistsand foodand wine scientistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234200", Name = "Chemists, and Food and Wine Scientists nfd", CodeSystem = "ANZSCO")]
        ChemistsandFoodandWineScientistsnfd,
        /// <summary>
        /// The chemist
        /// </summary>
        [EnumMember]
        [Name(Code = "234211", Name = "Chemist", CodeSystem = "ANZSCO")]
        Chemist,
        /// <summary>
        /// The food technologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234212", Name = "Food Technologist", CodeSystem = "ANZSCO")]
        FoodTechnologist,
        /// <summary>
        /// The wine maker
        /// </summary>
        [EnumMember]
        [Name(Code = "234213", Name = "Wine Maker", CodeSystem = "ANZSCO")]
        WineMaker,
        /// <summary>
        /// The environmental scientistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234300", Name = "Environmental Scientists nfd", CodeSystem = "ANZSCO")]
        EnvironmentalScientistsnfd,
        /// <summary>
        /// The conservation officer
        /// </summary>
        [EnumMember]
        [Name(Code = "234311", Name = "Conservation Officer", CodeSystem = "ANZSCO")]
        ConservationOfficer,
        /// <summary>
        /// The environmental consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "234312", Name = "Environmental Consultant", CodeSystem = "ANZSCO")]
        EnvironmentalConsultant,
        /// <summary>
        /// The environmental research scientist
        /// </summary>
        [EnumMember]
        [Name(Code = "234313", Name = "Environmental Research Scientist", CodeSystem = "ANZSCO")]
        EnvironmentalResearchScientist,
        /// <summary>
        /// The park ranger
        /// </summary>
        [EnumMember]
        [Name(Code = "234314", Name = "Park Ranger", CodeSystem = "ANZSCO")]
        ParkRanger,
        /// <summary>
        /// The environmental scientistsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "234399", Name = "Environmental Scientists nec", CodeSystem = "ANZSCO")]
        EnvironmentalScientistsnec,
        /// <summary>
        /// The geologistsand geophysicistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234400", Name = "Geologists and Geophysicists nfd", CodeSystem = "ANZSCO")]
        GeologistsandGeophysicistsnfd,
        /// <summary>
        /// The geologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234411", Name = "Geologist", CodeSystem = "ANZSCO")]
        Geologist,
        /// <summary>
        /// The geophysicist
        /// </summary>
        [EnumMember]
        [Name(Code = "234412", Name = "Geophysicist", CodeSystem = "ANZSCO")]
        Geophysicist,
        /// <summary>
        /// The life scientistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234500", Name = "Life Scientists nfd", CodeSystem = "ANZSCO")]
        LifeScientistsnfd,
        /// <summary>
        /// The life scientist general
        /// </summary>
        [EnumMember]
        [Name(Code = "234511", Name = "Life Scientist (General)", CodeSystem = "ANZSCO")]
        LifeScientistGeneral,
        /// <summary>
        /// The anatomistor physiologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234512", Name = "Anatomist or Physiologist", CodeSystem = "ANZSCO")]
        AnatomistorPhysiologist,
        /// <summary>
        /// The biochemist
        /// </summary>
        [EnumMember]
        [Name(Code = "234513", Name = "Biochemist", CodeSystem = "ANZSCO")]
        Biochemist,
        /// <summary>
        /// The biotechnologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234514", Name = "Biotechnologist", CodeSystem = "ANZSCO")]
        Biotechnologist,
        /// <summary>
        /// The botanist
        /// </summary>
        [EnumMember]
        [Name(Code = "234515", Name = "Botanist", CodeSystem = "ANZSCO")]
        Botanist,
        /// <summary>
        /// The marine biologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234516", Name = "Marine Biologist", CodeSystem = "ANZSCO")]
        MarineBiologist,
        /// <summary>
        /// The microbiologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234517", Name = "Microbiologist", CodeSystem = "ANZSCO")]
        Microbiologist,
        /// <summary>
        /// The zoologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234518", Name = "Zoologist", CodeSystem = "ANZSCO")]
        Zoologist,
        /// <summary>
        /// The life scientistsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "234599", Name = "Life Scientists nec", CodeSystem = "ANZSCO")]
        LifeScientistsnec,
        /// <summary>
        /// The medical laboratory scientist
        /// </summary>
        [EnumMember]
        [Name(Code = "234611", Name = "Medical Laboratory Scientist", CodeSystem = "ANZSCO")]
        MedicalLaboratoryScientist,
        /// <summary>
        /// The veterinarian
        /// </summary>
        [EnumMember]
        [Name(Code = "234711", Name = "Veterinarian", CodeSystem = "ANZSCO")]
        Veterinarian,
        /// <summary>
        /// The other naturaland physical science professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "234900", Name = "Other Natural and Physical Science Professionals nfd", CodeSystem = "ANZSCO")]
        OtherNaturalandPhysicalScienceProfessionalsnfd,
        /// <summary>
        /// The conservator
        /// </summary>
        [EnumMember]
        [Name(Code = "234911", Name = "Conservator", CodeSystem = "ANZSCO")]
        Conservator,
        /// <summary>
        /// The metallurgist
        /// </summary>
        [EnumMember]
        [Name(Code = "234912", Name = "Metallurgist", CodeSystem = "ANZSCO")]
        Metallurgist,
        /// <summary>
        /// The meteorologist
        /// </summary>
        [EnumMember]
        [Name(Code = "234913", Name = "Meteorologist", CodeSystem = "ANZSCO")]
        Meteorologist,
        /// <summary>
        /// The physicist
        /// </summary>
        [EnumMember]
        [Name(Code = "234914", Name = "Physicist", CodeSystem = "ANZSCO")]
        Physicist,
        /// <summary>
        /// The naturaland physical science professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "234999", Name = "Natural and Physical Science Professionals nec", CodeSystem = "ANZSCO")]
        NaturalandPhysicalScienceProfessionalsnec,
        /// <summary>
        /// The education professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "240000", Name = "Education Professionals nfd", CodeSystem = "ANZSCO")]
        EducationProfessionalsnfd,
        /// <summary>
        /// The school teachersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "241000", Name = "School Teachers nfd", CodeSystem = "ANZSCO")]
        SchoolTeachersnfd,
        /// <summary>
        /// The early childhood preprimary school teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "241111", Name = "Early Childhood (Pre-primary School) Teacher", CodeSystem = "ANZSCO")]
        EarlyChildhoodPreprimarySchoolTeacher,
        /// <summary>
        /// The primary school teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "241213", Name = "Primary School Teacher", CodeSystem = "ANZSCO")]
        PrimarySchoolTeacher,
        /// <summary>
        /// The middle school teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "241311", Name = "Middle School Teacher", CodeSystem = "ANZSCO")]
        MiddleSchoolTeacher,
        /// <summary>
        /// The secondary school teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "241411", Name = "Secondary School Teacher", CodeSystem = "ANZSCO")]
        SecondarySchoolTeacher,
        /// <summary>
        /// The special education teachersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "241500", Name = "Special Education Teachers nfd", CodeSystem = "ANZSCO")]
        SpecialEducationTeachersnfd,
        /// <summary>
        /// The special needs teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "241511", Name = "Special Needs Teacher", CodeSystem = "ANZSCO")]
        SpecialNeedsTeacher,
        /// <summary>
        /// The teacherofthe hearing impaired
        /// </summary>
        [EnumMember]
        [Name(Code = "241512", Name = "Teacher of the Hearing Impaired", CodeSystem = "ANZSCO")]
        TeacheroftheHearingImpaired,
        /// <summary>
        /// The teacherofthe sight impaired
        /// </summary>
        [EnumMember]
        [Name(Code = "241513", Name = "Teacher of the Sight Impaired", CodeSystem = "ANZSCO")]
        TeacheroftheSightImpaired,
        /// <summary>
        /// The special education teachersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "241599", Name = "Special Education Teachers nec", CodeSystem = "ANZSCO")]
        SpecialEducationTeachersnec,
        /// <summary>
        /// The tertiary education teachersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "242000", Name = "Tertiary Education Teachers nfd", CodeSystem = "ANZSCO")]
        TertiaryEducationTeachersnfd,
        /// <summary>
        /// The university lecturersand tutorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "242100", Name = "University Lecturers and Tutors nfd", CodeSystem = "ANZSCO")]
        UniversityLecturersandTutorsnfd,
        /// <summary>
        /// The university lecturer
        /// </summary>
        [EnumMember]
        [Name(Code = "242111", Name = "University Lecturer", CodeSystem = "ANZSCO")]
        UniversityLecturer,
        /// <summary>
        /// The university tutor
        /// </summary>
        [EnumMember]
        [Name(Code = "242112", Name = "University Tutor", CodeSystem = "ANZSCO")]
        UniversityTutor,
        /// <summary>
        /// The vocational education teacher
        /// </summary>
        [EnumMember]
        [Name(Code = "242211", Name = "Vocational Education Teacher", CodeSystem = "ANZSCO")]
        VocationalEducationTeacher,
        /// <summary>
        /// The miscellaneous education professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "249000", Name = "Miscellaneous Education Professionals nfd", CodeSystem = "ANZSCO")]
        MiscellaneousEducationProfessionalsnfd,
        /// <summary>
        /// The education advisersand reviewersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "249100", Name = "Education Advisers and Reviewers nfd", CodeSystem = "ANZSCO")]
        EducationAdvisersandReviewersnfd,
        /// <summary>
        /// The education adviser
        /// </summary>
        [EnumMember]
        [Name(Code = "249111", Name = "Education Adviser", CodeSystem = "ANZSCO")]
        EducationAdviser,
        /// <summary>
        /// The education reviewer
        /// </summary>
        [EnumMember]
        [Name(Code = "249112", Name = "Education Reviewer", CodeSystem = "ANZSCO")]
        EducationReviewer,
        /// <summary>
        /// The private tutorsand teachersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "249200", Name = "Private Tutors and Teachers nfd", CodeSystem = "ANZSCO")]
        PrivateTutorsandTeachersnfd,
        /// <summary>
        /// The art teacher private tuition
        /// </summary>
        [EnumMember]
        [Name(Code = "249211", Name = "Art Teacher (Private Tuition)", CodeSystem = "ANZSCO")]
        ArtTeacherPrivateTuition,
        /// <summary>
        /// The dance teacher private tuition
        /// </summary>
        [EnumMember]
        [Name(Code = "249212", Name = "Dance Teacher (Private Tuition)", CodeSystem = "ANZSCO")]
        DanceTeacherPrivateTuition,
        /// <summary>
        /// The drama teacher private tuition
        /// </summary>
        [EnumMember]
        [Name(Code = "249213", Name = "Drama Teacher (Private Tuition)", CodeSystem = "ANZSCO")]
        DramaTeacherPrivateTuition,
        /// <summary>
        /// The music teacher private tuition
        /// </summary>
        [EnumMember]
        [Name(Code = "249214", Name = "Music Teacher (Private Tuition)", CodeSystem = "ANZSCO")]
        MusicTeacherPrivateTuition,
        /// <summary>
        /// The private tutorsand teachersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "249299", Name = "Private Tutors and Teachers nec", CodeSystem = "ANZSCO")]
        PrivateTutorsandTeachersnec,
        /// <summary>
        /// The teacherof englishto speakersof other languages
        /// </summary>
        [EnumMember]
        [Name(Code = "249311", Name = "Teacher of English to Speakers of Other Languages", CodeSystem = "ANZSCO")]
        TeacherofEnglishtoSpeakersofOtherLanguages,
        /// <summary>
        /// The health professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "250000", Name = "Health Professionals nfd", CodeSystem = "ANZSCO")]
        HealthProfessionalsnfd,
        /// <summary>
        /// The health diagnosticand promotion professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251000", Name = "Health Diagnostic and Promotion Professionals nfd", CodeSystem = "ANZSCO")]
        HealthDiagnosticandPromotionProfessionalsnfd,
        /// <summary>
        /// The dietitian
        /// </summary>
        [EnumMember]
        [Name(Code = "251111", Name = "Dietitian", CodeSystem = "ANZSCO")]
        Dietitian,
        /// <summary>
        /// The medical imaging professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251200", Name = "Medical Imaging Professionals nfd", CodeSystem = "ANZSCO")]
        MedicalImagingProfessionalsnfd,
        /// <summary>
        /// The medical diagnostic radiographer
        /// </summary>
        [EnumMember]
        [Name(Code = "251211", Name = "Medical Diagnostic Radiographer", CodeSystem = "ANZSCO")]
        MedicalDiagnosticRadiographer,
        /// <summary>
        /// The medical radiation therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "251212", Name = "Medical Radiation Therapist", CodeSystem = "ANZSCO")]
        MedicalRadiationTherapist,
        /// <summary>
        /// The nuclear medicine technologist
        /// </summary>
        [EnumMember]
        [Name(Code = "251213", Name = "Nuclear Medicine Technologist", CodeSystem = "ANZSCO")]
        NuclearMedicineTechnologist,
        /// <summary>
        /// The sonographer
        /// </summary>
        [EnumMember]
        [Name(Code = "251214", Name = "Sonographer", CodeSystem = "ANZSCO")]
        Sonographer,
        /// <summary>
        /// The occupationaland environmental health professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251300", Name = "Occupational and Environmental Health Professionals nfd", CodeSystem = "ANZSCO")]
        OccupationalandEnvironmentalHealthProfessionalsnfd,
        /// <summary>
        /// The environmental health officer
        /// </summary>
        [EnumMember]
        [Name(Code = "251311", Name = "Environmental Health Officer", CodeSystem = "ANZSCO")]
        EnvironmentalHealthOfficer,
        /// <summary>
        /// The occupational healthand safety adviser
        /// </summary>
        [EnumMember]
        [Name(Code = "251312", Name = "Occupational Health and Safety Adviser", CodeSystem = "ANZSCO")]
        OccupationalHealthandSafetyAdviser,
        /// <summary>
        /// The optometristsand orthoptistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251400", Name = "Optometrists and Orthoptists nfd", CodeSystem = "ANZSCO")]
        OptometristsandOrthoptistsnfd,
        /// <summary>
        /// The optometrist
        /// </summary>
        [EnumMember]
        [Name(Code = "251411", Name = "Optometrist", CodeSystem = "ANZSCO")]
        Optometrist,
        /// <summary>
        /// The orthoptist
        /// </summary>
        [EnumMember]
        [Name(Code = "251412", Name = "Orthoptist", CodeSystem = "ANZSCO")]
        Orthoptist,
        /// <summary>
        /// The pharmacistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251500", Name = "Pharmacists nfd", CodeSystem = "ANZSCO")]
        Pharmacistsnfd,
        /// <summary>
        /// The hospital pharmacist
        /// </summary>
        [EnumMember]
        [Name(Code = "251511", Name = "Hospital Pharmacist", CodeSystem = "ANZSCO")]
        HospitalPharmacist,
        /// <summary>
        /// The industrial pharmacist
        /// </summary>
        [EnumMember]
        [Name(Code = "251512", Name = "Industrial Pharmacist", CodeSystem = "ANZSCO")]
        IndustrialPharmacist,
        /// <summary>
        /// The retail pharmacist
        /// </summary>
        [EnumMember]
        [Name(Code = "251513", Name = "Retail Pharmacist", CodeSystem = "ANZSCO")]
        RetailPharmacist,
        /// <summary>
        /// The other health diagnosticand promotion professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "251900", Name = "Other Health Diagnostic and Promotion Professionals nfd", CodeSystem = "ANZSCO")]
        OtherHealthDiagnosticandPromotionProfessionalsnfd,
        /// <summary>
        /// The health promotion officer
        /// </summary>
        [EnumMember]
        [Name(Code = "251911", Name = "Health Promotion Officer", CodeSystem = "ANZSCO")]
        HealthPromotionOfficer,
        /// <summary>
        /// The orthotistor prosthetist
        /// </summary>
        [EnumMember]
        [Name(Code = "251912", Name = "Orthotist or Prosthetist", CodeSystem = "ANZSCO")]
        OrthotistorProsthetist,
        /// <summary>
        /// The health diagnosticand promotion professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "251999", Name = "Health Diagnostic and Promotion Professionals nec", CodeSystem = "ANZSCO")]
        HealthDiagnosticandPromotionProfessionalsnec,
        /// <summary>
        /// The health therapy professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "252000", Name = "Health Therapy Professionals nfd", CodeSystem = "ANZSCO")]
        HealthTherapyProfessionalsnfd,
        /// <summary>
        /// The chiropractorsand osteopathsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "252100", Name = "Chiropractors and Osteopaths nfd", CodeSystem = "ANZSCO")]
        ChiropractorsandOsteopathsnfd,
        /// <summary>
        /// The chiropractor
        /// </summary>
        [EnumMember]
        [Name(Code = "252111", Name = "Chiropractor", CodeSystem = "ANZSCO")]
        Chiropractor,
        /// <summary>
        /// The osteopath
        /// </summary>
        [EnumMember]
        [Name(Code = "252112", Name = "Osteopath", CodeSystem = "ANZSCO")]
        Osteopath,
        /// <summary>
        /// The complementary health therapistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "252200", Name = "Complementary Health Therapists nfd", CodeSystem = "ANZSCO")]
        ComplementaryHealthTherapistsnfd,
        /// <summary>
        /// The acupuncturist
        /// </summary>
        [EnumMember]
        [Name(Code = "252211", Name = "Acupuncturist", CodeSystem = "ANZSCO")]
        Acupuncturist,
        /// <summary>
        /// The homoeopath
        /// </summary>
        [EnumMember]
        [Name(Code = "252212", Name = "Homoeopath", CodeSystem = "ANZSCO")]
        Homoeopath,
        /// <summary>
        /// The naturopath
        /// </summary>
        [EnumMember]
        [Name(Code = "252213", Name = "Naturopath", CodeSystem = "ANZSCO")]
        Naturopath,
        /// <summary>
        /// The traditional chinese medicine practitioner
        /// </summary>
        [EnumMember]
        [Name(Code = "252214", Name = "Traditional Chinese Medicine Practitioner", CodeSystem = "ANZSCO")]
        TraditionalChineseMedicinePractitioner,
        /// <summary>
        /// The complementary health therapistsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "252299", Name = "Complementary Health Therapists nec", CodeSystem = "ANZSCO")]
        ComplementaryHealthTherapistsnec,
        /// <summary>
        /// The dental practitionersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "252300", Name = "Dental Practitioners nfd", CodeSystem = "ANZSCO")]
        DentalPractitionersnfd,
        /// <summary>
        /// The dental specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "252311", Name = "Dental Specialist", CodeSystem = "ANZSCO")]
        DentalSpecialist,
        /// <summary>
        /// The dentist
        /// </summary>
        [EnumMember]
        [Name(Code = "252312", Name = "Dentist", CodeSystem = "ANZSCO")]
        Dentist,
        /// <summary>
        /// The occupational therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "252411", Name = "Occupational Therapist", CodeSystem = "ANZSCO")]
        OccupationalTherapist,
        /// <summary>
        /// The physiotherapist
        /// </summary>
        [EnumMember]
        [Name(Code = "252511", Name = "Physiotherapist", CodeSystem = "ANZSCO")]
        Physiotherapist,
        /// <summary>
        /// The podiatrist
        /// </summary>
        [EnumMember]
        [Name(Code = "252611", Name = "Podiatrist", CodeSystem = "ANZSCO")]
        Podiatrist,
        /// <summary>
        /// The speech professionalsand audiologistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "252700", Name = "Speech Professionals and Audiologists nfd", CodeSystem = "ANZSCO")]
        SpeechProfessionalsandAudiologistsnfd,
        /// <summary>
        /// The audiologist
        /// </summary>
        [EnumMember]
        [Name(Code = "252711", Name = "Audiologist", CodeSystem = "ANZSCO")]
        Audiologist,
        /// <summary>
        /// The speech pathologist
        /// </summary>
        [EnumMember]
        [Name(Code = "252712", Name = "Speech Pathologist", CodeSystem = "ANZSCO")]
        SpeechPathologist,
        /// <summary>
        /// The medical practitionersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "253000", Name = "Medical Practitioners nfd", CodeSystem = "ANZSCO")]
        MedicalPractitionersnfd,
        /// <summary>
        /// The generalist medical practitionersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "253100", Name = "Generalist Medical Practitioners nfd", CodeSystem = "ANZSCO")]
        GeneralistMedicalPractitionersnfd,
        /// <summary>
        /// The general medical practitioner
        /// </summary>
        [EnumMember]
        [Name(Code = "253111", Name = "General Medical Practitioner", CodeSystem = "ANZSCO")]
        GeneralMedicalPractitioner,
        /// <summary>
        /// The resident medical officer
        /// </summary>
        [EnumMember]
        [Name(Code = "253112", Name = "Resident Medical Officer", CodeSystem = "ANZSCO")]
        ResidentMedicalOfficer,
        /// <summary>
        /// The anaesthetist
        /// </summary>
        [EnumMember]
        [Name(Code = "253211", Name = "Anaesthetist", CodeSystem = "ANZSCO")]
        Anaesthetist,
        /// <summary>
        /// The specialist physiciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "253300", Name = "Specialist Physicians nfd", CodeSystem = "ANZSCO")]
        SpecialistPhysiciansnfd,
        /// <summary>
        /// The specialist physician general medicine
        /// </summary>
        [EnumMember]
        [Name(Code = "253311", Name = "Specialist Physician (General Medicine)", CodeSystem = "ANZSCO")]
        SpecialistPhysicianGeneralMedicine,
        /// <summary>
        /// The cardiologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253312", Name = "Cardiologist", CodeSystem = "ANZSCO")]
        Cardiologist,
        /// <summary>
        /// The clinical haematologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253313", Name = "Clinical Haematologist", CodeSystem = "ANZSCO")]
        ClinicalHaematologist,
        /// <summary>
        /// The medical oncologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253314", Name = "Medical Oncologist", CodeSystem = "ANZSCO")]
        MedicalOncologist,
        /// <summary>
        /// The endocrinologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253315", Name = "Endocrinologist", CodeSystem = "ANZSCO")]
        Endocrinologist,
        /// <summary>
        /// The gastroenterologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253316", Name = "Gastroenterologist", CodeSystem = "ANZSCO")]
        Gastroenterologist,
        /// <summary>
        /// The intensive care specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "253317", Name = "Intensive Care Specialist", CodeSystem = "ANZSCO")]
        IntensiveCareSpecialist,
        /// <summary>
        /// The neurologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253318", Name = "Neurologist", CodeSystem = "ANZSCO")]
        Neurologist,
        /// <summary>
        /// The paediatrician
        /// </summary>
        [EnumMember]
        [Name(Code = "253321", Name = "Paediatrician", CodeSystem = "ANZSCO")]
        Paediatrician,
        /// <summary>
        /// The renal medicine specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "253322", Name = "Renal Medicine Specialist", CodeSystem = "ANZSCO")]
        RenalMedicineSpecialist,
        /// <summary>
        /// The rheumatologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253323", Name = "Rheumatologist", CodeSystem = "ANZSCO")]
        Rheumatologist,
        /// <summary>
        /// The thoracic medicine specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "253324", Name = "Thoracic Medicine Specialist", CodeSystem = "ANZSCO")]
        ThoracicMedicineSpecialist,
        /// <summary>
        /// The specialist physiciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "253399", Name = "Specialist Physicians nec", CodeSystem = "ANZSCO")]
        SpecialistPhysiciansnec,
        /// <summary>
        /// The psychiatrist
        /// </summary>
        [EnumMember]
        [Name(Code = "253411", Name = "Psychiatrist", CodeSystem = "ANZSCO")]
        Psychiatrist,
        /// <summary>
        /// The surgeonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "253500", Name = "Surgeons nfd", CodeSystem = "ANZSCO")]
        Surgeonsnfd,
        /// <summary>
        /// The surgeon general
        /// </summary>
        [EnumMember]
        [Name(Code = "253511", Name = "Surgeon (General)", CodeSystem = "ANZSCO")]
        SurgeonGeneral,
        /// <summary>
        /// The cardiothoracic surgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253512", Name = "Cardiothoracic Surgeon", CodeSystem = "ANZSCO")]
        CardiothoracicSurgeon,
        /// <summary>
        /// The neurosurgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253513", Name = "Neurosurgeon", CodeSystem = "ANZSCO")]
        Neurosurgeon,
        /// <summary>
        /// The orthopaedic surgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253514", Name = "Orthopaedic Surgeon", CodeSystem = "ANZSCO")]
        OrthopaedicSurgeon,
        /// <summary>
        /// The otorhinolaryngologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253515", Name = "Otorhinolaryngologist", CodeSystem = "ANZSCO")]
        Otorhinolaryngologist,
        /// <summary>
        /// The paediatric surgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253516", Name = "Paediatric Surgeon", CodeSystem = "ANZSCO")]
        PaediatricSurgeon,
        /// <summary>
        /// The plasticand reconstructive surgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253517", Name = "Plastic and Reconstructive Surgeon", CodeSystem = "ANZSCO")]
        PlasticandReconstructiveSurgeon,
        /// <summary>
        /// The urologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253518", Name = "Urologist", CodeSystem = "ANZSCO")]
        Urologist,
        /// <summary>
        /// The vascular surgeon
        /// </summary>
        [EnumMember]
        [Name(Code = "253521", Name = "Vascular Surgeon", CodeSystem = "ANZSCO")]
        VascularSurgeon,
        /// <summary>
        /// The other medical practitionersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "253900", Name = "Other Medical Practitioners nfd", CodeSystem = "ANZSCO")]
        OtherMedicalPractitionersnfd,
        /// <summary>
        /// The dermatologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253911", Name = "Dermatologist", CodeSystem = "ANZSCO")]
        Dermatologist,
        /// <summary>
        /// The emergency medicine specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "253912", Name = "Emergency Medicine Specialist", CodeSystem = "ANZSCO")]
        EmergencyMedicineSpecialist,
        /// <summary>
        /// The obstetricianand gynaecologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253913", Name = "Obstetrician and Gynaecologist", CodeSystem = "ANZSCO")]
        ObstetricianandGynaecologist,
        /// <summary>
        /// The ophthalmologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253914", Name = "Ophthalmologist", CodeSystem = "ANZSCO")]
        Ophthalmologist,
        /// <summary>
        /// The pathologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253915", Name = "Pathologist", CodeSystem = "ANZSCO")]
        Pathologist,
        /// <summary>
        /// The diagnosticand interventional radiologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253917", Name = "Diagnostic and Interventional Radiologist", CodeSystem = "ANZSCO")]
        DiagnosticandInterventionalRadiologist,
        /// <summary>
        /// The radiation oncologist
        /// </summary>
        [EnumMember]
        [Name(Code = "253918", Name = "Radiation Oncologist", CodeSystem = "ANZSCO")]
        RadiationOncologist,
        /// <summary>
        /// The medical practitionersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "253999", Name = "Medical Practitioners nec", CodeSystem = "ANZSCO")]
        MedicalPractitionersnec,
        /// <summary>
        /// The midwiferyand nursing professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "254000", Name = "Midwifery and Nursing Professionals nfd", CodeSystem = "ANZSCO")]
        MidwiferyandNursingProfessionalsnfd,
        /// <summary>
        /// The midwife
        /// </summary>
        [EnumMember]
        [Name(Code = "254111", Name = "Midwife", CodeSystem = "ANZSCO")]
        Midwife,
        /// <summary>
        /// The nurse educatorsand researchersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "254200", Name = "Nurse Educators and Researchers nfd", CodeSystem = "ANZSCO")]
        NurseEducatorsandResearchersnfd,
        /// <summary>
        /// The nurse educator
        /// </summary>
        [EnumMember]
        [Name(Code = "254211", Name = "Nurse Educator", CodeSystem = "ANZSCO")]
        NurseEducator,
        /// <summary>
        /// The nurse researcher
        /// </summary>
        [EnumMember]
        [Name(Code = "254212", Name = "Nurse Researcher", CodeSystem = "ANZSCO")]
        NurseResearcher,
        /// <summary>
        /// The nurse manager
        /// </summary>
        [EnumMember]
        [Name(Code = "254311", Name = "Nurse Manager", CodeSystem = "ANZSCO")]
        NurseManager,
        /// <summary>
        /// The registered nursesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "254400", Name = "Registered Nurses nfd", CodeSystem = "ANZSCO")]
        RegisteredNursesnfd,
        /// <summary>
        /// The nurse practitioner
        /// </summary>
        [EnumMember]
        [Name(Code = "254411", Name = "Nurse Practitioner", CodeSystem = "ANZSCO")]
        NursePractitioner,
        /// <summary>
        /// The registered nurse aged care
        /// </summary>
        [EnumMember]
        [Name(Code = "254412", Name = "Registered Nurse (Aged Care)", CodeSystem = "ANZSCO")]
        RegisteredNurseAgedCare,
        /// <summary>
        /// The registered nurse childand family health
        /// </summary>
        [EnumMember]
        [Name(Code = "254413", Name = "Registered Nurse (Child and Family Health)", CodeSystem = "ANZSCO")]
        RegisteredNurseChildandFamilyHealth,
        /// <summary>
        /// The registered nurse community health
        /// </summary>
        [EnumMember]
        [Name(Code = "254414", Name = "Registered Nurse (Community Health)", CodeSystem = "ANZSCO")]
        RegisteredNurseCommunityHealth,
        /// <summary>
        /// The registered nurse critical careand emergency
        /// </summary>
        [EnumMember]
        [Name(Code = "254415", Name = "Registered Nurse (Critical Care and Emergency)", CodeSystem = "ANZSCO")]
        RegisteredNurseCriticalCareandEmergency,
        /// <summary>
        /// The registered nurse developmental disability
        /// </summary>
        [EnumMember]
        [Name(Code = "254416", Name = "Registered Nurse (Developmental Disability)", CodeSystem = "ANZSCO")]
        RegisteredNurseDevelopmentalDisability,
        /// <summary>
        /// The registered nurse disabilityand rehabilitation
        /// </summary>
        [EnumMember]
        [Name(Code = "254417", Name = "Registered Nurse (Disability and Rehabilitation)", CodeSystem = "ANZSCO")]
        RegisteredNurseDisabilityandRehabilitation,
        /// <summary>
        /// The registered nurse medical
        /// </summary>
        [EnumMember]
        [Name(Code = "254418", Name = "Registered Nurse (Medical)", CodeSystem = "ANZSCO")]
        RegisteredNurseMedical,
        /// <summary>
        /// The registered nurse medical practice
        /// </summary>
        [EnumMember]
        [Name(Code = "254421", Name = "Registered Nurse (Medical Practice)", CodeSystem = "ANZSCO")]
        RegisteredNurseMedicalPractice,
        /// <summary>
        /// The registered nurse mental health
        /// </summary>
        [EnumMember]
        [Name(Code = "254422", Name = "Registered Nurse (Mental Health)", CodeSystem = "ANZSCO")]
        RegisteredNurseMentalHealth,
        /// <summary>
        /// The registered nurse perioperative
        /// </summary>
        [EnumMember]
        [Name(Code = "254423", Name = "Registered Nurse (Perioperative)", CodeSystem = "ANZSCO")]
        RegisteredNursePerioperative,
        /// <summary>
        /// The registered nurse surgical
        /// </summary>
        [EnumMember]
        [Name(Code = "254424", Name = "Registered Nurse (Surgical)", CodeSystem = "ANZSCO")]
        RegisteredNurseSurgical,
        /// <summary>
        /// The registered nursesnec
        /// </summary>
        [EnumMember]
        [Name(Code = "254499", Name = "Registered Nurses nec", CodeSystem = "ANZSCO")]
        RegisteredNursesnec,
        /// <summary>
        /// The ICT professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "260000", Name = "ICT Professionals nfd", CodeSystem = "ANZSCO")]
        ICTProfessionalsnfd,
        /// <summary>
        /// The businessand systems analystsand programmersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "261000", Name = "Business and Systems Analysts, and Programmers nfd", CodeSystem = "ANZSCO")]
        BusinessandSystemsAnalystsandProgrammersnfd,
        /// <summary>
        /// The ICT businessand systems analystsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "261100", Name = "ICT Business and Systems Analysts nfd", CodeSystem = "ANZSCO")]
        ICTBusinessandSystemsAnalystsnfd,
        /// <summary>
        /// The ICT business analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "261111", Name = "ICT Business Analyst", CodeSystem = "ANZSCO")]
        ICTBusinessAnalyst,
        /// <summary>
        /// The systems analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "261112", Name = "Systems Analyst", CodeSystem = "ANZSCO")]
        SystemsAnalyst,
        /// <summary>
        /// The multimedia specialistsand web developersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "261200", Name = "Multimedia Specialists and Web Developers nfd", CodeSystem = "ANZSCO")]
        MultimediaSpecialistsandWebDevelopersnfd,
        /// <summary>
        /// The multimedia specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "261211", Name = "Multimedia Specialist", CodeSystem = "ANZSCO")]
        MultimediaSpecialist,
        /// <summary>
        /// The web developer
        /// </summary>
        [EnumMember]
        [Name(Code = "261212", Name = "Web Developer", CodeSystem = "ANZSCO")]
        WebDeveloper,
        /// <summary>
        /// The softwareand applications programmersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "261300", Name = "Software and Applications Programmers nfd", CodeSystem = "ANZSCO")]
        SoftwareandApplicationsProgrammersnfd,
        /// <summary>
        /// The analyst programmer
        /// </summary>
        [EnumMember]
        [Name(Code = "261311", Name = "Analyst Programmer", CodeSystem = "ANZSCO")]
        AnalystProgrammer,
        /// <summary>
        /// The developer programmer
        /// </summary>
        [EnumMember]
        [Name(Code = "261312", Name = "Developer Programmer", CodeSystem = "ANZSCO")]
        DeveloperProgrammer,
        /// <summary>
        /// The software engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "261313", Name = "Software Engineer", CodeSystem = "ANZSCO")]
        SoftwareEngineer,
        /// <summary>
        /// The software tester
        /// </summary>
        [EnumMember]
        [Name(Code = "261314", Name = "Software Tester", CodeSystem = "ANZSCO")]
        SoftwareTester,
        /// <summary>
        /// The softwareand applications programmersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "261399", Name = "Software and Applications Programmers nec", CodeSystem = "ANZSCO")]
        SoftwareandApplicationsProgrammersnec,
        /// <summary>
        /// The databaseand systems administratorsand ICT security specialistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "262100", Name = "Database and Systems Administrators, and ICT Security Specialists nfd", CodeSystem = "ANZSCO")]
        DatabaseandSystemsAdministratorsandICTSecuritySpecialistsnfd,
        /// <summary>
        /// The database administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "262111", Name = "Database Administrator", CodeSystem = "ANZSCO")]
        DatabaseAdministrator,
        /// <summary>
        /// The ICT security specialist
        /// </summary>
        [EnumMember]
        [Name(Code = "262112", Name = "ICT Security Specialist", CodeSystem = "ANZSCO")]
        ICTSecuritySpecialist,
        /// <summary>
        /// The systems administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "262113", Name = "Systems Administrator", CodeSystem = "ANZSCO")]
        SystemsAdministrator,
        /// <summary>
        /// The ICT networkand support professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "263000", Name = "ICT Network and Support Professionals nfd", CodeSystem = "ANZSCO")]
        ICTNetworkandSupportProfessionalsnfd,
        /// <summary>
        /// The computer network professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "263100", Name = "Computer Network Professionals nfd", CodeSystem = "ANZSCO")]
        ComputerNetworkProfessionalsnfd,
        /// <summary>
        /// The computer networkand systems engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263111", Name = "Computer Network and Systems Engineer", CodeSystem = "ANZSCO")]
        ComputerNetworkandSystemsEngineer,
        /// <summary>
        /// The network administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "263112", Name = "Network Administrator", CodeSystem = "ANZSCO")]
        NetworkAdministrator,
        /// <summary>
        /// The network analyst
        /// </summary>
        [EnumMember]
        [Name(Code = "263113", Name = "Network Analyst", CodeSystem = "ANZSCO")]
        NetworkAnalyst,
        /// <summary>
        /// The ICT supportand test engineersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "263200", Name = "ICT Support and Test Engineers nfd", CodeSystem = "ANZSCO")]
        ICTSupportandTestEngineersnfd,
        /// <summary>
        /// The ICT quality assurance engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263211", Name = "ICT Quality Assurance Engineer", CodeSystem = "ANZSCO")]
        ICTQualityAssuranceEngineer,
        /// <summary>
        /// The ICT support engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263212", Name = "ICT Support Engineer", CodeSystem = "ANZSCO")]
        ICTSupportEngineer,
        /// <summary>
        /// The ICT systems test engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263213", Name = "ICT Systems Test Engineer", CodeSystem = "ANZSCO")]
        ICTSystemsTestEngineer,
        /// <summary>
        /// The ICT supportand test engineersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "263299", Name = "ICT Support and Test Engineers nec", CodeSystem = "ANZSCO")]
        ICTSupportandTestEngineersnec,
        /// <summary>
        /// The telecommunications engineering professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "263300", Name = "Telecommunications Engineering Professionals nfd", CodeSystem = "ANZSCO")]
        TelecommunicationsEngineeringProfessionalsnfd,
        /// <summary>
        /// The telecommunications engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263311", Name = "Telecommunications Engineer", CodeSystem = "ANZSCO")]
        TelecommunicationsEngineer,
        /// <summary>
        /// The telecommunications network engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "263312", Name = "Telecommunications Network Engineer", CodeSystem = "ANZSCO")]
        TelecommunicationsNetworkEngineer,
        /// <summary>
        /// The legal socialand welfare professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "270000", Name = "Legal, Social and Welfare Professionals nfd", CodeSystem = "ANZSCO")]
        LegalSocialandWelfareProfessionalsnfd,
        /// <summary>
        /// The legal professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "271000", Name = "Legal Professionals nfd", CodeSystem = "ANZSCO")]
        LegalProfessionalsnfd,
        /// <summary>
        /// The barrister
        /// </summary>
        [EnumMember]
        [Name(Code = "271111", Name = "Barrister", CodeSystem = "ANZSCO")]
        Barrister,
        /// <summary>
        /// The judicialand other legal professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "271200", Name = "Judicial and Other Legal Professionals nfd", CodeSystem = "ANZSCO")]
        JudicialandOtherLegalProfessionalsnfd,
        /// <summary>
        /// The judge
        /// </summary>
        [EnumMember]
        [Name(Code = "271211", Name = "Judge", CodeSystem = "ANZSCO")]
        Judge,
        /// <summary>
        /// The magistrate
        /// </summary>
        [EnumMember]
        [Name(Code = "271212", Name = "Magistrate", CodeSystem = "ANZSCO")]
        Magistrate,
        /// <summary>
        /// The tribunal member
        /// </summary>
        [EnumMember]
        [Name(Code = "271213", Name = "Tribunal Member", CodeSystem = "ANZSCO")]
        TribunalMember,
        /// <summary>
        /// The judicialand other legal professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "271299", Name = "Judicial and Other Legal Professionals nec", CodeSystem = "ANZSCO")]
        JudicialandOtherLegalProfessionalsnec,
        /// <summary>
        /// The solicitor
        /// </summary>
        [EnumMember]
        [Name(Code = "271311", Name = "Solicitor", CodeSystem = "ANZSCO")]
        Solicitor,
        /// <summary>
        /// The socialand welfare professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "272000", Name = "Social and Welfare Professionals nfd", CodeSystem = "ANZSCO")]
        SocialandWelfareProfessionalsnfd,
        /// <summary>
        /// The counsellorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "272100", Name = "Counsellors nfd", CodeSystem = "ANZSCO")]
        Counsellorsnfd,
        /// <summary>
        /// The careers counsellor
        /// </summary>
        [EnumMember]
        [Name(Code = "272111", Name = "Careers Counsellor", CodeSystem = "ANZSCO")]
        CareersCounsellor,
        /// <summary>
        /// The drugand alcohol counsellor
        /// </summary>
        [EnumMember]
        [Name(Code = "272112", Name = "Drug and Alcohol Counsellor", CodeSystem = "ANZSCO")]
        DrugandAlcoholCounsellor,
        /// <summary>
        /// The familyand marriage counsellor
        /// </summary>
        [EnumMember]
        [Name(Code = "272113", Name = "Family and Marriage Counsellor", CodeSystem = "ANZSCO")]
        FamilyandMarriageCounsellor,
        /// <summary>
        /// The rehabilitation counsellor
        /// </summary>
        [EnumMember]
        [Name(Code = "272114", Name = "Rehabilitation Counsellor", CodeSystem = "ANZSCO")]
        RehabilitationCounsellor,
        /// <summary>
        /// The student counsellor
        /// </summary>
        [EnumMember]
        [Name(Code = "272115", Name = "Student Counsellor", CodeSystem = "ANZSCO")]
        StudentCounsellor,
        /// <summary>
        /// The counsellorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "272199", Name = "Counsellors nec", CodeSystem = "ANZSCO")]
        Counsellorsnec,
        /// <summary>
        /// The ministerof religion
        /// </summary>
        [EnumMember]
        [Name(Code = "272211", Name = "Minister of Religion", CodeSystem = "ANZSCO")]
        MinisterofReligion,
        /// <summary>
        /// The psychologistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "272300", Name = "Psychologists nfd", CodeSystem = "ANZSCO")]
        Psychologistsnfd,
        /// <summary>
        /// The clinical psychologist
        /// </summary>
        [EnumMember]
        [Name(Code = "272311", Name = "Clinical Psychologist", CodeSystem = "ANZSCO")]
        ClinicalPsychologist,
        /// <summary>
        /// The educational psychologist
        /// </summary>
        [EnumMember]
        [Name(Code = "272312", Name = "Educational Psychologist", CodeSystem = "ANZSCO")]
        EducationalPsychologist,
        /// <summary>
        /// The organisational psychologist
        /// </summary>
        [EnumMember]
        [Name(Code = "272313", Name = "Organisational Psychologist", CodeSystem = "ANZSCO")]
        OrganisationalPsychologist,
        /// <summary>
        /// The psychotherapist
        /// </summary>
        [EnumMember]
        [Name(Code = "272314", Name = "Psychotherapist", CodeSystem = "ANZSCO")]
        Psychotherapist,
        /// <summary>
        /// The psychologistsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "272399", Name = "Psychologists nec", CodeSystem = "ANZSCO")]
        Psychologistsnec,
        /// <summary>
        /// The social professionalsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "272400", Name = "Social Professionals nfd", CodeSystem = "ANZSCO")]
        SocialProfessionalsnfd,
        /// <summary>
        /// The historian
        /// </summary>
        [EnumMember]
        [Name(Code = "272411", Name = "Historian", CodeSystem = "ANZSCO")]
        Historian,
        /// <summary>
        /// The interpreter
        /// </summary>
        [EnumMember]
        [Name(Code = "272412", Name = "Interpreter", CodeSystem = "ANZSCO")]
        Interpreter,
        /// <summary>
        /// The translator
        /// </summary>
        [EnumMember]
        [Name(Code = "272413", Name = "Translator", CodeSystem = "ANZSCO")]
        Translator,
        /// <summary>
        /// The social professionalsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "272499", Name = "Social Professionals nec", CodeSystem = "ANZSCO")]
        SocialProfessionalsnec,
        /// <summary>
        /// The social worker
        /// </summary>
        [EnumMember]
        [Name(Code = "272511", Name = "Social Worker", CodeSystem = "ANZSCO")]
        SocialWorker,
        /// <summary>
        /// The welfare recreationand community arts workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "272600", Name = "Welfare, Recreation and Community Arts Workers nfd", CodeSystem = "ANZSCO")]
        WelfareRecreationandCommunityArtsWorkersnfd,
        /// <summary>
        /// The community arts worker
        /// </summary>
        [EnumMember]
        [Name(Code = "272611", Name = "Community Arts Worker", CodeSystem = "ANZSCO")]
        CommunityArtsWorker,
        /// <summary>
        /// The recreation officer
        /// </summary>
        [EnumMember]
        [Name(Code = "272612", Name = "Recreation Officer", CodeSystem = "ANZSCO")]
        RecreationOfficer,
        /// <summary>
        /// The welfare worker
        /// </summary>
        [EnumMember]
        [Name(Code = "272613", Name = "Welfare Worker", CodeSystem = "ANZSCO")]
        WelfareWorker,
        /// <summary>
        /// The techniciansand trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "300000", Name = "Technicians and Trades Workers nfd", CodeSystem = "ANZSCO")]
        TechniciansandTradesWorkersnfd,
        /// <summary>
        /// The engineering IC tand science techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "310000", Name = "Engineering, ICT and Science Technicians nfd", CodeSystem = "ANZSCO")]
        EngineeringICTandScienceTechniciansnfd,
        /// <summary>
        /// The agricultural medicaland science techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "311000", Name = "Agricultural, Medical and Science Technicians nfd", CodeSystem = "ANZSCO")]
        AgriculturalMedicalandScienceTechniciansnfd,
        /// <summary>
        /// The agricultural technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311111", Name = "Agricultural Technician", CodeSystem = "ANZSCO")]
        AgriculturalTechnician,
        /// <summary>
        /// The medical techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "311200", Name = "Medical Technicians nfd", CodeSystem = "ANZSCO")]
        MedicalTechniciansnfd,
        /// <summary>
        /// The anaesthetic technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311211", Name = "Anaesthetic Technician", CodeSystem = "ANZSCO")]
        AnaestheticTechnician,
        /// <summary>
        /// The cardiac technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311212", Name = "Cardiac Technician", CodeSystem = "ANZSCO")]
        CardiacTechnician,
        /// <summary>
        /// The medical laboratory technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311213", Name = "Medical Laboratory Technician", CodeSystem = "ANZSCO")]
        MedicalLaboratoryTechnician,
        /// <summary>
        /// The operating theatre technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311214", Name = "Operating Theatre Technician", CodeSystem = "ANZSCO")]
        OperatingTheatreTechnician,
        /// <summary>
        /// The pharmacy technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311215", Name = "Pharmacy Technician", CodeSystem = "ANZSCO")]
        PharmacyTechnician,
        /// <summary>
        /// The pathology collector
        /// </summary>
        [EnumMember]
        [Name(Code = "311216", Name = "Pathology Collector", CodeSystem = "ANZSCO")]
        PathologyCollector,
        /// <summary>
        /// The medical techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "311299", Name = "Medical Technicians nec", CodeSystem = "ANZSCO")]
        MedicalTechniciansnec,
        /// <summary>
        /// The primary products inspectorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "311300", Name = "Primary Products Inspectors nfd", CodeSystem = "ANZSCO")]
        PrimaryProductsInspectorsnfd,
        /// <summary>
        /// The fisheries officer
        /// </summary>
        [EnumMember]
        [Name(Code = "311311", Name = "Fisheries Officer", CodeSystem = "ANZSCO")]
        FisheriesOfficer,
        /// <summary>
        /// The meat inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "311312", Name = "Meat Inspector", CodeSystem = "ANZSCO")]
        MeatInspector,
        /// <summary>
        /// The quarantine officer
        /// </summary>
        [EnumMember]
        [Name(Code = "311313", Name = "Quarantine Officer", CodeSystem = "ANZSCO")]
        QuarantineOfficer,
        /// <summary>
        /// The primary products inspectorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "311399", Name = "Primary Products Inspectors nec", CodeSystem = "ANZSCO")]
        PrimaryProductsInspectorsnec,
        /// <summary>
        /// The science techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "311400", Name = "Science Technicians nfd", CodeSystem = "ANZSCO")]
        ScienceTechniciansnfd,
        /// <summary>
        /// The chemistry technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311411", Name = "Chemistry Technician", CodeSystem = "ANZSCO")]
        ChemistryTechnician,
        /// <summary>
        /// The earth science technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311412", Name = "Earth Science Technician", CodeSystem = "ANZSCO")]
        EarthScienceTechnician,
        /// <summary>
        /// The life science technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311413", Name = "Life Science Technician", CodeSystem = "ANZSCO")]
        LifeScienceTechnician,
        /// <summary>
        /// The school laboratory technician
        /// </summary>
        [EnumMember]
        [Name(Code = "311414", Name = "School Laboratory Technician", CodeSystem = "ANZSCO")]
        SchoolLaboratoryTechnician,
        /// <summary>
        /// The science techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "311499", Name = "Science Technicians nec", CodeSystem = "ANZSCO")]
        ScienceTechniciansnec,
        /// <summary>
        /// The buildingand engineering techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312000", Name = "Building and Engineering Technicians nfd", CodeSystem = "ANZSCO")]
        BuildingandEngineeringTechniciansnfd,
        /// <summary>
        /// The architectural buildingand surveying techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312100", Name = "Architectural, Building and Surveying Technicians nfd", CodeSystem = "ANZSCO")]
        ArchitecturalBuildingandSurveyingTechniciansnfd,
        /// <summary>
        /// The architectural draftsperson
        /// </summary>
        [EnumMember]
        [Name(Code = "312111", Name = "Architectural Draftsperson", CodeSystem = "ANZSCO")]
        ArchitecturalDraftsperson,
        /// <summary>
        /// The building associate
        /// </summary>
        [EnumMember]
        [Name(Code = "312112", Name = "Building Associate", CodeSystem = "ANZSCO")]
        BuildingAssociate,
        /// <summary>
        /// The building inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "312113", Name = "Building Inspector", CodeSystem = "ANZSCO")]
        BuildingInspector,
        /// <summary>
        /// The construction estimator
        /// </summary>
        [EnumMember]
        [Name(Code = "312114", Name = "Construction Estimator", CodeSystem = "ANZSCO")]
        ConstructionEstimator,
        /// <summary>
        /// The plumbing inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "312115", Name = "Plumbing Inspector", CodeSystem = "ANZSCO")]
        PlumbingInspector,
        /// <summary>
        /// The surveyingor spatial science technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312116", Name = "Surveying or Spatial Science Technician", CodeSystem = "ANZSCO")]
        SurveyingorSpatialScienceTechnician,
        /// <summary>
        /// The architectural buildingand surveying techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "312199", Name = "Architectural, Building and Surveying Technicians nec", CodeSystem = "ANZSCO")]
        ArchitecturalBuildingandSurveyingTechniciansnec,
        /// <summary>
        /// The civil engineering draftspersonsand techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312200", Name = "Civil Engineering Draftspersons and Technicians nfd", CodeSystem = "ANZSCO")]
        CivilEngineeringDraftspersonsandTechniciansnfd,
        /// <summary>
        /// The civil engineering draftsperson
        /// </summary>
        [EnumMember]
        [Name(Code = "312211", Name = "Civil Engineering Draftsperson", CodeSystem = "ANZSCO")]
        CivilEngineeringDraftsperson,
        /// <summary>
        /// The civil engineering technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312212", Name = "Civil Engineering Technician", CodeSystem = "ANZSCO")]
        CivilEngineeringTechnician,
        /// <summary>
        /// The electrical engineering draftspersonsand techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312300", Name = "Electrical Engineering Draftspersons and Technicians nfd", CodeSystem = "ANZSCO")]
        ElectricalEngineeringDraftspersonsandTechniciansnfd,
        /// <summary>
        /// The electrical engineering draftsperson
        /// </summary>
        [EnumMember]
        [Name(Code = "312311", Name = "Electrical Engineering Draftsperson", CodeSystem = "ANZSCO")]
        ElectricalEngineeringDraftsperson,
        /// <summary>
        /// The electrical engineering technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312312", Name = "Electrical Engineering Technician", CodeSystem = "ANZSCO")]
        ElectricalEngineeringTechnician,
        /// <summary>
        /// The electronic engineering draftspersonsand techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312400", Name = "Electronic Engineering Draftspersons and Technicians nfd", CodeSystem = "ANZSCO")]
        ElectronicEngineeringDraftspersonsandTechniciansnfd,
        /// <summary>
        /// The electronic engineering draftsperson
        /// </summary>
        [EnumMember]
        [Name(Code = "312411", Name = "Electronic Engineering Draftsperson", CodeSystem = "ANZSCO")]
        ElectronicEngineeringDraftsperson,
        /// <summary>
        /// The electronic engineering technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312412", Name = "Electronic Engineering Technician", CodeSystem = "ANZSCO")]
        ElectronicEngineeringTechnician,
        /// <summary>
        /// The mechanical engineering draftspersonsand techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312500", Name = "Mechanical Engineering Draftspersons and Technicians nfd", CodeSystem = "ANZSCO")]
        MechanicalEngineeringDraftspersonsandTechniciansnfd,
        /// <summary>
        /// The mechanical engineering draftsperson
        /// </summary>
        [EnumMember]
        [Name(Code = "312511", Name = "Mechanical Engineering Draftsperson", CodeSystem = "ANZSCO")]
        MechanicalEngineeringDraftsperson,
        /// <summary>
        /// The mechanical engineering technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312512", Name = "Mechanical Engineering Technician", CodeSystem = "ANZSCO")]
        MechanicalEngineeringTechnician,
        /// <summary>
        /// The safety inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "312611", Name = "Safety Inspector", CodeSystem = "ANZSCO")]
        SafetyInspector,
        /// <summary>
        /// The other buildingand engineering techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "312900", Name = "Other Building and Engineering Technicians nfd", CodeSystem = "ANZSCO")]
        OtherBuildingandEngineeringTechniciansnfd,
        /// <summary>
        /// The maintenance planner
        /// </summary>
        [EnumMember]
        [Name(Code = "312911", Name = "Maintenance Planner", CodeSystem = "ANZSCO")]
        MaintenancePlanner,
        /// <summary>
        /// The metallurgicalor materials technician
        /// </summary>
        [EnumMember]
        [Name(Code = "312912", Name = "Metallurgical or Materials Technician", CodeSystem = "ANZSCO")]
        MetallurgicalorMaterialsTechnician,
        /// <summary>
        /// The mine deputy
        /// </summary>
        [EnumMember]
        [Name(Code = "312913", Name = "Mine Deputy", CodeSystem = "ANZSCO")]
        MineDeputy,
        /// <summary>
        /// The buildingand engineering techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "312999", Name = "Building and Engineering Technicians nec", CodeSystem = "ANZSCO")]
        BuildingandEngineeringTechniciansnec,
        /// <summary>
        /// The IC tand telecommunications techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "313000", Name = "ICT and Telecommunications Technicians nfd", CodeSystem = "ANZSCO")]
        ICTandTelecommunicationsTechniciansnfd,
        /// <summary>
        /// The ICT support techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "313100", Name = "ICT Support Technicians nfd", CodeSystem = "ANZSCO")]
        ICTSupportTechniciansnfd,
        /// <summary>
        /// The hardware technician
        /// </summary>
        [EnumMember]
        [Name(Code = "313111", Name = "Hardware Technician", CodeSystem = "ANZSCO")]
        HardwareTechnician,
        /// <summary>
        /// The ICT customer support officer
        /// </summary>
        [EnumMember]
        [Name(Code = "313112", Name = "ICT Customer Support Officer", CodeSystem = "ANZSCO")]
        ICTCustomerSupportOfficer,
        /// <summary>
        /// The web administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "313113", Name = "Web Administrator", CodeSystem = "ANZSCO")]
        WebAdministrator,
        /// <summary>
        /// The ICT support techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "313199", Name = "ICT Support Technicians nec", CodeSystem = "ANZSCO")]
        ICTSupportTechniciansnec,
        /// <summary>
        /// The telecommunications technical specialistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "313200", Name = "Telecommunications Technical Specialists nfd", CodeSystem = "ANZSCO")]
        TelecommunicationsTechnicalSpecialistsnfd,
        /// <summary>
        /// The radiocommunications technician
        /// </summary>
        [EnumMember]
        [Name(Code = "313211", Name = "Radiocommunications Technician", CodeSystem = "ANZSCO")]
        RadiocommunicationsTechnician,
        /// <summary>
        /// The telecommunications field engineer
        /// </summary>
        [EnumMember]
        [Name(Code = "313212", Name = "Telecommunications Field Engineer", CodeSystem = "ANZSCO")]
        TelecommunicationsFieldEngineer,
        /// <summary>
        /// The telecommunications network planner
        /// </summary>
        [EnumMember]
        [Name(Code = "313213", Name = "Telecommunications Network Planner", CodeSystem = "ANZSCO")]
        TelecommunicationsNetworkPlanner,
        /// <summary>
        /// The telecommunications technical officeror technologist
        /// </summary>
        [EnumMember]
        [Name(Code = "313214", Name = "Telecommunications Technical Officer or Technologist", CodeSystem = "ANZSCO")]
        TelecommunicationsTechnicalOfficerorTechnologist,
        /// <summary>
        /// The automotiveand engineering trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "320000", Name = "Automotive and Engineering Trades Workers nfd", CodeSystem = "ANZSCO")]
        AutomotiveandEngineeringTradesWorkersnfd,
        /// <summary>
        /// The automotive electriciansand mechanicsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "321000", Name = "Automotive Electricians and Mechanics nfd", CodeSystem = "ANZSCO")]
        AutomotiveElectriciansandMechanicsnfd,
        /// <summary>
        /// The automotive electrician
        /// </summary>
        [EnumMember]
        [Name(Code = "321111", Name = "Automotive Electrician", CodeSystem = "ANZSCO")]
        AutomotiveElectrician,
        /// <summary>
        /// The motor mechanicsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "321200", Name = "Motor Mechanics nfd", CodeSystem = "ANZSCO")]
        MotorMechanicsnfd,
        /// <summary>
        /// The motor mechanic general
        /// </summary>
        [EnumMember]
        [Name(Code = "321211", Name = "Motor Mechanic (General)", CodeSystem = "ANZSCO")]
        MotorMechanicGeneral,
        /// <summary>
        /// The diesel motor mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "321212", Name = "Diesel Motor Mechanic", CodeSystem = "ANZSCO")]
        DieselMotorMechanic,
        /// <summary>
        /// The motorcycle mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "321213", Name = "Motorcycle Mechanic", CodeSystem = "ANZSCO")]
        MotorcycleMechanic,
        /// <summary>
        /// The small engine mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "321214", Name = "Small Engine Mechanic", CodeSystem = "ANZSCO")]
        SmallEngineMechanic,
        /// <summary>
        /// The fabrication engineering trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "322000", Name = "Fabrication Engineering Trades Workers nfd", CodeSystem = "ANZSCO")]
        FabricationEngineeringTradesWorkersnfd,
        /// <summary>
        /// The metal casting forgingand finishing trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "322100", Name = "Metal Casting, Forging and Finishing Trades Workers nfd", CodeSystem = "ANZSCO")]
        MetalCastingForgingandFinishingTradesWorkersnfd,
        /// <summary>
        /// The blacksmith
        /// </summary>
        [EnumMember]
        [Name(Code = "322111", Name = "Blacksmith", CodeSystem = "ANZSCO")]
        Blacksmith,
        /// <summary>
        /// The electroplater
        /// </summary>
        [EnumMember]
        [Name(Code = "322112", Name = "Electroplater", CodeSystem = "ANZSCO")]
        Electroplater,
        /// <summary>
        /// The farrier
        /// </summary>
        [EnumMember]
        [Name(Code = "322113", Name = "Farrier", CodeSystem = "ANZSCO")]
        Farrier,
        /// <summary>
        /// The metal casting trades worker
        /// </summary>
        [EnumMember]
        [Name(Code = "322114", Name = "Metal Casting Trades Worker", CodeSystem = "ANZSCO")]
        MetalCastingTradesWorker,
        /// <summary>
        /// The metal polisher
        /// </summary>
        [EnumMember]
        [Name(Code = "322115", Name = "Metal Polisher", CodeSystem = "ANZSCO")]
        MetalPolisher,
        /// <summary>
        /// The sheetmetal trades worker
        /// </summary>
        [EnumMember]
        [Name(Code = "322211", Name = "Sheetmetal Trades Worker", CodeSystem = "ANZSCO")]
        SheetmetalTradesWorker,
        /// <summary>
        /// The structural steeland welding trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "322300", Name = "Structural Steel and Welding Trades Workers nfd", CodeSystem = "ANZSCO")]
        StructuralSteelandWeldingTradesWorkersnfd,
        /// <summary>
        /// The metal fabricator
        /// </summary>
        [EnumMember]
        [Name(Code = "322311", Name = "Metal Fabricator", CodeSystem = "ANZSCO")]
        MetalFabricator,
        /// <summary>
        /// The pressure welder
        /// </summary>
        [EnumMember]
        [Name(Code = "322312", Name = "Pressure Welder", CodeSystem = "ANZSCO")]
        PressureWelder,
        /// <summary>
        /// The welder first class
        /// </summary>
        [EnumMember]
        [Name(Code = "322313", Name = "Welder (First Class)", CodeSystem = "ANZSCO")]
        WelderFirstClass,
        /// <summary>
        /// The mechanical engineering trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "323000", Name = "Mechanical Engineering Trades Workers nfd", CodeSystem = "ANZSCO")]
        MechanicalEngineeringTradesWorkersnfd,
        /// <summary>
        /// The aircraft maintenance engineersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "323100", Name = "Aircraft Maintenance Engineers nfd", CodeSystem = "ANZSCO")]
        AircraftMaintenanceEngineersnfd,
        /// <summary>
        /// The aircraft maintenance engineer avionics
        /// </summary>
        [EnumMember]
        [Name(Code = "323111", Name = "Aircraft Maintenance Engineer (Avionics)", CodeSystem = "ANZSCO")]
        AircraftMaintenanceEngineerAvionics,
        /// <summary>
        /// The aircraft maintenance engineer mechanical
        /// </summary>
        [EnumMember]
        [Name(Code = "323112", Name = "Aircraft Maintenance Engineer (Mechanical)", CodeSystem = "ANZSCO")]
        AircraftMaintenanceEngineerMechanical,
        /// <summary>
        /// The aircraft maintenance engineer structures
        /// </summary>
        [EnumMember]
        [Name(Code = "323113", Name = "Aircraft Maintenance Engineer (Structures)", CodeSystem = "ANZSCO")]
        AircraftMaintenanceEngineerStructures,
        /// <summary>
        /// The metal fittersand machinistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "323200", Name = "Metal Fitters and Machinists nfd", CodeSystem = "ANZSCO")]
        MetalFittersandMachinistsnfd,
        /// <summary>
        /// The fitter general
        /// </summary>
        [EnumMember]
        [Name(Code = "323211", Name = "Fitter (General)", CodeSystem = "ANZSCO")]
        FitterGeneral,
        /// <summary>
        /// The fitterand turner
        /// </summary>
        [EnumMember]
        [Name(Code = "323212", Name = "Fitter and Turner", CodeSystem = "ANZSCO")]
        FitterandTurner,
        /// <summary>
        /// The fitter welder
        /// </summary>
        [EnumMember]
        [Name(Code = "323213", Name = "Fitter-Welder", CodeSystem = "ANZSCO")]
        FitterWelder,
        /// <summary>
        /// The metal machinist first class
        /// </summary>
        [EnumMember]
        [Name(Code = "323214", Name = "Metal Machinist (First Class)", CodeSystem = "ANZSCO")]
        MetalMachinistFirstClass,
        /// <summary>
        /// The textile clothingand footwear mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "323215", Name = "Textile, Clothing and Footwear Mechanic", CodeSystem = "ANZSCO")]
        TextileClothingandFootwearMechanic,
        /// <summary>
        /// The metal fittersand machinistsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "323299", Name = "Metal Fitters and Machinists nec", CodeSystem = "ANZSCO")]
        MetalFittersandMachinistsnec,
        /// <summary>
        /// The precision metal trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "323300", Name = "Precision Metal Trades Workers nfd", CodeSystem = "ANZSCO")]
        PrecisionMetalTradesWorkersnfd,
        /// <summary>
        /// The engraver
        /// </summary>
        [EnumMember]
        [Name(Code = "323311", Name = "Engraver", CodeSystem = "ANZSCO")]
        Engraver,
        /// <summary>
        /// The gunsmith
        /// </summary>
        [EnumMember]
        [Name(Code = "323312", Name = "Gunsmith", CodeSystem = "ANZSCO")]
        Gunsmith,
        /// <summary>
        /// The locksmith
        /// </summary>
        [EnumMember]
        [Name(Code = "323313", Name = "Locksmith", CodeSystem = "ANZSCO")]
        Locksmith,
        /// <summary>
        /// The precision instrument makerand repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "323314", Name = "Precision Instrument Maker and Repairer", CodeSystem = "ANZSCO")]
        PrecisionInstrumentMakerandRepairer,
        /// <summary>
        /// The saw makerand repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "323315", Name = "Saw Maker and Repairer", CodeSystem = "ANZSCO")]
        SawMakerandRepairer,
        /// <summary>
        /// The watchand clock makerand repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "323316", Name = "Watch and Clock Maker and Repairer", CodeSystem = "ANZSCO")]
        WatchandClockMakerandRepairer,
        /// <summary>
        /// The toolmakersand engineering patternmakersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "323400", Name = "Toolmakers and Engineering Patternmakers nfd", CodeSystem = "ANZSCO")]
        ToolmakersandEngineeringPatternmakersnfd,
        /// <summary>
        /// The engineering patternmaker
        /// </summary>
        [EnumMember]
        [Name(Code = "323411", Name = "Engineering Patternmaker", CodeSystem = "ANZSCO")]
        EngineeringPatternmaker,
        /// <summary>
        /// The toolmaker
        /// </summary>
        [EnumMember]
        [Name(Code = "323412", Name = "Toolmaker", CodeSystem = "ANZSCO")]
        Toolmaker,
        /// <summary>
        /// The panelbeatersand vehicle body builders trimmersand paintersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "324000", Name = "Panelbeaters, and Vehicle Body Builders, Trimmers and Painters nfd", CodeSystem = "ANZSCO")]
        PanelbeatersandVehicleBodyBuildersTrimmersandPaintersnfd,
        /// <summary>
        /// The panelbeater
        /// </summary>
        [EnumMember]
        [Name(Code = "324111", Name = "Panelbeater", CodeSystem = "ANZSCO")]
        Panelbeater,
        /// <summary>
        /// The vehicle body buildersand trimmersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "324200", Name = "Vehicle Body Builders and Trimmers nfd", CodeSystem = "ANZSCO")]
        VehicleBodyBuildersandTrimmersnfd,
        /// <summary>
        /// The vehicle body builder
        /// </summary>
        [EnumMember]
        [Name(Code = "324211", Name = "Vehicle Body Builder", CodeSystem = "ANZSCO")]
        VehicleBodyBuilder,
        /// <summary>
        /// The vehicle trimmer
        /// </summary>
        [EnumMember]
        [Name(Code = "324212", Name = "Vehicle Trimmer", CodeSystem = "ANZSCO")]
        VehicleTrimmer,
        /// <summary>
        /// The vehicle painter
        /// </summary>
        [EnumMember]
        [Name(Code = "324311", Name = "Vehicle Painter", CodeSystem = "ANZSCO")]
        VehiclePainter,
        /// <summary>
        /// The construction trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "330000", Name = "Construction Trades Workers nfd", CodeSystem = "ANZSCO")]
        ConstructionTradesWorkersnfd,
        /// <summary>
        /// The bricklayersand carpentersand joinersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "331000", Name = "Bricklayers, and Carpenters and Joiners nfd", CodeSystem = "ANZSCO")]
        BricklayersandCarpentersandJoinersnfd,
        /// <summary>
        /// The bricklayersand stonemasonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "331100", Name = "Bricklayers and Stonemasons nfd", CodeSystem = "ANZSCO")]
        BricklayersandStonemasonsnfd,
        /// <summary>
        /// The bricklayer
        /// </summary>
        [EnumMember]
        [Name(Code = "331111", Name = "Bricklayer", CodeSystem = "ANZSCO")]
        Bricklayer,
        /// <summary>
        /// The stonemason
        /// </summary>
        [EnumMember]
        [Name(Code = "331112", Name = "Stonemason", CodeSystem = "ANZSCO")]
        Stonemason,
        /// <summary>
        /// The carpenterand joiner
        /// </summary>
        [EnumMember]
        [Name(Code = "331211", Name = "Carpenter and Joiner", CodeSystem = "ANZSCO")]
        CarpenterandJoiner,
        /// <summary>
        /// The carpenter
        /// </summary>
        [EnumMember]
        [Name(Code = "331212", Name = "Carpenter", CodeSystem = "ANZSCO")]
        Carpenter,
        /// <summary>
        /// The joiner
        /// </summary>
        [EnumMember]
        [Name(Code = "331213", Name = "Joiner", CodeSystem = "ANZSCO")]
        Joiner,
        /// <summary>
        /// The floor finishersand painting trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "332000", Name = "Floor Finishers and Painting Trades Workers nfd", CodeSystem = "ANZSCO")]
        FloorFinishersandPaintingTradesWorkersnfd,
        /// <summary>
        /// The floor finisher
        /// </summary>
        [EnumMember]
        [Name(Code = "332111", Name = "Floor Finisher", CodeSystem = "ANZSCO")]
        FloorFinisher,
        /// <summary>
        /// The painting trades worker
        /// </summary>
        [EnumMember]
        [Name(Code = "332211", Name = "Painting Trades Worker", CodeSystem = "ANZSCO")]
        PaintingTradesWorker,
        /// <summary>
        /// The glaziers plasterersand tilersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "333000", Name = "Glaziers, Plasterers and Tilers nfd", CodeSystem = "ANZSCO")]
        GlaziersPlasterersandTilersnfd,
        /// <summary>
        /// The glazier
        /// </summary>
        [EnumMember]
        [Name(Code = "333111", Name = "Glazier", CodeSystem = "ANZSCO")]
        Glazier,
        /// <summary>
        /// The plasterersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "333200", Name = "Plasterers nfd", CodeSystem = "ANZSCO")]
        Plasterersnfd,
        /// <summary>
        /// The fibrous plasterer
        /// </summary>
        [EnumMember]
        [Name(Code = "333211", Name = "Fibrous Plasterer", CodeSystem = "ANZSCO")]
        FibrousPlasterer,
        /// <summary>
        /// The solid plasterer
        /// </summary>
        [EnumMember]
        [Name(Code = "333212", Name = "Solid Plasterer", CodeSystem = "ANZSCO")]
        SolidPlasterer,
        /// <summary>
        /// The roof tiler
        /// </summary>
        [EnumMember]
        [Name(Code = "333311", Name = "Roof Tiler", CodeSystem = "ANZSCO")]
        RoofTiler,
        /// <summary>
        /// The walland floor tiler
        /// </summary>
        [EnumMember]
        [Name(Code = "333411", Name = "Wall and Floor Tiler", CodeSystem = "ANZSCO")]
        WallandFloorTiler,
        /// <summary>
        /// The plumbersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "334100", Name = "Plumbers nfd", CodeSystem = "ANZSCO")]
        Plumbersnfd,
        /// <summary>
        /// The plumber general
        /// </summary>
        [EnumMember]
        [Name(Code = "334111", Name = "Plumber (General)", CodeSystem = "ANZSCO")]
        PlumberGeneral,
        /// <summary>
        /// The airconditioningand mechanical services plumber
        /// </summary>
        [EnumMember]
        [Name(Code = "334112", Name = "Airconditioning and Mechanical Services Plumber", CodeSystem = "ANZSCO")]
        AirconditioningandMechanicalServicesPlumber,
        /// <summary>
        /// The drainer
        /// </summary>
        [EnumMember]
        [Name(Code = "334113", Name = "Drainer", CodeSystem = "ANZSCO")]
        Drainer,
        /// <summary>
        /// The gasfitter
        /// </summary>
        [EnumMember]
        [Name(Code = "334114", Name = "Gasfitter", CodeSystem = "ANZSCO")]
        Gasfitter,
        /// <summary>
        /// The roof plumber
        /// </summary>
        [EnumMember]
        [Name(Code = "334115", Name = "Roof Plumber", CodeSystem = "ANZSCO")]
        RoofPlumber,
        /// <summary>
        /// The electrotechnologyand telecommunications trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "340000", Name = "Electrotechnology and Telecommunications Trades Workers nfd", CodeSystem = "ANZSCO")]
        ElectrotechnologyandTelecommunicationsTradesWorkersnfd,
        /// <summary>
        /// The electriciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "341100", Name = "Electricians nfd", CodeSystem = "ANZSCO")]
        Electriciansnfd,
        /// <summary>
        /// The electrician general
        /// </summary>
        [EnumMember]
        [Name(Code = "341111", Name = "Electrician (General)", CodeSystem = "ANZSCO")]
        ElectricianGeneral,
        /// <summary>
        /// The electrician special class
        /// </summary>
        [EnumMember]
        [Name(Code = "341112", Name = "Electrician (Special Class)", CodeSystem = "ANZSCO")]
        ElectricianSpecialClass,
        /// <summary>
        /// The lift mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "341113", Name = "Lift Mechanic", CodeSystem = "ANZSCO")]
        LiftMechanic,
        /// <summary>
        /// The electronicsand telecommunications trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "342000", Name = "Electronics and Telecommunications Trades Workers nfd", CodeSystem = "ANZSCO")]
        ElectronicsandTelecommunicationsTradesWorkersnfd,
        /// <summary>
        /// The airconditioningand refrigeration mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "342111", Name = "Airconditioning and Refrigeration Mechanic", CodeSystem = "ANZSCO")]
        AirconditioningandRefrigerationMechanic,
        /// <summary>
        /// The electrical distribution trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "342200", Name = "Electrical Distribution Trades Workers nfd", CodeSystem = "ANZSCO")]
        ElectricalDistributionTradesWorkersnfd,
        /// <summary>
        /// The electrical linesworker
        /// </summary>
        [EnumMember]
        [Name(Code = "342211", Name = "Electrical Linesworker", CodeSystem = "ANZSCO")]
        ElectricalLinesworker,
        /// <summary>
        /// The technical cable jointer
        /// </summary>
        [EnumMember]
        [Name(Code = "342212", Name = "Technical Cable Jointer", CodeSystem = "ANZSCO")]
        TechnicalCableJointer,
        /// <summary>
        /// The electronics trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "342300", Name = "Electronics Trades Workers nfd", CodeSystem = "ANZSCO")]
        ElectronicsTradesWorkersnfd,
        /// <summary>
        /// The business machine mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "342311", Name = "Business Machine Mechanic", CodeSystem = "ANZSCO")]
        BusinessMachineMechanic,
        /// <summary>
        /// The communications operator
        /// </summary>
        [EnumMember]
        [Name(Code = "342312", Name = "Communications Operator", CodeSystem = "ANZSCO")]
        CommunicationsOperator,
        /// <summary>
        /// The electronic equipment trades worker
        /// </summary>
        [EnumMember]
        [Name(Code = "342313", Name = "Electronic Equipment Trades Worker", CodeSystem = "ANZSCO")]
        ElectronicEquipmentTradesWorker,
        /// <summary>
        /// The electronic instrument trades worker general
        /// </summary>
        [EnumMember]
        [Name(Code = "342314", Name = "Electronic Instrument Trades Worker (General)", CodeSystem = "ANZSCO")]
        ElectronicInstrumentTradesWorkerGeneral,
        /// <summary>
        /// The electronic instrument trades worker special class
        /// </summary>
        [EnumMember]
        [Name(Code = "342315", Name = "Electronic Instrument Trades Worker (Special Class)", CodeSystem = "ANZSCO")]
        ElectronicInstrumentTradesWorkerSpecialClass,
        /// <summary>
        /// The telecommunications trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "342400", Name = "Telecommunications Trades Workers nfd", CodeSystem = "ANZSCO")]
        TelecommunicationsTradesWorkersnfd,
        /// <summary>
        /// The cabler dataand telecommunications
        /// </summary>
        [EnumMember]
        [Name(Code = "342411", Name = "Cabler (Data and Telecommunications)", CodeSystem = "ANZSCO")]
        CablerDataandTelecommunications,
        /// <summary>
        /// The telecommunications cable jointer
        /// </summary>
        [EnumMember]
        [Name(Code = "342412", Name = "Telecommunications Cable Jointer", CodeSystem = "ANZSCO")]
        TelecommunicationsCableJointer,
        /// <summary>
        /// The telecommunications linesworker
        /// </summary>
        [EnumMember]
        [Name(Code = "342413", Name = "Telecommunications Linesworker", CodeSystem = "ANZSCO")]
        TelecommunicationsLinesworker,
        /// <summary>
        /// The telecommunications technician
        /// </summary>
        [EnumMember]
        [Name(Code = "342414", Name = "Telecommunications Technician", CodeSystem = "ANZSCO")]
        TelecommunicationsTechnician,
        /// <summary>
        /// The food trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "351000", Name = "Food Trades Workers nfd", CodeSystem = "ANZSCO")]
        FoodTradesWorkersnfd,
        /// <summary>
        /// The bakersand pastrycooksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "351100", Name = "Bakers and Pastrycooks nfd", CodeSystem = "ANZSCO")]
        BakersandPastrycooksnfd,
        /// <summary>
        /// The baker
        /// </summary>
        [EnumMember]
        [Name(Code = "351111", Name = "Baker", CodeSystem = "ANZSCO")]
        Baker,
        /// <summary>
        /// The pastrycook
        /// </summary>
        [EnumMember]
        [Name(Code = "351112", Name = "Pastrycook", CodeSystem = "ANZSCO")]
        Pastrycook,
        /// <summary>
        /// The butcheror smallgoods maker
        /// </summary>
        [EnumMember]
        [Name(Code = "351211", Name = "Butcher or Smallgoods Maker", CodeSystem = "ANZSCO")]
        ButcherorSmallgoodsMaker,
        /// <summary>
        /// The chef
        /// </summary>
        [EnumMember]
        [Name(Code = "351311", Name = "Chef", CodeSystem = "ANZSCO")]
        Chef,
        /// <summary>
        /// The cook
        /// </summary>
        [EnumMember]
        [Name(Code = "351411", Name = "Cook", CodeSystem = "ANZSCO")]
        Cook,
        /// <summary>
        /// The skilled animaland horticultural workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "360000", Name = "Skilled Animal and Horticultural Workers nfd", CodeSystem = "ANZSCO")]
        SkilledAnimalandHorticulturalWorkersnfd,
        /// <summary>
        /// The animal attendantsand trainersand shearersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "361000", Name = "Animal Attendants and Trainers, and Shearers nfd", CodeSystem = "ANZSCO")]
        AnimalAttendantsandTrainersandShearersnfd,
        /// <summary>
        /// The animal attendantsand trainersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "361100", Name = "Animal Attendants and Trainers nfd", CodeSystem = "ANZSCO")]
        AnimalAttendantsandTrainersnfd,
        /// <summary>
        /// The dog handleror trainer
        /// </summary>
        [EnumMember]
        [Name(Code = "361111", Name = "Dog Handler or Trainer", CodeSystem = "ANZSCO")]
        DogHandlerorTrainer,
        /// <summary>
        /// The horse trainer
        /// </summary>
        [EnumMember]
        [Name(Code = "361112", Name = "Horse Trainer", CodeSystem = "ANZSCO")]
        HorseTrainer,
        /// <summary>
        /// The pet groomer
        /// </summary>
        [EnumMember]
        [Name(Code = "361113", Name = "Pet Groomer", CodeSystem = "ANZSCO")]
        PetGroomer,
        /// <summary>
        /// The zookeeper
        /// </summary>
        [EnumMember]
        [Name(Code = "361114", Name = "Zookeeper", CodeSystem = "ANZSCO")]
        Zookeeper,
        /// <summary>
        /// The animal attendantsand trainersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "361199", Name = "Animal Attendants and Trainers nec", CodeSystem = "ANZSCO")]
        AnimalAttendantsandTrainersnec,
        /// <summary>
        /// The shearer
        /// </summary>
        [EnumMember]
        [Name(Code = "361211", Name = "Shearer", CodeSystem = "ANZSCO")]
        Shearer,
        /// <summary>
        /// The veterinary nurse
        /// </summary>
        [EnumMember]
        [Name(Code = "361311", Name = "Veterinary Nurse", CodeSystem = "ANZSCO")]
        VeterinaryNurse,
        /// <summary>
        /// The horticultural trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "362000", Name = "Horticultural Trades Workers nfd", CodeSystem = "ANZSCO")]
        HorticulturalTradesWorkersnfd,
        /// <summary>
        /// The florist
        /// </summary>
        [EnumMember]
        [Name(Code = "362111", Name = "Florist", CodeSystem = "ANZSCO")]
        Florist,
        /// <summary>
        /// The gardenersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "362200", Name = "Gardeners nfd", CodeSystem = "ANZSCO")]
        Gardenersnfd,
        /// <summary>
        /// The gardener general
        /// </summary>
        [EnumMember]
        [Name(Code = "362211", Name = "Gardener (General)", CodeSystem = "ANZSCO")]
        GardenerGeneral,
        /// <summary>
        /// The arborist
        /// </summary>
        [EnumMember]
        [Name(Code = "362212", Name = "Arborist", CodeSystem = "ANZSCO")]
        Arborist,
        /// <summary>
        /// The landscape gardener
        /// </summary>
        [EnumMember]
        [Name(Code = "362213", Name = "Landscape Gardener", CodeSystem = "ANZSCO")]
        LandscapeGardener,
        /// <summary>
        /// The greenkeeper
        /// </summary>
        [EnumMember]
        [Name(Code = "362311", Name = "Greenkeeper", CodeSystem = "ANZSCO")]
        Greenkeeper,
        /// <summary>
        /// The nurseryperson
        /// </summary>
        [EnumMember]
        [Name(Code = "362411", Name = "Nurseryperson", CodeSystem = "ANZSCO")]
        Nurseryperson,
        /// <summary>
        /// The other techniciansand trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "390000", Name = "Other Technicians and Trades Workers nfd", CodeSystem = "ANZSCO")]
        OtherTechniciansandTradesWorkersnfd,
        /// <summary>
        /// The hairdresser
        /// </summary>
        [EnumMember]
        [Name(Code = "391111", Name = "Hairdresser", CodeSystem = "ANZSCO")]
        Hairdresser,
        /// <summary>
        /// The printing trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "392000", Name = "Printing Trades Workers nfd", CodeSystem = "ANZSCO")]
        PrintingTradesWorkersnfd,
        /// <summary>
        /// The print finishersand screen printersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "392100", Name = "Print Finishers and Screen Printers nfd", CodeSystem = "ANZSCO")]
        PrintFinishersandScreenPrintersnfd,
        /// <summary>
        /// The print finisher
        /// </summary>
        [EnumMember]
        [Name(Code = "392111", Name = "Print Finisher", CodeSystem = "ANZSCO")]
        PrintFinisher,
        /// <summary>
        /// The screen printer
        /// </summary>
        [EnumMember]
        [Name(Code = "392112", Name = "Screen Printer", CodeSystem = "ANZSCO")]
        ScreenPrinter,
        /// <summary>
        /// The graphic prepress trades worker
        /// </summary>
        [EnumMember]
        [Name(Code = "392211", Name = "Graphic Pre-press Trades Worker", CodeSystem = "ANZSCO")]
        GraphicPrepressTradesWorker,
        /// <summary>
        /// The printersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "392300", Name = "Printers nfd", CodeSystem = "ANZSCO")]
        Printersnfd,
        /// <summary>
        /// The printing machinist
        /// </summary>
        [EnumMember]
        [Name(Code = "392311", Name = "Printing Machinist", CodeSystem = "ANZSCO")]
        PrintingMachinist,
        /// <summary>
        /// The small offset printer
        /// </summary>
        [EnumMember]
        [Name(Code = "392312", Name = "Small Offset Printer", CodeSystem = "ANZSCO")]
        SmallOffsetPrinter,
        /// <summary>
        /// The textile clothingand footwear trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "393000", Name = "Textile, Clothing and Footwear Trades Workers nfd", CodeSystem = "ANZSCO")]
        TextileClothingandFootwearTradesWorkersnfd,
        /// <summary>
        /// The canvasand leather goods makersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "393100", Name = "Canvas and Leather Goods Makers nfd", CodeSystem = "ANZSCO")]
        CanvasandLeatherGoodsMakersnfd,
        /// <summary>
        /// The canvas goods fabricator
        /// </summary>
        [EnumMember]
        [Name(Code = "393111", Name = "Canvas Goods Fabricator", CodeSystem = "ANZSCO")]
        CanvasGoodsFabricator,
        /// <summary>
        /// The leather goods maker
        /// </summary>
        [EnumMember]
        [Name(Code = "393112", Name = "Leather Goods Maker", CodeSystem = "ANZSCO")]
        LeatherGoodsMaker,
        /// <summary>
        /// The sail maker
        /// </summary>
        [EnumMember]
        [Name(Code = "393113", Name = "Sail Maker", CodeSystem = "ANZSCO")]
        SailMaker,
        /// <summary>
        /// The shoemaker
        /// </summary>
        [EnumMember]
        [Name(Code = "393114", Name = "Shoemaker", CodeSystem = "ANZSCO")]
        Shoemaker,
        /// <summary>
        /// The clothing trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "393200", Name = "Clothing Trades Workers nfd", CodeSystem = "ANZSCO")]
        ClothingTradesWorkersnfd,
        /// <summary>
        /// The apparel cutter
        /// </summary>
        [EnumMember]
        [Name(Code = "393211", Name = "Apparel Cutter", CodeSystem = "ANZSCO")]
        ApparelCutter,
        /// <summary>
        /// The clothing patternmaker
        /// </summary>
        [EnumMember]
        [Name(Code = "393212", Name = "Clothing Patternmaker", CodeSystem = "ANZSCO")]
        ClothingPatternmaker,
        /// <summary>
        /// The dressmakeror tailor
        /// </summary>
        [EnumMember]
        [Name(Code = "393213", Name = "Dressmaker or Tailor", CodeSystem = "ANZSCO")]
        DressmakerorTailor,
        /// <summary>
        /// The clothing trades workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "393299", Name = "Clothing Trades Workers nec", CodeSystem = "ANZSCO")]
        ClothingTradesWorkersnec,
        /// <summary>
        /// The upholsterer
        /// </summary>
        [EnumMember]
        [Name(Code = "393311", Name = "Upholsterer", CodeSystem = "ANZSCO")]
        Upholsterer,
        /// <summary>
        /// The wood trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "394000", Name = "Wood Trades Workers nfd", CodeSystem = "ANZSCO")]
        WoodTradesWorkersnfd,
        /// <summary>
        /// The cabinetmaker
        /// </summary>
        [EnumMember]
        [Name(Code = "394111", Name = "Cabinetmaker", CodeSystem = "ANZSCO")]
        Cabinetmaker,
        /// <summary>
        /// The wood machinistsand other wood trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "394200", Name = "Wood Machinists and Other Wood Trades Workers nfd", CodeSystem = "ANZSCO")]
        WoodMachinistsandOtherWoodTradesWorkersnfd,
        /// <summary>
        /// The furniture finisher
        /// </summary>
        [EnumMember]
        [Name(Code = "394211", Name = "Furniture Finisher", CodeSystem = "ANZSCO")]
        FurnitureFinisher,
        /// <summary>
        /// The picture framer
        /// </summary>
        [EnumMember]
        [Name(Code = "394212", Name = "Picture Framer", CodeSystem = "ANZSCO")]
        PictureFramer,
        /// <summary>
        /// The wood machinist
        /// </summary>
        [EnumMember]
        [Name(Code = "394213", Name = "Wood Machinist", CodeSystem = "ANZSCO")]
        WoodMachinist,
        /// <summary>
        /// The wood turner
        /// </summary>
        [EnumMember]
        [Name(Code = "394214", Name = "Wood Turner", CodeSystem = "ANZSCO")]
        WoodTurner,
        /// <summary>
        /// The wood machinistsand other wood trades workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "394299", Name = "Wood Machinists and Other Wood Trades Workers nec", CodeSystem = "ANZSCO")]
        WoodMachinistsandOtherWoodTradesWorkersnec,
        /// <summary>
        /// The miscellaneous techniciansand trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399000", Name = "Miscellaneous Technicians and Trades Workers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousTechniciansandTradesWorkersnfd,
        /// <summary>
        /// The boat buildersand shipwrightsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399100", Name = "Boat Builders and Shipwrights nfd", CodeSystem = "ANZSCO")]
        BoatBuildersandShipwrightsnfd,
        /// <summary>
        /// The boat builderand repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "399111", Name = "Boat Builder and Repairer", CodeSystem = "ANZSCO")]
        BoatBuilderandRepairer,
        /// <summary>
        /// The shipwright
        /// </summary>
        [EnumMember]
        [Name(Code = "399112", Name = "Shipwright", CodeSystem = "ANZSCO")]
        Shipwright,
        /// <summary>
        /// The chemical gas petroleumand power generation plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399200", Name = "Chemical, Gas, Petroleum and Power Generation Plant Operators nfd", CodeSystem = "ANZSCO")]
        ChemicalGasPetroleumandPowerGenerationPlantOperatorsnfd,
        /// <summary>
        /// The chemical plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "399211", Name = "Chemical Plant Operator", CodeSystem = "ANZSCO")]
        ChemicalPlantOperator,
        /// <summary>
        /// The gasor petroleum operator
        /// </summary>
        [EnumMember]
        [Name(Code = "399212", Name = "Gas or Petroleum Operator", CodeSystem = "ANZSCO")]
        GasorPetroleumOperator,
        /// <summary>
        /// The power generation plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "399213", Name = "Power Generation Plant Operator", CodeSystem = "ANZSCO")]
        PowerGenerationPlantOperator,
        /// <summary>
        /// The gallery libraryand museum techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399300", Name = "Gallery, Library and Museum Technicians nfd", CodeSystem = "ANZSCO")]
        GalleryLibraryandMuseumTechniciansnfd,
        /// <summary>
        /// The galleryor museum technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399311", Name = "Gallery or Museum Technician", CodeSystem = "ANZSCO")]
        GalleryorMuseumTechnician,
        /// <summary>
        /// The library technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399312", Name = "Library Technician", CodeSystem = "ANZSCO")]
        LibraryTechnician,
        /// <summary>
        /// The jeweller
        /// </summary>
        [EnumMember]
        [Name(Code = "399411", Name = "Jeweller", CodeSystem = "ANZSCO")]
        Jeweller,
        /// <summary>
        /// The performing arts techniciansnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399500", Name = "Performing Arts Technicians nfd", CodeSystem = "ANZSCO")]
        PerformingArtsTechniciansnfd,
        /// <summary>
        /// The broadcast transmitter operator
        /// </summary>
        [EnumMember]
        [Name(Code = "399511", Name = "Broadcast Transmitter Operator", CodeSystem = "ANZSCO")]
        BroadcastTransmitterOperator,
        /// <summary>
        /// The camera operator film televisionor video
        /// </summary>
        [EnumMember]
        [Name(Code = "399512", Name = "Camera Operator (Film, Television or Video)", CodeSystem = "ANZSCO")]
        CameraOperatorFilmTelevisionorVideo,
        /// <summary>
        /// The light technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399513", Name = "Light Technician", CodeSystem = "ANZSCO")]
        LightTechnician,
        /// <summary>
        /// The make up artist
        /// </summary>
        [EnumMember]
        [Name(Code = "399514", Name = "Make Up Artist", CodeSystem = "ANZSCO")]
        MakeUpArtist,
        /// <summary>
        /// The musical instrument makeror repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "399515", Name = "Musical Instrument Maker or Repairer", CodeSystem = "ANZSCO")]
        MusicalInstrumentMakerorRepairer,
        /// <summary>
        /// The sound technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399516", Name = "Sound Technician", CodeSystem = "ANZSCO")]
        SoundTechnician,
        /// <summary>
        /// The television equipment operator
        /// </summary>
        [EnumMember]
        [Name(Code = "399517", Name = "Television Equipment Operator", CodeSystem = "ANZSCO")]
        TelevisionEquipmentOperator,
        /// <summary>
        /// The performing arts techniciansnec
        /// </summary>
        [EnumMember]
        [Name(Code = "399599", Name = "Performing Arts Technicians nec", CodeSystem = "ANZSCO")]
        PerformingArtsTechniciansnec,
        /// <summary>
        /// The signwriter
        /// </summary>
        [EnumMember]
        [Name(Code = "399611", Name = "Signwriter", CodeSystem = "ANZSCO")]
        Signwriter,
        /// <summary>
        /// The other miscellaneous techniciansand trades workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "399900", Name = "Other Miscellaneous Technicians and Trades Workers nfd", CodeSystem = "ANZSCO")]
        OtherMiscellaneousTechniciansandTradesWorkersnfd,
        /// <summary>
        /// The diver
        /// </summary>
        [EnumMember]
        [Name(Code = "399911", Name = "Diver", CodeSystem = "ANZSCO")]
        Diver,
        /// <summary>
        /// The interior decorator
        /// </summary>
        [EnumMember]
        [Name(Code = "399912", Name = "Interior Decorator", CodeSystem = "ANZSCO")]
        InteriorDecorator,
        /// <summary>
        /// The optical dispenser
        /// </summary>
        [EnumMember]
        [Name(Code = "399913", Name = "Optical Dispenser", CodeSystem = "ANZSCO")]
        OpticalDispenser,
        /// <summary>
        /// The optical mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "399914", Name = "Optical Mechanic", CodeSystem = "ANZSCO")]
        OpticalMechanic,
        /// <summary>
        /// The photographers assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "399915", Name = "Photographer's Assistant", CodeSystem = "ANZSCO")]
        PhotographersAssistant,
        /// <summary>
        /// The plastics technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399916", Name = "Plastics Technician", CodeSystem = "ANZSCO")]
        PlasticsTechnician,
        /// <summary>
        /// The wool classer
        /// </summary>
        [EnumMember]
        [Name(Code = "399917", Name = "Wool Classer", CodeSystem = "ANZSCO")]
        WoolClasser,
        /// <summary>
        /// The fire protection equipment technician
        /// </summary>
        [EnumMember]
        [Name(Code = "399918", Name = "Fire Protection Equipment Technician", CodeSystem = "ANZSCO")]
        FireProtectionEquipmentTechnician,
        /// <summary>
        /// The techniciansand trades workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "399999", Name = "Technicians and Trades Workers nec", CodeSystem = "ANZSCO")]
        TechniciansandTradesWorkersnec,
        /// <summary>
        /// The communityand personal service workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "400000", Name = "Community and Personal Service Workers nfd", CodeSystem = "ANZSCO")]
        CommunityandPersonalServiceWorkersnfd,
        /// <summary>
        /// The healthand welfare support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "411000", Name = "Health and Welfare Support Workers nfd", CodeSystem = "ANZSCO")]
        HealthandWelfareSupportWorkersnfd,
        /// <summary>
        /// The ambulance officersand paramedicsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "411100", Name = "Ambulance Officers and Paramedics nfd", CodeSystem = "ANZSCO")]
        AmbulanceOfficersandParamedicsnfd,
        /// <summary>
        /// The ambulance officer
        /// </summary>
        [EnumMember]
        [Name(Code = "411111", Name = "Ambulance Officer", CodeSystem = "ANZSCO")]
        AmbulanceOfficer,
        /// <summary>
        /// The intensive care ambulance paramedic
        /// </summary>
        [EnumMember]
        [Name(Code = "411112", Name = "Intensive Care Ambulance Paramedic", CodeSystem = "ANZSCO")]
        IntensiveCareAmbulanceParamedic,
        /// <summary>
        /// The dental hygienists techniciansand therapistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "411200", Name = "Dental Hygienists, Technicians and Therapists nfd", CodeSystem = "ANZSCO")]
        DentalHygienistsTechniciansandTherapistsnfd,
        /// <summary>
        /// The dental hygienist
        /// </summary>
        [EnumMember]
        [Name(Code = "411211", Name = "Dental Hygienist", CodeSystem = "ANZSCO")]
        DentalHygienist,
        /// <summary>
        /// The dental prosthetist
        /// </summary>
        [EnumMember]
        [Name(Code = "411212", Name = "Dental Prosthetist", CodeSystem = "ANZSCO")]
        DentalProsthetist,
        /// <summary>
        /// The dental technician
        /// </summary>
        [EnumMember]
        [Name(Code = "411213", Name = "Dental Technician", CodeSystem = "ANZSCO")]
        DentalTechnician,
        /// <summary>
        /// The dental therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "411214", Name = "Dental Therapist", CodeSystem = "ANZSCO")]
        DentalTherapist,
        /// <summary>
        /// The diversional therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "411311", Name = "Diversional Therapist", CodeSystem = "ANZSCO")]
        DiversionalTherapist,
        /// <summary>
        /// The enrolledand mothercraft nursesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "411400", Name = "Enrolled and Mothercraft Nurses nfd", CodeSystem = "ANZSCO")]
        EnrolledandMothercraftNursesnfd,
        /// <summary>
        /// The enrolled nurse
        /// </summary>
        [EnumMember]
        [Name(Code = "411411", Name = "Enrolled Nurse", CodeSystem = "ANZSCO")]
        EnrolledNurse,
        /// <summary>
        /// The mothercraft nurse
        /// </summary>
        [EnumMember]
        [Name(Code = "411412", Name = "Mothercraft Nurse", CodeSystem = "ANZSCO")]
        MothercraftNurse,
        /// <summary>
        /// The aboriginaland torres strait islander health worker
        /// </summary>
        [EnumMember]
        [Name(Code = "411511", Name = "Aboriginal and Torres Strait Islander Health Worker", CodeSystem = "ANZSCO")]
        AboriginalandTorresStraitIslanderHealthWorker,
        /// <summary>
        /// The massage therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "411611", Name = "Massage Therapist", CodeSystem = "ANZSCO")]
        MassageTherapist,
        /// <summary>
        /// The welfare support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "411700", Name = "Welfare Support Workers nfd", CodeSystem = "ANZSCO")]
        WelfareSupportWorkersnfd,
        /// <summary>
        /// The community worker
        /// </summary>
        [EnumMember]
        [Name(Code = "411711", Name = "Community Worker", CodeSystem = "ANZSCO")]
        CommunityWorker,
        /// <summary>
        /// The disabilities services officer
        /// </summary>
        [EnumMember]
        [Name(Code = "411712", Name = "Disabilities Services Officer", CodeSystem = "ANZSCO")]
        DisabilitiesServicesOfficer,
        /// <summary>
        /// The family support worker
        /// </summary>
        [EnumMember]
        [Name(Code = "411713", Name = "Family Support Worker", CodeSystem = "ANZSCO")]
        FamilySupportWorker,
        /// <summary>
        /// The paroleor probation officer
        /// </summary>
        [EnumMember]
        [Name(Code = "411714", Name = "Parole or Probation Officer", CodeSystem = "ANZSCO")]
        ParoleorProbationOfficer,
        /// <summary>
        /// The residential care officer
        /// </summary>
        [EnumMember]
        [Name(Code = "411715", Name = "Residential Care Officer", CodeSystem = "ANZSCO")]
        ResidentialCareOfficer,
        /// <summary>
        /// The youth worker
        /// </summary>
        [EnumMember]
        [Name(Code = "411716", Name = "Youth Worker", CodeSystem = "ANZSCO")]
        YouthWorker,
        /// <summary>
        /// The carersand aidesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "420000", Name = "Carers and Aides nfd", CodeSystem = "ANZSCO")]
        CarersandAidesnfd,
        /// <summary>
        /// The child carersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "421100", Name = "Child Carers nfd", CodeSystem = "ANZSCO")]
        ChildCarersnfd,
        /// <summary>
        /// The child care worker
        /// </summary>
        [EnumMember]
        [Name(Code = "421111", Name = "Child Care Worker", CodeSystem = "ANZSCO")]
        ChildCareWorker,
        /// <summary>
        /// The family day care worker
        /// </summary>
        [EnumMember]
        [Name(Code = "421112", Name = "Family Day Care Worker", CodeSystem = "ANZSCO")]
        FamilyDayCareWorker,
        /// <summary>
        /// The nanny
        /// </summary>
        [EnumMember]
        [Name(Code = "421113", Name = "Nanny", CodeSystem = "ANZSCO")]
        Nanny,
        /// <summary>
        /// The outof school hours care worker
        /// </summary>
        [EnumMember]
        [Name(Code = "421114", Name = "Out of School Hours Care Worker", CodeSystem = "ANZSCO")]
        OutofSchoolHoursCareWorker,
        /// <summary>
        /// The education aidesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "422100", Name = "Education Aides nfd", CodeSystem = "ANZSCO")]
        EducationAidesnfd,
        /// <summary>
        /// The aboriginaland torres strait islander education worker
        /// </summary>
        [EnumMember]
        [Name(Code = "422111", Name = "Aboriginal and Torres Strait Islander Education Worker", CodeSystem = "ANZSCO")]
        AboriginalandTorresStraitIslanderEducationWorker,
        /// <summary>
        /// The integration aide
        /// </summary>
        [EnumMember]
        [Name(Code = "422112", Name = "Integration Aide", CodeSystem = "ANZSCO")]
        IntegrationAide,
        /// <summary>
        /// The preschool aide
        /// </summary>
        [EnumMember]
        [Name(Code = "422115", Name = "Preschool Aide", CodeSystem = "ANZSCO")]
        PreschoolAide,
        /// <summary>
        /// The teachers aide
        /// </summary>
        [EnumMember]
        [Name(Code = "422116", Name = "Teachers' Aide", CodeSystem = "ANZSCO")]
        TeachersAide,
        /// <summary>
        /// The personal carersand assistantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "423000", Name = "Personal Carers and Assistants nfd", CodeSystem = "ANZSCO")]
        PersonalCarersandAssistantsnfd,
        /// <summary>
        /// The aged or disabled carer
        /// </summary>
        [EnumMember]
        [Name(Code = "423111", Name = "Aged or Disabled Carer", CodeSystem = "ANZSCO")]
        AgedOrDisabledCarer,
        /// <summary>
        /// The dental assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "423211", Name = "Dental Assistant", CodeSystem = "ANZSCO")]
        DentalAssistant,
        /// <summary>
        /// The nursing supportand personal care workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "423300", Name = "Nursing Support and Personal Care Workers nfd", CodeSystem = "ANZSCO")]
        NursingSupportandPersonalCareWorkersnfd,
        /// <summary>
        /// The hospital orderly
        /// </summary>
        [EnumMember]
        [Name(Code = "423311", Name = "Hospital Orderly", CodeSystem = "ANZSCO")]
        HospitalOrderly,
        /// <summary>
        /// The nursing support worker
        /// </summary>
        [EnumMember]
        [Name(Code = "423312", Name = "Nursing Support Worker", CodeSystem = "ANZSCO")]
        NursingSupportWorker,
        /// <summary>
        /// The personal care assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "423313", Name = "Personal Care Assistant", CodeSystem = "ANZSCO")]
        PersonalCareAssistant,
        /// <summary>
        /// The therapy aide
        /// </summary>
        [EnumMember]
        [Name(Code = "423314", Name = "Therapy Aide", CodeSystem = "ANZSCO")]
        TherapyAide,
        /// <summary>
        /// The special care workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "423400", Name = "Special Care Workers nfd", CodeSystem = "ANZSCO")]
        SpecialCareWorkersnfd,
        /// <summary>
        /// The childor youth residential care assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "423411", Name = "Child or Youth Residential Care Assistant", CodeSystem = "ANZSCO")]
        ChildorYouthResidentialCareAssistant,
        /// <summary>
        /// The hostel parent
        /// </summary>
        [EnumMember]
        [Name(Code = "423412", Name = "Hostel Parent", CodeSystem = "ANZSCO")]
        HostelParent,
        /// <summary>
        /// The refuge worker
        /// </summary>
        [EnumMember]
        [Name(Code = "423413", Name = "Refuge Worker", CodeSystem = "ANZSCO")]
        RefugeWorker,
        /// <summary>
        /// The hospitality workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "431000", Name = "Hospitality Workers nfd", CodeSystem = "ANZSCO")]
        HospitalityWorkersnfd,
        /// <summary>
        /// The bar attendantsand baristasnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "431100", Name = "Bar Attendants and Baristas nfd", CodeSystem = "ANZSCO")]
        BarAttendantsandBaristasnfd,
        /// <summary>
        /// The bar attendant
        /// </summary>
        [EnumMember]
        [Name(Code = "431111", Name = "Bar Attendant", CodeSystem = "ANZSCO")]
        BarAttendant,
        /// <summary>
        /// The barista
        /// </summary>
        [EnumMember]
        [Name(Code = "431112", Name = "Barista", CodeSystem = "ANZSCO")]
        Barista,
        /// <summary>
        /// The cafe worker
        /// </summary>
        [EnumMember]
        [Name(Code = "431211", Name = "Cafe Worker", CodeSystem = "ANZSCO")]
        CafeWorker,
        /// <summary>
        /// The gaming worker
        /// </summary>
        [EnumMember]
        [Name(Code = "431311", Name = "Gaming Worker", CodeSystem = "ANZSCO")]
        GamingWorker,
        /// <summary>
        /// The hotel service manager
        /// </summary>
        [EnumMember]
        [Name(Code = "431411", Name = "Hotel Service Manager", CodeSystem = "ANZSCO")]
        HotelServiceManager,
        /// <summary>
        /// The waiter
        /// </summary>
        [EnumMember]
        [Name(Code = "431511", Name = "Waiter", CodeSystem = "ANZSCO")]
        Waiter,
        /// <summary>
        /// The other hospitality workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "431900", Name = "Other Hospitality Workers nfd", CodeSystem = "ANZSCO")]
        OtherHospitalityWorkersnfd,
        /// <summary>
        /// The bar usefulor busser
        /// </summary>
        [EnumMember]
        [Name(Code = "431911", Name = "Bar Useful or Busser", CodeSystem = "ANZSCO")]
        BarUsefulorBusser,
        /// <summary>
        /// The doorpersonor luggage porter
        /// </summary>
        [EnumMember]
        [Name(Code = "431912", Name = "Doorperson or Luggage Porter", CodeSystem = "ANZSCO")]
        DoorpersonorLuggagePorter,
        /// <summary>
        /// The hospitality workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "431999", Name = "Hospitality Workers nec", CodeSystem = "ANZSCO")]
        HospitalityWorkersnec,
        /// <summary>
        /// The protective service workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "440000", Name = "Protective Service Workers nfd", CodeSystem = "ANZSCO")]
        ProtectiveServiceWorkersnfd,
        /// <summary>
        /// The defence force members fire fightersand policenfd
        /// </summary>
        [EnumMember]
        [Name(Code = "441000", Name = "Defence Force Members, Fire Fighters and Police nfd", CodeSystem = "ANZSCO")]
        DefenceForceMembersFireFightersandPolicenfd,
        /// <summary>
        /// The defence force member other ranks
        /// </summary>
        [EnumMember]
        [Name(Code = "441111", Name = "Defence Force Member - Other Ranks", CodeSystem = "ANZSCO")]
        DefenceForceMemberOtherRanks,
        /// <summary>
        /// The fireand emergency workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "441200", Name = "Fire and Emergency Workers nfd", CodeSystem = "ANZSCO")]
        FireandEmergencyWorkersnfd,
        /// <summary>
        /// The emergency service worker
        /// </summary>
        [EnumMember]
        [Name(Code = "441211", Name = "Emergency Service Worker", CodeSystem = "ANZSCO")]
        EmergencyServiceWorker,
        /// <summary>
        /// The fire fighter
        /// </summary>
        [EnumMember]
        [Name(Code = "441212", Name = "Fire Fighter", CodeSystem = "ANZSCO")]
        FireFighter,
        /// <summary>
        /// The policenfd
        /// </summary>
        [EnumMember]
        [Name(Code = "441300", Name = "Police nfd", CodeSystem = "ANZSCO")]
        Policenfd,
        /// <summary>
        /// The detective
        /// </summary>
        [EnumMember]
        [Name(Code = "441311", Name = "Detective", CodeSystem = "ANZSCO")]
        Detective,
        /// <summary>
        /// The police officer
        /// </summary>
        [EnumMember]
        [Name(Code = "441312", Name = "Police Officer", CodeSystem = "ANZSCO")]
        PoliceOfficer,
        /// <summary>
        /// The prisonand security officersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "442000", Name = "Prison and Security Officers nfd", CodeSystem = "ANZSCO")]
        PrisonandSecurityOfficersnfd,
        /// <summary>
        /// The prison officer
        /// </summary>
        [EnumMember]
        [Name(Code = "442111", Name = "Prison Officer", CodeSystem = "ANZSCO")]
        PrisonOfficer,
        /// <summary>
        /// The security officersand guardsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "442200", Name = "Security Officers and Guards nfd", CodeSystem = "ANZSCO")]
        SecurityOfficersandGuardsnfd,
        /// <summary>
        /// The alarm securityor surveillance monitor
        /// </summary>
        [EnumMember]
        [Name(Code = "442211", Name = "Alarm, Security or Surveillance Monitor", CodeSystem = "ANZSCO")]
        AlarmSecurityorSurveillanceMonitor,
        /// <summary>
        /// The armoured car escort
        /// </summary>
        [EnumMember]
        [Name(Code = "442212", Name = "Armoured Car Escort", CodeSystem = "ANZSCO")]
        ArmouredCarEscort,
        /// <summary>
        /// The crowd controller
        /// </summary>
        [EnumMember]
        [Name(Code = "442213", Name = "Crowd Controller", CodeSystem = "ANZSCO")]
        CrowdController,
        /// <summary>
        /// The private investigator
        /// </summary>
        [EnumMember]
        [Name(Code = "442214", Name = "Private Investigator", CodeSystem = "ANZSCO")]
        PrivateInvestigator,
        /// <summary>
        /// The retail loss prevention officer
        /// </summary>
        [EnumMember]
        [Name(Code = "442215", Name = "Retail Loss Prevention Officer", CodeSystem = "ANZSCO")]
        RetailLossPreventionOfficer,
        /// <summary>
        /// The security consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "442216", Name = "Security Consultant", CodeSystem = "ANZSCO")]
        SecurityConsultant,
        /// <summary>
        /// The security officer
        /// </summary>
        [EnumMember]
        [Name(Code = "442217", Name = "Security Officer", CodeSystem = "ANZSCO")]
        SecurityOfficer,
        /// <summary>
        /// The security officersand guardsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "442299", Name = "Security Officers and Guards nec", CodeSystem = "ANZSCO")]
        SecurityOfficersandGuardsnec,
        /// <summary>
        /// The sportsand personal service workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "450000", Name = "Sports and Personal Service Workers nfd", CodeSystem = "ANZSCO")]
        SportsandPersonalServiceWorkersnfd,
        /// <summary>
        /// The personal serviceand travel workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451000", Name = "Personal Service and Travel Workers nfd", CodeSystem = "ANZSCO")]
        PersonalServiceandTravelWorkersnfd,
        /// <summary>
        /// The beauty therapist
        /// </summary>
        [EnumMember]
        [Name(Code = "451111", Name = "Beauty Therapist", CodeSystem = "ANZSCO")]
        BeautyTherapist,
        /// <summary>
        /// The driving instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "451211", Name = "Driving Instructor", CodeSystem = "ANZSCO")]
        DrivingInstructor,
        /// <summary>
        /// The funeral workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451300", Name = "Funeral Workers nfd", CodeSystem = "ANZSCO")]
        FuneralWorkersnfd,
        /// <summary>
        /// The funeral director
        /// </summary>
        [EnumMember]
        [Name(Code = "451311", Name = "Funeral Director", CodeSystem = "ANZSCO")]
        FuneralDirector,
        /// <summary>
        /// The funeral workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "451399", Name = "Funeral Workers nec", CodeSystem = "ANZSCO")]
        FuneralWorkersnec,
        /// <summary>
        /// The gallery museumand tour guidesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451400", Name = "Gallery, Museum and Tour Guides nfd", CodeSystem = "ANZSCO")]
        GalleryMuseumandTourGuidesnfd,
        /// <summary>
        /// The galleryor museum guide
        /// </summary>
        [EnumMember]
        [Name(Code = "451411", Name = "Gallery or Museum Guide", CodeSystem = "ANZSCO")]
        GalleryorMuseumGuide,
        /// <summary>
        /// The tour guide
        /// </summary>
        [EnumMember]
        [Name(Code = "451412", Name = "Tour Guide", CodeSystem = "ANZSCO")]
        TourGuide,
        /// <summary>
        /// The personal care consultantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451500", Name = "Personal Care Consultants nfd", CodeSystem = "ANZSCO")]
        PersonalCareConsultantsnfd,
        /// <summary>
        /// The natural remedy consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "451511", Name = "Natural Remedy Consultant", CodeSystem = "ANZSCO")]
        NaturalRemedyConsultant,
        /// <summary>
        /// The weight loss consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "451512", Name = "Weight Loss Consultant", CodeSystem = "ANZSCO")]
        WeightLossConsultant,
        /// <summary>
        /// The tourismand travel advisersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451600", Name = "Tourism and Travel Advisers nfd", CodeSystem = "ANZSCO")]
        TourismandTravelAdvisersnfd,
        /// <summary>
        /// The tourist information officer
        /// </summary>
        [EnumMember]
        [Name(Code = "451611", Name = "Tourist Information Officer", CodeSystem = "ANZSCO")]
        TouristInformationOfficer,
        /// <summary>
        /// The travel consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "451612", Name = "Travel Consultant", CodeSystem = "ANZSCO")]
        TravelConsultant,
        /// <summary>
        /// The travel attendantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451700", Name = "Travel Attendants nfd", CodeSystem = "ANZSCO")]
        TravelAttendantsnfd,
        /// <summary>
        /// The flight attendant
        /// </summary>
        [EnumMember]
        [Name(Code = "451711", Name = "Flight Attendant", CodeSystem = "ANZSCO")]
        FlightAttendant,
        /// <summary>
        /// The travel attendantsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "451799", Name = "Travel Attendants nec", CodeSystem = "ANZSCO")]
        TravelAttendantsnec,
        /// <summary>
        /// The other personal service workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "451800", Name = "Other Personal Service Workers nfd", CodeSystem = "ANZSCO")]
        OtherPersonalServiceWorkersnfd,
        /// <summary>
        /// The civil celebrant
        /// </summary>
        [EnumMember]
        [Name(Code = "451811", Name = "Civil Celebrant", CodeSystem = "ANZSCO")]
        CivilCelebrant,
        /// <summary>
        /// The hairor beauty salon assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "451812", Name = "Hair or Beauty Salon Assistant", CodeSystem = "ANZSCO")]
        HairorBeautySalonAssistant,
        /// <summary>
        /// The sex workeror escort
        /// </summary>
        [EnumMember]
        [Name(Code = "451813", Name = "Sex Worker or Escort", CodeSystem = "ANZSCO")]
        SexWorkerorEscort,
        /// <summary>
        /// The body artist
        /// </summary>
        [EnumMember]
        [Name(Code = "451814", Name = "Body Artist", CodeSystem = "ANZSCO")]
        BodyArtist,
        /// <summary>
        /// The first aid trainer
        /// </summary>
        [EnumMember]
        [Name(Code = "451815", Name = "First Aid Trainer", CodeSystem = "ANZSCO")]
        FirstAidTrainer,
        /// <summary>
        /// The religious assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "451816", Name = "Religious Assistant", CodeSystem = "ANZSCO")]
        ReligiousAssistant,
        /// <summary>
        /// The personal service workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "451899", Name = "Personal Service Workers nec", CodeSystem = "ANZSCO")]
        PersonalServiceWorkersnec,
        /// <summary>
        /// The sportsand fitness workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "452000", Name = "Sports and Fitness Workers nfd", CodeSystem = "ANZSCO")]
        SportsandFitnessWorkersnfd,
        /// <summary>
        /// The fitness instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452111", Name = "Fitness Instructor", CodeSystem = "ANZSCO")]
        FitnessInstructor,
        /// <summary>
        /// The outdoor adventure guidesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "452200", Name = "Outdoor Adventure Guides nfd", CodeSystem = "ANZSCO")]
        OutdoorAdventureGuidesnfd,
        /// <summary>
        /// The bungy jump master
        /// </summary>
        [EnumMember]
        [Name(Code = "452211", Name = "Bungy Jump Master", CodeSystem = "ANZSCO")]
        BungyJumpMaster,
        /// <summary>
        /// The fishing guide
        /// </summary>
        [EnumMember]
        [Name(Code = "452212", Name = "Fishing Guide", CodeSystem = "ANZSCO")]
        FishingGuide,
        /// <summary>
        /// The hunting guide
        /// </summary>
        [EnumMember]
        [Name(Code = "452213", Name = "Hunting Guide", CodeSystem = "ANZSCO")]
        HuntingGuide,
        /// <summary>
        /// The mountainor glacier guide
        /// </summary>
        [EnumMember]
        [Name(Code = "452214", Name = "Mountain or Glacier Guide", CodeSystem = "ANZSCO")]
        MountainorGlacierGuide,
        /// <summary>
        /// The outdoor adventure instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452215", Name = "Outdoor Adventure Instructor", CodeSystem = "ANZSCO")]
        OutdoorAdventureInstructor,
        /// <summary>
        /// The trekking guide
        /// </summary>
        [EnumMember]
        [Name(Code = "452216", Name = "Trekking Guide", CodeSystem = "ANZSCO")]
        TrekkingGuide,
        /// <summary>
        /// The whitewater rafting guide
        /// </summary>
        [EnumMember]
        [Name(Code = "452217", Name = "Whitewater Rafting Guide", CodeSystem = "ANZSCO")]
        WhitewaterRaftingGuide,
        /// <summary>
        /// The outdoor adventure guidesnec
        /// </summary>
        [EnumMember]
        [Name(Code = "452299", Name = "Outdoor Adventure Guides nec", CodeSystem = "ANZSCO")]
        OutdoorAdventureGuidesnec,
        /// <summary>
        /// The sports coaches instructorsand officialsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "452300", Name = "Sports Coaches, Instructors and Officials nfd", CodeSystem = "ANZSCO")]
        SportsCoachesInstructorsandOfficialsnfd,
        /// <summary>
        /// The diving instructor open water
        /// </summary>
        [EnumMember]
        [Name(Code = "452311", Name = "Diving Instructor (Open Water)", CodeSystem = "ANZSCO")]
        DivingInstructorOpenWater,
        /// <summary>
        /// The gymnastics coachor instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452312", Name = "Gymnastics Coach or Instructor", CodeSystem = "ANZSCO")]
        GymnasticsCoachorInstructor,
        /// <summary>
        /// The horse riding coachor instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452313", Name = "Horse Riding Coach or Instructor", CodeSystem = "ANZSCO")]
        HorseRidingCoachorInstructor,
        /// <summary>
        /// The snowsport instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452314", Name = "Snowsport Instructor", CodeSystem = "ANZSCO")]
        SnowsportInstructor,
        /// <summary>
        /// The swimming coachor instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452315", Name = "Swimming Coach or Instructor", CodeSystem = "ANZSCO")]
        SwimmingCoachorInstructor,
        /// <summary>
        /// The tennis coach
        /// </summary>
        [EnumMember]
        [Name(Code = "452316", Name = "Tennis Coach", CodeSystem = "ANZSCO")]
        TennisCoach,
        /// <summary>
        /// The other sports coachor instructor
        /// </summary>
        [EnumMember]
        [Name(Code = "452317", Name = "Other Sports Coach or Instructor", CodeSystem = "ANZSCO")]
        OtherSportsCoachorInstructor,
        /// <summary>
        /// The dogor horse racing official
        /// </summary>
        [EnumMember]
        [Name(Code = "452318", Name = "Dog or Horse Racing Official", CodeSystem = "ANZSCO")]
        DogorHorseRacingOfficial,
        /// <summary>
        /// The sports development officer
        /// </summary>
        [EnumMember]
        [Name(Code = "452321", Name = "Sports Development Officer", CodeSystem = "ANZSCO")]
        SportsDevelopmentOfficer,
        /// <summary>
        /// The sports umpire
        /// </summary>
        [EnumMember]
        [Name(Code = "452322", Name = "Sports Umpire", CodeSystem = "ANZSCO")]
        SportsUmpire,
        /// <summary>
        /// The other sports official
        /// </summary>
        [EnumMember]
        [Name(Code = "452323", Name = "Other Sports Official", CodeSystem = "ANZSCO")]
        OtherSportsOfficial,
        /// <summary>
        /// The sportspersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "452400", Name = "Sportspersons nfd", CodeSystem = "ANZSCO")]
        Sportspersonsnfd,
        /// <summary>
        /// The footballer
        /// </summary>
        [EnumMember]
        [Name(Code = "452411", Name = "Footballer", CodeSystem = "ANZSCO")]
        Footballer,
        /// <summary>
        /// The golfer
        /// </summary>
        [EnumMember]
        [Name(Code = "452412", Name = "Golfer", CodeSystem = "ANZSCO")]
        Golfer,
        /// <summary>
        /// The jockey
        /// </summary>
        [EnumMember]
        [Name(Code = "452413", Name = "Jockey", CodeSystem = "ANZSCO")]
        Jockey,
        /// <summary>
        /// The lifeguard
        /// </summary>
        [EnumMember]
        [Name(Code = "452414", Name = "Lifeguard", CodeSystem = "ANZSCO")]
        Lifeguard,
        /// <summary>
        /// The sportspersonsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "452499", Name = "Sportspersons nec", CodeSystem = "ANZSCO")]
        Sportspersonsnec,
        /// <summary>
        /// The clericaland administrative workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "500000", Name = "Clerical and Administrative Workers nfd", CodeSystem = "ANZSCO")]
        ClericalandAdministrativeWorkersnfd,
        /// <summary>
        /// The office managersand program administratorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "510000", Name = "Office Managers and Program Administrators nfd", CodeSystem = "ANZSCO")]
        OfficeManagersandProgramAdministratorsnfd,
        /// <summary>
        /// The contract programand project administratorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "511100", Name = "Contract, Program and Project Administrators nfd", CodeSystem = "ANZSCO")]
        ContractProgramandProjectAdministratorsnfd,
        /// <summary>
        /// The contract administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "511111", Name = "Contract Administrator", CodeSystem = "ANZSCO")]
        ContractAdministrator,
        /// <summary>
        /// The programor project administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "511112", Name = "Program or Project Administrator", CodeSystem = "ANZSCO")]
        ProgramorProjectAdministrator,
        /// <summary>
        /// The officeand practice managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "512000", Name = "Office and Practice Managers nfd", CodeSystem = "ANZSCO")]
        OfficeandPracticeManagersnfd,
        /// <summary>
        /// The office manager
        /// </summary>
        [EnumMember]
        [Name(Code = "512111", Name = "Office Manager", CodeSystem = "ANZSCO")]
        OfficeManager,
        /// <summary>
        /// The practice managersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "512200", Name = "Practice Managers nfd", CodeSystem = "ANZSCO")]
        PracticeManagersnfd,
        /// <summary>
        /// The health practice manager
        /// </summary>
        [EnumMember]
        [Name(Code = "512211", Name = "Health Practice Manager", CodeSystem = "ANZSCO")]
        HealthPracticeManager,
        /// <summary>
        /// The practice managersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "512299", Name = "Practice Managers nec", CodeSystem = "ANZSCO")]
        PracticeManagersnec,
        /// <summary>
        /// The personal assistantsand secretariesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "521000", Name = "Personal Assistants and Secretaries nfd", CodeSystem = "ANZSCO")]
        PersonalAssistantsandSecretariesnfd,
        /// <summary>
        /// The personal assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "521111", Name = "Personal Assistant", CodeSystem = "ANZSCO")]
        PersonalAssistant,
        /// <summary>
        /// The secretariesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "521200", Name = "Secretaries nfd", CodeSystem = "ANZSCO")]
        Secretariesnfd,
        /// <summary>
        /// The secretary general
        /// </summary>
        [EnumMember]
        [Name(Code = "521211", Name = "Secretary (General)", CodeSystem = "ANZSCO")]
        SecretaryGeneral,
        /// <summary>
        /// The legal secretary
        /// </summary>
        [EnumMember]
        [Name(Code = "521212", Name = "Legal Secretary", CodeSystem = "ANZSCO")]
        LegalSecretary,
        /// <summary>
        /// The general clerical workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "530000", Name = "General Clerical Workers nfd", CodeSystem = "ANZSCO")]
        GeneralClericalWorkersnfd,
        /// <summary>
        /// The general clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "531111", Name = "General Clerk", CodeSystem = "ANZSCO")]
        GeneralClerk,
        /// <summary>
        /// The keyboard operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "532100", Name = "Keyboard Operators nfd", CodeSystem = "ANZSCO")]
        KeyboardOperatorsnfd,
        /// <summary>
        /// The data entry operator
        /// </summary>
        [EnumMember]
        [Name(Code = "532111", Name = "Data Entry Operator", CodeSystem = "ANZSCO")]
        DataEntryOperator,
        /// <summary>
        /// The machine shorthand reporter
        /// </summary>
        [EnumMember]
        [Name(Code = "532112", Name = "Machine Shorthand Reporter", CodeSystem = "ANZSCO")]
        MachineShorthandReporter,
        /// <summary>
        /// The word processing operator
        /// </summary>
        [EnumMember]
        [Name(Code = "532113", Name = "Word Processing Operator", CodeSystem = "ANZSCO")]
        WordProcessingOperator,
        /// <summary>
        /// The inquiry clerksand receptionistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "540000", Name = "Inquiry Clerks and Receptionists nfd", CodeSystem = "ANZSCO")]
        InquiryClerksandReceptionistsnfd,
        /// <summary>
        /// The callor contact centre information clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "541000", Name = "Call or Contact Centre Information Clerks nfd", CodeSystem = "ANZSCO")]
        CallorContactCentreInformationClerksnfd,
        /// <summary>
        /// The callor contact centre workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "541100", Name = "Call or Contact Centre Workers nfd", CodeSystem = "ANZSCO")]
        CallorContactCentreWorkersnfd,
        /// <summary>
        /// The callor contact centre team leader
        /// </summary>
        [EnumMember]
        [Name(Code = "541111", Name = "Call or Contact Centre Team Leader", CodeSystem = "ANZSCO")]
        CallorContactCentreTeamLeader,
        /// <summary>
        /// The callor contact centre operator
        /// </summary>
        [EnumMember]
        [Name(Code = "541112", Name = "Call or Contact Centre Operator", CodeSystem = "ANZSCO")]
        CallorContactCentreOperator,
        /// <summary>
        /// The inquiry clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "541211", Name = "Inquiry Clerk", CodeSystem = "ANZSCO")]
        InquiryClerk,
        /// <summary>
        /// The receptionistsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "542100", Name = "Receptionists nfd", CodeSystem = "ANZSCO")]
        Receptionistsnfd,
        /// <summary>
        /// The receptionist general
        /// </summary>
        [EnumMember]
        [Name(Code = "542111", Name = "Receptionist (General)", CodeSystem = "ANZSCO")]
        ReceptionistGeneral,
        /// <summary>
        /// The admissions clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "542112", Name = "Admissions Clerk", CodeSystem = "ANZSCO")]
        AdmissionsClerk,
        /// <summary>
        /// The hotelor motel receptionist
        /// </summary>
        [EnumMember]
        [Name(Code = "542113", Name = "Hotel or Motel Receptionist", CodeSystem = "ANZSCO")]
        HotelorMotelReceptionist,
        /// <summary>
        /// The medical receptionist
        /// </summary>
        [EnumMember]
        [Name(Code = "542114", Name = "Medical Receptionist", CodeSystem = "ANZSCO")]
        MedicalReceptionist,
        /// <summary>
        /// The numerical clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "550000", Name = "Numerical Clerks nfd", CodeSystem = "ANZSCO")]
        NumericalClerksnfd,
        /// <summary>
        /// The accounting clerksand bookkeepersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "551000", Name = "Accounting Clerks and Bookkeepers nfd", CodeSystem = "ANZSCO")]
        AccountingClerksandBookkeepersnfd,
        /// <summary>
        /// The accounting clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "551100", Name = "Accounting Clerks nfd", CodeSystem = "ANZSCO")]
        AccountingClerksnfd,
        /// <summary>
        /// The accounts clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "551111", Name = "Accounts Clerk", CodeSystem = "ANZSCO")]
        AccountsClerk,
        /// <summary>
        /// The cost clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "551112", Name = "Cost Clerk", CodeSystem = "ANZSCO")]
        CostClerk,
        /// <summary>
        /// The bookkeeper
        /// </summary>
        [EnumMember]
        [Name(Code = "551211", Name = "Bookkeeper", CodeSystem = "ANZSCO")]
        Bookkeeper,
        /// <summary>
        /// The payroll clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "551311", Name = "Payroll Clerk", CodeSystem = "ANZSCO")]
        PayrollClerk,
        /// <summary>
        /// The financialand insurance clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "552000", Name = "Financial and Insurance Clerks nfd", CodeSystem = "ANZSCO")]
        FinancialandInsuranceClerksnfd,
        /// <summary>
        /// The bank worker
        /// </summary>
        [EnumMember]
        [Name(Code = "552111", Name = "Bank Worker", CodeSystem = "ANZSCO")]
        BankWorker,
        /// <summary>
        /// The creditor loans officer
        /// </summary>
        [EnumMember]
        [Name(Code = "552211", Name = "Credit or Loans Officer", CodeSystem = "ANZSCO")]
        CreditorLoansOfficer,
        /// <summary>
        /// The insurance money marketand statistical clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "552300", Name = "Insurance, Money Market and Statistical Clerks nfd", CodeSystem = "ANZSCO")]
        InsuranceMoneyMarketandStatisticalClerksnfd,
        /// <summary>
        /// The bookmaker
        /// </summary>
        [EnumMember]
        [Name(Code = "552311", Name = "Bookmaker", CodeSystem = "ANZSCO")]
        Bookmaker,
        /// <summary>
        /// The insurance consultant
        /// </summary>
        [EnumMember]
        [Name(Code = "552312", Name = "Insurance Consultant", CodeSystem = "ANZSCO")]
        InsuranceConsultant,
        /// <summary>
        /// The money market clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "552313", Name = "Money Market Clerk", CodeSystem = "ANZSCO")]
        MoneyMarketClerk,
        /// <summary>
        /// The statistical clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "552314", Name = "Statistical Clerk", CodeSystem = "ANZSCO")]
        StatisticalClerk,
        /// <summary>
        /// The clericaland office support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "561000", Name = "Clerical and Office Support Workers nfd", CodeSystem = "ANZSCO")]
        ClericalandOfficeSupportWorkersnfd,
        /// <summary>
        /// The betting clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "561100", Name = "Betting Clerks nfd", CodeSystem = "ANZSCO")]
        BettingClerksnfd,
        /// <summary>
        /// The betting agency counter clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561111", Name = "Betting Agency Counter Clerk", CodeSystem = "ANZSCO")]
        BettingAgencyCounterClerk,
        /// <summary>
        /// The bookmakers clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561112", Name = "Bookmaker's Clerk", CodeSystem = "ANZSCO")]
        BookmakersClerk,
        /// <summary>
        /// The telephone betting clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561113", Name = "Telephone Betting Clerk", CodeSystem = "ANZSCO")]
        TelephoneBettingClerk,
        /// <summary>
        /// The betting clerksnec
        /// </summary>
        [EnumMember]
        [Name(Code = "561199", Name = "Betting Clerks nec", CodeSystem = "ANZSCO")]
        BettingClerksnec,
        /// <summary>
        /// The couriersand postal deliverersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "561200", Name = "Couriers and Postal Deliverers nfd", CodeSystem = "ANZSCO")]
        CouriersandPostalDeliverersnfd,
        /// <summary>
        /// The courier
        /// </summary>
        [EnumMember]
        [Name(Code = "561211", Name = "Courier", CodeSystem = "ANZSCO")]
        Courier,
        /// <summary>
        /// The postal delivery officer
        /// </summary>
        [EnumMember]
        [Name(Code = "561212", Name = "Postal Delivery Officer", CodeSystem = "ANZSCO")]
        PostalDeliveryOfficer,
        /// <summary>
        /// The filingor registry clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561311", Name = "Filing or Registry Clerk", CodeSystem = "ANZSCO")]
        FilingorRegistryClerk,
        /// <summary>
        /// The mail sortersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "561400", Name = "Mail Sorters nfd", CodeSystem = "ANZSCO")]
        MailSortersnfd,
        /// <summary>
        /// The mail clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561411", Name = "Mail Clerk", CodeSystem = "ANZSCO")]
        MailClerk,
        /// <summary>
        /// The postal sorting officer
        /// </summary>
        [EnumMember]
        [Name(Code = "561412", Name = "Postal Sorting Officer", CodeSystem = "ANZSCO")]
        PostalSortingOfficer,
        /// <summary>
        /// The survey interviewer
        /// </summary>
        [EnumMember]
        [Name(Code = "561511", Name = "Survey Interviewer", CodeSystem = "ANZSCO")]
        SurveyInterviewer,
        /// <summary>
        /// The switchboard operator
        /// </summary>
        [EnumMember]
        [Name(Code = "561611", Name = "Switchboard Operator", CodeSystem = "ANZSCO")]
        SwitchboardOperator,
        /// <summary>
        /// The other clericaland office support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "561900", Name = "Other Clerical and Office Support Workers nfd", CodeSystem = "ANZSCO")]
        OtherClericalandOfficeSupportWorkersnfd,
        /// <summary>
        /// The classified advertising clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "561911", Name = "Classified Advertising Clerk", CodeSystem = "ANZSCO")]
        ClassifiedAdvertisingClerk,
        /// <summary>
        /// The meter reader
        /// </summary>
        [EnumMember]
        [Name(Code = "561912", Name = "Meter Reader", CodeSystem = "ANZSCO")]
        MeterReader,
        /// <summary>
        /// The parking inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "561913", Name = "Parking Inspector", CodeSystem = "ANZSCO")]
        ParkingInspector,
        /// <summary>
        /// The clericaland office support workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "561999", Name = "Clerical and Office Support Workers nec", CodeSystem = "ANZSCO")]
        ClericalandOfficeSupportWorkersnec,
        /// <summary>
        /// The other clericaland administrative workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "590000", Name = "Other Clerical and Administrative Workers nfd", CodeSystem = "ANZSCO")]
        OtherClericalandAdministrativeWorkersnfd,
        /// <summary>
        /// The logistics clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "591000", Name = "Logistics Clerks nfd", CodeSystem = "ANZSCO")]
        LogisticsClerksnfd,
        /// <summary>
        /// The purchasingand supply logistics clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "591100", Name = "Purchasing and Supply Logistics Clerks nfd", CodeSystem = "ANZSCO")]
        PurchasingandSupplyLogisticsClerksnfd,
        /// <summary>
        /// The production clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "591112", Name = "Production Clerk", CodeSystem = "ANZSCO")]
        ProductionClerk,
        /// <summary>
        /// The purchasing officer
        /// </summary>
        [EnumMember]
        [Name(Code = "591113", Name = "Purchasing Officer", CodeSystem = "ANZSCO")]
        PurchasingOfficer,
        /// <summary>
        /// The stock clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "591115", Name = "Stock Clerk", CodeSystem = "ANZSCO")]
        StockClerk,
        /// <summary>
        /// The warehouse administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "591116", Name = "Warehouse Administrator", CodeSystem = "ANZSCO")]
        WarehouseAdministrator,
        /// <summary>
        /// The order clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "591117", Name = "Order Clerk", CodeSystem = "ANZSCO")]
        OrderClerk,
        /// <summary>
        /// The transportand despatch clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "591200", Name = "Transport and Despatch Clerks nfd", CodeSystem = "ANZSCO")]
        TransportandDespatchClerksnfd,
        /// <summary>
        /// The despatchingand receiving clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "591211", Name = "Despatching and Receiving Clerk", CodeSystem = "ANZSCO")]
        DespatchingandReceivingClerk,
        /// <summary>
        /// The import export clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "591212", Name = "Import-Export Clerk", CodeSystem = "ANZSCO")]
        ImportExportClerk,
        /// <summary>
        /// The miscellaneous clericaland administrative workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599000", Name = "Miscellaneous Clerical and Administrative Workers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousClericalandAdministrativeWorkersnfd,
        /// <summary>
        /// The conveyancersand legal executivesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599100", Name = "Conveyancers and Legal Executives nfd", CodeSystem = "ANZSCO")]
        ConveyancersandLegalExecutivesnfd,
        /// <summary>
        /// The conveyancer
        /// </summary>
        [EnumMember]
        [Name(Code = "599111", Name = "Conveyancer", CodeSystem = "ANZSCO")]
        Conveyancer,
        /// <summary>
        /// The legal executive
        /// </summary>
        [EnumMember]
        [Name(Code = "599112", Name = "Legal Executive", CodeSystem = "ANZSCO")]
        LegalExecutive,
        /// <summary>
        /// The courtand legal clerksnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599200", Name = "Court and Legal Clerks nfd", CodeSystem = "ANZSCO")]
        CourtandLegalClerksnfd,
        /// <summary>
        /// The clerkof court
        /// </summary>
        [EnumMember]
        [Name(Code = "599211", Name = "Clerk of Court", CodeSystem = "ANZSCO")]
        ClerkofCourt,
        /// <summary>
        /// The court bailiffor sheriff
        /// </summary>
        [EnumMember]
        [Name(Code = "599212", Name = "Court Bailiff or Sheriff", CodeSystem = "ANZSCO")]
        CourtBailifforSheriff,
        /// <summary>
        /// The court orderly
        /// </summary>
        [EnumMember]
        [Name(Code = "599213", Name = "Court Orderly", CodeSystem = "ANZSCO")]
        CourtOrderly,
        /// <summary>
        /// The law clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "599214", Name = "Law Clerk", CodeSystem = "ANZSCO")]
        LawClerk,
        /// <summary>
        /// The trust officer
        /// </summary>
        [EnumMember]
        [Name(Code = "599215", Name = "Trust Officer", CodeSystem = "ANZSCO")]
        TrustOfficer,
        /// <summary>
        /// The debt collector
        /// </summary>
        [EnumMember]
        [Name(Code = "599311", Name = "Debt Collector", CodeSystem = "ANZSCO")]
        DebtCollector,
        /// <summary>
        /// The human resource clerk
        /// </summary>
        [EnumMember]
        [Name(Code = "599411", Name = "Human Resource Clerk", CodeSystem = "ANZSCO")]
        HumanResourceClerk,
        /// <summary>
        /// The inspectorsand regulatory officersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599500", Name = "Inspectors and Regulatory Officers nfd", CodeSystem = "ANZSCO")]
        InspectorsandRegulatoryOfficersnfd,
        /// <summary>
        /// The customs officer
        /// </summary>
        [EnumMember]
        [Name(Code = "599511", Name = "Customs Officer", CodeSystem = "ANZSCO")]
        CustomsOfficer,
        /// <summary>
        /// The immigration officer
        /// </summary>
        [EnumMember]
        [Name(Code = "599512", Name = "Immigration Officer", CodeSystem = "ANZSCO")]
        ImmigrationOfficer,
        /// <summary>
        /// The motor vehicle licence examiner
        /// </summary>
        [EnumMember]
        [Name(Code = "599513", Name = "Motor Vehicle Licence Examiner", CodeSystem = "ANZSCO")]
        MotorVehicleLicenceExaminer,
        /// <summary>
        /// The noxious weedsand pest inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "599514", Name = "Noxious Weeds and Pest Inspector", CodeSystem = "ANZSCO")]
        NoxiousWeedsandPestInspector,
        /// <summary>
        /// The social security assessor
        /// </summary>
        [EnumMember]
        [Name(Code = "599515", Name = "Social Security Assessor", CodeSystem = "ANZSCO")]
        SocialSecurityAssessor,
        /// <summary>
        /// The taxation inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "599516", Name = "Taxation Inspector", CodeSystem = "ANZSCO")]
        TaxationInspector,
        /// <summary>
        /// The train examiner
        /// </summary>
        [EnumMember]
        [Name(Code = "599517", Name = "Train Examiner", CodeSystem = "ANZSCO")]
        TrainExaminer,
        /// <summary>
        /// The transport operations inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "599518", Name = "Transport Operations Inspector", CodeSystem = "ANZSCO")]
        TransportOperationsInspector,
        /// <summary>
        /// The water inspector
        /// </summary>
        [EnumMember]
        [Name(Code = "599521", Name = "Water Inspector", CodeSystem = "ANZSCO")]
        WaterInspector,
        /// <summary>
        /// The inspectorsand regulatory officersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "599599", Name = "Inspectors and Regulatory Officers nec", CodeSystem = "ANZSCO")]
        InspectorsandRegulatoryOfficersnec,
        /// <summary>
        /// The insurance investigators loss adjustersand risk surveyorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599600", Name = "Insurance Investigators, Loss Adjusters and Risk Surveyors nfd", CodeSystem = "ANZSCO")]
        InsuranceInvestigatorsLossAdjustersandRiskSurveyorsnfd,
        /// <summary>
        /// The insurance investigator
        /// </summary>
        [EnumMember]
        [Name(Code = "599611", Name = "Insurance Investigator", CodeSystem = "ANZSCO")]
        InsuranceInvestigator,
        /// <summary>
        /// The insurance loss adjuster
        /// </summary>
        [EnumMember]
        [Name(Code = "599612", Name = "Insurance Loss Adjuster", CodeSystem = "ANZSCO")]
        InsuranceLossAdjuster,
        /// <summary>
        /// The insurance risk surveyor
        /// </summary>
        [EnumMember]
        [Name(Code = "599613", Name = "Insurance Risk Surveyor", CodeSystem = "ANZSCO")]
        InsuranceRiskSurveyor,
        /// <summary>
        /// The library assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "599711", Name = "Library Assistant", CodeSystem = "ANZSCO")]
        LibraryAssistant,
        /// <summary>
        /// The other miscellaneous clericaland administrative workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "599900", Name = "Other Miscellaneous Clerical and Administrative Workers nfd", CodeSystem = "ANZSCO")]
        OtherMiscellaneousClericalandAdministrativeWorkersnfd,
        /// <summary>
        /// The production assistant film television radioor stage
        /// </summary>
        [EnumMember]
        [Name(Code = "599912", Name = "Production Assistant (Film, Television, Radio or Stage)", CodeSystem = "ANZSCO")]
        ProductionAssistantFilmTelevisionRadioorStage,
        /// <summary>
        /// The proof reader
        /// </summary>
        [EnumMember]
        [Name(Code = "599913", Name = "Proof Reader", CodeSystem = "ANZSCO")]
        ProofReader,
        /// <summary>
        /// The radio despatcher
        /// </summary>
        [EnumMember]
        [Name(Code = "599914", Name = "Radio Despatcher", CodeSystem = "ANZSCO")]
        RadioDespatcher,
        /// <summary>
        /// The clinical coder
        /// </summary>
        [EnumMember]
        [Name(Code = "599915", Name = "Clinical Coder", CodeSystem = "ANZSCO")]
        ClinicalCoder,
        /// <summary>
        /// The facilities administrator
        /// </summary>
        [EnumMember]
        [Name(Code = "599916", Name = "Facilities Administrator", CodeSystem = "ANZSCO")]
        FacilitiesAdministrator,
        /// <summary>
        /// The clericaland administrative workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "599999", Name = "Clerical and Administrative Workers nec", CodeSystem = "ANZSCO")]
        ClericalandAdministrativeWorkersnec,
        /// <summary>
        /// The sales workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "600000", Name = "Sales Workers nfd", CodeSystem = "ANZSCO")]
        SalesWorkersnfd,
        /// <summary>
        /// The sales representativesand agentsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "610000", Name = "Sales Representatives and Agents nfd", CodeSystem = "ANZSCO")]
        SalesRepresentativesandAgentsnfd,
        /// <summary>
        /// The insurance agentsand sales representativesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "611000", Name = "Insurance Agents and Sales Representatives nfd", CodeSystem = "ANZSCO")]
        InsuranceAgentsandSalesRepresentativesnfd,
        /// <summary>
        /// The auctioneersand stockand station agentsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "611100", Name = "Auctioneers, and Stock and Station Agents nfd", CodeSystem = "ANZSCO")]
        AuctioneersandStockandStationAgentsnfd,
        /// <summary>
        /// The auctioneer
        /// </summary>
        [EnumMember]
        [Name(Code = "611111", Name = "Auctioneer", CodeSystem = "ANZSCO")]
        Auctioneer,
        /// <summary>
        /// The stockand station agent
        /// </summary>
        [EnumMember]
        [Name(Code = "611112", Name = "Stock and Station Agent", CodeSystem = "ANZSCO")]
        StockandStationAgent,
        /// <summary>
        /// The insurance agent
        /// </summary>
        [EnumMember]
        [Name(Code = "611211", Name = "Insurance Agent", CodeSystem = "ANZSCO")]
        InsuranceAgent,
        /// <summary>
        /// The sales representativesnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "611300", Name = "Sales Representatives nfd", CodeSystem = "ANZSCO")]
        SalesRepresentativesnfd,
        /// <summary>
        /// The sales representative buildingand plumbing supplies
        /// </summary>
        [EnumMember]
        [Name(Code = "611311", Name = "Sales Representative (Building and Plumbing Supplies)", CodeSystem = "ANZSCO")]
        SalesRepresentativeBuildingandPlumbingSupplies,
        /// <summary>
        /// The sales representative business services
        /// </summary>
        [EnumMember]
        [Name(Code = "611312", Name = "Sales Representative (Business Services)", CodeSystem = "ANZSCO")]
        SalesRepresentativeBusinessServices,
        /// <summary>
        /// The sales representative motor vehicle partsand accessories
        /// </summary>
        [EnumMember]
        [Name(Code = "611313", Name = "Sales Representative (Motor Vehicle Parts and Accessories)", CodeSystem = "ANZSCO")]
        SalesRepresentativeMotorVehiclePartsandAccessories,
        /// <summary>
        /// The sales representative personaland household goods
        /// </summary>
        [EnumMember]
        [Name(Code = "611314", Name = "Sales Representative (Personal and Household Goods)", CodeSystem = "ANZSCO")]
        SalesRepresentativePersonalandHouseholdGoods,
        /// <summary>
        /// The sales representativesnec
        /// </summary>
        [EnumMember]
        [Name(Code = "611399", Name = "Sales Representatives nec", CodeSystem = "ANZSCO")]
        SalesRepresentativesnec,
        /// <summary>
        /// The real estate sales agentsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "612100", Name = "Real Estate Sales Agents nfd", CodeSystem = "ANZSCO")]
        RealEstateSalesAgentsnfd,
        /// <summary>
        /// The business broker
        /// </summary>
        [EnumMember]
        [Name(Code = "612111", Name = "Business Broker", CodeSystem = "ANZSCO")]
        BusinessBroker,
        /// <summary>
        /// The property manager
        /// </summary>
        [EnumMember]
        [Name(Code = "612112", Name = "Property Manager", CodeSystem = "ANZSCO")]
        PropertyManager,
        /// <summary>
        /// The real estate agency principal
        /// </summary>
        [EnumMember]
        [Name(Code = "612113", Name = "Real Estate Agency Principal", CodeSystem = "ANZSCO")]
        RealEstateAgencyPrincipal,
        /// <summary>
        /// The real estate agent
        /// </summary>
        [EnumMember]
        [Name(Code = "612114", Name = "Real Estate Agent", CodeSystem = "ANZSCO")]
        RealEstateAgent,
        /// <summary>
        /// The real estate representative
        /// </summary>
        [EnumMember]
        [Name(Code = "612115", Name = "Real Estate Representative", CodeSystem = "ANZSCO")]
        RealEstateRepresentative,
        /// <summary>
        /// The sales assistantsand salespersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "621000", Name = "Sales Assistants and Salespersons nfd", CodeSystem = "ANZSCO")]
        SalesAssistantsandSalespersonsnfd,
        /// <summary>
        /// The sales assistant general
        /// </summary>
        [EnumMember]
        [Name(Code = "621111", Name = "Sales Assistant (General)", CodeSystem = "ANZSCO")]
        SalesAssistantGeneral,
        /// <summary>
        /// The ICT sales assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "621211", Name = "ICT Sales Assistant", CodeSystem = "ANZSCO")]
        ICTSalesAssistant,
        /// <summary>
        /// The motor vehicleand vehicle parts salespersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "621300", Name = "Motor Vehicle and Vehicle Parts Salespersons nfd", CodeSystem = "ANZSCO")]
        MotorVehicleandVehiclePartsSalespersonsnfd,
        /// <summary>
        /// The motor vehicleor caravan salesperson
        /// </summary>
        [EnumMember]
        [Name(Code = "621311", Name = "Motor Vehicle or Caravan Salesperson", CodeSystem = "ANZSCO")]
        MotorVehicleorCaravanSalesperson,
        /// <summary>
        /// The motor vehicle parts interpreter
        /// </summary>
        [EnumMember]
        [Name(Code = "621312", Name = "Motor Vehicle Parts Interpreter", CodeSystem = "ANZSCO")]
        MotorVehiclePartsInterpreter,
        /// <summary>
        /// The pharmacy sales assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "621411", Name = "Pharmacy Sales Assistant", CodeSystem = "ANZSCO")]
        PharmacySalesAssistant,
        /// <summary>
        /// The retail supervisor
        /// </summary>
        [EnumMember]
        [Name(Code = "621511", Name = "Retail Supervisor", CodeSystem = "ANZSCO")]
        RetailSupervisor,
        /// <summary>
        /// The service station attendant
        /// </summary>
        [EnumMember]
        [Name(Code = "621611", Name = "Service Station Attendant", CodeSystem = "ANZSCO")]
        ServiceStationAttendant,
        /// <summary>
        /// The street vendorsand related salespersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "621700", Name = "Street Vendors and Related Salespersons nfd", CodeSystem = "ANZSCO")]
        StreetVendorsandRelatedSalespersonsnfd,
        /// <summary>
        /// The cash van salesperson
        /// </summary>
        [EnumMember]
        [Name(Code = "621711", Name = "Cash Van Salesperson", CodeSystem = "ANZSCO")]
        CashVanSalesperson,
        /// <summary>
        /// The doortodoor salesperson
        /// </summary>
        [EnumMember]
        [Name(Code = "621712", Name = "Door-to-door Salesperson", CodeSystem = "ANZSCO")]
        DoortodoorSalesperson,
        /// <summary>
        /// The street vendor
        /// </summary>
        [EnumMember]
        [Name(Code = "621713", Name = "Street Vendor", CodeSystem = "ANZSCO")]
        StreetVendor,
        /// <summary>
        /// The other sales assistantsand salespersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "621900", Name = "Other Sales Assistants and Salespersons nfd", CodeSystem = "ANZSCO")]
        OtherSalesAssistantsandSalespersonsnfd,
        /// <summary>
        /// The materials recycler
        /// </summary>
        [EnumMember]
        [Name(Code = "621911", Name = "Materials Recycler", CodeSystem = "ANZSCO")]
        MaterialsRecycler,
        /// <summary>
        /// The rental salesperson
        /// </summary>
        [EnumMember]
        [Name(Code = "621912", Name = "Rental Salesperson", CodeSystem = "ANZSCO")]
        RentalSalesperson,
        /// <summary>
        /// The sales assistantsand salespersonsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "621999", Name = "Sales Assistants and Salespersons nec", CodeSystem = "ANZSCO")]
        SalesAssistantsandSalespersonsnec,
        /// <summary>
        /// The sales support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "630000", Name = "Sales Support Workers nfd", CodeSystem = "ANZSCO")]
        SalesSupportWorkersnfd,
        /// <summary>
        /// The checkout operatorsand office cashiersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "631100", Name = "Checkout Operators and Office Cashiers nfd", CodeSystem = "ANZSCO")]
        CheckoutOperatorsandOfficeCashiersnfd,
        /// <summary>
        /// The checkout operator
        /// </summary>
        [EnumMember]
        [Name(Code = "631111", Name = "Checkout Operator", CodeSystem = "ANZSCO")]
        CheckoutOperator,
        /// <summary>
        /// The office cashier
        /// </summary>
        [EnumMember]
        [Name(Code = "631112", Name = "Office Cashier", CodeSystem = "ANZSCO")]
        OfficeCashier,
        /// <summary>
        /// The miscellaneous sales support workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "639000", Name = "Miscellaneous Sales Support Workers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousSalesSupportWorkersnfd,
        /// <summary>
        /// The modelsand sales demonstratorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "639100", Name = "Models and Sales Demonstrators nfd", CodeSystem = "ANZSCO")]
        ModelsandSalesDemonstratorsnfd,
        /// <summary>
        /// The model
        /// </summary>
        [EnumMember]
        [Name(Code = "639111", Name = "Model", CodeSystem = "ANZSCO")]
        Model,
        /// <summary>
        /// The sales demonstrator
        /// </summary>
        [EnumMember]
        [Name(Code = "639112", Name = "Sales Demonstrator", CodeSystem = "ANZSCO")]
        SalesDemonstrator,
        /// <summary>
        /// The retailand wool buyersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "639200", Name = "Retail and Wool Buyers nfd", CodeSystem = "ANZSCO")]
        RetailandWoolBuyersnfd,
        /// <summary>
        /// The retail buyer
        /// </summary>
        [EnumMember]
        [Name(Code = "639211", Name = "Retail Buyer", CodeSystem = "ANZSCO")]
        RetailBuyer,
        /// <summary>
        /// The wool buyer
        /// </summary>
        [EnumMember]
        [Name(Code = "639212", Name = "Wool Buyer", CodeSystem = "ANZSCO")]
        WoolBuyer,
        /// <summary>
        /// The telemarketer
        /// </summary>
        [EnumMember]
        [Name(Code = "639311", Name = "Telemarketer", CodeSystem = "ANZSCO")]
        Telemarketer,
        /// <summary>
        /// The ticket salespersonsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "639400", Name = "Ticket Salespersons nfd", CodeSystem = "ANZSCO")]
        TicketSalespersonsnfd,
        /// <summary>
        /// The ticket seller
        /// </summary>
        [EnumMember]
        [Name(Code = "639411", Name = "Ticket Seller", CodeSystem = "ANZSCO")]
        TicketSeller,
        /// <summary>
        /// The transport conductor
        /// </summary>
        [EnumMember]
        [Name(Code = "639412", Name = "Transport Conductor", CodeSystem = "ANZSCO")]
        TransportConductor,
        /// <summary>
        /// The visual merchandiser
        /// </summary>
        [EnumMember]
        [Name(Code = "639511", Name = "Visual Merchandiser", CodeSystem = "ANZSCO")]
        VisualMerchandiser,
        /// <summary>
        /// The other sales support worker
        /// </summary>
        [EnumMember]
        [Name(Code = "639911", Name = "Other Sales Support Worker", CodeSystem = "ANZSCO")]
        OtherSalesSupportWorker,
        /// <summary>
        /// The machinery operatorsand driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "700000", Name = "Machinery Operators and Drivers nfd", CodeSystem = "ANZSCO")]
        MachineryOperatorsandDriversnfd,
        /// <summary>
        /// The machineand stationary plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "710000", Name = "Machine and Stationary Plant Operators nfd", CodeSystem = "ANZSCO")]
        MachineandStationaryPlantOperatorsnfd,
        /// <summary>
        /// The machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711000", Name = "Machine Operators nfd", CodeSystem = "ANZSCO")]
        MachineOperatorsnfd,
        /// <summary>
        /// The clay concrete glassand stone processing machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711100", Name = "Clay, Concrete, Glass and Stone Processing Machine Operators nfd", CodeSystem = "ANZSCO")]
        ClayConcreteGlassandStoneProcessingMachineOperatorsnfd,
        /// <summary>
        /// The clay products machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711111", Name = "Clay Products Machine Operator", CodeSystem = "ANZSCO")]
        ClayProductsMachineOperator,
        /// <summary>
        /// The concrete products machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711112", Name = "Concrete Products Machine Operator", CodeSystem = "ANZSCO")]
        ConcreteProductsMachineOperator,
        /// <summary>
        /// The glass production machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711113", Name = "Glass Production Machine Operator", CodeSystem = "ANZSCO")]
        GlassProductionMachineOperator,
        /// <summary>
        /// The stone processing machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711114", Name = "Stone Processing Machine Operator", CodeSystem = "ANZSCO")]
        StoneProcessingMachineOperator,
        /// <summary>
        /// The clay concrete glassand stone processing machine operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "711199", Name = "Clay, Concrete, Glass and Stone Processing Machine Operators nec", CodeSystem = "ANZSCO")]
        ClayConcreteGlassandStoneProcessingMachineOperatorsnec,
        /// <summary>
        /// The industrial spraypainter
        /// </summary>
        [EnumMember]
        [Name(Code = "711211", Name = "Industrial Spraypainter", CodeSystem = "ANZSCO")]
        IndustrialSpraypainter,
        /// <summary>
        /// The paperand wood processing machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711300", Name = "Paper and Wood Processing Machine Operators nfd", CodeSystem = "ANZSCO")]
        PaperandWoodProcessingMachineOperatorsnfd,
        /// <summary>
        /// The paper products machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711311", Name = "Paper Products Machine Operator", CodeSystem = "ANZSCO")]
        PaperProductsMachineOperator,
        /// <summary>
        /// The sawmilling operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711313", Name = "Sawmilling Operator", CodeSystem = "ANZSCO")]
        SawmillingOperator,
        /// <summary>
        /// The other wood processing machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711314", Name = "Other Wood Processing Machine Operator", CodeSystem = "ANZSCO")]
        OtherWoodProcessingMachineOperator,
        /// <summary>
        /// The photographic developerand printer
        /// </summary>
        [EnumMember]
        [Name(Code = "711411", Name = "Photographic Developer and Printer", CodeSystem = "ANZSCO")]
        PhotographicDeveloperandPrinter,
        /// <summary>
        /// The plasticsand rubber production machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711500", Name = "Plastics and Rubber Production Machine Operators nfd", CodeSystem = "ANZSCO")]
        PlasticsandRubberProductionMachineOperatorsnfd,
        /// <summary>
        /// The plastic cablemaking machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711511", Name = "Plastic Cablemaking Machine Operator", CodeSystem = "ANZSCO")]
        PlasticCablemakingMachineOperator,
        /// <summary>
        /// The plastic compoundingand reclamation machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711512", Name = "Plastic Compounding and Reclamation Machine Operator", CodeSystem = "ANZSCO")]
        PlasticCompoundingandReclamationMachineOperator,
        /// <summary>
        /// The plastics fabricatoror welder
        /// </summary>
        [EnumMember]
        [Name(Code = "711513", Name = "Plastics Fabricator or Welder", CodeSystem = "ANZSCO")]
        PlasticsFabricatororWelder,
        /// <summary>
        /// The plastics production machine operator general
        /// </summary>
        [EnumMember]
        [Name(Code = "711514", Name = "Plastics Production Machine Operator (General)", CodeSystem = "ANZSCO")]
        PlasticsProductionMachineOperatorGeneral,
        /// <summary>
        /// The reinforced plasticand composite production worker
        /// </summary>
        [EnumMember]
        [Name(Code = "711515", Name = "Reinforced Plastic and Composite Production Worker", CodeSystem = "ANZSCO")]
        ReinforcedPlasticandCompositeProductionWorker,
        /// <summary>
        /// The rubber production machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711516", Name = "Rubber Production Machine Operator", CodeSystem = "ANZSCO")]
        RubberProductionMachineOperator,
        /// <summary>
        /// The plasticsand rubber production machine operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "711599", Name = "Plastics and Rubber Production Machine Operators nec", CodeSystem = "ANZSCO")]
        PlasticsandRubberProductionMachineOperatorsnec,
        /// <summary>
        /// The sewing machinist
        /// </summary>
        [EnumMember]
        [Name(Code = "711611", Name = "Sewing Machinist", CodeSystem = "ANZSCO")]
        SewingMachinist,
        /// <summary>
        /// The textileand footwear production machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711700", Name = "Textile and Footwear Production Machine Operators nfd", CodeSystem = "ANZSCO")]
        TextileandFootwearProductionMachineOperatorsnfd,
        /// <summary>
        /// The footwear production machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711711", Name = "Footwear Production Machine Operator", CodeSystem = "ANZSCO")]
        FootwearProductionMachineOperator,
        /// <summary>
        /// The hideand skin processing machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711712", Name = "Hide and Skin Processing Machine Operator", CodeSystem = "ANZSCO")]
        HideandSkinProcessingMachineOperator,
        /// <summary>
        /// The knitting machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711713", Name = "Knitting Machine Operator", CodeSystem = "ANZSCO")]
        KnittingMachineOperator,
        /// <summary>
        /// The textile dyeingand finishing machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711714", Name = "Textile Dyeing and Finishing Machine Operator", CodeSystem = "ANZSCO")]
        TextileDyeingandFinishingMachineOperator,
        /// <summary>
        /// The weaving machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711715", Name = "Weaving Machine Operator", CodeSystem = "ANZSCO")]
        WeavingMachineOperator,
        /// <summary>
        /// The yarn cardingand spinning machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711716", Name = "Yarn Carding and Spinning Machine Operator", CodeSystem = "ANZSCO")]
        YarnCardingandSpinningMachineOperator,
        /// <summary>
        /// The textileand footwear production machine operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "711799", Name = "Textile and Footwear Production Machine Operators nec", CodeSystem = "ANZSCO")]
        TextileandFootwearProductionMachineOperatorsnec,
        /// <summary>
        /// The other machine operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "711900", Name = "Other Machine Operators nfd", CodeSystem = "ANZSCO")]
        OtherMachineOperatorsnfd,
        /// <summary>
        /// The chemical production machine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "711911", Name = "Chemical Production Machine Operator", CodeSystem = "ANZSCO")]
        ChemicalProductionMachineOperator,
        /// <summary>
        /// The motion picture projectionist
        /// </summary>
        [EnumMember]
        [Name(Code = "711912", Name = "Motion Picture Projectionist", CodeSystem = "ANZSCO")]
        MotionPictureProjectionist,
        /// <summary>
        /// The sand blaster
        /// </summary>
        [EnumMember]
        [Name(Code = "711913", Name = "Sand Blaster", CodeSystem = "ANZSCO")]
        SandBlaster,
        /// <summary>
        /// The sterilisation technician
        /// </summary>
        [EnumMember]
        [Name(Code = "711914", Name = "Sterilisation Technician", CodeSystem = "ANZSCO")]
        SterilisationTechnician,
        /// <summary>
        /// The machine operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "711999", Name = "Machine Operators nec", CodeSystem = "ANZSCO")]
        MachineOperatorsnec,
        /// <summary>
        /// The stationary plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "712000", Name = "Stationary Plant Operators nfd", CodeSystem = "ANZSCO")]
        StationaryPlantOperatorsnfd,
        /// <summary>
        /// The crane hoistor lift operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712111", Name = "Crane, Hoist or Lift Operator", CodeSystem = "ANZSCO")]
        CraneHoistorLiftOperator,
        /// <summary>
        /// The drillers minersand shot firersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "712200", Name = "Drillers, Miners and Shot Firers nfd", CodeSystem = "ANZSCO")]
        DrillersMinersandShotFirersnfd,
        /// <summary>
        /// The driller
        /// </summary>
        [EnumMember]
        [Name(Code = "712211", Name = "Driller", CodeSystem = "ANZSCO")]
        Driller,
        /// <summary>
        /// The miner
        /// </summary>
        [EnumMember]
        [Name(Code = "712212", Name = "Miner", CodeSystem = "ANZSCO")]
        Miner,
        /// <summary>
        /// The shot firer
        /// </summary>
        [EnumMember]
        [Name(Code = "712213", Name = "Shot Firer", CodeSystem = "ANZSCO")]
        ShotFirer,
        /// <summary>
        /// The engineering production worker
        /// </summary>
        [EnumMember]
        [Name(Code = "712311", Name = "Engineering Production Worker", CodeSystem = "ANZSCO")]
        EngineeringProductionWorker,
        /// <summary>
        /// The other stationary plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "712900", Name = "Other Stationary Plant Operators nfd", CodeSystem = "ANZSCO")]
        OtherStationaryPlantOperatorsnfd,
        /// <summary>
        /// The boileror engine operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712911", Name = "Boiler or Engine Operator", CodeSystem = "ANZSCO")]
        BoilerorEngineOperator,
        /// <summary>
        /// The bulk materials handling plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712912", Name = "Bulk Materials Handling Plant Operator", CodeSystem = "ANZSCO")]
        BulkMaterialsHandlingPlantOperator,
        /// <summary>
        /// The cement production plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712913", Name = "Cement Production Plant Operator", CodeSystem = "ANZSCO")]
        CementProductionPlantOperator,
        /// <summary>
        /// The concrete batching plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712914", Name = "Concrete Batching Plant Operator", CodeSystem = "ANZSCO")]
        ConcreteBatchingPlantOperator,
        /// <summary>
        /// The concrete pump operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712915", Name = "Concrete Pump Operator", CodeSystem = "ANZSCO")]
        ConcretePumpOperator,
        /// <summary>
        /// The paperand pulp mill operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712916", Name = "Paper and Pulp Mill Operator", CodeSystem = "ANZSCO")]
        PaperandPulpMillOperator,
        /// <summary>
        /// The railway signal operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712917", Name = "Railway Signal Operator", CodeSystem = "ANZSCO")]
        RailwaySignalOperator,
        /// <summary>
        /// The train controller
        /// </summary>
        [EnumMember]
        [Name(Code = "712918", Name = "Train Controller", CodeSystem = "ANZSCO")]
        TrainController,
        /// <summary>
        /// The waste wateror water plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712921", Name = "Waste Water or Water Plant Operator", CodeSystem = "ANZSCO")]
        WasteWaterorWaterPlantOperator,
        /// <summary>
        /// The weighbridge operator
        /// </summary>
        [EnumMember]
        [Name(Code = "712922", Name = "Weighbridge Operator", CodeSystem = "ANZSCO")]
        WeighbridgeOperator,
        /// <summary>
        /// The stationary plant operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "712999", Name = "Stationary Plant Operators nec", CodeSystem = "ANZSCO")]
        StationaryPlantOperatorsnec,
        /// <summary>
        /// The mobile plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "721000", Name = "Mobile Plant Operators nfd", CodeSystem = "ANZSCO")]
        MobilePlantOperatorsnfd,
        /// <summary>
        /// The agricultural forestryand horticultural plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "721100", Name = "Agricultural, Forestry and Horticultural Plant Operators nfd", CodeSystem = "ANZSCO")]
        AgriculturalForestryandHorticulturalPlantOperatorsnfd,
        /// <summary>
        /// The agriculturaland horticultural mobile plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721111", Name = "Agricultural and Horticultural Mobile Plant Operator", CodeSystem = "ANZSCO")]
        AgriculturalandHorticulturalMobilePlantOperator,
        /// <summary>
        /// The logging plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721112", Name = "Logging Plant Operator", CodeSystem = "ANZSCO")]
        LoggingPlantOperator,
        /// <summary>
        /// The earthmoving plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "721200", Name = "Earthmoving Plant Operators nfd", CodeSystem = "ANZSCO")]
        EarthmovingPlantOperatorsnfd,
        /// <summary>
        /// The earthmoving plant operator general
        /// </summary>
        [EnumMember]
        [Name(Code = "721211", Name = "Earthmoving Plant Operator (General)", CodeSystem = "ANZSCO")]
        EarthmovingPlantOperatorGeneral,
        /// <summary>
        /// The backhoe operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721212", Name = "Backhoe Operator", CodeSystem = "ANZSCO")]
        BackhoeOperator,
        /// <summary>
        /// The bulldozer operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721213", Name = "Bulldozer Operator", CodeSystem = "ANZSCO")]
        BulldozerOperator,
        /// <summary>
        /// The excavator operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721214", Name = "Excavator Operator", CodeSystem = "ANZSCO")]
        ExcavatorOperator,
        /// <summary>
        /// The grader operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721215", Name = "Grader Operator", CodeSystem = "ANZSCO")]
        GraderOperator,
        /// <summary>
        /// The loader operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721216", Name = "Loader Operator", CodeSystem = "ANZSCO")]
        LoaderOperator,
        /// <summary>
        /// The forklift driver
        /// </summary>
        [EnumMember]
        [Name(Code = "721311", Name = "Forklift Driver", CodeSystem = "ANZSCO")]
        ForkliftDriver,
        /// <summary>
        /// The other mobile plant operatorsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "721900", Name = "Other Mobile Plant Operators nfd", CodeSystem = "ANZSCO")]
        OtherMobilePlantOperatorsnfd,
        /// <summary>
        /// The aircraft baggage handlerand airline ground crew
        /// </summary>
        [EnumMember]
        [Name(Code = "721911", Name = "Aircraft Baggage Handler and Airline Ground Crew", CodeSystem = "ANZSCO")]
        AircraftBaggageHandlerandAirlineGroundCrew,
        /// <summary>
        /// The linemarker
        /// </summary>
        [EnumMember]
        [Name(Code = "721912", Name = "Linemarker", CodeSystem = "ANZSCO")]
        Linemarker,
        /// <summary>
        /// The paving plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721913", Name = "Paving Plant Operator", CodeSystem = "ANZSCO")]
        PavingPlantOperator,
        /// <summary>
        /// The railway track plant operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721914", Name = "Railway Track Plant Operator", CodeSystem = "ANZSCO")]
        RailwayTrackPlantOperator,
        /// <summary>
        /// The road roller operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721915", Name = "Road Roller Operator", CodeSystem = "ANZSCO")]
        RoadRollerOperator,
        /// <summary>
        /// The streetsweeper operator
        /// </summary>
        [EnumMember]
        [Name(Code = "721916", Name = "Streetsweeper Operator", CodeSystem = "ANZSCO")]
        StreetsweeperOperator,
        /// <summary>
        /// The mobile plant operatorsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "721999", Name = "Mobile Plant Operators nec", CodeSystem = "ANZSCO")]
        MobilePlantOperatorsnec,
        /// <summary>
        /// The roadand rail driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "730000", Name = "Road and Rail Drivers nfd", CodeSystem = "ANZSCO")]
        RoadandRailDriversnfd,
        /// <summary>
        /// The automobile busand rail driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "731000", Name = "Automobile, Bus and Rail Drivers nfd", CodeSystem = "ANZSCO")]
        AutomobileBusandRailDriversnfd,
        /// <summary>
        /// The automobile driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "731100", Name = "Automobile Drivers nfd", CodeSystem = "ANZSCO")]
        AutomobileDriversnfd,
        /// <summary>
        /// The chauffeur
        /// </summary>
        [EnumMember]
        [Name(Code = "731111", Name = "Chauffeur", CodeSystem = "ANZSCO")]
        Chauffeur,
        /// <summary>
        /// The taxi driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731112", Name = "Taxi Driver", CodeSystem = "ANZSCO")]
        TaxiDriver,
        /// <summary>
        /// The automobile driversnec
        /// </summary>
        [EnumMember]
        [Name(Code = "731199", Name = "Automobile Drivers nec", CodeSystem = "ANZSCO")]
        AutomobileDriversnec,
        /// <summary>
        /// The busand coach driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "731200", Name = "Bus and Coach Drivers nfd", CodeSystem = "ANZSCO")]
        BusandCoachDriversnfd,
        /// <summary>
        /// The bus driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731211", Name = "Bus Driver", CodeSystem = "ANZSCO")]
        BusDriver,
        /// <summary>
        /// The charterand tour bus driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731212", Name = "Charter and Tour Bus Driver", CodeSystem = "ANZSCO")]
        CharterandTourBusDriver,
        /// <summary>
        /// The passenger coach driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731213", Name = "Passenger Coach Driver", CodeSystem = "ANZSCO")]
        PassengerCoachDriver,
        /// <summary>
        /// The trainand tram driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "731300", Name = "Train and Tram Drivers nfd", CodeSystem = "ANZSCO")]
        TrainandTramDriversnfd,
        /// <summary>
        /// The train driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731311", Name = "Train Driver", CodeSystem = "ANZSCO")]
        TrainDriver,
        /// <summary>
        /// The tram driver
        /// </summary>
        [EnumMember]
        [Name(Code = "731312", Name = "Tram Driver", CodeSystem = "ANZSCO")]
        TramDriver,
        /// <summary>
        /// The delivery driver
        /// </summary>
        [EnumMember]
        [Name(Code = "732111", Name = "Delivery Driver", CodeSystem = "ANZSCO")]
        DeliveryDriver,
        /// <summary>
        /// The truck driversnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "733100", Name = "Truck Drivers nfd", CodeSystem = "ANZSCO")]
        TruckDriversnfd,
        /// <summary>
        /// The truck driver general
        /// </summary>
        [EnumMember]
        [Name(Code = "733111", Name = "Truck Driver (General)", CodeSystem = "ANZSCO")]
        TruckDriverGeneral,
        /// <summary>
        /// The aircraft refueller
        /// </summary>
        [EnumMember]
        [Name(Code = "733112", Name = "Aircraft Refueller", CodeSystem = "ANZSCO")]
        AircraftRefueller,
        /// <summary>
        /// The furniture removalist
        /// </summary>
        [EnumMember]
        [Name(Code = "733113", Name = "Furniture Removalist", CodeSystem = "ANZSCO")]
        FurnitureRemovalist,
        /// <summary>
        /// The tanker driver
        /// </summary>
        [EnumMember]
        [Name(Code = "733114", Name = "Tanker Driver", CodeSystem = "ANZSCO")]
        TankerDriver,
        /// <summary>
        /// The tow truck driver
        /// </summary>
        [EnumMember]
        [Name(Code = "733115", Name = "Tow Truck Driver", CodeSystem = "ANZSCO")]
        TowTruckDriver,
        /// <summary>
        /// The storeperson
        /// </summary>
        [EnumMember]
        [Name(Code = "741111", Name = "Storeperson", CodeSystem = "ANZSCO")]
        Storeperson,
        /// <summary>
        /// The labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "800000", Name = "Labourers nfd", CodeSystem = "ANZSCO")]
        Labourersnfd,
        /// <summary>
        /// The cleanersand laundry workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "811000", Name = "Cleaners and Laundry Workers nfd", CodeSystem = "ANZSCO")]
        CleanersandLaundryWorkersnfd,
        /// <summary>
        /// The car detailer
        /// </summary>
        [EnumMember]
        [Name(Code = "811111", Name = "Car Detailer", CodeSystem = "ANZSCO")]
        CarDetailer,
        /// <summary>
        /// The commercial cleaner
        /// </summary>
        [EnumMember]
        [Name(Code = "811211", Name = "Commercial Cleaner", CodeSystem = "ANZSCO")]
        CommercialCleaner,
        /// <summary>
        /// The domestic cleaner
        /// </summary>
        [EnumMember]
        [Name(Code = "811311", Name = "Domestic Cleaner", CodeSystem = "ANZSCO")]
        DomesticCleaner,
        /// <summary>
        /// The housekeepersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "811400", Name = "Housekeepers nfd", CodeSystem = "ANZSCO")]
        Housekeepersnfd,
        /// <summary>
        /// The commercial housekeeper
        /// </summary>
        [EnumMember]
        [Name(Code = "811411", Name = "Commercial Housekeeper", CodeSystem = "ANZSCO")]
        CommercialHousekeeper,
        /// <summary>
        /// The domestic housekeeper
        /// </summary>
        [EnumMember]
        [Name(Code = "811412", Name = "Domestic Housekeeper", CodeSystem = "ANZSCO")]
        DomesticHousekeeper,
        /// <summary>
        /// The laundry workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "811500", Name = "Laundry Workers nfd", CodeSystem = "ANZSCO")]
        LaundryWorkersnfd,
        /// <summary>
        /// The laundry worker general
        /// </summary>
        [EnumMember]
        [Name(Code = "811511", Name = "Laundry Worker (General)", CodeSystem = "ANZSCO")]
        LaundryWorkerGeneral,
        /// <summary>
        /// The drycleaner
        /// </summary>
        [EnumMember]
        [Name(Code = "811512", Name = "Drycleaner", CodeSystem = "ANZSCO")]
        Drycleaner,
        /// <summary>
        /// The ironeror presser
        /// </summary>
        [EnumMember]
        [Name(Code = "811513", Name = "Ironer or Presser", CodeSystem = "ANZSCO")]
        IronerorPresser,
        /// <summary>
        /// The other cleanersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "811600", Name = "Other Cleaners nfd", CodeSystem = "ANZSCO")]
        OtherCleanersnfd,
        /// <summary>
        /// The carpet cleaner
        /// </summary>
        [EnumMember]
        [Name(Code = "811611", Name = "Carpet Cleaner", CodeSystem = "ANZSCO")]
        CarpetCleaner,
        /// <summary>
        /// The window cleaner
        /// </summary>
        [EnumMember]
        [Name(Code = "811612", Name = "Window Cleaner", CodeSystem = "ANZSCO")]
        WindowCleaner,
        /// <summary>
        /// The cleanersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "811699", Name = "Cleaners nec", CodeSystem = "ANZSCO")]
        Cleanersnec,
        /// <summary>
        /// The constructionand mining labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "821000", Name = "Construction and Mining Labourers nfd", CodeSystem = "ANZSCO")]
        ConstructionandMiningLabourersnfd,
        /// <summary>
        /// The buildingand plumbing labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "821100", Name = "Building and Plumbing Labourers nfd", CodeSystem = "ANZSCO")]
        BuildingandPlumbingLabourersnfd,
        /// <summary>
        /// The builders labourer
        /// </summary>
        [EnumMember]
        [Name(Code = "821111", Name = "Builder's Labourer", CodeSystem = "ANZSCO")]
        BuildersLabourer,
        /// <summary>
        /// The drainage sewerageand stormwater labourer
        /// </summary>
        [EnumMember]
        [Name(Code = "821112", Name = "Drainage, Sewerage and Stormwater Labourer", CodeSystem = "ANZSCO")]
        DrainageSewerageandStormwaterLabourer,
        /// <summary>
        /// The earthmoving labourer
        /// </summary>
        [EnumMember]
        [Name(Code = "821113", Name = "Earthmoving Labourer", CodeSystem = "ANZSCO")]
        EarthmovingLabourer,
        /// <summary>
        /// The plumbers assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "821114", Name = "Plumber's Assistant", CodeSystem = "ANZSCO")]
        PlumbersAssistant,
        /// <summary>
        /// The concreter
        /// </summary>
        [EnumMember]
        [Name(Code = "821211", Name = "Concreter", CodeSystem = "ANZSCO")]
        Concreter,
        /// <summary>
        /// The fencer
        /// </summary>
        [EnumMember]
        [Name(Code = "821311", Name = "Fencer", CodeSystem = "ANZSCO")]
        Fencer,
        /// <summary>
        /// The insulationand home improvement installersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "821400", Name = "Insulation and Home Improvement Installers nfd", CodeSystem = "ANZSCO")]
        InsulationandHomeImprovementInstallersnfd,
        /// <summary>
        /// The building insulation installer
        /// </summary>
        [EnumMember]
        [Name(Code = "821411", Name = "Building Insulation Installer", CodeSystem = "ANZSCO")]
        BuildingInsulationInstaller,
        /// <summary>
        /// The home improvement installer
        /// </summary>
        [EnumMember]
        [Name(Code = "821412", Name = "Home Improvement Installer", CodeSystem = "ANZSCO")]
        HomeImprovementInstaller,
        /// <summary>
        /// The pavingand surfacing labourer
        /// </summary>
        [EnumMember]
        [Name(Code = "821511", Name = "Paving and Surfacing Labourer", CodeSystem = "ANZSCO")]
        PavingandSurfacingLabourer,
        /// <summary>
        /// The railway track worker
        /// </summary>
        [EnumMember]
        [Name(Code = "821611", Name = "Railway Track Worker", CodeSystem = "ANZSCO")]
        RailwayTrackWorker,
        /// <summary>
        /// The structural steel construction workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "821700", Name = "Structural Steel Construction Workers nfd", CodeSystem = "ANZSCO")]
        StructuralSteelConstructionWorkersnfd,
        /// <summary>
        /// The construction rigger
        /// </summary>
        [EnumMember]
        [Name(Code = "821711", Name = "Construction Rigger", CodeSystem = "ANZSCO")]
        ConstructionRigger,
        /// <summary>
        /// The scaffolder
        /// </summary>
        [EnumMember]
        [Name(Code = "821712", Name = "Scaffolder", CodeSystem = "ANZSCO")]
        Scaffolder,
        /// <summary>
        /// The steel fixer
        /// </summary>
        [EnumMember]
        [Name(Code = "821713", Name = "Steel Fixer", CodeSystem = "ANZSCO")]
        SteelFixer,
        /// <summary>
        /// The structural steel erector
        /// </summary>
        [EnumMember]
        [Name(Code = "821714", Name = "Structural Steel Erector", CodeSystem = "ANZSCO")]
        StructuralSteelErector,
        /// <summary>
        /// The other constructionand mining labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "821900", Name = "Other Construction and Mining Labourers nfd", CodeSystem = "ANZSCO")]
        OtherConstructionandMiningLabourersnfd,
        /// <summary>
        /// The crane chaser
        /// </summary>
        [EnumMember]
        [Name(Code = "821911", Name = "Crane Chaser", CodeSystem = "ANZSCO")]
        CraneChaser,
        /// <summary>
        /// The drillers assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "821912", Name = "Driller's Assistant", CodeSystem = "ANZSCO")]
        DrillersAssistant,
        /// <summary>
        /// The lagger
        /// </summary>
        [EnumMember]
        [Name(Code = "821913", Name = "Lagger", CodeSystem = "ANZSCO")]
        Lagger,
        /// <summary>
        /// The mining support worker
        /// </summary>
        [EnumMember]
        [Name(Code = "821914", Name = "Mining Support Worker", CodeSystem = "ANZSCO")]
        MiningSupportWorker,
        /// <summary>
        /// The surveyors assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "821915", Name = "Surveyor's Assistant", CodeSystem = "ANZSCO")]
        SurveyorsAssistant,
        /// <summary>
        /// The factory process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "830000", Name = "Factory Process Workers nfd", CodeSystem = "ANZSCO")]
        FactoryProcessWorkersnfd,
        /// <summary>
        /// The food process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "831000", Name = "Food Process Workers nfd", CodeSystem = "ANZSCO")]
        FoodProcessWorkersnfd,
        /// <summary>
        /// The foodand drink factory workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "831100", Name = "Food and Drink Factory Workers nfd", CodeSystem = "ANZSCO")]
        FoodandDrinkFactoryWorkersnfd,
        /// <summary>
        /// The baking factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831111", Name = "Baking Factory Worker", CodeSystem = "ANZSCO")]
        BakingFactoryWorker,
        /// <summary>
        /// The brewery worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831112", Name = "Brewery Worker", CodeSystem = "ANZSCO")]
        BreweryWorker,
        /// <summary>
        /// The confectionery maker
        /// </summary>
        [EnumMember]
        [Name(Code = "831113", Name = "Confectionery Maker", CodeSystem = "ANZSCO")]
        ConfectioneryMaker,
        /// <summary>
        /// The dairy products maker
        /// </summary>
        [EnumMember]
        [Name(Code = "831114", Name = "Dairy Products Maker", CodeSystem = "ANZSCO")]
        DairyProductsMaker,
        /// <summary>
        /// The fruitand vegetable factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831115", Name = "Fruit and Vegetable Factory Worker", CodeSystem = "ANZSCO")]
        FruitandVegetableFactoryWorker,
        /// <summary>
        /// The grain mill worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831116", Name = "Grain Mill Worker", CodeSystem = "ANZSCO")]
        GrainMillWorker,
        /// <summary>
        /// The sugar mill worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831117", Name = "Sugar Mill Worker", CodeSystem = "ANZSCO")]
        SugarMillWorker,
        /// <summary>
        /// The winery cellar hand
        /// </summary>
        [EnumMember]
        [Name(Code = "831118", Name = "Winery Cellar Hand", CodeSystem = "ANZSCO")]
        WineryCellarHand,
        /// <summary>
        /// The foodand drink factory workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "831199", Name = "Food and Drink Factory Workers nec", CodeSystem = "ANZSCO")]
        FoodandDrinkFactoryWorkersnec,
        /// <summary>
        /// The meat bonersand slicersand slaughterersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "831200", Name = "Meat Boners and Slicers, and Slaughterers nfd", CodeSystem = "ANZSCO")]
        MeatBonersandSlicersandSlaughterersnfd,
        /// <summary>
        /// The meat bonerand slicer
        /// </summary>
        [EnumMember]
        [Name(Code = "831211", Name = "Meat Boner and Slicer", CodeSystem = "ANZSCO")]
        MeatBonerandSlicer,
        /// <summary>
        /// The slaughterer
        /// </summary>
        [EnumMember]
        [Name(Code = "831212", Name = "Slaughterer", CodeSystem = "ANZSCO")]
        Slaughterer,
        /// <summary>
        /// The meat poultryand seafood process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "831300", Name = "Meat, Poultry and Seafood Process Workers nfd", CodeSystem = "ANZSCO")]
        MeatPoultryandSeafoodProcessWorkersnfd,
        /// <summary>
        /// The meat process worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831311", Name = "Meat Process Worker", CodeSystem = "ANZSCO")]
        MeatProcessWorker,
        /// <summary>
        /// The poultry process worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831312", Name = "Poultry Process Worker", CodeSystem = "ANZSCO")]
        PoultryProcessWorker,
        /// <summary>
        /// The seafood process worker
        /// </summary>
        [EnumMember]
        [Name(Code = "831313", Name = "Seafood Process Worker", CodeSystem = "ANZSCO")]
        SeafoodProcessWorker,
        /// <summary>
        /// The packersand product assemblersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "832000", Name = "Packers and Product Assemblers nfd", CodeSystem = "ANZSCO")]
        PackersandProductAssemblersnfd,
        /// <summary>
        /// The packersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "832100", Name = "Packers nfd", CodeSystem = "ANZSCO")]
        Packersnfd,
        /// <summary>
        /// The chocolate packer
        /// </summary>
        [EnumMember]
        [Name(Code = "832111", Name = "Chocolate Packer", CodeSystem = "ANZSCO")]
        ChocolatePacker,
        /// <summary>
        /// The container filler
        /// </summary>
        [EnumMember]
        [Name(Code = "832112", Name = "Container Filler", CodeSystem = "ANZSCO")]
        ContainerFiller,
        /// <summary>
        /// The fruitand vegetable packer
        /// </summary>
        [EnumMember]
        [Name(Code = "832113", Name = "Fruit and Vegetable Packer", CodeSystem = "ANZSCO")]
        FruitandVegetablePacker,
        /// <summary>
        /// The meat packer
        /// </summary>
        [EnumMember]
        [Name(Code = "832114", Name = "Meat Packer", CodeSystem = "ANZSCO")]
        MeatPacker,
        /// <summary>
        /// The seafood packer
        /// </summary>
        [EnumMember]
        [Name(Code = "832115", Name = "Seafood Packer", CodeSystem = "ANZSCO")]
        SeafoodPacker,
        /// <summary>
        /// The packersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "832199", Name = "Packers nec", CodeSystem = "ANZSCO")]
        Packersnec,
        /// <summary>
        /// The product assembler
        /// </summary>
        [EnumMember]
        [Name(Code = "832211", Name = "Product Assembler", CodeSystem = "ANZSCO")]
        ProductAssembler,
        /// <summary>
        /// The miscellaneous factory process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "839000", Name = "Miscellaneous Factory Process Workers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousFactoryProcessWorkersnfd,
        /// <summary>
        /// The metal engineering process worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839111", Name = "Metal Engineering Process Worker", CodeSystem = "ANZSCO")]
        MetalEngineeringProcessWorker,
        /// <summary>
        /// The plasticsand rubber factory workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "839200", Name = "Plastics and Rubber Factory Workers nfd", CodeSystem = "ANZSCO")]
        PlasticsandRubberFactoryWorkersnfd,
        /// <summary>
        /// The plastics factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839211", Name = "Plastics Factory Worker", CodeSystem = "ANZSCO")]
        PlasticsFactoryWorker,
        /// <summary>
        /// The rubber factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839212", Name = "Rubber Factory Worker", CodeSystem = "ANZSCO")]
        RubberFactoryWorker,
        /// <summary>
        /// The product quality controllersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "839300", Name = "Product Quality Controllers nfd", CodeSystem = "ANZSCO")]
        ProductQualityControllersnfd,
        /// <summary>
        /// The product examiner
        /// </summary>
        [EnumMember]
        [Name(Code = "839311", Name = "Product Examiner", CodeSystem = "ANZSCO")]
        ProductExaminer,
        /// <summary>
        /// The product grader
        /// </summary>
        [EnumMember]
        [Name(Code = "839312", Name = "Product Grader", CodeSystem = "ANZSCO")]
        ProductGrader,
        /// <summary>
        /// The product tester
        /// </summary>
        [EnumMember]
        [Name(Code = "839313", Name = "Product Tester", CodeSystem = "ANZSCO")]
        ProductTester,
        /// <summary>
        /// The timberand wood process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "839400", Name = "Timber and Wood Process Workers nfd", CodeSystem = "ANZSCO")]
        TimberandWoodProcessWorkersnfd,
        /// <summary>
        /// The paperand pulp mill worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839411", Name = "Paper and Pulp Mill Worker", CodeSystem = "ANZSCO")]
        PaperandPulpMillWorker,
        /// <summary>
        /// The sawmillor timber yard worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839412", Name = "Sawmill or Timber Yard Worker", CodeSystem = "ANZSCO")]
        SawmillorTimberYardWorker,
        /// <summary>
        /// The woodand wood products factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839413", Name = "Wood and Wood Products Factory Worker", CodeSystem = "ANZSCO")]
        WoodandWoodProductsFactoryWorker,
        /// <summary>
        /// The other factory process workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "839900", Name = "Other Factory Process Workers nfd", CodeSystem = "ANZSCO")]
        OtherFactoryProcessWorkersnfd,
        /// <summary>
        /// The cementand concrete plant worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839911", Name = "Cement and Concrete Plant Worker", CodeSystem = "ANZSCO")]
        CementandConcretePlantWorker,
        /// <summary>
        /// The chemical plant worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839912", Name = "Chemical Plant Worker", CodeSystem = "ANZSCO")]
        ChemicalPlantWorker,
        /// <summary>
        /// The clay processing factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839913", Name = "Clay Processing Factory Worker", CodeSystem = "ANZSCO")]
        ClayProcessingFactoryWorker,
        /// <summary>
        /// The fabricand textile factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839914", Name = "Fabric and Textile Factory Worker", CodeSystem = "ANZSCO")]
        FabricandTextileFactoryWorker,
        /// <summary>
        /// The footwear factory worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839915", Name = "Footwear Factory Worker", CodeSystem = "ANZSCO")]
        FootwearFactoryWorker,
        /// <summary>
        /// The glass processing worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839916", Name = "Glass Processing Worker", CodeSystem = "ANZSCO")]
        GlassProcessingWorker,
        /// <summary>
        /// The hideand skin processing worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839917", Name = "Hide and Skin Processing Worker", CodeSystem = "ANZSCO")]
        HideandSkinProcessingWorker,
        /// <summary>
        /// The recycling worker
        /// </summary>
        [EnumMember]
        [Name(Code = "839918", Name = "Recycling Worker", CodeSystem = "ANZSCO")]
        RecyclingWorker,
        /// <summary>
        /// The factory process workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "839999", Name = "Factory Process Workers nec", CodeSystem = "ANZSCO")]
        FactoryProcessWorkersnec,
        /// <summary>
        /// The farm forestryand garden workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841000", Name = "Farm, Forestry and Garden Workers nfd", CodeSystem = "ANZSCO")]
        FarmForestryandGardenWorkersnfd,
        /// <summary>
        /// The aquaculture worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841111", Name = "Aquaculture Worker", CodeSystem = "ANZSCO")]
        AquacultureWorker,
        /// <summary>
        /// The crop farm workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841200", Name = "Crop Farm Workers nfd", CodeSystem = "ANZSCO")]
        CropFarmWorkersnfd,
        /// <summary>
        /// The fruitor nut farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841211", Name = "Fruit or Nut Farm Worker", CodeSystem = "ANZSCO")]
        FruitorNutFarmWorker,
        /// <summary>
        /// The fruitor nut picker
        /// </summary>
        [EnumMember]
        [Name(Code = "841212", Name = "Fruit or Nut Picker", CodeSystem = "ANZSCO")]
        FruitorNutPicker,
        /// <summary>
        /// The grain oilseedor pasture farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841213", Name = "Grain, Oilseed or Pasture Farm Worker", CodeSystem = "ANZSCO")]
        GrainOilseedorPastureFarmWorker,
        /// <summary>
        /// The vegetable farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841214", Name = "Vegetable Farm Worker", CodeSystem = "ANZSCO")]
        VegetableFarmWorker,
        /// <summary>
        /// The vegetable picker
        /// </summary>
        [EnumMember]
        [Name(Code = "841215", Name = "Vegetable Picker", CodeSystem = "ANZSCO")]
        VegetablePicker,
        /// <summary>
        /// The vineyard worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841216", Name = "Vineyard Worker", CodeSystem = "ANZSCO")]
        VineyardWorker,
        /// <summary>
        /// The mushroom picker
        /// </summary>
        [EnumMember]
        [Name(Code = "841217", Name = "Mushroom Picker", CodeSystem = "ANZSCO")]
        MushroomPicker,
        /// <summary>
        /// The crop farm workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "841299", Name = "Crop Farm Workers nec", CodeSystem = "ANZSCO")]
        CropFarmWorkersnec,
        /// <summary>
        /// The forestryand logging workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841300", Name = "Forestry and Logging Workers nfd", CodeSystem = "ANZSCO")]
        ForestryandLoggingWorkersnfd,
        /// <summary>
        /// The forestry worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841311", Name = "Forestry Worker", CodeSystem = "ANZSCO")]
        ForestryWorker,
        /// <summary>
        /// The logging assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "841312", Name = "Logging Assistant", CodeSystem = "ANZSCO")]
        LoggingAssistant,
        /// <summary>
        /// The tree faller
        /// </summary>
        [EnumMember]
        [Name(Code = "841313", Name = "Tree Faller", CodeSystem = "ANZSCO")]
        TreeFaller,
        /// <summary>
        /// The gardenand nursery labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841400", Name = "Garden and Nursery Labourers nfd", CodeSystem = "ANZSCO")]
        GardenandNurseryLabourersnfd,
        /// <summary>
        /// The garden labourer
        /// </summary>
        [EnumMember]
        [Name(Code = "841411", Name = "Garden Labourer", CodeSystem = "ANZSCO")]
        GardenLabourer,
        /// <summary>
        /// The horticultural nursery assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "841412", Name = "Horticultural Nursery Assistant", CodeSystem = "ANZSCO")]
        HorticulturalNurseryAssistant,
        /// <summary>
        /// The livestock farm workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841500", Name = "Livestock Farm Workers nfd", CodeSystem = "ANZSCO")]
        LivestockFarmWorkersnfd,
        /// <summary>
        /// The beef cattle farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841511", Name = "Beef Cattle Farm Worker", CodeSystem = "ANZSCO")]
        BeefCattleFarmWorker,
        /// <summary>
        /// The dairy cattle farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841512", Name = "Dairy Cattle Farm Worker", CodeSystem = "ANZSCO")]
        DairyCattleFarmWorker,
        /// <summary>
        /// The mixed livestock farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841513", Name = "Mixed Livestock Farm Worker", CodeSystem = "ANZSCO")]
        MixedLivestockFarmWorker,
        /// <summary>
        /// The poultry farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841514", Name = "Poultry Farm Worker", CodeSystem = "ANZSCO")]
        PoultryFarmWorker,
        /// <summary>
        /// The sheep farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841515", Name = "Sheep Farm Worker", CodeSystem = "ANZSCO")]
        SheepFarmWorker,
        /// <summary>
        /// The stablehand
        /// </summary>
        [EnumMember]
        [Name(Code = "841516", Name = "Stablehand", CodeSystem = "ANZSCO")]
        Stablehand,
        /// <summary>
        /// The wool handler
        /// </summary>
        [EnumMember]
        [Name(Code = "841517", Name = "Wool Handler", CodeSystem = "ANZSCO")]
        WoolHandler,
        /// <summary>
        /// The livestock farm workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "841599", Name = "Livestock Farm Workers nec", CodeSystem = "ANZSCO")]
        LivestockFarmWorkersnec,
        /// <summary>
        /// The mixed cropand livestock farm worker
        /// </summary>
        [EnumMember]
        [Name(Code = "841611", Name = "Mixed Crop and Livestock Farm Worker", CodeSystem = "ANZSCO")]
        MixedCropandLivestockFarmWorker,
        /// <summary>
        /// The other farm forestryand garden workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "841900", Name = "Other Farm, Forestry and Garden Workers nfd", CodeSystem = "ANZSCO")]
        OtherFarmForestryandGardenWorkersnfd,
        /// <summary>
        /// The hunter trapper
        /// </summary>
        [EnumMember]
        [Name(Code = "841911", Name = "Hunter-Trapper", CodeSystem = "ANZSCO")]
        HunterTrapper,
        /// <summary>
        /// The pest controller
        /// </summary>
        [EnumMember]
        [Name(Code = "841913", Name = "Pest Controller", CodeSystem = "ANZSCO")]
        PestController,
        /// <summary>
        /// The farm forestryand garden workersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "841999", Name = "Farm, Forestry and Garden Workers nec", CodeSystem = "ANZSCO")]
        FarmForestryandGardenWorkersnec,
        /// <summary>
        /// The food preparation assistantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "851000", Name = "Food Preparation Assistants nfd", CodeSystem = "ANZSCO")]
        FoodPreparationAssistantsnfd,
        /// <summary>
        /// The fast food cook
        /// </summary>
        [EnumMember]
        [Name(Code = "851111", Name = "Fast Food Cook", CodeSystem = "ANZSCO")]
        FastFoodCook,
        /// <summary>
        /// The food trades assistantsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "851200", Name = "Food Trades Assistants nfd", CodeSystem = "ANZSCO")]
        FoodTradesAssistantsnfd,
        /// <summary>
        /// The pastrycooks assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "851211", Name = "Pastrycook's Assistant", CodeSystem = "ANZSCO")]
        PastrycooksAssistant,
        /// <summary>
        /// The food trades assistantsnec
        /// </summary>
        [EnumMember]
        [Name(Code = "851299", Name = "Food Trades Assistants nec", CodeSystem = "ANZSCO")]
        FoodTradesAssistantsnec,
        /// <summary>
        /// The kitchenhand
        /// </summary>
        [EnumMember]
        [Name(Code = "851311", Name = "Kitchenhand", CodeSystem = "ANZSCO")]
        Kitchenhand,
        /// <summary>
        /// The other labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "890000", Name = "Other Labourers nfd", CodeSystem = "ANZSCO")]
        OtherLabourersnfd,
        /// <summary>
        /// The freight handlersand shelf fillersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "891000", Name = "Freight Handlers and Shelf Fillers nfd", CodeSystem = "ANZSCO")]
        FreightHandlersandShelfFillersnfd,
        /// <summary>
        /// The freightand furniture handlersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "891100", Name = "Freight and Furniture Handlers nfd", CodeSystem = "ANZSCO")]
        FreightandFurnitureHandlersnfd,
        /// <summary>
        /// The freight handler rail or road
        /// </summary>
        [EnumMember]
        [Name(Code = "891111", Name = "Freight Handler (Rail or Road)", CodeSystem = "ANZSCO")]
        FreightHandlerRailOrRoad,
        /// <summary>
        /// The truck drivers offsider
        /// </summary>
        [EnumMember]
        [Name(Code = "891112", Name = "Truck Driver's Offsider", CodeSystem = "ANZSCO")]
        TruckDriversOffsider,
        /// <summary>
        /// The waterside worker
        /// </summary>
        [EnumMember]
        [Name(Code = "891113", Name = "Waterside Worker", CodeSystem = "ANZSCO")]
        WatersideWorker,
        /// <summary>
        /// The shelf filler
        /// </summary>
        [EnumMember]
        [Name(Code = "891211", Name = "Shelf Filler", CodeSystem = "ANZSCO")]
        ShelfFiller,
        /// <summary>
        /// The miscellaneous labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "899000", Name = "Miscellaneous Labourers nfd", CodeSystem = "ANZSCO")]
        MiscellaneousLabourersnfd,
        /// <summary>
        /// The caretaker
        /// </summary>
        [EnumMember]
        [Name(Code = "899111", Name = "Caretaker", CodeSystem = "ANZSCO")]
        Caretaker,
        /// <summary>
        /// The deckand fishing handsnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "899200", Name = "Deck and Fishing Hands nfd", CodeSystem = "ANZSCO")]
        DeckandFishingHandsnfd,
        /// <summary>
        /// The deck hand
        /// </summary>
        [EnumMember]
        [Name(Code = "899211", Name = "Deck Hand", CodeSystem = "ANZSCO")]
        DeckHand,
        /// <summary>
        /// The fishing hand
        /// </summary>
        [EnumMember]
        [Name(Code = "899212", Name = "Fishing Hand", CodeSystem = "ANZSCO")]
        FishingHand,
        /// <summary>
        /// The handyperson
        /// </summary>
        [EnumMember]
        [Name(Code = "899311", Name = "Handyperson", CodeSystem = "ANZSCO")]
        Handyperson,
        /// <summary>
        /// The motor vehicle partsand accessories fittersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "899400", Name = "Motor Vehicle Parts and Accessories Fitters nfd", CodeSystem = "ANZSCO")]
        MotorVehiclePartsandAccessoriesFittersnfd,
        /// <summary>
        /// The motor vehicle partsand accessories fitter general
        /// </summary>
        [EnumMember]
        [Name(Code = "899411", Name = "Motor Vehicle Parts and Accessories Fitter (General)", CodeSystem = "ANZSCO")]
        MotorVehiclePartsandAccessoriesFitterGeneral,
        /// <summary>
        /// The autoglazier
        /// </summary>
        [EnumMember]
        [Name(Code = "899412", Name = "Autoglazier", CodeSystem = "ANZSCO")]
        Autoglazier,
        /// <summary>
        /// The exhaustand muffler repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "899413", Name = "Exhaust and Muffler Repairer", CodeSystem = "ANZSCO")]
        ExhaustandMufflerRepairer,
        /// <summary>
        /// The radiator repairer
        /// </summary>
        [EnumMember]
        [Name(Code = "899414", Name = "Radiator Repairer", CodeSystem = "ANZSCO")]
        RadiatorRepairer,
        /// <summary>
        /// The tyre fitter
        /// </summary>
        [EnumMember]
        [Name(Code = "899415", Name = "Tyre Fitter", CodeSystem = "ANZSCO")]
        TyreFitter,
        /// <summary>
        /// The printing assistantsand table workersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "899500", Name = "Printing Assistants and Table Workers nfd", CodeSystem = "ANZSCO")]
        PrintingAssistantsandTableWorkersnfd,
        /// <summary>
        /// The printers assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "899511", Name = "Printer's Assistant", CodeSystem = "ANZSCO")]
        PrintersAssistant,
        /// <summary>
        /// The printing table worker
        /// </summary>
        [EnumMember]
        [Name(Code = "899512", Name = "Printing Table Worker", CodeSystem = "ANZSCO")]
        PrintingTableWorker,
        /// <summary>
        /// The recyclingor rubbish collector
        /// </summary>
        [EnumMember]
        [Name(Code = "899611", Name = "Recycling or Rubbish Collector", CodeSystem = "ANZSCO")]
        RecyclingorRubbishCollector,
        /// <summary>
        /// The vending machine attendant
        /// </summary>
        [EnumMember]
        [Name(Code = "899711", Name = "Vending Machine Attendant", CodeSystem = "ANZSCO")]
        VendingMachineAttendant,
        /// <summary>
        /// The other miscellaneous labourersnfd
        /// </summary>
        [EnumMember]
        [Name(Code = "899900", Name = "Other Miscellaneous Labourers nfd", CodeSystem = "ANZSCO")]
        OtherMiscellaneousLabourersnfd,
        /// <summary>
        /// The bicycle mechanic
        /// </summary>
        [EnumMember]
        [Name(Code = "899911", Name = "Bicycle Mechanic", CodeSystem = "ANZSCO")]
        BicycleMechanic,
        /// <summary>
        /// The car park attendant
        /// </summary>
        [EnumMember]
        [Name(Code = "899912", Name = "Car Park Attendant", CodeSystem = "ANZSCO")]
        CarParkAttendant,
        /// <summary>
        /// The crossing supervisor
        /// </summary>
        [EnumMember]
        [Name(Code = "899913", Name = "Crossing Supervisor", CodeSystem = "ANZSCO")]
        CrossingSupervisor,
        /// <summary>
        /// The electricalor telecommunications trades assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "899914", Name = "Electrical or Telecommunications Trades Assistant", CodeSystem = "ANZSCO")]
        ElectricalorTelecommunicationsTradesAssistant,
        /// <summary>
        /// The leafletor newspaper deliverer
        /// </summary>
        [EnumMember]
        [Name(Code = "899915", Name = "Leaflet or Newspaper Deliverer", CodeSystem = "ANZSCO")]
        LeafletorNewspaperDeliverer,
        /// <summary>
        /// The mechanics assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "899916", Name = "Mechanic's Assistant", CodeSystem = "ANZSCO")]
        MechanicsAssistant,
        /// <summary>
        /// The railways assistant
        /// </summary>
        [EnumMember]
        [Name(Code = "899917", Name = "Railways Assistant", CodeSystem = "ANZSCO")]
        RailwaysAssistant,
        /// <summary>
        /// The sign erector
        /// </summary>
        [EnumMember]
        [Name(Code = "899918", Name = "Sign Erector", CodeSystem = "ANZSCO")]
        SignErector,
        /// <summary>
        /// The ticket collectoror usher
        /// </summary>
        [EnumMember]
        [Name(Code = "899921", Name = "Ticket Collector or Usher", CodeSystem = "ANZSCO")]
        TicketCollectororUsher,
        /// <summary>
        /// The trolley collector
        /// </summary>
        [EnumMember]
        [Name(Code = "899922", Name = "Trolley Collector", CodeSystem = "ANZSCO")]
        TrolleyCollector,
        /// <summary>
        /// The road traffic controller
        /// </summary>
        [EnumMember]
        [Name(Code = "899923", Name = "Road Traffic Controller", CodeSystem = "ANZSCO")]
        RoadTrafficController,
        /// <summary>
        /// The labourersnec
        /// </summary>
        [EnumMember]
        [Name(Code = "899999", Name = "Labourers nec", CodeSystem = "ANZSCO")]
        Labourersnec,
    }
}

using DC.Utilities.SQLDb.Helpers;
using System;

namespace ILR_Support_Tool.DEDS
{
    public class RestoreDedsDatabases: BaseDatabaseHelper
    {

        public RestoreDedsDatabases(ILogger logger):base(logger){ }
        public void Run()
        {
            //System.Threading.ThreadPool.QueueUserWorkItem(delegate
            //{
            _logger.Clear();
            _logger.Message("Restoring DES Databases.");

            new RestoreDedsDatabase(_logger).Run("LARS", Environment.CurrentDirectory + @"\DataSets\LARS.bak");
            new RestoreDedsDatabase(_logger).Run("ORGANISATION", Environment.CurrentDirectory + @"\DataSets\ORGANISATION.bak");
            new RestoreDedsDatabase(_logger).Run("ULNv2", Environment.CurrentDirectory + @"\DataSets\ULNv2.bak");
            new RestoreDedsDatabase(_logger).Run("DS_Employers", Environment.CurrentDirectory + @"\DataSets\DS_Employers.bak");
            new RestoreDedsDatabase(_logger).Run("Postcodes_List", Environment.CurrentDirectory + @"\DataSets\Postcodes_List.bak");
            //new RestoreDedsDatabase(_logger).Run("DS_ILR1516_Summarised_Actuals", Environment.CurrentDirectory + @"\DataSets\DS_ILR1516_Summarised_Actuals.bak");
            new RestoreDedsDatabase(_logger).Run("FCS-Contracts", Environment.CurrentDirectory + @"\DataSets\FCS-Contracts.bak");
            new RestoreDedsDatabase(_logger).Run("DS_EAS1718_Collection", Environment.CurrentDirectory + @"\DataSets\DS_EAS1718_Collection.bak");
            new RestoreDedsDatabase(_logger).Run("DS_OLASSEAS1617_Collection", Environment.CurrentDirectory + @"\DataSets\DS_OLASSEAS1617_Collection.bak");
            new RestoreDedsDatabase(_logger).Run("OLASS_Reference_Data", Environment.CurrentDirectory + @"\DataSets\OLASS_Reference_Data.bak");
            new RestoreDedsDatabase(_logger).Run("Validation_Messages_Reference_Data", Environment.CurrentDirectory + @"\DataSets\Validation_Messages_Reference_Data.bak");
            new RestoreDedsDatabase(_logger).Run("ESF_Supplementary_Data", Environment.CurrentDirectory + @"\DataSets\ESF_Supplementary_Data.bak");
            new RestoreDedsDatabase(_logger).Run("ESF_Contract_Reference_Data", Environment.CurrentDirectory + @"\DataSets\ESF_Contract_Reference_Data.bak");
            new RestoreDedsDatabase(_logger).Run("ESF_Funding_Data", Environment.CurrentDirectory + @"\DataSets\ESF_Funding_Data.bak");
            new RestoreDedsDatabase(_logger).Run("ESF_Summarised_Actuals", Environment.CurrentDirectory + @"\DataSets\ESF_Summarised_Actuals.bak");
            new RestoreDedsDatabase(_logger).Run("Large_Employers_Reference_Data", Environment.CurrentDirectory + @"\DataSets\Large_Employers_Reference_Data.bak");
            new RestoreDedsDatabase(_logger).Run("PostcodeFactorsReferenceData", Environment.CurrentDirectory + @"\DataSets\PostcodeFactorsReferenceData.bak");
            new RestoreDedsDatabase(_logger).Run("EFA_CoF_Removal_Reference_Data", Environment.CurrentDirectory + @"\DataSets\EFA_CoF_Removal_Reference_Data.bak");
            new RestoreDedsDatabase(_logger).Run("ONS_Postcode_Directory", Environment.CurrentDirectory + @"\DataSets\ONS_Postcode_Directory.bak");
            new RestoreDedsDatabase(_logger).Run("DS_ILR1718_Summarised_Actuals", Environment.CurrentDirectory + @"\DataSets\DS_ILR1718_Summarised_Actuals.bak");
            new RestoreDedsDatabase(_logger).Run("DAS_CommitmentsReferenceData", Environment.CurrentDirectory + @"\DataSets\DAS_CommitmentsReferenceData.bak");
            new RestoreDedsDatabase(_logger).Run("DAS_EarningsHistoryReferenceData", Environment.CurrentDirectory + @"\DataSets\DAS_EarningsHistoryReferenceData.bak");
            new RestoreDedsDatabase(_logger).Run("DAS_ProviderEvents", Environment.CurrentDirectory + @"\DataSets\DAS_ProviderEvents.bak");
			new RestoreDedsDatabase(_logger).Run("Collections_Calendar", Environment.CurrentDirectory + @"\DataSets\Collections_Calendar.bak");
            new RestoreDedsDatabase(_logger).Run("DAS_PeriodEnd", Environment.CurrentDirectory + @"\DataSets\DAS_PeriodEnd.bak");
            new RestoreDedsDatabase(_logger).Run("DAS_LevyAccountsReferenceData", Environment.CurrentDirectory + @"\DataSets\DAS_LevyAccountsReferenceData.bak");
            new RestoreDedsDatabase(_logger).Run("EPAReferenceData", Environment.CurrentDirectory + @"\DataSets\EPAReferenceData.bak");
            _logger.Message("All DES Databases Restored.");
            //}, null);
        }


        public string OrgTestData()
        {
            return
               @"insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000001, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000002, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000003, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000004, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000005, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000006, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000007, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000008, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000009, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000010, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000011, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000012, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')
                insert into ORGANISATION1415.Core.ORG_Details (UKPRN,Name,Created_On,Created_By,Modified_On,Modified_By) values(20000013, 'TEST PROVIDER',getDate(),'gdenham',getDate(),'gdenham')

                ;with Factors (C1,C2,C3,C4) as
                (
	                select '16-19 Learner Responsive Funding','HISTORIC AREA COST FACTOR','EFA 16-19','1.03548'
	                union all select '16-19 Learner Responsive Funding','HISTORIC DISADVANTAGE FUNDING PROPORTION', 'EFA 16-19', '0.14110'
	                union all select '16-19 Learner Responsive Funding','HISTORIC PROGRAMME COST WEIGHTING FACTOR', 'EFA 16-19', '1.02993'
	                union all select '16-19 Learner Responsive Funding', 'HISTORIC RETENTION FACTOR', 'EFA 16-19', '0.96676'
	                union all select 'Adult Skills (College)', 'SPECIALIST RESOURCES', 'Adult Skills', '1'
	                union all select '16-19 Learner Responsive Funding','SPECIALIST RESOURCES', 'EFA 16-19', '1'
	                union all select 'Adult Skills (College)', 'TRANSITION FACTOR', 'Adult Skills', '1.000000'
                ), UKPRNs as (select 20000001 as UKPRN union all select UKPRN+1 from UKPRNs where UKPRN <20000013)
                insert into ORGANISATION1415.Core.ORG_Funding(
		                [UKPRN],
		                [FundModelName],
		                [FundingFactor],
		                [FundingFactorType],
		                [FundingFactorValue],
		                [EffectiveFrom],
		                [Created_On],
		                [Created_By],
		                [Modified_On],
		                [Modified_By]
	                )
                select UKPRN,Factors.C1,Factors.C2,Factors.C3,Factors.C4,'1-aug-2014',getDate(),'gdenham',getDate(),'gdenham' from Factors cross join UKPRNs";
        }        
    }
}



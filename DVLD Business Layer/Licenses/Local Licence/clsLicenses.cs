﻿using DVLD_Database_Layer.Licenses.Drivers;
using DVLD_Database_Layer.Licenses.Local_Licence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business_Layer.Licenses.Local_Licence.clsLicenses;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Business_Layer.Licenses.Local_Licence
{
    public class clsLicenses
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public bool IsActive { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public enum Mode { New = 1, Update = 2 }
        public Mode enMode { get; set; }
        public clsLicenses(int applicationID, int driverID, int licenseClass,
            int issueReason, int createdByUserID, DateTime expirationDate, string notes, float paidFees)
        {
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IsActive = true;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            IssueDate = DateTime.Now;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            enMode = Mode.New;
        }

        private clsLicenses(int licenseID, int applicationID, int driverID, int licenseClass, bool isActive,
            int issueReason, int createdByUserID, DateTime issueDate, DateTime expirationDate, string notes,
            float paidFees)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            this.enMode = Mode.Update;
        }

        public enum IssueReasons
        {
            FirstTime = 1,
            Renew,
            ReplacementforDamaged,
            ReplacementforLost
        }
        private bool AddNewLicense()
        {
            this.LicenseID = clsLicensesDB.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
             this.IssueReason, this.CreatedByUserID, this.ExpirationDate, this.Notes, this.PaidFees);

            return this.LicenseID != -1;
        }

        public static clsLicenses GetLicenseByID(int licenseID)
        {
            int applicationID = 0; int driverID = 0; int licenseClass = 0; bool isActive = false;
            int issueReason = 0; int createdByUserID = 0; DateTime issueDate = DateTime.Now; DateTime expirationDate = DateTime.Now;
            string notes = ""; float paidFees = 0;

            if (clsLicensesDB.GetLicenseByID(licenseID, ref applicationID, ref driverID, ref licenseClass,
            ref isActive, ref issueReason, ref createdByUserID, ref issueDate, ref expirationDate, ref notes, ref paidFees))
                return new clsLicenses(licenseID, applicationID, driverID, licenseClass,
             isActive, issueReason, createdByUserID, issueDate, expirationDate, notes, paidFees);
            return null;
        }

        private bool UpdateLicense()
        {
            return clsLicensesDB.UdpateLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
             IsActive, IssueReason, CreatedByUserID, IssueDate, ExpirationDate, Notes, PaidFees);
        }

        public bool SaveLicense()
        {
            switch (enMode)
            {
                case Mode.New:
                    enMode = Mode.Update;
                    return AddNewLicense();
                default:
                    return UpdateLicense();
            }
        }

        public static DataTable GetLicenseByApplicationID(int applicationID)
        {
            return clsLicensesDB.GetLicenseByApplicationID(applicationID);
        }

        public static DataTable GetLicensesByDriverID(int driverID)
        {
            return clsLicensesDB.GetLicensesByDriverID(driverID);
        }

        public static DataTable GetLicenseByLicenseID(int licenseID)
        {
            return clsLicensesDB.GetLicenseByLicenseID(licenseID);
        }
    }
}

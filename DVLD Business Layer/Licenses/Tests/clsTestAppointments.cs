﻿using DVLD_Business_Layer.Licenses.Local_License;
using DVLD_Database_Layer.Licenses.Tests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer.Licenses.Tests
{
    public class clsTestAppointments
    {
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }
       public Mode enMode { get; set; }
        public enum Mode
        {
            New = 1,
            Update
        }
        public clsTestAppointments()
        {
            TestAppointmentID = 0;
            TestTypeID = 0;
            LocalDrivingLicenseApplicationID = 0;
            CreatedByUserID = 0;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            RetakeTestApplicationID = 0;
            IsLocked = false;
            enMode = Mode.New;
        }

        

        private bool AddNewAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsDB.AddNewAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.CreatedByUserID,
            this.RetakeTestApplicationID, this.AppointmentDate, this.PaidFees, this.IsLocked);

            return this.TestAppointmentID != -1;
        }

        public bool SaveAppointment()
        {
            switch (enMode)
            {
                case Mode.New:
                    enMode = Mode.Update;
                    return AddNewAppointment();
                default:
                    return false;
            }
        }


        public static DataTable GetAppointmentDetails(int localDrivingAppID)
        {
            return clsTestAppointmentsDB.GetAppointmentDetails(localDrivingAppID);
        }

        public static DataTable GetAppointmentFullDetails(int localDrivingAppID, int testTypeID)
        {
            return clsTestAppointmentsDB.GetAppointmentFullDetails(localDrivingAppID, testTypeID);  
        }

    }
}

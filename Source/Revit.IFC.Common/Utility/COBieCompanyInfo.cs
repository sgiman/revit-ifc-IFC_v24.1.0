﻿/*******************************************************************************
 * COBieCompanyInfo.cs
 * 
 * Autodesk Revit 24.0.4.427 (ENU) - IFC 24.1.1.6 (IFC import/Export) 
 * https://github.com/Autodesk/revit-ifc/releases
 *
 * -----------------------------------------------------------------------------
 * Create Build (API REVIT 2024) 
 * Application (add-ins)
 * -----------------------------------------------------------------------------
 * Visual Studio 2022 
 * C# | .NET 4.8
 * ----------------------------------------------------------------------------- 
 * Writing sgiman @ 2023 
 *******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Revit.IFC.Common.Utility
{
   public class COBieCompanyInfo
   {
      public string CompanyType { get; set; }
      public string CompanyName { get; set; }
      public string StreetAddress { get; set; }
      public string City { get; set; }
      public string State_Region { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
      public string CompanyPhone { get; set; }
      public string CompanyEmail { get; set; }

      public COBieCompanyInfo()
      {

      }

      public COBieCompanyInfo(string compInfoStr)
      {
         if (!string.IsNullOrEmpty(compInfoStr))
         {
            JavaScriptSerializer js = new JavaScriptSerializer();
            COBieCompanyInfo compInfo = js.Deserialize<COBieCompanyInfo>(compInfoStr);
            CompanyType = compInfo.CompanyType;
            CompanyName = compInfo.CompanyName;
            StreetAddress = compInfo.StreetAddress;
            City = compInfo.City;
            State_Region = compInfo.State_Region;
            PostalCode = compInfo.PostalCode;
            Country = compInfo.Country;
            CompanyPhone = compInfo.CompanyPhone;
            CompanyEmail = compInfo.CompanyEmail;
         }
      }

      public bool EmailValidator()
      {
         Regex emailRegex = new Regex("([A-Z0-9a-z_(-)(.)]+)@([A-Z0-9a-z_(-)]+)(.[A-Z0-9a-z_(-)]+)+");
         Match m = emailRegex.Match(CompanyEmail);
         if (m.Success)
            return true;
         return false;
      }

      public bool PhoneValidator()
      {
         Regex phoneRegex = new Regex("(?<IDDCode>[+][0-9]{1,3}[ ]*)?(?<AreaCode>[(][0-9]{1,4}[)])?[ ]*(?<PhoneNumber>[0-9]{3,5}(?:-|[ ]*)[0-9]{3,4})");
         Match m = phoneRegex.Match(CompanyPhone);
         if (m.Success)
            return true;
         return false;
      }

      public string ToJsonString()
      {
         JavaScriptSerializer js = new JavaScriptSerializer();
         return js.Serialize(this);
      }
   }
}
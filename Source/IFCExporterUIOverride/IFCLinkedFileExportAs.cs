﻿/*******************************************************************************
 * IFCLinkedFileExportAs.cs
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
 ********************************************************************************/ 
//
// BIM IFC export alternate UI library: this library works with Autodesk(R) Revit(R) to provide an alternate user interface for the export of IFC files from Revit.
// Copyright (C) 2016  Autodesk, Inc.
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
//
using BIM.IFC.Export.UI.Properties;
using Revit.IFC.Export.Utility;

namespace BIM.IFC.Export.UI
{
   public class IFCLinkedFileExportAs
   {

      public LinkedFileExportAs ExportAs { get; set; }

      public IFCLinkedFileExportAs(LinkedFileExportAs exportAs)
      {
         ExportAs = exportAs;
      }

      public override string ToString()
      {
         switch (ExportAs)
         {
            case LinkedFileExportAs.DontExport:
               return Resources.LinkedFilesDontExport;
            case LinkedFileExportAs.ExportAsSeparate:
               return Resources.LinkedFilesSeparate;
            case LinkedFileExportAs.ExportSameProject:
               return Resources.LinkedFilesSameProject;
            case LinkedFileExportAs.ExportSameSite:
               return Resources.LinkedFilesSameSite;
            default:
               return Resources.LinkedFilesDontExport;
         }
      }
   }
}

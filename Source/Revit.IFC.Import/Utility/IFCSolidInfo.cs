﻿/*******************************************************************************
 * ........
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
//
// Revit IFC Import library: this library works with Autodesk(R) Revit(R) to import IFC files.
// Copyright (C) 2013  Autodesk, Inc.
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

using Autodesk.Revit.DB;
using Revit.IFC.Common.Enums;

namespace Revit.IFC.Import.Utility
{
   /// <summary>
   /// A class that contains the GeometryObject, Id, and material information for created Revit geometry.
   /// </summary>
   /// <remarks>TODO: Rename to IFCGeometryObjectInfo, as it can contain Solids, Meshes, 
   /// Curves and Points.</remarks>
   public class IFCSolidInfo
   {
      /// <summary>
      /// The id of the geometry.
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// The representation that created the geometry.
      /// </summary>
      public IFCRepresentationIdentifier RepresentationIdentifier { get; set; }

      /// <summary>
      /// The created geometry.
      /// </summary>
      public GeometryObject GeometryObject { get; set; }

      protected IFCSolidInfo()
      {
         Id = -1;
         RepresentationIdentifier = IFCRepresentationIdentifier.Other;
         GeometryObject = null;
      }

      protected IFCSolidInfo(int id, GeometryObject geometryObject)
      {
         Id = id;
         GeometryObject = geometryObject;
      }

      /// <summary>
      /// Create an IFCSolidInfo from the created geometry.
      /// </summary>
      /// <param name="id">The id associated with the geometry in the IFC file.</param>
      /// <param name="geometryObject">The created geometry.</param>
      /// <returns>The IFCSolidInfo class.</returns>
      /// <remarks>The RepresentationIdentifier is intended to be added in the AddGeometry function call.</remarks>
      public static IFCSolidInfo Create(int id, GeometryObject geometryObject)
      {
         return new IFCSolidInfo(id, geometryObject);
      }
   }
}
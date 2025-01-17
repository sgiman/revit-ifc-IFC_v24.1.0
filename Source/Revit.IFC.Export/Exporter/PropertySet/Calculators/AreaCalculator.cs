﻿/*******************************************************************************
 * AreaCalculator.cs
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
// BIM IFC library: this library works with Autodesk(R) Revit(R)
// to export IFC files containing model geometry.
// Copyright (C) 2012  Autodesk, Inc.
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
using Autodesk.Revit.DB.IFC;
using Revit.IFC.Export.Utility;
using Revit.IFC.Common.Utility;

namespace Revit.IFC.Export.Exporter.PropertySet.Calculators
{
   /// <summary>
   /// A calculation class to calculate area for a Door.
   /// </summary>
   class AreaCalculator : PropertyCalculator
   {
      /// <summary>
      /// A double variable to keep the calculated value.
      /// </summary>
      private double m_Area = 0;

      /// <summary>
      /// The AreaCalculator instance.
      /// </summary>
      public static AreaCalculator Instance { get; } = new AreaCalculator();

      /// <summary>
      /// Calculates the area.
      /// </summary>
      /// <param name="exporterIFC">The ExporterIFC object.</param>
      /// <param name="extrusionCreationData">The IFCExportBodyParams.</param>
      /// <param name="element">The element to calculate the value.</param>
      /// <param name="elementType">The element type.</param>
      /// <returns>True if the operation succeed, false otherwise.</returns>
      public override bool Calculate(ExporterIFC exporterIFC, IFCExportBodyParams extrusionCreationData, Element element, ElementType elementType, EntryMap entryMap)
      {
         double height = 0.0;
         double width = 0.0;

         ElementId categoryId = CategoryUtil.GetSafeCategoryId(element);

         // Work for Door element
         if (categoryId == new ElementId(BuiltInCategory.OST_Doors))
         {
            if ((ParameterUtil.GetDoubleValueFromElementOrSymbol(element, BuiltInParameter.DOOR_HEIGHT, out height) != null) &&
                  (ParameterUtil.GetDoubleValueFromElementOrSymbol(element, BuiltInParameter.DOOR_WIDTH, out width) != null))
            {
               m_Area = UnitUtil.ScaleArea(height * width);
               return true;
            }
         }
         // Work for Window element
         else if (categoryId == new ElementId(BuiltInCategory.OST_Windows))
         {
            if ((ParameterUtil.GetDoubleValueFromElementOrSymbol(element, BuiltInParameter.WINDOW_HEIGHT, out height) != null) &&
                  (ParameterUtil.GetDoubleValueFromElementOrSymbol(element, BuiltInParameter.WINDOW_WIDTH, out width) != null))
            {
               m_Area = UnitUtil.ScaleArea(height * width);
               return true;
            }
         }
         else if (categoryId == new ElementId(BuiltInCategory.OST_Ceilings) || categoryId == new ElementId(BuiltInCategory.OST_Floors)
            || element is Floor)
         {
            if (ParameterUtil.GetDoubleValueFromElementOrSymbol(element, BuiltInParameter.HOST_AREA_COMPUTED, out height) != null)
            {
               m_Area = UnitUtil.ScaleArea(height * width);
               return true;
            }
         }

         // If no value from the above, consider the parameter override
         if (ParameterUtil.GetDoubleValueFromElementOrSymbol(element, entryMap.RevitParameterName, out m_Area, entryMap.CompatibleRevitParameterName, "IfcQtyArea") != null)
         {
            m_Area = UnitUtil.ScaleArea(m_Area);
            if (m_Area > MathUtil.Eps() * MathUtil.Eps())
               return true;
         }

         // Work for Space element or other element that has extrusion
         m_Area = extrusionCreationData?.ScaledArea ?? 0.0;
         return m_Area > MathUtil.Eps() * MathUtil.Eps();
      }

      /// <summary>
      /// Gets the calculated double value.
      /// </summary>
      /// <returns>
      /// The double value.
      /// </returns>
      public override double GetDoubleValue()
      {
         return m_Area;
      }
   }
}

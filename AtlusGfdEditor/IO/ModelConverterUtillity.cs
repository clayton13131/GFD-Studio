﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlusGfdEditor.FormatModules;
using AtlusGfdEditor.GUI.Forms;
using AtlusGfdLib;

namespace AtlusGfdEditor.IO
{
    public static class ModelConverterUtillity
    {
        public static Model ConvertAssimpModel()
        {
            using ( var dialog = new OpenFileDialog() )
            {
                dialog.AutoUpgradeEnabled = true;
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                dialog.Filter = ModuleFilterGenerator.GenerateFilter( FormatModuleUsageFlags.ImportForEditing, typeof( Assimp.Scene ) );
                dialog.Multiselect = false;
                dialog.SupportMultiDottedExtensions = true;
                dialog.Title = "Select an Assimp model.";
                dialog.ValidateNames = true;

                if ( dialog.ShowDialog() != DialogResult.OK )
                {
                    return null;
                }

                return ConvertAssimpModel( dialog.FileName );
            }
        }

        public static Model ConvertAssimpModel( string path )
        {
            using ( var dialog = new ModelConverterOptionsDialog( false ) )
            {
                if ( dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK )
                    return null;

                ModelConverterOptions options = new ModelConverterOptions()
                {
                    MaterialPreset = dialog.MaterialPreset,
                    Version = dialog.Version,
                    ConvertSkinToZUp = dialog.ConvertSkinToZUp,
                    GenerateVertexColors = dialog.GenerateVertexColors
                };

                return ModelConverter.ConvertFromAssimpScene( path, options );
            }
        }
    }
}
﻿using GFDLibrary.IO.Common;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace GFDLibrary.Textures.Texpack
{
    public class MetaphorTexpack
    {
        public Dictionary<string, byte[]> TextureList;
        public MetaphorTexpack( Stream stream )
        {
            TextureList = new();
            using (var reader = new EndianBinaryReader(stream, Encoding.Default, Endianness.BigEndian))
            {
                var textureCount = reader.ReadInt32();
                for (int i = 0; i < textureCount; i++ )
                {
                    string CurrTextureName = reader.ReadString( StringBinaryFormat.FixedLength, 0x100 );
                    var textureFileSize = reader.ReadInt32();
                    byte[] CurrTextureData = reader.ReadBytes( textureFileSize );
                    TextureList.Add( CurrTextureName, CurrTextureData );
                }
            }
        }
    }
}
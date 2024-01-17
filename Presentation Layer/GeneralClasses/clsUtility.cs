using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GeneralClasses
{
    internal class clsUtility
    {
        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }

        public static bool CreateFolderIfNotExist(string path)
        {
            if (!File.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Failed to create folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceFileName)
        {
            FileInfo fi = new FileInfo(SourceFileName);

            string Extention = fi.Extension;

            return GenerateGUID() + Extention;

        }

        public static bool CopyImageToFolder(ref string SourceFileName)
        {
            string DestinationFolder = "C:\\Users\\DELL\\Desktop\\My-GitHub\\Driving-License-Management-System\\People Images\\";

            if(!CreateFolderIfNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFileName = DestinationFolder + ReplaceFileNameWithGUID(SourceFileName);

            try
            {
                File.Copy(SourceFileName, DestinationFileName, true);
            }
            catch(IOException IO)
            {
                MessageBox.Show(IO.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFileName = DestinationFileName;
            return true;
        }
    
    }
}

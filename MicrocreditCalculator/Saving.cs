using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrocreditCalculator
{
    class Saving
    {
        public static void SavingMetod(string forSave)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                string FilePath = saveFileDialog.FileName;
                using (StreamWriter sw = new StreamWriter(FilePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(forSave);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eksamensprojekt
{
    [Serializable()]
    class Quiz
    {
        public String Name;
        public String Description;
        public IList<QuestionReference> QuestionReferences;
        public IList<ImageReference> ImageReferences;

        public static Quiz LoadFromZip(String path)
        {
            ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read);
            ZipArchiveEntry quizEntry = archive.GetEntry("quiz.xml");

            Quiz quiz = null;
            using (Stream stream = quizEntry.Open())
            {
                XmlSerializer x = new XmlSerializer(typeof(Quiz));
                quiz = (Quiz)x.Deserialize(stream);
            }

            return quiz;
        }

    }

    [Serializable()]
    class QuestionReference
    {
        public String Path;
    }

    [Serializable()]
    class ImageReference
    {
        public String Path;
    }
}
